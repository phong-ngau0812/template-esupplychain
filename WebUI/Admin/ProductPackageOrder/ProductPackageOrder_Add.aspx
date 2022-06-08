<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="ProductPackageOrder_Add.aspx.cs" Inherits="ProductPackageOrder_Add" ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="sc"></telerik:RadScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="ProductPackageOrder_List.aspx">Quản lý lệnh sản xuất </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title"><%=title %></h4>
                        <asp:Label runat="server" ID="lblMessage1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Chọn doanh nghiệp <span class=" red">*</span> </label>
                                        <asp:DropDownList runat="server" ID="ddlProductBrand" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Tên lệnh <span class=" red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập tên lệnh"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Chọn danh mục lệnh  <span class=" red">*</span> </label>
                                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn danh mục lệnh"></asp:DropDownList>
                                    </div>
                                    <div class="form-group none">
                                        <label>SGTIN (Lịch sử vật tư) </label>
                                        <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlSGTIN" OnSelectedIndexChanged="ddlSGTIN_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn SGTIN --" AutoPostBack="true"></asp:ListBox>
                                        <a runat="server" visible="false" data-toggle="modal" data-target=".bd-example-modal-xl" id="viewmore" class="breadcrumb-item active mt-3" href="javascript:void(0);"><i class="mdi mdi-more font-18"></i>Xem chi tiết lịch sử</a>
                                    </div>
                                    <div class="form-group">
                                        <label>SGTIN (ngoài hệ thống) </label>
                                        <asp:TextBox runat="server" ID="txtSGTINTEXT" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Thời gian dự kiến hoàn thành</label>
                                        <div class="input-group" style="width: 300px">

                                            <asp:TextBox runat="server" ID="txtSX" ClientIDMode="Static" CssClass="form-control" Text="" />
                                            <%--Text="01/01/1980"--%>
                                            <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label>Mã lệnh <span class=" red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtCode" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập mã lệnh"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>PO đơn hàng<span class=" red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtPO" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập mã PO"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Sản phẩm  <span class="red">*</span></label>
                                        <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlProduct" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn các sản phẩm --" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn sản phẩm"></asp:ListBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Số lượng hàng </label>
                                        <asp:TextBox runat="server" ID="txtItemcount" ClientIDMode="Static" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                                    </div>

                                </div>
                            </div>


                            <div class="form-group">
                                <label>Kho</label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlWarehouse" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn kho --" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="form-group">

                                <div class="form-group">
                                    <label>Chọn vật tư</label>
                                    <telerik:RadComboBox RenderMode="Lightweight" MaxHeight="300px" ID="ddlMaterial" Filter="Contains" Skin="MetroTouch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Chọn vật tư " Localization-ItemsCheckedString="vật tư được chọn">
                                        <Localization CheckAllString="Chọn tất cả"
                                            AllItemsCheckedString="Tất cả đều được chọn" />
                                    </telerik:RadComboBox>
                                </div>
                                <%-- <label>Chọn vật tư</label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlMaterial" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn vật tư --" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged"></asp:ListBox>
                                --%><div runat="server" id="tbl" visible="false">
                                    <br />
                                    <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Tên vật tư</th>
                                                <th width="100px">Số lượng</th>
                                                <th width="10%">Đơn vị</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptMaterial">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("Name") %>
                                                            <asp:Literal runat="server" ID="lblMaterial_ID" Text='<%#Eval("Material_ID") %>' Visible="false"></asp:Literal>
                                                            <%--<asp:Literal runat="server" ID="lblWarehouseImport" Text='<%#Eval("WarehouseImport_ID") %>' Visible="false"></asp:Literal>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox></td>
                                                        <td><%#Eval("Unit") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Ảnh scan lệnh</label>
                                <br />
                                <div style="margin: 5px 0px;">
                                    <a href="<%=avatar %>" target="_blank">
                                        <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                    </a>
                                </div>
                                <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                            </div>
                            <div class="form-group">
                                <label>Trạng thái </label>
                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                            </div>
                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--end form-group-->

            <!--end form-->
            </>
            <!--end card-body-->

            <!--end card-->



            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>

            <div class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title mt-0" id="myModalLabel">LỊCH SỬ VẬT TƯ</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-12">
                                    <div class="card">
                                        <div class="card-body">


                                            <ul class="nav nav-tabs" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" data-toggle="tab" href="#tab1" role="tab" aria-selected="true">Nhật ký sản xuất</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#tab2" role="tab" aria-selected="false">Nhật ký sơ chế chế biến</a>
                                                </li>
                                            </ul>

                                            <!-- Tab panes -->
                                            <div class="tab-content">
                                                <div class="tab-pane mt-3 active" id="tab1" role="tabpanel">
                                                    <asp:Repeater runat="server" ID="rptPackage" OnItemDataBound="rptPackage_ItemDataBound">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                                <thead>
                                                                    <tr>
                                                                        <td colspan="3" class="text-center" style="background: #fff;">
                                                                            <h5 class="text-primary"><%#Eval("SGTIN") %> | <%#Eval("ProductPackageName") %> - <%#Eval("ProductBrandName") %>
                                                                                <asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                                            </h5>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th>Đầu mục công việc</th>
                                                                        <th>Ngày thực hiện</th>
                                                                        <th width="15%">Trạng thái</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>


                                                                    <asp:Repeater runat="server" ID="rptTaskHistory">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%#Eval("Name") %>
                                                                                    <br />
                                                                                    Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                                    <br />
                                                                                    <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                                                </td>
                                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                                                                <td><%#Eval("StatusName") %>

                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>



                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                                <div class="tab-pane mt-3" id="tab2" role="tabpanel">
                                                    <asp:Repeater runat="server" ID="rptPackage1" OnItemDataBound="rptPackage1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                                <thead>
                                                                    <tr>
                                                                        <td colspan="3" class="text-center" style="background: #fff;">
                                                                            <h5 class="text-primary"><%#Eval("SGTIN") %> | <%#Eval("ProductPackageName") %> - <%#Eval("ProductBrandName") %>
                                                                                <asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                                            </h5>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th>Đầu mục công việc</th>
                                                                        <th>Ngày thực hiện</th>
                                                                        <th width="15%">Trạng thái</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater runat="server" ID="rptTaskHistory1">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%#Eval("Name") %>
                                                                                    <br />
                                                                                    Người thực hiện: <b><%#Eval("UserName") %></b>
                                                                                    <br />
                                                                                    <i class="dripicons-location font-14"></i><%#Eval("Location") %>

                                                                                </td>
                                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>

                                                                                <td><%#Eval("StatusName") %>
                                                                                </td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <!--end col-->


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
            <asp:HiddenField runat="server" ID="hdfList_ID" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <script>
        function img() {
            var url = inputToURL(document.getElementById("<%=fulAnh.ClientID %>"));
            document.getElementById("<%=imganh.ClientID %>").src = url;
        }
        function inputToURL(inputElement) {
            var file = inputElement.files[0];
            return window.URL.createObjectURL(file);
        }
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            Init();
        });

        $(document).ready(function () {
            Init();
            $('#txtSX').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minYear: 1901,
                //maxYear: parseInt(moment().format('YYYY'), 10),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
            });
        });
    </script>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <!-- Parsley js -->
    <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>
    <!----date---->
    <%--<script src="/theme/plugins/select2/select2.min.js"></script>--%>
    <script src="/theme/plugins/moment/moment.js"></script>
    <script src="/theme/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"></script>
    <script src="/theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="/theme/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <script src="/theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="/theme/plugins/bootstrap-touchspin/js/jquery.bootstrap-touchspin.min.js"></script>
    <%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>

    <!--Wysiwig js-->
    <script src="/theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="/theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="/theme/assets/js/jquery.core.js"></script>
    <script src="../../js/Function.js"></script>
</asp:Content>

