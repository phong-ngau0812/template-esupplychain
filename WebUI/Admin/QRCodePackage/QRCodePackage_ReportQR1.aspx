<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackage_ReportQR1.aspx.cs" Inherits="Admin_QRCodePackage_QRCodePackage_ReportQR1" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
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
            width: 18px;
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
                                <li class="breadcrumb-item active"><a>Báo cáo sản phẩm đã xác thực tem lớp 1</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Báo cáo sản phẩm đã xác thực tem lớp 1</h4>
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
                            <div class="row mb-3">
                                <!-- end row -->
                                   <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage" CssClass="select2 form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlProductSet_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlSo" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSo_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlLocation" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlWard" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->
                                <!-- end row -->
                                <%--  <div class="col-md-6 right">
                                   
                                </div>--%>
                            </div>
                            <div class="row mb-3" style="float:right;">
                                <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnSearch" Text="Xem báo cáo" OnClick="btnSearch_Click2" />
                                <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnExport" Text="Xuất báo cáo" OnClick="btnSearch_Click1" />
                            </div>
                            <div class="form-group mt-3">
                                <div>
                                    <label>Hiển thị </label>
                                    <asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">
                                        <asp:ListItem Value="20"></asp:ListItem>
                                        <asp:ListItem Value="40"></asp:ListItem>
                                        <asp:ListItem Value="50"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <%--     Tổng <%=TotalItem %> lô mã--%>
                            </div>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th width="5%">STT</th>
                                                        <th width="7%">Serial Number</th>
                                                        <th width="10%">Tên sản phẩm</th>
                                                        <th width="10%">Tên doanh nghiệp</th>
                                                        <th width="10%" class="text-center">Lô sản xuất</th>
                                                        <th width="10%" class="text-center">Ngày tạo mã</th>
                                                        <th width="20%" class="text-center">Vị trí xác thực</th>
                                                        <th width="10%" class="text-center">Thời gian xác thực</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="grdProductPackage">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <%=No++ %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("QRCodeSerial").ToString() %>
                                                                </td>
                                                                <td><%#Eval("Name") %></td>
                                                                <td><%#Eval("TenDN") %></td>
                                                                <td><%#Eval("Malo") %></td>
                                                                <td><%#Eval("NgayTaoTem") %></td>
                                                                <td><%#Eval("Location") %></td>
                                                                <td><%#Eval("TrackingDate") %></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <%--    <tr style="background: #edf0f5; font-weight: bold; color: red;">
                                                        <td colspan="3"><b>Tổng</b></td>
                                                        <td colspan="7"><b><%=TinhTong.ToString("N0") %></b></td>

                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <div id="x_box_pager" style="float: right; text-align: right; margin-top: 10px" runat="Server" class="box_pager">
                                        <label>Trang <%=Pager1.CurrentIndex %>/<%=TotalPage %></label>
                                        (<label> <%=TotalItem %> bản ghi</label>)
                                        <cc1:PagerV2_8 ID="Pager1" runat="server" OnCommand="Pager1_Command"
                                            GenerateFirstLastSection="True" GenerateGoToSection="False" GenerateHiddenHyperlinks="False"
                                            GeneratePagerInfoSection="False" NextToPageClause="" OfClause="/" PageClause=""
                                            ToClause="" CompactModePageCount="1" MaxSmartShortCutCount="5" NormalModePageCount="5"
                                            GenerateToolTips="False" BackToFirstClause="" BackToPageClause="" FromClause=""
                                            GenerateSmartShortCuts="False" GoClause="" GoToLastClause="" />
                                        <div class="clear">
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
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
                });
            }
        });
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            Init();
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);
        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            $("#spinner").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            setTimeout(function () { $("#spinner").hide(); }, 0);
        }
    </script>
</asp:Content>

