using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// This class is like Business layer facade which will fetch data from Data Access Layer.
/// For simpliciy this class generate dummy data.
/// </summary>
public class ProductFacade
{
    #region Constructor
    public ProductFacade()
    {
    }
    #endregion

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of products</returns>
    public static List<Product> GetAllProducts()
    {
        List<Product> products = new List<Product>();
        Product p = default(Product);
        for (int i = 1; i <= 10; i++)
        {
            p = new Product();
            p.ProductCode = string.Format("p_{0}", i);
            p.ProductID = i;
            p.ProductName = string.Format("Product {0}", i);
            products.Add(p);
        }
        return products;
    }
    /// <summary>
    /// Get all Proudcts starting with prefix provided
    /// </summary>
    /// <param name="prefix">product name prefix</param>
    /// <returns></returns>
    public static List<Product> GetProducts(string prefix)
    {
        List<Product> products = new List<Product>();
        Product p = default(Product);
        for (int i = 1; i <= 10; i++)
        {
            p = new Product();
            p.ProductCode = string.Format("p_{0}", i);
            p.ProductID = i;
            p.ProductName = string.Format("{0} Product {1}", prefix, i);
            products.Add(p);
        }
        return products;
    }

}
