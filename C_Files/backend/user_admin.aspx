<%@ Page Title="Centerfold Strips | User Admin" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="user_admin.aspx.cs" Inherits="backend_user_admin" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:HiddenField runat="server" ID="hiddenUserId" Value="" />

    <h3>User Admin</h3>
    
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>    
    
    <div id="leftColumn">
        <div class="contentBlock">
            <h2>Active Users</h2>
            <asp:EntityDataSource runat="server" ID="entityDataSrcActive" ConnectionString="name=CfsEntity" DefaultContainerName="CfsEntity" EntitySetName="CfsUser" 
                    Include="CfsUserType" Where="it.IsActive = True" OrderBy="it.UserName" />
            
            <asp:GridView runat="server" ID="grdActiveUsers" AutoGenerateColumns="false" DataSourceID="entityDataSrcActive" >
                <Columns>
                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                    <asp:TemplateField HeaderText="UserType" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUserType" Text="" />
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField ShowHeader="false" >                    
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnEdit" Text="EDIT" CssClass="button" OnClick="OnClick_btnEdit" CommandArgument='<%# Eval("UserId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="contentBlock">
            <h2>Inactive Users</h2>
            <asp:EntityDataSource runat="server" ID="entityDataSrcInactive" ConnectionString="name=CfsEntity" DefaultContainerName="CfsEntity" EntitySetName="CfsUser" 
                    Include="CfsUserType" Where="it.IsActive = False" OrderBy="it.UserName" />
                    
            <asp:GridView runat="server" ID="grdInactiveUsers" AutoGenerateColumns="false" DataSourceID="entityDataSrcInactive" >
                <Columns>
                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                    <asp:TemplateField HeaderText="UserType" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUserType" Text="" />
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField ShowHeader="false" >                    
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnEdit" Text="EDIT" CssClass="button" OnClick="OnClick_btnEdit" CommandArgument='<%# Eval("UserId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false" >                    
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="DELETE" CssClass="button" OnClick="OnClick_btnDelete" CommandArgument='<%# Eval("UserId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>        
    </div>
    <div id="rightColumn">
        <div class="contentBlock">
        <h2>Add / Edit User</h2>
            <table width="661" cellspacing="0">
            <tr>
                <td width="113" align="right">User Name</td>
                <td width="220"><asp:TextBox runat="server" ID="tBoxUserName" MaxLength="30" CssClass="textfield" /></td>
                <td width="328" rowspan="6" class="topAlign">
                    Section Access<br /><br />
                    <asp:CheckBox runat="server" ID="chkActive" Text="Active" /><br /><br />
                    <asp:CheckBox runat="server" ID="chkUserAdmin" Text="User Admin" /><br />
                    <asp:CheckBox runat="server" ID="chkEmployeeMgmt" Text="Employee Management" /><br />
                    <asp:CheckBox runat="server" ID="chkPendingJobs" Text="Pending Jobs" /><br />
                    <asp:CheckBox runat="server" ID="chkWorkOrders" Text="Work Orders" /><br />
                    <asp:CheckBox runat="server" ID="chkFranchiseMgmt" Text="Franchise Management" /><br />
                    <asp:CheckBox runat="server" ID="chkAccounting" Text="Accounting" /><br />
                    <asp:CheckBox runat="server" ID="chkBalanceCollected" Text="Balance Collected" /><br />
                    <asp:CheckBox runat="server" ID="chkViewCCinfo" Text="View Credit Card Info" /><br />
                </td>
            </tr>
            <tr>
                <td align="right">User Type</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlUserType" OnSelectedIndexChanged="OnChange_ddlUserType" CssClass="select" CausesValidation="false" AutoPostBack="true" >
                        <asp:ListItem Value="" Text="Choose a User Type" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Password</td>
                <td><asp:TextBox runat="server" ID="tBoxPassword" TextMode="Password" MaxLength="20" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td align="right">Password Again</td>
                <td><asp:TextBox runat="server" ID="tBoxPasswordAgain" TextMode="Password" MaxLength="20" CssClass="textfield" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Notes</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><asp:TextBox runat="server" ID="tBoxNotes" TextMode="MultiLine" Rows="4" Columns="50" MaxLength="255" CssClass="textfield" /></td>
                <td class="topAlign">
                    <!-- Removed for now, need client feedback 
                    <select name="select2" class="select" id="select2">
                    <option>Franchise Specific - What is this? TO DO</option>
                    </select>
                    -->
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="OnClick_btnSubmit" CssClass="button" /></td>
            </tr>
            </table>
        </div>
    </div>
    <div class="cleaner">&nbsp;</div>

</asp:Content>

