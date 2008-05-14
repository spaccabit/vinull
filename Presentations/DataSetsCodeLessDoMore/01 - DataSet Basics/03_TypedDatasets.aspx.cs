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
using _02___TypedDataSet;
using _02___TypedDataSet.AdventureWorksDSTableAdapters;

public partial class _03_TypedDatasets : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        AdventureWorksDS ds = new AdventureWorksDS();
        ProductTableAdapter taProducts = new ProductTableAdapter();
        ProductModelTableAdapter taModels = new ProductModelTableAdapter();

        taProducts.Fill(ds.Product);
        taModels.Fill(ds.ProductModel);

        ListView1.DataSource = ds.ProductModel;
        ListView1.DataBind();

        

    }
}
