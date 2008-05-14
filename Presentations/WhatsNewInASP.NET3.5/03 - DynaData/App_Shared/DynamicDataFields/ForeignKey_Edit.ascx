<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    // TODO: Clean this up.  Some logic should be moved to base class.
    
    protected void Page_Load(object sender, EventArgs e) {
        DynamicMetaForeignKeyMember column = (DynamicMetaForeignKeyMember)MetaMember;

        DynamicMetaTable parentTable = column.ParentMetaTable;

        DropDownList1.DataSource = parentTable.Query;

        DropDownList1.DataTextField = parentTable.DisplayMetaMember.Name;
        // REVIEW: what if the PK has several columns?
        DropDownList1.DataValueField = parentTable.IdentityMetaMembers[0].Name;

        if (!column.IsRequired) {
            DropDownList1.Items.Add(new ListItem("[Not Set]", ""));
        }

        DropDownList1.AppendDataBoundItems = true;
        DropDownList1.DataBind();

        // Need to set DataSource back to null to make sure the framework doesn't bind a second time
        DropDownList1.DataSource = null;
    }

    private string GetCurrentValue() {
        DynamicMetaForeignKeyMember column = (DynamicMetaForeignKeyMember)MetaMember;
        object value = DataBinder.GetPropertyValue(DataItem, column.KeyMetaMemberInThisTable.Name);

        if (value == null)
            return String.Empty;
        return value.ToString();
    }

    protected override void OnDataBinding(EventArgs e) {
        base.OnDataBinding(e);

        if (TemplateMode == DynamicTemplateMode.EditItemTemplate) {
            DropDownList1.SelectedValue = GetCurrentValue();
        }
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        // If it's an empty string, change it to null
        string val = DropDownList1.SelectedValue;
        if (val == String.Empty)
            val = null;

        DynamicMetaForeignKeyMember column = (DynamicMetaForeignKeyMember)MetaMember;
        dictionary[column.KeyMetaMemberInThisTable.Name] = val;
    }
</script>

<asp:DropDownList ID="DropDownList1" runat="server">
</asp:DropDownList>