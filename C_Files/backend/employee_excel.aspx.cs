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
public partial class backend_employee_excel : System.Web.UI.Page
{
    string pass = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["type"] != null)
        {
            string type = Request["type"].ToString();
            create_email(type);
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }
    protected void create_email(string type)
    {

        CfsEntity cfsEntity = new CfsEntity();
        List<Talent> list;


        

        try
        {

            string whereStmt = "";
            
            if (!string.IsNullOrEmpty(type))
            {
                //whereStmt += "AND t.TalentType LIKE '%" + type + "%' ";

                whereStmt += "AND it.TalentType LIKE '%" + type + "%' ";
                if (type == "female")
                {
                    whereStmt += "AND it.TalentType NOT LIKE '%minifemale%' ";
                }
                if (type == "male")
                {
                    whereStmt += "AND it.TalentType NOT LIKE '%minimale%' ";
                    whereStmt += "AND it.TalentType NOT LIKE '%female%' ";
                    whereStmt += "AND it.TalentType NOT LIKE '%minifemale%' ";
                }
               
            }
            list = ((ObjectQuery<Talent>)cfsEntity.Talent.Where("it.IsActive = true " + whereStmt)).ToList();




            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DateTime time = DateTime.Now;             // Use current time.
            string format = "MMM ddd d HH:mm yyyy";  
            // Use this format.
            
            string append = time.ToString(format).Replace('/',' ' ); // Write to console.
            append = append.Replace('-', ' ');
            append = append.Replace(':', ' ');
            append = append.Replace(' ', '_');
            append = append.Trim();
            string filePath = Server.MapPath("~//media//") + "mycsvfile_"+type+"_"+append+".csv";
            pass = "mycsvfile_" + type + "_" + append + ".csv";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            else
            {
                File.Delete(filePath);

                File.Create(filePath).Close();
            }
            
            string delimter = ",";
            List<string[]> output = new List<string[]>();

            //flexible part ... add as many object as you want based on your app logic

       /*     
1.       Stage Name

2.       Real Name

3.       Cell Phone

4.       Home Phone

5.       Email Address

6.       Alternate Email Address

7.       Mailing Address
        * */

            output.Add(new string[] { "Stage Name", "Real Name", "Cell Phone", "Home Phone", "Email Address", "Mailing Address" });
            int i = 0;

            List<CommaOutputEntertainer> commalist = new List<CommaOutputEntertainer>();

            foreach (Talent t in list)
            {
                if (t.Address1 == null)
                    t.Address1 = "";
                if (t.Address2 == null)
                    t.Address2 = "";

                if (t.DisplayName == null)
                    t.DisplayName = "";
                if (t.SpecialNotes == null)
                    t.SpecialNotes = "";
                if (t.State == null)
                    t.State = "";
                try
                {
                    CommaOutputEntertainer D = new CommaOutputEntertainer();
                   // D.Alternate_Email_Address = t.DisplayName.Replace(",", "-");
                    D.Cell_Phone = t.CellPhone;
                    D.Email_Address = t.EmailPrimary;
                    D.Home_Phone = t.HomePhone;
                    D.Mailing_Address = t.Address1.Replace(",", "-") + " " + t.Address2.Replace(",", "-") + " " + t.City.Replace(",", "-") + " " + t.State + " " + t.Zip;
                    D.Real_Name = t.FirstName + " " + t.LastName ;
                    D.Stage_Name = t.DisplayName.Replace(",", "-");
                   
                    
                    commalist.Add(D);

                }

                catch (Exception exxx)
                {
                    string ddddd = t.DisplayName;

                }


            }
            string s = "";


            try
            {
                foreach (CommaOutputEntertainer D in commalist)
                {

                  
                    string[] h = new string[]
                {
                    D.Stage_Name,
                    D.Real_Name,
                    D.Cell_Phone,
                    D.Home_Phone,
                     D.Email_Address,
                    
                    D.Mailing_Address
                };
                
                        try
                        {
                            output.Add(h);
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                }
            
            catch (Exception ex)
            {
                string g = "";
            }
           

            int length = output.Count;

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {

                for (int index = 0; index < length; index++)
                {
                    writer.WriteLine(string.Join(delimter, output[index]));
                }
            }
        }
        catch (Exception ex)
        { }
        pass = Server.HtmlEncode(pass);
        Response.Redirect("TalentExcel.aspx?pass="+pass);

    }
    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    
}