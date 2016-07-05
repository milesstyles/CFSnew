using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using CfsNamespace;


public partial class backend_view_work_orders : System.Web.UI.Page
{
    CfsEntity CfsEntity;
    public string date =""; 
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);

        grdViewWorkOrders.RowDataBound += new GridViewRowEventHandler(grdViewWorkOrders_RowDataBound);

        if (!IsPostBack)
        {
            if (Request.QueryString["dRange"] != null)
            {
                date = Request.QueryString["dRange"].ToString();
                h3Search.Visible = true;
                LoadDataOldSchoolSearch();
            }
          
            if (Request.QueryString["search"] == "true")
            {
                h3Search.Visible = true;
                LoadDataOldSchoolSearch();
                return;
            }
            else
            {
                h3View.Visible = true;
                LoadDataOldSchool();
            }
                 
              
            
            
        }
    }

    protected void OnClick_btnViewDetails(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("view_job_info.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument, true);
        }
    }
    #endregion

    #region Callbacks
    void grdViewWorkOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row == null || e.Row.DataItem == null) { return; }

        DataRowView dRowView = (DataRowView)e.Row.DataItem;

        if (CfsEntity == null)
        {
            CfsEntity = new CfsEntity();
        }
        try
        {
            Job theJob = CfsCommon.GetJobRecord(CfsEntity, dRowView["JobId"].ToString());

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
        { 
        
        }
    }
    #endregion

    private void LoadDataOldSchool()
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select;
        string orderBy = " GROUP BY c.FirstName, c.LastName, e.EventDate,e.EventType,e.LocationName, e.LocationCity, e.LocationState, e.StartTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins ORDER BY e.EventDate ASC, min(tj.StartDateTime) ASC;";

        if (date == "")
        {
            date = DateTime.Now.ToString("MM/dd/yyyy");


            select = "SELECT DISTINCT TOP 1000 c.FirstName, c.LastName, e.EventDate, e.EventType, e.LocationName, e.LocationCity, e.LocationState, e.StartTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins, min(tj.StartDateTime)";
            select += "FROM Customer c JOIN Event e on c.CustomerId = e.CustomerId";
            select += " JOIN Job j on e.EventId = j.EventId";
            select += " LEFT OUTER JOIN TalentToJob tj on j.JobId = tj.JobId";
            select += " WHERE j.IsJobCancelled = 0 AND e.EventDate >= '" + date + " 12:00 AM' ";
            select += "AND (j.IsBalanceCollected = 0 OR j.IsJobComplete = 0)";

        }
        else
        {
            select = "SELECT DISTINCT TOP 1000 c.FirstName, c.LastName, e.EventDate, e.EventType, e.LocationName, e.LocationCity, e.LocationState, e.StartTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins, min(tj.StartDateTime)";
            select += "FROM Customer c JOIN Event e on c.CustomerId = e.CustomerId";
            select += " JOIN Job j on e.EventId = j.EventId";
            select += " LEFT OUTER JOIN TalentToJob tj on j.JobId = tj.JobId";
            select += " WHERE j.IsJobCancelled = 0 AND e.EventDate = '" + date+"'";
            select += "AND (j.IsBalanceCollected = 0 OR j.IsJobComplete = 0)";
        }

        select += orderBy;

        SqlDataSource dataSrc = new SqlDataSource(conn, select);

        //try
        {
            grdViewWorkOrders.DataSource = dataSrc;
            grdViewWorkOrders.DataBind();
        }
        //catch
        {

        }
    }

    private void LoadDataOldSchoolSearch()
    {
        /* Have to use the old method for now, until we figure out how to w/ Entities */
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
        string select;
        string orderBy = " ORDER BY e.EventDate DESC, e.StartTime DESC;";

        select = "SELECT DISTINCT TOP 1000 c.FirstName, c.LastName, e.EventDate, e.EventType, e.LocationName, e.LocationCity, e.LocationState, e.StartTime, j.JobId, j.IsJobCancelled, j.TotalShowLengthMins ";
        select += " FROM Customer c,Event e,Job j, TalentToJob Tj ";
        select += " WHERE c.CustomerId = e.CustomerId AND e.EventId = j.EventId AND j.JobId = Tj.JobId ";

        if (date != "" && date !=null)
        {
           select += " AND e.EventDate = '" + date + "'";
        }

        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_JOB_DATE] != null)
        {
            select += " AND e.EventDate = '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_JOB_DATE].ToString() + "'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_WO_NUM] != null)
        {
            select += " AND j.JobId LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_WO_NUM].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_FIRST_NAME] != null)
        {
            select += " AND c.FirstName LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_FIRST_NAME].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_LAST_NAME] != null)
        {
            select += " AND c.LastName LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_LAST_NAME].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_LOCATION] != null)
        {
            select += " AND e.LocationName LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_LOCATION].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_CITY] != null)
        {
            select += " AND e.LocationCity LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_CITY].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_STATE] != null)
        {
            select += " AND e.LocationState = '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_STATE].ToString() + "'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_CCNUM] != null)
        {
            string encryptCcNum = CfsCommon.Encrypt(Request.Params[CfsCommon.PARAM_WO_SEARCH_CCNUM].ToString() , true);
            
            select += " AND j.CCNum = '" + encryptCcNum + "'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_TALID] != null)
        {
            select += " AND Tj.TalentId = " + Request.Params[CfsCommon.PARAM_WO_SEARCH_TALID].ToString();
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_REFER] != null)
        {
            select += " AND c.ReferredBy LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_REFER].ToString() + "%'";
        }
        if (Request.Params[CfsCommon.PARAM_WO_SEARCH_EVENT_TYPE] != null)
        {
            select += " AND e.EventType LIKE '" + Request.Params[CfsCommon.PARAM_WO_SEARCH_EVENT_TYPE].ToString() + "%'";
        }
        select += orderBy;
        
        SqlDataSource dataSrc = new SqlDataSource(conn, select);

        try
        {
            grdViewWorkOrders.DataSource = dataSrc;
            grdViewWorkOrders.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void grdViewWorkOrders_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
