<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="Task_List_Manufacturing_List, App_Web_pmdaiu5p" %>

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
                                <li class="breadcrumb-item active"><a>Nhật ký sản xuất</a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=name %></a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Nhật ký sản xuất - Lô <%=name %></h4>
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
                                        <asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnExport" Text="Báo cáo sản xuất" OnClick="btnExport_Click" />
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
                                                <%--<th>Lô sản xuất</th>--%>
                                                <th width="10%">Trạng thái</th>
                                                <th width="10%">Chức năng</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptTask" OnItemCommand="rptTask_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class='<%#Eval("StatusName").ToString()!="Đã hoàn thành" ?"table-danger":"" %>'>
                                                        <td>
                                                            <asp:Label ID="lblXuanHoa" runat="server" Visible="false"> <b>&diams; Bộ phận thực hiện: </b><%#Eval("NameDepartment") %></asp:Label>
                                                            <br />
                                                            <b>&diams; Người thực hiện (Who): </b><%#Eval("UserName") %> (<%# new MyUser().FullNameFromUserName( Eval("UserName").ToString()) %>)
                                                            <br />

                                                            <b>&diams; Đối tượng truy xuất (What):</b>
                                                            <br />
                                                            - Lô/ mẻ: <%#Eval("Code")+" | "+Eval("SGTIN") %>
                                                            <br />
                                                            - Tiêu chuẩn: <%# string.IsNullOrEmpty(Eval("Quality").ToString())? "tự công bố": Eval("Quality")%>
                                                            <br />
                                                            <b>&diams; Địa điểm thực hiện (Where):</b>
                                                            <i class="dripicons-location font-14"></i><%#string.IsNullOrEmpty( Eval("Location").ToString())?"Chưa xác định":Eval("Location") %>
                                                            <br />
                                                            <b>&diams; Thời gian thực hiện (When):</b>
                                                            Từ <%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %> đến <%# string.IsNullOrEmpty(Eval("EndDate").ToString())?"":DateTime.Parse( Eval("EndDate").ToString()).ToString("dd/MM/yyyy") %>
                                                            <br />
                                                            <b>&diams; Lý do (Why):</b>
                                                            <a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="text-primary font-16"><%#Eval("Name") %></a>
                                                            <br />
                                                            <a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>'>
                                                                <span class="btn btn-warning">Xem chi tiết</span>
                                                            </a>
                                                        </td>
                                                        <%--   <td>
                                                            <%#Eval("ProductPackageName") %>
                                                        </td>--%>
                                                        <td><%#Eval("StatusName") %>

                                                        </td>
                                                        <td align="center">
                                                            <a href='Task_List_Manufacturing_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Task_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <%--    <div class="card">
                        <div class="card-body">
                            <asp:UpdatePanel runat="server" ID="upHis">
                                <ContentTemplate>
                                    <h5 class="page-title breadcrumb-item active">Lịch sử nhật ký sản xuất</h5>
                                    <asp:Repeater runat="server" ID="rptPackage" OnItemDataBound="rptPackage_ItemDataBound" OnItemCommand="rptPackage_ItemCommand">
                                        <ItemTemplate>
                                            <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <td colspan="2" class="text-center" style="background: #fff;">
                                                            <h5 class="text-primary"><%#Eval("ProductPackageName") %> - <%#Eval("Name") %>
                                                                <asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                            </h5>
                                                            
                                                        </td>
                                                        <td class="text-center" style="background: #fff;"><asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%#Eval("ProductPackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhật ký không?')"><i class="fas fa-trash-alt text-danger font-20"></i> Xóa nhật ký</asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <th>Đầu mục công việc</th>
                                                        <th>Ngày thực hiện</th>
                                                        <th width="15%">Trạng thái</th>
                                                    </tr>
                                                </thead>
                                                <tbody>


                                                    <asp:Repeater runat="server" ID="rptTaskHistory">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%#Eval("Name") %>

                                                                    <br />
                                                                    Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                    <br />
                                                                    <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                                </td>
                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                              
                                                                <td><%#Eval("StatusName") %>

                                                                </td>
                                                           
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>



                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <asp:UpdatePanel runat="server" ID="up1">
                                <ContentTemplate>

                                    <h5 class="page-title breadcrumb-item active">Sử dụng lại nhật ký</h5>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <asp:DropDownList runat="server" ID="ddlProductPackageCopy" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackageCopy_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>
                                                <asp:Literal runat="server" ID="lblInfo"></asp:Literal>
                                            </label>
                                        </div>
                                    </div>

                                    <asp:Panel runat="server" ID="data" Visible="false">
                                        <p class="text-muted mb-3 right">
                                            <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnCopy" Text="Copy nhật ký" OnClick="btnCopy_Click" />
                                        </p>
                                        <br />
                                        <table id="datatableNoSortCopy" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                            <thead>
                                                <tr>
                                                    <th>Đầu mục công việc</th>
                                                    <th>Ngày thực hiện</th>
                                                    <th>Lô sản xuất</th>
                                                    <th width="15%">Trạng thái</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater runat="server" ID="rptTaskCopy">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("Name") %>

                                                                <br />
                                                                Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                <br />
                                                                <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                            </td>
                                                            <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                            <td>
                                                                <%#Eval("ProductPackageName") %>
                                                            </td>
                                                            <td><%#Eval("StatusName") %>

                                                            </td>
                                                         
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>--%>
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
                    $(".select2").select2({
                        width: '100%'
                    });
                });
            }
        });
    </script>
</asp:Content>
