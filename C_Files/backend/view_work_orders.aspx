<%@ Page Title="Centerfold Strips | View Work Orders" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_work_orders.aspx.cs" Inherits="backend_view_work_orders" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">


<h3 runat="server" id="h3Search" visible="false" >Find a Work Order Results (Top 100 Results Displayed)</h3>
<h3 runat="server" id="h3View" visible="false" >View Work Orders</h3>
<div id="fullColumn">
    <asp:GridView runat="server" ID="grdViewWorkOrders" AutoGenerateColumns="False" 
        GridLines="Horizontal" BorderColor="#383838" 
        onselectedindexchanged="grdViewWorkOrders_SelectedIndexChanged" >
        <Columns>
        <asp:TemplateField ItemStyle-Width="25" >
                <ItemTemplate>
                    <asp:Image runat="server" ID="imgHasPaid" ImageUrl="~/images/dollar_sign.jpg" AlternateText="&nbsp;" Visible="false" />
                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="25" >
                <ItemTemplate>
                    <asp:Image runat="server" ID="imgHasTalent" ImageUrl="~/images/talent.png" AlternateText="&nbsp;" Visible="false" />
                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Event Date" DataField="EventDate" 
                DataFormatString="{0:dddd M/dd/yyyy}" ItemStyle-Width="190" >
<ItemStyle Width="190px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="180" >
                <ItemTemplate>
                    <%# Eval("FirstName") + " " + Eval("LastName") %>
                </ItemTemplate>

<ItemStyle Width="180px"></ItemStyle>
            </asp:TemplateField>
           
            <asp:BoundField HeaderText="StartTime" DataField="StartTime" 
                DataFormatString="{0:h:mm tt}" Visible="false" ItemStyle-Width="130" >
<ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="StartTime" ItemStyle-Width="120" >
                <ItemTemplate>
                    <%# CfsCommon.FormatShowLengthHumanReadable( (int)Eval("TotalShowLengthMins") ) %>
                </ItemTemplate>

<ItemStyle Width="180px"></ItemStyle>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Location" ItemStyle-Width="180" >
                <ItemTemplate>
                    <%# Eval("LocationCity") + ", " + Eval("LocationState") %>
                </ItemTemplate>

<ItemStyle Width="180px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name"> <ItemTemplate>
            <%# Eval("LocationName") %>
            </ItemTemplate>
                <ItemStyle Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event Type">
            <ItemTemplate>
            <%# Eval("EventType") %>
            </ItemTemplate>
                <ItemStyle Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnViewDetails" OnClick="OnClick_btnViewDetails" Text="DETAILS" CommandArgument='<%# Eval("JobId") %>' CssClass="button" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div> 
</asp:Content>

