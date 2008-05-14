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

public partial class _05_Calculations : System.Web.UI.Page {

    protected Decimal Products = 0M;
    protected Decimal Retail = 0M;

    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e) {
        AdventureWorksDS.ProductModelDataTable dtModels = e.ReturnValue as AdventureWorksDS.ProductModelDataTable;
    
        dtModels.Columns.Add("ProductCount", typeof(Decimal), "Count(Child(FK_Product_ProductModel_ProductModelID).ProductID)");
        dtModels.Columns.Add("ProductRetail", typeof(Decimal), "Sum(Child(FK_Product_ProductModel_ProductModelID).ListPrice)");


        AdventureWorksDS ds = dtModels.DataSet as AdventureWorksDS;
        Products = Convert.ToDecimal(ds.Product.Compute("Count(ProductID)", String.Empty));
        Retail = Convert.ToDecimal(ds.Product.Compute("Sum(ListPrice)", String.Empty));
    }
}
