<%@ Page Title="Centerfold Strips | View / Edit Applicant Details" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_edit_applicant_details.aspx.cs" Inherits="backend_view_edit_applicant_details" EnableEventValidation="false" %>
<%@ Register assembly="AtomImageEditorServerControls" namespace="AtomImageEditorServerControls" tagprefix="atom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register src="../AtomImageEditor/ImageSelector.ascx" tagname="ImageSelector" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >
</style>
    <link rel="stylesheet" type="text/css" href="../AtomImageEditor/ImageEditor.css" />
    <link rel="stylesheet" type="text/css" href="../AtomImageEditor/ui-lightness/jquery-ui-1.7.2.custom.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="scriptManager">
    <Scripts>
        <asp:ScriptReference Path="~/js/jquery-1.3.2.min.js" />
        <asp:ScriptReference Path="~/js/jquery-ui-1.7.2.custom.min.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/WatermarkDefinitionLibrary.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/EditableImage.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/SelectImageButton.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/ImageSelector.js" />
    </Scripts>
    <Services>
        <asp:ServiceReference Path="~/ImageManagerWS.asmx" />
    </Services>
</asp:ScriptManager>
    
<h3 runat="server" id="headerViewEdit" >View / Edit Applicant Details</h3>

<!-- Error Msg Box -->
<div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
    <ul runat="server" id="ulErrorMsg" >
    </ul>
</div>  

<asp:HiddenField runat="server" ID="hiddenPageMode" Value='<%# CfsCommon.MODE_ADD %>' />

