<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Duos-Talent.aspx.cs" Inherits="Entertainers_Duos_Talent" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
	#frameHolder #flashNav
	{
	    background-image: url(../images/headerDuoShows.jpg);
	    background-repeat: no-repeat;	
	}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
<!--Start Repeating Dancer Row-->
			<div class="dancerDuoThumb"><a href="Alexia-Michelle.aspx"><img src="../talentimages/duos/Michelle-Alexia-Thumb.jpg" width="160" height="160" alt="Alexia Moore &amp; Michelle" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
				Alexia Moore &amp; Michelle<br />
	     	NY</div>
			<div class="dancerDuoThumb"> <a href="Alissa-Becky.aspx"><img src="../talentimages/duos/Alissa-Becky-Thumb.jpg" width="160" height="160" alt="Alissa &amp; Becky" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
				Alissa &amp; Becky<br />
				FL
			</div>
<div class="dancerDuoThumb" style="margin-right:0px;"> <a href="Heaven-Tina.aspx"><img src="../talentimages/duos/Heaven-Tina-Thumb.jpg" width="160" height="160" alt="Heaven &amp; Tina" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
		Heaven &amp; Tina<br />
				IL
			</div>
			<div class="cleaner">&nbsp;</div>
			<!--End Repeating Dancer Row-->
               <!--Insert the hr only if there are more dancers to display--><hr />
			<!--Start Repeating Dancer Row-->
<div class="dancerDuoThumb"><a href="Jade-Nikki.aspx"><img src="../talentimages/duos/Jade-Nikki-Thumb.jpg" width="160" height="160" alt="Dancer Name" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
		Jade &amp; Nikki<br />
				NY
			</div>
			<div class="dancerDuoThumb"> <a href="Mandy-Michelle.aspx"><img src="../talentimages/duos/Mandy-Michelle-Thumb.jpg" width="160" height="160" alt="Mandy &amp; Michelle" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
				Mandy &amp; Michelle<br />
			NY</div>
<div class="dancerDuoThumb" style="margin-right:0px;"> <a href="Troy-Rebecca.aspx"><img src="../talentimages/duos/Troy-Rebecca-Thumb.jpg" width="160" height="160" alt="Dancer Name" /></a>
			     <!--[if lte IE 6]><br /><![endif]-->
		Troy &amp; Rebecca<br />
				NY
			</div>
			<div class="cleaner">&nbsp;</div>
			<!--End Repeating Dancer Row-->

</asp:Content>

