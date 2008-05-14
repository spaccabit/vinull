<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Post.ascx.cs" Inherits="Assets_Layout_Components_Post" %>
<div class="post">
    <div class="title">
        <h2><a href="#"><asp:PlaceHolder ID="phTitle" runat="server" /></a></h2>
        <p><small>Posted on
            <asp:Label ID="lDate" runat="server" />
            by <a href="#">
                <asp:Label ID="lAuthor" runat="server" /></a></small></p>
    </div>
    <div class="entry">
        <asp:PlaceHolder ID="phBody" runat="server" />
    </div>
    <p class="links"><a href="#" class="more">Read More</a> &nbsp;&nbsp;&nbsp; <a href="#"
        class="comments">No Comments</a> </p>
</div>
