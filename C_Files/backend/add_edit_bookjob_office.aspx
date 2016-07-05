﻿<%@ Page Title="Centerfold Strips | Add / Edit Job (Office)" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="add_edit_bookjob_office.aspx.cs" Inherits="backend_add_edit_bookjob_office" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <style type="text/css" >
.textfieldreadonly
{
    background-color: #333333;
    color: White;
    padding: 1px 2px;
    border: none;
    font: 1em "Trebuchet MS", Tahoma, Arial, Helvetica;    
}

#dblBookConfirm 
{
    color: Black;
    background-color: Red;
    border: 3px solid white;
    width: 300px;
    padding: 10px;    
    float: right;
    font-size: 1.2em;
    font-weight: bold;
}

#dblBookConfirm label
{
    display: block;
    padding-bottom: 3px;
}
</style>
<script type="text/javascript" src="../js/Functions.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <asp:ScriptManager runat="server" />
    <asp:HiddenField runat="server" ID="hiddenEventId" Value="" />
    <asp:HiddenField runat="server" ID="hiddenJobId" Value="" />
    <asp:HiddenField runat="server" ID="hiddenPageMode" Value="0" />
    <asp:HiddenField runat="server" ID="hiddenEventDate" Value="" />
    
    <h3>Book A Job</h3>
    
    <asp:UpdatePanel runat="server" ID="uPnlErrors" UpdateMode="Conditional" >
        <ContentTemplate>
            <div runat="server" id="divErrorMsg" class="errorBox" >
                <ul runat="server" id="ulErrorMsg" >
                </ul>
            </div>
            <div id="divDblBookConfirm" runat="server" visible="false" >
                <div id="dblBookConfirm" >
                    <p runat="server" id="pBookMsg" ></p>
                    <label>Are you sure you want to double book?</label>
                    <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
                    <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
                </div>
            </div>                
        </ContentTemplate>
    </asp:UpdatePanel>        
    <div id="fullColumn">

        <div class="contentBlock" style="margin-bottom:0px;">
            <h2>Talent &amp; Office Info</h2>
            
            <asp:UpdatePanel runat="server" ID="uPnlTalentList" UpdateMode="Conditional" >
                <ContentTemplate>
                    
                    <label>Total Length Of Show:  </label><asp:Label runat="server" ID="lblTotalShowLen" ></asp:Label><br />
                    <table cellspacing="0" >
                        <tr>
                            <th>&nbsp;</th>
                            <th>Talent</th>
                            <th>Start Time</th>
                            <th>Show Length</th>
                            <th>Payroll</th>
                            <th>&nbsp;</th>
                        </tr>                            

                    <asp:Repeater runat="server" ID="rptrTalentList" >
                        <ItemTemplate>
                        <tr>
                            <td><%# rptrTalentList.Items.Count + 1 %>.</td>
                            <td><asp:TextBox runat="server" ID="tBoxTalentName1" ReadOnly="true" CssClass="textfieldreadonly" size="65" Text='<%# Eval(TEMP_TABLE_COLUMN_NAME) %>' /></td>
                            <td><asp:TextBox runat="server" ID="tBoxStartTime1"  ReadOnly="true" CssClass="textfieldreadonly" size="10" Text='<%# Eval(TEMP_TABLE_COLUMN_START_TIME) %>' /></td>
                            <td><asp:TextBox runat="server" ID="tBoxShowLength1" ReadOnly="true" CssClass="textfieldreadonly" size="10" Text='<%# CfsCommon.FormatShowLengthHumanReadable( (string)Eval(TEMP_TABLE_COLUMN_SHOW_LENGTH) ) %>'   /></td>
                            <td><asp:TextBox runat="server" ID="tBoxPayroll1"    ReadOnly="true" CssClass="textfieldreadonly" size="6" Text='<%# Eval(TEMP_TABLE_COLUMN_PAYROLL) %>' /></td>
                            <td><asp:Button runat="server" ID="btnEditTalent"   Text="Edit" OnClick="OnClick_btnEditTalent" CssClass="button" CommandArgument='<%# Eval(TEMP_TABLE_COLUMN_TALENT_ID) + "," + Eval(TEMP_TABLE_COLUMN_TALTOJOB_UID) %>' /></td>                            
                            <td><asp:Button runat="server" ID="btnDeleteTalent" Text="X"    OnClick="OnClick_btnDeleteTalent" CssClass="button" CommandArgument='<%# Eval(TEMP_TABLE_COLUMN_TALENT_ID) + "," + Eval(TEMP_TABLE_COLUMN_TALTOJOB_UID) %>' /></td>
                        </tr>                                
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <asp:PlaceHolder runat="server" ID="pAddTalentControls" >
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:DropDownList runat="server" ID="ddlTalentList" CssClass="select" /></td>
                            <td><asp:TextBox runat="server" ID="tBoxStartTime" size="10" MaxLength="10" CssClass="textfield" /></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlShowLength" CssClass="select" >
                                    <asp:ListItem Value="15"  Text="15 mins" />
                                    <asp:ListItem Value="30"  Text="30 mins" />
                                    <asp:ListItem Value="45"  Text="45 mins" />
                                    <asp:ListItem Value="60"  Text="1    hour" />
                                    <asp:ListItem Value="75"  Text="1.25 hours" />
                                    <asp:ListItem Value="90"  Text="1.5  hours" />
                                    <asp:ListItem Value="105" Text="1.75 hours" />
                                    <asp:ListItem Value="120" Text="2    hours" />
                                    <asp:ListItem Value="150" Text="2.5  hours" />
                                    <asp:ListItem Value="180" Text="3    hours" />
                                    <asp:ListItem Value="210" Text="3.5  hours" />
                                    <asp:ListItem Value="240" Text="4    hours" />
                                    <asp:ListItem Value="270" Text="4.5  hours" />
                                    <asp:ListItem Value="300" Text="5    hours" />
                                    <asp:ListItem Value="330" Text="5.5  hours" />
                                    <asp:ListItem Value="360" Text="6    hours" />
                                </asp:DropDownList>
                            </td>
                            <td><asp:TextBox runat="server" ID="tBoxPayroll" size="6" MaxLength="5" CssClass="textfield" /></td>
                            <td><asp:Button runat="server" ID="btnAddTalent" Text="ADD" OnClick="OnClick_btnAddTalent" CssClass="button" /></td>
                        </tr>      
                    </asp:PlaceHolder>                                                                                  
                    
                    </table>       
                    
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <h2>&nbsp;</h2>
            <table width="700" cellspacing="0" style="background-color:#611113; color:#fff; padding:10px;">
                <tr>
                    <td colspan="2"><h2>Office Info</h2></td>
                    <td width="270" rowspan="15" style="vertical-align:top;">

                    <asp:UpdatePanel runat="server" ID="uPnlAccting" UpdateMode="Conditional" >
                        <ContentTemplate>

                        <!-- BEGIN - Inner Table -->
                        <table width="100%" cellspacing="0" style="background-color:#333333; color:#fff; padding:6px;" >
                            <tr>
                                <td width="47%"><h2>Income</h2></td>
                                <td width="53%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">Entertainment Total:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxEntTotal" CssClass="textfield"  size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Limo Total:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxLimoTotal" CssClass="textfield"  size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Location Total:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxLocTotal" CssClass="textfield"  size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Accessories Total:</td>
                                <td><asp:TextBox runat="server" ID="tBoxAccessoriesTotal" CssClass="textfield"  size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td colspan="2"><hr /></td>
                            </tr>
                            <tr>
                                <td><h2>Expenses</h2></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">Dancer Payroll:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxDancerPayroll" CssClass="textfieldreadonly" ReadOnly="true" size="12" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Gratuity:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxGratuity" CssClass="textfield" size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Security Payroll:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxSecPayroll" CssClass="textfield" size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Referral Commission:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxReferCommission" CssClass="textfield" size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Sales Commission:</td>
                                <td><asp:TextBox runat="server" ID="tBoxSalesCommission" CssClass="textfield" size="12" MaxLength="10" onblur="CalculateJobTotals();" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td colspan="2"><hr /></td>
                            </tr>
                            <tr>
                                <td align="right">Gross Income:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxGrossIncome" CssClass="textfieldreadonly" size="12" ReadOnly="true" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Total Expenses:<br /></td>
                                <td><asp:TextBox runat="server" ID="tBoxTotalExpenses" CssClass="textfieldreadonly" size="12" ReadOnly="true" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right">Office Net:</td>
                                <td><asp:TextBox runat="server" ID="tBoxOfficeNet" CssClass="textfieldreadonly" size="12" ReadOnly="true" style="text-align: right;" /></td>
                            </tr>
                            <tr>
                                <td align="right"><asp:CheckBox runat="server" ID="chkChargedNetToCC" /></td>
                                <td>Charged Net to CC</td>
                            </tr>
                        </table>
                        <!-- END - INNER Table -->

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddTalent" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><label>Record Created: </label><asp:Label runat="server" ID="lblDateCreated" >(In Progress)</asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2"><label>Prepared By: </label><asp:Label runat="server" ID="lblCreatedBy" >[ERROR]</asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Balance Due:</td>
                    <td><asp:TextBox runat="server" ID="tBoxBalanceDue" MaxLength="10" size="12" CssClass="select" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        Job Completed:    <asp:CheckBox runat="server" ID="chkJobComplete" />&nbsp;&nbsp;&nbsp;
                        Balance Collected:<asp:CheckBox runat="server" ID="chkBalanceCollected" />&nbsp;&nbsp;&nbsp;
                        Cancelled:        <asp:CheckBox runat="server" ID="chkCancelled" />&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Responsible for Balance:</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DropDownList runat="server" ID="ddlTalentListBalanceResp" CssClass="select" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Issuing Bank Name:</td>
                    <td><asp:TextBox runat="server" ID="tBoxIssueBankName" CssClass="select" /></td>
                </tr>
                <tr>
                    <td align="right" >Card Type 1:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCardTypeCreditOrDebit" >
                            <asp:ListItem Value="" Text=" ---- " />
                            <asp:ListItem Text="Credit" />
                            <asp:ListItem Text="Debit" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="105" align="right">Card Type 2:<br /></td>
                    <td width="320">
                        <asp:DropDownList runat="server" ID="ddlCardBrand" CssClass="select" >
                            <asp:ListItem Value="" Text=" ---- " />
                            <asp:ListItem Text="Mastercard" />
                            <asp:ListItem Text="Visa" />
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2"> <div class="card-wrapper"></div>
</td>
                    
                </tr>
                <tr>
                    <td align="right">Name on Card:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCCNameOnCard" size="25" MaxLength="30" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">CC Num:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCCNum" size="25" MaxLength="19" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">Expiration:<br /></td>
                    <td><asp:TextBox runat="server" ID="tBoxCCExpire" size="8" MaxLength="7" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td align="right">CV2 Num:</td>
                    <td><asp:TextBox runat="server" ID="tBoxCCCv2Num" size="5" MaxLength="5" CssClass="textfield" /></td>
                </tr>
               
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">Special Instructions:</td><td>Highlight: <asp:CheckBox ID="chkbxJobHighlight" runat="server" /></td>
                </tr>
                 <tr>
                    <td colspan="2">Special Instructions:</td><td>Paid: <asp:CheckBox ID="chkbxJobPaid" runat="server" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="tBoxSpecialInstructions" TextMode="MultiLine" Rows="8" MaxLength="1000" CssClass="textfield" style="width:600px;" />
                    </td>
                </tr>
                
                <!-- None of these checkboxes need to be written to the DB, just have JS check to see if they were marked off. -->
                <asp:PlaceHolder runat="server" ID="pHoldPolicyChkBoxes" >
                <tr>
                    <td colspan="3">Before you submit this work order, you need to make sure you have discussed these topics with the customer:</td>
                </tr>
                <tr>
                    <td colspan="3">
                        
                        <asp:CheckBox runat="server" ID="chkCancelPolicy" Text="Cancellation Policy" />
                        <asp:CheckBox runat="server" ID="chkNeedMusic" Text="Need for Music" />
                        <asp:CheckBox runat="server" ID="chkTippingPolicy" Text="Tipping Policy" />
                        <asp:CheckBox runat="server" ID="chkDirections" Text="Directions" />
                        <asp:CheckBox runat="server" ID="chkAbusePolicy" Text="Abuse Policy" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:CheckBox runat="server" ID="chkNoPictures" Text="No Picture Taking" />
                        <asp:CheckBox runat="server" ID="chkEscortPolicy" Text="Escort service policy" />
                        <asp:CheckBox runat="server" ID="chkOver18Policy" Text="Over 18" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:CheckBox runat="server" ID="chkPersonalItems" Text="Personal Items" />
                        <asp:CheckBox runat="server" ID="chkCallOffice" Text="Call the office" />
                        <asp:CheckBox runat="server" ID="chkPopoutCake" Text="Popout Cake" />
                         <asp:CheckBox runat="server" ID="chkCashCannon" Text="Cash Cannon" />
                       
                        <asp:CheckBox runat="server" ID="chkChangeFeeNotice" Text="Read change fee notice" />
                        
                        <asp:CheckBox runat="server" ID="chkArrival" Text="Arrival" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">Check off each point as you cover them. This form will not submit until you have checked off each box.</td>
                </tr>
                </asp:PlaceHolder>

                <tr>
                    <td colspan="3" align="right">
                        <asp:Button runat="server" ID="btnAddEditJob" Text="ADD" OnClick="OnClick_btnAddEditJob" CssClass="button" style="margin-right:15px;" />
                    </td>
                </tr>
            </table>
            
            <p>&nbsp;</p>
        </div>
    </div>
   
    

    <script src="lib/js/card2.js"></script>
    <script>
        new Card({
            form: document.querySelector('form'),
            container: '.card-wrapper',
            width: 200


        });
        

       
    </script>
  

</asp:Content>

