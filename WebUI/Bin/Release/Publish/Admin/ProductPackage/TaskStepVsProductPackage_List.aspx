<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="TaskStepVsProductPackage_List, App_Web_quwo0bpb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
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
                                <li class="breadcrumb-item active"><a>Cài đặt thời gian đề mục công việc</a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=namepackage %></a></li>
                                <li class="breadcrumb-item "><a href="ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Cài đặt thời gian đề mục công việc: Lô <%=namepackage %></h4>
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
                                <div class="col-md-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnBackFix1" OnClick="btnBackFix_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="IDUPDATE" visible="false">
                                <div class="row">

                                    <div class="col-md-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Tên đề mục</label>
                                            <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập tên đề mục"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <div class="form-group">
                                            <label>Ngày bắt đầu</label>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtStart" ClientIDMode="Static" CssClass="form-control" name="birthday" required />
                                                <div class="input-group-append">
                                                    <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <div class="form-group">
                                            <label>Ngày kết thúc</label>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtEnd" Text="" ClientIDMode="Static" CssClass="form-control" name="birthday" required />
                                                <div class="input-group-append">
                                                    <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-0">
                                    <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Cập nhật thời gian" />
                                    <%--<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>--%>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                </div>
                                <!--end form-group-->
                            </div>

                            <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static" CssClass="msg-sc-edit" Visible="false"></asp:Label>
                                    <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Tên đề mục</th>
                                                <th>Thời gian bắt đầu</th>
                                                <th>Thời gian Kết thúc</th>
                                                <th width="8%">Sửa thời gian</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptTaskStep">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("Name")%></td>
                                                        <td><%#string.IsNullOrEmpty(Eval("StartTime").ToString())?"":DateTime.Parse(Eval("StartTime").ToString()).ToString("dd/MM/yyyy")%></td>
                                                        <td><%#string.IsNullOrEmpty(Eval("EndTime").ToString())?"":DateTime.Parse(Eval("EndTime").ToString()).ToString("dd/MM/yyyy")%></td>
                                                        <%--    <td><%#GetStartTime(ProductPackage_ID,Convert.ToInt32( Eval("TaskStep_ID").ToString())) %></td>
                                                        <td><%#GetEndTime(ProductPackage_ID,Convert.ToInt32( Eval("TaskStep_ID").ToString())) %></td>--%>
                                                        <td align="center">
                                                            <a href='TaskStepVsProductPackage_List?ProductPackage_ID=<%=ProductPackage_ID %>&Product_ID=<%=Product_ID %>&TaskStep_ID=<%#Eval("TaskStep_ID") %>' class="mr-2" title="Sửa thông tin"><i class="fas fa-edit text-success font-16"></i></a>
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
        <asp:HiddenField runat="server" ID="YearStart" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="MonthStart" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="DayStart" ClientIDMode="Static" />

        <asp:HiddenField runat="server" ID="YearEnd" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="MonthEnd" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="DayEnd" ClientIDMode="Static" />
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
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <!-- Responsive examples -->
    <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="/theme/plugins/datatables/responsive.bootstrap4.min.js"></script>
    <script src="/theme/assets/pages/jquery.datatable.init.js"></script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            var YearStart = $("#YearStart").val();
            var MonthStart = $("#MonthStart").val();
            var DayStart = $("#DayStart").val();

            var YearEnd = $("#YearEnd").val();
            var MonthEnd = $("#MonthEnd").val();
            var DayEnd = $("#DayEnd").val();
            $('#txtStart').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                minDate: new Date(YearStart, MonthStart - 1, DayStart),
                maxDate: new Date(YearEnd, MonthEnd - 1, DayEnd),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                $('#txtStart').val(start.format('DD/MM/YYYY'));
            });
            $('#txtEnd').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                minDate: new Date(YearStart, MonthStart - 1, DayStart),
                maxDate: new Date(YearEnd, MonthEnd - 1, DayEnd),
                locale: {
                    format: 'DD/MM/YYYY',
                },
            }, function (start, end, label) {
                $('#txtEnd').val(start.format('DD/MM/YYYY'));
            });
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 2000);
                });
            }
        });
    </script>

</asp:Content>
