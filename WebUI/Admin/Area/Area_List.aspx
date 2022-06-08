﻿<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="Area_List.aspx.cs" Inherits="Area_List" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<!-- DataTables -->
	<link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<!-- Responsive datatable examples -->
	<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
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
								<li class="breadcrumb-item active"><a>Quản lý khu sản xuất </a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Quản lý khu sản xuất </h4>
					</div>
					<!--end page-title-box-->
				</div>
				<!--end col-->
			</div>
			<!-- end page title end breadcrumb -->
			<div class="row">
				<div class="col-12">
					<div class="card">
						<div class="card-body">
							<%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
							<div class="row">
								<div class="col-lg-4 col-xs-12 mb-2">
									<asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Width="300px" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
								</div>
								<div class="col-md-4 mb-3">
									<asp:DropDownList runat="server" ID="ddlZone" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
								</div>
								<div class="col-lg-4 col-xs-12 mb-2 right">
									
									<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
									<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnExportFile" Text="Xuất file" OnClick="btnExportFile_Click" />
								</div>
							</div>
							<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									<table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>
												<%--<th>Tên doanh nghiệp </th>--%>
												<th>Tên vùng sản xuất </th>
												<th>Tên khu vực sản xuất </th>
												<th>Diện tích(m2) </th>
												<th>Số nhân viên</th>
												<th width="8%">Chức năng</th>
											</tr>
										</thead>
										<tbody>
											<asp:Repeater runat="server" ID="rptArea" OnItemCommand="rptArea_ItemCommand" OnItemDataBound="rptArea_ItemDataBound">
												<ItemTemplate>
													<tr>
														<%--<td><%#Eval("ProductBrandName")%></td>--%>
														<td><%#Eval("ZoneName")%></td>
														<td><a href='Area_Edit?Area_ID=<%#Eval("Area_ID")%> ' class="breadcrumb-item active font-15"><%#Eval("Name") %></td>	
														<td><%#Eval("Acreage")%></td>
														<td>
															<%#Eval("Soluong") %>
															<%--<%#ReturnCount(Convert.ToInt32( Eval("Area_ID"))) %>--%>
														</td>
														<td align="center">
															<div class="div-edit" runat="server" id="Edit" visible='<%#MyActionPermission.CanEdit() %>'>
																<a href='Area_Edit?Area_ID=<%#Eval("Area_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
															</div>
															<div class="div-edit">
																<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteArea(Convert.ToInt32(Eval("Area_ID").ToString()),ref Message) %>' CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Area_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
																
															</div>
														</td>
													</tr>
												</ItemTemplate>
											</asp:Repeater>
										</tbody>
									</table>
								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>
				<!-- end col -->
			</div>
		</div>
		<!-- container -->
		<!--  Modal content for the above example -->
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
	<!-- Responsive examples -->
	<script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
	<script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
	<script src="/theme/assets/pages/jquery.datatable.init.js"></script>
	<script src="/theme/plugins/select2/select2.min.js"></script>
	<script>
		$(window).on('load', function () {
			setTimeout(function () { $('#spinner').fadeOut(); }, 200);
		})
		$(document).ready(function () {
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
			if (typeof (Sys) !== 'undefined') {
				var parameter = Sys.WebForms.PageRequestManager.getInstance();
				parameter.add_endRequest(function () {
					setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
				});
			}
		});
		$(document).ready(function () {
			$(".select2").select2({
				width: '100%'
			});
			Init();
		});
	</script>
</asp:Content>
