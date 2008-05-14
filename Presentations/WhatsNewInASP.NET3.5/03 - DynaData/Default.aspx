<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <h2>My tables</h2>

    <br /><br />

    <asp:DynamicGridView ID="Menu1" runat="server" DataSourceID="NDS" AllowSorting="true" AutoGenerateColumns="false"
        CssClass="gridview" AlternatingRowStyle-CssClass="even">
        <Columns>
            <asp:TemplateField HeaderText="Table Name" SortExpression="TableName">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Path") %>'><%#Eval("TableName") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:DynamicGridView>
    <asp:DynamicNavigatorDataSource runat="server" ID="NDS" />
</asp:Content>


