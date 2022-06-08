<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="BaoHiem_Add, App_Web_etdphumj" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
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
                                <li class="breadcrumb-item"><a href="BaoHiem_List.aspx">Quản lý bảo hiểm</a></li>
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
                                <label>Loại xe<span style="color: red; font-size: 15px"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn loại xe" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Text="Xe máy" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Ô tô" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Chủ xe<span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập chủ xe"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Số điện thoại <span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static" CssClass="form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập số điện thoại"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Biển kiểm soát</label>
                                <asp:TextBox runat="server" ID="txtBienXe" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Số khung <span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtSoKhung" ClientIDMode="Static" CssClass="form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập số khung"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Số máy <span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtSoMay" ClientIDMode="Static" CssClass="form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa nhập số máy"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Loại xe</label>
                                <asp:TextBox runat="server" ID="txtNote" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" id="Oto" visible="false">
                                <label>Trọng tải</label>
                                <asp:TextBox runat="server" ID="txtTrongTai" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" id="Oto1" visible="false">
                                <label>Số chỗ ngồi</label>
                                <asp:TextBox runat="server" ID="txtSoCho" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                              <div class="form-group" runat="server" id="Oto2" visible="false">
                                <label>Mục đích kinh doanh</label>
                                <asp:TextBox runat="server" ID="txtMucDich" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Thời hạn bảo hiểm</label>
                                <asp:DropDownList runat="server" ID="txtHanBH" CssClass="select2 form-control">
                                    <asp:ListItem Text="1 năm" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2 năm" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3 năm" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Phí bảo hiểm</label>
                                <asp:TextBox runat="server" ID="txtPhiBH" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
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

