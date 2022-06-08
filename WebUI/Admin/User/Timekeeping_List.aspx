<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="Timekeeping_List.aspx.cs" Inherits="Admin_User_Timekeeping_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />
    <!-- qrcode-reader core CSS file -->
    <link rel="stylesheet" href="css/qrcode-reader.min.css">
    <link rel="stylesheet" href="https://cdn.staticaly.com/gh/mauntrelio/qrcode-reader/master/dist/css/qrcode-reader.min.css?env=dev">
    <!-- jQuery -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>

    <!-- qrcode-reader core JS file -->
    <script src="js/qrcode-reader.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Quản lý chấm công</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Quản lý chấm công</h4>
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

                            <div class="row" id="qrcode1">
                                <label for="single">Single input (rebound click, depending on target input's content):</label>
                                <input id="single2" type="text" size="50">
                                <button type="button" id="openreader-single2"
                                    data-qrr-target="#single2"
                                    data-qrr-audio-feedback="true">
                                    Read or follow QRCode</button>
                                <br>
                                <br>
                            </div>
                        </div>
                    </div>
                    <!-- end col -->
                </div>
            </div>
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" id="btnKeeping" style="display: none;" data-toggle="modal" data-target="#exampleModal">
                Launch demo modal
            </button>

            <!-- Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Thành công
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- container -->

            <!--  Modal content for the above example -->
            <asp:HiddenField ID="QRCodeContent" runat="server" ClientIDMode="Static" Value="0" />
    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdn.staticaly.com/gh/mauntrelio/qrcode-reader/master/dist/js/qrcode-reader.min.js?env=dev"></script>

    <script>

        $(function () {

            // overriding path of JS script and audio 
            $.qrCodeReader.jsQRpath = "https://cdn.staticaly.com/gh/mauntrelio/qrcode-reader/master/dist/js/jsQR/jsQR.min.js";
            $.qrCodeReader.beepPath = "https://cdn.staticaly.com/gh/mauntrelio/qrcode-reader/master/dist/audio/beep.mp3";
            // read or follow qrcode depending on the content of the target input
            $("#openreader-single2").qrCodeReader({
                callback: function (code) {
                    if (code) {
                        document.getElementById('QRCodeContent').value = code;
                        getGeo();
                    }
                }
            }).off("click.qrCodeReader").on("click", function () {
                var qrcode = $("#single2").val().trim();
                if (qrcode) {
                    /* $("#QRCodeContent").innerHTML = qrcode;*/
                    document.getElementById('QRCodeContent').value = $("#single2").val().trim();
                    getGeo();
                } else {
                    $.qrCodeReader.instance.open.call(this);
                }
            });


        });
        var getGeo = function () {
            navigator.geolocation.getCurrentPosition(success, error, { maximumAge: 600000, timeout: 10000 });
        }

        function success(position) {
            var lng = position.coords.longitude;
            var lat = position.coords.latitude;
            $.ajax({
                url: "/WebServices/GetLocation.asmx/GetAddressLocation",
                data: {
                    Latitude: "'" + lat + "'",
                    Longitude: "'" + lng + "'"
                },
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != "") {
                        console.log(result.d);
                        var data = {
                            Address: result.d,
                            QRCode: $("#QRCodeContent").val()
                        };
                        console.log(data);
                        $.ajax({
                            url: "/WebServices/GetLocation.asmx/Timekeeping1",
                            data: JSON.stringify(data),
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                setTimeout(function () { $("#spinner").hide(); }, 400);
                                if (result.d != "0") {
                                    $("#qrcode1").css("display", "none");

                                    $("#btnKeeping").click();

                                    console.log('ok')
                                } else {
                                    console.log('fail');
                                }
                            },
                            error: function (errormessage) {
                                console.log('fail');
                            }
                        });
                    }
                },
                error: function (errormessage) {
                    console.log('fail');
                }
            });
        }

        var constant;
        function error(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    constant = "Hệ thống không được quyền truy cập GPS";
                    break;
                case error.POSITION_UNAVAILABLE:
                    constant = "Hệ thống không định vị được";
                    break;
                case error.TIMEOUT:
                    constant = "Hết thời gian chờ";
                    break;
                default:
                    constant = "Lỗi truy cập GPS";
                    break;
            }
            // alert("Mã lỗi: " + error.code + "\nLý do: " + constant + "\nMessage: " + error.message);
        }
    </script>
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
            $(".select2").select2({
                width: '100%'
            });
        })
        $(document).ready(function () {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
            if (typeof (Sys) !== 'undefined') {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
                    $(".select2").select2({
                        width: '100%'
                    });
                });
            }
        });
    </script>

</asp:Content>
