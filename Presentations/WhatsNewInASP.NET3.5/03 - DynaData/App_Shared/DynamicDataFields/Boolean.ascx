<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected override void OnDataBinding(EventArgs e) {
        base.OnDataBinding(e);

        object val = DataValue;
        if (val != null)
            CheckBox1.Checked = (bool) val;
    }
</script>

<asp:CheckBox runat="server" ID="CheckBox1" Enabled="false" />