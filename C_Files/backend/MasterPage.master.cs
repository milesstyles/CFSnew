using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;
using CfsNamespace;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //   CheckLoggedIn();

        //    HideInaccessibleMenuItems();
        if (!IsPostBack)
        {
            CreateBusinessLinks();

            CreateBusinessPanel();
        }
    
     //   CreateDocumentLinks();
    //    GetPromoSpecialInfo();
       
    }
    private void GetPromoSpecialInfo()
    {
        CfsEntity cfsEntity = new CfsEntity();

        foreach (AppSetting setting in cfsEntity.AppSetting)
        {
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_PROMO_URL)
            {
               // tBoxPromoUrl.Text = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC)
            {
                //imgPromo.ImageUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_LOGO_IMGSRC)
            {
                imgLogoTop.ImageUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_LOGO_TEXT)
            {
                LogoName.Text = setting.SettingValue;
            }

        }
    }
    private void CreateDocumentLinks()
    { 
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
                string url = "";
                string text = "";
                foreach (XmlNode node2 in node)
                {

                    if (node2.Name == "url")
                    {
                        url = node2.InnerText.ToString();
                        //string[] strFileParts = strPath.Split('/');

                        //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                    }
                    if (node2.Name == "text")
                    {
                        text = node2.InnerText.ToString();
                        //string[] strFileParts = strPath.Split('/');

                        //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                    }
                }
                addlinktodocument(url, text);
               // addlinktobusiness(url, text);
            }
        }
    }
    public static bool IsFileReady(String sFilename)
    {
        // If the file can be opened for exclusive access it means that the file
        // is no longer locked by another process.
        try
        {
            using (FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                if (inputStream.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void CreateBusinessLinks()
    {

        XmlDocument doc = new XmlDocument();
        bool fileready = false;
        /* while (!fileready)
         {
           fileready =  IsFileReady(Server.MapPath("~//backend/BusinessLinks.xml"));
         }
         */
        try
        {
            doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));



            XmlNode root = doc.DocumentElement;
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());
            foreach (var coordinate in xDoc.Descendants("item"))
            {
                XmlDocument hhh = new XmlDocument();
                hhh.LoadXml(coordinate.ToString());
                foreach (XmlNode node in hhh)
                {
                    string url = "";
                    string text = "";
                    string links = "";

                    foreach (XmlNode node2 in node)
                    {

                        if (node2.Name == "url")
                        {
                            url = node2.InnerText.ToString();
                            //string[] strFileParts = strPath.Split('/');

                            //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                        }
                        if (node2.Name == "text")
                        {
                            text = node2.InnerText.ToString();
                            //string[] strFileParts = strPath.Split('/');

                            //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                        }
                        if (node2.Name == "links")
                        {
                            links = node2.InnerText.ToString();
                            //string[] strFileParts = strPath.Split('/');

                            //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                        }
                    }
                    if (links.ToUpper() == "TRUE")
                    {
                        addlinktobusiness(url, text);
                    }
                }

            }

        


        }
    catch(Exception)
        {
            Response.Redirect("loading.aspx");
        }
       // Place.Controls.Add(checklist);
    }

    private void CreateBusinessPanel()
    {

        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~//backend/BusinessLinks.xml"));



        XmlNode root = doc.DocumentElement;
        XDocument xDoc = new XDocument();
        xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());
        int x = 0;
        foreach (var coordinate in xDoc.Descendants("item"))
        {
            x = x + 1;
            XmlDocument hhh = new XmlDocument();
            hhh.LoadXml(coordinate.ToString());
            foreach (XmlNode node in hhh)
            {
                string url = "";
                string text = "";
                string img = "";
                string panel = "";
                string links = "";
                foreach (XmlNode node2 in node)
                {

                    if (node2.Name == "url")
                    {
                        url = node2.InnerText.ToString();
                        //string[] strFileParts = strPath.Split('/');

                        //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                    }
                    if (node2.Name == "text")
                    {
                        text = node2.InnerText.ToString();
                        //string[] strFileParts = strPath.Split('/');

                        //checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                    }
                    if (node2.Name == "img")
                    {
                        img = node2.InnerText.ToString();
                    }
                    if (node2.Name == "panel")
                    {

                        panel = node2.InnerText.ToString();
                    }
                    if (node2.Name == "links")
                    {

                        links = node2.InnerText.ToString();
                    }
                }
                addlinktobusinesspanel(url, text,panel,img, x.ToString(),links);
            }

        }



        // Place.Controls.Add(checklist);
    }


    private void addlinktodocument(string url, string text)
    {

        HtmlGenericControl li = new HtmlGenericControl("li");

        url = url.Replace("\\", "").Replace("\"", "");
        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", url);
        anchor.Attributes.Add("target", "_blank");
        anchor.InnerText = text;

        li.Controls.Add(anchor);
        liDocumentLinks.Controls.Add(li);
    }

    private void addlinktobusiness(string url, string text){

        HtmlGenericControl li = new HtmlGenericControl("li");

        url = url.Replace("\\", "").Replace("\"", "");
        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", url);
        anchor.Attributes.Add("target", "_blank");
        anchor.InnerText = text;

        li.Controls.Add(anchor);
        liBusinessLinksholder.Controls.Add(li);
    }

    private void addlinktobusinesspanel(string url, string text, string panel, string img, string order, string links)
    {

        HtmlGenericControl li = new HtmlGenericControl("li");

        url = url.Replace("\\", "").Replace("\"", "");
        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", url);
        anchor.Attributes.Add("target", "_blank");
        //anchor.InnerText = text;

        HtmlGenericControl image = new HtmlGenericControl("img");

        image.Attributes.Add("height", "90px");
        image.Attributes.Add("width", "90px");
        image.Attributes.Add("src", "images/panel/"+img);
        //image.ClientID = order;
       
        anchor.Controls.Add(image);
        
        li.Attributes["class"] = "myClass";
        li.Attributes["save"] = "IMG:" + img + ":URL:" + url + ":TEXT:" + text+ ":PANEL:"+panel+":LINKS:"+links+":ORDER:"+order; 

        li.Controls.Add(anchor);

        //li.Attributes.

        HtmlGenericControl div = new HtmlGenericControl("div");

        div.Visible = true;
        //div.InnerText = "IMG:" + img + ":URL:" + url + ":TEXT:" + text; 
        div.InnerHtml = order;

        div.ID = "order_" + order;

        li.Controls.Add(div);
        if (panel == "TRUE")
        {
          //  li.Style.Add("visibility", "hidden");
        }
        else
        {
              //li.Style.Add("visibility", "hidden");
            li.Style.Add("visibility", "hidden");
            
            li.Attributes.Add("style", "display: none;");

        }
        

        

        
        //sortable.Controls.Add(x);
       // x.Attributes["onclick"] = "window.open('"+url+"')";
        sortable.Controls.Add(li);
        //liBusinessLinksholder.Controls.Add(li);
    }

    private void CheckLoggedIn()
    {
        if (Session[CfsCommon.SESSION_KEY_LOGGED_IN] == null)
        {
            Response.Redirect(CfsCommon.LOGIN_PAGE, true);
        }
    }

    private void HideInaccessibleMenuItems()
    {
        
        liUserAdmin.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.UserAdmin, Session);
        liEmployeeMgmt.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.EmployeeMgmt, Session);
        liPendingJobs.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.PendingJobs, Session);
        liWorkOrders.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.WorkOrders, Session);
        liBasicAccouting.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.Accounting, Session);
       
    }
    protected void btn_SaveBusinessPanelOrder_Click(object sender, EventArgs e)
    {

        XmlDocument xDocument = new XmlDocument();
        xDocument.Load(Server.MapPath("~//backend/BusinessLinks.xml"));



        
        foreach (XmlNode s in xDocument)               // Copies the list (it's needed because we modify it in the foreach (when the element is removed)
        {

            s.RemoveAll();                                // Removes the element
            
        }
      //  xDocument.Save(Server.MapPath("~//backend/BusinessLinks.xml"));


        List<string> mylist = new List<string>();
        
        for (int i = 0; i < sortable.Controls.Count; i++)
        {
            string hhhhhh = sortable.Controls[i].GetType().ToString();
            if (sortable.Controls[i].GetType().ToString() == "System.Web.UI.HtmlControls.HtmlGenericControl")
            { 
            HtmlGenericControl g = sortable.Controls[i] as HtmlGenericControl;
            if (g.TagName == "li")
            {
                string attributes = g.Attributes["save"].ToString();
               // Response.Write(fff+"<br>");
               // string[] splitme = attributes.Split(':');
                mylist.Add(attributes);

            }
            }
        }

      //  mylist = mylist.Sort(
        mylist.Reverse();

        XmlDocument doc = xDocument;

        XmlNode root = doc.DocumentElement;
        XDocument xDoc = new XDocument();

        xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

        for (int y = mylist.Count-1; y >= 0; y--)
        {
            string att = mylist[y];
            string[] splitme = att.Split(':');

            string url = splitme[3]+":"+splitme[4];
            string text = splitme[6];
            string img = splitme[1];
            string panel = splitme[8];
            string links = splitme[10];
          


        
            XElement srcTree = new XElement("item",
    new XElement("url", url),
    new XElement("text", text),
    new XElement("img", img),
    new XElement("panel", panel),
    new XElement("links", links),
    new XElement("id", y)
    );
            xDoc.XPathSelectElement("//businesslinks").Add(srcTree);
        }

        xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));

        sortable.Controls.Clear();
        CreateBusinessPanel();
       // sortable.Controls = null;

     
    }
    protected void saveHyperlink(string url, string text, string img, string panel, bool edititem)
    {

        string removeval = "Wix";
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
                    if (nodes[i].SelectSingleNode("text") != null)
                    {
                        val = nodes[i].SelectSingleNode("text").InnerText;
                        if (val == removeval)
                        {
                            removenode = i;
                            break;
                        }
                    }
                }
                int y = removenode;

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

                xDoc2.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
            }
            // Response.Redirect("~//backend//EditBusinessLinks.aspx");




            string myvideofilepath = SaveLocation;
            XmlNode root = doc.DocumentElement;
            XDocument xDoc = new XDocument();



            xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

            XElement srcTree = new XElement("item",
new XElement("url", url),
new XElement("text", text),
new XElement("img", img),
new XElement("panel", panel)
);
            xDoc.XPathSelectElement("//businesslinks").Add(srcTree);



            xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
            Response.Redirect("~//backend//EditBusinessLinks.aspx");

        }
        catch (Exception ex)
        {
            //  Label1.Text = "ERROR: " + ex.Message.ToString();
        }



    }


    protected void btn_SaveBusinessPanelOrder_Click1(object sender, EventArgs e)
    {
        Response.Redirect("loading.aspx");
    }
}
