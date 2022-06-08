<%@ page title="" language="C#" autoeventwireup="false" inherits="LookUpInformationTask, App_Web_udcfgcto" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta charset="utf-8" />
    <title>Tra cứu thông tin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta content="Premium Multipurpose Admin & Dashboard Template" name="description" />
    <meta content="" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />


    <!-- App favicon -->
    <link rel="shortcut icon" href="../../images/logo1.png">

    <!-- App css -->
    <link href="../../theme/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/assets/css/jquery-ui.min.css" rel="stylesheet">
    <link href="../../theme/assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/assets/css/metisMenu.min.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/site.css" rel="stylesheet" type="text/css" />
    <style>
        .msg-sc-edit {
            color: #ffffff;
            background-color: #f3727a;
        }

        @media screen and (min-width: 1280px) {
            .table-responsive {
                display: table !important;
            }
        }

        @media screen and (max-width: 540px) {
            .card-body {
                padding: 0px !important;
            }
        }
    </style>
</head>
<body class="account-body accountbg">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row pt-5 ">
                <div class="col-12 align-self-center">
                    <div class="">
                        <div class="card auth-card shadow-lg">
                            <div class="card-body">
                                <div class="px-3">
                                    <div class="auth-logo-box ">
                                        <a href="../../theme/dashboard/analytics-index.html" class="logo logo-admin">
                                            <img src="/images/logo-login.png?v=2" height="55" alt="logo" class="auth-logo"></a>

                                    </div>

                                    <div class="text-center auth-logo-text mb-5 mt-5">
                                        <h4>Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng</h4>
                                    </div>

                                    <div class="form-group mb-1">
                                        <div class="row">

                                            <div class="col-lg-10 col-sm-12 mb-3 ">
                                                <asp:TextBox runat="server" ID="txtSearch" ClientIDMode="Static" Font-Size="Larger" Width="100%" placeholder="Nhập mã AI(10)/PO" class="alert alert-outline-purple " autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class=" col-lg-2 col-sm-12 ">
                                                <asp:Button CssClass=" btn btn-danger  dropdown-toggle  btn-gradient-primary alert " runat="server" ID="btnSearch" ClientIDMode="Static" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                                <a class="btn btn-gradient-danger  dropdown-toggle  alert" href="../../Login" role="button">Quay lại</a>
                                            </div>

                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="es">
                                        <div class="form-group">
                                            <h5><%=title %> </h5>
                                        </div>
                                        <div class="row" runat="server" id="Data" visible="false">
                                            <div class="col-12">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li class="nav-item" runat="server" id="NKSX" visible="false">
                                                                <%--aria-selected="true"--%>
                                                                <a class="nav-link" data-toggle="tab" href="#tab1" role="tab">Nhật ký sản xuất</a>
                                                            </li>
                                                            <li class="nav-item" runat="server" id="NKVT" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab2" role="tab" aria-selected="false">Nhật ký vật tư</a>
                                                            </li>
                                                            <li class="nav-item" runat="server" id="NKTH" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab3" role="tab" aria-selected="false"><%=NameNK %></a>
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

                                                        <!-- Tab panes -->
                                                        <div class="tab-content">
                                                            <div class="tab-pane mt-3 active" id="tab1" role="tabpanel">

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
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

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                                    <thead>

                                                                        <tr>
                                                                            <th>Tên vật tư</th>
                                                                            <th>Người sử dụng</th>
                                                                            <th>Ngày thực hiện</th>
                                                                            <th width="5%">Số lượng</th>
                                                                            <th width="5%" class="none">Đơn giá</th>

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
                                                                                    <td class="none"><%#decimal.Parse( Eval("Price").ToString()).ToString("N0") %></td>

                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>

                                                            </div>

                                                            <div class="tab-pane mt-3" id="tab3" role="tabpanel">

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
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

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                                    <thead>

                                                                        <tr>
                                                                            <th>Đầu mục công việc</th>
                                                                            <th>Người thực hiện</th>
                                                                            <th>Vị trí</th>
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

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
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

                                                                <table class="table table-responsive table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
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
                                                    <!--end card-body-->
                                                </div>
                                                <!--end card-->
                                            </div>
                                            <!--end col-->
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="xuanhoa">
                                        <asp:Label runat="server" ID="htmlXH"></asp:Label>
                                    </asp:Panel>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    </div>
                                    <!--end auth-logo-text-->
                                </div>
                                <!--end /div-->
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!--end auth-page-->
                </div>
                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <div id="spinner" class="spinner" style="display: none;">
            <p class="textload" style="color: #000">Hệ thống đang tải dữ liệu, vui lòng đợi !!!</p>
            <img src="/images/logo-login.png?v=2" class="img-load">
        </div>
        <!--end container-->
    </form>



</body>

<script src="../../theme/assets/js/jquery.min.js"></script>
<script src="../../theme/assets/js/jquery-ui.min.js"></script>
<script src="../../theme/assets/js/bootstrap.bundle.min.js"></script>
<script src="../../theme/assets/js/metismenu.min.js"></script>
<script src="../../theme/assets/js/waves.js"></script>
<script src="../../theme/assets/js/feather.min.js"></script>
<script src="../../theme/assets/js/jquery.slimscroll.min.js"></script>

<!-- App js -->
<script src="../../theme/assets/js/app.js"></script>
<!-- jQuery  -->

<script>

    $(document).ready(function () {
        //$("li a").first().addClass("active");
        $("li a").first().click();
        setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);

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


</html>
