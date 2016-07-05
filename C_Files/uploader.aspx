<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="uploader.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>
<%@ Register Assembly="FlashUpload" Namespace="FlashUpload" TagPrefix="FlashUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Videos For Main Page</title>
    <script type="text/javascript" language="javascript">
        function UploadComplete() {
            //alert("done");
        }
    </script>
</head>
<body class="contentBlock">
    <form id="form1" runat="server">

    <div>
        <asp:LinkButton ID="LinkButton1" runat="server" 
            Visible="true" onclick="LinkButton1_Click1">Return To Backend</asp:LinkButton>
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server" 
            Visible="true" onclick="LinkButton2_Click">Go To CenterFold Main Page</asp:LinkButton>[Note you will need to close your browser (let the browser cache clear) and reopen to see the effects of the upload}
        <br />
    <FlashUpload:FlashUpload ID="flashUpload" runat="server" 
             UploadPage="Upload.cs" OnUploadComplete="UploadComplete()" 
            FileTypeDescription="Images" 
            FileTypes="*.*" 
            UploadFileSizeLimit="99999999999999900000" TotalUploadSizeLimit="99999999999999999992097152" />
    </div>
    </form>
</body>
</html>
