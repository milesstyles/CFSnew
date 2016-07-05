using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/* Entity Includes */
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using CfsNamespace;

public partial class backend_view_pending_jobs : System.Web.UI.Page
{
    #region Page Events
    public string preferences = "Manas";
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.PendingJobs, Session, Response);

        grdPendingJobs.RowDataBound += new GridViewRowEventHandler(grdPendingJobs_RowDataBound);
    }

    /* This Event Handler Makes a Row Green-ish color, if the event is in the current week 
     * (According to the client's definition of the "Current Week")
     */
    void grdPendingJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;

        Pending pend = CfsCommon.EntityDataSourceExtensions.GetItemObject<Pending>(e.Row.DataItem);

        if (pend == null || pend.EventDate == null)
        {
            return;
        }

        DateTime startWk = CfsCommon.GetStartOfCurrentWeek();
        DateTime endWk = CfsCommon.GetEndOfCurrentWeek();
        DateTime date = (DateTime)pend.EventDate;

        //If the date is in the current 'work week', according to client's definition
        //(Between the Start of the Current Week, and End of the Current Week)
        if (DateTime.Compare(date, CfsCommon.GetStartOfCurrentWeek()) >= 0 &&
            DateTime.Compare(date, CfsCommon.GetEndOfCurrentWeek()) <= 0      )
        {
            e.Row.CssClass = "currentWeek"; 
        }
        if (pend.Highlight == 1)
        {
            e.Row.CssClass = "highlightWeek"; 
        }
        preferences = getEmails();
        preferences = preferences.Replace("&nbsp;;", "");
    }

    protected void OnClick_btnEdit(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            string redirUrl = "add_edit_pending.aspx?pendid=" + btn.CommandArgument;
            Response.Redirect(redirUrl, true);
        }
    }

    protected void OnClick_btnAddPendJob(object sender, EventArgs e)
    {
        Response.Redirect("add_edit_pending.aspx", true);
    }
    protected void OnClick_btnShowEmail(object sender, EventArgs e)
    {
        grdPendingJobs.Visible = false;
        emailList.Visible = true;
        preferences = getEmails();
        preferences = preferences.Replace("&nbsp;;", "");
    }

    protected void OnClick_btnDelete(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        /* TO DO - Locate this better */
        if ( !string.IsNullOrEmpty(btn.CommandArgument) )
        {
            hiddenDeleteId.Value = btn.CommandArgument;

            divDeleteConfirm.Style[HtmlTextWriterStyle.Position] = "absolute";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Left] = "40%";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Top] = "30%";
            divDeleteConfirm.Visible = true;
        }
    }

    protected void OnClick_btnConfirmNo(object sender, EventArgs e)
    {
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;
    }

    protected void OnClick_btnConfirmYes(object sender, EventArgs e)
    {
        if (hiddenDeleteId.Value == "")
        {
            return; //Don't expect this to ever happen
        }

        CfsEntity cfsEntity = new CfsEntity();

        List<Pending> list = ((ObjectQuery<Pending>)cfsEntity.Pending.Where("it.PendId = " + hiddenDeleteId.Value)).ToList();

        if (list.Count == 1)
        {
            cfsEntity.DeleteObject(list[0]);
            cfsEntity.SaveChanges();            
        }

        /* Refresh View */
        grdPendingJobs.DataBind();

        /* Hide Confirm Box */
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;
    }
    #endregion

    protected string getEmails()
    {
        string Emails = "";
       // GridViewRow row = grdPendingJobs.Rows[e.RowIndex];

        foreach (GridViewRow row in grdPendingJobs.Rows)
        {
            for (int i = 0; i < grdPendingJobs.Columns.Count; i++)
            {
                //email is in column 5
                if (grdPendingJobs.Columns[i].HeaderText=="EmailAddress")
                {
                    //String header = grdPendingJobs.Columns[i].HeaderText;
                    String cellText = row.Cells[i].Text;
                    cellText.Replace(" ", "");
                    if (cellText != "" && cellText!=" ")
                    {
                       string shtml= cellText;
                       if (shtml.Length > 10)
                       {
                           var innerString = shtml.Substring(shtml.IndexOf(">") + 1, shtml.IndexOf("</a>") - shtml.IndexOf(">") - 1);
                           Emails += innerString + ";";
 
                       }
     
                       // Emails += matches + ";";
                    }
                }

            }
        }
        return Emails;
        //System.Windows.Clipboard.SetData(DataFormats.Text, (Object)textData);
    }
}
