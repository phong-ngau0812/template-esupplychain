<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="Material_Add.aspx.cs" Inherits="Material_Add" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<%--<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />--%>
	<%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>

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
								<li class="breadcrumb-item"><a href="Material_List.aspx">Quản lý vật tư  </a></li>
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
								<label>
									Tên doanh nghiệp
                                    <span class="red">*</span>
								</label>
								<asp:DropDownList runat="server" ID="ddlProductBrand" AutoPostBack="true" OnSelectedIndexChanged ="ddlProductBrand_SelectedIndexChanged" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
							</div>

							<div class="form-group">
								<label>Chọn kho <span class="red">*</span> </label>
								<asp:ListBox runat="server" ClientIDMode="Static" ID="ddWarehouse" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn kho --"></asp:ListBox>
							</div>
							<div class="form-group none">
								<label>Chọn nhà cung cấp </label>
								<asp:ListBox runat="server" ClientIDMode="Static" ID="ddlSupplier" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn nhà cung cấp --"></asp:ListBox>
							</div>
							<div class="form-group none">
								<div class="radio radio-primary">
									<asp:RadioButton runat="server" ID="rdoDaCo" Text="Chọn loại vật tư đã có" GroupName="rdo" Checked="true" AutoPostBack="true" OnCheckedChanged="rdoDaCo_CheckedChanged" />&nbsp;&nbsp;&nbsp;
									<asp:RadioButton runat="server" ID="rdo" Text="Chọn loại vật tư mới" GroupName="rdo" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
								</div>
							</div>
							<div class="form-group" runat="server" id="DaCo">
								<label>
									Loại vật tư
                                    <span class="red">*</span>
								</label>
								<asp:DropDownList runat="server" ID="ddlMateriaType" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
							</div>
							<div class="form-group" runat="server" id="themmoi">
								<label>
									Tên loại vật tư
                                    <span class="red">*</span>
								</label>
								<asp:TextBox runat="server" ID="txtMateriaType" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập loại vật tư"></asp:TextBox>
							</div>
							<div class="form-group" runat="server" visible="false">
								<label>Mã vật tư (tự động hệ thống)</label>
								<asp:TextBox runat="server" ID="txtGLN" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
							</div>
							<div class="form-group" runat="server" id="xuanhoa">
								<label>Mã Vật Tư Nội Bộ  </label>
								<asp:TextBox runat="server" ID="txtCodePrivate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Mã SGTIN  </label>
								<asp:TextBox runat="server" ID="txtGTIN" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>
									Tên vật tư
                                        <span class="red">*</span>
								</label>
								<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập tên vật tư"></asp:TextBox>
							</div>

							<div class="form-group">
								<label>Quy cách đóng gói </label>
								<asp:TextBox runat="server" ID="txtPackingType" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
							</div>
							<div class="form-group">
								<div class="row">
									<%--<div class="col-md-2">
										<label>Số lượng </label>
										<asp:TextBox runat="server" ID="txtAcreage" ClientIDMode="Static" CssClass="form-control" Width="200px" TextMode="Number"></asp:TextBox>
									</div>--%>
									<div class="col-md-2">
										<label>
											Đơn vị tính
                                                <span class="red">*</span>
										</label>
										<asp:TextBox runat="server" ID="txtUnit" ClientIDMode="Static" CssClass="form-control" Width="200px" required data-parsley-required-message="Bạn chưa nhập đơn vị tính "></asp:TextBox>
									</div>
									<div class="col-md-2">
										<label>Cách ly (ngày) </label>
										<asp:TextBox runat="server" ID="txtIsolationDay" ClientIDMode="Static" CssClass="form-control" Width="200px" TextMode="Number"></asp:TextBox>
									</div>
								</div>
							</div>

							<div class="form-group">
								<label>Mô tả</label>
								<CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
								</CKEditor:CKEditorControl>
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
			$("#ckActive").addClass("custom-control-input");
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
		}
		$(document).ready(function () {
			$(".select2").select2({
				width: '100%'
			});
			Init();
		});
		$('#txtFromDate').daterangepicker({
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
		$('#txtToDate').daterangepicker({
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

	<script src="/theme/plugins/select2/select2.min.js"></script>

	<!-- Parsley js -->
	<script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
	<script src="../../theme/assets/pages/jquery.validation.init.js"></script>

	<!----date---->
	<%--<script src="../../theme/plugins/select2/select2.min.js"></script>--%>
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

