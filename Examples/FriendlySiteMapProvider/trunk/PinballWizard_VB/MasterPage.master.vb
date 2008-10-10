
Partial Class MasterPage
	Inherits System.Web.UI.MasterPage

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		Page.Title = SiteMap.CurrentNode.Title
		Dim smnCursor As SiteMapNode = SiteMap.CurrentNode.ParentNode
		While Not (smnCursor Is Nothing)
			Page.Title = smnCursor.Title + ": " + Page.Title
			smnCursor = smnCursor.ParentNode
		End While

	End Sub	'Page_Load

End Class
