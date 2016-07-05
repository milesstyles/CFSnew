<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="cc1" %>
<%@ Register Src="Controls/LeftColumn.ascx" TagName="LeftColumn" TagPrefix="cc1" %>
<%@ Register Src="Controls/MainMenu.ascx" TagName="MainMenu" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
 <meta http-equiv="x-ua-compatible" content="IE=8">     

 

<script type="text/javascript">var _sf_startpt=(new Date()).getTime()</script>
    <link rel="SHORTCUT ICON" HREF="http://www.centerfoldstrips.com/favicon.ico"> 
    <title>Strippers | Adult entertainment booking agency - Centerfold Strips</title>
    <link href="css/mainStyle.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/homeStyle.css" rel="stylesheet" type="text/css" media="all" />
    <meta name="keywords" content="strippers, adult entertainment booking agency" />
    <meta name="description" content="The ultimate upscale exotic entertainment experience, featuring the hottest female and male dancers." />
    <meta name="verify-v1" content="sYS2JmqQWD8qlH4VUaxPRQb6YPAL2RrTHP2z6f9ZG7A=" />
    <meta http-equiv="imagetoolbar" content="no" />
    <meta http-equiv="pics-label" content='(pics-1.1 "http://www.icra.org/ratingsv02.html" l gen true for "http://www.centerfoldstrips.com" r (nd 1 ne 1 nh 1 ni 1 vz 1 lz 1 oz 1 cz 1))' />
    <meta name="robots" content="index, follow, all" />
    <meta name="googlebot" content="index, follow" />
    <meta name="revisit-after" content="1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=500, initial-scale=1, maximum-scale=1"/>
    <meta name="google-site-verification" content="R2KPWyn9PaiDkN69NvHwhvlGkqlRk5AZtA7vDHUEQbQ" />
    <link rel="stylesheet" type="text/css" href="history/history.css" />
    <!--[if lte IE 6]>
	    <link href="css/is6Style.css" rel="stylesheet" type="text/css" media="all" />
    <![endif]-->
    <script src="js/AC_OETags.js" type="text/javascript" language="javascript"></script>
    <!--  BEGIN Browser History required section -->
    <script src="history/history.js" type="text/javascript" language="javascript"></script>
    <!--  END Browser History required section -->
    <script type="text/javascript" language="javascript" >// <![CDATA[
    startList = function() {
	    if (document.all && document.getElementById) {
		    navRoot = document.getElementById("mainMenu");
		    for (i=0; i<navRoot.childNodes.length; i++) {
			    node = navRoot.childNodes[i];
			    if (node.nodeName=="LI") {
				    node.onmouseover=function() {
					    this.className+=" over";
				    }
				    node.onmouseout=function() {
					    this.className=this.className.replace(" over", "");
				    }
			    }
		    }
	    }
    }
    window.onload=startList;

    // -----------------------------------------------------------------------------
    // Globals
    // Major version of Flash required
    var requiredMajorVersion = 9;
    // Minor version of Flash required
    var requiredMinorVersion = 0;
    // Minor version of Flash required
    var requiredRevision = 28;
    // -----------------------------------------------------------------------------
    // 
    // ]]>
    </script>    
    
    <script type="text/javascript" src="<%= ResolveUrl("~/js/rightclick.js") %>"></script>

	<!-- google pluse script -->
    <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
          
            <asp:ScriptReference Path="~/js/ie7.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="container">
        <div id="content">
            <div id="header">
                <asp:HyperLink runat="server" CssClass="findTalent" ID="findTalent" NavigateUrl="~/Entertainers/Booking.aspx" ImageUrl="~/images/bookNow.gif" Text="Book Your Party Now" width="156" height="54"  />
                <a href="tel:+18774278747"><asp:Image runat="server" CssClass="cCards" ImageUrl="~/images/phoneNumber.gif" AlternateText="1-877-4-A-Strip" width="153" height="52" ID="cCards" /></a>
                <asp:Image runat="server" CssClass="logo" ImageUrl="~/images/logoIndex.gif" AlternateText="Centerfold Strips" width="336" height="76" ID="logo" />
            </div>
            
            
            <div id="frameHolder">
                <h1><span>We Bring The Stripclub To You</span></h1>
                    <div id="flashNav">
    
               <script language="JavaScript" type="text/javascript">
			    <!--
                   // Version check for the Flash Player that has the ability to start Player Product Install (6.0r65)
                   var hasProductInstall = DetectFlashVer(6, 0, 65);

                   // Version check based upon the values defined in globals
                   var hasRequestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);

                   if (hasProductInstall && !hasRequestedVersion) {
                       // DO NOT MODIFY THE FOLLOWING FOUR LINES
                       // Location visited after installation is complete if installation is required
                       var MMPlayerType = (isIE == true) ? "ActiveX" : "PlugIn";
                       var MMredirectURL = window.location;
                       document.title = document.title.slice(0, 47) + " - Flash Player Installation";
                       var MMdoctitle = document.title;

                       AC_FL_RunContent(
					"src", "playerProductInstall",
					"FlashVars", "MMredirectURL=" + MMredirectURL + '&MMplayerType=' + MMPlayerType + '&MMdoctitle=' + MMdoctitle + "",
					"width", "100%",
					"height", "100%",
					"align", "middle",
					"id", "CenterfoldCarousel",
					"quality", "high",
					"bgcolor", "#000000",
					"wmode", "transparent",
					"name", "CenterfoldCarousel",
					"allowScriptAccess", "sameDomain",
					"type", "application/x-shockwave-flash",
					"pluginspage", "http://www.adobe.com/go/getflashplayer"
				);
                   } else if (hasRequestedVersion) {
                       // if we've detected an acceptable version
                       // embed the Flash Content SWF when all tests are passed
                       AC_FL_RunContent(
						"src", "CenterfoldCarousel",
						"width", "100%",
						"height", "100%",
						"align", "middle",
						"id", "CenterfoldCarousel",
						"quality", "high",
						"bgcolor", "#000000",
						"wmode", "transparent",
						"name", "CenterfoldCarousel",
						"allowScriptAccess", "sameDomain",
						"type", "application/x-shockwave-flash",
						"pluginspage", "http://www.adobe.com/go/getflashplayer"
				);
                   } else {  // flash is too old or we can't detect the plugin
                       var alternateContent = 'Alternate HTML content should be placed here. '
				+ 'This content requires the Adobe Flash Player. '
				+ '<a href=http://www.adobe.com/go/getflash/>Get Flash</a>';
                       document.write(alternateContent);  // insert non-flash content
                   }
			// -->
			</script>
			<noscript>
			<object data="CenterfoldCarousel.swf" type="application/x-shockwave-flash"
						id="CenterfoldCarousel" width="100%" height="100%">
			     <param name="movie" value="CenterfoldCarousel.swf" />
			     <param name="quality" value="high" />
			     <param name="bgcolor" value="#000000" />
			     <param name="allowScriptAccess" value="sameDomain" />
                    <param name="wmode" value="transparent" />
			     <!--
			     <embed src="CenterfoldCarousel.swf" quality="high" bgcolor="#000000"
							width="100%" height="100%" name="CenterfoldCarousel" align="middle"
							play="true"
							loop="false"
							allowscriptaccess="sameDomain"
							type="application/x-shockwave-flash"
							pluginspage="http://www.adobe.com/go/getflashplayer"> </embed>
				 -->
            </object>
			</noscript>
               </div>


  
            </div>
            
            <cc1:MainMenu runat="server" ID="mainMenu1" />
            
  <cc1:LeftColumn runat="server" ID="leftColumn1" />
            
            <div id="middleColumn">
                <div id="partyAccessories2" onclick="location.href='Services/Party-Accessories.aspx';" style="cursor: pointer;">
                    <div id="pop_out_header" style="color:White;"><div style="font-size:18px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pop Out Cakes for Sale</div><br /><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;click here</div></div>
                    <img id="popoutcake" alt="popoutcake" src="images/partyAccessoriesBGnew.jpg" />
                      </div>
             
                <div id="middleRightColumn">
                    <!--<h3>Call us to book your ultimate party today!</h3>-->
                    <div class="contentBlock2" style="margin-bottom:0px;">
                    <div id="player" style="margin-left:-10px;margin-top:-15px;">Loading videos.</div>
                    <script type="text/javascript" src="media/swfobject.js"></script>
                    
