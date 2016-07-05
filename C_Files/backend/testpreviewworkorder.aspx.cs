using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using CfsNamespace;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;
public partial class backend_testpreviewworkorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select;


        select = "SELECT c.FirstName, c.LastName, e.EventDate, e.LocationName, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins, tj.StartDateTime ";
        select += "FROM Customer c JOIN Event e on c.CustomerId = e.CustomerId ";
        select += "JOIN Job j on e.EventId = j.EventId ";
        select += "LEFT OUTER JOIN TalentToJob tj on j.JobId = tj.JobId ";
        select += "WHERE e.EventDate >= '" + DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM' ";
        select += "AND e.EventDate <= '" + CfsCommon.GetEndOfCurrentWeek() + "' ";
        select += "AND j.IsJobCancelled = 0 ";
        select += "AND (j.IsBalanceCollected = 0 OR j.IsJobComplete = 0)";
        select += "GROUP BY c.FirstName, c.LastName, e.EventDate, e.LocationName, tj.StartDateTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins ORDER BY e.EventDate ASC, tj.StartDateTime ASC;";


        SqlDataSource dataSrc = new SqlDataSource(conn, select);
        
        DataSourceSelectArguments args = new DataSourceSelectArguments();
        DataView view = (DataView)dataSrc.Select(args);
        DataTable dt = view.ToTable();

        GridView1.DataSource = dataSrc;
        GridView1.DataBind();

    }
}