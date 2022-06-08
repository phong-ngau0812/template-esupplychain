<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="QRCodePackage_Split, App_Web_knjlquph" validaterequest="false" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

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
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="QRCodePackage_List">QUẢN LÝ LÔ MÃ</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title"><%=title %></h4>
                        <h6>Tổng số <b><%=number %></b> tem từ <b class="red"><%=start %></b> đến <b class="red"><%=end %></b></h6>
                        <asp:Label runat="server" ID="lblMessage1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                    </div>
                    <!--end page-title-box-->
                </div>
                <!--end col-->
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <div class="card">
                        <div class="card-body">
                            <h4 style="text-align: center">Lịch sử chia lô mã </h4>
                            <hr />
                            <h6>Danh sách khoảng lô mã còn lại</h6>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="Label1" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    <table id="datatable1" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>STT </th>
                                                <th>Serial đầu</th>
                                                <th>Serial cuối</th>
                                                <th>Số tem</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="rptHistory">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%=No++ %></td>
                                                        <td><%#Eval("SerialNumberStart")%></td>
                                                        <td><%#Eval("SerialNumberEnd")%></td>
                                                        <td><%#Eval("QRCodeNumber")%></td>
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
                <div class="col-lg-6">
                    <%--    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>--%>
                    <div class="card">
                        <div class="card-body">
                            <h4 style="text-align: center">Thông tin chia lô mã</h4>
                            <hr />
                            <div class="form-group ">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        1. Tên lô mã <span class="red">*</span>
                                    </label>
                                    <div class="col-lg-9">
                                        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group ">
                                <div class="row">
                                    <label class="col-lg-3">Phương pháp chia </label>
                                    <div class="col-lg-9">

                                        <div class="radio radio-primary" style="width: 100%">
                                            <div class="custom-control custom-radio ">
                                                <asp:RadioButton runat="server" ID="rdoSoluong" AutoPostBack="true" Text="Chia theo số lượng" GroupName="rdo" Checked="true" />&nbsp;
                                            </div>
                                            <div class="custom-control custom-radio ">
                                                <asp:RadioButton runat="server" ID="rdoLienMach" AutoPostBack="true" Text="Chia theo chuỗi Serial liền mạch" GroupName="rdo" />&nbsp;
                                            </div>
                                            <div class="custom-control custom-radio">
                                                <asp:RadioButton runat="server" ID="rdoKhongLienMach" AutoPostBack="true" Text="Chia theo Serial không liền mạch" GroupName="rdo" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <div class="row">
                                    <label class="col-lg-3">2. Số lượng tem<span class="red">*</span></label>
                                    <div class="col-lg-9">
                                        <div class="row">
                                            <div class="col-lg-9">
                                                <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" CssClass="form-control formatMoney" required data-parsley-required-message="Bạn chưa nhập số lượng"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3"><small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Tối đa: <%= number %> tem</small></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group ">
                                <div class="row">
                                    <label class="col-lg-3">Serial</label>
                                    <div class="col-lg-9">
                                        <div class="row">
                                            <div class="col-lg-1"><small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Từ</small></div>
                                            <div class="col-lg-3">
                                                <asp:TextBox runat="server" ID="txtSerialStart" onkeyup="QRCodeCountByRange()" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-1"><small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Đến</small></div>
                                            <div class="col-lg-3">
                                                <asp:TextBox runat="server" ID="txtSerialEnd" onkeyup="QRCodeCountByRange()" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-4">
                                                <small style="float: left; margin-top: 10px; margin-bottom: -1rem;">Tổng số: <span id="Number">0</span> tem</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group ">
                                <div class="row">
                                    <label class="col-lg-3">Danh sách Serial(Mỗi dòng một Serial)</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox runat="server" ID="txtListSerial" TextMode="MultiLine" Rows="4" CssClass="form-control" onKeyUp="countLines(this)"> </asp:TextBox>
                                        <small>Tổng số Serial: <span id="linesUsed">0</span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        3. 
                                    </label>
                                    <div class="col-lg-9">
                                        <div class="custom-control custom-checkbox">
                                            <asp:CheckBox runat="server" ID="ckSound" ClientIDMode="Static" Checked="true" />
                                            <label for="ckSound" class="custom-control-label">
                                                Âm Thanh
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        Trạng thái
                                    </label>
                                    <div class="col-lg-9">
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Cấp độ mã định danh</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="radio radio-primary" style="width: 100%">
                                        <div class="custom-control custom-radio ">
                                            <asp:RadioButton runat="server" ID="rdoProductPackage" AutoPostBack="true" Text="Mã định danh lô" GroupName="rdo1" Checked="true" />&nbsp;
                                        </div>
                                        <div class="custom-control custom-radio">
                                            <asp:RadioButton runat="server" ID="rdoProduct" AutoPostBack="true" Text="Mã định danh sản phẩm" GroupName="rdo1" />&nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        4. Doanh nghiệp<span class="red">*</span>
                                    </label>
                                    <div class="col-lg-9">
                                        <asp:DropDownList runat="server" ID="ddlProductBrand" AutoPostBack="true" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" ClientIDMode="Static" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        5. Hộ sản xuất
                                    </label>
                                    <div class="col-lg-9">
                                        <asp:DropDownList runat="server" ID="ddlWorkshop" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkShop_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3 ">
                                        6. Lô sản xuất
                                    </label>
                                    <div class="col-lg-9">
                                        <asp:DropDownList runat="server" ID="ddlProductPackage" ClientIDMode="Static" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                             <div class="form-group row" runat="server" id="productPackage">
                                <div class="col-lg-3">
                                    <label>
                                       6. Lô sản xuất
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProductPackage"  ClientIDMode="Static" CssClass="select2 form-control"></asp:DropDownList>
                                    <br />
                                    <asp:Literal runat="server" ID="lblNote"></asp:Literal>
                                </div>
                            </div>
                            <div class="form-group row" runat="server" id="product" >
                                <div class="col-lg-3">
                                    <label>
                                     6. Sản phẩm
                                    </label>
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" ></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3">7. Khối lượng(g) </label>
                                    <div class="col-lg-9">
                                        <asp:TextBox runat="server" ID="txtKhoiLuong" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--  <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3">Ngày thu hoạch </label>
                                    <div class="col-lg-9">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtThuHoach" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-lg-3">8. Hạn sử dụng(ngày) </label>
                                    <div class="col-lg-9">
                                        <asp:TextBox runat="server" ID="txtHSD" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return Validate();" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>


                    <%--   </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
                <div class="col-lg-3 none">
                    <div class="card">
                        <div class="card-body">
                            <h4 style="text-align: center">Thông tin sản phẩm chia lô mã </h4>
                            <hr />
                            <%--   <div class="form-group">
                                <label>
                                    Danh mục sản phẩm
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProducCategory" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProducCategory_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    Sản phẩm<span class="red">*</span>
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn sản phẩm"></asp:DropDownList>
                            </div>--%>

                            <%--<div class="form-group">
                                        <label>
                                            Cửa Hàng 
                                        </label>
                                        <asp:DropDownList runat="server" ID="DropDownList3" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <label>Mã vạch sản phẩm </label>
                                        <asp:TextBox runat="server" ID="TextBox2" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group mb-0">
                                        <asp:Button runat="server" ID="Button1" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lấy thông tin sản phẩm" />
                                    </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="HDQRCodePackageSource_ID" ClientIDMode="Static" Value="0" />
        <asp:HiddenField runat="server" ID="ProductBrandList" />
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
    <script src="../../js/Function.js"></script>
    <script type="text/javascript">
        function countLines(theArea) {
            var text = $(theArea).val();
            var lines = text.split("\n");
            var count = 0;
            for (var i = 0; i < lines.length - 1; i++) {
                if (lines[i].trim() != "" && lines[i].trim() != null) {
                    count += 1;
                }
            }
            var linesUsedspan = document.getElementById('linesUsed');
            linesUsedspan.innerHTML = count;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function Validate() {
            var objProductBrand = document.getElementById("ddlProductBrand");
            var objPackage = document.getElementById("ddlProductPackage");

            if (objProductBrand.value == "") {
                alert("Vui lòng chọn doanh nghiệp!");
                objProductBrand.focus();
                return false;
            }
            if (objPackage.value == "") {
                alert("Vui lòng chọn lô!");
                objPackage.focus();
                return false;
            }
            $('#spinner').css('opacity', '0.8');
            $('#spinner').delay("slow").fadeIn();
            var counter = 0;
            var myInterval = setInterval(function () {
                ++counter;
                $("p.textload").html("Hệ thống đang chia tem, vui lòng đợi trong giây lát <br><span style='font-size:35px;' class='red'>" + counter + "s</h2><br><img src='/images/128x128.gif'>");
            }, 1000);

            return true;
        }
    </script>
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
            //$("#ckActive").addClass("custom-control-input");
            //$("#check").addClass("custom-control-input");
            //$("#CheckBox1").addClass("custom-control-input");
            //$("#CheckBox2").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(".select2").select2({
                width: '100%'
            });
            $("#ckSound").addClass("custom-control-input");
        }
        $(function () {
            $(".formatMoney").keyup(function (e) {
                $(this).val(format($(this).val()));
            });
        });
        $(document).ready(function () {
            Init();
        });
        //$('#txtThuHoach').daterangepicker({
        //    singleDatePicker: true,
        //    showDropdowns: true,
        //    //minYear: 1901,
        //    //maxYear: parseInt(moment().format('YYYY'), 10),
        //    locale: {
        //        format: 'DD/MM/YYYY',
        //    },
        //}, function (start, end, label) {
        //    //console.log("A new date selection was made: " + start.format('DD/MM/YYYY') + ' to ' + end.format('YYYY-MM-DD'));
        //});
    </script>
</asp:Content>
