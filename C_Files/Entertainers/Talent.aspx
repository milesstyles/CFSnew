<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Talent.aspx.cs" Inherits="Talent_Page" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server" >
<style type="text/css">
    #frameHolder #flashNav
    {
    	background-image:url(../images/<%= m_hdrImage %>);
    	background-repeat:no-repeat;
    	
    }
    #talentSubHdr
    {
         font-size:12px;
          font-family:Arial;
        }
  
</style>
<script type="text/javascript">
    talentHeaders = {
        bbw: "",
        bellydancer: "",
        dragqueen: "",
        duo: "",
        female: "",
        femalelittleperson: "",
        impersonator: ",
        male: "",
        malelittleperson: "",
        novelty: ""
    };
</script>
</asp:Content>
<%-- Female Meta content--%>
<asp:Content Id="content12" ContentPlaceHolderID="headFemale" Visible="false" runat="server">
    <title>Female Strippers | Your destination for elite female stripper service</title>
    <meta name="description" content="Explore our listings of the hottest female strippers in your area and conveniently book online or over the phone." />
    <meta name="keywords" content="female strippers, female stripper" />
    <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
Centerfold Strips provides the hottest female strippers, female exotic dancers, lapdancers, topless dancers, nude dancers, pole dancers, table dancers and female adult entertainers for any event. Our female strippers can provide amazing adult entertainment and striptease shows for your bachelor party, stag party, frat party, Holiday party, Superbowl party, birthday party, going away party or any special occasion. We also provide topless waitresses, topless bartenders, and nude models.
    
</td>

</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Male Meta content--%>
<asp:Content Id="content3" ContentPlaceHolderID="headMale"  Visible="false" runat="server">
    <title>Male Strippers | Your destination for male stripper service</title>
    <meta name="description" content="Explore our listings of the hottest male strippers in your area and conveniently book online or over the phone." />
    <meta name="keywords" content="male strippers, male stripper" />
   <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
Centerfold Strips provides the sexiest hardbody male strippers, male exotic dancers and male adult entertainers for any event. Our sexy male strippers, studs and gigolos provide orgasmic adult entertainment for your bachelorette party, bridal shower, hen’s night, passion party, birthday party sorority party, retirement party or any special occasion. Our male stripper hunks have performed at the top male revues around the globe including Chippendales, The Thunder From Down Under, Hunkmania, Hunkomania, Magic Mike, Fifty Shades, and many more.
           
</td>

</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Female Little Person Meta content--%>
<asp:Content Id="content4" ContentPlaceHolderID="headFemaleLittle" Visible="false" runat="server">
    <title>Midget Strippers | Female midget stripper services are highly entertaining</title>
    <meta name="description" content="Our female midget strippers are available for a variety of entertainment possibilities. What will it be?" />
    <meta name="keywords" content="midget strippers, female midget stripper" />
      <div class="nanHeaderFixMaleMidget"><table width="248">
<tr>

    <td>
    Centerfold Strips has the hottest and largest selection of female midget strippers, dwarf strippers, little people strippers, midget exotic dancers and dwarf exotic dancers in the adult entertainment industry. Our female midget strippers and dwarf exotic dancers provide thrilling and unique adult entertainment for a for a bachelor party, stag party, fraternity party, frat party, Holiday party, Superbowl party, birthday party, going away party or any special occasion. Some of our midget strippers have been featured in Hollywood movies, television commercials and TV shows such as Little Women LA, The Little Couple, Little People, Big World, Pit Boss, 7 Little Johnstons and Our Little Family.        
</td>
</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Male Little Person Meta content--%>
<asp:Content Id="content5" ContentPlaceHolderID="headMaleLittle" Visible="false" runat="server">
    <title>Dwarf Strippers | Male dwarf stripper services are highly entertaining</title>
    <meta name="description" content="Our male dwarf strippers are available for a variety of entertainment possibilities. What will it be?" />
    <meta name="keywords" content="dwarf strippers, male dwarf stripper" />
     <div class="nanHeaderFixMaleMidget"><table width="248">
<tr>

<td>
    Centerfold Strips has the hottest and largest selection of male midget strippers, dwarf strippers, little people strippers, midget exotic dancers and dwarf exotic dancers in the adult entertainment industry. Our male midget strippers and dwarf exotic dancers provide thrilling and unique adult entertainment for a for a bachelorette party, hen’s night, passion party, sorority party, Holiday party, Superbowl party, birthday party, going away party or any special occasion.
    </td>
