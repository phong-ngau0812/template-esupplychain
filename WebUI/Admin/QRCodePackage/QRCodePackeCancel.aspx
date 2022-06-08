<%@ Page Title="" Language="C#" MasterPageFile="~/Template/CMS.master" AutoEventWireup="true" CodeFile="QRCodePackeCancel.aspx.cs" Inherits="QRCodePackeCancel" ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/telerik.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="src"></telerik:RadScriptManager>
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active">Hủy lô tem: <%=NameProductPackageQR %> </li>
                                <li class="breadcrumb-item"><a href="QRCodePackage_List">Quản lý lô mã </a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Hủy lô tem: <%=NameProductPackageQR %> </h4>
                        <h6>Sản phẩm: <%=NameProductQR%> </h6>
                        <h6>Tổng sô <%= TotalTem%> tem. Serial từ <%=SerialStart%> đến <% =SerialEnd %> </h6>
                        <%--<h6 >Số tem đã hủy trong khoảng <%= SerialCancel%> </h6>--%>

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

                            <div class="form-group row none">
                                <label class="col-lg-3 col-sm-12">Phương pháp hủy </label>
                                <div class="radio radio-primary col-lg-7 col-sm-12">
                                    <div class="col-lg-3 col-sm-12 " style="float: left">
                                        <asp:RadioButton runat="server" ID="rdoSerial" Text="Hủy theo Serial" GroupName="rdo" AutoPostBack="true" OnCheckedChanged="rdoSerial_CheckedChanged" Checked="true" />&nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="col-lg-3 col-sm-12" style="float: left">
                                        <asp:RadioButton runat="server" ID="rdoKhoangSerial" Text="Hủy theo Serial liền mạch" AutoPostBack="true" OnCheckedChanged="rdoKhoangSerial_CheckedChanged" GroupName="rdo" />
                                    </div>

                                </div>
                            </div>
                            <div class="form-group row none">
                                <label class="col-lg-3 col-sm-12">số Serial <span class="red">*</span> </label>
                                <div class=" col-lg-7 col-sm-12" style="width: 100%">
                                    <asp:TextBox runat="server" ID="txtSeria" ClientIDMode="Static" CssClass="col-lg-12 col-sm-12 form-control "></asp:TextBox>
                                </div>

                            </div>


                            <div class="form-group row ">
                                <label class="col-lg-3 col-sm-12">Serial (xóa 1 tem Serial đầu cuối giống nhau) <span class="red">*</span></label>
                                <div class="col-lg-7 right">
                                    <div class="row">
                                        <div class="col-lg-1 col-sm-12 "><small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Từ</small></div>
                                        <div class="col-lg-4 col-sm-12">
                                            <asp:TextBox runat="server" ID="txtSerialStart" onkeyup="QRCodeCountByRange()" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-12"><small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Đến</small></div>
                                        <div class="col-lg-4 col-sm-12">
                                            <asp:TextBox runat="server" ID="txtSerialEnd" onkeyup="QRCodeCountByRange()" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-12 none">
                                            <small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Tổng số: <span runat="server" id="Number">0</span> tem</small>
                                        </div>
                                    </div>
                                    <%--<asp:LinkButton runat="server" ID="btnAll" OnClick="btnAll_Click" Style="margin-top: 10px; float: left;">Lấy tất cả tem của lô</asp:LinkButton>--%>
                                </div>

                            </div>
                            <div class="form-group row mb-0">
                                <span class="col-lg-3 col-sm-12"></span>
                                <div class="col-lg-7 col-sm-12">
                                    <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                    <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
            </div>
        </div>
        <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        <asp:HiddenField runat="server" ID="HDQRCodePackageSource_ID" ClientIDMode="Static" Value="0" />
        <asp:HiddenField runat="server" ID="Count" ClientIDMode="Static" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Parsley js -->
    <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>
    <!----date---->
    <script src="/theme/plugins/select2/select2.min.js"></script>
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
    <script>
        function QRCodeCountByRange() {
            $.ajax({
                url: "/WebServices/QRCodePackage.asmx/QRCodeCountByRange",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: {
                    QRCodePackage_ID: parseInt($("#HDQRCodePackageSource_ID").val()), SerialNumberStart: "'" + $("#txtSerialStart").val() + "'", SerialNumberEnd: "'" + $("#txtSerialEnd").val() + "'"
                },
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        var data = $.parseJSON(result.d);
                        //console.log(data.length);
                        if (data.length > 0) {
                            console.log(data);
                            $.each(data, function (index, item) {
                                $("#Number").html(item.Number);
                            });
                        }
                    }
                },
                error: function (errormessage) {
                    //alert(errormessage.responseText);
                    // window.showToast("err", "Có lỗi xảy ra, vui lòng thử lại sau.!", 3000);
                }
            });


        }
    </script>
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            $("#check").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(".select2").select2({
                width: '100%'
            });
        }

        $(function () {
            $(".formatMoney").keyup(function (e) {
                $(this).val(format($(this).val()));
            });
        });

        $(document).ready(function () {
            Init();
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

        $('#txtHSD').daterangepicker({
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
    </script>
</asp:Content>

