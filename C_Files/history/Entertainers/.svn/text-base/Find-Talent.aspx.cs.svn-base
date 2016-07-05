using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using CfsNamespace;

public partial class Entertainers_Find_Talent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlState);
            CfsCommon.GetTalentTypeList(ddlCategory, true);
            ddlCategory.Items.Remove(ddlCategory.Items[5]);
            ddlCategory.Items.Remove(ddlCategory.Items[5]);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string queryString = "";
        CfsEntity cfse=new CfsEntity();

        if (ddlState.SelectedIndex == 0 && ddlCategory.SelectedIndex == 0)
        {
            divError.Visible = true;
        }
        else
        {
            if (ddlState.SelectedIndex > 0)
            {
                List<USState> stateList = ((ObjectQuery<USState>)cfse.USState.Where("it.StateAbbrev = '" + ddlState.SelectedValue + "'")).ToList();
                var state = stateList[0];
                queryString += "state=" + state.StateAbbrev;
            }
            if (ddlState.SelectedIndex > 0 && ddlCategory.SelectedIndex > 0)
            {
                queryString += "&";
            }
            if (ddlCategory.SelectedIndex > 0)
            {
                queryString += "cat=" + ddlCategory.SelectedValue.ToLower();
            }

            Response.Redirect("Talent.aspx?" + queryString);
        }
    }
}
