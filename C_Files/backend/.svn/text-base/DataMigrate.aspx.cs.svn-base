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

using System.Data.SqlClient;
using System.Collections;

using CfsNamespace; //FRESH NEW DB
using CfsNamespaceOld; //Old DB

//This page sole purpose to provide data migration from old DB to new DB. 

public partial class backend_DataMigrate : System.Web.UI.Page
{
    /* LOCAL Dev and Testing */
    private string SQL_CONN_STRING_NEW_DB = "Data Source=NEWTRINO;Initial Catalog=CenterfoldNew;Persist Security Info=True;User ID=sa;Password=iHKybd!;";
    private string SQL_CONN_STRING_OLD_DB = "Data Source=NEWTRINO;Initial Catalog=Centerfold;Persist Security Info=True;User ID=sa;Password=iHKybd!;";

    /* REMOTE - Live Server */
    //private string SQL_CONN_STRING_NEW_DB = "Data Source=DED1138\\sqlexpress;Initial Catalog=CenterfoldNew;Persist Security Info=True;User ID=sa;Password=DeadCats1;";
    //private string SQL_CONN_STRING_OLD_DB = "Data Source=DED1138\\sqlexpress;Initial Catalog=Centerfold;Persist Security Info=True;User ID=sa;Password=DeadCats1;";


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Stuff to Migrate Talent Records (Talent, TalentWorksIn Tables)
    protected void OnClick_btnMigrateTalent(object sender, EventArgs e)
    {
        SqlConnection sqlConn = new SqlConnection(SQL_CONN_STRING_NEW_DB);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;

        CfsEntityOld oldData = new CfsEntityOld();
        string insert, insertWin;
        sqlConn.Open();

        RunSqlCmd(sqlConn, "SET IDENTITY_INSERT Talent ON");

        //Create Empty record, for Talent that no longer exists
        sqlCmd.CommandText  = "INSERT INTO Talent(IsActive,IsFeatureTalent,TalentType,DoesToys,DoesFullNudeStrip,DoesLesbianShow,DoesPromoModeling,";
        sqlCmd.CommandText += "DoesToplessBartender,DoesPoleDancing,DoesToplessStrip,DoesToplessWaiting,DoesPopoutCake,DoesInternetChat,";
        sqlCmd.CommandText += "DoesToplessCardDealing,DoesLapdancing,DateCreated,DateLastUpdate,TalentId,FirstName,LastName,DisplayName) ";
        sqlCmd.CommandText += "VALUES('false','false','none','false','false','false','false','false','false','false','false','false','false','false',";
        sqlCmd.CommandText += "'false',GetDate(),GetDate(),0,'NONE','NONE','Talent Removed from DB');";
        sqlCmd.ExecuteNonQuery();

        foreach (CfsNamespaceOld.Talent oldRecord in oldData.Talent)
        {
            insert = CreateTalentInsert(oldRecord);
            insertWin = CreateTalentInsertWorksIn(oldRecord);

            try
            {
                sqlCmd.CommandText = insert;
                sqlCmd.ExecuteNonQuery();

                if (insertWin != "")
                {
                    sqlCmd.CommandText = insertWin;
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                divErrorLog.Style["color"] = "red";
                divErrorLog.InnerHtml += "<p>Old Record UID#: " + oldRecord.UID.ToString() + ": " + ex.Message + ":" + ex.InnerException + "<p>\r\n";
                return;                
            }

        }

        RunSqlCmd(sqlConn, "SET IDENTITY_INSERT Talent OFF");

        sqlConn.Close();
    }

    private string CreateTalentInsert(CfsNamespaceOld.Talent oldRecord)
    {
        string talentType;
        string stmt, vals;
        int tmpInt;

        stmt = "INSERT INTO Talent(";
        vals = "VALUES(";

        stmt += "TalentId,";
        vals += "" + oldRecord.UID.ToString() + ",";

        stmt += "IsActive,";

        if ((bool)oldRecord.Active)
        {
            vals += "'true',";
        }
        else
        {
            vals += "'false',";
        }

        if (oldRecord.Type == "mini")
        {
            talentType = "minifemale";
        }
        else
        {
            talentType = oldRecord.Type;
        }

        stmt += "TalentType,";
        vals += "'" + talentType + "',";
        stmt += "FirstName,";
        vals += "'" + oldRecord.FirstName + "',";
        stmt += "LastName,";
        vals += "'" + oldRecord.LastName + "',";
        stmt += "StageName,";
        vals += "'" + oldRecord.StageName + "',";
        stmt += "EmailPrimary,";
        vals += "'" + oldRecord.Email + "',";
        stmt += "Address1,";
        vals += "'" + oldRecord.Address + "',";
        stmt += "City,";
        vals += "'" + oldRecord.City + "',";

        //Remap Location, field "state" in old DB used inconsistently
        if (oldRecord.State.Length <= 2)
        {
            stmt += "State,";
            vals += "'" + oldRecord.State + "',";
            stmt += "Country,";
            vals += "'United States',";
        }
        else
        {
            stmt += "State,";
            vals += "'',";
            stmt += "Country,";
            vals += "'" + oldRecord.State + "',";
        }

        stmt += "DisplayName,";
        vals += "'" + CfsCommon.FormatDisplayName(talentType, oldRecord.FirstName, oldRecord.LastName, oldRecord.StageName, oldRecord.State) + "',";
        stmt += "Zip,";
        vals += "'" + oldRecord.Zip + "',";
        stmt += "HomePhone,";
        vals += "'" + oldRecord.PriPhone + "',"; //IS THIS THE RIGHT MAPPING ?!
        stmt += "CellPhone,";
        vals += "'" + oldRecord.SecPhone + "',"; //IS THIS THE RIGHT MAPPING ?!
        stmt += "PersonalSite,";
        vals += "'" + oldRecord.WebSite + "',";
        stmt += "SpecialNotes,";
        vals += "'" + oldRecord.SpecialNotes + "',";
        
        /* Removing Talent images, start from 'Scratch', per client request.
        stmt += "ImageOne,";
        vals += "'" + oldRecord.Image1 + "',";
        stmt += "ImageTwo,";
        vals += "'" + oldRecord.Image2 + "',";
        stmt += "ThumbImg,";
        vals += "'" + oldRecord.ThumbImage + "',";
        */

        stmt += "ImageOne,";
        vals += "'',";
        stmt += "ImageTwo,";
        vals += "'',";
        stmt += "ThumbImg,";
        vals += "'',";
        stmt += "Bust,";
        vals += "'" + oldRecord.Bust + "',";
        stmt += "Waist,";
        vals += "'" + oldRecord.Waist + "',";
        stmt += "Hips,";
        vals += "'" + oldRecord.Hips + "',";

        if (!string.IsNullOrEmpty(oldRecord.HeightFt))
        {
            stmt += "HeightFt,";
            vals += "'" + oldRecord.HeightFt + "',";
        }

        if (!string.IsNullOrEmpty(oldRecord.HeightIn))
        {
            stmt += "HeightIn,";
            vals += "'" + oldRecord.HeightIn + "',";
        }

        if (int.TryParse(oldRecord.Weight, out tmpInt))
        {
            stmt += "Weight,";
            vals += tmpInt + ",";
        }

        if (oldRecord.DOB != null)
        {
            stmt += "DOB,";
            vals += "'" + ((DateTime)oldRecord.DOB).ToString("yyyy-MM-dd") + "',";
        }

        stmt += "Race,";
        vals += "'" + oldRecord.Race + "',";

        stmt += "IsFeatureTalent,";
        vals += "'" + oldRecord.Feature.ToString() + "',";

        stmt += "DateCreated,";
        vals += "'" + ((DateTime)oldRecord.DateCreate).ToString("yyyy-MM-dd") + "',";

        if (oldRecord.Updated != null)
        {
            stmt += "DateLastUpdate";
            vals += "'" + ((DateTime)oldRecord.Updated).ToString("yyyy-MM-dd") + "'";
        }
        else
        {
            stmt += "DateLastUpdate";
            vals += "'1900-01-01'";
        }

        stmt += ") ";
        vals += "); ";

        return stmt + " " + vals;
    }

    private string CreateTalentInsertWorksIn(CfsNamespaceOld.Talent oldRecord)
    {
        string stmt = "INSERT INTO TalentWorksIn(";
        string vals = "VALUES(";

        if (oldRecord.State.Length > 2)
        {
            return "";
        }

        stmt += "TalentId,";
        vals += oldRecord.UID + ",";

        stmt += "State,";
        vals += "'" + oldRecord.State + "',";

        stmt += "Country)";
        vals += "'United States')";

        return stmt + " " + vals;
    }    
    #endregion

    #region Stuff to Migrate Pending Jobs
    protected void OnClick_btnMigratePendJobs(object sender, EventArgs e)
    {
        string recordId;
        CfsEntityOld oldData = new CfsEntityOld();
        CfsEntity newData = new CfsEntity();

        CfsNamespace.Pending newRecord;

        ObjectQuery<CfsNamespaceOld.Pending> queryOldData = oldData.Pending.OrderBy("it.UID");

        foreach (CfsNamespaceOld.Pending oldRecord in queryOldData)
        {
            recordId = oldRecord.UID.ToString();
            newRecord = new CfsNamespace.Pending();

            newRecord.PendId = oldRecord.UID;
            newRecord.ClientName = oldRecord.CustomerName;
            newRecord.CityStateZip = oldRecord.Location;
            newRecord.ContactNumber = oldRecord.Phone;
            newRecord.EventDate = (DateTime)oldRecord.EventDate;
            newRecord.EventType = oldRecord.PartyType;
            newRecord.Notes = oldRecord.Notes;

            try
            {
                newData.AddToPending(newRecord);
                newData.SaveChanges();
            }
            catch (Exception ex)
            {
                divErrorLog.Style["color"] = "red";
                divErrorLog.InnerHtml += "<p>Record#: " + recordId.ToString() + ": " + ex.Message + ":" + ex.InnerException + "<p>\r\n";
                return;
            }

        }

    }    
    #endregion

    #region Stuff to Migrate Customer, Event, Jobs Tables
    protected void OnClick_btnMigrateCustEventsJobs(object sender, EventArgs e)
    {
        string select = "SELECT * FROM Customers c,Event e,Jobs j WHERE c.CID = e.CusID AND e.EID = j.EventId ORDER BY JID;";
        int recordId = -1;

        SqlConnection sqlConnOld = new SqlConnection(SQL_CONN_STRING_OLD_DB);
        SqlCommand sqlCmdOld = new SqlCommand(select, sqlConnOld);
        SqlConnection sqlConnNew = new SqlConnection(SQL_CONN_STRING_NEW_DB);
        SqlCommand sqlCmdNew = new SqlCommand();
        sqlCmdNew.Connection = sqlConnNew;

        SqlDataReader sqlRead;

        CfsEntity newData = new CfsEntity();

        CfsNamespace.Customer newCust;
        CfsNamespace.Event newEvent;

        string insertJob;

        try
        {
            sqlConnOld.Open();
            sqlConnNew.Open();
            sqlRead = sqlCmdOld.ExecuteReader();

            RunSqlCmd(sqlConnNew, "SET IDENTITY_INSERT Job ON");
            while (sqlRead.Read())
            {
                recordId = (int)sqlRead["JID"];

                newCust = CopyCustomerInfo(sqlRead);
                newEvent = CopyEventInfo(sqlRead);

                newData.AddToCustomer(newCust);
                newCust.Event.Add(newEvent);

                newData.SaveChanges();

                insertJob = CreateJobInsert(sqlRead, newEvent.EventId.ToString());


                sqlCmdNew.CommandText = insertJob;
                sqlCmdNew.ExecuteNonQuery();
            }
            RunSqlCmd(sqlConnNew, "SET IDENTITY_INSERT Job OFF");
        }
        catch (Exception ex)
        {
            divErrorLog.Style["color"] = "red";
            divErrorLog.InnerHtml += "<p>Record JobId #: " + recordId.ToString() + ": " + ex.Message + ":" + ex.InnerException + "<p>\r\n";

            RunSqlCmd(sqlConnNew, "SET IDENTITY_INSERT Job OFF");

            sqlConnOld.Close();
            sqlConnNew.Close();
            return;
        }

        sqlConnOld.Close();
        sqlConnNew.Close();
    }

    private CfsNamespaceOld.Event GetOldEvent(CfsEntityOld oldData, string eventId)
    {
        List<CfsNamespaceOld.Event> evtList = ((ObjectQuery<CfsNamespaceOld.Event>)oldData.Event.Where("it.EID = " + eventId)).ToList();

        if (evtList.Count != 1)
        {
            return null;
        }

        return evtList[0];
    }

    private CfsNamespaceOld.Customers GetOldCustomer(CfsEntityOld oldData, string custId)
    {
        List<CfsNamespaceOld.Customers> list = ((ObjectQuery<CfsNamespaceOld.Customers>)oldData.Customers.Where("it.CID = " + custId)).ToList();

        if (list.Count != 1)
        {
            return null;
        }

        return list[0];
    }

    private CfsNamespace.Customer CopyCustomerInfo(SqlDataReader oldRecord)
    {
        CfsNamespace.Customer newRecord = new Customer();

        newRecord.CustomerId = (int)oldRecord["CID"];
        newRecord.FirstName = CastStringIfNotNull(oldRecord["CFirstName"]);
        newRecord.LastName = CastStringIfNotNull(oldRecord["CLastName"]);
        newRecord.Address1 = CastStringIfNotNull(oldRecord["CStreet"]);
        //No Address2 in oldRecord
        newRecord.City = CastStringIfNotNull(oldRecord["CCity"]);
        newRecord.State = CastStringIfNotNull(oldRecord["CState"]);
        newRecord.Zip = CastStringIfNotNull(oldRecord["CZip"]);
        newRecord.HomePhone = CastStringIfNotNull(oldRecord["CHome"]);
        newRecord.CellPhone = CastStringIfNotNull(oldRecord["CCell"]);
        newRecord.BusinessPhone = CastStringIfNotNull(oldRecord["CBusiness"]);
        newRecord.Fax = CastStringIfNotNull(oldRecord["CFax"]);
        newRecord.AltContactName = CastStringIfNotNull(oldRecord["CAlternateName"]);
        newRecord.AltContactPhone = CastStringIfNotNull(oldRecord["CAlternate"]);
        newRecord.Email = CastStringIfNotNull(oldRecord["CEmail"]);
        newRecord.ReferredBy = CastStringIfNotNull(oldRecord["CHear"]);

        return newRecord;
    }

    private string CastStringIfNotNull(object toCast)
    {
        return toCast.ToString();

        if (toCast == null)
        {
            return null;
        }

        return (string)toCast;
    }

    private CfsNamespace.Event CopyEventInfo(SqlDataReader oldRecord)
    {
        DateTime dt;

        CfsNamespace.Event newRecord = new CfsNamespace.Event();

        newRecord.ContactPerson = CastStringIfNotNull(oldRecord["EContact"]);
        newRecord.ContactPhone = CastStringIfNotNull(oldRecord["EContactPhone"]);
        newRecord.GuestOfHonor = CastStringIfNotNull(oldRecord["EHonor"]);
        newRecord.LocationName = CastStringIfNotNull(oldRecord["ELocation"]);
        newRecord.LocationAddress1 = CastStringIfNotNull(oldRecord["EStreet"]);
        newRecord.LocationAddress2 = ""; /* No Address 2 in old record */
        newRecord.LocationCity = CastStringIfNotNull(oldRecord["ECity"]);
        newRecord.LocationState = CastStringIfNotNull(oldRecord["EState"]);
        newRecord.LocationZip = CastStringIfNotNull(oldRecord["EZip"]);
        newRecord.LocationCountry = CastStringIfNotNull("UNITED STATES");
        newRecord.LocationPhone = CastStringIfNotNull(oldRecord["EPhone"]);

        if (oldRecord["EGuests"].GetType().Name != "DBNull")
        {
            newRecord.NumGuests = (short)oldRecord["EGuests"];
        }

        newRecord.AgeRange = CastStringIfNotNull(oldRecord["ERange"]);
        newRecord.EventType = CastStringIfNotNull(oldRecord["EType"]);

        if (oldRecord["ESuprise"].GetType().Name == "Boolean")
        {
            newRecord.IsSurpriseParty = (bool)oldRecord["ESuprise"];
        }
        else
        {
            newRecord.IsSurpriseParty = false;
        }

        newRecord.EventDate = (DateTime)oldRecord["EDate"];

        Type testType = oldRecord["EStart"].GetType();
        Type testType2 = oldRecord["EEnd"].GetType();

        if (oldRecord["EStart"].GetType().Name == "String")
        {
            if (DateTime.TryParse((string)oldRecord["EStart"], out dt) &&
                DateTime.TryParse("1900-01-01 " + (string)oldRecord["EStart"], out dt))
            {
                newRecord.StartTime = dt;
            }
        }

        if (oldRecord["EEnd"].GetType().Name == "String")
        {
            if (DateTime.TryParse((string)oldRecord["EEnd"], out dt) &&
                DateTime.TryParse("1900-01-01 " + (string)oldRecord["EEnd"], out dt))
            {
                newRecord.EndTime = dt;
            }
        }

        return newRecord;
    }

    private string CreateJobInsert(SqlDataReader oldRecord, string newEventId)
    {
        /* TO DO - New Vars from Ticket Change requests */

        int preparedBy;
        string stmt = "INSERT INTO Job(";
        string vals = "VALUES(";

        DateTime dtCreated = (DateTime)oldRecord["DateCreated"]; /* Never NULL in old DB */
        string dtTimeStr = dtCreated.ToString("yyyy-MM-dd");

        Type testType = oldRecord["TimeCreated"].GetType();

        if (oldRecord["TimeCreated"].GetType().Name == "DateTime")
        {
            DateTime timeCreated = (DateTime)oldRecord["TimeCreated"];
            dtTimeStr += " " + timeCreated.ToString("hh:mm tt");
        }
        else
        {
            dtTimeStr += " 12:00 AM";
        }

        stmt += "JobId,";
        vals += oldRecord["JID"].ToString() + ",";

        stmt += "EventId,";
        vals += newEventId + ",";

        stmt += "DateTimeCreated,";
        vals += "'" + dtTimeStr + "',";

        stmt += "IsJobComplete,";
        vals += "'" + ((bool)oldRecord["JobDone"]).ToString() + "',";

        stmt += "IsJobCancelled,";
        vals += "'" + ((bool)oldRecord["Cancel"]).ToString() + "',";

        stmt += "IsBalanceCollected,";
        vals += "'" + ((bool)oldRecord["Balance"]).ToString() + "',";

        stmt += "SpecialInstructions,";
        vals += "'" + oldRecord["Special"] + "',";

        stmt += "CCTypeBrand,";
        vals += "'" + oldRecord["CCType"] + "',";

        stmt += "CCTypeCreditOrDebit," + "',";
        vals += "'Credit',";

        stmt += "CCName,";
        vals += "'" + oldRecord["CCName"] + "',";

        if (oldRecord["CCNum"] != null)
        {
            string encrypt = CfsCommon.Encrypt((string)oldRecord["CCNum"], true);

            stmt += "CCNum,";
            vals += "'" + encrypt + "',";
        }

        stmt += "CCcv2Num,";
        vals += "'" + oldRecord["CV2Num"] + "',";

        stmt += "CCExp,";
        vals += "'" + oldRecord["CCExp"] + "',";

        /* Charge and Expenses */
        stmt += "ChargeForAccessories,";
        vals += oldRecord["MiscExp"] + ",";

        stmt += "ChargeForEntertain,";
        vals += oldRecord["Subtotal"] + ",";

        stmt += "ChargeForLimo,";
        vals += oldRecord["Limo"] + ",";

        stmt += "ChargeForLocation,";
        vals += oldRecord["PartyFee"] + ",";

        stmt += "IsChargeNetToCC,";
        vals += "'false',";

        stmt += "ExpenseTalent,";
        vals += "0,";

        stmt += "ExpenseGratuity,";
        vals += "0,";

        stmt += "ExpenseSecurity,";
        vals += "0,";

        stmt += "ExpenseReferral,";
        vals += "0,";

        stmt += "ExpenseSales,";
        vals += "0,";

        stmt += "ExpenseTotal,";
        vals += "0,";

        stmt += "GrossIncome,";
        vals += oldRecord["Total"] + ",";
        
        stmt += "OfficeNet,";
        vals += oldRecord["BalanceDue"] + ",";

        switch ((string)oldRecord["PreparedBy"])
        {
            case "John": { preparedBy = 1; break; }
            case "Dan": { preparedBy = 2; break; }
            case "Dave": { preparedBy = 3; break; }
            case "Marc": { preparedBy = 4; break; }
            case "Kevin": { preparedBy = 5; break; }
            default:
            {
                /* Don't expect this to happen */
                preparedBy = -1;
                break;
            }
        }

        stmt += "PreparedBy,";
        vals += preparedBy.ToString() + ",";


        if (oldRecord["Responsible"].GetType().Name != "DBNull")
        {
            /* Zero is reserved, field should be null if none selected */
            if ((int)oldRecord["Responsible"] != 0)
            {
                stmt += "RespForBalance,";
                vals += "'" + ((int)oldRecord["Responsible"]).ToString() + "',";
            }
        }
        
        stmt += "TotalShowLengthMins";
        vals += "0";


        stmt += ")";
        vals += ")";

        return stmt + " " + vals;
    }

    
    #endregion

    #region Stuff to Migrate Talent To Job
    protected void OnClick_btnMigrateTalToJob(object sender, EventArgs e)
    {
        CfsNamespace.CfsEntity newData = new CfsEntity();
        CfsNamespaceOld.CfsEntityOld oldData = new CfsEntityOld();
        
        List<CfsNamespace.TalentToJob> talToJobList;

        foreach (CfsNamespaceOld.Jobs oldJob in oldData.Jobs)
        {
            talToJobList = CreateTalToJobList(newData, oldJob);
            foreach (TalentToJob tJob in talToJobList)
            {
                newData.AddToTalentToJob(tJob);
            }

            try
            {
                newData.SaveChanges();
            }
            catch (Exception ex)
            {
                divErrorLog.Style["color"] = "red";
                divErrorLog.InnerHtml += "<p>JobId: " + oldJob.JID.ToString() + ": " + ex.Message + ":" + ex.InnerException + "<p>\r\n";
                return;
            }
        }
    }

    private CfsNamespaceOld.Jobs GetOldJob(CfsEntityOld oldData, int jobId)
    {
        List<Jobs> list = ((ObjectQuery<Jobs>)oldData.Jobs.Where("it.JID = " + jobId.ToString())).ToList();

        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null;
        }
    }

