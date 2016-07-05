﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;


using CfsNamespace;

public partial class backend_add_edit_pending : System.Web.UI.Page
{
    const string GUEST_TYPE_MALE_STRING = "MALE";
    const string GUEST_TYPE_FEMALE_STRING = "FEMALE";
    const string GUEST_TYPE_MIXED_STRING = "BOTH";

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.PendingJobs, Session, Response);

        if (!IsPostBack)
        {
            if (Request.Params["pendid"] != null)
            {
                if (GetPendingJobInfo(Request.Params["pendid"].ToString()))
                {
                    UpdatePageMode(CfsCommon.MODE_READONLY);
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
                if (FormIsValid())
                {
                    if (AddOrEditPending(hiddenPendId.Value))
                    {
                        Response.Redirect("view_pending_jobs.aspx");
                    }
                }
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                UpdatePageMode(CfsCommon.MODE_UPDATE);  //Button Changes mode to Update
                break;
            }
            default:
            {
                //should never happen
                return;
            }
        }
    }
    #endregion

    #region DB Add or Update
    private bool AddOrEditPending(string pendId)
    {
        CfsEntity cfEntity = new CfsEntity();
        Pending newPend; //new Pending Record OR Update Pending Record
        DateTime dt;
        int tempInt;

        if ( string.IsNullOrEmpty(pendId) )
        {
            newPend = new Pending();
        }
        else
        {
            ObjectQuery<Pending> pendQuery = cfEntity.Pending.Where("it.PendId = " + pendId);
            List<Pending> pendList = pendQuery.ToList();

            //There can be only ONE... record
            if (pendList.Count == 1)
            {
                newPend = pendList[0];
            }
            else
            {
                //Should never happen, but just in case...
                return false;
            }
        }

        //Fill in the data for the Add/Update
        
        //String fields:
        newPend.ClientName = tBoxClientName.Text;
        newPend.ReferredBy = tBoxReferredBy.Text;
        newPend.CityStateZip = tBoxCityStateZip.Text;
        newPend.EventType = ddlPartyType.SelectedValue;
        newPend.EntertainType = ddlEntertainerType.SelectedValue;
        newPend.Budget = tBoxBudget.Text;
        newPend.QuotedPrice = tBoxQuotedPrice.Text;
        newPend.ContactNumber = tBoxContactNumber.Text;
        newPend.EmailAddress = tBoxEmailAddress.Text;
        newPend.Notes = tBoxNotes.Text;

        //Radio Buttons:
        if (rdoMale.Checked) { newPend.GuestType = GUEST_TYPE_MALE_STRING; }
        else if (rdoFemale.Checked) { newPend.GuestType = GUEST_TYPE_FEMALE_STRING; }
        else if (rdoMixed.Checked) { newPend.GuestType = GUEST_TYPE_MIXED_STRING; }

        if (chkbx_Highlight_Pending.Checked)
        {
            newPend.Highlight = 1;
        }
        else
        {
            newPend.Highlight = 0;
        }

        //Date Conversion fields:
        if (DateTime.TryParse(tBoxEventDate.Text, out dt))
        {
            newPend.EventDate = dt;
        }
        else
        {
            newPend.EventDate = DateTime.Parse("1900-01-01 12:00 AM");
        }

        if (DateTime.TryParse( tBoxTimeRequested.Text, out dt)) //TO DO - make sure this works properly
        {
            newPend.TimeRequested = dt;
        }
        else
        {
            newPend.TimeRequested = null;
        }
        
        //Integer Conversion Fields:
        if (int.TryParse(tBoxNumGuests.Text, out tempInt))
        {
            newPend.NumOfGuests = tempInt;
        }
        else
        {
            newPend.NumOfGuests = null;
        }
        
        if (int.TryParse(tBoxNumEntertainers.Text, out tempInt))
        {
            newPend.NumEntertainers = tempInt;
        }
        else
        {
            newPend.NumEntertainers = null;
        }

        if ( string.IsNullOrEmpty(pendId)) 
        {
            /* Create New Pending record */
            cfEntity.AddToPending(newPend);
        }
        // else: Code will just update Current Pending Record

        if (cfEntity.SaveChanges() <= 0) 
        {
            return false;
        }

        return true;
    }
    #endregion

    #region DB Read Only
    private bool GetPendingJobInfo(string pendid)
    {
        CfsEntity cfEntity = new CfsEntity();
        ObjectQuery<Pending> pendQuery = cfEntity.Pending.Where("it.PendId = " + pendid);
        List<Pending> pendList = pendQuery.ToList();
        Pending curPend;
        
        if( pendList.Count != 1 )
        {
            hiddenPendId.Value = "";
            return false;
        }

        curPend = pendList[0];

        tBoxClientName.Text = curPend.ClientName;
        tBoxReferredBy.Text = curPend.ReferredBy;
        tBoxCityStateZip.Text = curPend.CityStateZip;

        if (curPend.EventDate != null)
        {
            tBoxEventDate.Text = ((DateTime)curPend.EventDate).ToString("MM/dd/yyyy");
        }

        if (curPend.Highlight == null)
        {
            chkbx_Highlight_Pending.Checked = false;
        }
        if (curPend.Highlight != null)
        {
            if (curPend.Highlight == 0)
            {
                chkbx_Highlight_Pending.Checked = false;
            }
            else
            {
                chkbx_Highlight_Pending.Checked = true;
            }
        }


        ddlPartyType.SelectedValue = curPend.EventType;
        ddlEntertainerType.SelectedValue = curPend.EntertainType;

        switch (curPend.GuestType)
        {
            case GUEST_TYPE_MALE_STRING:   rdoMale.Checked = true; break;
            case GUEST_TYPE_FEMALE_STRING: rdoFemale.Checked = true; break;
            case GUEST_TYPE_MIXED_STRING:  rdoMixed.Checked = true; break;
            default: break;
        }

        if (curPend.NumOfGuests != null)
        {
            tBoxNumGuests.Text = curPend.NumOfGuests.ToString();
        }

        if( curPend.NumEntertainers != null )
        {
            tBoxNumEntertainers.Text = curPend.NumEntertainers.ToString();
        }

        tBoxBudget.Text = curPend.Budget;
        
        if (curPend.TimeRequested != null)
        {
            tBoxTimeRequested.Text = ((DateTime)curPend.TimeRequested).ToString("t");
        }

        tBoxQuotedPrice.Text = curPend.QuotedPrice;
        tBoxContactNumber.Text = curPend.ContactNumber;
        tBoxEmailAddress.Text = curPend.EmailAddress;
        tBoxNotes.Text = curPend.Notes;

        hiddenPendId.Value = curPend.PendId.ToString();

        if (tBoxEmailAddress.Text != "")
        {
            hlEmailSend.NavigateUrl = "mailto:" + tBoxEmailAddress.Text;
            hlEmailSend.Visible = true;
        }

        return true;
    }
    #endregion

    private void UpdatePageMode(string mode)
    {
        /* Buttons and Labels */
        switch (mode)
        {
            case CfsCommon.MODE_ADD:
            {
                headerAddEditTitle.InnerText = "Add Pending Job";
                btnAddEdit.Text = "ADD";
                break;
            }
            case CfsCommon.MODE_UPDATE:
            {
                headerAddEditTitle.InnerText = "Update Pending Job";
                btnAddEdit.Text = "UPDATE";
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                headerAddEditTitle.InnerText = "View Pending Job";
                btnAddEdit.Text = "EDIT";
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

            calendarDateOfEvent.Enabled = true;
        }
        else //Mode is READONLY
        {
            //Disables all Textboxes, checkboxes, and dropdowns
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsNonEditable(c);
            }

            calendarDateOfEvent.Enabled = false;
        }

        /* Persist */
        hiddenPageMode.Value = mode;
    }

    private bool FormIsValid()
    {
        bool formValid = true;
        ulErrorMsg.InnerHtml = "";

        /* Date of Event */
        if ( !CfsCommon.ValidateTextBoxReq(tBoxEventDate, "Date of Event", ulErrorMsg) )
        {
            formValid = false;
        }
        else if(!CfsCommon.ValidateTextBoxDate(tBoxEventDate, "Date of Event", ulErrorMsg))
        {
            formValid = false;
        }

        /* Email Address */
        if (tBoxEmailAddress.Text != "" && !CfsCommon.ValidateTextBoxEmail(tBoxEmailAddress, "Email Address", ulErrorMsg))
        {
            formValid = false;
        }
        
        /* Num of Guests */
        if (tBoxNumGuests.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxNumGuests, "# of Guests", ulErrorMsg))
        {
            formValid = false;
        }

        /* Num of Entertainers */
        if (tBoxNumEntertainers.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxNumEntertainers, "# of Entertainers", ulErrorMsg))
        {
            formValid = false;
        }

        /* Phone Num */
        if (tBoxContactNumber.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxContactNumber, "Contact Number", ulErrorMsg))
        {
            formValid = false;
        }

        /* Time */
        if (tBoxTimeRequested.Text != "" && !CfsCommon.ValidateTextBoxTime(tBoxTimeRequested, "Time Requested", ulErrorMsg))
        {
            formValid = false;
        }

        divErrorMsg.Visible = !formValid;

        return formValid;
    }
}