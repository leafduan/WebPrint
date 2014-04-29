<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestNHibernate.aspx.cs" Inherits="WebPrint.Web.Forms.TestNHibernate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_Click" Text="Delete All Upc"/>
    <br/>
    <br/>
    <asp:Button runat="server" ID="btnAdd" Text="Add Upc" onclick="btnAdd_Click"/>
    <br/>
    <br/>
    <asp:Button runat="server" ID="btnShow" Text="Show Upc" onclick="btnShow_Click"/>
    
    <br/>
    <br/>
    <asp:Button runat="server" ID="btnLazyLoadTest" Text="Select Ueser" 
            onclick="btnLazyLoadTest_Click"/>
    </div>
    </form>
</body>
</html>
