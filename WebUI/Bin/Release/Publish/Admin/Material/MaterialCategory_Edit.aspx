<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="MaterialCategory_Edit, App_Web_nad04s4b" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
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
                                <li class="breadcrumb-item"><a href="MaterialCategory_List.aspx">Quản lý danh mục vật tư</a></li>
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
                                <label>Tên danh mục vật tư<span class ="red">*</span></label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập tên bộ"></asp:TextBox>
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
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click"  CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
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
            Init();
        });
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

    <!--Wysiwig js-->
    <script src="/theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="/theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="/theme/assets/js/jquery.core.js"></script>

</asp:Content>

