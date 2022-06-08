<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TruyXuatBH.aspx.cs" Inherits="TruyXuatBH" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng</title>
    <link rel="shortcut icon" href="/favicons/logo.ico">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"
        integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet">
    <link rel="stylesheet" href="/TruyXuatSRC/css/style.css" />
</head>
<!-- Hồ sơ tổ chức,Kiểm định và chứng nhận,Hình ảnh sản phẩm chạy slide bằng carousel -->
<body>
    <form runat="server" id="Form1">
        <%--    <div class="first-alert d-flex align-items-center">
            <img class="alert-logo" src="/images/logo-login.png?v=2" />
            <p class="text2 m-0">
                Bạn đang truy xuất nguồn gốc trên "Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng" hy.check.net.vn
            </p>
            <img class="alert-close" src="/TruyXuatSRC/img/cancel.png" />

        </div>--%>
        <div runat="server" id="Nodata" class="p-3">
            <p class="notify">
                <asp:Literal runat="server" ID="lblTitle" Text="Bảo hiểm chưa được kích hoạt trên hệ thống, vui lòng liên hệ nhà cung cấp"></asp:Literal>
            </p>
        </div>
        <div runat="server" id="Data" class="p-3">
            <h5 class="text-center" style="color: green; line-height: 1.5;">GIẤY CHỨNG NHẬN<br />
                BẢO HIỂM BẮT BUỘC TNDS<br />
                CỦA CHỦ <%=cogioi.ToUpper() %><br />
            </h5>
            <p><b>Số:</b> <%=qrcodecontent %> </p>
            <p><b>CHỦ XE:</b> <%=chuxe %></p>
            <p><b>ĐỊA CHỈ:</b> <%=diachi %></p>
            <p><b>ĐIỆN THOẠI:</b> <%=dienthoai %></p>
            <p><b>SỐ BIỂN KIỂM SOÁT:</b> <%=bks %></p>
            <p><b>SÔ KHUNG:</b>  <%=sokhung %></p>
            <p><b>SỐ MÁY:</b>  <%=somay %></p>
            <p><b>LOẠI XE:</b>  <%=loaixe %></p>
            <p><b>Thời hạn bảo hiểm:</b> <%=thoihan %></p>
            <p><b>Phí bảo hiểm:</b>  <%=phibh %></p>
            <p><b>Cấp ngày GCP:</b> <%=ngaycap %></p>
            <br />
            <p style="color: green; text-align: center;"><b>DOANH NGHIỆP BẢO HIỂM:</b></p>
            <p style="color: green; text-align: center;"><b>TỔNG CÔNG TY BẢO HIỂM BIDV (BIC)</b></p>
            <p><b>Đường dây nóng DNBH:</b> 19004956 </p>
        </div>
        <asp:Label runat="server" ID="lblCode" Style="display: none;" ClientIDMode="Static"></asp:Label>
    </form>
</body>
<script src="/theme/assets/js/jquery.min.js"></script>
<script src="/theme/assets/js/jquery-ui.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"
    integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN"
    crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js"
    integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s"
    crossorigin="anonymous"></script>
<script src="/TruyXuatSRC/js/index.js"></script>

<!-- Required datatable js -->
<script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/theme/assets/pages/jquery.datatable.init.js"></script>
</html>
