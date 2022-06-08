<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="QRCodePacke_Edit_ProductPackage, App_Web_knjlquph" validaterequest="false" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
	<link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
	<link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
	<link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
	<link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
	<link href="../../css/telerik.css" rel="stylesheet" type="text/css" />

	<style>
		.table-bordered {
			border: 0px solid #eaf0f7 !important;
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
								<li class="breadcrumb-item active">Cập nhật thông tin lô sản xuất cho lô mã </li>
								<li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
								<li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
							</ol>
						</div>
						<h4 class="page-title">Cập nhật thông tin lô sản xuất cho lô mã</h4>
						<br />
						<h5 class="page-title mb-3">Tên lô mã: <%=NameProductPackageQR%> </h5>


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
								<h4>Tìm kiếm lô sản xuất </h4>
							</div>
							<div class="card">
								<div class="card-body">

									<%--<div class="row">
										<div class="col-lg-12">--%>
									<div class="form-group">
										<div class="row ">
											<div class="col-lg-6">
												<asp:TextBox runat="server" ID="txtSearch" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
											</div>
											<div class="col-lg-6">

												<asp:Button runat="server" ID="btnSearch" ClientIDMode="Static" OnClick="btnSearch_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Tìm kiếm" />
											</div>
										</div>
									</div>
									<div runat="server" id="Data" visible="false">
										<p style="margin-top: 10px; margin-bottom: 0px;"><b>Thông tin DN sản xuất</b></p>
										<table style="width: 100%;">
											<tbody>
												<asp:Repeater runat="server" ID="rptProductBrand">
													<ItemTemplate>
														<tr>
															<td class="col-left" style="width: 30%;">Tên doanh nghiệp</td>
															<td><%#Eval("Name")%></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Địa chỉ</td>
															<td><%#string.IsNullOrEmpty(Eval("Address").ToString()) ? (Eval("NameWard"))+","+(Eval("NameDistrict"))+","+(Eval("NameLocation")):(Eval("Address"))%></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Số điện thoại</td>
															<td><%#Eval("Telephone")%></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Email</td>
															<td><%#Eval("Email")%></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Website</td>
															<td><%#Eval("Website")%></td>
														</tr>
													</ItemTemplate>
												</asp:Repeater>
											</tbody>
										</table>
										<p style="margin-top: 10px; margin-bottom: 0px;"><b>Thông tin lô sản xuất</b></p>

										<table style="width: 100%;">
											<tbody>
												<asp:Repeater runat="server" ID="rptProductPackage">
													<ItemTemplate>
														<tr>
															<td class="col-left" style="width: 30%;">Mã lô</td>
															<td><%#Eval("Code") %></td>
														</tr>

														<tr>
															<td class="col-left" style="width: 30%;">Tên lô sản xuất</td>
															<td><%#Eval("Name") %></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Ngày tạo lô</td>
															<td><%#DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Phụ trách sản xuất</td>
															<td><%#Eval("WorkshopName") %> </td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Vùng sản xuất</td>
															<td><%#Eval("ZoneName") %></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">Khu sản xuất</td>
															<td><%#Eval("AreaName") %></td>
														</tr>
														<tr>
															<td class="col-left" style="width: 30%;">thửa đất</td>
															<td><%#Eval("FarmName") %></td>
														</tr>
													</ItemTemplate>
												</asp:Repeater>
											</tbody>
										</table>
										<div class="mb-3 mt-3" runat="server" id="DataTask">
											<ul class="nav nav-tabs" role="tablist">
												<li class="nav-item" runat="server" id="NKSX" visible="false">
													<%--aria-selected="true"--%>
													<a class="nav-link" data-toggle="tab" href="#tab1" role="tab">Nhật ký sản xuất</a>
												</li>
												<li class="nav-item" runat="server" id="NKVT" visible="false">
													<a class="nav-link" data-toggle="tab" href="#tab2" role="tab" aria-selected="false">Nhật ký vật tư</a>
												</li>
												<li class="nav-item" runat="server" id="NKTH" visible="false">
													<a class="nav-link" data-toggle="tab" href="#tab3" role="tab" aria-selected="false">Nhật ký thu hoạch</a>
												</li>
												<li class="nav-item" runat="server" id="NKSCCB" visible="false">
													<a class="nav-link" data-toggle="tab" href="#tab4" role="tab" aria-selected="false">Nhật ký sơ chế, chế biến</a>
												</li>
												<li class="nav-item" runat="server" id="NKVC" visible="false">
													<a class="nav-link" data-toggle="tab" href="#tab5" role="tab" aria-selected="false">Nhật ký vận chuyển</a>
												</li>
												<li class="nav-item" runat="server" id="NKBH" visible="false">
													<a class="nav-link" data-toggle="tab" href="#tab6" role="tab" aria-selected="false">Nhật ký bán hàng</a>
												</li>
											</ul>

											<div class="tab-content">
												<div class="tab-pane mt-3 active" id="tab1" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Đầu mục công việc</th>
																<th>Người thực hiện </th>
																<th>Vị trí </th>
																<th>Ngày thực hiện</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistorySX">
																<ItemTemplate>
																	<tr>
																		<td>
																			<%#Eval("Name") %>
																		</td>
																		<td>
																			<%#Eval("UserName") %>
																		</td>
																		<td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
																		<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
												<div class="tab-pane mt-3" id="tab2" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Tên vật tư</th>
																<th>Người sử dụng</th>
																<th>Ngày thực hiện</th>
																<th>Số lượng</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistoryVT">
																<ItemTemplate>
																	<tr>
																		<td>
																			<%#Eval("Name") %>
																		</td>
																		<td><%#Eval("UserName") %>
																			<br />
																			<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																		</td>
																		<td><%--<%#Eval("BuyerName") %>--%>
																			<%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>
																		</td>
																		<td><%#decimal.Parse( Eval("Quantity").ToString()).ToString("N0") %> <%#Eval("Unit") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
												<div class="tab-pane mt-3" id="tab3" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Nội dung thu hoạch </th>
																<th>Số lượng thu hoạch</th>
																<th>Số ngày thu hoạch còn lại</th>
																<th>Ngày thu hoạch</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistoryTH">
																<ItemTemplate>
																	<tr>
																		<td>
																			<%#Eval("Name") %>
																		</td>
																		<td>
																			<%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%>
																		</td>
																		<td><%#Eval("HarvestDayRemain") %>
																		</td>
																		<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
												<div class="tab-pane mt-3" id="tab4" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Đầu mục công việc</th>
																<th>Người thực hiện</th>
																<th>vị trí</th>
																<th>Ngày thực hiện</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistoryCB">
																<ItemTemplate>
																	<tr>
																		<td><%#Eval("Name") %>
																		</td>
																		<td>
																			<%#Eval("UserName") %>
																		</td>
																		<td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
																		<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
												<div class="tab-pane mt-3" id="tab5" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Nội dung vận chuyển </th>
																<th>Người giao</th>
																<th>Người nhận</th>
																<th>Ngày vận chuyển</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistoryVC">
																<ItemTemplate>
																	<tr>
																		<td>
																			<%#Eval("Name") %>
																		</td>
																		<td><%#Eval("UserName") %>
																			<br />
																			<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																		</td>
																		<td><%#Eval("BuyerName") %>                                                          
																		</td>
																		<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
												<div class="tab-pane mt-3" id="tab6" role="tabpanel">
													<table class="table table-striped table-bordered table-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
														<thead>
															<tr>
																<th>Nội dung bán hàng </th>
																<th>Người bán</th>
																<th>Người mua</th>
																<th>Ngày bán</th>
															</tr>
														</thead>
														<tbody>
															<asp:Repeater runat="server" ID="rptTaskHistoryBH">
																<ItemTemplate>
																	<tr>
																		<td>
																			<%#Eval("Name") %>
																		</td>
																		<td><%#Eval("UserName") %>
																			<br />
																			<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																		</td>
																		<td><%#Eval("BuyerName") %>
																		</td>
																		<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</tbody>
													</table>
												</div>
											</div>
										</div>
										<div>
											<asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Cập nhật lô sản xuất" />
										</div>
									</div>
									<%--	</div>
									</div>--%>
								</div>
							</div>

							<div class="card" runat="server" id="LoadListQRCode" visible="false">
								<div class="card-body">
									<h4 style="text-align: center">Danh sách Lô sản xuất được gán </h4>


									<asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound" OnItemCommand="rptList_ItemCommand">
										<ItemTemplate>

											<asp:Literal runat="server" ID="lblProductPackage_ID" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
											<div runat="server" id="DataList">
												<table style="width: 100%;">
													<tbody>
														<td>
															<p style="margin-top: 10px; margin-bottom: 0px;"><b>Thông tin DN sản xuất</b></p>
														</td>
														<td style="text-align: right">
															<asp:LinkButton runat="server" ID="LinkButton1" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("ProductPackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
														</td>
													</tbody>
												</table>

												<table style="width: 100%;">
													<tbody>
														<asp:Repeater runat="server" ID="rptProductBrandlist">
															<ItemTemplate>
																<tr>
																	<td class="col-left" style="width: 30%;">Tên doanh nghiệp</td>
																	<td><%#Eval("Name")%></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Địa chỉ</td>
																	<td><%#string.IsNullOrEmpty(Eval("Address").ToString()) ? (Eval("NameWard"))+","+(Eval("NameDistrict"))+","+(Eval("NameLocation")):(Eval("Address"))%></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Số điện thoại</td>
																	<td><%#Eval("Telephone")%></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Email</td>
																	<td><%#Eval("Email")%></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Website</td>
																	<td><%#Eval("Website")%></td>
																</tr>
															</ItemTemplate>
														</asp:Repeater>
													</tbody>
												</table>
												<p style="margin-top: 10px; margin-bottom: 0px;"><b>Thông tin lô sản xuất</b></p>

												<table style="width: 100%;">
													<tbody>
														<asp:Repeater runat="server" ID="rptProductPackgelist">
															<ItemTemplate>
																<tr>
																	<td class="col-left" style="width: 30%;">Mã lô</td>
																	<td><%#Eval("Code") %></td>
																</tr>

																<tr>
																	<td class="col-left" style="width: 30%;">Tên lô sản xuất</td>
																	<td><%#Eval("Name") %></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Ngày tạo lô</td>
																	<td><%#DateTime.Parse( Eval("CreateDate").ToString()).ToString("dd/MM/yyyy") %></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Phụ trách sản xuất</td>
																	<td><%#Eval("WorkshopName") %> </td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Vùng sản xuất</td>
																	<td><%#Eval("ZoneName") %></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">Khu sản xuất</td>
																	<td><%#Eval("AreaName") %></td>
																</tr>
																<tr>
																	<td class="col-left" style="width: 30%;">thửa đất</td>
																	<td><%#Eval("FarmName") %></td>
																</tr>
															</ItemTemplate>
														</asp:Repeater>
													</tbody>
												</table>
												<div class="mb-3 mt-3" runat="server" id="DataTask2" visible="false">
													<ul class="nav nav-tabs" role="tablist">
														<li class="nav-item" id='li1<%#Eval("ProductPackage_ID") %>'>
															<%--aria-selected="true"--%>
															<a class="nav-link active" data-toggle="tab" href='#tablist1<%#Eval("ProductPackage_ID") %>' role="tablist">Nhật ký sản xuất</a>
														</li>
														<li class="nav-item" id='li2<%#Eval("ProductPackage_ID") %>'>
															<a class="nav-link" data-toggle="tab" href='#tablist2<%#Eval("ProductPackage_ID") %>' role="tablist" aria-selected="false">Nhật ký vật tư</a>
														</li>
														<li class="nav-item" id='li3<%#Eval("ProductPackage_ID") %>'>
															<a class="nav-link" data-toggle="tab" href='#tablist3<%#Eval("ProductPackage_ID") %>' role="tablist" aria-selected="false">Nhật ký thu hoạch</a>
														</li>
														<li class="nav-item" id='li4<%#Eval("ProductPackage_ID") %>'>
															<a class="nav-link" data-toggle="tab" href='#tablist4<%#Eval("ProductPackage_ID") %>' role="tablist" aria-selected="false">Nhật ký sơ chế, chế biến</a>
														</li>
														<li class="nav-item" id='li5<%#Eval("ProductPackage_ID") %>'>
															<a class="nav-link" data-toggle="tab" href='#tablist5<%#Eval("ProductPackage_ID") %>' role="tablist" aria-selected="false">Nhật ký vận chuyển</a>
														</li>
														<li class="nav-item" id='li6<%#Eval("ProductPackage_ID") %>'>
															<a class="nav-link" data-toggle="tab" href='#tablist6<%#Eval("ProductPackage_ID") %>' role="tablist" aria-selected="false">Nhật ký bán hàng</a>
														</li>
													</ul>

													<div class="tab-content">
														<div class="tab-pane mt-3 active" id='tablist1<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Đầu mục công việc</th>
																		<th>Người thực hiện </th>
																		<th>Vị trí </th>
																		<th>Ngày thực hiện</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNTSX1">
																		<ItemTemplate>
																			<tr>
																				<td>
																					<%#Eval("Name") %>
																				</td>
																				<td>
																					<%#Eval("UserName") %>
																				</td>
																				<td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
																				<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
														<div class="tab-pane mt-3" id='tablist2<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Tên vật tư</th>
																		<th>Người sử dụng</th>
																		<th>Ngày thực hiện</th>
																		<th>Số lượng</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNKVT1">
																		<ItemTemplate>
																			<tr>
																				<td>
																					<%#Eval("Name") %>
																				</td>
																				<td><%#Eval("UserName") %>
																					<br />
																					<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																				</td>
																				<td><%--<%#Eval("BuyerName") %>--%>
																					<%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %>
																				</td>
																				<td><%#decimal.Parse( Eval("Quantity").ToString()).ToString("N0") %> <%#Eval("Unit") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
														<div class="tab-pane mt-3" id='tablist3<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Nội dung thu hoạch </th>
																		<th>Số lượng thu hoạch</th>
																		<th>Số ngày thu hoạch còn lại</th>
																		<th>Ngày thu hoạch</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNKTH">
																		<ItemTemplate>
																			<tr>
																				<td>
																					<%#Eval("Name") %>
																				</td>
																				<td>
																					<%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%>
																				</td>
																				<td><%#Eval("HarvestDayRemain") %>
																				</td>
																				<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
														<div class="tab-pane mt-3" id='tablist4<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Đầu mục công việc</th>
																		<th>Người thực hiện</th>
																		<th>vị trí</th>
																		<th>Ngày thực hiện</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNKSC1">
																		<ItemTemplate>
																			<tr>
																				<td><%#Eval("Name") %>
																				</td>
																				<td>
																					<%#Eval("UserName") %>
																				</td>
																				<td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
																				<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
														<div class="tab-pane mt-3" id='tablist5<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Nội dung vận chuyển </th>
																		<th>Người giao</th>
																		<th>Người nhận</th>
																		<th>Ngày vận chuyển</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNKVC1">
																		<ItemTemplate>
																			<tr>
																				<td>
																					<%#Eval("Name") %>
																				</td>
																				<td><%#Eval("UserName") %>
																					<br />
																					<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																				</td>
																				<td><%#Eval("BuyerName") %>                                                          
																				</td>
																				<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
														<div class="tab-pane mt-3" id='tablist6<%#Eval("ProductPackage_ID") %>' role="tabpanel">
															<table class="table table-striped table-bordered table-responsive nowrap" style="border: 0px solid #eaf0f7">
																<thead>
																	<tr>
																		<th>Nội dung bán hàng </th>
																		<th>Người bán</th>
																		<th>Người mua</th>
																		<th>Ngày bán</th>
																	</tr>
																</thead>
																<tbody>
																	<asp:Repeater runat="server" ID="rptNKBH1">
																		<ItemTemplate>
																			<tr>
																				<td>
																					<%#Eval("Name") %>
																				</td>
																				<td><%#Eval("UserName") %>
																					<br />
																					<i class="dripicons-location font-14"></i><%#Eval("Location") %>
																				</td>
																				<td><%#Eval("BuyerName") %>
																				</td>
																				<td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</tbody>
															</table>
														</div>
													</div>
												</div>
												<hr />
												<%--<div class="mb-3" style="width: 100%; border-bottom: #435177 solid 1.5px;">--%>
												<%--<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("ProductPackage_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>--%>
												<%--<asp:Button runat="server" ID="Button2" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-danger waves-effect m-l-5" Text="xóa" />--%>
												<%--</div>--%>
											</div>

										</ItemTemplate>
									</asp:Repeater>

								</div>
							</div>

							<div class="form-group ">
								<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
							</div>
						</div>
					</div>
					<div class="form-group">
						<asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
					</div>


				</div>
			</div>
		</div>

		<%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
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

	<script>

		$(window).on('load', function () {
			setTimeout(function () { $('#spinner').fadeOut(); }, 100);
		})
		//function Init() {
		//	$("li a").first().click();
		//	//$("li a").first().addClass("active");
		//	setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);	
		//}

		$(document).ready(function () {
			$("li a").first().click();
			//$("li a").first().addClass("active");
			setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
			//Init()
		});

		$("#btnSearch").click(function () {
			$("#spinner").show();
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

