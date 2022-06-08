<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackeProductPackageregister_List.aspx.cs" Inherits="QRCodePackeProductPackageregister_List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<!-- DataTables -->
	<link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<!-- Responsive datatable examples -->
	<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />

	<style>
		@media only screen and (max-width: 1024px) {
			.marginBtnAdd {
				margin-bottom: 7px
			}
		}
	</style>

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
								<li class="breadcrumb-item active"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Quản Lý phân lô sản xuất cho lô tem:<%=NameProductPackage %> </h4>
						<h5 class="page-title">Sản phẩm:<%=NameProduct%> </h5>
						<p class="page-title">Tổng sô<%= TotalTem%> tem.Serial từ <%=SerialStart%> đến <% =SerialEnd %> </p>
					</div>
					<!--end page-title-box-->
				</div>
				<!--end col-->
			</div>
			<!-- end page title end breadcrumb -->
			<div class="row">
				<div class="col-9">
					<div class="card">
						<div class="card-body">
							<h4 style="text-align: center">Danh sách lô sản xuất đã phân cho lô tem</h4>
							<div class="row">
								<div class="col-lg-12  col-xs-12 mb-4 right">
									<asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
								</div>
							</div>
							<asp:ScriptManager runat="server" ID="ScriptManager2"></asp:ScriptManager>
							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									<table id="datatable" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>
												<th>Tên lô sản xuất </th>
												<th>Serial bắt đầu </th>
												<th>Serial kết thúc </th>
												<th>Số lượng tem </th>
												<th width="5%">Chức năng</th>
											</tr>
										</thead>
										<tbody>

											<asp:Repeater runat="server" ID="rptMateria" OnItemCommand="rptMateria_ItemCommand" OnItemDataBound="rptMateria_ItemDataBound">
												<ItemTemplate>
													<tr>
														<td><a href='Material_Edit?Material_ID=<%#Eval("Material_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Name") %></td>
														<td><%#Eval("Unit")%></td>
														<td><%#Eval("Unit")%></td>
														<td><%#Eval("Unit")%></td>

														<td align="center">
															<div class="div-edit" runat="server" id="Edit" visible='<%#MyActionPermission.CanEdit() %>'>
																<a href='Material_Edit?Material_ID=<%#Eval("Material_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
															</div>
															<div class="div-edit">
																<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" Visible='<%#MyActionPermission.CanDeleteMaterial(Convert.ToInt32(Eval("Material_ID").ToString()),ref Message) %>' CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("Material_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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

				<div class="col-lg-3">
					<div class="card">
						<div class="card-body">
							<asp:UpdatePanel runat="server" ID="UpdatePanel2">
								<ContentTemplate>
									<h4 style="text-align: center">Thông tin cấu hình lô mã </h4>
									<div class="card">
										<div class="card-body">

											<div class="form-group">
												<label>Sản phẩm: </label>
												<asp:TextBox runat="server" ID="TextBox3" ClientIDMode="Static" CssClass="form-control formatMoney" placeholder="Tự động..." Enabled="false"></asp:TextBox>
											</div>
											<div class="form-group">

												<label>Tên lô mã:</label>
												<asp:TextBox runat="server" ID="TextBox1" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
											</div>

											<div class="form-group">
												<label>Trạng thái:</label>
												<asp:TextBox runat="server" ID="TextBox2" ClientIDMode="Static" CssClass="form-control formatMoney" placeholder="Tự động..." Enabled="false"></asp:TextBox>
											</div>
											<div class="form-group">
												<%-- <label>Âm Thanh </label>--%>
												<div class="custom-control custom-checkbox">
													<asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" />
													<label for="ckActive" class="custom-control-label">
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
			$("#ckActive").addClass("custom-control-input");
			$(".select2").select2({
				width: '100%'
			});
			Init();
		});
	</script>
</asp:Content>
