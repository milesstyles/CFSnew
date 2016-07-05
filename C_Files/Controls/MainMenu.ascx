﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainMenu.ascx.cs" Inherits="Controls_MainMenu" %>
<script type="text/javascript">
    // <![CDATA[
    startList = function() {
        if (document.all && document.getElementById) {
            navRoot = document.getElementById("mainMenu");
            for (i = 0; i < navRoot.childNodes.length; i++) {
                node = navRoot.childNodes[i];
                if (node.nodeName == "LI") {
                    node.onmouseover = function() {
                        this.className += " over";
                    }
                    node.onmouseout = function() {
                        this.className = this.className.replace(" over", "");
                    }
                }
            }
        }
    }
    window.onload = startList;
    // ]]>
</script>

<ul id="mainMenu">
    <li>
        <asp:HyperLink runat="server" Width="175px" ID="hlEntertainers" NavigateUrl="~/Entertainers/Talent.aspx?cat=female" Text="Adult Entertainers" />
        <ul>
            
            <li><asp:HyperLink runat="server" ID="hlFemaleDancers" NavigateUrl="~/Entertainers/Talent.aspx?cat=female" Text="Female Strippers" /></li>
             <li><asp:HyperLink runat="server" ID="hlFeatureEntertainers" NavigateUrl="~/Exclusives/Feature-Entertainers.aspx" Text="Feature Entertainers" /></li>
             <li><asp:HyperLink runat="server" ID="hlDuoShows" NavigateUrl="~/Entertainers/Talent.aspx?cat=duo" Text="Lesbian Strippers" /></li>
          
            <li><asp:HyperLink runat="server" ID="hlMaleDancers" NavigateUrl="~/Entertainers/Talent.aspx?cat=male" Text="Male Strippers" /></li>
            <li><asp:HyperLink runat="server" ID="hlFemaleLittle" NavigateUrl="~/Entertainers/Talent.aspx?cat=minifemale" Text="Midget Strippers" /></li>
            <li><asp:HyperLink runat="server" ID="hlMaleLittle" NavigateUrl="~/Entertainers/Talent.aspx?cat=minimale" Text="Dwarf Strippers" /></li>
             <li><asp:HyperLink runat="server" ID="hlBig" NavigateUrl="~/Entertainers/Talent.aspx?cat=bbw" Text="BBW Overweight Strippers" /></li>
           
            <li><asp:HyperLink runat="server" ID="hlBelly" NavigateUrl="~/Entertainers/Talent.aspx?cat=bellydancer" Text="Belly Dancers" /></li>
            <li><asp:HyperLink runat="server" ID="hlImpersonators" NavigateUrl="~/Entertainers/Talent.aspx?cat=impersonator" Text="Celebrity Impersonators" /></li>
           
            <li><asp:HyperLink runat="server" id="hlDrag" NavigateUrl="~/Entertainers/Talent.aspx?cat=dragqueen" Text="Drag Queens" /></li>
            <li><asp:HyperLink runat="server" ID="hlNovelty" NavigateUrl="~/Entertainers/Talent.aspx?cat=novelty" Text="Novelty Acts" /></li>
            </ul>
    </li>
    <li>
        <asp:HyperLink runat="server" Width="180px" ID="hlServices" NavigateUrl="~/Services/Services.aspx" Text="Party Services" />
        <ul>
            <li><asp:HyperLink runat="server" ID="hlPartyBus" NavigateUrl="~/Exclusives/Limos-and-Party-Buses.aspx" Text="Book Your Own Private Strip Club In A Limo Bus" /></li>
            <li><asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/Exclusives/Party-Locations.aspx" Text="Book Private Party Rooms Available For Rental" /></li>
            <li><asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="~/Exclusives/Male-Review.aspx" Text="Male Revues" /></li>
            <li><asp:HyperLink runat="server" ID="HyperLink6" NavigateUrl="~/Exclusives/Bar-Nightclub-Owners.aspx" Text="Bar And Nightclub Owners" /></li>
            
            <li><asp:HyperLink runat="server" ID="hlPartyAcc" NavigateUrl="~/Services/Party-Accessories.aspx" Text="Purchase A Stripper Pop Out Cake" /></li>
           
            <li><asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl="~/Services/Cash_Cannon.aspx" Text="Purchase A Cash Cannon" /></li>
            <li><asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/Services/Customized_Videos.aspx" Text="Purchase Custom Entertainer Videos" /></li>
            <li><asp:HyperLink runat="server" ID="hlBachelor" NavigateUrl="~/Services/Plan-Bachelor-Party.aspx" Text="Guide to Planning a Bachelor Party" /></li>
            <li><asp:HyperLink runat="server" ID="hlBachelorette" NavigateUrl="~/Services/Plan-Bachelorette-Party.aspx" Text="Guide to Planning a Bachelorette Party" /></li>
           <!-- <li><asp:HyperLink runat="server" ID="HyperLink8" NavigateUrl="~/Services/Plan-Bachelor-Party.aspx" Text="Bachelor Party Invitation Template" /></li>-->
           
           </ul>
    </li>
   <!-- <li>
        <asp:HyperLink runat="server" Width="152px" ID="hlExclusives" NavigateUrl="~/Exclusives/Feature-Entertainers.aspx" Text="Exclusives" />
        <ul>
         <li><asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/Exclusives/Feature-Entertainers.aspx" Text="Feature Entertainers" /></li>
           
            <li><asp:HyperLink runat="server" ID="hlPartyExp" NavigateUrl="~/Exclusives/Strip-Club-Experience.aspx" Text="The Total Strip Club Experience" /></li>
            <li><asp:HyperLink runat="server" ID="hlPartyLoc" NavigateUrl="~/Exclusives/Party-Locations.aspx" Text="Private Party Rooms" /></li>
            <li><asp:HyperLink runat="server" ID="hlLimos" NavigateUrl="~/Exclusives/Limos-and-Party-Buses.aspx" Text="Strip Club Limousines" /></li>	
            <li><asp:HyperLink runat="server" ID="hlPartyPic" NavigateUrl="~/Exclusives/Party-Pics.aspx" Text="Stripper Party Photos" /></li>
            <li><asp:HyperLink runat="server" ID="hlMaleReview" NavigateUrl="~/Exclusives/Male-Review.aspx" Text="Male Revue" /></li>
            <li><asp:HyperLink runat="server" ID="hlBar" NavigateUrl="~/Exclusives/Bar-Nightclub-Owners.aspx" Text="Bar/Nightclub Owners" /></li>
            <li><asp:HyperLink runat="server" ID="hlEvents" NavigateUrl="~/Exclusives/Event-Schedule.aspx" Text="Schedule of Events" /></li>
        </ul>
    </li>-->
    <li>
        <asp:HyperLink runat="server" Width="180px" ID="hlAboutUs" NavigateUrl="~/About-Us/About-Us.aspx" Text="About Us" />
        <ul>
          <li><asp:HyperLink runat="server" ID="hlAdvantage" NavigateUrl="~/About-Us/Advantage.aspx" Text="The Centerfold Strips Advantage" /></li>
    
            <li><asp:HyperLink runat="server" ID="hlFAQ" NavigateUrl="~/About-Us/FAQ.aspx" Text="FAQ" /></li> 
             <li><asp:HyperLink runat="server" ID="hlTestimonials" NavigateUrl="~/About-Us/Testimonials.aspx" Text="Client Testimonials" /></li>
           
            <li><asp:HyperLink runat="server" ID="hlAppearances" NavigateUrl="~/About-Us/Credits.aspx" Text="Centerfold Strips Media Appearances" /></li>
            
                <li><asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/Exclusives/Party-Pics.aspx" Text="Event Photos" /></li>
              
             <li><asp:HyperLink runat="server" ID="hlPartyInfo" NavigateUrl="~/Services/Party-Information.aspx" Text="Company Policies" /></li>
     
            </ul>
    </li>
    <li> <asp:HyperLink runat="server" Width="180px" ID="h1Employment" NavigateUrl="~/About-Us/Employment.aspx" Text="Employment" />
        </li>
    <li>
        <asp:HyperLink runat="server" Width="180px" ID="hlContact" NavigateUrl="~/Contact-Us/Contact-Us.aspx" Text="Contact Us" />
        <ul>
          <!--  <li><asp:HyperLink runat="server" ID="hlLinks" NavigateUrl="~/Contact-Us/Links.aspx" Text="Affiliated Companies" /></li>
         <!--     <li><asp:HyperLink runat="server" ID="hlApply" NavigateUrl="~/About-Us/employment-app.aspx" Text="Apply Today" /></li>
           <li><asp:HyperLink runat="server" ID="hlEmployment2" NavigateUrl="~/About-Us/Employment.aspx" Text="Current Job Openings" /></li>
            <li><asp:HyperLink runat="server" ID="hlBlog" NavigateUrl="http://blog.centerfoldstrips.com/" Text="CFS Blog" /></li>
            <li><asp:HyperLink runat="server" ID="h1Facebook" NavigateUrl="http://www.facebook.com/pages/New-York-NY/Centerfold-Strips/121157427508" Text="Become a Fan On Facebook" /></li>
            <li><asp:HyperLink runat="server" ID="hlMyspace" NavigateUrl="http://www.myspace.com/centerfoldstrips" Text="CFS On MySpace" /></li>
            <li><asp:HyperLink runat="server" ID="hlTwitter" NavigateUrl="http://twitter.com/centerfoldstrip" Text="CFS on Twitter" /></li>-->
           
        </ul>
    </li>
</ul>