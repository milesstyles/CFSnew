using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.ComponentModel;
using System.Collections;
using System.Web.SessionState;

using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;

using CfsNamespace;


/// <summary>
/// Summary description for Common
/// </summary>
public class CfsCommon
{
    #region Configuration
    public const bool DEBUG_MODE = false;
    public const string SMTP_SERVER = "localhost";
    public const string DEBUG_EMAIL = "kdeshane@theatomgroup.com";
    #endregion

    #region SystemWide Consts
    public const int ERROR_VAL = -1;
    public const string ONLINE_BOOKING_TIME_OTHER = "05:00 AM";

    public const string APP_SETTING_KEY_PROMO_URL = "PromoSpecialUrl";
    public const string APP_SETTING_KEU_PROMO_IMGSRC = "PromoSpecialImg";
    public const string APP_SETTING_KEU_LOGO_IMGSRC = "LogoSpecialImg";
    public const string APP_SETTING_KEY_LOGO_TEXT = "LogoText";

    public const string PARAM_CUSTOMER_ID = "custid";
    public const string PARAM_EVENT_ID = "eventid";
    public const string PARAM_JOB_ID = "jobid";
    public const string PARAM_UPDATE_MODE = "editmode";

    public const string PARAM_WO_SEARCH_WO_NUM = "worknum";
    public const string PARAM_WO_SEARCH_FIRST_NAME = "first";
    public const string PARAM_WO_SEARCH_LAST_NAME = "last";
    public const string PARAM_WO_SEARCH_JOB_DATE = "date";
    public const string PARAM_WO_SEARCH_LOCATION = "location";
    public const string PARAM_WO_SEARCH_CITY = "city";
    public const string PARAM_WO_SEARCH_STATE = "state";
    public const string PARAM_WO_SEARCH_CCNUM = "ccnum";
    public const string PARAM_WO_SEARCH_TALID = "tid";
    public const string PARAM_WO_SEARCH_REFER = "refby";
    public const string PARAM_WO_SEARCH_EVENT_TYPE = "eventtype";

    public const string SESSION_KEY_USERID = "userid";
    public const string SESSION_KEY_USERNAME = "user";
    public const string SESSION_KEY_LOGGED_IN = "loggedin";
    public const string SESSION_KEY_ACCESS_TABLE = "accessTable";

    public const string LOGIN_PAGE = "login.aspx";
    
    public const string MODE_ADD = "0";
    public const string MODE_READONLY = "1";
    public const string MODE_UPDATE = "2";

    public const string TEXTBOX_CLASS_NORM = "textfield";
    public const string TEXTBOX_CLASS_ERROR = "textfield error";

    public const string APPLICANT_STATUS_DELETED = "DELETED";
    public const string APPLICANT_STATUS_HIRED = "HIRED";
    public const string APPLICANT_STATUS_NEW = "NEW";

    public const string MEDIA_TYPE_VIDEO = "VIDEO";
    public const string MEDIA_TYPE_IMAGE = "IMAGE";

    public const string TALENT_TYPE_ID_APPLICANT = "applicant";
    public const string TALENT_TYPE_ID_FEMALE = "female";
    public const string TALENT_TYPE_ID_MALE = "male";
    public const string TALENT_TYPE_ID_MALE_MINI = "minimale";
    public const string TALENT_TYPE_ID_FEMALE_MINI = "minifemale";
    public const string TALENT_TYPE_ID_BELLY_DANCER = "bellydancer";
    public const string TALENT_TYPE_ID_BBW = "bbw";
    public const string TALENT_TYPE_ID_DRAG = "dragqueen";
    public const string TALENT_TYPE_ID_IMPERSON = "impersonator";
    public const string TALENT_TYPE_ID_NOVELTY = "novelty";
    public const string TALENT_TYPE_ID_DUO = "duo";
    public const string TALENT_TYPE_ID_DRIVER = "driver";
    public const string TALENT_TYPE_ID_AFFILIATE = "affiliate";

    public const string IMAGE_PATH_BASE = "talentimages/";
    public const string VIDEO_PATH_BASE = "talentvids/";

    public static string SQL_CONN = System.Configuration.ConfigurationManager.ConnectionStrings["CenterfoldConn"].ConnectionString;
    
    /* Controls Section Access, in tandem w/ DB Entries */
    /* THESE MUST MATCH TABLE: CfsSection in the DB */
    public enum Section
    {
        UserAdmin = 1,
        EmployeeMgmt = 2,
        PendingJobs = 3,
        WorkOrders = 4,
        FranchiseMgmt = 5,
        Accounting = 6,
        BalanceCollected = 7,
        CreditCard = 8
    }

    #endregion

    public CfsCommon(){} /* No constructor needed */

    #region Objects
    /* Very basic objects, used for sorting*/
    public class AltPhoneNumber
    {
        public int Pk;
        public int SortId;
        public string Name;
        public string Num;
    }

    /* Very basic objects, used for sorting*/
    public class TalentCreditObj
    {
        /* NOT the primary key from DB, used as a Unique ID for Update Credit Functionality */
        public int Key; 
        public int SortId;
        public string Name;
        public string Details;
    }
    #endregion
    #region functions to fill textara
    public static void GetSingleTalentListEmail(HtmlTextArea area, string type)
    {
        ObjectQuery query;
        CfsEntity cfsEntity = new CfsEntity();

        // query = cfsEntity.Talent.Where("it.TalentType = '" + type + "' AND it.IsActive = true").OrderBy("it.DisplayName");
        query = cfsEntity.Talent.Where("it.IsActive = true").OrderBy("it.DisplayName");

        string populate = "";
        int counter = 0;
        foreach (Talent record in query)
        {
            string taltype = record.TalentType;
            string[] checktaltayp = taltype.Split('*');
            if (checktaltayp[0] == type)
            {
                
                if (record.EmailPrimary != "")
                {
                    if (counter != 0)
                    {
                        populate = populate + " ; " + record.EmailPrimary;
                    } 
                    else
                    {
                        populate = record.EmailPrimary;
                        counter = counter + 1;
                    }
                    
                }
            }
        }
        area.InnerText = populate;
    }
  
