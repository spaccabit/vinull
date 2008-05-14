<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeyGen.aspx.cs" Inherits="KeyGen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Key Generator</h1>
    PassCode: <asp:TextBox ID="tbCode" runat="server"/> <asp:Button ID="bGen" 
        runat="server" Text="Generate" onclick="bGen_Click" /><br />
    <asp:Label ID="lKey" runat="server"></asp:Label>
    </form>
</body>
</html>
