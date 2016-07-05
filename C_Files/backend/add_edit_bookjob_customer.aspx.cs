using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;

using CfsNamespace;

public partial class backend_add_edit_bookajob : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);
        
        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlState);

            if (Request.Params[CfsCommon.PARAM_CUSTOMER_ID] != null)
            {
                string custId = (string)Request.Params[CfsCommon.PARAM_CUSTOMER_ID];

                if (GetCustomerInfo(custId))
                {
                    hiddenCustId.Value = custId;

                    if (Request.Params[CfsCommon.PARAM_UPDATE_MODE] != null)
                    {
                        UpdatePageMode(CfsCommon.MODE_UPDATE);
                    }
                    else
                    {
                        UpdatePageMode(CfsCommon.MODE_READONLY);
                    }
                }
            }
        }
    }

    protected void OnClick_btnAddEdit(object sender, EventArgs e)
    {
        switch (hiddenPageMode.Value)
        {
            case CfsCommon.MODE_ADD:
            case CfsCommon.MODE_UPDATE:
            {
                if (IsFormValid())
                {
                    int custId = AddOrUpdateCustomer(hiddenCustId.Value);

                    
                    
                    if (custId != CfsCommon.ERROR_VAL)
                    {
                        if (!String.IsNullOrEmpty((string)Request.Params[CfsCommon.PARAM_JOB_ID]))
                        {
                            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + (string)Request.Params[CfsCommon.PARAM_JOB_ID]);
                        }
                        else
                        {
                            Response.Redirect("add_edit_bookjob_event.aspx?" + CfsCommon.PARAM_CUSTOMER_ID + "=" + custId.ToString());
                        }
                    }
                }
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                /* Button changes to Edit Mode */
                UpdatePageMode(CfsCommon.MODE_UPDATE);
                break;
            }
        }
    }
    #endregion

    #region DB Add / Update
    private int AddOrUpdateCustomer(string custId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Customer newCust;

        if ( string.IsNullOrEmpty(custId) )
        {
            /* Create new Customer Record */
            newCust = new Customer();
            newCust.IsCustConfirmSent = false;
        }
        else
        {
            /* Retrieve Existing Customer Record */
            newCust = CfsCommon.GetCustomerRecord(cfsEntity, custId); 

            if (newCust == null)
            {
                return CfsCommon.ERROR_VAL;
            }
        }

        newCust.FirstName = tBoxFirstName.Text;
        newCust.LastName = tBoxLastName.Text;
        newCust.Address1 = tBoxAddress1.Text;
        newCust.Address2 = tBoxAddress2.Text;
        newCust.City = tBoxCity.Text;
        newCust.State = ddlState.SelectedValue;
        newCust.Zip = tBoxZip.Text;

        newCust.HomePhone = tBoxHomePhone.Text;
        newCust.CellPhone = tBoxCellPhone.Text;
        newCust.BusinessPhone = tBoxBusinessPhone.Text;
        newCust.BusinessPhoneExt = tBoxBusinessPhoneExt.Text;
        newCust.Fax = tBoxFax.Text;
        newCust.AltContactName = tBoxAltContact.Text;
        newCust.AltContactPhone = tBoxAltPhone.Text;
        newCust.Email = tBoxEmail.Text;
        newCust.ReferredBy = tBoxReferredBy.Text;


        if ( string.IsNullOrEmpty(custId)) 
        {
            /* Add new Customer Record */
            cfsEntity.AddToCustomer(newCust);
        }
        //else, record will just be updated
        UpdateYmlp();
        if (cfsEntity.SaveChanges() <= 0)
        {
            return CfsCommon.ERROR_VAL;
        }
        else
        {
            return newCust.CustomerId;
        }
    }
    #endregion
    private void UpdateYmlp()
    {
        string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tBoxEmail.Text + "&GroupID=2&Field1=" + tBoxFirstName.Text + "&Field2=" + tBoxLastName.Text + "&Field11=" + tBoxAddress1.Text + "&Field12=" + tBoxAddress2.Text + "&Field13=" + tBoxCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tBoxZip.Text + "&Field16=United States" + "&Field17=" + tBoxHomePhone.Text + "&Field18=" + tBoxCellPhone.Text;
        Myxmlhttp callme = new Myxmlhttp();
        callme.xmlhttp(ymlpurl);
       
            
       
    
    
    }
    private bool GetCustomerInfo(string custId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Customer custRecord;

        if ((custRecord = CfsCommon.GetCustomerRecord(cfsEntity, custId)) == null)
        {
            /* No record found, bad or no CustId. */
            return false;
        }

        tBoxFirstName.Text = custRecord.FirstName;
        tBoxLastName.Text = custRecord.LastName;
        tBoxAddress1.Text = custRecord.Address1;
        tBoxAddress2.Text = custRecord.Address2;
        tBoxCity.Text = custRecord.City;
        ddlState.SelectedValue = custRecord.State;
        tBoxZip.Text = custRecord.Zip;
        tBoxHomePhone.Text = custRecord.HomePhone;
        tBoxCellPhone.Text = custRecord.CellPhone;
        tBoxBusinessPhone.Text = custRecord.BusinessPhone;
        tBoxBusinessPhoneExt.Text = custRecord.BusinessPhoneExt;
        tBoxFax.Text = custRecord.Fax;
        tBoxAltContact.Text = custRecord.AltContactName;
        tBoxAltPhone.Text = custRecord.AltContactPhone;
        tBoxEmail.Text = custRecord.Email;
        tBoxReferredBy.Text = custRecord.ReferredBy;
        return true;
    }

    private void UpdatePageMode(string mode)
    {
        /* Buttons and Labels */
        switch (mode)
        {
            case CfsCommon.MODE_READONLY:
            {
                headerBookAJob.InnerText = "View a Job";
                btnAddEdit.Text = "Edit";
                break;
            }
            case CfsCommon.MODE_UPDATE:
            {
                headerBookAJob.InnerText = "Edit a Job";
                btnAddEdit.Text = "Update";
                break;
            }
            case CfsCommon.MODE_ADD:
            {
                headerBookAJob.InnerText = "Book a Job";
                btnAddEdit.Text = "Add";
                break;
            }
            default:
            {
                return; //bad mode
            }
        }

        /* Visible & Enable/Disable Controls */
        if (mode == CfsCommon.MODE_UPDATE || mode == CfsCommon.MODE_ADD)
        {
            /* Enables all Textboxes, checkboxes, and dropdowns */
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsEditable(c);
            }
        }
        else //Mode is READONLY
        {
            /* Disables all Textboxes, checkboxes, and dropdowns */
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsNonEditable(c);
            }
        }

        /* Persist */
        hiddenPageMode.Value = mode;
    }

    private bool IsFormValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";

        if (tBoxFirstName.Text == "" &&
            tBoxLastName.Text == "" &&
            tBoxAddress1.Text == "" &&
            tBoxAddress2.Text == "" &&
            tBoxCity.Text == "" &&
            ddlState.SelectedValue == "" &&
            tBoxZip.Text == "" &&
            tBoxHomePhone.Text == "" &&
            tBoxCellPhone.Text == "" &&
            tBoxBusinessPhone.Text == "" &&
            tBoxBusinessPhoneExt.Text == "" &&
            tBoxFax.Text == "" &&
            tBoxAltContact.Text == "" &&
            tBoxAltPhone.Text == "" &&
            tBoxEmail.Text == "" &&
            tBoxReferredBy.Text == "")
        {
            isValid = false;
            ulErrorMsg.InnerHtml += "<li>You must fill in at least one field.</li>";
        }

        if (tBoxEmail.Text != "" && !CfsCommon.ValidateTextBoxEmail(tBoxEmail, "Email", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxHomePhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxHomePhone, "Home Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxCellPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxCellPhone, "Cell Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxBusinessPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxBusinessPhone, "Business Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxFax.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxFax, "Fax", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxAltPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxAltPhone, "Alt Contact Phone", ulErrorMsg))
        {
            isValid = false;
        }



        divErrorMsg.Visible = !isValid;
        return isValid;
    }
}
