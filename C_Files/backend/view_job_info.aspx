<%@ Page Title="Centerfold Strips | View Job Details" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_job_info.aspx.cs" Inherits="backend_view_job_info" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
     <h3>View Job Details</h3>
     <div id="fullColumn">
          <div class="contentBlock" style="margin-bottom:0px;">
               <table cellspacing="0" class="fixWidth">
                    <tr>
                         <td colspan="2" align="left">Job Created on:
                              <asp:Label runat="server" ID="lblJobCreatedDate" ></asp:Label></td>
                         <td colspan="2" align="right">Work Order #:
                              <asp:Label runat="server" ID="lblWorkOrderNum" ></asp:Label></td>
                    </tr>
                    <tr class="hidePrint">
                         <td colspan="4"><hr /></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptrClientInfo" >
                         <ItemTemplate>
                              <tr>
                                   <td><h2>Client Info:</h2></td>
                                   <td width="30%">&nbsp;</td>
                                   <td colspan="2" align="right"><asp:Button runat="server" ID="btnSendClientConfirm" CssClass="button hidePrint" Text="Send Client Confirmation" OnClick="OnClick_btnSendClientConfirm" CommandArgument='<%# Eval("CustomerId") %>' />
                                        <asp:Label runat="server" ID="lblClientConfirmSent" CssClass="red" style="display: block;" >Client Confirmation Sent</asp:Label></td>
                              </tr>
                              <tr>
                                   <td align="right">Contracted By:</td>
                                   <td><%# Eval("FirstName") + " " + Eval("LastName") %></td>
                                   <td align="right">Home Phone:</td>
                                   <td width="30%"><a href="tel:<%# Eval("HomePhone") %>"><%# Eval("HomePhone") %></a></td>
                              </tr>
                              <tr>
                                   <td align="right">Address 1:</td>
                                   <td><%# Eval("Address1") %></td>
                                   <td align="right">Business Phone:<br /></td>
                                   <td><a href="tel:<%# Eval("BusinessPhone") %><%# (Eval("BusinessPhoneExt") != null && !String.IsNullOrEmpty(Eval("BusinessPhoneExt").ToString()) ? " x" + Eval("BusinessPhoneExt") : "") %>"><%# Eval("BusinessPhone") %><%# (Eval("BusinessPhoneExt") != null && !String.IsNullOrEmpty(Eval("BusinessPhoneExt").ToString()) ? " x" + Eval("BusinessPhoneExt") : "") %></a></td>
                              </tr>
                              <tr>
                                   <td align="right">Address 2:<br /></td>
                                   <td><%# Eval("Address2") %></td>
                                   <td align="right">Cell Phone:<br /></td>
                                   <td><a href="tel:<%# Eval("CellPhone") %>"><%# Eval("CellPhone") %></a></td>
                              </tr>
                              <tr>
                                   <td align="right">City:<br /></td>
                                   <td><%# Eval("City") %></td>
                                   <td align="right">Fax:<br /></td>
                                   <td><%# Eval("Fax") %></td>
                              </tr>
                              <tr>
                                   <td align="right">State:<br /></td>
                                   <td><%# Eval("State") %></td>
                                   <td align="right">Alternate Contact:<br /></td>
                                   <td><%# Eval("AltContactName") %></td>
                              </tr>
                              <tr>
                                   <td align="right">Zip:<br /></td>
                                   <td><%# Eval("Zip") %></td>
                                   <td align="right">Alt Contact Phone:</td>
                                   <td><%# Eval("AltContactPhone") %></td>
                              </tr>
                              <tr>
                                   <td align="right">E-mail address:</td>
                                   <td><a href='<%# "mailto:" + Eval("Email") %>' ><%# Eval("Email") %></a></td>
                                   <td align="right">Referred By:</td>
                                   <td><%# Eval("ReferredBy") %></td>
                              </tr>
                              <tr>
                                   <td align="right">&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td align="right">&nbsp;</td>
                                   <td align="right"><asp:Button runat="server" ID="btnEditCustInfo" Text="Edit Info" OnClick="OnClick_btnEditCustInfo" CommandArgument='<%# Eval("CustomerId") %>' CssClass="button hidePrint" /></td>
                              </tr>
                         </ItemTemplate>
                    </asp:Repeater>
                    <tr class="hidePrint">
                         <td colspan="4" align="right"><hr /></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptrEventInfo" >
                         <ItemTemplate>
                              <tr>
                                   <td><h2>Event Info:</h2></td>
                                   <td>&nbsp;</td>
                                   <td colspan="2" align="right" class="red"><asp:Label runat="server" ID="lblSurprise" >This is a surprise party</asp:Label></td>
                              </tr>
                              <tr>
                                   <td align="right">Contact Person:</td>
                                   <td><%# Eval("ContactPerson") %></td>
                                   <td align="right">Guest of Honor:</td>
                                   <td><%# Eval("GuestOfHonor") %></td>
                              </tr>
                              <tr>
                                   <td align="right">Date of Event:</td>
                                   <td><%# CfsCommon.FormatDate( Eval("EventDate"), "MM/dd/yyyy") %></td>
                                   <td align="right">Type of Function:</td>
                                   <td><%# Eval("EventType") %></td>
                              </tr>
                              <tr>
                                   <td align="right">Location Name:</td>
                                   <td><%# Eval("LocationName") %></td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                              </tr>
                              <tr>
                                   <td align="right">Address 1:</td>
                                   <td><%# Eval("LocationAddress1") %></td>
                                   <td align="right">Address 2:</td>
                                   <td><%# Eval("LocationAddress2") %></td>
                              </tr>
                              <tr>
                                   <td align="right">City:</td>
                                   <td><%# Eval("LocationCity") %></td>
                                   <td align="right">Number of Guests:<br /></td>
                                   <td><%# Eval("NumGuests") %></td>
                              </tr>
                              <tr>
                                   <td align="right">State:</td>
                                   <td><%# Eval("LocationState") %></td>
                                   <td align="right">Age Range:</td>
                                   <td><%# Eval("AgeRange") %></td>
                              </tr>
                              <tr>
                                   <td align="right">Country:</td>
                                   <td><%# Eval("LocationCountry") %></td>
                                   <td align="right">Start Time:<br /></td>
                                   <td><%# CfsCommon.FormatTime( Eval("StartTime") ) %></td>
                              </tr>
                              <tr>
                                   <td align="right">Zip:<br /></td>
                                   <td><%# Eval("LocationZip") %></td>
                                   <td align="right">End Time:</td>
                                   <td><%# CfsCommon.FormatTime( Eval("EndTime") ) %></td>
                              </tr>
                              <tr>
                                   <td align="right">Contact Phone:<br /></td>
                                   <td><a href="tel:<%# Eval("ContactPhone") %>"><%# Eval("ContactPhone") %></a></td>
                                   <td align="right">&nbsp;</td>
                                   <td>&nbsp;</td>
                              </tr>
                              <tr>
                                   <td align="right">Location Phone:</td>
                                   <td><a href="tel:<%# Eval("LocationPhone") %>"><%# Eval("LocationPhone") %></a></td>
                                   <td align="right">&nbsp;</td>
                                   <td><a href="<%# GetMapsUrl((string)Eval("LocationAddress1"), (string)Eval("LocationAddress2"), (string)Eval("LocationCity"), (string)Eval("LocationState"), (string)Eval("LocationZip")) %>" target="_blank" >Map The Location</a></td>
                              </tr>
                               <tr>
                                   <td align="right">Guest Type:</td>
                                   <td><%# Eval("GuestType") %></a></td>
                              </tr>
                              <tr>
                                   <td align="right">&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td align="right">&nbsp;</td>
                                   <td align="right"><asp:Button runat="server" ID="btnEditEventInfo" Text="Edit Info" OnClick="OnClick_btnEditEventInfo" CommandArgument='<%# Eval("EventId") %>' CssClass="button hidePrint"  /></td>
                              </tr>
                         </ItemTemplate>
                    </asp:Repeater>
               </table>
               <table cellspacing="0" class="fixWidth">
                    <tr class="hidePrint">
                         <td colspan="4" align="right"><hr /></td>
                    </tr>
                    <tr>
                         <td width="25%"><h2>Talent Info:</h2></td>
                         <td width="25%">&nbsp;</td>
                         <td width="25%" align="right">Total Talent:</td>
                         <td width="25%"><span runat="server" id="spanTotalTalent" ></span></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptrTalentInfo" >
                         <ItemTemplate>
                              <tr>
                                   <td width="25%" align="right">Talent:</td>
                                   <td width="25%"><%# Eval("DisplayName") %></td>
                                   <td width="25%" align="right">Length of Show:</td>
                                   <td width="25%"><%# Eval("ShowLength") %></td>
                              </tr>
                              <tr>
                                   <td width="25%" align="right">Contact #:</td>
                                   <td width="25%"><%# Eval("ContactPhone") %></td>
                                   <td width="25%" align="right">Start Time:</td>
                                   <td width="25%"><%# Eval("StartTime") %> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        Payroll: <%# "$" + Eval("Payroll")  %></td>
                              </tr>
                              <tr class="showPrint">
                                   <td width="25%" align="right" class="costume">Costume:</td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%">&nbsp;</td>
                              </tr>
                              <tr class="hidePrint">
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%">&nbsp;</td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%" align="right">
                                    <asp:Button runat="server" ID="btnSendTalentConfirm" Text="Send Dancer Confirmation" CssClass="button hidePrint" OnClick="btnSendTalentConfirm_Click" 
                                        CommandArgument='<%# Eval("TalentId") + "|" + Eval("CustomerId") %>' />
                                    <asp:Label runat="server" ID="lblTalentConfirmSent" class="red" >Dancer Confirmation Sent</asp:Label>
                                    </td>
                              </tr>
                         </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                         <td colspan="4">Responsible for balance: &nbsp;&nbsp;<span runat="server" id="spanBalResponsible" ></span></td>
                    </tr>
               </table>
               
               <table cellspacing="0" class="fixWidth">
                    <tr>
                         <td colspan="4" align="right"><hr /></td>
                    </tr>
                    
                    <asp:Repeater runat="server" ID="rptrJobInfo" >
                         <ItemTemplate>
                              <tr>
                                   <td width="25%"><h2>Payment Info:</h2></td>
                                   <td width="25%">&nbsp;</td>
                                   <td width="25%"><h2>Fees:</h2></td>
                                   <td width="25%">&nbsp;</td>
                              </tr>
                              <tr>
                                   <td width="25%" align="right">CC Type:</td>
                                   <td width="25%"><%# Eval("CCTypeBrand")%></td>
                                   <td width="25%" align="right">Total Gross:<br /></td>
                                   <td width="25%"><%# "$" + Eval("GrossIncome") %></td>
                              </tr>
                              <tr>
                                   <td width="25%" align="right">Name on Card:</td>
                                   <td width="25%"><%# maskCCinfo( (string)Eval("CCName")) %></td>
                                   <td width="25%" align="right">Total Expenses:</td>
                                   <td width="25%"><%# "$" + Eval("ExpenseTotal")%></td>
                              </tr>
                              <tr>
                                   <td width="25%" align="right">CC Number:<br /></td>
                                   <td width="25%"><%# maskCCinfo(MaskCCNum((string)Eval("CCNum")))%></td>
                                   <td width="25%" align="right">Office Net:</td>
                                   <td width="25%"><%# "$" + Eval("OfficeNet")%></td>
                              </tr>
                              <tr>
                                   <td width="25%" align="right">Expiration:</td>
                                   <td width="25%"><%# maskCCinfo( (string)Eval("CCExp") )%></td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%">&nbsp;</td>
                              </tr>
                              <tr>
                                   <td width="25%"><br />
                                        <br />
                                        Special Instructions:</td>
                                   <td width="25%">&nbsp;</td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%">&nbsp;</td>
                              </tr>
                              <tr>
                                   <td colspan="4" align="left"><%# Eval("SpecialInstructions") %></td>
                              </tr>
                              <tr class="hidePrint">
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%">&nbsp;</td>
                                   <td width="25%" align="right">&nbsp;</td>
                                   <td width="25%" align="right"><asp:Button runat="server" ID="btnEditJobInfo" Text="Edit Info" OnClick="OnClick_btnEditJobInfo" CommandArgument='<%# Eval("JobId") %>' CssClass="button" /></td>
                              </tr>
                         </ItemTemplate>
                    </asp:Repeater>
               </table>
              
               <table cellspacing="0" class="fixWidth">
                    <tr>
                         <td colspan="4" align="right"><hr /></td>
                    </tr>
                    <tr>
                         <td width="25%" align="right">Day &amp; Date: </td>
                         <td width="25%"><asp:Label runat="server" ID="lblDayAndDate" style="font-weight: bold;" ></asp:Label></td>
                         <td width="25%" align="right">Start Time:</td>
                         <td width="25%"><asp:Label runat="server" ID="lblStartTime" style="font-weight: bold;" ></asp:Label></td>
                    </tr>
                    <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td><asp:Button runat="server" ID="btnCloneJobInfo" Text="Clone Job Info" OnClick="OnClick_btnCloneJobInfo" CommandArgument='<%# Eval("JobId") %>' CssClass="button" /></td>
                    </tr>
               </table>
               <table cellspacing="0" style="width:600px;">
                    <tr class="showPrint" style="display:block;">
                         <td align="right" style="width:200px;">Security Name &amp; Phone: </td>
                         <td><img src="../images/signLine.gif" width="400" height="15" alt="" /></td>
                    </tr>
                    <tr class="showPrint" style="display:block;">
                         <td align="right" style="width:200px;">Hotel Room:</td>
                         <td><img src="../images/signLine.gif" width="400" height="15" alt="" /></td>
                    </tr>
                    <tr class="showPrint" style="display:block;">
                         <td align="right" style="width:200px;">Directions Attached:</td>
                         <td ><input type="checkbox" name="checkbox" id="checkbox" /></td>
                    </tr>
               </table>
          </div>
     </div>
</asp:Content>
