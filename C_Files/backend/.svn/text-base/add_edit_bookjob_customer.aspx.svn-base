<%@ Page Title="Centerfold Strips | Add / Edit a Job (Customer)" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_bookjob_customer.aspx.cs" Inherits="backend_add_edit_bookajob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <h3 runat="server" id="headerBookAJob" >Book A Job</h3>
    <asp:HiddenField runat="server" ID="hiddenPageMode" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenCustId" Value="" />
    
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>     
    
    <div id="fullColumn">
        <div class="contentBlock" style="margin-bottom:0px;">
            <h2>Customer Info</h2>
          	<table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">First Name:</td>
                    <td width="215"><asp:TextBox runat="server" ID="tBoxFirstName" CssClass="textfield" MaxLength="20" /></td>
                    <td width="120" align="right">Last Name:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLastName" CssClass="textfield" MaxLength="20" /></td>
                </tr>
                <tr>
                    <td align="right">Address 1:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress1" CssClass="textfield" MaxLength="50" /></td>
                    <td align="right">Address 2:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress2" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td align="right">City: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCity" CssClass="textfield" MaxLength="20" /></td>
                    <td align="right">State:</td>
                    <td><asp:DropDownList runat="server" ID="ddlState" CssClass="select" /></td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">Zip:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxZip" CssClass="textfield" MaxLength="10" /></td>
                </tr>
                <tr>
                    <td align="right">Home Phone: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxHomePhone" CssClass="textfield" MaxLength="14" /></td>
                    <td align="right">Cell Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxCellPhone" CssClass="textfield" MaxLength="14" /></td>
                </tr>
                <tr>
                    <td align="right">Business Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxBusinessPhone" CssClass="textfield" MaxLength="14" /> Ext.:&nbsp;<asp:TextBox runat="server" ID="tBoxBusinessPhoneExt" CssClass="textfield" style="width: 40px;" MaxLength="6" /></td>
                    <td align="right">Fax:</td>
                    <td><asp:TextBox runat="server" ID="tBoxFax" CssClass="textfield" MaxLength="14" /></td>
                </tr>
                <tr>
                    <td align="right">Alternate Contact:</td>
                    <td><asp:TextBox runat="server" ID="tBoxAltContact" CssClass="textfield" MaxLength="50" /></td>
                    <td align="right">Alternate Contact Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxAltPhone" CssClass="textfield" MaxLength="14" /></td>
                </tr>
                <tr>
                    <td align="right">Email: </td>
                    <td><asp:TextBox runat="server" ID="tBoxEmail" CssClass="textfield" MaxLength="50" /></td>
                    <td align="right">Referred By:</td>
                    <td><asp:TextBox runat="server" ID="tBoxReferredBy" CssClass="textfield" MaxLength="100" /></td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td><asp:Button runat="server" ID="btnAddEdit" Text="Add" OnClick="OnClick_btnAddEdit" CssClass="button" /></td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>




</asp:Content>

