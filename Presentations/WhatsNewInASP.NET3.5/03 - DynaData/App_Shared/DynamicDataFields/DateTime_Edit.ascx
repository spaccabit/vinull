<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        TextBox1.ToolTip = MetaMember.Description;
        
        SetupRequiredFieldValidator(reqName);
        SetupDynamicValidator(dynamicValidator);
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[DataField] = ConvertEditedValue(TextBox1.Text);
    }
</script>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataValue %>' Columns="20"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="dynamicValidator" ControlToValidate="TextBox1" Display="Dynamic" />