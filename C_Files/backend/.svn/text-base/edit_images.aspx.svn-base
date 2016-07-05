<%@ Page Title="Centerfold Strips | Edit Images" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="edit_images.aspx.cs" Inherits="edit_images" %>
<%@ Register assembly="AtomImageEditorServerControls" namespace="AtomImageEditorServerControls" tagprefix="atom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register src="../AtomImageEditor/ImageSelector.ascx" tagname="ImageSelector" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CFS Master Page</title>
    <link href="../css/backend.css" rel="Stylesheet" type="text/css" media="all" />
    <link href="../AtomImageEditor/ImageEditor.css" rel="Stylesheet" type="text/css" media="all" />
    <link href="../css/backendPrint.css" rel="Stylesheet" type="text/css" media="print" />
    <link rel="stylesheet" type="text/css" href="../AtomImageEditor/ImageEditor.css" />
    <link rel="stylesheet" type="text/css" href="../AtomImageEditor/ui-lightness/jquery-ui-1.7.2.custom.css" />

</head>
<body>
<form id="form1" runat="server">
    <div id="container">
        <div id="content">

<asp:ScriptManager runat="server" ID="scriptManager">
    <Scripts>
        <asp:ScriptReference Path="~/js/jquery-1.3.2.min.js" />
        <asp:ScriptReference Path="~/js/jquery-ui-1.7.2.custom.min.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/WatermarkDefinitionLibrary.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/EditableImage.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/SelectImageButton.js" />
        <asp:ScriptReference Path="~/AtomImageEditor/ImageSelector.js" />
    </Scripts>
    <Services>
        <asp:ServiceReference Path="~/ImageManagerWS.asmx" />
    </Services>
</asp:ScriptManager>

<h3 runat="server" id="headerViewEdit" style="padding-top: 15px;">Edit Images</h3>
<a style="color: white; font-size: 12px; text-decoration: underline; position: absolute; top: 15px; right: 50px;" href="#" onclick="self.parent.tb_remove();">Close</a>
<!-- Error Msg Box -->
<div runat="server" id="divErrorMsg" visible="false" class="errorBox" >
    <ul runat="server" id="ulErrorMsg" >
    </ul>
</div>  

<asp:HiddenField runat="server" ID="hiddenPageMode" Value='<%# CfsCommon.MODE_ADD %>' />

<div runat="server" id="divSelectTalent" visible="false">
    <asp:DropDownList runat="server" ID="ddlFullTalentList" CssClass="select" >
    </asp:DropDownList>                                                  
    <asp:Button runat="server" ID="btnSelectTalent" Text="GO" CssClass="button" />
</div>
<div runat="server" id="divEditImages" visible="true">
<!-- Used to keep track of the image file names (w/ out the path -->
<asp:HiddenField runat="server" ID="hiddenImgFn1" />
<asp:HiddenField runat="server" ID="hiddenImgFn2" />
<asp:HiddenField runat="server" ID="hiddenImgFn3" />        
        <div style="width: 100%; height: 20px" >&nbsp;</div>
            
            <h3><asp:Literal ID="litTalentName" runat="server" /></h3>
            
            
            <div class="imageEditorContainer">
                <atom:EditableImage id="imgTalent1" Width="160" Height="240" runat="server"  />
                <atom:SelectImageButton runat="server" id="selectImageButton1">Select Image</atom:SelectImageButton>
            </div>
            
            <div class="imageEditorContainer">
                <atom:EditableImage id="imgTalent2" Width="160" Height="240" runat="server" />
                <atom:SelectImageButton runat="server" id="selectImageButton2">Select Image</atom:SelectImageButton>
            </div>
            
            <div class="imageEditorContainer">
                <atom:EditableImage id="imgTalent3" Width="160" Height="240" runat="server" />
                <atom:SelectImageButton runat="server" id="selectImageButton3">Select Image</atom:SelectImageButton>
            </div>
            
                        <div style="clear: both;" >&nbsp;</div>
            
            <div class="imageEditorContainer">
                <div style="font-weight: bold; color: #ffffff; margin-bottom: 10px; margin-top: 5px;">Thumbnail Image</div>
                <atom:EditableImage id="imgTalentThumb" Width="90" Height="90" runat="server" />
                <atom:SelectImageButton runat="server" id="selectImageButton4">Select Image</atom:SelectImageButton>
            </div>
            
             <div class="imageEditorContainer">
                <div style="font-weight: bold; color: #ffffff; margin-bottom: 10px; margin-top: 5px;">New Talent Image</div>
                <atom:EditableImage id="imgTalentNew" Width="58" Height="78" runat="server" />
                <atom:SelectImageButton runat="server" id="selectImageButton5">Select Image</atom:SelectImageButton>
            </div>
            
            <div style="clear: both;" >&nbsp;</div>
            
            <div>
            <asp:Button runat="server" ID="btnSaveEdits" Text="SAVE EDITS" OnClick="OnClick_btnSaveEdits" CssClass="button" />
            </div>

            <uc1:ImageSelector ID="ImageSelector1" runat="server" />     
            
            <div class="watermarkLibrary">
                <h2>Watermarks</h2>
                <atom:WatermarkDefinitionLibrary ID="WatermarkDefinitionLibrary1" runat="server">
                    <atom:WatermarkDefinition ID="WatermarkDefinition1" runat="server" ImageUrl="../AtomImageEditor/ImageHandler.aspx?ID=eba37a2b-09f8-467e-b4a7-9bc67d149b55"></atom:WatermarkDefinition>
                    <atom:WatermarkDefinition ID="WatermarkDefinition2" runat="server" ImageUrl="../AtomImageEditor/ImageHandler.aspx?ID=77915ba4-10ce-4d7b-a84b-63fa54bb2f71"></atom:WatermarkDefinition>
                </atom:WatermarkDefinitionLibrary> 
            </div> 
        <div style="clear: both;" >&nbsp;</div>
</div>
<div class="cleaner">&nbsp;</div>
</div>
</div>
<div id="footerGlow" >
&nbsp;
</div>
</form>
</body>
</html>