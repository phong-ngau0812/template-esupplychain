<%@ page language="C#" autoeventwireup="true" inherits="QRCode, App_Web_mnfemkl2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           URL <asp:TextBox runat="server" ID="txt" Width="200px"></asp:TextBox>
            <br />
            Tên ảnh <asp:TextBox runat="server" ID="txtTen" Width="200px"></asp:TextBox>
            <asp:Button  runat="server" ID="btnGen" Text="Tạo QR" OnClick="btnGen_Click"/>
            <br />
            Design by Duong2492.dd
        </div>
    </form>
</body>
</html>
