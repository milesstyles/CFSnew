using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;
using System.Data.Objects.DataClasses;

using CfsNamespace;

public partial class Feature_Entertainers : System.Web.UI.Page
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

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

    private void BindData()
    {
        string esql = "it.IsActive = true AND it.ThumbImg IS NOT NULL AND it.ThumbImg <> '' AND it.IsFeatureTalent = true";

        CfsEntity cfse = new CfsEntity();
        List<Talent> talList = ((ObjectQuery<Talent>)cfse.Talent.Where(esql)).OrderBy("it.StageName ASC").ToList();


        PagedDataSource objPds = new PagedDataSource();
        objPds.DataSource = talList;
        objPds.AllowPaging = true;
        objPds.PageSize = 15;
        objPds.CurrentPageIndex = CurrentPage;

        ibMoreTop.Visible = !objPds.IsLastPage;
        ibMoreBottom.Visible = !objPds.IsLastPage;
        ibBackTop.Visible = !objPds.IsFirstPage;
        ibBackBottom.Visible = !objPds.IsFirstPage;

        dlTalentList.DataSource = objPds;
        dlTalentList.DataBind();
    }
}
