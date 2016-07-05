<%@ Page Title="Centerfold Strips | Add / Edit Employee" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_employee.aspx.cs" Inherits="add_edit_employee" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css" >
    .creditTable
    {
        margin-left: 5px;
        margin-top: 10px;
        width: 270px;
        border-collapse: collapse;
    }
    
    .creditTable tr
    {
        background-color: #CCCCCC;
        color: Black;
        height: 30px;
        border: solid 5px black;
    }    
    </style>
    <link rel="stylesheet" href="../css/thickbox.css" type="text/css" media="screen" />     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Scripts>
            <asp:ScriptReference Path="~/js/jquery-1.3.2.min.js" />
            <asp:ScriptReference Path="~/js/thickbox.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hiddenCurrentMode" Value="0" />
    
    <h3 id="headerAddEdit" runat="server" >Add Employee / Affiliate </h3>

    <!-- Error Msg Box -->
    <div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
        <ul runat="server" id="ulErrorMsg" >
        </ul>
    </div>      
    
    
    <table width="100%" cellpadding="0">
        <tr>
            <td width="35%">&nbsp;</td>
            <td width="47%">
                <asp:DropDownList runat="server" ID="ddlFullTalentList" CssClass="select" >
                </asp:DropDownList>                                                  
                <asp:Button runat="server" ID="btnGetTalentInfo" Text="GO" OnClick="OnClick_btnGetTalentInfo" CssClass="button" />
                <br />
                <br />
            </td>
            <td width="18%" align="right">
                <asp:Button runat="server" ID="btnAddEditTalent" Text="ADD TALENT" OnClick="OnClick_btnAddEditTalent" CssClass="button" style="margin-right:15px;" />
            </td>
        </tr>
    </table>
        
    <!-- BEGIN - Column ONE -->
    <div class="triCol">
        <div class="contentBlock">
            <h2>Talent Info</h2>
            <table width="100%" cellspacing="0">
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td width="60%">
                        <asp:CheckBox runat="server" ID="chkActive" Text="Active" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Talent Type</td>
                    <td>
                        <asp:HiddenField runat="server" ID="hiddenCurTalType" Value="" />
                        <asp:DropDownList runat="server" ID="ddlTalentType" CssClass="select" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right"> Additional Talent Type (for Search)</td>
                    <td>
                        <asp:HiddenField runat="server" ID="HiddenCurTalTypeAddl" Value="" />
                        <asp:CheckBoxList RepeatColumns="2" RepeatLayout="Table" ID="chkBoxAdditionalTalent" runat="server">
                        </asp:CheckBoxList>
                           </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">First Name<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxFirstName" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Last Name<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxLastName" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Stage Name<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxStageName" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Date Hired<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxDateHired" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Email<br /></td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxEmail" CssClass="textfield" MaxLength="50" />
                        <asp:Hyperlink runat="server" ID="hlEmailSend" ImageUrl="~/images/sendemail_icon.jpg" Visible="false" Text="Send" style="margin-left: 5px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Additional Email</td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxExtraEmail" CssClass="textfield" MaxLength="50" />
                        <asp:HyperLink runat="server" ID="hlExtraEmailSend" ImageUrl="~/images/sendemail_icon.jpg" Visible="false" Text="Send" style="margin-left: 5px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Address 1</td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress1" CssClass="textfield" MaxLength="30" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Address 2<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxAddress2" CssClass="textfield" MaxLength="30" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">City<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCity" CssClass="textfield" MaxLength="30" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">State<br /></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlState" CssClass="select" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Country</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCountry" CssClass="select" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Zip<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxZip" CssClass="textfield" MaxLength="15" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Home Phone<br /></td>
                    <td><asp:HyperLink runat="server" ID="hNumberLink"><asp:TextBox runat="server" ID="tBoxHomePhone" CssClass="textfield" MaxLength="20" /></asp:HyperLink></td></tr><tr>
                    <td colspan="2" align="right">Cell Phone<br /></td>
                    <td><asp:HyperLink runat="server" ID="cNumberLink"><asp:TextBox runat="server" ID="tBoxCellPhone" CssClass="textfield" MaxLength="20" /></asp:HyperLink></td></tr></table><asp:UpdatePanel runat="server" ID="uPnlAltPhoneAndWorksIn" >
            <ContentTemplate>
            
            <table>
                <asp:Repeater runat="server" ID="rptrAltPhone" onitemdatabound="Repeater1_ItemDataBound" >
                    <ItemTemplate>
                        <tr>
                       
                            <td><asp:HiddenField ID="HiddenField1" runat="server" Value="<%# Eval(TEMP_TABLE_COLUMN_PHONE_PK) %>" /></td>
                            <td width="20" ><asp:ImageButton runat="server" ID="btnAddAltPhone" ImageUrl= "../images/plus.png" alt="plus" width="20" height="20" OnClick="OnClick_btnAddAltPhone" /></td>
                            <td width="60" > <asp:TextBox runat="server" ID="tBoxAltPhoneName" CssClass="textfield" size="10" MaxLength="10" Text="<%# Eval(TEMP_TABLE_COLUMN_PHONE_NAME) %>" /></td>
                            <td><asp:HyperLink runat="server" ID="bestNumberLink"><asp:TextBox runat="server" ID="tBoxAltPhoneNum" CssClass="textfield" size="20" MaxLength="20" Text="<%# Eval(TEMP_TABLE_COLUMN_PHONE_NUM) %>" /></asp:HyperLink></td></tr></ItemTemplate></asp:Repeater><asp:Repeater runat="server" ID="rptrWorksIn" >
                    <ItemTemplate>
                        <tr>
                            <td>&nbsp;</td><td><asp:ImageButton runat="server" ID="btnAddWorksIn" ImageUrl= "../images/plus.png" alt="plus" width="20" height="20" OnClick="OnClick_btnAddWorksIn" /></td>
                            <td align="right" >Works In:</td><td>
                                <asp:DropDownList runat="server" ID="ddlStateWorksIn" CssClass="select" />
                            </td>
                        </tr>
                    </ItemTemplate>                        
                </asp:Repeater>                        
            
            </table>

            </ContentTemplate>
            </asp:UpdatePanel>

            <table>                
                <tr>
                    <td colspan="3"><hr /></td>
                </tr>            
                <tr>
                    <td colspan="2" align="right">IM Client<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxImClient" CssClass="textfield" MaxLength="20" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">IM Name<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxImName" CssClass="textfield" MaxLength="30" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">Personal Site</td><td><asp:TextBox runat="server" ID="tBoxPersonalWebsite" CssClass="textfield" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td colspan="2">Special Notes</td><td>&nbsp;</td></tr><tr>
                    <td colspan="3"><asp:TextBox runat="server" ID="tBoxSpecialNotes" TextMode="MultiLine" Columns="45" Rows="9" CssClass="textfield" MaxLength="1000" /></td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel runat="server" ID="uPnlContactTalent" Visible="false" >
            <ContentTemplate>
                <div class="contentBlockAlt">
                    <h2>Contact Talent</h2><table width="100%" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="tBoxContactTalent" TextMode="MultiLine" Columns="45" Rows="4" CssClass="textfield" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><asp:Button runat="server" ID="btnContactTalent" Text="SEND" CssClass="button" OnClick="OnClick_btnContactTalent" /></td>
                        </tr>
                    </table>
                    <div runat="server" id="divTalMsgFeedback" >
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    <!-- END - Column ONE -->
    
    <!-- BEGIN - Column TWO -->
    <div class="triCol">
        <div class="contentBlock">
            <h2>Employee Images and Media</h2><asp:UpdatePanel runat="server" ID="uPnlMedia" >
                    <ContentTemplate>
                        <table width="100%" cellspacing="0">
                            <tr>
                                <td align="right">New Talent Image</td><td>
                                    <asp:FileUpload runat="server" ID="upNewTalentImg" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkNewTalentImg" Visible="false" Target="_blank" />
                                </td>
                               
                            </tr>
                             <tr>
                                <td>
                                        <asp:Button runat="server" ID="btnNewTalImgDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                            
                                </td>
                                </tr>
                            <tr>
                                <td align="right">Thumbnail Image</td><td>
                                    <asp:FileUpload runat="server" ID="upThumbImg" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkThumbImg" Visible="false" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                            <td>
                                  <asp:Button runat="server" ID="btnThumbImgDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                              
                            </td>
                            </tr>
                            <tr>
                                <td align="right">Image 1<br /></td><td>
                                    <asp:FileUpload runat="server" ID="upImageOne" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkImageOne" Visible="false" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                            <td>
                                  <asp:Button runat="server" ID="btnImgOneDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                              
                            </td>
                            </tr>
                            <tr>
                                <td align="right">Image 2<br /></td><td>
                                    <asp:FileUpload runat="server" ID="upImageTwo" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkImageTwo" Visible="false" Target="_blank" />
                                 </td>
                            </tr>
                            <tr>
                            <td>
                               <asp:Button runat="server" ID="btnImgTwoDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                
                            </td>
                            </tr>
                            <tr>
                                <td align="right">Image 3</td><td>
                                    <asp:FileUpload runat="server" ID="upImageThree" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkImageThree" Visible="false" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                            <td>
                                 <asp:Button runat="server" ID="btnImgThreeDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                               
                            </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><a class="thickbox" href="edit_images.aspx?id=<%= Request.QueryString["empid"] %>&placeValuesBeforeTB_=savedValues&TB_iframe=true&height=800&width=1020&modal=true">Add Water Mark to Images</a></td></tr><tr>
                                <td colspan="2" align="left"><br />Video format is FLV only</td></tr><tr>
                                <td align="right">Video 1</td><td>
                                    <asp:FileUpload runat="server" ID="upVideoOne" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkVideoOne" Visible="false" />
                                    <asp:Button runat="server" ID="btnVidOneDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Video 2</td><td>
                                    <asp:FileUpload runat="server" ID="upVideoTwo" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkVideoTwo" Visible="false" />
                                    <asp:Button runat="server" ID="btnVidTwoDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Video 3</td><td>
                                    <asp:FileUpload runat="server" ID="upVideoThree" CssClass="textfield" />
                                    <asp:HyperLink runat="server" ID="linkVideoThree" Visible="false" />
                                    <asp:Button runat="server" ID="btnVidThreeDel" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <hr />
            <h2>Vital Stats</h2><table width="100%" cellspacing="0">
                <tr>
                    <td width="31%">Bust</td><td width="69%"><asp:TextBox runat="server" ID="tBoxBust" CssClass="textfield" MaxLength="10" /></td>
                </tr>
                <tr>
                    <td>Waist</td><td><asp:TextBox runat="server" ID="tBoxWaist" CssClass="textfield" MaxLength="2" /></td>
                </tr>
                <tr>
                    <td>Hips</td><td><asp:TextBox runat="server" ID="tBoxHips" CssClass="textfield" MaxLength="2" /></td>
                </tr>
                <tr>
                    <td>Height</td><td>
                        <asp:TextBox runat="server" ID="tBoxHeightFt" CssClass="textfield" Width="30" MaxLength="1" /> ft. <asp:TextBox runat="server" ID="tBoxHeightIn" CssClass="textfield" Width="30" MaxLength="2" /> in. </td></tr><tr>
                    <td>Hair Color</td><td><asp:TextBox runat="server" ID="tBoxHairColor" CssClass="textfield" MaxLength="15" /></td>
                </tr>
                <tr>
                    <td>Eye Color</td><td><asp:TextBox runat="server" ID="tBoxEyeColor" CssClass="textfield" MaxLength="10" /></td>
                </tr>
                <tr>
                    <td>Weight</td><td><asp:TextBox runat="server" ID="tBoxWeight" CssClass="textfield" MaxLength="4" /> lbs.</td></tr><tr>
                    <td>DOB<br /></td>
                    <td>
                        <asp:TextBox runat="server" ID="tBoxDateOfBirth" CssClass="textfield" />
                        <asp:Label runat="server" ID="lblAgeInYears" Text="()" />
                    </td>
                </tr>
                <tr>
                    <td>Race</td><td>
                        <asp:DropDownList runat="server" ID="ddlRace" CssClass="select" >
                            <asp:ListItem Value="" Text=" ------- " />
                            <asp:ListItem Value="Caucasian" Text="Caucasian" />
                            <asp:ListItem Value="African" Text="African" />
                            <asp:ListItem Value="Latino" Text="Latino" />
                            <asp:ListItem Value="Asian" Text="Asian" />
                            <asp:ListItem Value="Arab" Text="Arab" />
                            <asp:ListItem Value="Indian" Text="Indian" />
                            <asp:ListItem Value="Other" Text="Other" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkToys" Text="Toys" />
                        <asp:CheckBox runat="server" ID="chkFullNudeStrip" Text="Full Nude Strip" />
                        <asp:CheckBox runat="server" ID="chkLesbianShow" Text="Lesbian Show" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkPromoModel" Text="Promotional Modeling" />
                        <asp:CheckBox runat="server" ID="chkToplessBartender" Text="Topless Bartender" />                                   
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkPoleDancer" Text="Pole Dancing" />
                        <asp:CheckBox runat="server" ID="chkToplessStrip" Text="Topless Strip" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkToplessWtr" Text="Topless Waitress / Waiter" />
                        <asp:CheckBox runat="server" ID="chkPopOutCake" Text="Pop Out of Cake" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkInternetChat" Text="Live Internet Chat" />
                        <asp:CheckBox runat="server" ID="chkToplessCard" Text="Topless Card Dealer" />
                    </td>                            
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkLapDancer" Text="Lapdancing" />
                        <asp:CheckBox runat="server" ID="chkFeatureDancing" Text="Feature Dancing" />
                    </td>                            
                </tr>
                <tr>
                    <td colspan="2">Likes</td></tr><tr>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxLikes" TextMode="MultiLine" Columns="45" Rows="7" CssClass="textfield" MaxLength="255" /></td>
                </tr>
                <tr>
                    <td colspan="2">Dislikes</td></tr><tr>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxDislikes" TextMode="MultiLine" Columns="45" Rows="7" CssClass="textfield" MaxLength="255" /></td>
                </tr>
                <tr>
                    <td colspan="2">Available Costumes</td></tr><tr>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxCostumes" TextMode="MultiLine" Columns="45" Rows="7" CssClass="textfield" MaxLength="255" /></td>
                </tr>
                <tr>
                    <td colspan="2">Special Talents</td></tr><tr>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxTalents" TextMode="MultiLine" Columns="45" Rows="7" CssClass="textfield" MaxLength="255" /></td>
                </tr>
            </table>
        </div>
    </div>
    <!-- END - Column TWO -->
    
    <!-- BEGIN - Column THREE -->
    <div class="triCol">
        <div class="contentBlock">
            <h2>Feature Content</h2><asp:UpdatePanel runat="server" >
                <ContentTemplate>
                    <table width="100%" cellspacing="0">
                        <tr>
                            <td align="right">Feature Talent</td><td><asp:CheckBox runat="server" ID="chkFeatureTalent" /></td>
                        </tr>
                        <tr>
                            <td align="right">Set to Front Page</td><td><asp:CheckBox runat="server" ID="chkFeatureTalentFrontPage" /></td>
                        </tr>
                        <tr>
                            <td width="33%" align="right">Category Name<br /></td><td width="67%"><asp:TextBox runat="server" ID="tBoxCatName" CssClass="textfield" /></td>
                        </tr>
                        <tr>
                            <td>Category Details</td><td>&nbsp;</td></tr><tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="tBoxCatDetails" TextMode="MultiLine" Columns="45" Rows="7" CssClass="textfield" style="width:260px;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td><td>
                                <asp:Button runat="server" ID="btnAddUpdateCredit" Text="Add Credit" CssClass="button" OnClick="OnClick_btnAddUpdateCredits" />
                            </td>
                        </tr>
                    </table>   
                    <asp:HiddenField runat="server" ID="hiddenCreditMode" Value="1" />
                    <asp:HiddenField runat="server" ID="hiddenCreditUpdateKey" Value="" />
                    <asp:Repeater runat="server" ID="rptrCredits" >
                        <HeaderTemplate>
                            <table class="creditTable" >
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width: 150px; font-weight: bold;" ><%# Eval(TEMP_TABLE_COLUMN_CAT_NAME) %></td>
                                <td><asp:Button runat="server" ID="btnEditCredit" Text="Edit" OnClick="OnClick_btnEditCredit" CommandArgument='<%# Eval(TEMP_TABLE_COLUMN_CAT_ID) %>' CssClass="button"  /></td>
                                <td><asp:Button runat="server" ID="btnDeleteCredit" Text="Delete" OnClick="OnClick_btnDeleteCredit" CommandArgument='<%# rptrCredits.Items.Count.ToString() %>' CssClass="button" /></td>
                                <td><asp:Button runat="server" ID="btnMoveUp" Text="^" OnClick="OnClick_btnMoveUp" CommandArgument='<%# rptrCredits.Items.Count.ToString() %>' CssClass="button" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>                
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="contentBlock">
            <h2 runat="server" id="h2HoldsTitle" >Hold Information</h2><table width="100%" cellspacing="0">
                <tr>
                    <td width="27%" align="right">Date: <br /></td>
                    <td width="73%">
                        <asp:TextBox runat="server" ID="tBoxHoldDate" CssClass="textfield" />
                        <img id="imgCalendar2" src="../images/calendar_icon.gif" alt="" /> <ajax:CalendarExtender runat="server" ID="calendarHold"  TargetControlID="tBoxHoldDate" PopupButtonID="imgCalendar2" Format="MM/dd/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Location:</td><td><asp:TextBox runat="server" ID="tBoxHoldLocation" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td colspan="2" ><asp:TextBox runat="server" ID="tBoxHoldNotes" TextMode="MultiLine" Columns="45" Rows="4" CssClass="textfield" style="width:260px;" /></td>
                </tr>
            </table>
        </div>
         <div class="contentBlock"><h2>Government ID: </h2><br />
         <table>
          <tr>
                                <td align="right"></td><td>
                                
                                    <asp:FileUpload runat="server" ID="FileUploadID" CssClass="textfield" />
                                      <asp:HyperLink runat="server" ID="linkImagePhtoID" Visible="false" Target="_blank" >
                                   
                                    <img src="" alt="applicant ID" runat="server" id="imgID1" Visible="false" height="200" width="200" /> 
        </asp:HyperLink><asp:Button runat="server" ID="btnDeleteGovernmentID" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                </td>
                            </tr>
                        <!--    <tr>
                                <td align="right"></td><td>
                                
                                    <asp:FileUpload runat="server" ID="FileUploadID2" CssClass="textfield" />
                                      <asp:HyperLink runat="server" ID="linkImagePhtoID2" Visible="false" Target="_blank" >
                                   
                                    <img src="" alt="applicant ID" runat="server" id="imgID2" Visible="false" height="200" width="200" /> 
        </asp:HyperLink><asp:Button runat="server" ID="Button1" Visible="false" OnClick="OnClick_btnMediaDel" Text="X" CssClass="button" />
                                </td>
                            </tr>-->
                            </table>
         </div>
         
         </div><!-- END Column THREE --><div runat="server" id="tmpTestDiv" >
    </div>
    
    <div class="cleaner">&nbsp;</div></asp:Content>