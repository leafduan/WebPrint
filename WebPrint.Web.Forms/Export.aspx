<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="WebPrint.Web.Forms.Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:TextBox runat="server" ID="txtId" Width="400" Text="Id"></asp:TextBox>
       <br/>
       <asp:TextBox runat="server" ID="txtFileName" Width="400" Text="Filename.xls"></asp:TextBox>
      <br/>
      <br/>
      <asp:Button runat="server" Text="Export" ID="btnExport" 
            onclick="btnExport_Click"/>
    </div>
    </form>
</body>
</html>
