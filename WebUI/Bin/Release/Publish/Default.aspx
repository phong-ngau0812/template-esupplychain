<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="_Default, App_Web_mnfemkl2" %>

<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <style>
        h4 {
            text-transform: uppercase !important
        }

        .apexcharts-menu-item.exportCSV {
            display: none;
        }

        .table-bordered thead th, .table-bordered thead td {
            border-bottom-width: 2px;
            COLOR: #6d81f5;
        }

        .font-20 {
            margin-right: 5px;
        }

        span.apexcharts-tooltip-text-label span {
            display: none;
        }

        .my-custom-scrollbar {
            position: relative;
            height: 500px;
            overflow: auto;
        }

        .table-wrapper-scroll-y {
            display: block;
        }
    </style>
    <form runat="server" id="frm">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title text-center mb-5 mt-5 ">Chào mừng bạn đã đến với hệ thống quản lý truy xuất nguồn gốc chuỗi cung ứng !
                    </h4>

                    <div class="row" runat="server" id="Admin">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê tốc độ tăng trưởng số lượng doanh nghiệp theo năm</h4>
                                    <div class="">
                                        <div id="ana_dash_ProductBrandReport" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê doanh nghiệp theo ngành</h4>
                                    <div class="">
                                        <div id="ana_dash_ProductBrand" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê sản phẩm</h4>
                                    <div class="">
                                        <div id="ana_dash_Product" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê lô sản xuất</h4>
                                    <div class="">
                                        <div id="ana_dash_ProductPackage" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--    <div class="col-lg-4">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê lệnh sản xuất</h4>
                                    <div class="">
                                        <div id="ana_dash_ProductPackageOrder" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">Thống kê lô mã (NĂM <%=DateTime.Now.Year %>)</h4>
                                    <div class="">
                                        <div id="ana_dash_QRCode" class="apex-charts"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="header-title mt-0">THỐNG KÊ DOANH NGHIỆP THEO TỈNH THÀNH</h4>
                                    <br />
                                    <uc1:ctlDatePicker ID="ctlDatePicker1" runat="server" OnDateChange="ctlDatePicker1_DateChange" ClientIDMode="Static" />
                                    <br />
                                    <div class="">
                                        <%--   <div id="ana_dash_ProductBrandReport" class="apex-charts"></div>--%>
                                        <%-- <asp:ScriptManager ID="up1" runat="server"></asp:ScriptManager>--%>
                                        <asp:UpdatePanel runat="server" ID="up">
                                            <ContentTemplate>

                                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                                <div class="table-rep-plugin">
                                                    <div class="table-responsive mb-0 table-bordered table-wrapper-scroll-y my-custom-scrollbar" data-pattern="priority-columns">
                                                        <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                            <thead>
                                                                <tr>
                                                                    <th>Tỉnh/TP</th>
                                                                    <th>Tổng số sản phẩm</th>
                                                                    <th>Sản phẩm ngừng sản xuất</th>
                                                                    <th>Sản phẩm thêm mới</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>

                                                                <asp:Repeater runat="server" ID="rptReport" OnItemDataBound="rptReport_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("Name") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TotalActive")%>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("TotalNoActive")%>
                                                                            </td>
                                                                            <td>+
                                                                                <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                                                <asp:Literal runat="server" ID="lblTotalActive" Visible="false" Text='<%#Eval("TotalActive") %>'></asp:Literal>
                                                                                <asp:Literal runat="server" ID="lblTotalProductNew" Visible="false" Text='<%#Eval("TotalProductNew") %>'></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--end card-body-->
                    </div>
                    <div class="row" runat="server" id="divDoanhNghiep">
                        <div class="col-md-12">



                            <div class="row justify-content-center">
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="col-8">
                                                    <p class="text-dark font-weight-semibold font-14">DOANH NGHIỆP</p>
                                                    <h6 class="my-2">Phòng ban/bộ phận <%=TongPhong %></h6>
                                                    <h6 class="my-2">Nhân viên <%=TongNV %></h6>


                                                </div>
                                                <div class="col-4 align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i class="fas fa-building" style="color: #ff9f43"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="col-8">
                                                    <p class="text-dark font-weight-semibold font-14">KHÁCH HÀNG</p>
                                                    <h3 class="my-3"><%=TongKhach %></h3>

                                                </div>
                                                <div class="col-4 align-self-center">
                                                    <div class="report-main-icon bg-light-alt">

                                                        <i class="fas fa-people-carry" style="color: #fd3c97"></i>
                                                        <%--<svg xmlns="fas fa-building" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-clock align-self-center icon-dual-secondary icon-lg"><circle cx="12" cy="12" r="10"></circle><polyline points="12 6 12 12 16 14"></polyline></svg>  --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="col-8">
                                                    <p class="text-dark font-weight-semibold font-14">NHÀ CUNG CẤP</p>
                                                    <h3 class="my-3"><%=TongNCC %></h3>

                                                </div>
                                                <div class="col-4 align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i class="fas fa-cart-plus" style="color: #41cbd8"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="col-8">
                                                    <p class="text-dark font-weight-semibold font-14">NHÀ VẬN CHUYỂN</p>
                                                    <h3 class="my-3"><%=TongVC %></h3>

                                                </div>
                                                <div class="col-4 align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i class="fas fa-truck-moving" style="color: #6d81f5"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->
                            </div>

                            <div class="row">
                                <div class="col-lg-4">

                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">Thống kê khu vực sản xuất</h4>
                                            <div class="">
                                                <div id="ana_dash_Zone" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">Thống kê lô sản xuất</h4>
                                            <div class="">
                                                <div id="ana_dash_ProductPackage1" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">Thống kê lệnh sản xuất</h4>
                                            <div class="">
                                                <div id="ana_dash_ProductPackageOrder1" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">

                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">THỐNG KÊ PHIẾU XUẤT KHO VẬT TƯ (NĂM <%=DateTime.Now.Year %>) </h4>
                                            <div class="">
                                                <div id="ana_dash_WarehouseExport" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">

                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">THỐNG KÊ PHIẾU XUẤT KHO SẢN PHẨM (NĂM <%=DateTime.Now.Year %>) </h4>
                                            <div class="">
                                                <div id="ana_dash_WarehouseProductExport" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="header-title mt-0">Thống kê lô mã (NĂM <%=DateTime.Now.Year %>)</h4>
                                            <div class="">
                                                <div id="ana_dash_QRCode" class="apex-charts"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!--end /table-->
                            <%--   </div>--%>
                        </div>
                    </div>

                    <div class="row" runat="server" id="Giamsat">
                        <div class="col-md-12">
                            <div class="row justify-content-center">
                                <div class="col-md-12 col-lg-12">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="col-12">
                                                    <p class="text-dark font-weight-semibold font-14"><i class="fas fa-building font-20" style="color: #ff9f43"></i>DOANH NGHIỆP ĐANG GIÁM SÁT: <%=index %></p>
                                                    <%=listdn %>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->


                                <!--end /table-->
                                <%--   </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="NongNghiep" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="ChanNuoi" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="ThuyHaiSan" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="NongNghiepSP" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="ChanNuoiSP" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="ThuyHaiSanSP" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="DangSanXuat" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="DangThuHoach" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="ThuHoachXong" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Huy" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="Lenhchuaduyet" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Lenhdaduyet" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="NowYear" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="LastYear" Value="" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="precious1" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="precious2" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="precious3" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="precious4" Value="" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="Product_precious1" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Product_precious2" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Product_precious3" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Product_precious4" Value="" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="Zone" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Area" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="Farm" Value="" ClientIDMode="Static" />

            <asp:HiddenField runat="server" ID="QRCode" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="QRCodeActive" Value="" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="QRCodeNotActive" Value="" ClientIDMode="Static" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <script src="../../theme/plugins/apexcharts/apexcharts.min.js"></script>

    <!--<script src="../plugins/jvectormap/jquery-jvectormap-2.0.2.min.js"></script>
        <script src="../plugins/jvectormap/jquery-jvectormap-us-aea-en.js"></script>-->

    <script>
        var NongNghiep = 0;
        var ChanNuoi = 0;
        var ThuyHaiSan = 0;
        var NongNghiepSP = 0;
        var ChanNuoiSP = 0;
        var ThuyHaiSanSP = 0;

        var DangSanXuat = 0;
        var DangThuHoach = 0;
        var ThuHoachXong = 0;
        var Huy = 0;
        var listNow = [];
        var listLast = [];
        var TotalNow = 0;
        var TotalLast = 0;
        $(document).ready(function () {
            NongNghiep = parseInt($("#NongNghiep").val());
            ChanNuoi = parseInt($("#ChanNuoi").val());
            ThuyHaiSan = parseInt($("#ThuyHaiSan").val());
            NongNghiepSP = parseInt($("#NongNghiepSP").val());
            ChanNuoiSP = parseInt($("#ChanNuoiSP").val());
            ThuyHaiSanSP = parseInt($("#ThuyHaiSanSP").val());

            DangSanXuat = parseInt($("#DangSanXuat").val());
            DangThuHoach = parseInt($("#DangThuHoach").val());
            ThuHoachXong = parseInt($("#ThuHoachXong").val());
            Huy = parseInt($("#Huy").val());
            GetChart();


            //Biểu đồ tốc độ tăng trưởng của doanh nghiệp theo năm
            var ListDataNow = $("#NowYear").val();
            var splitDataNow = ListDataNow.split(',');
            $.each(splitDataNow, function (i, obj) {
                listNow.push(parseInt(splitDataNow[i]));
                TotalNow += parseInt(splitDataNow[i]);
            })

            var ListDataLast = $("#LastYear").val();
            var splitDataLast = ListDataLast.split(',');
            $.each(splitDataLast, function (i, obj) {
                listLast.push(parseInt(splitDataLast[i]));
                TotalLast += parseInt(splitDataLast[i]);
            })
            GetChartProductBrand();
            //Kết thúc biểu đồ tốc độ tăng trưởng của doanh nghiệp theo năm
        });

    </script>
    <script>
        var Lenhchuaduyet = 0;
        var Lenhdaduyet = 0;
        $(document).ready(function () {
            Lenhchuaduyet = parseInt($("#Lenhchuaduyet").val());
            Lenhdaduyet = parseInt($("#Lenhdaduyet").val());

            GetChartProductPackageOrder();
        });
    </script>
    <script>
        var Zone = 0;
        var Area = 0;
        var Farm = 0;
        $(document).ready(function () {
            Zone = parseInt($("#Zone").val());
            Area = parseInt($("#Area").val());
            Farm = parseInt($("#Farm").val());
            GetChartZone();
        });
    </script>
    <script>
        var Product_precious1 = 0;
        var Product_precious2 = 0;
        var Product_precious3 = 0;
        var Product_precious4 = 0;
        $(document).ready(function () {
            Product_precious1 = parseInt($("#Product_precious1").val());
            Product_precious2 = parseInt($("#Product_precious2").val());
            Product_precious3 = parseInt($("#Product_precious3").val());
            Product_precious4 = parseInt($("#Product_precious4").val());
            GetChartWarehouseProductExport();
        });
    </script>
    <script>
        var precious1 = 0;
        var precious2 = 0;
        var precious3 = 0;
        var precious4 = 0;
        $(document).ready(function () {
            precious1 = parseInt($("#precious1").val());
            precious2 = parseInt($("#precious2").val());
            precious3 = parseInt($("#precious3").val());
            precious4 = parseInt($("#precious4").val());
            GetChartWarehouseExport();
        });
    </script>

    <script>
        var QRCode = 0;
        var QRCodeActive = 0;
        var QRCodeNotActive = 0;
        $(document).ready(function () {
            QRCode = parseInt($("#QRCode").val());
            QRCodeActive = parseInt($("#QRCodeActive").val());
            QRCodeNotActive = parseInt($("#QRCodeNotActive").val());
            GetChartQRCode();
        });

    </script>


    <script src="../../theme/assets/pages/jquery.analytics_dashboard.init.js?v=<%=Systemconstants.Version(5) %>"></script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
    </script>
</asp:Content>

