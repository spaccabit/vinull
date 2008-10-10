Imports Microsoft.VisualBasic


 _
Public Class PinballSiteMapProvider
	Inherits SiteMapProvider

	Private _smnRoot As SiteMapNode
	Private _smncCategory As SiteMapNodeCollection
	Private _dArticle As Hashtable
	Private lockobj As New Object()


	Public Sub BuildNodes()
		SyncLock lockobj
			_smnRoot = New SiteMapNode(Me, "PinballRoot", "~/Default.aspx", "Pinball Wizard")
			_smncCategory = New SiteMapNodeCollection()
			_dArticle = New Hashtable

			Dim taArticle As New ContentTableAdapters.ArticleTableAdapter()
			Dim dtArticle As Content.ArticleDataTable = taArticle.AllArticles()

			Dim row As Content.ArticleRow
			For Each row In dtArticle.Rows
				Dim smnNew As New SiteMapNode(Me, row.article_id.ToString(), MakeURL(row.title, row.category, row.post_date), row.title)

				If Not _dArticle.ContainsKey(row.code) Then
					Dim smnNewCat As New SiteMapNode(Me, row.code, MakeURL(row.category), row.category)
					smnNew.ParentNode = smnNewCat
					_smncCategory.Add(smnNewCat)
					_dArticle(row.code) = New SiteMapNodeCollection()
				Else
					smnNew.ParentNode = _dArticle(row.code)(0).ParentNode
				End If

				_dArticle(row.code).Add(smnNew)
			Next row
		End SyncLock
	End Sub	'BuildNodes


	Public Shared Function Sanitize(ByVal data As [String]) As [String]
		Dim allowed As [String] = "abcdefghijklmnopqrstuvwxyz0123456789."
		Dim clean As [String] = ""

		Dim l As [Char]
		For Each l In data.ToCharArray()
			If allowed.Contains(l.ToString().ToLower()) Then
				clean += l
			Else
				If l.Equals(" "c) And (clean.Length > 0 And clean((clean.Length - 1)) <> "-"c) Then
					clean += "-"
				End If
			End If
		Next l
		Return clean
	End Function 'Sanitize


	Public Overloads Shared Function MakeURL(ByVal title As [String], ByVal category As [String], ByVal post_date As DateTime) As [String]
		Dim url As [String] = ""
		title = Sanitize(title)
		category = Sanitize(category)

		url = "~/Articles/" + category + post_date.ToString("/yyy/MM/dd/") + title + ".aspx"

		Return url
	End Function 'MakeURL


	Public Overloads Shared Function MakeURL(ByVal category As [String]) As [String]
		Dim url As [String] = ""
		category = Sanitize(category)

		url = "~/Articles/" + category + "/Default.aspx"

		Return url
	End Function 'MakeURL


	Public Overrides Sub Initialize(ByVal name As String, ByVal attributes As System.Collections.Specialized.NameValueCollection)
		MyBase.Initialize(name, attributes)
		Me.BuildNodes()
	End Sub	'Initialize


	Public Overrides Function FindSiteMapNode(ByVal rawUrl As String) As SiteMapNode
		Dim relUrl As [String] = rawUrl.Replace(HttpRuntime.AppDomainAppVirtualPath, "~").ToLower()

		If _smnRoot.Url.ToLower() = relUrl Or _smnRoot.Key = rawUrl Then
			Return _smnRoot
		End If
		Dim smnCat As SiteMapNode
		For Each smnCat In _smncCategory
			If smnCat.Url.ToLower() = relUrl Or smnCat.Key = rawUrl Then
				Return smnCat
			End If
		Next smnCat
		Dim smncArticle As SiteMapNodeCollection
		For Each smncArticle In _dArticle.Values
			Dim smnArticle As SiteMapNode
			For Each smnArticle In smncArticle
				If smnArticle.Url.ToLower() = relUrl Or smnArticle.Key = rawUrl Then
					Return smnArticle
				End If
			Next smnArticle
		Next smncArticle
		Return Nothing
	End Function 'FindSiteMapNode


	Public Overrides Function GetChildNodes(ByVal node As SiteMapNode) As SiteMapNodeCollection
		If _smnRoot.Key = node.Key Then
			Return _smncCategory
		End If
		If _dArticle.ContainsKey(node.Key) Then
			Return _dArticle(node.Key)
		End If
		Return Nothing
	End Function 'GetChildNodes


	Public Overrides Function GetParentNode(ByVal node As SiteMapNode) As SiteMapNode
		If _smnRoot.Key = node.Key Then
			Return Nothing
		End If
		If _dArticle.ContainsKey(node.Key) Then
			Return _smnRoot
		End If
		Dim smncArticle As SiteMapNodeCollection
		For Each smncArticle In _dArticle.Values
			Dim smnArticle As SiteMapNode
			For Each smnArticle In smncArticle
				If smnArticle.Key = node.Key Then
					Return smnArticle.ParentNode
				End If
			Next smnArticle
		Next smncArticle
		Return Nothing
	End Function 'GetParentNode


	Protected Overrides Function GetRootNodeCore() As SiteMapNode
		Return _smnRoot
	End Function 'GetRootNodeCore
End Class 'PinballSiteMapProvider