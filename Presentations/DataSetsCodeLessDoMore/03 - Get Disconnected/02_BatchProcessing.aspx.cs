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

public partial class _02_BatchProcessing : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void bHike_Click(object sender, EventArgs e) {
        ProductTableAdapter ta = new ProductTableAdapter();
        AdventureWorksDS.ProductDataTable dt = ta.GetData();
        foreach (AdventureWorksDS.ProductRow row in dt.Select("ListPrice >= 2000")) {
            row.ListPrice += 500M;
        }
        ta.Update(dt);

        GridView1.DataBind();
    }
    protected void bDrop_Click(object sender, EventArgs e) {
        ProductTableAdapter ta = new ProductTableAdapter();
        AdventureWorksDS.ProductDataTable dt = ta.GetData();
        foreach (AdventureWorksDS.ProductRow row in dt.Select("ListPrice >= 2000")) {
            row.ListPrice -= 500M;
        }
        ta.Update(dt);

        GridView1.DataBind();
    }
}
