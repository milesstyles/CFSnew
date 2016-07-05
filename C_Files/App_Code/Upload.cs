using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;

public class Upload : IHttpHandler, IRequiresSessionState
{    
    public Upload()
    {
    }

    public bool IsReusable
    {
        get { return true; }
    }
    private IsolatedStorageFile GetUserStoreAsAppropriateForCurrentPlatform()
    {

        return IsolatedStorageFile.GetUserStoreForDomain();
    }
   

    public void ProcessRequest(HttpContext context)
    {
        if ( context.Request.Files.Count > 0 )
        {
            for(int j = 0; j < context.Request.Files.Count; j++)
            {
                HttpPostedFile uploadFile = context.Request.Files[j];
                if (uploadFile.ContentLength > 0)
                {
                    uploadFile.SaveAs(context.Server.MapPath("~//media//video//ss"+uploadFile.FileName));
                   string myvideofilepath = "video/ss"+uploadFile.FileName;
                 
                     XmlDocument doc = new XmlDocument();
                     string mypathforfile = Path.GetFullPath(uploadFile.FileName);
                    //doc.Load(context.Server.MapPath(
                     //string mypath = context.Server.MapPath("~//media//video//").ToString();
                     doc.Load("M:\\NET\\centerfold\\playlist.xml");
                    
                   XmlNode root = doc.DocumentElement;
                   XDocument xDoc = new XDocument();
                   xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

                   xDoc.XPathSelectElement("//item")
                       .AddBeforeSelf(new XElement("item"), "");

                   

                   xDoc.XPathSelectElement("//item").Add(new XElement("title", "Main Page Video"));
                   xDoc.XPathSelectElement("//item").Add(new XElement("content", new XAttribute("url", myvideofilepath), new XAttribute("type", "video/x-flv"), new XAttribute("start", "00:00")));
                   xDoc.XPathSelectElement("//item").Add(new XElement("thumbnail", new XAttribute("url", "media/video/videoStill1.jpg")));
                   
                   xDoc.XPathSelectElement("//item").Add(new XElement("description", "Main Page Video"));
 
                   xDoc.XPathSelectElement("//item").Add(new XElement("link", "http://CenterfoldStrips.com"));

                   var xmlDocument = new XmlDocument();
                   using (var xmlReader = xDoc.CreateReader())
                   {
                       xmlDocument.Load(xmlReader);
                   }



                   using (IsolatedStorageFile storage = GetUserStoreAsAppropriateForCurrentPlatform())
                   {
                       using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("playlist2.xml", FileMode.Create, storage))
                       {
                           xmlDocument.Save(stream);
                       }
                   }
                   //xDoc.Save("~\\playlist2.xml");

                
                }                
            }
           

        }
    }

}
