<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="WarehouseExport_List, App_Web_ijujwaxe" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />

    <style>
        @media only screen and (max-width: 1024px) {
            .marginBtnAdd {
                margin-bottom: 7px
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
                                <li class="breadcrumb-item active"><a>Quản lý xuất kho </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý xuất kho </h4>
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
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <div class="row">
                                <div class="col-lg-3 col-xs-12 mb-4">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlProductPackageOrder" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductPackageOrder_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Tên phiếu"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4 right">
                                    <%--<asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnExport" Text="In phiếu " OnClick="ImportExcel_Click" Visible="false" />--%>
                                    <asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click1" />
                                    <asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <label>Hiển thị </label>
                                    <asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">
                                        <asp:ListItem Value="10"></asp:ListItem>
                                        <asp:ListItem Value="20"></asp:ListItem>
                                        <asp:ListItem Value="30"></asp:ListItem>
                                        <asp:ListItem Value="50"></asp:ListItem>
                                    </asp:DropDownList>Tổng <%=TotalItem %> phiếu xuất
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">

                                                <thead>
                                                    <tr>
                                                        <th>Tên phiếu </th>
                                                        <%--<th runat="server" id="tenlenh1">Lệnh </th>--%>
                                                        <th>Tên nguyên liệu</th>
                                                        <th>Mã lô</th>
                                                        <th>Số lượng</th>
                                                        <th>Người xuất kho</th>
                                                        <th>Người nhận </th>
                                                        <th>Ngày xuất</th>
                                                        <th width="9%">Chức năng</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptWarehouseExport" OnItemCommand="rptWarehouseExport_ItemCommand" OnItemDataBound="rptWarehouseExport_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><a href='WarehouseExport_Edit?WarehouseExport_ID=<%#Eval("WarehouseExport_ID") %>'><%#Eval("Name") %></a><br />
                                                                    <%#string.IsNullOrEmpty(Eval("Comment").ToString())?string.Empty: "( " + Eval("Comment") +")" %>
                                                                </td>
                                                                <%--<td><%#Eval("TenLenh") %></td>--%>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td><%#Eval("Exporter")%></td>
                                                                <td><%#Eval("Importer")%></td>
                                                                <td><%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy")%></td>
                                                                <td align="center">
                                                                    <a href='WarehouseExport_Download.aspx?WarehouseExport_ID=<%#Eval("WarehouseExport_ID") %>' target="_blank" title="Xuất phiếu" class="mr-2">
                                                                        <i class="typcn typcn-export font-22 text-warning"></i>
                                                                    </a>
                                                                    <a href='WarehouseExport_Edit?WarehouseExport_ID=<%#Eval("WarehouseExport_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("WarehouseExport_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
                                        (<label> <%=TotalItem %> Vật liệu</label>)
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
</asp:Content>
