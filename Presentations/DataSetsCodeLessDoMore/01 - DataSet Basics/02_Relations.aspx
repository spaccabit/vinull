<%@ Page Language="C#" AutoEventWireup="true" CodeFile="02_Relations.aspx.cs" Inherits="_02_Relations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Relationships</h1>
    <asp:ListView ID="ListView1" runat="server">
        <LayoutTemplate>
            <table>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><b><%# Eval("Name") %></b><br />
                    <i><%# Eval("CatalogDescription") %></i></td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="Repeater1" runat="server"
                    DataSource='<%# ((System.Data.DataRowView)Container.DataItem).Row.GetChildRows("model_product") %>'>
                        <ItemTemplate>
                            <%# ((System.Data.DataRow)Container.DataItem)["Name"]%> 
                            ( <%# ((System.Data.DataRow)Container.DataItem)["ProductNumber"] %> )
                        </ItemTemplate>
                        <SeparatorTemplate><br /></SeparatorTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    </form>
</body>
</html>
