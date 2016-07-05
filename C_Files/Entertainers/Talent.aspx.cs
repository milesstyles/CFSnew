using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;

using System.Data.Objects;
using System.Data.Objects.DataClasses;

using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using AtomImageEditor;

public partial class Talent_Page : System.Web.UI.Page
{
    #region Page Properties
    public int CurrentPage
    {
        get
        {
            object o = this.ViewState["_CurrentPage"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }

    #endregion

    #region Page Life Cycle
    private string m_category = "";
    private string m_state = null;
    protected string m_hdrImage = "";
    protected string m_talentType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlMeta metakey = new HtmlMeta();
        metakey.HttpEquiv = "x-ua-compatible";
        metakey.Content = "IE=8";
       
        Header.Controls.Add(metakey);

        if (Request.QueryString["cat"] != null )
        {
            m_category = Request.QueryString["cat"];
        }
        if (Request.QueryString["state"] != null)
        {
            m_state = Request.QueryString["state"];
        }

        if (!IsPostBack)
        {
            switch (m_category)
            {
                case CfsCommon.TALENT_TYPE_ID_FEMALE:
                case CfsCommon.TALENT_TYPE_ID_MALE:
                {
                    LoadWorksInStateList(m_category);
                    ddlTalentWorksIn.SelectedValue = m_state;
                    ddlTalentWorksIn.SelectedItem.Text = "Talent in " + ddlTalentWorksIn.SelectedItem.Text;
                    pHldFilter.Visible = true;
                    break;
                }
                default:
                {
                    pHldFilter.Visible = false;
                    break;
                }
            }

            BindData();
        }

        // Set header image and h1
        ContentPlaceHolder head = null;
        switch (m_category)
        {
            case CfsCommon.TALENT_TYPE_ID_BBW:
                m_hdrImage = "headerBigBeautiful.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_BBW.ToString();
                m_talentType = CfsCommon.TALENT_TYPE_ID_FEMALE.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headBBW");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_BELLY_DANCER:
                m_hdrImage = "headerBellyDancers.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_BELLY_DANCER.ToString();
                m_talentType = CfsCommon.TALENT_TYPE_ID_FEMALE.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headBellyDancer");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;    
            case CfsCommon.TALENT_TYPE_ID_DRAG:
                m_hdrImage = "headerDragQueens.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_DRAG.ToString();
                m_talentType = CfsCommon.TALENT_TYPE_ID_FEMALE.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headDragQueen");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_FEMALE:
                m_hdrImage = "headerFemaleDancers.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_FEMALE.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headFemale");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_FEMALE_MINI:
                m_hdrImage = "headerFemaleLittle.jpg";
                m_talentType = "femalelittleperson";
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headFemaleLittle");
                if (head != null)
                {
                    head.Visible = true;
                }
                
                break;
            case CfsCommon.TALENT_TYPE_ID_IMPERSON:
                m_hdrImage = "headerImpersonators.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_IMPERSON.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headImpersonator");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_MALE:
                m_hdrImage = "headerMaleDancers.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_MALE.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headMale");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_MALE_MINI:
                m_hdrImage = "headerMaleLittle.jpg";
                m_talentType = "malelittleperson";
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headMaleLittle");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_NOVELTY:
                m_hdrImage = "headerNoveltyActs.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_NOVELTY.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headNovelty");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
            case CfsCommon.TALENT_TYPE_ID_DUO:
                m_hdrImage = "headerDuoShows.jpg";
                m_talentType = CfsCommon.TALENT_TYPE_ID_DUO.ToString();
                head = (ContentPlaceHolder)this.Master.Controls[0].Controls[0].FindControl("head").FindControl("headDuo");
                if (head != null)
                {
                    head.Visible = true;
                }
                break;
        }

    }

    protected void OnChange_ddlTalentWorksIn(object sender, EventArgs e)
    {
        string state = ddlTalentWorksIn.SelectedValue;
        string redirUrl = "Talent.aspx?cat=" + Request.QueryString["cat"];

        if (state != "")
        {
            redirUrl += "&state=" + state;
        }

        Response.Redirect(redirUrl, true);
    }
    #endregion

    private void LoadWorksInStateList(string talType)
    {
        /* Loads available list of states, based on locations loaded
         * into 'Talent' profiles of a certain type
         */
        ddlTalentWorksIn.Items.Add(new ListItem("All Locations", ""));


        string cmd;

        cmd = "SELECT DISTINCT s.StateName, s.StateAbbrev";
        cmd += " FROM Talent t, TalentWorksIn w, UsState s";
        cmd += " WHERE t.TalentType = '" + talType + "'";
        cmd += " AND t.IsActive = 'true' ";
        cmd += " AND t.TalentId = w.TalentId";
        cmd += " AND w.State = s.StateAbbrev";
        cmd += " AND t.ThumbImg IS NOT NULL";
        cmd += " AND t.ThumbImg <> ''";
        cmd += " ORDER BY s.StateName";

        SqlConnection sqlConn = new SqlConnection(CfsCommon.SQL_CONN);
        SqlCommand sqlCmd = new SqlCommand(cmd, sqlConn);
        SqlDataReader sRead = null;

        try
        {
            sqlConn.Open();

            sRead = sqlCmd.ExecuteReader();

            if (sRead.HasRows)
            {
                while (sRead.Read())
                {
                    ddlTalentWorksIn.Items.Add(new ListItem((string)sRead["StateName"], (string)sRead["StateAbbrev"]));
                }
            }

            sRead.Close();
            sqlConn.Close();
        }
        catch
        {

        }
    }


    #region Data Binding
    private void BindData()
    {
        CfsEntity cfse = new CfsEntity();
        List<Talent> talList;
        string additionalParam = "";
        if (m_category.ToLower() == "male")
        {
            additionalParam = " AND it.TalentType NOT LIKE '%FEMALE%'  AND it.TalentType NOT LIKE '%MINIMALE%'";
        }
        if (m_category.ToLower() == "female")
        {
            additionalParam = " AND it.TalentType NOT LIKE '%MINIFEMALE%' ";
       
        }
        if (m_category != null && m_state == null)
        {
            talList = ((ObjectQuery<Talent>)cfse.Talent.Where("it.IsActive = true AND it.TalentType LIKE '%" + m_category + "%'" +
               additionalParam +  " AND it.ThumbImg IS NOT NULL AND it.ThumbImg <> ''")).OrderBy("it.StageName ASC").ToList();
        }
        else if (m_state != null && m_category == null)
        {
            talList = ((ObjectQuery<Talent>)cfse.Talent.Where("it.IsActive = true AND it.WorksInList LIKE '%" + m_state + "%'" +
                " AND it.ThumbImg IS NOT NULL AND it.ThumbImg <> ''")).OrderBy("it.StageName ASC").ToList();
        }
        else
        {

            talList = ((ObjectQuery<Talent>)cfse.Talent.Where("it.IsActive = true AND it.WorksInList LIKE '%" + m_state + "%' AND it.TalentType LIKE '%" + m_category + "%'" +
               additionalParam + " AND it.ThumbImg IS NOT NULL AND it.ThumbImg <> ''")).OrderBy("it.StageName ASC").ToList();
        }

        PagedDataSource objPds = new PagedDataSource();
        objPds.DataSource = talList;
        objPds.AllowPaging = true;
        objPds.PageSize = 15;
        objPds.CurrentPageIndex = CurrentPage;

        ibMoreTop.Visible = !objPds.IsLastPage;
        ibMoreBottom.Visible = !objPds.IsLastPage;
        ibBackTop.Visible = !objPds.IsFirstPage;
        ibBackBottom.Visible = !objPds.IsFirstPage;

        if (m_category != CfsCommon.TALENT_TYPE_ID_DUO)
        {
            dlTalentList.DataSource = objPds;
            dlTalentList.DataBind();
        }
        else
        {
            // Viewing duo talent type; use the special duo datalist
            dlDuoTalentList.DataSource = objPds;
            dlDuoTalentList.DataBind();
        }
    }

    #endregion

    #region Button Click Events
    protected void btnMore_Click(object sender, ImageClickEventArgs e)
    {
        CurrentPage += 1;
        BindData();
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        CurrentPage -= 1;
        BindData();
    }
    #endregion
}
