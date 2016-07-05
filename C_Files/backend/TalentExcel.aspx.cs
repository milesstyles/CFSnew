using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class backend_TalentExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["pass"] != null)
        {
            
            string path = Request["pass"].ToString();

            if (path == "")
            {
                Response.Write("empty pass");
                return;
            }

            string filePath = Server.MapPath("~//media//") + path;


            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + path;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("~//media//") + path);
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }
        else
        {
            Response.Write("empty pass");
        }
    }
}