using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;

using CfsNamespace;

public partial class dat_videos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            string talentId = Request.QueryString["id"];
            
            CfsEntity cfsEntity = new CfsEntity();

            Talent talRec = CfsCommon.GetTalentRecord(cfsEntity, talentId);

            // Create a new XmlTextWriter instance
            XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("videos");

            if (talRec != null && !string.IsNullOrEmpty(talRec.VideoOne))
            {
                ArrayList videos = new ArrayList();
                videos.Add(talRec.VideoOne);
                if (!string.IsNullOrEmpty(talRec.VideoTwo))
                {
                    videos.Add(talRec.VideoTwo);

                    if (!string.IsNullOrEmpty(talRec.VideoThree))
                    {
                        videos.Add(talRec.VideoThree);
                    }
                }

                foreach (string videoStr in videos)
                {
                    writer.WriteStartElement("video");
                    writer.WriteElementString("src", ResolveUrl(String.Format("~/talentvids/{0}", videoStr)));
                    writer.WriteElementString("still", ResolveUrl(String.Format("~/images/{0}", "cover_yrbmag.jpg")));
                    writer.WriteEndElement();
                }
               
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
