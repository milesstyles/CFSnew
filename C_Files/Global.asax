<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Application_BeginRequest(object sender, EventArgs e)
    {        
        string uriFragment = Request.AppRelativeCurrentExecutionFilePath;
        
        //only try to do the rewrite if it's a path that could possibly work (eliminate all explicit files)
        if (uriFragment.IndexOf(".") == -1)
        {
            string folder = uriFragment.Split('/')[1];
            folder = folder.ToLower().Replace("-", "").Replace("strippers", "");

            foreach (string city in Enum.GetNames(typeof(Cities)))
            {
                if (city.Equals(folder, StringComparison.CurrentCultureIgnoreCase))
                {
                    Context.RewritePath(string.Format("{0}/Cities/Default.aspx?city={1}", Context.Request.ApplicationPath, city));
                    return;
                }
            }
        }
    } 
       
</script>
