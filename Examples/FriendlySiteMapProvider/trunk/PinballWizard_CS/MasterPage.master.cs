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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Page.Title = SiteMap.CurrentNode.Title;
		SiteMapNode smnCursor = SiteMap.CurrentNode.ParentNode;
		while (smnCursor != null) {
			Page.Title = smnCursor.Title + ": " + Page.Title;
			smnCursor = smnCursor.ParentNode;
		}
	}
}
