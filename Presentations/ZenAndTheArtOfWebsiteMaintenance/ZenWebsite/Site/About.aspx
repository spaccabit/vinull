<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Site_About" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <div class="post">
        <div class="title">
            <h2><a href="#">About This Site</a></h2>
        </div>
        <div class="entry">
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1"></asp:TreeView>
        </div>
    </div>
    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" StartingNodeOffset="-1" StartingNodeUrl="~/Default.aspx" />
</asp:Content>

