<%@ Page Title="Centerfold Strips | Add / Edit Clone of a Job (Event)" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_clone_event.aspx.cs" Inherits="backend_add_edit_bookjob_event" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:EntityDataSource ID="EntityDataSource1" runat="server">
</asp:EntityDataSource>
    <asp:HiddenField runat="server" ID="hiddenPageMode" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenCustId" Value="" />
    <asp:HiddenField runat="server" ID="hiddenEventId" Value="" />
    <h3 runat="server" id="headerBookAJob" >Clone A Job</h3>
    
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>    
    
    <div id="fullColumn">
        <div class="contentBlock" style="margin-bottom:0px;">
            <h2>Event Info</h2>
          	<table width="600" cellspacing="0">
               
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
                  
                </tr>
                
                <tr>
                    <td align="right">Start Time:</td>
                    <td><asp:TextBox runat="server" ID="tBoxStartTime" MaxLength="20" CssClass="textfield" /></td>
                    <td align="right">End Time:</td>
                    <td><asp:TextBox runat="server" ID="tBoxEndTime" MaxLength="20" CssClass="textfield" /></td>
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

