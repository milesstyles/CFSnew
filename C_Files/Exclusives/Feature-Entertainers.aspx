<%@ Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Feature-Entertainers.aspx.cs" Inherits="Feature_Entertainers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<title>Adult Dancers | Featured Strippers from CenterfoldStrips.com</title>
<meta name="keywords" content="Adult Dancers" />
<meta name="description" content="This elite group of dancers are the hottest showgirls, who have reached the pinnacle of the industry!" />
<style type="text/css">
    #frameHolder #flashNav
    {
    	background-image:url(../images/headerFeature.jpg);
    	background-repeat:no-repeat;
    }
</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="partyLocationsHolder" runat="server" >
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="Server">

<div class="navigation" style="height:31px;">
    <asp:ImageButton runat="server" ID="ibMoreTop" ImageUrl="~/images/moreButtonArrow.gif" Width="63" Height="19" AlternateText="More" style="float:right;" OnClick="btnMore_Click" />
    <asp:ImageButton runat="server" ID="ibBackTop" ImageUrl="~/images/backButton.gif" Width="63" Height="19" AlternateText="Back" OnClick="btnBack_Click" /> 
</div>

<asp:DataList runat="server" ID="dlTalentList" RepeatColumns="5" RepeatDirection="Horizontal">
    <ItemTemplate>
        <div class="dancerThumb">
            <asp:HyperLink runat="server" ID="hlDancer" NavigateUrl='<%#CfsCommon.GetNavUrl(Eval("TalentId").ToString()) %>' >
                <img src='<%# ResolveUrl(CfsCommon.RetrieveImageUrl((string)Eval("ThumbImg"))) %>' alt="Dancer Thumb" />
            </asp:HyperLink>
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

<div style="position:absolute; margin-top:-670px; margin-left:-140px;" >
<table width=400px">
<tr>
<td>
Centerfold Features specializes in providing the top Feature Entertainers and Dancers in the exotic dancer industry to perform as headliners in gentlemen’s clubs, strip clubs, topless clubs and adult night clubs around the globe. We represent the sexiest and most talented adult performers on the circuit today. Our Feature Entertainers include the most famous celebrity XXX Porn Stars, the most stunning, glamorous & award winning showgirls and pole dance champions with the biggest stage shows and exquisite costumes on the circuit, and unique novelty acts!

</td>

</tr>
</table>
</div>
</asp:Content>



