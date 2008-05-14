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

public partial class _01_Basic : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

        SqlConnection conn =
            new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksLT"].ConnectionString);

        SqlCommand com = conn.CreateCommand();
        com.CommandText = @"SELECT ProductID, Name, ProductNumber, StandardCost, Size, Weight
                                       ProductCategoryID, ProductModelID  
                                  FROM SalesLT.Product";

        conn.Open();
        SqlDataReader reader = com.ExecuteReader();
        DataTable dt = new DataTable("Product");
        dt.Load(reader);
        conn.Close();

        reader.Dispose();
        com.Dispose();
        conn.Dispose();

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
}