<!-- Used to keep track of the image file names (w/ out the path -->
<asp:HiddenField runat="server" ID="hiddenImgFn1" />
<asp:HiddenField runat="server" ID="hiddenImgFn2" />
<asp:HiddenField runat="server" ID="hiddenImgFn3" />
<asp:HiddenField runat="server" ID="hiddenImgID1" />
<asp:HiddenField runat="server" ID="hiddenImgID2" />
<div id="rightColumn">
    <div class="contentBlock">
        <h2>Applicant Info</h2>
        <table width="661" cellspacing="0" >
            <tr>
                <td width="110" align="right">&nbsp;</td>
                <td width="212" align="right">&nbsp;</td>
                <td width="110" align="right">&nbsp;</td>
                <td align="right">Applied on:  <label runat="server" id="lblDateApplied" ></label><br /><br /></td>
            </tr>
            <tr>
                <td align="right">First Name:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxFirstName" MaxLength="30" CssClass="textfield" /></td>
                <td align="right">Address 1:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxAddress1" MaxLength="50" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Last Name:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxLastName" MaxLength="30" CssClass="textfield" /></td>
                <td align="right">Address 2:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxAddress2" MaxLength="50" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Stage Name:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxStageName" MaxLength="50" CssClass="textfield" /></td>
                <td align="right">City:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxCity" MaxLength="50" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Email Address:</td>
                <td>
                    <asp:TextBox runat="server" ID="tBoxEmailAddress" MaxLength="50" CssClass="textfield" />
                    <asp:Hyperlink runat="server" ID="hlEmailSend" ImageUrl="~/images/sendemail_icon.jpg" Visible="false" Text="Send" style="margin-left: 5px;" />
                </td>
                <td align="right">State:<br /></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStateApp" CssClass="select" />
                </td>
            </tr>
            <tr>
                <td align="right">Website:</td>
                <td><asp:TextBox runat="server" ID="tBoxWebsite" MaxLength="50" CssClass="textfield" /></td>
                <td align="right">Country:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCountryApp" CssClass="select" />
                </td>
            </tr>
            <tr>
                <td align="right">&nbsp;</td>
                <td>&nbsp;</td>
                <td align="right">Zip:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxZip" MaxLength="15" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">&nbsp;</td>
                <td>&nbsp;</td>
                <td align="right">Home Phone:<br /></td>
                <td><asp:HyperLink ID="hBoxHomePhone" runat="server"><asp:TextBox runat="server" ID="tBoxHomePhone" MaxLength="20" CssClass="textfield" /></asp:HyperLink></td></tr><tr>
                <td align="right">&nbsp;</td><td>&nbsp;</td><td align="right">Cell Phone:<br /></td>
                <td><asp:HyperLink ID="hBoxCellPhone" runat="server"><asp:TextBox runat="server" ID="tBoxCellPhone" MaxLength="20" CssClass="textfield" /></asp:HyperLink></td></tr><tr>
                <td align="right">&nbsp;</td><td>&nbsp;</td><td align="right">Alternate Phone:</td><td><asp:HyperLink ID="hBoxAltPhone" runat="server"><asp:TextBox runat="server" ID="tBoxAltPhone" MaxLength="20" CssClass="textfield" /></asp:HyperLink></td></tr></table><hr />
        
        <h2>Vital Stats</h2><table width="661" cellspacing="0">
            <tr>
                <td width="110" align="right">Height:<br /></td>
                <td width="212">
                    <asp:TextBox runat="server" ID="tBoxHeightFt" MaxLength="1" CssClass="textfield" Width="30" /> ft. <asp:TextBox runat="server" ID="tBoxHeightIn" MaxLength="2" CssClass="textfield" Width="30" /> in. </td><td width="110" align="right">Bust:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxBust" MaxLength="10" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Weight:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxWeight" MaxLength="10" CssClass="textfield" /></td>
                <td align="right">Waist:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxWaist" MaxLength="10" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Eyes:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxEyeColor" MaxLength="10" CssClass="textfield" /></td>
                <td align="right">Hips:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxHips" MaxLength="10" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Hair Color:<br /></td>
                <td width="212"><asp:TextBox runat="server" ID="tBoxHairColor" MaxLength="20" CssClass="textfield" /></td>
                <td align="right">DOB: <br /></td>
                <td width="212">
                    <asp:TextBox runat="server" ID="tBoxDateOfBirth" MaxLength="10" CssClass="textfield" />
                    <asp:Label runat="server" id="lblAgeInYears" />
                </td>
            </tr>
            <tr>
                <td align="right">Experience:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxExperience" TextMode="MultiLine" Columns="25" Rows="2" MaxLength="1000" CssClass="textfield" /></td>
                <td align="right">&nbsp;</td><td>&nbsp;</td></tr><tr>
                <td align="right">Availability:<br /></td>
                <td><asp:TextBox runat="server" ID="tBoxAvailable" TextMode="MultiLine" Columns="25" Rows="2" MaxLength="255" CssClass="textfield" /></td>
                <td align="right">&nbsp;</td><td>&nbsp;</td></tr><tr>
                <td align="right">Preferred Days:</td><td><asp:TextBox runat="server" ID="tBoxPrefDays" TextMode="MultiLine" Columns="25" Rows="2" MaxLength="255" CssClass="textfield" /></td>
                <td align="right">&nbsp;</td><td>&nbsp;</td></tr></table><hr />
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="50%" align="center"><asp:Button runat="server" ID="btnDeleteApplicant" Text="Delete Applicant" OnClick="OnClick_btnDeleteApplicant" CssClass="button" /></td>
                <td width="50%" align="center"><asp:Button runat="server" ID="btnEditApplicant" Text="Edit Applicant" OnClick="OnClick_btnEditApplicant" CssClass="button" /></td>
            </tr>
        </table>            
        
        <div style="width: 100%; height: 20px" >&nbsp;</div>
        <img src="" alt="applicant image" runat="server" id="imgApplicant1" height="300" width="300" Visible="false" /> 
        <span runat="server" id="noApplicantImage1Message" Visible="false">Picture 1 Not Found</span> <br />
        <img src="" alt="applicant image" runat="server" id="imgApplicant2" Visible="false" height="300" width="300" /> 
        <span runat="server" id="noApplicantImage2Message" Visible="false">Picture 2 Not Found</span> <br />
        <img src="" alt="applicant image" runat="server" id="imgApplicant3" Visible="false" height="300" width="300" /> 
        <span runat="server" id="noApplicantImage3Message" Visible="false" >Picture 3 Not Found</span><br />
         <img src="" alt="applicant ID" runat="server" id="imgID1" Visible="false" height="300" width="300" /> 
        <span runat="server" id="noApplicantImageID1" Visible="false" >ID1 Not Found</span><br />
         <img src="" alt="applicant ID" runat="server" id="imgID2" Visible="false"  height="300" width="300"/> 
        <span runat="server" id="noApplicantImageID2" Visible="false" >ID2 Not Found</span><br />
         <div style="clear: both;" >&nbsp;</div></div></div><div id="leftColumn">
    <div class="contentBlockAlt">
        <h2>Office Info</h2><table width="212" cellspacing="0">
            <tr>
                <td width="52%">Hiring Notes:</td><td width="48%">&nbsp;</td></tr><tr>
                <td colspan="2"><asp:TextBox runat="server" ID="tBoxHiringNotes" TextMode="MultiLine" Rows="5" Columns="20" CssClass="textfield" style="width: 210px;" />
                </td>
            </tr>
            <tr>
                <td colspan="2" ><asp:CheckBox runat="server" ID="chkContacted" Text="Talent has been Contacted" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td><td align="right"><asp:Button runat="server" ID="btnUpdateInfo" Text="UPDATE INFO" OnClick="OnClick_btnUpdateInfo" CssClass="button" /></td>
            </tr>
        </table>
        <hr />
                        
        <table width="214" cellspacing="0">
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTalentType" CssClass="select" />
                </td>
            </tr>
            <tr>
                <td><asp:DropDownList runat="server" ID="ddlStateHire" CssClass="select" /></td>
            </tr>
            <tr>
                <td><asp:DropDownList runat="server" ID="ddlCountryHire" CssClass="select" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td></tr><tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnHireTalent" Text="HIRE TALENT" OnClick="OnClick_btnHireTalent" CssClass="button" />
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="cleaner">&nbsp;</div><div id="divDeleteConfirm" runat="server" visible="false" >
    <div id="deleteConfirm" >
        <label>Confirm Delete:</label> <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
        <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
    </div>
</div>


</asp:Content>

