<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Admin_QRCodePackage_ReportQRCodePackage, App_Web_knjlquph" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- Responsive datatable examples -->
    <%--<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/RWD-Table-Patterns/dist/css/rwd-table.min.css" rel="stylesheet" type="text/css" media="screen">
    <style>
        .btn-group.focus-btn-group {
            display: none;
        }

        .icon {
            padding: 5px 0px;
            margin-right: 3px;
        }

        @media only screen and (min-width: 1280px) {
            .table-responsive[data-pattern="priority-columns"] > .table > thead > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > tbody > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > tfoot > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > thead > tr > td, .table-responsive[data-pattern="priority-columns"] > .table > tbody > tr > td, .table-responsive[data-pattern="priority-columns"] > .table > tfoot > tr > td {
                white-space: inherit !important;
            }
        }
    </style>
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
                                <li class="breadcrumb-item active"><a>Quản lý lô mã</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Báo cáo thống kê danh sách lô mã định danh</h4>
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
                            <uc1:ctlDatePicker ID="ctlDatePicker1" runat="server" OnDateChange="ctlDatePicker1_DateChange" />
                            <br />
                            <div class="row" runat="server" visible="false" id="ok">


                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="-- Loại tem --"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Tem công khai"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Tem bí mật"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->

                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtName" placeholder="Tìm theo tên" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtSerial" placeholder="Tìm số Serial" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlWard" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <!-- end row -->
                            </div>

                            <div class="row">

                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage" AutoPostBack="true" ClientIDMode="Static" CssClass="select2 form-control" OnSelectedIndexChanged="ddlProductPackage_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 right">
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnSearch" Text="Xem báo cáo" OnClick="btnSearch_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnReport" Text="Xuất báo cáo" OnClick="btnReport_Click1" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnAdd" Visible="false" Text="Thêm mới" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <div class="row mt-5">
                                <div class="col-md-12">
                                    Tổng <%=TotalItem %> lô mã
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th width="3%">STT</th>
                                                        <th width="20%">Tên lô mã</th>
                                                        <th width="20%">Tên sản phẩm</th>
                                                        <th width="10%">Người tạo lô mã</th>
                                                        <th width="10%" class="text-center">Ngày sản xuất</th>
                                                        <th width="10%" class="text-center">Ngày thu hoạch</th>
                                                        <th width="5%" class="text-center">SL tem</th>
                                                        <th width="10%" class="text-center">Serial</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="grdProductPackage">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%=No++ %></td>
                                                                <td><%#Eval("Name")%></td>
                                                                <td><%#Eval("ProductName")%></td>
                                                                <td><%# Eval("UserName")%></td>
                                                                <td><%# Eval("NgaySX")%></td>
                                                                <td><%# Eval("NgayThuHoach")%></td>
                                                                <td><%#Eval("QRCodeNumber") %></td>
                                                                <td>Serial từ <b><%#Eval("SerialNumberStart") %></b> đến <b><%#Eval("SerialNumberEnd") %></b></td>
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
        </div>
        <!-- end col -->


        <!-- container -->

        <!--  Modal content for the above example -->
        <asp:HiddenField runat="server" ID="ProductBrandList" />

        <!-- /.modal-dialog -->


    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Required datatable js -->
    <script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>

    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <%--<script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>--%>
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

