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

public partial class Articles_Article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ContentTableAdapters.ArticleTableAdapter taArticle = new ContentTableAdapters.ArticleTableAdapter();
		Content.ArticleDataTable daArticle = taArticle.ArticleByID(Convert.ToInt32(SiteMap.CurrentNode.Key));

		lBody.Text = daArticle[0].body;
		lDate.Text = daArticle[0].post_date.ToString("D");

		odsComments.SelectParameters["article_id"].DefaultValue = SiteMap.CurrentNode.Key;
    }
	protected void dvAddComment_ItemInserted(object sender, DetailsViewInsertedEventArgs e) {
		rComments.DataBind();
	}
	protected void dvAddComment_ItemInserting(object sender, DetailsViewInsertEventArgs e) {
		e.Values["article_id"] = SiteMap.CurrentNode.Key;
	}
}