<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Articles_Article" Title="Untitled Page" EnableEventValidation="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:ObjectDataSource ID="odsComments" runat="server" InsertMethod="AddComment"
		OldValuesParameterFormatString="original_{0}" SelectMethod="CommentsByArticle"
		TypeName="ContentTableAdapters.CommentTableAdapter">
		<SelectParameters>
			<asp:Parameter Name="article_id" Type="Int32" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="article_id" Type="Int32" />
			<asp:Parameter Name="author" Type="String" />
			<asp:Parameter Name="comment" Type="String" />
		</InsertParameters>
	</asp:ObjectDataSource>
	<b><asp:Literal ID="lDate" runat="server" /></b>
	<asp:Literal ID="lBody" runat="server" />
	<asp:Repeater ID="rComments" runat="server" DataSourceID="odsComments">
		<ItemTemplate>
			<b><asp:Literal ID="lAuthor" runat="server" Text='<%# Eval("author") %>'  /> said on <asp:Literal ID="lCommentDate" runat="server" Text='<%# Eval("comment_date") %>'  />:</b>
			<p><asp:Literal ID="lComment" runat="server" Text='<%# Eval("comment") %>'  /></p>
		</ItemTemplate>
	</asp:Repeater>
	<h2>Leave a comment...</h2>
	<asp:DetailsView ID="dvAddComment" runat="server" AutoGenerateRows="False" DataKeyNames="comment_id"
		DataSourceID="odsComments" DefaultMode="Insert" Height="50px" Width="125px" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnItemInserted="dvAddComment_ItemInserted" OnItemInserting="dvAddComment_ItemInserting">
		<Fields>
			<asp:BoundField DataField="author" HeaderText="Name" SortExpression="author" />
			<asp:TemplateField HeaderText="Comment" SortExpression="comment">
				<EditItemTemplate>
					<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("comment") %>'></asp:TextBox>
				</EditItemTemplate>
				<InsertItemTemplate>
					<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("comment") %>' Rows="5" TextMode="MultiLine" Width="250px"></asp:TextBox>
				</InsertItemTemplate>
				<ItemTemplate>
					<asp:Label ID="Label1" runat="server" Text='<%# Bind("comment") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ShowHeader="False">
				<InsertItemTemplate>
					<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="True" CommandName="Insert"
						Text="Leave Comment" PostBackUrl='<%# SiteMap.CurrentNode.Url %>'></asp:LinkButton>
				</InsertItemTemplate>
				<ItemTemplate>
					<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="New"
						Text="New"></asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateField>
		</Fields>
		<FooterStyle BackColor="White" ForeColor="#333333" />
		<EditRowStyle Font-Bold="True" />
		<RowStyle BackColor="White" ForeColor="#333333" />
		<PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
		<HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
	</asp:DetailsView>
</asp:Content>