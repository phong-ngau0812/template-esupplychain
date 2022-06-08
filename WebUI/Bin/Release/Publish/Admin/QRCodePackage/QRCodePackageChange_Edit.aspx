<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Admin_QRCodePackage_QRCodePackageChange_Edit, App_Web_knjlquph" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css" rel="stylesheet" type="text/css" />

    <style>
        label {
            line-height: 23px;
            margin-top: .5rem;
        }
        /*@media only screen and (min-width: 1024px) {
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

            .form-cate {
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
        }*/
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
                                <li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý yêu cầu cấp tem </a></li>
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
                        <div class="col-lg-12">

                            <div class="form-group mb-0">
                                <%-- <asp:Button runat="server" ID="btnResend" ClientIDMode="Static" Visible="false" OnClick="btnResend_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Gửi lại yêu cầu" />--%>
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Duyệt" />
                                <%-- <asp:Button runat="server" ID="btnCancel" ClientIDMode="Static" OnClick="btnCancel_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Không duyệt và gửi phản hồi" />--%>
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group" runat="server" id="Role">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <label>Nội dung phản hồi</label>
                                        <asp:TextBox runat="server" ID="txtNoteChange" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Nội dung yêu cầu </label>
                                        <asp:TextBox runat="server" ID="txtChange" CssClass="form-control" TextMode="MultiLine" Rows="3" Enabled="false"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="col-lg-6">

                            <div class="row form-group">
                                <div class="col-lg-3">
                                    <label>Tên lô</label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row form-group">
                                <div class="col-lg-3">
                                    <label>Số lượng tem </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Text="1" min="1" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Độ dài mã(ký tự) </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:TextBox runat="server" ID="txtLength" ClientIDMode="Static" CssClass="form-control" Text="12" TextMode="Number" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>
                                        Loại tem
									<span class="red">*</span>
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlTemType" CssClass="select2 form-control" Enabled="false">
                                        <asp:ListItem Value="1" Text="Tem công khai" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Tem bí mật"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckSound" ClientIDMode="Static" Checked="true" />
                                    <label for="ckSound" class="custom-control-label">
                                        Âm Thanh
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckSMS" ClientIDMode="Static" Checked="true" />
                                    <label for="ckSMS" class="custom-control-label">
                                        Tạo kèm mã SMS chỉ áp dụng cho lớp mã bảo mật
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>
                                        Doanh nghiệp<span class="red">*</span>
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProductBrand" ClientIDMode="Static" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProducBrand_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Cấp độ mã định danh</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="radio radio-primary" style="width: 100%">
                                        <div class="custom-control custom-radio ">
                                            <asp:RadioButton runat="server" ID="rdoProductPackage" AutoPostBack="true" Text="Mã định danh lô" GroupName="rdo" Checked="true" />&nbsp;
                                        </div>
                                        <div class="custom-control custom-radio">
                                            <asp:RadioButton runat="server" ID="rdoProduct" AutoPostBack="true" Text="Mã định danh sản phẩm" GroupName="rdo" />&nbsp;
                                        </div>
                                        <div class="custom-control custom-radio ">
                                            <asp:RadioButton runat="server" ID="rdoKhongLienMach" Text="Mã định danh đơn vị Logistic" GroupName="rdo" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" id="productPackage">
                                <div class="col-lg-3">
                                    <label>
                                        Lô sản xuất
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage" AutoPostBack="true" OnSelectedIndexChanged="ddlProductPackage_SelectedIndexChanged1" ClientIDMode="Static" CssClass="select2 form-control" Enabled="false"></asp:DropDownList>
                                    <br />
                                    <asp:Literal runat="server" ID="lblNote"></asp:Literal>
                                </div>
                            </div>
                            <div class="form-group row" runat="server" visible="false">
                                <div class="col-lg-3">
                                    <label>
                                        Danh mục sản phẩm
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProducCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlProducCategory_SelectedIndexChanged" CssClass="select2 form-control" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" runat="server" id="product">
                                <div class="col-lg-3">
                                    <label>
                                        Sản phẩm
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Ngày xuất bán</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">

                                        <asp:TextBox runat="server" ID="txtSX" ClientIDMode="Static" CssClass="form-control" Text="" Enabled="false" />
                                        <%--Text="01/01/1980"--%>
                                        <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Ngày thu hoạch</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtThuHoach" ClientIDMode="Static" CssClass="form-control" Text="" Enabled="false" />
                                        <%--Text="01/01/1980"--%>
                                        <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Hạn sử dụng đến ngày </label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">

                                        <asp:TextBox runat="server" ID="txtHSD" ClientIDMode="Static" CssClass="form-control" Text="" Enabled="false" />
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Ngày dự kiến dán tem </label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <div class="row" style="width: 100%">
                                            <div class="col-lg-6 row">
                                                <div class="col-lg-9" style="padding-right: 0px;">
                                                    <asp:TextBox runat="server" ID="txtNgayDukien" ClientIDMode="Static" CssClass="form-control" name="birthday" Enabled="false" />
                                                </div>
                                                <div class="col-lg-3" style="padding-left: 0px;">
                                                    <div class="input-group-append ">
                                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlHour" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Giờ</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlMinutes" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Phút</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Khối lượng(g) </label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:TextBox runat="server" ID="txtKhoiLuong" ClientIDMode="Static" CssClass="form-control formatMoney" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Đơn vị sơ chế và đóng gói thành phẩm</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="col-lg-12">
                                        <asp:DropDownList runat="server" ID="ddlSupplier" CssClass="select2 form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Chú Thích</label>
                        <br />
                        <br />
                        <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server" Enabled='false'>
                        </CKEditor:CKEditorControl>
                    </div>
                    <div class="form-group">
                        <label>Hồ sơ lô mã/Lịch sử sản xuất</label>
                        <br />
                        <br />
                        <CKEditor:CKEditorControl ID="txtHistoryProductPackage" BasePath="/ckeditor/" runat="server" Enabled='false'>
                        </CKEditor:CKEditorControl>
                    </div>
                    <%--   <div class="form-group" runat="server" id="Role">
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Nhận xét</label>
                                <asp:TextBox runat="server" ID="txtChange" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-0">
                        <asp:Button runat="server" ID="btnSave" OnClientClick="return Validate();" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                    </div>--%>
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
        function Validate() {
            var objProductBrand = document.getElementById("ddlProductBrand");
            var objPackage = document.getElementById("ddlProductPackage");

            if (objProductBrand.value == "") {
                alert("Vui lòng chọn doanh nghiệp!");
                objProductBrand.focus();
                return false;
            }
            if (objPackage.value == "") {
                alert("Vui lòng chọn lô!");
                objPackage.focus();
                return false;
            }
            $('#spinner').css('opacity', '0.8');
            $('#spinner').delay("slow").fadeIn();
            var counter = 0;
            var myInterval = setInterval(function () {
                ++counter;
                $("p.textload").html("Hệ thống đang tạo tem, vui lòng đợi trong giây lát <br><span style='font-size:35px;' class='red'>" + counter + "s</h2><br><img src='/images/128x128.gif'>");
            }, 1000);

            return true;
        }
    </script>

    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckSound").addClass("custom-control-input");
            $("#ckSMS").addClass("custom-control-input");
            $("#ckProductPackage").addClass("custom-control-input");
            $("#ckProduct").addClass("custom-control-input");
            $("#ckLogistic").addClass("custom-control-input");
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
            //minYear: 1901,
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
            //minYear: 1901,
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
            //minYear: 1901,
            //maxYear: parseInt(moment().format('YYYY'), 10),
            locale: {
                format: 'DD/MM/YYYY',
            },
        }, function (start, end, label) {
            $('#txtHSD').val(start.format('DD/MM/YYYY'));
            //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        });
        //$("#btnSave").click(function () {
        //    $('#spinner').show();
        //});
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

