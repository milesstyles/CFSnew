<%@ Page Title="Centerfold Strips | Job Talent Management" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_job_talent_mgmt.aspx.cs" Inherits="backend_view_job_talent_mgmt" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<h3>Job Talent Management</h3>
<div id="fullColumn">
               
    <asp:GridView runat="server" ID="grdTalMgmt" AutoGenerateColumns="false" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
            <asp:BoundField HeaderText="Event Date" DataField="EventDate" DataFormatString="{0:dddd M/d/yyyy}" ItemStyle-Width="125" />
            <asp:BoundField HeaderText="Event Location" DataField="LocationName" ItemStyle-Width="165" />
            <asp:BoundField HeaderText="Talent" DataField="DisplayName" ItemStyle-Width="300" />
            <asp:BoundField HeaderText="Start Time" DataField="StartDateTime" DataFormatString="{0:h:mm tt}" ItemStyle-Width="100" />
            <asp:TemplateField HeaderText="Show Length" ItemStyle-Width="125" >
                <ItemTemplate>
                    <%# CfsCommon.FormatShowLengthHumanReadable( (int)Eval("ShowLengthMins") ) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" Text="View Job" OnClick="OnClick_btnViewJob" CommandArgument='<%# Eval("JobId") %>' CssClass="button" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>               
               

</div>
</asp:Content>

