using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data;
using System.Data.SqlClient;
using CfsNamespace;
using System.Configuration;
public partial class backend_add_edit_bookjob_event : System.Web.UI.Page
{
    const string GUEST_TYPE_MALE_STRING = "MALE";
    const string GUEST_TYPE_FEMALE_STRING = "FEMALE";
    const string GUEST_TYPE_MIXED_STRING = "BOTH";
    int returnvalue;
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);

        if (!IsPostBack)
        {

        }
    }
    public void RunStoredProc(DateTime  eventDate, string  startTime, string  endTime, int jobid)
    {
        SqlConnection conn = null;
        SqlDataReader rdr = null;

       
        try
        {
            // create and open a connection object
            conn = new
                SqlConnection();
            //conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CfsEntity"].ConnectionString;
            conn.ConnectionString = "Data Source=96.43.209.212; initial Catalog=CenterfoldNew; User Id= SA; Password=DeadCats1";
            conn.Open();

            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand(
                "Clone_Jobs", conn);

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@b", SqlDbType.Int  , 50, ParameterDirection.ReturnValue  , false, 0, 50, "return_value", DataRowVersion.Default, null));
            cmd.Parameters.Add(new SqlParameter("@job_to_clone_id", SqlDbType.Int, 0, "jobid"));
            cmd.Parameters.Add(new SqlParameter("@EventDate", SqlDbType.DateTime , 0, "DateTimeEvent"));

            if (startTime != null)
            {
                cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime, 0, "StartTimeEvent"));
                cmd.Parameters["@StartTime"].Value = startTime;
            }
            if (startTime != null)
            {
                cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime, 0, "EndTimeEvent"));
                cmd.Parameters["@EndTime"].Value = endTime;
            }

            cmd.Parameters["@job_to_clone_id"].Value = jobid;
            cmd.Parameters["@EventDate"].Value = eventDate ;
            cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
            int i = cmd.ExecuteNonQuery();
            returnvalue = (int)cmd.Parameters["@b"].Value;

          
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
            if (rdr != null)
            {
                rdr.Close();
            }
        }
        Response.Redirect("add_edit_bookjob_office.aspx?eventid=" + returnvalue.ToString()+"&flagchecks=true");
    }

    protected void OnClick_btnAddEdit(object sender, EventArgs e)
    {
        DateTime dt;
        DateTime dt2;

        DateTime  EventDate;
        string start;
        string end;
        if (DateTime.TryParse(tBoxEventDate.Text, out dt))
        {
            EventDate = dt;
        }
        else
        {
            //This should never happen. We NEED the Event Date!!
              EventDate  = DateTime.Parse("1900-01-01 12:00 AM");
        }

        if (DateTime.TryParse(tBoxStartTime.Text, out dt) && DateTime.TryParse("1900-01-01 " + tBoxStartTime.Text, out dt2))
        {
            start = dt2.ToString();
        }
        else
        {
            start = null;
              }

        if (DateTime.TryParse(tBoxEndTime.Text, out dt) && DateTime.TryParse("1900-01-01 " + tBoxEndTime.Text, out dt2))
        {
            end  = dt2.ToString();
        }
        else
        {
            end = null;
        }
        string job = Request.QueryString["jobid"];
        if (job == null)
        {
            return;
        }
        int jobid = Convert.ToInt32(job);
        RunStoredProc(EventDate, start , end, jobid);
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
       
      
        return true;
    }
}
