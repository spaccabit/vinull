<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected string GetDisplayString() {
        DynamicMetaForeignKeyMember column = (DynamicMetaForeignKeyMember)MetaMember;
        return FormatDataValue(column.OtherMetaTable.GetDisplayString(DataValue));
    }
</script>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# ForeignKeyPath %>" />