<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="ProductBrand_ChangeEdit, App_Web_owdcsjra" validaterequest="false" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css" rel="stylesheet" type="text/css" />
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
                                <li class="breadcrumb-item"><a href="ProductBrand_List">Quản lý doanh nghiệp</a></li>
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
                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnResend" ClientIDMode="Static" Visible="false" OnClick="btnResend_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Gửi lại yêu cầu" />
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Duyệt" />
                                <asp:Button runat="server" ID="btnCancel" ClientIDMode="Static" OnClick="btnCancel_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Không duyệt và gửi phản hồi" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
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
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                            <h4 class="mt-0 header-title"><i class="mdi mdi-tooltip-account"></i>THÔNG TIN DOANH NGHIỆP</h4>
                            <div class="form-group">
                                <label>Tên doanh nghiệp <span class="red">*</span></label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập tên doanh nghiệp "></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" id="Admin">
                                <label>Gói doanh nghiệp<span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlPackage" CssClass="select2 form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn gói doanh nghiệp"></asp:DropDownList>
                            </div>
                            <%--   <div class="form-group" runat="server" id="HideCapBac">
                                <label>Cấp bậc quản lý<span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlRank" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn cấp bậc"></asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" id="Div1">
                                <label>Quyền sửa đổi<span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlRole" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn quyền sửa đổi thông tin"></asp:DropDownList>
                            </div>--%>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <label>Mã GCP</label>
                                        <asp:TextBox runat="server" ID="txtGCP" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Mã GLN</label>
                                        <asp:TextBox runat="server" ID="txtGLN" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <label>Mã vùng trồng(300x300 px)</label>
                                <br />
                                <div style="margin: 5px 0px;">
                                    <a href="<%=MaVungtrong %>">
                                        <asp:Image ID="ImgPUC" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                    </a>
                                </div>
                                <asp:FileUpload ID="ProductionUnitCode" runat="server" ClientIDMode="Static" onchange="imgCodePUC();" />
                            </div>
                            <div class="form-group">
                                <label>Loại hình</label>
                                <%--   <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn loại hình doanh nghiệp"></asp:DropDownList>--%>
                                <telerik:RadComboBox RenderMode="Lightweight" MaxHeight="300px" ID="ddlType" Skin="MetroTouch" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Chọn loại hình " Localization-ItemsCheckedString="loại hình được chọn">
                                    <Localization CheckAllString="Chọn tất cả"
                                        AllItemsCheckedString="Tất cả đều được chọn" />
                                </telerik:RadComboBox>
                            </div>
                            <div class="form-group">
                                <label>Ngành <span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlBranch" CssClass="select2 form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn ngành" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Lĩnh vực kinh doanh <span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlBussines" CssClass="select2 form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn lĩnh vực kd"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Chuỗi liên kết </label>
                                <asp:DropDownList runat="server" ID="ddlChainlink" CssClass="select2 form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Tên giao dịch</label>
                                <asp:TextBox runat="server" ID="txtTenGiaoDich" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Thương hiệu <span class="red">*</span></label>
                                <asp:TextBox runat="server" ID="txtThuongHieu" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập thương hiệu "></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label>Logo (300x300 px)</label>
                                    <br />

                                    <div style="margin: 5px 0px;">
                                        <a href="<%=avatar %>" target="_blank">
                                            <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                        </a>
                                    </div>

                                    <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                                </div>
                                <div class="col-lg-6">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="ckShowlogo" ClientIDMode="Static" />
                                        <label for="ckShowlogo" class="custom-control-label" style="margin: 50px;">
                                            HIỂN THỊ LOGO DOANH NGHIỆP TRÊN CỔNG TRUY XUẤT
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Mã số thuế</label>
                                <asp:TextBox runat="server" ID="txtMST" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Số ĐKKD</label>
                                <asp:TextBox runat="server" ID="txtDKKD" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Ngày cấp</label>
                                <div class="input-group" style="width: 200px;">
                                    <asp:TextBox runat="server" ID="txtNgayCap" ClientIDMode="Static" CssClass="form-control" name="birthday" Text="" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Ngày tham gia hệ thống</label>
                                <div class="input-group" style="width: 200px;">
                                    <asp:TextBox runat="server" ID="txtDate" ClientIDMode="Static" CssClass="form-control" Text="" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group none">
                                <label>Lĩnh vực kinh doanh</label>
                                <asp:TextBox runat="server" ID="txtLinhVucKD" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Sở phụ trách</label>
                                <asp:DropDownList runat="server" ID="ddlSo" CssClass="select2 form-control" Enabled="false"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:UpdatePanel runat="server" ID="u">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label>Tỉnh/Thành phố <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlLocation" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn tỉnh/ thành phố"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Quận huyện <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn quận/ huyện" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Phường xã <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlWard" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn phường/ xã"></asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <label>Điện thoại</label>
                                <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Mobile</label>
                                <asp:TextBox runat="server" ID="txtMobile" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>E-Mail</label>
                                <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" TextMode="Email" parsley-type="email" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Website</label>
                                <asp:TextBox runat="server" ID="txtWebsite" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Facebook ID</label>
                                <asp:TextBox runat="server" ID="txtFacebookID" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Youtube</label>
                                <asp:TextBox runat="server" ID="txtYoutube" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Zalo</label>
                                <asp:TextBox runat="server" ID="txtZalo" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Hotline</label>
                                <asp:TextBox runat="server" ID="txtHotline" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Skype</label>
                                <asp:TextBox runat="server" ID="txtSkype" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <!--end form-group-->

                            <!--end form-group-->
                            <div class="form-group">
                                <%-- <label>Kích hoạt</label>--%>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" />
                                    <label for="ckActive" class="custom-control-label">
                                        KÍCH HOẠT
                                    </label>
                                </div>
                            </div>

                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                            <h4 class="mt-0 header-title"><i class="mdi mdi-tooltip-account"></i>THÔNG TIN NGƯỜI ĐẠI DIỆN PHÁP NHÂN</h4>
                            <div class="form-group">
                                <label>Họ tên</label>
                                <asp:TextBox runat="server" ID="txtHotenPhapNhan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Ngày sinh</label>
                                <div class="input-group" style="width: 200px;">
                                    <asp:TextBox runat="server" ID="txtBirth" ClientIDMode="Static" CssClass="form-control" name="birthday" Text="" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <asp:TextBox runat="server" ID="txtDiaChiPhapNhan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Điện thoại</label>
                                <asp:TextBox runat="server" ID="txtDienThoaiPhapNhan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox runat="server" ID="txtEmailPhapNhan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Chức vụ</label>
                                <asp:TextBox runat="server" ID="txtChucVuPhapNhan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <br />
                            <h4 class="mt-0 header-title"><i class="mdi mdi-tooltip-account"></i>THÔNG TIN NGƯỜI LIÊN HỆ</h4>
                            <div class="form-group">
                                <label>Họ tên</label>
                                <asp:TextBox runat="server" ID="txtHoTenLienHe" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <asp:TextBox runat="server" ID="txtDiaChiLienHe" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Điện thoại</label>
                                <asp:TextBox runat="server" ID="txtDienThoaiLienHe" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox runat="server" ID="txtEmailLienHe" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <br />
                            <h4 class="mt-0 header-title"><i class="mdi mdi-tooltip-account"></i>THÔNG TIN TÀI KHOẢN</h4>
                            <div class="form-group">
                                <label>Tên đăng nhập</label>
                                <asp:TextBox runat="server" ID="txtTaiKhoan" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox runat="server" ID="txtEmailLogin" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Hồ sơ doanh nghiệp</label>
                                <CKEditor:CKEditorControl ID="txtHoso" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <label>Câu chuyện thương hiệu</label>
                                <CKEditor:CKEditorControl ID="txtStory" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <!--end form-group-->
                        </div>
                        <!--end form-->
                    </div>
                    <!--end card-->
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">

                            <div class="form-group">
                                <label>Thông tin truyền thông quảng bá</label>
                                <CKEditor:CKEditorControl ID="txtQuangBa" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <label>Thông tin đại lý</label>
                                <CKEditor:CKEditorControl ID="txtDaiLy" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
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
    <script>
        function img() {
            var url = inputToURL(document.getElementById("<%=fulAnh.ClientID %>"));
            document.getElementById("<%=imganh.ClientID %>").src = url;
        }
        function imgCodePUC() {
            var url = inputToURL(document.getElementById("<%=ProductionUnitCode.ClientID %>"));
            document.getElementById("<%=ImgPUC.ClientID %>").src = url;
        }
        function inputToURL(inputElement) {
            var file = inputElement.files[0];
            return window.URL.createObjectURL(file);
        }
    </script>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            $("#ckShowlogo").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(".select2").select2({
                width: '100%'
            });
        }
        $(document).ready(function () {
            Init();
            $('#txtNgayCap').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                //minYear: 1901,
                //maxYear: parseInt(moment().format('YYYY'), 10),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                $('#txtNgayCap').val(start.format('DD/MM/YYYY'));
                //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
            });
            $('#txtBirth').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                //minYear: 1901,
                //maxYear: parseInt(moment().format('YYYY'), 10),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                $('#txtBirth').val(start.format('DD/MM/YYYY'));
                //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
            });
            $('#txtDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                //minYear: 1901,
                //maxYear: parseInt(moment().format('YYYY'), 10),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                $('#txtDate').val(start.format('DD/MM/YYYY'));
                //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
            });
        });
    </script>

</asp:Content>

