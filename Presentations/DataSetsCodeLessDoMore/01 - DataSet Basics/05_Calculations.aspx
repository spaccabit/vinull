<%@ Page Language="C#" AutoEventWireup="true" CodeFile="05_Calculations.aspx.cs"
    Inherits="_05_Calculations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Calculations</h1>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetModelsAndProdcuts" TypeName="_02___TypedDataSet.AdventureWorksDSTableAdapters.ProductModelTableAdapter"
        OnSelected="ObjectDataSource1_Selected"></asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ProductModelID" Width="50%" DataSourceID="ObjectDataSource1" ShowFooter="True"
        AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Model" SortExpression="Name" />
            <asp:TemplateField HeaderText="Products" SortExpression="ProductCount">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductCount") %>'/>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Products.ToString("n0") %>'/>
                </FooterTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Retail" SortExpression="ProductRetail">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductRetail", "{0:c}") %>'/>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Retail.ToString("c") %>'/>
                </FooterTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
