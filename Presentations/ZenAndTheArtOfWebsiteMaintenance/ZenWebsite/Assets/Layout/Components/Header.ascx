<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Assets_Layout_Components_Header" %>
<!-- start header -->
<div id="header">
	<h1><asp:HyperLink ID="hlHome" NavigateUrl="~/Default.aspx" runat="server"><%= SiteMap.RootNode.Title %></asp:HyperLink></h1>
	<p><%= SiteMap.RootNode.Description %></p>
</div>
<!-- end header -->
