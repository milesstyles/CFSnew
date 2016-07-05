﻿<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Employment-App.aspx.cs" Inherits="Employment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server" >
<title>Stripper Jobs | Stripper Job and Employment application for at CenterfoldStrips.com</title>
<meta name="keywords" content="Stripper Jobs, Stripper Job" />
<meta name="description" content="Interested in working with Centerfold Strips? We are currently seeking new talent in the entire continental U.S. Apply today!" />
<style type="text/css">
	#frameHolder #flashNav
	{
	    background-image: url(../images/headerEmployment.jpg);
	    background-repeat: no-repeat;	
	}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
    Thanks for your interest in Centerfold
    Strips. We are currently seeking new talent in the entire continental U.S.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <asp:Panel ID="pnlThanks" Visible="false" runat="server">
        <script type="text/javascript">
            alert("Your application has been submitted. Thank you.");
            window.location.href = "http://www.centerfoldstrips.com";
        </script>
    </asp:Panel>
    <script type="text/javascript" src="~/js/Functions.js"></script> 
    <p>Please fill out the form below, to be considered for employment. Follow all instructions carefully so your application does not 
        get discarded. We are looking for at three pictures that you can submit to our office. Your picture and application will remain 
        confidential. Although you do not need a picture to submit an application, the application process cannot continue without a 
     current image. You can also mail still images to our main office.</p>
    <p>If you prefer, you can also fill out our interactive form by <a href="CFS_Application.pdf">downloading it here</a></p>
    <p>In order to stay compliant with the federal governament, all applications must be acompanied by as <a href="usc2257.pdf">USC 2257 form</a>. If you are hired, you will need to fax this form to our office.</p>
    <p><span class="required">*</span> Required </p>
    <h3><asp:Image runat="server" ID="imgTalentInfo" ImageUrl="~/images/h2TalentInfo.gif" AlternateText="" /><span>Talent Information</span></h3>
    <asp:ValidationSummary runat="server" ID="valSummary" ValidationGroup="grpEmployment" ShowSummary="true" CssClass="errorBox" 
        HeaderText="<span>The following fields must be filled out in order to successfully complete your application</span><br/><br/>" />
    <asp:Label runat="server" ID="lblPhoneErr" Text="You must fill in at least one phone number" CssClass="errorBox" Visible="false" />
    <label>
        <asp:Label runat="server" ID="lbFirstName" Text="First Name:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbFirstName" CssClass="textfield" MaxLength="30" />
        <asp:Label runat="server" ID="lbReq1" CssClass="required" Text="*" />
   
    </label>
    <label>
        <asp:Label runat="server" ID="lbLastName" Text="Last Name:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbLastName" CssClass="textfield" MaxLength="30" />
        <asp:Label runat="server" ID="lbReq2" CssClass="required" Text="*" />
     
    </label>
    <label>
        <asp:Label runat="server" ID="lbEmail" Text="Email:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbEmail" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq3" CssClass="required" Text="*" />
      
    </label>
    <label>
        <asp:Label runat="server" ID="lbAddress1" Text="Address 1:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbAddress1" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq4" CssClass="required" Text="*" />
     
    </label>
    <label>
        <asp:Label runat="server" ID="lbAddress2" Text="Address 2:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbAddress2" CssClass="textfield" MaxLength="50" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbCity" Text="City:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbCity" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq5" CssClass="required" Text="*" />
   
    </label>
    <label>
        <asp:Label runat="server" ID="lbState" Text="State:" CssClass="formLabel" />
        <asp:DropDownList runat="server" ID="ddlState" CssClass="select" />
        <asp:Label runat="server" ID="lbReq6" CssClass="required" Text="*" />
     
    </label>
    <label>
        <asp:Label runat="server" ID="lbZip" Text="Zip:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbZip" CssClass="textfield formQuarterWidth" MaxLength="15" />
        <asp:Label runat="server" ID="lbReq7" CssClass="required" Text="*" />
      
    </label>
    <div class="cleaner">&nbsp;</div>
    <label>
        <asp:Label runat="server" ID="lbHomePhone" Text="Home Phone:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbHomePhone" CssClass="textfield" MaxLength="20" />
      
    </label>
    <label>
        <asp:Label runat="server" ID="lbCellPhone" Text="Cell Phone:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbCellPhone" CssClass="textfield" MaxLength="20" />
      
    </label>
    <label>
        <asp:Label runat="server" ID="lbAltPhone" Text="Alt Phone:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbAltPhone" CssClass="textfield" MaxLength="20" />
        
    </label>
    <label>
        <asp:Label runat="server" ID="lbWebsite" Text="Website:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbWebsite" CssClass="textfield" MaxLength="50" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbStageName" Text="Stage Name:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbStageName" CssClass="textfield" MaxLength="50" />
    </label>
    <div class="cleaner">&nbsp;</div>
    <p>
        Although we do book parties during the week, the busiest days for our agency is Friday and Saturday evening. You will need to be available 
        at least one of those nights to be considered for a job.
    </p>
    <div class="cleaner">&nbsp;</div>
    <label>
        <asp:Label runat="server" ID="lbDaysAvailable" Text="What days are you available to work?:" CssClass="formLabel" />
        <asp:ListBox runat="server" ID="lbxDaysAvailable" CssClass="select" SelectionMode="Multiple"/>
    </label>
    <label>
        <asp:Label runat="server" ID="lbDaysPreferred" Text="What nights / days would you prefer?:" CssClass="formLabel" />
        <asp:ListBox runat="server" ID="lbxDaysPreferred" CssClass="select" SelectionMode="Multiple" />
    </label>
    <div class="cleaner">&nbsp;</div>
    * Hold Control (PC) or Command (Mac) to select multiple days.
    
    <hr />
    <h3><asp:Image runat="server" ID="imgVitalStats" ImageUrl="~/images/h2VitalStats.gif" AlternateText="" /><span>Vital Stats</span></h3>
    <label>
        <asp:Label runat="server" ID="lbHeight" Text="Height:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbHeightFt" CssClass="textfield formQuarterWidth" /> ft
        <cc1:FilteredTextBoxExtender runat="server" ID="ftbe1" TargetControlID="tbHeightFt" FilterType="Numbers" />
        <asp:TextBox runat="server" ID="tbHeightIn" CssClass="textfield formQuarterWidth" /> in
        <cc1:FilteredTextBoxExtender runat="server" ID="ftbe2" TargetControlID="tbHeightIn" FilterType="Numbers" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbWeight" Text="Weight:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbWeight" CssClass="textfield formQuarterWidth" /> lbs
        <cc1:FilteredTextBoxExtender runat="server" ID="ftbe3" TargetControlID="tbWeight" FilterType="Numbers" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbEyeColor" Text="Eye Color:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbEyeColor" CssClass="textfield formHalfWidth" MaxLength="10" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbHairColor" Text="Hair Color:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbHairColor" CssClass="textfield formHalfWidth" MaxLength="20" />
    </label>
    <label>
        <asp:Label runat="server" ID="lbDOB" Text="Date of Birth:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbDOB" CssClass="textfield formHalfWidth" />
        <asp:Label runat="server" ID="lbReq9" Text="*" CssClass="required" />
       
        <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="tbDOB" />
    </label>
    <div class="cleaner">&nbsp;</div>
    <label class="full">
        <asp:Label runat="server" ID="lbBust" CssClass="formLabel" Text="Bust:" />
        <asp:TextBox runat="server" ID="tbBust" CssClass="textfield formQuarterWidth" MaxLength="10" />
        &nbsp;&nbsp;&nbsp;Waist:
        <asp:TextBox runat="server" ID="tbWaist" CssClass="textfield formQuarterWidth" MaxLength="10" />
        &nbsp;&nbsp;&nbsp;Hips:
        <asp:TextBox runat="server" ID="tbHips" CssClass="textfield formQuarterWidth" MaxLength="10" />
    </label>
    <label class="full">
        <asp:Label runat="server" ID="lbExperience" Text="Experience:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbExperience" TextMode="MultiLine" Columns="45" Rows="5" CssClass="textfield formDoubleWidth"
            onkeypress="Count(this.value,1000)" />
    </label>
    
    <hr />
    <h3><asp:Image runat="server" ID="imgTalentPhotos" ImageUrl="~/images/h2TalentPhotos.gif" AlternateText="" /><span>Talent Pictures</span></h3>
    <p>
        Please submit at least three(3) current images  with your application. One of the images must be a full body bikini or semi-nude shot taken 
        <strong>within the past six months.</strong> Stores such as CVS, Walgreens or photo specialty outlets may be able to provide scanning services 
        if your images are in hard copy format. 
    A few other things to keep in mind:</p>
    <ul>
         <li>Do not include group shots, only supply images of you standing by yourself.</li>
         <li>Be mindful of items in the background of your images.</li>
         <li>Ensure the image is clear and flattering.</li>
         <li>Standing poses or slightly bent work the best.</li>
         <li>Do not send more than three (3) images. If you are hired and you give your consent, you will be asked to send more images to add to the site.</li>
    </ul>
