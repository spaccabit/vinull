<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected override void OnDataBinding(EventArgs e) {
        base.OnDataBinding(e);

        if (TemplateMode == DynamicTemplateMode.EditItemTemplate) {
            object val = DataValue;
            if (val != null)
                CheckBox1.Checked = (bool) val;
        }
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[DataField] = CheckBox1.Checked;
    }
</script>

<asp:CheckBox runat="server" ID="CheckBox1" />