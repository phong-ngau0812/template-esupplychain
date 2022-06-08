<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackage_List.aspx.cs" Inherits="QRCodePackage_List" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- Responsive datatable examples -->
    <%--<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/RWD-Table-Patterns/dist/css/rwd-table.min.css" rel="stylesheet" type="text/css" media="screen">
    <style>
        .btn-group.focus-btn-group {
            display: none;
        }

        .icon {
            padding: 5px 0px;
            margin-right: 3px;
        }

        @media only screen and (min-width: 1280px) {
            .table-responsive[data-pattern="priority-columns"] > .table > thead > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > tbody > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > tfoot > tr > th, .table-responsive[data-pattern="priority-columns"] > .table > thead > tr > td, .table-responsive[data-pattern="priority-columns"] > .table > tbody > tr > td, .table-responsive[data-pattern="priority-columns"] > .table > tfoot > tr > td {
                white-space: inherit !important;
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
                                <li class="breadcrumb-item active"><a>Quản lý lô mã</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý lô mã</h4>
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
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="-- Loại tem --"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Tem công khai"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Tem bí mật"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->

                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtName" placeholder="Tìm theo tên" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:TextBox runat="server" ID="txtSerial" placeholder="Tìm số Serial" CssClass="form-control"></asp:TextBox>
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
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnReport" Text="Báo cáo" OnClick="btnReport_Click" />
                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
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
                                <%--
                                <asp:ListItem Value="100"></asp:ListItem>--%>
                            </asp:DropDownList>
                            Tổng <%=TotalItem %> lô mã
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th width="6%">Ảnh</th>
                                                        <th width="25%">Thông tin lô mã</th>
                                                        <th width="7%">Loại tem</th>
                                                        <th width="2%"></th>
                                                        <th width="7%" class="text-center">Trạng thái</th>
                                                        <th width="7%" class="text-center"></th>
                                                        <th width="10%" class="text-center">Chức năng</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="grdProductPackage" OnItemDataBound="grdProductPackage_ItemDataBound" OnItemCommand="grdProductPackage_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <img src='<%# Common.GetImgQR(Eval("Image"))%>' width="70" />
                                                                </td>
                                                                <td>
                                                                    <a href='QRCodePackage_Edit?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString()%></b></a>
                                                                    <br />
                                                                    Sản phẩm:<b> <%#Eval("ProductName")%></b>
                                                                    <br />
                                                                    Đăng bởi: <%# Eval("UserName")%> vào lúc  <%# string.IsNullOrEmpty( Eval("CreateDate").ToString())?"": DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss tt") %>
                                                                    <br />
                                                                    Sửa bởi:  <%# Eval("NguoiSua")%> vào lúc  <%# string.IsNullOrEmpty( Eval("LastEditDate").ToString())?"": DateTime.Parse( Eval("LastEditDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss tt") %>
                                                                    <br />
                                                                    Tổng số <b><%#Eval("QRCodeNumber") %></b> tem<br />
                                                                    Serial từ <span class="badge badge-danger"><%#Eval("SerialNumberStart") %></span> đến <span class="badge badge-danger"><%#Eval("SerialNumberEnd") %></span>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("QRCodePackageType_ID").ToString()=="1"?"<span class=\"badge badge-success\"><i class='fas fa-lock-open'></i> Tem công khai</span>":"<span class=\"badge badge-primary\"><i class='fas fa-lock'></i> Tem bí mật</span> " %>
                                                                    <br />
                                                                    <a href='QRCodePackage_Download.aspx?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>&QRCodePackageType_ID=<%#Eval("QRCodePackageType_ID") %>&Product_ID=<%#Eval("Product_ID") %>' target="_blank"><i class="mdi mdi-download font-16"></i>Tải file mã</a>
                                                                </td>
                                                                <td>
                                                                    <asp:Literal runat="server" ID="lblType" Text='<%#Eval("QRCodePackageType_ID") %>' Visible="false"></asp:Literal>
                                                                    <asp:Label runat="server" ID="lbText"><a href='QRCodepackage_print_landing.aspx?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>&QRCodePackageType_ID=<%#Eval("QRCodePackageType_ID") %>&Product_ID=<%#Eval("Product_ID") %>' title="In mã" target="_blank">
                                                                        <img alt="In Mã" src="/images/print.png" style="width: 20px; height: 20px;" /></a></asp:Label>
                                                                </td>
                                                                <td align="center"><%#ReturnStatus(Eval("TrangThai").ToString()) %></td>
                                                                <td align="center">
                                                                    <asp:LinkButton runat="server" ID="btnActive" CommandName="Active" Visible='<%#Eval("TrangThai").ToString()=="Kích hoạt đưa mã tem ra thị trường"?false:true %>' ToolTip="Kích hoạt tem" CommandArgument='<%#Eval("QRCodePackage_ID") %>'><span class="badge badge-success"><i class="fas fa-check font-12"></i> Kích hoạt</span></asp:LinkButton>
                                                                </td>
                                                                <td align="center">
                                                                    <a href='QRCodePackage_Edit?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                        <img class="icon" src="../../img/icons/Edit16.png" title="Chỉnh sửa thông tin" /></a>
                                                                    <a href='QRCodePackage_Edit_Status.aspx?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                        <img class="icon" src="../../img/icons/status16.png" title="Cập nhật trạng thái lô mã" />
                                                                    </a>
                                                                    <a href='QRCodePackage_Edit_Pendingactive?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                        <img class="icon" src="../../img/icons/time16.png" title="Hẹn giờ kích hoạt lô mã" /></a>
                                                                    <a href='QRCodePackage_Edit_Info?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>'>
                                                                        <img class="icon" src="../../img/icons/updateproduct16.png" title="Cập nhật thông tin sản phẩm" /></a>
                                                                    <br />
                                                                    <a href="QRCodePackeLocationregister_List?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>">
                                                                        <img class="icon" src="../../img/icons/location16.png" title="Ấn định vị trí lô mã" /></a>
                                                                    <%--<a href="#">
                                                                        <img class="icon" src="../../img/icons/undo16.png" title="Khôi phục lô mã" /></a>--%>
                                                                    <asp:Literal runat="server" ID="lblProductBrand_ID" Text='<%#Eval("ProductBrand_ID") %>' Visible="false"></asp:Literal>
                                                                    <asp:LinkButton runat="server" ID="btnUndo" CommandName="Undo" Visible='<%# Convert.ToInt32( Eval("Product_ID").ToString())<0?false:true %>' ToolTip="Khôi phục" CommandArgument='<%#Eval("QRCodePackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn khôi phục không?')"> <img class="icon" src="../../img/icons/undo16.png" title="Khôi phục" /></asp:LinkButton>
                                                                    <span runat="server" visible='<%# Convert.ToInt32( Eval("Product_ID").ToString())<0?true:false %>'>
                                                                        <a href='QRCodePackage_Split.aspx?QRCodePackageSource_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                            <img class="icon" src="../../img/icons/split16.png" title="Chia lô mã" /></a>
                                                                    </span>
                                                                    <a href='QRCodePackeWarehouseregister_List?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>'>
                                                                        <img class="icon" src="../../img/icons/warehouse16.png" title="Phân kho cho lô mã" /></a>
                                                                    <a href="QRCodePackeCustomerregister_List?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>">
                                                                        <img class="icon" src="../../img/icons/customer16.png" title="Cập nhật thông tin khách hàng cho lô mã" /></a>
                                                                    <br />
                                                                    <a href='QRCodePackeWorkshopregister_List?QRCodePackage_ID=<%#Eval("QRCodePackage_ID")%>'>
                                                                        <img class="icon" src="../../img/icons/workshop16.png" title="Cập nhật thông tin hộ sản xuất cho lô mã" /></a>
                                                                    <a href='QRCodePacke_Edit_ProductPackage?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                        <img class="icon" src="../../img/icons/productpackage16.png" title="Cập nhật thông tin lô sản xuất" /></a>
                                                                    <a href='QRCodePackeCancel?QRCodePackage_ID=<%#Eval("QRCodePackage_ID") %>'>
                                                                        <img class="icon" src="../../img/icons/cancel16.png" title="Hủy tem" /></a>

                                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" ToolTip="Xóa" CommandArgument='<%#Eval("QRCodePackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"> <img class="icon" src="../../img/icons/delete16.png" title="Xóa" /></asp:LinkButton>

                                                                </td>

                                                                <%--  <td align="center">
                                                                    <br />
                                                                    <div class="div-edit" runat="server" id="Edit" visible='<%#MyActionPermission.CanEdit() %>'>
                                                                        <a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    </div>
                                                                    <div class="div-edit">
                                                                        <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteProductPackage(Convert.ToInt32(Eval("ProductPackage_ID").ToString()),ref Message) %>' ToolTip="Xóa" CommandArgument='<%#Eval("ProductPackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
                                                                    </div>
                                                                </td>--%>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <div id="x_box_pager" style="float: right; text-align: right; margin-top: 10px" runat="Server" class="box_pager">
                                        <label>Trang <%=Pager1.CurrentIndex %>/<%=TotalPage %></label>
                                        (<label> <%=TotalItem %> lô sản xuất</label>)
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
            </div>
        </div>
        <!-- end col -->


        <!-- container -->

        <!--  Modal content for the above example -->
        <asp:HiddenField runat="server" ID="ProductBrandList" />

        <!-- /.modal-dialog -->


    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Required datatable js -->
    <script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>

    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <%--<script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>--%>
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