</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- BBW Meta content--%>
<asp:Content Id="content6" ContentPlaceHolderID="headBBW" Visible="false" runat="server">
    <title>Big Booty Strippers | Fat stripper & overweight stripper services</title>
    <meta name="description" content="With lots of love to give, our beautiful big women will rock your world" />
    <meta name="keywords" content="big booty strippers, fat stripper" />
    <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
  Centerfold Strips has the hottest and largest selection of BBW Strippers, overweight strippers, large strippers, obese strippers, fat strippers, fat exotic dancers, overweight exotic dancers, plumpers, fat chicks, chubby strippers, and bbw exotic dancers in the adult entertainment industry. Our bbw strippers provide fun adult entertainment for a for a bachelor party, stag party, fraternity party, frat party, Holiday party, Superbowl party, birthday party, going away party or any special occasion, especially for that friend who is a chubby chaser.      
    </td>

</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Duo Meta content--%>
<asp:Content Id="content7" ContentPlaceHolderID="headDuo" Visible="false" runat="server">
    <title>Lesbian Strippers | Hot stripper duo shows and girl-on-girl action</title>
    <meta name="description" content="For men who like lots of sexy, hot entertainment. Get ready to experience the wild side of two girls" />
    <meta name="keywords" content="lesbian strippers, stripper duo" />
    
      <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
   Lesbian strippers are the hottest show to book for your bachelor party, birthday party, frat party or super bowl party. Our lesbian strippers and lesbian exotic dancers provide the hottest interactive girl / girl action for your event. Lesbian stripper shows include adult toys, anal ring toss, and many more fun XXX party games.     
    </td>

</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Belly Dancer Meta content--%>
<asp:Content Id="content8" ContentPlaceHolderID="headBellyDancer" Visible="false" runat="server">
    <title>Belly Dancers | Belly dancer services to impress your guests</title>
    <meta name="description" content="Your event will be one to remember with our beautiful and talented belly dancers entertaining your guests" />
    <meta name="keywords" content="belly dancers, belly dancer" />
    <div class="nanHeaderFixBelly"><table width="240">
<tr>
<td>
  Centerfold Strips provides the most gorgeous and talented belly dancers in the industry. Our professionally trained and highly experienced bellydancers are available to perform at your bachelor party, bachelorette party, birthday party, stag party, hen’s night, bridal shower, holiday party, retirement party, graduation party, corporate event, or any special occasion. Our bellydancers are proficient in all styles of bellydancing including: American Cabaret belly dancing, Egyptian belly dancing, tribal belly dancing, tribal fusion belly dancing, Persian belly dancing, Turkish belly dancing, Gypsy belly dancing and Greek belly dancing. The belly dancers also have props including candle trays, cymbals, canes, swords, zills, veils, and Isis wing  
  
</td>

</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Drag Queen Meta content--%>
<asp:Content Id="content9" ContentPlaceHolderID="headDragQueen" Visible="false" runat="server">
    <title>Drag Queen Strippers | Your destination for cross dressing strippers</title>
    <meta name="" content="" />
    <div class="nanHeaderFixDragQueen"><table width="250">
<tr>
<td>
 Centerfold Strips presents the hottest Drag Queens, divas, female impersonators, transgender performers, TS performers, transvestites, and gender illusionists. The drag Queen entertainers are available to perform, greet & host at your bachelor party, bachelorette party, birthday party, stag party, hen’s night, bridal shower, holiday party, retirement party, graduation party, corporate event, or any special occasion. Our drag Queens has performed at some of the top Drag Queen revue shows and venues around the globe including Lucky Chengs, Lips, Dreamgirls Revue, Diva Royale, Fauxgirls! And RuPaul’s Drag Race.
 </td>
</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Impersonator Meta content--%>
<asp:Content Id="content10" ContentPlaceHolderID="headImpersonator" Visible="false" runat="server">
    <title>Centerfold Strips</title>
    <meta name="" content="" />
    <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
 Centerfold Strips books the most professional celebrity impersonators, tribute artists, and celebrity look a likes in the entertainment business. Our celebrity impersonators are available for fairs, theme parks, grand openings, casinos, in store promotions, anniversary parties, birthday parties, bridal showers, weddings, cruise ships, benefits, corporate events, holiday parties, and any special occasion. From Elvis impersonators and Marilyn Monroe impersonators to Lady Gaga impersonators Centerfold Strips has you covered.
 </td>
</tr>
</table>
    
    
    </div>
</asp:Content>

