<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="QRCodePackageReport_List, App_Web_knjlquph" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlDatePicker.ascx" TagPrefix="uc1" TagName="ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<!-- DataTables -->
	<link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
	<%--<link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
	<!-- Responsive datatable examples -->
	<%--<link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
	<link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/RWD-Table-Patterns/dist/css/rwd-table.min.css" rel="stylesheet" type="text/css" media="screen">

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
								<li class="breadcrumb-item active">Báo Cáo Số Lượng Tem</li>
								<li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Báo Cáo Số Lượng Tem</h4>
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
							<uc1:ctlDatePicker ID="ctlDatePicker1" runat="server" OnDateChange="ctlDatePicker1_DateChange" />
							<br />
							<div class="row">
								<div class="col-md-3 mb-3">
									<asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged"></asp:DropDownList>
								</div>
								<div class="col-md-3 mb-3">
									<asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
								</div>

								<!-- end row -->
								<div class="col-md-3 mb-3">
									<asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
										<asp:ListItem Value="1" Text="Tem công khai"></asp:ListItem>
										<asp:ListItem Value="2" Text="Tem bí mật"></asp:ListItem>
									</asp:DropDownList>
								</div>
								<div class="col-md-3 mb-3">
									<asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
								</div>
								<!-- end row -->

								<div class="col-md-12 right mb-3">

									<asp:Button CssClass="btn btn-gradient-primary mr-3" runat="server" ID="btnExport" Text="Xuất file" OnClick="btnExport_Click" />
								</div>
								<!-- end row -->
							</div>

							<asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="1" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Tháng 1</th>
                                                <th>Tháng 2</th>
                                                <th>Tháng 3</th>
                                                <th>Tháng 4</th>
												<th>Tháng 5</th>
                                                <th>Tháng 6</th>
                                                <th>Tháng 7</th>
                                                <th>Tháng 8</th>
												<th>Tháng 9</th>
                                                <th>Tháng 10</th>
                                                <th>Tháng 11</th>
                                                <th>Tháng 12</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptQRCodeReport">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("T1") %> </td>
                                                        <td><%#Eval("T2") %> </td>
                                                        <td><%#Eval("T3") %> </td>
														<td><%#Eval("T4") %> </td>
														<td><%#Eval("T5") %> </td>
                                                        <td><%#Eval("T6") %> </td>
                                                        <td><%#Eval("T7") %> </td>
														<td><%#Eval("T8") %> </td>
														<td><%#Eval("T9") %> </td>
                                                        <td><%#Eval("T10") %> </td>
                                                        <td><%#Eval("T11") %> </td>
														<td><%#Eval("T12") %>  </td>
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
			</div>
		</div>

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
