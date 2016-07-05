using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.IO;
using CfsNamespace;
using AtomImageEditor;
using AtomImageEditorServerControls;

public partial class edit_images : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.selectImageButton1.TargetEditableImageClientID = this.imgTalent1.ClientID;
        this.selectImageButton2.TargetEditableImageClientID = this.imgTalent2.ClientID;
        this.selectImageButton3.TargetEditableImageClientID = this.imgTalent3.ClientID;
        this.selectImageButton4.TargetEditableImageClientID = this.imgTalentThumb.ClientID;
        this.selectImageButton5.TargetEditableImageClientID = this.imgTalentNew.ClientID;
        
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);

        btnSelectTalent.Click += new EventHandler(btnSelectTalent_Click);

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            LoadTalentData(Request.QueryString["id"]);

            ImageSelector1.TalentID = Convert.ToInt32(Request.QueryString["id"]);
        }
        else
        {
            divEditImages.Visible = false;
            divSelectTalent.Visible = true;
            CfsCommon.GetFullTalentList(ddlFullTalentList);
        }
    }

    void btnSelectTalent_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFullTalentList.SelectedValue))
        {
            Response.Redirect("edit_images.aspx?id=" + ddlFullTalentList.SelectedValue);
        }
    }

    protected void OnClick_btnSaveEdits(object sender, EventArgs e)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Talent talentRecord = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);

        if (this.imgTalent1.HasEditedImage)
        {
            this.saveEditImageManipulation(this.imgTalent1, talentRecord, 1);
        }

        if (this.imgTalent2.HasEditedImage)
            this.saveEditImageManipulation(this.imgTalent2, talentRecord, 2);

        if (this.imgTalent3.HasEditedImage)
            this.saveEditImageManipulation(this.imgTalent3, talentRecord, 3);

        if (this.imgTalentThumb.HasEditedImage)
        {
            this.saveEditImageManipulation(this.imgTalentThumb, talentRecord, 4);
        }

        if (this.imgTalentNew.HasEditedImage)
            this.saveEditImageManipulation(this.imgTalentNew, talentRecord, 7);

        cfsEntity.SaveChanges();
    }

    private string CopyImage(string imgName, string talType)
    {
        if( string.IsNullOrEmpty(imgName) )
        {
            return "";
        }

        string src = Server.MapPath("~/" + CfsCommon.IMAGE_PATH_BASE + CfsCommon.TALENT_TYPE_ID_APPLICANT + "/") + imgName;
        string destPath = Server.MapPath( "~/" + CfsCommon.GetTalentMediaPath(talType, CfsCommon.MEDIA_TYPE_IMAGE, true));
        string destFileName = imgName;

        if (!File.Exists(src))
        {
            /* Can't move a file that doesn't exist */
            return "";
        }

        /* If Destination file exists, come up w/ a new name (append numbers) */
        if (File.Exists(destPath + destFileName))
        {
            destFileName = CfsCommon.CreateNewFilename(destPath, destFileName);
        }

        try
        {
            File.Copy(src, destPath + destFileName, false);
        }
        catch (Exception)
        {
            /* Ignore Exceptions, so user page don't blow up */
            return "";
        }

        /* Returns the relative location, so the website can use */
        return CfsCommon.GetTalentMediaPath(talType, CfsCommon.MEDIA_TYPE_IMAGE, false) + destFileName;
    }

    private void saveEditImageManipulation(EditableImage editableImage, Talent talentRecord, int imageIndex)
    {
        // get image ID
        Guid imageID = ImageManager.GetImageIdFromPath(editableImage.ImagePath);

        // Set image index
        ImageManager.SetImageIndex(imageID, imageIndex);

        // Set talent imageN property
        if (imageIndex == 1)
        {
            talentRecord.ImageOne = imageID.ToString();
        }
        else if (imageIndex == 2)
        {
            talentRecord.ImageTwo = imageID.ToString();
        }
        else
        {
            talentRecord.ImageThree = imageID.ToString();
        }

        // convert watermarks to image overlays
        List<OverlayImage> overlayImages = new List<OverlayImage>();

        foreach (WatermarkInfo watermarkInfo in editableImage.Watermarks)
        {
            // get watermark ID
            Guid watermarkImageID = ImageManager.GetImageIdFromPath(watermarkInfo.imagePath);

            // add watermark to overlays list
            overlayImages.Add(new OverlayImage(
                watermarkInfo.x,
                watermarkInfo.y,
                watermarkInfo.width,
                watermarkInfo.height,
                watermarkImageID
                ));
        }

        // edit image
        ImageManager.EditImage(imageID, editableImage.ResizeWidth, editableImage.ResizeHeight, editableImage.CropX, editableImage.CropY, editableImage.CropWidth, editableImage.CropHeight, overlayImages.ToArray());
    }

    private void LoadTalentData(string talentId)
    {
        CfsEntity cfsEntity = new CfsEntity();

        Talent talRec = CfsCommon.GetTalentRecord(cfsEntity, talentId);

        if (talRec == null || talRec.IsActive == false)
        {
            Response.Redirect("~/");
        }

        litTalentName.Text = talRec.StageName + " - " + talRec.TalentType;

        // image one
        Guid imageGuid1 = ImageManager.GetImageGuid(1, talRec.TalentId, talRec.TalentType, talRec.ImageOne);
        if (imageGuid1 != Guid.Empty)
        {
            imgTalent1.DefaultImageURL = CfsCommon.GetTalentImagePath("../", imageGuid1);
            imgTalent1.Visible = true;
        }

        // image two
        Guid imageGuid2 = ImageManager.GetImageGuid(2, talRec.TalentId, talRec.TalentType, talRec.ImageTwo);
        if (imageGuid2 != Guid.Empty)
        {
            imgTalent2.DefaultImageURL = CfsCommon.GetTalentImagePath("../", imageGuid2);
            imgTalent2.Visible = true;
        }

        // image three
        Guid imageGuid3 = ImageManager.GetImageGuid(3, talRec.TalentId, talRec.TalentType, talRec.ImageThree);
        if (imageGuid3 != Guid.Empty)
        {
            imgTalent3.DefaultImageURL = CfsCommon.GetTalentImagePath("../", imageGuid3);
            imgTalent3.Visible = true;
        }

        // thumb image
        Guid thumbGuid = ImageManager.GetImageGuid(4, talRec.TalentId, talRec.TalentType, talRec.ThumbImg);
        if (thumbGuid != Guid.Empty)
        {
            imgTalentThumb.DefaultImageURL = CfsCommon.GetTalentImagePath("../", thumbGuid);
            imgTalentThumb.Visible = true;

            if (talRec.ThumbImg != thumbGuid.ToString())
            {
                talRec.ThumbImg = thumbGuid.ToString();
                cfsEntity.SaveChanges();
            }
        }
        else
        {
            // No image so make a copy of image 1 and use that
            if (imageGuid1 != Guid.Empty)
            {
                Guid newGuid = ImageManager.AddImage(ImageManager.GetImageBytes(imageGuid1, true), talRec.TalentId, false, 4);
                imgTalentThumb.DefaultImageURL = CfsCommon.GetTalentImagePath("../", newGuid);
                imgTalentThumb.Visible = true;
            }
        }

        // new talent image
        Guid newTalentGuid = ImageManager.GetImageGuid(7, talRec.TalentId, talRec.TalentType, talRec.NewTalentImg);
        if (newTalentGuid != Guid.Empty)
        {
            imgTalentNew.DefaultImageURL = CfsCommon.GetTalentImagePath("../", newTalentGuid);
            imgTalentNew.Visible = true;

            if (talRec.NewTalentImg != newTalentGuid.ToString())
            {
                talRec.NewTalentImg = newTalentGuid.ToString();
                cfsEntity.SaveChanges();
            }
        }
        else
        {
            // No image so make a copy of image 1 and use that
            if (imageGuid1 != Guid.Empty)
            {
                Guid newGuid = ImageManager.AddImage(ImageManager.GetImageBytes(imageGuid1, true), talRec.TalentId, false, 7);
                imgTalentNew.DefaultImageURL = CfsCommon.GetTalentImagePath("../", newGuid);
                imgTalentNew.Visible = true;
            }

        }

    }
}