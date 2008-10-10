<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Category.aspx.vb" Inherits="Articles_Category" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="False"
		StartFromCurrentNode="True" />
	<h2>Articles</h2>
	<asp:Menu ID="Menu1" runat="server" BackColor="#F7F6F3" DataSourceID="SiteMapDataSource1"
		DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="1.1em" ForeColor="#7C6F57"
		StaticSubMenuIndent="10px">
		<StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
		<DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
		<DynamicMenuStyle BackColor="#F7F6F3" />
		<StaticSelectedStyle BackColor="#5D7B9D" />
		<DynamicSelectedStyle BackColor="#5D7B9D" />
		<DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
		<StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
	</asp:Menu>
</asp:Content>