using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CfsNamespace;

public enum TalentCategory { 
    Female,
    Male,
    MaleMidget,
    FemaleMidget
}

public enum Cities {
    NewYork = 0,
    Boston = 1,
    LongIsland = 2,
    AtlanticCity = 3,	
    NewJersey = 4,	
    Philadelphia = 5,	
    Pennsylvania = 6,	
    Pittsburgh = 7,	
    Miami = 8,	
    Tampa = 9,	
    Orlando = 10,	
    Jacksonville = 11,	
    Florida = 12,	
    Baltimore = 13,	
    Maryland = 14,	
    Seattle = 15,	
    Washington = 16,	
    LosAngeles = 17,	
    SanDiego = 18,	
    SanFrancisco = 19,	
    California = 20,
	Massachusetts = 21,
	Phoenix = 22,
	Arizona = 23,
	Atlanta = 24,
	Georgia = 25,
	Denver = 26,
	Colorado = 27,
	Nevada = 28,
	LasVegas = 29,
	StLouis = 30,
	Missouri = 31,
	Detroit = 32,
	Michigan = 33,
	NorthCarolina = 34,
	Charlotte = 35,
	Chicago = 36,
	Illinois = 37,
	Columbus = 38,
	Ohio = 39,
	Dallas = 40,
	Houston = 41,
	SanAntonio = 42,
	Texas = 43
}

/// <summary>
/// Summary description for CitySpecificBase
/// </summary>
public class CitySpecificBase : Page
{
    // set by child page
    protected Cities City { get; set; }
    //protected Repeater StrippersRepeater { get; set; }
    //protected HiddenField ShownCategoryHiddenField { get; set; }
    //protected Panel NoResultsPanel { get; set; }

    // pulled from appsettings
    protected string NoResultsText {
        get {
            return ConfigurationManager.AppSettings["NoStrippersMessage"]; 
        }
    }

    // pulled from appsettings
    protected string RightColumnText
    {
        get
        {
            return ConfigurationManager.AppSettings[this.CitySettingsKey + "RightColumn"]; 
        }
    }

    // pulled from appsettings
    protected string IntroText
    {
        get
        {
            return ConfigurationManager.AppSettings[this.CitySettingsKey + "Intro"]; 
        }
    }


    // pulled from appsettings
    protected string PageTitle
    {
        get
        {
            return ConfigurationManager.AppSettings[this.CitySettingsKey + "PageTitle"]; 
        }
    }

    // dynamic
    protected string thumbCssClass = string.Empty;
    protected string displayCategory = string.Empty;
    protected string displayCity = string.Empty;
    protected string imageUrl = string.Empty;
    private string CitySettingsKey { 
        get {
            return this.City.ToString();
        } 
    }

    public CitySpecificBase()
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        //this.bindStrippers();
        
