﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects.DataClasses;
using System.Data.Objects;

using CfsNamespace;

public partial class backend_add_edit_bookjob_event : System.Web.UI.Page
{
    const string GUEST_TYPE_MALE_STRING = "MALE";
    const string GUEST_TYPE_FEMALE_STRING = "FEMALE";
    const string GUEST_TYPE_MIXED_STRING = "BOTH";

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);
        
        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlLocState);
            CfsCommon.GetCountryList(ddlLocCountry, false);

            /* On ADDING a new job: We are passed the customer ID from the page: add_edit_bookjob_customer.aspx
             * This allows us to associate the new Event Record we are adding w/ the Customer record.
             */
            if (Request.Params[CfsCommon.PARAM_CUSTOMER_ID] != null)
            {
                string custId = (string)Request.Params[CfsCommon.PARAM_CUSTOMER_ID];
                int eventId;

                if (!string.IsNullOrEmpty(custId))
                {
                    hiddenCustId.Value = custId;

                    if (GetEventFromCustId(custId, out eventId))
                    {
                        hiddenEventId.Value = eventId.ToString();

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
            /* On VIEW/UPDATE a job: We are passed the event ID from the calling page, so that
             * we can load and modify the record.
             */
            else if (Request.Params[CfsCommon.PARAM_EVENT_ID] != null)
            {
                string eventIdStr = (string)Request.Params[CfsCommon.PARAM_EVENT_ID];

                if (GetEventFromEventId(eventIdStr))
                {
                    hiddenEventId.Value = eventIdStr;

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
            else
            {
                /* We MUST have either an EventId or CustomerId, for user to use this page 
                 * If not, redirect them to the beginning of Adding a new job.
                 */
                Response.Redirect("add_edit_bookjob_customer.aspx");
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
                    int eventId = AddOrUpdateEvent(hiddenCustId.Value, hiddenEventId.Value);

                    if (eventId != CfsCommon.ERROR_VAL)
                    {
                        string redirUrl = "add_edit_bookjob_office.aspx?" + CfsCommon.PARAM_EVENT_ID + "=" + eventId.ToString();
                        Response.Redirect(redirUrl, true);
                    }
                }
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                UpdatePageMode(CfsCommon.MODE_UPDATE);
                break;
            }
        }
    }
    #endregion

    #region DB Read-Only Functions
    private bool GetEventFromEventId(string eventId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Event eventRec;

        if ((eventRec = CfsCommon.GetEventRecord(cfsEntity, eventId)) == null)
        {
            return false;
        }

        LoadEventInfo(eventRec);
        return true; 
    }

    private bool GetEventFromCustId(string custId, out int eventId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Customer custRecord;
        eventId = CfsCommon.ERROR_VAL;

        if ((custRecord = CfsCommon.GetCustomerRecord(cfsEntity, custId)) == null)
        {
            /* This should never happen, but if it does, it is a BIG problem.*/
            //TO DO - What to do???
            return false;
        }

        if (custRecord.Event != null)
        {
            if (!custRecord.Event.IsLoaded)
            {
                /* Load the records, from Entity Framework */
                custRecord.Event.Load();
            }

            List<Event> eventList = custRecord.Event.ToList();

            if (eventList.Count == 1)
            {
                /* Event Record associated w/ Customer exists */
                LoadEventInfo(eventList[0]);
                eventId = eventList[0].EventId;
                return true; 
            }
        }

        /* There is no Event Record associated w/ the current customer Record */
        return false; 
    }

    private void LoadEventInfo(Event eventInfo)
    {
        tBoxContactPerson.Text = eventInfo.ContactPerson;
        tBoxContactPhone.Text = eventInfo.ContactPhone;
        tBoxGuestHonor.Text = eventInfo.GuestOfHonor;
        tBoxLocName.Text = eventInfo.LocationName;
        tBoxLocAddress1.Text = eventInfo.LocationAddress1;
        tBoxLocAddress2.Text = eventInfo.LocationAddress2;
        tBoxLocCity.Text = eventInfo.LocationCity;
        ddlLocState.SelectedValue = eventInfo.LocationState;
        ddlLocCountry.SelectedValue = eventInfo.LocationCountry;
        tBoxLocZip.Text = eventInfo.LocationZip;
        tBoxLocPhone.Text = eventInfo.LocationPhone;

        tBoxEventDate.Text = eventInfo.EventDate.ToString("MM/dd/yyyy");

        if (eventInfo.NumGuests != null)
        {
            tBoxNumGuests.Text = eventInfo.NumGuests.ToString();
        }
        
        tBoxAgeRange.Text = eventInfo.AgeRange;
        tBoxStartTime.Text = CfsCommon.FormatTime(eventInfo.StartTime); /* FormatTime checks null */
        tBoxEndTime.Text = CfsCommon.FormatTime(eventInfo.EndTime); /* FormatTime checks null */
        tBoxEventType.Text = eventInfo.EventType;

        if (eventInfo.HasPrivateRoom != null)
        {
            chkPrivateRoom.Checked = (bool)eventInfo.HasPrivateRoom;
        }

        if (eventInfo.HasOwnerPermission != null)
        {
            chkOwnerPermission.Checked = (bool)eventInfo.HasOwnerPermission;
        }

        if (eventInfo.IsSurpriseParty != null)
        {
            chkSurpriseParty.Checked = (bool)eventInfo.IsSurpriseParty;
        }
        if (eventInfo.GuestType != null)
        {
            switch (eventInfo.GuestType)
            {
                case GUEST_TYPE_MALE_STRING: rdoMale.Checked = true; break;
                case GUEST_TYPE_FEMALE_STRING: rdoFemale.Checked = true; break;
                case GUEST_TYPE_MIXED_STRING: rdoMixed.Checked = true; break;
                default: break;
            }
        }
    }
    #endregion

    #region DB Add/Update Functions
    /*
     * AddOrUpdateEvent()
     * Adds an Event Record if eventId is null/emptyString
     * Updated an Event Record if eventId is a valid ID to an event record
     */
    private int AddOrUpdateEvent(string custId, string eventId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Event newEvent;
        DateTime dt, dt2;
        short tmpShort;
        
        if ( string.IsNullOrEmpty(eventId)) 
        {
            /* Create new Event */
            newEvent = new Event();

            /* GET customer reference for a new record */
            Customer customer;

            if ((customer = CfsCommon.GetCustomerRecord(cfsEntity, custId)) == null)
            {
                /* Not expected to happen, unless we lose the customer reference. */
                return CfsCommon.ERROR_VAL;
            }

            newEvent.CustomerReference.Value = customer;
        }
        else
        {
            if ((newEvent = CfsCommon.GetEventRecord(cfsEntity, eventId)) == null)
            {
                /* This is not expected to ever happen */
                return CfsCommon.ERROR_VAL;
            }
        }
        
        //String fields:
        newEvent.ContactPerson = tBoxContactPerson.Text;
        newEvent.ContactPhone = tBoxContactPhone.Text;
        newEvent.GuestOfHonor = tBoxGuestHonor.Text;
        newEvent.LocationName = tBoxLocName.Text;
        newEvent.LocationAddress1 = tBoxLocAddress1.Text;
        newEvent.LocationAddress2 = tBoxLocAddress2.Text;
        newEvent.LocationCity = tBoxLocCity.Text;
        newEvent.LocationState = ddlLocState.SelectedValue;
        newEvent.LocationCountry = ddlLocCountry.SelectedValue;
        newEvent.LocationZip = tBoxLocZip.Text;
        newEvent.LocationPhone = tBoxLocPhone.Text;
        newEvent.AgeRange = tBoxAgeRange.Text;
        newEvent.EventType = tBoxEventType.Text;

        //CheckBoxes:
        newEvent.HasPrivateRoom = chkPrivateRoom.Checked;
        newEvent.HasOwnerPermission = chkOwnerPermission.Checked;
        newEvent.IsSurpriseParty = chkSurpriseParty.Checked;

        //Radio Boxes
        if (rdoMale.Checked) { newEvent.GuestType = GUEST_TYPE_MALE_STRING; }
        else if (rdoFemale.Checked) { newEvent.GuestType = GUEST_TYPE_FEMALE_STRING; }
        else if (rdoMixed.Checked) { newEvent.GuestType = GUEST_TYPE_MIXED_STRING; }

        //DateTime conversion fields:
        if (DateTime.TryParse(tBoxEventDate.Text, out dt))
        {
            newEvent.EventDate = dt;
        }
        else
        {
            //This should never happen. We NEED the Event Date!!
            newEvent.EventDate = DateTime.Parse("1900-01-01 12:00 AM");
        }

        if (DateTime.TryParse(tBoxStartTime.Text, out dt) && DateTime.TryParse("1900-01-01 " + tBoxStartTime.Text, out dt2))
        {
            newEvent.StartTime = dt2;
        }
        else
        {
            newEvent.StartTime = null;
        }

        if (DateTime.TryParse(tBoxEndTime.Text, out dt) && DateTime.TryParse("1900-01-01 " + tBoxEndTime.Text, out dt2))
        {
            newEvent.EndTime = dt;
        }
        else
        {
            newEvent.EndTime = null;
        }

        //Short Int conversion field:
        if (short.TryParse(tBoxNumGuests.Text, out tmpShort))
        {
            newEvent.NumGuests = tmpShort;
        }
        else
        {
            newEvent.NumGuests = null;
        }

        if ( string.IsNullOrEmpty(eventId) )
        {
            /* Add new Event Record */
            cfsEntity.AddToEvent(newEvent);
        }
        //else, just update current record:

        if (cfsEntity.SaveChanges() <= 0) 
        {
            return CfsCommon.ERROR_VAL;
        }

        UpdateTalentToJobStartTime(newEvent);

        cfsEntity.SaveChanges();
        
        return newEvent.EventId;
    }

    private void UpdateTalentToJobStartTime(Event eventRec)
    {
        if (eventRec.Job != null)
        {
            if (!eventRec.Job.IsLoaded)
            {
                eventRec.Job.Load();
            }

            if (eventRec.Job.Count == 1)
            {
                Job theJob = eventRec.Job.ToList()[0];

                if (!theJob.TalentToJob.IsLoaded)
                {
                    theJob.TalentToJob.Load();
                }

                foreach (TalentToJob talToJob in theJob.TalentToJob)
                {
                    talToJob.StartDateTime = CfsCommon.CreateTalentToJobStartDateTime(eventRec, talToJob.StartDateTime.ToString("hh:mm tt"));
                }
            }
        }
    }
    #endregion


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

            calendarEventDate.Enabled = true;
        }
        else //Mode is READONLY
        {
            /* Disables all Textboxes, checkboxes, and dropdowns */
            foreach (Control c in Page.Controls)
            {
                CfsCommon.MakeControlsNonEditable(c);
            }

            calendarEventDate.Enabled = false;
        }

        /* Persist */
        hiddenPageMode.Value = mode;
    }

    private bool IsFormValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";

        /* Event Date is Required */
        if (!CfsCommon.ValidateTextBoxReq(tBoxEventDate, "Date of Event", ulErrorMsg))
        {
            isValid = false;
        }
        else if (!CfsCommon.ValidateTextBoxDate(tBoxEventDate, "Date of Event", ulErrorMsg))
        {
            isValid = false;
        }

      /*  if (tBoxStartTime.Text.Trim() != String.Empty)
        {
            if (!CfsCommon.ValidateTextBoxTime(tBoxStartTime, "Start Time", ulErrorMsg))
            {
                isValid = false;
            }
        }

        if (tBoxEndTime.Text != "" && !CfsCommon.ValidateTextBoxTime(tBoxEndTime, "End Time", ulErrorMsg))
        {
            isValid = false;
        }
        */
        if (tBoxNumGuests.Text != "" && !CfsCommon.ValidateTextBoxInt(tBoxNumGuests, "Number of Guests", ulErrorMsg))
        {
            isValid = false;
        }

        if (tBoxContactPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxContactPhone, "Contact Phone", ulErrorMsg))
        {
            isValid = false;
        }
        if (tBoxLocPhone.Text != "" && !CfsCommon.ValidateTextBoxPhone(tBoxLocPhone, "Location Phone", ulErrorMsg))
        {
            isValid = false;
        }

        divErrorMsg.Visible = !isValid;
        return isValid;
    }
}