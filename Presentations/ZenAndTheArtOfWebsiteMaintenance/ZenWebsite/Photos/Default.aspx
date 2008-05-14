<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Photos_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="post">
        <div class="title">
            <h2>Zen Demo Photos</h2>
        </div>
        <div class="entry">
            <asp:DataList ID="DataList1" runat="server" DataKeyField="FullPath" DataSourceID="ObjectDataSource1" CellPadding="10" HorizontalAlign="Center" RepeatColumns="2">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl='<%# Eval("ThumbUrl") %>'
                        NavigateUrl='<%# Eval("PageUrl") %>' />
                    <br />
                    <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("PageUrl") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:DataList><br />
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPhotos" TypeName="Zen.Data.PhotoManager"></asp:ObjectDataSource>
</asp:Content>
