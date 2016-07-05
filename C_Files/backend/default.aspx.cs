using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using CfsNamespace;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO.IsolatedStorage;

public partial class _default : System.Web.UI.Page
{
    private CfsEntity CfsEntity;

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        HtmlMeta metakey = new HtmlMeta();
        metakey.HttpEquiv = "x-ua-compatible";
        metakey.Content = "IE=8";

        Header.Controls.Add(metakey);
        grdViewWorkOrders.RowDataBound += new GridViewRowEventHandler(grdViewWorkOrders_RowDataBound);
        btnStaffNotes.Click += new EventHandler(btnStaffNotes_Click);
        HttpResponse.RemoveOutputCacheItem("/backend/default.aspx");
        if (!IsPostBack)
        {
            HttpResponse.RemoveOutputCacheItem("/backend/default.aspx");
            CfsCommon.GetStateListAbbr(ddlSearchLivesIn);
            CfsCommon.GetStateListAbbr(ddlSearchWorksIn);
            CfsCommon.GetTalentTypeList(ddlSearchTalType, true);
            CfsCommon.GetFullTalentList(ddlSearchFullPerformerList);
            CfsCommon.GetStateList(ddlSearchState);

            GetQuickRefTalentLists();
            LoadWorkOrderQuickViewOldSchool();
            GetPromoSpecialInfo();

            LoadStaffNotes();
        }
        //loop through the rss items in the dataset and populate the list of rss feed items
        CheckBoxList checklist = new CheckBoxList();
        checklist.ID = "checkboxlist1";
        //checklist.AutoPostBack = false;
        checklist.CellPadding = 5;
        checklist.CellSpacing = 5;
        checklist.RepeatColumns = 1;
        checklist.RepeatDirection = RepeatDirection.Vertical;
        checklist.RepeatLayout = RepeatLayout.Flow;
        checklist.TextAlign = TextAlign.Right;



        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~//media//") + "playlist.xml");



