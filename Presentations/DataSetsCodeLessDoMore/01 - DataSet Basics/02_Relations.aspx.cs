using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class _02_Relations : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

        SqlConnection conn =
            new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksLT"].ConnectionString);

        SqlCommand comProducts = conn.CreateCommand();
        SqlCommand comModels = conn.CreateCommand();
        comProducts.CommandText = @"SELECT ProductID, Name, ProductNumber, StandardCost, Size, Weight
                                       ProductCategoryID, ProductModelID  
                                  FROM SalesLT.Product";
        comModels.CommandText = @"SELECT ProductModelID, Name, CatalogDescription
                                  FROM SalesLT.ProductModel";

        DataTable dtProducts = new DataTable("Product");
        DataTable dtModels = new DataTable("Model");

        conn.Open();
        SqlDataReader rProducts = comProducts.ExecuteReader();
        dtProducts.Load(rProducts);
        SqlDataReader rModels = comModels.ExecuteReader();
        dtModels.Load(rModels);
        conn.Close();

        rProducts.Dispose();
        rModels.Dispose();
        comProducts.Dispose();
        comModels.Dispose();
        conn.Dispose();

        DataSet ds = new DataSet("AdventureWorks");
        ds.Tables.Add(dtProducts);
        ds.Tables.Add(dtModels);

        ds.Relations.Add("model_product",
            dtModels.Columns["ProductModelID"],
            dtProducts.Columns["ProductModelID"]);

        ListView1.DataSource = dtModels;
        ListView1.DataBind();
    }
}
