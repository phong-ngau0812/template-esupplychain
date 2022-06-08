<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="ProductPackage_Edit.aspx.cs" Inherits="ProductPackage_Edit" ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../css/telerik.css" rel="stylesheet" type="text/css" />
    <style>
        @media only screen and (min-width: 1024px) {
            label {
                width: 30%;
                float: left;
                line-height: 23px;
                margin-top: .5rem;
            }

            .form-group {
                margin-bottom: 20px;
                width: 100%;
                float: left;
            }

            .form-control {
                float: left;
                width: 70% !important;
            }

            .select2-container {
                width: 70% !important;
                float: left;
            }

            .input-group {
                width: 70%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1"></telerik:RadScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="ProductPackage_List">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title"><%=title %></h4>
                        <div class="radio radio-primary row-cols-lg-2" style="width: 100%; display: inline-flex">
                            <div class="custom-control custom-radio">
                                <asp:RadioButton runat="server" Checked="true" ID="AddLoSanXuat" AutoPostBack="true" GroupName="rdo" Text=" Lô sản xuất"></asp:RadioButton>
                            </div>
                            <div class="custom-control custom-radio none">
                                <asp:RadioButton runat="server" ID="AddLoCheBien" AutoPostBack="true" GroupName="rdo" Text="Lô chế biến"></asp:RadioButton>
                            </div>
                        </div>
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
                            <%--<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>--%>
                            <div class="row">

                                <div class="col-lg-6 col-sm-6 col-xs-6">

                                    <asp:UpdatePanel runat="server" ID="up">
                                        <ContentTemplate>

                                            <div class="form-group">
                                                <label>Mã lô</label>
                                                <asp:TextBox runat="server" ID="txtCode" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Tên lô</label>
                                                <asp:TextBox runat="server" ID="txtTenLo" ClientIDMode="Static" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Số lô mẻ/ batch</label>
                                                <asp:TextBox runat="server" ID="txtSGIN" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <label>Doanh nghiệp <span class="red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                                            </div>
                                            <asp:Panel runat="server" ID="LSX">
                                                <div class="form-group" runat="server" id="Nhom2">
                                                    <label>Lệnh sản xuất  <span class="red">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlProductPackageOrder" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn lệnh sản xuất" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackageOrder_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </asp:Panel>

                                            <div class="form-group" runat="server" id="QTCN">
                                                <label>QTCN sản xuất  <span class="red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlProductPackageProcessing" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackageProcessing_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="form-group" runat="server" id="XuanHoa">
                                                <label>PO</label>
                                                <asp:TextBox runat="server" ID="txtPO" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group none">
                                                <label>Danh mục sản phẩm</label>
                                                <asp:DropDownList runat="server" ID="ddlCha" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <asp:Panel runat="server" ID="oneproduct">
                                                <div class="form-group">
                                                    <label>Sản phẩm  <span class="red">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlProduct" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" CssClass="form-control select2" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn sản phẩm"></asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="muntipleproduct" Visible="false">
                                                <div class="form-group">
                                                    <label>Sản phẩm<span class="red">*</span> </label>
                                                    <asp:ListBox runat="server" ID="ddlProductmultiple" AutoPostBack="true" OnSelectedIndexChanged="ddlProductmultiple_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn sản phẩm  --"></asp:ListBox>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group" runat="server" id="Nhom2_1">
                                                <label>Phiếu kiểm nghiệm</label>
                                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlTestingCertificates" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn phiếu kiểm nghiệm --"></asp:ListBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Số cá thể</label>
                                                <asp:TextBox runat="server" ID="txtCaThe" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Width="200px" Text="1" min="1"></asp:TextBox>
                                            </div>
                                            <div class="form-group none">
                                                <label>Gán cho tài khoản  </label>
                                                <asp:DropDownList runat="server" ID="ddlUser" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Vùng</label>
                                                <asp:DropDownList runat="server" ID="ddlZone" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Khu vực</label>
                                                <asp:DropDownList runat="server" ID="ddlArea" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <asp:Panel runat="server" ID="NV">
                                                <div class="form-group">
                                                    <label>Nhân viên/ Hộ sản xuất </label>
                                                    <asp:DropDownList runat="server" ID="ddlWorkshop" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group">
                                                <label>Lô đất, khu vực sản xuất</label>
                                                <asp:DropDownList runat="server" ID="ddlFarm" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Diện tích (m2)</label>
                                                <asp:TextBox runat="server" ID="txtDienTich" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Width="200px" Text="0" min="0"></asp:TextBox>
                                            </div>
                                            <asp:Panel runat="server" ID="gropsx">
                                                <div class="form-group">
                                                    <label>Giống sản phẩm</label>
                                                    <asp:TextBox runat="server" ID="txtGiongSanPham" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Ngày bắt đầu  <span class="red">*</span></label>
                                                    <div class="row">

                                                        <div class="input-group col-lg-6">
                                                            <div class="row">
                                                                <asp:TextBox runat="server" ID="txtStart" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Chưa chọn ngày tạo lô" />
                                                                <div class="input-group-append">
                                                                    <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="input-group col-lg-6">
                                                            <div class="row">
                                                                <label>Giờ</label>
                                                                <asp:TextBox runat="server" ID="timepicker" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" id="trong">
                                                    <label>Ngày trồng</label>
                                                    <div class="input-group ">
                                                        <asp:TextBox runat="server" ID="txtCropDate" Text="" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label><%=titleEnd %>  <span class="red">*</span></label>
                                                    <div class="row">
                                                        <div class="input-group col-lg-6">
                                                            <div class="row">
                                                                <asp:TextBox runat="server" ID="txtEnd" Text="" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Chưa chọn ngày dự kiến thu hoạch" />
                                                                <div class="input-group-append">
                                                                    <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="input-group col-lg-6">
                                                            <div class="row">
                                                                <label>Giờ</label>
                                                                <asp:TextBox runat="server" ID="timepickerEnd" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-6">
                                    <asp:Panel runat="server" ID="UpdatePanel1">
                                        <contenttemplate>
                                            <div class="form-group">
                                                <label>Nhiệt độ bảo quản</label>
                                                <asp:TextBox runat="server" ID="txtC" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Số ngày sinh trưởng</label>
                                                <asp:TextBox runat="server" ID="txtDay" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Width="200px" Text="0" min="0"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Năng suất dự kiến</label>
                                                <asp:TextBox runat="server" ID="txtNangSuat" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Style="width: 20% !important" Text="0" min="0"></asp:TextBox>
                                                <label>
                                                    <span id="lblTitle"></span>
                                                </label>
                                            </div>
                                            <div class="form-group none">
                                                <label>Sản lượng dự kiến</label>
                                                <asp:TextBox runat="server" ID="txtSanLuong" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Width="200px" Text="0" min="0"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Link camera</label>
                                                <asp:TextBox runat="server" ID="txtCamera" ClientIDMode="Static" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </div>
                                            <div class="form-group none">
                                                <label>Khách hàng  </label>

                                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlCustomer" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn khách hàng --"></asp:ListBox>
                                            </div>
                                            <div class="form-group" style="display: none;">
                                                <label>Nhà cung cấp  </label>
                                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlSupplier" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn nhà cung cấp --"></asp:ListBox>
                                            </div>
                                            <div class="form-group" runat="server">
                                                <label>Trạng thái</label>
                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="display: none;">
                                                <label>Nhân viên kỹ thuật phụ trách</label>
                                                <asp:DropDownList runat="server" ID="ddlNhanVienKT" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Người ghi chép</label>
                                                <asp:DropDownList runat="server" ID="ddlUserGhiChep" CssClass="form-control select2"></asp:DropDownList>
                                                <asp:TextBox Visible="false" runat="server" ID="txtNguoiGhiChep" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:HiddenField runat="server" ID="DienTich" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="NangSuat" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="Kg" ClientIDMode="Static" />
                                        </contenttemplate>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="UpdatePanel2" Visible="false">
                                        <div class="form-group">
                                            <label>Ngày bắt đầu lô <span class="red">*</span></label>
                                            <div class="row">

                                                <div class="input-group col-lg-6">
                                                    <div class="row">
                                                        <asp:TextBox runat="server" ID="txtStart2" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Chưa chọn ngày tạo lô" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="input-group col-lg-6">
                                                    <div class="row">
                                                        <label>Giờ</label>
                                                        <asp:TextBox runat="server" ID="timepicker2" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Ngày dự kiến kết thúc  <span class="red">*</span></label>
                                            <div class="row">
                                                <div class="input-group col-lg-6">
                                                    <div class="row">
                                                        <asp:TextBox runat="server" ID="txtEnd2" Text="" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Chưa chọn ngày dự kiến thu hoạch" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="input-group col-lg-6">
                                                    <div class="row">
                                                        <label>Giờ</label>
                                                        <asp:TextBox runat="server" ID="timepickerEnd2" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group">
                                        <label>Chú thích</label>
                                        <br />
                                        <br />
                                        <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                        </CKEditor:CKEditorControl>
                                    </div>
                                    <div class="form-group">
                                        <label>QR Code</label>
                                        <%=QRCode() %>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="HdCustomer" ClientIDMode="Static" />

                            <!--end form-group-->

                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                            <!--end form-group-->

                            <!--end form-->
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>

            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <asp:HiddenField runat="server" ID="hdCopy" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

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
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <!--Wysiwig js-->
    <script src="/theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="/theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="/theme/assets/js/jquery.core.js"></script>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $('#txtStart').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                //$('#txtStart').val(start.format('DD/MM/YYYY'));
                if ($("#txtDay").length > 0) {
                    $('#txtEnd').val((start.add(parseInt($("#txtDay").val()), 'days')).format('DD/MM/YYYY'));
                }
            });
            $('#txtStart2').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            });

            $('#txtEnd').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                //$('#txtEnd').val(start.format('DD/MM/YYYY'));
            });

            $('#txtEnd2').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            });
            $('#txtCropDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //autoUpdateInput: false,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                //$('#txtCropDate').val(start.format('DD/MM/YYYY'));
            });
            $(".select2").select2({
                width: '100%'
            });
            $(".select2-multiple").select2({
                width: '100%'
            });
            $('#ddlCustomer').on('change', function () {
                $('#HdCustomer').val($(this).val());
                if ($('#HdCustomer').val().length > 0) {
                    $('#HdCustomer').val("," + $('#HdCustomer').val() + ",")
                }
                console.log($('#HdCustomer').val())
            });
            $('#timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm', time: true, date: false
            });
            $('#timepickerEnd').bootstrapMaterialDatePicker({
                format: 'HH:mm', time: true, date: false
            });
            $(function () {
                $("#txtDienTich").keyup(function (e) {
                    var DienTich = parseInt($("#txtDienTich").val());
                    var NangSuatProduct = parseInt($("#NangSuat").val());
                    var DienTichProduct = parseInt($("#DienTich").val());
                    $("#lblTitle").html(" " + $("#Kg").val());
                    $("#txtNangSuat").val(Math.round(NangSuatProduct * DienTich / DienTichProduct));
                    //alert('a')
                });
            });
        }
        $(document).ready(function () {
            Init();
        });
        if (typeof (Sys) !== 'undefined') {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                Init();
            });
        }
    </script>
</asp:Content>

