using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

using System.Globalization;
using System.Data;
using System.Data.Objects;
using CfsNamespace;

public partial class backend_view_accounting : System.Web.UI.Page
{
    private string SQL_CONN = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
    private const int ACCOUNTING_START_YEAR = 2003;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.Accounting, Session, Response);

        int thisYear = DateTime.Now.Year;
        int yearBook, yearGross, yearNet;
        int totalBook=0, totalGross=0, totalNet=0;

        for (int year = thisYear; year >= ACCOUNTING_START_YEAR; year--)
        {
            divContent.InnerHtml += CalculateYear(year, out yearGross, out yearNet, out yearBook);

            totalBook += yearBook;
            totalGross += yearGross;
            totalNet += yearNet;
        }

        lblGrossToDate.Text = totalGross.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
        lblNetToDate.Text = totalNet.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
        lblTtlBookings.Text = totalBook.ToString("0", CultureInfo.CreateSpecificCulture("en-US"));
    }

    private string CalculateYear(int year, out int totalGross, out int totalNet, out int totalBookings)
    {
        /* OUT params */
        totalGross = 0;
        totalNet = 0;
        totalBookings = 0;
        
        /* Indexes into my Array */
        const int TOTAL_GROSS = 0;
        const int TOTAL_NET = 1;
        const int TOTAL_BOOK = 2;

        /* Array:
         * First element is the month: 1-12 (0 not used) 
         * Second element is the Gross, Net, or Bookings. (See consts, directly above)
         */
        int[,] sum = new int[13, 3];

        string select;

        /* Start Totals at 0 */
        for (int i = 0; i < 13; i++)
        {
            sum[i, TOTAL_GROSS] = 0;
            sum[i, TOTAL_NET] = 0;
            sum[i, TOTAL_BOOK] = 0;
        }

        /* Get the data for the current year */
        select  = "SELECT * FROM Event e, Job j ";
        select += "WHERE e.EventId = j.EventId AND ";
        select += "j.IsJobComplete = 'true' AND j.IsJobCancelled = 'false' ";
        select += "AND e.EventDate >= '" + year.ToString() + "-01-01' AND e.EventDate <= '" + year.ToString() + "-12-31' ";
        select += "ORDER BY e.EventDate ASC;";

        SqlConnection conn = new SqlConnection(SQL_CONN);
        SqlCommand cmd = new SqlCommand(select, conn);
        SqlDataReader sRead;

        conn.Open();
        sRead = cmd.ExecuteReader();

        if (sRead.HasRows)
        {
            /* Read each data, and add to the proper month */
            while (sRead.Read())
            {
                DateTime evtDate = (DateTime)sRead["EventDate"];

                sum[evtDate.Month, TOTAL_GROSS] += (int)sRead["GrossIncome"];
                sum[evtDate.Month, TOTAL_NET] += (int)sRead["OfficeNet"];
                sum[evtDate.Month, TOTAL_BOOK]++;
            }        
        }

        sRead.Close();
        conn.Close();

        for (int i = 1; i <= 12; i++)
        {
            totalGross += sum[i, TOTAL_GROSS];
            totalNet += sum[i, TOTAL_NET];
            totalBookings += sum[i, TOTAL_BOOK];
        }

        string output="";
        int month = 0;

        for (int q = 1; q <= 4; q++) /* q if for 'Quarter' */
        {
            output += "<table cellspacing=\"0\" class=\"accouting\" >\r\n";
            output += "<tr>\r\n";
            output += "  <th colspan=\"4\" scope=\"col\" >Q" + q.ToString() + " " + year.ToString() + "</th>\r\n";
            output += "</tr>\r\n";
            output += "<tr>\r\n";
            output += "  <th scope=\"col\" >Month</th>\r\n";
            output += "  <th scope=\"col\" >Gross</th>\r\n";
            output += "  <th scope=\"col\" >Net</th>\r\n";
            output += "  <th scope=\"col\" >Total Book</th>\r\n";
            output += "</tr>\r\n";

            for (int i = 0; i < 3; i++)
            {
                month++;

                output += "<tr>\r\n";
                output += "  <td>" + convertMonth(month) + "</td>";
                output += "  <td>" + sum[month, TOTAL_GROSS].ToString("C0", CultureInfo.CreateSpecificCulture("en-US")) + "</td>\r\n";
                output += "  <td>" + sum[month, TOTAL_NET].ToString("C0", CultureInfo.CreateSpecificCulture("en-US")) + "</td>\r\n";
                output += "  <td>" + sum[month, TOTAL_BOOK].ToString() + "</td>\r\n";
                output += "</tr>\r\n";
            }

            output += "</table>\r\n";
        }

        output += "<div style='padding: 15px 0px 0px 3px; float: left;' >";
        output += "<label style='font-weight: bold;'>" + year.ToString() + " Totals - </label>";
        output += "<label>";
        output += totalGross.ToString("C0", CultureInfo.CreateSpecificCulture("en-US")) + " Gross | ";
        output += totalNet.ToString("C0", CultureInfo.CreateSpecificCulture("en-US")) + " Net | ";
        output += string.Format("{0} Bookings</label></div>", totalBookings);

        output += "<div class=\"cleaner\" ></div>\r\n<hr/>";


        
        return output;

    }

    private string convertMonth(int month)
    {
        DateTime dt = DateTime.Parse("1900-" + month.ToString() + "-01");

        return dt.ToString("MMMM");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=Accounting.xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        divexport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    //don’t forget to add this method , other wise we will get Control gv of type 

    //'GridView' must be placed inside a form tag with runat=server.

    
    public override void VerifyRenderingInServerForm(Control control)
    {
    } 
}
