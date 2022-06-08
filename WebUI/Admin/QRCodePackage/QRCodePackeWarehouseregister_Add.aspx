<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackeWarehouseregister_Add.aspx.cs" Inherits="QRCodePackeWarehouseregister_Add" ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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
								<li class="breadcrumb-item active">Quản lý phân hộ sản xuất :<%=NameProductPackage %> </li>
								<li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Cập nhật thông tin hộ sản xuất cho lô:<%=NameProductPackage %> </h4>
						<h6>Sản phẩm:<%=NameProduct%> </h6>
						<h6>Tổng sô<%= TotalTem%> tem.Serial từ <%=SerialStart%> đến <% =SerialEnd %> </h6>

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
							<div class="row">
								<div class="col-lg-12">


									<div class="form-group col-lg-10">
										<label style="margin-right: 17px">Phương pháp chia </label>
										<div class="radio radio-primary">
											<asp:RadioButton runat="server" ID="rdoLienMach" Text="Chia theo chuỗi Serial liền mạch" GroupName="rdo" AutoPostBack="true" Checked="true" OnCheckedChanged="rdoLienMach_CheckedChanged" />&nbsp;&nbsp;&nbsp;
											<asp:RadioButton runat="server" ID="rdoKhongLienMach" Text="Chia theo Serial không liền mạch" AutoPostBack="true" GroupName="rdo" OnCheckedChanged ="rdoKhongLienMach_CheckedChanged" />
										</div>
									</div>

									<div class="form-group col-lg-10">
										<label>Chọn Kho <span class="red">*</span> </label>
										 <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn kho"></asp:DropDownList>
									</div>

									<div class="form-group col-lg-10">

										<label>Serial bắt đầu <span class="red">*</span> </label>
										<asp:TextBox runat="server" ID="txtStart" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
									</div>

									<div class="form-group col-lg-10 ">

										<label>Serial kết thúc <span class="red">*</span> </label>
										<asp:TextBox runat="server" ID="txtEnd" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
										<asp:LinkButton runat="server" ID="btnAll" OnClick="btnAll_Click" Style="margin-top: 10px; float: left;">Lấy tất cả tem của lô</asp:LinkButton>

									</div>

									<div class="form-group col-lg-10">
										<label>Danh sách Serial(Mỗi dòng một Serial)</label>
										<asp:TextBox CssClass="form-control" runat="server" ID="txtList" TextMode="MultiLine" Enabled ="false" Rows="4" onKeyUp="countLines(this)"></asp:TextBox>
										<small style="margin-left: 30%; float: left; margin-top: 10px; margin-bottom: -1rem;">Tổng số Serial: <span id="linesUsed">0</span></small>
									</div>


									<div class="form-group col-lg-10 mb-0">
										<div style="margin-left: 30%;">
											<asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
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
			</div>
		</div>

		<%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
		<asp:HiddenField runat="server" ID="Count" ClientIDMode="Static"/>
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
	<script src="../../js/Function.js"></script>
	<script type="text/javascript">
		function countLines(theArea) {
			var text = $(theArea).val();
			var lines = text.split("\n");
			var count = 0;
			for (var i = 0; i < lines.length - 1; i++) {
				if (lines[i].trim() != "" && lines[i].trim() != null) {
					count += 1;
				}
			}
			var linesUsedspan = document.getElementById('linesUsed');
			linesUsedspan.innerHTML = count;
			$("#Count").val(count);
		}
	</script>

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

