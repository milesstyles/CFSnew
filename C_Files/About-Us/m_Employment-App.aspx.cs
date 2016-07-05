﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CfsNamespace;

public partial class Employment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CfsCommon.GetStateList(ddlState);
            CfsCommon.GetDaysOfWeekList(lbxDaysAvailable);
            CfsCommon.GetDaysOfWeekList(lbxDaysPreferred);
        }
    }

    protected void btnSubmitApplication_Click(object sender, EventArgs e)
    {
        if (!(tbHomePhone.Text == "" && tbCellPhone.Text == "" && tbAltPhone.Text == ""))
        {
            lblPhoneErr.Visible = false;
            
            int result = AddApplicant();
            pnlThanks.Visible = true;
        }
        else
        {
            lblPhoneErr.Visible = true;        
        }
    }

    private int AddApplicant()
    {
        CfsEntity cfse = new CfsEntity();
        Applicant newApp = new Applicant();

        newApp.HasBeenContacted = false;
        newApp.Status = CfsCommon.APPLICANT_STATUS_NEW;
        newApp.Country = "United States";
        newApp.DateApplied = DateTime.Now;

        newApp.FirstName = tbFirstName.Text;
        newApp.LastName = tbLastName.Text;
        newApp.Email = tbEmail.Text;
        newApp.Address1 = tbAddress1.Text;
        newApp.Address2 = tbAddress2.Text;
        newApp.City = tbCity.Text;
        newApp.State = ddlState.SelectedValue;
        newApp.Zip = tbZip.Text;
        newApp.HomePhone = tbHomePhone.Text;
        newApp.CellPhone = tbCellPhone.Text;
        newApp.Website = tbWebsite.Text;
        newApp.StageName = tbStageName.Text;
        newApp.DaysAvail = RetrieveDays(lbxDaysAvailable);
        newApp.DaysPrefer = RetrieveDays(lbxDaysPreferred);
        if (tbHeightFt.Text != "")
        {
            newApp.HeightFt = Convert.ToInt32(tbHeightFt.Text);
        }
        if (tbHeightIn.Text != "")
        {
            newApp.HeightIn = Convert.ToInt32(tbHeightIn.Text);
        }
        if (tbWeight.Text != "")
        {
            newApp.Weight = Convert.ToInt32(tbWeight.Text);
        }
        newApp.EyeColor = tbEyeColor.Text;
        newApp.HairColor = tbHairColor.Text;
        if (tbDOB.Text != "")
        {
            newApp.DOB = Convert.ToDateTime(tbDOB.Text);
        }
        newApp.Bust = tbBust.Text;
        newApp.Waist = tbWaist.Text;
        newApp.Hips = tbHips.Text;
        newApp.Experience = tbExperience.Text;
        newApp.ImageOne = SaveImage(fuImage1);
        newApp.ImageTwo = SaveImage(fuImage2);
        newApp.ImageThree = SaveImage(fuImage3);

        newApp.ImageIdOne = SaveImage(fuID1);
        newApp.ImageIdTwo = SaveImage(fuID2);

        cfse.AddToApplicant(newApp);

        return cfse.SaveChanges();
    }

    private string RetrieveDays(ListBox p_lb)
    {
        string result = "";
        foreach (ListItem li in p_lb.Items)
        {
            if (li.Selected)
            {
                result += ", " + li.Text;
            }
        }
        try
        {
            result = result.Remove(0, 2);
            return result;
        }
        catch
        {
            return result;
        }
    }

    private string SaveImage(FileUpload p_fu)
    {
        string timeStamp = DateTime.Now.ToString();

        timeStamp = timeStamp.Replace("/", "").Replace(":", "").Replace(" ", "");
        timeStamp += p_fu.FileName;
        string path = Server.MapPath("~/talentimages/applicant/" + timeStamp);
        p_fu.SaveAs(path);

        return timeStamp;
    }

}