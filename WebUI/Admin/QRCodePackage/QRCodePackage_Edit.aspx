<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackage_Edit.aspx.cs" Inherits="QRCodePackage_Edit" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
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

            .custom-control-label {
                width: 100% !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="src"></telerik:RadScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
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
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-6">

                            <div class="form-group">

                                <label>Tên lô</label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Số lượng tem </label>
                                <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Text="1" min="1"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Độ dài mã(ký tự) </label>
                                <asp:TextBox runat="server" ID="txtLength" ClientIDMode="Static" CssClass="form-control" Text="12" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Loại tem
									<span class="red">*</span>
                                </label>
                                <asp:DropDownList runat="server" ID="ddlTemType" CssClass="select2 form-control">
                                    <asp:ListItem Value="1" Text="Tem công khai"></asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True" Text="Tem bí mật"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <%-- <label>Âm Thanh </label>--%>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckSound" ClientIDMode="Static" Checked="true" />
                                    <label for="ckSound" class="custom-control-label">
                                        Âm Thanh
                                    </label>
                                </div>
                            </div>


                            <div class="form-group">
                                <%-- <label>Âm Thanh </label>--%>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckSMS" ClientIDMode="Static" Checked="true" />
                                    <label for="ckSMS" class="custom-control-label">
                                        Tạo kèm mã SMS chỉ áp dụng cho lớp mã bảo mật
                                    </label>
                                </div>
                            </div>
                            <div class="form-group" style="visibility:hidden;">
                                  <label>Âm thanh khi tem công khai đã nhận dạng được sản phẩm</label>
                            </div>
                            <div class="form-group">
                                <label>Âm thanh khi tem công khai đã nhận dạng được sản phẩm</label>
                                <asp:DropDownList runat="server" ID="ddlAudioPublic" CssClass="select2 form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Âm thanh khi tem bí mật của sản phẩm đã bán</label>
                                <asp:DropDownList runat="server" ID="ddlAudioSold" CssClass="select2 form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Âm thanh khi tem bí mật của sản phẩm được bảo vệ bởi IDE</label>
                                <asp:DropDownList runat="server" ID="ddlAudioIDE" CssClass="select2 form-control"></asp:DropDownList>
                            </div>



                        </div>
                        <div class="col-lg-6">

                            <div class="form-group">
                                <label>
                                    Doanh nghiệp<span class="red">*</span>
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>

                                                      <%--  <div class="form-group" >
                                <label>
                                    Danh mục sản phẩm
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProducCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlProducCategory_SelectedIndexChanged" CssClass="select2 form-control"></asp:DropDownList>
                            </div>--%>
                            <div class="form-group">
                                <label>
                                    Sản phẩm<span class="red">*</span>
                                </label>
                                <asp:TextBox runat="server" ID="txtProductName" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>

                            <div class="form-group">

                                <label>Ngày sản xuất</label>
                                <div class="input-group">

                                    <asp:TextBox runat="server" ID="txtSX" ClientIDMode="Static" CssClass="form-control" Text="" />
                                    <%--Text="01/01/1980"--%>
                                    <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <label>Ngày thu hoạch</label>
                                <div class="input-group">

                                    <asp:TextBox runat="server" ID="txtThuHoach" ClientIDMode="Static" CssClass="form-control" Text="" />
                                    <%--Text="01/01/1980"--%>
                                    <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">

                                <label>Hạn sử dụng đến ngày </label>
                                <div class="input-group">

                                    <asp:TextBox runat="server" ID="txtHSD" ClientIDMode="Static" CssClass="form-control" Text="" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">

                                <label>Ngày dự kiến dán tem </label>
                                <div class="input-group">
                                    <div class="row" style="width: 100%">
                                        <div class="col-lg-6">
                                            <asp:TextBox runat="server" ID="txtNgayDukien" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:DropDownList runat="server" ID="ddlHour" CssClass="select2 form-control">
                                            </asp:DropDownList><label> Giờ</label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:DropDownList runat="server" ID="ddlMinutes" CssClass="select2 form-control">
                                            </asp:DropDownList><label> Phút</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Khối lượng(g) </label>
                                <div class="input-group">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:TextBox runat="server" ID="txtKhoiLuong" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Đơn vị sơ chế và đóng gói thành phẩm</label>
                                <div class="col-lg-12">
                                    <asp:DropDownList runat="server" ID="ddlSupplier" CssClass="select2 form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Thông điệp khi tem công khai đã nhận dạng được sản phẩm</label>
                                <asp:DropDownList runat="server" ID="ddlMessagePublic" CssClass="select2 form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Thông điệp khi tem bí mật của sản phẩm đã bán</label>
                                <asp:DropDownList runat="server" ID="ddlMessageSold" CssClass="select2 form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Thông điệp khi tem bí mật của sản phẩm được bảo vệ bởi IDE</label>
                                <asp:DropDownList runat="server" ID="ddlMessageIDE" CssClass="select2 form-control"></asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <label>Chú Thích</label>
                        <br />
                        <br />
                        <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                        </CKEditor:CKEditorControl>
                    </div>
                    <div class="form-group">
                        <label>Hồ sơ lô mã/Lịch sử sản xuất</label>
                        <br />
                        <br />
                        <CKEditor:CKEditorControl ID="txtHistoryProductPackage" BasePath="/ckeditor/" runat="server">
                        </CKEditor:CKEditorControl>
                    </div>
                    <div class="form-group mb-0">
                        <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                    </div>
                    <!--end card-body-->
                </div>
                <!--end card-->
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

    <!-- Parsley js -->
    <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <script src="/theme/plugins/select2/select2.min.js"></script>
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

    <script src="/js/Function.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".formatMoney").keyup(function (e) {
                $(this).val(format($(this).val()));
            });
        });

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckSound").addClass("custom-control-input");
            $("#ckSMS").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(".select2").select2({
                width: '100%'
            });
        }

        $(function () {
            $(".formatMoney").keyup(function (e) {
                $(this).val(format($(this).val()));
            });
        });

        $(document).ready(function () {
            Init();
        });
        $('#txtSX').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            minYear: 1901,
            //maxYear: parseInt(moment().format('YYYY'), 10),
            locale: {
                format: 'DD/MM/YYYY',
            },
        }, function (start, end, label) {
            $('#txtSX').val(start.format('DD/MM/YYYY'));
            //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        });
        $('#txtThuHoach').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            minYear: 1901,
            //maxYear: parseInt(moment().format('YYYY'), 10),
            locale: {
                format: 'DD/MM/YYYY',
            },
        }, function (start, end, label) {
            $('#txtThuHoach').val(start.format('DD/MM/YYYY'));
            //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        });

        $('#txtHSD').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            minYear: 1901,
            //maxYear: parseInt(moment().format('YYYY'), 10),
            locale: {
                format: 'DD/MM/YYYY',
            },
        }, function (start, end, label) {
            $('#txtHSD').val(start.format('DD/MM/YYYY'));
            //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        });
        $("#btnSave").click(function () {
            $('#spinner').show();
        });
        $('#txtNgayDukien').daterangepicker({
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
    </script>

</asp:Content>


