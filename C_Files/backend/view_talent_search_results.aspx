﻿<%@ Page Title="Centerfold Strips | Talent Search" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_talent_search_results.aspx.cs" Inherits="backend_view_talent_search_results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<h3 runat="server" id="headerSearchResults" >Talent Search Results for (State Name)</h3>
<div id="fullColumn">
    
    <asp:Repeater runat="server" ID="rptrResults" >
        <HeaderTemplate>
            <table width="938" cellspacing="0" class="tabData" style="clear:both;" >
                <tr>
                    <th scope="col"></th>
                     <th scope="col">Location</th>
                     <th scope="col">Talent Name</th>
                     <th scope="col">Email</th>
                     <th scope="col">Home Phone</th>
                     <th scope="col">Cell Phone</th>
                     <th width="300" scope="col">Notes</th>
                     <th>&nbsp;</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td><asp:Button runat="server" ID="btn_Delete" Visible='<%# IsInactive() %>' Text="DELETE" OnClick="OnClick_btnDelete" CommandArgument='<%# Eval("TalentId") %>' CssClass="button" /></td>
              
                    <td><%# FormatLocation( (string)Eval("City"), (string)Eval("State"), (string)Eval("Country")) %></td>
                    <td><%# Eval("DisplayName") %></td>
                    <td><%# String.Format("<a href='mailto:{0}'>{0}</a>", (string)Eval("EmailPrimary")) %></td>
                    <td><%# Eval("HomePhone") %></td>
                    <td><%# Eval("CellPhone") %></td>
                    <td><%# Eval("SpecialNotes") %></td>
                    <td><asp:Button runat="server" ID="btnDetails" Text="DETAILS" OnClick="OnClick_btnDetails" CommandArgument='<%# Eval("TalentId") %>' CssClass="button" /></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    
 </div>

</asp:Content>

