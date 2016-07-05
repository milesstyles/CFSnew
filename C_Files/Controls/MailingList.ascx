<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MailingList.ascx.cs" Inherits="Controls_MailingList" %>

<div class="contentBlock" style="overflow:hidden;">
    <h2><asp:Image ID="Image1" runat="server" ImageUrl="~/images/h2MailingList.gif" AlternateText="Mailing List" /></h2>
    <p>Please enter your email address below to be notified of new entertainers, seasonal promos &amp; exclusive vip events.  </p>
    <asp:TextBox runat="server" ID="tbEmail" CssClass="mailingBox" />
    <asp:ImageButton runat="server" ID="btnSubmitEmail" ImageUrl="~/images/goButton.gif" OnClick="btnSubmitEmail_Click" />
    <asp:Label runat="server" ID="lblFeedback" style="color: Red;" />
</div>