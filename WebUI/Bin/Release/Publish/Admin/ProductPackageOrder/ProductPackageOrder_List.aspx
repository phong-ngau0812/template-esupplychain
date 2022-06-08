<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="ProductPackageOrder_List, App_Web_hjladc4b" maintainscrollpositiononpostback="true" %>

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
                                <li class="breadcrumb-item active"><a>Quản lý lệnh sản xuất </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý lệnh sản xuất </h4>
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
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <div class="row">

                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlApproved" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="-- Chọn trạng thái duyệt --" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Chưa duyệt" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Đã duyệt" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 col-xs-12 mb-2 right">
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <br />
                            <%--       <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>--%>
                            <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                <thead>
                                    <tr>
                                        <th>Tên lệnh </th>
                                        <th>Doanh nghiệp</th>
                                        <th>Danh mục lệnh </th>
                                        <th>Trạng thái </th>
                                        <th>Định mức vật tư</th>
                                        <th class="text-center">Duyệt lệnh </th>
                                        <th width="8%">Chức năng</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater runat="server" ID="rptProductPackageOrder" OnItemCommand="rptProductPackageOrder_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><a href="ProductPackageOrder_Edit?ProductPackageOrder_ID=<%#Eval("ProductPackageOrder_ID") %>" class="breadcrumb-item active"><b><%#Eval("Name")%></b></a>
                                                    <br />
                                                    Tạo bởi: <b><%# Eval("NguoiTao")%></b> vào lúc  <%# string.IsNullOrEmpty( Eval("CreateDate").ToString())?"": DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>

                                                    <%#string.IsNullOrEmpty(Eval("AdminApproveDate").ToString())?"":("<br/>Duyệt bởi: <b>"+Eval("NguoiDuyet")+"</b> vào lúc "+ DateTime.Parse( Eval("AdminApproveDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss")+"")%>
                                                </td>
                                                <td><%#Eval("ProductBrandName")%></td>
                                                <td><%#Eval("ProductPackageOrderCategoryName")%></td>
                                                <td><%#Eval("Status")%>
                                                <td align="center"><%#CheckAmountMaterial(Eval("ProductPackageOrder_ID").ToString())%>
                                                            
                                                </td>
                                                <td align="center">
                                                    <asp:LinkButton runat="server" Visible='<%#Eval("Approve").ToString()=="0"?true:false %>' ID="btnApproved" CommandName="Approved" CssClass="mr-2" ToolTip="Duyệt lệnh" CommandArgument='<%#Eval("ProductPackageOrder_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn duyệt lệnh không?')"><i class="far fa-check-square text-success font-16"></i> Duyệt lệnh</asp:LinkButton>
                                                    <asp:Literal runat="server" ID="lbl" Visible='<%#Eval("Approve").ToString()=="1"?true:false %>'><span class="badge badge-success">Đã duyệt</span></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <a href='ProductPackageOrder_Edit?ProductPackageOrder_ID=<%#Eval("ProductPackageOrder_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                    <asp:LinkButton runat="server" ID="btnDelete" Visible='<%#MyActionPermission.CanDeleteProductPackageOrder(Convert.ToInt32(Eval("ProductPackageOrder_ID").ToString()),ref Message) %>' CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("ProductPackageOrder_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <%--    </ContentTemplate>
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
