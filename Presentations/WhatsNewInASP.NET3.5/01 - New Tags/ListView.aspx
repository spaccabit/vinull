<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListView.aspx.cs" Inherits="_ListView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProductID" DataSourceID="odsProducts"
        GroupItemCount="3" InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <h3>
                Adventure Works Products</h3>
            <table width="100%" cellpadding="4" cellspacing="0" border="1">
                <tr>
                    <td colspan="8">
                        <b>Sort</b>
                        <asp:LinkButton runat="server" ID="SortByProductNumber" CommandName="Sort" Text="SKU"
                            CommandArgument="ProductNumber" />
                        <asp:LinkButton runat="server" ID="SortByName" CommandName="Sort" Text="Name" CommandArgument="Name" />
                        <asp:LinkButton runat="server" ID="SortByColorButton" CommandName="Sort" Text="Color"
                            CommandArgument="Color" />
                    </td>
                </tr>
                <tr id="groupPlaceholder" runat="server">
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <asp:DataPager ID="dpProducts" runat="server" PageSize="14">
                            <Fields>
                                <asp:TemplatePagerField>
                                    <PagerTemplate>
                                        <b>Page
                                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.TotalRowCount>0 ? (Container.StartRowIndex / Container.PageSize) + 1 : 0 %>" />
                                            of
                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Math.Ceiling ((double)Container.TotalRowCount / Container.PageSize) %>" />
                                            (
                                            <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                            records)
                                            <br />
                                        </b>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true" ShowNextPageButton="false"
                                    ShowPreviousPageButton="false" />
                                <asp:NumericPagerField PreviousPageText="&lt; Prev 10" NextPageText="Next 10 &gt;"
                                    ButtonCount="10" />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="false"
                                    ShowPreviousPageButton="false" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <GroupTemplate>
            <tr>
                <td id="itemPlaceholder" runat="server" />
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td valign="middle" align="center">
                <asp:Image ID="iProduct" runat="server" ImageUrl='<%# Eval("ProductID","Thumbnail.ashx?pid={0:d}") %>' />
            </td>
            <td valign="top">
                <%# Eval("ProductNumber") %><br />
                <%# Eval("Name") %><br />
                <%# Eval("Color") %><br />
            </td>
        </ItemTemplate>
        <EmptyItemTemplate>
            <td colspan="2">
                &nbsp;
            </td>
        </EmptyItemTemplate>
        <InsertItemTemplate>
            <td align="right" valign="top">
                <b>SKU:</b><br />
                <b>Name:</b><br />
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" /><br />
                <asp:TextBox ID="TextBox2" runat="server" /><br />
                <asp:Button ID="Button1" runat="server" Text="Add Product" />
            </td>
        </InsertItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="odsProducts" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataWithImages"
        TypeName="AWDataSetTableAdapters.ProductTableAdapter" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_ProductID" Type="Int32" />
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
        </UpdateParameters>
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
