<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="ProductPackage_List.aspx.cs" Inherits="ProductPackage_List" %>

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

        .none1 {
            display: none !important;
        }
        .div-edit{
            width: 33%;
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
                                <li class="breadcrumb-item active"><a>Quản lý lô sản xuất</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý lô sản xuất</h4>
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
                            <div class="row" id="FormSearch">
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <!-- end row -->
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlWorkshop" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!-- end row -->

                                <div class="col-md-4 mb-3">
                                    <asp:TextBox runat="server" ID="txtName" placeholder="Tên lô / PO" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:TextBox runat="server" ID="txtCODE" placeholder="CODE" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:TextBox runat="server" ID="txtSGTIN" placeholder="Mã SGTIN" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <asp:Panel runat="server" ID="noxuanhoa" Visible="false" CssClass="row col-md-12">
                                    <div class="col-md-3 mb-3">
                                        <asp:DropDownList runat="server" ID="ddlSo" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-xs-12 mb-2">
                                        <asp:DropDownList runat="server" ID="ddlLocation" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:DropDownList runat="server" ID="ddlWard" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="xuanhoaLenh" CssClass="col-md-4 mb-4">

                                    <asp:DropDownList runat="server" ID="ddlProductPackageOrder" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackageOrder_SelectedIndexChanged"></asp:DropDownList>

                                </asp:Panel>
                                <div class="col-md-12 right">

                                    <asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" />
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
                                <%--
                                <asp:ListItem Value="100"></asp:ListItem>--%>
                            </asp:DropDownList>
                            Tổng <%=TotalItem %> lô sản xuất
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>

                                    <div class="table-rep-plugin">
                                        <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                            <table id="tech-companies-1" class="table table-striped mb-0">
                                                <thead>
                                                    <tr>
                                                        <th width="6%">Ảnh</th>
                                                        <th width="25%">Tên lô</th>
                                                        <th width="15%">Dữ liệu thu thập</th>
                                                        <th width="15%">Dữ liệu lưu trữ</th>
                                                        <th width="15%" class="text-center">Dữ liệu chia sẻ</th>
                                                        <th width="7%" class="text-center">Chức năng</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="grdProductPackage" OnItemDataBound="grdProductPackage_ItemDataBound" OnItemCommand="grdProductPackage_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active">
                                                                        <img src='<%# Common.GetImgQR(Eval("Image"))%>' width="70" />
                                                                    </a>
                                                                    <%--  <%#QRCode(Eval("ProductPackage_ID").ToString()) %>--%>
                                                                </td>
                                                                <td>
                                                                    <b>&diams; Thông tin lô: </b>

                                                                    <b><%#Eval("TypeP").ToString()=="2"?" LÔ CHẾ BIẾN" :" LÔ SẢN XUẤT" %></b>
                                                                    <br />
                                                                    Người tạo: <%# Eval("UserName")%>
                                                                    <br />
                                                                    <b>&diams; Thông tin D/N (Who):</b><br />

                                                                    - <%#Eval("ProductBrandName") %>
                                                                    <br />
                                                                    - Vùng sản xuất:  <%#Eval("ZoneName") %>
                                                                    <%#Eval("Type").ToString()=="2"?"<br />- Hộ sản xuất: "+Eval("HoSX"):"" %>
                                                                    <br />
                                                                    <b>&diams; Đối tượng truy xuất (What)</b>
                                                                    <br />
                                                                    <%#Eval("TypeP").ToString()=="2"?"- Quy trình công nghệ:" + LoadNameQTCN(Eval("ManufactureTech_ID").ToString()):"- Tên sản phẩm:<a href='javascript:void(0);' onclick='GetProductInfo("+Eval("Product_ID")+") data-toggle='modal' data-target='.bd-example-modal-xl' class='text-success'><b>"+Eval("Pname")+"("+ Eval("ItemCount")+" cá thể)</b></a>" %>
                                                                    <%--<a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString().ToUpper()%></b></a>--%>
                                                                    <br />
                                                                    - Tên Lô: <a class="text-danger" href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>'><b><%--<%#Eval("CODE")%> | <%#Eval("SGTIN") %>--%> <%#Eval("Name") %></b></a>
                                                                    <br />
                                                                    - Lô/mẻ: <a class="text-danger" href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>'><b><%#Eval("CODE")%> | <%#Eval("SGTIN") %></b></a>
                                                                    <br />
                                                                    <%#string.IsNullOrEmpty(Eval("CodePO").ToString())?"":"- PO: "+Eval("CodePO")+"<br/>" %>

                                                                    - Tiêu chuẩn: <%# string.IsNullOrEmpty( Eval("QualityName").ToString())?"Tự công bố":Eval("QualityName") %>
                                                                    <br />
                                                                    <b>&diams; Nơi sản xuất (Where): </b>
                                                                    <br />
                                                                    <%#string.IsNullOrEmpty(Eval("AddressFarm").ToString())?Eval("ProductBrandName"):Eval("AddressFarm") %>

                                                                    <br />
                                                                    <b>&diams; Thời gian sản xuất (When): </b>
                                                                    <br />
                                                                    Từ <%#  string.IsNullOrEmpty( Eval("StartDate").ToString())?"": DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString())%> đến <%#  string.IsNullOrEmpty( Eval("EndDate").ToString())?"": DateTime.Parse( Eval("EndDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString())%>
                                                                    <br />
                                                                    <b>&diams; Lý do (Why): </b><%# string.IsNullOrEmpty(Eval("TenLenh").ToString())?"Lệnh sản xuất": "<span class='badge badge-danger'>"+ Eval("TenLenh").ToString()+"</span>"%>
                                                                    <br />

                                                                    <%--Hộ sản xuất:<b> <%#Eval("HoSX")%></b>--%>
                                                                    <%--<br />
                                                                    Đăng bởi: <%# Eval("UserName")%> vào lúc  <%# string.IsNullOrEmpty( Eval("CreateDate").ToString())?"": DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss tt") %>
                                                                    <br />
                                                                    Sửa bởi:  <%# Eval("NguoiSua")%> vào lúc  <%# string.IsNullOrEmpty( Eval("LastEditDate").ToString())?"": DateTime.Parse( Eval("LastEditDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss tt") %>--%>

                                                                  Trạng thái: <%#ReturnStatus( Eval("TrangThai").ToString()) %>
                                                                    <%-- <br />
                                                                    <a href="ProductPackage_Export.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" target="_blank"><i class="typcn typcn-export font-16"></i>Xuất mã lô sản xuất</a>--%>
                                                                </td>
                                                                <td>
                                                                    <a href="javascript:void(0);" onclick='GetTestingCertificatesInfo(<%#Eval("ProductPackage_ID")%>)' data-toggle="modal" data-target=".xl123"><i class="mdi mdi-more font-16"></i>Cập nhật và xem phiếu kiểm nghiệm...</a>
                                                                    <br />
                                                                    <a href="ProductPackage_Download.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" target="_blank"><i class="mdi mdi-download font-16"></i>Tải file mã lô sản xuất</a>
                                                                    <br />
                                                                    <asp:Panel runat="server" ID="pn" Visible='<%#MyUser.GetFunctionGroup_ID()=="2"?false:true %>'>
                                                                        <a href="ProductPackage_History.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" target="_blank"><i class="fas fa-history"></i>&nbsp;Lịch sử nguồn nguyên liệu</a>
                                                                    </asp:Panel>
                                                                    <a href="javascript:void(0);"><i class="mdi mdi-truck-delivery font-16"></i>Dữ liệu giao nhận</a>
                                                                    <br />
                                                                    <a href="javascript:void(0);"><i class="mdi mdi-file-import font-16"></i>Phiếu nhập vật tư</a>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel runat="server">
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %> <%=CheckUserQL(1)%>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký sản xuất...</a>
                                                                        <br />
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Material_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %> <%=CheckUserQL(2)%>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký vật tư...</a>
                                                                        <br />

                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Harvest_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %> <%=CheckUserQL(3)%>'><i class="mdi mdi-table-edit font-16"></i><%=TitleNKTH %></a>

                                                                        <br />
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Transport_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":""%> <%=CheckUserQL(5)%>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký vận chuyển...</a>
                                                                        <br />
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Selling_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":""%> <%=CheckUserQL(6)%>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký bán hàng...</a>
                                                                        <br />
                                                                        <asp:Panel runat="server" Visible='<%#Eval("TypeP").ToString()=="2"?true:false %>'>
                                                                            <a href="/Admin/TaskVsProductPackage/Task_List_Processing_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %> <%=none %> <%=CheckUserQL(4)%>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký sơ chế chế biến...</a>
                                                                        </asp:Panel>
                                                                        <br />
                                                                        <a href="ProductPackage_Material.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>"><i class="mdi mdi-settings-outline font-16"></i><%=TitleXH %></a>
                                                                        <marquee style="color: red;" onmouseover="this.stop()" onmouseout="this.start()"><%#NotiUpdateMaterial(Convert.ToInt32( Eval("ProductPackage_ID")),string.IsNullOrEmpty(Eval("Product_ID").ToString())?0:Convert.ToInt32(Eval("Product_ID")))%></marquee>
                                                                        <br />
                                                                        <a href="TaskStepVsProductPackage_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>&Product_ID=<%#Eval("Product_ID") %>"><i class="mdi mdi-timer font-16"></i>Cài đặt thời gian đầu mục CV</a>
                                                                        <br />
                                                                        <a href='/Admin/Product/TaskStepProduct_List?Product_ID=<%#Eval("Product_ID")%>' target="_blank"><i class="mdi mdi-settings-outline font-16"></i>Cài đặt đầu mục cho sản phẩm</a>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" Visible='<%#Eval("TypeP").ToString()=="2"?true:false %>'>
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %>'><i class="mdi mdi-table-edit font-16"></i>Nhật ký sản xuất...</a>
                                                                        <br />
                                                                        <a href="/Admin/TaskVsProductPackage/Task_List_Harvest_List.aspx?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":"" %>'><i class="mdi mdi-table-edit font-16"></i><%=TitleNKTH %></a>
                                                                        <br />
                                                                        <asp:LinkButton runat="server" ID="btnActive" CommandName="Active" Visible='<%#Eval("TrangThai").ToString()=="Chưa kích hoạt"?true:false %>' ToolTip="Kích hoạt lô chế biến" CommandArgument='<%#Eval("ProductPackage_ID") %>'><span class="badge badge-success"><i class="fas fa-check font-12"></i> Kích hoạt lô chế biến</span></asp:LinkButton>
                                                                    </asp:Panel>
                                                                    <%-- <%#Eval("TypeP").ToString()=="2"?"<a href='/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID")+"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký sản xuất...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Harvest_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":"" +"'><i class='mdi mdi-table-edit font-16'></i>"+"</a><br /><asp:LinkButton runat='server' ID='btnActive' CommandName='Active' Visible='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"true":"false" +"' ToolTip='Kích hoạt lô' CommandArgument='"+Eval("ProductPackage_ID")+"'><span class='badge badge-success'><i class='fas fa-check font-12'></i> Kích hoạt</span></asp:LinkButton>":"<a href='/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID")+"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký sản xuất...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Material_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID")+" class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký vật tư...</a><br /> <a href='/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID")+" class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký sản xuất...</a><br /> <a href='/Admin/TaskVsProductPackage/Task_List_Manufacturing_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký sản xuất...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Material_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":"" +"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký vật tư...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Harvest_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":"" +"'><i class='mdi mdi-table-edit font-16'></i>"+"</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Transport_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký vận chuyển...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Selling_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID")+"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký bán hàng...</a><br /><a href='/Admin/TaskVsProductPackage/Task_List_Processing_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"' class='"+Eval("TrangThai").ToString()=="Chưa kích hoạt"?"none":""+"'><i class='mdi mdi-table-edit font-16'></i>Nhật ký sơ chế chế biến...</a><br /><a href='ProductPackage_Material.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"><i class='mdi mdi-settings-outline font-16'></i>Cài đặt định mức vật tư</a><br /><a href='TaskStepVsProductPackage_List.aspx?ProductPackage_ID="+Eval("ProductPackage_ID") +"&Product_ID="+Eval("Product_ID") +"'><i class='mdi mdi-timer font-16'></i>Cài đặt thời gian đầu mục CV</a><br /><a href='/Admin/Product/TaskStepProduct_List?Product_ID="+Eval("Product_ID")+"' target='_blank'><i class='mdi mdi-settings-outline font-16'></i>Cài đặt đầu mục cho sản phẩm</a>"%>--%>
                                                                    <%--  <a href="javascript:void(0);" onclick='GetProductInfo(<%#Eval("Product_ID")%>)' data-toggle="modal" data-target=".bd-example-modal-xl"><i class="mdi mdi-file-search font-16"></i>Xem thông tin sản phẩm...</a>
                                                                    <br />--%>
                                                                    <%--<a href='TestingCertificatesVsProductPackage_List?ProductPackage_ID=<%#Eval("ProductPackage_ID")%>' target="_blank" ><i class="mdi mdi-more font-16"></i>Xem phiếu kiểm nghiệm...</a>--%>
                                                                </td>
                                                                <%-- <td><i class="mdi mdi-av-timer font-16"></i>Bắt đầu:
                                                                    <br />
                                                                    <%#  string.IsNullOrEmpty( Eval("StartDate").ToString())?"": DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString())%>
                                                                    <br />
                                                                    <br />
                                                                    <i class="mdi mdi-av-timer font-16"></i>Dự kiến thu hoạch<br />
                                                                    <%#  string.IsNullOrEmpty( Eval("EndDate").ToString())?"": DateTime.Parse( Eval("EndDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString())%></td>--%>
                                                                <td>
                                                                    <a href='#' title="Xuất html nhật ký">
                                                                        <i class="typcn typcn-export font-18 text-warning"></i>Xuất mã thùng
                                                                    </a>
                                                                    <br />
                                                                    <a href='/Admin/ProductPackage/ProductPackage_Trace?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' target="_blank" title="Xuất html nhật ký" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":""%>'>
                                                                        <i class="typcn typcn-export font-18 text-warning"></i>Xuất mã truy vết (QR-code)
                                                                    </a>
                                                                    <br />

                                                                    <a href="ProductPackageVsTaskType?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>"><i class="fab fa-whmcs font-16 text-warning"></i>Cài đặt hiển thị nhật ký bên cổng truy xuất...</a>
                                                                    <br />
                                                                    <a href='/Admin/ProductPackage/ProductPackage_HTML?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' target="_blank" title="Xuất html nhật ký" class='<%#(Eval("TrangThai").ToString())=="Chưa kích hoạt"?"none":""%>'>
                                                                        <i class="typcn typcn-export font-18 text-warning"></i>Xuất html nhật ký
                                                                    </a>

                                                                    <marquee style="color: red;" onmouseover="this.stop()" onmouseout="this.start()"><%# NotificationTask(Convert.ToInt32( Eval("ProductPackage_ID"))) %></marquee>
                                                                </td>
                                                                <%--<td><a href='Product_Edit?Product_ID=<%#Eval("Product_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString().ToUpper()%></b></a><br />
                                                                    Danh mục:
                                                            <a href='?ProductCategory_ID=<%#Eval("ProductCategory_ID") %>' class="breadcrumb-item active"><%#Eval("ProductCategoryName") %></a>
                                                                    <br />
                                                                   
                                                                      Đăng bởi: <%# Eval("UserName")%> vào lúc  <%# DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                                    <br />
                                                                    Sửa bởi:  <%# Eval("NguoiSua")%> vào lúc  <%# DateTime.Parse( Eval("LastEditDate").ToString()).ToString("dd/MM/yyyy hh:mm:ss") %>
                                                                </td>
                                                                <td><a href="TaskStepProduct_List.aspx?Product_ID=<%#Eval("Product_ID") %>"><i class="fas fa-edit text-success font-16"></i>Quản lý đề mục công việc</a></td>
                                                                <td>
                                                                    <asp:Literal runat="server" ID="lblText"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblID" Text='<%#Eval("Product_ID") %>' Visible="false"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblApproved" Text='<%#Eval("Active") %>' Visible="false"></asp:Literal>

                                                                </td>--%>
                                                                <td align="center">
                                                                    <br />
                                                                    <div class="div-edit" runat="server" id="Edit" visible='<%#MyActionPermission.CanDeleteProductPackage(Convert.ToInt32(Eval("ProductPackage_ID").ToString()),ref Message) %>'>
                                                                        <a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
                                                                    </div>

                                                                    <div class="div-edit" runat="server" id="Div1" visible='<%#MyActionPermission.CanDeleteProductPackage(Convert.ToInt32(Eval("ProductPackage_ID").ToString()),ref Message) %>'>
                                                                        <a href='ProductPackage_Add?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' title="Copy lô"><i class="fas fa-copy text-success font-16"></i></a>
                                                                    </div>

                                                                    <div class="div-edit">
                                                                        <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteProductPackage(Convert.ToInt32(Eval("ProductPackage_ID").ToString()),ref Message) %>' ToolTip="Xóa" CommandArgument='<%#Eval("ProductPackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
                            <div class="modal fade bd-example-modal-xl " tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title mt-0" id="myModalLabel"></h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        </div>
                                        <div class="modal-body">

                                            <div class="row">
                                                <div class="col-md-8 mb-3">
                                                    <p id="chungloai">
                                                    </p>
                                                    <p id="tieuchuan">
                                                    </p>
                                                    <p id="day">
                                                    </p>
                                                    <p id="content">
                                                    </p>
                                                </div>
                                                <div class="col-md-4 mb-3">
                                                    <img class="img-fluid" id="imgProduct">
                                                </div>
                                            </div>

                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-danger waves-effect" data-dismiss="modal">Tắt cửa sổ</button>
                                            <%--<button type="button" class="btn btn-primary waves-effect waves-light">Save changes</button>--%>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                            <!-- /.modal -->
                            <script>
                                function GetProductInfo(Product_ID) {
                                    $("#myModalLabel").html('');
                                    $("#chungloai").html('')
                                    $("#tieuchuan").html('')
                                    $("#day").html('')
                                    $("#imgProduct").attr("src", "");
                                    $.ajax({
                                        url: "/WebServices/Product.asmx/GetInfoProduct",
                                        type: "GET",
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        data: { Product_ID: Product_ID },
                                        async: false,
                                        success: function (result) {
                                            if (result.d != "") {
                                                var data = $.parseJSON(result.d);
                                                //console.log(data.length);
                                                if (data.length > 0) {
                                                    console.log(data);
                                                    $.each(data, function (index, item) {
                                                        $("#myModalLabel").html(item.Name);
                                                        $("#chungloai").html("Chủng loại: <b>" + item.ProductCategoryName + "</b>")
                                                        $("#tieuchuan").html("Tiêu chuẩn: <b>" + item.TieuChuan + "</b>")
                                                        $("#day").html("Số ngày sinh trưởng: <b>" + item.GrowthByDay + "</b>")
                                                        $("#content").html("Mô tả: <b>" + item.Content + "</b>")
                                                        $("#imgProduct").attr("src", "../../data/product/mainimages/original/" + item.Image);
                                                    });
                                                }
                                            }
                                        },
                                        error: function (errormessage) {
                                            //alert(errormessage.responseText);
                                            // window.showToast("err", "Có lỗi xảy ra, vui lòng thử lại sau.!", 3000);
                                        }
                                    });
                                }
                            </script>
                            <div class="modal fade bd-example-modal-xl xl123" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title mt-0" id="myModalLabelTestingCertificates"></h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>

                                        </div>
                                        <div class="modal-body">

                                            <div class="row">
                                                <div class="col-md-12 mb-3" id="DataCertificates">
                                                </div>

                                            </div>

                                        </div>
                                        <div class="modal-footer">
                                            <a id="btnHref" class="btn btn-primary waves-effect waves-light">Cập nhật phiếu kiểm nghiệm</a>
                                            <button type="button" class="btn btn-danger waves-effect" data-dismiss="modal">Tắt cửa sổ</button>

                                            <%--<button type="button" class="btn-gradient-primary" data-dismiss="modal" aria-hidden="true"></button>--%>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>

                            <!-- /.modal-content -->
                        </div>


                        <script>
                            function GetTestingCertificatesInfo(ProductPackage_ID) {
                                $("#myModalLabelTestingCertificates").html('');
                                $("#Name").html('')
                                $("#StartDate").html('')
                                $("#EndDate").html('')
                                $("#LinkFile").attr("src", "");
                                $.ajax({
                                    url: "/WebServices/TestingCertificates.asmx/GetInfoTestingCertificates",
                                    type: "GET",
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    data: { ProductPackage_ID: ProductPackage_ID },
                                    async: false,
                                    success: function (result) {
                                        $("#myModalLabelTestingCertificates").html("Phiếu kiểm nghiệm");
                                        $("#DataCertificates").html('');
                                        if (result.d != "") {
                                            $("#btnHref").attr("href", "ProductPackage_Edit?ProductPackage_ID=" + ProductPackage_ID);
                                            var data = $.parseJSON(result.d);
                                            //console.log(data.length);
                                            if (data.length > 0) {
                                                console.log(data);
                                                var html = '<table  class="table table-striped table-bordered dt-responsive nowrap"><thead><tr><th>Tên phiếu kiểm nghiệm</th><th>Ngày có hiệu lực</th><th>Ngày hết hiệu lực</th><th>LinkFile</th></tr></thead><tbody>';
                                                $.each(data, function (index, item) {

                                                    html += ("<tr><td> " + item.Name + "</td>")
                                                    html += ("<td>" + item.StartDate + "</td>")
                                                    html += ("<td>" + item.EndDate + "</td>")
                                                    html += ("<td> <a href='" + item.LinkFile + "' target=_blank >" + item.LinkFile + "</a></td></tr>")
                                                    //$("#Name").html("Tên phiếu kiểm nghiệm: <b>" + item.Name + "</b>")
                                                    //$("#StartDate").html("Ngày có hiệu lực: <b>" + item.StartDate + "</b>")
                                                    //$("#EndDate").html("Ngày hết hiệu lực: <b>" + item.EndDate + "</b>")
                                                });
                                                html += '</tbody></table>';
                                                $("#DataCertificates").html(html);
                                            }
                                        }
                                    },
                                    error: function (errormessage) {
                                        //alert(errormessage.responseText);
                                        // window.showToast("err", "Có lỗi xảy ra, vui lòng thử lại sau.!", 3000);
                                    }
                                });
                            }
                        </script>
                        <!-- /.modal-dialog -->
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
    <%--<script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>--%>
    <!-- Buttons examples -->
    <%--<script src="/theme/plugins/datatables/dataTables.buttons.min.js"></script>--%>
    <%--<script src="/theme/plugins/datatables/buttons.bootstrap4.min.js"></script>--%>
    <%--    <script src="/theme/plugins/datatables/jszip.min.js"></script>
    <script src="/theme/plugins/datatables/pdfmake.min.js"></script>
    <script src="/theme/plugins/datatables/vfs_fonts.js"></script>--%>
    <%--<script src="/theme/plugins/datatables/buttons.html5.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.print.min.js"></script>
    <script src="/theme/plugins/datatables/buttons.colVis.min.js"></script>--%>
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

            if (getUrlParameter('Code').length > 0) {
                $("#FormSearch").hide();
                $("#DateP").hide();
            }
        });
    </script>
    <script>
        var getUrlParameter = function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
        };
    </script>
</asp:Content>
