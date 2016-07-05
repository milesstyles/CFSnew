<%@ Page Title="Centerfold Strips | Online Booking Form Info" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_online_booking.aspx.cs" Inherits="backend_view_online_booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<h3>Online Booking Form Info</h3>
<div id="fullColumn">
    <div class="contentBlock" style="margin-bottom:0px;">

    <asp:Repeater runat="server" ID="rptrBookingInfo" >
        <ItemTemplate>
            <h2>Customer Info</h2>
            <table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">First Name:</td>
                    <td><asp:TextBox runat="server" ID="tBoxFirstName" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("FirstName") %>' /></td>
                    <td width="120" align="right">Last Name:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLastName" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LastName") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Address 1:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress1" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("Address1") %>' /></td>
                    <td align="right">Address 2:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress2" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("Address2") %>' /></td>
                </tr>
                <tr>
                    <td align="right">City: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCity" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("City") %>' /></td>
                    <td align="right">State:</td>
                    <td><asp:TextBox runat="server" ID="tBoxState" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("State") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Home Phone: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxHomePhone" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("HomePhone") %>' /></td>
                    <td align="right">ZIP:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxZip" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("Zip") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Cell Phone:</td>
                    <td><asp:TextBox runat="server" ID="tBoxCellPhone" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("CellPhone") %>' /></td>
                    <td align="right">Referred By:</td>
                    <td><asp:TextBox runat="server" ID="tBoxReferredBy" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("HomePhone") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Email: </td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxEmail" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("Email") %>' />
                        <asp:Hyperlink runat="server" ID="hlEmailSend" ImageUrl="~/images/sendemail_icon.jpg" Text="Send" NavigateUrl='<%# "mailto:" + Eval("Email") %>' style="margin-left: 5px;" />
                    </td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            
            <h2>Event Info:</h2>
            <table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">Event Date: </td>
                    <td><asp:TextBox runat="server" ID="tBoxEventDate" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# CfsCommon.FormatDateLong( Eval("EventDate") ) %>' /></td>
                    <td width="120" align="right">Start Time:<br /></td>
                    <td><asp:TextBox runat="server" ID="TextBox1" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# GetArrivalTime( Eval("ArrivalTime") ) %>' /></td>
                </tr>
                <tr>
                    <td align="right"># of Guests:</td>
                    <td><asp:TextBox runat="server" ID="tBoxNumGuests" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("NumGuests") %>' /></td>
                    <td align="right">Type of Event:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxEventType" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("EventType") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Show Length:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxShowLength" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# CfsCommon.FormatShowLengthHumanReadable( (int)Eval("ShowLengthMins")) %>' /></td>
                    <td align="right">Surprise Party:</td>
                    <td><asp:TextBox runat="server" ID="tBoxIsSurprise" ReadOnly="true" CssClass="textfieldreadonly" style='<%# GetSurpriseColor((bool)Eval("IsSurprise")) %>' Text='<%# GetSurpriseText((bool)Eval("IsSurprise")) %>' /></td>
                </tr>
                <tr>
                    <td align="right">Guest of Honor:</td>
                    <td><asp:TextBox runat="server" ID="tBoxGuestOfHonor" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("GuestOfHonor") %>' /></td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />

            <h2>Location Info:</h2>
            <table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">Name:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocName" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocName") %>' /></td>
                    <td width="120" align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td width="120" align="right">Address1:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocAddress1" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocAddress1") %>' /></td>
                    <td width="120" align="right">Address2:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocAddress2" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocAddress2") %>' /></td>
                </tr>
                <tr>
                    <td align="right">City:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLocCity" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocCity") %>' /></td>
                    <td align="right">State:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLocState" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocState") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Phone:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLocPhone" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocPhone") %>' /></td>
                    <td align="right">Zip:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocZip" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocZip") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Cross Street:</td>
                    <td><asp:TextBox runat="server" ID="tBoxLocCrossSt" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("LocCrossSt") %>' /></td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />

            <h2>Additional Info:</h2>
            <table width="600" cellspacing="0">
                <tr>
                    <td width="120" align="right">Talent Type:</td>
                    <td colspan="3" >
                        <asp:TextBox runat="server" ID="tBoxTalentType" ReadOnly="true" CssClass="textfieldreadonly" Width="400"  
                            Text='<%# GetTalentType((bool)Eval("WantFemale"), (bool)Eval("WantMale"), (bool)Eval("WantToplessWtr"), (bool)Eval("WantLittlePeople"), (bool)Eval("WantDragQueen"), (bool)Eval("WantBellyDancer"), (bool)Eval("WantFatMama"), (bool)Eval("WantImpersonator")) %>'
                        />
                    </td>
                </tr>
                <tr>
                    <td align="right">Type of Crowd: <br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCrowdType" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("CrowdType") %>' /></td>
                    <td align="right">Age Range:</td>
                    <td><asp:TextBox runat="server" ID="tBoxAgeRange" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("GuestAgeRange") %>' /></td>
                </tr>
                <tr>
                    <td align="right"># of Dancers:<br /></td>
                    <td><asp:TextBox runat="server" ID="TextBox12" ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("NumTalent") %>' /></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">Special Requests:</td>
                    <td colspan="3"><asp:TextBox runat="server" ID="tBoxSpecialInstructions" TextMode="MultiLine" Columns="45" Rows="3"
                                        ReadOnly="true" CssClass="textfieldreadonly" Text='<%# Eval("SpecialInstructions") %>' /></td>
                </tr>
            </table>

        </ItemTemplate>
    </asp:Repeater>

    </div>
</div>

</asp:Content>

