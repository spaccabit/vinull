<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        DynamicMetaChildrenMember column = (DynamicMetaChildrenMember)MetaMember;

        HyperLink1.Text = "View " + column.OtherMetaTable.Name;
    }
</script>

<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%# ChildrenPath %>" />