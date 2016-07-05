<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="Edit_B_Links.aspx.cs" Inherits="backend_Edit_B_Links" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
   <div class="contentBlock" >
       <asp:Button ID="btn_add" runat="server" Text="Add New Business Link" CssClass="buttonA"
           onclick="btn_add_Click" /><br /><br />
     <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" EditRowStyle-Height="30"
           
            >
             
             
        <Columns>
         <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonSelectRow" runat="server" 
                                Text="Edit" 
                                OnClick="GridViewUserRow_Click"
                     
                                 /><br /><br />
                                 <asp:LinkButton ID="LinkButton1" runat="server" 
                                Text="Delete" 
                                OnClick="GridViewUserRowDelete_Click"
                     
                                 />
                </ItemTemplate>
            </asp:TemplateField>
           
            
             <asp:TemplateField HeaderText="Text" SortExpression="SerialNumber">
                <ItemTemplate>
                <asp:Label ID="LabelDeviceIDHidden" Visible="false"  runat="server" Text='<%#Eval("id") %>'></asp:Label>
                
                 <asp:Label ID="SerialNumberTooltip"  runat="server" ToolTip='<%#Eval("url") %>'>
                    <div style=" overflow: hidden; height:90px; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("text")%>
                         
      
                    </div>
                      </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
        
            <asp:TemplateField HeaderText="Image" SortExpression="img">
                <ItemTemplate>
                    <asp:Label ID="StatusTooltip"  runat="server" ToolTip='<%# string.Format("images/panel/{0}", Eval("img"))%>'>
             
                    <div style=" overflow: hidden; margin-top:-5px; height:90px; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                      <a href="<%# Eval("url")%>" ><img alt="no image" src="<%# string.Format("images/panel/{0}", Eval("img"))%>" width="90px" height="90px" /></a>
                         
      
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Business Link" SortExpression="links">
                <ItemTemplate>
                    <asp:Label ID="panelTooltip"  runat="server" ToolTip='<%#Eval("links") %>'>
             
                    <div style=" overflow: hidden; height:90px; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("links")%>
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            

            
            <asp:TemplateField HeaderText="Panel View" SortExpression="panel">
                <ItemTemplate>
                    <asp:Label ID="businessTooltip"  runat="server" ToolTip='<%#Eval("panel") %>'>
             
                    <div style=" overflow: hidden; height:90px; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("panel")%>
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ID" SortExpression="id>
                <ItemTemplate>
                    <asp:Label ID="idTooltip"  runat="server" ToolTip='<%#Eval("id") %>'>
             
                    <div style=" overflow: hidden; height:90px; white-space: nowrap; text-align: center; font-family:Calibri;  text-overflow: ellipsis">
                        <%# Eval("id")%>
                    </div>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
        
      
         
    
  
               </Columns>
        
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#7AC5CD" Font-Bold="True" ForeColor="AliceBlue" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" ForeColor="Black" Font-Size="14px" Font-Bold="true" />
        <AlternatingRowStyle BackColor="#7AC5CD" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        
         </asp:GridView>
    </div>
    jjjjjj
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="deleteConfirm" Runat="Server">
</asp:Content>

