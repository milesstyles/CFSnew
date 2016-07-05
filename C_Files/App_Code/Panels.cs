using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Panels
/// </summary>
/// 

[JsonObject(MemberSerialization.OptIn)]
public class Panels
{
    public Panels()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Attributes
    private string _IMG;
    private string _URL;
    private string _PANEL;
    private string _LINKS;
    private string _ORDER;
    #endregion

    #region IMG
    [DataMemberAttribute]
    public string IMG
    {
        get { return _IMG; }
        set { _IMG = value; }
    }
    #endregion

    #region URL
    public string URL
    {
        get { return _URL; }
        set { _URL = value; }
    }
    #endregion

    #region PANEL
    [DataMemberAttribute]
    public string PANEL
    {
        get { return _PANEL; }
        set { _PANEL = value; }
    }
    #endregion
    #region LINKS
    [DataMemberAttribute]
    public string LINKS
    {
        get { return _LINKS; }
        set { _LINKS = value; }
    }
    #endregion
    #region ORDER
    [DataMemberAttribute]
    public string ORDER
    {
        get { return _ORDER; }
        set { _ORDER = value; }
    }
    #endregion
}