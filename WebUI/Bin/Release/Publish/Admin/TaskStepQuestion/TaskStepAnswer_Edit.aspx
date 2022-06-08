<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="TaskStepAnswer_Edit, App_Web_bplabqom" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
<link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
      
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><% =LinkCallBack %> </li>
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
				<div class="col-12">
					<div class="card">
						<div class="card-body">
							<div class="row">
								<div class="col-md-5" runat="server" id="AddAnswer">
									<hr />
									<h4 style="text-align: center">Thông tin câu trả lời </h4>
									<div class="form-group">
										<label>Nội dung câu hỏi </label>
										<br />
										<b><%=Question %> </b>
									</div>
							
									<div class=" form-group" >
										<label>Nội dung câu trả lời <span class="red">*</span></label>
										
										<asp:TextBox runat="server" ID="txtName" TextMode="MultiLine" Rows="4" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập nội dung"> </asp:TextBox>

										<%--<textarea runat="server" ID="tsst" TextMode="MultiLine" Rows="4" style ="width :100%" CssClass="form-control"> </textarea>--%>
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
								<div class="col-md-7">
									<hr />
									<h4 class="md-3" style="text-align: center">Danh sách câu trả lời</h4>
									<h5 class="right"><asp:Button CssClass="btn btn-gradient-primary" UseSubmitBehavior="false" runat="server" ID="btnAdd" Text="Thêm mới" OnClick="btnAdd_Click" /></h5>
									<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
									<asp:UpdatePanel runat="server" ID="up">
										<ContentTemplate>
											<asp:Label runat="server" ID="Label1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
											 <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
												<thead>
													<tr>
														<th width="5%">STT</th>
														<th>Nội dung trả lời </th>
														<th width="15%">Chức năng</th>
													</tr>
												</thead>
												<tbody>

													<asp:Repeater runat="server" ID="rptTaskStepAnswer" OnItemCommand="rptTaskStepAnswer_ItemCommand" OnItemDataBound="rptTaskStepAnswer_ItemDataBound">
														<ItemTemplate>
															<tr>
																<td><%=Srot++%></td>
																<td><%#Eval("Name") %></td>

																<td align="center">
																	<a href='TaskStepAnswer_Edit?TaskStepAnswer_ID=<%#Eval("TaskStepAnswer_ID")%>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
																	<%-- <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" UseSubmitBehavior="false" CommandName="Edit" CssClass="mr-2" ToolTip="Sửa nội dung câu trả lời" CommandArgument='<%#Eval("TaskStepAnswer_ID") %>'><i class="fas fa-edit text-success font-16"></i></asp:LinkButton>--%>
																	<asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("TaskStepAnswer_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>
																
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

							<!--end card-->
						</div>

					</div>
				</div>

			</div>
            
        </div>
       <asp:HiddenField runat="server" ID="ProductID"/>
       <asp:HiddenField runat="server" ID="TaskStepQuestion_ID"/>
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


	<script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
	<script src="../../theme/assets/pages/jquery.validation.init.js"></script>

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
	<script src="../../theme/plugins/moment/moment.js"></script>
	<script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
	<script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>

</asp:Content>

