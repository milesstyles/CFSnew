using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.ComponentModel;

using CfsNamespace;


public partial class backend_user_admin : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.UserAdmin, Session, Response);
            
        if( !IsPostBack )
        {
            GetUserTypeList();
        }

        grdActiveUsers.RowDataBound += new GridViewRowEventHandler(grdUsers_RowDataBound);
        grdInactiveUsers.RowDataBound += new GridViewRowEventHandler(grdUsers_RowDataBound);
    }

    protected void OnClick_btnSubmit(object sender, EventArgs e)
    {
        if (FormIsValid())
        {
            if (AddOrUpdateUser(hiddenUserId.Value))
            {
                ClearForm();
                grdActiveUsers.DataBind();
                grdInactiveUsers.DataBind();
            }
        }
    }

    protected void OnClick_btnEdit(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (string.IsNullOrEmpty(btn.CommandArgument))
        {
            return; /* Should never happen */
        }

        LoadUserForEdit(btn.CommandArgument);
    }

    protected void OnClick_btnDelete(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (string.IsNullOrEmpty(btn.CommandArgument))
        {
            return; /* Should never happen */
        }
        LoadUsersForDelete(btn.CommandArgument);

        Response.Redirect("user_admin.aspx");
       // LoadUserForEdit(btn.CommandArgument);
    }

    protected void OnChange_ddlUserType(object sender, EventArgs e)
    {
        GetUserTypeTemplate(ddlUserType.SelectedValue);
    }

    void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;

        CfsUser userRecord = CfsCommon.EntityDataSourceExtensions.GetItemObject<CfsUser>(e.Row.DataItem);


        if (userRecord == null) { return; }

        if (userRecord.CfsUserType != null ) //Ensure the foreign key data is valid
        {
            Label lbl = e.Row.FindControl("lblUserType") as Label;

            if (lbl != null)
            {
                lbl.Text = userRecord.CfsUserType.UserTypeName;
            }
        }
    }
    #endregion

    #region DB Read Only Functions

    private bool LoadUsersForDelete(string userId)
    {
        CfsEntity cfsEntity = new CfsEntity();
       
        bool re = CfsCommon.DeleteUserRecord(cfsEntity, userId);

        return re;
       
    }
    private bool LoadUserForEdit(string userId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        CfsUser userRecord;

        if ((userRecord = CfsCommon.GetUserRecord(cfsEntity, userId)) == null)
        {
            return false;
        }

        tBoxUserName.Text = userRecord.UserName;
        tBoxPassword.Text = "";
        tBoxPasswordAgain.Text = "";
        chkActive.Checked = userRecord.IsActive; //Special Checkbox

        if (!userRecord.CfsUserTypeReference.IsLoaded)
        {
            userRecord.CfsUserTypeReference.Load();
        }

        ddlUserType.SelectedValue = userRecord.CfsUserType.UserTypeId.ToString();
        tBoxNotes.Text = userRecord.Notes;
        LoadUserPermissions(userRecord);

        hiddenUserId.Value = userRecord.UserId.ToString();
        return true;
    }

    private void LoadUserPermissions(CfsUser userRecord)
    {
        if (!userRecord.CfsUserToSection.IsLoaded)
        {
            userRecord.CfsUserToSection.Load();
        }

        foreach (CfsUserToSection secAccess in userRecord.CfsUserToSection)
        {
            if (!secAccess.CfsSectionReference.IsLoaded)
            {
                secAccess.CfsSectionReference.Load();
            }

            switch (secAccess.CfsSection.SectionId)
            {
                case (int)CfsCommon.Section.UserAdmin: { chkUserAdmin.Checked = true; break; }
                case (int)CfsCommon.Section.EmployeeMgmt: { chkEmployeeMgmt.Checked = true; break; }
                case (int)CfsCommon.Section.PendingJobs: { chkPendingJobs.Checked = true; break; }
                case (int)CfsCommon.Section.WorkOrders: { chkWorkOrders.Checked = true; break; }
                case (int)CfsCommon.Section.FranchiseMgmt: { chkFranchiseMgmt.Checked = true; break; }
                case (int)CfsCommon.Section.Accounting: { chkAccounting.Checked = true; break; }
                case (int)CfsCommon.Section.BalanceCollected: { chkBalanceCollected.Checked = true; break; }
                case (int)CfsCommon.Section.CreditCard: { chkViewCCinfo.Checked= true; break; }
           
            }
        }
    }

    private void GetUserTypeList()
    {
        CfsEntity cfsEntity = new CfsEntity();

        foreach (CfsUserType userType in cfsEntity.CfsUserType)
        {
            ListItem item = new ListItem(userType.UserTypeName, userType.UserTypeId.ToString());

            ddlUserType.Items.Add(item);
        }
    }
    #endregion

    #region DB Update Functions
    private bool AddOrUpdateUser(string userId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        CfsUser userRecord;
        string newPass = tBoxPassword.Text;

        if (ddlUserType.SelectedValue == "")
        {
            return false; /* FailSafe */
        }

        if (string.IsNullOrEmpty(userId))
        {
            /* Create new record */
            userRecord = new CfsUser();
            userRecord.UserPass = CfsCommon.Encrypt(newPass, true);
            newPass = ""; //Clear from Memory

            userRecord.CfsUserType = new CfsUserType();
        }
        else
        {
            /* Find existing record */
            if ((userRecord = CfsCommon.GetUserRecord(cfsEntity, userId)) == null)
            {
                return false;
            }

            /* If pass field is blank, we don't update the password on current record. */
            if (newPass != "")
            {
                userRecord.UserPass = CfsCommon.Encrypt(newPass, true);
                newPass = ""; //Clear from Memory
            }
        }

        userRecord.IsActive = chkActive.Checked; //Special Checkbox
        userRecord.CfsUserTypeReference.Value = GetUserTypeReference(cfsEntity, ddlUserType.SelectedValue);
        userRecord.UserName = tBoxUserName.Text;
        userRecord.Notes = tBoxNotes.Text;

        if (string.IsNullOrEmpty(userId))
        {
            cfsEntity.AddToCfsUser(userRecord);
        }

        cfsEntity.SaveChanges();

        UpdateUserPermissions(cfsEntity, userRecord);

        cfsEntity.SaveChanges();

        return true;
    }
    

    private void UpdateUserPermissions(CfsEntity cfsEntity, CfsUser userRecord)
    {
        if (!userRecord.CfsUserToSection.IsLoaded)
        {
            userRecord.CfsUserToSection.Load();
        }

        List<CfsUserToSection> usrAccessList = userRecord.CfsUserToSection.ToList();

        /* Remove all User Access Records (Table 'CfsUserToSection' */
        for (int i = 0; i < usrAccessList.Count; i++)
        {
            cfsEntity.DeleteObject(usrAccessList[i]);
        }

        if (chkUserAdmin.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.UserAdmin);
        }
        if (chkEmployeeMgmt.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.EmployeeMgmt);
        }
        if (chkPendingJobs.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.PendingJobs);
        }
        if (chkWorkOrders.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.WorkOrders);
        }
        if (chkFranchiseMgmt.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.FranchiseMgmt);
        }
        if (chkAccounting.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.Accounting);
        }
        if (chkBalanceCollected.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.BalanceCollected);
        }
        if (chkViewCCinfo.Checked)
        {
            AddUserSecAccessRecord(cfsEntity, userRecord, (int)CfsCommon.Section.CreditCard);
        }
    }

    private void AddUserSecAccessRecord(CfsEntity cfsEntity, CfsUser userRecord, int sectionId)
    {
        List<CfsSection> sectionList = ((ObjectQuery<CfsSection>)cfsEntity.CfsSection.Where("it.SectionId = " + sectionId.ToString())).ToList();

        if (sectionList.Count != 1)
        {
            /* ERROR - Section does not exists 
             *
             * This is not ever expected to happen, unless there are abnormal DB changes.
             */
            return;
        }

        CfsUserToSection usrAccessRec = new CfsUserToSection();

        usrAccessRec.CfsUserReference.Value = userRecord;
        usrAccessRec.CfsSectionReference.Value = sectionList[0];

        cfsEntity.AddToCfsUserToSection(usrAccessRec);
    }
    #endregion

    private CfsUserType GetUserTypeReference(CfsEntity cfsEntity, string userTypeId)
    {
        List<CfsUserType> userTypeList = ((ObjectQuery<CfsUserType>)cfsEntity.CfsUserType.Where("it.UserTypeId = " + userTypeId)).ToList();

        if (userTypeList.Count == 1)
        {
            return userTypeList[0];
        }
        else
        {
            return null;
        }
    }

    private void GetUserTypeTemplate(string userTypeId)
    {
        ClearCheckboxes();
        
        if (string.IsNullOrEmpty(userTypeId))
        {
            return;
        }

        CfsEntity cfsEntity = new CfsEntity();

        foreach (CfsUserTypeTemplate usrTemplate in cfsEntity.CfsUserTypeTemplate)
        {
            if (!usrTemplate.CfsUserTypeReference.IsLoaded)
            { 
                usrTemplate.CfsUserTypeReference.Load();
            }

            if (usrTemplate.CfsUserType.UserTypeId.ToString() == userTypeId)
            {
                if (!usrTemplate.CfsSectionReference.IsLoaded)
                {
                    usrTemplate.CfsSectionReference.Load();
                }

                switch (usrTemplate.CfsSection.SectionId)
                {
                    case (int)CfsCommon.Section.UserAdmin: { chkUserAdmin.Checked = true; break; }
                    case (int)CfsCommon.Section.EmployeeMgmt: { chkEmployeeMgmt.Checked = true; break; }
                    case (int)CfsCommon.Section.PendingJobs: { chkPendingJobs.Checked = true; break; }
                    case (int)CfsCommon.Section.WorkOrders: { chkWorkOrders.Checked = true; break; }
                    case (int)CfsCommon.Section.FranchiseMgmt: { chkFranchiseMgmt.Checked = true; break; }
                    case (int)CfsCommon.Section.Accounting: { chkAccounting.Checked = true; break; }
                    case (int)CfsCommon.Section.BalanceCollected: { chkBalanceCollected.Checked = true; break; }
                    case (int)CfsCommon.Section.CreditCard: { chkViewCCinfo.Checked = true; break; }                    
                
                }
            }
        }
    }

    private void ClearCheckboxes()
    {
        chkActive.Checked = false;
        chkAccounting.Checked = false;
        chkBalanceCollected.Checked = false;
        chkEmployeeMgmt.Checked = false;
        chkFranchiseMgmt.Checked = false;
        chkPendingJobs.Checked = false;
        chkWorkOrders.Checked = false;
        chkUserAdmin.Checked = false;
        chkViewCCinfo.Checked = false;
    }

    private void ClearForm()
    {
        ClearCheckboxes();
        
        tBoxUserName.Text = "";
        tBoxPassword.Text = "";
        tBoxPasswordAgain.Text = "";
        tBoxNotes.Text = "";

        ddlUserType.SelectedValue = "";

        hiddenUserId.Value = "";
    }

    private bool FormIsValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";

        if (!CfsCommon.ValidateTextBoxReq(tBoxUserName, "User Name", ulErrorMsg))
        {
            isValid = false;
        }

        /* Blank Passwords are Ok only when editing new users. (PW does not change on blank) */
        if (hiddenUserId.Value == "" && !CfsCommon.ValidateTextBoxReq(tBoxPassword, "Password", ulErrorMsg))
        {
            isValid = false;
            tBoxPassword.CssClass = "textfield error";
            tBoxPasswordAgain.CssClass = "textfield error";
        }
        else if (tBoxPassword.Text != tBoxPasswordAgain.Text)
        {
            isValid = false;
            ulErrorMsg.InnerHtml += "<li>The Passwords entered do not match.</li>";
            tBoxPassword.CssClass = "textfield error";
            tBoxPasswordAgain.CssClass = "textfield error";
        }
        else
        {
            tBoxPassword.CssClass = "textfield";
            tBoxPasswordAgain.CssClass = "textfield";
        }

        if (ddlUserType.SelectedValue == "")
        {
            isValid = false;
            ulErrorMsg.InnerHtml += "<li>You must choose a 'User Type' from the list.</li>";
            ddlUserType.CssClass = "select error";
        }
        else
        {
            ddlUserType.CssClass = "select";
        }

        divErrorMsg.Visible = !isValid;

        return isValid;
    }

}
