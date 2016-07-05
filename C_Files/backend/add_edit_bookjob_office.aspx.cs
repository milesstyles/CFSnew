﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;

using System.Collections;

using CfsNamespace;

public partial class backend_add_edit_bookjob_office : System.Web.UI.Page
{
    private const int NUM_MINUTES_CHECK_DBL_BOOK = 30;
    
    protected const string TEMP_TALENT_SESSION_ID = "talTable";
    protected const string TEMP_TABLE_COLUMN_TALTOJOB_UID = "TalToJobId";
    protected const string TEMP_TABLE_COLUMN_TALENT_ID = "TalentId";
    protected const string TEMP_TABLE_COLUMN_NAME = "Name";
    protected const string TEMP_TABLE_COLUMN_START_TIME = "StartTime";
    protected const string TEMP_TABLE_COLUMN_SHOW_LENGTH = "ShowLength";
    protected const string TEMP_TABLE_COLUMN_PAYROLL = "Payroll";

    private const string TEMP_LIST_REMOVE_TAL_JOB_REC = "removeList";

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime eventDate;

        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);
        
        if (!IsPostBack)
        {
            /* Clear any Temp Talent table that may be present */
            Session[TEMP_TALENT_SESSION_ID] = null;
            Session[TEMP_LIST_REMOVE_TAL_JOB_REC] = null;
            divErrorMsg.Visible = false;
            
            /* Load Currently Logged-In User by default for new Record */
            if (Session[CfsCommon.SESSION_KEY_USERNAME] != null)
            {
                lblCreatedBy.Text = (string)Session[CfsCommon.SESSION_KEY_USERNAME];
            }
            
            CfsCommon.GetFullTalentList(ddlTalentList);
            CfsCommon.GetFullTalentList(ddlTalentListBalanceResp);

            /* On ADDING a new job: We are passed the event ID from the page: add_edit_bookjob_event.aspx
             * This allows us to associate the new Job Record we are adding w/ the Event record.
             */
            if (Request.Params[CfsCommon.PARAM_EVENT_ID] != null)
            {
                hiddenEventId.Value = (string)Request.Params[CfsCommon.PARAM_EVENT_ID];
                string jobId;

                if (GetJobFromEventId(hiddenEventId.Value, out jobId, out eventDate))
                {
                    hiddenJobId.Value = jobId;
                    LoadTempTalentTable();

                    if (Request.Params[CfsCommon.PARAM_UPDATE_MODE] != null)
                    {
                        UpdatePageMode(CfsCommon.MODE_UPDATE);
                    }
                    else
                    {
                        UpdatePageMode(CfsCommon.MODE_READONLY);
                    }
                }

                hiddenEventDate.Value = eventDate.ToString("yyyy-MM-dd");
            }
            /* On VIEW/UPDATE a job: We are passed the Job ID from the calling page, so that
             * we can load and modify the record.
             */
            else if (Request.Params[CfsCommon.PARAM_JOB_ID] != null)
            {
                string jobId = (string)Request.Params[CfsCommon.PARAM_JOB_ID];
                string eventId;

                if (GetJobFromJobId(jobId, out eventId, out eventDate))
                {
                    hiddenJobId.Value = jobId;
                    hiddenEventId.Value = eventId;
                    hiddenEventDate.Value = eventDate.ToString("yyyy-MM-dd");
                    LoadTempTalentTable();

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
                /* We MUST have either an EventId or JobId, for user to use this page 
                 * If not, redirect them to the beginning of Adding a new job.
                 */
                Response.Redirect("add_edit_bookjob_customer.aspx", true);
            }

            UpdateComputedFields();
            string checkflag = Request.QueryString["flagchecks"];
            if (checkflag != null)
            {
                List<CheckBox> chkList = new List<CheckBox>();
                List<string> chkListName = new List<string>();

                /* All of these boxes must be checked, before work order is completed */
                chkList.Add(chkCancelPolicy); chkListName.Add("Cancellation Policy");
                chkList.Add(chkNeedMusic); chkListName.Add("Need for Music");
                chkList.Add(chkTippingPolicy); chkListName.Add("Tipping Policy");
                chkList.Add(chkDirections); chkListName.Add("Directions");
                chkList.Add(chkAbusePolicy); chkListName.Add("Abuse Policy");
                chkList.Add(chkNoPictures); chkListName.Add("No Picture Taking");
                chkList.Add(chkEscortPolicy); chkListName.Add("Escort Service Policy");
                chkList.Add(chkOver18Policy); chkListName.Add("Over 18");
                chkList.Add(chkPersonalItems); chkListName.Add("Personal Items");
                chkList.Add(chkCallOffice); chkListName.Add("Call the Office");
                chkList.Add(chkPopoutCake); chkListName.Add("Popout Cake");
                chkList.Add(chkCashCannon); chkListName.Add("Cash Cannon");

                chkList.Add(chkChangeFeeNotice); chkListName.Add("Read Change Fee notice");
                chkList.Add(chkArrival); chkListName.Add("Arrival");
                for (int i = 0; i < chkList.Count; i++)
                {
                    if (!chkList[i].Checked)
                    {
                        chkList[i].CssClass = "";
                        chkList[i].Checked = true;
                    }
                    else
                    {
                        chkList[i].CssClass = "";
                    }
                }
            }
        }
        if (!IsPostBack)
        {
            protectCCinfo();
        }
    }
    protected void protectCCinfo()
    {
    bool isauth = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.CreditCard, Session);
    if (!isauth)
    {
        tBoxCCNameOnCard.Text = "";
        tBoxCCCv2Num.Text = "";
        tBoxCCExpire.Text = "";
        tBoxCCNum.Text = "";
    
    }
    }
    protected void OnClick_btnAddTalent(object sender, EventArgs e)
    {
        if (IsTalentInfoValid())
        {
            DateTime dtTime = DateTime.Parse(hiddenEventDate.Value + " " + tBoxStartTime.Text);
            DateTime dblBookTime;
            int showLength = int.Parse(ddlShowLength.SelectedValue);

            CfsEntity cfsEntity = new CfsEntity();
            Talent talent = CfsCommon.GetTalentRecord(cfsEntity, ddlTalentList.SelectedValue);

            if (talent.TalentType == CfsCommon.TALENT_TYPE_ID_AFFILIATE || !IsDoubleBooking(hiddenJobId.Value, ddlTalentList.SelectedValue, dtTime, showLength, out dblBookTime))
            {
                AddTalentToTempTable();
                ddlTalentList.SelectedItem.Enabled = false;

                LoadTempTalentTable();
                UpdateComputedFields();
            }
            else
            {
                pBookMsg.InnerText = ddlTalentList.SelectedItem.Text;
                pBookMsg.InnerText += " is already booked on " + dblBookTime.ToString("MM/dd/yyyy @ h:mm tt");

                divDblBookConfirm.Visible = true;
            }
        }

        uPnlAccting.Update();
        uPnlTalentList.Update();
        uPnlErrors.Update();
    }

    protected void OnClick_btnDeleteTalent(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (string.IsNullOrEmpty(btn.CommandArgument))
        {
            /* This is not expected to happen */
            return;
        }

        string[] ids = btn.CommandArgument.Split(',');
        string talToJobUid = "";
        string talentId = "";

        if (ids.Length == 1)
        {
            talentId = ids[0];
        }
        else if (ids.Length == 2)
        {
            talentId = ids[0];
            talToJobUid = ids[1];
        }

        if (RemoveTalentFromTempTable(talentId, talToJobUid, false))
        {
            MarkTalentToJobForDeletion(talToJobUid);
        }

        LoadTempTalentTable();

        /* Re-enable the List Item, so user can re-add Talent */
        ListItem item = ddlTalentList.Items.FindByValue(talentId);

        if (item != null)
        {
            item.Enabled = true;
        }

        UpdateComputedFields();

        uPnlAccting.Update();
        uPnlTalentList.Update();
        uPnlErrors.Update();
    }

    protected void OnClick_btnEditTalent(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (string.IsNullOrEmpty(btn.CommandArgument))
        {
            /* This is not expected to happen */
            return;
        }

        string[] ids = btn.CommandArgument.Split(',');
        string talToJobUid = "";
        string talentId = "";

        if (ids.Length == 1)
        {
            talentId = ids[0];
        }
        else if (ids.Length == 2)
        {
            talentId = ids[0];
            talToJobUid = ids[1];
        }

        /* Re-enable the List Item, so user can re-add Talent */
        ListItem item = ddlTalentList.Items.FindByValue(talentId);

        if (item != null)
        {
            item.Enabled = true;
        }
        
        /* Remove from Temp table, fill in boxes on bottom for re-add (Edit) */
        if (RemoveTalentFromTempTable(talentId, talToJobUid, true))
        {
            MarkTalentToJobForDeletion(talToJobUid);
        }

        LoadTempTalentTable();
        UpdateComputedFields();

        uPnlAccting.Update();
        uPnlTalentList.Update();
        uPnlErrors.Update();
    }

    protected void OnClick_btnAddEditJob(object sender, EventArgs e)
    {
        switch (hiddenPageMode.Value)
        {
            case CfsCommon.MODE_ADD:
            case CfsCommon.MODE_UPDATE:
            {
                if (IsFormValid())
                {
                    int jobId = AddOrUpdateJob(hiddenEventId.Value, hiddenJobId.Value);

                    if (jobId != CfsCommon.ERROR_VAL)
                    {
                        Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + jobId.ToString(), true);
                    }
                }
                else
                {
                    uPnlErrors.Update();
                }
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                /* Button Changes page mode from Read-Only to Editable */
                UpdatePageMode(CfsCommon.MODE_UPDATE); 
                break;
            }
        }
    }
    #endregion

    #region Temp Talent Table Functions
    private void LoadTempTalentTable()
    {
        DataTable talTable;

        if (Session[TEMP_TALENT_SESSION_ID] != null)
        {
            talTable = (DataTable)Session[TEMP_TALENT_SESSION_ID];
            rptrTalentList.DataSource = talTable;
            rptrTalentList.DataBind();
        }
    }

    private DataTable CreateTempTalentTable()
    {
        DataTable talTable = new DataTable();

        talTable.Columns.Add(TEMP_TABLE_COLUMN_TALTOJOB_UID);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_TALENT_ID);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_NAME);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_START_TIME);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_SHOW_LENGTH);
        talTable.Columns.Add(TEMP_TABLE_COLUMN_PAYROLL);

        return talTable;
    }

    private void AddTalentToTempTable()
    {
        DataTable talTable;

        if (Session[TEMP_TALENT_SESSION_ID] == null)
        {
            talTable = CreateTempTalentTable();
        }
        else
        {
            talTable = (DataTable)Session[TEMP_TALENT_SESSION_ID];
        }

        string[] newData = new string[6];

        newData[0] = ""; /* New record, no UID from DB */
        newData[1] = ddlTalentList.SelectedValue;
        newData[2] = ddlTalentList.SelectedItem.Text;
        newData[3] = tBoxStartTime.Text;
        newData[4] = ddlShowLength.SelectedValue;
        newData[5] = tBoxPayroll.Text;

        talTable.LoadDataRow(newData, true);

        Session[TEMP_TALENT_SESSION_ID] = talTable;
    }

    private bool RemoveTalentFromTempTable(string talentId, string talentToJobUid, bool setAddToVals)
    {
        bool found = false;

        if (Session[TEMP_TALENT_SESSION_ID] == null)
        {
            /* Error: Not expected to happen */
            return false;
        }
        else if (string.IsNullOrEmpty(talentId))
        {
            /* Error: Also not expected to happen */
            return false;
        }

        DataTable talTable = (DataTable)Session[TEMP_TALENT_SESSION_ID];
        string talIdCmpr, uidCmpr;

        for (int i = 0; i < talTable.Rows.Count; i++)
        {
            talIdCmpr = (string)talTable.Rows[i][TEMP_TABLE_COLUMN_TALENT_ID];
            uidCmpr = (string)talTable.Rows[i][TEMP_TABLE_COLUMN_TALTOJOB_UID];

            if (talIdCmpr == talentId && uidCmpr == talentToJobUid)
            {
                if (setAddToVals)
                {
                    ddlTalentList.SelectedValue = talentId;
                    tBoxStartTime.Text = (string)talTable.Rows[i][TEMP_TABLE_COLUMN_START_TIME];
                    tBoxPayroll.Text = (string)talTable.Rows[i][TEMP_TABLE_COLUMN_PAYROLL];

                    if ((string)talTable.Rows[i][TEMP_TABLE_COLUMN_SHOW_LENGTH] != "0")
                    {
                        ddlShowLength.SelectedValue = (string)talTable.Rows[i][TEMP_TABLE_COLUMN_SHOW_LENGTH];
                    }
                }
                
                talTable.Rows.RemoveAt(i);
                found = true;
                break;
            }
        }

        /* Persist */
        Session[TEMP_TALENT_SESSION_ID] = talTable;

        return found;
    }

    private void MarkTalentToJobForDeletion(string talToJobUID)
    {
        ArrayList deleteList;

        if (Session[TEMP_LIST_REMOVE_TAL_JOB_REC] == null)
        {
            deleteList = new ArrayList();
        }
        else
        {
            deleteList = (ArrayList)Session[TEMP_LIST_REMOVE_TAL_JOB_REC];
        }

        if ( !string.IsNullOrEmpty(talToJobUID))
        {
            deleteList.Add(talToJobUID);
        }

        /* Persist */
        Session[TEMP_LIST_REMOVE_TAL_JOB_REC] = deleteList;
    }
    #endregion

    #region Computation Functions
    private void UpdateComputedFields()
    {
        tBoxDancerPayroll.Text = CalculateDancerPayroll().ToString();
        
        int grossIncome = CalculateIncome();
        int totalExpenses = CalculateExpenses();
        int officeNet = grossIncome - totalExpenses;

        tBoxGrossIncome.Text = grossIncome.ToString();
        tBoxTotalExpenses.Text = totalExpenses.ToString();
        tBoxOfficeNet.Text = officeNet.ToString();

    }

    private int CalculateDancerPayroll()
    {
        int total = 0;
        int tmpInt;
        string tmpStr;

        
        if (Session[TEMP_TALENT_SESSION_ID] == null)
        {
            //No talent on Payroll
            return total;
        }

        DataTable talTable = (DataTable)Session[TEMP_TALENT_SESSION_ID];

        for (int i = 0; i < talTable.Rows.Count; i++)
        {
            tmpStr = (string)talTable.Rows[i]["Payroll"];

            if (int.TryParse(tmpStr, out tmpInt))
            {
                total += tmpInt;
            }
        }

        return total;
    }

    private int CalculateIncome()
    {
        int total=0;
        int tmpInt;

        if (int.TryParse(tBoxEntTotal.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxLimoTotal.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxLocTotal.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxAccessoriesTotal.Text, out tmpInt))
        {
            total += tmpInt;
        }

        return total;
    }

    private int CalculateExpenses()
    {
        int total=0;
        int tmpInt;

        if (int.TryParse(tBoxDancerPayroll.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxGratuity.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxSecPayroll.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxReferCommission.Text, out tmpInt))
        {
            total += tmpInt;
        }

        if (int.TryParse(tBoxSalesCommission.Text, out tmpInt))
        {
            total += tmpInt;
        }

        return total;
    }

    private int CalcTotalShowLengthMins(Job jobRecord)
    {
        if (!jobRecord.TalentToJob.IsLoaded)
        {
            jobRecord.TalentToJob.Load();
        }

        if (jobRecord.TalentToJob.Count == 0)
        {
            return 0;
        }

        List<TalentToJob> talList = jobRecord.TalentToJob.ToList();
        DateTime earliestStartTime = talList[0].StartDateTime;
        DateTime latestEndTime = talList[0].StartDateTime.AddMinutes(talList[0].ShowLengthMins);

        for (int i = 1; i < talList.Count; i++)
        {
            DateTime curTalStartTime = talList[i].StartDateTime;
            DateTime curTalEndTime = talList[i].StartDateTime.AddMinutes(talList[i].ShowLengthMins);
            
            /* If Talent StartDateTime is before the earliestStartTime */
            if( DateTime.Compare(earliestStartTime, curTalStartTime) > 0 )
            {
                earliestStartTime = talList[i].StartDateTime;
            }

            /* If Talent End Date and Time is after the latestEndTime */
            if (DateTime.Compare(latestEndTime, curTalEndTime) < 0)
            {
                latestEndTime = curTalEndTime;
            }
        }

        TimeSpan totalLen = latestEndTime - earliestStartTime;

        return Convert.ToInt32(totalLen.TotalMinutes);
    }
    #endregion Computation Functions

    #region DB Add/Update Functions
    private int AddOrUpdateJob(string eventId, string jobId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Job newJob;
        Event eventRef;
        int tmpInt;

        if ( string.IsNullOrEmpty(jobId) )
        {
            /* Create a new Job */
            newJob = new Job();

            /* Get Event Reference for new Job */
            if ((eventRef = CfsCommon.GetEventRecord(cfsEntity, eventId)) == null)
            {
                //This should never happen, but a real problem if it does.
                //TO DO - What to do?
                return CfsCommon.ERROR_VAL;
            }

            if (Session[CfsCommon.SESSION_KEY_USERID] == null)
            {
                /* MUST have a UserId to Mark Record w/ who Created it. */
                return CfsCommon.ERROR_VAL;
            }

            newJob.PreparedBy = (int)Session[CfsCommon.SESSION_KEY_USERID];
            newJob.EventReference.Value = eventRef;
            newJob.DateTimeCreated = DateTime.Now;
        }
        else
        {
            /* Get Job Record for Update an existing Job */
            if ((newJob = CfsCommon.GetJobRecord(cfsEntity, jobId)) == null )
            {
                //This should never happen either.
                //TO DO - What to do?
                return CfsCommon.ERROR_VAL;
            }
        }

        //Check Boxes (Only 4 Checkboxes are saved to DB Record)
        newJob.IsJobComplete = chkJobComplete.Checked;
        newJob.IsJobCancelled = chkCancelled.Checked;
        newJob.IsBalanceCollected = chkBalanceCollected.Checked;
        newJob.IsChargeNetToCC = chkChargedNetToCC.Checked;

        //Numbers Conversion:
        if (int.TryParse(tBoxBalanceDue.Text, out tmpInt)) { newJob.BalanceDue = tmpInt; } 
        if (int.TryParse(tBoxEntTotal.Text, out tmpInt)) { newJob.ChargeForEntertain = tmpInt; } else {newJob.ChargeForEntertain = 0;}
        if (int.TryParse(tBoxLimoTotal.Text, out tmpInt)){ newJob.ChargeForLimo = tmpInt; } else { newJob.ChargeForLimo = 0; }
        if (int.TryParse(tBoxLocTotal.Text, out tmpInt)) { newJob.ChargeForLocation = tmpInt; } else { newJob.ChargeForLocation = 0; }
        if (int.TryParse(tBoxAccessoriesTotal.Text, out tmpInt)) { newJob.ChargeForAccessories = tmpInt; } else { newJob.ChargeForAccessories = 0; }
        if (int.TryParse(tBoxGratuity.Text, out tmpInt))        { newJob.ExpenseGratuity = tmpInt; } else { newJob.ExpenseGratuity = 0; }
        if (int.TryParse(tBoxSecPayroll.Text, out tmpInt))      { newJob.ExpenseSecurity = tmpInt; } else { newJob.ExpenseSecurity = 0; }
        if (int.TryParse(tBoxReferCommission.Text, out tmpInt)) { newJob.ExpenseReferral = tmpInt; } else { newJob.ExpenseReferral = 0; }
        if (int.TryParse(tBoxSalesCommission.Text, out tmpInt)) { newJob.ExpenseSales = tmpInt; } else { newJob.ExpenseSales = 0; }
        if (int.TryParse(ddlTalentListBalanceResp.SelectedValue, out tmpInt)) { newJob.RespForBalance = tmpInt; } else { newJob.RespForBalance = null; }

        newJob.ExpenseTalent = CalculateDancerPayroll();
        newJob.ExpenseTotal = CalculateExpenses();
        newJob.GrossIncome = CalculateIncome();
        newJob.OfficeNet = newJob.GrossIncome - newJob.ExpenseTotal;

        //Credit Card: 
        newJob.CCIssueBank = tBoxIssueBankName.Text;
        newJob.CCTypeCreditOrDebit = ddlCardTypeCreditOrDebit.SelectedValue;
        newJob.CCTypeBrand = ddlCardBrand.SelectedValue;
        newJob.CCName = tBoxCCNameOnCard.Text;
        newJob.CCNum = CfsCommon.Encrypt( tBoxCCNum.Text, true );
        newJob.CCExp = tBoxCCExpire.Text;
        newJob.CCcv2Num = tBoxCCCv2Num.Text;
        if (chkbxJobHighlight.Checked)
        {
            newJob.Highlight = 1;

        }
        else
        {
            newJob.Highlight = 0;

        }

        if (chkbxJobPaid.Checked)
        {
            newJob.JobPaid = 1;

        }
        else
        {
            newJob.JobPaid = 0;
        }
        
        newJob.SpecialInstructions = tBoxSpecialInstructions.Text;

        if ( string.IsNullOrEmpty(jobId) )
        {
            /* Add new Job Record */
            cfsEntity.AddToJob(newJob);
        }
        //else, just update the record

        if (cfsEntity.SaveChanges() <= 0)
        {
            return CfsCommon.ERROR_VAL;
        }

        /* Add Or Update Job TalentList */
        AddOrUpdateTalentList(cfsEntity, newJob);

        /* Update Show Length, now that Talent StartDateTimes are updated */
        newJob.TotalShowLengthMins = CalcTotalShowLengthMins(newJob);

        cfsEntity.SaveChanges(); /* User might not Add/Remove any TalentsToJob */

        return newJob.JobId;

    }

    private void AddOrUpdateTalentList(CfsEntity cfsEntity, Job jobRecord)
    {
        DataTable talTable;

        /* Remove records marked for Deletion */
        DeleteMarkedTalentRecords(cfsEntity);

        if (Session[TEMP_TALENT_SESSION_ID] == null)
        {
            /* Nothing else to do */
            return;
        }

        /* Add Or Update Remaining Records */
        talTable = (DataTable)Session[TEMP_TALENT_SESSION_ID];

        if (!jobRecord.EventReference.IsLoaded)
        {
            jobRecord.EventReference.Load();
        }

        for (int i = 0; i < talTable.Rows.Count; i++)
        {
            /* Add or Update TalentToJob Records */
            CreateOrUpdateTalentAssocRecord(cfsEntity, talTable.Rows[i], jobRecord.Event, jobRecord, (string)talTable.Rows[i][TEMP_TABLE_COLUMN_TALTOJOB_UID]);
        }
    }

    private bool CreateOrUpdateTalentAssocRecord(CfsEntity cfsEntity, DataRow tRow, Event eventRec, Job jobRecord, string talToJobId)
    {
        TalentToJob talAssoc;

        if ( string.IsNullOrEmpty(talToJobId) )
        {
            /* Empty talToJobId is a flag to Create New Record */
            talAssoc = new TalentToJob();

            /* This data needed on new record only */
            talAssoc.TalentReference.Value = CfsCommon.GetTalentRecord(cfsEntity, (string)tRow[TEMP_TABLE_COLUMN_TALENT_ID]);
            talAssoc.JobReference.Value = jobRecord;
        }
        else
        {
            if(( talAssoc = CfsCommon.GetTalentToJobRecord(cfsEntity, talToJobId )) == null )
            {
                return false;
            }
        }
        
        /* Fill in the data */
        talAssoc.StartDateTime = CfsCommon.CreateTalentToJobStartDateTime(eventRec, (string)tRow[TEMP_TABLE_COLUMN_START_TIME]);
        talAssoc.ShowLengthMins = int.Parse((string)tRow[TEMP_TABLE_COLUMN_SHOW_LENGTH]);
        talAssoc.Payroll = int.Parse((string)tRow[TEMP_TABLE_COLUMN_PAYROLL]);
        talAssoc.IsConfirmSent = false;


        if( string.IsNullOrEmpty( talToJobId ) )
        {
            cfsEntity.AddToTalentToJob(talAssoc);
        }
        
        return true;
    }

    private void DeleteMarkedTalentRecords(CfsEntity cfsEntity)
    {
        if (Session[TEMP_LIST_REMOVE_TAL_JOB_REC] == null)
        {
            /* None marked for deletion, nothing to do */
            return;
        }

        ArrayList deleteList = (ArrayList)Session[TEMP_LIST_REMOVE_TAL_JOB_REC];

        foreach (string uid in deleteList)
        {
            List<TalentToJob> list = ((ObjectQuery<TalentToJob>)cfsEntity.TalentToJob.Where("it.UID = " + uid)).ToList();

            if (list.Count == 1)
            {
                cfsEntity.DeleteObject(list[0]);
            }
        }
    }
    #endregion DB Add/Update Functions

    #region DB Read Only Functions
    private TalentToJob FindTalentAssocRecord(Job jobRecord, DataRow tRow)
    {
        string talentId = tRow[TEMP_TABLE_COLUMN_TALENT_ID].ToString();
        
        foreach (TalentToJob talAssoc in jobRecord.TalentToJob)
        {
            if (talAssoc.Talent.TalentId.ToString() == talentId)
            {
                return talAssoc;
            }
        }
        
        return null;
    }

    private bool GetJobFromEventId(string eventId, out string jobId, out DateTime eventDate)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Event eventRecord;
        jobId = "";
        eventDate = DateTime.Parse("1900-01-01");

        if ((eventRecord = CfsCommon.GetEventRecord(cfsEntity, eventId)) == null)
        {
            //This should never happen, but if it does, it is a BIG problem.
            //TO DO - What to do???
            return false;
        }

        if (eventRecord.Job != null)
        {
            if (!eventRecord.Job.IsLoaded)
            {
                eventRecord.Job.Load();
            }

            List<Job> jobList = eventRecord.Job.ToList();
            eventDate = eventRecord.EventDate;

            if (jobList.Count == 1)
            {
                LoadTalentInfo(jobList[0]);
                LoadJobInfo(cfsEntity, jobList[0]);
                jobId = jobList[0].JobId.ToString();
                return true;
            }
        }

        /* There is no Event Record associated w/ the current customer Record */
        return false; 
    }

    private bool GetJobFromJobId(string jobId, out string eventId, out DateTime eventDate)
    {
        eventId = "";
        CfsEntity cfsEntity = new CfsEntity();
        eventDate = DateTime.Parse("1900-01-01");
        Job jobRec;

        if ((jobRec = CfsCommon.GetJobRecord(cfsEntity, jobId)) == null)
        {
            return false;
        }

        if (!jobRec.EventReference.IsLoaded)
        {
            jobRec.EventReference.Load();
        }

        eventId = jobRec.Event.EventId.ToString();
        eventDate = jobRec.Event.EventDate;
        LoadJobInfo(cfsEntity, jobRec);
        LoadTalentInfo(jobRec);
        
        return true;
    }

    private void LoadJobInfo(CfsEntity cfsEntity, Job jobRecord)
    {
        CfsUser user = CfsCommon.GetUserRecord(cfsEntity, jobRecord.PreparedBy.ToString());

        if (user != null)
        {
            lblCreatedBy.Text = user.UserName;
        }
        else
        {
            lblCreatedBy.Text = "( User no longer exists )";
        }
        
        lblDateCreated.Text = jobRecord.DateTimeCreated.ToLongDateString();

        /* CheckBoxes */
        chkJobComplete.Checked = (bool)jobRecord.IsJobComplete;
        chkBalanceCollected.Checked = (bool)jobRecord.IsBalanceCollected;
        chkCancelled.Checked = (bool)jobRecord.IsJobCancelled;
        chkChargedNetToCC.Checked = (bool)jobRecord.IsChargeNetToCC;

        /* Accounting */
        if (jobRecord.BalanceDue.HasValue){ tBoxBalanceDue.Text = jobRecord.BalanceDue.ToString(); }
        
        tBoxEntTotal.Text = jobRecord.ChargeForEntertain.ToString();
        tBoxLimoTotal.Text = jobRecord.ChargeForLimo.ToString();
        tBoxLocTotal.Text = jobRecord.ChargeForLocation.ToString();
        tBoxAccessoriesTotal.Text = jobRecord.ChargeForAccessories.ToString();
        tBoxGratuity.Text = jobRecord.ExpenseGratuity.ToString();
        tBoxSecPayroll.Text = jobRecord.ExpenseSecurity.ToString();
        tBoxReferCommission.Text = jobRecord.ExpenseReferral.ToString();
        tBoxSalesCommission.Text = jobRecord.ExpenseSales.ToString();

        /* Balance Responsibility */
        if (jobRecord.RespForBalance.HasValue)
        {
            ddlTalentListBalanceResp.SelectedValue = jobRecord.RespForBalance.ToString();
        }

        /* Credit Card Info */
        if (jobRecord.CCIssueBank != null) { tBoxIssueBankName.Text = jobRecord.CCIssueBank; }
        if (jobRecord.CCTypeBrand != null){ ddlCardBrand.SelectedValue = jobRecord.CCTypeBrand; }
        if (jobRecord.CCTypeCreditOrDebit != null){ ddlCardTypeCreditOrDebit.SelectedValue = jobRecord.CCTypeCreditOrDebit; }
        if (jobRecord.CCName != null) { tBoxCCNameOnCard.Text = jobRecord.CCName; }
        if (jobRecord.CCNum != null) { tBoxCCNum.Text = CfsCommon.Decrypt(jobRecord.CCNum, true); }
        if (jobRecord.CCExp != null) { tBoxCCExpire.Text = jobRecord.CCExp; }
        if (jobRecord.CCcv2Num != null) { tBoxCCCv2Num.Text = jobRecord.CCcv2Num; }

        if (jobRecord.SpecialInstructions != null) { tBoxSpecialInstructions.Text = jobRecord.SpecialInstructions; }

        if (jobRecord.Highlight != null) {
            if (jobRecord.Highlight == 1)
            {
                chkbxJobHighlight.Checked = true;
            }
            else
            {
                chkbxJobHighlight.Checked = false;
           
            }
        }
        if (jobRecord.JobPaid != null)
        {
            if (jobRecord.JobPaid == 1)
            {
                chkbxJobPaid.Checked = true;
            }
            else
            {
                chkbxJobPaid.Checked = false;

            }
        }
        
        lblTotalShowLen.Text = CfsCommon.FormatShowLengthHumanReadable( jobRecord.TotalShowLengthMins );
    }

    private void LoadTalentInfo(Job jobRecord)
    {
        DataTable dataTalent;
        string[] newData = new string[6];

        if (jobRecord.TalentToJob != null)
        {
            if (!jobRecord.TalentToJob.IsLoaded)
            {
                jobRecord.TalentToJob.Load();
            }
            
            dataTalent = CreateTempTalentTable();

            dataTalent.BeginLoadData();
            foreach (TalentToJob talAssoc in jobRecord.TalentToJob)
            {
                if (!talAssoc.TalentReference.IsLoaded)
                {
                    talAssoc.TalentReference.Load();
                }

                newData[0] = talAssoc.UID.ToString();
                newData[1] = talAssoc.Talent.TalentId.ToString();
                newData[2] = talAssoc.Talent.DisplayName;
                newData[3] = ((DateTime)talAssoc.StartDateTime).ToString("hh:mm tt");
                newData[4] = talAssoc.ShowLengthMins.ToString();
                newData[5] = talAssoc.Payroll.ToString();

                dataTalent.LoadDataRow(newData, true);

                /* Disable the talent from the full talent list, so she/he cannot be added twice */
                ListItem item = ddlTalentList.Items.FindByValue( talAssoc.Talent.TalentId.ToString() );

                if( item != null )
                {
                    item.Enabled = false;
                }
            }
            dataTalent.EndLoadData();

            /* Save the new data back to the Session */
            Session[TEMP_TALENT_SESSION_ID] = dataTalent; 
        }
    }
    #endregion

    #region Page Mode Functions
    private void UpdatePageMode(string mode)
    {
        /* Buttons and Labels */
        switch (mode)
        {
            case CfsCommon.MODE_ADD:
            {
                btnAddEditJob.Text = "ADD";
                break;
            }
            case CfsCommon.MODE_UPDATE:
            {
                btnAddEditJob.Text = "UPDATE";
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                btnAddEditJob.Text = "EDIT";
                break;
            }
            default:
            {
                return; //Bad Mode
            }
        }
        
        /* Enable/Disable fields */
        switch (mode)
        {
            case CfsCommon.MODE_ADD:
            case CfsCommon.MODE_UPDATE:
            {
                foreach (Control c in Page.Controls)
                {
                    CfsCommon.MakeControlsEditable(c);
                }

                /* Make all delete buttons in Talent Table Visible */
                foreach (Control c in rptrTalentList.Controls)
                {
                    CfsCommon.ChangeButtonsVisible(c, true);
                }

                pAddTalentControls.Visible = true;

                /* This single checkbox is controlled by permission on User Account */
                if( !CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.BalanceCollected, Session))
                {
                    chkBalanceCollected.Enabled = false;
                }
                break;
            }
            case CfsCommon.MODE_READONLY:
            {
                /* Make all Textboxes, Checkboxes, and Dropdowns Editable */
                foreach (Control c in Page.Controls)
                {
                    CfsCommon.MakeControlsNonEditable(c);
                }

                /* Make all delete buttons in Talent Table Invisible */
                foreach (Control c in rptrTalentList.Controls)
                {
                    CfsCommon.ChangeButtonsVisible(c, false);
                }

                pAddTalentControls.Visible = false;
                break;
            }
            default:
            {
                break;
            }
        }
        
        if (mode == CfsCommon.MODE_ADD)
        {
            pHoldPolicyChkBoxes.Visible = true;        
        }
        else 
        {
            pHoldPolicyChkBoxes.Visible = false;
        }

        DoControlExceptions();

        /* Persist */
        hiddenPageMode.Value = mode;
    }

    private void DoControlExceptions()
    {
        /* Talent Table is always Read-Only */
        foreach (Control c in rptrTalentList.Controls)
        {
            CfsCommon.MakeControlsNonEditable(c);
        }

        /* These fields are always read-only */
        tBoxDancerPayroll.ReadOnly = true;
        tBoxGrossIncome.ReadOnly = true;
        tBoxTotalExpenses.ReadOnly = true;
        tBoxOfficeNet.ReadOnly = true;

        tBoxDancerPayroll.CssClass = "textfieldreadonly";
        tBoxGrossIncome.CssClass = "textfieldreadonly";
        tBoxTotalExpenses.CssClass = "textfieldreadonly";
        tBoxOfficeNet.CssClass = "textfieldreadonly";
    }
    #endregion

    #region Page Validation Functions
    private bool IsTalentInfoValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";
        ClearAllFieldErrors();

        /* Talent Select */
        if (ddlTalentList.SelectedValue == "")
        {
            isValid = false;
            ddlTalentList.CssClass = "select error";
            ulErrorMsg.InnerHtml += "<li>You must choose Talent from the List to Add.</li>";
        }
        else
        {
            ddlTalentList.CssClass = "select";
        }

        /* Talent Start Time */
        if (!CfsCommon.ValidateTextBoxReq(tBoxStartTime, "Start Time", ulErrorMsg))
        {
            isValid = false;
        }
        else if (!CfsCommon.ValidateTextBoxTime(tBoxStartTime, "Start Time", ulErrorMsg))
        {
            isValid = false;
        }

        /* Payroll */
        if (!CfsCommon.ValidateTextBoxReq(tBoxPayroll, "Payroll", ulErrorMsg))
        {
            isValid = false;
        }
        else if (!CfsCommon.ValidateTextBoxInt(tBoxPayroll, "Payroll", ulErrorMsg))
        {
            isValid = false;
        }

        divErrorMsg.Visible = !isValid;

        return isValid;
    }
    
    private bool IsFormValid()
    {
        bool isValid = true;
        ulErrorMsg.InnerHtml = "";
        ClearAllFieldErrors();

        List<TextBox> tBoxList = new List<TextBox>();
        List<string> tBoxNameList = new List<string>();

        /* All of these fields must be ints, if they are filled in */
        tBoxList.Add(tBoxEntTotal);  tBoxNameList.Add("Entertainment Total");
        tBoxList.Add(tBoxLimoTotal); tBoxNameList.Add("Limo Total");
        tBoxList.Add(tBoxLocTotal);  tBoxNameList.Add("Location Total");
        tBoxList.Add(tBoxAccessoriesTotal); tBoxNameList.Add("Accessories Total");
        tBoxList.Add(tBoxGratuity);         tBoxNameList.Add("Gratuity");
        tBoxList.Add(tBoxSecPayroll);       tBoxNameList.Add("Security Payroll");
        tBoxList.Add(tBoxReferCommission); tBoxNameList.Add("Referral Commission");
        tBoxList.Add(tBoxSalesCommission); tBoxNameList.Add("Sales Commission");
        
        for (int i = 0; i < tBoxList.Count; i++)
        {
            TextBox tBox = (TextBox)tBoxList[i]; 
            
            if( tBox.Text != "" && !CfsCommon.ValidateTextBoxInt(tBox, (string)tBoxNameList[i], ulErrorMsg ))
            {
                isValid = false;            
            }
        }

        if (tBoxCCNum.Text != "" && !CfsCommon.ValidateTextBoxCreditCard(tBoxCCNum, "CC Num", ulErrorMsg))
        {
            isValid = false;
        }


        /* Lower checkboxes are only visible and applicable on ADD new job record */
        if (isValid && hiddenPageMode.Value == CfsCommon.MODE_ADD)
        {
            if (!IsLowerCheckboxesValid())
            {
                isValid = false;
            }
        }

        divErrorMsg.Visible = !isValid;

        return isValid;
    }

    private bool IsLowerCheckboxesValid()
    {
        bool isValid = true;

        List<CheckBox> chkList = new List<CheckBox>();
        List<string> chkListName = new List<string>();
        
        /* All of these boxes must be checked, before work order is completed */
        chkList.Add(chkCancelPolicy); chkListName.Add("Cancellation Policy");
        chkList.Add(chkNeedMusic); chkListName.Add("Need for Music");
        chkList.Add(chkTippingPolicy); chkListName.Add("Tipping Policy");
        chkList.Add(chkDirections); chkListName.Add("Directions");
        chkList.Add(chkAbusePolicy); chkListName.Add("Abuse Policy");
        chkList.Add(chkNoPictures); chkListName.Add("No Picture Taking");
        chkList.Add(chkEscortPolicy); chkListName.Add("Escort Service Policy");
        chkList.Add(chkOver18Policy); chkListName.Add("Over 18");
        chkList.Add(chkPersonalItems); chkListName.Add("Personal Items");
        chkList.Add(chkCallOffice); chkListName.Add("Call the Office");
        chkList.Add(chkPopoutCake); chkListName.Add("Popout Cake");
        chkList.Add(chkCashCannon); chkListName.Add("Cash Cannon");

        
        chkList.Add(chkChangeFeeNotice); chkListName.Add("Read Change Fee notice");
        chkList.Add(chkArrival); chkListName.Add("Arrival");

        for (int i = 0; i < chkList.Count; i++)
        {
            if (!chkList[i].Checked)
            {
                isValid = false;
                ulErrorMsg.InnerHtml += "<li>Checkbox '" + chkListName[i] + "' is not checked.</li>";
                chkList[i].CssClass = "error";
            }
            else
            {
                chkList[i].CssClass = "";
            }
        }

        return isValid;
    }

    private void ClearAllFieldErrors()
    {
        List<CheckBox> chkList = new List<CheckBox>();
        List<TextBox> tBoxList = new List<TextBox>();

        /* Checkboxes */
        chkList.Add(chkCancelPolicy);
        chkList.Add(chkNeedMusic);
        chkList.Add(chkTippingPolicy);
        chkList.Add(chkDirections);
        chkList.Add(chkAbusePolicy);
        chkList.Add(chkNoPictures);
        chkList.Add(chkEscortPolicy);
        chkList.Add(chkOver18Policy);
        chkList.Add(chkPersonalItems);
        chkList.Add(chkCallOffice);
        chkList.Add(chkPopoutCake);
        chkList.Add(chkCashCannon);
        chkList.Add(chkChangeFeeNotice);

        /* Textfields */
        tBoxList.Add(tBoxEntTotal);
        tBoxList.Add(tBoxLimoTotal);
        tBoxList.Add(tBoxLocTotal);
        tBoxList.Add(tBoxAccessoriesTotal);
        tBoxList.Add(tBoxGratuity);
        tBoxList.Add(tBoxSecPayroll);
        tBoxList.Add(tBoxReferCommission);
        tBoxList.Add(tBoxSalesCommission);

        tBoxList.Add(tBoxStartTime);
        tBoxList.Add(tBoxPayroll);

        foreach (CheckBox chk in chkList)
        {
            chk.CssClass = "";
        }

        foreach (TextBox tBox in tBoxList)
        {
            tBox.CssClass = "textfield";
        }

        ddlTalentList.CssClass = "select";

    }
    #endregion

    #region Double Booking Functions
    private bool IsDoubleBooking(string curJobId, string talentId, DateTime newTime, int showLenMins, out DateTime dblBookTime)
    {
        bool retVal = false; //Return var
        dblBookTime = new DateTime(1900, 1, 1); //default

        DateTime startTime = newTime.AddMinutes(NUM_MINUTES_CHECK_DBL_BOOK * -1);
        DateTime endTime = newTime.AddMinutes(showLenMins + NUM_MINUTES_CHECK_DBL_BOOK);

        string select;
        
        select  = "SELECT * FROM TalentToJob WHERE";
        select += " TalentId = " + talentId;
        select += " AND StartDateTime >= '" + startTime.ToString("yyyy-MM-dd hh:mm tt") + "'";
        select += " AND StartDateTime <= '" + endTime.ToString("yyyy-MM-dd hh:mm tt") + "'";

        /* Existing Jobs Only! (Prevents problem with 'Edit') */
        if (!string.IsNullOrEmpty(curJobId))
        {
            select += " AND JobId != " + curJobId;
        }

        /* So much simpler to do it the old way. Not sure how w/ entities */
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand(select, conn);
        SqlDataReader sRead;

        try
        {
            conn.Open();
            sRead = cmd.ExecuteReader();

            if (sRead.HasRows && sRead.Read())
            {
                dblBookTime = (DateTime)sRead["StartDateTime"];
                retVal = true;
            }
            else
            {
                retVal = false;
            }

            sRead.Close();
            conn.Close();
        }
        catch (Exception)
        {
        
        }

        return retVal;
    }

    protected void OnClick_btnConfirmNo(object sender, EventArgs arrggss)
    {
        divDblBookConfirm.Visible = false;
    }

    protected void OnClick_btnConfirmYes(object sender, EventArgs arrggss)
    {
        if (IsTalentInfoValid())
        {
            AddTalentToTempTable();
            ddlTalentList.SelectedItem.Enabled = false;

            LoadTempTalentTable();
            UpdateComputedFields();
        }

        divDblBookConfirm.Visible = false;

        uPnlAccting.Update();
        uPnlTalentList.Update();
        uPnlErrors.Update();
    }
    #endregion

}