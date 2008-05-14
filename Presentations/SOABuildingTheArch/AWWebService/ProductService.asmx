<%@ WebService Language="C#" Class="ProductService" %>

using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Xml;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for ProductService
/// </summary>
[WebService(Namespace = "http://AWService.org/Product")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class ProductService : ApiKeyService {

    [WebMethod]
    public string HelloWorld(String apiKey) {
        String user = GetUsername(apiKey, HttpContext.Current);

        if (user == null)
            return null;
        
        return "Hello World: " + user;
    }

    [WebMethod]
    public AWServiceCore.Types.ProductCollection GetProducts(String apiKey) {

        if (GetUsername(apiKey, HttpContext.Current) == null)
            return null;

        return AWServiceCore.ProductService.GetProducts();       
    }

    [WebMethod]
    public AWServiceCore.Types.ProductCollection GetProductsByModel(String apiKey, Int32 ModelID) {

        if (GetUsername(apiKey, HttpContext.Current) == null)
            return null;

        return AWServiceCore.ProductService.GetProductsByModel(ModelID);
    }

    [WebMethod]
    public XmlDocument DirectQuery(String apiKey, String Query) {
        if (GetUsername(apiKey, HttpContext.Current) == null)
            return null;

        DataSet ds = AWServiceCore.ProductService.DirectQuery(Query);
        StringWriter data = new StringWriter();
        ds.WriteXml(data, XmlWriteMode.IgnoreSchema);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(data.ToString());

        return xml;
    }
}

