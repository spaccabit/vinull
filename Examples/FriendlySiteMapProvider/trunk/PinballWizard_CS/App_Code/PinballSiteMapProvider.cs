using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for PinballSiteMapProvider
/// </summary>
public class PinballSiteMapProvider : SiteMapProvider
{
	private SiteMapNode _smnRoot;
	private SiteMapNodeCollection _smncCategory;
	private Dictionary<string, SiteMapNodeCollection> _dArticle;
	private object lockobj = new object();

	public void BuildNodes() {
		lock (lockobj) {
			_smnRoot = new SiteMapNode(this, "PinballRoot", "~/Default.aspx", "Pinball Wizard");
			_smncCategory = new SiteMapNodeCollection();
			_dArticle = new Dictionary<string, SiteMapNodeCollection>();

			ContentTableAdapters.ArticleTableAdapter taArticle = new ContentTableAdapters.ArticleTableAdapter();
			Content.ArticleDataTable dtArticle = taArticle.AllArticles();

			foreach (Content.ArticleRow row in dtArticle.Rows) {
				SiteMapNode smnNew = new SiteMapNode(this, row.article_id.ToString(), MakeURL(row.title, row.category, row.post_date), row.title);

				if (!_dArticle.ContainsKey(row.code)) {
					SiteMapNode smnNewCat = new SiteMapNode(this, row.code, MakeURL(row.category), row.category);
					smnNew.ParentNode = smnNewCat;
					_smncCategory.Add(smnNewCat);
					_dArticle[row.code] = new SiteMapNodeCollection();
				} else {
					smnNew.ParentNode = _dArticle[row.code][0].ParentNode;
				}
				
				_dArticle[row.code].Add(smnNew);
			}
		}
	}

	public static String Sanitize(String data) {
		String allowed = "abcdefghijklmnopqrstuvwxyz0123456789.";
		String clean = "";

		foreach (Char l in data.ToCharArray()) {
			if (allowed.Contains(l.ToString().ToLower())) {
				clean += l;
			} else if (l.Equals(' ') && (clean.Length > 0 && clean[clean.Length - 1] != '-')) {
				clean += "-";
			}
		}

		return clean;
	}

	public static String MakeURL(String title, String category, DateTime post_date) {
		String url = "";
		title = Sanitize(title);
		category = Sanitize(category);

		url = "~/Articles/" + category + post_date.ToString("/yyy/MM/dd/") + title + ".aspx";

		return url;
	}

	public static String MakeURL(String category) {
		String url = "";
		category = Sanitize(category);

		url = "~/Articles/" + category + "/Default.aspx";

		return url;
	}

	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes) {
		base.Initialize(name, attributes);
		this.BuildNodes();
	}
	
	public override SiteMapNode FindSiteMapNode(string rawUrl) {
		String relUrl = rawUrl.Replace(HttpRuntime.AppDomainAppVirtualPath, "~").ToLower();
		
		if (_smnRoot.Url.ToLower() == relUrl || _smnRoot.Key == rawUrl)
			return _smnRoot;

		foreach (SiteMapNode smnCat in _smncCategory)
			if (smnCat.Url.ToLower() == relUrl || smnCat.Key == rawUrl)
				return smnCat;

		foreach (SiteMapNodeCollection smncArticle in _dArticle.Values)
			foreach (SiteMapNode smnArticle in smncArticle)
				if (smnArticle.Url.ToLower() == relUrl || smnArticle.Key == rawUrl)
					return smnArticle;

		return null;
	}

	public override SiteMapNodeCollection GetChildNodes(SiteMapNode node) {
		if (_smnRoot.Key == node.Key)
			return _smncCategory;

		if (_dArticle.ContainsKey(node.Key))
			return _dArticle[node.Key];

		return null;
	}

	public override SiteMapNode GetParentNode(SiteMapNode node) {
		if (_smnRoot.Key == node.Key)
			return null;

		if (_dArticle.ContainsKey(node.Key))
			return _smnRoot;

		foreach (SiteMapNodeCollection smncArticle in _dArticle.Values)
			foreach (SiteMapNode smnArticle in smncArticle)
				if (smnArticle.Key == node.Key)
					return smnArticle.ParentNode;

		return null;		
	}

	protected override SiteMapNode GetRootNodeCore() {
		return _smnRoot;
	}
}
