<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Talent.aspx.cs" Inherits="Talent_Page" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server" >
<style type="text/css">
    #frameHolder #flashNav
    {
    	background-image:url(../images/<%= m_hdrImage %>);
    	background-repeat:no-repeat;
    }
</style>
<script type="text/javascript">
    talentHeaders = {
        bbw: "Centerfold Strips is your number one destination for  the most entertaining overweight and fat strippers. Simply select from our amazing  big booty  strippers  below and bring the hottest big beautiful women off the web page and to your next event. So if you’re looking for the hottest big booty strippers, be assured that you’ve come to the right place. ",
        bellydancer: "Pick your dancer from our selection below",
        dragqueen: "Centerfold Strips is  your  number one  destination for  the  most entertaining  cross dressing strippers. Simply select from our amazing drag queen strippers below and bring the hottest adult entertainment off the web page and to your next event. So if you’re looking for the hottest drag queen strippers, be assured that you’ve come to the right place. ",
        duo: "Centerfold Strips is your number one destination for the hottest stripper duo shows and girl on girl action. Simply select from our amazing lesbian stripper below and bring the hottest talent off the web page and to your next event. Our talent has made their mark in television, adult magazines, movies and some of the best clubs on the planet - and now our sexy lesbian strippers are ready to perform for you!",
        female: "Centerfold Strips is your number one destination for all your  female  stripper service  needs. Simply select from our amazing female strippers below and bring the hottest talent off the web page and to your next event. So if you’re looking for the hottest female strippers, be assured that you have come to the right place.",
        femalelittleperson: "Centerfold Strips is  your  number one  destination for  the  most entertaining  female  midget stripper services.  Simply select from our amazing  female midget strippers below and bring the hottest talent off the web page and to your next event.  So if you’re  looking for the hottest female midget strippers, be assured that you’ve come to the right place.",
        impersonator: "Centerfold Strips is  your number one destination for  the most entertaining  celebrity look alikes. Simply select from our amazing celebrity impersonators below and bring the hottest adult entertainment off the web page and to your next event. So if you’re looking for the hottest celebrity impersonatos in costume, be assured that you’ve come to the right place. ",
        male: "Centerfold Strips is your number one destination for all your male stripper service needs. Simply select from our amazing male strippers below and bring the hottest talent off the web page and to your next event. Our talent has made their mark in television, adult magazines, movies and some of the best clubs on the planet - and now our sexy male strippers are ready to perform for you!",
        malelittleperson: "Centerfold Strips is your number one destination for the most entertaining male dwarf stripper services. Simply select from our amazing male dwarf stripper hunks below and bring the hottest talent off the web page and to your next event. Our  talent has made their mark in television, adult magazines, movies and some of the best clubs on the planet - and now our sexy male dwarf strippers are ready to perform for you!",
        novelty: "Pick your dancer from our selection below"
    };
</script>
</asp:Content>
<%-- Female Meta content--%>
<asp:Content Id="content12" ContentPlaceHolderID="headFemale" Visible="false" runat="server">
    <title>Female Strippers | Your destination for elite female stripper service</title>
    <meta name="description" content="Explore our listings of the hottest female strippers in your area and conveniently book online or over the phone." />
    <meta name="keywords" content="female strippers, female stripper" />
    <asp:Label ID="blah" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Male Meta content--%>
<asp:Content Id="content3" ContentPlaceHolderID="headMale" Visible="false" runat="server">
    <title>Male Strippers | Your destination for male stripper service</title>
    <meta name="description" content="Explore our listings of the hottest male strippers in your area and conveniently book online or over the phone." />
    <meta name="keywords" content="male strippers, male stripper" />
    <asp:Label ID="Label1" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Female Little Person Meta content--%>
<asp:Content Id="content4" ContentPlaceHolderID="headFemaleLittle" Visible="false" runat="server">
    <title>Midget Strippers | Female midget stripper services are highly entertaining</title>
    <meta name="description" content="Our female midget strippers are available for a variety of entertainment possibilities. What will it be?" />
    <meta name="keywords" content="midget strippers, female midget stripper" />
    <asp:Label ID="Label2" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Male Little Person Meta content--%>
<asp:Content Id="content5" ContentPlaceHolderID="headMaleLittle" Visible="false" runat="server">
    <title>Dwarf Strippers | Male dwarf stripper services are highly entertaining</title>
    <meta name="description" content="Our male dwarf strippers are available for a variety of entertainment possibilities. What will it be?" />
    <meta name="keywords" content="dwarf strippers, male dwarf stripper" />
    <asp:Label ID="Label3" runat="server">wefe</asp:Label>
</asp:Content>

<%-- BBW Meta content--%>
<asp:Content Id="content6" ContentPlaceHolderID="headBBW" Visible="false" runat="server">
    <title>Big Booty Strippers | Fat stripper & overweight stripper services</title>
    <meta name="description" content="With lots of love to give, our beautiful big women will rock your world" />
    <meta name="keywords" content="big booty strippers, fat stripper" />
    <asp:Label ID="Label4" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Duo Meta content--%>
<asp:Content Id="content7" ContentPlaceHolderID="headDuo" Visible="false" runat="server">
    <title>Lesbian Strippers | Hot stripper duo shows and girl-on-girl action</title>
    <meta name="description" content="For men who like lots of sexy, hot entertainment. Get ready to experience the wild side of two girls" />
    <meta name="keywords" content="lesbian strippers, stripper duo" />
    <asp:Label ID="Label5" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Belly Dancer Meta content--%>
<asp:Content Id="content8" ContentPlaceHolderID="headBellyDancer" Visible="false" runat="server">
    <title>Belly Dancers | Belly dancer services to impress your guests</title>
    <meta name="description" content="Your event will be one to remember with our beautiful and talented belly dancers entertaining your guests" />
    <meta name="keywords" content="belly dancers, belly dancer" />
    <asp:Label ID="Label6" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Drag Queen Meta content--%>
<asp:Content Id="content9" ContentPlaceHolderID="headDragQueen" Visible="false" runat="server">
    <title>Drag Queen Strippers | Your destination for cross dressing strippers</title>
    <meta name="" content="" />
    <asp:Label ID="Label7" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Impersonator Meta content--%>
<asp:Content Id="content10" ContentPlaceHolderID="headImpersonator" Visible="false" runat="server">
    <title>Centerfold Strips</title>
    <meta name="" content="" />
    <asp:Label ID="Label8" runat="server">wefe</asp:Label>
</asp:Content>

<%-- Novelty Act Meta content--%>
<asp:Content Id="content11" ContentPlaceHolderID="headNovelty" Visible="false" runat="server">
    <title>Centerfold Strips</title>
    <meta name="" content="" />
    <asp:Label ID="Label9" runat="server">wefe</asp:Label>
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
                    <img id="imgThumb" runat="server" src='<%#ResolveUrl(CfsCommon.RetrieveImageUrl((string)Eval("ThumbImg")) )%>' width="90" alt="Dancer Thumb" />
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
                    <img src="<%#ResolveUrl(CfsCommon.RetrieveImageUrl((string)Eval("ThumbImg")) )%>" width="160" height="160" alt="Dancer Thumb" />
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

