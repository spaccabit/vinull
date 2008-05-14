﻿<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        TextBox1.MaxLength = MetaMember.MaxLength;
        if (MetaMember.MaxLength < 20)
            TextBox1.Columns = MetaMember.MaxLength;
        TextBox1.ToolTip = MetaMember.Description;

        SetupRequiredFieldValidator(reqName);
        SetupRegexValidator(regexValidator);
        SetupDynamicValidator(dynamicValidator);
    }
    
    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[DataField] = ConvertEditedValue(TextBox1.Text);
    }
</script>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataValue %>'></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="dynamicValidator" ControlToValidate="TextBox1" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server" ID="regexValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />