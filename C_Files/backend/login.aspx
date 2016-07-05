<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="backend_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site | Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/backend.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" language="javascript" src="../js/offspring-1.0.js"></script>
</head>
<body>
<form id="form1" runat="server">

<div id="container">
    <div id="content">
        <div id="header">
            <h1 runat="server" visible="false"><a href="login.aspx"><asp:Label ID="LogoName" runat="server" Text=""></asp:Label>
      Admin</a></h1>
        </div>
        <div id="fullColumn">
            <p>Use of this administration system without express permission is prohibited. </p>
            <asp:Label runat="server" ID="lblErrorMsg" style="color: Red;" />
            <table width="300" border="0" cellspacing="0" cellpadding="5">
                <tr>
                    <td width="65">User Name:</td>
                    <td width="215"><asp:TextBox runat="server" ID="tBoxUserName" size="30" MaxLength="30" CssClass="textfield" AutoCompleteType="None" /></td>
                </tr>
                <tr>
                    <td>Password: </td>
                    <td><asp:TextBox runat="server" ID="tBoxPassword" TextMode="Password" size="15" MaxLength="30" CssClass="textfield" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="OnClick_btnLogin" CssClass="button" /></td>
                </tr>
            </table>
            <p>&nbsp;</p>
          </div>
     </div>
</div>
<img src="../images/mainBGGlowBottom.png" alt="" width="1050" height="51" />
</form>
</body>
</html>
