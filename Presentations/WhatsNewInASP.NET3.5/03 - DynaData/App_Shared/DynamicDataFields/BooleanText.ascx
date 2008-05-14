<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected override void OnDataBinding(EventArgs e) {
        base.OnDataBinding(e);

        object val = DataValue;
        if (val != null)
            Label1.Text = (bool) val == true ? "True" : "False";
    }
</script>

<asp:Label runat="server" ID="Label1" />