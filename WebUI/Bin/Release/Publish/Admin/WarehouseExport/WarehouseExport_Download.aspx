<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="WarehouseExport_Download, App_Web_ijujwaxe" %>

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

        .th {
            padding-bottom: 100px;
            text-align: center;
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
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="WarehouseExport_List.aspx">Quản lý xuất kho </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Phiếu xuất kho </h4>
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

                            <%-- <h4 class="mt-0 header-title">Buttons example</h4>--%>
                            <div class="row">

                                <div class="col-lg-12  col-xs-12 mb-4 right">
                                    <a onclick="printDiv('PrintFile')"><span class="btn btn-gradient-secondary">
                                        <i class="fa fa-print" style="color: white"></i>&nbsp;  In phiếu
                                    </span></a>

                                    <a href="WarehouseExport_List.aspx"><span class="btn btn-gradient-danger">Quay lại
                                    </span></a>

                                    <%--<a onclick="printDiv('PrintFile')" class="btn btn-gradient-secondary"><i class="fa fa-print" style ="color:white"> <span>In phiếu</span></i></a>--%>
                                    <%--<asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>--%>

                                    <%--<asp:Button CssClass="btn btn-gradient-primary marginBtnAdd " runat="server" ID="ImportExcel" Text="xuất file" OnClick="ImportExcel_Click" />--%>

                                    <%--<asp:Button CssClass="btn btn-gradient-primary" runat="server" ID="Button3" Text="Thêm mới sản phẩm" OnClick="btnAddSP_Click" />--%>
                                </div>


                            </div>

                            <div id="PrintFile">

                                <%--<table id="footable-1" class="table table-striped mb-0">

									<tbody>
										<tr>
											<th>
												<h4>Doanh nghiệp  <%=NameProductBrand %></h4>
												<p>Đại chỉ:<%=AddressProductBrand %>  </p>
											</th>
											<th style="text-align: center; width: 30%;">
												<h4>Mẫu số: 02-VT</h4>
												<p style="height: 7px">(Ban hành theo thông tư số 133/2016/TT-</p>
												<p>BTC &nbsp; Ngày 26/08/2016 của Bộ Tài chính) </p>
											</th>

										</tr>
									</tbody>
								</table>--%>
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12 mb-3">
                                        <h4><%=NameProductBrand %></h4>
                                        <p>Đại chỉ :<%=AddressProductBrand %>  </p>
                                    </div>
                                    <%--<div class="col-lg-3 col-xs-6 mb-3 center ">
										<h4>Mẫu số: 02-VT</h4>
										<p>(Ban hành theo thông tư số 133/2016/TT-</p>
										<p>BTC &nbsp; Ngày 26/08/2016 của Bộ Tài chính) </p>
									</div>--%>
                                </div>
                                <div class="row">
                                    <%--<div class="col-lg-4">
									</div>--%>
                                    <div class="col-lg-12 col-xs-12 mb-3" style="text-align: center">
                                        <h4>PHIẾU XUẤT KHO VẬT TƯ</h4>
                                        <label><%=ProductPackageOrder%> </label><br />
                                        <label>Ngày: <%=DateTime.Parse(DateTime.Now.ToString()).ToString("dd/MM/yyyy")%> </label><br />
                                        <label>Số phiếu: <%=sophieu%>  </label>
                                    </div>
                                    <%--<div class="col-lg-4">
									</div>--%>
                                </div>

                                <div class="row mb-5">
                                    <div class="col-lg-12">
                                        <div class="form-group row" style="margin-bottom: 0px; margin-bottom: 0rem">
                                            <label class="col-lg-2 col-form-label text-left">Họ và tên người nhận: </label>
                                            <div class="col-lg-10">
                                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="static" CssClass="form-control" Style="border: 0px solid #e8ebf3; border-bottom: 2.9px dotted #696b6f;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row" style="margin-bottom: 0px; margin-bottom: 0rem">
                                            <label class="col-lg-2  col-form-label text-left">Địa chỉ (bộ phận): </label>
                                            <div class="col-lg-10">
                                                <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" CssClass="form-control" Style="border: 0px solid #e8ebf3; border-bottom: 2.9px dotted #696b6f;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row" style="margin-bottom: 0px; margin-bottom: 0rem">
                                            <label class="col-lg-2  col-form-label text-left">Lý do xuất kho: </label>
                                            <div class="col-lg-10">
                                                <asp:TextBox runat="server" ID="txtNote" ClientIDMode="Static" CssClass="form-control" Style="border: 0px solid #e8ebf3; border-bottom: 2.9px dotted #696b6f;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                                <asp:UpdatePanel runat="server" ID="up">
                                    <ContentTemplate>
                                        <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                        <div class="table-rep-plugin">
                                            <div class="table-responsive mb-0 table-bordered" data-pattern="priority-columns">
                                                <table id="tech-companies-1 " class="table table-striped mb-0 table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>STT </th>
                                                            <th>Tên vật tư</th>
                                                            <th>Số lượng </th>
                                                            <th>Đơn giá </th>
                                                            <th>Thành tiền </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptMaterial" OnItemDataBound="rptMaterial_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%=No++ %></td>
                                                                    <td><%#Eval("Name")%></td>
                                                                    <td><%#Eval("Amount")%> (<%#Eval("Unit")%>)</td>
                                                                    <td><%#decimal.Parse(Eval("Price").ToString()).ToString("N0")%>
                                                                        <asp:Literal runat="server" ID="lblPrice" Text='<%#Eval("Price") %>' Visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lblAmount" Text='<%#Eval("Amount") %>' Visible="false"></asp:Literal>
                                                                    </td>
                                                                    <td><%#decimal.Parse(Eval("Total").ToString()).ToString("N0")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr class="table-warning">
                                                            <td colspan="4" class="text-right">Tổng</td>
                                                            <td><%=Total.ToString("N0") %></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="table-rep-plugin">
                                    <div class="table-responsive mb-4">
                                        <table class="table table-striped mb-4" style="border: none; text-align: center">
                                            <thead>
                                                <tr>
                                                    <th style="padding-bottom: 100px;">
                                                        <h5>Người lập phiếu </h5>
                                                        <label>(chữ ký)</label>
                                                    </th>
                                                    <th style="padding-bottom: 100px;">
                                                        <h5>Người nhận </h5>
                                                        <label>(chữ ký)</label>
                                                    </th>
                                                    <th style="padding-bottom: 100px;">
                                                        <h5>Thủ kho </h5>
                                                        <label>(chữ ký)</label></th>
                                                    <th style="padding-bottom: 100px;">
                                                        <h5>Kế toán trưởng </h5>
                                                        <label>(chữ ký)</label></th>
                                                    <th style="padding-bottom: 100px;">
                                                        <h5>Giám đốc </h5>
                                                        <label>(chữ ký)</label>
                                                    </th>

                                                </tr>
                                            </thead>
                                        </table>
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
        $(document).ready(function () {
            $("#txtgooglemap").keyup(function () {
                //alert("Handler for .change() called.");
                googlemap();
            });

        });

        function googlemap() {
            var googlemap = $("#txtgooglemap").val();
            if (googlemap.includes("<iframe")) {
                $("#EmbedGG").html(googlemap);
            }
        }
    </script>

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
        function printDiv(divName) {
            // khi nhập gán giá trị ô text box luôn  
            $('#txtName').attr("value", $("#txtName").val());
            $('#txtAddress').attr("value", $("#txtAddress").val());
            $('#txtNote').attr("value", $("#txtNote").val());

            var printContents = $("#" + divName).html();
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;;
        }

        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });

        });
    </script>
</asp:Content>
