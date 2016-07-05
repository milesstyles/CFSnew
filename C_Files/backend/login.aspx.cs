using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Objects;
using System.Collections;

using System.Web.SessionState;

using CfsNamespace;

public partial class backend_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CfsEntity cfsEntity = new CfsEntity();

        foreach (AppSetting setting in cfsEntity.AppSetting)
        {
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_PROMO_URL)
            {
                // tBoxPromoUrl.Text = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC)
            {
                //imgPromo.ImageUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_LOGO_IMGSRC)
            {
             //   imgLogoTop.ImageUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_LOGO_TEXT)
            {
                LogoName.Text = setting.SettingValue;
            }

        }
    }

    protected void OnClick_btnLogin(object sender, EventArgs arrgggs)
    {
        if (CfsCommon.AuthenticateUser(tBoxUserName.Text, tBoxPassword.Text, Session))
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            lblErrorMsg.Text = "Login Failed";
        }
    }


}
