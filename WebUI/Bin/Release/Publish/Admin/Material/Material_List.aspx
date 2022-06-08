<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="Material_List, App_Web_nad04s4b" %>

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
                                <li class="breadcrumb-item active"><a>Quản lý vật tư </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý vật tư </h4>
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
                                <div class="col-lg-3 col-xs-12 mb-4">
                                    <asp:DropDownList runat="server" ID="ddlMateriaType" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlMateriaType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4">
                                    <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <%--<asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                </div>
                                <div class="col-lg-3  col-xs-12 mb-4 right">

                                    <asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnExportFile" Text="Xuất file" OnClick="btnExportFile_Click" />
                                    <%--<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="Button3" Text="Xuất sản phẩm" OnClick="btnAddSP_Click" />--%>
                                </div>


                            </div>

                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="datatable" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>

                                                <%--<th>Tên doanh nghiệp </th>--%>
                                                <th>Tên vật tư </th>
                                                <th>Loại vật tư </th>
                                                <th>Đơn vị tính </th>
                                                <th>Giá vật tư (vnđ) </th>
                                                <th runat="server" visible="false" id="hidenXuanHoa">Cách ly (ngày) </th>

                                                <th width="10%">Chức năng</th>
                                                <%--<th width="10%">Kích hoạt</th>
                                                <th width="5%">Chức năng</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptMateria" OnItemCommand="rptMateria_ItemCommand" OnItemDataBound="rptMateria_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>

                                                        <%--<td><%#Eval("ProductBrandName")%></td>--%>
                                                        <td><a href='Material_Edit?Material_ID=<%#Eval("Material_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Name") %></td>
                                                        <td><%#Eval("NameMaterialCategory")%></td>
                                                        <td><%#Eval("Unit")%></td>
                                                        <td><%# decimal.Parse( Eval("MaterialPrice").ToString()).ToString("N0")%></td>
                                                        <td runat="server" id="tdnoxuanhoa"><%#Eval("IsolationDay")%></td>
                                                        <td align="center">
                                                            <div class="div-edit" style="width: auto;" runat="server" id="Edit" visible='<%#MyActionPermission.CanEdit() %>'>
                                                                <a href='Material_Edit?Material_ID=<%#Eval("Material_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                            </div>
                                                            <span>
                                                                <a href='Material_Add?Material_ID_Fill=<%#Eval("Material_ID") %>' class="mr-2" title="Copy thông tin"><i class="fas fa-copy text-success font-16"></i></i></a>
                                                            </span>
                                                            <div class="div-edit">
                                                                <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteMaterial(Convert.ToInt32(Eval("Material_ID").ToString()),ref Message) %>' CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Material_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
            //Init();
        });
    </script>
</asp:Content>
