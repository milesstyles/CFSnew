<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageSelector.ascx.cs" Inherits="AtomImageEditor_ImageSelector" %>

<%--image list--%>

<div runat="server" id="ImagesListContainer" style="display:none;width:450px;">
    
    <asp:ListBox runat="server" ID="ImagesList"></asp:ListBox>
    <a href="#" class="cancelSelect">Cancel</a>
    
    <hr />

    <%--image upload--%>
    <span>Upload new image:</span>          
    <iframe frameborder="0" style="height:50px;width:400px;" src="../AtomImageEditor/UploadImage.aspx?id=<%= talentId.ToString() %>"></iframe>
</div>

<script>
    Sys.Application.add_init(initImageSelector);

    var ImageSelector;
    function initImageSelector() {
        ImageSelector = $create(AtomImageEditor.ImageSelector, {}, {}, {}, $get("<%= ImagesListContainer.ClientID %>"));
        ImageSelector.set_imagesList($get("<%= ImagesList.ClientID %>"));
    }

    // fires after image has been uploaded
    function onImageUploaded(newImageID) {
        ImageSelector.onImageUploaded(newImageID);
    }
</script>