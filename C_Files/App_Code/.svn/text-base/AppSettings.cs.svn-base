using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

public class AppSettings
{
    public static int GetIntValue(string key)
    {
        int value = 0;
        if (int.TryParse(ConfigurationSettings.AppSettings[key], out value))
        {
            return value;
        }
        else
        {
            throw new Exception("couldn't covert key: " + key + " to an integer");
        }
    }

    public static string GetStringValue(string key)
    {
        string value = ConfigurationSettings.AppSettings[key];
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("couldn't covert key: " + key + " to a string");
        }
        else
        {
            return value;
        }
    }

    public static DateTime GetDateTimeValue(string key)
    {
        const string DateFormat = "MM/dd/yyyy HH:mm:ss"; //01/20/10 09:00

        try
        {
            DateTime value = DateTime.ParseExact(ConfigurationSettings.AppSettings[key], DateFormat, CultureInfo.InvariantCulture);
            return value;
        }
        catch
        {
            throw new Exception("couldn't convert key: " + key + " to a date-time object");
        }
    }

}
