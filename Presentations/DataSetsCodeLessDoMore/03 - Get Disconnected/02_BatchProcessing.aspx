<%@ Page Language="C#" AutoEventWireup="true" CodeFile="02_BatchProcessing.aspx.cs" Inherits="_02_BatchProcessing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Batch Processing</h1>
    <div>
        <asp:Button ID="bHike" runat="server" Text="$500 Price Hike" 
            onclick="bHike_Click" />
    &nbsp;<asp:Button ID="bDrop" runat="server" Text="$500 Price Drop" 
            onclick="bDrop_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Caption="Products over $3500.00 List Price" DataKeyNames="ProductID" 
        DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="ProductNumber" HeaderText="ProductNumber" 
                SortExpression="ProductNumber" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="StandardCost" HeaderText="StandardCost" 
                SortExpression="StandardCost" />
            <asp:BoundField DataField="ListPrice" HeaderText="ListPrice" 
                SortExpression="ListPrice" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPrice"
        
        TypeName="_02___TypedDataSet.AdventureWorksDSTableAdapters.ProductTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="3500" Name="PricePoint" Type="Decimal" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
