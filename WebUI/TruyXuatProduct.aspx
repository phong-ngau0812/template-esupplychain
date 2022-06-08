<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TruyXuatProduct.aspx.cs" Inherits="TruyXuatProduct" %>

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
    <link rel="stylesheet" href="/TruyXuatSRC/css/style.css?v=1" />
    <style>
        form#Form1 {
            background: #fff !important;
        }

        .red {
            background: red;
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
    <form id="form1" runat="server" style="background: #fff !important;">
        <div class="first-alert d-flex align-items-center">
            <img class="alert-logo" src="<%=logo %>" />
            <p class="text2 m-0">
                <%=message %>
                <%--hy.check.net.vn--%>
            </p>
            <img class="alert-close" src="/TruyXuatSRC/img/cancel.png" />

        </div>
        <div runat="server" id="Nodata" class="p-3">
            <p class="notify">Sản phẩm không tồn tại trên hệ thống</p>
        </div>
        <div runat="server" id="Data">
            <section class="section1">
                <div id="slide-header" class="carousel slide" data-ride="carousel" data-interval="false">
                    <div class="carousel-inner">
                        <asp:Repeater runat="server" ID="rptGallery">
                            <ItemTemplate>
                                <div style="display: none"><%=No++ %></div>
                                <div class="carousel-item <%=No==1?"active":"" %>">
                                    <img src="<%#"/data/product/product_info/"+ Eval("Image") %>" class="d-block w-100" alt="...">
                                    <div class="page d-flex">
                                        <p class="text4 m-0"><%=No %>/<%=dtImg.Rows.Count %></p>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="carousel-item active" runat="server" id="ImgThumbnail">
                            <img src="<%=Image%>" class="d-block w-100" alt="...">
                            <div class="page d-flex">
                                <p class="text4 m-0">1/1</p>
                            </div>
                        </div>

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
                        <img class="logo-cp" src="<%=LogoBrand %>"></img>
                        <div class="row1">
                            <p class=" text3"><%=ProductBrandName %></p>
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
                    <div class="menu3-son son1 active-on   col-4 p-0 d-flex justify-content-center">
                        <li class="text2">SẢN PHẨM</li>
                    </div>

                    <div class="menu3-son son2 col-4 p-0 d-flex justify-content-center" runat="server" visible="false">
                        <li class="text2">NGUỒN GỐC</li>
                    </div>

                    <div class="menu3-son son3 col-4 p-0 d-flex justify-content-center">
                        <li class="text2">THƯƠNG HIỆU</li>
                    </div>

                </ul>
                <div class="content-history">
                    <div class="detail">
                        <div class="title">
                            Nhật ký sản xuất
                        </div>
                        <asp:Repeater runat="server" ID="rptSX">
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
                                            <td><%#Eval("Description") %></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="title">
                            Nhật ký vật tư
                        </div>
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
                                            <td><%#decimal.Parse( Eval("Quantity").ToString()).ToString("N0") %> <%#Eval("Unit") %></td>
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
                                            <td><%#Eval("Description") %></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="title">
                            Nhật ký thu hoạch
                        </div>
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
                                            <td><%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="title">
                            Nhật ký sơ chế chế biến
                        </div>
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
                                        <%--    <tr>
                                        <td class='col-left'>Nội dung: </td>
                                        <td></td>
                                    </tr>--%>
                                        <tr>
                                            <td class='col-left'>Vị trí: </td>
                                            <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="content-product  active-show">
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
                        <%--  <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="<%=video %>" allowfullscreen></iframe>
                    </div>--%>
                        <%=video %>
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
                        <div class="d-flex detail-title">
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

                        <%--<div class="show-more-box">
                        <div class="d-flex justify-content-center">
                            <p class="text show-more m-0">Xem thêm</p>
                        </div>
                    </div>--%>
                    </div>
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
                                            <p class="text m-0"><%#Eval("GiaBan").ToString() =="0.0000"?"": decimal.Parse( Eval("GiaBan").ToString()).ToString("N0") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
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
                            <%-- <div class="d-flex about">
                            <div class="col-4 d-flex p-0 align-items-end about-title">
                                <div>
                                    <img class="img-fluid" src="/TruyXuatSRC/img/iconfacebook.png" />
                                </div>
                                <p class="text mb-0">Fanpage:</p>
                            </div>
                            <div class="col-8 p-0 about-content d-flex align-items-end pr-0">
                                <p class="text mb-0">Bấm vào được</p>
                            </div>
                        </div>
                        <div class="d-flex about">
                            <div class="col-4 d-flex p-0 align-items-end about-title">
                                <div>
                                    <img class="img-fluid" src="/TruyXuatSRC/img/iconstan.png" />
                                </div>
                                <p class="text mb-0">Tiêu chuẩn:</p>
                            </div>
                        </div>--%>
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
                        <br />
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
        <asp:HiddenField runat="server" ID="productinfo_id" ClientIDMode="Static" />
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
<style>
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
</style>

</html>
