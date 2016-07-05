using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using CfsNamespace;


public partial class backend_view_post_job_info : System.Web.UI.Page
{
    private CfsEntity TheCfsEntity;

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);

        grdPostJob.RowDataBound += new GridViewRowEventHandler(grdPostJob_RowDataBound);

        if (!IsPostBack)
        {
            TheCfsEntity = new CfsEntity();
            
            LoadDataOldSchool();
        }
    }

    protected void OnClick_btnViewJob(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument, true);
        }        
    }

    protected void OnClick_btnUpdateJob(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (string.IsNullOrEmpty(btn.CommandArgument))
        {
            return;
        }

        string[] param = btn.CommandArgument.Split(',');

        if (param.Length != 2)
        {
            return;
        }

        int rowNum;
        string rowNumStr = param[0];
        string jobIdStr = param[1];

        if (!int.TryParse(rowNumStr, out rowNum))
        {
            return;
        }

        UpdateJobInfo(rowNum, jobIdStr);

        Response.Redirect("view_post_job_info.aspx");
    }
    #endregion

    #region Callbacks
    private void grdPostJob_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row == null || e.Row.DataItem == null) { return; }

        DataRowView dRowView = (DataRowView)e.Row.DataItem;

        if (dRowView["RespForBalance"].GetType().Name == "Int32" && (int)dRowView["RespForBalance"] != 0) //Zero is reserved
        {
            Talent talRec = CfsCommon.GetTalentRecord(TheCfsEntity, dRowView["RespForBalance"].ToString());

            Label lbl = (Label)e.Row.FindControl("lblbalresp");

            if (talRec != null)
            {
                lbl.Text = talRec.DisplayName;
            }
        }

        if (dRowView["OfficeNet"].GetType().Name == "Int32" && (int)dRowView["OfficeNet"] == 0)
        {
            CheckBox cBox = (CheckBox)e.Row.FindControl("chkBalanceCollected");

            if (cBox != null)
            {
                cBox.Visible = false;
            }

            /*
            Label lbl = (Label)e.Row.FindControl("lblbalresp");

            lbl.Text = ""; //Not shown, if Office is owed $0
            */
        }



        try
        {
            if (TheCfsEntity == null)
            {
                TheCfsEntity = new CfsEntity();
            }
            Job theJob = CfsCommon.GetJobRecord(TheCfsEntity, dRowView["JobId"].ToString());

            if (!theJob.TalentToJob.IsLoaded)
            {
                theJob.TalentToJob.Load();
            }
            if (theJob.JobPaid != 1)
            {
                Image imgHasPaid = (Image)e.Row.FindControl("imgHasPaid");

                if (imgHasPaid != null)
                {
                    imgHasPaid.Visible = true;
                }

            }
        }
        catch (Exception ex)
        { 
        
        }


    }
    #endregion

    private bool UpdateJobInfo(int rowNum, string jobId)
    {
        CheckBox chkJobComplete = (CheckBox)grdPostJob.Rows[rowNum].FindControl("chkJobCompleted");
        CheckBox chkBalanceCol = (CheckBox)grdPostJob.Rows[rowNum].FindControl("chkBalanceCollected");

        if (chkBalanceCol == null || chkJobComplete == null)
        {
            return false;
        }

        CfsEntity cfsEntity = new CfsEntity();
        Job theJob;

        if ((theJob = CfsCommon.GetJobRecord(cfsEntity, jobId)) == null)
        {
            return false;
        }

        theJob.IsJobComplete = chkJobComplete.Checked;

        if (chkBalanceCol.Visible)
        {
            theJob.IsBalanceCollected = chkBalanceCol.Checked;
        }

        cfsEntity.SaveChanges();
        return true;
    }

    private void LoadDataOldSchool()
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select;

        select = "SELECT DISTINCT j.JobId, e.EventDate, e.LocationName, j.IsJobCancelled, j.IsBalanceCollected, j.IsJobComplete, j.OfficeNet, j.RespForBalance";
        select += " FROM Customer c, Event e, Job j ";
        select += " WHERE c.CustomerId = e.CustomerId AND e.EventId = j.EventId AND (j.IsJobComplete = 'false' OR j.IsBalanceCollected = 'false') AND j.IsJobCancelled = 'false' ";
        select += " AND e.EventDate < '" + DateTime.Now.ToString("yyyy-MM-dd") + " 12:00 AM'";
        select += " ORDER BY e.EventDate DESC;";
        
        SqlDataSource dataSrc = new SqlDataSource(conn, select);

        grdPostJob.DataSource = dataSrc;
        grdPostJob.DataBind();
    }

    protected bool GetBalCollectEnabled()
    {
        return CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.BalanceCollected, Session);
    }
}
