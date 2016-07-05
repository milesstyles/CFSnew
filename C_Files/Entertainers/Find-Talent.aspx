<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Find-Talent.aspx.cs" Inherits="Entertainers_Find_Talent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <div runat="server" id="divError" class="errorBox" visible="false" style="padding-bottom:12px;">
        <span>Please select at least one option below</span>
    </div>
    <label class="full">
        <asp:Label runat="server" ID="lbState" CssClass="formLabel" Text="Search by State" />
        <asp:DropDownList runat="server" ID="ddlState" CssClass="select" />
    </label>
    <label class="full">
        <asp:Label runat="server" ID="lbCategory" CssClass="formLabel" Text="Search by Category" />
        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="select" />
    </label>
    <p>
        <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
    </p>
</asp:Content>

