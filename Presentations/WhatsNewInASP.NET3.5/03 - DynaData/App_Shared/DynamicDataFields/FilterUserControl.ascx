<%@ Control Language="C#" Inherits="System.Web.DynamicData.FilterUserControlBase" %>

<script runat="server">
    public event EventHandler SelectedIndexChanged {
        add {
            DropDownList1.SelectedIndexChanged += value;
        }
        remove {
            DropDownList1.SelectedIndexChanged -= value;
        }
    }

    public override string SelectedValue {
        get {
            return DropDownList1.SelectedValue;
        }
    }

    protected void Page_Init(object sender, EventArgs e) {
        DropDownList1.DataSource = DataSource;
        DropDownList1.DataTextField = DataTextField;

        // REVIEW: deal with multiple primary key case
        DropDownList1.DataValueField = DataValueField;

        // Set the initial value if there is one
        if (!String.IsNullOrEmpty(InitialValue))
            DropDownList1.SelectedValue = InitialValue;

        DropDownList1.DataBind();

        // Need to set DataSource back to null to make sure the framework doesn't bind a second time
        DropDownList1.DataSource = null;
    }
</script>

<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
    <asp:ListItem Text="All" Value="" />
</asp:DropDownList>