<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="PO_Edit.aspx.cs" Inherits="Admin_POManage_PO_Edit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <%--<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />

    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <style>
        .box-price {
            border: none;
            pointer-events: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="sc"></telerik:RadScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="Agency_List.aspx">Danh sách đơn hàng PO</a></li>
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
                                <label>Doanh nghiệp <span class="red">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="form-control select2" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Mã đơn hàng PO <span style="color: red; font-size: 15px">*</span></label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn nhập mã đơn hàng PO"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Tên khách hàng <span style="color: red; font-size: 15px">*</span></label>
                                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control select2" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn khách hàng"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Danh sách sản phẩm <span style="color: red; font-size: 15px">*</span></label>
                                <asp:ListBox runat="server" ID="ddlProduct" SelectionMode="Multiple" AutoPostBack="true" CssClass="select2 form-control" Width="100%" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" data-placeholder="-- Chọn các sản phẩm --"></asp:ListBox>
                            </div>
                            <div runat="server" id="Data1" visible="false">
                                <table class="table table-striped table-bordered dt-responsive nowrap" id="myTable" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                    <thead>
                                        <tr>
                                            <th width="20%">Sản phẩm</th>
                                            <th width="20%">Mô tả</th>
                                            <th width="15%">Ngày Giao</th>
                                            <th width="6%">Số Lượng</th>
                                            <th width="8%">Đơn giá</th>
                                            <th width="13%">Thành tiền</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptProduct">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#NameProduct(Eval("Product_ID").ToString()) %>
                                                        <asp:Literal runat="server" ID="lblProduct_ID" Text='<%#Eval("Product_ID") %>' Visible="false"></asp:Literal>
                                                        <asp:Literal runat="server" ID="lblPOVsProduct_ID" Text='<%#Eval("POVsProduct_ID") %>' Visible="false"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtContent" ClientIDMode="Static" CssClass="form-control" TextMode="MultiLine" Rows="3" Text='<%#Eval("Description")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker1" Width="70%" runat="server" SelectedDate='<%# string.IsNullOrEmpty(Eval("SendDate").ToString())? DateTime.Now : DateTime.Parse(Eval("SendDate").ToString()) %>'></telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control" min="0" Text='<%#Eval("Amount") %>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtPrice" ClientIDMode="Static" CssClass="form-control formatMoney" Text='<%#decimal.Parse(Eval("Price").ToString()).ToString("N0")%>'></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtTotalPrice" ClientIDMode="Static" CssClass="form-control box-price" Text='<%#decimal.Parse(Eval("TotalPrice").ToString()).ToString("N0") %>'></asp:TextBox>
                                                    </td>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-group mt-3">
                                <label>Ngày tạo</label>
                                <div class="input-group" style="width: 200px;">
                                    <asp:TextBox runat="server" ID="txtDateStart" ClientIDMode="Static" CssClass="form-control" name="birthday" Text="" />
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Nội dung/Ghi chú </label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group">
                                <label>Thông tin chi tiết </label>
                                <CKEditor:CKEditorControl ID="txtSummary" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>



                            <!--end form-group-->

                            <div class="form-group">
                                <%-- <label>Kích hoạt</label>--%>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" />
                                    <label for="ckActive" class="custom-control-label">
                                        KÍCH HOẠT
                                    </label>
                                </div>
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
                    </div>
                    <!--end card-->
                </div>

            </div>

        </div>

    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <script src="../../js/Function.js"></script>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(".select2").select2({
                width: '100%'
            });
            $(".select2-multiple").select2({
                width: '100%'
            });
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });
        }
        $(document).ready(function () {
            Init();
            $('#txtDateStart').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: true,
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {

            });
        });
    </script>

    <script src="/theme/plugins/select2/select2.min.js"></script>

    <!-- Parsley js -->
    <script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="../../theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <%--<script src="../../theme/plugins/select2/select2.min.js"></script>--%>
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-touchspin/js/jquery.bootstrap-touchspin.min.js"></script>
    <%--<script src="../../theme/assets/pages/jquery.forms-advanced.js"></script>--%>

    <!--Wysiwig js-->
    <script src="../../theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="../../theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="../../theme/assets/js/jquery.core.js"></script>
    <script>
        $(document).ready(function () {

            $("#myTable").on('input', '#txtQuantity', function () {
                var calculated_total_sum = 0;
                $("#myTable tr").each(function () {
                    var get_Quantity_value = $(this).find('#txtQuantity').val();
                    var get_Price_value = $(this).find('#txtPrice').val();
                    var Set_TotalPrice_value = $(this).find('#txtTotalPrice');

                    if ($.isNumeric(get_Quantity_value)) {
                        calculated_total_sum = parseInt(get_Quantity_value) * parseInt(get_Price_value.replaceAll(',', ''));

                        Set_TotalPrice_value.val(formatter.format(calculated_total_sum));
                    }
                });


            });
            $("#myTable").on('input', '#txtPrice', function () {
                var calculated_total_sum = 0;
                $("#myTable tr").each(function () {
                    var get_Quantity_value = $(this).find('#txtQuantity').val();
                    var get_Price_value = $(this).find('#txtPrice').val();
                    var Set_TotalPrice_value = $(this).find('#txtTotalPrice');
                    if ($.isNumeric(get_Quantity_value)) {
                        calculated_total_sum = parseInt(get_Quantity_value) * parseInt(get_Price_value.replaceAll(',', ''));
                        Set_TotalPrice_value.val(formatter.format(calculated_total_sum));
                    }
                });


            });

        });
        var formatter = new Intl.NumberFormat('it-IT', {
            style: 'currency',
            currency: 'VND',

            // These options are needed to round to whole numbers if that's what you want.
            //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
            //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
        });
    </script>
</asp:Content>

