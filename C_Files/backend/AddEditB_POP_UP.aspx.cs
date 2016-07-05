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
public partial class backend_EditB_POP_UP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["edit"] != null)
            {
                if (Request["edit"].ToString() == "true")
                {
                    loadedit();
                }

            }
            else
            {
                Image1.ImageUrl = "images/panel/noimage.jpg";
            }
        }

    }
    protected void loadedit()
    {
        string removeval = "";
        try
        {
            removeval = Request["item"].ToString();
        }
        catch (Exception ex)
        {
            Response.Write("Couldn't load item");

        }
     XmlDocument doc = new XmlDocument();
           
            doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));
           // IEnumerable<XElement> specificChildElements = doc.Elements("tag");

            XmlNodeList nodes = doc.SelectNodes("businesslinks/item");
            
               
                string val = "";
                string url = "";
                string text = "";
                string image = "";
                string panel = "";
                string id = "";
                string links = "";
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].SelectSingleNode("id") != null)
                    {
                        val = nodes[i].SelectSingleNode("id").InnerText;
                        if (val == removeval)
                        {
                            try
                            {
                                url = nodes[i].SelectSingleNode("url").InnerText;
                                panel = nodes[i].SelectSingleNode("panel").InnerText;
                                image = nodes[i].SelectSingleNode("img").InnerText;
                                text = nodes[i].SelectSingleNode("text").InnerText;
                                id = nodes[i].SelectSingleNode("id").InnerText;
                                links = nodes[i].SelectSingleNode("links").InnerText;
                            }
                            catch (Exception ex)
                            { 
                            
                            }
                            break;
                        }
                    }

                }

                Textname.Text = text;
                image = image.Replace("//", "/");

                image = string.Format("images/panel/{0}", image);
                Image1.ImageUrl = image;
                    
                if (panel.ToString().ToUpper() == "TRUE")
                {
                    DropDownList2.SelectedIndex = 0;
                }
                else
                {
                    DropDownList2.SelectedIndex = 1;
              
                }

                if (links.ToString().ToUpper() == "TRUE")
                {
                    DropDownList3.SelectedIndex = 0;
                }
                else
                {
                    DropDownList3.SelectedIndex = 1;

                }
                if (url.Contains("https"))
                {
                    DropDownList1.SelectedIndex = 1;
                }
                else
                {
                    DropDownList1.SelectedIndex = 0;
                }

                string[] split = url.Split(':');
                URL.Text = split[1].Replace("//","");

    }
    protected void Add_Click(object sender, EventArgs e)
    {
        string panel = DropDownList2.Text;
        string img = SaveImage(FileUpload1);
        string links = DropDownList3.Text;
        string id="holder";
        bool edititem = false;
        if(Request["edit"]!=null)
        {
        if(Request["edit"].ToString()=="true")
        {
            edititem = true;
        }
        }
        if (!FileUpload1.HasFile)
        {
            try
            {
                img = Image1.ImageUrl.Replace("images/panel/","");
            }
            catch (Exception ex)
            { }
        }
        saveHyperlink(DropDownList1.SelectedValue.ToString() + URL.Text, Textname.Text, img, panel, links, id, edititem);
    }
   

    private string SaveImage(FileUpload p_fu)
    {
        string timeStamp = DateTime.Now.ToString();

        timeStamp = timeStamp.Replace("/", "").Replace(":", "").Replace(" ", "");
        timeStamp += p_fu.FileName;
        string path = Server.MapPath("~/backend/images/panel/" + timeStamp);
        p_fu.SaveAs(path);

        return timeStamp;
    }
    protected void saveHyperlink(string url, string text, string img, string panel, string links, string id, bool edititem)
    {
        
        string removeval="Wix";
        if (edititem)
        {

            try
            {
                removeval = Request["item"].ToString();
            }
            catch (Exception ex)
            {
                removeval = "";
            }
        
        }
        try
        {
            string SaveLocation = Server.MapPath("~//backend/BusinessLinks.xml");

            
           // XDocument doc = new XDocument();
           XmlDocument doc = new XmlDocument();
           
            doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));
           // IEnumerable<XElement> specificChildElements = doc.Elements("tag");

            XmlNodeList nodes = doc.SelectNodes("businesslinks/item");
            if (edititem)
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
                
               
                XElement srcTree = new XElement("item",
    new XElement("url", url),
    new XElement("text", text),
    new XElement("img", img),
    new XElement("panel", panel),
    new XElement("links", links),
    new XElement("id", removenode.ToString())
    );
                xDoc2.XPathSelectElement("//businesslinks").Add(srcTree);


               
                xDoc2.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
                Response.Redirect("~//backend//Edit_B_Links.aspx");
                return;
               // xDoc2.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
            }
          // Response.Redirect("~//backend//EditBusinessLinks.aspx");
           

         
          
           // string myvideofilepath = SaveLocation;
            
            XmlNode root2 = doc.DocumentElement;
            XDocument xDoc = new XDocument();



           xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

            XElement srcTree2 = new XElement("item",
new XElement("url", url),
new XElement("text", text),
new XElement("img", img),
new XElement("panel", panel),
    new XElement("links", links),
    new XElement("id", (xDoc.Descendants("item").Count()+1).ToString())
);
            xDoc.XPathSelectElement("//businesslinks").Add(srcTree2);

            xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));

            XmlDocument doc2 = new XmlDocument();

            doc2.Load(Server.MapPath("~//backend/BusinessLinks.xml"));
            // IEnumerable<XElement> specificChildElements = doc.Elements("tag");

            XmlNodeList nodes2 = doc2.SelectNodes("businesslinks/item");

            for (int i = 0; i < nodes2.Count; i++)
            {
                if (nodes2[i].SelectSingleNode("id") != null)
                {
                    nodes2[i].SelectSingleNode("id").InnerText=i.ToString();
                    
                }
            }

            XDocument xDoc3 = new XDocument();



            xDoc3 = XDocument.Load(doc2.CreateNavigator().ReadSubtree());

            xDoc3.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
            Response.Redirect("~//backend//Edit_B_Links.aspx");

        }
        catch (Exception ex)
        {
            //  Label1.Text = "ERROR: " + ex.Message.ToString();
        }



    }
    /*
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
            Response.Redirect("~//backend//EditBusinessLinks.aspx");
            //checklist.Items.Clear();
            // DeleteVideo.Attributes["onclick"] = "javascript:SomeFunction();return true;";
            //button1
        }
    }
   */
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        FileUpload1.Attributes.Clear();
    }
}