<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="WarehouseImport_List.aspx.cs" Inherits="WarehouseImport_List" %>

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
                                <li class="breadcrumb-item active"><a>Quản lý nhập kho </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý nhập kho </h4>
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
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server" visible="false">
                                    <asp:DropDownList runat="server" ID="ddlMaterialCategory" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlMaterialCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlWareHouse" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlWareHouse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Tên nguyên liệu/Mã lô"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server" visible="true">
                                    <asp:DropDownList runat="server" ID="ddlMaterial" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server" visible="true">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-4" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                
                                <div class="col-lg-12  col-xs-12 mb-4 right">

                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới nhập kho" OnClick="btnAdd_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnExportFile" Text="Xuất file" OnClick="btnExportFile_Click" />
                                    <%--<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="Button3" Text="Thêm mới sản phẩm" OnClick="btnAddSP_Click" />--%>
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
                                        <%--
                                <asp:ListItem Value="100"></asp:ListItem>--%>
                                    </asp:DropDownList>Tổng <%=TotalItem %>
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
                                                        <th>Thời gian</th>
                                                        <th runat="server" id="noxuanhoa">Mã Lô</th>
                                                        <th runat="server" id="xuanhoa">Mã lô vật tư NCC</th>
                                                        <th>Tên nguyên liệu</th>
                                                        <th>Kho</th>
                                                        <th>Số lượng</th>
                                                        <%--<th>Đơn vị tính </th>--%>
                                                        <th runat="server" id="nodatexuanhoa">Hạn sử dụng</th>
                                                        <th>Người nhập</th>
                                                        <th width="7%">Chức năng</th>
                                                        <%--<th width="10%">Kích hoạt</th>
                                                <th width="5%">Chức năng</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater runat="server" ID="rptWarehouseImport" OnItemCommand="rptWarehouseImport_ItemCommand" OnItemDataBound="rptWarehouseImport_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("ImportDate")%></td>
                                                                <%-- <td><a href='WarehouseImport_Edit?WarehouseImport_ID=<%#Eval("WarehouseImport_ID") %>' class="breadcrumb-item active font-15"><%#Eval("WarehouseName") %></td>--%>
                                                                <td runat="server" id="tdnoxuanhoa"><%#Eval("Code")%></td>
                                                                <td runat="server" id="tdxuanhoa"><%#Eval("CodeMaterialPackage") %></td>
                                                                <td><%#Eval("Name")%></td>
                                                                <td><%#Eval("WarehouseName") %></td>
                                                                <td><%# Eval("WarehouseImportType_ID").ToString()=="1"? decimal.Parse(Eval("Amount").ToString()).ToString("N0") +" "+ Eval("Unit").ToString() : LoadAmount(Eval("WarehouseImport_ID").ToString())  %></td>
                                                                <td runat="server" id="tdnodatexuanhoa">đến <%# string.IsNullOrEmpty(Eval("WarrantyEndDate").ToString())?"": DateTime.Parse(Eval("WarrantyEndDate").ToString()).ToString("dd/MM/yyyy")%> </td>
                                                                <%--<td><%#Eval("MaterialUnit")%></td>--%>
                                                                <td><%#Eval("Importer")%></td>
                                                                <%--<td><%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("dd/MM/yyyy")%></td>--%>

                                                                <%--<td align ="center" ><%#Eval("Count")%></td>--%>
                                                                <%--    <td>
                                                            <asp:CheckBox runat="server" ID="ckActive" Checked="true" AutoPostBack="true" OnCheckedChanged="ckActive_CheckedChanged" />
                                                            <asp:Literal runat="server" ID="lblID" Text='<%#Eval("StaffType_ID") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblApproved" Text='<%#Eval("Active") %>' Visible="false"></asp:Literal>
                                                        </td>--%>
                                                                <td align="center">
                                                                    <%--<asp:LinkButton runat="server" ID="btnActive" CommandName="Deactive" CssClass="mr-2" ToolTip="Kích hoạt" CommandArgument='<%#Eval("StaffType_ID") %>'><i class="fas fa-check text-success font-16"></i></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnDeactive" CommandName="Active" CssClass="mr-2" ToolTip="Ngừng kích hoạt" CommandArgument='<%#Eval("StaffType_ID") %>'><i class="fas fa-stop text-danger font-16"></i></asp:LinkButton>--%>
                                                                    <a href='WarehouseImport_Edit?WarehouseImport_ID=<%#Eval("WarehouseImport_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("WarehouseImport_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
            Init();
        });
    </script>
</asp:Content>
