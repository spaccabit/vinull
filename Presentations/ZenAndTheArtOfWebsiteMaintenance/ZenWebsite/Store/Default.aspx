<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Store_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="post">
        <div class="title">
            <h2>
                <%= SiteMap.CurrentNode.Title %>
            </h2>
        </div>
        <asp:MultiView ID="mvProducts" runat="server" ActiveViewIndex="0">
            <asp:View ID="vCategories" runat="server">
                <br />
                <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductCategoryID"
                    DataSourceID="odsCategories" GridLines="None" OnSelectedIndexChanged="gvCategories_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Eval("Name") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:View>
            <asp:View ID="vProduct" runat="server">
                <br />
                <asp:DetailsView ID="dvProduct" runat="server" AllowPaging="True" AutoGenerateRows="False"
                    DataKeyNames="ProductID" DataSourceID="odsProducts" SkinID="Product">
                    <Fields>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:TemplateField HeaderText="Model">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ((System.Data.DataRowView)Container.DataItem).Row.GetParentRow("FK_Product_ProductModel_ProductModelID")["Name"] %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProductNumber" HeaderText="Product Number" SortExpression="ProductNumber" />
                        <asp:BoundField DataField="ListPrice" HeaderText="List Price" SortExpression="ListPrice"
                            DataFormatString="{0:c}" HtmlEncode="False" />
                    </Fields>
                </asp:DetailsView>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:ObjectDataSource ID="odsCategories" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" TypeName="AdventureWorks.InventoryTableAdapters.ProductCategoryTableAdapter"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_ProductCategoryID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ParentProductCategoryID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="rowguid" Type="Object" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
            <asp:Parameter Name="Original_ProductCategoryID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ParentProductCategoryID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="rowguid" Type="Object" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsProducts" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetLinkedProducts"
        TypeName="AdventureWorks.InventoryTableAdapters.ProductTableAdapter" UpdateMethod="Update">
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
        <SelectParameters>
            <asp:ControlParameter ControlID="gvCategories" Name="ProductCategoryID" PropertyName="SelectedValue"
                Type="Int32" />
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
</asp:Content>
