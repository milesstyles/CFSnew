<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftColumn.ascx.cs" Inherits="Controls_LeftColumn" %>

<%@ Register src="MailingList.ascx" tagname="MailingList" tagprefix="uc1" %>

<div id="leftColumn">
	<asp:Panel Id="pnlShare" visible="true" runat="server">
	<!-- AddThis Button BEGIN -->
	<script type="text/javascript">
          var addthis_pub="centerfoldstrips";
          var addthis_brand = "Centerfold Strips";
          var addthis_header_color = "#ffffff";
          var addthis_header_background = "#000000";
          var addthis_options = 'email, facebook, favorites, myspace, aim, ask, delicious, digg, fark, friendfeed, google, linkedin, live, magnolia, newsvine, print, reddit, slashdot, stumbleupon, technorati, twitter, more';
     </script>
    <!-- <a href="http://www.addthis.com/bookmark.php" 
        style="text-decoration:none;" 
        onmouseover="return addthis_open(this, '', '[URL]', '[TITLE]');" 
        onmouseout="addthis_close();" 
        onclick="return addthis_sendto();"><img runat="server" src="~/images/bookmark.gif" width="124" height="18" alt="Bookmark or Share" style="margin-bottom:3px;" />
        </a>-->
     <script type="text/javascript" src="http://s7.addthis.com/js/200/addthis_widget.js"></script>
     <!-- AddThis Button END -->
     </asp:Panel>
    <br /><br /><br /><br /><br />
    <asp:HyperLink runat="server" NavigateUrl="" ID="hlPromoLink">
        <asp:Image runat="server" ID="imgPromo" ImageUrl="~/images/specialempty.jpg" CssClass="redBorder" BorderWidth="1" AlternateText="Promotional Special" />
    </asp:HyperLink>
    <br />
    <br /><br /><br /><div style="padding-bottom:9px;">s</div>
   <!-- <ul id="subMenu">
    	<!--<li><iframe src="//www.facebook.com/plugins/like.php?href=https%3A%2F%2Fwww.facebook.com%2Fcenterfoldentertainment&amp;send=false&amp;layout=button_count&amp;width=450&amp;show_faces=false&amp;action=like&amp;colorscheme=dark&amp;font&amp;height=21" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:450px; height:31px;" allowTransparency="true"></iframe></li>
        <li><asp:HyperLink runat="server" ID="hlBooking" NavigateUrl="~/Entertainers/Booking.aspx" Text="Book Your Show Now!" /></li>
        <li><asp:HyperLink runat="server" ID="hlAdvantage" NavigateUrl="~/About-Us/Advantage.aspx" Text="The CFS Advantage" /></li>
        </ul><!-->
    <uc1:MailingList ID="MailingList1" runat="server" />
</div>

