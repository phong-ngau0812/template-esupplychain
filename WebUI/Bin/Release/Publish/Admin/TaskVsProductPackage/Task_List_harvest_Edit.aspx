<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="Task_List_harvest_Edit, App_Web_pmdaiu5p" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/UserControl/ctlVoice.ascx" TagName="ctlVoice" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <!-- DataTables -->
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Nhật ký bán hàng </a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx?Code=<%=code %>">Lô <%=name %></a></li>
                                <li class="breadcrumb-item"><a href="../ProductPackage/ProductPackage_List.aspx">Quản lý lô</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Thêm mới nhật ký <%=titleXuanHoa %>  - Lô <%=name %></h4>
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
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" runat="server" id="ProductBrand">
                                        <label>Doanh nghiệp</label>
                                        <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" runat="server" id="ProductPackage">
                                        <label>Lô sản xuất</label>
                                        <asp:DropDownList runat="server" ID="ddlProductPackage" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Chọn kho <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlWarehouse" CssClass="form-control select2" required data-parsley-required-message="Bạn chưa chọn kho"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Nội dung <%=titleXuanHoa %> </label>
                                        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Số lượng (sản phẩm <%=titleXuanHoa %>) <span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtHarvestVolume" ClientIDMode="Static" CssClass="form-control formatMoney" required data-parsley-required-message="Bạn chưa nhập số lượng "></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Đơn vị</label>
                                        <asp:TextBox runat="server" ID="txtUnit" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Rủi ro <%=titleXuanHoa %> </label>
                                        <asp:TextBox runat="server" ID="txtRisk" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>


                                    <div class="form-group">
                                        <label>Ngày <%=titleXuanHoa %> <span class="red">*</span></label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtStart" ClientIDMode="Static" CssClass="form-control" name="birthday" required data-parsley-required-message="Bạn chưa nhập ngày bán " />
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label>Số ngày thu hoạch còn lại</label>
                                        <asp:TextBox runat="server" ID="txtHarvestDayRemain" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                    </div>
                                    <asp:Panel ID="xuanhoa" runat="server">
                                        <div class="form-group">
                                            <label>Người nhập kho</label>
                                            <asp:DropDownList runat="server" ID="ddlWorkshopImport" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Thủ kho nhận</label>
                                            <asp:DropDownList runat="server" ID="ddlThukho" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Mô tả nhật ký công việc</label>
                                        <uc1:ctlVoice ID="ctlVoice1" runat="server" />
                                        <CKEditor:CKEditorControl ID="txtDes" BasePath="/ckeditor/" runat="server" ClientIDMode="Static">
                                        </CKEditor:CKEditorControl>
                                    </div>
                                    <div class="form-group">
                                        <label>Ảnh minh họa</label>
                                        <br />

                                        <div style="margin: 5px 0px;">
                                            <a href="<%=avatar %>" target="_blank">
                                                <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                            </a>
                                        </div>

                                        <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                                    </div>


                                    <div class="form-group">
                                        <label>Vị trí hiện tại</label>
                                        <asp:TextBox runat="server" ID="txtLocation" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Thời tiết</label>
                                        <asp:TextBox runat="server" ID="txtWeather" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Trạng thái công việc</label>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control select2"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Nhận xét của cơ quan/ đơn vị giám sát</label>
                                        <asp:TextBox runat="server" ID="txtComment" ClientIDMode="Static" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        <em style="color: red;">(*) Nhấn lưu để gửi phản hồi</em>
                                    </div>
                                    <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <%--<asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server" ID="up">
                                        <ContentTemplate>
                                            <h5 class="mt-0">Lịch sử cập nhật nhật ký</h5>
                                            <hr />
                                       
                                            <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th width="45%">Lô</th>
                                                        <th width="15%">Số lượng</th>
                                                        <th width="25%">Ngày thu hoạch</th>
                                                        <th width="15%">Trạng thái</th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater runat="server" ID="rptTask">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><a href='Task_List_harvest_Edit?Task_ID=<%#Eval("Task_ID") %>&ProductPackage_ID=<%=ProductPackage_ID %>' class="breadcrumb-item active font-15"><b><%#Eval("Name") %></b></a>
                                                                    <br />
                                                                    Lô : <b><%#Eval("ProductPackageName") %></b>

                                                                </td>

                                                                <td>

                                                                    <%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%>
																	
                                                                </td>

                                                                <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>

                                                                <td><%#Eval("StatusName") %>

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mb-0">
                                        <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                        <asp:Button ID="btnBackFix" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                </div>
                            </div>
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
    <script src="../../js/Function.js"></script>
    <script>
        function img() {
            var url = inputToURL(document.getElementById("<%=fulAnh.ClientID %>"));
            document.getElementById("<%=imganh.ClientID %>").src = url;
        }
        function inputToURL(inputElement) {
            var file = inputElement.files[0];
            return window.URL.createObjectURL(file);
        }
    </script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
        $(document).ready(function () {
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });
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
                    format: 'DD/MM/YYYY HH:mm:ss',
                    "daysOfWeek": [
                        "CN",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6",
                        "7",
                    ],
                    "monthNames": [
                        "Tháng 1",
                        "Tháng 2",
                        "Tháng 3",
                        "Tháng 4",
                        "Tháng 5",
                        "Tháng 6",
                        "Tháng 7",
                        "Tháng 8",
                        "Tháng 9",
                        "Tháng 10",
                        "Tháng 11",
                        "Tháng 12",
                    ],
                },
            }, function (start, end, label) {
                $('#txtStart').val(start.format('DD/MM/YYYY HH:mm:ss'));
            });
            $(".select2").select2({
                width: '100%'
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
