<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TruyXuat.aspx.cs" Inherits="TruyXuat" %>

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
    <link rel="stylesheet" href="/TruyXuatSRC/css/style.css?v=213" />
    <style>
        .accordion input {
            display: none;
        }

        .red {
            background: red;
        }

        .none1 {
            display: none !important;
        }
    </style>
</head>
<!-- Hồ sơ tổ chức,Kiểm định và chứng nhận,Hình ảnh sản phẩm chạy slide bằng carousel -->
<body>
    <audio style="display: none;" id="bgmusic" autoplay preload controls>
        <source src='/data/audio/<%=audio %>' />
    </audio>
    <script type="text/javascript">
        (function () {
            function log(info) {
                console.log(info);
                // alert(info);
            }
            function forceSafariPlayAudio() {
                audioEl.load(); // iOS 9
                audioEl.play(); // iOS 7/8
            }

            var audioEl = document.getElementById('bgmusic');
            var times = 0;
            // loadstart
            // loadedmetadata
            // loadeddata
            // canplay
            // play
            // playing
            // 
            // iPhone5  iOS 7.0.6 loadstart
            // iPhone6s iOS 9.1   loadstart -> loadedmetadata -> loadeddata -> canplay
            audioEl.addEventListener('loadstart', function () {
                log('loadstart');
            }, false);
            audioEl.addEventListener('loadeddata', function () {
                log('loadeddata');
            }, false);
            audioEl.addEventListener('loadedmetadata', function () {
                log('loadedmetadata');
            }, false);
            audioEl.addEventListener('canplay', function () {
                log('canplay');
            }, false);
            audioEl.addEventListener('play', function () {
                log('play');
                window.removeEventListener('touchstart', forceSafariPlayAudio, false);
            }, false);
            audioEl.addEventListener('playing', function () {
                log('playing');
            }, false);
            audioEl.addEventListener('pause', function () {
                log('pause');
            }, false);

            audioEl.addEventListener('ended', function () {
                times = times + 1
                if (times < 2) {
                    audioEl.pause();
                    audioEl.load();
                    audioEl.play();
                }
            });

            //iOS Safari audio autoplay, click,
            //play audio.
            window.addEventListener('touchstart', forceSafariPlayAudio, false);

        })();
    </script>
    <form runat="server" id="Form1">
        <div class="first-alert d-flex align-items-center <%=red %>">
            <img class="alert-logo" src="<%=logo %>" />
            <p class="text2 m-0">
                <%=message %>
                <%--  Bạn đang truy xuất nguồn gốc trên "Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng của CheckVN"<%-- hy.check.net.vn--%>
            </p>
            <img class="alert-close" src="/TruyXuatSRC/img/cancel.png" />
        </div>
        <%--<div runat="server" id="Nodata" class="p-3">
            <p class="notify">
                <asp:Literal runat="server" ID="lblTitle" Text="Tem chưa được kích hoạt trên hệ thống, vui lòng liên hệ nhà cung cấp"></asp:Literal>
            </p>
        </div>--%>
        <div runat="server" id="Data">
            <section class="section1">
                <div id="slide-header" class="carousel slide" data-ride="carousel" data-interval="false">
                    <div class="carousel-inner">
                        <asp:Repeater runat="server" ID="rptGallery">
                            <ItemTemplate>
                                <div style="display: none"><%#Eval("STT")%></div>
                                <div class="carousel-item <%#Eval("STT").ToString()=="1"?"active":"" %>">
                                    <img src="<%#Eval("Name") %>" class="d-block w-100" alt="...">
                                    <div class="page d-flex">
                                        <p class="text4 m-0"><%#Eval("STT") %>/<%=dtFlag.Rows.Count %></p>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
                        <%--  <div class="carousel-item active" runat="server" id="ImgThumbnail">
                            <img src="<%=Image%>" class="d-block w-100" alt="...">
                            <div class="page d-flex">
                                <p class="text4 m-0">1/1</p>
                            </div>
                        </div>--%>

                        <%--  <div class="carousel-item">
                    <img src="/TruyXuatSRC/img/product.png" class="d-block w-100" alt="...">
                    <div class="page d-flex">
                        <p class="text4 m-0">2/3</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img src="/TruyXuatSRC/img/product.png" class="d-block w-100" alt="...">
                    <div class="page d-flex">
                        <p class="text4 m-0">3/3</p>
                    </div>
                </div>--%>
                        <div class="share d-flex">
                            <i class="material-icons" style="color: #999999">share</i>
                        </div>
                    </div>
                    <a class="carousel-control-prev" href="#slide-header" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#slide-header" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
                <div class="content1">
                    <div class="content1-border d-flex justify-content-between">
                        <div>
                            <p class="text-title"><%=ProductName %></p>
                            <p class="text1"><%=SGTIN%></p>
                            <div class="d-flex content1-price align-items-end">
                                <p class="text1"><%=PriceOld %></p>
                                <div class="content1-old-price">
                                    <p class="text1"><%=Price %></p>
                                    <div class="line"></div>
                                </div>
                                <div class="content1-sale">
                                    <p class="text">Giảm giá: <%=discount %></p>
                                </div>
                            </div>
                            <div class="d-flex align-items-end rate-box">
                                <asp:Literal runat="server" ID="lblStar"></asp:Literal>

                                <p class="text1">( <%=review %> lượt đánh giá )</p>
                            </div>
                        </div>
                        <asp:Literal runat="server" ID="lblQR"></asp:Literal>
                        <%--<img class="qr-code" src="/TruyXuatSRC/img/qr.jpg"></img>--%>
                    </div>

                </div>
                <div class="logo1 d-flex">
                    <asp:Repeater runat="server" ID="rptQuality">
                        <ItemTemplate>
                            <div>
                                <div class="logo1-border d-flex align-items-center">
                                    <img src="<%#Eval("Image") %>" height="30px" />
                                    <p class="text"><%#Eval("Name") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <div>
                <div class="logo1-border d-flex align-items-center">
                    <img src="/TruyXuatSRC/img/logoocop.png" />
                    <p class="text">Sản phẩm 4 sao</p>
                </div>
            </div>
            <div>
                <div class="logo1-border">
                    <img src="/TruyXuatSRC/img/logovietgap2.png" />
                </div>
            </div>--%>
                </div>

            </section>
            <section class="section2">
                <p class="text1">Cam kết chính hãng bởi</p>
                <div class="d-flex justify-content-between">
                    <div class="d-flex">
                        <img class="logo-cp" src="<%=LogoBrand %>" />
                        <div class="row1">
                            <p class=" text3"><%=ProductBrandName %></p>
                            <p class=" text1"><%=SoSao %></p>
                            <%--     <div class="logo-small">
                        <img src="/TruyXuatSRC/img/logogap.png"></img>
                        <img src="/TruyXuatSRC/img/logovietgap.png"></img>
                        <img src="/TruyXuatSRC/img/logoiso.png"></img>
                    </div>--%>
                        </div>
                    </div>
                    <%--   <div>
                <img src="/TruyXuatSRC/img/go.png" />
            </div>--%>
                </div>
            </section>
            <section class="section3">
                <ul class="d-flex menu3 d-flex justify-content-around p-0 m-0">
                    <li class="menu3-son son1 active-on col-4 d-flex justify-content-center">
                        <div class="text2">SẢN PHẨM</div>
                    </li>
                    <li class="menu3-son son2 col-4 d-flex justify-content-center <%=none%>">
                        <div class="text2">NGUỒN GỐC</div>
                    </li>

                    <li class="menu3-son son3 col-4 d-flex justify-content-center">
                        <div class="text2">THƯƠNG HIỆU</div>
                    </li>
                </ul>

                <div class="content-history">
                    <div class="detail">
                        <div class="title">
                            Mã truy xuất
                        </div>
                        <div style="width: 100%; float: left;">
                            <b>- Tên sản phẩm:</b> <%=ProductName %>
                        </div>
                        <div style="width: 100%; float: left;">
                            <b>- Thông tin sản phẩm:</b>
                        </div>
                        <div style="width: 100%; float: left;">
                            <b>&emsp;+ Lô sản xuất:</b> <%=tenlo %>
                        </div>
                        <div style="width: 100%; float: left;" runat="server" id="xuanhoa">
                            <b>&emsp;+ Mã PO:</b> <%=PO %>
                        </div>
                        <div style="width: 100%; float: left;">
                            <b>- Thông tin sơ chế và đóng gói thành phẩm:</b>
                            <br />
                            <%=supplier %>
                        </div>
                        <br />

                        <div class="title">
                            Thông tin đơn vị/ nhà sản xuất
                        </div>
                        <div style="width: 70%; float: left;">
                            <b>- Tên đơn vị/ nhà sản xuất: </b><%=ProductBrandName %><br />
                            <b>- Địa chỉ: </b><%=DiaChi %><br />
                            <b>- Số điện thoại: </b><%=Phone %><br />
                            <b>- Website: <%=Web %></b><br />

                        </div>
                        <div style="width: 25%; float: left; text-align: center;">
                            <img class="logo-cp" src="<%=LogoBrand %>" /><br />
                        </div>
                        <br />
                        <div class="title">
                            THÔNG TIN VÙNG NGUYÊN LIỆU
                        </div>
                        <asp:Repeater runat="server" ID="rptHistory" OnItemDataBound="rptHistory_ItemDataBound">
                            <ItemTemplate>
                                <div style="width: 100%; float: left; margin-bottom: 10px;">
                                    <b><%=NoHistory++ %>. Lô sản xuất:</b> <%#Eval("SGTIN") %> | <%#Eval("ProductPackageName") %> - <%#Eval("ProductBrandName") %>
                                    <br />
                                    <b>- Thông tin vùng trồng: </b>
                                    <br />
                                    <b>&emsp;+ Vùng sản xuất: </b><%#Eval("VungTrong") %>
                                    <br />
                                    <b>&emsp;+ Địa chỉ: </b><%#Eval("DiaChi") %>
                                    <br />
                                    <b>&emsp;+ Chứng nhận: </b><%#Eval("ChungNhan") %>
                                </div>

                                <asp:Literal runat="server" ID="lblProductPackage_ID" Visible="false" Text='<%#Eval("ProductPackage_ID") %>'></asp:Literal>
                                <div class="accordion" id="accordionExample<%#Eval("ProductPackage_ID") %>">
                                    <div class="card">
                                        <div class="card-header" id="headingOne<%#Eval("ProductPackage_ID") %>">
                                            <h2 class="mb-0">
                                                <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne<%#Eval("ProductPackage_ID") %>"><i class="fa fa-plus"></i>NHẬT KÝ SẢN XUẤT</button>
                                            </h2>
                                        </div>
                                        <div id="collapseOne<%#Eval("ProductPackage_ID") %>" class="collapse" aria-labelledby="headingOne<%#Eval("ProductPackage_ID") %>" data-parent="#accordionExample<%#Eval("ProductPackage_ID") %>">
                                            <div class="card-body">
                                                <asp:HiddenField runat="server" ID="productinfo_id" ClientIDMode="Static" />
                                                <asp:Repeater runat="server" ID="rptSX" OnItemDataBound="rptSX_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table style='width: 100%;'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='col-left'>Đề mục công việc: </td>
                                                                    <td><b><%#Eval("Name") %></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Thời gian: </td>
                                                                    <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class='col-left'>Vị trí: </td>
                                                                    <td><%#Eval("Location") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Nội dung: </td>
                                                                    <td><%#Eval("Description") %>
                                                                        <asp:Literal runat="server" ID="lblTaskStep_ID" Text='<%#Eval("TaskStep_ID") %>' Visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lblTask_ID" Text='<%#Eval("Task_ID") %>' Visible="false"></asp:Literal>
                                                                        <div style="display: none"><%#NoQuestion=1 %>  <%=NoQuestion=1 %></div>
                                                                        <asp:Repeater runat="server" ID="rptQuestion" OnItemDataBound="rptQuestion_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <b><%=NoQuestion++ %>. <%#Eval("Name") %>
                                                                                    <asp:Literal runat="server" ID="lblQuestionID" Text='<%#Eval("TaskStepQuestion_ID") %>' Visible="false"></asp:Literal>
                                                                                </b>
                                                                                <br />
                                                                                <asp:CheckBoxList runat="server" ID="ckAnswer">
                                                                                </asp:CheckBoxList>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <br />
                                                                        <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card">
                                        <div class="card-header" id="headingThree<%#Eval("ProductPackage_ID") %>">
                                            <h2 class="mb-0">
                                                <button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree<%#Eval("ProductPackage_ID") %>"><i class="fa fa-plus"></i>NHẬT KÝ THU HOẠCH</button>
                                            </h2>
                                        </div>
                                        <div id="collapseThree<%#Eval("ProductPackage_ID") %>" class="collapse" aria-labelledby="headingThree<%#Eval("ProductPackage_ID") %>" data-parent="#accordionExample<%#Eval("ProductPackage_ID") %>">
                                            <div class="card-body">
                                                <asp:Repeater runat="server" ID="rptTaskHistoryTH">
                                                    <ItemTemplate>
                                                        <table style='width: 100%;'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='col-left'>Đầu mục công việc: </td>
                                                                    <td><b><%#Eval("Name") %></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Thời gian: </td>
                                                                    <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Số lượng: </td>
                                                                    <td><%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%> <%#Eval("Unit") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Nội dung: </td>
                                                                    <td><%#Eval("Description") %>
                                                                        <br />
                                                                        <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-header" id="headingFour<%#Eval("ProductPackage_ID") %>">
                                            <h2 class="mb-0">
                                                <button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseFour<%#Eval("ProductPackage_ID") %>"><i class="fa fa-plus"></i>NHẬT KÝ SƠ CHẾ CHẾ BIẾN</button>
                                            </h2>
                                        </div>
                                        <div id="collapseFour<%#Eval("ProductPackage_ID") %>" class="collapse" aria-labelledby="headingFour<%#Eval("ProductPackage_ID") %>" data-parent="#accordionExample<%#Eval("ProductPackage_ID") %>">
                                            <div class="card-body">
                                                <asp:Repeater runat="server" ID="rptTaskHistoryCB">
                                                    <ItemTemplate>
                                                        <table style='width: 100%;'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='col-left'>Đề mục công việc: </td>
                                                                    <td><b><%#Eval("Name") %></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Thời gian: </td>
                                                                    <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class='col-left'>Vị trí: </td>
                                                                    <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class='col-left'>Nội dung: </td>
                                                                    <td><%#Eval("Description") %>
                                                                        <br />
                                                                        <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="title">
                            THÔNG TIN SẢN XUẤT, SƠ CHẾ - CHẾ BIẾN
                        </div>
                        <div style="width: 100%; float: left; margin-bottom: 10px;">
                            <b>- Lô sản xuất :</b><%=losx %>
                            <br />
                            <b>- Tên đơn vị/ nhà sản xuất: </b><%=ProductBrandName %><br />
                            <b>- Địa chỉ: </b><%=DiaChi %><br />
                        </div>
                        <div class="col-12" style="width: 100%; margin-bottom: 10px; text-align: center">
                            <div class="btn-primary custom-btn" id="btn-update" style="width: 50%; float: right; height: 30px">
                                Cập nhật nhật ký sản xuất
                            </div>
                            <%-- <input type="button" class="btn-primary custom-btn" value="<%#Eval("DiaChi")%>" onclick="dosomething(this.value)" /> --%>
                        </div>
                        <div style="width: 100%; float: left; margin-bottom: 10px;">
                            <br />
                            <div class="accordion" id="accordionExample">
                                <div class="card <%=noneSX %>">
                                    <div class="card-header" id="headingOne">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne"><i class="fa fa-plus"></i>NHẬT KÝ SẢN XUẤT</button>
                                        </h2>
                                    </div>
                                    <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                        <div class="card-body">
                                            <asp:HiddenField runat="server" ID="productinfo_id" ClientIDMode="Static" />
                                            <asp:Repeater runat="server" ID="rptSX" OnItemDataBound="rptSX_ItemDataBound">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Đề mục công việc: </td>
                                                                <td><b><%#Eval("Name") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>

                                                            <tr>
                                                                <td class='col-left'>Vị trí: </td>
                                                                <td><%#Eval("Location") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Nội dung: </td>
                                                                <td><%#Eval("Description") %>
                                                                    <asp:Literal runat="server" ID="lblTaskStep_ID" Text='<%#Eval("TaskStep_ID") %>' Visible="false"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblTask_ID" Text='<%#Eval("Task_ID") %>' Visible="false"></asp:Literal>
                                                                    <div style="display: none"><%#NoQuestion=1 %>  <%=NoQuestion=1 %></div>
                                                                    <asp:Repeater runat="server" ID="rptQuestion" OnItemDataBound="rptQuestion_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <b><%=NoQuestion++ %>. <%#Eval("Name") %>
                                                                                <asp:Literal runat="server" ID="lblQuestionID" Text='<%#Eval("TaskStepQuestion_ID") %>' Visible="false"></asp:Literal>
                                                                            </b>
                                                                            <br />
                                                                            <asp:CheckBoxList runat="server" ID="ckAnswer">
                                                                            </asp:CheckBoxList>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <br />
                                                                    <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="card <%=noneVattu %>">
                                    <div class="card-header" id="headingTwo">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseTwo"><i class="fa fa-plus"></i>NHẬT KÝ VẬT TƯ</button>
                                        </h2>
                                    </div>
                                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                        <div class="card-body">
                                            <asp:Repeater runat="server" ID="rptVT">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Tên vật tư: </td>
                                                                <td><b><%#Eval("Name") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Số lượng: </td>
                                                                <td><%#decimal.Parse(Eval("Quantity").ToString())%> <%#Eval("Unit") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Vị trí: </td>

                                                                <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Nội dung: </td>
                                                                <td><%#Eval("Description") %>
                                                                    <br />
                                                                    <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="card <%=noneThuhoach %>">
                                    <div class="card-header" id="headingThree">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree"><i class="fa fa-plus"></i>NHẬT KÝ THU HOẠCH</button>
                                        </h2>
                                    </div>
                                    <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                                        <div class="card-body">
                                            <asp:Repeater runat="server" ID="rptTaskHistoryTH">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Đầu mục công việc: </td>
                                                                <td><b><%#Eval("Name") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Số lượng: </td>
                                                                <td><%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%> <%#Eval("Unit") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Nội dung: </td>
                                                                <td><%#Eval("Description") %>
                                                                    <br />
                                                                    <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="card <%=noneCheBien %>">
                                    <div class="card-header" id="headingFour">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseFour"><i class="fa fa-plus"></i>NHẬT KÝ SƠ CHẾ CHẾ BIẾN</button>
                                        </h2>
                                    </div>
                                    <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordionExample">
                                        <div class="card-body">
                                            <asp:Repeater runat="server" ID="rptTaskHistoryCB">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Đề mục công việc: </td>
                                                                <td><b><%#Eval("Name") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>

                                                            <tr>
                                                                <td class='col-left'>Vị trí: </td>
                                                                <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Nội dung: </td>
                                                                <td><%#Eval("Description") %>
                                                                    <br />
                                                                    <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div style="width: 100%; float: left;">
                            <div class="title">
                                THÔNG TIN VẬN CHUYỂN
                            </div>
                            <%--    <b>- Tên đơn vị/ nhà sản xuất: </b><%=ProductBrandName %><br />
                            <b>- Địa chỉ: </b><%=DiaChi %><br />
                            <br />--%>
                            <b>Danh sách nhà vận chuyển: </b>
                            <br />

                            <asp:Repeater runat="server" ID="rptVanChuyen">
                                <ItemTemplate>
                                    <b><%=NoTransport++ %>. <%#Eval("Name") %> </b>
                                    <br />
                                    <b>&emsp;Địa chỉ: </b><%#Eval("Address") %><br />
                                    <b>&emsp;Số điện thoại: </b><%#Eval("Phone") %>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />

                            <div class="accordion" id="accordionTransport">
                                <div class="card  <%=noneVanchuyen %>">
                                    <div class="card-header" id="headingTransport">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseTransport"><i class="fa fa-plus"></i>NHẬT KÝ VẬN CHUYỂN</button>
                                        </h2>
                                    </div>
                                    <div id="collapseTransport" class="collapse" aria-labelledby="headingTransport" data-parent="#accordionTransport">
                                        <div class="card-body">
                                            <asp:Repeater runat="server" ID="rptVC">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Nhà vận chuyển: </td>
                                                                <td><b><%#Eval("NhaVC") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Nội dung: </td>
                                                                <td><b><%#Eval("Description") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>

                                                            <tr>
                                                                <td class='col-left'>Điểm đi: </td>
                                                                <td><%#Eval("StartingPoint") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Điểm đến: </td>
                                                                <td><%#Eval("Destination") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Người nhận: </td>
                                                                <td><%#Eval("BuyerName") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Địa chỉ người nhận: </td>
                                                                <td><%#Eval("ShopAddress") %>
                                                                  
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div style="width: 100%; float: left; display: none;">
                            <div class="title">
                                THÔNG TIN PHÂN PHỐI
                            </div>
                            <b>Danh sách nhà phân phối: </b>
                            <br />

                            <asp:Repeater runat="server" ID="rptPP">
                                <ItemTemplate>
                                    <b><%=NoPP++ %>. <%#Eval("Name") %> </b>
                                    <br />
                                    <b>&emsp;Địa chỉ: </b><%#Eval("Address") %><br />
                                    <b>&emsp;Số điện thoại: </b><%#Eval("Phone") %>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                            <div class="accordion" id="accordionPhanPhoi">
                                <div class="card">
                                    <div class="card-header" id="headingPhanPhoi">
                                        <h2 class="mb-0">
                                            <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapsePhanPhoi"><i class="fa fa-plus"></i>NHẬT KÝ PHÂN PHỐI</button>
                                        </h2>
                                    </div>
                                    <div id="collapsePhanPhoi" class="collapse" aria-labelledby="headingPhanPhoi" data-parent="#accordionPhanPhoi">
                                        <div class="card-body">
                                            <asp:Repeater runat="server" ID="rptPhanPhoi">
                                                <ItemTemplate>
                                                    <table style='width: 100%;'>
                                                        <tbody>
                                                            <tr>
                                                                <td class='col-left'>Tên khách hàng: </td>
                                                                <td><b><%#Eval("TenKhach") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Đỉa chỉ: </td>
                                                                <td><b><%#Eval("ShopAddress") %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Đại diện bên mua: </td>
                                                                <td><%#Eval("BuyerName") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class='col-left'>Thời gian: </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            </tr>

                                                        </tbody>
                                                    </table>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content-product  active-show">
                    <div class="detail">
                        <div style="width: 100%; float: left;">
                            <b>- Ngày Sản Xuất:</b> <%=nsx %>
                        </div>
                        <div style="width: 100%; float: left;">
                            <b>- Hạn Sử Dụng:</b> <%=hsd %>
                            <br />

                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                    <div class="detail">
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Thông tin sản phẩm</p>
                            </div>
                            <%-- <div class="d-flex show-more align-items-center">
                            <p class="text m-0">Xem thêm</p>
                            <img class="show-img" src="/TruyXuatSRC/img/go2.png" />
                        </div>--%>
                        </div>
                        <div>
                            <p class="text">
                                <%=ProductInfo %>
                            </p>
                        </div>

                    </div>
                    <div class="detail">
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Quy trình</p>
                            </div>
                            <%--  <div class="d-flex show-more align-items-center">
                            <p class="text m-0">Xem thêm</p>
                            <img class="show-img" src="/TruyXuatSRC/img/go2.png" />
                        </div>--%>
                        </div>
                        <div>
                            <p class="text">
                                <%=ProductQT %>
                            </p>
                        </div>

                    </div>
                    <div class="detail">
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Câu chuyện sản phẩm</p>
                            </div>
                            <%--  <div class="d-flex show-more align-items-center">
                            <p class="text m-0">Xem thêm</p>
                            <img class="show-img" src="/TruyXuatSRC/img/go2.png" />
                        </div>--%>
                        </div>
                        <div>
                            <p class="text">
                                <%=storysp %>
                            </p>
                        </div>

                    </div>
                    <div class="detail">
                        <div class="d-flex detail-title">
                            <div class="h-100">
                                <p class="text4 m-0">Kiểm định và chứng nhận</p>
                            </div>
                        </div>
                        <div class="detail-column detail-border">
                            <p class="text">Bản công bố sản phẩm</p>
                            <div id="slide-product-1" class="carousel slide" data-ride="carousel">
                                <div class="carousel-inner">
                                    <asp:Repeater runat="server" ID="rptCongBoSP">
                                        <ItemTemplate>
                                            <div style="display: none"><%=NoCB++ %></div>
                                            <div class="carousel-item <%=NoCB==1?"active":"" %>">
                                                <div class="d-flex">
                                                    <div class="col-12 accreditation">
                                                        <img class="img-fluid"
                                                            src="<%#"/data/product/product_info/"+ Eval("Image") %>"></img>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <a class="carousel-control-prev" href="#slide-product-1" role="button" data-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="carousel-control-next" href="#slide-product-1" role="button" data-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                        </div>
                        <div class="detail-column detail-border">
                            <p class="text">Phiếu kiểm nghiệm</p>

                            <div id="slide-product-2" class="carousel slide" data-ride="carousel">
                                <div class="carousel-inner">
                                    <asp:Repeater runat="server" ID="rptKN">
                                        <ItemTemplate>
                                            <div style="display: none"><%=NoKN++ %></div>
                                            <div class="carousel-item <%=NoKN==1?"active":"" %>">
                                                <div class="d-flex">
                                                    <div class="col-12 accreditation">
                                                        <img class="img-fluid"
                                                            src="<%#"/data/product/product_info/"+ Eval("Image") %>"></img>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <a class="carousel-control-prev" href="#slide-product-2" role="button" data-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="carousel-control-next" href="#slide-product-2" role="button" data-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                        </div>
                        <div class="detail-column detail-border">
                            <p class="text">Đối tác</p>
                            <div id="slide-product-4" class="carousel slide" data-ride="carousel">
                                <div class="carousel-inner">
                                    <asp:Repeater runat="server" ID="rptDT">
                                        <ItemTemplate>
                                            <div style="display: none"><%=NoDT++ %></div>
                                            <div class="carousel-item <%=NoDT==1?"active":"" %>">
                                                <div class="d-flex">
                                                    <div class="col-12 accreditation">
                                                        <img class="img-fluid"
                                                            src="<%#"/data/product/product_info/"+ Eval("Image") %>"></img>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <a class="carousel-control-prev" href="#slide-product-4" role="button" data-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="carousel-control-next" href="#slide-product-4" role="button" data-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                        </div>

                    </div>
                    <div class="detail2">
                        <div class="d-flex detail-title2">
                            <div class="h-100">
                                <p class="text4 m-0">Video</p>
                            </div>
                        </div>
                        <%=video %>
                        <%--<div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="<%=video %>" allowfullscreen></iframe>
                    </div>--%>
                    </div>

                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Đánh giá sản phẩm</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <div class="rating">
                                            <input type="radio" name="rating" value="5" id="5"><label for="5">☆</label>
                                            <input type="radio" name="rating" value="4" id="4" checked><label for="4">☆</label>
                                            <input type="radio" name="rating" value="3" id="3"><label for="3">☆</label>
                                            <input type="radio" name="rating" value="2" id="2"><label for="2">☆</label>
                                            <input type="radio" name="rating" value="1" id="1"><label for="1">☆</label>
                                        </div>
                                        <label for="exampleFormControlInput1">Họ tên</label>
                                        <input class="form-control" id="txtName">
                                    </div>
                                    <div class="form-group">
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleFormControlInput1">Nội dung</label>
                                        <textarea class="form-control" id="txtContent" rows="3"></textarea>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="SaveInfo();">Gửi đánh giá</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel2">Đánh giá chỉ số tin cậy sản phẩm</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <div class="rating">
                                            <input type="radio" name="rating2" value="5" id="5"><label for="5">☆</label>
                                            <input type="radio" name="rating2" value="4" id="4" checked><label for="4">☆</label>
                                            <input type="radio" name="rating2" value="3" id="3"><label for="3">☆</label>
                                            <input type="radio" name="rating2" value="2" id="2"><label for="2">☆</label>
                                            <input type="radio" name="rating2" value="1" id="1"><label for="1">☆</label>
                                        </div>
                                        <label for="exampleFormControlInput1">Họ tên</label>
                                        <input class="form-control" id="txtName2">
                                    </div>
                                    <div class="form-group">
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleFormControlInput1">Nội dung</label>
                                        <textarea class="form-control" id="txtContent2" rows="3"></textarea>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" onclick="SaveInfo2();">Gửi đánh giá</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="detail detail-bottom-none">
                        <div class="d-flex detail-title">
                            <div class="h-100">
                                <p class="text4 m-0">Đánh giá sản phẩm (<%=review %> nhận xét)</p>
                            </div>
                        </div>

                        <div class="d-flex">
                            <textarea class="form-control" id="Comment" rows="3"></textarea>
                        </div>

                        <div class="text-right">
                            <button type="button" class="btn btn-primary" id="btnSend" data-toggle="modal" data-target="#exampleModal">Nhận xét</button>
                        </div>
                        <br />
                        <div class="accordion accordion-flush" id="accordionDanhgiaCS">
                            <div class="card">
                                <div class="card-header" id="headingDanhgiaCS">
                                    <h2 class="mb-0">
                                        <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseDanhgiaCS"><i class="fa fa-plus"></i>ĐÁNH GIÁ CHỈ SỔ TIN CẬY</button>
                                    </h2>
                                </div>
                                <div id="collapseDanhgiaCS" class="collapse" aria-labelledby="headingPhanPhoi" data-parent="#accordionDanhgiaCS">
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <textarea class="form-control" id="Comment2" rows="3"></textarea>
                                        </div>

                                        <div class="text-right">
                                            <button type="button" class="btn btn-primary" id="btnSend2" data-toggle="modal" data-target="#exampleModal2">Nhận xét</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="review-column-box">
                            <div class="row review-column">
                                <table class="table-responsive" id="datatableNoSort">
                                    <thead style="display: none">
                                        <tr>
                                            <th>Avatar</th>
                                            <th>Nội dung </th>
                                            <%--<th>Ngày </th>--%>
                                        </tr>
                                    </thead>
                                    <asp:Repeater runat="server" ID="rptReview">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class="name-box d-flex justify-content-start align-items-centerp-0">
                                                        <p class="text3 m-0"><%#Eval("FullName").ToString().Substring(0, 1) %></p>
                                                    </div>
                                                </td>
                                                <td class="col-12">
                                                    <div class="reviewp-0">
                                                        <div>
                                                            <small><%#string.IsNullOrEmpty(Eval("Type").ToString())? "" : Eval("Type").ToString() == "1"? "(Đánh giá chỉ số tin cậy)" : "(Đánh giá sản phẩm)"%></small>
                                                            <small><%# DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %></small>
                                                            | <%#Eval("Sao") %>
                                                        </div>
                                                        <p class="text-title m-0"><%#Eval("FullName") %></p>
                                                        <p class="text m-0"><%#Eval("Description") %></p>
                                                    </div>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>

                        <br />
                        <div class="relate">
                            <ul class="d-flex menu d-flex justify-content-around p-0 m-0">
                                <div class="menu-son show1 col-12 p-0 d-flex justify-content-center">
                                    <li class="text2 menu-son-li1 menu-color">Sản phẩm cùng thương hiệu</li>
                                    <div class="line-hover line-hover1 active-show"></div>
                                </div>
                                <%--   <div class="menu-son show2 col-6 p-0 d-flex justify-content-center">
                            <li class="text2 menu-son-li2 ">Liên quan</li>
                            <div class="line-hover line-hover2"></div>
                        </div>--%>
                            </ul>
                            <div class="product-list">
                                <div class="d-flex row m-0">
                                    <asp:Repeater runat="server" ID="rptProduct">
                                        <ItemTemplate>
                                            <div class="col-4 product">
                                                <a href='/p/<%#Eval("Product_ID") %>'>
                                                    <img class="img-fluid" src='<%# Common.GetImg(Eval("Image"))%>' /></a>
                                                <p class="text1 m-0"><%#Eval("Name").ToString()%></p>
                                                <p class="text m-0"><%#Eval("GiaBan").ToString() =="0.0000"?"": Double.Parse(Eval("GiaBan").ToString()).ToString("N0")  %></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="content-trademark">
                    <div class="detail">
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Thông tin chung</p>
                            </div>
                        </div>
                        <div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconname.png" />
                                    </div>
                                    <p class="text mb-0">Tên giao dịch:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=TenGiaoDich %></p>
                                </div>
                            </div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconlocation.png" />
                                    </div>
                                    <p class="text mb-0">Địa chỉ:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=DiaChi %></p>
                                </div>
                            </div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconphone.png" />
                                    </div>
                                    <p class="text mb-0">Số điện thoại:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=Phone %></p>
                                </div>
                            </div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconweb.png" />
                                    </div>
                                    <p class="text mb-0">Website:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><a href="<%=Web %>"><%=Web %></a></p>
                                </div>
                            </div>

                            <br />
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconlink.png" />
                                    </div>
                                    <p class="text mb-0">Chuỗi liên kết:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=Chuoi %></p>
                                </div>
                            </div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconvaitro.png" />
                                    </div>
                                    <p class="text mb-0">Vai trò trong chuỗi:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=Vaitro %></p>
                                </div>
                            </div>
                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-end about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/iconlhdn.png" />
                                    </div>
                                    <p class="text mb-0">Loại hình DN:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=LoaihinhDN %></p>
                                </div>
                            </div>

                            <div class="d-flex about">
                                <div class="col-4 d-flex p-0 align-items-start about-title">
                                    <div>
                                        <img class="img-fluid" src="/TruyXuatSRC/img/icondn.png" />
                                    </div>
                                    <p class="text mb-0">Các đối tác của DN:</p>
                                </div>
                                <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                    <p class="text mb-0"><%=DoitacDN %></p>
                                </div>
                            </div>

                            <%-- <div class="standard d-flex justify-content-between">
                        <img src="/TruyXuatSRC/img/logocp.png" />
                        <img src="/TruyXuatSRC/img/logocp.png" />
                        <img src="/TruyXuatSRC/img/logocp.png" />
                        <img src="/TruyXuatSRC/img/logocp.png" />
                        <img src="/TruyXuatSRC/img/logocp.png" />
                    </div>--%>
                        </div>
                        <br />
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Câu chuyện thương hiệu</p>
                            </div>
                        </div>
                        <div class="d-flex about">
                            <div class="text mb-0">
                                <%=storybrand %>
                            </div>
                        </div>
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Truyền thông quảng bá</p>
                            </div>
                        </div>
                        <div class="d-flex about">
                            <div class="text mb-0">
                                <%=prinfo %>
                            </div>
                        </div>
                        <div class="d-flex detail-title justify-content-between">
                            <div class="h-100">
                                <p class="text4 m-0">Hồ sơ doanh nghiệp</p>
                            </div>
                        </div>
                        <div class="d-flex about">
                            <div class="text mb-0">
                                <%=storebrand %>
                            </div>
                        </div>
                    </div>

                </div>

            </section>
            <br />
        </div>

        <asp:HiddenField ID="lblLocation" runat="server" ClientIDMode="Static" />
        <asp:Label runat="server" ID="lblCode" Style="display: none;" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField runat="server" ID="lblTrackingID" Value="0" ClientIDMode="Static" />
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
<script>
    $("#btnSend").click(function () {
        $("#txtContent").val($("#Comment").val());
    });
    $("#btn-update").click(function () {
        if (($("#lblCode").html()).length > 0) {
            window.location.replace("/?ReturnUrl=" + $("#lblCode").html());
        }
    });
    //function dosomething(val) {
    //    console.log(val);
    //}
    function SaveInfo() {
        var name = $("#txtName").val();
        var content = $("#txtContent").val();
        var star = $('input[name=rating]:checked').val();
        var productinfo_id = $("#productinfo_id").val();
        var type = 0;
        if (name.length < 1) {
            alert('Vui lòng nhập đầy đủ họ tên trước khi bình luận !')
            $("#txtName").focus();
        }
        else {
            // PushCM();
            console.log(star);
            var _tblCM = {
                productinfo_id: productinfo_id,
                name: name,
                description: content,
                star: star,
                type: type
            };
            $.ajax({
                url: "/WebServices/Product.asmx/PushCM",
                data: JSON.stringify(_tblCM),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                //processData: false,
                dataType: "json",
                success: function (result) {
                    if (result.d > 0) {
                        // console.log(result.d);
                        $("#txtName").val("");
                        $("#txtContent").val("");
                        $("#Comment").val("");
                        alert('Cảm ơn bạn đã gửi bình luận, chúng tôi sẽ kiểm duyệt bình luận của bạn trong vòng 24h!');
                    }
                },
                error: function (errormessage) {
                    console.log("err", "Có lỗi xảy ra, vui lòng thử lại sau.!", 3000);
                }
            });
        }
    }
    $("#btnSend2").click(function () {
        $("#txtContent2").val($("#Comment2").val());
    });

    function SaveInfo2() {
        var name = $("#txtName2").val();
        var content = $("#txtContent2").val();
        var star = $('input[name=rating2]:checked').val();
        var productinfo_id = $("#productinfo_id").val();
        var type = 1;
        if (name.length < 1) {
            alert('Vui lòng nhập đầy đủ họ tên trước khi bình luận !')
            $("#txtName").focus();
        }
        else {
            // PushCM();
            console.log(star);
            var _tblCM = {
                productinfo_id: productinfo_id,
                name: name,
                description: content,
                star: star,
                type: type
            };
            $.ajax({
                url: "/WebServices/Product.asmx/PushCM",
                data: JSON.stringify(_tblCM),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                //processData: false,
                dataType: "json",
                success: function (result) {
                    if (result.d > 0) {
                        // console.log(result.d);
                        $("#txtName").val("");
                        $("#txtContent").val("");
                        $("#Comment").val("");
                        alert('Cảm ơn bạn đã gửi bình luận, chúng tôi sẽ kiểm duyệt bình luận của bạn trong vòng 24h!');
                    }
                },
                error: function (errormessage) {
                    console.log("err", "Có lỗi xảy ra, vui lòng thử lại sau.!", 3000);
                }
            });
        }
    }
