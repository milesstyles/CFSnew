using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web.UI.HtmlControls;
using CfsNamespace;

public partial class backend_view_job_info : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsCommon.CheckPageAccess((int)CfsCommon.Section.WorkOrders, Session, Response);
        
        rptrClientInfo.ItemDataBound += new RepeaterItemEventHandler(rptrClientInfo_ItemDataBound);
        rptrEventInfo.ItemDataBound += new RepeaterItemEventHandler(rptrEventInfo_ItemDataBound);
        rptrTalentInfo.ItemDataBound += new RepeaterItemEventHandler(rptrTalentInfo_ItemDataBound);



        //tbl_paymentinfo.Visible = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.CreditCard, Session);
        if (Request.Params[CfsCommon.PARAM_JOB_ID] != null)
        {
            GetJobInfoFromJobId((string)Request.Params[CfsCommon.PARAM_JOB_ID]);
        }
        else if (Request.Params[CfsCommon.PARAM_EVENT_ID] != null)
        {
            /* TO DO - For Event ID ? */
        }
        
        
    }

    protected void OnClick_btnEditCustInfo(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            string redirUrl = "add_edit_bookjob_customer.aspx?" + CfsCommon.PARAM_CUSTOMER_ID + "=" + btn.CommandArgument;
            redirUrl += "&" + CfsCommon.PARAM_UPDATE_MODE + "=true";
            redirUrl += "&" + CfsCommon.PARAM_JOB_ID + "=" + (string)Request.Params[CfsCommon.PARAM_JOB_ID];
            Response.Redirect(redirUrl, true);
        }
    }

    protected void OnClick_btnEditEventInfo(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            string redirUrl = "add_edit_bookjob_event.aspx?" + CfsCommon.PARAM_EVENT_ID + "=" + btn.CommandArgument;
            redirUrl += "&" + CfsCommon.PARAM_UPDATE_MODE + "=true";

            Response.Redirect(redirUrl, true);
        }
    }

    protected void OnClick_btnEditJobInfo(object sender, EventArgs arrgggs)
    {
        Button btn = (Button)sender;

        if (!string.IsNullOrEmpty(btn.CommandArgument))
        {
            string redirUrl = "add_edit_bookjob_office.aspx?" + CfsCommon.PARAM_JOB_ID + "=" + btn.CommandArgument;
            redirUrl += "&" + CfsCommon.PARAM_UPDATE_MODE + "=true";

            Response.Redirect(redirUrl, true);
        }
    }

    protected void OnClick_btnSendClientConfirm(object sender, EventArgs arrgggs)
    {
        Button sendBtn = (Button)sender;

        if (string.IsNullOrEmpty(sendBtn.CommandArgument))
        {
            /* Not expected to happen */
            return;
        }

        string custId = sendBtn.CommandArgument;
        string msg;

        Button btnConfirm = (Button)rptrClientInfo.Items[0].FindControl("btnSendClientConfirm");
        Label lblConfirm = (Label)rptrClientInfo.Items[0].FindControl("lblClientConfirmSent");

        lblConfirm.Visible = true;

        if (SendConfirmEmail(custId, out msg))
        {
            btnConfirm.Visible = false;
        }
        else
        {
            lblConfirm.Text = msg;
        }
    }

    protected void btnSendTalentConfirm_Click(object sender, EventArgs e)
    {
        Button sendBtn = sender as Button;
        if (string.IsNullOrEmpty(sendBtn.CommandArgument))
            return;

        string[] commandArgSplit = sendBtn.CommandArgument.Split('|');
        if (!commandArgSplit.Length.Equals(2))
            return;

        string talentId = commandArgSplit[0];
        string customerId = commandArgSplit[1];
        string msg;
        Button btnConfirm = sendBtn.Parent.FindControl("btnSendTalentConfirm") as Button;
        Label lblConfirm = sendBtn.Parent.FindControl("lblTalentConfirmSent") as Label;

        lblConfirm.Visible = true;

        if (SendDancerEmail(talentId, customerId, out msg))
        {
            Response.Redirect(Request.Url.ToString());
        }
        else
        {
            lblConfirm.Text = msg;
        }
    }
    #endregion

    #region Callbacks
    /* This Callback controls the visiblity of the "Client Confirmations" button
     * and labels in the 'Client Info' section.
     */
    private void rptrClientInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Button btn = (Button)e.Item.FindControl("btnSendClientConfirm");
        Label lbl = (Label)e.Item.FindControl("lblClientConfirmSent");

        Customer custRec = CfsCommon.EntityDataSourceExtensions.GetItemObject<Customer>(e.Item.DataItem);

        /* Check Email on file */
        if (string.IsNullOrEmpty(custRec.Email))
        {
            lbl.Text = "(No Client Email on record)";
            btn.Visible = false;
            lbl.Visible = true;
            return;
        }

        /* Check Email format OK */
        try
        {
            MailAddress email = new MailAddress(custRec.Email);
        }
        catch (FormatException)
        {
            lbl.Text = "(Client Email improperly formatted)";
            btn.Visible = false;
            lbl.Visible = true;
            return;
        }
        catch (Exception ex)
        {
            lbl.Text = "(Error - Exception: " + ex.Message + ")";
            btn.Visible = false;
            lbl.Visible = true;
            return;        
        }

        if (custRec.IsCustConfirmSent)
        {
            btn.Visible = false;
            lbl.Visible = true;
            lbl.Text = "Client Confirmation Sent";
        }
        else
        {
            btn.Visible = true;
            lbl.Visible = false;
        }
    }

    /* This Callback controls the visiblity of the "Dancer Confirmation" buttons
     * and labels in the 'Talent Info' section.
     */
    private void rptrTalentInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        /* Part 1: Logic to Show/Hide Dancer Confirm Button */
        Button btn = (Button)e.Item.FindControl("btnSendTalentConfirm");
        Label lbl = (Label)e.Item.FindControl("lblTalentConfirmSent");

        DataRowView dRow = (DataRowView)e.Item.DataItem;

        if ( (dRow["Email1"] == null || dRow["Email1"].ToString() == "") &&
             (dRow["Email2"] == null || dRow["Email2"].ToString() == "")    )
        {
            btn.Visible = false;
            lbl.Text = "( No email on record )";
            lbl.Visible = true;
            return;
        }

        if ((string)dRow["ConfirmSent"] == "true")
        {
            btn.Visible = false;
            lbl.Visible = true;
        }
        else
        {
            btn.Visible = true;
            lbl.Visible = false;
        }

        /* Part 2: Logic to fill in Talent Contact # */

    }

    /* This Callback controls the visiblity of the "Surprise Party" Label in
     * the 'Event Info' Section.
     */
    private void rptrEventInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl = (Label)e.Item.FindControl("lblSurprise");

        Event eventRec = CfsCommon.EntityDataSourceExtensions.GetItemObject<Event>(e.Item.DataItem);

        if ((bool)eventRec.IsSurpriseParty)
        {
            lbl.Visible = true;
        }
        else
        {
            lbl.Visible = false;
        }
    }
    #endregion
    protected string GetDivClass()
    {
        return "hideDIV";
    }
    private void GetJobInfoFromJobId(string jobId)
    {
        Customer custRec;
        Event eventRec;
        Job jobRec;
        Talent talRec;

        string eventId;
        string custId;

        CfsEntity cfsEntity = new CfsEntity();

        if ((jobRec = CfsCommon.GetJobRecord(cfsEntity, jobId)) == null)
        {
            /* Bad JobId, Job does not exist */
            return;
        }

        if (!jobRec.EventReference.IsLoaded)
        {
            jobRec.EventReference.Load();
        }

        eventRec = jobRec.EventReference.Value;
        eventId = eventRec.EventId.ToString();

        if (!eventRec.CustomerReference.IsLoaded)
        {
            eventRec.CustomerReference.Load();
        }

        custRec = eventRec.CustomerReference.Value;
        custId = custRec.CustomerId.ToString();

        rptrClientInfo.DataSource = cfsEntity.Customer.Where("it.CustomerId = " + custId);
        rptrClientInfo.DataBind();

        rptrEventInfo.DataSource = cfsEntity.Event.Where("it.EventId = " + eventId);
        rptrEventInfo.DataBind();

        GetTalentInfo(jobRec, custRec);

        if (jobRec.RespForBalance != null)
        {
            talRec = CfsCommon.GetTalentRecord(cfsEntity, jobRec.RespForBalance.ToString());

            if (talRec != null)
            {
                spanBalResponsible.InnerText = talRec.DisplayName;
            }
        }
       
        rptrJobInfo.DataSource = cfsEntity.Job.Where("it.JobId = " + jobId);
        rptrJobInfo.DataBind();
        bool isCCauth = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.CreditCard, Session);

        
        /* Fill in additional Job Info (Top of Page) */
        lblJobCreatedDate.Text = jobRec.DateTimeCreated.ToString("dddd MMM dd, yyyy h:mm tt");
        lblWorkOrderNum.Text = jobRec.JobId.ToString();

        /* Fill in Event Info (Bottom of Page) */
        lblDayAndDate.Text = eventRec.EventDate.ToString("dddd - MM/dd/yyyy");



        // lblStartTime.Text = CfsCommon.FormatTime(eventRec.StartTime);
    }

    private void GetTalentInfo(Job jobRec, Customer customerRec)
    {
        if (!jobRec.TalentToJob.IsLoaded)
        {
            jobRec.TalentToJob.Load();
        }

        DataTable talTable = new DataTable();

        talTable.Columns.Add("AssocId");
        talTable.Columns.Add("DisplayName");
        talTable.Columns.Add("ContactPhone");
        talTable.Columns.Add("StartTime");
        talTable.Columns.Add("ShowLength");
        talTable.Columns.Add("Payroll");
        talTable.Columns.Add("Email1");
        talTable.Columns.Add("Email2");
        talTable.Columns.Add("ConfirmSent");
        talTable.Columns.Add("TalentId");
        talTable.Columns.Add("CustomerId");


        string[] data = new string[11];
        DateTime? firstStartTime = null;

        foreach (TalentToJob talAssoc in jobRec.TalentToJob)
        {
            if (!talAssoc.TalentReference.IsLoaded)
            {
                talAssoc.TalentReference.Load();
            }

            data[0] = talAssoc.UID.ToString();
            data[1] = talAssoc.TalentReference.Value.DisplayName;
            data[2] = GetTalentContactNum(talAssoc.Talent);
            data[3] = CfsCommon.FormatTime(talAssoc.StartDateTime);
            data[4] = CfsCommon.FormatShowLengthHumanReadable(talAssoc.ShowLengthMins);
            data[5] = talAssoc.Payroll.ToString();
            data[6] = talAssoc.TalentReference.Value.EmailPrimary;
            data[7] = talAssoc.TalentReference.Value.EmailSecondary;
            data[8] = talAssoc.IsConfirmSent.ToString().ToLower();
            data[9] = talAssoc.TalentReference.Value.TalentId.ToString();
            data[10] = customerRec.CustomerId.ToString();

            talTable.LoadDataRow(data, true);


            if (!firstStartTime.HasValue || (firstStartTime.HasValue && firstStartTime.Value > talAssoc.StartDateTime))
            {
                firstStartTime = talAssoc.StartDateTime;
            }
        }

        if (firstStartTime.HasValue)
        {
            lblStartTime.Text = CfsCommon.FormatTime(firstStartTime.Value);
        }

        spanTotalTalent.InnerText = talTable.Rows.Count.ToString();

        rptrTalentInfo.DataSource = talTable;
        rptrTalentInfo.DataBind();
    }

    #region Functions to Choose / Format Certain items on the page
    protected string GetTalentContactNum(Talent talent)
    {
        string contactNum = "";

        if (!string.IsNullOrEmpty(talent.CellPhone))
        {
            contactNum = "Cell: " + talent.CellPhone;        
        }
        else if (!string.IsNullOrEmpty(talent.HomePhone))
        {
            contactNum = "Home: " + talent.HomePhone;
        }

        return contactNum;
    }
    protected string maskCCinfo( string input)
    {
        bool isCCauth = CfsCommon.UserHasSectionAccess((int)CfsCommon.Section.CreditCard, Session);
        if (isCCauth)
        {
            return input;
        }
       
        return "Hidden";
    }
    protected string MaskCCNum( string encryptyedCcNum )
    {
        if (string.IsNullOrEmpty(encryptyedCcNum))
        {
            return "";
        }
        
        string ccNum = CfsCommon.Decrypt(encryptyedCcNum, true);
        string maskCc = "";

        if (string.IsNullOrEmpty(ccNum))
        {
            return "";
        }

        int i=0;
        while(i < ccNum.Length - 4 )
        {
            if (ccNum[i] == '-')
            {
                maskCc += "-";
            }
            else
            {
                maskCc += "X";
            }

            i++;
        }

        if (ccNum.Length - 4 >= 0)
        {
            maskCc += ccNum.Substring(ccNum.Length - 4);
        }

        ccNum = "";
        return maskCc;
    }

    protected string GetMapsUrl(string address1, string address2, string city, string state, string zip)
    {
        string url = "http://maps.google.com/maps?f=q&amp;hl=en&amp;q=";
        string locParam="";

        if (!string.IsNullOrEmpty(address1))
        {
            locParam = address1;
        }

        if (!string.IsNullOrEmpty(address2))
        {
            if (locParam != "")
            {
                locParam += "+";
            }

            locParam += city;
        }

        if (!string.IsNullOrEmpty(city))
        {
            if (locParam != "")
            {
                locParam += "+";
            }

            locParam += city;
        }

        if (!string.IsNullOrEmpty(state))
        {
            if (locParam != "")
            {
                locParam += "+";
            }
            
            locParam += state;
        }

        if (!string.IsNullOrEmpty(zip))
        {
            if (locParam != "")
            {
                locParam += "+";
            }

            locParam += zip;
        }

        url += locParam.Replace(' ', '+');

        return url;
    }
    #endregion

    #region Functions for Email confirms
    private bool GetReferencesForMail(CfsEntity cfsEntity, string custId, out Customer custRec, out Event eventRec, out Job jobRec, out int numTalent, out string errMsg)
    {
        custRec = null;
        eventRec = null;
        jobRec = null;
        errMsg = "";
        numTalent = 0;

        if ((custRec = CfsCommon.GetCustomerRecord(cfsEntity, custId)) == null)
        {
            /* Not expected to happen */
            errMsg = "The Customer record could not be found";
            return false;
        }

        if (!custRec.Event.IsLoaded)
        {
            custRec.Event.Load();
        }

        if (custRec.Event.Count != 1)
        {
            /* Not expected to happen */
            errMsg = "The Event record could not be found";
            return false;
        }

        eventRec = custRec.Event.ToList()[0];

        if (!eventRec.Job.IsLoaded)
        {
            eventRec.Job.Load();
        }

        if (eventRec.Job.Count != 1)
        {
            /* Not expected to happen */
            errMsg = "The Job record could not be found";
            return false;
        }

        jobRec = eventRec.Job.ToList()[0];

        if (!jobRec.TalentToJob.IsLoaded)
        {
            jobRec.TalentToJob.Load();
        }

        numTalent = jobRec.TalentToJob.Count;

        return true;
    }

    private bool GetReferencesForMail(CfsEntity ctx, string customerId,string talentId, out Customer custRec, out Event eventRec, out Job jobRec, out Talent talentRec,out TalentToJob tJobRec, out string msg)
    {
        custRec = null;
        eventRec = null;
        jobRec = null;
        talentRec = null;
        tJobRec = null;
        msg = string.Empty;

        if ((custRec = CfsCommon.GetCustomerRecord(ctx, customerId)) == null)
        {
            msg = "The Customer record could not be found";
            return false;
        }
        if (!custRec.Event.IsLoaded)
            custRec.Event.Load();

        if (custRec.Event.Count != 1)
        {
            msg = "The Event record could not be found";
            return false;
        }

        eventRec = custRec.Event.ToList()[0];

        if (!eventRec.Job.IsLoaded)
            eventRec.Job.Load();

        if (eventRec.Job.Count != 1)
        {
            msg = "The Job record could not be found";
            return false;
        }

        jobRec = eventRec.Job.ToList()[0];

        if ((talentRec = CfsCommon.GetTalentRecord(ctx, talentId)) == null)
        {
            msg = "The Talent record could not be found";
            return false;
        }

        if (!jobRec.TalentToJob.IsLoaded)
            jobRec.TalentToJob.Load();

        Talent tempTalent = talentRec;
        tJobRec = jobRec.TalentToJob.Where(a => a.Talent == tempTalent).FirstOrDefault();
        if (tJobRec == null)
        {
            msg = "The Talent Job record could not be found";
            return false;
        }

        return true;
    }

    private bool SendDancerEmail(string talentId, string customerId, out string msg)
    {
        bool retval = false;

        CfsEntity ctx = new CfsEntity();

        Customer custRec;
        Event evtRec;
        Job jobRec;
        Talent talentRec;
        TalentToJob tJobRec;

        if (!GetReferencesForMail(ctx, customerId, talentId, out custRec, out evtRec, out jobRec, out talentRec,out tJobRec, out msg))
            retval = false;

        StreamReader sRead = new StreamReader(Server.MapPath("~/App_Data/EmailTemplates/dancerConfirm.htm"));
        string emailContent = sRead.ReadToEnd();
        sRead.Close();

        emailContent = emailContent.Replace("#FIRSTNAME#", custRec.FirstName);
        emailContent = emailContent.Replace("#GUESTOFHONOR#", evtRec.GuestOfHonor);
        emailContent = emailContent.Replace("#EVENTDATE#", evtRec.EventDate.ToString("MM/dd/yyyy"));
        emailContent = emailContent.Replace("#TYPEOFEVENT#", evtRec.EventType);
        emailContent = emailContent.Replace("#LOCNAME#", evtRec.LocationName);
        emailContent = emailContent.Replace("#LOCADDRESS1#", evtRec.LocationAddress1);
        emailContent = emailContent.Replace("#LOCADDRESS2#", evtRec.LocationAddress2);
        emailContent = emailContent.Replace("#LOCCITY#", evtRec.LocationCity);
        emailContent = emailContent.Replace("#LOCSTATE#", evtRec.LocationState);
        emailContent = emailContent.Replace("#ZIP#", evtRec.LocationZip);
        emailContent = emailContent.Replace("#STARTTIME#", tJobRec.StartDateTime.ToString("hh:mm tt"));
        emailContent = emailContent.Replace("#PAYROLL#", String.Format("${0}", tJobRec.Payroll));

        MailAddress from = new MailAddress("info@centerfoldstrips.com", "Centerfold Strips");
        MailAddress to;

        if (!CfsCommon.DebugMode())
        {
            try
            {
                to = new MailAddress(talentRec.EmailPrimary);
            }
            catch (FormatException ex)
            {
                msg = String.Format("The Client Email is not formatted correctly. {0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                msg = String.Format("ERROR - Exception: {0}", ex.Message);
                return false;
            }
        }
        else
            to = new MailAddress(CfsCommon.DEBUG_EMAIL);

        string subject = "Your Centerfold Strips Dancer Confirmation";

        if (SendMail(from, to, null, subject, emailContent, out msg))
        {
            tJobRec.IsConfirmSent = true;
            ctx.SaveChanges();
            return true;
        }

        return retval;
    }

    private bool SendConfirmEmail(string customerId, out string errMsg)
    {
        CfsEntity cfsEntity = new CfsEntity();

        Customer custRec;
        Event evtRec;
        Job jobRec;
        int numDancers;

        if (!GetReferencesForMail(cfsEntity, customerId, out custRec, out evtRec, out jobRec, out numDancers, out errMsg))
        {
            return false;
        }
        
        StreamReader sRead = new StreamReader(Server.MapPath("~/App_Data/EmailTemplates/clientConfirm.htm"));
        string emailContent = sRead.ReadToEnd();
        sRead.Close();

        emailContent = emailContent.Replace("#FIRSTNAME#", custRec.FirstName);
        emailContent = emailContent.Replace("#LASTNAME#", custRec.LastName);
        emailContent = emailContent.Replace("#ADDRESS1#", custRec.Address1);
        emailContent = emailContent.Replace("#ADDRESS2#", custRec.Address2);
        emailContent = emailContent.Replace("#CITY#", custRec.City);
        emailContent = emailContent.Replace("#STATE#", custRec.State);
        emailContent = emailContent.Replace("#HOMEPHONE#", custRec.HomePhone);
        emailContent = emailContent.Replace("#BUSPHONE#", custRec.BusinessPhone);
        emailContent = emailContent.Replace("#CELLPHONE#", custRec.CellPhone);
        emailContent = emailContent.Replace("#FAX#", custRec.Fax);
        
        emailContent = emailContent.Replace("#CONTACTPERSON#", evtRec.ContactPerson);
        emailContent = emailContent.Replace("#CONTACTPHONE#", evtRec.ContactPhone);
        emailContent = emailContent.Replace("#GUESTOFHONOR#", evtRec.GuestOfHonor);
        emailContent = emailContent.Replace("#LOCNAME#", evtRec.LocationName);
        emailContent = emailContent.Replace("#LOCPHONE#", evtRec.LocationPhone);
        emailContent = emailContent.Replace("#LOCADDRESS1#", evtRec.LocationAddress1);
        emailContent = emailContent.Replace("#LOCADDRESS2#", evtRec.LocationAddress2);
        emailContent = emailContent.Replace("#LOCADDRESS1#", "");
        emailContent = emailContent.Replace("#LOCADDRESS2#", "");
        emailContent = emailContent.Replace("#LOCCITY#", evtRec.LocationCity);
        emailContent = emailContent.Replace("#LOCSTATE#", evtRec.LocationState);
        emailContent = emailContent.Replace("#EVENTDATE#", evtRec.EventDate.ToString("MM/dd/yyyy"));
        emailContent = emailContent.Replace("#NUMOFGUESTS#", CheckNullAndFormat(evtRec.NumGuests));
        emailContent = emailContent.Replace("#TYPEOFEVENT#", evtRec.EventType);
        
        emailContent = emailContent.Replace("#NUMOFDANCERS#", numDancers.ToString());
        emailContent = emailContent.Replace("#SHOWLENGTH#", CfsCommon.FormatShowLengthHumanReadable( jobRec.TotalShowLengthMins ));

        emailContent = emailContent.Replace("#ENTERTAINMENTFEE#", "$" + jobRec.ChargeForEntertain.ToString());
        emailContent = emailContent.Replace("#LIMOFEE#", "$" + jobRec.ChargeForLimo.ToString());
        emailContent = emailContent.Replace("#LOCATIONFEE#", "$" + jobRec.ChargeForLocation.ToString());
        emailContent = emailContent.Replace("#ACCESSORIESFEE#", "$" + jobRec.ChargeForAccessories.ToString());
        emailContent = emailContent.Replace("#CUSTTOTAL#", "$" + jobRec.GrossIncome.ToString());
        

        MailAddress from = new MailAddress("info@centerfoldstrips.com", "Centerfold Strips");
        MailAddress to;

        if (!CfsCommon.DebugMode())
        {
            /* Normal Mode - NOT Debug */
            try
            {
                to = new MailAddress(custRec.Email);
            }
            catch (System.FormatException ex)
            {
                errMsg = "The Client Email is not formatted correctly. ";
                errMsg += ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                errMsg = "ERROR - Exception: " + ex.Message;
                return false;
            }
        }
        else
        {
            /* Send to Debug Address */
            to = new MailAddress(CfsCommon.DEBUG_EMAIL);
        }

        string subject = "Your Centerfold Strips Event Confirmation";

        if( SendMail(from, to, null, subject, emailContent, out errMsg) )
        {
            custRec.IsCustConfirmSent = true;
            cfsEntity.SaveChanges();

            return true;
        }

        return false;
    }

    private bool SendMail(MailAddress emailFrom, MailAddress emailTo, MailAddress emailCC, string subject, string body, out string errMsg)
    {
        errMsg = "";

        MailMessage msg = new MailMessage();

        msg.From = emailFrom;
        msg.To.Add(emailTo);

        if (emailCC != null)
        {
            msg.CC.Add(emailCC);
        }

        msg.Subject = subject;
        msg.IsBodyHtml = true;
        msg.Body = body;

        SmtpClient theClient = new SmtpClient(CfsCommon.SMTP_SERVER);

        try
        {
            theClient.Send(msg);
            return true;
        }
        catch (SmtpFailedRecipientsException ex)
        {
            errMsg = "ERROR: SmtpFailedRecipientsException:" + ex.Message;
        }
        catch (SmtpException ex)
        {
            errMsg = "ERROR: SmtpException:" + ex.Message;
        }
        catch (Exception ex)
        {
            errMsg = "ERROR: Exception:" + ex.Message;
        }

        return false;
    }

    private string CheckNullAndFormat(object val)
    {
        if (val == null)
        {
            return "";
        }

        return val.ToString();
    }

    private string CheckNullAndFormatTime(object val)
    {
        if (val == null)
        {
            return "";
        }

        return " at " + ((DateTime)val).ToString("h:mm tt");
    }
    #endregion

    protected void OnClick_btnCloneJobInfo(object sender, EventArgs e)
    {
       string jobid = Request.QueryString["jobid"];
       if (jobid != null)
       {
           Response.Redirect("add_edit_clone_event.aspx?jobid=" + jobid);
       }
    }
}
