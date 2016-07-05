using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class _SubPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script language='javascript'>");
        sb.Append(@"document.onload=alert();");
         
        sb.Append(@"");
        sb.Append(@"</script>");

       
     //   Page.ClientScript.RegisterStartupScript(this.GetType(), "JSScriptBlock4",
     //     sb.ToString());
    }
}
