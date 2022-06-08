<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Admin_Config_Config, App_Web_rzvtfdjd" validaterequest="false" enableeventvalidation="false" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" Runat="Server">
     <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="Audio_List.aspx">Cấu hình chung</a></li>
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

                            <%--  <div class="form-group">
                                <label>Tiêu đề truy xuất<span style="color: red; font-size: 15px"> *</span></label>
                                <asp:TextBox runat="server" ID="txtTitle" TextMode="MultiLine" Rows="3" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập tiêu đề"></asp:TextBox>
                            </div>--%>
                            <div class="form-group">
                                <label>Chọn doanh nghiệp để lọc âm thanh và thông điệp </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Âm thanh khi tem công khai đã nhận dạng được sản phẩm<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioPublic" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Âm thanh khi tem bí mật của sản phẩm đã bán<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioSold" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Âm thanh khi tem bí mật của sản phẩm được bảo vệ bởi IDE<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioIDE" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Âm thanh khi tem không nằm trong hệ thống<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioNoExsits" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Âm thanh khi tem chưa kích hoạt<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioActive" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Âm thanh khi tem bị hủy<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioCancel" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Âm thanh khi tem giao cho nhà in<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlAudioPrint" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn âm thanh"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Thông điệp khi tem công khai đã nhận dạng được sản phẩm<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessagePublic" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Thông điệp khi tem bí mật của sản phẩm đã bán<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessageSold" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Thông điệp khi tem bí mật của sản phẩm được bảo vệ bởi IDE<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessageIDE" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Thông điệp khi tem không nằm trong hệ thống<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessageNoExsit" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>

                                       <div class="form-group">
                                        <label>Thông điệp khi tem chưa kích hoạt<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessageActive" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>


                                    <div class="form-group">
                                        <label>Thông điệp khi tem bị hủy<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessageCancel" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Thông điệp khi tem giao cho nhà in<span style="color: red; font-size: 15px"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlMessagePrint" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn thông điệp"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>


                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
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

        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" Runat="Server">
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $(".select2").select2({
                width: '100%'
            });
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            Init();
        });
    </script>

    <!-- Parsley js -->
    <script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="../../theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <script src="../../theme/plugins/select2/select2.min.js"></script>
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-touchspin/js/jquery.bootstrap-touchspin.min.js"></script>
    <%--<script src="../../theme/assets/pages/jquery.forms-advanced.js"></script>--%>

    <!--Wysiwig js-->
    <script src="../../theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="../../theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="../../theme/assets/js/jquery.core.js"></script>

</asp:Content>

