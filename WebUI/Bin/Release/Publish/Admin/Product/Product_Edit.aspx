<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Product_Edit, App_Web_quwo134q" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="Product_List">Quản lý sản phẩm</a></li>
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

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Mã SGTIN </label>
                                            <asp:TextBox runat="server" ID="txtGTIN" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Tên sản phẩm <span class="red">*</span></label>
                                            <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập tên sản phẩm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Danh mục sản phẩm <span class="red">*</span> </label>
                                            <asp:DropDownList runat="server" ID="ddlCha" CssClass="form-control select2" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn danh mục sản phẩm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Doanh nghiệp <span class="red">*</span></label>
                                            <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="form-control select2" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Tiêu chuẩn</label>
                                            <asp:DropDownList runat="server" ID="ddlTieuChuan" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Số ngày sinh trưởng</label>
                                            <asp:TextBox runat="server" ID="txtGrowthByDay" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Quy cách</label>
                                            <asp:TextBox runat="server" ID="txtSpecitication" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Khối lượng mặc định</label>
                                            <asp:TextBox runat="server" ID="txtWeight" TextMode="Number" Text="0" min="0" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Năng suất dự kiến</label>
                                            <asp:TextBox runat="server" ID="txtNangSuat" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Text="0" min="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Đơn vị (mô tả)</label>
                                            <asp:TextBox runat="server" ID="txtNangSuat1" ClientIDMode="Static" CssClass="form-control" placeholder="Ví dụ: KG..."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Trên diện tích</label>
                                            <asp:DropDownList runat="server" ID="ddlDienTich" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Chọn đơn vị diện tích --" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1 sào Bắc Bộ 360 m²" Value="360"></asp:ListItem>
                                                <asp:ListItem Text="1 sào Trung Bộ 500 m²" Value="500"></asp:ListItem>
                                                <asp:ListItem Text="1 sào Nam Bộ 1000 m²" Value="1000"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group none">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Sản lượng dự kiến</label>
                                            <asp:TextBox runat="server" ID="txtSanLuong" ClientIDMode="Static" CssClass="form-control" TextMode="Number" Text="0" min="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-8">
                                        <div class="form-group">
                                            <label>Đơn vị (mô tả)</label>
                                            <asp:TextBox runat="server" ID="txtSanLuong1" ClientIDMode="Static" CssClass="form-control" placeholder="Ví dụ: KG/1 sào...."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Âm thanh khi tem công khai đã nhận dạng được sản phẩm</label>
                                        <asp:DropDownList runat="server" ID="ddlAudioPublic" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <label>Thông điệp khi tem công khai đã nhận dạng được sản phẩm</label>
                                        <asp:DropDownList runat="server" ID="ddlMessagePublic" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Âm thanh khi tem bí mật của sản phẩm được bảo vệ bởi IDE</label>
                                        <asp:DropDownList runat="server" ID="ddlAudioSecret" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <label>Thông điệp khi tem bí mật của sản phẩm được bảo vệ bởi IDE</label>
                                        <asp:DropDownList runat="server" ID="ddlMessageSecret" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Âm thanh khi tem bí mật của sản phẩm đã bán</label>
                                        <asp:DropDownList runat="server" ID="ddlAudioSold" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <label>Thông điệp khi tem bí mật của sản phẩm đã bán</label>
                                        <asp:DropDownList runat="server" ID="ddlMessageSold" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group mt-3">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Ảnh (300px - 300px)</label>
                                        <br />

                                        <div style="margin: 5px 0px;">
                                            <a href="<%=avatar %>" target="_blank">
                                                <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                            </a>
                                        </div>

                                        <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                                    </div>
                                    <div class=" col-lg-4">
                                        <asp:Literal runat="server" ID="lblQR"></asp:Literal>
                                    </div>
                                    <div class=" col-lg-4">
                                        <label>Mã truy viết sản phẩm </label>
                                        <asp:TextBox runat="server" ID="txtTrackingCode" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label>Thông tin sản phẩm</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <label>Quy trình</label>
                                <CKEditor:CKEditorControl ID="txtQuyTrinh" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <label>Câu chuyện sản phẩm</label>
                                <CKEditor:CKEditorControl ID="txtstory" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <%-- <label>Kích hoạt</label>--%>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" />
                                    <label for="ckActive" class="custom-control-label">
                                        KÍCH HOẠT
                                    </label>
                                </div>
                            </div>
                            <!--end form-group-->

                            <div class="form-group" runat="server" id="Role">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <label>Nội dung yêu cầu</label>
                                        <asp:TextBox runat="server" ID="txtChange" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
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
    </script>
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
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            Init();
            $(".select2").select2({
                width: '100%'
            });
        });
    </script>
</asp:Content>