<p>
     These images will be used for the application process only, and in no way  will be used on the site without your expressed permission. <strong>
          Your application WILL NOT be considered if we do not have your current images on file. </strong>
</p>
    
    <div id="fileUploads">
        <label class="full">
            <asp:Label runat="server" ID="lbImage1" Text="Image 1:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuImage1" size="40"/>
            <asp:Label runat="server" ID="lbReq10" Text="*" CssClass="required" />
       
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbImage2" Text="Image 2:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuImage2" size="40" />
            <asp:Label runat="server" ID="lbReq11" Text="*" CssClass="required" />
          
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbImage3" Text="Image 3:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuImage3" size="40" />
            <asp:Label runat="server" ID="lbReq12" Text="*" CssClass="required" />
          
        </label>
        <div>Government issued ID only – Passport OR Driver’s License</div>
         <label class="full">
            <asp:Label runat="server" ID="Label1" Text="ID:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuID1" size="40" />
            <asp:Label runat="server" ID="Label2" Text="*" CssClass="required" />
          
        </label>
        
    </div>
    <div class="cleaner">&nbsp;</div>
    
    <div align="center">        
        <p>If we are interested, someone from our office will be in touch with you shortly. </p>
        <p>
            <asp:Label runat="server" id="uploadedFiles" Text="" />

            <recaptcha:RecaptchaControl ID="recaptcha" runat="server" ErrorMessage="The verification words are incorrect. Please try again." 
                                        PublicKey="6Lf-8wQAAAAAAGhcwrLQOiVZlpPn0YBVgDl1Zw3Z" PrivateKey="6Lf-8wQAAAAAAIu0xZIa7U34fuaqIe6QRAYifhh9" />
            <asp:Button runat="server" ID="btnSubmitApplication" Text="Submit Application"  OnClick="btnSubmitApplication_Click" style="margin-top: 10px;" />
        </p>
    </div>
</asp:Content>