<script type="text/javascript">
    var d = new Date();
    var n = d.getSeconds();
   // var so = new SWFObject('/media/player.swf?'+n, 'mpl', '236', '275', '9');
    var so = new SWFObject('/media/player.swf?' + n, 'mpl', '285', '305', '9');
   
    so.addParam('allowscriptaccess', 'always');
    so.addParam('allowfullscreen', 'false');
    so.addParam('wmode', 'transparent');
    so.addParam('flashvars', '&file=/media/playlist.xml&playlistsize=0');
    so.write('player');
    
    
</script>
                        

		                
                       
                    </div>
                </div>
                <div class="cleaner">&nbsp;</div>
                <div class="contentBlock" id="newTalent">
                    <div id="shortLeft">
                        <h2><asp:Image runat="server" ImageUrl="~/images/h2NewTalent.gif" AlternateText="New Talent" ID="imgNewTalent" /><span>New Talent</span></h2>
                        <p>Centerfold Strips is always hiring new strippers and exotic dancers to add to our roster of adult entertainers. Check out some of our  newest talent.</p>
                        <!--<asp:HyperLink runat="server" ImageUrl="~/images/moreButton.gif" ID="hlMoreTalent" NavigateUrl="#" />-->
                    </div>
                    <div id="tallRight">
                        <ul>
                        <li>
                        <asp:DataList runat="server" ID="dlNewTalent" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                    <span style="float: left; margin-left: 15px; width: 58px;">
                                    <asp:HyperLink runat="server" ID="hlNewTalent" NavigateUrl='<%#CfsCommon.GetNavUrl(Eval("TalentId").ToString()) %>'>
                                        <asp:Image runat="server" ID="imgNewTalent" AlternateText='<%#Eval("StageName") %>'
                                            Width="58" Height="78" />
                                    </asp:HyperLink>
                                    <span style="width: 58px; height: 1em; overflow: hidden; display: block;">
                                    <%#Eval("StageName") %>
                                    </span>
                                    <span style="width: 58px;">
                                    <%#Eval("State") %>
                                    </span>
                                    </span>                            
                            </ItemTemplate>
                        </asp:DataList>
                        </li>
                        </ul>
                       
                    </div>
                    <div class="cleaner">&nbsp;</div>
                </div>
                
            </div>
            
            <div id="rightColumn">
                <div class="contentBlock" style="height:442px;">
                    <h2><asp:Image runat="server" ImageUrl="~/images/h2CustomerTest.gif" AlternateText="Customer Testimonials" ID="imgCustomerTest" /><span>Customer Testimonials</span></h2>
                    <p>WOW! Kitty was FANTASTIC! Just as you said, she was super sexy and put on an awesome show. My husband was so surprised and loved every minute of 
                        it. It was great. You will definitely be hearing from us again...</p>
                    <p>- Kim S</p>
                    <asp:HyperLink runat="server" ImageUrl="~/images/moreButton.gif" ID="hlMoreTest" Text="More" Width="63" Height="19" NavigateUrl="~/About-Us/Testimonials.aspx" />
                    <asp:Image runat="server" ImageUrl="~/images/seperatorBar.gif" ID="imgSeparator" AlternateText="--" />
                    <h2><asp:Image runat="server" ImageUrl="~/images/h2CreditsAppearances.gif" AlternateText="Credits &amp; Appearances" ID="imgCredits" /><span>Credits &amp; Appearances</span></h2>
                    <asp:HyperLink runat="server" NavigateUrl="~/About-Us/Credits.aspx" ImageUrl="~/images/seenIn.jpg" ID="imgSeen" Text="Seen In" Width="144" Height="128" />
				<!--
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
				   onclick="return addthis_sendto();"><img runat="server" src="~/images/bookmark.gif" width="124" height="18" alt="Bookmark or Share" style="margin-top:8px; margin-left:8px;" /></a>
				<script type="text/javascript" src="http://s7.addthis.com/js/200/addthis_widget.js"></script>
                -->
				<!-- Place this tag where you want the +1 button to render -->
				<!--<div style="text-align:center; padding-top:5px"><g:plusone size="tall" annotation="none" href="https://plus.google.com/114563856311390819240/posts/W1XUAHukgEA#114563856311390819240/posts"></g:plusone></div>                </div>-->
            </div>
            <div class="cleaner">&nbsp;</div>
        </div>
        <div class="cleaner" style="background-color:#08070B;">&nbsp;</div>
        <cc1:Footer runat="server" ID="footer1" />
        
    </div>
    
   <!--<div style="margin-left:-26px"><asp:Image runat="server" ID="imgBottom" ImageUrl="~/images/mainBGGlowBottom.png" AlternateText="Glowing Background" Width="1100" Height="51" />
   </div>-->
  
   
    </form>
   <!--  <div class="noclass" align="center">
   <a href="http://stripclubs4sale.com/" target="_blank"><asp:Image runat="server" ID="Banner222" ImageUrl="~/images/sc4sbanner13.jpg" AlternateText="Strip4Club" Width="700" /></a>
   </div>-->
   <!--   <div class="noclass" align="center">
      <a href="http://www.theedexpo.com" target="_blank"><asp:Image runat="server" ID="Banner22233" ImageUrl="~/images/banners/expobanner.gif" AlternateText="www.theedexpo.com" Width="700" /></a>
 
    </div> -->
