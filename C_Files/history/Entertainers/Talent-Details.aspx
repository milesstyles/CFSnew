﻿<%@ Language="C#" MasterPageFile="~/SubPage.master" AutoEventWireup="true" CodeFile="Talent-Details.aspx.cs" Inherits="Talent_Details" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
table tr td{ vertical-align: top;}
#frameHolder #flashNav {
	background-image: url(images/headerPartyLocations.jpg);
	background-repeat: no-repeat;
}
#serviceSpotlight {
	width: 742px;
	display: block;
}
#serviceSpotlight ul li {
	color: #a7a9ac;
	font-size: 1.2em;
	background-image: url(checkOff.png);
	background-repeat: no-repeat;
	background-position: left top;
	padding-left: 19px;
	float: left;
	height: 26px;
	display: block;
	margin: 0px 5px;
	font-weight: bold;
	padding-top: 1px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
<div id="detailsHolder">
    <div id="detailsFrame">
        <div class="detailsColumn">
            <h1 runat="server" id="h1StageName" ></h1>
            <h2 runat="server" id="h2WorksInList"></h2>
            <p>
                <asp:Label runat="server" ID="lbMeasurements" style="display:block;" />
                <asp:Label runat="server" ID="lbHairColor" style="display:block;" />
                <asp:Label runat="server" ID="lbEyeColor" style="display:block;" />
                <asp:Label runat="server" ID="lbHeight" style="display:block;" />
            </p>
            <p>
                <asp:HyperLink runat="server" ID="hlBookNow" NavigateUrl="~/Entertainers/Booking.aspx" ImageUrl="~/images/bookNow.jpg" Width="165" Height="46" Text="Book Now" />
            </p>
            <asp:HyperLink runat="server" ID="hlBookClick" NavigateUrl="~/Entertainers/Booking.aspx" Text="CLICK HERE OR CALL 1-877-4-A-STRIP" /><br /><br />
            <!-- AddThis Button BEGIN -->
			<script type="text/javascript">
                    var addthis_pub="centerfoldstrips";
                    var addthis_brand = "Centerfold Strips";
                    var addthis_header_color = "#ffffff";
                    var addthis_header_background = "#000000";
                    var addthis_options = 'email, facebook, favorites, myspace, aim, ask, delicious, digg, fark, friendfeed, google, linkedin, live, magnolia, newsvine, print, reddit, slashdot, stumbleupon, technorati, twitter, more';
               </script>
               <a href="http://www.addthis.com/bookmark.php" 
                  style="text-decoration:none;" 
                  onmouseover="return addthis_open(this, '', '[URL]', '[TITLE]');" 
                  onmouseout="addthis_close();" 
                  onclick="return addthis_sendto();"><img runat="server" src="~/images/bookmark.gif" width="124" height="18" alt="Bookmark or Share" /></a>
               <script type="text/javascript" src="http://s7.addthis.com/js/200/addthis_widget.js"></script>
     	<!-- AddThis Button END -->
        </div>
            
        <div class="detailsColumn">
            <asp:Image runat="server" ID="img1" Width="178" Height="264" AlternateText='Pic 1' Visible="false" />
        </div>
        <div class="detailsColumn">
            <asp:Image runat="server" ID="img2" Width="178" Height="264" AlternateText='Pic 2' Visible="false" />
        </div>
        <div class="detailsColumn noPaddingMargin">
            <asp:Image runat="server" ID="img3" Width="178" Height="264" AlternateText='Pic 3' Visible="false" />
        </div>
        <div class="cleaner">&nbsp;</div>
        <hr runat="server"/>
        <asp:PlaceHolder runat="server" ID="phServices" Visible="false">
            <div id="serviceSpotlight">
                <asp:BulletedList ID="blServices" runat="server" />
            </div>
            <div class="cleaner">&nbsp;</div>
        </asp:PlaceHolder>
        <div class="cleaner">&nbsp;</div>
        <hr runat="server" id="hrAttributes" />
        <asp:PlaceHolder runat="server" ID="phLikes" Visible="false" >
            <div class="detailsColumn" style="font-size:1em;">
                <h3><asp:Image runat="server" ID="imgLikes" ImageUrl="~/images/h2Likes.gif" AlternateText="" /><span>Likes</span></h3>
                <p runat="server" id="pLikes" >
                    
                </p>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phDislikes" Visible="false" >
            <div class="detailsColumn" style="font-size:1em;">
                <h3><asp:Image runat="server" ID="imgDislikes" ImageUrl="~/images/h2Dislikes.gif" AlternateText="" /><span>Dislikes</span></h3>
                <p runat="server" id="pDislikes" >
                </p>
            </div>
        </asp:PlaceHolder>                        
        <asp:PlaceHolder runat="server" ID="phTalents" Visible="false" >
            <div class="detailsColumn" style="font-size:1em;">
                <h3><asp:Image runat="server" ID="imgSpecial" ImageUrl="~/images/h2SpecialTalents.gif" AlternateText="" /><span>Special Talents</span></h3>
                <p runat="server" id="pTalents" >
                </p>
            </div>
        </asp:PlaceHolder>                        
        <asp:PlaceHolder runat="server" ID="phCostumes" Visible="false" >
            <div class="detailsColumn noPaddingMargin" style="font-size:1em;">
                <h3><asp:Image runat="server" ID="imgCostumes" ImageUrl="~/images/h2AvailableCostumes.gif" AlternateText="" /><span>Available Costumes</span></h3>
                <p runat="server" id="pCostumes" >
                </p>
            </div>
            <div class="cleaner">&nbsp;</div>
        </asp:PlaceHolder>    
        <hr runat="server" id="hrFeatureContent" visible="false" />
        <asp:DataList runat="server" ID="dlCredits" RepeatColumns="4" RepeatDirection="Horizontal" >
            <ItemTemplate>
                <div class="detailsColumn" style="font-size:1em;">
                    <h4><%#Eval("Name") %></h4>
                    <p>
                        <%#Eval("Info") %>
                    </p>
                </div>
            </ItemTemplate>
        </asp:DataList>
        <div class="cleaner">&nbsp;</div>
        
        <asp:Panel ID="pnlVids" visible="false" runat="server">
            <hr />
            <script type="text/javascript" src="<%= ResolveUrl("~/js/swfobject.js") %>"></script>
        	<script type="text/javascript">
			var flashvars = new Object();    
			flashvars.xmlSrc = "<%= ResolveUrl("~/dat/videos.aspx") %>?id=<%= m_talentId %>";
			flashvars.autoPlay = "true";
			var params = new Object();
			params.allowFullScreen = "false";
			params.AllowScriptAccess = "always";
			swfobject.embedSWF("<%= ResolveUrl("~/QuickPlayer.swf") %>", "player", "236", "188", "9.0.0", "expressInstall.swf", flashvars, params);
		</script>

		<div id="player">
			<h1>INSTALL FLASH 9!</h1>
		</div>
        </asp:Panel>
        
        <a onclick="javascript:window.history.go(-1);" style="float:right; cursor: pointer;" >
            <img runat="server" id="imgBack" src="~/images/backButton.gif" width="63" height="19" alt="Back" />
        </a>
            
        <div class="cleaner">&nbsp;</div>
    </div>
</div>
</asp:Content>

