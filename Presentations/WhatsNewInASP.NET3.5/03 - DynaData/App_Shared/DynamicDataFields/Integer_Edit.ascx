<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        TextBox1.ToolTip = MetaMember.Description;
        
        SetupRequiredFieldValidator(RequiredFieldValidator1);
        SetupDynamicValidator(DynamicValidator1);
        SetupRangeValidator(RangeValidator1);
    }
    
    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[DataField] = ConvertEditedValue(TextBox1.Text);
    }
</script>

<asp:TextBox ID="TextBox1" runat="server" Text="<%# DataValue %>" Columns="10"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" ControlToValidate="TextBox1" Display="Dynamic" />
<asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="TextBox1" Type="Integer"
    Enabled="false" EnableClientScript="true" MinimumValue="0" MaximumValue="100" Display="Dynamic" />