<%@ WebService Language="C#" Class="ProductService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using AdventureWorks.InventoryTableAdapters;
using System.Web.Script.Services;
using System.Xml;
using System.Data;
using System.IO;

[ScriptService]
[WebService(Namespace = "http://zendemo/products")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ProductService : System.Web.Services.WebService {
    
    private bool ValidUser(String apikey) {
        if (HttpContext.Current.User.Identity.IsAuthenticated ||
            apikey.Equals("letmein"))
            return true;
        return false;
    }

    [WebMethod]
    public Zen.Data.ProductList GetProducts(String apiKey) {
        if (!ValidUser(apiKey))
            return null;
        
        ProductTableAdapter taProd = new ProductTableAdapter();
        return taProd.GetAllZen();
    }

    public enum Table { Models, Categories }
    
    [WebMethod]
    public XmlDocument GetLookupTable(String apiKey, Table table) {
        if (!ValidUser(apiKey)) {
            XmlDocument error = new XmlDocument();
            error.LoadXml("<Error/>");
            return error;
        };

        DataSet ds = new DataSet(table.ToString());
        if (table == Table.Categories) {
            ProductCategoryTableAdapter taCat = new ProductCategoryTableAdapter();
            ds.Tables.Add(taCat.GetAll());            
        }
        else if (table == Table.Models) {
            ProductModelTableAdapter taModel = new ProductModelTableAdapter();
            ds.Tables.Add(taModel.GetAll());
        }
        ds.Tables[0].Columns.Remove("rowguid");
        ds.Tables[0].Columns.Remove("ModifiedDate");
      
        StringWriter data = new StringWriter();
        ds.WriteXml(data, XmlWriteMode.IgnoreSchema);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(data.ToString());

        return xml;
    }
    
    
}

