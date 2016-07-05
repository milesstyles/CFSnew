using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_MainMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetSelectedNavLink();

        hlAppearances.Text = Server.HtmlEncode(hlAppearances.Text);
    }

    private void SetSelectedNavLink()
    {
        string url = Request.Url.ToString().ToLower();

        string[] dirs = url.Split('/');

        if (dirs.Length >= 2)
        {
            switch (dirs[dirs.Length - 2])
            {
                case "entertainers":
                {
                    hlEntertainers.Attributes.Add("class", "active"); break;
                }
                case "services":
                {
                    hlServices.Attributes.Add("class", "active"); break;
                }
                case "exclusives":
                {
                    hlExclusives.Attributes.Add("class", "active"); break;
                }
                case "about-us":
                {
                    hlAboutUs.Attributes.Add("class", "active"); break;
                }
                case "contact-us":
                {
                    hlContact.Attributes.Add("class", "active"); break;
                }
            }
        }
    }
}
