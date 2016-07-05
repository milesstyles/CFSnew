﻿<%@ Page Title="Centerfold Strips | Add / Edit a Job (Event)" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_bookjob_event.aspx.cs" Inherits="backend_add_edit_bookjob_event" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:HiddenField runat="server" ID="hiddenPageMode" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenCustId" Value="" />
    <asp:HiddenField runat="server" ID="hiddenEventId" Value="" />
    <h3 runat="server" id="headerBookAJob" >Book A Job</h3>
    
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>    
    
    <div id="fullColumn">
        <div class="contentBlock" style="margin-bottom:0px;">
            <h2>Event Info</h2>
          	<table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">Contact Person:</td>
                    <td><asp:TextBox runat="server" ID="tBoxContactPerson" MaxLength="30" CssClass="textfield" /></td>
                    <td width="120" align="right">Contact Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxContactPhone" MaxLength="14" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Guest of Honor:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxGuestHonor" MaxLength="30" CssClass="textfield" /></td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4"><hr /></td>
                </tr>
                <tr>
                    <td align="right">Location Name:<br /></td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxLocName" MaxLength="50" CssClass="textfield" />
                        
                    </td>
                    <td align="right">Location Address 1:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocAddress1" MaxLength="50" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Location Address 2:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocAddress2" MaxLength="50" CssClass="textfield" /></td>
                    <td align="right">Location City: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLocCity" MaxLength="20" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Location State:<br /></td>
                    <td><asp:DropDownList runat="server" ID="ddlLocState" CssClass="select" /></td>
                    <td align="right">Location Country:</td>
                    <td><asp:DropDownList runat="server" ID="ddlLocCountry" CssClass="select" /></td>
                </tr>
                <tr>
                    <td align="right">Location ZIP:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLocZip" MaxLength="10" CssClass="textfield" /></td>
                    <td align="right">Location  Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocPhone" MaxLength="14" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Private Room:</td>
                    <td><asp:CheckBox runat="server" ID="chkPrivateRoom" /></td>
                    <td align="right">Permission From Owner:</td>
                    <td><asp:CheckBox runat="server" ID="chkOwnerPermission" /></td>
                </tr>
                <tr>
                    <td colspan="4" align="right"><hr /></td>
                </tr>
                <tr>
                    <td align="right">Date of Event:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxEventDate" MaxLength="10" CssClass="textfield" />
                        <img id="imgCalendar" src="../images/calendar_icon.gif" alt="" />
                        <span style="color: Red;" >&nbsp;*</span>
                        <ajax:CalendarExtender runat="server" ID="calendarEventDate" TargetControlID="tBoxEventDate" PopupButtonID="imgCalendar" Format="MM/dd/yyyy" />
                    </td>
                    <td align="right">Number of Guests</td>
                    <td><asp:TextBox runat="server" ID="tBoxNumGuests" MaxLength="5" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">Age Range:</td>
                    <td><asp:TextBox runat="server" ID="tBoxAgeRange" MaxLength="20" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Start Time:</td>
                    <td><asp:TextBox runat="server" ID="tBoxStartTime" MaxLength="20" CssClass="textfield" /></td>
                    <td align="right">End Time:</td>
                    <td><asp:TextBox runat="server" ID="tBoxEndTime" MaxLength="20" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Type of Event</td>
                    <td><asp:TextBox runat="server" ID="tBoxEventType" MaxLength="30" CssClass="textfield" /></td>
                    <td align="right">Surprise Party:</td>
                    <td><asp:CheckBox runat="server" ID="chkSurpriseParty" /></td>
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
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td><asp:Button runat="server" ID="btnAddEdit" Text="ADD" OnClick="OnClick_btnAddEdit" CssClass="button" /></td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>


</asp:Content>

