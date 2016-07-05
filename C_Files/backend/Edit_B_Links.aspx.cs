using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;
using System.Data;

public partial class backend_Edit_B_Links : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("~//backend/BusinessLinks.xml"));
            // dataSet.ReadXml(xDoc);

            //AuthorsDataSet.ReadXml(filePath);
            GridView1.DataSource = dataSet;
        }
        catch (Exception)
        {
            Response.Redirect("loading.aspx");
        }
        
        

        //dataGridView1.DataSource = AuthorsDataSet;
        //dataGridView1.DataMember = "authors";
        GridView1.DataBind();
    }
     protected void GridViewUserRow_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        Label lblID = (Label)clickedRow.FindControl("LabelDeviceIDHidden");

        string navUrl = "";

        navUrl = "~/backend/AddEditB_POP_UP.aspx?Edit=true&Item=" + lblID.Text;
        Response.Redirect(navUrl, false);

        // User loggedin = LoginLogic();

        /*

        string passusertoedit = lblID.Text;
        CHCSLogin client = new CHCSLogin();

        string usertoencode = loggedin.PartnersID;

        string encryptedusr = client.Encrypt(usertoencode);
        string encryptedusrtoedit = client.Encrypt(passusertoedit);

        string urlencodestring1 = Server.UrlEncode(encryptedusr);
        string urlencodestring2 = Server.UrlEncode(encryptedusrtoedit);
        string navUrl = "~/Secure/UsersEdit.aspx?key=" + urlencodestring1 + "&User=" + urlencodestring2;
        Response.Redirect(navUrl, false);
        Response.End();


        */
    }

     protected void GridViewUserRowDelete_Click(object sender, EventArgs e)
     {
         GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
         Label lblID = (Label)clickedRow.FindControl("LabelDeviceIDHidden");

         string navUrl = "";


         navUrl = "~/backend/Edit_B_Links.aspx";

         string removeval = lblID.Text;

         string SaveLocation = Server.MapPath("~//backend/BusinessLinks.xml");


         // XDocument doc = new XDocument();
         XmlDocument doc = new XmlDocument();

         doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));
         // IEnumerable<XElement> specificChildElements = doc.Elements("tag");

         XmlNodeList nodes = doc.SelectNodes("businesslinks/item");
         if (true)
         {
             int removenode = 0;
             string val = "";

             for (int i = 0; i < nodes.Count; i++)
             {
                 if (nodes[i].SelectSingleNode("id") != null)
                 {
                     val = nodes[i].SelectSingleNode("id").InnerText;
                     if (val == removeval)
                     {
                         removenode = i;
                         break;
                     }
                 }
             }


             XDocument xDoc2 = new XDocument();
             xDoc2 = XDocument.Load(doc.CreateNavigator().ReadSubtree());

             int num = 0;



             foreach (var coordinate in xDoc2.Descendants("item"))
             {

                 if (removenode == num)
                 {

                     coordinate.Remove();
                     break;
                 }
                 num++;
             }


             XmlNode root = doc.DocumentElement;






             xDoc2.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
             Response.Redirect("~//backend//Edit_B_Links.aspx");

            // Response.Redirect(navUrl, false);

            
         }
     }


     protected void btn_add_Click(object sender, EventArgs e)
     {
         Response.Redirect("AddEditB_POP_UP.aspx");
           
     }
}