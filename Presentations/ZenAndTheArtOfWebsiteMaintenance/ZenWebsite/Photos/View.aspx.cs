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

public partial class Photos_View : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        ((Assets_Layout_Pages_Main)Page.Master).FullScreenMode = true;

    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e) {
        Trace.Write("Photo View", "Query: " + Request.Url.Query);
        e.InputParameters["FileName"] = Request.Url.Query.Trim("?".ToCharArray());
    }
}
