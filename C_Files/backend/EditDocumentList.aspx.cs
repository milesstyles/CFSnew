using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;

public partial class backend_EditDocumentList : System.Web.UI.Page
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



        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~//backend/DocumentList.xml"));




        XmlNode root = doc.DocumentElement;
        XDocument xDoc = new XDocument();
        xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());
        foreach (var coordinate in xDoc.Descendants("item"))
        {
            XmlDocument hhh = new XmlDocument();
            hhh.LoadXml(coordinate.ToString());
            foreach (XmlNode node in hhh)
            {
                string textname = "";
                foreach (XmlNode node2 in node)
                {

                    if (node2.Name == "text")
                    {
                        textname = node2.InnerText.ToString();


                        checklist.Items.Add(new ListItem(textname));

                    }
                }
            }

        }


        //PlaceHolder.Controls.Add(checklist);
        Place.Controls.Add(checklist);
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
            doc.Load(Server.MapPath("~//backend/DocumentList.xml"));


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

            xDoc.Save(Server.MapPath("~//backend/DocumentList.xml"));
            Response.Redirect("~//backend//EditDocumentList.aspx");
            //checklist.Items.Clear();
            // DeleteVideo.Attributes["onclick"] = "javascript:SomeFunction();return true;";
            //button1
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        Deletelink();
    }
    protected void saveHyperlink(string url, string text)
    {

        try
        {
            string SaveLocation = Server.MapPath("~//backend/DocumentList.xml");



            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~//backend/DocumentList.xml"));

            string myvideofilepath = SaveLocation;
            XmlNode root = doc.DocumentElement;
            XDocument xDoc = new XDocument();


            xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

            XElement srcTree = new XElement("item",
new XElement("url", url),
new XElement("text", text)
);
            xDoc.XPathSelectElement("//documentLinks").Add(srcTree);
            //xDoc.XPathSelectElement("//businesslinks").XPathSelectElement("thumbnail").Attribute("url").SetValue("media/video/" + fn);



            xDoc.Save(Server.MapPath("~//backend/DocumentList.xml"));
            Response.Redirect("~//backend//EditDocumentList.aspx", false);
           
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            //  Label1.Text = "ERROR: " + ex.Message.ToString();
        }



    }
    protected void Add_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
            try
            {
                string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Documents//");


                string fileName = fn;

                // Create the path and file name to check for duplicates.
                string pathToCheck = SaveLocation + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = SaveLocation + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    //UploadStatusLabel.Text = "A file with the same name already exists." +
                      //  "<br />Your file was saved as " + fileName;
                }
                SaveLocation += fileName;
                FileUpload1.SaveAs(SaveLocation);
               // saveHyperlink(("media/video/") + fn, Textname.Text);
                saveHyperlink(("Documents/") + fileName, Textname.Text);

            }
            catch (Exception ex)
            {
                Label1.Text = "ERROR: " + ex.Message.ToString();
            }
        else
        {
            Label1.Text = "You have not specified a file.";
        }
       // saveHyperlink(DropDownList1.SelectedValue.ToString() + URL.Text, Textname.Text);
    }
}