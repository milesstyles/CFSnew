<%@ Page Title="Centerfold Strips | Add / Edit Holds" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_holds.aspx.cs" Inherits="backend_add_edit_holds" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
<asp:ScriptManager runat="server" />

<div id="divDeleteConfirm" runat="server" visible="false" >
    <div id="deleteConfirm">
        <label>Confirm Delete:</label>
        <asp:HiddenField runat="server" ID="hiddenDeleteId" Value="" />
        <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
        <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
    </div>
</div>


<h3>Add / Edit Holds</h3>
<div id="fullColumn">
    <table width="100%" cellspacing="0">
        <tr>
            <td>Date </td>
            <td>Talent </td>
            <td>Location </td>
            <td>Notes</td>
            <td width="8%">&nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <asp:TextBox runat="server" ID="tBoxDate" CssClass="textfield" />
                <img id="imgCalendar" src="../images/calendar_icon.gif" alt="" />
                <ajax:CalendarExtender ID="calendarDob" runat="server" TargetControlID="tBoxDate" PopupButtonID="imgCalendar" Format="MM/dd/yyyy" />
            </td>
            <td style="vertical-align:top;">
                <asp:DropDownList runat="server" ID="ddlFullTalentList" CssClass="select" style="width: 270px;" />
            </td>
            <td style="vertical-align:top;"><asp:TextBox runat="server" ID="tBoxLocation" CssClass="textfield" /></td>
            <td>
                <asp:TextBox runat="server" ID="tBoxNotes" TextMode="MultiLine" Columns="45" Rows="2" CssClass="textfield" />
            </td>
            <td><asp:Button runat="server" ID="btnAddEdit" Text="ADD NEW" OnClick="OnClick_btnAddEdit" CssClass="button" /></td>
        </tr>
    </table>
    <hr />
        
        
    <asp:EntityDataSource runat="server" ID="EntityDataSrc"  ConnectionString="name=CfsEntity" 
                DefaultContainerName="CfsEntity" EntitySetName="TalentHold" Include="Talent" OrderBy="it.DateStart" EnableUpdate="true" />
    
    <asp:GridView runat="server" ID="grdHolds" AllowSorting="true"  DataKeyNames="HoldId" DataSourceID="EntityDataSrc" AutoGenerateColumns="false" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
            <asp:BoundField HeaderText="Date" DataField="DateStart" SortExpression="DateStart" ItemStyle-Width="110" DataFormatString="{0:dddd MM/dd/yyyy}" ApplyFormatInEditMode="true" />
            <asp:TemplateField ItemStyle-Width="160" HeaderText="Talent" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="talentName" Text="" />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="Current Location" DataField="CurLocation" SortExpression="CurLocation" ItemStyle-Width="170" />
            <asp:BoundField HeaderText="Notes" DataField="Notes" SortExpression="Notes" ItemStyle-Width="380" />
            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="button" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDeleteHold" Text="DELETE" CssClass="button" OnClick="OnClick_btnDeleteHold" CommandArgument='<%# Eval("HoldId") %>' />
                </ItemTemplate>                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        
</div>
</asp:Content>

