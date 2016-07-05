using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;

using System.Data.Objects;
using System.Data.Objects.DataClasses;

using System.Web.UI.HtmlControls;
using AtomImageEditor;

public partial class Talent_Details : System.Web.UI.Page
{
    protected string m_talentId;

    #region Page Lifecycle
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !string.IsNullOrEmpty(Request.QueryString["id"]) )
        {
            LoadTalentData(Request.QueryString["id"]);                    
        }
    }
    #endregion

    private void LoadTalentData(string talentId)
    {
        CfsEntity cfsEntity = new CfsEntity();
        
        Talent talRec = CfsCommon.GetTalentRecord(cfsEntity, talentId);

        if (talRec == null || talRec.IsActive == false)
        {
            Response.Redirect("~/");
        }

        /* Name, Location(s) */
        h1StageName.InnerText = talRec.StageName;
        h2WorksInList.InnerText = talRec.WorksInList;

        /* Stats */
        if (talRec.TalentType == CfsCommon.TALENT_TYPE_ID_FEMALE &&
            !string.IsNullOrEmpty(talRec.Bust) &&
            !string.IsNullOrEmpty(talRec.Hips) &&
            !string.IsNullOrEmpty(talRec.Waist))
        {
            lbMeasurements.Text = "Measurements: " + talRec.Bust + "-" + talRec.Waist + "-" + talRec.Hips;
        }
        if (!string.IsNullOrEmpty(talRec.HairColor))
        {
            lbHairColor.Text = "Hair Color: " + talRec.HairColor;
        }
        if (!string.IsNullOrEmpty(talRec.EyeColor))
        {
            lbEyeColor.Text = "Eye Color: " + talRec.EyeColor;
        }
        if (talRec.HeightFt != null && talRec.HeightIn != null)
        {
            lbHeight.Text = "Height: " + talRec.HeightFt + "' " + talRec.HeightIn + "\"";
        }

        /* Images */
        if (!string.IsNullOrEmpty(talRec.ImageOne))
        {
            Guid imageGuid1 = ImageManager.GetImageGuid(1, talRec.TalentId, talRec.TalentType, talRec.ImageOne);
            if (imageGuid1 != Guid.Empty)
            {
                img1.ImageUrl = CfsCommon.GetTalentImagePath("../", imageGuid1);
                img1.Visible = true;
            }
        }

        if (!string.IsNullOrEmpty(talRec.ImageTwo))
        {
            Guid imageGuid2 = ImageManager.GetImageGuid(2, talRec.TalentId, talRec.TalentType, talRec.ImageTwo);
            if (imageGuid2 != Guid.Empty)
            {
                img2.ImageUrl = CfsCommon.GetTalentImagePath("../", imageGuid2);
                img2.Visible = true;
            }
        }

        if (!string.IsNullOrEmpty(talRec.ImageThree))
        {
            Guid imageGuid3 = ImageManager.GetImageGuid(3, talRec.TalentId, talRec.TalentType, talRec.ImageThree);
            if (imageGuid3 != Guid.Empty)
            {
                img3.ImageUrl = CfsCommon.GetTalentImagePath("../", imageGuid3);
                img3.Visible = true;
            }
        }

        /* Standard 'attributes' */
        if( string.IsNullOrEmpty(talRec.Likes) &&
            string.IsNullOrEmpty(talRec.Dislikes) &&
            string.IsNullOrEmpty(talRec.Costumes) &&
            string.IsNullOrEmpty(talRec.SpecialTalents) )
        {
            hrAttributes.Visible = false;        
        }
        else
        {
            SetAttributeValue(phLikes, pLikes, talRec.Likes);
            SetAttributeValue(phDislikes, pDislikes, talRec.Dislikes);
            SetAttributeValue(phTalents, pTalents, talRec.SpecialTalents);
            SetAttributeValue(phCostumes, pCostumes, talRec.Costumes);
        }

        /* Feature 'attributes' */
        if (talRec.IsFeatureTalent)
        {
            hrFeatureContent.Visible = true;
            BindFeatureContent(talRec);
        }

        /* Videos */
        if (!string.IsNullOrEmpty(talRec.VideoOne))
        {
            pnlVids.Visible = true;
            m_talentId = talentId;
        }

        /* Services */
        SortedDictionary<string, bool> talServices = new SortedDictionary<string, bool>() {
            {"Full Nude Strip", talRec.DoesFullNudeStrip},
            {"Internet Chat", talRec.DoesInternetChat},
            {"Lap Dancing", talRec.DoesLapdancing},
            {"Lesbian Show", talRec.DoesLesbianShow},
            {"Pole Dancing", talRec.DoesPoleDancing},
            {"Popout Cake", talRec.DoesPopoutCake},
            {"Promo Modeling", talRec.DoesPromoModeling},
            {"Topless Bartending", talRec.DoesToplessBartender},
            {"Topless Card Dealing", talRec.DoesToplessCardDealing},
            {"Topless Strip", talRec.DoesToplessStrip},
            {"Topless Waiter/Waitress", talRec.DoesToplessWaiting},
            {"Toys", talRec.DoesToys}
        };
        foreach (var serviceItem in talServices)
        {
            if (serviceItem.Value)
            {
                blServices.Items.Add(new ListItem(serviceItem.Key, serviceItem.Key));
            }
        }
        if (blServices.Items.Count > 0)
        {
            phServices.Visible = true;
        }
    }

    private void SetAttributeValue(PlaceHolder pHld, HtmlGenericControl p, string text )
    {
        if (!string.IsNullOrEmpty(text))
        {
            pHld.Visible = true;
            p.InnerHtml = text;
        }
    }

    private void BindFeatureContent(Talent talRec)
    {
        if (!talRec.TalentCredit.IsLoaded)
        {
            talRec.TalentCredit.Load();
        }

        List<TalentCredit> talCredList = talRec.TalentCredit.ToList();
        dlCredits.DataSource = talCredList;
        dlCredits.DataBind();
    }

    #region Data Binding
    private void FormatStats(Talent p_talent)
    {
        /*
        Control[] cc = CfsCommon.FlattenHierachy(rptTalent);
        Label lMeasure = null;
        Label lHair = null;
        Label lEye = null;
        Label lHeight = null;
        
        foreach (Control c in cc)
        {
            if (c.ID == "lbMeasurements")
                lMeasure = (Label)c;
            if (c.ID == "lbHairColor")
                lHair = (Label)c;
            if (c.ID == "lbEyeColor")
                lEye = (Label)c;
            if (c.ID == "lbHeight")
                lHeight = (Label)c;
        }
        
        if (p_talent.TalentType == CfsCommon.TALENT_TYPE_ID_FEMALE &&
            !string.IsNullOrEmpty(p_talent.Bust) &&
            !string.IsNullOrEmpty(p_talent.Hips) &&
            !string.IsNullOrEmpty(p_talent.Waist))
        {
            lMeasure.Text = "Measurements: " + p_talent.Bust + "-" + p_talent.Waist + "-" + p_talent.Hips;
        }
        if (!string.IsNullOrEmpty(p_talent.HairColor))
        {
            lHair.Text = "Hair Color: " + p_talent.HairColor;
        }
        if (!string.IsNullOrEmpty(p_talent.EyeColor))
        {
            lEye.Text = "<br/>Eye Color: " + p_talent.EyeColor;
        }
        bool showHeight;
        showHeight = HeightIf(p_talent.HeightFt);
        showHeight = HeightIf(p_talent.HeightIn);
        if (showHeight)
        {
            lHeight.Text = "<br/>Height: " + p_talent.HeightFt + "' " + p_talent.HeightIn + "\"";
        }
        */
    }

    #endregion

    #region Text Formatting

    protected string ReturnState(string p_abbrev)
    {
        try
        {
            CfsEntity cfse = new CfsEntity();
            List<USState> stateList = ((ObjectQuery<USState>)cfse.USState.Where("it.StateAbbrev = '" + p_abbrev + "'")).ToList();
            return stateList[0].StateName;
        }
        catch
        {
            return "";
        }
    }
    #endregion
}
