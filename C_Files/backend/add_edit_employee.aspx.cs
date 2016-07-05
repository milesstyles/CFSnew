using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using System.IO;
using System.Net;


using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel;

using AtomImageEditor;
using AtomImageEditorServerControls;

using CfsNamespace;

public partial class add_edit_employee : System.Web.UI.Page
{
    const string PARAM_EMPLOYEE_ID = "empid";
    const int ERROR = -1;

    /* NEED this ID for login/setup: ClientId: FDO444 */
    private const string SMS_MSG_API_ID = "3143988";
    private const string SMS_MSG_USER = "centerfold";
    private const string SMS_MSG_PW = "jaguar123";

    /* For the Feature Content 'Category' Temp Table*/

    /* 'Credit' is used like a movie credit */
    public const string TEMP_TABLE_CREDIT_ID = "creditTable";
    public const string TEMP_TABLE_COLUMN_CAT_ID = "creditId";
    public const string TEMP_TABLE_COLUMN_CAT_NAME = "creditName";
    public const string TEMP_TABLE_COLUMN_CAT_DETAILS = "creditDetails";

    public const string TEMP_TABLE_ALT_PHONE_ID = "altPhoneTable";
    public const string TEMP_TABLE_COLUMN_PHONE_PK = "altPhonePk";
    public const string TEMP_TABLE_COLUMN_PHONE_NAME = "altPhoneName";
    public const string TEMP_TABLE_COLUMN_PHONE_NUM = "altPhoneNum";

    public const string TEMP_LIST_WORKS_IN_ID = "worksInList";

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);

        rptrAltPhone.ItemDataBound += new RepeaterItemEventHandler(rptrAltPhone_ItemDataBound);
        rptrWorksIn.ItemDataBound += new RepeaterItemEventHandler(rptrWorksIn_ItemDataBound);

        if (!IsPostBack) /* Only on first page Load */
        {
            Session[TEMP_TABLE_CREDIT_ID] = null;
            Session[TEMP_TABLE_ALT_PHONE_ID] = CreateTempAltPhoneTable(true);
            Session[TEMP_LIST_WORKS_IN_ID] = CreateTempWorksInList(true);
            LoadTempAltPhoneTable(); /* Load, to show first blank Alt Phone entry area */
            LoadTempWorksInList(); /* Load, to show first empty works in dropdown */

            CfsCommon.GetStateList(ddlState);
            CfsCommon.GetCountryList(ddlCountry, false);
            CfsCommon.GetTalentTypeList(ddlTalentType, true);
           
            
            CfsCommon.GetTalentTypeListCheckbox(chkBoxAdditionalTalent, false);
            
            CfsCommon.GetFullTalentList(ddlFullTalentList);

            if (Request.Params[PARAM_EMPLOYEE_ID] != null)
            {
                if (GetEmployeeInfo(Request.Params[PARAM_EMPLOYEE_ID].ToString()))
                {
                    UpdatePageMode(CfsCommon.MODE_READONLY);
                    UpdateFeatureContentMode(CfsCommon.MODE_READONLY);
                }
            }
            else
            {
                // New employee so default to active
                chkActive.Checked = true;
            }
            hNumberLink.NavigateUrl = "tel:" + tBoxHomePhone.Text;
            cNumberLink.NavigateUrl = "tel:" + tBoxCellPhone.Text;
           
            
            LoadTempAltPhoneTable();
            LoadTempWorksInList();
            LoadTempTalentCreditTable();
        }
        else
        {
            /* Have to save this on every Postback, or we could lose the last record */
            SaveCurAltPhoneViewToTempTable();
            SaveCurWorksInView();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        tBoxDateHired.Enabled = false;

        base.OnPreRender(e);
    }

    protected void OnClick_btnGetTalentInfo(object sender, EventArgs e)
    {
        if (ddlFullTalentList.SelectedValue != "")
        {
            Response.Redirect("add_edit_employee.aspx?" + PARAM_EMPLOYEE_ID + "=" + ddlFullTalentList.SelectedValue);
        }
    }

    protected void OnClick_btnAddAltPhone(object sender, EventArgs arrgggs)
    {
        AddRowToAltPhoneTable("", "", "");
        LoadTempAltPhoneTable();
    }

    protected void OnClick_btnAddWorksIn(object sender, EventArgs arrgggs)
    {
        AddRowToWorksInList("");
        LoadTempWorksInList();
    }

    protected void OnClick_btnContactTalent(object sender, EventArgs e)
    {
        if (IsSmsDataValid())
        {
            string errMsg;

            divTalMsgFeedback.InnerHtml = "";
            if (SendTxtMessage(tBoxCellPhone.Text, tBoxContactTalent.Text, out errMsg))
            {
                divTalMsgFeedback.InnerHtml = "Message Sent to: " + tBoxCellPhone.Text;
            }
            else
            {
                divTalMsgFeedback.InnerHtml = errMsg;
            }
        }
    }

    protected void OnClick_btnAddEditTalent(object sender, EventArgs e)
    {
        int talId;
        
        switch (hiddenCurrentMode.Value)
        {
            case CfsCommon.MODE_ADD:
            case CfsCommon.MODE_UPDATE:
            {
                if (FormIsValid())
                {
                    talId = AddOrUpdateTalent(ddlFullTalentList.SelectedValue);
                    /*update ymlp*/
                    string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                    Myxmlhttp callme = new Myxmlhttp();
                    callme.xmlhttp(ymlpurl);
                    int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
                    if (groupid == 0)
                    { }
                    else
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);
                    }
                    if (chkFeatureTalent.Checked)
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=30&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);

                    }
                    if (talId != ERROR)
                    {
                        /* Do a new HTTP GET, to refresh the page */
                        Response.Redirect("add_edit_employee.aspx?" + PARAM_EMPLOYEE_ID + "=" + talId.ToString(), true);
                    }
                }
                break;   
            }
            case CfsCommon.MODE_READONLY:
            {
                if (chkActive.Checked == true)
                {
                    string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                    Myxmlhttp callme = new Myxmlhttp();
                    callme.xmlhttp(ymlpurl);
                    int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
                    if (groupid == 0)
                    { }
                    else
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);
                    }
                    if (chkFeatureTalent.Checked)
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=30&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);

                    }
                }
                else
                {

                    string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                    Myxmlhttp callme = new Myxmlhttp();
                    callme.xmlhttp(ymlpurl);
                    int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
                    if (groupid == 0)
                    { }
                    else
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Delete?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);
                    }
                    if (chkFeatureTalent.Checked)
                    {
                        ymlpurl = "https://www.ymlp.com/api/Contacts.Delete?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=30&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                        callme.xmlhttp(ymlpurl);

                    }
                
                }
                UpdatePageMode(CfsCommon.MODE_UPDATE);  //Button Changes mode to Update
                UpdateFeatureContentMode(CfsCommon.MODE_ADD);
                break;
            }
            default:
            {
                /* should never happen */
                return;
            }
        }

        LoadTempTalentCreditTable(); //Need to reload to change buttons
    }

    protected void OnClick_btnMediaDel(object sender, EventArgs e)
    {
        string btnName = ((Button)sender).ID;

        switch (btnName)
        {
            case "btnNewTalImgDel":{ DoMediaDelete(linkNewTalentImg, upNewTalentImg, btnNewTalImgDel,7); break; }
            case "btnThumbImgDel": { DoMediaDelete(linkThumbImg, upThumbImg, btnThumbImgDel,4); break; }
            case "btnImgOneDel":   { DoMediaDelete(linkImageOne, upImageOne, btnImgOneDel,1); break; }
            case "btnImgTwoDel":   { DoMediaDelete(linkImageTwo, upImageTwo, btnImgTwoDel,2); break; }
            case "btnImgThreeDel": { DoMediaDelete(linkImageThree, upImageThree, btnImgThreeDel,3); break; }
            case "btnVidOneDel":      { DoMediaDelete(linkVideoOne, upVideoOne, btnVidOneDel); break; }
            case "btnVidTwoDel":      { DoMediaDelete(linkVideoTwo, upVideoTwo, btnVidTwoDel); break; }
            case "btnVidThreeDel":    { DoMediaDelete(linkVideoThree, upVideoThree, btnVidThreeDel); break; }
            case "btnDeleteGovernmentID": { DoMediaDelete(linkImagePhtoID, FileUploadID, btnDeleteGovernmentID); break; }
        }
    
    }

    protected void OnClick_btnAddUpdateCredits(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (hiddenCreditMode.Value == CfsCommon.MODE_ADD)
        {
            AddCreditToTempTable();
            hiddenCreditUpdateKey.Value = "";
        }
        else if (hiddenCreditMode.Value == CfsCommon.MODE_UPDATE)
        {
            if ( !string.IsNullOrEmpty(hiddenCreditUpdateKey.Value) )
            {
                UpdateCreditInTempTable(hiddenCreditUpdateKey.Value);
            }
        }
        
        LoadTempTalentCreditTable();
        tBoxCatName.Text = "";
        tBoxCatDetails.Text = "";
    }

    protected void OnClick_btnEditCredit(object sender, EventArgs e)
    {
        /* Loads the Selected Row into Textboxes for Editing */
        
        Button btn = (Button)sender;
        int rowNum;

        if (Session[TEMP_TABLE_CREDIT_ID] == null)
        {
            return; /* Dont expect this ever to happen */
        }

        DataTable crTable = (DataTable)Session[TEMP_TABLE_CREDIT_ID];

        if (int.TryParse(btn.CommandArgument, out rowNum))
        {
            hiddenCreditUpdateKey.Value = (string)crTable.Rows[rowNum][TEMP_TABLE_COLUMN_CAT_ID];

            tBoxCatName.Text = (string)crTable.Rows[rowNum][TEMP_TABLE_COLUMN_CAT_NAME];
            tBoxCatDetails.Text = (string)crTable.Rows[rowNum][TEMP_TABLE_COLUMN_CAT_DETAILS];

            UpdateFeatureContentMode(CfsCommon.MODE_UPDATE);
        }
    }

    protected void OnClick_btnDeleteCredit(object sender, EventArgs e)
    {
        int rowNum = -1;
        Button btn = (Button)sender;

        if ( int.TryParse(btn.CommandArgument, out rowNum) )
        {
            DeleteRowFromTempTable(rowNum);
        }

        LoadTempTalentCreditTable();
    }

    protected void OnClick_btnMoveUp(object sender, EventArgs e)
    {
        int rowNum = -1;
        Button btn = (Button)sender;

        if( int.TryParse(btn.CommandArgument, out rowNum) )
        {
            MoveCreditUp(rowNum);
        }

        LoadTempTalentCreditTable();
    }
    #endregion Page Events

    #region Callbacks
    private void rptrAltPhone_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e == null || e.Item == null)
        {
            return;
        }

        TextBox tBoxName = (TextBox)e.Item.FindControl("tBoxAltPhoneName");
        TextBox tBoxNum = (TextBox)e.Item.FindControl("tBoxAltPhoneNum");
        ImageButton iBtn = (ImageButton)e.Item.FindControl("btnAddAltPhone");

        if (tBoxNum.Text == "")
        {
            HyperLink hypl = (HyperLink)e.Item.FindControl("bestNumberLink");
            tBoxNum.Text = hypl.Text;
        }

        if (e.Item.ItemIndex == 0)
        {
            iBtn.Visible = true;
        }
        else
        {
            iBtn.Visible = false;
        }

        if (hiddenCurrentMode.Value == CfsCommon.MODE_READONLY)
        {
            tBoxName.Enabled = false;
            tBoxNum.Enabled = false;

            tBoxName.CssClass = "textfieldreadonly";
            tBoxNum.CssClass = "textfieldreadonly";
            iBtn.Enabled = false;
            HyperLink hyp1 = (HyperLink)e.Item.FindControl("bestNumberLink");
            hyp1.Enabled = true;
            //hyp1.NavigateUrl = "";
        }
        else
        {
            HyperLink hyp1 = (HyperLink)e.Item.FindControl("bestNumberLink");
          //  hyp1.Enabled = true;
           
            if (hyp1.Text != "")
            {
                hyp1.Enabled = true;
            }
            tBoxName.Enabled = true;
            tBoxNum.Enabled = true;
      
            tBoxName.CssClass = "textfield";
            tBoxNum.CssClass = "textfield";
            iBtn.Enabled = true;
        }
    }

    private void rptrWorksIn_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e == null || e.Item == null)
        {
            return;
        }

        ImageButton iBtn = (ImageButton)e.Item.FindControl("btnAddWorksIn");
        DropDownList ddlWorksIn = (DropDownList)e.Item.FindControl("ddlStateWorksIn");

        if (iBtn == null || ddlWorksIn == null)
        {
            /* This should never happen */
            return;
        }

        if (e.Item.ItemIndex == 0)
        {
            iBtn.Visible = true;
        }
        else
        {
            iBtn.Visible = false;
        }
        
        CfsCommon.GetStateList(ddlWorksIn);
        ddlWorksIn.SelectedValue = (string)e.Item.DataItem;

        if (hiddenCurrentMode.Value == CfsCommon.MODE_READONLY)
        {
            ddlWorksIn.Enabled = false;
            iBtn.Enabled = false;
            ddlWorksIn.CssClass = "selectreadonly";
        }
        else
        {
            ddlWorksIn.Enabled = true;
            iBtn.Enabled = true;
            ddlWorksIn.CssClass = "select";
        }
    }
    #endregion

    #region DB Read Only Functions
    private bool GetEmployeeInfo(string empIdStr)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Talent talent;

        if ((talent = CfsCommon.GetTalentRecord(cfsEntity, empIdStr)) == null)
        {
            return false;
        }

        ddlFullTalentList.SelectedValue = empIdStr;

        /**** TALENT INFO SECTION *********/
        chkActive.Checked = (bool)talent.IsActive;
        string[] talents = talent.TalentType.Split('*');
        
        foreach (string tal in talents)
        {
            foreach(ListItem item in chkBoxAdditionalTalent.Items)
            
            {
                if ( item.Text == tal)
                { 
                item.Selected=true;
                }
            }
        }

        ddlTalentType.SelectedValue = talents[0];
        hiddenCurTalType.Value = talent.TalentType;
        tBoxFirstName.Text = talent.FirstName;
        tBoxLastName.Text = talent.LastName;
        tBoxStageName.Text = talent.StageName;
        tBoxDateHired.Text = talent.DateCreated.ToString("MM/dd/yyyy");
        tBoxEmail.Text = talent.EmailPrimary;
        tBoxExtraEmail.Text = talent.EmailSecondary;
        tBoxAddress1.Text = talent.Address1;
        tBoxAddress2.Text = talent.Address2;
        tBoxCity.Text = talent.City;
        ddlState.SelectedValue = talent.State;
        ddlCountry.SelectedValue = talent.Country;
        tBoxZip.Text = talent.Zip;
        tBoxHomePhone.Text = talent.HomePhone;
        tBoxCellPhone.Text = talent.CellPhone;
        tBoxImClient.Text = talent.IMClient;
        tBoxImName.Text = talent.IMName;
        tBoxPersonalWebsite.Text = talent.PersonalSite;
        tBoxSpecialNotes.Text = talent.SpecialNotes;

        /**** EMPLOYEE IMAGES AND MEDIA SECTION ****/
        linkNewTalentImg.Text = talent.NewTalentImg;
        linkThumbImg.Text = talent.ThumbImg;
        linkImageOne.Text = talent.ImageOne;
        linkImageTwo.Text = talent.ImageTwo;
        linkImageThree.Text = talent.ImageThree;
        linkVideoOne.Text = talent.VideoOne;
        linkVideoTwo.Text = talent.VideoTwo;
        linkVideoThree.Text = talent.VideoThree;
        //HyperlinkGovernmentID.Text = talent.ImageID1;
        int empi = Convert.ToInt32(empIdStr);
        Guid imageGuidID1 = Guid.Empty;
        
        try
        {
            imageGuidID1 = ImageManager.GetImageGuid(4,empi, CfsCommon.TALENT_TYPE_ID_APPLICANT, talent.ImageID1);
        }
        catch
        {
        }
        if (imageGuidID1 == Guid.Empty)
        {
        }

        else
        {
            string[] talentypes = talent.TalentType.Split('*');
            string thistalenttype = talentypes[0];
           
            imgID1.Src = "../talentimages/applicant/" + talent.ImageID1.ToString();
            if (imgID1.Src != null)
            {
                if (imgID1.Src.ToString().Contains("jpg") | imgID1.Src.ToString().Contains("gif"))
                {
                    imgID1.Visible = true;
                    linkImagePhtoID.NavigateUrl = imgID1.Src;
                    linkImagePhtoID.Visible = true;
                }
                else
                {
                    imgID1.Visible = false;
                    linkImagePhtoID.Visible = false;
                }
            }
            else
            {
                imgID1.Visible = false;
                linkImagePhtoID.Visible = false;
            }
        }
