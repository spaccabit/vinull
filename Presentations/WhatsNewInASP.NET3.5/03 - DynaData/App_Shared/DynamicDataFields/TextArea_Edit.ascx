<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        TextBox1.MaxLength = MetaMember.MaxLength;
        TextBox1.ToolTip = MetaMember.Description;

        SetupRequiredFieldValidator(reqName);
        SetupDynamicValidator(dynamicValidator);
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[DataField] = ConvertEditedValue(TextBox1.Text);
    }
</script>

<asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Text='<%# DataValue %>'></asp:TextBox>

<asp:RequiredFieldValidator runat="server" id="reqName" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" id="dynamicValidator" ControlToValidate="TextBox1" Display="Dynamic" />