<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Booking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server" >
<style type="text/css" >
#frameHolder #flashNav 
{
    background-image:url('../images/headerVIPForm.jpg');
    background-repeat:no-repeat;
}

#dateDiv
{
    clear: both;
}

#dateDiv select
{
    margin-left: 10px;
}

.chkTable
{
    margin-left: 100px;
    margin-top: 10px;
    margin-bottom: 20px;
}
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
    VIP express online booking forms will be given the highest priority. They are the fastest, most efficient way to reserve 
    entertainers for your party. Feel free to use this form for inquiries about rates, make a reservation, or request specific 
    dancers for your event.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <script type="text/javascript" src="../js/Functions.js"></script>
    
    <p>
    One of our friendly professional booking agents will contact you discreetly by telephone to confirm your reservation &amp; discuss 
    the details of your party. All reservations are strictly confidential. Note - All online bookings require at lest 24 hours advance 
    notice to process &amp; must be verbally confirmed with the client. 
    </p>
    <p>Please call the office directly at <span style="font-weight: bold;" >1-877-427-8747</span> to book your show with less than 24 hours notice.</p>
    
    <p runat="server" id="pIpMsg" ></p>
    <asp:PlaceHolder runat="server" ID="pHldForm" >
    
        <!-- Summary for Form -->
        <asp:ValidationSummary runat="server" ID="valSumm" ShowSummary="true" CssClass="errorBox" 
            HeaderText="<span>The following fields must be filled out in order to successfully complete your application</span><br/><br/>" ValidationGroup="grpBook" />
        
        <!-- Summary for Server check errors -->
        <div id="divServerErrors" runat="server" class="errorBox" visible="false" >
        </div>
        
        <!-- Summary for Captcah -->
        <asp:ValidationSummary runat="server" ID="valSummCaptcha" ShowSummary="true" CssClass="errorBox" Visible="false" />
            
        <!-- BEGIN - Validators -->
        <asp:RequiredFieldValidator runat="server" ID="rfv1"  Display="None" ControlToValidate="tbFirstName" ErrorMessage="<a href='#firstName'>First Name</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv2"  Display="None" ControlToValidate="tbLastName" ErrorMessage="<a href='#'>Last Name</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv3"  Display="None" ControlToValidate="tbEmail" ErrorMessage="<a href='#'>Email</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv4"  Display="None" ControlToValidate="tbAddress1" ErrorMessage="<a href='#'>Address 1</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv5"  Display="None" ControlToValidate="tbCity" ErrorMessage="<a href='#'>City</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv6"  Display="None" ControlToValidate="ddlState" ErrorMessage="<a href='#'>State</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv7"  Display="None" ControlToValidate="tbZip" ErrorMessage="<a href='#'>Zip</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv9"  Display="None" ControlToValidate="tbLocCity" ErrorMessage="<a href='#'>Location City</a>" CssClass="required"  ValidationGroup="grpBook" />
        <asp:RequiredFieldValidator runat="server" ID="rfv10" Display="None" ControlToValidate="ddlLocState" ErrorMessage="<a href='#'>Location State</a>" CssClass="required"  ValidationGroup="grpBook" />

        <asp:RegularExpressionValidator runat="server" ID="reg1"  ControlToValidate="tbHomePhone" Display="None" ErrorMessage="<a href='#'>Home phone must be in format: XXX-XXX-XXXX</a>" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="grpBook"  />
        <asp:RegularExpressionValidator runat="server" ID="reg2"  ControlToValidate="tbCellPhone" Display="None" ErrorMessage="<a href='#'>Cell phone must be in format: XXX-XXX-XXXX</a>" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="grpBook"  />
        <!-- END - Validators -->        


        <p><span class="required">*</span> Required</p>
        <h3><asp:Image runat="server" ID="imgClientContact" ImageUrl="~/images/h2ClientContact.gif" AlternateText="Client Contact Information" /></h3>

        <label>
            <asp:Label runat="server" ID="lbFirstName" Text="First Name:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbFirstName" CssClass="textfield" MaxLength="30" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbLastName" Text="Last Name:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLastName" CssClass="textfield" MaxLength="30" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbEmail" Text="Email:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbEmail" CssClass="textfield" MaxLength="50" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbAddress1" Text="Address 1:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbAddress1" CssClass="textfield" MaxLength="50" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbAddress2" Text="Address 2:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbAddress2" CssClass="textfield" MaxLength="50" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbCity" Text="City:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbCity" CssClass="textfield" MaxLength="30" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbState" Text="State:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlState" CssClass="select" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbZip" Text="Zip:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbZip" CssClass="textfield formQuarterWidth" MaxLength="5" />
            <span class="required" >*</span>
        </label>
        <div class="cleaner">&nbsp;</div>
        <label>
            <asp:Label runat="server" ID="lbHomePhone" Text="Home Phone:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbHomePhone" CssClass="textfield" MaxLength="20" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbCellPhone" Text="Cell Phone:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbCellPhone" CssClass="textfield" MaxLength="20" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbReferred" Text="Referred By:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlReferred" CssClass="select">
                <asp:ListItem Value="">Select One</asp:ListItem>
                <asp:ListItem Value="AltaVista">AltaVista</asp:ListItem>
                <asp:ListItem Value="AOL">AOL</asp:ListItem>
                <asp:ListItem Value="Ask.com" >Ask.com</asp:ListItem>
                <asp:ListItem Value="Frequent Customer">Frequent Customer</asp:ListItem>
                <asp:ListItem Value="Google">Google</asp:ListItem>
                <asp:ListItem Value="MSN">MSN</asp:ListItem>
                <asp:ListItem Value="Yahoo">Yahoo</asp:ListItem>
                <asp:ListItem Value="Friend">Friend</asp:ListItem>
                <asp:ListItem Value="Magazine">Magazine</asp:ListItem>
                <asp:ListItem Value="Newspaper" >Newspaper</asp:ListItem>
                <asp:ListItem Value="TV">TV</asp:ListItem>
                <asp:ListItem Value="Radio" >Radio</asp:ListItem>                
                <asp:ListItem Value="Other">Other</asp:ListItem>
            </asp:DropDownList>
        </label>
        
        <hr />
        <h3><asp:Image runat="server" ID="imgEventContact" ImageUrl="../images/h2EventContact.gif" AlternateText="" /><span>Event Contact Information</span></h3>
        <label>
            <asp:Label runat="server" ID="lbCopyAddress" Text="Copy Address From Above:" CssClass="formLabel" />
            <asp:CheckBox runat="server" ID="chkCopyAddress" onclick="CheckChanged(this)"/>
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocName" Text="Location Name:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocName" CssClass="textfield" MaxLength="50" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocationAddress1" Text="Location Address:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocationAddress1" CssClass="textfield" MaxLength="50" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocAddress2" Text="Loc Address 2:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocAddress2" CssClass="textfield" MaxLength="50" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocCity" Text="Location City:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocCity" CssClass="textfield" MaxLength="30" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocState" Text="Location State:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlLocState" CssClass="select" />
            <span class="required" >*</span>
        </label>
        <label>
            <asp:Label runat="server" ID="lbLocZip" Text="Location Zip:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocZip" CssClass="textfield formQuarterWidth" MaxLength="5" />
        </label>
        <div class="cleaner">&nbsp;</div>
        <label>
            <asp:Label runat="server" ID="lbLocPhone" Text="Location Phone:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbLocPhone" CssClass="textfield" MaxLength="20" />
        </label>
        <label>
            <asp:Label runat="server" ID="lbCrossSt" Text="Main Cross Street:" CssClass="formLabel" />
            <asp:TextBox runat="server" id="tbCrossSt" CssClass="textfield" MaxLength="64" />
        </label>
        
        <hr />
        <h3><asp:Image runat="server" ID="imgDetails" ImageUrl="~/images/h2EventDetails.gif" AlternateText="" /><span>Talent Pictures</span></h3>
        <div id="dateDiv" >
            <asp:Label runat="server" ID="lbDateOfEvent" Text="Date Of Event:" CssClass="formLabel" style="margin-left: 20px;" />
            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="select formHalfWidth">
                <asp:ListItem Value="01" >Jan</asp:ListItem>
                <asp:ListItem Value="02" >Feb</asp:ListItem>
                <asp:ListItem Value="03" >Mar</asp:ListItem>
                <asp:ListItem Value="04" >Apr</asp:ListItem>
                <asp:ListItem Value="05" >May</asp:ListItem>
                <asp:ListItem Value="06" >Jun</asp:ListItem>
                <asp:ListItem Value="07" >Jul</asp:ListItem>
                <asp:ListItem Value="08" >Aug</asp:ListItem>
                <asp:ListItem Value="09" >Sep</asp:ListItem>
                <asp:ListItem Value="10" >Oct</asp:ListItem>
                <asp:ListItem Value="11" >Nov</asp:ListItem>
                <asp:ListItem Value="12" >Dec</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlDay" CssClass="select formHalfWidth" />
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="select formHalfWidth" />
        </div>
        
        <div style="clear: both;" >&nbsp;</div>
        
        <label class="full">
            <asp:Label runat="server" ID="lbEventType" Text="Type Of Event:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlEventType" CssClass="select">
                  <asp:ListItem>Please Select One</asp:ListItem>
                  <asp:ListItem Text="Bachelor Party" />
                  <asp:ListItem Text="Bachelorette Party" />
                  <asp:ListItem Text="Birthday Party for Male" />
                  <asp:ListItem Text="Birthday Party for Female" />
                  <asp:ListItem Text="Bridal Shower" />
                  <asp:ListItem Text="Fraternity Party" />
                  <asp:ListItem Text="Sorority Party" />
                  <asp:ListItem Text="Holiday Party" />
                  <asp:ListItem Text="Going Away Party" />
                  <asp:ListItem Text="Corporate Event" />
                  <asp:ListItem Text="Office Party" />
                  <asp:ListItem Text="Graduation Party" />
                  <asp:ListItem Text="Promo Event" />
                  <asp:ListItem Text="Superbowl Party" />
                  <asp:ListItem Text="Sporting Event Party" />
                  <asp:ListItem Text="Divorce Party" />
                  <asp:ListItem Text="Get Well Party" />
                  <asp:ListItem Text="Just Hanging Out" />
                  <asp:ListItem Text="Passion Party" />
                  <asp:ListItem Text="Retirement Party" />
                  <asp:ListItem Text="Welcome Home" />
                  <asp:ListItem Text="Girls Night Out" />
                  <asp:ListItem Text="Guys Night Out" />
                  <asp:ListItem Text="Poker Party" />
                  <asp:ListItem Text="Pool Party" />
                  <asp:ListItem Text="Golf Outing" />
                  <asp:ListItem Text="Promotional Modeling" />
                  <asp:ListItem Text="Other" />
            </asp:DropDownList>
        </label>
        <label class="full" style="margin-bottom:0px;">
            <asp:Label runat="server" ID="lbEntertainer" CssClass="formLabel" style="width:250px;" Text="Type of Entertainer Needed:(select all that apply)" />
        </label>
        
        <div style="clear:both;" ></div>
        <table class="chkTable" >
            <tr>
                <td><asp:CheckBox runat="server" ID="chkFemale" />&nbsp;Female</td>
                <td><asp:CheckBox runat="server" ID="chkDrag" />&nbsp;Drag Queen</td>
            </tr>
            <tr>
                <td><asp:CheckBox runat="server" ID="chkMale" />&nbsp;Male</td>
                <td><asp:CheckBox runat="server" ID="chkBelly" />&nbsp;Belly Dancer</td>
            </tr>
            <tr>
                <td><asp:CheckBox runat="server" ID="chkTopless" />&nbsp;Topless Waitress</td>
                <td><asp:CheckBox runat="server" ID="chkFat" />&nbsp;Fat Mama</td>
            </tr>
            <tr>
                <td><asp:CheckBox runat="server" ID="chkLittle" />&nbsp;Little People</td>
                <td><asp:CheckBox runat="server" ID="chkImpersonator" />&nbsp;Impersonator</td>
            </tr>
        </table>
        
        <label class="full">
            <asp:Label runat="server" ID="lblNumDancers" Text="Number of Dancers Requested:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlNumDancers" CssClass="select formHalfWidth" />
            Please call our office directly for more than 6 entertainers
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbShowLength" Text="Length of Show:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlShowLength" CssClass="select">
                <asp:ListItem Value="0"  >Please Select</asp:ListItem>
                <asp:ListItem Value="15" >15 Minutes</asp:ListItem>
                <asp:ListItem Value="30" >30 Minutes</asp:ListItem>
                <asp:ListItem Value="60" >1 Hour</asp:ListItem>
                <asp:ListItem Value="90" >1.5 Hours</asp:ListItem>
                <asp:ListItem Value="120">2 Hours</asp:ListItem>
            </asp:DropDownList>
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbArrivalTime" Text="Requested Entertainment Arrival Time:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlArrivalTime" CssClass="select">
            </asp:DropDownList>
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbCrowdType" Text="Type of Crowd:" CssClass="formLabel" />
            <asp:DropDownList runat="server" ID="ddlCrowdType" CssClass="select">
                  <asp:ListItem value="Male"  >All Male</asp:ListItem>
                  <asp:ListItem value="Female">All Female</asp:ListItem>
                  <asp:ListItem value="Both"  >Mixed Crowd</asp:ListItem>
            </asp:DropDownList>
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbAgeRange" Text="Age Range Of Guests:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbAgeRange" CssClass="textfield formHalfWidth" MaxLength="20" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbNumGuests" Text="Number Of Guests:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbNumGuests" CssClass="textfield formHalfWidth" />
            <cc1:FilteredTextBoxExtender runat="server" ID="ftbe1" TargetControlID="tbNumGuests" FilterType="Numbers" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbGuestHonor" Text="Guest of Honor Name:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbGuestHonor" CssClass="textfield" MaxLength="64" />
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbSurprise" Text="Is This a Surprise Party?" CssClass="formLabel" />
            <asp:CheckBox runat="server" ID="chkSurprise" />
            Select if yes. 
        </label>
        <label class="full">
            <asp:Label runat="server" ID="lbSpecialInst" Text="Special Instructions:" CssClass="formLabel" />
            <asp:TextBox runat="server" ID="tbSpecialInst" TextMode="MultiLine" Columns="45" Rows="3" CssClass="textfield formDoubleWidth" 
                onkeypress="Count(this.value,1000)" />
        </label>
        <div class="cleaner">&nbsp;</div>
        
        <p>
            Bookings require at least 24 hours advance notice when using this online form. Please call us at 
            1-877-427-8747 to book your show with less than 24 hours notice. 
        </p>
        <p>
            After you submit this form, a Centerfold Strips booking agent will contact you to confirm pricing, 
            location, and dancer choices. Please verify that all your contact information is correct. 
        </p>
        
        <div align="center">
            <recaptcha:RecaptchaControl ID="recaptcha" runat="server" ErrorMessage="The verification words are incorrect. Please try again." 
                    PublicKey="6Lf-8wQAAAAAAGhcwrLQOiVZlpPn0YBVgDl1Zw3Z" PrivateKey="6Lf-8wQAAAAAAIu0xZIa7U34fuaqIe6QRAYifhh9" />
            <asp:Button runat="server" ID="btnBookShow" Text="Book Your Show!" OnClick="btnBookShow_Click"  ValidationGroup="grpBook" />
        </div>
    
    </asp:PlaceHolder>
    
    <asp:PlaceHolder runat="server" ID="pHldThankYou" Visible="false" >
        <div align="center" >
            <p style="font-size: 1.3em; margin-top: 25px;" >Thank You. Your online booking has been submitted.</p>
        </div>
    </asp:PlaceHolder>
    
</asp:Content>