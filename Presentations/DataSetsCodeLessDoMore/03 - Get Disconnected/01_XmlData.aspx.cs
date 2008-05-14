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

public partial class _01_XmlData : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void DBToXML_Click(object sender, EventArgs e) {
        AdventureWorksDS ds = new AdventureWorksDS();
        ProductModelTableAdapter ta = new ProductModelTableAdapter();

        ta.Fill(ds.ProductModel);
        ds.WriteXml(MapPath("~/App_Data/AdventureWorksDS.xml"));
    }

    protected void XMLToDB_Click(object sender, EventArgs e) {
        AdventureWorksDS ds = new AdventureWorksDS();
        AdventureWorksDS dsDB = new AdventureWorksDS();
        ProductModelTableAdapter ta = new ProductModelTableAdapter();

        ds.ReadXml(MapPath("~/App_Data/AdventureWorksDS.xml"));
        ta.Fill(dsDB.ProductModel);

        dsDB.Merge(ds, false);
        ta.Update(dsDB.ProductModel);

        GridView1.DataBind();
    }
}
