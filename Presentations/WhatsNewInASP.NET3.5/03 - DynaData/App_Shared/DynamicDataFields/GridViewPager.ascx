<%@ Control Language="C#" Inherits="System.Web.DynamicData.FieldTemplateUserControlBase" %>

<script runat="server">
    private GridView _gridView;

    protected void Page_Load(object sender, EventArgs e) {
        Control c = Parent;
        while (c != null) {
            if (c is GridView) {
                _gridView = (GridView)c;
                break;
            }
            c = c.Parent;
        }
        if (_gridView != null) {
            _gridView.RowDataBound += new GridViewRowEventHandler(GridView_RowDataBound);
        }
    }

    protected void TextBoxPage_TextChanged(object sender, EventArgs e) {
        if (_gridView == null) {
            return;
        }
        int page;
        if (int.TryParse(TextBoxPage.Text.Trim(), out page)) {
            if (page < 0) {
                page = 1;
            }
            if (page > _gridView.PageCount) {
                page = _gridView.PageCount;
            }
            _gridView.PageIndex = page - 1;
        }
        TextBoxPage.Text = (_gridView.PageIndex + 1).ToString();
    }

    protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs e) {
        if (_gridView == null) {
            return;
        }
        DropDownList dropdownlistpagersize = (DropDownList)sender;
        _gridView.PageSize = Convert.ToInt32(dropdownlistpagersize.SelectedValue);
        int pageindex = _gridView.PageIndex;
        _gridView.DataBind();
        if (_gridView.PageIndex != pageindex) {
            //if page index changed it means the previous page was not valid and was adjusted. Rebind to fill control with adjusted page
            _gridView.DataBind();
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        GridView gridview = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Pager) {
            LabelNumberOfPages.Text = gridview.PageCount.ToString();
            TextBoxPage.Text = (gridview.PageIndex + 1).ToString();
            DropDownListPageSize.SelectedValue = gridview.PageSize.ToString();
        }
    }
</script>

<div class="PageXofN">
    <asp:ImageButton ToolTip="First page" ID="ImageButtonFirst" runat="server" ImageUrl="~/App_Shared/Images/PgFirst.gif" Width="8" Height="9" CommandName="Page" CommandArgument="First" />
    &nbsp;
    <asp:ImageButton ToolTip="Previous page" ID="ImageButtonPrev" runat="server" ImageUrl="~/App_Shared/Images/PgPrev.gif" Width="5" Height="9" CommandName="Page" CommandArgument="Prev" />
    &nbsp;
    Page 
    <asp:TextBox ID="TextBoxPage" runat="server" Columns="5" AutoPostBack="true" ontextchanged="TextBoxPage_TextChanged" Width="20px" />
    of
    <asp:Label ID="LabelNumberOfPages" runat="server" />
    &nbsp;
    <asp:ImageButton ToolTip="Next page" ID="ImageButtonNext" runat="server" ImageUrl="~/App_Shared/Images/PgNext.gif" Width="5" Height="9" CommandName="Page" CommandArgument="Next" />
    &nbsp;
    <asp:ImageButton ToolTip="Last page" ID="ImageButtonLast" runat="server" ImageUrl="~/App_Shared/Images/PgLast.gif" Width="8" Height="9" CommandName="Page" CommandArgument="Last" />
    <div class="Results">
    <asp:Label ID="LabelRows" runat="server" Text="Results per page:" />
    <asp:DropDownList ID="DropDownListPageSize" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListPageSize_SelectedIndexChanged">
        <asp:ListItem Value="5" />
        <asp:ListItem Value="10" />
        <asp:ListItem Value="15" />
        <asp:ListItem Value="20" />
    </asp:DropDownList>
    </div>
</div>