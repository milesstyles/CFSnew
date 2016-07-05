<%@ Page Title="Centerfold Strips | View Pending Jobs" Language="C#" MasterPageFile="~/backend/MasterPage.master" AutoEventWireup="true" CodeFile="view_pending_jobs.aspx.cs" Inherits="backend_view_pending_jobs" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

<div id="divDeleteConfirm" runat="server" visible="false" >
    <div id="deleteConfirm">
        <label>Confirm Delete:</label>
        <asp:HiddenField runat="server" ID="hiddenDeleteId" Value="" />
        <asp:Button runat="server" ID="btnConfirmYes" Text="Yes" OnClick="OnClick_btnConfirmYes" CssClass="button" />
        <asp:Button runat="server" ID="btnConfirmNo" Text="No" OnClick="OnClick_btnConfirmNo" CssClass="button" />
    </div>
</div>
<script type="text/javascript">
    myEmails = "<%=this.preferences%>";

    function copy() {
       // myEmails = myEmails.replace(/ ;/g, '');
        document.getElementById("copyTextarea").innerText = myEmails;

        var copyStr = document.getElementById("copyTextarea").innerText
        copyHtmlArea.innerHTML = copyStr;
        copyHtmlFrame.document.body.innerHTML = "";
        copyHtmlFrame.document.write(copyStr);

        try {
            var r = copyHtmlFrame.document.body.createTextRange();
            r.select();
            r.execCommand('copy');
            // document.getElementById("msgArea").innerText = "The list has been copied onto your clipboard.";
        }
        catch (er) {
            alert("Pepl ECT is unable to place the text on your clipboard.  Please select the entire list, right-click and select Copy to place the data on your clipboard.");
        }
    }
    function copy2() {
        var textRange = document.body.createTextRange();
        textRange.moveToElementText(document.getElementById("mycopytable"));
        try {
            textRange.execCommand("Copy");
        }
        catch (ee) {
            copy2();
        }

    }
    function copy3() {
        try {
            var range = document.selection.createRange();
            range.selectNode(document.getElementById("mycopytable"));
            window.getSelection().removeAllRanges();
            window.getSelection().addRange(range);
            range.execCommand("Copy");
        }
        catch (e) {
            alert("Sorry, your browser does not support this feature. Please right click the highlighted text and select 'copy'.")
        }
    }

