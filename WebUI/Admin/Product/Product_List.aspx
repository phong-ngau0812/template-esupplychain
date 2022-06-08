<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="Product_List.aspx.cs" Inherits="Product_List" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/RWD-Table-Patterns/dist/css/rwd-table.min.css" rel="stylesheet" type="text/css" media="screen">
    <style>
        .btn-group.focus-btn-group {
            display: none;
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
                                <li class="breadcrumb-item active"><a>Quản lý sản phẩm</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý sản phẩm</h4>
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
                            <div class="row">
                                <div class="col-md-3 mb-3">

                                    <%--              <telerik:RadComboBox RenderMode="Lightweight" MaxHeight="300px" ID="ddlDMSP" Skin="MetroTouch" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%"  AutoPostBack="true" EmptyMessage="Chọn danh mục sản phẩm" Localization-ItemsCheckedString="danh mục sản phẩm được chọn">
                            <Localization CheckAllString="Chọn tất cả"
                                AllItemsCheckedString="Tất cả đều được chọn" />
                            <Items>
                                <telerik:RadComboBoxItem Text="ok" Value="1"/>
                                <telerik:RadComboBoxItem Text="ok1" Value="2"/>
                                <telerik:RadComboBoxItem Text="ok2" Value="3"/>
                                <telerik:RadComboBoxItem Text="ok3" Value="4"/>
                                <telerik:RadComboBoxItem Text="ok4" Value="5"/>
                            </Items>
                        </telerik:RadComboBox>--%>
                                    <asp:DropDownList runat="server" ID="ddlCha" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCha_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlTieuChuan" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlTieuChuan_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtName" placeholder="Tên sản phẩm" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <div class='checkbox checkbox-success font-13 ml-2'>
                                        <asp:CheckBox runat="server" ID="ckChung" Text="Hiển thị sản phẩm dùng chung" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlSo" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSo_SelectedIndexChanged"></asp:DropDownList>
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
                                <div class="col-md-12 right">

                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnCopy" Text="Copy nhanh đầu mục" OnClick="btnCopy_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnExport" Text="Xuất file" OnClick="btnExport_Click" />
                                </div>
                                <!-- end row -->
                            </div>

                            <div class="form-group">
                            </div>
                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <label>Hiển thị </label>
                            <asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">
                                <asp:ListItem Value="5"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="20"></asp:ListItem>
                                <%--<asp:ListItem Value="30"></asp:ListItem>--%>
                                <%--<asp:ListItem Value="50"></asp:ListItem>
                                <asp:ListItem Value="100"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th width="10%">Ảnh</th>
                                                        <th width="10%">QR Code</th>
                                                        <th width="40%">Tên danh mục</th>
                                                        <th width="10%">Màn hình quét tem</th>
                                                        <th width="15%">Đề mục công việc</th>
                                                        <th width="10%">Quản lý bình luận</th>
                                                        <th width="5%" class="text-center">Trạng thái</th>
                                                        <th width="10%" class="text-center">Chức năng</th></tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="grdProduct" OnItemDataBound="grdProduct_ItemDataBound" OnItemCommand="grdProduct_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <a href='Product_Edit?Product_ID=<%#Eval("Product_ID") %>' class="breadcrumb-item active">
                                                                        <img src='<%# Common.GetImg(Eval("Image"))%>' width="70" />
                                                                    </a>
                                                                </td>
                                                                <td>
                                                                    <%#QRCode(Eval("Product_ID").ToString()) %>
                                                                </td>
                                                                <td><a href='Product_Edit?Product_ID=<%#Eval("Product_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString().ToUpper()%></b></a><br />
                                                                    Danh mục:
                                                            <a href='?ProductCategory_ID=<%#Eval("ProductCategory_ID") %>' class="breadcrumb-item active" title="<%#Eval("ProductCategoryName") %>"><%# Common.CatChuoiHTML(Eval("ProductCategoryName").ToString(),42) %></a>
                                                                    <br />
                                                                    <%--Đăng bởi: <%# (new MyUser().FullNameFromUser_ID(Eval("CreateBy").ToString()))%> vào lúc  <%# DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                                    <br />
                                                                    Sửa bởi: <%#(new MyUser().FullNameFromUser_ID(Eval("LastEditBy").ToString())) %> vào lúc  <%# DateTime.Parse( Eval("LastEditDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>--%>
                                                                      Đăng bởi: <%# Eval("UserName")%> vào lúc  <%# DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                                    <br />
                                                                    Sửa bởi:  <%# string.IsNullOrEmpty(Eval("NguoiSua").ToString())?"":Eval("NguoiSua")%> vào lúc  <%# string.IsNullOrEmpty(Eval("LastEditDate").ToString())?"": DateTime.Parse( Eval("LastEditDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                                </td>
                                                                <td><a class='<%=style %>' href="ProductInfo.aspx?Product_ID=<%#Eval("Product_ID") %>"><i class="fas fa-edit text-success font-16"></i>Thông tin truy xuất</a></td>
                                                                <td><a class='<%=style %>' href="TaskStepProduct_List.aspx?Product_ID=<%#Eval("Product_ID") %>"><i class="fas fa-edit text-success font-16"></i>Quản lý đề mục công việc</a></td>
                                                                <td>
                                                                    <a class='<%=style %>' href="ProductReview_List.aspx?Product_ID=<%#Eval("Product_ID") %>"><i class="far fa-calendar-check text-warning font-16"></i>Quản lý bình luận sản phẩm </a>
                                                                </td>
                                                                <td>
                                                                    <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblID" Text='<%#Eval("Product_ID") %>' Visible="false"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblApproved" Text='<%#Eval("Active") %>' Visible="false"></asp:Literal>

                                                                </td>
                                                                <td align="center">
                                                                    <span class='<%=style %>'>
                                                                        <asp:LinkButton runat="server" ID="btnActive" CommandName="Deactive" CssClass="mr-2" ToolTip="Kích hoạt" CommandArgument='<%#Eval("Product_ID") %>'><i class="fas fa-check text-success font-16"></i></asp:LinkButton>
                                                                        <asp:LinkButton runat="server" ID="btnDeactive" CommandName="Active" CssClass="mr-2" ToolTip="Ngừng kích hoạt" CommandArgument='<%#Eval("Product_ID") %>'><i class="fas fa-stop text-danger font-16"></i></asp:LinkButton>
                                                                        <a href='Product_Edit?Product_ID=<%#Eval("Product_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    </span>
                                                                    <asp:LinkButton runat="server" ID="btnCopy" CommandName="Copy" CssClass="mr-2" ToolTip="Nhân bản sản phẩm" CommandArgument='<%#Eval("Product_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn nhân bản sản phẩm này không?')"><i class="fas fa-copy text-success font-16"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Product_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')" Visible='<%#MyActionPermission.CanDeleteProduct(Convert.ToInt32(Eval("Product_ID").ToString()),ref Message) %>'><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
                                        (<label> <%=TotalItem %> sản phẩm</label>)
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
    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
    <script src="/theme/assets/pages/jquery.datatable.init.js"></script>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>
    <!-- Responsive-table-->
    <script src="/theme/plugins/RWD-Table-Patterns/dist/js/rwd-table.min.js"></script>
    <script src="/theme/assets/pages/jquery.responsive-table.init.js"></script>
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
                    $('.table-responsive').responsiveTable({
                        addDisplayAllBtn: 'btn btn-secondary',
                    });
                });
            }
        });
    </script>
</asp:Content>
