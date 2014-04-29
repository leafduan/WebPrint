<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebPrint.Web.Forms.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="tbTest" runat="server"></asp:TextBox>
        <asp:Label runat="server" ID="lblTest"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Application Preview" />
        <br />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <br />
    <br />
    <div>
        <asp:Button ID="btnTestEmail" runat="server" Text="SendEmail" OnClick="btnTestEmail_Click" />
        <br />
        <asp:Button ID="btnNetPreview" runat="server" Text="TaskManager Preview" OnClick="btnNetPreview_Click" />
    </div>
    <br />
    <div>
        <asp:Image ID="imgPreview" runat="server" />
    </div>
    </form>
</body>
<script>
</script>
</html>
