<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeProductPackage.aspx.cs" Inherits="QRCodeProductPackage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng</title>
    <link rel="shortcut icon" href="/favicons/logo.ico">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"
        integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet">
    <link rel="stylesheet" href="/TruyXuatSRC/css/style.css?v=1" />
    <style>
        .custom-btn {
            padding: 5px 30px;
            text-align: center;
            border-radius: 7px;
            text-transform: uppercase;
            font-weight: bold;
            cursor: pointer;
        }

        span.none {
            display: none;
        }
    </style>
</head>
<body>
    <form runat="server" id="Form1">
        <div class="first-alert d-flex align-items-center">
            <img class="alert-logo" src="/images/logo-login.png?v=2" />
            <p class="text2 m-0">
                Bạn đang truy xuất nguồn gốc trên "Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng của CheckVN"<%-- hy.check.net.vn--%>
            </p>
            <img class="alert-close" src="/TruyXuatSRC/img/cancel.png" />
        </div>
        <div class="container-fluid">
            <div class="row" id="divBtn">
                <div class="col-12">
                    <asp:Repeater runat="server" ID="rptProductPackage">
                        <ItemTemplate>
                            <b>&diams; Thông tin D/N (Who):</b><br />

                            - <%#Eval("ProductBrandName") %>
                            <br />
                            - Nhà máy:  <%#Eval("ZoneName") %>
                            <%#Eval("Type").ToString()=="2"?"<br />- Hộ sản xuất: "+Eval("HoSX"):"" %>
                            <br />
                            <b>&diams; Đối tượng truy xuất (What)</b>
                            <br />
                            - Tên sản phẩm: <a href="javascript:void(0);" class="text-success"><b><%#Eval("Pname") %> (<%#Eval("ItemCount") %> cá thể)</b></a>
                            <%--<a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString().ToUpper()%></b></a>--%>
                            <br />
                            - Lô/mẻ: <a class="text-success" href='javascript:void(0);'><b><%#Eval("CODE")%> | <%#Eval("SGTIN") %></b></a>
                            <br />
                            - Tiêu chuẩn: <%# string.IsNullOrEmpty( Eval("QualityName").ToString())?"Tự công bố":Eval("QualityName") %>
                            <br />
                            <b>&diams; Nơi sản xuất (Where): </b>
                            <br />
                            <%#string.IsNullOrEmpty(Eval("Address").ToString())?Eval("ProductBrandName"):Eval("Address") %>

                            <br />
                            <b>&diams; Thời gian sản xuất (When): </b>
                            <br />
                            Từ <%#  string.IsNullOrEmpty( Eval("StartDate").ToString())?"": DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString())%> đến <%#  string.IsNullOrEmpty( Eval("EndDate").ToString())?"": DateTime.Parse( Eval("EndDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString())%>
                            <br />
                            <b>&diams; Lý do (Why): </b><%# string.IsNullOrEmpty(Eval("TenLenh").ToString())?"Lệnh sản xuất": "<span class='badge badge-danger'>"+ Eval("TenLenh").ToString()+"</span>"%>
                            <br />
                            Trạng thái: <%#ReturnStatus( Eval("TrangThai").ToString()) %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-12">
                    <br />
                    <div class="btn-primary custom-btn btn-sx" id="btn-sx">
                        Xem nhật ký sản xuất
                    </div>
                    <br />
                    <div class="btn-primary custom-btn" id="btn-update">
                        Cập nhật nhật ký sản xuất
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="content-history">
                    <div class="col-12 mt-2">
                        <h5>Lô: <%=info %></h5>
                        <h5 runat="server" id="xuanhoa">Mã PO:<%=PO%></h5>
                    </div>
                    <div class="detail">
                        <div class="title">
                            Nhật ký sản xuất
                        </div>
                        <asp:Repeater runat="server" ID="rptSX" OnItemDataBound="rptSX_ItemDataBound">
                            <ItemTemplate>
                                <table style='width: 100%;'>
                                    <tbody>
                                        <tr>
                                            <td class='col-left'>Đề mục công việc: </td>
                                            <td><b><%#Eval("Name") %></b></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Thời gian: </td>
                                            <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                        </tr>

                                        <tr>
                                            <td class='col-left'>Vị trí: </td>
                                            <td><%#Eval("Location") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %>
                                                <asp:Literal runat="server" ID="lblTaskStep_ID" Text='<%#Eval("TaskStep_ID") %>' Visible="false"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblTask_ID" Text='<%#Eval("Task_ID") %>' Visible="false"></asp:Literal>
                                                <div style="display: none"><%#NoQuestion=1 %>  <%=NoQuestion=1 %></div>
                                                <asp:Repeater runat="server" ID="rptQuestion" OnItemDataBound="rptQuestion_ItemDataBound">
                                                    <ItemTemplate>
                                                        <b><%=NoQuestion++ %>. <%#Eval("Name") %>
                                                            <asp:Literal runat="server" ID="lblQuestionID" Text='<%#Eval("TaskStepQuestion_ID") %>' Visible="false"></asp:Literal>
                                                        </b>
                                                        <br />
                                                        <asp:CheckBoxList runat="server" ID="ckAnswer">
                                                        </asp:CheckBoxList>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <br />
                                                <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="title">
                            Nhật ký vật tư
                        </div>
                        <asp:Repeater runat="server" ID="rptVT">
                            <ItemTemplate>
                                <table style='width: 100%;'>
                                    <tbody>
                                        <tr>
                                            <td class='col-left'>Tên vật tư: </td>
                                            <td><b><%#Eval("Name") %></b></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Số lượng: </td>
                                            <td><%#decimal.Parse( Eval("Quantity").ToString()).ToString("N0") %> <%#Eval("Unit") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Thời gian: </td>
                                            <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Vị trí: </td>

                                            <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %>
                                                <br />
                                                <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %></td>

                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="title">
                            Nhật ký thu hoạch
                        </div>
                        <asp:Repeater runat="server" ID="rptTaskHistoryTH">
                            <ItemTemplate>
                                <table style='width: 100%;'>
                                    <tbody>
                                        <tr>
                                            <td class='col-left'>Đầu mục công việc: </td>
                                            <td><b><%#Eval("Name") %></b></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Thời gian: </td>
                                            <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Số lượng: </td>
                                            <td><%#decimal.Parse(Eval("HarvestVolume").ToString()).ToString("N0")%><%#Eval("Unit") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %>
                                                <br />
                                                <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %></td>

                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="title">
                            Nhật ký sơ chế chế biến
                        </div>
                        <asp:Repeater runat="server" ID="rptTaskHistoryCB">
                            <ItemTemplate>
                                <table style='width: 100%;'>
                                    <tbody>
                                        <tr>
                                            <td class='col-left'>Đề mục công việc: </td>
                                            <td><b><%#Eval("Name") %></b></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Thời gian: </td>
                                            <td><%# string.IsNullOrEmpty(Eval("StartDate").ToString())?"":DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") %></td>
                                        </tr>
                                        <%--    <tr>
                                        <td class='col-left'>Nội dung: </td>
                                        <td></td>
                                    </tr>--%>
                                        <tr>
                                            <td class='col-left'>Vị trí: </td>
                                            <td><i class="dripicons-location font-14"></i><%#Eval("Location") %></td>
                                        </tr>
                                        <tr>
                                            <td class='col-left'>Nội dung: </td>
                                            <td><%#Eval("Description") %>
                                                <br />
                                                <%# string.IsNullOrEmpty(Eval("Image").ToString())?"":"<img src='../../data/task/"+Eval("Image")+"'/>" %></td>

                                        </tr>
                                    </tbody>
                                </table>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label runat="server" ID="lblCode" Style="display: none;" ClientIDMode="Static"></asp:Label>
        <script src="/theme/assets/js/jquery.min.js"></script>
        <script src="/theme/assets/js/jquery-ui.min.js"></script>
        <!-- Required datatable js -->
        <script src="/theme/plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="/theme/plugins/datatables/dataTables.bootstrap4.min.js"></script>
        <!-- Responsive examples -->
        <script src="/theme/plugins/datatables/dataTables.responsive.min.js"></script>
        <script src="/theme/assets/pages/jquery.datatable.init.js"></script>
        <script>
            $("#btn-sx").click(function () {
                $(".content-history").show();
                $("#divBtn").hide();
            });
            $("#btn-update").click(function () {
                if (($("#lblCode").html()).length > 0) {
                    window.location.replace("/?ReturnUrl=" + $("#lblCode").html());
                }
            });
        </script>
    </form>
</body>
</html>
