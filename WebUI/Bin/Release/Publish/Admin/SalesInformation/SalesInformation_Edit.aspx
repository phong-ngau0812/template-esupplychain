<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="SalesInformation_Edit, App_Web_steskao1" validaterequest="false" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />


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
                                <li class="breadcrumb-item"><a href="SalesInformation_List.aspx">Quản lý thông tin bán hàng </a></li>
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
                                <div class="form-group">
                                    <label>Loại khách<span class=" red">*</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="select2 form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Khách lẻ"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Khách có trên hệ thống"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divCustomerLe">
                                    <label>Tên khách</label>
                                    <asp:TextBox runat="server" ID="txtCustomerName" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" runat="server" id="DivCustomer" visible="false">
                                    <label>Chọn khách<span class=" red">*</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="select2 form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <label>Chọn sản phẩm </label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlProduct" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn sản phẩm --" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:ListBox>
                                <div runat="server" id="tbl" visible="false">
                                    <br />
                                    <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Tên sản phẩm</th>
                                                <th width="100px">Số lượng</th>
                                                <th width="200px">Giá</th>
                                                <th width="10%">Đơn vị</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptMaterial">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("Name") %>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Enabled="false" runat="server" ID="txtQuantity" Text='<%# decimal.Parse( Eval("Quantity").ToString()).ToString("N0") %>' ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox Enabled="false" runat="server" ID="txtPrice" Text='<%# decimal.Parse( Eval("Price").ToString()).ToString("N0") %>' ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox Enabled="false" runat="server" ID="txtUnit" Text='<%#Eval("Unit") %>' ClientIDMode="Static" CssClass="form-control"></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>SGTIN  </label>
                                <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlSGTIN" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn SGTIN --"></asp:ListBox>
                                <%--<a runat="server" visible="false" data-toggle="modal" data-target=".bd-example-modal-xl" id="viewmore" class="breadcrumb-item active mt-3" href="javascript:void(0);"><i class="mdi mdi-more font-18"></i>Xem chi tiết lịch sử</a>--%>
                            </div>
                            <div class="form-group">
                                <label>Mã vạch <span class=" red">*</span></label>
                                <asp:TextBox runat="server" ID="txtCode" ClientIDMode="Static" CssClass="form-control" required data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa nhập mã vạch"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Chiết khấu (%)</label>
                                <asp:TextBox runat="server" ID="txtChietKhau" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Nhân viên <span class=" red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlWorkshop" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn nhân viên"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Ca bán hàng<span class=" red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlCa" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn ca bán hàng"></asp:DropDownList>
                            </div>

                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" Visible="false" ClientIDMode="Static" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5" OnClick="btnBack_Click"></asp:Button>
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

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            Init();
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });
        });

    </script>
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
    <script src="../../js/Function.js"></script>
</asp:Content>

