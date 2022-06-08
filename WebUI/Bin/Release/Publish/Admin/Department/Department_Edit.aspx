<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Department_Edit, App_Web_cjronz32" validaterequest="false" enableeventvalidation="false" %>

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
								<li class="breadcrumb-item"><a href="Department_List.aspx">Quản lý phòng ban  </a></li>
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
								<label>Tên doanh nghiệp
									<span class="red">*</span>
								</label>
								<asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
							</div>
							<div class="form-group">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <label>
                                            Vùng
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>
                                            Khu
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
							<div class="form-group">
								<label>Tên phòng ban/ bộ phận 
									<span class="red">*</span>
								</label>
								<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập tên phòng ban"></asp:TextBox>
							</div>
							  <div class="form-group">
                                <label>Chọn nhật ký quản lý <span class="red">*</span> </label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlTaskType" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn Nhật ký quản lý --"></asp:ListBox>
                            </div>
							<div class="form-group">
								<label>Mô tả</label>
								<CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
								</CKEditor:CKEditorControl>
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

