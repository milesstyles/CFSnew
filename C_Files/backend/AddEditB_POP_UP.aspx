<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditB_POP_UP.aspx.cs" Inherits="backend_EditB_POP_UP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     Add New Busines Panel:<br /><br />

     <table>
     <tr><td>URL</td><td>
     <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="http://" Text="http" Selected="True"></asp:ListItem>
        <asp:ListItem Value="https://" Text="https"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="URL" runat="server"></asp:TextBox>
     </td></tr>
     <tr>
     <td>
     
        Display Name:  <br />
        
     </td>
     <td><asp:TextBox ID="Textname" runat="server"></asp:TextBox></td>
     </tr>

     <tr>
     <td>
     Image Location :
     </td>
     <td>
      <asp:FileUpload ID="FileUpload1" Width="450px" runat="server" />
         <asp:Button ID="btn_clear" runat="server" 
          Text="Clear" onclick="btn_clear_Click" />

     </td>
     </tr>
     <tr>
     <td>
     Current Image :
     </td>
     <td>
     
      <asp:Image ID="Image1" Height="90px" Width="90px" runat="server" />
     </td>
     </tr>
     <tr>
     <td> Show Panel :</td>

     <td> <asp:DropDownList ID="DropDownList2" runat="server">
        <asp:ListItem Value="TRUE" Text="TRUE" Selected="True"></asp:ListItem>
        <asp:ListItem Value="FALSE" Text="FALSE"></asp:ListItem>
        </asp:DropDownList> </td>
     </tr>
     <tr>
     <td> Show In Business Links :</td>

     <td> <asp:DropDownList ID="DropDownList3" runat="server">
        <asp:ListItem Value="TRUE" Text="TRUE" Selected="True"></asp:ListItem>
        <asp:ListItem Value="FALSE" Text="FALSE"></asp:ListItem>
        </asp:DropDownList> </td>
     </tr>
     </table>
        
         
       
       <br />
        <asp:Button ID="Add" runat="server" Text="Save Panel" 
        onclick="Add_Click" /><br /><br />
        
    </div>
    </form>
</body>
</html>
