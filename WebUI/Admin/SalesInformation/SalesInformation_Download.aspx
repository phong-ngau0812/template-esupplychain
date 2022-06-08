<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="SalesInformation_Download.aspx.cs" Inherits="SalesInformation_Download" %>

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

        #PrintFile p {
            padding: 0px;
            margin: 0px;
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
                                <li class="breadcrumb-item"><a href="SalesInformation_List.aspx">Quản lý bán lẻ </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Xuất hóa đơn bán lẻ </h4>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>
            <!-- end page title end breadcrumb -->
            <div class="row">
                <div class="col-6 offset-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-12  col-xs-12 mb-4 right">
                                    <a onclick="printDiv('PrintFile')"><span class="btn btn-gradient-secondary">
                                        <i class="fa fa-print" style="color: white"></i>&nbsp;  In phiếu
                                    </span></a>

                                    <a href="SalesInformation_List.aspx"><span class="btn btn-gradient-danger">Quay lại
                                    </span></a>

                                </div>
                            </div>

                            <div id="PrintFile">
                                <div class="row">
                                    <div class="col-lg-12 col-xs-12 mb-1 text-center">
                                        <h6 style="margin:2px;">
                                            <asp:Literal runat="server" ID="lblCompany"></asp:Literal></h6>
                                        <p>
                                            <asp:Literal runat="server" ID="lblAddress"></asp:Literal></p>
                                        <p>ĐT:
                                            <asp:Literal runat="server" ID="lblPhone"></asp:Literal>
                                        </p>
                                        <h4><b>HÓA ĐƠN BÁN LẺ</b></h4>
                                        <p>Số HĐ:
                                            <asp:Literal runat="server" ID="lblSoHoaDon"></asp:Literal></p>
                                        <hr />
                                    </div>

                                    <div class="col-lg-12 col-xs-12 mb-3">
                                        <p>Ngày mua:
                                            <asp:Literal runat="server" ID="lblDate"></asp:Literal></p>
                                        <p>NV bán hàng:
                                            <asp:Literal runat="server" ID="lblNV"></asp:Literal></p>
                                        <p>Người mua:
                                            <asp:Literal runat="server" ID="lblKhachHang"></asp:Literal></p>
                                    </div>
                                    <div class="col-lg-12 col-xs-12 mb-3">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>STT </th>
                                                    <th>Tên hàng</th>
                                                    <th>ĐVT </th>
                                                    <th>SL</th>
                                                    <th>ĐG</th>
                                                    <th class="text-right">Thành tiền </th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater runat="server" ID="rptProduct" OnItemDataBound="rptProduct_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>1</td>
                                                            <td><%#Eval("Name") %>
                                                                <asp:Literal runat="server" ID="lblPrice" Text='<%#Eval("Price") %>' Visible="false"></asp:Literal>
                                                                <asp:Literal runat="server" ID="lblQuantity" Text='<%#Eval("Quantity") %>' Visible="false"></asp:Literal>
                                                            </td>
                                                            <td><%#Eval("Unit") %></td>
                                                            <td><%#Eval("Quantity") %></td>
                                                            <td><%#decimal.Parse( Eval("Price").ToString()).ToString("N0") %></td>
                                                            <td align="right"><%# decimal.Parse( (decimal.Parse(Eval("Quantity").ToString())*decimal.Parse(Eval("Price").ToString())).ToString()).ToString("N0") %> đ</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="col-lg-12 col-xs-12 mb-1">
                                        <table class="table">
                                            <tr>
                                                <td>
                                                    <p>Mã NV:
                                                        <asp:Literal runat="server" ID="lblMaNV"></asp:Literal></p>
                                                </td>
                                                <td>
                                                    <p>Chiết khấu:</p>
                                                    <p>Tổng TT:</p>
                                                    <%--<p>Tổng SL:</p>--%>
                                                </td>
                                                <td class="text-right">
                                                    <p style="font-weight:bold;"><asp:Literal runat="server" ID="Discount"></asp:Literal> %</p>
                                                    <p style="font-weight:bold;"><%=total.ToString("N0") %> đ</p>
                                                    <%--<p>1</p>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <p style="font-weight:bold;">Viết bằng chữ: <%=Common.DocTienBangChu(Convert.ToInt64( total)," đồng") %></p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-lg-12 col-xs-12 text-center">
                                        <i>Cảm ơn quý khách đã mua hàng</i>
                                        <br />
                                        <i>Rất mong được sự góp ý về phương thức phục vụ, chất lượng, giá cả của chúng tôi!!!</i>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
        </div>
        <asp:HiddenField runat="server" ID="lblDiscount"/>
        <!-- container -->
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
          /*  printDiv('PrintFile');*/
        });
        
        function printDiv(divName) {
            // khi nhập gán giá trị ô text box luôn  
            //$('#txtName').attr("value", $("#txtName").val());
            //$('#txtAddress').attr("value", $("#txtAddress").val());
            //$('#txtNote').attr("value", $("#txtNote").val());

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
