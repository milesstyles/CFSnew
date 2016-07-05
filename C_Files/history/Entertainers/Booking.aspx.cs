using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;
public partial class Booking : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlState);
            CfsCommon.GetStateList(ddlLocState);
            GetTimeList(ddlArrivalTime);
            PopulateDDL(ddlDay, 31);
            PopulateDDL(ddlNumDancers, 6);
            PopulateYears();

            if (string.IsNullOrEmpty(Request.UserHostAddress))
            {
                pIpMsg.InnerText = "To prevent fraud, your IP address of: " + Request.UserHostAddress + " has been documented.";
            }
        }
    }

    protected void btnBookShow_Click(object sender, EventArgs e)
    {
        /* Check at least 1 Phone Num */
        if (!CheckHasOnePhone())
        {
            return;
        }

        /* Check Captcha */
        recaptcha.Validate();
        valSummCaptcha.Visible = !recaptcha.IsValid; // Show Error msg only on Invalid Captcha
        
        if( Page.IsValid )
        {
            if (AddBooking())
            {
                pHldForm.Visible = false;
                pHldThankYou.Visible = true;
            }
        }
    }
    #endregion


    private void PopulateYears()
    {
        int end = DateTime.Now.Year + 5;
        for (int i = DateTime.Now.Year; i <= end; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void PopulateDDL(DropDownList p_ddl, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            p_ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void GetTimeList(DropDownList ddlTimeList)
    {
        DateTime start = DateTime.Parse("1900-01-01 10:00 AM");
        DateTime end = DateTime.Parse("1900-01-02 2:00 AM");

        while (start.CompareTo(end) <= 0)
        {
            ddlTimeList.Items.Add(new ListItem(start.ToString("hh:mm tt")));
            start = start.AddMinutes(30);
        }

        ddlTimeList.Items.Add(new ListItem("Other", CfsCommon.ONLINE_BOOKING_TIME_OTHER));
    }

    private bool CheckHasOnePhone()
    {
        if (tbHomePhone.Text == "" && tbCellPhone.Text == "")
        {
            divServerErrors.InnerHtml = "<p>You must fill in either a Home or Cell Phone Number<p>";
            divServerErrors.Visible = true;
            return false;
        }

        divServerErrors.Visible = false;
        return true;
    }

    private bool AddBooking()
    {
        CfsEntity cfse = new CfsEntity();
        OnlineBooking newBook = new OnlineBooking();
        int tmpInt;

        newBook.DateSubmitted = DateTime.Now;
        
        /* Client Contact Info */
        newBook.FirstName = tbFirstName.Text;
        newBook.LastName = tbLastName.Text;
        newBook.Email = tbEmail.Text;
        newBook.Address1 = tbAddress1.Text;
        newBook.Address2 = tbAddress2.Text;
        newBook.City = tbCity.Text;
        newBook.State = ddlState.SelectedValue;
        newBook.Country = "United States";
        newBook.Zip = tbZip.Text;
        newBook.HomePhone = tbHomePhone.Text;
        newBook.CellPhone = tbCellPhone.Text;
        newBook.ReferredBy = ddlReferred.SelectedValue;
        
        /* Event Location Info */
        newBook.LocName = tbLocName.Text;
        newBook.LocAddress1 = tbLocationAddress1.Text;
        newBook.LocAddress2 = tbLocAddress2.Text;
        newBook.LocCity = tbLocCity.Text;
        newBook.LocState = ddlLocState.SelectedValue;
        newBook.LocCountry = "United States";
        newBook.LocZip = tbLocZip.Text;
        newBook.LocPhone = tbLocPhone.Text;
        newBook.LocCrossSt = tbCrossSt.Text;

        /* Event Details */
        newBook.EventType = ddlEventType.SelectedValue;
        newBook.EventDate = DateTime.Parse(ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + ddlDay.SelectedValue);

        if( int.TryParse(tbNumGuests.Text, out tmpInt) )
        {
            newBook.NumGuests = tmpInt;
        }

        /* CheckBoxes */
        newBook.WantFemale = chkFemale.Checked;
        newBook.WantMale = chkMale.Checked;
        newBook.WantToplessWtr = chkTopless.Checked;
        newBook.WantLittlePeople = chkLittle.Checked;
        newBook.WantDragQueen = chkDrag.Checked;
        newBook.WantBellyDancer = chkBelly.Checked;
        newBook.WantFatMama = chkFat.Checked;
        newBook.WantImpersonator = chkImpersonator.Checked;

        newBook.NumTalent = Convert.ToInt32(ddlNumDancers.SelectedValue);
        newBook.ShowLengthMins = Convert.ToInt32(ddlShowLength.SelectedValue);
        
        newBook.ArrivalTime = Convert.ToDateTime("1900-01-01 " + ddlArrivalTime.SelectedValue);
        newBook.CrowdType = ddlCrowdType.SelectedValue;
        newBook.GuestAgeRange = tbAgeRange.Text;
        
        newBook.GuestOfHonor = tbGuestHonor.Text;
        
        newBook.IsSurprise = chkSurprise.Checked;
        newBook.SpecialInstructions = tbSpecialInst.Text;

        cfse.AddToOnlineBooking(newBook);
        
        if( cfse.SaveChanges() == 1 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
