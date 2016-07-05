using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class backend_Email2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~//media//") + "mycsvfile.csv";

     
        HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
        String Header = "Attachment; Filename=" + "mycsvfile.csv";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
        System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("~//media//") + "mycsvfile.csv");
        HttpContext.Current.Response.WriteFile(Dfile.FullName);
        HttpContext.Current.Response.End();
    
    }
}