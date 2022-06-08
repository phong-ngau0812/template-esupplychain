<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="User_ListKD, App_Web_00zkwi3t" %>

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
                                <li class="breadcrumb-item active"><a>Quản lý tài khoản kinh doanh </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý tài khoản kinh doanh</h4>
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
                                <div class="col-lg-3 col-xs-12 mb-2 none" id="kd1" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2 none"  id="kd2" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                 <div class="col-lg-3 col-xs-12 mb-2 none"  id="kd3" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlDepartmentUser" CssClass="select2 form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-xs-12 mb-2 none" runat ="server" id ="FillFunctionGroup" visible ="false" >
                                    <asp:DropDownList runat ="server" ID ="ddlFunctionGroup" CssClass ="select2 form-control" OnSelectedIndexChanged ="ddlFunctionGroup_SelectedIndexChanged" AutoPostBack ="true"></asp:DropDownList>
                                </div>
                                <div  class="col-lg-12 col-xs-12 mb-2 right none" runat ="server" id="AddAccountAdmin" visible ="false">
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd1" Text="Thêm mới" OnClick="btnAdd_Click" />
                                </div>

                                <div class="col-lg-12 col-xs-12 mb-2 right " runat ="server" id="AddAccountProductBrand" >
                                    <p class="text-muted mb-3 right">
                                        <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    </p>
                                </div>
                            </div>
                            <%--<h4 class="mt-0 header-title">Buttons example</h4>--%>

                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>UserName</th>
                                                <th>Name</th>
                                                <th>Avatar</th>
                                                <th>Email</th>
                                                <th>Điện thoại</th>
                                                <th>Trạng thái</th>
                                                <th width="10%">Kích hoạt</th>
                                                <th width="10%">Chức năng</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptUser" OnItemCommand="rptUser_ItemCommand" OnItemDataBound="rptUser_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><a href='User_Edit?UserName=<%#Eval("UserName") %>' class="breadcrumb-item active font-15"><%#Eval("UserName") %></td>
                                                        <%-- <td><%#Eval("UserName")%></td>--%>
                                                        <td><%# new MyUser().FullNameFromUserName(Eval("UserName").ToString())%></td>
                                                        <td><%# new MyUser().AvatarFromUserName(Eval("UserName").ToString())%></td>                                                       <td><%#Eval("Email") %></td>
                                                        <td  <td><%# new MyUser().PhoneFromUserName(Eval("UserName").ToString())%></td>                                                      <td><%# new MyUser().OnlineStatus(Eval("UserName").ToString())%></td>                                                    <td align="center">
                                                            <%--<asp:CheckBox runat="server" ID="ckActive" Checked="true" AutoPostBack="true" OnCheckedChanged="ckActive_CheckedChanged" />--%>
                                                            <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblID" Text='<%#Eval("UserName") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblApproved" Text='<%# new MyUser().ApprovedStatus(Eval("UserName").ToString())%>' Visible="false"></asp:Literal>
                                                        </td>
                                                        <td align="center">
                                                            <asp:LinkButton runat="server" ID="btnActive" CommandName="Deactive" CssClass="mr-2" ToolTip="Kích hoạt" CommandArgument='<%#Eval("UserName") %>'><i class="fas fa-check text-success font-16"></i></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnDeactive" CommandName="Active" CssClass="mr-2" ToolTip="Ngừng kích hoạt" CommandArgument='<%#Eval("UserName") %>'><i class="fas fa-stop text-danger font-16"></i></asp:LinkButton>
                                                            <a href='User_Edit?UserName=<%#Eval("UserName") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("UserName") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnReset" CommandName="Reset" CommandArgument='<%#Eval("UserName") %>' ToolTip="Khôi phục mật khẩu" OnClientClick="return confirm('Bạn có chắc chắn muốn khôi phục mật khẩu?')"><i class="mdi mdi-lock-reset text-success font-16"></i></asp:LinkButton>
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
            $(".select2").select2({
                width: '100%'
            });
        })
        $(document).ready(function () {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
                    $(".select2").select2({
                        width: '100%'
                    });
                });
            }
        });
    </script>
</asp:Content>
