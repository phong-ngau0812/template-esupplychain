<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="TaskStepQuestion_List, App_Web_bplabqom" %>

<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
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
								<li class="breadcrumb-item active"><a>Quản lý câu hỏi </a></li>
								<li class="breadcrumb-item "><%=LinkCallBack %></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Quản lý câu hỏi </h4>
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
							<%--<uc1:ctlDatePicker ID="ctlDatePicker1" runat="server" OnDateChange="ctlDatePicker1_DateChange" />
							<br />--%>
							<%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
							<div class="row">
								<div class="col-lg-4 col-xs-12 mb-3">
									<asp:DropDownList runat="server" ID="ddlTask" ClientIDMode="Static" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"></asp:DropDownList>
								</div>

								<div class="col-lg-8 col-xs-12 mb-3 right">
									<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" />
								</div>
							</div>
							<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
							<asp:UpdatePanel runat="server" ID="up">
								<ContentTemplate>
									<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
									<table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
										<thead>
											<tr>
												<th width="5%">STT</th>
												<th>Nội dung câu hỏi </th>
												<th>Đề mục công việc </th>
												<th width="10%">Thêm đáp án</th>
												<th width="10%">Chức năng</th>
											</tr>
										</thead>
										<tbody>

											<asp:Repeater runat="server" ID="rptTaskStepQuestion" OnItemCommand="rptTaskStepQuestion_ItemCommand" OnItemDataBound="rptTaskStepQuestion_ItemDataBound">
												<ItemTemplate>
													<tr>
														<td><%=Srot++%></td>
														<td><a href='TaskStepQuestion_Edit?TaskStepQuestion_ID=<%#Eval("TaskStepQuestion_ID") %>' class="breadcrumb-item active font-15"><%#Eval("Name") %></td>
														<td><%#Eval("NameTaskStep") %> </td>
														<td>
															<a href='TaskStepAnswer_Add?TaskStepQuestion_ID=<%#Eval("TaskStepQuestion_ID")%>' class="mr-2" ><span class ="btn btn-gradient-success">Thêm đáp án </span> </a>
														</td>
														<%--<td>
															<asp:Literal runat="server" ID="lblID" Text='<%#Eval("TaskStepQuestion_ID") %>' Visible="false"></asp:Literal>
															<asp:Literal runat="server" ID="lblText"></asp:Literal>
															<asp:Literal runat="server" ID="lblApproved" Text='<%#Eval("Active") %>' Visible="false"></asp:Literal>
														</td>--%>
														<td align="center">
															<%--<asp:LinkButton runat="server" ID="btnActive" CommandName="Deactive" CssClass="mr-2" ToolTip="Kích hoạt" CommandArgument='<%#Eval("TaskStepQuestion_ID") %>'><i class="fas fa-check text-success font-16"></i></asp:LinkButton>
															<asp:LinkButton runat="server" ID="btnDeactive" CommandName="Active" CssClass="mr-2" ToolTip="Ngừng kích hoạt" CommandArgument='<%#Eval("TaskStepQuestion_ID") %>'><i class="fas fa-stop text-danger font-16"></i></asp:LinkButton>--%>
															<a href='TaskStepQuestion_Edit?TaskStepQuestion_ID=<%#Eval("TaskStepQuestion_ID")%>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
															<asp:LinkButton runat="server" ID="LinkButton1" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("TaskStepQuestion_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
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