<script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
<script type="text/javascript">_uacct = "UA-1316591-2";urchinTracker();</script>
<script type="text/javascript">
var _sf_async_config={uid:6798,domain:"centerfoldstrips.com"};
(function(){
  function loadChartbeat() {
    window._sf_endpt=(new Date()).getTime();
    var e = document.createElement('script');
    e.setAttribute('language', 'javascript');
    e.setAttribute('type', 'text/javascript');
    e.setAttribute('src',
       (("https:" == document.location.protocol) ? "https://s3.amazonaws.com/" : "http://") +
       "static.chartbeat.com/js/chartbeat.js");
    document.body.appendChild(e);
  }
  var oldonload = window.onload;
  window.onload = (typeof window.onload != 'function') ?
     loadChartbeat : function() { oldonload(); loadChartbeat(); bodyloadOne();};
})();

</script>
<!-- Begin Tracking Code -->
<script type="text/javascript">
var trackCid = 136781;
var trackTid = '';
</script>
<script type="text/javascript">
document.write(unescape('%3Cscript src="' + (document.location.protocol == 'https:' ? 'https:' : 'http:') + '//otracking.com/js/TrackingV2.js" type="text/javascript"%3E%3C/script%3E'));
</script>
<!-- End Tracking Code -->
<!-- Quantcast Tag -->
<script type="text/javascript">
var _qevents = _qevents || [];

(function() {
var elem = document.createElement('script');
elem.src = (document.location.protocol == "https:" ? "https://secure" : "http://edge") + ".quantserve.com/quant.js";
elem.async = true;
elem.type = "text/javascript";
var scpt = document.getElementsByTagName('script')[0];
scpt.parentNode.insertBefore(elem, scpt);
})();

_qevents.push({
qacct:"p-caklBAn4EXPWs"
});
</script>

<noscript>
<div style="display:none;">
<img src="//pixel.quantserve.com/pixel/p-caklBAn4EXPWs.gif" border="0" height="1" width="1" alt="Quantcast"/>
</div>
</noscript>
<!-- End Quantcast tag -->
</body>
</html>
