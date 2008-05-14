using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Store_Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void gvCategories_SelectedIndexChanged(object sender, EventArgs e) {
        Trace.Write("Start");
        mvProducts.SetActiveView(vProduct);
        Trace.Write("Done");
    }
}