</script>
<h3>View Pending Jobs</h3>
<div id="fullColumn">
    <!-- <input type="button" id="copy-button2" onclick="javascript:void(0);" class="button" style="position:absolute;margin-top:4px;margin-left:450px;" value="Copy Emails To Clipboard"/>-->
   
    <table width="100%" cellspacing="0" style="margin-bottom:20px;">
        <tr>
      
            <td width="15%"><asp:Button runat="server" ID="btnAddPendJob" Text="ADD A PENDING JOB" CssClass="button" OnClick="OnClick_btnAddPendJob" /></td>
            <td width="12%"><asp:Button runat="server" ID="btnPrint" Text="PRINT THIS LIST" OnClientClick="window.print()" CssClass="button" /></td>
              <td></td>
            <!--<td width="12%"><asp:Button runat="server" ID="btn_Copy" 
                    Text="Copy EMAIL LIST to ClipBoard" 
                    CssClass="button" OnClientClick="copy()" /></td>-->
            
            <td width="40%"><input type="button" class="button" value="VIEW CONTACT FORM INQUIRIES" onclick="window.location = 'inquiries.aspx'" /></td>
             
            <td width="80%" style="text-align: right;" ><div style="font-size:1.1em; float:right; margin-bottom:6px;"><span style="display:block; width:15px; height:15px; background-color:#aac3b1; float:left; margin:-1px 6px 0px 0px;">&nbsp;</span>Event in current week</div></td>
            <td width="12%"><asp:Button runat="server" ID="view_email" Text="EMAIL List" OnClick="OnClick_btnShowEmail"  CssClass="button" /></td>
           
       </tr>
    </table>
    
    <asp:EntityDataSource runat="server" ID="EntityDataSrc"  ConnectionString="name=CfsEntity" DefaultContainerName="CfsEntity" 
            EntitySetName="Pending" EnableDelete="true" OrderBy="it.EventDate ASC" />

    <asp:GridView runat="server" ID="grdPendingJobs" AllowSorting="true" DataKeyNames="PendId" DataSourceID="EntityDataSrc" AutoGenerateColumns="false" GridLines="Horizontal" BorderColor="#383838" >
        <Columns>
            <asp:BoundField HeaderText="Event Date" DataField="EventDate" SortExpression="EventDate" DataFormatString="{0:dddd M/d/yyyy}" />
            <asp:BoundField HeaderText="Customer Name" DataField="ClientName" SortExpression="ClientName" />
            <asp:BoundField HeaderText="Location" DataField="CityStateZip" SortExpression="CityStateZip" />
            <asp:BoundField HeaderText="Number" DataField="ContactNumber" SortExpression="ContactNumber" DataFormatString="<a href='tel:{0}'>{0}</a>" HtmlEncodeFormatString="false"   />
            <asp:BoundField HeaderText="EmailAddress" DataField="EmailAddress" SortExpression="EmailAddress" DataFormatString="<a href='mailto:{0}'>{0}</a>" HtmlEncodeFormatString="false" ItemStyle-Width="100px" />
            <asp:BoundField HeaderText="Notes" DataField="Notes" ItemStyle-Width="200px" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" Text="VIEW" CommandArgument='<%# Eval("PendId") %>' OnClick="OnClick_btnEdit" CssClass="button" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDelete" Text="DELETE" CssClass="button" CommandArgument='<%# Eval("PendId") %>' OnClick="OnClick_btnDelete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView runat="server" Visible="false" ID="emailList" AllowSorting="true" DataKeyNames="PendId" DataSourceID="EntityDataSrc" AutoGenerateColumns="false" BorderColor="#383838" >
        <Columns>
           <asp:BoundField HeaderText="EmailAddress" DataField="EmailAddress" SortExpression="EmailAddress" DataFormatString="<a href='mailto:{0}'>{0}</a>" HtmlEncodeFormatString="false" ItemStyle-Width="100px" />
          
        </Columns>
    </asp:GridView>
    
</div><textarea cols="89" rows="10" id="copyTextarea" style="margin-top:450px; display:none; position:absolute; overflow:auto;"></textarea>
        <div style="margin-top:200px; position:absolute; overflow:auto;" id="myElist"></div>
        <div id="mycopytable" style="margin-top:450px; position:absolute; overflow:auto; width:90%;"></div>
        <div id="copyHtmlArea" style="width:90%; display:none;"></div>
	    <iframe id="copyHtmlFrame" style="position:absolute; top:0px; left:0px; width:1px; height:1px;"></iframe>
    <div>
  <!--  <input id="txtCopyText" type="text" value="www.dotnetbull.com"/>
    <a href="javascript:void(0);" id="copy-button" class="">Copy To ClipBoard</a>-->
      
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js" type="text/javascript"></script>

    
   <!-- <script src="http://www.steamdev.com/zclip/js/jquery.zclip.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $("#copy-button").zclip({
            path: "http://www.steamdev.com/zclip/js/ZeroClipboard.swf",
            copy: function () { return $('#txtCopyText').val(); },
            beforeCopy: function () { },
            afterCopy: function () {
                alert('Copy To Clipboard : \n' + $("#txtCopyText").val());
            }
        });
        $("#copy-button2").zclip({
            path: "http://www.steamdev.com/zclip/js/ZeroClipboard.swf",
            copy: function () { return myEmails },
            beforeCopy: function () { },
            afterCopy: function () {
                alert('Copy To Clipboard : success\n');
            }
        });

</script>-->
    <!--<div class="zclip" id="zclip-ZeroClipboardMovie_1" style="position: absolute; left: 165px; top: 1117px; width: 108px; height: 16px; z-index: 99;"><embed id="ZeroClipboardMovie_1" src="http://www.steamdev.com/zclip/js/ZeroClipboard.swf" loop="false" menu="false" quality="best" bgcolor="#ffffff" width="108" height="16" name="ZeroClipboardMovie_1" align="middle" allowscriptaccess="always" allowfullscreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="id=1&amp;width=108&amp;height=16" wmode="transparent"></div>-->
    </div>
</asp:Content>

