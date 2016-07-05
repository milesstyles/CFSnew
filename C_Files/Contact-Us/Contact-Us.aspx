﻿<%@ Page Title="Centerfold Strips | Contact Us" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Contact-Us.aspx.cs" Inherits="Contact_Us" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server" >
<!--<title>Centerfold Strips contact information | 1-877-4-A-STRIP</title>-->
<meta name="keywords" content="Contact Centerfold Strips" />
<meta name="description" content="Contact centerfold strips through our website or give us a call at 1-877-427-8747 today." />
<style type="text/css" >
#frameHolder #flashNav 
{
    background-image:url('../images/headerContactUs.jpg');
    background-repeat:no-repeat;
}
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
    If you would like to contact us immediately concerning anything except employment, please 
    fill out the form below and we will respond back to you within one day.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <p>
        All employment questions can be directed to 
        <asp:HyperLink runat="server" ID="hlEmploymentEmail" NavigateUrl="mailto:employment@centerfoldstrips.com" Text="employment@centerfoldstrips.com" /> 
        or see our employment page. Thank you.
    </p>
    
    <asp:PlaceHolder runat="server" ID="pHldForm" >
        <p><span class="required">*</span> Required</p>
        <h3><span>Contact Information</span></h3>

        <!-- This appears to be the wrong image, need the right one? 
        <asp:Image runat="server" ID="imgTalentInfo" ImageUrl="~/images/h2TalentInfo.gif" AlternateText="" />
        -->
    
        <label class="full">
            <asp:Label runat="server" ID="lbName" Text="Name:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbName" CssClass="textfield" MaxLength="50" />
            <span class="required" >*</span>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ErrorMessage=" required field" ControlToValidate="tbName" CssClass="required" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbEmail" Text="Email:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbEmail" CssClass="textfield" MaxLength="50" />
            <span class="required" >*</span>
            <asp:RequiredFieldValidator runat="server" ID="rfv2" ControlToValidate="tbEmail" ErrorMessage=" required field"  CssClass="required" />
            <asp:RegularExpressionValidator runat="server" ID="rev1" ControlToValidate="tbEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ErrorMessage="Not a valid email address" CssClass="required" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbPhone" Text="Phone Number:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbPhone" CssClass="textfield" MaxLength="20" />
            <span class="required" >*</span>
            <asp:RequiredFieldValidator runat="server" ID="rfv3" ControlToValidate="tbPhone" ErrorMessage=" required field" CssClass="required" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbInquiry" Text="Inquiry:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbInquiry" Columns="45" Rows="5" TextMode="MultiLine" CssClass="textfield formDoubleWidth" MaxLength="255" /><br />
        </label>
        <div class="cleaner">&nbsp;</div>
        <div style="width: 325px; margin: 5px auto;" >
            <recaptcha:RecaptchaControl ID="recaptcha" runat="server" ErrorMessage="The verification words are incorrect. Please try again." 
                        PublicKey="6Lf-8wQAAAAAAGhcwrLQOiVZlpPn0YBVgDl1Zw3Z" PrivateKey="6Lf-8wQAAAAAAIu0xZIa7U34fuaqIe6QRAYifhh9" />
            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" style="margin-top: 10px;" />
        </div>
        
    </asp:PlaceHolder>        
    
    <asp:PlaceHolder runat="server" ID="pHldThankYou" Visible="false" >
        <div style="width: 300px; margin: 10px auto; font-size: 1.3em;" >
            <p>Your request has been submitted. Thank You.</p>    
        </div>
    </asp:PlaceHolder>
    
    <hr />
    <p><strong>Office Hours:</strong></p>
    <ul>
        <li>Monday - Thursday 10:00 AM -7:00 PM</li>
        <li>Friday &amp; Saturday 10:00 AM - 4:00 AM</li>
        <li>Sunday - Closed </li>
    </ul>
    <p>
        <strong>Booking:</strong><br />
        To book entertainer/s for your event or require more information or pricing please contact us at:<br />
        Main Office # <a href="tel:1-877-427-8747">1-877-427-8747</a><br />
        <asp:HyperLink runat="server" ID="hlWebmaster" NavigateUrl="mailto:webmaster@centerfoldstrips.com?subject=EMAIL%20FROM%20WWW.CENTERFOLDSTRIPS.COM" 
        Text="webmaster@centerfoldstrips.com" />
    </p>
    <p>
        <strong>Employment Opportunities:</strong><br />
        For information regarding employment opportunities with Centerfold Strips please contact us at: <br />
        Main Office #  <a href="tel:1-877-427-8747">1-877-427-8747</a><br />
        <asp:HyperLink runat="server" ID="hlEmployment" NavigateUrl="mailto:employment@centerfoldstrips.com?subject=EMAIL%20FROM%20WWW.CENTERFOLDSTRIPS.COM" 
            Text="employment@centerfoldstrips.com" />
    </p>
    <p>
        <strong>Media Inquiries:</strong><br />
        Talk shows, magazines, video producers, television, radio &amp; all media please contact us at:<br />
        Main Office # <a href="tel:1-877-427-8747">1-877-427-8747</a><br />
        <asp:HyperLink runat="server" ID="hlInfo" NavigateUrl="mailto:info@centerfoldstrips.com?subject=EMAIL%20FROM%20WWW.CENTERFOLDSTRIPS.COM" 
            Text="info@centerfoldstrips.com" />
    </p>
    <p>
        <strong>Legal Department:</strong><br />
        If you see our copyrighted text or photos on any other site please contact us immediately. <br />
        Main Office # <a href="tel:1-877-427-8747">1-877-427-8747</a><br />
        <asp:HyperLink runat="server" ID="hlLegal" NavigateUrl="mailto:info@centerfoldstrips.com?subject=EMAIL%20FROM%20WWW.CENTERFOLDSTRIPS.COM" Text="info@centerfoldstrips.com" />
    </p>
</asp:Content>

