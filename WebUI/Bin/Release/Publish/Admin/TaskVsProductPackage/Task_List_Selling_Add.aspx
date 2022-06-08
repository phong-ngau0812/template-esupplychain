<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="Task_List_Selling_Add, App_Web_23km1lrn" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/UserControl/ctlVoice.ascx" TagName="ctlVoice" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Nhật ký sản xuất</a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=name %></a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Thêm mới nhật ký bán hàng - Lô <%=name %></h4>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>
            <!-- end page title end breadcrumb -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" runat="server" id="ProductBrand">
                                        <label>Doanh nghiệp</label>
                                        <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" runat="server" id="ProductPackage">
                                        <label>Lô sản xuất</label>
                                        <asp:DropDownList runat="server" ID="ddlProductPackage" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Chọn kho <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlWarehouse" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" CssClass="form-control select2" required data-parsley-required-message="Bạn chưa chọn kho"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Nội dung bán hàng <span style="color: red; font-size: 15px">*</span>  </label>
                                        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập nội dung bán hàng"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Khách hàng <span style="color: red; font-size: 15px">*</span>  </label>
                                        <asp:DropDownList runat="server" ID="ddlCustomer" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Địa chỉ khách hàng</label>
                                        <asp:TextBox runat="server" ID="txtShopAddress" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Đại diện bên mua <span style="color: red; font-size: 15px">*</span> </label>
                                        <asp:TextBox runat="server" ID="txtBuyerName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập người mua"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Số lượng <span class="red">*</span> (sản phẩm) <%=FillAmount%></label>
                                        <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control formatMoney" required data-parsley-required-message="Bạn chưa nhập số lượng "></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Giá tiền (trên một sản phẩm )</label>
                                        <asp:TextBox runat="server" ID="txtPrice" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Phần trăm triết khấu</label>
                                        <asp:TextBox runat="server" ID="txtDiscountPercent" ClientIDMode="Static" CssClass="form-control formatMoney" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Tổng tiền</label>
                                        <asp:TextBox runat="server" ID="txtSumMoney" ClientIDMode="Static" CssClass="form-control formatMoney" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Số Container</label>
                                        <asp:TextBox runat="server" ID="txtContainer" ClientIDMode="Static" CssClass="form-control "></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Số Chì</label>
                                        <asp:TextBox runat="server" ID="txtSoChi" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Ngày Bán</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtStart" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Bạn chưa nhập ngày bán " />
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Mô tả nhật ký công việc</label>
                                        <uc1:ctlVoice ID="ctlVoice1" runat="server" />
                                        <CKEditor:CKEditorControl ID="txtDes" BasePath="/ckeditor/" runat="server" ClientIDMode="Static">
                                        </CKEditor:CKEditorControl>
                                    </div>
                                    <div class="form-group">
                                        <label>Ảnh minh họa</label>
                                        <br />
                                        <div style="margin: 5px 0px;">
                                            <a href="<%=avatar %>" target="_blank">
                                                <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                            </a>
                                        </div>
                                        <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                                    </div>
                                    <div class="form-group">
                                        <label>Vị trí hiện tại</label>
                                        <asp:TextBox runat="server" ID="txtLocation" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Trạng thái công việc</label>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-0">
                                        <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                        <asp:Button ID="btnBackFix" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
        </div>
        <!-- container -->

        <!--  Modal content for the above example -->
        <asp:HiddenField runat="server" ID="YearStart" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="MonthStart" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="DayStart" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="YearEnd" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="MonthEnd" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="DayEnd" ClientIDMode="Static" />
    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

    <script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="../../theme/assets/pages/jquery.validation.init.js"></script>

    <!-- Required datatable js -->
    <script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>
    <!-- Buttons examples -->
    <script src="/theme/plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.bootstrap4.min.js"></script>
    <script src="/theme/plugins/datatables/jszip.min.js"></script>
    <script src="/theme/plugins/datatables/pdfmake.min.js"></script>
    <script src="/theme/plugins/datatables/vfs_fonts.js"></script>
    <script src="/theme/plugins/datatables/buttons.html5.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.print.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.colVis.min.js"></script>
    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
    <script src="/theme/assets/pages/jquery.datatable.init.js"></script>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script>
        function img() {
            var url = inputToURL(document.getElementById("<%=fulAnh.ClientID %>"));
            document.getElementById("<%=imganh.ClientID %>").src = url;
        }
        function inputToURL(inputElement) {
            var file = inputElement.files[0];
            return window.URL.createObjectURL(file);
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#txtQuantity").keyup(function () {
                //alert("Handler for .change() called.");
                TingTong();
            });
            $("#txtPrice").keyup(function () {
                //alert("Handler for .change() called.");
                TingTong();
            });
            $("#txtDiscountPercent").keyup(function () {
                //alert("Handler for .change() called.");
                TingTong();
            });

        });
        function TingTong() {
            var soluong = $("#txtQuantity").val();
            var tien = $("#txtPrice").val();
            var chietkhau = $("#txtDiscountPercent").val();
            if ($("#txtQuantity").val() != "" && $("#txtPrice").val() != "") {
                var tong = 0;
                if ($("#txtDiscountPercent").val() != "") {
                    if (parseInt(chietkhau.replace(',', '').replace(',', '')) < 100) {
                        tong = parseInt(soluong.replace(',', '').replace(',', '')) * parseInt(tien.replace(',', '').replace(',', '')) - ((parseInt(soluong.replace(',', '').replace(',', '')) * parseInt(tien.replace(',', '').replace(',', ''))) * (parseInt(chietkhau) / 100));
                    }

                }
                else {
                    tong = parseInt(soluong.replace(',', '').replace(',', '')) * parseInt(tien.replace(',', '').replace(',', ''));
                }
                $("#txtSumMoney").val(format(tong));
            }

        }
    </script>
    <script>
        var getGeo = function () {
            navigator.geolocation.getCurrentPosition(success, error, { maximumAge: 600000, timeout: 10000 });
        }
        $(document).ready(function () {
            getGeo();
        });
        function success(position) {
            var lng = position.coords.longitude;
            var lat = position.coords.latitude;
            $(function () {
                $.ajax({
                    url: "/WebServices/GetLocation.asmx/GetAddressLocation",
                    type: "GET",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: { Latitude: "'" + lat + "'", Longitude: "'" + lng + "'" },
                    success: function (result) {
                        if (result.d != "") {
                            $("#txtLocation").val(result.d);
                        }
                    },
                    error: function (errormessage) {
                        //alert(error);
                    }
                });

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
            //alert("Mã lỗi: " + error.code + "\nLý do: " + constant + "\nMessage: " + error.message);
        }

    </script>
    <script src="../../js/Function.js"></script>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });
            $('#txtStart').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
                    format: 'DD/MM/YYYY HH:mm:ss',
                    "daysOfWeek": [
                        "CN",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6",
                        "7",
                    ],
                    "monthNames": [
                        "Tháng 1",
                        "Tháng 2",
                        "Tháng 3",
                        "Tháng 4",
                        "Tháng 5",
                        "Tháng 6",
                        "Tháng 7",
                        "Tháng 8",
                        "Tháng 9",
                        "Tháng 10",
                        "Tháng 11",
                        "Tháng 12",
                    ],
                },
            }, function (start, end, label) {

            });
            $(".select2").select2({
                width: '100%'
            });
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
                });
            }
        });

    </script>

</asp:Content>
