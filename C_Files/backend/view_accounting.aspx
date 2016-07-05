<%@ Page Title="Centerfold Strips | Accounting" Language="C#" MasterPageFile="~/backend/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="view_accounting.aspx.cs" Inherits="backend_view_accounting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel File" OnClick="btnExport_Click" />
<div runat="server" id="divexport">
<h3>Basic Accounting</h3>
<div id="fullColumn">
    <div class="contentBlock" style="margin-bottom:0px;">
        
        <table width="600" cellspacing="0">
            <tr>
                <td width="33%"><label>Gross to Date: </label><asp:Label runat="server" ID="lblGrossToDate" ></asp:Label></td>
                <td width="33%"><label>Net to Date: </label><asp:Label runat="server" ID="lblNetToDate" ></asp:Label></td>
                <td width="33%"><label>Total bookings to date: </label><asp:Label runat="server" ID="lblTtlBookings" ></asp:Label></td>
            </tr>
        </table>
        <p>&nbsp;</p>
        
        <div runat="server" id="divContent" >
        
        </div>
        
        <p>&nbsp;</p>
    </div>
    </div>
</div>


</asp:Content>

