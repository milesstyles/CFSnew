using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

/// <summary>
/// Summary description for JsonWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class JsonWebService : System.Web.Services.WebService
{

    public JsonWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProductsJson(string prefix)
    {
        List<Product> products = new List<Product>();
        if (prefix.Trim().Equals(string.Empty, StringComparison.OrdinalIgnoreCase))
        {
            products = ProductFacade.GetAllProducts();
        }
        else
        {
            products = ProductFacade.GetProducts(prefix);
        }
        //yourobject is your actula object (may be collection) you want to serialize to json
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(products.GetType());
        //create a memory stream
        MemoryStream ms = new MemoryStream();
        //serialize the object to memory stream
        serializer.WriteObject(ms, products);
        //convert the serizlized object to string
        string jsonString = Encoding.Default.GetString(ms.ToArray());
        //close the memory stream
        ms.Close();
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SetPanelsJson(string prefix)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(prefix));
        RootObject myob = ser.ReadObject(ms) as RootObject;



        //save new order
        //delete previous order

        XmlDocument xDocument = new XmlDocument();
        xDocument.Load(Server.MapPath("~//backend/BusinessLinks.xml"));




        foreach (XmlNode s in xDocument)               // Copies the list (it's needed because we modify it in the foreach (when the element is removed)
        {

            s.RemoveAll();                                // Removes the element

        }

        XmlDocument doc = xDocument;

        XmlNode root = doc.DocumentElement;
        XDocument xDoc = new XDocument();

        xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());




        int y = 0;
        foreach (Panelss x in myob.panelss)
        {


            string url = Server.UrlDecode(x.URL);
            string text = Server.UrlDecode(x.TEXT);
            string img = Server.UrlDecode(x.IMG);
            string panel = Server.UrlDecode(x.PANEL);
            string links = Server.UrlDecode(x.LINKS);

           /// url = 


            XElement srcTree = new XElement("item",
    new XElement("url", url),
    new XElement("text", text),
    new XElement("img", img),
    new XElement("panel", panel),
    new XElement("links", links),
    new XElement("id", y)
    );
            xDoc.XPathSelectElement("//businesslinks").Add(srcTree);
            y++;
}
        xDoc.Save(Server.MapPath("~//backend/BusinessLinks.xml"));
        return prefix;
    }

}