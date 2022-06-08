<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="TaskStepProduct_Setup, App_Web_quwo134q" maintainscrollpositiononpostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
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
                                <li class="breadcrumb-item active"><a>Cài đặt cách thức nhập nội dung đầu mục</a></li>
                                <li class="breadcrumb-item "><a href="TaskStepProduct_List?Product_ID=<%=Product_ID %>">Danh sách đầu mục công việc</a></li>
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

                    <div class="card" runat="server" id="Nhom4">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-12 col-xs-12">

                                    <h4>Cài đặt cách thức nhập nội dung đầu mục: <%=name %></h4>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblMessage1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-lg-12">

                                            <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlType" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn loại --">
                                                <%--<asp:ListItem Text="Truyền thống" Value="1"></asp:ListItem>--%>
                                                <%--<asp:ListItem Text="Ghi âm" Value="2"></asp:ListItem>--%>
                                                <%--<asp:ListItem Text="Nhận diện giọng nói" Value="3"></asp:ListItem>--%>
                                                <asp:ListItem Text="Câu hỏi và trả lời" Value="4"></asp:ListItem>
                                            </asp:ListBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12" runat="server" id="Data1">
                                    <div class="form-group mb-0">
                                        <asp:Button runat="server" ID="btnAddMaterial" ClientIDMode="Static" OnClick="btnAddMaterial_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Cập nhật" />
                                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
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
    <script src="/theme/plugins/datatables/pdfmake.min.js"></script>
    <script src="/theme/plugins/datatables/vfs_fonts.js"></script>
    <script src="/theme/plugins/datatables/buttons.html5.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.print.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.colVis.min.js"></script>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
    <script src="/theme/assets/pages/jquery.datatable.init.js"></script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            //$(function () {
            //    $(".formatMoney").keyup(function (e) {
            //        $(this).val(format($(this).val()));
            //    });
            //});
            $(".select2").select2({
                width: '100%'
            });
            setTimeout(function () { $('#lblMessage1').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
                    $(".select2").select2({
                        width: '100%'
                    });
                });
            }
        });
    </script>
    <script src="../../js/Function.js"></script>
</asp:Content>
