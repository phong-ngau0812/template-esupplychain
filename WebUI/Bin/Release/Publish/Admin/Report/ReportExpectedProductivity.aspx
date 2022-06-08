<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="ReportExpectedProductivity, App_Web_vohscbt0" %>

<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
                                <li class="breadcrumb-item active"><a>Báo cáo thông kê sản lượng dự kiến </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Báo cáo thông kê sản lượng dự kiến </h4>
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

                                <div class="col-lg-3 col-xs-12 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlProduct" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-xs-12 mb-3">
                                    <asp:DropDownList runat="server" ID="ddlZone" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                                </div>
                                <%--<div class="col-lg-3 col-xs-12 mb-3">
									<asp:DropDownList runat="server" ID="ddlArea" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
								</div>
								<div class="col-lg-3 col-xs-12 mb-3 ">
									<asp:DropDownList runat="server" ID="ddlFarm" OnSelectedIndexChanged="ddlFarm_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
								</div>--%>
                                <div class="col-lg-12 col-xs-12 mb-3 right">

                                    <%--<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />--%>
                                    <%-- <asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="Button1" Text="Xuất file" OnClick="btnExportFile_Click" />--%>
                                </div>
                            </div>

                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 style="text-align: center">Báo cáo thông kê sản lượng dự kiến theo sản phẩm </h4>
                                            <table id="datatableNoSort2" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th>Tên Lô</th>
                                                        <th style="width: 15%">Sản lượng dự kiến</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptProduct" OnItemDataBound="rptProduct_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><a href='/Admin/ProductPackage/ProductPackage_List?Code=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Name") %></td>
                                                                <td><%#string.IsNullOrEmpty( Eval("ExpectedProductivity").ToString())?0:Eval("ExpectedProductivity")%>  <%#string.IsNullOrEmpty( Eval("ExpectedProductivityDescription").ToString())?"": Eval("ExpectedProductivityDescription")%>/ <%#string.IsNullOrEmpty( Eval("Acreage").ToString())?0:Eval("Acreage")%>m2																	
                                                                    <asp:Literal runat="server" ID="lblAmountProduct" Text='<%#Eval("ExpectedProductivity") %>' Visible="false"></asp:Literal>
                                                                    <%--<asp:Literal runat="server" ID="Literal1" Text= <%=  %> Visible="false"></asp:Literal>--%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="table-warning none">
                                                        <td colspan="1" class="text-right">Tổng</td>
                                                        <td><%=TotalProduct.ToString("N0") %> (m2)</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="card" runat="server" id="Thongkesanluong">
                                        <div class="card-body">
                                            <h4 style="text-align: center">Báo cáo thông kê sản lượng dự kiến theo khu vực sản xuất </h4>
                                            <table id="datatableNoSort1" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th>Tên khu vực sản xuất</th>
                                                        <th style="width: 15%">Sản lượng dự kiến </th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater runat="server" ID="rptDataKVSX" OnItemDataBound="rptDataKVSX_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("Name")%></td>
                                                                <td><%#Eval("Sanluong")%> <%#Eval("Donvi")%>
                                                                    <asp:Literal runat="server" ID="lblAmountZone" Text='<%#Eval("Sanluong") %>' Visible="false"></asp:Literal>
                                                                    <asp:Literal runat="server" ID="lblDienTich" Text='<%#Eval("DienTich") %>' Visible="false"></asp:Literal>

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="table-warning">
                                                        <td colspan="1" class="text-right">Tổng</td>
                                                        <td><%=AmountZone.ToString("N0") %> <%=donvi %> /<%=AmountDT.ToString("N0") %> m2</td>
                                                    </tr>
                                                </tbody>
                                            </table>
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
