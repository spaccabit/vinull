<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Nav.ascx.cs" Inherits="Assets_Layout_Components_Nav" %>
<!-- star menu -->
<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="False"
    StartingNodeOffset="-1" StartingNodeUrl="~/Default.aspx" />
<asp:Repeater ID="rNav" runat="server" DataSourceID="SiteMapDataSource1">
    <HeaderTemplate>
        <div id="menu">
            <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink ID="hlNav" runat="server" NavigateUrl='<%# Eval("Url") %>' Text='<%# Eval("Title") %>' /></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </div>
    </FooterTemplate>
</asp:Repeater>
<!-- end menu -->
