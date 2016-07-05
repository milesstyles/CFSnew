using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* Entity Includes */
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using CfsNamespace;

public partial class backend_view_applicants : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);

        GetApplicants();
    }

    protected void OnClick_btnEdit(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string redirUrl = "view_edit_applicant_details.aspx?appid=";

        if( btn.CommandArgument != null && btn.CommandArgument != "" )
        {
            redirUrl += btn.CommandArgument;
            Response.Redirect(redirUrl, true);        
        }
    }

    protected void OnClick_btnDelete(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (btn.CommandArgument != null && btn.CommandArgument != "")
        {
            hiddenDeleteId.Value = btn.CommandArgument;

            divDeleteConfirm.Style[HtmlTextWriterStyle.Position] = "absolute";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Left] = "40%";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Top] = "40%";

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
        
        CfsEntity cfEntity = new CfsEntity();

        ObjectQuery<Applicant> applicants = cfEntity.Applicant.Where("it.ApplicantId = " + hiddenDeleteId.Value );
        List<Applicant> appList = applicants.ToList();

        if (appList.Count == 1)
        {
            appList[0].Status = "DELETED";

            cfEntity.SaveChanges();
        }

        /* Hide Confirm Box */
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;

        /* Rebind, to update page. */
        GetApplicants(); 
    }
    #endregion

    private void GetApplicants()
    {
        CfsEntity cfEntity = new CfsEntity();

        ObjectQuery<Applicant> applicants = cfEntity.Applicant.Where("it.Status != 'DELETED' AND it.Status != 'HIRED'").OrderBy("it.DateApplied");
        List<Applicant> appList = applicants.ToList();

        rptrViewApplicants.DataSource = appList;
        rptrViewApplicants.DataBind();
    }

    protected string GetRowColor(bool contacted, int rowNum)
    {
        if (contacted)
        {
            return "class=\"contactRow\" ";
        }

        return "";
    }

    protected string GetHasImages(string imageOne, string imageTwo, string imageThree)
    {
        if ((imageOne == null || imageOne == "") &&
            (imageTwo == null || imageTwo == "") &&
            (imageThree == null || imageThree == ""))
        {
            return "&nbsp;";
        }

        return "<img src=\"../images/pictures.png\" alt=\"Pictures\" width=\"20\" height=\"20\" />";
    }

    protected string FormatDate(DateTime dt)
    {
        if (dt == null)
        {
            return "";
        }

        return dt.ToString("dddd M/d/yyyy h:mm tt");
    }
}
