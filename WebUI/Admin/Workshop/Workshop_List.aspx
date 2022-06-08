<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="Workshop_List.aspx.cs" Inherits="Workshop_List" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
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
        <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Quản lý nhân viên, hộ sản xuất</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý nhân viên,  hộ sản xuất</h4>
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
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtName" placeholder="Tên Nhân viên" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-2">
                                    <asp:DropDownList runat="server" ID="ddlLocation" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlWard" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-12 mb-3 right">
                                    <asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnExportFile" Text="Xuất file" OnClick="btnExportFile_Click" />

                                </div>

                            </div>
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>


                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <label>Hiển thị </label>
                                    <asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">

                                        <asp:ListItem Value="10"></asp:ListItem>
                                        <asp:ListItem Value="20"></asp:ListItem>
                                        <asp:ListItem Value="30"></asp:ListItem>
                                        <asp:ListItem Value="50"></asp:ListItem>
                                        <%--
                                <asp:ListItem Value="100"></asp:ListItem>--%>
                                    </asp:DropDownList>Tổng <%=TotalItem %> <%=Title %>
                                </div>
                                <div class="col-md-8 mb-3">
                                    <div class="form-group" style="margin-top: 10px">

                                        <asp:CheckBox runat="server" ID="cknhanvien" Text="Nhân viên" Class="test" AutoPostBack="true" OnCheckedChanged="CheckNhanVien_Check" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox runat="server" ID="ckhsx" Text="Hộ sản xuất" Class="test" AutoPostBack="true" OnCheckedChanged="CheckNhanVien_Check" />

                                    </div>
                                </div>
                            </div>


                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th>Tên nhân viên/Hộ sản xuất</th>
                                                        <th>Phòng Ban công tác </th>
                                                        <%--<th>Doanh nghiệp</th>--%>
                                                        <th width="10%">Chức năng</th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater runat="server" ID="rptWorkshop" OnItemCommand="rptWorkshop_ItemCommand" OnItemDataBound="rptWorkshop_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><a href='Workshop_Edit?Workshop_ID=<%#Eval("Workshop_ID") %>' class="breadcrumb-item active font-15" title="Sửa thông tin"><%#Eval("Name")%></a></td>
                                                                <td><%#Eval("NameDepartment") %></td>
                                                                <%--<td><a target="_blank" href='/Admin/ProductBrand/ProductBrand_Edit?ProductBrand_ID=<%#Eval("ProductBrand_ID") %>' title="Sửa thông tin"><%#Eval("ProductBrandName")%></a></td>--%>

                                                                <td align="center">


                                                                    <div class="div-edit" style="width: 29%" runat="server" id="Edit" visible='<%#MyActionPermission.CanEdit() %>'>
                                                                        <a href='Workshop_Edit?Workshop_ID=<%#Eval("Workshop_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    </div>
                                                                    <div class="div-edit" style="width: 29%">
                                                                        <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteWorkshop(Convert.ToInt32(Eval("Workshop_ID").ToString()),ref Message) %>' CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Workshop_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                                    </div>
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
                                        (<label> <%=TotalItem %> Nhân viên/chủ hộ</label>)
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
    <script type="text/javascript">
        $(document).ready(function () {
            $(".test input:checkbox").on('change', function () {
                $(".test input:checkbox").not(this).prop('checked', false);
            });
        });
    </script>
</asp:Content>