        XmlNode root = doc.DocumentElement;
        XDocument xDoc = new XDocument();
        xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());
        foreach (var coordinate in xDoc.Descendants("item"))
        {
            XmlDocument hhh = new XmlDocument();
            hhh.LoadXml(coordinate.ToString());
            foreach (XmlNode node in hhh)
            {
                foreach (XmlNode node2 in node)
                {
                    if (node2.Name == "content")
                    {
                        string strPath = node2.Attributes[0].Value;
                        string[] strFileParts = strPath.Split('/');

                        checklist.Items.Add(new ListItem(strFileParts[strFileParts.Length - 1]));

                    }
                }
            }

        }



        Place.Controls.Add(checklist);
    }

    protected void OnClick_EmpQuickRefGoBtn(object sender, EventArgs e)
    {
        string buttonId = ((Button)sender).ID;
        string empId = "";

        /* Get the Employee ID, based on which 'GO' button was clicked */
        switch (buttonId)
        {
            case "btnFemaleDancerGo": empId = ddlFemaleDancer.SelectedValue; break;
            case "btnMaleDancerGo": empId = ddlMaleDancer.SelectedValue; break;
            case "btnFemaleMiniGo": empId = ddlFemaleMini.SelectedValue; break;
            case "btnMaleMiniGo": empId = ddlMaleMini.SelectedValue; break;
            case "bntBellyDanceGo": empId = ddlBellyDancer.SelectedValue; break;
            case "btnBbwGo": empId = ddlBbw.SelectedValue; break;
            case "btnDragQueenGo": empId = ddlDragQueen.SelectedValue; break;
            case "btnImpersonatorGo": empId = ddlImpersonator.SelectedValue; break;
            case "btnNoveltyActsGo": empId = ddlNoveltyActs.SelectedValue; break;
            case "btnDuoShowsGo": empId = ddlDuoShows.SelectedValue; break;
            case "btnDriversBouncersGo": empId = ddlDriversBouncers.SelectedValue; break;
            case "btnAffiliateGo": empId = ddlAffiliate.SelectedValue; break;
            case "btnBirthdayGo": empId = ddlBirthdays.SelectedValue; break;
            default:
            {
                return; //No other Controls should call this function
            }
        }
        string talenttype = "";
        if (empId.Contains("Email"))
        {
            talenttype = empId.Replace("Email_", "");
            Response.Redirect("email.aspx?TalentType=" + talenttype);
        }
        if (empId != "")
        {
            Response.Redirect("add_edit_employee.aspx?empid=" + empId);
        }
    }
    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    protected void create_email()
    {

        CfsEntity cfsEntity = new CfsEntity();
        List<Talent> list;




        try
        {


            list = ((ObjectQuery<Talent>)cfsEntity.Talent.Where("it.IsActive = true")).ToList();
          
       
       

            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Server.MapPath("~//media//") + "mycsvfile.csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string delimter = ",";
            List<string[]> output = new List<string[]>();

            //flexible part ... add as many object as you want based on your app logic
            output.Add(new string[] { "Title","First Name","Middle Name","Last Name","Suffix","Company","Department","Job Title","Business Street","Business Street 2","Business Street 3","Business City","Business State","Business Postal Code","Business Country/Region","Home Street","Home Street 2","Home Street 3","Home City","Home State","Home Postal Code","Home Country/Region","Other Street","Other Street 2","Other Street 3","Other City","Other State","Other Postal Code","Other Country/Region","Assistant's Phone","Business Fax","Business Phone","Business Phone 2","Callback","Car Phone","Company Main Phone","Home Fax","Home Phone","Home Phone 2","ISDN","Mobile Phone","Other Fax","Other Phone","Pager","Primary Phone","Radio Phone","TTY/TDD Phone","Telex","Account","Anniversary","Assistant's Name","Billing Information","Birthday","Business Address PO Box","Categories","Children","Directory Server","E-mail Address","E-mail Type","E-mail Display Name","E-mail 2 Address","E-mail 2 Type","E-mail 2 Display Name","E-mail 3 Address","E-mail 3 Type","E-mail 3 Display Name","Gender","Government ID Number","Hobby","Home Address PO Box","Initials","Internet Free Busy","Keywords","Language","Location","Manager's Name","Mileage","Notes","Office Location","Organizational ID Number","Other Address PO Box","Priority","Private","Profession","Referred By","Sensitivity","Spouse","User 1","User 2","User 3","User 4","Web Page"});
            int i = 0;
            
            List<CommaOutput> commalist = new List<CommaOutput>();

            foreach (Talent t in list)
            {
                if (t.Address1 == null)
                    t.Address1 = "";
                if (t.Address2 == null)
                    t.Address2 = "";
                
                if (t.DisplayName == null)
                    t.DisplayName = "";
                if (t.SpecialNotes == null)
                    t.SpecialNotes = "";
                if (t.State == null)
                    t.State = "";
                try
                {
                    CommaOutput D = new CommaOutput();
                    D.Title = "";
                    D.FirstName = t.DisplayName.Replace(",", "-");
                    D.MiddleName = "";
                    D.LastName = "";
                    D.Suffix = "";
                    D.Company = "Centerfold Strips";
                    D.Department = ""; 
                    D.Job_Title = UppercaseFirst(t.TalentType.Replace(",", "-")); 

                    D.Business_Street = t.Address1.Replace(",", "-");
                    

                    D.Business_Street_2 = t.Address2.Replace(",", "-"); 
                    D.Business_Street_3 = "";
                    D.Business_City = t.City.Replace(",", "-");
                    D.Business_State = t.State;
                    D.Business_Postal_Code = t.Zip;
                    D.Business_Country_Region = "" ;
                    D.Home_Street = "";
                    D.Home_Street_2 = "";
                    D.Home_Street_3 = "";
                    D.Home_City = "";
                    D.Home_State = "";
                    D.Home_Postal_Code = "";
                    D.Home_Country_Region = "";
                    D.Other_Street = "";
                    D.Other_Street_2 = "";
                    D.Other_Street_3 = "";
                    D.Other_City = "";
                    D.Other_State = "";
                    D.Other_Postal_Code = "";
                    D.Other_Country_Region = "";
                    D.Assistants_Phone = "";
                    D.Business_Fax = "";
                   
                    D.Business_Phone ="";
                    D.Business_Phone_2 = t.HomePhone;
                    D.Callback = "";
                    D.Car_Phone = "";
                    D.Company_Main_Phone = "";
                    D.Home_Fax = "";
                    D.Home_Phone = "";
                    D.Home_Phone_2 = "";
                    D.ISDN = "";
                    D.Mobile_Phone = t.CellPhone;
                    D.Other_Fax = "";
                    D.Other_Phone = "";
                    D.Pager = "";
                    D.Primary_Phone = "";
                    D.Radio_Phone = "";
                    D.TTY_TDD_Phone = "";
                    D.Telex = "";
                    D.Account = "";
                    D.Anniversary = "";
                    D.Assistants_Name = "";
                    D.Billing_Information = "";
                    try
                    {
                        D.Birthday = t.DOB.Value.ToShortDateString().Replace(",", "-");
                    }
                    catch (Exception ex)
                    {
                        D.Birthday = "";

                    }
                    D.Business_Address_PO_Box = "";
                    D.Categories = "";
                    D.Children = "";
                    D.Directory_Server = "";
                    D.E_mail_Address = t.EmailPrimary;
                    D.E_mail_2_Type = "";
                    D.E_mail_Display_Name = t.DisplayName.Replace(",", "-"); ;
                    D.E_mail_2_Address = t.EmailSecondary;
                    D.E_mail_2_Type = "";
                    D.E_mail_2_Display_Name = t.DisplayName.Replace(",", "-"); ;
                    D.E_mail_3_Address = "";
                    D.E_mail_3_Type = "";
                    D.E_mail_3_Display_Name = "";
                    D.Gender = "";
                    D.Government_ID_Number = "";
                    D.Hobby = "";
                    D.Home_Address_PO_Box = "";
                    D.Initials = "";
                    D.Internet_Free_Busy = "";
                    D.Keywords = "";
                    D.Language = "";
                    D.Location = "";
                    D.Managers_Name = "";
                    D.Mileage = "";
                    //D.Notes = t.SpecialNotes.Replace(",", "-");
                    D.Notes = ""; 
                    D.Office_Location = "";
                    D.Organizational_ID_Number = "";
                    D.Other_Address_PO_Box = "";
                    D.Priority = "";
                    D.Private = "";
                    D.Profession = "";
                    D.Referred_By = "";
                    D.Sensitivity = "";
                    D.Spouse = "";
                    D.User_1 = "";
                    D.User_2 = "";
                    D.User_3 = "";
                    D.User_4 = "";
                    D.Web_Page = "http://centerfoldstrips.com/Entertainers/Talent-Details.aspx?id=" + t.TalentId;


                    commalist.Add(D);

                }

                catch (Exception exxx)
                {
                    string ddddd = t.DisplayName;
                
                }
                

            }
            string s = "";


            try
            {
                foreach (CommaOutput D in commalist)
                {

                   // i = i + 1;
                    if (i <= 3)
                    {
                        string[] h = new string[]{
                D.Title, 
                D.FirstName,
                D.MiddleName ,
                D.LastName ,
                D.Suffix ,
                D.Company ,
                D.Department ,
                D.Job_Title ,
                D.Business_Street,
                D.Business_Street_2 ,
                D.Business_Street_3 ,
                D.Business_City ,
                D.Business_State,
                D.Business_Postal_Code ,
                D.Business_Country_Region ,
                D.Home_Street,
                D.Home_Street_2 ,
                D.Home_Street_3 ,
                D.Home_City,
                D.Home_State ,
                D.Home_Postal_Code ,
                D.Home_Country_Region ,
                D.Other_Street,
                D.Other_Street_2 ,
                D.Other_Street_3,
                D.Other_City ,
                D.Other_State ,
                D.Other_Postal_Code,
                D.Other_Country_Region,
                D.Assistants_Phone ,
                D.Business_Fax ,
                D.Business_Phone ,
                D.Business_Phone_2 ,
                D.Callback ,
                D.Car_Phone ,
                D.Company_Main_Phone,
                D.Home_Fax ,
                D.Home_Phone ,
                D.Home_Phone_2 ,
                D.ISDN ,
                D.Mobile_Phone ,
                D.Other_Fax ,
                D.Other_Phone,
                D.Pager ,
                D.Primary_Phone ,
                D.Radio_Phone ,
                D.TTY_TDD_Phone ,
                D.Telex,
                D.Account ,
                D.Anniversary ,
                D.Assistants_Name ,
                D.Billing_Information ,
                D.Birthday ,
                D.Business_Address_PO_Box,
                D.Categories ,
                D.Children ,
                D.Directory_Server ,
                D.E_mail_Address ,
                D.E_mail_2_Type ,
                D.E_mail_Display_Name ,
                D.E_mail_2_Address ,
                D.E_mail_2_Type,
                D.E_mail_2_Display_Name ,
                D.E_mail_3_Address ,
                D.E_mail_3_Type,
                D.E_mail_3_Display_Name ,
                D.Gender,
                D.Government_ID_Number ,
                D.Hobby ,
                D.Home_Address_PO_Box ,
                D.Initials ,
                D.Internet_Free_Busy ,
                D.Keywords ,
                D.Language ,
                D.Location,
                D.Managers_Name ,
                D.Mileage ,
                D.Notes,
                D.Office_Location ,
                D.Organizational_ID_Number ,
                D.Other_Address_PO_Box ,
                D.Priority ,
                D.Private ,
                D.Profession ,
                D.Referred_By ,
                D.Sensitivity ,
                D.Spouse ,
                D.User_1 ,
                D.User_2 ,
                D.User_3 ,
                D.User_4,
                
                D.Web_Page
                
                };
                        try
                        {
                            output.Add(h);
                        }
                        catch (Exception ex)
                        { 
                        
                        }
                    }

                     
                }
            }
            catch (Exception ex)
            {
                string g = "";
            }
           /* foreach (Talent t in list)
            {
                i = i + 1;
                if (i == 1)
                {
                    
                    output.Add(new string[] { "", t.DisplayName, "", "", "", "Centerfold Strips", t.TalentType, t.TalentType, t.Address1, t.Address2, "", t.City, t.State, t.Zip, t.Country, t.Address1, t.Address2, "", t.City, t.State, t.Zip, t.Country, "", "", "", "", "", "", "", "", "", t.HomePhone, t.CellPhone, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", t.TalentId.ToString(), "", "", "", t.DOB.ToString(), "", "", "", "", t.EmailPrimary, "", t.FirstName + " , " + t.LastName, t.EmailSecondary, "", t.FirstName + " , " + t.LastName, "", "", "", "", "", "", "", "", "", t.TalentType, "", "", "", "", t.SpecialNotes, "", "", "", "", "", t.TalentType, "", "", "", "", "", "", "", "http://centerfoldstrips.com/Entertainers/Talent-Details.aspx?id=" + t.TalentId });
                    
                }
            }*/
           
            int length = output.Count;

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {

                for (int index = 0; index < length; index++)
                {
                    writer.WriteLine(string.Join(delimter, output[index]));
                }
            }
        }
        catch (Exception ex)
        { }
        Response.Redirect("Email2.aspx");
    
    }
    protected void DeleteVideo_Click(object sender, EventArgs e)
    {
        CheckBoxList checklist =
            (CheckBoxList)Place.FindControl("checkboxlist1");

        // Make sure a control was found.
        if (checklist != null)
        {

            //Message.Text = "Deleted Selected Item(s):<br><br>";

            // Iterate through the Items collection of the CheckBoxList 
            // control and display the selected items.
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~//media//") + "playlist.xml");


            // string myvideofilepath = SaveLocation;
            XmlNode root = doc.DocumentElement;
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

            for (int i = checklist.Items.Count - 1; i > -1; i--)
            {

                if (checklist.Items[i].Selected)
                {
                    // removelist[begin] = i;
                    int num = 0;
                    foreach (var coordinate in xDoc.Descendants("item"))
                    {

                        if (num == i)
                        {

                            coordinate.Remove();
                            break;
                        }
                        num++;



                    }
                    //Message.Text += checklist.Items[i].Text + "<br>";

                }

            }

            xDoc.Save(Server.MapPath("~//media//" + "playlist.xml"));
           Response.Redirect("~//backend//default.aspx");
            //checklist.Items.Clear();
            // DeleteVideo.Attributes["onclick"] = "javascript:SomeFunction();return true;";
            //button1
        }


    }
    protected void OnClick_btnTalentSearchGo(object sender, EventArgs arrrggggs)
    {
        string talentType = ddlSearchTalType.SelectedValue;
        string talentLoc = ddlSearchLivesIn.SelectedValue;
        string talentWorks = ddlSearchWorksIn.SelectedValue;

        string redirUrl = "view_talent_search_results.aspx?search=true";
        
        if (talentType != "")
        {
            redirUrl += "&type=" + talentType;
        }
        if (talentLoc != "")
        {
            redirUrl += "&livesin=" + talentLoc;
        }
        if (talentWorks != "")
        {
            redirUrl += "&worksin=" + talentWorks;
        }
        
        Response.Redirect(redirUrl);
    }

    protected void OnClick_btnGetInactiveEmployees(object sender, EventArgs e)
    {
        Response.Redirect("view_talent_search_results.aspx?inactive=true");
    }

    protected void OnClick_btnSearchWorkOrder(object sender, EventArgs e)
    {
        string redirUrl = "view_work_orders.aspx?search=true";

        if (tBoxSearchWorkOrderNum.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_WO_NUM + "=" + tBoxSearchWorkOrderNum.Text;
        }
        if (tBoxSearchFirstName.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_FIRST_NAME + "=" + tBoxSearchFirstName.Text;
        }
        if (tBoxSearchLastName.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_LAST_NAME + "=" + tBoxSearchLastName.Text;
        }
        if (tBoxSearchJobDate.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_JOB_DATE + "=" + tBoxSearchJobDate.Text;
        }
        if (tBoxSearchLocation.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_LOCATION + "=" + tBoxSearchLocation.Text;
        }
        if (tBoxSearchCity.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_CITY + "=" + tBoxSearchCity.Text;
        }
        if (ddlSearchState.SelectedValue != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_STATE + "=" + ddlSearchState.SelectedValue;
        }
        if (tBoxSearchCcNum.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_CCNUM + "=" + tBoxSearchCcNum.Text;
        }
        if (ddlSearchFullPerformerList.SelectedValue != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_TALID + "=" + ddlSearchFullPerformerList.SelectedValue;
        }
        if (tBoxSearchReferredBy.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_REFER + "=" + tBoxSearchReferredBy.Text;
        }
		if (tBoxSearchEventType.Text != "")
        {
            redirUrl += "&" + CfsCommon.PARAM_WO_SEARCH_EVENT_TYPE + "=" + tBoxSearchEventType.Text;
        }
        Response.Redirect(redirUrl, true);
    }

    protected void OnClick_btnViewWorkOrder(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument);
        }
    }

    protected void OnClick_btnDeleteLogo(object sender, EventArgs e)
    {
        const string WEB_IMAGE_PATH = "~/images/specialempty.jpg";


        CfsEntity cfsEntity = new CfsEntity();
        List<AppSetting> list;

       


        try
        {


            list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEU_LOGO_IMGSRC + "'")).ToList();
            if (list.Count == 1)
            {
                //Img Setting
                list[0].SettingValue = WEB_IMAGE_PATH;
            }
        }
        catch (Exception)
        {
            lblPromoFeedback.Text = "Error saving file.";
            return;
        }


        if (cfsEntity.SaveChanges() > 1)
        {
            lblPromoFeedback.Text = "Updated Successfully.";
        }

        GetPromoSpecialInfo();
    }

    protected void OnClick_btnDeletePromo(object sender, EventArgs e)
    {
        const string WEB_IMAGE_PATH = "~/images/specialempty.jpg";
       

        CfsEntity cfsEntity = new CfsEntity();
        List<AppSetting> list;

        list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEY_PROMO_URL + "'")).ToList();
        if (list.Count == 1)
        {
            //Url Setting:
            list[0].SettingValue = tBoxPromoUrl.Text;
        }

       
            try
            {
               

                list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC + "'")).ToList();
                if (list.Count == 1)
                {
                    //Img Setting
                    list[0].SettingValue = WEB_IMAGE_PATH;
                }
            }
            catch (Exception)
            {
                lblPromoFeedback.Text = "Error saving file.";
                return;
            }
        

        if (cfsEntity.SaveChanges() > 1)
        {
            lblPromoFeedback.Text = "Updated Successfully.";
        }

        GetPromoSpecialInfo();
    }
    protected void OnClick_btnUpdatePromo(object sender, EventArgs e)
    {
        const string WEB_IMAGE_PATH = "~/images/promo/";
        string fileName = "";
        string path = Server.MapPath(WEB_IMAGE_PATH);

        CfsEntity cfsEntity = new CfsEntity();
        List<AppSetting> list;

        list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEY_PROMO_URL + "'")).ToList();
        if (list.Count == 1)
        {
            //Url Setting:
            list[0].SettingValue = tBoxPromoUrl.Text;
        }

        if (fuPromoImg.HasFile)
        {
            try
            {
                fileName = fuPromoImg.FileName;

                fuPromoImg.SaveAs(path + fileName);

                list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC + "'")).ToList();
                if (list.Count == 1)
                {
                    //Img Setting
                    list[0].SettingValue = WEB_IMAGE_PATH + fileName;
                }
            }
            catch (Exception)
            {
                lblPromoFeedback.Text = "Error saving file.";
                return;
            }
        }

        if (cfsEntity.SaveChanges() > 1)
        {
            lblPromoFeedback.Text = "Updated Successfully.";
        }

        GetPromoSpecialInfo();
    }

    protected void OnClick_btnUpdateLogoText(object sender, EventArgs e)
    {
        CfsEntity cfsEntity = new CfsEntity();
        List<AppSetting> list;
        try
        {
            list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEY_LOGO_TEXT + "'")).ToList();
            if (list.Count == 1)
            {
                //Img Setting
                list[0].SettingValue = txt_Logo.Text;
            }
        }
        catch (Exception ex)
        {
            lbl_LogoText.Text = "Error saving Text";
            return;
        }
        if (cfsEntity.SaveChanges() > 1)
        {
            lbl_LogoText.Text = "Updated Successfully.";
        }
        Response.Redirect("default.aspx");
        GetPromoSpecialInfo();
    }

    protected void OnClick_btnUpdateLogo(object sender, EventArgs e)
    {
        const string WEB_IMAGE_PATH = "~/images/promo/logo/";
        string fileName = "";
        string path = Server.MapPath(WEB_IMAGE_PATH);

        CfsEntity cfsEntity = new CfsEntity();
        List<AppSetting> list;

        string SaveLocation = path;

        if (fuLogoImg.HasFile)
        {
            try
            {
                fileName = fuLogoImg.FileName;
                fileName = fileName.Replace("%20", "_");
               

                // Create the path and file name to check for duplicates.
                string pathToCheck = SaveLocation + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = SaveLocation + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    //UploadStatusLabel.Text = "A file with the same name already exists." +
                    //  "<br />Your file was saved as " + fileName;
                }
                SaveLocation += fileName;
                fuLogoImg.SaveAs(SaveLocation);

                list = ((ObjectQuery<AppSetting>)cfsEntity.AppSetting.Where("it.SettingKey = '" + CfsCommon.APP_SETTING_KEU_LOGO_IMGSRC + "'")).ToList();
                if (list.Count == 1)
                {
                    //Img Setting
                    list[0].SettingValue = WEB_IMAGE_PATH + fileName;
                }
            }
            catch (Exception ex)
            {
                lblLogoFeedback.Text = "Error saving file. [Try changing file name]";
                return;
            }
        }

        if (cfsEntity.SaveChanges() > 1)
        {
            lblLogoFeedback.Text = "Updated Successfully.";
        }

        GetPromoSpecialInfo();
    }

    protected void btnStaffNotes_Click(object sender, EventArgs e)
    {
        CfsEntity ent = new CfsEntity();
        StaffNote note = new StaffNote();
        note.Text = tbStaffNotes.Text;
        note.Date = DateTime.Now;
        note.UserId = (int)Session[CfsCommon.SESSION_KEY_USERID];

        ent.AddToStaffNote(note);
        ent.SaveChanges();

        LoadStaffNotes();
    }

    #endregion

    #region Callbacks
    void grdViewWorkOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row == null || e.Row.DataItem == null) { return; }

        DataRowView dRowView = (DataRowView)e.Row.DataItem;
        
        if( CfsEntity == null )
        {
            CfsEntity = new CfsEntity();
        }

        Job theJob = CfsCommon.GetJobRecord(CfsEntity, dRowView["JobId"].ToString() );
        try
        {
            if (!theJob.TalentToJob.IsLoaded)
            {
                theJob.TalentToJob.Load();
            }

            if (theJob.TalentToJob.Count > 0)
            {
                e.Row.Cells[4].Text = theJob.TalentToJob.Min(st => st.StartDateTime).ToString("h:mm tt");

                Image imgHasTalent = (Image)e.Row.FindControl("imgHasTalent");

                if (imgHasTalent != null)
                {
                    imgHasTalent.Visible = true;
                }
            }
            if (theJob.JobPaid != 1)
            {
                Image imgHasPaid = (Image)e.Row.FindControl("imgHasPaid");

                if (imgHasPaid != null)
                {
                    imgHasPaid.Visible = true;
                }

            }
            if (theJob.Highlight != null)
            {
                if (theJob.Highlight == 1)
                {
                    e.Row.CssClass = "highlightWeek";
                }

            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    private void GetQuickRefTalentLists()
    {
        CfsCommon.GetSingleTalentList(ddlFemaleDancer, CfsCommon.TALENT_TYPE_ID_FEMALE);
        CfsCommon.GetSingleTalentList(ddlMaleDancer, CfsCommon.TALENT_TYPE_ID_MALE);
        CfsCommon.GetSingleTalentList(ddlFemaleMini, CfsCommon.TALENT_TYPE_ID_FEMALE_MINI);
        CfsCommon.GetSingleTalentList(ddlMaleMini, CfsCommon.TALENT_TYPE_ID_MALE_MINI);
        CfsCommon.GetSingleTalentList(ddlBellyDancer, CfsCommon.TALENT_TYPE_ID_BELLY_DANCER);
        CfsCommon.GetSingleTalentList(ddlBbw, CfsCommon.TALENT_TYPE_ID_BBW);
        CfsCommon.GetSingleTalentList(ddlDragQueen, CfsCommon.TALENT_TYPE_ID_DRAG);
        CfsCommon.GetSingleTalentList(ddlImpersonator, CfsCommon.TALENT_TYPE_ID_IMPERSON);
        CfsCommon.GetSingleTalentList(ddlNoveltyActs, CfsCommon.TALENT_TYPE_ID_NOVELTY);
        CfsCommon.GetSingleTalentList(ddlDuoShows, CfsCommon.TALENT_TYPE_ID_DUO);
        CfsCommon.GetSingleTalentList(ddlDriversBouncers, CfsCommon.TALENT_TYPE_ID_DRIVER);
        CfsCommon.GetSingleTalentList(ddlAffiliate, CfsCommon.TALENT_TYPE_ID_AFFILIATE);
        CfsCommon.getTalentBirthdayList(ddlBirthdays);
    }

    private void LoadWorkOrderQuickViewOldSchool()
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select;

        select = "SELECT c.FirstName, c.LastName, e.EventDate, e.LocationName, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins, tj.StartDateTime ";
        select += "FROM Customer c JOIN Event e on c.CustomerId = e.CustomerId ";
        select += "JOIN Job j on e.EventId = j.EventId ";
        select += "LEFT OUTER JOIN TalentToJob tj on j.JobId = tj.JobId ";
        select += "WHERE e.EventDate >= '" + DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM' ";
        select += "AND e.EventDate <= '" + CfsCommon.GetEndOfCurrentWeek() + "' ";
        select += "AND j.IsJobCancelled = 0 ";
        select += "AND (j.IsBalanceCollected = 0 OR j.IsJobComplete = 0)";
        select += "GROUP BY c.FirstName, c.LastName, e.EventDate, e.LocationName, tj.StartDateTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins ORDER BY e.EventDate ASC, tj.StartDateTime ASC;";


        SqlDataSource dataSrc = new SqlDataSource(conn, select);

        //try
        {
            grdViewWorkOrders.DataSource = dataSrc;
            grdViewWorkOrders.DataBind();
        }
        //catch (Exception)
        {

        }
    }

    /* Gets the Hyperlink and Image for the 'Promo Specials' box (line 26-ish) */
    private void GetPromoSpecialInfo()
    {
        CfsEntity cfsEntity = new CfsEntity();

        foreach (AppSetting setting in cfsEntity.AppSetting)
        {
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_PROMO_URL)
            {
                tBoxPromoUrl.Text = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC)
            {
                imgPromo.ImageUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_LOGO_IMGSRC)
            {
                imgLogoBottom.ImageUrl = setting.SettingValue;
                
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_LOGO_TEXT)
            {
                txt_Logo.Text= setting.SettingValue;

            }
        }
    }

    private void LoadStaffNotes()
    {
        CfsEntity cfsEntity = new CfsEntity();

        StaffNote note = null;
        try
        {
            note = cfsEntity.StaffNote.OrderBy("it.Date DESC").First();
        }
        catch
        {
        }

        if (note != null)
        {
            tbStaffNotes.Text = note.Text;
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
            try
            {
                string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("~//media//video//") +  fn;
               // string SaveLocation = Server.MapPath("Data") + "\\" + fn;
               
                FileUpload1.SaveAs(SaveLocation);
                //Label1.Text = "File name: " +
                  //   FileUpload1.PostedFile.FileName + "<br>" +
                    // FileUpload1.PostedFile.ContentLength + " kb<br>" +
                    // "Content type: " +
                     //FileUpload1.PostedFile.ContentType;
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~//media//") + "playlist.xml");

                string myvideofilepath = SaveLocation;
                XmlNode root = doc.DocumentElement;
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(doc.CreateNavigator().ReadSubtree());

                if (xDoc.XPathSelectElement("//item") != null)
                {
                    xDoc.XPathSelectElement("//item")
                        .AddBeforeSelf(new XElement("item"), "");

                }
                else
                {
                    xDoc.XPathSelectElement("//channel").Add(new XElement("item"), "");
                }

                xDoc.XPathSelectElement("//item").Add(new XElement("title", "Main Page Video"));
                xDoc.XPathSelectElement("//item").Add(new XElement("content", new XAttribute("url", "http://www.centerfoldstrips.com:55555/media/video/" + fn), new XAttribute("type", "video/x-flv"), new XAttribute("start", "00:00")));
                xDoc.XPathSelectElement("//item").Add(new XElement("thumbnail", new XAttribute("url", "media/video/videoStill1.jpg")));

                xDoc.XPathSelectElement("//item").Add(new XElement("description", "Main Page Video"));

                xDoc.XPathSelectElement("//item").Add(new XElement("link", "http://CenterfoldStrips.com"));

                var xmlDocument = new XmlDocument();
                using (var xmlReader = xDoc.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }
                //context.Server.MapPath("~//media//video//
                //xDoc.Save(Server.MapPath("Video") + "\\" +"playlist2.xml");
                xDoc.Save(Server.MapPath("~//media//" + "playlist.xml"));
                Response.Redirect("~//backend//default.aspx");
               
            }
            catch (Exception ex)
            {
                Label1.Text = "ERROR: " + ex.Message.ToString();
            }
        else
        {
            Label1.Text = "You have not specified a file.";
        }

        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (FileUpload2.HasFile)
            try
            {
                string fn = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                string SaveLocation = Server.MapPath("~//media//video/videoStill1.jpg");
                // string SaveLocation = Server.MapPath("Data") + "\\" + fn;

                FileUpload2.SaveAs(SaveLocation);
               
                
                Response.Redirect("~//backend//default.aspx");

            }
            catch (Exception ex)
            {
                Label1.Text = "ERROR: " + ex.Message.ToString();
            }
        else
        {
            Label1.Text = "You have not specified a file.";
        }


    }
    protected void btn_createcsv_Click(object sender, EventArgs e)
    {
        create_email();
    }
}
