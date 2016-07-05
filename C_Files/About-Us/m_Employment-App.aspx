<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_Employment-App.aspx.cs" Inherits="Employment" %>


<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>


<title>Stripper Jobs | Stripper Job and Employment application for at CenterfoldStrips.com</title>
<meta name="keywords" content="Stripper Jobs, Stripper Job" />
<meta name="viewport" content="width=device-width,initial-scale=1">
<meta name="description" content="Interested in working with Centerfold Strips? We are currently seeking new talent in the entire continental U.S. Apply today!" />
<head runat="server">
 <link id="Link1" rel="Stylesheet" type="text/css" media="all" href="~/css/m_mainStyle.css" runat="server" />
    
</head>

<body>
<form runat="server">
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
        <asp:Label runat="server" ID="lbFirstName" Text="First Name:" CssClass="formLabel" />&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbFirstName" CssClass="textfield" MaxLength="30" />
        <asp:Label runat="server" ID="lbReq1" CssClass="required" Text="*" />
       
    </label><br />
    <label>
        <asp:Label runat="server" ID="lbLastName" Text="Last Name:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbLastName" CssClass="textfield" MaxLength="30" />
        <asp:Label runat="server" ID="lbReq2" CssClass="required" Text="*" />
       
    </label><br />
    <label>
        <asp:Label runat="server" ID="lbEmail" Text="Email:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbEmail" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq3" CssClass="required" Text="*" />
        
      
    </label><br />
    <label>
        <asp:Label runat="server" ID="lbAddress1" Text="Address 1:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbAddress1" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq4" CssClass="required" Text="*" />
       
    </label><br />
    <label>
        <asp:Label runat="server" ID="lbAddress2" Text="Address 2:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbAddress2" CssClass="textfield" MaxLength="50" />
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbCity" Text="City:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbCity" CssClass="textfield" MaxLength="50" />
        <asp:Label runat="server" ID="lbReq5" CssClass="required" Text="*" />
       
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbState" Text="State:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList runat="server" ID="ddlState" CssClass="select" />
        <asp:Label runat="server" ID="lbReq6" CssClass="required" Text="*" />
       
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbZip" Text="Zip:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbZip" CssClass="textfield formQuarterWidth" MaxLength="15" />
        <asp:Label runat="server" ID="lbReq7" CssClass="required" Text="*" />
       
    </label><br />
    <div class="cleaner">&nbsp;</div>
    <label>
        <asp:Label runat="server" ID="lbHomePhone" Text="Home Phone:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbHomePhone" CssClass="textfield" MaxLength="20" />
       
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbCellPhone" Text="Cell Phone:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbCellPhone" CssClass="textfield" MaxLength="20" />
       
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbAltPhone" Text="Alt Phone:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbAltPhone" CssClass="textfield" MaxLength="20" />
              
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbWebsite" Text="Website:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbWebsite" CssClass="textfield" MaxLength="50" />
    </label><br />
    <label>
        <asp:Label runat="server" ID="lbStageName" Text="Stage Name:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbStageName" CssClass="textfield" MaxLength="50" />
    </label><br />
    <div class="cleaner">&nbsp;</div>
    <p>
        Although we do book parties during the week, the busiest days for our agency is Friday and Saturday evening. You will need to be available 
        at least one of those nights to be considered for a job.
    </p>
    <div class="cleaner">&nbsp;</div>
    <label><br />
        <asp:Label runat="server" ID="lbDaysAvailable" Text="What days are you available to work?:" CssClass="formLabel" />
        <asp:ListBox runat="server" ID="lbxDaysAvailable" CssClass="select" SelectionMode="Multiple"/>
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbDaysPreferred" Text="What nights / days would you prefer?:" CssClass="formLabel" />
        <asp:ListBox runat="server" ID="lbxDaysPreferred" CssClass="select" SelectionMode="Multiple" />
    </label><br />
    <div class="cleaner">&nbsp;</div>
    * Hold Control (PC) or Command (Mac) to select multiple days.
    
    <hr />
    <h3><asp:Image runat="server" ID="imgVitalStats" ImageUrl="~/images/h2VitalStats.gif" AlternateText="" /><span>Vital Stats</span></h3>
    <label><br />
        <asp:Label runat="server" ID="lbHeight" Text="Height:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbHeightFt" CssClass="textfield formQuarterWidth" /> ft
      <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbHeightIn" CssClass="textfield formQuarterWidth" /> in
        <br />
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbWeight" Text="Weight:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbWeight" CssClass="textfield formQuarterWidth" /> lbs
      
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbEyeColor" Text="Eye Color:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbEyeColor" CssClass="textfield formHalfWidth" MaxLength="10" />
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbHairColor" Text="Hair Color:" CssClass="formLabel" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbHairColor" CssClass="textfield formHalfWidth" MaxLength="20" />
    </label>
    <label><br />
        <asp:Label runat="server" ID="lbDOB" Text="Date of Birth:" CssClass="formLabel" />
        <asp:TextBox runat="server" ID="tbDOB" CssClass="textfield formHalfWidth" />
        <asp:Label runat="server" ID="lbReq9" Text="*" CssClass="required" />
      
        
    </label><br />
    <div class="cleaner">&nbsp;</div>
    <label class="full"><br />
         &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbBust" CssClass="formLabel" Text="Bust:" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbBust" CssClass="textfield formQuarterWidth" MaxLength="10" /><br />
        &nbsp;&nbsp;&nbsp;Waist:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbWaist" CssClass="textfield formQuarterWidth" MaxLength="10" /><br />
        &nbsp;&nbsp;&nbsp;Hips:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="tbHips" CssClass="textfield formQuarterWidth" MaxLength="10" /><br />
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
    
    <div id="fileUploads"><br />
        <label class="full">
            <asp:Label runat="server" ID="lbImage1" Text="Image 1:" CssClass="formLabel" /><br />
            <asp:FileUpload runat="server" ID="fuImage1" size="40"/>
            <asp:Label runat="server" ID="lbReq10" Text="*" CssClass="required" />
           
        </label><br />
        <label class="full">
            <asp:Label runat="server" ID="lbImage2" Text="Image 2:" CssClass="formLabel" /><br />
            <asp:FileUpload runat="server" ID="fuImage2" size="40" />
            <asp:Label runat="server" ID="lbReq11" Text="*" CssClass="required" />
           
        </label><br />
        <label class="full">
            <asp:Label runat="server" ID="lbImage3" Text="Image 3:" CssClass="formLabel" /><br />
            <asp:FileUpload runat="server" ID="fuImage3" size="40" />
            <asp:Label runat="server" ID="lbReq12" Text="*" CssClass="required" />
           
        </label>
         <div>Government issued ID only – Passport OR Driver’s License</div>
         <label class="full">
            <asp:Label runat="server" ID="Label1" Text="ID 1 [front]:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuID1" size="40" />
            <asp:Label runat="server" ID="Label2" Text="*" CssClass="required" />
          
        </label>
        <label class="full">
            <asp:Label runat="server" ID="Label3" Text="ID 2 [back]:" CssClass="formLabel" />
            <asp:FileUpload runat="server" ID="fuID2" size="40" />
            <asp:Label runat="server" ID="Label4" Text="*" CssClass="required" />
          
        </label>
    </div>
    <div class="cleaner">&nbsp;</div>
    
    <div align="center">        
        <p>If we are interested, someone from our office will be in touch with you shortly. </p>
        <p>
            

            <recaptcha:RecaptchaControl ID="recaptcha" runat="server" ErrorMessage="The verification words are incorrect. Please try again." 
                                        PublicKey="6Lf-8wQAAAAAAGhcwrLQOiVZlpPn0YBVgDl1Zw3Z" PrivateKey="6Lf-8wQAAAAAAIu0xZIa7U34fuaqIe6QRAYifhh9" />
            <asp:Button runat="server" ID="btnSubmitApplication" Text="Submit Application" ValidationGroup="grpEmployment" OnClick="btnSubmitApplication_Click" style="margin-top: 10px;" />
        </p>
    </div>

    </body>
    </form>
