<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="QRCodePackeWarehouseregister_List, App_Web_knjlquph" %>

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
						<h4 class="page-title">Quản Lý phân tem cho kho: <%=NameQRCodePackage %> </h4>
						<h6 >Sản phẩm: <%=NameProduct%> </h6>
						<h6>Tổng sô <%= TotalTem%> tem.Serial từ <%=SerialStart%> đến <% =SerialEnd %> </h6>
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

								<div class="col-lg-12  col-xs-12 mb-4 right">

									<asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
								</div>


							</div>
							<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									<table id="datatable" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>

												<%--<th>Tên doanh nghiệp </th>--%>
												<th>Tên kho </th>
												<th>Serial bắt đầu </th>
												<th>Serial kết thúc </th>
												<th>Số lượng tem </th>
												<th width="8%">Chức năng</th>
												<%--<th width="10%">Kích hoạt</th>
                                                <th width="5%">Chức năng</th>--%>
											</tr>
										</thead>
										<tbody>

											<asp:Repeater runat="server" ID="rptQRCodeWarehouse" OnItemCommand="rptQRCodeWarehouse_ItemCommand" OnItemDataBound="rptQRCodeWarehouse_ItemDataBound">
												<ItemTemplate>
													<tr>

														<%--<td><%#Eval("ProductBrandName")%></td>--%>
														<td><a href='QRCodePackeWarehouseregister_Add?QRCodeWarehouseRegister_ID=<%#Eval("QRCodeWarehouseRegister_ID") %>' class="breadcrumb-item active font-15"><%#Eval("NameWarehouse") %></td>
														<td><%#Eval("SerialNumberStart")%></td>
														<td><%#Eval("SerialNumberEnd")%></td>
														<td><%#Eval("QRCodeNumber")%></td>

														<td align="center">
															<a href='QRCodePackeWarehouseregister_Add?QRCodeWarehouseRegister_ID=<%#Eval("QRCodeWarehouseRegister_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
															<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete"  CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("QRCodeWarehouseRegister_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
