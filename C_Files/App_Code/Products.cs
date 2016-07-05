using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;

[System.Runtime.Serialization.DataContractAttribute]
public class Product
{
    #region Constructor
    public Product()
    {
    }
    #endregion

    #region Attributes
    private int _productId;
    private string _productCode;
    private string _productName;
    #endregion

    #region ID
    [DataMemberAttribute]
    public int ProductID
    {
        get { return _productId; }
        set { _productId = value; }
    }
    #endregion

    #region Code
    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }
    #endregion

    #region Name
    [DataMemberAttribute]
    public string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }
    #endregion
}
