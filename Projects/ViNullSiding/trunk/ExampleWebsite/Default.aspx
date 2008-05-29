<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form runat="server">
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" 
        AllowPaging="True" AutoGenerateRows="False" DataSourceID="ObjectDataSource1" 
        DefaultMode="Insert">
        <Fields>
            <asp:BoundField DataField="PostID" HeaderText="PostID" 
                SortExpression="PostID" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Preview" HeaderText="Preview" 
                SortExpression="Preview" />
            <asp:BoundField DataField="Body" HeaderText="Body" SortExpression="Body" />
            <asp:BoundField DataField="Author" HeaderText="Author" 
                SortExpression="Author" />
            <asp:BoundField DataField="AuthorUsername" HeaderText="AuthorUsername" 
                SortExpression="AuthorUsername" />
            <asp:BoundField DataField="AuthotEmail" HeaderText="AuthotEmail" 
                SortExpression="AuthotEmail" />
            <asp:BoundField DataField="PostedOn" HeaderText="PostedOn" 
                SortExpression="PostedOn" />
            <asp:BoundField DataField="LastModified" HeaderText="LastModified" 
                SortExpression="LastModified" />
            <asp:CheckBoxField DataField="Draft" HeaderText="Draft" 
                SortExpression="Draft" />
            <asp:BoundField DataField="Link" HeaderText="Link" SortExpression="Link" />
            <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="ViNull.Blog.Post" InsertMethod="SavePost" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetPosts" 
        TypeName="ViNull.Blog.BlogService" UpdateMethod="SavePost">
        <SelectParameters>
            <asp:Parameter Name="Limit" Type="Int32" />
            <asp:Parameter Name="Offset" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
