using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;
using System.Data;

public partial class backend_EditBusinessLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckBoxList checklist = new CheckBoxList();
        checklist.ID = "checkboxlist1";
        //checklist.AutoPostBack = false;
        checklist.CellPadding = 5;
        checklist.CellSpacing = 5;
        checklist.RepeatColumns = 1;
        checklist.RepeatDirection = RepeatDirection.Vertical;
        checklist.RepeatLayout = RepeatLayout.Flow;
        checklist.TextAlign = TextAlign.Right;
        


        //XmlDocument doc = new XmlDocument();
        //doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));


        

        //XmlNode root = doc.DocumentElement;
        //XDocument xDoc = new XDocument();
        //xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

        DataSet dataSet = new DataSet();
        dataSet.ReadXml(Server.MapPath("~//backend/BusinessLinks.xml"));
       // dataSet.ReadXml(xDoc);
        
        //AuthorsDataSet.ReadXml(filePath);
        GridView1.DataSource = dataSet;
        //dataGridView1.DataSource = AuthorsDataSet;
        //dataGridView1.DataMember = "authors";
        GridView1.DataBind();
        //foreach (var coordinate in xDoc.Descendants("item"))
        //{
        //    XmlDocument hhh = new XmlDocument();
        //    hhh.LoadXml(coordinate.ToString());
        //    foreach (XmlNode node in hhh)
        //    {
        //        string textname = "";
        //        foreach (XmlNode node2 in node)
        //        {
                    
        //            if (node2.Name == "text")
        //            {
        //                textname = node2.InnerText.ToString();
                        

        //                checklist.Items.Add(new ListItem(textname));

        //            }
        //        }
        //    }

        //}


        
        //Place.Controls.Add(checklist);
    }
    protected void Deletelink()
    {
        CheckBoxList checklist =
            (CheckBoxList)Place.FindControl("checkboxlist1");

        // Make sure a control was found.
        if (checklist != null)
        {

            //Message.Text = "Deleted Selected Item(s):<br><br>";

            // Iterate through the Items collection of the CheckBoxList 
            // control and display the selected items.
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));


            // string myvideofilepath = SaveLocation;
            XmlNode root = doc.DocumentElement;
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

            for (int i = checklist.Items.Count - 1; i > -1; i--)
            {

                if (checklist.Items[i].Selected)
                {
                    // removelist[begin] = i;
                    int num = 0;
                    foreach (var coordinate in xDoc.Descendants("item"))
                    {

                        if (num == i)
                        {

                            coordinate.Remove();
                            break;
                        }
                        num++;



                    }
                    //Message.Text += checklist.Items[i].Text + "<br>";

                }

            }
           
            xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
            Response.Redirect("~//backend//Edit_B_Links.aspx");
            //checklist.Items.Clear();
            // DeleteVideo.Attributes["onclick"] = "javascript:SomeFunction();return true;";
            //button1
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        Deletelink();
    }
    protected void GridViewUserRow_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        Label lblID = (Label)clickedRow.FindControl("LabelDeviceIDHidden");

        string navUrl = "";

        navUrl = "~/backend/Edit_B_Links.aspx?Device=" + lblID.Text;
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
        
    protected void saveHyperlink(string url, string text)
    {
        
            try
            {
                string SaveLocation = Server.MapPath("~//backend/BusinessLinks.xml");
               
               

                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));

                string myvideofilepath = SaveLocation;
                XmlNode root = doc.DocumentElement;
                XDocument xDoc = new XDocument();


                xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

                XElement srcTree = new XElement("item",
    new XElement("url", url),
    new XElement("text", text)
);
                xDoc.XPathSelectElement("//businesslinks").Add(srcTree);
                //xDoc.XPathSelectElement("//businesslinks").XPathSelectElement("thumbnail").Attribute("url").SetValue("media/video/" + fn);



                xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
                Response.Redirect("~//backend//Edit_B_Links.aspx");

            }
            catch (Exception ex)
            {
              //  Label1.Text = "ERROR: " + ex.Message.ToString();
            }
       


    }
    protected void Add_Click(object sender, EventArgs e)
    {
        saveHyperlink(DropDownList1.SelectedValue.ToString() + URL.Text, Textname.Text);
    }
}