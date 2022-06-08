<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackeWorkshopregister_List.aspx.cs" Inherits="QRCodePackeWorkshopregister_List" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
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
						<h4 class="page-title">Quản Lý phân nhân viên/hộ sản xuất: <%=NameQRCodePackage %> </h4>
						<h6>Sản phẩm: <%=NameProduct%> </h6>
						<h6>Tổng sô <%= TotalTem%> tem.Serial từ <%=SerialStart%> đến <% =SerialEnd %> </h6>
					</div>
					<!--end page-title-box-->
				</div>
				<!--end col-->
			</div>
			<!-- end page title end breadcrumb -->
			<div class="row">

				<div class="col-lg-3">
					<div class="card">
						<div class="card-body">
							<h4 style="text-align: center">Danh sách nhân viên/hộ sản xuất</h4>

							<div class="row">
								<div class="col-md-12 mb-3">
									<asp:TextBox runat="server" ID="txtSearch" placeholder="Tên nhân viên" CssClass="form-control"></asp:TextBox>
									<div class="mt-3 right">
										<asp:Button CssClass="btn btn-gradient-primary mr-2" runat="server" ID="btnSearch" Text="Tìm kiếm" ClientIDMode="Static" OnClick="btnSearch_Click" />
									</div>

								</div>
								<div class="col-md-12 mb-3">
									<label>Hiển thị </label>
									<asp:DropDownList runat="server" ID="ddlItem" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="custom-select custom-select-sm form-control form-control-sm">

										<asp:ListItem Value="10"></asp:ListItem>
										<asp:ListItem Value="20"></asp:ListItem>
										<asp:ListItem Value="30"></asp:ListItem>
										<asp:ListItem Value="50"></asp:ListItem>
										<%--
                                <asp:ListItem Value="100"></asp:ListItem>--%>
									</asp:DropDownList>Tổng <%=TotalItem %> Nhân viên/chủ hộ 
								</div>
							</div>

							<asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
							<asp:UpdatePanel runat="server" ID="up1">
								<ContentTemplate>
									<table id="datatable1" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>
												<th>STT </th>
												<th>Tên nhân viên/hộ sản xuất </th>
											</tr>
										</thead>
										<tbody>

											<asp:Repeater runat="server" ID="rptWorkshop">
												<ItemTemplate>
													<tr>
														<td><%#Eval("RowNum")%></td>
														<td><%#Eval("Name")%></td>
													</tr>
												</ItemTemplate>
											</asp:Repeater>
										</tbody>
									</table>

									<div id="x_box_pager" style="float: right; text-align: right; margin-top: 10px" runat="Server" class="box_pager">
										<label>Trang <%=Pager1.CurrentIndex %>/<%=TotalPage %></label>
										(<label> <%=TotalItem %> Nhân viên/chủ hộ</label>)
                                         <cc1:PagerV2_8 ID="Pager1" runat="server" OnCommand="Pager1_Command"
											 GenerateFirstLastSection="True" GenerateGoToSection="False" GenerateHiddenHyperlinks="False"
											 GeneratePagerInfoSection="False" NextToPageClause="" OfClause="/" PageClause=""
											 ToClause="" CompactModePageCount="1" MaxSmartShortCutCount="5" NormalModePageCount="5"
											 GenerateToolTips="False" BackToFirstClause="" BackToPageClause="" FromClause=""
											 GenerateSmartShortCuts="False" GoClause="" GoToLastClause="" />
										<div class="clear">
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
							<h4 style="text-align: center">Danh sách nhân viên/hộ sản xuất đã phân</h4>
							<div class="row">
								<div class="col-lg-12  col-xs-12 mb-4 right">
									<asp:Button CssClass="btn btn-gradient-primary marginBtnAdd" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
								</div>


							</div>

							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									<table id="datatable" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>
												<th>Tên nhân viên/hộ sản xuất </th>
												<th>Serial bắt đầu </th>
												<th>Serial kết thúc </th>
												<th>Số lượng tem </th>
												<th width="5%">Chức năng</th>
											</tr>
										</thead>
										<tbody>

											<asp:Repeater runat="server" ID="rptQRCodePackeWorkshop" OnItemCommand="rptQRCodePackeWorkshop_ItemCommand" OnItemDataBound="rptQRCodePackeWorkshop_ItemDataBound">
												<ItemTemplate>
													<tr>


														<td><a href='QRCodePackeWorkshopregister_Add?QRCodeWorkshopRegister_ID=<%#Eval("QRCodeWorkshopRegister_ID") %>' class="breadcrumb-item active font-15"><%#Eval("NameWorkshop") %></td>
														<td><%#Eval("SerialNumberStart")%></td>
														<td><%#Eval("SerialNumberEnd")%></td>
														<td><%#Eval("QRCodeNumber")%></td>

														<td align="center">

															<a href='QRCodePackeWorkshopregister_Add?QRCodeWorkshopRegister_ID=<%#Eval("QRCodeWorkshopRegister_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>

															<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("QRCodeWorkshopRegister_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>


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
			$("#CheckSound").addClass("custom-control-input");
			$(".select2").select2({
				width: '100%'
			});
			Init();
		});


		$("#btnSearch").click(function () {
			//$("#spinner").show();
		});
		var input = document.getElementById("txtSearch");

		// Execute a function when the user releases a key on the keyboard
		input.addEventListener("keyup", function (event) {
			// Number 13 is the "Enter" key on the keyboard
			if (event.keyCode === 13) {
				// Cancel the default action, if needed
				event.preventDefault();
				// Trigger the button element with a click
				document.getElementById("btnSearch").click();
			}
		});
	</script>
</asp:Content>
