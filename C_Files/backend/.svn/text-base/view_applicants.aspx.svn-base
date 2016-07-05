<%@ Page Title="Centerfold Strips | View Applicants" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_applicants.aspx.cs" Inherits="backend_view_applicants" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >
.applicantTable
{
    width: 938px;
    border: none;
    clear: both;
}

.applicantTable tr.nth-child-even td
{
	border-top: 2px solid #383838;
	border-bottom: 2px solid #383838;
}

.contactRow
{
    background-color: #8A181A;
    color: White;
}
</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <h3>View Applicants</h3>
    <div id="fullColumn">
        <div style="font-size:1.1em; float:right; margin-bottom:6px;"><span style="display:block; width:15px; height:15px; background-color:#8a181a; float:left; margin:-1px 6px 0px 0px;">&nbsp;</span>Background is red if contacted</div>
        
        <asp:Repeater runat="server" ID="rptrViewApplicants" >
            <HeaderTemplate>
                <table class="applicantTable" >
                <tr>
                     <th scope="col">Date Applied</th>
                     <th scope="col">Talent Name (Stage Name)</th>
                     <th scope="col">Location</th>
                     <th scope="col">Imagery</th>
                     <th width="45" scope="col">&nbsp;</th>
                     <th width="60" scope="col">&nbsp;</th>
                </tr>                  
            </HeaderTemplate>
            <ItemTemplate>
                <tr <%# GetRowColor( (bool)Eval("HasBeenContacted"), rptrViewApplicants.Items.Count ) %> >
                     <td><%# FormatDate((DateTime)Eval("DateApplied")) %></td>
                     <td><%# Eval("FirstName") %> <%# Eval("LastName") %> (<%# Eval("StageName") %>)</td>
                     <td><%# Eval("City") %>, <%# Eval("State") %></td>
                     <td><%# GetHasImages( (string)Eval("ImageOne"), (string)Eval("ImageTwo"), (string)Eval("ImageThree") ) %></td>
                     <td><asp:Button runat="server" ID="btnEdit" Text="View" CssClass="button" OnClick="OnClick_btnEdit" CommandArgument='<%# Eval("ApplicantId") %>' /></td>
                     <td><asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="button" OnClick="OnClick_btnDelete" CommandArgument='<%# Eval("ApplicantId") %>' /></td>
                </tr>                                            
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
    </div>
    

</asp:Content>

<asp:Content runat="Server" ID="Content3" ContentPlaceHolderID="deleteConfirm" >

<div id="divDeleteConfirm" runat="server" visible="false" style="float: left;" >
    <div id="deleteConfirm">
        <label>Confirm Delete:</label>
        <asp:HiddenField runat="server" ID="hiddenDeleteId" Value="" />
        <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
        <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
    </div>
</div>

</asp:Content>


