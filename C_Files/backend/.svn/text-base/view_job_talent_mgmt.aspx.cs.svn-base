using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;

using System.Data.SqlClient;

using CfsNamespace;

public partial class backend_view_job_talent_mgmt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);
        
        LoadDataOldSchool();
    }

    protected void OnClick_btnViewJob(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument, true);
        }
    }

    private void LoadDataOldSchool()
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */

        string select;
        select = "SELECT j.JobId,e.EventDate,e.LocationName,t.DisplayName,tj.StartDateTime,tj.ShowLengthMins ";
        select += "FROM Talent t, Job j, Event e, TalentToJob tj WHERE e.EventId = j.EventId AND j.JobId = tj.JobId AND tj.TalentId = t.TalentId AND j.IsJobCancelled = 0";
        select += "AND e.EventDate >= '" + DateTime.Now.ToString("MM/dd/yyyy") + "' ";
        select += "ORDER BY e.EventDate, tj.StartDateTime, t.DisplayName;";

        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;

        SqlDataSource dataSrc = new SqlDataSource(conn, select);

        

        //try
        //{
            grdTalMgmt.DataSource = dataSrc;
            grdTalMgmt.DataBind();        
        //}
        //catch (Exception)
       // {
        
        //}
    }

    /*
    private void LoadData()
    {
        DataTable dTable = new DataTable();
        DataSet dSet = new DataSet();

        dTable.Columns.Add("JobId");
        dTable.Columns.Add("EventDate");
        dTable.Columns.Add("EventLocation");
        dTable.Columns.Add("DisplayName");
        dTable.Columns.Add("StartTime");
        dTable.Columns.Add("ShowLength");

        string[] data = new string[6];

        CfsEntity cfsEntity = new CfsEntity();

        string today = DateTime.Now.ToString("MM/dd/yyyy");
        //List<Event> eventList = ((ObjectQuery<Event>)cfsEntity.Event.Where("it.EventDate >= " + today)).ToList();
        List<Event> eventList = ((ObjectQuery<Event>)cfsEntity.Event.Where("it.EventDate >= @today", new ObjectParameter("today", DateTime.Now))).ToList();

        dTable.BeginLoadData();
        foreach (Event evt in eventList)
        {
            if (!evt.Job.IsLoaded) { evt.Job.Load(); }

            foreach (Job job in evt.Job)
            {
                if (!job.TalentToJob.IsLoaded){ job.TalentToJob.Load(); }

                foreach (TalentToJob talAssoc in job.TalentToJob)
                {
                    if (!talAssoc.TalentReference.IsLoaded) { talAssoc.TalentReference.Load(); }

                    data[0] = job.JobId.ToString();
                    data[1] = evt.EventDate.ToString("dddd MM/dd/yyyy");
                    data[2] = evt.LocationName;
                    data[3] = talAssoc.Talent.DisplayName;
                    data[4] = CfsCommon.FormatTime(talAssoc.StartDateTime);
                    data[5] = CfsCommon.FormatShowLengthHumanReadable(talAssoc.ShowLengthMins);

                    dTable.LoadDataRow(data, true);
                }
            }
        }
        dTable.EndLoadData();

        grdTalMgmt.DataSource = dTable;
        grdTalMgmt.DataBind();
    }
     */

    

}
