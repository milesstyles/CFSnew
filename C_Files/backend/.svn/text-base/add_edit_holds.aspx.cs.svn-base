using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.ComponentModel;

using CfsNamespace;

public partial class backend_add_edit_holds : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.EmployeeMgmt, Session, Response);
        
        if (!IsPostBack)
        {
            CfsCommon.GetFullTalentList(ddlFullTalentList);
        }

        grdHolds.RowDataBound += new GridViewRowEventHandler(grdHolds_RowDataBound);
    }

    protected void OnClick_btnAddEdit(object sender, EventArgs e)
    {
        if (AddNewHold(ddlFullTalentList.SelectedValue))
        {
            grdHolds.DataBind();

            ddlFullTalentList.SelectedValue = "";
            tBoxDate.Text = "";
            tBoxLocation.Text = "";
            tBoxNotes.Text = "";
        }
    }

    protected void OnClick_btnDeleteHold(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        /* TO DO - Locate this better ? */
        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            hiddenDeleteId.Value = btn.CommandArgument;

            divDeleteConfirm.Style[HtmlTextWriterStyle.Position] = "absolute";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Left] = "40%";
            divDeleteConfirm.Style[HtmlTextWriterStyle.Top] = "50%";
            divDeleteConfirm.Visible = true;
        }
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

    protected void OnClick_btnConfirmNo(object sender, EventArgs e)
    {
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;
    }

    protected void OnClick_btnConfirmYes(object sender, EventArgs e)
    {
        if (hiddenDeleteId.Value == "")
        {
            /* Don't expect this to ever happen */
            return; 
        }

        CfsEntity cfsEntity = new CfsEntity();

        List<TalentHold> list = ((ObjectQuery<TalentHold>)cfsEntity.TalentHold.Where("it.HoldId = " + hiddenDeleteId.Value)).ToList();

        if (list.Count == 1)
        {
            cfsEntity.DeleteObject(list[0]);
            cfsEntity.SaveChanges();
        }

        /* Refresh View */
        grdHolds.DataBind();

        /* Hide Confirm Box */
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;
    }
    #endregion

    #region Callbacks
    private void grdHolds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;

        TalentHold talHold = CfsCommon.EntityDataSourceExtensions.GetItemObject<TalentHold>(e.Row.DataItem);

        if (talHold == null)
            return;

        if (talHold.Talent != null) //Ensure the foreign key data is valid
        {
            Label lbl = e.Row.FindControl("talentName") as Label;

            if (lbl != null)
            {
                lbl.Text = talHold.TalentReference.Value.DisplayName;
            }
        }
    }
    #endregion




    private bool AddNewHold(string talentId)
    {
        if (talentId == "")
        {
            return false;
        }

        if (HasHoldRecord(talentId))
        {
            //Hold record already exists, only 1 Hold Record per talent record.
            return false; 
        }

        CfsEntity cfsEntity = new CfsEntity();
        Talent talentRef;
        DateTime dtStart;

        if ((talentRef = CfsCommon.GetTalentRecord(cfsEntity, ddlFullTalentList.SelectedValue)) == null)
        {
            return false; //Don't expect this to ever happen
        }

        TalentHold newHold = new TalentHold();

        if (DateTime.TryParse(tBoxDate.Text, out dtStart))
        {
            newHold.DateStart = dtStart;
        }
        else
        {
            newHold.DateStart = DateTime.Parse("1900-01-01");
        }
        
        newHold.TalentReference.Value = talentRef;
        newHold.CurLocation = tBoxLocation.Text;
        newHold.Notes = tBoxNotes.Text;

        cfsEntity.AddToTalentHold(newHold);

        if (cfsEntity.SaveChanges() <= 0)
        {
            return false;
        }

        return true;
    }

    private bool HasHoldRecord(string talentId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        Talent talRecord = CfsCommon.GetTalentRecord(cfsEntity, talentId);

        if (talRecord == null)
        {
            /* Talent record does not exists, so no hold exists either*/
            return false;
        }

        if( !talRecord.TalentHold.IsLoaded )
        {
            talRecord.TalentHold.Load();
        }

        if (talRecord.TalentHold.Count == 0)
        {
            /* No Hold record associated w/ this talent exists */
            return false;
        }
        else
        {
            /* A Hold record associated w/ this talent exists */
            return true;
        }
    }
}