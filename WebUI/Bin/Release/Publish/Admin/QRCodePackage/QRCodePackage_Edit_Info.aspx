<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="QRCodePackage_Edit_Info, App_Web_knjlquph" validaterequest="false" enableeventvalidation="false" %>

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

			td {
				padding-bottom: 10px;
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
								<li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã</a></li>
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
				<div class="col-lg-3">
					<div class="card">
						<div class="card-body">
							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<h4 style="text-align: center">Lịch sử cập nhập </h4>

									<div class="card">
										<div class="card-body">
											<h5 class="mb-3" style="text-align: center">Thông tin sản xuất </h5>


											<table class=" table-responsive mb-0" style="width: 100%; border-spacing: 0px; border-color: grey;">
												<tbody>
													<tr>
														<td style="width: 90px;">Cập nhập:</td>
														<td><%=ProduceInfoEditDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Kiểm tra:</td>
														<td><%=VerifyApproveDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Duyệt Đăng:</td>
														<td><%=VerifyApproveBy%></td>
													</tr>
												</tbody>
											</table>

											<div class="form-group">
												<a style="color: #506ee4" href="#">Chi tiết.. </a>
											</div>
										</div>
									</div>

									<div class="card">
										<div class="card-body">
											<h5 class="mb-3" style="text-align: center">Thông tin chất lượng </h5>


											<table class=" table-responsive mb-0" style="width: 100%; border-spacing: 0px; border-color: grey;">
												<tbody>
													<tr>
														<td style="width: 90px;">Cập nhập:</td>
														<td><%=QualityInfoEditDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Kiểm tra:</td>
														<td><%=CheckApproveDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Duyệt Đăng:</td>
														<td><%=CheckApproveBy%></td>
													</tr>
												</tbody>
											</table>

											<div class="form-group">
												<a style="color: #506ee4" href="#">Chi tiết.. </a>
											</div>
										</div>
									</div>

									<div class="card">
										<div class="card-body">
											<h5 class="mb-3" style="text-align: center">Thông tin giao nhận </h5>


											<table class=" table-responsive mb-0" style="width: 100%; border-spacing: 0px; border-color: grey;">
												<tbody>
													<tr>
														<td style="width: 90px;">Cập nhập:</td>
														<td><%=DeliveryInfoEditDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Kiểm tra:</td>
														<td><%=AdminApproveDate%></td>
													</tr>
													<tr>
														<td style="width: 90px;">Duyệt Đăng:</td>
														<td><%=AdminApproveBy%></td>
													</tr>
												</tbody>
											</table>

											<div class="form-group">
												<a style="color: #506ee4" href="#">Chi tiết.. </a>
											</div>
										</div>
									</div>

								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>


				<div class="col-lg-6">
					<div class="card">
						<div class="card-body">
							<asp:UpdatePanel runat="server" ID="UpdatePanel1">
								<ContentTemplate>

									<div class="card">
										<div class="card-body">
											<h4 style="text-align: center">Thông tin sản xuất</h4>
											<div class="form-group">
												<label style="width: 100%">Thông tin nội bộ</label>
												<br />
												<br />
												<CKEditor:CKEditorControl ID="txtProduceInfo" BasePath="/ckeditor/" runat="server">
												</CKEditor:CKEditorControl>
											</div>

											<div class="form-group">
												<label style="width: 100%">Thông tin hiển thị cho người tiêu dùng</label>
												<br />
												<br />
												<CKEditor:CKEditorControl ID="txtCustomerInfo" BasePath="/ckeditor/" runat="server">
												</CKEditor:CKEditorControl>
											</div>

											<div class="form-group">

												<label>Ngày sản xuất</label>
												<div class="input-group">

													<asp:TextBox runat="server" ID="txtSX" ClientIDMode="Static" CssClass="form-control" Text="" />
													<%--Text="01/01/1980"--%>
													<%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
													<div class="input-group-append">
														<span class="input-group-text"><i class="dripicons-calendar"></i></span>
													</div>
												</div>
											</div>

											<div class="form-group">

												<label>Hạn sử dụng đến ngày </label>
												<div class="input-group">

													<asp:TextBox runat="server" ID="txtHSD" ClientIDMode="Static" CssClass="form-control" Text=""  />
													<div class="input-group-append">
														<span class="input-group-text"><i class="dripicons-calendar"></i></span>
													</div>
												</div>
											</div>

											<div class="form-group">
												<%-- <label>Âm Thanh </label>--%>
												<div class="custom-control custom-checkbox">
													<asp:CheckBox runat="server" ID="checkProduceInfo" ClientIDMode="Static" Checked="true" />
													<label for="CheckProduceInfo" class="custom-control-label">
														Cho phép hiển thị trên tem bảo mật
													</label>
												</div>
											</div>
										</div>
									</div>



									<div class="card">
										<div class="card-body">
											<h4 class="mb-5" style="text-align: center">Thông tin chất lượng</h4>
											<div class="form-group">

												<CKEditor:CKEditorControl ID="txtQualityInfo" BasePath="/ckeditor/" runat="server">
												</CKEditor:CKEditorControl>
											</div>

											<div class="form-group">
												<%-- <label>Âm Thanh </label>--%>
												<div class="custom-control custom-checkbox">
													<asp:CheckBox runat="server" ID="checkQualityInfo" ClientIDMode="Static" Checked="true" />
													<label for="checkQualityInfo" class="custom-control-label">
														Cho phép hiển thị trên tem bảo mật
													</label>
												</div>
											</div>
										</div>
									</div>

									<div class="card">
										<div class="card-body">
											<h4 class="mb-5" style="text-align: center">Thông tin giao nhận</h4>
											<div class="form-group">

												<CKEditor:CKEditorControl ID="txtDeliveryInfo" BasePath="/ckeditor/" runat="server">
												</CKEditor:CKEditorControl>
											</div>
										</div>
									</div>

									<div class="card">
										<div class="card-body">
											<h4 class="mb-5" style="text-align: center">Thông tin kho</h4>

											<div class="form-group">
												<label>Chọn kho</label>
												<asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="form-control select2" ></asp:DropDownList>
											</div>

											<div class="form-group none">

												<CKEditor:CKEditorControl ID="CKEditorControl5" BasePath="/ckeditor/" runat="server">
												</CKEditor:CKEditorControl>
											</div>

											<div class="form-group none">
												<%-- <label>Âm Thanh </label>--%>
												<div class="custom-control custom-checkbox">
													<asp:CheckBox runat="server" ID="CheckBox1" ClientIDMode="Static" Checked="true" />
													<label for="CheckBox1" class="custom-control-label">
														Cho phép hiển thị trên tem bảo mật
													</label>
												</div>
											</div>
										</div>
									</div>

									<div class="form-group mb-0">
										<asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
										<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
									</div>
									<div class="form-group">
										<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>

				<div class="col-lg-3">
					<div class="card">
						<div class="card-body">
							<asp:UpdatePanel runat="server" ID="UpdatePanel2">
								<ContentTemplate>
									<h4 style="text-align: center">Thông tin cấu hình lô mã </h4>
									<div class="card">
										<div class="card-body">
											<table class=" table-responsive mb-0" style="width: 100%; border-spacing: 0px; border-color: grey;">
												<tbody>
													<tr>
														<td style="width: 30%;">Sản phẩm:</td>
														<td><%=NameProduct%></td>
													</tr>
													<tr>
														<td style="width: 30%;">Tên lô:</td>
														<td><%=NameQRCodePackage%></td>
													</tr>
													<tr>
														<td style="width: 30%;">Trạng thái:</td>
														<td><%=NameQRCodeStatus%></td>
													</tr>
												</tbody>
											</table>
											<div class="form-group">
												<%-- <label>Âm Thanh </label>--%>
												<div class="custom-control custom-checkbox">
													<asp:CheckBox runat="server" ID="CheckSound" ClientIDMode="Static" Checked="true" />
													<label for="CheckSound" class="custom-control-label">
														Âm thanh
													</label>
												</div>
											</div>
										</div>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>
			</div>
			<!--end card-body-->
		</div>
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

	<script>

		$(window).on('load', function () {
			setTimeout(function () { $('#spinner').fadeOut(); }, 100);
		})
		function Init() {
			$("#checkProduceInfo").addClass("custom-control-input");
			$("#checkQualityInfo").addClass("custom-control-input");
			$("#CheckBox1").addClass("custom-control-input");
			$("#CheckSound").addClass("custom-control-input");
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
			$(".select2").select2({
				width: '100%'
			});
		}

		$(function () {
			$(".formatMoney").keyup(function (e) {
				$(this).val(format($(this).val()));
			});
		});

		//function Init() {
		//	$("#ckActive").addClass("custom-control-input");
		//	$("#check").addClass("custom-control-input");
		//	setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
		//}

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
			//console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
		});

		$('#txtNgayDukien').daterangepicker({
			singleDatePicker: true,
			showDropdowns: true,
			autoUpdateInput: false,
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


