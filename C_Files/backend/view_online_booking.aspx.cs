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
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.PendingJobs, Session, Response);

        string bookId = (string)Request.Params["bookid"];
        
        LoadData(bookId);
    }

    private void LoadData(string bookingId)
    {
        if (string.IsNullOrEmpty(bookingId))
        {
            return; /* Bad booking Id */
        }
        
        CfsEntity cfsEntity = new CfsEntity();

        List<OnlineBooking> list = ((ObjectQuery<OnlineBooking>)cfsEntity.OnlineBooking.Where("it.BookingId = " + bookingId)).ToList();

        if (list.Count == 1)
        {
            rptrBookingInfo.DataSource = list;
            rptrBookingInfo.DataBind();
        }
    }

    protected string GetSurpriseText(bool isSuprise)
    {
        if (isSuprise)
        {
            return "Yes";
        }
        else
        {
            return "No";
        }
    }

    protected string GetSurpriseColor(bool isSurprise)
    {
        if (isSurprise)
        {
            return "color: red; font-weight: bold;";
        }

        return "";
    }

    protected string GetTalentType(bool wantFemale, bool wantMale, bool wantToplessWtr, bool wantLittlePeep, bool wantDrag, bool wantBellyDance, bool wantFatMama, bool wantImpersonator)
    {
        string retString = "";

        List<string> list = new List<string>();

        if (wantFemale) { list.Add("Female"); }
        if (wantMale) { list.Add("Male"); }
        if (wantToplessWtr) { list.Add("Topless Waiting"); }
        if (wantLittlePeep) { list.Add("Little People"); }
        if (wantDrag) { list.Add("Drag Queen"); }
        if (wantBellyDance) { list.Add("Belly Dance"); }
        if (wantFatMama) { list.Add("Fat Mama"); }
        if (wantImpersonator) { list.Add("Impersonator"); }

        foreach (string item in list)
        {
            retString += item + ", ";
        }

        //Remove the leading comma from the last item
        if (retString.Length > 2)
        {
            retString = retString.Remove(retString.Length - 2);
        }

        return retString;
    }

    protected string GetArrivalTime(object arrivalTime)
    {
        if (arrivalTime == null)
        {
            return "";
        }
        else if (((DateTime)arrivalTime).ToString("hh:mm tt") == CfsCommon.ONLINE_BOOKING_TIME_OTHER)
        {
            return "OTHER";
        }
        
        return CfsCommon.FormatTime(arrivalTime);
    }
    
}
