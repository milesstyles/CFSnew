<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDocumentList.aspx.cs" Inherits="backend_EditDocumentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
    <title>Edit Document List</title>
</head>
<body>
    <form id="form1" runat="server">
     Add New Busines URL:<br />
        <asp:label runat="server" ID="Label1"></asp:label>
        <br /><asp:FileUpload ID="FileUpload1" runat="server" />
        Display Name:  <asp:TextBox ID="Textname" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="Add" runat="server" Text="Add URL Hyperlink" 
        onclick="Add_Click" /><br /><br />
        Current Document Links: <br /><br />

   <asp:PlaceHolder id="Place" runat="server"/><br /><br /><br />
    <asp:Button ID="Delete" runat="server" Text="Delete Checked Hyperlinks" 
        onclick="Delete_Click" />
    <div>
    
    </div>
    </form>
</body>
</html>
