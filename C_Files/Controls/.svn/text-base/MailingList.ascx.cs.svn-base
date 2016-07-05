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
using System.Collections.Generic;
using System.Data.Objects;

public partial class Controls_MailingList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmitEmail_Click(object sender, ImageClickEventArgs e)
    {
        if (tbEmail.Text != "" && tbEmail.Text.Contains('@'))
        {
            CfsEntity cfse = new CfsEntity();

            // Prevent Duplicate Description
            if (IsEmailInDb(cfse, tbEmail.Text))
            {
                lblFeedback.Text = "You are already Subscribed";
                return;
            }

            MailingList ml = new MailingList();

            ml.EmailAddress = tbEmail.Text;
            cfse.AddToMailingList(ml);
            cfse.SaveChanges();

            PostToMailListForm(ml.EmailAddress);
        }
        else
        {
            lblFeedback.Text = "Email address not valid.";
        }

        lblFeedback.Visible = true;
    }


    private bool IsEmailInDb(CfsEntity cfsEntity, string email)
    {
        List<MailingList> list = ((ObjectQuery<MailingList>)cfsEntity.MailingList.Where("it.EmailAddress = '" + email + "'")).ToList();

        if (list.Count >= 1)
        {
            return true;
        }

        return false;
    }

    // Post data to external mailing list form
    private void PostToMailListForm(string email)
    {
        Response.Redirect(String.Format("http://ymlp.com/subscribe.php?YMLPID=guweysmgmge&YMP0={0}", email));
    }
}
