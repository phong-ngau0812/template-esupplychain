<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackage_Edit_Pendingactive.aspx.cs" Inherits="QRCodePackage_Edit_Pendingactive" ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
	<link href="../../css/telerik.css" rel="stylesheet" type="text/css" />



	<style>
		@media only screen and (min-width: 1024px) {
			label {
				width: 30%;
				float: left;
				line-height: 23px;
				margin-top: .5rem;
			}

			.form-group {
				margin-bottom: 20px;
				width: 100%;
				float: left;
			}

			.form-control {
				float: left;
				width: 70% !important;
			}

			.select2-container {
				width: 70% !important;
				float: left;
			}

			.input-group {
				width: 70%;
			}

			.custom-control-label {
				width: 100% !important;
			}
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
								<li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
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
						<div class="col-lg-12 mt-4">
							<div class="form-group">
								<label>Tên lô</label>
								<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:TextBox>
							</div>

							<div class="form-group">
								<label>Ngày kích hoạt lô mã </label>
								<div class="input-group">
									<div class="row" style="width: 100%">
										<div class="col-lg-6">
											<asp:TextBox runat="server" ID="txtDayKichHoat" ClientIDMode="Static" CssClass="form-control" name="birthday" />
											<div class="input-group-append">
												<span class="input-group-text"><i class="dripicons-calendar"></i></span>
											</div>
										</div>

										<div class="input-group col-lg-6">
											<div class="row">
												<label>Giờ</label>
												<asp:TextBox runat="server" ID="timepicker" Text="00:00" ClientIDMode="Static" CssClass="form-control" />
											</div>
										</div>

										<%--<div class="col-lg-3">
													<asp:DropDownList runat="server" ID="ddlHour" CssClass="select2 form-control">
													</asp:DropDownList>
													<label>&nbsp  Giờ</label>
												</div>
												<div class="col-lg-3">
													<asp:DropDownList runat="server" ID="ddlMinutes" CssClass="select2 form-control">
													</asp:DropDownList>
													<label>&nbsp  Phút</label>
												</div>--%>
									</div>
								</div>
							</div>

							<div class="form-group">
								<label></label>
								<div class="input-group">
									<div class="custom-control custom-checkbox">
										<asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" />
										<label for="ckActive" class="custom-control-label">
											Hẹn ngày kích hoạt lô mã
										</label>
									</div>
								</div>
							</div>

							<%--<div class="form-group">
										<label>Chú Thích</label>
										<br />
										<br />
										<CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
										</CKEditor:CKEditorControl>
									</div>--%>

							<div class="form-group">
								<label></label>
								<div class="input-group">
									<asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light mr-1" Text="Lưu" />
									<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
								</div>
							</div>
						</div>
					</div>
					<div class="form-group">
						<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
					</div>
				</div>
				<!--end card-body-->
			</div>
			<!--end card-->
		</div>

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
	<script src="/theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
	<%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>

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
			$("#check").addClass("custom-control-input");
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
			$(".select2").select2({
				width: '100%'
			});

			$('#timepicker').bootstrapMaterialDatePicker({
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
		$('#txtDayKichHoat').daterangepicker({
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

