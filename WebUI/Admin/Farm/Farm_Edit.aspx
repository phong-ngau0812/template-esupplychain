<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="Farm_Edit.aspx.cs" Inherits="Farm_Edit" ValidateRequest="false" EnableEventValidation="false" %>

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
                                <li class="breadcrumb-item"><a href="Farm_List.aspx">Quản lý thửa/ruộng </a></li>
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
                                <label>Doanh nghiệp <span style="color: red; font-size: 15px">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>vùng sản xuất </label>
                                <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" Width="100%" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Khu sản xuất </label>
                                <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Nhân viên sản xuất (hoặc hộ gia đình) </label>
                                <asp:DropDownList runat="server" ID="ddlWorkshop" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Mã Thửa/Ruộng</label>
                                <asp:TextBox runat="server" ID="txtGLN" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Tên Thửa/Ruộng  <span style="color: red; font-size: 15px">*</span> </label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập thửa/ruộng"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>Diện tích (m2) </label>
                                        <asp:TextBox runat="server" ID="txtAcreage" ClientIDMode="Static" CssClass="form-control" Width="200px" TextMode="Number" required data-parsley-required-message="Bạn chưa nhập diện tích "></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label>Trạng thái thửa/ruộng  <span style="color: red; font-size: 15px">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlFarmStatus" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn trạng thái"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%-- <div class="form-group">
                                <label>E-Mail</label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" TextMode="Email" parsley-type="email" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <label>Số điện thoại<span class="red">*</span> </label>
                                <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập số điện thoại"></asp:TextBox>
                            </div>--%>

                            <div class="form-group">
                                <label>Mô tả</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>


                            <div class="form-group">
                                <label>Lịch sử thửa/lô </label>
                                <CKEditor:CKEditorControl ID="txtHistoryFarm" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
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

