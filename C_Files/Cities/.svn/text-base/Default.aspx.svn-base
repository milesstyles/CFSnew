<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Entertainers_CitySpecific" %>

<%@ Register src="../Controls/MailingList.ascx" tagname="MailingList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link media="all" type="text/css" rel="Stylesheet" id="citySpecific" runat="server"/>
    <title>
        <asp:MultiView ID="PageTitleMultiView" runat="server">
            <asp:View ID="PageTitleView1" runat="server">
                Centerfold Strips | Strippers in <%=this.displayCity%>
            </asp:View>
            <asp:View ID="PageTitleView2" runat="server">
                <%=this.PageTitle%>
            </asp:View>
        </asp:MultiView>
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headFemale" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headMale" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="headBBW" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="headDuo" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="headFemaleLittle" Runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="headMaleLittle" Runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="headImpersonator" Runat="Server">
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="headBellyDancer" Runat="Server">
</asp:Content>
<asp:Content ID="Content10" ContentPlaceHolderID="headNovelty" Runat="Server">
</asp:Content>
<asp:Content ID="Content11" ContentPlaceHolderID="headDragQueen" Runat="Server">
</asp:Content>

<asp:Content ID="Content12" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
            <Scripts>
                <%--<asp:ScriptReference Path="~/js/jquery-1.3.2.min.js" />--%>
            </Scripts>
        </asp:ScriptManagerProxy>
        
        <h1><%= displayCity%> Strippers</h1>
        
        <ul id="subNav">
          <li><asp:HyperLink runat="server" ID="showFemalesButton"  Text="Female Strippers" NavigateUrl="#females"></asp:HyperLink></li>
          <li><asp:HyperLink runat="server" ID="showMalesButton"  Text="Male Strippers"  NavigateUrl="#males"></asp:HyperLink></li>
          <li><asp:HyperLink runat="server" ID="showFemaleLittlesButton"  Text="Female Little People" NavigateUrl="#minifemale"></asp:HyperLink></li>
          <li><asp:HyperLink runat="server" ID="showMaleLittlesButton"  Text="Male Little People" NavigateUrl="#minimale"></asp:HyperLink></li>
        </ul>
        <div id="bookNow"><a href="<%= Context.Request.ApplicationPath%>Entertainers/Booking.aspx"><asp:Image CssClass="book-your-party" ImageUrl="~/images/bookYourParty.jpg" width="163" height="45" AlternateText="Book Your Party" runat="server" /></a><br />
	        Click here or call 1-877-4-A-Strip</div>
        <div id="topContent">
            <asp:MultiView ID="IntroMultiView" runat="server">
                <asp:View ID="IntroView1" runat="server">
                    We are the largest upscale exotic dancer service in the USA, specializing in having the sexiest <%=this.displayCity%> Strippers since 1996. No other adult entertainment 
                    agency can compete with the beauty of our stunning <%=this.displayCity%> Strippers or the professionalism of our booking agents. Centerfold Strips is proud to have 
                    provided the hottest <%=this.displayCity%> Strippers to rock stars, rappers, professional athletes and heads of state. But you don't have to be famous to be a VIP at 
                    Centerfold Strips; it only takes one call for one of our expert service agents to connect you with gorgeous Strippers in <%=this.displayCity%>.
                </asp:View>
                <asp:View ID="IntroView2" runat="server">
                    <%= this.IntroText.Replace("[siteroot]", Context.Request.ApplicationPath)%>
                </asp:View>
            </asp:MultiView>
        </div>
        <div class="cleaner">&nbsp;</div>
       
        <!--this is shown on the left-->
