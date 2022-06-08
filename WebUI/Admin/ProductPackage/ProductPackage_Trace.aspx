<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="ProductPackage_Trace.aspx.cs" Inherits="ProductPackage_Trace" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>HTML Nhật ký </a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=namepackage %></a></li>
                                <li class="breadcrumb-item "><a href="ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title"></h4>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <h4>Dữ liệu truy vết lô: <%=namepackage %></h4>
                            <br />
                            <div class="form-group">
                                <label>Mã truy vết đơn vị logistic (SSCC) </label>
                                <asp:TextBox runat="server" ID="txtSSCC_Logistic" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <%--   <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlProductBrand" CssClass="select2 form-control" Width="100%"  data-parsley-required="true" data-parsley-allselected="true"  data-parsley-required-message="Bạn chưa chọn doanh nghiệp" data-placeholder="-- Chọn doanh nghiệp --"></asp:DropDownList>--%>
                            </div>

                            <div class="form-group">
                                <label>Tên hàng hóa, giống cây</label>
                                <asp:TextBox runat="server" ID="txtProductName" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Mã truy vết bên nhận (GLN)</label>
                                <asp:TextBox runat="server" ID="txtGLN_Receive" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Ngày chuyển hàng</label>
                                <div class="input-group ">
                                    <asp:TextBox runat="server" ID="txtDate" Text="" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Địa điểm gửi đi (GLN)</label>
                                <asp:TextBox runat="server" ID="txtGLN_From" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Địa điểm gửi đến (GLN)</label>
                                <asp:TextBox runat="server" ID="txtGLN_To" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button ID="btnExportQR" OnClick="btnExportQR_Click" runat="server" ClientIDMode="Static" Text="Xuất mã QR" CssClass="btn btn-gradient-primary waves-effect waves-light"></asp:Button>
                                <asp:Button ID="Button1" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
        </div>
        <!-- container -->


    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Required datatable js -->

    <script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>
    <!-- Buttons examples -->
    <script src="/theme/plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.bootstrap4.min.js"></script>
    <script src="/theme/plugins/datatables/jszip.min.js"></script>
    <script src="/theme/plugins/datatables/vfs_fonts.js"></script>
    <script src="/theme/plugins/moment/moment.js"></script>
    <script src="/theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="/theme/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- Responsive examples -->
    <script src="/theme/plugins//clipboard/clipboard.min.js"></script>
    <script src="/theme/assets/pages/jquery.clipboard.init.js"></script>

    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        });
        $('#txtDate').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            //minYear: 1901,
            //maxYear: parseInt(moment().format('YYYY'), 10),
            locale: {
                format: 'DD/MM/YYYY',
            },
        }, function (start, end, label) {
            $('#txtDate').val(start.format('DD/MM/YYYY'));
            //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        });
    </script>
    <script src="../../js/Function.js"></script>
</asp:Content>
