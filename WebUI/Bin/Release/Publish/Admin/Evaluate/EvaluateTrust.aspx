<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Admin_Evaluate_EvaluateTrust, App_Web_sopxxjtg" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Quản lý đánh giá chỉ số tin cậy</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý đánh giá chỉ số tin cậy</h4>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h4>THÔNG TIN DOANH NGHIỆP ĐÁNH GIÁ</h4>
                            <div class="form-group">
                                <label>
                                    Doanh nghiệp
									<span class="red">*</span>
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" id="XemTT" visible="false">
                                <asp:Button runat="server" ID="btnXemTT"  OnClick="btnXemTT_Click" Text="Xem thông tin doanh nghiệp" CssClass="btn btn-gradient-primary waves-effect m-l-5" />
                            </div>

                            <div class="form-group">
                                <label>Số sản phẩm</label>
                                <asp:TextBox runat="server" ID="txtSoSP" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label>Số tem mã công khai</label>
                                    <asp:TextBox runat="server" ID="txtTemcongkhai" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <label>Số tem mã bí mật</label>
                                    <asp:TextBox runat="server" ID="txtTembimat" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>


                            </div>
                            <div class="form-group">
                            </div>
                            <h4>CƠ QUAN QUẢN LÝ ĐÁNH GIÁ</h4>
                            <div class="form-group">
                                <label>Chỉ số tin cậy</label>
                                <asp:DropDownList runat="server" ID="ddlChiso" CssClass="form-control">
                                    <asp:ListItem Text="-Chọn chỉ số-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Nhận xét, đánh giá</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>

                            <!--end form-group-->

                            <!--end form-group-->

                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Đánh giá và gửi phản hồi" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>

            </div>
        </div>
        <!-- container -->

        <!--  Modal content for the above example -->
        <asp:HiddenField runat="server" ID="ProductBrandList" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
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
    <%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>
    <!-- Responsive-table-->
    <script src="/theme/plugins/RWD-Table-Patterns/dist/js/rwd-table.min.js"></script>
    <script src="/theme/assets/pages/jquery.responsive-table.init.js"></script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
                    $('.table-responsive').responsiveTable({
                        addDisplayAllBtn: 'btn btn-secondary',
                    });
                });
            }
        });
    </script>
</asp:Content>

