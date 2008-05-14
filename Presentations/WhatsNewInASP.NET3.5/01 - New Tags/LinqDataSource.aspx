<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinqDataSource.aspx.cs" Inherits="LinqDataSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>
        Adventure Works Products</h3>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1"
        AllowPaging="True" AllowSorting="True" DataKeyNames="ProductID" Width="100%">
        <Columns>
            <asp:BoundField DataField="ProductNumber" HeaderText="SKU" SortExpression="ProductNumber" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
            <asp:BoundField DataField="StandardCost" HeaderText="Cost" 
                SortExpression="StandardCost" HtmlEncode="false" DataFormatString="{0:c}" 
                ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ListPrice" HeaderText="Price" 
                SortExpression="ListPrice" HtmlEncode="false" DataFormatString="{0:c}" 
                ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" 
                ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" 
                ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="AWLinqDataContext"
    TableName="Products" Where="ThumbnailPhotoFileName != @ThumbnailPhotoFileName &amp;&amp; ListPrice &gt; @ListPrice"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
        OrderBy="ListPrice desc">
    <WhereParameters>
        <asp:Parameter DefaultValue="no_image_available_small.gif" Name="ThumbnailPhotoFileName"
            Type="String" />
        <asp:Parameter DefaultValue="150" Name="ListPrice" Type="Decimal" />
    </WhereParameters>
</asp:LinqDataSource>
    </form>
</body>
</html>
