using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class backend_Email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string TalentType = "";
        if (Request["TalentType"] != null)
        {
            TalentType = Request["TalentType"].ToString();
        }
        try
        {
            CfsCommon.GetSingleTalentListEmail(txt_email, TalentType);
        }
        catch (Exception ex)
        {
            txt_email.InnerText = ex.Message;
        }
    }
}