/****   FOr when we include back of id *****/
     /*   Guid imageGuidID2 = Guid.Empty;

        try
        {
            imageGuidID2 = ImageManager.GetImageGuid(5, empi, CfsCommon.TALENT_TYPE_ID_APPLICANT, talent.ImageID2);
        }
        catch
        {
        }
        if (imageGuidID2 == Guid.Empty)
        {
        }

        else
        {
            string[] talentypes = talent.TalentType.Split('*');
            string thistalenttype = talentypes[0];

            imgID2.Src = "../talentimages/applicant/" + talent.ImageID1.ToString();
            if (imgID2.Src != null)
            {
                if (imgID2.Src.ToString().Contains("jpg") | imgID2.Src.ToString().Contains("gif"))
                {
                    imgID2.Visible = true;
                    linkImagePhtoID2.NavigateUrl = imgID2.Src;
                    linkImagePhtoID2.Visible = true;
                }
                else
                {
                    imgID2.Visible = false;
                    linkImagePhtoID2.Visible = false;
                }
            }
            else
            {
                imgID2.Visible = false;
                linkImagePhtoID2.Visible = false;
            }
        }

        */////*******


        linkNewTalentImg.NavigateUrl = "~/" + CfsCommon.IMAGE_PATH_BASE + talent.NewTalentImg;
        linkThumbImg.NavigateUrl = "~/" + CfsCommon.IMAGE_PATH_BASE + talent.ThumbImg;
        linkImageOne.NavigateUrl = "edit_images.aspx?id=" + empIdStr + "&placeValuesBeforeTB_=savedValues&TB_iframe=true&height=800&width=1020&modal=true";
        linkImageOne.CssClass = "thickbox";
        linkImageTwo.NavigateUrl = "edit_images.aspx?id=" + empIdStr + "&placeValuesBeforeTB_=savedValues&TB_iframe=true&height=800&width=1020&modal=true";
        linkImageTwo.CssClass = "thickbox";
        linkImageThree.NavigateUrl = "edit_images.aspx?id=" + empIdStr + "&placeValuesBeforeTB_=savedValues&TB_iframe=true&height=800&width=1020&modal=true";
        linkImageThree.CssClass = "thickbox";

        /**** VITAL STATS SECTION ****/
        tBoxBust.Text = talent.Bust;
        tBoxWaist.Text = talent.Waist;
        tBoxHips.Text = talent.Hips;

        if (talent.HeightFt != null)
        {
            tBoxHeightFt.Text = talent.HeightFt.ToString();
        }
        if (talent.HeightIn != null)
        {
            tBoxHeightIn.Text = talent.HeightIn.ToString();
        }
        
        tBoxHairColor.Text = talent.HairColor;
        tBoxEyeColor.Text = talent.EyeColor;

        if (talent.Weight != null)
        {
            tBoxWeight.Text = talent.Weight.ToString();
        }
        
        ddlRace.SelectedValue = talent.Race;

        if (talent.DOB == null)
        {
            tBoxDateOfBirth.Text = "";
            lblAgeInYears.Text = "()";
        }
        else
        {
            tBoxDateOfBirth.Text = ((DateTime)talent.DOB).ToString("MM/dd/yyyy");
            lblAgeInYears.Text = CfsCommon.CalculateAge((DateTime)talent.DOB);
        }
        
        /* CheckBoxes */
        chkToys.Checked = talent.DoesToys;
        chkFullNudeStrip.Checked = talent.DoesFullNudeStrip;
        chkLesbianShow.Checked = talent.DoesLesbianShow;
        chkPromoModel.Checked = talent.DoesPromoModeling;
        chkToplessBartender.Checked = talent.DoesToplessBartender;
        chkPoleDancer.Checked = talent.DoesPoleDancing;
        chkToplessStrip.Checked = talent.DoesToplessStrip;
        chkToplessWtr.Checked = talent.DoesToplessWaiting;
        chkPopOutCake.Checked = talent.DoesPopoutCake;
        chkInternetChat.Checked = talent.DoesInternetChat;
        chkToplessCard.Checked = talent.DoesToplessCardDealing;
        chkLapDancer.Checked = talent.DoesLapdancing;
        try
        {
            chkFeatureDancing.Checked = talent.IsFeatureDancer.GetValueOrDefault(false);

        }
        catch(Exception ex)
        {
            chkFeatureDancing.Checked = false;
        }
        
        /* Large Text Fields */
        tBoxLikes.Text = talent.Likes;
        tBoxDislikes.Text = talent.Dislikes;
        tBoxCostumes.Text = talent.Costumes;
        tBoxTalents.Text = talent.SpecialTalents;

        /**** ALT PHONE #s ****/
        GetEmployeeAltPhones(talent);

        /**** Works In *****/
        GetEmployeeWorksIn(talent);
        
        /**** FEATURE SECTION ****/
        chkFeatureTalent.Checked = talent.IsFeatureTalent;
        GetEmployeeCredits(talent);

        /**** HOLD INFORMATION ****/
        GetEmployeeHold(talent);

       
        /**** Display "Contact Talent", only if Cell # present ****/
        if (tBoxCellPhone.Text != "")
        {
            uPnlContactTalent.Visible = true;
        }

        /*** Display 'Send email icon' Only if email is present **/
        if (tBoxEmail.Text != "")
        {
            hlEmailSend.NavigateUrl = "mailto:" + tBoxEmail.Text;
            hlEmailSend.Visible = true;
        }

        if (tBoxExtraEmail.Text != "")
        {
            hlExtraEmailSend.NavigateUrl = "mailto:" + tBoxExtraEmail.Text;
            hlExtraEmailSend.Visible = true;
        }

        return true;
    }

    private bool GetEmployeeHold(Talent talRecord)
    {
        if (talRecord.TalentHold != null)
        {
            if (!talRecord.TalentHold.IsLoaded)
            {
                talRecord.TalentHold.Load();
            }

            List<TalentHold> holdList = talRecord.TalentHold.ToList();

            if (holdList.Count >= 1)
            {
                tBoxHoldDate.Text = holdList[0].DateStart.ToString("MM/dd/yyyy");
                tBoxHoldLocation.Text = holdList[0].CurLocation;
                tBoxHoldNotes.Text = holdList[0].Notes;
                return true;
            }
        }

        return false;
    }

    private void GetEmployeeCredits(Talent talRecord)
    {
        DataTable dtTable = null;

        if (!talRecord.TalentCredit.IsLoaded)
        {
            talRecord.TalentCredit.Load();
        }

        if (talRecord.TalentCredit.Count > 0)
        {
            dtTable = CreateTempCreditTable();

            string[] data = new string[3];

            SortedList sortList = new SortedList();

            List<TalentCredit> list = talRecord.TalentCredit.ToList();

            /* Extra code to Sort Numbers, since I can't get Entities to Sort properly */
            int count = 0;
            foreach (TalentCredit crdRecord in list)
            {
                CfsCommon.TalentCreditObj crdObj = new CfsCommon.TalentCreditObj();

                crdObj.Key = count;
                crdObj.SortId = crdRecord.SortOrder;
                crdObj.Name = crdRecord.Name;
                crdObj.Details = crdRecord.Info;

                sortList.Add(crdObj.SortId, crdObj);
                count++;
            }

            for (int i = 0; i < sortList.Count; i++)
            {
                data[0] = ((CfsCommon.TalentCreditObj)sortList[i]).Key.ToString();
                data[1] = ((CfsCommon.TalentCreditObj)sortList[i]).Name;
                data[2] = ((CfsCommon.TalentCreditObj)sortList[i]).Details;

                dtTable.LoadDataRow(data, true);
            }
        }

        /* Persist */
        Session[TEMP_TABLE_CREDIT_ID] = dtTable;
    }

    private void GetEmployeeAltPhones(Talent talRecord)
    {
        DataTable dtTable; 
        
        if (!talRecord.TalentAltPhone.IsLoaded)
        {
            talRecord.TalentAltPhone.Load();
        }

        if (talRecord.TalentAltPhone.Count == 0)
        {
            dtTable = CreateTempAltPhoneTable(true);
        }
        else
        {
            dtTable = CreateTempAltPhoneTable(false);
            string[] data = new string[3];

            var phoneList = from record in talRecord.TalentAltPhone
                            orderby record.SortOrder
                            select record;

            foreach (TalentAltPhone altPhne in phoneList)
            {
                data[0] = altPhne.AltPhoneId.ToString();
                data[1] = altPhne.AltPhoneName;
                data[2] = altPhne.AltPhoneNum;

                dtTable.LoadDataRow(data, true);
            }
        }

        /* Persist */
        Session[TEMP_TABLE_ALT_PHONE_ID] = dtTable;
    }

    private void GetEmployeeWorksIn(Talent talRecord)
    {
        List<string> worksIn;
        
        if (!talRecord.TalentWorksIn.IsLoaded)
        {
            talRecord.TalentWorksIn.Load();
        }

        if (talRecord.TalentWorksIn.Count == 0)
        {
            worksIn = CreateTempWorksInList(true);
        }
        else
        {
            worksIn = CreateTempWorksInList(false);

            foreach (TalentWorksIn wRec in talRecord.TalentWorksIn)
            {
                worksIn.Add(wRec.State);
            }
        }

        /* Persist */
        Session[TEMP_LIST_WORKS_IN_ID] = worksIn;
    }

    #endregion

    #region DB Add or Update Functions
    private int AddOrUpdateTalent(string talentId)
    {
        int tmpInt;
        DateTime dob;
        if (talentId == "")
        {
            try
            {
                talentId = Request.QueryString["empid"].ToString();
            }
            catch (Exception err)
            {
                talentId = "";
            }
        }
        CfsEntity cfEntity = new CfsEntity();
        Talent newTal;

        if (ddlTalentType.SelectedValue == "")
        {
            /* Failsafe: This should be handled in Validation.
             * User MUST choose a talent type.
             */
            return ERROR;
        }

        if (talentId == null || talentId == "")
        {
            /* New Talent Record (ADD) */
            newTal = new Talent();
            newTal.DateCreated = DateTime.Now;
        }
        else
        {
            /* Get Existing Talent Record for Update */
            if ((newTal = CfsCommon.GetTalentRecord(cfEntity, talentId)) == null)
            {
                return ERROR;
            }
            if (chkFeatureTalentFrontPage.Checked == true)
            {
                newTal.DateCreated = DateTime.Now;
            }
        }

        newTal.DateLastUpdate = DateTime.Now;

        /* Talent Info */

        string TalentTypeALL = "";
        foreach (ListItem x in chkBoxAdditionalTalent.Items)
        {
            if (x.Selected==true)
            {
                TalentTypeALL = TalentTypeALL + "*" + x.Text;
            }
        }
        if (TalentTypeALL != "")
        {
            TalentTypeALL = ddlTalentType.SelectedValue + "*" + TalentTypeALL;
        }
        else
        {
            TalentTypeALL = ddlTalentType.SelectedValue;
        }
        newTal.IsActive = chkActive.Checked;
        newTal.TalentType = TalentTypeALL;
        newTal.FirstName = tBoxFirstName.Text;
        newTal.LastName = tBoxLastName.Text;
        newTal.StageName = tBoxStageName.Text;
        newTal.EmailPrimary = tBoxEmail.Text;
        newTal.EmailSecondary = tBoxExtraEmail.Text;
        newTal.Address1 = tBoxAddress1.Text;
        newTal.Address2 = tBoxAddress2.Text;
        newTal.City = tBoxCity.Text;
        newTal.State = ddlState.SelectedValue;
        if (ddlState.SelectedValue.ToUpper() == "CANADA")
        {
         //   newTal.State = "CN";
        }
        newTal.Country = ddlCountry.SelectedValue;
        newTal.Zip = tBoxZip.Text;
        newTal.HomePhone = tBoxHomePhone.Text;
        newTal.CellPhone = tBoxCellPhone.Text;
        newTal.DisplayName = CfsCommon.FormatDisplayName(ddlTalentType.Text, tBoxFirstName.Text, tBoxLastName.Text, tBoxStageName.Text, ddlState.SelectedValue);

        /* IM, Personal Site */
        newTal.IMClient = tBoxImClient.Text;
        newTal.IMName = tBoxImName.Text;
        newTal.PersonalSite = tBoxPersonalWebsite.Text;
        newTal.SpecialNotes = tBoxSpecialNotes.Text;

        /* Images and Media (Uploads) */
        DoImagesProcessing(talentId, newTal);
        DoVideoProcessing(talentId, newTal);
        
        /*do ID processin*/


        /* Vital Stats */
        newTal.Bust = tBoxBust.Text;
        newTal.Waist = tBoxWaist.Text;
        newTal.Hips = tBoxHips.Text;

        if (int.TryParse(tBoxHeightFt.Text, out tmpInt)) { newTal.HeightFt = tmpInt; } else { newTal.HeightFt = null; }
        if (int.TryParse(tBoxHeightIn.Text, out tmpInt)) { newTal.HeightIn = tmpInt; } else { newTal.HeightIn = null; }

        newTal.HairColor = tBoxHairColor.Text;
        newTal.EyeColor = tBoxEyeColor.Text;

        if (int.TryParse(tBoxWeight.Text, out tmpInt)) { newTal.Weight = tmpInt; } else { newTal.Weight = null; }
        if (DateTime.TryParse(tBoxDateOfBirth.Text, out dob)) { newTal.DOB = dob; } else { newTal.DOB = null; }

        newTal.Race = ddlRace.SelectedValue;

        /* CheckBoxes */
        newTal.DoesToys = chkToys.Checked;
        newTal.DoesFullNudeStrip = chkFullNudeStrip.Checked;
        newTal.DoesLesbianShow = chkLesbianShow.Checked;
        newTal.DoesPromoModeling = chkPromoModel.Checked;
        newTal.DoesToplessBartender = chkToplessBartender.Checked;
        newTal.DoesPoleDancing = chkPoleDancer.Checked;
        newTal.DoesToplessStrip = chkToplessStrip.Checked;
        newTal.DoesToplessWaiting = chkToplessWtr.Checked;
        newTal.DoesPopoutCake = chkPopOutCake.Checked;
        newTal.DoesInternetChat = chkInternetChat.Checked;
        newTal.DoesToplessCardDealing = chkToplessCard.Checked;
        newTal.DoesLapdancing = chkLapDancer.Checked;
       // newTal.IsFeatureDancer = chkFeatureDancing.Checked;
        newTal.IsFeatureDancer = chkFeatureDancing.Checked;
        //Big Textfields
        newTal.Likes = tBoxLikes.Text;
        newTal.Dislikes = tBoxDislikes.Text;
        newTal.Costumes = tBoxCostumes.Text;
        newTal.SpecialTalents = tBoxTalents.Text;
       // newTal.WorksInList = 
        //Feature Content:
        newTal.IsFeatureTalent = chkFeatureTalent.Checked;

        if (chkFeatureTalent.Checked)
        {
            CfsEntity cfEntitySaas = new CfsEntity("CfsEntitySA","saas");
           
                /* Create New Talent Record (ADD) */
                Talent copytal = new Talent();
                copytal.DateCreated = DateTime.Now;
                if (talentId == null || talentId == "")
                {
                    ///* New Talent Record (ADD) */
                    //copytal = new Talent();
                    //copytal.DateCreated = DateTime.Now;
                }
                else
                {
                    ///* Get Existing Talent Record for Update */
                    //if ((copytal = CfsCommon.GetTalentRecord(cfEntity, talentId)) == null)
                    //{
                    //    return ERROR;
                    //}
                }

                copytal.DateLastUpdate = DateTime.Now;

                /* Talent Info */
                copytal.IsActive = chkActive.Checked;
                copytal.TalentType = TalentTypeALL;
                copytal.FirstName = tBoxFirstName.Text;
                copytal.LastName = tBoxLastName.Text;
                copytal.StageName = tBoxStageName.Text;
                copytal.EmailPrimary = tBoxEmail.Text;
                copytal.EmailSecondary = tBoxExtraEmail.Text;
                copytal.Address1 = tBoxAddress1.Text;
                copytal.Address2 = tBoxAddress2.Text;
                copytal.City = tBoxCity.Text;
                copytal.State = ddlState.SelectedValue;
                copytal.Country = ddlCountry.SelectedValue;
                copytal.Zip = tBoxZip.Text;
                copytal.HomePhone = tBoxHomePhone.Text;
                copytal.CellPhone = tBoxCellPhone.Text;
                copytal.DisplayName = CfsCommon.FormatDisplayName(ddlTalentType.Text, tBoxFirstName.Text, tBoxLastName.Text, tBoxStageName.Text, ddlState.SelectedValue);

                /* IM, Personal Site */
                copytal.IMClient = tBoxImClient.Text;
                copytal.IMName = tBoxImName.Text;
                copytal.PersonalSite = tBoxPersonalWebsite.Text;
                copytal.SpecialNotes = tBoxSpecialNotes.Text;

                /* Images and Media (Uploads) */
               // DoImagesProcessingSaas(talentId, copytal);
                DoVideoProcessing(talentId, copytal);

                /* Vital Stats */
                copytal.Bust = tBoxBust.Text;
                copytal.Waist = tBoxWaist.Text;
                copytal.Hips = tBoxHips.Text;

                if (int.TryParse(tBoxHeightFt.Text, out tmpInt)) { copytal.HeightFt = tmpInt; } else { copytal.HeightFt = null; }
                if (int.TryParse(tBoxHeightIn.Text, out tmpInt)) { copytal.HeightIn = tmpInt; } else { copytal.HeightIn = null; }

                copytal.HairColor = tBoxHairColor.Text;
                copytal.EyeColor = tBoxEyeColor.Text;

                if (int.TryParse(tBoxWeight.Text, out tmpInt)) { copytal.Weight = tmpInt; } else { copytal.Weight = null; }
                if (DateTime.TryParse(tBoxDateOfBirth.Text, out dob)) { copytal.DOB = dob; } else { copytal.DOB = null; }

                copytal.Race = ddlRace.SelectedValue;

                /* CheckBoxes */
                copytal.DoesToys = chkToys.Checked;
                copytal.DoesFullNudeStrip = chkFullNudeStrip.Checked;
                copytal.DoesLesbianShow = chkLesbianShow.Checked;
                copytal.DoesPromoModeling = chkPromoModel.Checked;
                copytal.DoesToplessBartender = chkToplessBartender.Checked;
                copytal.DoesPoleDancing = chkPoleDancer.Checked;
                copytal.DoesToplessStrip = chkToplessStrip.Checked;
                copytal.DoesToplessWaiting = chkToplessWtr.Checked;
                copytal.DoesPopoutCake = chkPopOutCake.Checked;
                copytal.DoesInternetChat = chkInternetChat.Checked;
                copytal.DoesToplessCardDealing = chkToplessCard.Checked;
                copytal.DoesLapdancing = chkLapDancer.Checked;
                copytal.IsFeatureDancer = chkFeatureDancing.Checked;

                //Big Textfields
                copytal.Likes = tBoxLikes.Text;
                copytal.Dislikes = tBoxDislikes.Text;
                copytal.Costumes = tBoxCostumes.Text;
                copytal.SpecialTalents = tBoxTalents.Text;

                //Feature Content:
                copytal.IsFeatureTalent = chkFeatureTalent.Checked;
                cfEntitySaas.AddToTalent(copytal);
                //AddOrUpdateAltPhoneList(cfEntitySaas, newTal);

                /* Add or Update to Table 'TalentWorksIn' */
                //AddOrUpdateWorksInList(cfEntitySaas, newTal);

                /* Add or Update to Table 'TalentHold' */
                //AddOrUpdateHold(cfEntitySaas, newTal);

                /* Add or Update to Table 'TalentCredit' */
                //AddOrUpdateCredits(cfEntitySaas, newTal);
                cfEntitySaas.SaveChanges();
            
        }

        if (string.IsNullOrEmpty(talentId))
        {
            /* Create New Talent Record (ADD) */
            cfEntity.AddToTalent(newTal);
        }
        //else will just Update Current Talent

        /* Add Or Update to Table 'Talent' */
        if (cfEntity.SaveChanges() <= 0)
        {
            return ERROR;
        }

        /* Add or Update to Table 'TalentAltPhone' */
        AddOrUpdateAltPhoneList(cfEntity, newTal);

        /* Add or Update to Table 'TalentWorksIn' */
        AddOrUpdateWorksInList(cfEntity, newTal);

        /* Add or Update to Table 'TalentHold' */
        AddOrUpdateHold(cfEntity, newTal);

        /* Add or Update to Table 'TalentCredit' */
        AddOrUpdateCredits(cfEntity, newTal);

       

        cfEntity.SaveChanges();
        if (chkActive.Checked == true)
        {
            string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
            Myxmlhttp callme = new Myxmlhttp();
            callme.xmlhttp(ymlpurl);
            int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
            if (groupid == 0)
            { }
            else
            {
                ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                callme.xmlhttp(ymlpurl);
            }
            if (chkFeatureTalent.Checked)
            {
                ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=30&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                callme.xmlhttp(ymlpurl);

            }
        }
        else
        {
            string ymlpurl = "https://www.ymlp.com/api/Contacts.Delete?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=39&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
            Myxmlhttp callme = new Myxmlhttp();
            callme.xmlhttp(ymlpurl);
            int groupid = callme.getmygroup(ddlTalentType.SelectedValue.ToString());
            if (groupid == 0)
            { }
            else
            {
                ymlpurl = "https://www.ymlp.com/api/Contacts.Delete?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=" + groupid + "&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                callme.xmlhttp(ymlpurl);
            }
            if (chkFeatureTalent.Checked)
            {
                ymlpurl = "https://www.ymlp.com/api/Contacts.Delete?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=30&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text + "&Field7=" + ddlTalentType.SelectedValue;
                callme.xmlhttp(ymlpurl);

            }
        }

        return newTal.TalentId;
    }

    private void AddOrUpdateHold(CfsEntity cfsEntity, Talent talRecord)
    {
        bool addFlag = false;
        bool updateFlag = false;
        bool removeFlag = false;
        
        if (!talRecord.TalentHold.IsLoaded)
        {
            talRecord.TalentHold.Load();
        }

        #region Decide what needs to be done
        if (tBoxHoldDate.Text == "" &&
            tBoxHoldLocation.Text == "" &&
            tBoxHoldNotes.Text == "")
        {
            if (talRecord.TalentHold.Count == 0)
            {
                /* There are no 'hold' records associated w/ this Talent,
                 * and no data in the hold boxes, so no action is necessary
                 */
                return;
            }
            else
            {
                /* There is no data in the boxes, but a record exists. Need to
                 * remove that record.
                 */
                removeFlag = true;
            }
        }
        else
        {
            if (talRecord.TalentHold.Count == 0)
            {
                /* There is data in the hold boxes, but no associated hold record exists.
                 * We need to add a new record.
                 */
                addFlag = true;
            }
            else
            {
                /* There is data in the hold boxes, and a record already exists. Update
                 * the current record.
                 */
                updateFlag = true;
            }
        }
        #endregion

        TalentHold holdRecord = null;
        DateTime dtStart;

        if (removeFlag)
        {
            holdRecord = talRecord.TalentHold.ToList()[0];
            cfsEntity.DeleteObject(holdRecord);
            return;
        }
        else if (updateFlag)
        {
            /* Setup the reference */
            holdRecord = talRecord.TalentHold.ToList()[0];
        }
        else if (addFlag)
        {
            /* Create a new Hold Record */
            holdRecord = new TalentHold();
        }

        if (DateTime.TryParse(tBoxHoldDate.Text, out dtStart))
        {
            holdRecord.DateStart = dtStart;
        }
        else
        {
            holdRecord.DateStart = DateTime.Parse("1900-01-01");
        }

        holdRecord.CurLocation = tBoxHoldLocation.Text;
        holdRecord.Notes = tBoxHoldNotes.Text;

        if( addFlag )
        {
            talRecord.TalentHold.Add(holdRecord);
        }
        //else, the entity will just update.
    }

    private void AddOrUpdateCredits(CfsEntity cfsEntity, Talent talRecord)
    {
        if (!talRecord.TalentCredit.IsLoaded)
        {
            talRecord.TalentCredit.Load();
        }

        /* Remove all Associated Credit Records */
        List<TalentCredit> crList = talRecord.TalentCredit.ToList();

        foreach (TalentCredit crRecord in crList)
        {
            cfsEntity.DeleteObject(crRecord);
        }
        
        if (Session[TEMP_TABLE_CREDIT_ID] == null)
        {
            return; //nothing else to do            
        }

        /* Add all Credit References */
        DataTable cTable = (DataTable)Session[TEMP_TABLE_CREDIT_ID];
        TalentCredit talCredit;

        for (int i = 0; i < cTable.Rows.Count; i++ )
        {
            talCredit = new TalentCredit();

            talCredit.SortOrder = i;
            talCredit.Name = cTable.Rows[i][TEMP_TABLE_COLUMN_CAT_NAME].ToString();
            talCredit.Info = cTable.Rows[i][TEMP_TABLE_COLUMN_CAT_DETAILS].ToString();

            talRecord.TalentCredit.Add(talCredit);
        }
    }

    private void AddOrUpdateAltPhoneList(CfsEntity cfsEntity, Talent talRecord)
    {
        if (!talRecord.TalentAltPhone.IsLoaded)
        {
            talRecord.TalentAltPhone.Load();
        }

        /* Remove all Associated Alt Phone Records */
        List<TalentAltPhone> phList = talRecord.TalentAltPhone.ToList();

        foreach (TalentAltPhone phRecord in phList)
        {
            cfsEntity.DeleteObject(phRecord);
        }

        if (Session[TEMP_TABLE_ALT_PHONE_ID] == null)
        {
            return; //nothing else to do
        }

        /* Add all Alt Phone Records */
        DataTable dtTable = (DataTable)Session[TEMP_TABLE_ALT_PHONE_ID];
        TalentAltPhone altPhone;

        for(int i=0; i < dtTable.Rows.Count; i++ )
        {
            altPhone = new TalentAltPhone();

            string name = (string)dtTable.Rows[i][TEMP_TABLE_COLUMN_PHONE_NAME];
            string num = (string)dtTable.Rows[i][TEMP_TABLE_COLUMN_PHONE_NUM];
           // string num2 = (string)dtTable.Rows[i]["tBoxAltPhoneNum"];
        
            /* Only add records where one or both fields are not blank */
            if (!(name == "" && num == ""))
            {
                altPhone.SortOrder = i;
                altPhone.AltPhoneName = name;
                if (num == "")
                {
                    altPhone.AltPhoneNum = "test";
                }
                else
                {
                    altPhone.AltPhoneNum = num;
                }
               
                talRecord.TalentAltPhone.Add(altPhone);
            }
        }
    }

    private void AddOrUpdateWorksInList(CfsEntity cfsEntity, Talent talRecord)
    {
        if (!talRecord.TalentWorksIn.IsLoaded)
        {
            talRecord.TalentWorksIn.Load();
        }

        /* Remove all Associated Works In Records */
        List<TalentWorksIn> wList = talRecord.TalentWorksIn.ToList();

        foreach (TalentWorksIn wRecord in wList)
        {
            cfsEntity.DeleteObject(wRecord);
        }

        if (Session[TEMP_LIST_WORKS_IN_ID] == null)
        {
            /* This shouldn't happen, but if it does, we can't continue. */
            return;
        }

        /* Add all WorksIn State Records */
        List<string> stateList = (List<string>)Session[TEMP_LIST_WORKS_IN_ID];
        talRecord.WorksInList = ""; /* Clear out list for re-add */

        TalentWorksIn worksIn;

        foreach (string state in stateList)
        {
            /* Only add records where a state has been selected. */
            if (state != "")
            {
                worksIn = new TalentWorksIn();
                worksIn.State = state;
                worksIn.Country = "United States"; /* Default, unless they go Int'l */

                talRecord.TalentWorksIn.Add(worksIn);

                /* The comma delimited 'WorksInList' is for display on front end ONLY */
                if( talRecord.WorksInList == null || talRecord.WorksInList == "" )
                {
                    talRecord.WorksInList = state;
                }
                else
                {
                    talRecord.WorksInList += ", " + state;
                }
            }
        }    
    }
    #endregion
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HyperLink hypl = (HyperLink)e.Item.FindControl("bestNumberLink");
        TextBox mytext = (TextBox)e.Item.FindControl("tBoxAltPhoneNum");
        if (hypl.Text == "")
        {
         //   hypl.Enabled = false;

            hypl.NavigateUrl = "tel:" + mytext.Text;
        }
        else
        {
            hypl.NavigateUrl = "tel:" + mytext.Text;
        }
    }
    #region Update Editing Modes for Various Sections
    private void UpdatePageMode(string mode)
    {
        /* Buttons and Labels */
        switch (mode)
        {
            case CfsCommon.MODE_ADD:
            {
                headerAddEdit.InnerText = "Add Employee / Affiliate";
                btnAddEditTalent.Text = "ADD TALENT";
                break;
            }
            case CfsCommon.MODE_UPDATE:
            {
                headerAddEdit.InnerText = "Update Employee / Affiliate";
                btnAddEditTalent.Text = "UPDATE TALENT";
                cNumberLink.NavigateUrl = null;
                hNumberLink.NavigateUrl = null;
              
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                headerAddEdit.InnerText = "View Employee / Affiliate";
                btnAddEditTalent.Text = "EDIT TALENT";
                hNumberLink.NavigateUrl = "tel:" + tBoxHomePhone.Text;
                cNumberLink.NavigateUrl = "tel:" + tBoxCellPhone.Text;
                break;
            }
            default:
            {
                return; //bad mode
            }
        }
        
        /* Visible & Enable/Disable Controls */
        if (mode == CfsCommon.MODE_ADD || mode == CfsCommon.MODE_UPDATE)
        {
            //Enables all Textboxes, checkboxes, and dropdowns
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsEditable(c);
            }

            /* Enable 'Add New Alt Phone' Button */ 
            ImageButton iBtn;
            if (rptrAltPhone.Items.Count > 0 && (iBtn = (ImageButton)rptrAltPhone.Items[0].FindControl("btnAddAltPhone")) != null)
            {
                iBtn.Enabled = true;
            }

            /* Enable 'Add New Works In' Button */
            if (rptrWorksIn.Items.Count > 0 && (iBtn = (ImageButton)rptrWorksIn.Items[0].FindControl("btnAddWorksIn")) != null)
            {
                iBtn.Enabled = true;
            }
        }
        else //Mode is READONLY
        {
            //Disables all Textboxes, checkboxes, and dropdowns
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsNonEditable(c);
            }

            /* Disable 'Add New Alt Phone' Button */ 
            ImageButton iBtn;
            if (rptrAltPhone.Items.Count > 0 && (iBtn = (ImageButton)rptrAltPhone.Items[0].FindControl("btnAddAltPhone")) != null)
            {
              //  iBtn.Enabled = false;
            }

            /*Disable 'Add New Works In' Button */
            if (rptrWorksIn.Items.Count > 0 && (iBtn = (ImageButton)rptrWorksIn.Items[0].FindControl("btnAddWorksIn")) != null)
            {
                iBtn.Enabled = false;
            }
        }

        if (mode == CfsCommon.MODE_ADD)
        {
            ddlFullTalentList.Enabled = true;
            btnGetTalentInfo.Visible = true;
        }
        else //Mode UPDATE and READONLY dropDown nav is disabled
        {
            ddlFullTalentList.Enabled = false;
            btnGetTalentInfo.Visible = false;
        }
        
        /**** Image & Video Upload *****/
        if (mode == CfsCommon.MODE_ADD || mode == CfsCommon.MODE_READONLY)
        {
            btnThumbImgDel.Visible = false;
            btnNewTalImgDel.Visible = false;
            btnImgOneDel.Visible = false;
            btnImgTwoDel.Visible = false;
            btnImgThreeDel.Visible = false;
            btnVidOneDel.Visible = false;
            btnVidTwoDel.Visible = false;
            btnVidThreeDel.Visible = false;
        }
        //UPDATE MODE will be taken care of below

        if (mode == CfsCommon.MODE_READONLY)
        {
            linkNewTalentImg.Visible = true;
            linkThumbImg.Visible = true;
            linkImageOne.Visible = true;
            linkImageTwo.Visible = true;
            linkImageThree.Visible = true;
            linkVideoOne.Visible = true;
            linkVideoTwo.Visible = true;
            linkVideoThree.Visible = true;
            linkImagePhtoID.Visible = true;

            upThumbImg.Visible = false;
            upNewTalentImg.Visible = false;
            FileUploadID.Visible = false;
            upImageOne.Visible = false;
            upImageTwo.Visible = false;
            upImageThree.Visible = false;
            upVideoOne.Visible = false;
            upVideoTwo.Visible = false;
            upVideoThree.Visible = false;
        }
        else if (mode == CfsCommon.MODE_ADD)
        {
            linkThumbImg.Visible = false;
            linkNewTalentImg.Visible = false;
            linkImageOne.Visible = false;
            linkImageTwo.Visible = false;
            linkImageThree.Visible = false;
            linkVideoOne.Visible = false;
            linkVideoTwo.Visible = false;
            linkVideoThree.Visible = false;
            linkImagePhtoID.Visible = false;

            upThumbImg.Visible = true;
            upNewTalentImg.Visible = true;
            FileUploadID.Visible = true;
            upImageOne.Visible = true;
            upImageTwo.Visible = true;
            upImageThree.Visible = true;
            upVideoOne.Visible = true;
            upVideoTwo.Visible = true;
            upVideoThree.Visible = true;
        }
        else //UPDATE mode
        {
            CheckUpControl(linkNewTalentImg, upNewTalentImg, btnNewTalImgDel);
            CheckUpControl(linkImagePhtoID, FileUploadID, btnDeleteGovernmentID); 
            CheckUpControl(linkThumbImg, upThumbImg, btnThumbImgDel);
            CheckUpControl(linkImageOne, upImageOne, btnImgOneDel);
            CheckUpControl(linkImageTwo, upImageTwo, btnImgTwoDel);
            CheckUpControl(linkImageThree, upImageThree, btnImgThreeDel);
            
            CheckUpControl(linkVideoOne, upVideoOne, btnVidOneDel);
            CheckUpControl(linkVideoTwo, upVideoTwo, btnVidTwoDel);
            CheckUpControl(linkVideoThree, upVideoThree, btnVidThreeDel);
        }


        /* Persist */
        hiddenCurrentMode.Value = mode;

        DoControlExceptions();
    }

    private void CheckUpControl(HyperLink link, FileUpload fu, Button btn)
    {
        if (link.Text == "")
        {
            fu.Visible = true;
            link.Visible = false;
            btn.Visible = false;
        }
        else
        {
            fu.Visible = false;
            link.Visible = true;
            btn.Visible = true;
        }
    }

    private void DoControlExceptions()
    {
        tBoxContactTalent.Enabled = true;
        tBoxContactTalent.CssClass = "textfield";
    }

    private void UpdateFeatureContentMode(string mode)
    {
        if (mode == CfsCommon.MODE_READONLY)
        {
            btnAddUpdateCredit.Visible = false;
            tBoxCatName.Enabled = false;
            tBoxCatDetails.Enabled = false;
            chkBoxAdditionalTalent.Enabled = false;
            tBoxCatName.CssClass = "textfieldreadonly";
            tBoxCatDetails.CssClass = "textfieldreadonly";
        }
        else if (mode == CfsCommon.MODE_UPDATE)
        {
            chkBoxAdditionalTalent.Enabled = true;
         
            btnAddUpdateCredit.Visible = true;
            btnAddUpdateCredit.Text = "UPDATE CREDIT";
            tBoxCatName.Enabled = true;
            tBoxCatDetails.Enabled = true;
            tBoxCatName.CssClass = "textfield";
            tBoxCatDetails.CssClass = "textfield";
        }
        else if (mode == CfsCommon.MODE_ADD)
        {
            chkBoxAdditionalTalent.Enabled = true;
         
            btnAddUpdateCredit.Visible = true;
            btnAddUpdateCredit.Text = "ADD CREDIT";
            tBoxCatName.Enabled = true;
            tBoxCatDetails.Enabled = true;
            tBoxCatName.CssClass = "textfield";
            tBoxCatDetails.CssClass = "textfield";
        }
        else
        {
            return; //Bad Mode
        }

        /* Persist */
        hiddenCreditMode.Value = mode;
    }
    #endregion

    #region Temp Alt Phone Table
    private DataTable CreateTempAltPhoneTable(bool createBlankRow)
    {
        DataTable table = new DataTable();

        table.Columns.Add(TEMP_TABLE_COLUMN_PHONE_PK);
        table.Columns.Add(TEMP_TABLE_COLUMN_PHONE_NAME);
        table.Columns.Add(TEMP_TABLE_COLUMN_PHONE_NUM);

        if (createBlankRow)
        {
            string[] data = new string[3];
            data[0] = data[1] = data[2] = "";

            table.Rows.Add(data);
        }

        return table;
    }

    private void UpdateRowToAltPhoneTable(string id, string name, string number)
    {
        if (Session[TEMP_TABLE_ALT_PHONE_ID] == null)
        {
            return;
        }

        DataTable phoneTable = (DataTable)Session[TEMP_TABLE_ALT_PHONE_ID];

        string[] data = new string[3];
        data[0] = id;
        data[1] = name;
        data[2] = number;

        phoneTable.Rows.Add(data);

        Session[TEMP_TABLE_ALT_PHONE_ID] = phoneTable;
    }

    private void AddRowToAltPhoneTable(string id, string name, string number)
    {
        if (Session[TEMP_TABLE_ALT_PHONE_ID] == null)
        {
            return; 
        }

        DataTable phoneTable = (DataTable)Session[TEMP_TABLE_ALT_PHONE_ID];

        string[] data = new string[3];
        data[0] = id;
        data[1] = name;
        data[2] = number;

        phoneTable.Rows.Add(data);
        
        Session[TEMP_TABLE_ALT_PHONE_ID] = phoneTable;
    }

    private void LoadTempAltPhoneTable()
    {
        DataTable theTable;

        if (Session[TEMP_TABLE_ALT_PHONE_ID] == null)
        {
            return; /* Should never happen */
        }

        theTable = (DataTable)Session[TEMP_TABLE_ALT_PHONE_ID];

        rptrAltPhone.DataSource = theTable;
        rptrAltPhone.DataBind(); 
    }

    private void SaveCurAltPhoneViewToTempTable()
    {
        DataTable dtTable = CreateTempAltPhoneTable(false);
        
        string[] data = new string[3];
        
        foreach(RepeaterItem item in rptrAltPhone.Items)
        {
            data[0] = ((HiddenField)item.Controls[1]).Value;
            data[1] = ((TextBox)item.Controls[5]).Text;
            //data[2] = ((TextBox)item.Controls[7]).Text;
            TextBox find = (TextBox)item.FindControl("tBoxAltPhoneNum");
            data[2] = find.Text;
           // data[2] = ((HyperLink)item.Controls[7]).Text;
            dtTable.Rows.Add(data);
        }

        /* Persist */
        Session[TEMP_TABLE_ALT_PHONE_ID] = dtTable;
    }

    #endregion

    #region Temp Feature Talent/Credit Table
    private DataTable CreateTempCreditTable()
    {
        DataTable talTable = new DataTable();

        talTable.Columns.Add(TEMP_TABLE_COLUMN_CAT_ID);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_CAT_NAME);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_CAT_DETAILS);

        return talTable;
    }

    private void AddCreditToTempTable()
    {
        DataTable table;

        //Store Talent data temporarily, until the job is saved
        if (Session[TEMP_TABLE_CREDIT_ID] == null)
        {
            table = CreateTempCreditTable();
        }
        else
        {
            table = (DataTable)Session[TEMP_TABLE_CREDIT_ID];
        }

        string[] newData = new string[3];

        newData[0] = table.Rows.Count.ToString();
        newData[1] = tBoxCatName.Text;
        newData[2] = tBoxCatDetails.Text;

        table.LoadDataRow(newData, true);

        Session[TEMP_TABLE_CREDIT_ID] = table; //Save the new data back to the Session    
    }

    private void UpdateCreditInTempTable(string creditKey)
    {
        if (Session[TEMP_TABLE_CREDIT_ID] == null)
        {
            return;
        }

        DataTable dtTable = CreateTempCreditTable();
        DataTable tempTable = (DataTable)Session[TEMP_TABLE_CREDIT_ID];
        string[] newData = new string[3];

        for (int i = 0; i < tempTable.Rows.Count; i++)
        {
            string curKey = (string)tempTable.Rows[i][TEMP_TABLE_COLUMN_CAT_ID];
            
            if ( curKey == creditKey)
            {
                newData[0] = curKey;
                newData[1] = tBoxCatName.Text;
                newData[2] = tBoxCatDetails.Text;

                dtTable.LoadDataRow(newData, true);
            }
            else
            {
                dtTable.ImportRow(tempTable.Rows[i]);
            }
        }

        Session[TEMP_TABLE_CREDIT_ID] = dtTable;
    }

    private void DeleteRowFromTempTable(int rowNum)
    {
        DataTable table = (DataTable)Session[TEMP_TABLE_CREDIT_ID];

        if (table == null)
        {
            return;
        }

        table.Rows.RemoveAt(rowNum);
        Session[TEMP_TABLE_CREDIT_ID] = table; /* Persist */
    }

    private void LoadTempTalentCreditTable()
    {
        DataTable table;
        bool buttonsVisible;

        if (Session[TEMP_TABLE_CREDIT_ID] != null)
        {
            table = (DataTable)Session[TEMP_TABLE_CREDIT_ID];
            rptrCredits.DataSource = table;
            rptrCredits.DataBind();
        }

        if (hiddenCreditMode.Value == CfsCommon.MODE_READONLY)
        {
            buttonsVisible = false;
        }
        else
        {
            buttonsVisible = true;
        }

        foreach (Control c in rptrCredits.Controls)
        {
            CfsCommon.ChangeButtonsVisible(c, buttonsVisible );                
        }
    }

    private void MoveCreditUp(int rowNum)
    {
        if (rowNum <= 0 || Session[TEMP_TABLE_CREDIT_ID] == null)
        {
            return; 
        }

        DataTable table = (DataTable)Session[TEMP_TABLE_CREDIT_ID];

        string[] tempRow = new string[3];

        //Move Current Row Data to Temp Storage
        for (int i = 0; i < 3; i++)
        {
            tempRow[i] = (string)table.Rows[rowNum][i];
        }

        //Move Previous Row into Current Row
        for (int i = 0; i < 3; i++)
        {
            table.Rows[rowNum][i] = table.Rows[rowNum - 1][i];
        }

        //Move Temp Storage into Previous Row
        for (int i = 0; i < 3; i++)
        {
            table.Rows[rowNum - 1][i] = tempRow[i];
        }

        Session[TEMP_TABLE_CREDIT_ID] = table; //Save back to Session
    }
    #endregion

    #region Temp Works In List
    private List<string> CreateTempWorksInList(bool addBlank)
    {
        List<string> list = new List<string>();

        if (addBlank)
        {
            list.Add("");
        }

        return list;
    }

    private void LoadTempWorksInList()
    {
        List<string> list;

        if (Session[TEMP_LIST_WORKS_IN_ID] == null)
        {
            return; /* Should never happen */
        }

        list = (List<string>)Session[TEMP_LIST_WORKS_IN_ID];

        rptrWorksIn.DataSource = list;
        rptrWorksIn.DataBind();     
    }

    private void AddRowToWorksInList(string state)
    {
        if (Session[TEMP_LIST_WORKS_IN_ID] == null)
        {
            return; /* Should never happen */
        }

        List<string> list = (List<string>)Session[TEMP_LIST_WORKS_IN_ID];

        list.Add(state);

        Session[TEMP_LIST_WORKS_IN_ID] = list;
    }

    private void SaveCurWorksInView()
    {
        DropDownList ddl;
        List<string> list = CreateTempWorksInList(false);

        foreach (RepeaterItem item in rptrWorksIn.Items)
        {
            ddl = (DropDownList)item.FindControl("ddlStateWorksIn");

            list.Add(ddl.SelectedValue);
        }

        /* Persist */
        Session[TEMP_LIST_WORKS_IN_ID] = list;
    }

    #endregion

    #region Functions to do Media Processing
    private string SaveImage(FileUpload p_fu)
    {
        string timeStamp = DateTime.Now.ToString();

        timeStamp = timeStamp.Replace("/", "").Replace(":", "").Replace(" ", "");
        timeStamp += p_fu.FileName;
        string path = Server.MapPath("../talentimages/applicant/" + timeStamp);

        p_fu.SaveAs(path);


        if (chkFeatureTalent.Checked)
        {
           string  destPath = "C:/Net/c_saas/talentimages/applicant/";
            if (File.Exists(destPath + timeStamp))
            {
                timeStamp= CfsCommon.CreateNewFilename(destPath, timeStamp);
            }

            try
            {
                p_fu.SaveAs(destPath + timeStamp);
            }
            catch (HttpException)
            {
                /* Don't expect this to ever happen */
                return "ERROR";
            }
        }

        return timeStamp;
    }

    private void DoImagesProcessing(string talentId, Talent talRecord)
    {
        //if (!string.IsNullOrEmpty(talentId) && hiddenCurTalType.Value != "")
        //{
        //    if (hiddenCurTalType.Value != ddlTalentType.SelectedValue)
        //    {
        //        /* Field 'Talent Type' has been changed on an existing record. We need
        //         * to move the images to the right directory (Directories based on talent type */
        //        talRecord.NewTalentImg = linkNewTalentImg.Text = DoMediaMove(hiddenCurTalType.Value, linkNewTalentImg.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
        //        talRecord.ThumbImg = linkThumbImg.Text = DoMediaMove(hiddenCurTalType.Value, linkThumbImg.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
        //        talRecord.ImageOne = linkImageOne.Text = DoMediaMove(hiddenCurTalType.Value, linkImageOne.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
        //        talRecord.ImageTwo = linkImageTwo.Text = DoMediaMove(hiddenCurTalType.Value, linkImageTwo.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
        //        talRecord.ImageThree = linkImageThree.Text = DoMediaMove(hiddenCurTalType.Value, linkImageThree.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
        //        talRecord.ImageID1 = SaveImage(FileUploadID);
        //    }
        //}
        linkImageOne.Text =  talRecord.ImageOne;
        linkImageTwo.Text = talRecord.ImageTwo ;
        linkImageThree.Text = talRecord.ImageThree;
        linkThumbImg.Text= talRecord.ThumbImg ;
        linkNewTalentImg.Text=talRecord.NewTalentImg ;
      
        if (upNewTalentImg.HasFile)
        {
            Guid newtalentimgGUID = ImageManager.GetImageGuid(7, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkNewTalentImg, upNewTalentImg, CfsCommon.MEDIA_TYPE_IMAGE));
            talRecord.NewTalentImg = linkNewTalentImg.Text = newtalentimgGUID.ToString();
            ImageManager.EditImage(newtalentimgGUID);
            linkNewTalentImg.Text = newtalentimgGUID.ToString();
        }
        if (upThumbImg.HasFile)
        {
            Guid newthumbGUID = ImageManager.GetImageGuid(4, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkThumbImg, upThumbImg, CfsCommon.MEDIA_TYPE_IMAGE));
            talRecord.ThumbImg = linkThumbImg.Text = newthumbGUID.ToString();
            ImageManager.EditImage(newthumbGUID);
            linkThumbImg.Text = newthumbGUID.ToString();
        }
        if (upImageOne.HasFile)
        {
            Guid imageGuid1 = ImageManager.GetImageGuid(1, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageOne, upImageOne, CfsCommon.MEDIA_TYPE_IMAGE));
            talRecord.ImageOne = linkImageOne.Text = imageGuid1.ToString();
            ImageManager.EditImage(imageGuid1);
            linkImageOne.Text = imageGuid1.ToString();
        }

        if (upImageTwo.HasFile)
        {
            Guid imageGuid2 = ImageManager.GetImageGuid(2, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageTwo, upImageTwo, CfsCommon.MEDIA_TYPE_IMAGE));
            talRecord.ImageTwo = linkImageTwo.Text = imageGuid2.ToString();
            ImageManager.EditImage(imageGuid2);
            linkImageTwo.Text = imageGuid2.ToString();
        }
        if (upImageThree.HasFile)
        {
            Guid imageGuid3 = ImageManager.GetImageGuid(3, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageThree, upImageThree, CfsCommon.MEDIA_TYPE_IMAGE));
            talRecord.ImageThree = linkImageThree.Text = imageGuid3.ToString(); ;
            ImageManager.EditImage(imageGuid3);
            linkImageThree.Text = imageGuid3.ToString();
        }


     //   ImageManager.SetImageIndex(imageGuid1, 1);
       
        /* For Adds AND Updates, allow the user to Upload new images. */
      //  talRecord.NewTalentImg = linkNewTalentImg.Text = newtalentimgGUID.ToString();
       // talRecord.ThumbImg = linkThumbImg.Text = newthumbGUID.ToString();
       // talRecord.ImageOne = linkImageOne.Text = imageGuid1.ToString();
       // talRecord.ImageTwo = linkImageTwo.Text = imageGuid2.ToString();

       // talRecord.ImageThree = linkImageThree.Text = imageGuid3.ToString(); ;
       // talRecord.ImageID1 = linkImagePhtoID.Text = DoMediaUpload(linkImagePhtoID, FileUploadID, CfsCommon.MEDIA_TYPE_IMAGE);
        talRecord.ImageID1 = SaveImage(FileUploadID);


        //code to hopefully save to sticky


        Talent talentRecord = talRecord;

        if (linkImageOne.Text !="")
        {
        //    Guid imageEditGuid1 = ImageManager.GetImageGuid(1, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageOne, upImageOne, CfsCommon.MEDIA_TYPE_IMAGE));
       
        //    ImageManager.EditImage(imageEditGuid1);

         //   CfsEntity cfsEntity = new CfsEntity();
          //  Talent t = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);

          //  cfsEntity.SaveChanges();
        }
        if (linkImageTwo.Text != "")
        {
            //Guid imageEditGuid2 = ImageManager.GetImageGuid(2, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageTwo, upImageTwo, CfsCommon.MEDIA_TYPE_IMAGE));

            //ImageManager.EditImage(imageEditGuid2);

            //CfsEntity cfsEntity = new CfsEntity();
            ////Talent t = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);
            //cfsEntity.SaveChanges();
        }
        if (linkImageThree.Text != "")
        {
           // Guid imageEditGuid3 = ImageManager.GetImageGuid(3, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageThree, upImageThree, CfsCommon.MEDIA_TYPE_IMAGE));

           // ImageManager.EditImage(imageEditGuid3);

           // CfsEntity cfsEntity = new CfsEntity();
           //// Talent t = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);
           // cfsEntity.SaveChanges();
        }

        if (linkThumbImg.Text != "")
        {
            //Guid newthumbEditGUID = ImageManager.GetImageGuid(4, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkThumbImg, upThumbImg, CfsCommon.MEDIA_TYPE_IMAGE));
            //ImageManager.EditImage(newthumbEditGUID);

            //CfsEntity cfsEntity = new CfsEntity();
            ////Talent t = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);
            //cfsEntity.SaveChanges();
        }
        if (linkNewTalentImg.Text != "")
        {
            //Guid newtalentimgEditGUID = ImageManager.GetImageGuid(7, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkNewTalentImg, upNewTalentImg, CfsCommon.MEDIA_TYPE_IMAGE));
            //ImageManager.EditImage(newtalentimgEditGUID);

            //CfsEntity cfsEntity = new CfsEntity();
            ////Talent t = CfsCommon.GetTalentRecord(cfsEntity, Request.QueryString["id"]);
            //cfsEntity.SaveChanges();
        
        }
        talRecord.ImageOne = linkImageOne.Text;
        talRecord.ImageTwo = linkImageTwo.Text;
        talRecord.ImageThree = linkImageThree.Text;
        talRecord.ThumbImg = linkThumbImg.Text;
        talRecord.NewTalentImg = linkNewTalentImg.Text;
        // Update editable images entities
        //if (talRecord.ImageOne == "")
        //{
        //    ImageManager.DeleteImage(1, talRecord.TalentId);
        //}
        //if (talRecord.ImageTwo == "")
        //{
        //    ImageManager.DeleteImage(2, talRecord.TalentId);
        //}
        //if (talRecord.ImageThree == "")
        //{
        //    ImageManager.DeleteImage(3, talRecord.TalentId);
        //}
        //if (talRecord.ImageID1== "")
        //{
        //    ImageManager.DeleteImage(4, talRecord.TalentId);
        //}

        

    }

    private void saveEditImageManipulation(string imagepath, Talent talentRecord, int imageIndex)
    {
        // get image ID
        return;
        Guid imageID = new Guid(imagepath);
        // Set image index
        ImageManager.SetImageIndex(imageID, imageIndex);
       // Bitmap bitmap = (Bitmap)ImageManager.GetImage(imageID);

            
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
/*
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
        }*/

        // edit image
        byte[] thisisimage = ImageManager.GetImageBytes(imageID, false);

        ImageManager.SaveImagebyte(imageID, thisisimage);
        //ImageManager.EditImage(imageID, editableImage.ResizeWidth, editableImage.ResizeHeight, editableImage.CropX, editableImage.CropY, editableImage.CropWidth, editableImage.CropHeight, overlayImages.ToArray());
    }
    private void DoImagesProcessingSaas(string talentId, Talent talRecord)
    {
        if (!string.IsNullOrEmpty(talentId) && hiddenCurTalType.Value != "")
        {
            if (hiddenCurTalType.Value != ddlTalentType.SelectedValue)
            {
                /* Field 'Talent Type' has been changed on an existing record. We need
                 * to move the images to the right directory (Directories based on talent type */
                string hiddenCurTal = "";
                string[] separator = hiddenCurTalType.Value.Split('*');
                hiddenCurTal = separator[0];
                talRecord.NewTalentImg = linkNewTalentImg.Text = DoMediaMoveNewSaas(hiddenCurTal, linkNewTalentImg.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
                talRecord.ThumbImg = linkThumbImg.Text = DoMediaMoveNewSaas(hiddenCurTal, linkThumbImg.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
                talRecord.ImageOne = linkImageOne.Text = DoMediaMoveNewSaas(hiddenCurTal, linkImageOne.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
                talRecord.ImageTwo = linkImageTwo.Text = DoMediaMoveNewSaas(hiddenCurTal, linkImageTwo.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
                talRecord.ImageThree = linkImageThree.Text = DoMediaMoveNewSaas(hiddenCurTal, linkImageThree.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_IMAGE);
                talRecord.ImageID1 = SaveImage(FileUploadID);
            }
        }

        Guid newtalentimgGUID = ImageManager.GetImageGuid(7, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkNewTalentImg, upNewTalentImg, CfsCommon.MEDIA_TYPE_IMAGE));

        Guid newthumbGUID = ImageManager.GetImageGuid(4, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkThumbImg, upThumbImg, CfsCommon.MEDIA_TYPE_IMAGE));


        Guid imageGuid1 = ImageManager.GetImageGuid(1, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageOne, upImageOne, CfsCommon.MEDIA_TYPE_IMAGE));
        Guid imageGuid2 = ImageManager.GetImageGuid(2, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageTwo, upImageTwo, CfsCommon.MEDIA_TYPE_IMAGE));
        Guid imageGuid3 = ImageManager.GetImageGuid(3, talRecord.TalentId, talRecord.TalentType, DoMediaUpload(linkImageThree, upImageThree, CfsCommon.MEDIA_TYPE_IMAGE));
        ImageManager.SetImageIndex(imageGuid1, 1);
        /* For Adds AND Updates, allow the user to Upload new images. */
        talRecord.NewTalentImg = linkNewTalentImg.Text = newtalentimgGUID.ToString();
        talRecord.ThumbImg = linkThumbImg.Text = newthumbGUID.ToString();
        talRecord.ImageOne = linkImageOne.Text = imageGuid1.ToString();
        talRecord.ImageTwo = linkImageTwo.Text = imageGuid2.ToString();

       // talRecord.ImageThree = linkImageThree.Text = imageGuid3.ToString(); ;
        talRecord.ImageID1 = SaveImage(FileUploadID);

        // Update editable images entities
        if (talRecord.ImageOne == "")
        {
            ImageManager.DeleteImage(1, talRecord.TalentId);
        }
        if (talRecord.ImageTwo == "")
        {
            ImageManager.DeleteImage(2, talRecord.TalentId);
        }
        if (talRecord.ImageThree == "")
        {
            ImageManager.DeleteImage(3, talRecord.TalentId);
        }
        if (talRecord.ImageID1 == "")
        {
            ImageManager.DeleteImage(4, talRecord.TalentId);
        }



    }
   
    private void DoVideoProcessing(string talentId, Talent talRecord)
    {
        if (!string.IsNullOrEmpty(talentId) && hiddenCurTalType.Value != "")
        {
            if (hiddenCurTalType.Value != ddlTalentType.SelectedValue)
            {
                /* Field 'Talent Type' has been changed on an existing record. We need
                 * to move the videos to the right directory (Directories based on talent type ) */
                talRecord.VideoOne = linkVideoOne.Text = DoMediaMove(hiddenCurTalType.Value, linkVideoOne.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_VIDEO);
                talRecord.VideoTwo = linkVideoTwo.Text = DoMediaMove(hiddenCurTalType.Value, linkVideoTwo.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_VIDEO);
                talRecord.VideoThree = linkVideoThree.Text = DoMediaMove(hiddenCurTalType.Value, linkVideoThree.Text, ddlTalentType.SelectedValue, CfsCommon.MEDIA_TYPE_VIDEO);
            }
        }

        /* For Adds AND Updates, allow the user to Upload new videos. */
        talRecord.VideoOne = linkVideoOne.Text = DoMediaUpload(linkVideoOne, upVideoOne, CfsCommon.MEDIA_TYPE_VIDEO);
        talRecord.VideoTwo = linkVideoTwo.Text = DoMediaUpload(linkVideoTwo, upVideoTwo, CfsCommon.MEDIA_TYPE_VIDEO);
        talRecord.VideoThree = linkVideoThree.Text = DoMediaUpload(linkVideoThree, upVideoThree, CfsCommon.MEDIA_TYPE_VIDEO);
    }
    private string DoMediaMoveNewSaas(string talTypeOld, string filePathOld, string talTypeNew, string mediaType)
    {

        if (string.IsNullOrEmpty(filePathOld))
        {
            return "";
        }
        //string fileName = System.IO.Path.GetFileName(file.FileName);
        //string fullPath = System.IO.Path.Combine(Server.MapPath("~/images/profile"), fileName);
        string oldFileName = filePathOld.Replace(CfsCommon.GetTalentMediaPath(talTypeOld, mediaType, false), "");
        string oldPath = Server.MapPath("C:/Net/c_saas/" + CfsCommon.GetTalentMediaPath(talTypeOld, mediaType, true));
        string newFileName = oldFileName;
        string newPath = Server.MapPath("C:/Net/c_saas/" + CfsCommon.GetTalentMediaPath(talTypeNew, mediaType, true));

        if (!File.Exists(oldPath + oldFileName))
        {
            /* If the Src file does not exist for some reason, then there is nothing we can move */
            return "";
        }

        /* If a file already exists w/ that name (in the new location), create a new name (append numbers) */
        if (File.Exists(newPath + newFileName))
        {
            newFileName = CfsCommon.CreateNewFilename(newPath, newFileName);
        }

        try
        {
            File.Move(oldPath + oldFileName, newPath + newFileName);
            return CfsCommon.GetTalentMediaPath(talTypeNew, mediaType, false) + newFileName; //Return relative path for DB
        }
        catch (Exception)
        {
            //TO DO - Might want something specific, based on exception */
            return "ERROR";
        }
    }
    
    private string DoMediaMove(string talTypeOld, string filePathOld, string talTypeNew, string mediaType)
    {
        if (string.IsNullOrEmpty(filePathOld))
        {
            return "";
        }
        string[] separator = talTypeNew.Split('*');
        talTypeNew = separator[0];

        string[] separator1 = talTypeOld.Split('*');
        talTypeOld = separator1[0];

                
        string oldFileName = filePathOld.Replace(CfsCommon.GetTalentMediaPath(talTypeOld, mediaType, false), "");
        string oldPath = Server.MapPath("~/" + CfsCommon.GetTalentMediaPath(talTypeOld, mediaType, true));
        string newFileName = oldFileName;
        string newPath = Server.MapPath("~/" + CfsCommon.GetTalentMediaPath(talTypeNew, mediaType, true));

        if (!File.Exists(oldPath + oldFileName))
        {
            /* If the Src file does not exist for some reason, then there is nothing we can move */
            return "";
        }

        /* If a file already exists w/ that name (in the new location), create a new name (append numbers) */
        if (File.Exists(newPath + newFileName))
        {
            newFileName = CfsCommon.CreateNewFilename(newPath, newFileName);
        }

        try
        {
            File.Move(oldPath + oldFileName, newPath + newFileName);

            return CfsCommon.GetTalentMediaPath(talTypeNew, mediaType, false) + newFileName; //Return relative path for DB
        }
        catch (Exception)
        {
            //TO DO - Might want something specific, based on exception */
            return "ERROR";
        }
    }

    private string DoMediaUpload(HyperLink link, FileUpload fu, string mediaType)
    {
        if (mediaType != CfsCommon.MEDIA_TYPE_VIDEO && mediaType != CfsCommon.MEDIA_TYPE_IMAGE)
        {
            /* Error */
            return "";
        }

        if (!fu.HasFile)
        {
            /* User has NOT selected a file (or a file already exists in its place), return the contents of the link,
             * (which should be visible, the current file). If the link is blank (due to a delete action by the user), this has the effect of
             * clearing the file from the database */
            return link.Text;
        }
        //Else: User has selected a file
        /* User has selected a file, attempt the Upload, and return the new DB file path */

        /* Files are saved as relative paths in the DB in the following format:
         * [talentType]/filename.ext    Ex: female/candy1.jpg
         * 
         */

        string fileName = fu.FileName;
        string destPath = "";

    

        destPath = Server.MapPath("~/" + CfsCommon.GetTalentMediaPath(ddlTalentType.SelectedValue, mediaType, true));
       
        /* If Destination file exists, come up w/ a new name (append numbers) */
        if (File.Exists(destPath + fileName))
        {
            fileName = CfsCommon.CreateNewFilename(destPath, fileName);
        }

        try
        {
            fu.SaveAs(destPath + fileName);
        }
        catch (HttpException)
        {
            /* Don't expect this to ever happen */
            return "ERROR";
        }
        if (chkFeatureTalent.Checked)
        {
            if (mediaType == CfsCommon.MEDIA_TYPE_IMAGE)
            {
                destPath = "C:/Net/c_saas/talentimages/" + ddlTalentType.SelectedValue + "/";
            }
            else
            {
                //video
                destPath = "C:/Net/c_saas/talentvids/" + ddlTalentType.SelectedValue + "/";

            }
            // if (File.Exists(destPath + fileName))
            //{
            //  fileName = CfsCommon.CreateNewFilename(destPath, fileName);
            //}

            try
            {

                fu.SaveAs(destPath + fileName);


            }
            catch (HttpException)
            {
                /* Don't expect this to ever happen */
                // return "ERROR";
            }
        }
        destPath = Server.MapPath("~/" + CfsCommon.GetTalentMediaPath(ddlTalentType.SelectedValue, mediaType, true));
       
       // return ddlTalentType.SelectedValue+"/"+fileName;
        return CfsCommon.GetTalentMediaPath(ddlTalentType.SelectedValue, mediaType, false) + fileName;
    }

    private void DoMediaDelete(HyperLink link, FileUpload fu, Button btn)
    {
        link.Text = "Deleted";
        link.Visible = false;
        fu.Visible = true;
        btn.Visible = false;
    }
    private void DoMediaDelete(HyperLink link, FileUpload fu, Button btn, int val)
    {
    CfsEntity cfEntity = new CfsEntity();
    string talentId = Request.QueryString["empid"].ToString();
          
    Talent talRecord = CfsCommon.GetTalentRecord(cfEntity, talentId);

        link.Text = "Deleted";
        link.Visible = false;
        fu.Visible = true;
        btn.Visible = false;
       
            ImageManager.DeleteImage(val, talRecord.TalentId);
        switch(val)
        {
            case 1:
                talRecord.ImageOne="";
                break;
            case 2:
                talRecord.ImageTwo="";
                break;
            case 3:
                talRecord.ImageThree = "";
                break;
            case 4:
                talRecord.ThumbImg = "";
                break;
            case 7:
                talRecord.NewTalentImg = "";
                break;
            default:
                break;
        }
        cfEntity.SaveChanges();
        
    }
    #endregion

    #region Data Validation Functions
    private bool FormIsValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";

        if (ddlTalentType.SelectedValue == "")
        {
            isValid = false;
            ulErrorMsg.InnerHtml += "<li>You must choose a 'Talent Type' from the list.</li>";
            ddlTalentType.CssClass = "select error";
        }
        else
        {
            ddlTalentType.CssClass = "select";
        }

        if (tBoxEmail.Text != "" && !CfsCommon.ValidateTextBoxEmail(tBoxEmail, "Email", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxExtraEmail.Text != "" && !CfsCommon.ValidateTextBoxEmail(tBoxExtraEmail, "Additional Email", ulErrorMsg))
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
        if(tBoxDateOfBirth.Text != "" && !CfsCommon.ValidateTextBoxDate(tBoxDateOfBirth, "DOB", ulErrorMsg))
        {
            isValid = false;
        }

        divErrorMsg.Visible = !isValid;
        return isValid;
    }
    #endregion

    private bool SendTxtMessage(string phoneNumTo, string msg, out string response )
    {
        response = "";
        
        WebClient client = new WebClient ();

        phoneNumTo = ConvertNumForSms(phoneNumTo);
        phoneNumTo = "1" + phoneNumTo;

        // Add a user agent header in case the requested URI contains a query.
        client.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        client.QueryString.Add("user", SMS_MSG_USER);
        client.QueryString.Add("password", SMS_MSG_PW);
        client.QueryString.Add("api_id", SMS_MSG_API_ID);
        client.QueryString.Add("to", phoneNumTo);
        client.QueryString.Add("text", msg);
        
        string baseurl ="http://api.clickatell.com/http/sendmsg";

        try
        {
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            response = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception)
        {
            response = "FATAL ERROR. Please try again. Contact The Atom Group if problem persists. " + response;
            return false;
        }

        try
        {
            CfsEntity cfsEntity = new CfsEntity();
            TextMsgLog newMsg = new TextMsgLog();
            newMsg.PhoneNumTo = phoneNumTo;
            newMsg.Message = msg;
            newMsg.Status = response;

            cfsEntity.AddToTextMsgLog(newMsg);
            cfsEntity.SaveChanges();
        }
        catch (Exception)
        {
        
        }

        //Good response: "ID: a343b5a7e61807bf6cb4b6b39ad38b76"
        //Err response: "ERR: 001, Authentication failed"
        if( response.StartsWith("ID:" ) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string ConvertNumForSms(string phoneNum)
    {
        if (phoneNum == "")
        {
            return "";
        }

        string newNum = "";

        for (int i = 0; i < phoneNum.Length; i++)
        {
            if (phoneNum[i] >= '0' && phoneNum[i] <= '9')
            {
                newNum += phoneNum[i];
            }
        }

        return newNum;
    }

    private bool IsSmsDataValid()
    {
        bool isValid = true;
        divTalMsgFeedback.InnerHtml = "";

        string phoneTo = ConvertNumForSms(tBoxCellPhone.Text);

        if (phoneTo.Length != 10)
        {
            isValid = false;
            divTalMsgFeedback.InnerHtml += "<p>'Cell Phone' field is not a valid phone number. It must be a 10 digit phone number. Ex: 603-555-6789</p>";
        }
        if (tBoxContactTalent.Text == "")
        {
            isValid = false;
            divTalMsgFeedback.InnerHtml += "<p>'Contact Talent' is empty. You must fill in a message to send.</p>";
        }
        if (tBoxContactTalent.Text.Length > 140)
        {
            isValid = false;
            divTalMsgFeedback.InnerHtml += "<p>'Contact Talent' has a 140 character limit. Please shorten your message.</p>";
        }

        return isValid;
    }


    protected void upImg_Click(object sender, EventArgs e)
    {
        AddOrUpdateTalent(ddlFullTalentList.SelectedValue);
    }
}