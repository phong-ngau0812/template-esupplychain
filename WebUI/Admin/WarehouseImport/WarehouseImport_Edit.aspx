<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="WarehouseImport_Edit.aspx.cs" Inherits="WarehouseImport_Edit" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />


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
                                <li class="breadcrumb-item"><a href="WarehouseImport_List.aspx">Quản lý nhập kho </a></li>
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
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group">
                                <label>Tên doanh nghiệp<span class="red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <div class="radio radio-primary">
                                    <asp:RadioButton runat="server" ID="rdoDaCo" Text="Chọn vật tư đã có" GroupName="rdo" Checked="true" AutoPostBack="true" OnCheckedChanged="rdoDaCo_CheckedChanged" />&nbsp;&nbsp;&nbsp;
									<asp:RadioButton runat="server" ID="rdo" Text="Chọn vật tư mới" GroupName="rdo" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
                                    <%--<asp:RadioButtonList runat="server" ID="rdoCheck" AutoPostBack="true" OnSelectedIndexChanged="rdoCheck_SelectedIndexChanged">
										<asp:ListItem Text="Chọn vật tư đã có" Value="1" Selected="True"></asp:ListItem>
										<asp:ListItem Text="Chọn vật tư mới" Value="0"></asp:ListItem>
									</asp:RadioButtonList>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Mã số lô<span class="red">*</span> </label>
                                <asp:TextBox runat="server" ID="txtCode" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Tên kho <span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="select2 form-control" Width="100%" AutoPostBack="true" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn kho (hoặc doanh nghiệp bạn chưa có kho)" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group" runat="server" id="Product">
                                <label>Sản Phẩm </label>
                                <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" id="NCC">
                                <label>Nhà cung cấp</label>
                                <asp:DropDownList runat="server" ID="ddlSupplier" CssClass="select2 form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" id="NameMaterial">
                                <label>Tên vật tư đã có trong doanh nghiệp </label>
                                <asp:DropDownList runat="server" ID="ddlMaterial" CssClass="select2 form-control" Width="100%"></asp:DropDownList>
                            </div>

                            <div class="form-group" runat="server" id="xuanhoa" visible="false">
                                <label>Mã lô vật tư</label>
                                <asp:TextBox runat="server" ID="txtMaterialPackage" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" id="sp">
                                <label>Lịch sử nguồn nguyên liệu </label>
                                <asp:ListBox runat="server" ID="ddlProductPackage" SelectionMode="Multiple" AutoPostBack="true" CssClass="select2 form-control" Width="100%" OnSelectedIndexChanged="ddlProductPackage_SelectedIndexChanged" data-placeholder="-- Chọn các lô --"></asp:ListBox>
                            </div>
                            <div runat="server" id="Data1" visible="false">
                                <table class="table table-striped table-bordered dt-responsive nowrap" id="myTable" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>Tên lô</th>
                                            <th width="20%">Số lượng</th>
                                            <th width="5%">Đơn vị</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptProductPackage" OnItemDataBound="rptProductPackage_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Name") %>
                                                        <asp:Literal runat="server" ID="lblProductPackage" Text='<%#Eval("ProductPackage_ID") %>' Visible="false"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control" min="0" Text='<%#Eval("Amount") %>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <%#Eval("Unit") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-warning">
                                            <td class="text-right">Tổng: </td>
                                            <td colspan="4">
                                                <asp:Label runat="server" ID="total_sum_value" Text="0"><%=TotalProductPackage.ToString() %></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <%--<div runat="server" id="ThemMoi" >
								

								<div class="form-group" runat="server" id="NameMaterial">
									<label>Tên vật tư mới<span class ="red">*</span> </label>
									<asp:TextBox runat="server" ID="txtMaterial" required data-parsley-required-message="Bạn chưa nhập tên vật tư mới" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
								</div>

								<div class="form-group" runat="server" id="MaterialType">
									<label>Loại vật tư<span class ="red">*</span>  </label>
									<asp:DropDownList runat="server" ID="ddlMaterialType"  data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn loại vật tư"  CssClass="select2 form-control" Width="100%"></asp:DropDownList>
								</div>


							</div>--%>

                            <%--<div class="form-group">
                                <label> Tên vật liệu nhập </label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập tên vật liệu" ></asp:TextBox>
                            </div>--%>

                            <div class="form-group" runat="server" id="vattu">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>số lượng </label>
                                        <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" CssClass="form-control formatMoney" Width="200px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2" runat="server" id="UnitMaterila">
                                        <label>Đơn vị tính </label>
                                        <asp:TextBox runat="server" ID="txtUnit" ClientIDMode="Static" CssClass="form-control" Width="200px"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <label>Đơn Giá</label>
                                <asp:TextBox runat="server" ID="txtPrice" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Ngày nhập </label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <div class="row" style="width: 100%">
                                            <div class="col-lg-6 row">
                                                <div class="col-lg-9" style="padding-right: 0px;">
                                                    <asp:TextBox runat="server" ID="txtNgayDukien" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                                </div>
                                                <div class="col-lg-3" style="padding-left: 0px;">
                                                    <div class="input-group-append ">
                                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlHour" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Giờ</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlMinutes" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Phút</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Hạn sử dụng đến ngày</label>
                                <div class="input-group" style="width: 300px">

                                    <asp:TextBox runat="server" ID="txtSX" ClientIDMode="Static" CssClass="form-control" Text="" />
                                    <%--Text="01/01/1980"--%>
                                    <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Người nhập vào kho<span class="red">*</span> </label>
                                <asp:TextBox runat="server" ID="txtImprot" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Bạn chưa nhập người nhập vào kho"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Người giao</label>
                                <asp:TextBox runat="server" ID="txtSender" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Mô tả</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>

                            <!--end form-group-->


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
                    </div>
                    <!--end card-->
                </div>

            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <asp:HiddenField ID="hdfWare" runat="server" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {



            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });

            $(".select2").select2({
                width: '100%'
            });
            Init();
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
        });
    </script>
    <script src="../../js/Function.js"></script>

    <script src="/theme/plugins/select2/select2.min.js"></script>


    <!-- Parsley js -->
    <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <%--<script src="/theme/plugins/select2/select2.min.js"></script>--%>
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

</asp:Content>