</script>
<script>
    $(document).ready(function () {
        // Add minus icon for collapse element which is open by default
        $(".collapse.show").each(function () {
            $(this).prev(".card-header").find(".fa").addClass("fa-minus").removeClass("fa-plus");
        });

        // Toggle plus minus icon on show hide of collapse element
        $(".collapse").on('show.bs.collapse', function () {
            $(this).prev(".card-header").find(".fa").removeClass("fa-plus").addClass("fa-minus");
        }).on('hide.bs.collapse', function () {
            $(this).prev(".card-header").find(".fa").removeClass("fa-minus").addClass("fa-plus");
        });
    });
</script>
<style>
    .content-history span input {
        display: none;
    }

    div#datatableNoSort_paginate {
        text-align: right;
        float: right;
    }

    div#datatableNoSort_info {
        visibility: hidden;
    }

    div#datatableNoSort_length {
        display: none;
    }

    div#datatableNoSort_filter {
        display: none;
    }

    .reviewp-0 {
        padding: 10px 0px;
        max-width: 280px;
    }

    div#datatableNoSort_paginate {
        text-align: right;
        float: right;
        font-size: 12px;
    }

    .rating {
        display: flex;
        flex-direction: row-reverse;
        justify-content: center
    }

        .rating > input {
            display: none
        }

        .rating > label {
            position: relative;
            width: 1em;
            font-size: 6vw;
            color: #FFD600;
            cursor: pointer
        }

            .rating > label::before {
                content: "\2605";
                position: absolute;
                opacity: 0
            }

            .rating > label:hover:before,
            .rating > label:hover ~ label:before {
                opacity: 1 !important
            }

        .rating > input:checked ~ label:before {
            opacity: 1
        }

        .rating:hover > input:checked ~ label:before {
            opacity: 0.4
        }

    span.none {
        display: none;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        getGeo();
    });
    var getGeo = function () {
        navigator.geolocation.getCurrentPosition(success, error, { maximumAge: 600000, timeout: 10000 });
    }

    function success(position) {
        var lng = position.coords.longitude;
        var lat = position.coords.latitude;
        $.ajax({
            url: "/WebServices/GetLocation.asmx/GetAddressLocation",
            data: {
                Latitude: "'" + lat + "'",
                Longitude: "'" + lng + "'"
            },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != "") {
                    console.log(result.d);
                    var data = {
                        Address: result.d,
                        QRCodeTrackingID: parseInt($("#lblTrackingID").val())
                    };
                    $.ajax({
                        url: "/WebServices/GetLocation.asmx/UpdateLocation",
                        data: JSON.stringify(data),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            setTimeout(function () { $("#spinner").hide(); }, 400);
                            if (result.d != "0") {
                                console.log('ok')
                            } else {
                                console.log('fail');
                            }
                        },
                        error: function (errormessage) {
                            console.log('fail');
                        }
                    });
                }
            },
            error: function (errormessage) {
                console.log('fail');
            }
        });
    }

    var constant;
    function error(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                constant = "Hệ thống không được quyền truy cập GPS";
                break;
            case error.POSITION_UNAVAILABLE:
                constant = "Hệ thống không định vị được";
                break;
            case error.TIMEOUT:
                constant = "Hết thời gian chờ";
                break;
            default:
                constant = "Lỗi truy cập GPS";
                break;
        }
        // alert("Mã lỗi: " + error.code + "\nLý do: " + constant + "\nMessage: " + error.message);
    }


</script>
</html>
