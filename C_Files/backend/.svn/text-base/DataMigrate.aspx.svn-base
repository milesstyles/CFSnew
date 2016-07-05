<%@ Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="DataMigrate.aspx.cs" Inherits="backend_DataMigrate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >
body
{
    color: White;
}

.btnList li
{
    padding: 10px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <ul class="btnList" >
        <li>
            <asp:Button runat="server" ID="btnMigratePendJobs" Text="Migrate Pending Jobs Data" OnClick="OnClick_btnMigratePendJobs" />
            <asp:Button runat="server" ID="btnClearPendJobs" Text="Clear Pending Jobs" OnClick="OnClick_btnClearPendJobs" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnMigrateApplicants" Text="Migrate Applicants" OnClick="OnClick_btnMigrateApplicants" />
            <asp:Button runat="server" ID="btnClearApplicants" Text="Clear Applicants" OnClick="OnClick_btnClearApplicants" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnMigrateTalent" Text="Migrate Talent Data" OnClick="OnClick_btnMigrateTalent" Enabled="false" />
            <asp:Button runat="server" ID="btnClearTalent" Text="Clear Talent Data" OnClick="OnClick_btnClearTalent" Enabled="false" />
            <label>Disabled to protect currently migrated data.</label>
        </li>
        <li>
            <asp:Button runat="server" ID="btnMigrateCustEventsJobs" Text="Migrate Customers/Events/Jobs" OnClick="OnClick_btnMigrateCustEventsJobs" />
            <asp:Button runat="server" ID="btnClearCustEventsJobs" Text="Clear Customers/Events/Jobs Table" OnClick="OnClick_btnClearCustEventsJobs" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnMigrateTalToJob" Text="Migrate Talent To Job" OnClick="OnClick_btnMigrateTalToJob" />
            <asp:Button runat="server" ID="btnClearTalToJob" Text="Clear Talent To Job Table" OnClick="OnClick_btnClearTalToJob" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnUpdateTalPayroll" Text="Update Total Dancer Payroll" OnClick="OnClick_btnUpdateTalPayroll" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnUpdateTalName" Text="Update Talent Display Name" OnClick="OnClick_btnUpdateTalDisplayName" />
        </li>
        <li>
            <asp:Button runat="server" ID="btnUpdateTalWorksIn" Text="Update Talent WorksIn" OnClick="OnClick_btnUpdateTalWorksIn" />
        </li>
    </ul>
    
    <div id="divErrorLog" runat="server" >
    </div>
    
</asp:Content>

