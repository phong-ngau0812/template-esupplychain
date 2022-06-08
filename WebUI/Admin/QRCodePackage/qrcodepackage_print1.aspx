<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qrcodepackage_print1.aspx.cs" Inherits="Admin_QRCodePackage_qrcodepackage_print1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>In mã QR Code - Check VN</title>
    <link rel="shortcut icon" href="/images/logo.png?v=2">
    <link href="~/css/QRCode.css" rel="stylesheet" />
</head>
<body style="background-color: #ffffff; margin: 0px;" onload="window.print()">
    <form id="form1" runat="server">
        <div>
            <div class="detail">
                <div>
                    <asp:Repeater runat="server" ID="rptQRCode">
                        <ItemTemplate>
                            <div class="print_qrcode_item1">
                                <div class="productbrandname">TEM TRUY XUẤT NGUỒN GỐC</div>
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td style="width: 32px;">
                                            <%#Eval("QRCode")%>
                                             <br />
                                           <div class="" style="font-size:8px;margin-top: 0px;"><b><%#Eval("Serial") %></b></div>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 2px; padding-right: 2px">
                                            <div class="" style="font-size: 8px;"><b><%#Eval("Name") %></b></div>
                                            <div class="" style="font-size: 9px; margin-top: 0px;"><b>NĐG: </b><%#Eval("NDG") %></div>
                                            <div class="" style="font-size: 9px; margin-top: 0px;"><b>HSD: </b><%#Eval("HSD") %></div>
                                        </td>
                                    </tr>
                                </table>


                                <div class="" style="height: 10px; margin-top: 0px; text-align: center">
                                    <span id="rptQrCode_ctl00_RadBarcode1" class="RadBarcode" style="display: inline-block; height: 8px; width: 88px;">
                                        <svg xmlns="http://www.w3.org/2000/svg" version="1.1" width="100%" height="100%"></svg></span>
                                </div>
                                <div class="" style="font-size: 10px; margin-top: 0px; text-align: center">
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
        <div class="clear"></div>
    </form>
</body>



</html>
