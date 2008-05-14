<%@ Page Language="C#" AutoEventWireup="true" CodeFile="04_Tableadapters.aspx.cs" Inherits="_04_Tableadapters" %>
<%@ Import Namespace="_02___TypedDataSet" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>TableAdapters</h1>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetModelsAndProdcuts" 
            TypeName="_02___TypedDataSet.AdventureWorksDSTableAdapters.ProductModelTableAdapter">
        </asp:ObjectDataSource>
     <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">
        <LayoutTemplate>
            <table>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><b><%# Eval("Name") %></b><br />
                    <i><%# Eval("CatalogDescription") %></i></td>
            </tr>
            <tr>
                <td>
                <asp:Repeater ID="Repeater1" runat="server"
                    DataSource='<%# ((AdventureWorksDS.ProductModelRow)((DataRowView)Container.DataItem).Row).GetProductRows() %>'>
                        <ItemTemplate>
                            <%# Eval("Name")%> 
                            ( <%# Eval("ProductNumber") %> )
                        </ItemTemplate>
                        <SeparatorTemplate><br /></SeparatorTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    </form>
</body>
</html>
