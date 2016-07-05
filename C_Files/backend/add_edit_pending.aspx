<%@ Page Title="Centerfold Strips | Add / Edit Pending Job" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_pending.aspx.cs" Inherits="backend_add_edit_pending" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    
    <h3 runat="server" id="headerAddEditTitle" >Add a Pending Job</h3>
    <asp:HiddenField runat="server" ID="hiddenPageMode" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenPendId" Value="" />
    
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>
    
    <div id="rightColumn">
        <div class="contentBlock">
            <table width="100%" cellspacing="0">
            <tr>
            <td align="right">
            Highlight:
            <td><asp:CheckBox ID="chkbx_Highlight_Pending" align="right" runat="server" /><br /></td>
           </td>
            
            </tr>
                <tr>
                    
                    <td width="21%" align="right">Client Name:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxClientName" MaxLength="50" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Referred By:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxReferredBy" MaxLength="30" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">City, State &amp; Zip:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCityStateZip" MaxLength="300" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Date of Event:<br /></td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxEventDate" MaxLength="10" CssClass="textfield" />
                        <img id="imgCalendar" src="../images/calendar_icon.gif" alt="" />
                        <span style="color:Red;" >&nbsp;*</span>
                        <ajax:CalendarExtender runat="server" ID="calendarDateOfEvent" TargetControlID="tBoxEventDate" PopupButtonID="imgCalendar" Format="MM/dd/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Party Type:<br /></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPartyType" CssClass="select" >
                            <asp:ListItem Value="" Text="---------------" />
                            <asp:ListItem Text="Bachelor Party" />
                            <asp:ListItem Text="Bachelorette Party" />
                            <asp:ListItem Text="Bridal Shower " />
                            <asp:ListItem Text="Birthday Party" />
                            <asp:ListItem Text="Going Away Party" />
                            <asp:ListItem Text="Retirement Party" />
                            <asp:ListItem Text="Fraternity party" />
                            <asp:ListItem Text="Sorority party" />
                            <asp:ListItem Text="Passion party" />
                            <asp:ListItem Text="Girls Night Out" />
                            <asp:ListItem Text="Guys Night Out" />
                            <asp:ListItem Text="Divorce Party" />
                            <asp:ListItem Text="Holiday Party" />
                            <asp:ListItem Text="Super Bowl Party" />
                            <asp:ListItem Text="Anniversary Party" />
                            <asp:ListItem Text="Graduation Party" />
                            <asp:ListItem Text="Get Well Party" />
                            <asp:ListItem Text="Feature Booking" />
                            <asp:ListItem Text="Nightclub Promotion" />
                            <asp:ListItem Text="Poker Party" />
                            <asp:ListItem Text="Pop Out Cake" />
                            <asp:ListItem Text="Other See Notes" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Type of Entertainer:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlEntertainerType" CssClass="select" >
                            <asp:ListItem Value="" Text="-----------" />
                            <asp:ListItem Text="Female Dancer" />
                            <asp:ListItem Text="Male Dancer" />
                            <asp:ListItem Text="Duo Show" />
                            <asp:ListItem Text="Male Mini" />
                            <asp:ListItem Text="Female Mini" />
                            <asp:ListItem Text="Novelty Show" />
                            <asp:ListItem Text="Belly Dancer" />
                            <asp:ListItem Text="Fat Mama" />
                            <asp:ListItem Text="Impersonator" />  
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Type of Guests:</td>
                    <td>
                        <asp:RadioButton runat="server" GroupName="rdoGuestType" ID="rdoMale"   Text="Male" />
                        <asp:RadioButton runat="server" GroupName="rdoGuestType" ID="rdoFemale" Text="Female" />
                        <asp:RadioButton runat="server" GroupName="rdoGuestType" ID="rdoMixed"  Text="Mixed" />
                    </td>
                </tr>
                <tr>
                    <td align="right"># of Guests:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxNumGuests" MaxLength="10" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Budget:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxBudget" MaxLength="30" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right"># of Entertainers:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxNumEntertainers" MaxLength="5" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Time Requested:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxTimeRequested" MaxLength="8" CssClass="textfield" size="8" /></td>
                </tr>
                <tr>
                    <td align="right">Quoted Price:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxQuotedPrice" MaxLength="30" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Contact Number:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxContactNumber" MaxLength="50" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Email Address:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxEmailAddress" MaxLength="50" CssClass="textfield" />
                        <asp:Hyperlink runat="server" ID="hlEmailSend" ImageUrl="~/images/sendemail_icon.jpg" Visible="false" Text="Send" style="margin-left: 5px;" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Notes: </td>
                    <td><asp:TextBox runat="server" ID="tBoxNotes" TextMode="MultiLine" Columns="45" Rows="5" MaxLength="500" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Button runat="server" ID="btnAddEdit" Text="Add" CssClass="button" OnClick="OnClick_btnAddEdit" /></td>
                </tr>
                </table>
                
            </div>
        </div>
        <div id="leftColumn"></div>
        <div class="cleaner">&nbsp;</div>
</asp:Content>

