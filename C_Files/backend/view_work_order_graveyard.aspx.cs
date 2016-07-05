using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using CfsNamespace;


public partial class backend_view_work_order_graveyard : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);

        grdOldJobs.RowDataBound += new GridViewRowEventHandler(grdOldJobs_RowDataBound);

        CfsEntity cfsEntities = new CfsEntity();

        // Load only events with cancelled jobs or complete and fully paid jobs
        var query = from ev in cfsEntities.Event
                    from j in ev.Job
                    where j.IsJobCancelled == true || (j.IsJobComplete == true && j.IsBalanceCollected == true)
                    orderby ev.EventDate descending, ev.StartTime descending
                    select ev;

        grdOldJobs.DataSource = query;
        if (!IsPostBack)
        {
            grdOldJobs.DataBind();
        }
    }

    private void grdOldJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if( e.Row == null || e.Row.DataItem == null )
        {
            return;
        }

        Event evtRecord = CfsCommon.EntityDataSourceExtensions.GetItemObject<Event>(e.Row.DataItem);
        Job jobRecord;

        if (!evtRecord.Job.IsLoaded)
        {
            evtRecord.Job.Load();
        }

        Button btn = (Button)e.Row.FindControl("btnViewJob");
        Image imgCanc = (Image)e.Row.FindControl("imgJobCancelled");
        

        if (evtRecord.Job == null)
        {
            btn.Visible = false;
            return;
        }

        List<Job> jobList = evtRecord.Job.ToList();

        if (jobList.Count != 1)
        {
            btn.Visible = false;

            Label lblIncomplete = (Label)e.Row.FindControl("lblIncomplete");

            if (lblIncomplete != null)
            {
                lblIncomplete.Visible = true;
            }
            return;
        }

        jobRecord = jobList[0];
        
        if (jobRecord.IsJobCancelled)
        {
            imgCanc.Visible = true;
        }

        btn.CommandArgument = jobRecord.JobId.ToString();

        // load customer
        if (!evtRecord.CustomerReference.IsLoaded)
        {
            evtRecord.CustomerReference.Load();
        }
        if (evtRecord.Customer != null) { 
            // show customer name
            Label customerName = (Label)e.Row.FindControl("customerName");
            customerName.Text = evtRecord.Customer.FirstName + " " + evtRecord.Customer.LastName;
        }
    }

    protected void OnClick_btnViewJob(object sender, EventArgs arrggss)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument, true);
        }
    }
    #endregion

    protected void grdOldJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOldJobs.PageIndex = e.NewPageIndex;
        grdOldJobs.DataBind();
    }

    /*
    void grdOldJobs_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        DataRowView dataRow = (DataRowView)e.Item.DataItem;

        if (dataRow != null && dataRow.Row["IsJobCancelled"] != null)
        {
            if ((bool)dataRow.Row["IsJobCancelled"] == true)
            {
                Image img = (Image)e.Item.FindControl("imgJobCancelled");

                img.Visible = true;
            }
        }
    }

    private void LoadDataOldSchool()
    {
        //Have to use the old method for now, until we figure out how to w/ Entities
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;

        string select;

        select = "SELECT c.FirstName, c.LastName, e.EventDate, e.LocationName, e.StartTime, j.JobId, j.IsJobCancelled ";
        select += "FROM Customer c,Event e,Job j ";
        select += "WHERE c.CustomerId = e.CustomerId AND e.EventId = j.EventId AND e.EventDate < '" + DateTime.Now.ToString("MM/dd/yyyy") + "' ORDER BY e.EventDate DESC;";

        SqlDataSource dataSrc = new SqlDataSource(conn, select);
        
        try
        {
            grdOldJobs.DataSource = dataSrc;
            grdOldJobs.DataBind();
        }
        catch (Exception)
        {

        }
    }
    */
}