    private List<CfsNamespace.TalentToJob> CreateTalToJobList(CfsEntity cfsEntity, CfsNamespaceOld.Jobs oldJob )
    {
        List<CfsNamespace.TalentToJob> list = new List<TalentToJob>(8);
        TalentToJob talToJob;

        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer1, oldJob.Start1, oldJob.Pay1)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer2, oldJob.Start2, oldJob.Pay2)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer3, oldJob.Start3, oldJob.Pay3)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer4, oldJob.Start4, oldJob.Pay4)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer5, oldJob.Start5, oldJob.Pay5)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer6, oldJob.Start6, oldJob.Pay6)) != null)
        {
            list.Add(talToJob);
        }
        if ((talToJob = TryCreateTalObject(cfsEntity, oldJob.JID, oldJob.Dancer7, oldJob.Start7, oldJob.Pay7)) != null)
        {
            list.Add(talToJob);
        }

        return list;
    }

    private CfsNamespace.TalentToJob TryCreateTalObject(CfsEntity cfsEntity, int jobId, short? key, string startTime, short? pay)
    {
        if (key == null || key == 0)
        {
            /* Not a valid Dancer */
            return null;
        }

        CfsNamespace.TalentToJob talToJob = new TalentToJob();
        DateTime dt;

        int talentId = (int)key;
        talToJob.TalentReference.Value = CfsCommon.GetTalentRecord(cfsEntity, talentId.ToString());

        if (talToJob.TalentReference.Value == null)
        {
            talToJob.TalentReference.Value = CfsCommon.GetTalentRecord(cfsEntity, "0");
        }

        talToJob.JobReference.Value = CfsCommon.GetJobRecord(cfsEntity, jobId.ToString());

        if( pay == null )
        {
            talToJob.Payroll = 0;
        }
        else
        {
            talToJob.Payroll = (Int16)pay;
        }

        if (startTime != "")
        {
            if (DateTime.TryParse("1900-01-01 " + startTime, out dt))
            {
                talToJob.StartDateTime = dt;
            }
            else
            {
                talToJob.StartDateTime = DateTime.Parse("1900-01-01 12:00 AM");
            }
        }
        else
        {
            talToJob.StartDateTime = DateTime.Parse("1900-01-01 12:00 AM");
        }
        
        talToJob.ShowLengthMins = 0;
        
        return talToJob;
    }

    private bool DoesTalExist(SqlConnection newDbConn, int talId)
    {
        SqlCommand cmd = new SqlCommand("SELECT TalentId FROM Talent WHERE TalentId = " + talId.ToString(), newDbConn);

        SqlDataReader sRead = cmd.ExecuteReader();

        bool retVal = sRead.HasRows;

        sRead.Close();

        return retVal;
    }

    private CfsNamespaceOld.Event FindEvent(CfsEntityOld oldData, string custId)
    {
        List<CfsNamespaceOld.Event> list = ((ObjectQuery<CfsNamespaceOld.Event>)oldData.Event.Where("it.CusID = " + custId)).ToList();

        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null;
        }
    }

    
    #endregion

    #region Stuff to Migrate Applicants
    protected void OnClick_btnMigrateApplicants(object sender, EventArgs e)
    {
        CfsEntity newData = new CfsEntity();
        CfsEntityOld oldData = new CfsEntityOld();
        CfsNamespace.Applicant newApp;

        foreach (CfsNamespaceOld.Applicants oldApp in oldData.Applicants)
        {
            newApp = new Applicant();

            if (oldApp.PContact != null){ newApp.HasBeenContacted = oldApp.PContact; } else { newApp.HasBeenContacted = false; }
            if (oldApp.PSubmit != null) { newApp.DateApplied = (DateTime)oldApp.PSubmit; } else { newApp.DateApplied = DateTime.Parse("1900-01-01 12:00 AM"); }

            newApp.Status = CfsCommon.APPLICANT_STATUS_NEW;
            newApp.FirstName = oldApp.PFirstName;
            newApp.LastName = oldApp.PLastName;
            newApp.StageName = oldApp.PStageName;
            newApp.Email = oldApp.PEmail;
            newApp.Website = oldApp.PWebsite;
            newApp.Address1 = oldApp.PAddress;
            newApp.Address2 = ""; /* No Address2 in Old Record */
            newApp.City = oldApp.PCity;
            newApp.State = oldApp.PState;
            newApp.Country = "United States";
            newApp.Zip = oldApp.PZip;

            if (oldApp.PHomePhone.Length > 20)
            {
                newApp.HomePhone = oldApp.PHomePhone.Remove(20);
            }
            else
            {
                newApp.HomePhone = oldApp.PHomePhone;
            }

            
            newApp.CellPhone = oldApp.PCellPhone;
            newApp.AltPhone = ""; /* No Alt Phone in Old Record */

            newApp.HeightFt = oldApp.PHeightFt;
            newApp.HeightIn = oldApp.PHeightIn;
            newApp.Weight = oldApp.PWeight;
            newApp.EyeColor = oldApp.PEyes.Trim();
            newApp.HairColor = oldApp.PHair.Trim();
            newApp.Experience = oldApp.PExperience;
            newApp.DaysAvail = oldApp.PAvailable;
            newApp.DaysPrefer = oldApp.PPreference;
            newApp.Bust = oldApp.PBust;
            newApp.Hips = oldApp.PHips;
            newApp.DOB = null; /* No DOB in Old Record */

            newApp.ImageOne = oldApp.Image1;
            newApp.ImageTwo = oldApp.Image2;
            newApp.ImageThree = oldApp.Image3;

            if (oldApp.PWaist != null)
            {
                newApp.Waist = oldApp.PWaist.ToString();
            }

            newData.AddToApplicant(newApp);
            newData.SaveChanges();
        }
    }
    
    #endregion

    protected void OnClick_btnUpdateTalPayroll(object sender, EventArgs e)
    {
        int total;
        CfsEntity cfsEntity = new CfsEntity();

        Hashtable expTotal = new Hashtable(5000);

        foreach (CfsNamespace.Job newJob in cfsEntity.Job.Include("TalentToJob"))
        {
            total = 0;
            foreach (TalentToJob tJob in newJob.TalentToJob)
            {
                total += tJob.Payroll;
            }

            expTotal[newJob.JobId] = total;
        }

        foreach (CfsNamespace.Job newJob in cfsEntity.Job)
        {
            newJob.ExpenseTotal = (int)expTotal[newJob.JobId];
        }

        cfsEntity.SaveChanges();
    }

    #region Clear Functions
    protected void OnClick_btnClearCustEventsJobs(object sender, EventArgs e)
    {
        RunSqlCmd("DELETE FROM TalentToJob");
        RunSqlCmd("DELETE FROM Job");
        RunSqlCmd("DELETE FROM Event");
        RunSqlCmd("DELETE FROM Customer");

        divErrorLog.InnerHtml = "";
    }

    protected void OnClick_btnClearTalent(object sender, EventArgs e)
    {
        RunSqlCmd("DELETE FROM TalentHold");
        RunSqlCmd("DELETE FROM TalentCredit");
        RunSqlCmd("DELETE FROM TalentWorksIn");
        RunSqlCmd("DELETE FROM TalentAltPhone");
        RunSqlCmd("DELETE FROM Talent");

        divErrorLog.InnerHtml = "";
    }

    protected void OnClick_btnClearPendJobs(object sender, EventArgs e)
    {
        RunSqlCmd("DELETE FROM Pending");

        divErrorLog.InnerHtml = "";
    }

    protected void OnClick_btnClearApplicants(object sender, EventArgs e)
    {
        RunSqlCmd("DELETE FROM Applicant");

        divErrorLog.InnerHtml = "";
    }

    protected void OnClick_btnClearTalToJob(object sender, EventArgs e)
    {
        RunSqlCmd("DELETE FROM TalentToJob");

        divErrorLog.InnerHtml = "";
    }
    #endregion

    protected void OnClick_btnUpdateTalDisplayName(object sender, EventArgs e)
    {
        CfsEntity cfsEntity = new CfsEntity();
        CfsNamespace.Talent talRec;

        List<CfsNamespace.Talent> talList = ((ObjectQuery<CfsNamespace.Talent>)cfsEntity.Talent).ToList();

        foreach (CfsNamespace.Talent tal in talList)
        {
            talRec = CfsCommon.GetTalentRecord(cfsEntity, tal.TalentId.ToString());
            
            talRec.DisplayName = CfsCommon.FormatDisplayName(tal.TalentType, tal.FirstName, tal.LastName, tal.StageName, tal.State);

            cfsEntity.SaveChanges();
        }
    }

    private void RunSqlCmd(string command)
    {
        SqlConnection sqlConn = new SqlConnection(SQL_CONN_STRING_NEW_DB);
        SqlCommand sqlCmd = new SqlCommand(command, sqlConn);

        sqlConn.Open();
        sqlCmd.ExecuteNonQuery();
        sqlConn.Close();
    }

    private void RunSqlCmd(SqlConnection sqlConn, string command)
    {
        SqlCommand sqlCmd = new SqlCommand(command, sqlConn);

        sqlCmd.ExecuteNonQuery();
    }

    protected void OnClick_btnUpdateTalWorksIn(object sender, EventArgs e)
    {
        CfsEntity cfsEntity = new CfsEntity();
        CfsNamespace.Talent talRec;

        List<CfsNamespace.Talent> talList = ((ObjectQuery<CfsNamespace.Talent>)cfsEntity.Talent).ToList();

        foreach (CfsNamespace.Talent tal in talList)
        {
            talRec = CfsCommon.GetTalentRecord(cfsEntity, tal.TalentId.ToString());

            if (!talRec.TalentWorksIn.IsLoaded)
            {
                talRec.TalentWorksIn.Load();
            }

            talRec.WorksInList = "";
            foreach (TalentWorksIn worksIn in talRec.TalentWorksIn)
            {
                if (talRec.WorksInList.Length == 0)
                {
                    talRec.WorksInList = worksIn.State;
                }
                else
                {
                    talRec.WorksInList += ", " + worksIn.State;
                }
            }

            cfsEntity.SaveChanges();
        }    
    }

            





}

