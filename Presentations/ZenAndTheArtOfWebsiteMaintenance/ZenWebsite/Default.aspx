<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <Layout:Post ID="Post1" runat="server" PostDate="Aug 20, 2007" Author="Free CSS Templates">
        <Title>About this Template</Title>
        <Body>
            <p>This is <strong>Mr. Techie</strong>, a free, fully standards-compliant CSS template
        designed by <a href="http://www.freecsstemplates.org/">Free CSS Templates</a>. This
        free template is released under a <a href="http://creativecommons.org/licenses/by/2.5/">
            Creative Commons Attributions 2.5</a> license, so you're pretty much free to
        do whatever you want with it (even use it commercially) provided you keep the links
        in the footer intact. Aside from that, have fun with it :)</p>
    <p>This template is also available as a <a href="http://www.freewpthemes.net/preview/level2/">
        WordPress theme</a> at <a href="http://www.freewpthemes.net/">Free WordPress Themes</a>.</p>
        </Body>
    </Layout:Post>
    <Layout:Post ID="Post2" runat="server" PostDate="Jul 31, 2007"
        Author="Free Css Template">
        <Title>Sample Tags</Title>
        <Body>
            <h3>An H3 Followed by a Blockquote:</h3>
            <blockquote>
                <p>&#8220;Donec leo, vivamus nibh in augue at urna congue rutrum. Quisque dictum integer
                    nisl risus, sagittis convallis, rutrum id, congue, and nibh.&#8221;</p>
            </blockquote>
            <h3>Bulleted List:</h3>
            <asp:BulletedList ID="BulletedList1" runat="server" BulletStyle="Disc" >
                <asp:ListItem>Lorem ipsum dolor sit amet, consectetuer adipiscing elit.</asp:ListItem>
                <asp:ListItem>Phasellus nec erat sit amet nibh pellentesque congue.</asp:ListItem>
                <asp:ListItem>Cras vitae metus aliquam risus pellentesque pharetra.</asp:ListItem>
            </asp:BulletedList>
            <h3>Numbered List:</h3>
            <asp:BulletedList ID="BulletedList2" runat="server" BulletStyle="Numbered" >
                <asp:ListItem>Lorem ipsum dolor sit amet, consectetuer adipiscing elit.</asp:ListItem>
                <asp:ListItem>Phasellus nec erat sit amet nibh pellentesque congue.</asp:ListItem>
                <asp:ListItem>Cras vitae metus aliquam risus pellentesque pharetra.</asp:ListItem>
            </asp:BulletedList>
        </Body>    
    </Layout:Post>
</asp:Content>
