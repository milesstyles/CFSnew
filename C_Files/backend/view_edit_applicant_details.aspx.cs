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


public partial class backend_view_edit_applicant_details : System.Web.UI.Page
{
    const string PARAM_ID_APPLICATION = "appid";
    
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);

        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlStateApp);
            CfsCommon.GetStateList(ddlStateHire);
            CfsCommon.GetCountryList(ddlCountryApp, true);
            CfsCommon.GetCountryList(ddlCountryHire, false);
            CfsCommon.GetTalentTypeList(ddlTalentType, true);

            if (Request.Params[PARAM_ID_APPLICATION] != null)
            {
                if (GetApplicantData(Request.Params[PARAM_ID_APPLICATION].ToString()))
                {
                    UpdatePageMode(CfsCommon.MODE_READONLY);
                }
            }
        }
        hBoxHomePhone.NavigateUrl = "tel:" + tBoxHomePhone.Text;
        hBoxCellPhone.NavigateUrl = "tel:" + tBoxCellPhone.Text;
        hBoxAltPhone.NavigateUrl = "tel:" + tBoxCellPhone.Text;
    }

    protected void OnClick_btnEditApplicant(object sender, EventArgs arrrgggs)
    {
        if (hiddenPageMode.Value == CfsCommon.MODE_READONLY)
        {
            UpdatePageMode(CfsCommon.MODE_UPDATE);
        }
        else if (hiddenPageMode.Value == CfsCommon.MODE_UPDATE)
        {
            if (Request.Params[PARAM_ID_APPLICATION] != null)
            {
                if (FormIsValid())
                {
                    if (UpdateApplicant(Request.Params[PARAM_ID_APPLICATION].ToString()))
                    {
                        UpdatePageMode(CfsCommon.MODE_READONLY);
                    }
                }
            }
        }
    }

    protected void OnClick_btnDeleteApplicant(object sender, EventArgs arrrgggs)
    {
        divDeleteConfirm.Style[HtmlTextWriterStyle.Position] = "absolute";
        divDeleteConfirm.Style[HtmlTextWriterStyle.Left] = "40%";
        divDeleteConfirm.Style[HtmlTextWriterStyle.Top] = "40%";
        
        divDeleteConfirm.Visible = true;
    }

    protected void OnClick_btnConfirmNo(object sender, EventArgs e)
    {
        divDeleteConfirm.Visible = false;
    }

    protected void OnClick_btnConfirmYes(object sender, EventArgs e)
    {
        if (Request.Params[PARAM_ID_APPLICATION] == null || Request.Params[PARAM_ID_APPLICATION].ToString() == "")
        {
            //Don't expect this to ever happen
            return;
        }

        if (UpdateStatus((string)Request.Params[PARAM_ID_APPLICATION], CfsCommon.APPLICANT_STATUS_DELETED))
        {
            /* Hide Confirm Box */
            divDeleteConfirm.Visible = false;

            /* Redirect to Applicant List */
            Response.Redirect("view_applicants.aspx");
        }
    }


    protected void OnClick_btnUpdateInfo(object sender, EventArgs arrrgggs)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Applicant applicant;

        if (Request.Params[PARAM_ID_APPLICATION] == null)
        {
            return;
        }

        if ((applicant = GetApplicantRecord(cfsEntity, Request.Params[PARAM_ID_APPLICATION])) != null)
        {
            applicant.HasBeenContacted = chkContacted.Checked;
            applicant.HiringInfo = tBoxHiringNotes.Text;

            if (cfsEntity.SaveChanges() >= 1)
            {
                Response.Redirect("view_applicants.aspx");
            }
        }
    }

    protected void OnClick_btnHireTalent(object sender, EventArgs argggs)
    {
        int newHireId;

        if (ddlTalentType.SelectedValue != "")
        {
            if (AddNewHire(out newHireId))
            {

                string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmailAddress.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlStateApp.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                Myxmlhttp callme = new Myxmlhttp();
                callme.xmlhttp(ymlpurl);
                int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
                if (groupid == 0)
                { }
                else
                {
                    ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmailAddress.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlStateApp.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                    callme.xmlhttp(ymlpurl);
                }
               
                if (UpdateStatus(Request.Params[PARAM_ID_APPLICATION].ToString(), CfsCommon.APPLICANT_STATUS_HIRED))
                {

                    /* Redirect to Employee View / Edit screen */
                    Response.Redirect("add_edit_employee.aspx?empid=" + newHireId.ToString(), true);                
                }
            }
        }
    }
    #endregion

    #region DB Read Only Functions
    private Applicant GetApplicantRecord(CfsEntity cfsEntity, string appIdStr)
    {
        if (appIdStr == null || appIdStr == "")
        {
            return null;
        }

        ObjectQuery<Applicant> appQuery = cfsEntity.Applicant.Where("it.ApplicantId = " + appIdStr);
        List<Applicant> appList = appQuery.ToList();

        if (appList.Count <= 0)
        {
            return null;
        }

        return appList[0];
    }

    private bool GetApplicantData(string appIdStr)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Applicant applicant;

        if ((applicant = GetApplicantRecord(cfsEntity, appIdStr)) == null)
        {
            return false;
        }

        lblDateApplied.InnerText = ((DateTime)applicant.DateApplied).ToString("MM/dd/yyyy");

        if (applicant.HasBeenContacted != null)
        {
            chkContacted.Checked = (bool)applicant.HasBeenContacted;
        }
        
        tBoxHiringNotes.Text = applicant.HiringInfo;

        tBoxFirstName.Text = applicant.FirstName;
        tBoxLastName.Text = applicant.LastName;
        tBoxStageName.Text = applicant.StageName;
        tBoxEmailAddress.Text = applicant.Email;
        tBoxWebsite.Text = applicant.Website;

        tBoxAddress1.Text = applicant.Address1;
        tBoxAddress2.Text = applicant.Address2;
        tBoxCity.Text = applicant.City;
        ddlStateApp.SelectedValue = applicant.State;
        ddlCountryApp.SelectedValue = applicant.Country;
        tBoxZip.Text = applicant.Zip;
        tBoxHomePhone.Text = applicant.HomePhone;
        tBoxCellPhone.Text = applicant.CellPhone;
        tBoxAltPhone.Text = applicant.AltPhone;

        if (applicant.HeightFt != null)
        {
            tBoxHeightFt.Text = applicant.HeightFt.ToString();
        }
        if (applicant.HeightIn != null)
        {
            tBoxHeightIn.Text = applicant.HeightIn.ToString();
        }
        if (applicant.Weight != null)
        {
            tBoxWeight.Text = applicant.Weight.ToString();
        }
        
        tBoxEyeColor.Text = applicant.EyeColor;
        tBoxHairColor.Text = applicant.HairColor;
        tBoxExperience.Text = applicant.Experience;
        tBoxAvailable.Text = applicant.DaysAvail;
        tBoxPrefDays.Text = applicant.DaysPrefer;

        tBoxBust.Text = applicant.Bust;
        tBoxWaist.Text = applicant.Waist;
        tBoxHips.Text = applicant.Hips;

        if (applicant.DOB != null)
        {
            tBoxDateOfBirth.Text = ((DateTime)applicant.DOB).ToString("MM/dd/yyyy");
            lblAgeInYears.Text = CfsCommon.CalculateAge((DateTime)applicant.DOB);
        }

        hiddenImgFn1.Value = applicant.ImageOne;
        hiddenImgFn2.Value = applicant.ImageTwo;
        hiddenImgFn3.Value = applicant.ImageThree;
        hiddenImgID1.Value = applicant.ImageIdOne;
        hiddenImgID2.Value = applicant.ImageIdTwo;

        // image one
        Guid imageGuid1 = Guid.Empty;
        try
        {
            imageGuid1 = ImageManager.GetImageGuid(1, applicant.ApplicantId, CfsCommon.TALENT_TYPE_ID_APPLICANT, applicant.ImageOne);
        }
        catch
        {
        }
        if (imageGuid1 == Guid.Empty)
            noApplicantImage1Message.Visible = true;
        else
        {
            imgApplicant1.Src = CfsCommon.GetTalentImagePath("../", imageGuid1);
            imgApplicant1.Visible = true;
        }

        // image two
        Guid imageGuid2 = Guid.Empty;
        try
        {
            imageGuid2 = ImageManager.GetImageGuid(2, applicant.ApplicantId, CfsCommon.TALENT_TYPE_ID_APPLICANT, applicant.ImageTwo);
        }
        catch
        {
        }
        if (imageGuid2 == Guid.Empty)
            noApplicantImage2Message.Visible = true;
        else
        {
            imgApplicant2.Src = CfsCommon.GetTalentImagePath("../", imageGuid2);
            imgApplicant2.Visible = true;
        }

        // image three
        Guid imageGuid3 = Guid.Empty;
        try
        {
            imageGuid3 = ImageManager.GetImageGuid(3, applicant.ApplicantId, CfsCommon.TALENT_TYPE_ID_APPLICANT, applicant.ImageThree);
        }
        catch
        {
        }
        if (imageGuid3 == Guid.Empty)
            noApplicantImage3Message.Visible = true;
        else
        {
            imgApplicant3.Src = CfsCommon.GetTalentImagePath("../", imageGuid3);
            imgApplicant3.Visible = true;
        }
        
        
        
        //Grab the image ID's
        // image ID 1
        Guid imageGuidID1 = Guid.Empty;
        try
        {
            imageGuidID1 = ImageManager.GetImageGuid(4, applicant.ApplicantId, CfsCommon.TALENT_TYPE_ID_APPLICANT, applicant.ImageIdOne);
        }
        catch
        {
        }
        if (imageGuidID1 == Guid.Empty)
            noApplicantImageID1.Visible=true;
            
        else
        {
            imgID1.Src = CfsCommon.GetTalentImagePath("../", imageGuidID1);
         //   imgID1.Src = "../../talentimages/applicant/" + applicant.ImageIdOne.ToString();
            imgID1.Visible = true;
        }

        // image ID 2
        Guid imageGuidID2 = Guid.Empty;
        try
        {
            imageGuidID2 = ImageManager.GetImageGuid(5, applicant.ApplicantId, CfsCommon.TALENT_TYPE_ID_APPLICANT, applicant.ImageIdTwo);
        }
        catch
        {
        }
        if (imageGuidID2 == Guid.Empty)
            noApplicantImageID2.Visible = false;

        else
        {
            imgID2.Src = CfsCommon.GetTalentImagePath("../", imageGuidID2);
            imgID2.Visible = true;
        }

        /* Only display Email send if email is present */
        if( tBoxEmailAddress.Text != "" )
        {
            hlEmailSend.NavigateUrl = "mailto:" + tBoxEmailAddress.Text;
            hlEmailSend.Visible = true;
        }

        return true;
    }
    #endregion

    #region DB Update Functions
    private bool UpdateApplicant(string appIdStr)
    {
        int tmpInt;
        DateTime dt;
        CfsEntity cfsEntity = new CfsEntity();
        Applicant applicant;

        if ((applicant = GetApplicantRecord(cfsEntity, appIdStr)) == null)
        {
            return false;
        }

        /* Applicant Info */
        applicant.FirstName = tBoxFirstName.Text;
        applicant.LastName = tBoxLastName.Text;
        applicant.StageName = tBoxStageName.Text;
        applicant.Email = tBoxEmailAddress.Text;
        applicant.Website = tBoxWebsite.Text;
        applicant.Address1 = tBoxAddress1.Text;
        applicant.Address2 = tBoxAddress2.Text;
        applicant.City = tBoxCity.Text;
        applicant.State = ddlStateApp.SelectedValue;
        applicant.Country = ddlCountryApp.SelectedValue;
        applicant.Zip = tBoxZip.Text;
        applicant.HomePhone = tBoxHomePhone.Text;
        applicant.CellPhone = tBoxCellPhone.Text;
        applicant.AltPhone = tBoxAltPhone.Text;

        /* Vital Stats */
        if (int.TryParse(tBoxHeightFt.Text, out tmpInt)) { applicant.HeightFt = tmpInt; } else { applicant.HeightFt = null; }
        if (int.TryParse(tBoxHeightIn.Text, out tmpInt)) { applicant.HeightIn = tmpInt; } else { applicant.HeightIn = null; }
        if (int.TryParse(tBoxWeight.Text, out tmpInt)) { applicant.Weight = tmpInt; } else { applicant.Weight = null; }

        applicant.EyeColor = tBoxEyeColor.Text;
        applicant.HairColor = tBoxHairColor.Text;
        applicant.Experience = tBoxExperience.Text;
        applicant.DaysAvail = tBoxAvailable.Text;
        applicant.DaysPrefer = tBoxPrefDays.Text;
        applicant.Bust = tBoxBust.Text;
        applicant.Waist = tBoxWaist.Text;
        applicant.Hips = tBoxHips.Text;

        if (DateTime.TryParse(tBoxDateOfBirth.Text, out dt)){ applicant.DOB = dt; } else { applicant.DOB = null; }

        if (cfsEntity.SaveChanges() <= 0)
        {
            return false;
        }
       
        return true;
    }

    private bool AddNewHire(out int newHireId)
    {
        newHireId = 0;
        
        if (ddlTalentType.SelectedValue == "")
        {
            /* Failsafe: User MUST choose Talent Type */
            return false;
        }
        
        int tmpInt;
        DateTime dt;
        
        CfsEntity cfsEntity = new CfsEntity();
        Talent newTal = new Talent();

        /* Record Info */
        newTal.DateCreated = DateTime.Now;
        newTal.DateLastUpdate = DateTime.Now;

        
        /* Talent Info */
        newTal.IsActive = true;
        newTal.TalentType = ddlTalentType.SelectedValue;
        newTal.FirstName = tBoxFirstName.Text;
        newTal.LastName = tBoxLastName.Text;
        newTal.StageName = tBoxStageName.Text;
        newTal.EmailPrimary = tBoxEmailAddress.Text;
        newTal.PersonalSite = tBoxWebsite.Text;
        newTal.Address1 = tBoxAddress1.Text;
        newTal.Address2 = tBoxAddress2.Text;
        newTal.City = tBoxCity.Text;
        newTal.State = ddlStateApp.SelectedValue;
        newTal.Country = ddlCountryApp.SelectedValue;
        newTal.Zip = tBoxZip.Text;
        newTal.HomePhone = tBoxHomePhone.Text;
        newTal.CellPhone = tBoxCellPhone.Text;
        newTal.DisplayName = CfsCommon.FormatDisplayName(ddlTalentType.SelectedValue, newTal.FirstName, newTal.LastName, newTal.StageName, newTal.State);

        if (tBoxAltPhone.Text != "")
        {
            TalentAltPhone newAltPhone = new TalentAltPhone();

            newAltPhone.SortOrder = 0;
            newAltPhone.AltPhoneName = "";
            newAltPhone.AltPhoneNum = tBoxAltPhone.Text;

            newTal.TalentAltPhone.Add(newAltPhone);
        }

        /* Vital Stats */
        if (int.TryParse(tBoxHeightFt.Text, out tmpInt)) { newTal.HeightFt = tmpInt; } else { newTal.HeightFt = null; }
        if (int.TryParse(tBoxHeightIn.Text, out tmpInt)) { newTal.HeightIn = tmpInt; } else { newTal.HeightIn = null; }
        if (int.TryParse(tBoxWeight.Text, out tmpInt)) { newTal.Weight = tmpInt; } else { newTal.Weight = null; }

        newTal.EyeColor = tBoxEyeColor.Text;
        newTal.HairColor = tBoxHairColor.Text;
        newTal.Bust = tBoxBust.Text;
        newTal.Waist = tBoxWaist.Text;
        newTal.Hips = tBoxHips.Text;

        if (DateTime.TryParse(tBoxDateOfBirth.Text, out dt)) { newTal.DOB = dt; } else { newTal.DOB = null; }

        /* Experience, Availability, and Preferred Days data is not currently moved. */

        /* Images */
        newTal.ImageOne = CopyImage(hiddenImgFn1.Value, ddlTalentType.SelectedValue);
        newTal.ImageTwo = CopyImage(hiddenImgFn2.Value, ddlTalentType.SelectedValue);
        newTal.ImageThree = CopyImage(hiddenImgFn3.Value, ddlTalentType.SelectedValue);
        //newTalent ID
        newTal.ImageID1 = CopyImage(hiddenImgID1.Value, "applicant");
        newTal.ImageID2 = CopyImage(hiddenImgID2.Value, "applicant");
        if (newTal.ImageID1 != "")
        {
            string[] splitter = newTal.ImageID1.Split('/');
            try
            {
                newTal.ImageID1 = splitter[1];
            }
            catch (Exception ex)
            { 
            
            }
            
            //newTal.ImageID2 = CopyImage(hiddenImgID2.Value, ddlTalentType.SelectedValue);
        }
        if (newTal.ImageID2 != "")
        {
            string[] splitter2 = newTal.ImageID2.Split('/');
            try
            {
                newTal.ImageID2 = splitter2[1];
            }
            catch (Exception ex)
            {

            }

            //newTal.ImageID2 = CopyImage(hiddenImgID2.Value, ddlTalentType.SelectedValue);
        }
        cfsEntity.AddToTalent(newTal);

        if (cfsEntity.SaveChanges() <= 0)
        {
            return false;
        }

        newHireId = newTal.TalentId;
        
        return true;
    }

    private bool UpdateStatus(string appId, string newStatus)
    {
        if (appId == null || appId == "")
        {
            return false;
        }

        CfsEntity cfEntity = new CfsEntity();

        ObjectQuery<Applicant> applicants = cfEntity.Applicant.Where("it.ApplicantId = " + appId);
        List<Applicant> appList = applicants.ToList();

        if (appList.Count == 1)
        {
            appList[0].Status = newStatus;

            if( cfEntity.SaveChanges() > 0)
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Page Mode Functions
    private void UpdatePageMode(string mode)
    {
        /* Buttons and Labels */
        switch (mode)
        {
            case CfsCommon.MODE_UPDATE:
            {
                headerViewEdit.InnerText = "Edit Applicant Details";
                btnEditApplicant.Text = "Update Applicant";
                hBoxHomePhone.NavigateUrl = null;
                hBoxCellPhone.NavigateUrl = null;
                hBoxAltPhone.NavigateUrl = null;
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                headerViewEdit.InnerText = "View Applicant Details";
                btnEditApplicant.Text = "Edit Applicant";
                hBoxHomePhone.NavigateUrl = "tel:" + tBoxHomePhone.Text;
                hBoxCellPhone.NavigateUrl = "tel:" + tBoxCellPhone.Text;
                hBoxAltPhone.NavigateUrl = "tel:" + tBoxCellPhone.Text;
                break;
            }
            default:
            {
                return; //bad mode
            }
        }

        /* Visible & Enable/Disable Controls */
        if (mode == CfsCommon.MODE_UPDATE)
        {
            //Enables all Textboxes, checkboxes, and dropdowns
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsEditable(c);
            }
        }
        else //Mode is READONLY
        {
            //Disables all Textboxes, checkboxes, and dropdowns
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsNonEditable(c);
            }
        }

        /* Persist */
        hiddenPageMode.Value = mode;
        DoControlExceptions();
    }

    private void DoControlExceptions()
    {
        tBoxHiringNotes.Enabled = true;
        chkContacted.Enabled = true;
        ddlCountryHire.Enabled = true;
        ddlStateHire.Enabled = true;
        ddlTalentType.Enabled = true;

        tBoxHiringNotes.CssClass = "textfield";
        ddlCountryHire.CssClass = "select";
        ddlStateHire.CssClass = "select";
        ddlTalentType.CssClass = "select";
    }
    #endregion

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

    private bool FormIsValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";

        if (tBoxEmailAddress.Text != "" && !CfsCommon.ValidateTextBoxEmail(tBoxEmailAddress, "Email Address", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxHeightFt.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxHeightFt, "Height/Ft", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxHeightIn.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxHeightIn, "Height/In", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxWeight.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxWeight, "Weight", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxDateOfBirth.Text != "" && !CfsCommon.ValidateTextBoxDate(tBoxDateOfBirth, "DOB", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxHomePhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxHomePhone, "Home Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if(tBoxCellPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxCellPhone, "Cell Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxAltPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxAltPhone, "Alt Phone", ulErrorMsg))
        {
            isValid = false;
        }


        divErrorMsg.Visible = !isValid;

        return isValid;
    }

    private void saveEditImageManipulation(EditableImage editableImage)
    {
        // get image ID
        Guid imageID = ImageManager.GetImageIdFromPath(editableImage.ImagePath);

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
}
