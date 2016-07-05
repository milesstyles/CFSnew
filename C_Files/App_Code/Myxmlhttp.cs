using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for Myxmlhttp
/// </summary>
public class Myxmlhttp
{
    public int getmygroup(string Talenttype)
    {
        Talenttype = Talenttype.ToLower();
        Talenttype = Talenttype.Trim();
        int mygroupid = 0;
        if (Talenttype == "female")
        {
            mygroupid = 40;
        }
        else if (Talenttype == "male")
        {
            mygroupid = 12;
        }
        else if (Talenttype == "female mini")
        {
            mygroupid = 21;
        }
        else if (Talenttype == "femalemini")
        {
            mygroupid = 21;
        }
        else if (Talenttype == "minifemale")
        {
            mygroupid = 21;
        }
        else if (Talenttype == "mini female")
        {
            mygroupid = 21;
        }
        else if (Talenttype == "male mini")
        {
            mygroupid = 42;
        }
        else if (Talenttype == "malemini")
        {
            mygroupid = 42;
        }
        else if (Talenttype == "mini male")
        {
            mygroupid = 42;
        }
        else if (Talenttype == "minimale")
        {
            mygroupid = 42;
        }
        else if (Talenttype == "belly dancer")
        {
            mygroupid = 20;
        }
        else if (Talenttype == "bellydancer")
        {
            mygroupid = 20;
        }
        else if (Talenttype == "bbw")
        {
            mygroupid = 16;
        }
        else if (Talenttype == "drag queen")
        {
            mygroupid = 17;
        }
        else if (Talenttype == "dragqueen")
        {
            mygroupid = 17;
        }
        else if (Talenttype == "impersonator")
        {
            mygroupid = 18;

        }
        else if (Talenttype == "novelty")
        {
            mygroupid = 19;
        }
        else if (Talenttype == "duo")
        {

        }
        else if (Talenttype == "Driver")
        {

        }
        else if (Talenttype == "Affiliate")
        {

        }
        
        return mygroupid; 
    }
    public void xmlhttp(string url)
    {
        string method = "Get";
        string content = "";

        if (url == "")
        {
            //return "Error: url invalid";
        }
        XmlDocument xmlresp = new XmlDocument();        //Create new XML Doc to return at end (converted to string)
        WebResponse httpresp = null;                    //Initialize http response
        Uri xhruri = new Uri("http://www.ymlp.com/");

        try
        {
            xhruri = new Uri(url);
        }
        catch
        {
            //return "<error>URL is formatted improperly.</error>";
        }
        try
        {
            //Initialize the WebRequest
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(xhruri);
            // NetworkCredential netcred = new NetworkCredential(user, pass);
            // CredentialCache netcredcache = new CredentialCache();
            // netcredcache.Add(xhruri, "Basic", netcred);
            // httpreq.Credentials = netcredcache;
            httpreq.KeepAlive = false;
            httpreq.Timeout = 10000;                 //Set the timeout in milliseconds - some basic exception catching
            //httpreq.ContentType = "application/x-www-form-urlencoded";
            //httpreq.ContentType = "text/xml";

            //Change the content type if necessary
            if (content != "")
            {
                if (content == "appx")
                {
                    httpreq.ContentType = "application/x-www-form-urlencoded";  //useful for POST
                }
                else
                {
                    httpreq.ContentType = content;          //Should only be non-standard if plain/xml type gets checked?
                }
            }


            //Checking for GET or POST


            if (method == "GET")
            {
                httpreq.Method = "GET";             //Simply send the url which has the querystring right in it
            }

            httpresp = httpreq.GetResponse();       //This is where it errors out - need to read memory prior to this?
            string streamstring = "";
            try
            {
                Stream httprespstream = httpresp.GetResponseStream();
                StreamReader respstring = new StreamReader(httprespstream);
                streamstring = respstring.ReadToEnd();
                xmlresp.LoadXml(streamstring);
                //xmlresp.Load(httpresp.GetResponseStream());
            }
            catch
            {
                streamstring = streamstring.Replace("\b", "");
                streamstring = streamstring.Replace("\a", "");
                xmlresp.LoadXml(streamstring);
            }
            httpresp.Close();
        }

               //Error checking
        // May need to check for dns resolving/spoofing? (in the future)
        catch (WebException e)
        {
            if (httpresp != null)
            {
                httpresp.Close();
            }

            try
            {
                //This will actually catch the error from SOAPstation instead of returning a generic 500 error.
                //Create a stream to read the entire message from the exception, then dump it into a string after
                //retrieving the response stream.
                HttpWebResponse rsp = (HttpWebResponse)e.Response;
                StreamReader rd = new StreamReader(rsp.GetResponseStream());
                string res = rd.ReadToEnd();
                //httpresp.Close();
                rd.Close();
                //return res;
                //xmlresp.Load(e.Response.GetResponseStream());
            }

            catch
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description></error>");

                if (httpresp != null)
                {
                    httpresp.Close();
                }
            }
        }
        catch (Exception x)
        {
            if (httpresp != null)
            {
                httpresp.Close();
            }
            try
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description><message>" + x.Message + "</message><more></more></error>");
            }
            catch
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description></error>");

                if (httpresp != null)
                {
                    httpresp.Close();
                }
            }
        }
    }
}