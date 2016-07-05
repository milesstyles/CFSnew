<%@ Page Title="Centerfold Strips | Work Order Graveyard" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_work_order_graveyard.aspx.cs" Inherits="backend_view_work_order_graveyard" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<h3>Work Order Graveyard</h3>
<div id="fullColumn">
    
   <%-- <asp:EntityDataSource runat="server" ID="EntityDataSrc"  ConnectionString="name=CfsEntity" DefaultContainerName="CfsEntity" 
            EntitySetName="Event" Include="Customer" OrderBy="it.EventDate DESC" Where="  />   --%>
    
    <asp:GridView runat="server" ID="grdOldJobs" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdOldJobs_PageIndexChanging" PageSize="50" PagerSettings-Position="Top" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
            <asp:TemplateField ItemStyle-Width="30" >
                <ItemTemplate>
                    <asp:Image runat="server" ID="imgJobCancelled" AlternateText="C" ImageUrl="~/images/cancelled.png" Visible="false" /> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Event Date" DataField="EventDate" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="120" />
            <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="270" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="customerName"></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Location" DataField="LocationName" ItemStyle-Width="230" SortExpression="LocationName" />
            <asp:BoundField HeaderText="Start Time" DataField="StartTime" DataFormatString="{0:h:mm tt}" ItemStyle-Width="180" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblIncomplete" Text="(Incomplete)" Visible="false" />
                    <asp:Button runat="server" ID="btnViewJob" Text="VIEW" CssClass="button" OnClick="OnClick_btnViewJob" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>


</asp:Content>

