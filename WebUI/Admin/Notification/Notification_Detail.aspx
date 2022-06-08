<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="Notification_Detail.aspx.cs" Inherits="Notification_Detail" %>

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
                                <li class="breadcrumb-item active"><a>Thông báo </a></li>
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
                            <div class="row" runat="server" id="Export" visible="false">
                                <div class="col-md-12 col-xs-12">
                                    <h4><%=NotiName %></h4>
                                    <br />
                                    <label><i class="mdi mdi-note font-20"></i>&nbsp;Nội dung: <%=NotiDetail %></label>
                                    <br />
                                    <label><i class="dripicons-user font-20"></i>&nbsp;Người xuất: <%=NguoiXuat %></label>
                                    <br />
                                    <label><i class="mdi mdi-camera-timer font-20"></i>&nbsp;Ngày xuất: <%=NgayXuat %></label>
                                    <br />
                                    <br />
                                    <div class="form-group mb-0">
                                        <asp:Button ID="Button2" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                </div>

                            </div>
                            <div class="row" runat="server" id="Noti">
                                <div class="col-md-12 col-xs-12">
                                    <%--  <div class="form-group">
                                        <asp:Button ID="btnBackFix1" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>--%>
                                    <h4>Tên lệnh: <%=name %></h4>
                                    <br />
                                    <i class="dripicons-user font-20"></i>&nbsp; Người tạo:<b> <%=nguoitao %></b>
                                    <br />
                                    <i class="mdi mdi-camera-timer font-20"></i>&nbsp;Thời gian:<b> <%=thoigian %></b>
                                    <br />
                                    <i class="dripicons-user font-20"></i>&nbsp;  Người duyệt:<b> <%=nguoiduyet %></b>
                                </div>
                                <div class="col-md-12 col-xs-12" runat="server" id="Data">
                                    <br />
                                    <label>Danh sách vật tư</label>
                                    <br />
                                    <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>

                                            <tr>
                                                <th>Tên vật tư</th>
                                                <th width="100px">Định mức</th>
                                                <th width="10%">Đơn vị</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptMaterial">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("NameM") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Amount") %></td>
                                                        <td><%#Eval("Unit") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <div class="form-group mb-0">
                                        <asp:Button ID="btnExport" OnClick="Button1_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Xuất kho" CssClass="btn btn-gradient-primary waves-effect m-l-5"></asp:Button>
                                        <asp:Button ID="btnBackFix1" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="NotiApproved">
                                <div class="col-md-12 col-xs-12">
                                    <%--  <div class="form-group">
                                        <asp:Button ID="btnBackFix1" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>--%>
                                    <h4><%=name %></h4>
                                    <br />
                                    <i class="dripicons-user font-20"></i>&nbsp; Người tạo:<b> <%=nguoitao %></b>
                                    <br />
                                    <i class="dripicons-user font-20"></i>&nbsp;  Người duyệt:<b> <%=nguoiduyet %></b>
                                    <br />
                                    <i class="mdi mdi-camera-timer font-20"></i>&nbsp;Thời gian:<b> <%=thoigian %></b>
                                    <br />
                                    <i class="fab fa-product-hunt  font-20" runat="server" id="TT"></i>&nbsp;Xem lại thông tin:<b> <a href='<%=url %>'>Xem chi tiết</a>    </b>
                                    <br />
                                    <i class="mdi mdi-table-of-contents font-20"></i>&nbsp;Nội dung yêu cầu:<b> <%=yeucau %> </b>
                                    <br />
                                    <i class="mdi mdi-table-of-contents font-20"></i>&nbsp;Nội dung phản hồi:<b>  <%=duyet %>  </b>

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
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
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
