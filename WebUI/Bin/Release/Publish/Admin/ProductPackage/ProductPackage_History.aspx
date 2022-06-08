<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="ProductPackage_History, App_Web_quwo0bpb" %>

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
                                <li class="breadcrumb-item active"><a>Lịch sử SGTIN</a></li>
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
                            <h4>Lô: <%=namepackage %> - <%=name %></h4>
                            <div class="form-group">
                                <label>GTIN </label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlSGTIN" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn GTIN --"></asp:ListBox>

                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-gradient-primary" Text="Cập nhật lịch sử" OnClick="btnSave_Click" />
                                <br />
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                            <!-- Nav tabs -->
                            <hr />
                            <h5>LỊCH SỬ NHẬT KÝ MÃ GTIN</h5>
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab1" role="tab" aria-selected="true">Nhật ký sản xuất</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab2" role="tab" aria-selected="false">Nhật ký sơ chế chế biến</a>
                                </li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div class="tab-pane mt-3 active" id="tab1" role="tabpanel">
                                    <asp:Repeater runat="server" ID="rptPackage" OnItemDataBound="rptPackage_ItemDataBound">
                                        <ItemTemplate>
                                            <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <td colspan="3" class="text-center" style="background: #fff;">
                                                            <h5 class="text-primary"><%#Eval("SGTIN") %> | <%#Eval("ProductPackageName") %> - <%#Eval("ProductBrandName") %>
                                                                <asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                            </h5>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Đầu mục công việc</th>
                                                        <th>Ngày thực hiện</th>
                                                        <%--  <th>Lô sản xuất</th>--%>
                                                        <th width="15%">Trạng thái</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptTaskHistory">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%--<a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active font-15">--%>
                                                                    <%#Eval("Name") %>

                                                                    <%--</a>--%>
                                                                    <br />
                                                                    Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                    <br />
                                                                    <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                                </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                <%--   <td>
                                                                    <%#Eval("ProductPackageName") %>
                                                                </td>--%>
                                                                <td><%#Eval("StatusName") %>

                                                                </td>
                                                                <%--   <td align="center">
                                                            <a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Task_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                        </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="tab-pane mt-3" id="tab2" role="tabpanel">
                                    <asp:Repeater runat="server" ID="rptPackage1" OnItemDataBound="rptPackage1_ItemDataBound">
                                        <ItemTemplate>
                                            <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <td colspan="3" class="text-center" style="background: #fff;">
                                                            <h5 class="text-primary"><%#Eval("SGTIN") %> | <%#Eval("ProductPackageName") %> - <%#Eval("ProductBrandName") %>
                                                                <asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                            </h5>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Đầu mục công việc</th>
                                                        <th>Ngày thực hiện</th>
                                                        <%--  <th>Lô sản xuất</th>--%>
                                                        <th width="15%">Trạng thái</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptTaskHistory1">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%--<a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active font-15">--%>
                                                                    <%#Eval("Name") %>
                                                                    <%--</a>--%>
                                                                    <br />
                                                                    Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                    <br />
                                                                    <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                                </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                <%--   <td>
                                                                    <%#Eval("ProductPackageName") %>
                                                                </td>--%>
                                                                <td><%#Eval("StatusName") %>
                                                                </td>
                                                                <%--   <td align="center">
                                                            <a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Task_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                        </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <!--end col-->


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

</asp:Content>
