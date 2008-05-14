<%@ Page Language="C#" AutoEventWireup="true" CodeFile="01_XmlData.aspx.cs" Inherits="_01_XmlData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        XML Data</h1>
    <div>
        <asp:Button ID="DBToXML" runat="server" Text="DB -&gt; XML" OnClick="DBToXML_Click" />
        <asp:Button ID="XMLToDB" runat="server" Text="XML -&gt; DB" OnClick="XMLToDB_Click" />
    </div>
    <h2>Database</h2>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="ProductModelID" DataSourceID="ObjectDataSource1"
        PageSize="5">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ProductModelID" HeaderText="ProductModelID" InsertVisible="False"
                ReadOnly="True" SortExpression="ProductModelID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="CatalogDescription" HeaderText="CatalogDescription" 
                SortExpression="CatalogDescription" ReadOnly="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="_02___TypedDataSet.AdventureWorksDSTableAdapters.ProductModelTableAdapter"
        UpdateMethod="UpdateSimple">
        <DeleteParameters>
            <asp:Parameter Name="Original_ProductModelID" Type="Int32" />
            <asp:Parameter Name="Original_Name" Type="String" />
            <asp:Parameter Name="Original_rowguid" Type="Object" />
            <asp:Parameter Name="Original_ModifiedDate" Type="DateTime" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Original_ProductModelID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="CatalogDescription" Type="Object" />
            <asp:Parameter Name="rowguid" Type="Object" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
        </InsertParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
