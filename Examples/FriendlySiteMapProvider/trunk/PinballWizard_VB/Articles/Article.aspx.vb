
Partial Class Articles_Article
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim taArticle As New ContentTableAdapters.ArticleTableAdapter()
		Dim daArticle As Content.ArticleDataTable = taArticle.ArticleByID(Convert.ToInt32(SiteMap.CurrentNode.Key))

		lBody.Text = daArticle(0).body
		lDate.Text = daArticle(0).post_date.ToString("D")

		odsComments.SelectParameters("article_id").DefaultValue = SiteMap.CurrentNode.Key
	End Sub	'Page_Load

	Protected Sub dvAddComment_ItemInserted(ByVal sender As Object, ByVal e As DetailsViewInsertedEventArgs)
		rComments.DataBind()
	End Sub	'dvAddComment_ItemInserted

	Protected Sub dvAddComment_ItemInserting(ByVal sender As Object, ByVal e As DetailsViewInsertEventArgs)
		e.Values("article_id") = SiteMap.CurrentNode.Key
	End Sub	'dvAddComment_ItemInserting

End Class
