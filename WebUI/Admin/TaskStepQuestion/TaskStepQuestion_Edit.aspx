<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="TaskStepQuestion_Edit.aspx.cs" Inherits="TaskStepQuestion_Edit" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
	<link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />


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
								<li class="breadcrumb-item"><%=LinkCallBack %></li>
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
								<label>Đề mục công việc <span class="red">*</span>  </label>
								<asp:DropDownList runat="server" ID="ddlTask" ClientIDMode="Static" CssClass="form-control select2" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn đề mục công việc"></asp:DropDownList>
							</div>
							<div class="form-group">
								<label>Nội dung câu hỏi<span style="color: red; font-size: 15px"> *</span></label>
								<div style="overflow: scroll">
									<asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập nội dung câu hỏi"></asp:TextBox>
								</div>
							</div>

							<div class="form-group">
								<%-- <label>Kích hoạt</label>--%>
								<div class="custom-control custom-checkbox none">
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

