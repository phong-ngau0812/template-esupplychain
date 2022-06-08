<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="BaoHiem_List.aspx.cs" Inherits="BaoHiem_List" %>

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
                                <li class="breadcrumb-item active"><a>Quản lý bảo hiểm</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý bảo hiểm</h4>
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
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Text="Tất cả" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Xe máy" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Ô tô" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-8 mb-3">
                                    <p class="text-muted mb-3 right">

                                        <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    </p>
                                </div>
                            </div>
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>

                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Họ tên chủ xe</th>
                                                <th>Loại xe</th>
                                                <th>Biển kiểm soát</th>
                                                <th>Số khung</th>
                                                <th>Số máy</th>
                                                <th width="8%">Kích hoạt</th>
                                                <th width="8%">Chức năng</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptSupplier" OnItemCommand="rptSupplier_ItemCommand" OnItemDataBound="rptSupplier_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#QRCode(Eval("BaoHiem_ID").ToString(), Eval("LoaiXe").ToString()) %>
                                                        </td>
                                                        <td><a href='BaoHiem_Edit?BaoHiem_ID=<%#Eval("BaoHiem_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Hoten") %></td>
                                                        <%--   <td>
                                                            <img src='<%#Common.GetImgNoURL( Eval("AvatarUrl"))%>' width="70px" /></td>--%>
                                                        <td><%#Eval("LoaiXe").ToString()=="1"?"Xe máy":"Ô tô"%></td>
                                                        <td><%#Eval("BienXe")%></td>
                                                        <td><%#Eval("SoKhung")%></td>

                                                        <td><%#Eval("SoMay")%></td>
                                                        <td align="center">
                                                            <%-- <asp:CheckBox runat="server" ID="ckActive" Checked="true" AutoPostBack="true" OnCheckedChanged="ckActive_CheckedChanged" />--%>
                                                            <asp:Literal runat="server" ID="lblID" Text='<%#Eval("BaoHiem_ID") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblApproved" Text='<%#Eval("Active") %>' Visible="false"></asp:Literal>
                                                        </td>
                                                        <td align="center">

                                                            <div class="div-edit" style="width: 29%" runat="server" id="Active">
                                                                <asp:LinkButton runat="server" ID="btnActive" CommandName="Deactive" CssClass="mr-2" ToolTip="Kích hoạt" CommandArgument='<%#Eval("BaoHiem_ID") %>'><i class="fas fa-check text-success font-16"></i></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="btnDeactive" CommandName="Active" CssClass="mr-2" ToolTip="Ngừng kích hoạt" CommandArgument='<%#Eval("BaoHiem_ID") %>'><i class="fas fa-stop text-danger font-16"></i></asp:LinkButton>
                                                            </div>

                                                            <div class="div-edit" style="width: 29%" runat="server" id="Edit">
                                                                <a href='BaoHiem_Edit?BaoHiem_ID=<%#Eval("BaoHiem_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            </div>
                                                            <div class="div-edit" style="width: 29%">
                                                                <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("BaoHiem_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                            </div>
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
        <asp:HiddenField runat="server" ID="ProductBrandList" />
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
                });
            }
        });
    </script>
</asp:Content>
