<%@ Page Title="Centerfold Strips | Admin" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css" >
    clockStyle {
	background-color:#000;
	border:#999 2px inset;
	padding:6px;
	color:#0FF;
	font-family:"Arial Black", Gadget, sans-serif;
        font-size:16px;
        font-weight:bold;
	letter-spacing: 2px;
	display:inline;
}
a:link {
    color: #A7A9Ac;
}
a:visited {
    color: #A7A9Ac;
}
    </style>
       <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
  <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
  <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
 
  <script type="text/javascript">
      function update() {
          if ($("#datepicker").val() != "") {
              $("#datepicker").datepicker();
              //alert($("#datepicker").val());
          }
      }
      $(function () {
         // $('#datepicker').select(function () { /* do something */ });
          $("#datepicker").datepicker();
      });


  </script>
    <script type="text/javascript">
        var h = "";
        var newh = "";
        function dateChange() {
            if ($("#datepicker").val() != "") {
                //$("#dRange").val("0");
                if (h != $("#datepicker").val()) {
                    h = $("#datepicker").val();

                    var url = "view_work_orders.aspx?search=true" + "&dRange=" + h;
                    window.open(url, "_blank", "", "");
                }
            }
        }

        function rangeChange() {
            if ($("#dRange").val() != "0") {
                $("#datepicker").val("");
                $("#datepicker2").val("");
            }
        }
        function paytrace() {
            var w = window.outerWidth;
            var h = window.outerHeight;
            //window.open('https://paytrace.com/login.pay','mywin','height='+h+',width='+w+',fullscreen=true','scrollbars=true');
            window.open('https://paytrace.com/login.pay', 'mywin');
        
        }
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <div id="leftColumn">
        <div class="contentBlock" >
            <h2>Employee Quick Reference</h2>
            <table>
            <tr><td><asp:label ID="Label3" runat="server" CssClass="button" Text=" Click Number for Excel of Talent"></asp:label></td></tr>
            </table>
            <table width="218" cellspacing="0">
            
                <tr>
                    <td><asp:HyperLink NavigateUrl="employee_excel.aspx?type=female" runat="server"><%= ddlFemaleDancer.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlFemaleDancer" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Female Dancer -- " />
                            <asp:ListItem Value="Email_female" Text=" -- Email List -- " />
                        </asp:DropDownList>
                    </td>
                    <td><asp:Button runat="server" ID="btnFemaleDancerGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                     
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink1" NavigateUrl="employee_excel.aspx?type=male" runat="server"><%= ddlMaleDancer.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMaleDancer"  AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Male Dancer -- " />
                            <asp:ListItem Value="Email_male" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnMaleDancerGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                 
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink2" NavigateUrl="employee_excel.aspx?type=minifemale" runat="server"><%= ddlFemaleMini.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlFemaleMini" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Female Little People (Mini) -- " />
                             <asp:ListItem Value="Email_minifemale" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnFemaleMiniGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                
               
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink3" NavigateUrl="employee_excel.aspx?type=minimale" runat="server"><%= ddlMaleMini.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMaleMini" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Male Little People (Mini) -- " />
                             <asp:ListItem Value="Email_minimale" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnMaleMiniGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                   
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink4" NavigateUrl="employee_excel.aspx?type=bellydancer" runat="server"><%= ddlBellyDancer.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlBellyDancer" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- BellyDancers -- " />
                               <asp:ListItem Value="Email_bellydancer" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="bntBellyDanceGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                    
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink5" NavigateUrl="employee_excel.aspx?type=bbw" runat="server"><%= ddlBbw.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlBbw" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- BBW -- " />
                             <asp:ListItem Value="Email_bbw" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnBbwGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                   
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink6" NavigateUrl="employee_excel.aspx?type=dragqueen" runat="server"><%= ddlDragQueen.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDragQueen" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Drag Queens -- " />
                             <asp:ListItem Value="Email_dragqueen" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnDragQueenGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                     
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink7" NavigateUrl="employee_excel.aspx?type=impersonator" runat="server"><%= ddlImpersonator.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlImpersonator" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Impersonators -- " />
                             <asp:ListItem Value="Email_impersonator" Text=" -- Email List -- " />
                            
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnImpersonatorGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                     
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink8" NavigateUrl="employee_excel.aspx?type=novelty" runat="server"><%= ddlNoveltyActs.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlNoveltyActs" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Novelty Acts -- " />
                             <asp:ListItem Value="Email_novelty" Text=" -- Email List -- " />
                            
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnNoveltyActsGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                    
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink9" NavigateUrl="employee_excel.aspx?type=duo" runat="server"><%= ddlDuoShows.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDuoShows" AppendDataBoundItems="true" CssClass="select" style="width: 163px;" >
                            <asp:ListItem Value="" Text=" -- Duo Shows -- " />
                                <asp:ListItem Value="Email_duo" Text=" -- Email List -- " />
                         
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnDuoShowsGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                    
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink10" NavigateUrl="employee_excel.aspx?type=driver" runat="server"><%= ddlDriversBouncers.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDriversBouncers" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Drivers &amp; Bouncers -- " />
                                    <asp:ListItem Value="Email_driver" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                  
                    <td><asp:Button runat="server" ID="btnDriversBouncersGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                    
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HyperLink11" NavigateUrl="employee_excel.aspx?type=affiliate" runat="server"><%= ddlAffiliate.Items.Count - 1 %></asp:HyperLink></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAffiliate" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Affiliate Companies -- " />
                            <asp:ListItem Value="Email_affiliate" Text=" -- Email List -- " />
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnAffiliateGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                   
                </tr>
                 <tr>
                    <td><%= ddlBirthdays.Items.Count - 1%></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlBirthdays" AppendDataBoundItems="true" CssClass="select" style="width: 160px;" >
                            <asp:ListItem Value="" Text=" -- Birthdays Today -- " />
                           
                       
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Button runat="server" ID="btnBirthdayGo" Text="GO" OnClick="OnClick_EmpQuickRefGoBtn" CssClass="button" /></td>
                </tr>
            </table>

            <hr />
            
            <table width="218" cellspacing="0">
                <tr>
                    <td width="69"><label>Find:</label></td>
                    <td colspan="2">
                        <asp:DropDownList runat="server" ID="ddlSearchTalType" CssClass="select" />
                    </td>
                </tr>
                <%-- 
                <tr>
                    <td><label>Lives In:</label></td>
                    <td width="106">
                    --%>
                        <asp:DropDownList runat="server" ID="ddlSearchLivesIn" CssClass="select" Visible="false" />
                    <%--
                    </td>
                    <td width="45">&nbsp;</td>
                    
                </tr>
                --%>
                <tr>
                    <td><label>Works In:</label></td>
                    <td width="106">
                        <asp:DropDownList runat="server" ID="ddlSearchWorksIn" CssClass="select" />
                    </td>
                    <td width="45"><asp:Button runat="server" ID="btnTalentSearchGo" Text="GO" OnClick="OnClick_btnTalentSearchGo" CssClass="button" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center;">
                    <asp:Button runat="server" Height="40px" ID="btnGetInactiveEmployees" Text="Inactive Employees" OnClick="OnClick_btnGetInactiveEmployees" CssClass="buttonA"  />
                    <asp:Button runat="server"  ID="btn_createcsv" 
                            Text="Create Employee Contacts CSV" CssClass="button"
                            onclick="btn_createcsv_Click"  />
                   
                    </td>
                </tr>
            </table>
           
        </div>
        
        
        <div class="contentBlock">
            <h2>Manage Promotions</h2>
            
            <table width="218" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td><asp:Image runat="server" ID="imgPromo" ImageUrl="~/images/specialempty.jpg" AlternateText="Promo Image Not Found!" /></td>
                </tr>
                <tr>
                    <td>Image Location: <br />(please use 176 X 176 pixels)<br />
                        <asp:FileUpload runat="server" ID="fuPromoImg" CssClass="textfield" />
                    </td>
                </tr>
                <tr>
                    <td>Promo URL:<br />
                        <asp:TextBox runat="server" ID="tBoxPromoUrl" size="38" MaxLength="100" CssClass="textfield" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnUpdatePromo" OnClick="OnClick_btnUpdatePromo" Text="Update" CssClass="button" />
                        <asp:Label runat="server" ID="lblPromoFeedback" />
                        <asp:Button runat="server" ID="btnDeletePromo" 
                            OnClick="OnClick_btnDeletePromo" Text="Delete" CssClass="button" />
                    </td>
                </tr>
            </table>            
        </div>
 <div class="contentBlock">
            <h2>Manage Logo</h2>
            
            <table width="218" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td><asp:Image runat="server" ID="imgLogoBottom" ImageUrl="~/images/specialempty.jpg" AlternateText="Logo Image Not Found!" /></td>
                </tr>
                <tr>
                    <td>Image Location:<br />(please use 80 X 80 pixels)<br />
                        <asp:FileUpload runat="server" ID="fuLogoImg" CssClass="textfield" />
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <asp:Button runat="server" ID="Button3" OnClick="OnClick_btnUpdateLogo" Text="Update" CssClass="button" />
                        <asp:Label runat="server" ID="lblLogoFeedback" />
                        <asp:Button runat="server" ID="btnDeleteLogo" 
                            OnClick="OnClick_btnDeleteLogo" Text="Delete" CssClass="button" />
                    </td>
                </tr>
            </table>            
        </div>
        <div class="contentBlock">
            <h2>Manage Logo Name</h2>
            
            <table width="218" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td> <asp:TextBox ID="txt_Logo" runat="server"></asp:TextBox> </td>
                </tr>
                
                
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btn_UpdateLogoText" OnClick="OnClick_btnUpdateLogoText" Text="Update" CssClass="button" />
                        <asp:Label runat="server" ID="lbl_LogoText" />
                    </td>
                </tr>
            </table>            
        </div>
        <div class="contentBlock">
        <!--<a href="#" class="buttonLarge">Franchise Management</a>
            <a href="#" class="buttonLarge">Confirmation Email Admin</a>-->       
            <a href="../logs/current_month/index.html" class="buttonLarge">Current Month Log Files</a>
            <a href="../logs/last_month/index.html" class="buttonLarge">Log Files YTD</a> 
            <br /><br />
        </div>
        <br />
        <br /><br /><br />
    </div>

    <div id="rightColumn">
        <div class="contentBlock" style="height:450px; overflow:auto;">
            <h2>Work Order Quick View</h2>
            <asp:GridView runat="server" ID="grdViewWorkOrders" AutoGenerateColumns="false" GridLines="None"  >
                <AlternatingRowStyle BackColor="#383838" />
                <Columns>
                <asp:TemplateField ItemStyle-Width="25" >
                <ItemTemplate>
                    <asp:Image runat="server" ID="imgHasPaid" ImageUrl="~/images/dollar_sign.jpg" AlternateText="&nbsp;" Visible="false" />
                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
            </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="25" >
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgHasTalent" ImageUrl="~/images/talent.png" AlternateText="T" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Event Date" DataField="EventDate" DataFormatString="{0:dddd MM/dd/yyyy}" ItemStyle-Width="120" />
                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="110" >
                        <ItemTemplate>
                            <%# Eval("FirstName") + " " + Eval("LastName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location" Visible="false" ItemStyle-Width="165" >
                        <ItemTemplate>
                            <%# Eval("LocationName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Location" DataField="LocationName" ItemStyle-Width="165" />
                    <asp:BoundField HeaderText="Start Time" DataField="StartDateTime" DataFormatString="{0:h:mm tt}" ItemStyle-Width="70" />
                    <asp:TemplateField HeaderText="Show Length" ItemStyle-Width="70" >
                        <ItemTemplate>
                            <%# CfsCommon.FormatShowLengthHumanReadable( (int)Eval("TotalShowLengthMins") ) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnViewWorkOrder" Text="VIEW" CssClass="button" OnClick="OnClick_btnViewWorkOrder" CommandArgument='<%# Eval("JobId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
        <div class="contentBlock" style="float:left; width:380px; margin-right:15px;">
            <h2>Find A Work Order</h2>
            <table cellspacing="0">
                <tr>
                      <td align="right">Work Order #</td>
                      <td colspan="2"><asp:TextBox runat="server" ID="tBoxSearchWorkOrderNum" CssClass="textfield" /></td>
               
        <td >
           <font style="color:Silver;">Work Order by Day: </font><input type="text" style="width:120px;" id="datepicker" onblur="dateChange();" onclick="update()"; onchange="dateChange();" />&nbsp;
          
        </td>
    
              
                </tr>
                <tr>
                    <td align="right">First Name</td>
                    <td><asp:TextBox runat="server" ID="tBoxSearchFirstName" CssClass="textfield" ></asp:TextBox></td>
                    <td align="right">Last Name</td>
                    <td><asp:TextBox runat="server" ID="tBoxSearchLastName"  CssClass="textfield" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">Job Date</td>
                    <td><asp:TextBox runat="server" ID="tBoxSearchJobDate" CssClass="textfield" ></asp:TextBox></td>
                    <td align="right">Location</td>
                    <td><asp:TextBox runat="server" ID="tBoxSearchLocation" CssClass="textfield" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">City</td>
                    <td><asp:TextBox runat="server" ID="tBoxSearchCity" CssClass="textfield" ></asp:TextBox></td>
                    <td align="right">State</td>
                    <td><asp:DropDownList runat="server" ID="ddlSearchState" CssClass="select" Width="130px" /></td>
                </tr>
                <tr>
                    <td align="right">CC Num</td>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxSearchCcNum" CssClass="textfield" ></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">Performer @ job</td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlSearchFullPerformerList" CssClass="select" style="width:130px;" >
                            <asp:ListItem Value="" Text="ALL" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Referred By</td>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxSearchReferredBy" CssClass="textfield" ></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                    <td align="right">Event Type</td>
                    <td colspan="2"><asp:TextBox runat="server" ID="tBoxSearchEventType" CssClass="textfield" ></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="middle"><asp:Button runat="server" ID="btnSearchWorkOrder" OnClick="OnClick_btnSearchWorkOrder" Text="SEARCH" CssClass="button" /></td>
                </tr>
            </table>
             <h2>Current Home Page Videos</h2>
             <asp:PlaceHolder id="Place" runat="server"/>
        <asp:label id="Message" runat="server"/><br /><asp:Button ID="DeleteVideo" 
            runat="server" Text="Delete Video" onclick="DeleteVideo_Click" />
    
            <h2>Add Videos to HomePage</h2>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server"
                Text="Upload" onclick="Button1_Click" /><br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <h2>Change Main Page Video Thumbnail</h2>
            <asp:FileUpload ID="FileUpload2" runat="server" />
             
            <asp:Button ID="Button2" runat="server"
                Text="Upload" onclick="Button2_Click" /><br />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            <br /><br />
          <!--- <div style="padding-bottom:0px; padding-top:255px;"> <div ID="a_paytrace" runat="server" onclick="paytrace();">
                <asp:Image ID="img_paytrace" ImageUrl="~/images/paytrace.jpg" runat="server"/>       </div>
            </div>-->
            <br />
            
        </div>
        <div class="contentBlock" style="float:left; width:244px;">
            <h2>Staff Notes</h2>
            <asp:TextBox id="tbStaffNotes" TextMode="MultiLine" CssClass="textfield staffNotes" runat="server" />
            <asp:Button runat="server" ID="btnStaffNotes" Text="SAVE" CssClass="button" />
        </div>

        <div style="float:left;width:266px;">
                 
             
    </div>        
        </div>

       
    
    </div>

       
    
</div></asp:Content>