<%--        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="showFemalesButton" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="showMalesButton" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="showFemaleLittlesButton" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="showMaleLittlesButton" EventName="click" />
            </Triggers>
            <ContentTemplate>
                <div class="dancerColumn">
                    <h4><%= displayCategory %> Strippers</h4>
                    <div id="catWrapper">
                        <asp:HiddenField runat="server" ID="shownCategory" Value="female" />
                    </div>
                    <asp:Repeater runat="server" id="talentRepeater" 
                        onitemcommand="talentRepeater_ItemCommand" 
                        onitemdatabound="talentRepeater_ItemDataBound" 
                        ondatabinding="talentRepeater_DataBinding" 
                        onitemcreated="talentRepeater_ItemCreated">
                        <ItemTemplate>
                             <div class="dancerThumb <%# thumbCssClass %>"> 
                                 <div class="thumbWrapper">
                                    <a href="Entertainers/Talent-Details.aspx?id=<%# Eval( "TalentId" ) %>"><img src="<%# imageUrl %>" alt="<%# Eval( "StageName" ) %>" /></a>
                                 </div>
                                 <!--[if lte IE 6]><br /><![endif]-->
                                 <br /><span><%# Eval( "StageName" ) %></span>
                                 <br /><span><%# Eval( "State" ) %></span>
                             </div>       
                        </ItemTemplate>     
                    </asp:Repeater>
                    <asp:panel runat="server" ID="noResults"><%= this.NoResultsText %></asp:panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        
        <div class="dancerColumn">
            <asp:Repeater runat="server" id="femaleRepeater" onitemcreated="talentRepeater_ItemCreated">
                <HeaderTemplate>
                    <div class="clear"></div>
                    <div class="talent-category">
                    <a name="females"></a>
                    <h4>Female Strippers</h4>
                </HeaderTemplate>
                <ItemTemplate>
                     <div class="dancerThumb <%# thumbCssClass %>"> 
                         <div class="thumbWrapper">
                            <a href="Entertainers/Talent-Details.aspx?id=<%# Eval( "TalentId" ) %>"><img src="<%# imageUrl %>" alt="<%# Eval( "StageName" ) %>" /></a>
                         </div>
                         <!--[if lte IE 6]><br /><![endif]-->
                         <span><%# Eval("StageName")%></span>
                         <br /><span><%# Eval("State")%></span>
                     </div>       
                </ItemTemplate>     
                <FooterTemplate><div class="clear"></div></div></FooterTemplate>
            </asp:Repeater>
            
            <asp:Repeater runat="server" id="maleRepeater" onitemcreated="talentRepeater_ItemCreated">
                <HeaderTemplate>
                    <div class="clear"></div>
                    <div class="talent-category">
                    <a name="males"></a>
                    <h4>Male Strippers</h4>
                </HeaderTemplate>
                <ItemTemplate>
                     <div class="dancerThumb <%# thumbCssClass %>"> 
                         <div class="thumbWrapper">
                            <a href="Entertainers/Talent-Details.aspx?id=<%# Eval( "TalentId" ) %>"><img src="<%# imageUrl %>" alt="<%# Eval( "StageName" ) %>" /></a>
                         </div>
                         <!--[if lte IE 6]><br /><![endif]-->
                         <span><%# Eval("StageName")%></span>
                         <br /><span><%# Eval("State")%></span>
                     </div>       
                </ItemTemplate>   
                <FooterTemplate><div class="clear"></div></div></FooterTemplate>  
            </asp:Repeater>
            
            <asp:Repeater runat="server" id="miniFemaleRepeater" onitemcreated="talentRepeater_ItemCreated">
                <HeaderTemplate>
                    <div class="clear"></div>
                    <div class="talent-category">
                    <a name="minifemale"></a>
                    <h4>Female Little People</h4>
                </HeaderTemplate>
                <ItemTemplate>
                     <div class="dancerThumb <%# thumbCssClass %>"> 
                         <div class="thumbWrapper">
                            <a href="Entertainers/Talent-Details.aspx?id=<%# Eval( "TalentId" ) %>"><img src="<%# imageUrl %>" alt="<%# Eval( "StageName" ) %>" /></a>
                         </div>
                         <!--[if lte IE 6]><br /><![endif]-->
                         <span><%# Eval("StageName")%></span>
                         <br /><span><%# Eval("State")%></span>
                     </div>       
                </ItemTemplate>   
                <FooterTemplate><div class="clear"></div></div></FooterTemplate>  
            </asp:Repeater>
            
            <asp:Repeater runat="server" id="miniMaleRepeater" onitemcreated="talentRepeater_ItemCreated">
                <HeaderTemplate>
                    <div class="clear"></div>
                    <div class="talent-category">
                    <a name="minimale"></a>
                    <h4>Male Little People</h4>
                </HeaderTemplate>
                <ItemTemplate>
                     <div class="dancerThumb <%# thumbCssClass %>"> 
                         <div class="thumbWrapper">
                            <a href="Entertainers/Talent-Details.aspx?id=<%# Eval( "TalentId" ) %>"><img src="<%# imageUrl %>" alt="<%# Eval( "StageName" ) %>" /></a>
                         </div>
                         <!--[if lte IE 6]><br /><![endif]-->
                         <span><%# Eval("StageName")%></span>
                         <br /><span><%# Eval("State")%></span>
                     </div>       
                </ItemTemplate> 
                <FooterTemplate><div class="clear"></div></div></FooterTemplate>    
            </asp:Repeater>
        </div>
        
        
        
        <!--this is shown on the right-->
        <div id="extraCopy">
            <asp:MultiView ID="RightColumnMultiView" runat="server">
                <asp:View ID="RightColumnView1" runat="server">
                    <p>Centerfold Strips will bring the best Strippers in <%=this.displayCity%> to your bachelor or <a href="/Entertainers/Talent.aspx?cat=female">bachelorette parties</a>, office parties or birthdays; we  can come to you or bring you and your party out in our <a href="/Exclusives/Limos-and-Party-Buses.aspx">exotic limousine or party bus</a>! </p>
                    <p>Our <%=this.displayCity%> strippers are perfect for  whatever occasion your looking to make sexier – from erotic <a href="/Services/Party-Accessories.aspx">Pop Out Cakes</a> to naughty <a href="/Entertainers/Talent.aspx?cat=bbw">Big Booty Strippers</a>, Centerfold Strips’ friendly staff will help you plan the perfect adult entertainment party  from start to finish.</p>
                </asp:View>
                <asp:View ID="RightColumnView2" runat="server">
                    <%=this.RightColumnText.Replace("[siteroot]", Context.Request.ApplicationPath)%>
                </asp:View>
            </asp:MultiView>
          <uc1:MailingList ID="MailingList1" runat="server" />
        </div>
        <div style='clear:both;'></div>
</asp:Content>

<asp:Content ID="Content13" ContentPlaceHolderID="mainContentHolder" Runat="Server">

</asp:Content>

