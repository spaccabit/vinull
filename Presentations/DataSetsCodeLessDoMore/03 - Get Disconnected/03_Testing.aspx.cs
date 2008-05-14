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

public partial class _03_Testing : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e) {
        AdventureWorksDS.ProductDataTable dt = e.ReturnValue as AdventureWorksDS.ProductDataTable;
        Metrics.AddExpectedProfit(dt);
    }
}
