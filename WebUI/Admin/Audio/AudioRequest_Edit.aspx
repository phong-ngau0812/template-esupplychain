﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="AudioRequest_Edit.aspx.cs" Inherits="Admin_Audio_AudioRequest_Edit" %>

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
                                <li class="breadcrumb-item"><a href="Audio_List.aspx">Quản lý âm thanh</a></li>
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
                            <div class="form-group">
                                <label>Doanh nghiệp <span style="color: red; font-size: 15px">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Enabled="false" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Tiêu đề<span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" Enabled="false" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập tên nhà cung cấp"></asp:TextBox>
                            </div>


                            <div class="form-group">
                                <label>File âm thanh</label>
                                <br />

                                <asp:FileUpload ID="fulAnh" runat="server" Visible="false" ClientIDMode="Static" />
                                <br />
                                <br />
                                <audio controls>
                                    <source src="/data/audio/<%=audio%>">
                                    Your browser does not support the audio element.
                                </audio>

                            </div>


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
                            <!--end form-group-->



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

</asp:Content>