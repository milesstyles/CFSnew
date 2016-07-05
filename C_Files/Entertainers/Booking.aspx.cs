using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
public partial class Booking : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (mymobile.isMobileBrowser())
        {
            Response.Redirect("m_Booking.aspx");
        }
        HtmlMeta metakey = new HtmlMeta();
        metakey.HttpEquiv = "x-ua-compatible";
        metakey.Content = "IE=8";

        Header.Controls.Add(metakey);
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
         
        string ymlpurl = "https://www.ymlp.com/api/Contacts.Add?Key=PWHNVE3E1722GT30RR40&Username=seven10design&Email=" + tbEmail.Text + "&GroupID=2&Field1=" + tbFirstName.Text + "&Field2=" + tbLastName.Text + "&Field11=" + tbAddress1.Text + "&Field12=" + tbAddress2.Text + "&Field13=" + tbCity.Text + "&Field14=" + ddlState.SelectedValue + "&Field15=" + tbZip.Text + "&Field16=United States" + "&Field17=" + tbHomePhone.Text + "&Field18=" + tbCellPhone.Text;
        xmlhttp(ymlpurl);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void xmlhttp(string url)
    {
        string method = "Get";
        string content = "";
        if (url == "")
        {
            //return "Error: url invalid";
        }
        XmlDocument xmlresp = new XmlDocument();        //Create new XML Doc to return at end (converted to string)
        WebResponse httpresp = null;                    //Initialize http response
        Uri xhruri = new Uri("http://www.ymlp.com/");

        try
        {
            xhruri = new Uri(url);
        }
        catch
        {
            //return "<error>URL is formatted improperly.</error>";
        }
        try
        {
            //Initialize the WebRequest
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(xhruri);
           // NetworkCredential netcred = new NetworkCredential(user, pass);
           // CredentialCache netcredcache = new CredentialCache();
           // netcredcache.Add(xhruri, "Basic", netcred);
           // httpreq.Credentials = netcredcache;
            httpreq.KeepAlive = false;
            httpreq.Timeout = 10000;                 //Set the timeout in milliseconds - some basic exception catching
            //httpreq.ContentType = "application/x-www-form-urlencoded";
            //httpreq.ContentType = "text/xml";

            //Change the content type if necessary
            if (content != "")
            {
                if (content == "appx")
                {
                    httpreq.ContentType = "application/x-www-form-urlencoded";  //useful for POST
                }
                else
                {
                    httpreq.ContentType = content;          //Should only be non-standard if plain/xml type gets checked?
                }
            }


            //Checking for GET or POST
           

            if (method == "GET")
            {
                httpreq.Method = "GET";             //Simply send the url which has the querystring right in it
            }

            httpresp = httpreq.GetResponse();       //This is where it errors out - need to read memory prior to this?
            string streamstring = "";
            try
            {
                Stream httprespstream = httpresp.GetResponseStream();
                StreamReader respstring = new StreamReader(httprespstream);
                streamstring = respstring.ReadToEnd();
                xmlresp.LoadXml(streamstring);
                //xmlresp.Load(httpresp.GetResponseStream());
            }
            catch
            {
                streamstring = streamstring.Replace("\b", "");
                streamstring = streamstring.Replace("\a", "");
                xmlresp.LoadXml(streamstring);
            }
            httpresp.Close();
        }

               //Error checking
        // May need to check for dns resolving/spoofing? (in the future)
        catch (WebException e)
        {
            if (httpresp != null)
            {
                httpresp.Close();
            }

            try
            {
                //This will actually catch the error from SOAPstation instead of returning a generic 500 error.
                //Create a stream to read the entire message from the exception, then dump it into a string after
                //retrieving the response stream.
                HttpWebResponse rsp = (HttpWebResponse)e.Response;
                StreamReader rd = new StreamReader(rsp.GetResponseStream());
                string res = rd.ReadToEnd();
                //httpresp.Close();
                rd.Close();
                //return res;
                //xmlresp.Load(e.Response.GetResponseStream());
            }

            catch
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description></error>");

                if (httpresp != null)
                {
                    httpresp.Close();
                }
            }
        }
        catch (Exception x)
        {
            if (httpresp != null)
            {
                httpresp.Close();
            }
            try
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description><message>" + x.Message + "</message><more></more></error>");
            }
            catch
            {
                xmlresp.LoadXml("<error><description>Loading XML failed</description></error>");

                if (httpresp != null)
                {
                    httpresp.Close();
                }
            }
        }
    }
    
}
