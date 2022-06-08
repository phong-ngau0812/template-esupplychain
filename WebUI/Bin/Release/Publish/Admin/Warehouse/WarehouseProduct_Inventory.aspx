<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="WarehouseProduct_Inventory, App_Web_z2libucy" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />

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
                                <li class="breadcrumb-item active"><a>Danh sách tồn kho sản phẩm </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Danh sách tồn kho sản phẩm </h4>
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
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-xs-12 mb-4 none" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductPackage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" Width="300px" AutoPostBack="true"  OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-12 col-xs-12 mb-4 right">
                                    <asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnExport" Text="Xuất file" OnClick="btnExport_Click" />

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
                                                        <th>Tên kho </th>
                                                        <th>Tên sản phẩm </th>
                                                       <%-- <th>Tên lô </th>--%>
                                                        <th>Tổng nhập </th>
                                                        <th>Tổng xuất </th>
                                                        <th>Tồn kho </th>
                                                        <th>Đơn vị tính </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptWarehouseExport" OnItemDataBound="rptWarehouseExport_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("WarehouseName") %></td>
                                                                <td><%#Eval("ProductName") %></td>
                                                                <%--<td><%#Eval("Code") %></td>--%>
                                                                <td><%#decimal.Parse(Eval("ImportAmount").ToString()).ToString("N0")%>
                                                                    <asp:Literal runat="server" ID="lblTongThuHoach" Text='<%#Eval("ImportAmount") %>' Visible="false"></asp:Literal>
                                                                </td>
                                                                <td><%#decimal.Parse(Eval("ExportAmount").ToString()).ToString("N0")%>
                                                                    <asp:Literal runat="server" ID="lblTongBanHang" Text='<%#Eval("ExportAmount") %>' Visible="false"></asp:Literal>
                                                                </td>
                                                                <td><%#decimal.Parse(Eval("ProductInventory").ToString()).ToString("N0")%>
                                                                    <asp:Literal runat="server" ID="lblTonKho" Text='<%#Eval("ProductInventory") %>' Visible="false"></asp:Literal>
                                                                </td>
                                                                <td><%#Eval("ProductUnit") %></td>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="table-warning">
                                                       <%-- <td class="text-right"></td>--%>
                                                        <td class="text-right"></td>
                                                        <td class="text-right">Tổng</td>
                                                        <td><%=TotalThuhoach.ToString("N0") %></td>
                                                        <td><%=TotalBanHang.ToString("N0") %></td>
                                                        <td><%=TotalTonkho.ToString("N0") %></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                                </tbody>
                                            </table>
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
        });
    </script>
</asp:Content>
