<%@ WebHandler Language="C#" Class="SiteMapBuilder" %>

using System;
using System.Web;
using System.Xml;
using System.Collections.Generic;
using System.Web.UI;

public class SiteMapBuilder : IHttpHandler {

    public void ProcessRequest(HttpContext context) {

        context.Response.Clear();
        context.Response.ContentType = "text/xml";
        XmlTextWriter xmlSiteMap = new XmlTextWriter(context.Response.OutputStream, System.Text.Encoding.UTF8);
        xmlSiteMap.WriteStartDocument();
        xmlSiteMap.WriteStartElement("urlset");
        xmlSiteMap.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        xmlSiteMap.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");
        xmlSiteMap.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");

        Control crtl = new Control();
        String baseUrl = context.Request.Url.GetLeftPart(UriPartial.Path);
        List<string> seen = new List<string>();
        foreach (SiteMapNode node in SiteMap.Provider.FindSiteMapNode("~/Default.aspx").ParentNode.GetAllNodes()) {

            if (!seen.Contains(node.Url)) {
                
                xmlSiteMap.WriteStartElement("url");
                xmlSiteMap.WriteElementString("loc", baseUrl + crtl.ResolveUrl(node.Url));
                if (node.Url.Contains("/Photo/")) {
                    xmlSiteMap.WriteElementString("priority", "1.0");
                    xmlSiteMap.WriteElementString("changefreq", "weekly");
                }
                else {
                    xmlSiteMap.WriteElementString("priority", "0.5");
                    xmlSiteMap.WriteElementString("changefreq", "monthly");
                }
                xmlSiteMap.WriteEndElement();
                seen.Add(node.Url);
            }
        }
        xmlSiteMap.WriteEndElement();
        xmlSiteMap.WriteEndDocument();
        xmlSiteMap.Flush();
        xmlSiteMap.Close();
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}