        base.OnLoad(e);
    }


    protected void GetCityStateValues(ref string dbCity, ref string dbStateAcr)
    {
        switch (this.City)
        {
            case Cities.NewYork:
                dbCity = "New York";
                this.displayCity = "New York";
                dbStateAcr = "NY";
                break;

            case Cities.Boston:
                dbCity = "Boston";
                this.displayCity = "Boston";
                dbStateAcr = "MA";
                break;

            case Cities.Massachusetts:
                dbCity = "Massachusetts";
                this.displayCity = "Massachusetts";
                dbStateAcr = "MA";
                break;

            case Cities.LongIsland:
                dbCity = "Long Island";
                this.displayCity = "Long Island";
                dbStateAcr = "NY";
                break;

            case Cities.AtlanticCity:
                dbCity = "Atlantic City";
                this.displayCity = "Atlantic City";
                dbStateAcr = "NJ";
                break;

            case Cities.NewJersey:
                dbCity = "New Jersey";
                this.displayCity = "New Jersey";
                dbStateAcr = "NJ";
                break;

            case Cities.Philadelphia:
                dbCity = "Philadelphia";
                this.displayCity = "Philadelphia";
                dbStateAcr = "PA";
                break;

            case Cities.Pennsylvania:
                dbCity = "Pennsylvania";
                this.displayCity = "Pennsylvania";
                dbStateAcr = "PA";
                break;

            case Cities.Pittsburgh:
                dbCity = "Pittsburgh";
                this.displayCity = "Pittsburgh";
                dbStateAcr = "PA";
                break;

            case Cities.Miami:
                dbCity = "Miami";
                this.displayCity = "Miami";
                dbStateAcr = "FL";
                break;

            case Cities.Tampa:
                dbCity = "Tampa";
                this.displayCity = "Tampa";
                dbStateAcr = "FL";
                break;

            case Cities.Orlando:
                dbCity = "Orlando";
                this.displayCity = "Orlando";
                dbStateAcr = "FL";
                break;

            case Cities.Jacksonville:
                dbCity = "Jacksonville";
                this.displayCity = "Jacksonville";
                dbStateAcr = "FL";
                break;

            case Cities.Florida:
                dbCity = "Florida";
                this.displayCity = "Florida";
                dbStateAcr = "FL";
                break;

            case Cities.Baltimore:
                dbCity = "Baltimore";
                this.displayCity = "Baltimore";
                dbStateAcr = "MD";
                break;

            case Cities.Maryland:
                dbCity = "Maryland";
                this.displayCity = "Maryland";
                dbStateAcr = "MD";
                break;

            case Cities.Seattle:
                dbCity = "Seattle";
                this.displayCity = "Seattle";
                dbStateAcr = "WA";
                break;

            case Cities.Washington:
                dbCity = "Washington";
                this.displayCity = "Washington";
                dbStateAcr = "WA";
                break;

            case Cities.LosAngeles:
                dbCity = "Los Angeles";
                this.displayCity = "Los Angeles";
                dbStateAcr = "CA";
                break;

            case Cities.SanDiego:
                dbCity = "San Diego";
                this.displayCity = "San Diego";
                dbStateAcr = "CA";
                break;

            case Cities.SanFrancisco:
                dbCity = "San Francisco";
                this.displayCity = "San Francisco";
                dbStateAcr = "CA";
                break;

            case Cities.California:
                dbCity = "California";
                this.displayCity = "California";
                dbStateAcr = "CA";
                break;

            case Cities.Phoenix:
                dbCity = "Phoenix";
                this.displayCity = "Phoenix";
                dbStateAcr = "AZ";
                break;

            case Cities.Arizona:
                dbCity = "Arizona";
                this.displayCity = "Arizona";
                dbStateAcr = "AZ";
                break;

            case Cities.Atlanta:
                dbCity = "Atlanta";
                this.displayCity = "Atlanta";
                dbStateAcr = "GA";
                break;

            case Cities.Georgia:
                dbCity = "Georgia";
                this.displayCity = "Georgia";
                dbStateAcr = "GA";
                break;

            case Cities.Denver:
                dbCity = "Denver";
                this.displayCity = "Denver";
                dbStateAcr = "CO";
                break;

            case Cities.Colorado:
                dbCity = "Colorado";
                this.displayCity = "Colorado";
                dbStateAcr = "CO";
                break;

            case Cities.Nevada:
                dbCity = "Nevada";
                this.displayCity = "Nevada";
                dbStateAcr = "NV";
                break;

            case Cities.LasVegas:
                dbCity = "Las Vegas";
                this.displayCity = "Las Vegas";
                dbStateAcr = "NV";
                break;

            case Cities.StLouis:
                dbCity = "St. Louis";
                this.displayCity = "St. Louis";
                dbStateAcr = "MO";
                break;

            case Cities.Missouri:
                dbCity = "Missouri";
                this.displayCity = "Missouri";
                dbStateAcr = "MO";
                break;

            case Cities.Detroit:
                dbCity = "Detroit";
                this.displayCity = "Detroit";
                dbStateAcr = "MI";
                break;

            case Cities.Michigan:
                dbCity = "Michigan";
                this.displayCity = "Michigan";
                dbStateAcr = "MI";
                break;

            case Cities.NorthCarolina:
                dbCity = "North Carolina";
                this.displayCity = "North Carolina";
                dbStateAcr = "NC";
                break;

            case Cities.Charlotte:
                dbCity = "Charlotte";
                this.displayCity = "Charlotte";
                dbStateAcr = "NC";
                break;

            case Cities.Chicago:
                dbCity = "Chicago";
                this.displayCity = "Chicago";
                dbStateAcr = "IL";
                break;

            case Cities.Illinois:
                dbCity = "Illinois";
                this.displayCity = "Illinois";
                dbStateAcr = "IL";
                break;

            case Cities.Columbus:
                dbCity = "Columbus";
                this.displayCity = "Columbus";
                dbStateAcr = "OH";
                break;

            case Cities.Ohio:
                dbCity = "Ohio";
                this.displayCity = "Ohio";
                dbStateAcr = "OH";
                break;

            case Cities.Dallas:
                dbCity = "Dallas";
                this.displayCity = "Dallas";
                dbStateAcr = "TX";
                break;

            case Cities.Houston:
                dbCity = "Houston";
                this.displayCity = "Houston";
                dbStateAcr = "TX";
                break;

            case Cities.SanAntonio:
                dbCity = "San Antonio";
                this.displayCity = "San Antonio";
                dbStateAcr = "TX";
                break;

            case Cities.Texas:
                dbCity = "Texas";
                this.displayCity = "Texas";
                dbStateAcr = "TX";
                break;


            default:
                throw new Exception("invalid city");
        }
    }
    protected void talentRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        const int RowLength = 5;

        thumbCssClass = string.Empty;

        if ((e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) && e.Item.ItemIndex != 0)
        {
            if ((e.Item.ItemIndex+1) % RowLength == 0)
            {
                thumbCssClass = "last";
            }
        }

        Talent talent = (Talent)e.Item.DataItem;
        if (talent == null)
            return;

        this.imageUrl = string.Empty;
        if (!string.IsNullOrEmpty(talent.ThumbImg))
        {
            Guid imageGuid; 
            try {
                
                // load from DB
                imageGuid = new Guid( talent.ThumbImg );
                this.imageUrl = "../AtomImageEditor/ImageHandler.aspx?ID=" + imageGuid.ToString();

            } catch( Exception ex ) {

                // load from HDD
                this.imageUrl = "../talentimages/" + talent.ThumbImg;
            }
        }
        
    }
}