<%-- Novelty Act Meta content--%>
<asp:Content Id="content11" ContentPlaceHolderID="headNovelty" Visible="false" runat="server">
    <title>Centerfold Strips</title>
    <meta name="" content="" />
    <div class="nanHeaderFixMale"><table width="250">
<tr>
<td>
 Centerfold Strips is your source for amazing and unique specialty novelty entertainment, and variety acts. Our novelty acts include Santa Claus, MRS Claus complete with little people elves,  circus performers, magicians, stilt walkers, fire breathers, cover bands, cigar rollers, mermaids, super hero costumed characters, wrestling shows, and much more! Our variety act performers and shows perform for corporate events, fairs, festivals, cruise ships, holiday parties, birthday parties and any special event.
 </td>
</tr>
</table>
    
    
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="partyLocationsHolder" Runat="Server">
    <span id="talentSubHdr"><!--Centerfold Strips specializes in a multitude of different scenerios. --></span>
    <script type="text/javascript">
        // Set header text
        var hdr = document.getElementById("talentSubHdr");
        hdr.innerHTML = talentHeaders["<%= m_talentType %>"];
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <asp:PlaceHolder runat="server" ID="pHldFilter" >
        <div id="filter" >
            <asp:DropDownList runat="server" ID="ddlTalentWorksIn" AutoPostBack="true" OnSelectedIndexChanged="OnChange_ddlTalentWorksIn" >
            </asp:DropDownList>    
        </div>
    </asp:PlaceHolder>
    
    <div class="navigation" style="height:31px; display:<%= (m_talentType == CfsCommon.TALENT_TYPE_ID_DUO.ToString() ? "none" : "block") %>;">
        <asp:ImageButton runat="server" ID="ibMoreTop" ImageUrl="~/images/moreButtonArrow.gif" Width="63" Height="19" AlternateText="More" style="float:right;" OnClick="btnMore_Click" />
        <asp:ImageButton runat="server" ID="ibBackTop" ImageUrl="~/images/backButton.gif" Width="63" Height="19" AlternateText="Back" OnClick="btnBack_Click" /> 
    </div>
    
    <asp:DataList runat="server" ID="dlTalentList" RepeatColumns="5" RepeatDirection="Horizontal">
        <ItemTemplate>
            <div class="dancerThumb">
                <asp:HyperLink runat="server" ID="hlDancer" NavigateUrl='<%#CfsCommon.GetNavUrl(Eval("TalentId").ToString()) %>' >
                    <img id="imgThumb" runat="server" src='<%#ResolveUrl(CfsCommon.RetrieveImageUrl((string)Eval("ThumbImg")) )%>' width="90" height="90" alt="Dancer Thumb" />
                </asp:HyperLink>
                <label style="margin: 0" >&nbsp;<%#Eval("StageName") %></label>
                <label style="margin: 0" >&nbsp;<%#Eval("WorksInList") %></label>
            </div>
            <div class="cleaner">&nbsp;</div>
        </ItemTemplate>
    </asp:DataList>
    
    <asp:DataList runat="server" ID="dlDuoTalentList" RepeatColumns="3" RepeatDirection="Horizontal">
        <ItemTemplate>
            <div class="dancerDuoThumb" style="margin-bottom: 10px;">
                <asp:HyperLink runat="server" ID="hlDancer" NavigateUrl='<%#CfsCommon.GetNavUrl(Eval("TalentId").ToString()) %>' >
                    <img src="<%#ResolveUrl(CfsCommon.RetrieveImageUrl((string)Eval("ThumbImg")) )%>" width="90" height="90" alt="Dancer Thumb" />
                </asp:HyperLink>
                 <!--[if lte IE 6]><br /><![endif]-->
                <label style="margin: 0" >&nbsp;<%#Eval("StageName") %></label>
                <label style="margin: 0" >&nbsp;<%#Eval("WorksInList") %></label>
             </div>
             <div class="cleaner">&nbsp;</div>
        </ItemTemplate>
    </asp:DataList>
    
    <div class="navigation" style="height:31px;">
        <asp:ImageButton runat="server" ID="ibMoreBottom" ImageUrl="~/images/moreButtonArrow.gif" Width="63" Height="19" AlternateText="More" style="float:right;" OnClick="btnMore_Click" />
        <asp:ImageButton runat="server" ID="ibBackBottom" ImageUrl="~/images/backButton.gif" Width="63" Height="19" AlternateText="Back" OnClick="btnBack_Click" />  
    </div>
</asp:Content>

