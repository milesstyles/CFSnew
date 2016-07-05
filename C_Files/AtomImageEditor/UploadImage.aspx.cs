using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtomImageEditor;
using CfsNamespace;

public partial class UploadImage : System.Web.UI.Page
{
    private int talentId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            this.talentId = Convert.ToInt32(Request.QueryString["id"]);
        }

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

        CfsEntity cfsEntity = new CfsEntity();

        Talent talRec = CfsCommon.GetTalentRecord(cfsEntity, this.talentId.ToString());

        Guid newImageID = ImageManager.AddImage(imageData, this.talentId, (talRec.TalentType == CfsCommon.TALENT_TYPE_ID_APPLICANT), 9);

        // trigger refresh of list
        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "OnImageUploaded", "window.parent.onImageUploaded('"+newImageID.ToString()+"');", true);
    }
}