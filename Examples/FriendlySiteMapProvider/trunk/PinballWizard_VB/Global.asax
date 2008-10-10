<%@ Application Language="VB" %>

<script runat="server">


	Sub Application_BeginRequest(ByVal sender As [Object], ByVal e As EventArgs)
		If Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/Articles/") Then
			If Request.AppRelativeCurrentExecutionFilePath.EndsWith("/Default.aspx") Then
				HttpContext.Current.RewritePath("~/Articles/Category.aspx", False)
			Else
				HttpContext.Current.RewritePath("~/Articles/Article.aspx", False)
			End If
		End If
	End Sub	'Application_BeginRequest 
	
</script>