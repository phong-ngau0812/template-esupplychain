<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="SalesShift_Edit.aspx.cs" Inherits="SalesShift_Edit" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
	<link href="../../css/telerik.css" rel="stylesheet" type="text/css" />



	<style>
		.HourLabel {
			width: 30%;
			float: left;
			line-height: 23px;
			margin-top: .5rem;
		}

		.Hour {
			width: 70% !important;
			float: left;
		}
	</style>

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
								<li class="breadcrumb-item"><a href="SalesShift_List">Quản lý ca bán hàng </a></li>
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
			<div class="card">
				<div class="card-body">
					<div class="row">
						<div class="col-lg-12">

							<div class="form-group">
								<label>
									Doanh nghiệp<span class="red">*</span>
								</label>
								<asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
							</div>

							<div class="form-group">

								<label>Ca bán hàng<span class="red">*</span></label>
								<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập ca bán hàng"></asp:TextBox>
							</div>


							<div class="form-group">
								<div class="row" style="width: 100%">
									<div class="col-lg-3 ">
										<label>Giờ bắt đầu </label>
										<asp:TextBox runat="server" ID="timepickerStart" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
									</div>
									<div class="col-lg-3 ">
										<label>Giờ kết thúc  </label>
										<asp:TextBox runat="server" ID="timepickerEnd" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
									</div>

								</div>
							</div>


							<div class="form-group none">


								<div class="input-group">
									<div class="row" style="width: 100%">
										<div class="col-lg-4 ">
											<label>Giờ bắt đầu </label>
											<div class="row" style="width: 100%">
												<div class="col-lg-6">
													<div class="Hour">
														<asp:DropDownList runat="server" ID="ddlHourFrom" CssClass="select2 form-control">
														</asp:DropDownList>
													</div>
													<div class="HourLabel">
														<label>Giờ</label>
													</div>

												</div>
												<div class="col-lg-6">
													<div class="Hour">
														<asp:DropDownList runat="server" ID="ddlMinutesFrom" CssClass="select2 form-control">
														</asp:DropDownList>
													</div>
													<div class="HourLabel">
														<label>Phút</label>
													</div>

												</div>
											</div>
										</div>
										<div class="col-lg-4 ">
											<label>Giờ kết thúc  </label>
											<div class="row" style="width: 100%">
												<div class="col-lg-6">
													<div class="Hour">
														<asp:DropDownList runat="server" ID="ddHourTo" CssClass="select2 form-control">
														</asp:DropDownList>
													</div>
													<div class="HourLabel">
														<label>Giờ</label>
													</div>

												</div>
												<div class="col-lg-6">
													<div class="Hour">
														<asp:DropDownList runat="server" ID="ddlMinutesTo" CssClass="select2 form-control">
														</asp:DropDownList>
													</div>
													<div class="HourLabel">
														<label>Phút</label>
													</div>
												</div>
											</div>
										</div>

									</div>
								</div>
							</div>

							<div class="form-group">
								<label>Chú Thích</label>
								<CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
								</CKEditor:CKEditorControl>
							</div>

							<div class="form-group">
								<%-- <label>Âm Thanh </label>--%>
								<div class="custom-control custom-checkbox">
									<asp:CheckBox runat="server" ID="ckSound" ClientIDMode="Static" Checked="true" />
									<label for="ckActive" class="custom-control-label">
										Kích hoạt
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
						</div>
					</div>
				</div>
			</div>
		</div>
		<!--end card-body-->
		<!--end card-->

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

	<script src="/theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>

	<script>

		$(window).on('load', function () {
			setTimeout(function () { $('#spinner').fadeOut(); }, 100);
		})
		function Init() {
			$("#ckSound").addClass("custom-control-input");
			$("#ckSMS").addClass("custom-control-input");
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
			$(".select2").select2({
				width: '100%'
			});

			$('#timepickerStart').bootstrapMaterialDatePicker({
				format: 'HH:mm', time: true, date: false
			});
			$('#timepickerEnd').bootstrapMaterialDatePicker({
				format: 'HH:mm', time: true, date: false
			});
		}

		$(function () {
			$(".formatMoney").keyup(function (e) {
				$(this).val(format($(this).val()));
			});
		});

		$(document).ready(function () {
			Init();
		});
		$('#txtSX').daterangepicker({
			singleDatePicker: true,
			showDropdowns: true,
			autoUpdateInput: false,
			//minYear: 1901,
			//maxYear: parseInt(moment().format('YYYY'), 10),
			locale: {
				format: 'DD/MM/YYYY',
			},
		}, function (start, end, label) {
			$('#txtSX').val(start.format('DD/MM/YYYY'));
			//console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
		});
		$('#txtThuHoach').daterangepicker({
			singleDatePicker: true,
			showDropdowns: true,
			autoUpdateInput: false,
			//minYear: 1901,
			//maxYear: parseInt(moment().format('YYYY'), 10),
			locale: {
				format: 'DD/MM/YYYY',
			},
		}, function (start, end, label) {
			$('#txtThuHoach').val(start.format('DD/MM/YYYY'));
			//console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
		});

		$('#txtHSD').daterangepicker({
			singleDatePicker: true,
			showDropdowns: true,
			autoUpdateInput: false,
			//minYear: 1901,
			//maxYear: parseInt(moment().format('YYYY'), 10),
			locale: {
				format: 'DD/MM/YYYY',
			},
		}, function (start, end, label) {
			$('#txtHSD').val(start.format('DD/MM/YYYY'));
			//console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
		});
		$("#btnSave").click(function () {
			$('#spinner').show();
		});
		$('#txtNgayDukien').daterangepicker({
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

</asp:Content>



