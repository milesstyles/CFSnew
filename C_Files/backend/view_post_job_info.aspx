<%@ Page Title="Centerfold Strips | Post Job Information" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_post_job_info.aspx.cs" Inherits="backend_view_post_job_info" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<h3>Post Job Information</h3>
<div id="fullColumn">

    <asp:GridView runat="server" ID="grdPostJob" AutoGenerateColumns="false" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
         <asp:TemplateField ItemStyle-Width="25" >
                <ItemTemplate>
                    <asp:Image runat="server" ID="imgHasPaid" ImageUrl="~/images/dollar_sign.jpg" AlternateText="&nbsp;" Visible="false" />
                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Event Date" DataField="EventDate" DataFormatString="{0:ddd M/d/yyyy}" ItemStyle-Width="90" />
            <asp:BoundField HeaderText="Location" DataField="LocationName" ItemStyle-Width="120" />
            <asp:TemplateField ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnViewJob" Text="VIEW" OnClick="OnClick_btnViewJob" CssClass="button" CommandArgument='<%# Eval("JobId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Job Completed" ItemStyle-Width="90" >
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkJobCompleted" Checked='<%# Eval("IsJobComplete") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Amount Owed Office" DataField="OfficeNet" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:$0}" />
            <asp:TemplateField HeaderText="Balance Collected" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkBalanceCollected" Checked='<%# Eval("IsBalanceCollected") %>' Enabled='<%# GetBalCollectEnabled() %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Balance Responsibility" ItemStyle-Width="200" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblbalresp" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnUpdateJob" Text="UPDATE" OnClick="OnClick_btnUpdateJob" CssClass="button" CommandArgument='<%# grdPostJob.Rows.Count + "," + Eval("JobId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>


</div>

</asp:Content>

