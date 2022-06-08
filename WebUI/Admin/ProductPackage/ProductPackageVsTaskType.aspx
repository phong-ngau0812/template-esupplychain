<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="ProductPackageVsTaskType.aspx.cs" Inherits="ProductPackageVsTaskType"  MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<!-- DataTables -->
	<link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<!-- Responsive datatable examples -->
	<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

	<link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
	<form runat="server" id="frm1">
		<div class="container-fluid">
			<!-- Page-Title -->
			<div class="row">
				<div class="col-sm-12">
					<div class="page-title-box">
						<div class="float-right">
							<ol class="breadcrumb">
								<li class="breadcrumb-item active"><a>Hiển thị nhật ký bên cổng truy xuất </a></li>
								<li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=namepackage %></a></li>
								<li class="breadcrumb-item "><a href="ProductPackage_List.aspx">Quản lý lô</a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title"></h4>
					</div>
					<!--end page-title-box-->
				</div>
				<!--end col-->
			</div>

			<div class="row">
				<div class="col-12">

					<div class="card" runat="server">
						<div class="card-body">

							<div class="row">
								<div class="col-md-12 col-xs-12">

									<h4>Hiển thị nhật ký bên cổng truy xuất của lô: <%=namepackage %></h4>
									<div class="form-group">
										<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									</div>
									<div class="form-group row ">
										<div class="col-lg-12">
											
											<asp:ListBox runat="server" ClientIDMode="Static" ID="ddlTaskType" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskType_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn nhật ký --"></asp:ListBox>
										</div>
									
									</div>
								</div>
								<div class="col-md-12 col-xs-12" runat="server" id="Data1">
									<table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>

											<tr>
												<th>Loại nhật ký</th>
												
											</tr>
										</thead>
										<tbody>
											<asp:Repeater runat="server" ID="rptTaskType" OnItemDataBound="rptTaskType_ItemDataBound">
												<ItemTemplate>
													<tr>
														<td><%#Eval("Name") %>
															<asp:Literal runat="server" ID="lblTaskType_ID" Text='<%#Eval("TaskType_ID") %>' Visible="false"></asp:Literal>
														</td>
	
													</tr>
												</ItemTemplate>
											</asp:Repeater>
										</tbody>
									</table>
									<div class="form-group mb-0">
										<asp:Button runat="server" ID="btnAddTaskType" ClientIDMode="Static" OnClick="btnAddTaskType_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu lại" />
										<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>

									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<!-- end col -->
			</div>
		</div>
		<!-- container -->


	</form>
	<!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
	<!-- Required datatable js -->

	<script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>
	<script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>
	<!-- Buttons examples -->
	<script src="/theme/plugins/datatables/dataTables.buttons.min.js"></script>
	<script src="/theme/plugins/datatables/buttons.bootstrap4.min.js"></script>
	<script src="/theme/plugins/datatables/jszip.min.js"></script>
	<script src="/theme/plugins/datatables/pdfmake.min.js"></script>
	<script src="/theme/plugins/datatables/vfs_fonts.js"></script>
	<script src="/theme/plugins/datatables/buttons.html5.min.js"></script>
	<script src="/theme/plugins/datatables/buttons.print.min.js"></script>
	<script src="/theme/plugins/datatables/buttons.colVis.min.js"></script>
	<script src="/theme/plugins/select2/select2.min.js"></script>
	<!-- Responsive examples -->
	<script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
	<script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
	<script src="/theme/assets/pages/jquery.datatable.init.js"></script>
	<script>
		$(window).on('load', function () {
			setTimeout(function () { $('#spinner').fadeOut(); }, 200);
		})
		$(document).ready(function () {

			//$(function () {
			//    $(".formatMoney").keyup(function (e) {
			//        $(this).val(format($(this).val()));
			//    });
			//});

			$(".select2").select2({
				width: '100%'
			});
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
			if (typeof (Sys) !== 'undefined') {
				var parameter = Sys.WebForms.PageRequestManager.getInstance();
				parameter.add_endRequest(function () {
					setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
					$(".select2").select2({
						width: '100%'
					});
				});
			}
		});
	</script>
	<script src="../../js/Function.js"></script>
</asp:Content>
