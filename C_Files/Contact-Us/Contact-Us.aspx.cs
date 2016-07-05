using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;

public partial class Contact_Us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!recaptcha.IsValid)
        {
            return;
        }

        if (AddContactFormInfo())
        {
            pHldForm.Visible = false;
            pHldThankYou.Visible = true;
        }
    }

    private bool AddContactFormInfo()
    {
        CfsEntity cfse = new CfsEntity();
        ContactForm cfAdd = new ContactForm();

        cfAdd.Name = tbName.Text;
        cfAdd.EmailAddress = tbEmail.Text;
        cfAdd.PhoneNumber = tbPhone.Text;
        cfAdd.ContactText = tbInquiry.Text;
        cfAdd.Date = DateTime.Now;
        cfAdd.New = true;

        cfse.AddToContactForm(cfAdd);

        if (cfse.SaveChanges() == 1)
        {
            return true;
        }

        return false;
    }
}
