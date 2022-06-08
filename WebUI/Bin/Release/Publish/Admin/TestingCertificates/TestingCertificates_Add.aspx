<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="TestingCertificates_Add, App_Web_3tuccbkz" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<%--<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />--%>
	<%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>
	<link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />

	<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
	<form runat="server" id="frm1" class="form-parsley">
		<telerik:RadScriptManager runat="server" ID="sc"></telerik:RadScriptManager>
		<div class="container-fluid">
			<!-- Page-Title -->
			<div class="row">
				<div class="col-sm-12">
					<div class="page-title-box">
						<div class="float-right">
							<ol class="breadcrumb">
								<li class="breadcrumb-item active"><%=title %></li>
								<li class="breadcrumb-item"><a href="TestingCertificates_List.aspx">Quản lý phiếu kiểm nghiệm </a></li>
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
								<label>Chọn doanh nghiệp <span style="color: red; font-size: 15px">*</span></label>
								<asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlProductBrand"  CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp" data-placeholder="-- Chọn doanh nghiệp --"></asp:DropDownList>
							</div>
							<div class="form-group">
								<label>Tên phiếu <span style="color: red; font-size: 15px">*</span></label>
								<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập tên phiếu"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Upload File</label>
								<br />

								<div style="margin: 5px 0px;">
									<a href="<%=avatar %>" target="_blank">
										<asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
									</a>
								</div>

								<asp:FileUpload ID="UpFile" runat="server" ClientIDMode="Static" onchange="img();" />

							</div>


							<div class="form-group">
								<label>Ngày bắt đầu có hiệu lực của phiếu <span class="red">*</span> </label>
								<div class="input-group">

									<asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="form-control" name="birthday" />
									<div class="input-group-append">
										<span class="input-group-text"><i class="dripicons-calendar"></i></span>
									</div>
								</div>
							</div>

							<div class="form-group">
								<label>Ngày hết hiệu lực của phiếu <span class="red">*</span>  </label>
								<div class="input-group">

									<asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="form-control" name="birthday" />
									<div class="input-group-append">
										<span class="input-group-text"><i class="dripicons-calendar"></i></span>
									</div>
								</div>
							</div>


							<div class="form-group">
								<label>Mô tả </label>
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

		</div>
		<%-- <asp:HiddenField runat="server" ID="HdProductBrand" ClientIDMode="Static" />--%>
	</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

	<script>
		function img() {
			var url = inputToURL(document.getElementById("<%=UpFile.ClientID %>"));
			document.getElementById("<%=imganh.ClientID %>").src = url;
		}
		function inputToURL(inputElement) {
			var file = inputElement.files[0];
			return window.URL.createObjectURL(file);
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
			$(".select2-multiple").select2({
				width: '100%'
			});
			//$('#ddlProductBrand').on('change', function () {
			//	$('#HdProductBrand').val($(this).val());
			//	if ($('#HdProductBrand').val().length > 0) {
			//		$('#HdProductBrand').val("," + $('#HdProductBrand').val() + ",")
			//	}
			//	console.log($('#HdProductBrand').val())
			//});
			Init();
		});

		$(document).ready(function () {

			Init();
			$('#txtBirth').daterangepicker({
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

