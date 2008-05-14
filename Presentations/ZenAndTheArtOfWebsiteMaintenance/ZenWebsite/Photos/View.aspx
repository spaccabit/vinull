<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true"
    CodeFile="View.aspx.cs" Inherits="Photos_View" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="post">
        <div class="title">
            <h2>
                <%= SiteMap.CurrentNode.Title %>
            </h2>
        </div>
        <div class="entry">
            <br />
            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="5" DataKeyNames="FullPath" DataSourceID="ObjectDataSource1">
                <Fields>
                    <asp:ImageField DataImageUrlField="Url" ShowHeader="False"></asp:ImageField>
                </Fields>
            </asp:DetailsView>
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        OnSelecting="ObjectDataSource1_Selecting" SelectMethod="GetPhotoByFileName" TypeName="Zen.Data.PhotoManager">
        <SelectParameters>
            <asp:Parameter Name="FileName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
