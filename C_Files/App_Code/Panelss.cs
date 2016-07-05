using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Panel
/// </summary>
/// 
[System.Runtime.Serialization.DataContract]
public class Panelss
{
    [System.Runtime.Serialization.DataMember]
    public string IMG { get; set; }
    [System.Runtime.Serialization.DataMember]
    public string URL { get; set; }
    [System.Runtime.Serialization.DataMember]
    public string TEXT { get; set; }
    [System.Runtime.Serialization.DataMember]
    public string PANEL { get; set; }
    [System.Runtime.Serialization.DataMember]
    public string LINKS { get; set; }
    [System.Runtime.Serialization.DataMember]
    public string ORDER { get; set; }
}
[System.Runtime.Serialization.DataContract]
public class RootObject
{
    [System.Runtime.Serialization.DataMember]
    public List<Panelss> panelss { get; set; }
}