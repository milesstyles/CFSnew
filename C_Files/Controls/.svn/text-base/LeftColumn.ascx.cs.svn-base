using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;

public partial class Controls_LeftColumn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPromoSpecialInfo();
    }

     /* Gets the Hyperlink and Image for the 'Promo Specials' box (line 26-ish) */
    private void GetPromoSpecialInfo()
    {
        CfsEntity cfsEntity = new CfsEntity();

        foreach (AppSetting setting in cfsEntity.AppSetting)
        {
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEY_PROMO_URL)
            {
                hlPromoLink.NavigateUrl = setting.SettingValue;
            }
            if (setting.SettingKey == CfsCommon.APP_SETTING_KEU_PROMO_IMGSRC)
            {
                imgPromo.ImageUrl = setting.SettingValue;
            }
        }
    }

    // Post data to external mailing list form
    private void PostToMailListForm(string email)
    {
        Response.Redirect(String.Format("http://ymlp.com/subscribe.php?YMLPID=guweysmgmge&YMP0={0}", email));
    }
}

