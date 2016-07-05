using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;

using CfsNamespace;

public partial class backend_view_online_booking : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.PendingJobs, Session, Response);
    }

    protected void OnClick_btnDetails(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            string redirUrl = "view_online_booking.aspx?bookid=" + btn.CommandArgument;

            Response.Redirect(redirUrl, true);
        }    
    }

    protected void OnClick_btnDelete(object sender, EventArgs arrgggs)
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

        CfsEntity cfsEntity = new CfsEntity();

        ObjectQuery<OnlineBooking> booking = cfsEntity.OnlineBooking.Where("it.BookingId = " + hiddenDeleteId.Value);

        List<OnlineBooking> list = booking.ToList();

        if (list.Count == 1)
        {
            cfsEntity.DeleteObject(list[0]);
            cfsEntity.SaveChanges();
        }

        /* Hide Confirm Box */
        hiddenDeleteId.Value = "";
        divDeleteConfirm.Visible = false;

        /* Rebind, to update page. */
        grdBookings.DataBind();
    }

    #endregion
}
