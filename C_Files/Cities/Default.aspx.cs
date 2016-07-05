using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CfsNamespace;
using System.Diagnostics;

public partial class Entertainers_CitySpecific : CitySpecificBase
{           


    protected override void OnInit(EventArgs e)
    {
        //set the action of the form to satisfy viewstate mac validation
        Form.Action = Request.RawUrl;

        citySpecific.Attributes.Add("href", ResolveUrl("~/css/citySpecific.css"));

        // set refs for the base class
        this.City = getCityRequest();
        //this.StrippersRepeater = this.talentRepeater;
        //this.NoResultsPanel = this.noResults;
        //this.ShownCategoryHiddenField = this.shownCategory;

        IntroMultiView.ActiveViewIndex = string.IsNullOrEmpty(this.IntroText) ? 0 : 1;
        RightColumnMultiView.ActiveViewIndex = string.IsNullOrEmpty(this.RightColumnText) ? 0 : 1;
        PageTitleMultiView.ActiveViewIndex = string.IsNullOrEmpty(this.PageTitle) ? 0 : 1;

        ScriptManagerProxy1.Scripts.Add(new ScriptReference(ResolveUrl("~/js/jquery-1.3.2.min.js")));
                
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        this.bindStrippers(TalentCategory.Female, this.femaleRepeater, this.showFemalesButton);
        this.bindStrippers(TalentCategory.Male, this.maleRepeater, this.showMalesButton);
        this.bindStrippers(TalentCategory.FemaleMidget, this.miniFemaleRepeater, this.showFemaleLittlesButton);
        this.bindStrippers(TalentCategory.MaleMidget, this.miniMaleRepeater, this.showMaleLittlesButton);
        
        base.OnLoad(e);
    }

    private void bindStrippers(TalentCategory talentCategory, Repeater repeater, HyperLink jumpLink)
    {
        string dbTalentType = string.Empty;
        switch (talentCategory)
        {
            case TalentCategory.Female:
                dbTalentType = "female";
                break;

            case TalentCategory.Male:
                dbTalentType = "male";
                break;

            case TalentCategory.FemaleMidget:
                dbTalentType = "minifemale";
                break;

            case TalentCategory.MaleMidget:
                dbTalentType = "minimale";
                break;

            default:
                throw new Exception("invalid talent type");
        }

        // get the location
        string dbCity = string.Empty;
        string dbStateAcr = string.Empty;
        this.GetCityStateValues(ref dbCity, ref dbStateAcr);

        // get the strippers
        Talent[] talents = null;
        using (CfsEntity entities = new CfsEntity())
        {
            talents = entities.Talent.Where(it => it.State.Equals(dbStateAcr, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(it.ThumbImg) && it.TalentType == dbTalentType && it.IsActive == true && it.WorksInList.Contains(dbStateAcr)).ToArray();
        }

        // bind
        repeater.DataSource = talents;
        repeater.DataBind();

        // hide if no results
        repeater.Visible = jumpLink .Visible = 0 < talents.Length;
    }

    protected Cities getCityRequest()
    {
        try
        {
            Cities city = (Cities)Enum.Parse(typeof(Cities), Request.QueryString["city"]);
            return city;
        }
        catch
        {
            throw new Exception("invalid city");
        }
    }
}
