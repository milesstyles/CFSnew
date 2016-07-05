<%@ Page Title="Centerfold Strips | View Online Bookings" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_online_booking_list.aspx.cs" Inherits="backend_view_online_booking" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<div id="divDeleteConfirm" runat="server" visible="false" style="float: left;" >
    <div id="deleteConfirm">
        <label>Confirm Delete:</label>
        <asp:HiddenField runat="server" ID="hiddenDeleteId" Value="" />
        <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
        <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
    </div>
</div>

<h3>View Online Booking Form</h3>
<div id="fullColumn">
    
    <asp:EntityDataSource runat="server" ID="EntityDataSrc"  ConnectionString="name=CfsEntity" 
                        DefaultContainerName="CfsEntity" EntitySetName="OnlineBooking" EnableDelete="true" />

    <asp:GridView runat="server" ID="grdBookings" AllowSorting="true" DataKeyNames="BookingId" DataSourceID="EntityDataSrc" AutoGenerateColumns="false" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
            <asp:BoundField HeaderText="Date Entered" DataField="DateSubmitted" SortExpression="DateSubmitted" DataFormatString="{0:dddd M/d/yyyy}" ItemStyle-Width="160" />
            <asp:TemplateField HeaderText="Customer Name" SortExpression="it.FirstName,it.LastName" ItemStyle-Width="160" >
                <ItemTemplate>
                    <%# Eval("FirstName") + " " + Eval("LastName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Home Phone" DataField="HomePhone" SortExpression="HomePhone" ItemStyle-Width="110" />
            <asp:BoundField HeaderText="Cell Phone" DataField="CellPhone" SortExpression="CellPhone" ItemStyle-Width="110" />
            <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" DataFormatString="<a href='mailto:{0}'>{0}</a>" HtmlEncodeFormatString="false" ItemStyle-Width="170" />
            <asp:BoundField HeaderText="State" DataField="LocState" SortExpression="LocState" ItemStyle-Width="60" />
            <asp:BoundField HeaderText="EventDate" DataField="EventDate" SortExpression="EventDate" DataFormatString="{0:M/d/yy}" ItemStyle-Width="75" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDetails" Text="DETAILS" OnClick="OnClick_btnDetails" CommandArgument='<%# Eval("BookingId") %>' CssClass="button" />
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDelete" Text="DELETE" OnClick="OnClick_btnDelete" CommandArgument='<%# Eval("BookingId") %>' CssClass="button" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    
 </div>

</asp:Content>

