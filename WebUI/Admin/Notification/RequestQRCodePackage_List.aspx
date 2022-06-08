<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="RequestQRCodePackage_List.aspx.cs" Inherits="Admin_Notification_Default" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/RWD-Table-Patterns/dist/css/rwd-table.min.css" rel="stylesheet" type="text/css" media="screen">
    <style>
        .btn-group.focus-btn-group {
            display: none;
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
                                <li class="breadcrumb-item active"><a>Quản lý yêu cầu cấp tem</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Danh sách yêu cầu cấp tem</h4>
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
                            <div class="row">
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlLocation" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Tất cả trạng thái --" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="-- Chưa duyệt --" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="-- Đã duyệt --" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="-- Không duyệt --" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-3 right">
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                                </div>
                                <!-- end row -->
                            </div>

                            <div class="form-group">
                            </div>
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <label>Hiển thị </label>
                            <asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">
                                <asp:ListItem Value="5"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="20"></asp:ListItem>
                            </asp:DropDownList>
                            <%--  <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>--%>
                            <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                            <div class="table-rep-plugin">
                                <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                    <table id="tech-companies-1" class="table table-striped mb-0">
                                        <thead>
                                            <tr>
                                                <th width="20%">Nội dung</th>
                                                <th width="20%">Trạng thái</th>
                                                <th width="10%">Loại Tem</th>
                                                <th width="10%">Số lượng</th>
                                                <th width="20%" class="text-center">Chức năng</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="grdData" OnItemDataBound="grdData_ItemDataBound" OnItemCommand="grdData_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>Doanh nghiệp: <b><%#Eval("Name")%></b><br />
                                                            Tên sản phẩm: <b><%#Eval("ProductName")%></b><br />
                                                            Người yêu cầu: <b><%#Eval("Nguoitao") %></b><br />
                                                            Vào lúc:  <%# DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                           
                                                        </td>
                                                        <td>
                                                            <asp:Literal runat="server" ID="lblQRCodePackageChange_Status" Text='<%#Eval("QRCodePackageChange_Status") %>' Visible="false"></asp:Literal>
                                                            <%-- <asp:Literal runat="server" ID="lblProductBrand_ID" Text='<%#Eval("ProductBrand_ID") %>' Visible="false"></asp:Literal>--%>
                                                            <%#Eval("QRCodePackageChange_Status").ToString()=="0"?"<span class='text-danger'> Chưa duyệt</span>": (Eval("QRCodePackageChange_Status").ToString()=="2"?"<span class='text-danger'> Không duyệt</span>":"<span class='text-success'> Đã duyệt</span> ") %></td>
                                                        </td>
                                                        <td><%#Eval("QRCodePackageType_ID").ToString()=="1"?"<span class=\"badge badge-success\"><i class='fas fa-lock-open'></i> Tem công khai</span>":"<span class=\"badge badge-primary\"><i class='fas fa-lock'></i> Tem bí mật</span> " %></td>
                                                        <td align="center"><%#Eval("QRCodeNumber") %></td>

                                                        <td align="center">
                                                            <span>
                                                                <asp:LinkButton runat="server" ID="btnActive" CommandName="Active" CssClass=" btn btn-success mr-2" ToolTip="Duyệt" CommandArgument='<%#Eval("QRCodePackageChange_ID") %>'>Duyệt</asp:LinkButton>
                                                                <a href='/Admin/QRCodePackage/QRCodePackageChange_Edit?QRCodePackageChange_ID=<%#Eval("QRCodePackageChange_ID") %>' class="btn btn-gradient-primary " title="Xem chi tiết">Chi tiết</a>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div id="x_box_pager" style="float: right; text-align: right; margin-top: 10px" runat="Server" class="box_pager">
                                <label>Trang <%=Pager1.CurrentIndex %>/<%=TotalPage %></label>
                                (<label> <%=TotalItem %> yêu cầu</label>)
                                        <cc1:PagerV2_8 ID="Pager1" runat="server" OnCommand="Pager1_Command"
                                            GenerateFirstLastSection="True" GenerateGoToSection="False" GenerateHiddenHyperlinks="False"
                                            GeneratePagerInfoSection="False" NextToPageClause="" OfClause="/" PageClause=""
                                            ToClause="" CompactModePageCount="1" MaxSmartShortCutCount="5" NormalModePageCount="5"
                                            GenerateToolTips="False" BackToFirstClause="" BackToPageClause="" FromClause=""
                                            GenerateSmartShortCuts="False" GoClause="" GoToLastClause="" />
                                <div class="clear">
                                </div>
                            </div>
                            <%--     </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
        </div>
        <!-- container -->

        <!--  Modal content for the above example -->

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