    #endregion

    #region Functions to fill in DropDownLists
    public static void GetFullTalentList(DropDownList ddlTalent)
    {
        ddlTalent.Items.Add(new ListItem("--- FEMALE ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_FEMALE);

        ddlTalent.Items.Add(new ListItem("--- MALE ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_MALE);

        ddlTalent.Items.Add(new ListItem("--- FEMALE MINI ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_FEMALE_MINI);

        ddlTalent.Items.Add(new ListItem("--- MALE MINI ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_MALE_MINI);

        ddlTalent.Items.Add(new ListItem("--- BELLY DANCER ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_BELLY_DANCER);

        ddlTalent.Items.Add(new ListItem("--- BBW ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_BBW);

        ddlTalent.Items.Add(new ListItem("--- DRAG QUEEN ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_DRAG);

        ddlTalent.Items.Add(new ListItem("--- IMPERSONATOR ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_IMPERSON);

        ddlTalent.Items.Add(new ListItem("--- NOVELTY ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_NOVELTY);

        ddlTalent.Items.Add(new ListItem("--- DUO ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_DUO);

        ddlTalent.Items.Add(new ListItem("--- DRIVER/BOUNCER ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_DRIVER);

        ddlTalent.Items.Add(new ListItem("--- AFFILIATE ---", ""));
        GetSingleTalentList(ddlTalent, TALENT_TYPE_ID_AFFILIATE);
    }
    public static void GetSingleTalentListEmail(DropDownList ddlTalent, string type)
    {
        ObjectQuery query;
        CfsEntity cfsEntity = new CfsEntity();

        // query = cfsEntity.Talent.Where("it.TalentType = '" + type + "' AND it.IsActive = true").OrderBy("it.DisplayName");
        query = cfsEntity.Talent.Where("it.IsActive = true").OrderBy("it.DisplayName");

        foreach (Talent record in query)
        {
            string taltype = record.TalentType;
            string[] checktaltayp = taltype.Split('*');
            if (checktaltayp[0] == type)
            {
                ddlTalent.Items.Add(new ListItem(record.DisplayName, record.TalentId.ToString()));
            }
        }
    }

    public static void getTalentBirthdayList(DropDownList ddlTalent)
    {
        ObjectQuery query;
        CfsEntity cfsEntity = new CfsEntity();
        DateTime today = new DateTime();

        today = DateTime.Today.AddYears(-18);
        var sqlFormattedDate = today.Date.ToString("yyyy-MM-dd HH:mm:ss");
        // query = cfsEntity.Talent.Where("it.TalentType = '" + type + "' AND it.IsActive = true").OrderBy("it.DisplayName");
       // query = cfsEntity.Talent.Where("it.IsActive = true AND it.DOB > DATETIME '" + sqlFormattedDate.ToString() + "'");
       // it.IsActive = 1 AND MONTH(DOB) = MONTH(GETDATE()) AND DAY(DOB) = DAY(GETDATE())
      
        query = cfsEntity.Talent.Where(" it.IsActive = true AND MONTH(it.DOB) = "+today.Month+" AND DAY(it.DOB) = "+today.Day);
        
        foreach (Talent record in query)
        {
            
                ddlTalent.Items.Add(new ListItem(record.DisplayName, record.TalentId.ToString()));
            
        }
    }

    public static void GetSingleTalentList(DropDownList ddlTalent, string type)
    {
        ObjectQuery query;
        CfsEntity cfsEntity = new CfsEntity();
       
       // query = cfsEntity.Talent.Where("it.TalentType = '" + type + "' AND it.IsActive = true").OrderBy("it.DisplayName");
        query = cfsEntity.Talent.Where("it.IsActive = true").OrderBy("it.DisplayName");

        foreach (Talent record in query)
        {
            string taltype = record.TalentType;
            string[] checktaltayp = taltype.Split('*');
            if (checktaltayp[0] == type)
            {
                ddlTalent.Items.Add(new ListItem(record.DisplayName, record.TalentId.ToString()));
            }
        }
    }

    public static void GetStateList(DropDownList ddlLocations)
    {
        ddlLocations.Items.Add(new ListItem("--",""));
        ddlLocations.Items.Add(new ListItem("AUSTRALIA", "AUS"));
        ddlLocations.Items.Add(new ListItem("UNITED KINGDOM", "UK"));
        ddlLocations.Items.Add(new ListItem("CANADA", "CAN"));
        
        
        ddlLocations.Items.Add(new ListItem("ALABAMA", "AL"));
        ddlLocations.Items.Add(new ListItem("ALASKA", "AK"));
        ddlLocations.Items.Add(new ListItem("ARIZONA", "AZ"));
        ddlLocations.Items.Add(new ListItem("ARKANSAS", "AR"));
        ddlLocations.Items.Add(new ListItem("CALIFORNIA", "CA"));
        ddlLocations.Items.Add(new ListItem("COLORADO", "CO"));
        ddlLocations.Items.Add(new ListItem("CONNECTICUT", "CT"));
        ddlLocations.Items.Add(new ListItem("DELAWARE", "DE"));
        ddlLocations.Items.Add(new ListItem("DISTRICT OF COLUMBIA", "DC"));
        ddlLocations.Items.Add(new ListItem("FLORIDA", "FL"));
        ddlLocations.Items.Add(new ListItem("GEORGIA", "GA"));
        ddlLocations.Items.Add(new ListItem("HAWAII", "HI"));
        ddlLocations.Items.Add(new ListItem("IDAHO", "ID"));
        ddlLocations.Items.Add(new ListItem("ILLINOIS", "IL"));
        ddlLocations.Items.Add(new ListItem("INDIANA", "IN"));
        ddlLocations.Items.Add(new ListItem("IOWA", "IA"));
        ddlLocations.Items.Add(new ListItem("KANSAS", "KS"));
        ddlLocations.Items.Add(new ListItem("KENTUCKY", "KY"));
        ddlLocations.Items.Add(new ListItem("LOUISIANA", "LA"));
        ddlLocations.Items.Add(new ListItem("MAINE", "ME"));
        ddlLocations.Items.Add(new ListItem("MARYLAND", "MD"));
        ddlLocations.Items.Add(new ListItem("MASSACHUSETTS", "MA"));
        ddlLocations.Items.Add(new ListItem("MICHIGAN", "MI"));
        ddlLocations.Items.Add(new ListItem("MINNESOTA", "MN"));
        ddlLocations.Items.Add(new ListItem("MISSISSIPPI", "MS"));
        ddlLocations.Items.Add(new ListItem("MISSOURI", "MO"));
        ddlLocations.Items.Add(new ListItem("MONTANA", "MT"));
        ddlLocations.Items.Add(new ListItem("NEBRASKA", "NE"));
        ddlLocations.Items.Add(new ListItem("NEVADA", "NV"));
        ddlLocations.Items.Add(new ListItem("NEW HAMPSHIRE", "NH"));
        ddlLocations.Items.Add(new ListItem("NEW JERSEY", "NJ"));
        ddlLocations.Items.Add(new ListItem("NEW MEXICO", "NM"));
        ddlLocations.Items.Add(new ListItem("NEW YORK", "NY"));
        ddlLocations.Items.Add(new ListItem("NORTH CAROLINA", "NC"));
        ddlLocations.Items.Add(new ListItem("NORTH DAKOTA", "ND"));
        ddlLocations.Items.Add(new ListItem("OHIO", "OH"));
        ddlLocations.Items.Add(new ListItem("OKLAHOMA", "OK"));
        ddlLocations.Items.Add(new ListItem("OREGON", "OR"));
        ddlLocations.Items.Add(new ListItem("PENNSYLVANIA", "PA"));
        ddlLocations.Items.Add(new ListItem("RHODE ISLAND", "RI"));
        ddlLocations.Items.Add(new ListItem("SOUTH CAROLINA", "SC"));
        ddlLocations.Items.Add(new ListItem("SOUTH DAKOTA", "SD"));
        ddlLocations.Items.Add(new ListItem("TENNESSEE", "TN"));
        ddlLocations.Items.Add(new ListItem("TEXAS", "TX"));
        ddlLocations.Items.Add(new ListItem("UTAH", "UT"));
        ddlLocations.Items.Add(new ListItem("VERMONT", "VT"));
        ddlLocations.Items.Add(new ListItem("VIRGINIA", "VA"));
        ddlLocations.Items.Add(new ListItem("WASHINGTON", "WA"));
        ddlLocations.Items.Add(new ListItem("WEST VIRGINIA", "WV"));
        ddlLocations.Items.Add(new ListItem("WISCONSIN", "WI"));
        ddlLocations.Items.Add(new ListItem("WYOMING", "WY"));
    }

    public static void GetStateListAbbr(DropDownList ddlLocations)
    {
        ddlLocations.Items.Add(new ListItem("--", ""));
        ddlLocations.Items.Add(new ListItem("AK", "AK"));
        ddlLocations.Items.Add(new ListItem("AL", "AL"));
        ddlLocations.Items.Add(new ListItem("AR", "AR"));
        ddlLocations.Items.Add(new ListItem("AZ", "AZ"));
        ddlLocations.Items.Add(new ListItem("CA", "CA"));
        ddlLocations.Items.Add(new ListItem("CO", "CO"));
        ddlLocations.Items.Add(new ListItem("CT", "CT"));
        ddlLocations.Items.Add(new ListItem("DC", "DC"));
        ddlLocations.Items.Add(new ListItem("DE", "DE"));
        ddlLocations.Items.Add(new ListItem("FL", "FL"));
        ddlLocations.Items.Add(new ListItem("GA", "GA"));
        ddlLocations.Items.Add(new ListItem("HI", "HI"));
        ddlLocations.Items.Add(new ListItem("IA", "IA"));
        ddlLocations.Items.Add(new ListItem("ID", "ID"));
        ddlLocations.Items.Add(new ListItem("IL", "IL"));
        ddlLocations.Items.Add(new ListItem("IN", "IN"));
        ddlLocations.Items.Add(new ListItem("KS", "KS"));
        ddlLocations.Items.Add(new ListItem("KY", "KY"));
        ddlLocations.Items.Add(new ListItem("LA", "LA"));
        ddlLocations.Items.Add(new ListItem("MA", "MA"));
        ddlLocations.Items.Add(new ListItem("MD", "MD"));
        ddlLocations.Items.Add(new ListItem("ME", "ME"));
        ddlLocations.Items.Add(new ListItem("MI", "MI"));
        ddlLocations.Items.Add(new ListItem("MN", "MN"));
        ddlLocations.Items.Add(new ListItem("MO", "MO"));
        ddlLocations.Items.Add(new ListItem("MS", "MS"));
        ddlLocations.Items.Add(new ListItem("MT", "MT"));
        ddlLocations.Items.Add(new ListItem("NC", "NC"));
        ddlLocations.Items.Add(new ListItem("ND", "ND"));
        ddlLocations.Items.Add(new ListItem("NE", "NE"));
        ddlLocations.Items.Add(new ListItem("NH", "NH"));
        ddlLocations.Items.Add(new ListItem("NJ", "NJ"));
        ddlLocations.Items.Add(new ListItem("NM", "NM"));
        ddlLocations.Items.Add(new ListItem("NV", "NV"));
        ddlLocations.Items.Add(new ListItem("NY", "NY"));
        ddlLocations.Items.Add(new ListItem("OH", "OH"));
        ddlLocations.Items.Add(new ListItem("OK", "OK"));
        ddlLocations.Items.Add(new ListItem("OR", "OR"));
        ddlLocations.Items.Add(new ListItem("PA", "PA"));
        ddlLocations.Items.Add(new ListItem("RI", "RI"));
        ddlLocations.Items.Add(new ListItem("SC", "SC"));
        ddlLocations.Items.Add(new ListItem("SD", "SD"));
        ddlLocations.Items.Add(new ListItem("TN", "TN"));
        ddlLocations.Items.Add(new ListItem("TX", "TX"));
        ddlLocations.Items.Add(new ListItem("UT", "UT"));
        ddlLocations.Items.Add(new ListItem("VT", "VT"));
        ddlLocations.Items.Add(new ListItem("VA", "VA"));
        ddlLocations.Items.Add(new ListItem("WA", "WA"));
        ddlLocations.Items.Add(new ListItem("WI", "WI"));
        ddlLocations.Items.Add(new ListItem("WV", "WV"));
        ddlLocations.Items.Add(new ListItem("WY", "WY"));   
    }

    public static void GetDaysOfWeekList(ListBox lbDays)
    {
        lbDays.Items.Add(new ListItem("Monday", "Monday"));
        lbDays.Items.Add(new ListItem("Tuesday", "Tuesday"));
        lbDays.Items.Add(new ListItem("Wednesday", "Wednesday"));
        lbDays.Items.Add(new ListItem("Thursday", "Thursday"));
        lbDays.Items.Add(new ListItem("Friday", "Friday"));
        lbDays.Items.Add(new ListItem("Saturday", "Saturday"));
        lbDays.Items.Add(new ListItem("Sunday", "Sunday"));
    }

    public static void GetCountryList(DropDownList ddlCountry, bool includeBlank)
    {
        if (includeBlank)
        {
            ddlCountry.Items.Add(new ListItem("--", ""));
        }
        
        ddlCountry.Items.Add(new ListItem("United States", "United States"));
        ddlCountry.Items.Add(new ListItem("United Kingdom", "United Kingdom"));
        ddlCountry.Items.Add(new ListItem("Canada", "Canada"));
        ddlCountry.Items.Add(new ListItem("Australia", "Australia"));
    }
     public static void GetTalentTypeListCheckbox(CheckBoxList ddlTalType, bool includeBlank)
    {
        if (includeBlank)
        {
            ddlTalType.Items.Add(new ListItem("------", ""));
        }

        ddlTalType.Items.Add(new ListItem("Female", TALENT_TYPE_ID_FEMALE));
        ddlTalType.Items.Add(new ListItem("Male", TALENT_TYPE_ID_MALE));
        ddlTalType.Items.Add(new ListItem("Female Mini", TALENT_TYPE_ID_FEMALE_MINI));
        ddlTalType.Items.Add(new ListItem("Male Mini", TALENT_TYPE_ID_MALE_MINI));
        ddlTalType.Items.Add(new ListItem("Belly Dancer", TALENT_TYPE_ID_BELLY_DANCER));
        ddlTalType.Items.Add(new ListItem("BBW", TALENT_TYPE_ID_BBW));
        ddlTalType.Items.Add(new ListItem("Drag Queen", TALENT_TYPE_ID_DRAG));
        ddlTalType.Items.Add(new ListItem("Impersonator", TALENT_TYPE_ID_IMPERSON));
        ddlTalType.Items.Add(new ListItem("Novelty", TALENT_TYPE_ID_NOVELTY));
        ddlTalType.Items.Add(new ListItem("Duo", TALENT_TYPE_ID_DUO));
        ddlTalType.Items.Add(new ListItem("Driver", TALENT_TYPE_ID_DRIVER ));
        ddlTalType.Items.Add(new ListItem("Affiliate", TALENT_TYPE_ID_AFFILIATE));
    }
   
    public static void GetTalentTypeList(DropDownList ddlTalType, bool includeBlank)
    {
        if (includeBlank)
        {
            ddlTalType.Items.Add(new ListItem("------", ""));
        }

        ddlTalType.Items.Add(new ListItem("Female", TALENT_TYPE_ID_FEMALE));
        ddlTalType.Items.Add(new ListItem("Male", TALENT_TYPE_ID_MALE));
        ddlTalType.Items.Add(new ListItem("Female Mini", TALENT_TYPE_ID_FEMALE_MINI));
        ddlTalType.Items.Add(new ListItem("Male Mini", TALENT_TYPE_ID_MALE_MINI));
        ddlTalType.Items.Add(new ListItem("Belly Dancer", TALENT_TYPE_ID_BELLY_DANCER));
        ddlTalType.Items.Add(new ListItem("BBW", TALENT_TYPE_ID_BBW));
        ddlTalType.Items.Add(new ListItem("Drag Queen", TALENT_TYPE_ID_DRAG));
        ddlTalType.Items.Add(new ListItem("Impersonator", TALENT_TYPE_ID_IMPERSON));
        ddlTalType.Items.Add(new ListItem("Novelty", TALENT_TYPE_ID_NOVELTY));
        ddlTalType.Items.Add(new ListItem("Duo", TALENT_TYPE_ID_DUO));
        ddlTalType.Items.Add(new ListItem("Driver", TALENT_TYPE_ID_DRIVER ));
        ddlTalType.Items.Add(new ListItem("Affiliate", TALENT_TYPE_ID_AFFILIATE));
    }
    #endregion

    #region Format Functions
    public static string FormatDate(object dt, string formatString)
    {
        if (dt == null)
        {
            return "";
        }

        return ((DateTime)dt).ToString(formatString);
    }

    public static string FormatDateLong(object dt)
    {
        if (dt == null)
        {
            return "";
        }
        else
        {
            //example: Sunday 3/23/2007    
            return ((DateTime)dt).ToString("dddd M/d/yyyy");
        }
    }

    public static string FormatTime(object dt)
    {
        if (dt == null)
        {
            return "";
        }
        else
        {
            /* Examples: 1:00 PM or 11:00 AM */
            return ((DateTime)dt).ToString("h:mm tt");
        }
    }

    public static string FormatShowLengthHumanReadable(int mins)
    {
        string retStr;

        if (mins < 60)
        {
            return mins.ToString() + " mins";
        }

        retStr = CfsCommon.ConvertMinsToHours(mins);

        if (mins < 120)
        {
            retStr += " hour";
        }
        else
        {
            retStr += " hours";
        }

        return retStr;
    }

    public static string FormatShowLengthHumanReadable(string minsStr)
    {
        int mins;

        if (!int.TryParse(minsStr, out mins))
        {
            /* This is not expected to happen, ShowLength is a required field */
            return "ERROR";
        }

        return FormatShowLengthHumanReadable(mins);
    }

    public static string FormatDisplayName(string type, string firstName, string lastName, string stageName, string state)
    {
        string name;

        if (type == null) { type = ""; }
        if (firstName == null) { firstName = ""; }
        if (lastName == null) { lastName = ""; }
        if (stageName == null) { stageName = ""; }
        if (state == null) { state = ""; }

        switch (type)
        {
            case TALENT_TYPE_ID_DRIVER:
            {
                name = firstName + ", " + lastName + "[" + state + "]";
                break;
            }
            default:
            {
                //Format = StageName (FirstName, LastName) [State]
                name = stageName + " (" + firstName + ", " + lastName + ") [" + state + "]";
                break;
            }
        }

        return name;
    }
    #endregion

    #region Functions to Get Single DB Records
    public static Talent GetTalentRecord(CfsEntity cfsEntity, string talentIdStr)
    {
        int talentId;

        if (int.TryParse(talentIdStr, out talentId))
        {
            Talent talRec = (from t in cfsEntity.Talent
                             where t.TalentId == talentId
                             select t).FirstOrDefault();

            return talRec;
        }
        else
        {
            /* Bad talentIdStr */
            return null;
        }
    }
    
    public static Customer GetCustomerRecord(CfsEntity cfsEntity, string custIdStr)
    {
        int custId;

        if (int.TryParse(custIdStr, out custId))
        {
            Customer cust = (from c in cfsEntity.Customer
                             where c.CustomerId == custId
                             select c).FirstOrDefault();

            return cust;
        }
        else
        {
            /* Bad Customer Id */
            return null;        
        }
    }

    public static Event GetEventRecord(CfsEntity cfsEntity, string eventId)
    {
        if (eventId == null || eventId == "")
        {
            return null; //bad customerId
        }

        //Retrieve from Entity:
        List<Event> list = ((ObjectQuery<Event>)cfsEntity.Event.Where("it.EventId = " + eventId)).ToList();

        //There can be only one... record
        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null; //if record does not exist
        }
    }

    public static Job GetJobRecord(CfsEntity cfsEntity, string jobId)
    {
        if (jobId == null || jobId == "")
        {
            return null; //bad jobId
        }

        //Retrieve from Entity:
        List<Job> list = ((ObjectQuery<Job>)cfsEntity.Job.Where("it.JobId = " + jobId)).ToList();

        //There can be only one... record
        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null; //if record does not exist
        }
    }

    public static TalentToJob GetTalentToJobRecord(CfsEntity cfsEntity, string talToJobId)
    {
        if ( string.IsNullOrEmpty(talToJobId))
        {
            return null; //bad talToJobId
        }

        //Retrieve from Entity:
        List<TalentToJob> list = ((ObjectQuery<TalentToJob>)cfsEntity.TalentToJob.Where("it.UID = " + talToJobId)).ToList();

        //There can be only one... record
        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null; //if record does not exist
        }    
    }

    public static CfsUser GetUserRecord(CfsEntity cfsEntity, string userId)
    {
        if ( string.IsNullOrEmpty( userId ) )
        {
            return null; //bad userId
        }

        //Retrieve from Entity:
        List<CfsUser> list = ((ObjectQuery<CfsUser>)cfsEntity.CfsUser.Where("it.UserId = " + userId)).ToList();

        //There can be only one... record
        if (list.Count == 1)
        {
            return list[0];
        }
        else
        {
            return null; //if record does not exist
        }
    }

    public static bool DeleteTalent(CfsEntity cfsEntity, string userId)

    {
        if (string.IsNullOrEmpty(userId))
        {
            return false; //bad userId
        }

        List<Talent> list = ((ObjectQuery<Talent>)cfsEntity.Talent.Where("it.TalentId = " + userId)).ToList();

        
        List<TalentWorksIn> lista = ((ObjectQuery<TalentWorksIn>)cfsEntity.TalentWorksIn).ToList();



        for (int i = 0; i < lista.Count; i++)
        {
            EntityKeyMember key = lista[i].TalentReference.EntityKey.EntityKeyValues[0];


            if (key.Value.ToString() == userId)
            {
                cfsEntity.DeleteObject(lista[i]);
            }
        
        }

        List<TalentToJob> listb = ((ObjectQuery<TalentToJob>)cfsEntity.TalentToJob).ToList();

        for (int i = 0; i < listb.Count; i++)
        {
            EntityKeyMember key = listb[i].TalentReference.EntityKey.EntityKeyValues[0];


            if (key.Value.ToString() == userId)
            {
                cfsEntity.DeleteObject(listb[i]);
            }

        }

        List<TalentCredit> listc = ((ObjectQuery<TalentCredit>)cfsEntity.TalentCredit).ToList();

        for (int i = 0; i < listc.Count; i++)
        {
            EntityKeyMember key = listc[i].TalentReference.EntityKey.EntityKeyValues[0];


            if (key.Value.ToString() == userId)
            {
                cfsEntity.DeleteObject(listc[i]);
            }

        }

        List<TalentAltPhone> listd = ((ObjectQuery<TalentAltPhone>)cfsEntity.TalentAltPhone).ToList();

        for (int i = 0; i < listd.Count; i++)
        {
            EntityKeyMember key = listd[i].TalentReference.EntityKey.EntityKeyValues[0];


            if (key.Value.ToString() == userId)
            {
                cfsEntity.DeleteObject(listd[i]);
            }

        }
        /*
        List<TalentImages> liste = ((ObjectQuery<TalentImages>)cfsEntity.TalentImages).ToList();

        for (int i = 0; i < liste.Count; i++)
        {
            string key = liste[i].FK_talentID.ToString();


            if (key == userId)
            {
                cfsEntity.DeleteObject(liste[i]);
            }

        }
        */
        if (list.Count == 1)
        {
            cfsEntity.DeleteObject(list[0]);
            cfsEntity.SaveChanges();
            return true;
            //return list[0];
        }
        else
        {
            return false;
            // return null; //if record does not exist
        }

       
    }

    public static bool DeleteUserRecord(CfsEntity cfsEntity, string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return false; //bad userId
        }

        //Retrieve from Entity:

      

        List<CfsUser> list = ((ObjectQuery<CfsUser>)cfsEntity.CfsUser.Where("it.UserId = " + userId)).ToList();

        List<CfsUserToSection> lista = ((ObjectQuery<CfsUserToSection>)cfsEntity.CfsUserToSection).ToList();

        for(int i=0;i<lista.Count;i++)
        {
           EntityKeyMember key  = lista[i].CfsUserReference.EntityKey.EntityKeyValues[0];


           if (key.Value.ToString() == userId)
           {
               cfsEntity.DeleteObject(lista[i]);
           }
         //   cfsEntity.SaveChanges();
            //return true;
            //return list[0];
        }



        //There can be only one... record
        if (list.Count == 1)
        {
            cfsEntity.DeleteObject(list[0]);
            cfsEntity.SaveChanges();
            return true;
            //return list[0];
        }
        else
        {
            return false;
           // return null; //if record does not exist
        }
    }

   
    #endregion

    #region Functions for Login and Permissions
    public static bool AuthenticateUser(string userName, string password, HttpSessionState session)
    {
        session[SESSION_KEY_USERID] = null;
        session[SESSION_KEY_USERNAME] = null;
        session[SESSION_KEY_LOGGED_IN] = null;
        session[SESSION_KEY_ACCESS_TABLE] = null;


        CfsEntity cfsEntity = new CfsEntity();
        CfsUser user;

        string passHash = CfsCommon.Encrypt(password, true);
        password = "";
        string whereClause = "it.UserName = '" + userName + "' AND it.UserPass = '" + passHash + "' AND it.IsActive = True";

        List<CfsUser> userList = ((ObjectQuery<CfsUser>)cfsEntity.CfsUser.Where(whereClause)).ToList();

        if (userList.Count == 1)
        {
            user = userList[0];
            Hashtable accessTable = new Hashtable();

            if (!user.CfsUserToSection.IsLoaded)
            {
                user.CfsUserToSection.Load();
            }

            foreach (CfsUserToSection secAccesss in user.CfsUserToSection)
            {

                if (!secAccesss.CfsSectionReference.IsLoaded)
                {
                    secAccesss.CfsSectionReference.Load();
                }
                if (user.UserName == "MilesStyles")
                {
                    accessTable[0] = true;
                
                    accessTable[1] = true;
                    accessTable[2] = true;
                    accessTable[3] = true;
                    accessTable[4] = true;
                    accessTable[5] = true;
                    accessTable[6] = true;
                    accessTable[7] = true;
                    accessTable[8] = true;
                    accessTable[9] = true;
                }
                accessTable[secAccesss.CfsSection.SectionId] = true;
            }

            session[SESSION_KEY_USERID] = user.UserId;
            session[SESSION_KEY_USERNAME] = userName;
            session[SESSION_KEY_LOGGED_IN] = true;
            session[SESSION_KEY_ACCESS_TABLE] = accessTable;
            session.Timeout = 1000;
            return true;
        }

        return false;
    }

    public static void LogUserOut(HttpSessionState session)
    {
        session[SESSION_KEY_USERID] = null;
        session[SESSION_KEY_USERNAME] = null;
        session[SESSION_KEY_LOGGED_IN] = null;
        session[SESSION_KEY_ACCESS_TABLE] = null;    
    }

    public static bool UserHasSectionAccess(int sectionId, HttpSessionState session)
    {
        if (session[SESSION_KEY_LOGGED_IN] == null)
        {
            return false;
        }
        
        if (session[SESSION_KEY_ACCESS_TABLE] == null)
        {
            return false;
        }

        Hashtable accessTable = (Hashtable)session[SESSION_KEY_ACCESS_TABLE];

        if (accessTable[sectionId] == null)
        {
            return false;
        }
        else if ((bool)accessTable[sectionId] == true)
        {

            return true;
        }

        return false;
    }

    public static void CheckPageAccess(int sectionId, HttpSessionState session, HttpResponse response)
    {
        if (!UserHasSectionAccess(sectionId, session))
        {
            response.Redirect(LOGIN_PAGE, true);        
        }
    }

    public static string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        //get the byte code of the string

        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        //Get your key from config file to open the lock!
        string key = System.Web.Configuration.WebConfigurationManager.AppSettings["SecurityKey"];

        if (useHashing)
        {
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();
        }
        else
        {
            //if hashing was not implemented get the byte code of the key
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
        }

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm

        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        if (cipherString == "")
        {
            tdes.Clear();
            return "";
        }
        else
        {
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);

        }
    }

    public static string Encrypt(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        // Get the key from config file

        string key = System.Web.Configuration.WebConfigurationManager.AppSettings["SecurityKey"];
        //System.Windows.Forms.MessageBox.Show(key);
        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
    #endregion

    #region Functions for Image/Video File Manipulation
    public static string GetTalentMediaPath(string talentType, string mediaType, bool includeBase)
    {
        if (mediaType != CfsCommon.MEDIA_TYPE_IMAGE &&
            mediaType != CfsCommon.MEDIA_TYPE_VIDEO)
        {
            /* Error */
            return "";
        }

        string path= talentType + "/";

        if (includeBase && mediaType == CfsCommon.MEDIA_TYPE_IMAGE)
        {
            path = IMAGE_PATH_BASE + path;
        }
        else if (includeBase && mediaType == CfsCommon.MEDIA_TYPE_VIDEO)
        {
            path = VIDEO_PATH_BASE + path;
        }

        return path;
    }

    public static string CreateNewFilename(string filePath, string fileName)
    {
        string fileExt = "";
        
        if( !File.Exists(filePath + fileName ))
        {
            return fileName;
        }

        if (fileName.Contains('.'))
        {
            /* Assuming 3 letter file extension, if any. (most will be .jpg for this App) */
            fileExt = fileName.Substring(fileName.Length - 4, 4);
            fileName = fileName.Remove(fileName.Length - 4);
        }

        int count = 1;
        
        /* Create a new file name (append numbers). Format: filename[_X].jpg
         * Where X is a number to make a file that does not exist.
         * 
         * Ex: candy1_4.jpg
         */
        while (File.Exists(filePath + fileName + fileExt))
        {
            int pos = fileName.LastIndexOf('_');

            if (pos != -1)
            {
                /* Remove old Append '_X' */
                fileName = fileName.Remove(pos);
            }
            
            fileName += "_" + count.ToString();

            count++;
        }

        return fileName + fileExt;
    }
    #endregion

    #region Functions to change Controls (Read-Only/Visible)
    public static void MakeControlsNonEditable(Control c)
    {
        if (c is System.Web.UI.WebControls.TextBox)
        {
            ((TextBox)c).Enabled = false;
            ((TextBox)c).CssClass = "textfieldreadonly";
        }
        else if(c is System.Web.UI.WebControls.CheckBox )
        {
            ((CheckBox)c).Enabled = false;
        }
        else if(c is System.Web.UI.WebControls.DropDownList)
        {
            ((DropDownList)c).Enabled = false;
            ((DropDownList)c).CssClass = "selectreadonly";
        }

        foreach (Control child in c.Controls)
        {
            MakeControlsNonEditable(child);
        }
    }

    public static void MakeControlsEditable(Control c)
    {
        if (c is System.Web.UI.WebControls.TextBox)
        {
            ((TextBox)c).Enabled = true;
            ((TextBox)c).CssClass = "textfield";
        }
        else if (c is System.Web.UI.WebControls.CheckBox)
        {
            ((CheckBox)c).Enabled = true;
        }
        else if (c is System.Web.UI.WebControls.DropDownList)
        {
            ((DropDownList)c).Enabled = true;
            ((DropDownList)c).CssClass = "select";
        }

        foreach (Control child in c.Controls)
        {
            MakeControlsEditable(child);
        }
    }

    public static void ChangeButtonsVisible(Control c, bool visible)
    {
        if (c is System.Web.UI.WebControls.Button)
        {
            ((Button)c).Visible = visible;
        }

        foreach (Control child in c.Controls)
        {
            ChangeButtonsVisible(child, visible);
        }
    }
    #endregion

    #region Date Validation Functions
    public static bool ValidateTextBoxReq(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        if (tBox.Text != "")
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
            return true;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' is a required field.</li>";
            return false;
        }
    }

    public static bool ValidateTextBoxDate(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        DateTime dt;

        if (DateTime.TryParseExact(tBox.Text, "M/d/yyyy", null, System.Globalization.DateTimeStyles.AssumeLocal, out dt))
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
            return true;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. It must be in the form: 'mm/dd/yyyy' Example: 12/25/2005 </li>";
            return false;
        }
    }

    public static bool ValidateTextBoxInt(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        int tmpInt;

        if (int.TryParse(tBox.Text, out tmpInt))
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
            return true;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. (Must be a number)</li>";
            return false;
        }
    }

    public static bool ValidateTextBoxEmail(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        if (tBox.Text.Contains('@'))
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
            return true;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not a valid email address.</li>";
            return false;
        }
    }

    public static bool ValidateTextBoxTime(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        DateTime dt;

        if (DateTime.TryParseExact(tBox.Text, "h:mm tt", null, System.Globalization.DateTimeStyles.AssumeLocal, out dt))
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
            return true;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. It must be a time in the form: 'h:mm PP' Example: '9:58 AM' </li>";
            return false;
        }
    }

    public static bool ValidateTextBoxPhone(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        /* Valid Phone Num is format: XXX-XXX-XXXX */
        bool isValid = true;
        string phoneNum = tBox.Text.Trim();

        /* Did I mention I hate reg expressions? */
        if (phoneNum.Length != 12)
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. It must be a Phone Number in the form: 'XXX-XXX-XXXX' Example: 212-555-1212</li>";
            return false;
        }

        for (int i = 0; i < phoneNum.Length; i++)
        {
            if (i == 3 || i == 7)
            {
                if (phoneNum[i] != '-')
                {
                    isValid = false;
                    break;
                }
            }
            else
            {
                /* If digit NOT between */
                if (!(phoneNum[i] >= '0' && phoneNum[i] <= '9'))
                {
                    isValid = false;
                    break;                
                }
            }
        }

        if (isValid)
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. It must be a Phone Number in the form: 'XXX-XXX-XXXX' Example: 212-555-1212</li>";        
        }

        return isValid;
    }

    public static bool ValidateTextBoxCreditCard(TextBox tBox, string fieldName, HtmlGenericControl ulErrors)
    {
        /* Valid CC Num Format: XXXX-XXXX-XXXX-XXXX (Doesn't do AMEX)*/ 
        bool isValid = true;
        string ccNum = tBox.Text;

        if (ccNum.Length != 19)
        {
            isValid = false;
        }
        else
        {
            /* Did I mention I still hate regular expressions? */
            for (int i = 0; i < 19; i++)
            {
                if (i == 4 || i == 9 || i == 14)
                {
                    if (ccNum[i] != '-')
                    {
                        isValid = false;
                        break;
                    }
                }
                else
                {
                    /* If digit NOT between */
                    if (!(ccNum[i] >= '0' && ccNum[i] <= '9'))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
        }

        if (isValid)
        {
            tBox.CssClass = TEXTBOX_CLASS_NORM;
        }
        else
        {
            tBox.CssClass = TEXTBOX_CLASS_ERROR;
            ulErrors.InnerHtml += "<li>'" + fieldName + "' field is not valid. It must be in the form: 'XXXX-XXX-XXXX-XXXX' </li>";
        }

        return isValid;
    }
    #endregion

    public static bool DebugMode()
    {
        string debug = System.Web.Configuration.WebConfigurationManager.AppSettings["CfsDebugMode"].ToLower();
        if (debug.Equals("true"))
            return true;
        else
            return false;
    }

    public static DateTime GetStartOfCurrentWeek()
    {
        DateTime dt = DateTime.Now;

        //According to clients definition of "current week"
        //Get the previous Monday, either today, or in da past
        while (dt.DayOfWeek != DayOfWeek.Monday)
        {
            dt = dt.AddDays(-1.0);
        }

        return DateTime.Parse(dt.ToString("MM/dd/yyyy") + " 12:00:00 AM");
    }

    public static DateTime GetEndOfCurrentWeek()
    {
        DateTime dt = DateTime.Now;

        //According to clients definition of "current week"
        //Get the next Sunday, either today, or in da future
        while (dt.DayOfWeek != DayOfWeek.Sunday)
        {
            dt = dt.AddDays(1.0);
        }

        return DateTime.Parse(dt.ToString("MM/dd/yyyy") + " 11:59:59 PM");
    }

    public static string CalculateAge(DateTime dob)
    {
        if( dob == null )
        {
            return "()";
        }

        //Calculate Age:
        TimeSpan age = DateTime.Now.Subtract(dob);
        string ageStr = "  ( " + (age.Days / 365).ToString() + " yrs )";
        return ageStr;
    }

    public static string ConvertMinsToHours(int mins)
    {
        double hrs = mins / 60.0;

        return hrs.ToString("0.0");
    }

    public static DateTime CreateTalentToJobStartDateTime(Event eventRec, string talStartTimeStr)
    {
        DateTime startDateTimeRet;

        /* Try to create a DateTime Object of the combined EventDate AND StartTime 
         * This is used to create the Talent StartDateTime. The accuracy is important
         * for certain features of the site.
         */
        DateTime evtStartDtTime;
        string evtStartDtTimeStr = eventRec.EventDate.ToString("yyyy-MM-dd");

        if (eventRec.StartTime != null)
        {
            evtStartDtTimeStr += " " + ((DateTime)eventRec.StartTime).ToString("hh:mm tt");
        }

        if (!DateTime.TryParse(evtStartDtTimeStr, out evtStartDtTime))
        {
            /* Default, this shouldn't happen though */
            evtStartDtTime = eventRec.EventDate;
        }


        /* Talent Start Time is REQUIRED, so this should always work (if time is valid, in the form "h:mm tt" i.e. 11:00 PM) */
        string talStartDateTimeStr = evtStartDtTime.ToString("yyyy-MM-dd") + " " + talStartTimeStr;

        if (!DateTime.TryParse(talStartDateTimeStr, out startDateTimeRet))
        {
            /* Error */
            return DateTime.Parse("01/01/1900 12:00 AM");
        }


        /* Kind of fuzzy logic? 
         *
         * IF the Event Starts anytime after noon, and the talent is scheduled for the
         * morning, then technically the Talent StartDateTime is on the next day.
         * (This has to be accurate, for certain site features)
         * 
         * Ex: (To make this clearer)
         * 
         * If an event starts at 3/1/2008 at 11:00 PM, but a Talent is scheduled to go
         * on at 12:30 AM, THEN the Talent StartDateTime is the next day.
         * (i.e. 3/2/2008 12:30 AM)
         * 
         */
        talStartTimeStr = "1900-01-01 " + talStartTimeStr;
        DateTime talStartTime;

        if (!DateTime.TryParse(talStartTimeStr, out talStartTime))
        {
            /* Error */
            return DateTime.Parse("1900-01-01 12:00 AM");        
        }

        if (evtStartDtTime.Hour >= 12) /* Noon, or later */
        {
            if (talStartTime.Hour < 12)/* In the morning */
            {
                startDateTimeRet = startDateTimeRet.AddDays(1.0);
            }
        }

        return startDateTimeRet;
    }
    
    public static class EntityDataSourceExtensions
    {
        public static TEntity GetItemObject<TEntity>(object dataItem) where TEntity : class
        {
            var entity = dataItem as TEntity;

            if (entity != null)
            {
                return entity;
            }

            var td = dataItem as ICustomTypeDescriptor;

            if (td != null)
            {
                return (TEntity)td.GetPropertyOwner(null);
            }

            return null;
        }
    }

    public static Control[] FlattenHierachy(Control root)
    {
        List<Control> list = new List<Control>();
        list.Add(root);
        if (root.HasControls())
        {
            foreach (Control control in root.Controls)
            {
                list.AddRange(FlattenHierachy(control));
            }
        }
        return list.ToArray();
    }

    public static string RetrieveImageUrl(string imgRelPath)
    {
        if (string.IsNullOrEmpty(imgRelPath))
        {
            return string.Empty;
        }
        else
        {
            if (imgRelPath.Contains("-") )
            {
                // is probably a GUID
                try
                {
                    Guid guid = new Guid(imgRelPath);
                    string imageHandlerPath = CfsCommon.GetTalentImagePath("../", guid);

                    // return the image handler path
                    return imageHandlerPath;
                }
                catch { }
            }

            // return file system path
            return "~/talentimages/" + imgRelPath;
        }
    }

    public static string GetNavUrl(string p_id)
    {
        return "~/Entertainers/Talent-Details.aspx?id=" + p_id;
    }

    public static string GetTalentImagePath(string pathToRoot, Guid imageID)
    {
        return string.Format(pathToRoot + "AtomImageEditor/ImageHandler.aspx?ID={0}", imageID.ToString());
    }

}
