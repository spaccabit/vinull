﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" %>

<script runat="server">
    protected void Page_InitComplete(object sender, EventArgs e) {
        GridView1.InitializeMaster(/*loadForeignKeys*/true);
    }
    protected void Page_Load(object sender, EventArgs e) {
        Title = "My Title for " + GridDataSource.TableName;
    }
</script>

<%@ Register  src="~/App_Shared/DynamicDataFields/GridViewPager.ascx" tagname="GridViewPager" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><%= GridDataSource.TableName%> table</h2>

    <asp:ScriptManagerProxy runat="server" ID="ScriptManagerProxy1" />

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
        HeaderText="List of validation errors" />
    <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" />

    <br /><br />

    <asp:DynamicFilterRepeater ID="FilterRepeater" runat="server">
        <ItemTemplate>
            <%# Eval("Name") %> <asp:DynamicFilter runat="server" ID="DynamicFilter" />
        </ItemTemplate>
        <FooterTemplate><br /><br /></FooterTemplate>
    </asp:DynamicFilterRepeater>

    <asp:DynamicGridView ID="GridView1" runat="server" DataSourceID="GridDataSource"
        AutoGenerateEditButton="True" AutoGenerateDeleteButton="true"
        AllowPaging="True" AllowSorting="True" EnableQueryStringSelection="True"
        CssClass="gridview" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false">
        <Columns>
            <asp:DynamicField DataField="CompanyName" HeaderText="CompanyName" />
            <asp:DynamicField DataField="Title" HeaderText="Title" />
            <asp:DynamicField DataField="FirstName" HeaderText="FirstName" />
            <asp:DynamicField DataField="LastName" HeaderText="LastName" />
            <asp:DynamicTemplateField HeaderText="EmailAddress" >
                <ItemTemplate>
                    <asp:HyperLink ID="hl1" runat="server" Text='<%# Eval("EmailAddress") %>'
                        NavigateUrl='<%# Eval("EmailAddress","mailto:{0}") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicEditControl ID="dc1" runat="server" DataField="EmailAddress" />
                </EditItemTemplate>
            </asp:DynamicTemplateField>
            <asp:DynamicField DataField="CustomerAddresses" HeaderText="CustomerAddresses" />
            <asp:DynamicTemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"
                        NavigateUrl='<%# DynamicDatabase.TheDatabase.GetActionPath(GridDataSource.TableName, System.Web.DynamicData.Action.Details, GetDataItem()) %>'>Details</asp:HyperLink>
                </ItemTemplate>
            </asp:DynamicTemplateField>
        </Columns>

        <PagerStyle CssClass="Footer"/>        
        <PagerTemplate>
            <uc1:GridViewPager runat="server" />
        </PagerTemplate>
        <EmptyDataTemplate>
            There are currently no items in this table.
        </EmptyDataTemplate>
    </asp:DynamicGridView>

    <asp:LinqDataSource ID="GridDataSource" runat="server">
        <WhereParameters>
            <asp:DynamicControlParameter ControlID="FilterRepeater" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>