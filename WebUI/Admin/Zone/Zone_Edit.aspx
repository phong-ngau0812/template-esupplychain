<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="Zone_Edit.aspx.cs" Inherits="Zone_Edit" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />


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
                                <li class="breadcrumb-item"><a href="Zone_List.aspx">Quản lý vùng sản xuất </a></li>
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
                                <label>Tên doanh nghiệp <span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Chọn nhân viên/ hộ sx</label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlWorkshop" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn nhân viên  --"></asp:ListBox>
                                <asp:Literal runat="server" ID="lblCount"></asp:Literal>
                            </div>
                            <div class="form-group">
                                <label>Mã GLN vùng</label>
                                <asp:TextBox runat="server" ID="txtGLN" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                            </div>
                            <label>Tên vùng sản xuất <span class="red">*</span></label>
                            <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập tên vùng"></asp:TextBox>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-2">
                                        <label>Diện tích (m2) <span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtAcreage" ClientIDMode="Static" CssClass="form-control" Width="200px" TextMode="Number" required data-parsley-required-message="Bạn chưa nhập diện tic "></asp:TextBox>
                                    </div>
                                <div class="col-lg-10">
                                    <label>Địa chỉ</label>
                                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" Width="100%"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label>Nhập chia sẻ vị trí google map </label>
                                <asp:TextBox runat="server" ID="txtgooglemap" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <div id="EmbedGG"></div>
                                <div id="hideGG">
                                    <%=googlemap%>
                                </div>
                                <%--<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d29768.962289068113!2d105.81486220433442!3d21.14761082525156!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x313500fe2e049503%3A0x14263f9f2a5473af!2zVGnDqm4gRMawxqFuZywgxJDDtG5nIEFuaCwgSMOgIE7hu5lpLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1600828202942!5m2!1svi!2s" width="600" height="450" frameborder="0" style="border: 0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>--%>
                            </div>

                            <div class="form-group">
                                <label>Mô tả</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            
                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="u">
                                <ContentTemplate>
									<div class="form-group">
                                        <label>Tỉnh/Thành phố <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlLocation" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn tỉnh/ thành phố"></asp:DropDownList>
									</div>
                                    <div class="form-group">
                                        <label>Quận/Huyện <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn quận/ huyện"></asp:DropDownList>
									</div>
                                    <div class="form-group">
                                        <label>Phường/Xã <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlWard" CssClass="select2 form-control" AutoPostBack="true" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn phường/ xã"></asp:DropDownList>
									</div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

    <script>
        $(document).ready(function () {
            $("#txtgooglemap").keyup(function () {
                //alert("Handler for .change() called.");
                googlemap();
            });
        });
        function googlemap() {
            var googlemap = $("#txtgooglemap").val();
            if (googlemap.includes("<iframe")) {
                $("#EmbedGG").html(googlemap);
                $("#hideGG").hide();
            }

        }
    </script>

    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            Init();
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

</asp:Content>

