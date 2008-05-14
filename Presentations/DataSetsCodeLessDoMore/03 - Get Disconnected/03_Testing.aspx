﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="03_Testing.aspx.cs" Inherits="_03_Testing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Unit Testing</h1>
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
                <asp:BoundField DataField="ExpectedProfit" HeaderText="Expected Profits" 
                SortExpression="ExpectedProfit" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DeleteMethod="Delete" InsertMethod="Insert" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPrice" 
        TypeName="_02___TypedDataSet.AdventureWorksDSTableAdapters.ProductTableAdapter" 
        UpdateMethod="Update" onselected="ObjectDataSource1_Selected">
        <DeleteParameters>
            <asp:Parameter Name="Original_ProductID" Type="Int32" />
            <asp:Parameter Name="Original_Name" Type="String" />
            <asp:Parameter Name="Original_ProductNumber" Type="String" />
            <asp:Parameter Name="Original_Color" Type="String" />
            <asp:Parameter Name="Original_StandardCost" Type="Decimal" />
            <asp:Parameter Name="Original_ListPrice" Type="Decimal" />
            <asp:Parameter Name="Original_Size" Type="String" />
            <asp:Parameter Name="Original_Weight" Type="Decimal" />
            <asp:Parameter Name="Original_ProductCategoryID" Type="Int32" />
            <asp:Parameter Name="Original_ProductModelID" Type="Int32" />
            <asp:Parameter Name="Original_SellStartDate" Type="DateTime" />
            <asp:Parameter Name="Original_SellEndDate" Type="DateTime" />
            <asp:Parameter Name="Original_DiscontinuedDate" Type="DateTime" />
            <asp:Parameter Name="Original_ThumbnailPhotoFileName" Type="String" />
            <asp:Parameter Name="Original_rowguid" Type="Object" />
            <asp:Parameter Name="Original_ModifiedDate" Type="DateTime" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="ProductNumber" Type="String" />
            <asp:Parameter Name="Color" Type="String" />
            <asp:Parameter Name="StandardCost" Type="Decimal" />
            <asp:Parameter Name="ListPrice" Type="Decimal" />
            <asp:Parameter Name="Size" Type="String" />
            <asp:Parameter Name="Weight" Type="Decimal" />
            <asp:Parameter Name="ProductCategoryID" Type="Int32" />
            <asp:Parameter Name="ProductModelID" Type="Int32" />
            <asp:Parameter Name="SellStartDate" Type="DateTime" />
            <asp:Parameter Name="SellEndDate" Type="DateTime" />
            <asp:Parameter Name="DiscontinuedDate" Type="DateTime" />
            <asp:Parameter Name="ThumbNailPhoto" Type="Object" />
            <asp:Parameter Name="ThumbnailPhotoFileName" Type="String" />
            <asp:Parameter Name="rowguid" Type="Object" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
            <asp:Parameter Name="Original_ProductID" Type="Int32" />
            <asp:Parameter Name="Original_Name" Type="String" />
            <asp:Parameter Name="Original_ProductNumber" Type="String" />
            <asp:Parameter Name="Original_Color" Type="String" />
            <asp:Parameter Name="Original_StandardCost" Type="Decimal" />
            <asp:Parameter Name="Original_ListPrice" Type="Decimal" />
            <asp:Parameter Name="Original_Size" Type="String" />
            <asp:Parameter Name="Original_Weight" Type="Decimal" />
            <asp:Parameter Name="Original_ProductCategoryID" Type="Int32" />
            <asp:Parameter Name="Original_ProductModelID" Type="Int32" />
            <asp:Parameter Name="Original_SellStartDate" Type="DateTime" />
            <asp:Parameter Name="Original_SellEndDate" Type="DateTime" />
            <asp:Parameter Name="Original_DiscontinuedDate" Type="DateTime" />
            <asp:Parameter Name="Original_ThumbnailPhotoFileName" Type="String" />
            <asp:Parameter Name="Original_rowguid" Type="Object" />
            <asp:Parameter Name="Original_ModifiedDate" Type="DateTime" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="3500" Name="PricePoint" Type="Decimal" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="ProductNumber" Type="String" />
            <asp:Parameter Name="Color" Type="String" />
            <asp:Parameter Name="StandardCost" Type="Decimal" />
            <asp:Parameter Name="ListPrice" Type="Decimal" />
            <asp:Parameter Name="Size" Type="String" />
            <asp:Parameter Name="Weight" Type="Decimal" />
            <asp:Parameter Name="ProductCategoryID" Type="Int32" />
            <asp:Parameter Name="ProductModelID" Type="Int32" />
            <asp:Parameter Name="SellStartDate" Type="DateTime" />
            <asp:Parameter Name="SellEndDate" Type="DateTime" />
            <asp:Parameter Name="DiscontinuedDate" Type="DateTime" />
            <asp:Parameter Name="ThumbNailPhoto" Type="Object" />
            <asp:Parameter Name="ThumbnailPhotoFileName" Type="String" />
            <asp:Parameter Name="rowguid" Type="Object" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
        </InsertParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
