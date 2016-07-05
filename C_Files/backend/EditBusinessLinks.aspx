<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBusinessLinks.aspx.cs" Inherits="backend_EditBusinessLinks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Edit Business links</title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <a href="default.aspx">Return to Main Page</a>

    Add New Busines URL:<br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="http://" Text="http" Selected="True"></asp:ListItem>
        <asp:ListItem Value="https://" Text="https"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="URL" runat="server"></asp:TextBox><br />
        Display Name:  <asp:TextBox ID="Textname" runat="server"></asp:TextBox><br />
        <asp:Button ID="Add" runat="server" Text="Add URL Hyperlink" 
        onclick="Add_Click" /><br /><br />
        Current Business Links: <br /><br />

   <asp:PlaceHolder  Visible="false" id="Place" runat="server"/>
    
     <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" 
           
            >
             
             
        <Columns>
         <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonSelectRow" runat="server" 
                                Text="Select" 
                                OnClick="GridViewUserRow_Click"
                     
                                 />
                </ItemTemplate>
            </asp:TemplateField>
           
            
             <asp:TemplateField HeaderText="Text" SortExpression="SerialNumber">
                <ItemTemplate>
                <asp:Label ID="LabelDeviceIDHidden" Visible="false"  runat="server" Text='<%#Eval("text") %>'></asp:Label>
                
                 <asp:Label ID="SerialNumberTooltip"  runat="server" ToolTip='<%#Eval("url") %>'>
                    <div style=" overflow: hidden; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("text")%>
                         
      
                    </div>
                      </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
        
            <asp:TemplateField HeaderText="Image" SortExpression="img">
                <ItemTemplate>
                    <asp:Label ID="StatusTooltip"  runat="server" ToolTip='<%#Eval("img") %>'>
             
                    <div style=" overflow: hidden; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                      <a href="<%# Eval("url")%>" ><img alt="no image" src="<%# Eval("img")%>" width="90px" height="90px" /></a>
                         
      
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           

            
            <asp:TemplateField HeaderText="Panel View" SortExpression="panel">
                <ItemTemplate>
                    <asp:Label ID="panelTooltip"  runat="server" ToolTip='<%#Eval("panel") %>'>
             
                    <div style=" overflow: hidden; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("panel")%>
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
        
      
         
    
  
               </Columns>
        
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#7AC5CD" Font-Bold="True" ForeColor="AliceBlue" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <AlternatingRowStyle BackColor="#7AC5CD" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        
         </asp:GridView>

    </form>
</body>
</html>
