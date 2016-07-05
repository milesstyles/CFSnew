using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AtomImageEditor;
using CfsNamespace;

public partial class AtomImageEditor_UploadGenericImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.imageUpload.PostedFile != null)
        {
            this.SaveImage(imageUpload.PostedFile);
        }
    }

    private void SaveImage( HttpPostedFile httpPostedFile )
    { 
        // create byte array
        byte[] imageData = new byte[httpPostedFile.ContentLength];

        // read image into a byte array
        httpPostedFile.InputStream.Read(imageData, 0, httpPostedFile.ContentLength);

        // save image to db
        Guid newImageID = ImageManager.AddImage(imageData, -1, false, -1);

        this.imageGuid.Text = newImageID.ToString();
    }
}
