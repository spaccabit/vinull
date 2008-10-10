<%@ Application Language="C#" %>

<script runat="server">

	void Application_BeginRequest(Object sender, EventArgs e) {
		if (Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/Articles/")) {
			if (Request.AppRelativeCurrentExecutionFilePath.EndsWith("/Default.aspx"))
				HttpContext.Current.RewritePath("~/Articles/Category.aspx", false);
			else
				HttpContext.Current.RewritePath("~/Articles/Article.aspx", false);
		}
	}


</script>