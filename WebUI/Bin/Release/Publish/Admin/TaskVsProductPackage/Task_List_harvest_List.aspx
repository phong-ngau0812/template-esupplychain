<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="Task_List_harvest_List, App_Web_pmdaiu5p" %>

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
                                <li class="breadcrumb-item active"><a><%=TitleNKTH %></a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=name %></a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Đóng gói thành phẩm - Lô <%=name %></h4>
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

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackage_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <p class="text-muted mb-3 right">
                                         <asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnExport" Text="Xuất file" OnClick="btnExport_Click" />
                                        <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    </p>
                                </div>
                            </div>
                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Chi tiết nhật ký</th>
                                                <th>Số lượng thu hoạch</th>
                                                <th>Số ngày thu hoạch còn lại</th>
                                                <%--     <th>Ngày thu hoạch</th>--%>
                                                <th width="10%">Trạng thái</th>
                                                <th width="10%">Chức năng</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptTask" OnItemCommand="rptTask_ItemCommand" OnItemDataBound="rptTask_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class='<%#Eval("StatusName").ToString()!="Đã hoàn thành" ?"table-danger":"" %>'>
                                                        <td>
                                                            <b>&diams; Người thực hiện (Who): </b><%#Eval("UserName") %> (<%# new MyUser().FullNameFromUserName( Eval("UserName").ToString()) %>)
                                                            <br />

                                                            <b>&diams; Đối tượng truy xuất (What):</b>
                                                            <br />
                                                            - Lô/ mẻ: <%#Eval("Code")+" | "+Eval("SGTIN") %>
                                                            <br />
                                                            - Tiêu chuẩn:  <%# string.IsNullOrEmpty(Eval("Quality").ToString())? "tự công bố": Eval("Quality")%>
                                                            <br />
                                                            <b>&diams; Địa điểm thực hiện (Where):</b>
                                                            <i class="dripicons-location font-14"></i><%#string.IsNullOrEmpty( Eval("Location").ToString())?"Chưa xác định":Eval("Location") %>
                                                            <br />
                                                            <b>&diams; Thời gian thực hiện (When):</b>
                                                            <%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>
                                                            <br />
                                                            <b>&diams; Lý do (Why):</b>
                                                            <a href='Task_List_Harvest_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Name") %></a>
                                                            <br />
                                                            <a href='Task_List_Harvest_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>'>
                                                                <span class="btn btn-warning">Xem chi tiết</span>
                                                            </a>
                                                        </td>
                                                        <td>
                                                            <%--<br />
                                                            Người bán: <b><%#Eval("UserName") %></b>
                                                            <br />--%>
                                                            <%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%>
                                                            <%#" "+ Eval("Unit") %>
                                                            <asp:Literal runat="server" ID="lblTongThuHoach" Text='<%#Eval("HarvestVolume") %>' Visible="false"></asp:Literal>
                                                        </td>
                                                        <td><%#Eval("HarvestDayRemain") %>
                                                           

                                                        </td>

                                                        <%--<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>--%>
                                                        <td><%#Eval("StatusName") %>

                                                        </td>

                                                        <td align="center">
                                                            <a href='Task_List_Harvest_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Task_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr class="table-warning">
                                                <td class="text-right">Tổng: </td>
                                                <td colspan="4"><%=TotalThuhoach.ToString("N0") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                });
            }
        });
    </script>
</asp:Content>
