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

public partial class Assets_Layout_Pages_Main : System.Web.UI.MasterPage {

    public Boolean FullScreenMode {
        get {
            return ViewState["FullScreenMode"] == null ?
                   false : (Boolean)ViewState["FullScreenMode"];
        }
        set { 
            ViewState["FullScreenMode"] = value;
            lSideBar.Visible = !value;
            pPage.CssClass = value ? "page_full" : "page";
        }
    }


    protected void Page_Load(object sender, EventArgs e) {

        Page.Title = SiteMap.CurrentNode.Title;
        SiteMapNode parent = SiteMap.CurrentNode.ParentNode;
        while (parent != null) {
            Page.Title = parent.Title + ": " + Page.Title;
            parent = parent.ParentNode;
        }
    }
}
