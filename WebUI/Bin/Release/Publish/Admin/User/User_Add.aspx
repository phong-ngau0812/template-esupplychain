<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="User_Add, App_Web_00zkwi3t" validaterequest="false" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>
    <link href="../../css/telerik.css?v=<%=Systemconstants.Version(5) %>" rel="stylesheet" type="text/css" />
    <style>
        .font-13 label {
            color: #656d9a;
            font-size: 14px;
            font-weight: bold;
        }

        .font-11 input {
            margin-left: 25px;
        }

        .role {
            overflow-y: scroll;
            overflow-x: hidden;
            max-height: 600px;
            padding-left: 10px;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('#ddlLocation').select2({
            });
            $('#ddlDistrict').select2({
            });
            $('#ddlWard').select2({
            });
        });
    </script>
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

                                <li class="breadcrumb-item"><a href="User_List.aspx">Quản lý tài khoản</a></li>

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

                            <div class="row">
                                <div class="col-lg-6">
                                    <h3 class="mt-0 header-title">CHỌN HỆ THỐNG</h3>
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlHeThong" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHeThong_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn hệ thống"></asp:DropDownList>

                                    </div>
                                    <div class="form-group" runat="server" id="HideCapBac">
                                        <label>Cấp bậc <span class="red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlRank" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn cấp bậc tài khoản" AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="u">
                                        <ContentTemplate>
                                            <div class="form-group" runat="server" id="Tinh">
                                                <label>Tỉnh/Thành phố <span class="red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlLocation" ClientIDMode="Static" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn tỉnh/ thành phố"></asp:DropDownList>
                                            </div>
                                            <div class="form-group" runat="server" id="Huyen">
                                                <label>Quận huyện <span class="red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlDistrict" ClientIDMode="Static" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn quận/ huyện" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="form-group" runat="server" id="PhuongXa">
                                                <label>Phường xã <span class="red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlWard" ClientIDMode="Static" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn phường/ xã"></asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group" runat="server" id="CapSo" visible="false">
                                        <label>Sở phụ trách </label>
                                        <asp:DropDownList runat="server" ID="ddlDepartmentMan" CssClass="select2 form-control"></asp:DropDownList>
                                    </div>
                                    <asp:Panel runat="server" ID="pnCTT" Visible="false">
                                        <h3 class="mt-0 header-title">Phân quyền cổng thông tin</h3>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnEs" Visible="false">
                                        <h3 class="mt-0 header-title">Phân quyền</h3>

                                        <div class="form-group">
                                            <label>Nhóm quyền <span class="red">*</span></label>
                                            <asp:DropDownList runat="server" ID="ddlFunctionGroup" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFunctionGroup_SelectedIndexChanged" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn nhóm chức năng"></asp:DropDownList>
                                        </div>
                                        <div class="form-group" runat="server" visible="false" id="DivSale">
                                            <label>Loại tài khoản kinh doanh <span class="red">*</span></label>
                                            <asp:DropDownList runat="server" ID="ddlSaleCheckVN" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn loại tài khoản KD"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="ddlChainLink" CssClass="select2 form-control" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlChainLink_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="form-group">
                                            <asp:ListBox runat="server" Visible="false" ID="ddlProductBrandList" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="&nbsp;&nbsp;-- Chọn doanh nghiệp  --"></asp:ListBox>
                                        </div>

                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="ddlProductBrand" CssClass="select2 form-control" Visible="false" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn doanh nghiệp"></asp:DropDownList>
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn loại tài khoản" Visible="false"></asp:DropDownList>
                                                </div>

                                                <div runat="server" id="phongban" visible="false">
                                                    <div class="form-group">
                                                        <asp:DropDownList runat="server" ID="ddlDepartmentUser" CssClass="select2 form-control" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Chưa chọn phòng ban" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentUser_SelectedIndexChanged"></asp:DropDownList>

                                                    </div>
                                                    <div class="form-group">
                                                        <telerik:RadComboBox RenderMode="Lightweight" MaxHeight="300px" ID="ddlWarehouse" Filter="Contains" Skin="MetroTouch" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="--- Chọn kho ---" Localization-ItemsCheckedString="kho được chọn">
                                                            <Localization CheckAllString="Chọn tất cả"
                                                                AllItemsCheckedString="Tất cả đều được chọn" />
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList runat="server" ID="ddlWorkshop" CssClass="select2 form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList runat="server" ID="ddlZone" CssClass="select2 form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList runat="server" ID="ddlArea" CssClass="select2 form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList runat="server" ID="ddlFarm" CssClass="select2 form-control"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div runat="server" id="RolePermission">
                                                    <label>Chọn module quản lý</label>
                                                    <telerik:RadComboBox RenderMode="Lightweight" MaxHeight="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged" ID="ddlFunction" Skin="MetroTouch" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Chọn module " Localization-ItemsCheckedString="module được chọn">
                                                        <Localization CheckAllString="Chọn tất cả"
                                                            AllItemsCheckedString="Tất cả đều được chọn" />
                                                    </telerik:RadComboBox>

                                                    <br />
                                                    <br />
                                                    <div class="role">
                                                        <asp:Repeater runat="server" ID="rptFunction" OnItemDataBound="rptFunction_ItemDataBound">
                                                            <ItemTemplate>
                                                                <hr />
                                                                <div class="container-checked" id='test<%#Eval("Function_ID") %>'>
                                                                    <div class='checkbox checkbox-success font-13'>
                                                                        <asp:CheckBox runat="server" ID="ckParent" Text='<%#Eval("Name") %>' onclick='GetCheck(this);' Checked="true" />
                                                                    </div>

                                                                    <%-- <b><%#Eval("Name") %>--%>
                                                                    <asp:Literal runat="server" ID="lblFunction_ID" Text='<%#Eval("Function_ID") %>' Visible="false"></asp:Literal>
                                                                    <%--</b>--%>
                                                                    <div class="row">
                                                                        <asp:Repeater runat="server" ID="rptPage">
                                                                            <ItemTemplate>
                                                                                <div class="col-lg-12 col-md-12">
                                                                                    <div class='checkbox checkbox-primary font-11'>
                                                                                        <asp:Literal runat="server" ID="lblPageFunction_ID" Text='<%#Eval("PageFunction_ID") %>' Visible="false"></asp:Literal>
                                                                                        <asp:CheckBox runat="server" ID="ckRole" Text='<%#Eval("Name") %>' onclick='GetCheckChild(this);' Checked="true" />
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <br />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                    <asp:Literal ID="lblMsg" Text="" runat="server"></asp:Literal>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Tên tài khoản <span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtUser" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập tên tài khoản"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Avatar</label>
                                        <br />
                                        <div style="margin: 5px 0px;">
                                            <a href="<%=avatar %>" target="_blank">
                                                <asp:Image ID="imganh" runat="server" ImageUrl='../../images/no-image-icon.png' Width="100px" />
                                            </a>
                                        </div>
                                        <asp:FileUpload ID="fulAnh" runat="server" ClientIDMode="Static" onchange="img();" />
                                    </div>
                                    <div class="form-group" runat="server" id="HideHoTen">
                                        <label>Họ tên <span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtFullName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập họ tên"></asp:TextBox>
                                    </div>
                                    <!--end form-group-->
                                    <div class="form-group">
                                        <label>Mật khẩu<span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtPass" TextMode="Password" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập mật khẩu"></asp:TextBox>
                                        <div class="mt-2">
                                            <asp:TextBox runat="server" ID="txtPassRe" CssClass="form-control" required data-parsley-required-message="Chưa nhâp xác nhận mật khẩu"
                                                data-parsley-equalto="#txtPass"
                                                placeholder="Xác nhận mật khẩu" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--end form-group-->

                                    <div class="form-group">
                                        <label>E-Mail <span class="red">*</span></label>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" TextMode="Email" parsley-type="email" CssClass="form-control" required data-parsley-required-message="Chưa nhập Email"></asp:TextBox>

                                        </div>
                                    </div>
                                    <!--end form-group-->

                                    <div class="form-group">
                                        <label>Địa chỉ</label>
                                        <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group none" runat="server" id="HideSdt">
                                        <label>Số điện thoại</label>
                                        <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <!--end form-group-->

                                    <div class="form-group none" runat="server" id="HideGioiTinh">
                                        <label>Giới tính</label>
                                        <asp:DropDownList runat="server" ID="ddlGioiTinh" CssClass="form-control">
                                            <asp:ListItem Text="-Chọn giới tính-" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                                            <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <%--   <div class="form-group">
                                <label>Ngày sinh</label>
                                <asp:TextBox runat="server" ID="txtBirth" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>--%>
                                    <div class="form-group none" runat="server" id="HideNgaySinh">
                                        <label>Ngày sinh</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="txtBirth" Text="01/01/1980" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                            <%--<input name="birthday" type="text" value="01/01/1980" id="txtBirth" class="form-control">--%>
                                            <div class="input-group-append">
                                                <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end form-group-->
                                    <div class="form-group">
                                        <%-- <label>Kích hoạt</label>--%>
                                        <div class="checkbox checkbox-success">
                                            <asp:CheckBox runat="server" ID="ckActive" ClientIDMode="Static" Checked="true" Text="KÍCH HOẠT" />

                                        </div>
                                    </div>
                                    <!--end form-group-->

                                    <%--    <div class="form-group">
                                        <label>Giới thiệu</label>
                                        <asp:TextBox runat="server" ID="txtNote" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                    </div>--%>
                                    <!--end form-group-->

                                    <div class="form-group mb-0">
                                        <asp:Button runat="server" ID="btnSave" OnClientClick="SaveLife()" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                        <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                                    </div>
                                </div>


                            </div>
                            <%--   <div class="col-lg-12">
                            
                            </div>--%>
                            <!--end form-group-->

                            <!--end form-->
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>

                <!-- end col -->
                <!-- end col -->
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <%--<script type="text/javascript" src="/ckeditor/ckeditor.js"></script>--%>
    <%--<script src="/ckfinder/ckfinder.js"></script>--%>
    <script>

       
        //function UploadImage() {
        //    var finder = new CKFinder();
        //    //finder.selectActionFunction = function (fileUrl) {
        //    //    //$("#lblDocument").html("<img src='" + fileUrl + "' class='img-responsive'/>");
        //    //    //$('#hdImg').val(fileUrl);
        //    //    //$("#btn_upload").click();
        //    //};
        //    finder.ResourceType = 'Images';
        //    finder.popup();
        //}
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function SaveLife() {
            tinymce.triggerSave();
            //$('#spinner').fadeIn();
            //setTimeout(function () { $('#spinner').fadeOut(); }, 500);
        }
        function GetCheck(element) {
            var ID = "#" + ($(element).parent().parent().attr("id")) + " input[type='checkbox']";
            if (element.checked) {
                $(ID).prop('checked', true);
            }
            else {
                $(ID).prop('checked', false);
            }
        }
        function GetCheckChild(element) {
            var ID = "#" + ($(element).parent().parent().parent().parent().attr("id")) + " .font-13 input[type='checkbox']";
            var ID_Child = "#" + ($(element).parent().parent().parent().parent().attr("id")) + " .font-11 input[type='checkbox']";
            if (!element.checked) {
                $(ID).prop('checked', false);
            }
            var countCheckedCheckboxes = $(ID_Child).filter(':checked').length;
            //alert(countCheckedCheckboxes);
            var dem = 0;
            $(ID_Child).each(function () {
                dem++;

            });

            if (dem == countCheckedCheckboxes) {
                $(ID).prop('checked', true);
            }
        }
        function Init() {
            LoadMCE();
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 5000);
        }
        $(document).ready(function () {
            $(".select2").select2({
                width: '100%'
            });
            Init();
            $('#txtBirth').daterangepicker({
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

        });
    </script>
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
    <!-- Parsley js -->
    <script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="../../theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <script src="../../theme/plugins/select2/select2.min.js"></script>
    <script src="../../theme/plugins/moment/moment.js"></script>
    <script src="../../theme/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"></script>
    <script src="../../theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <script src="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="../../theme/plugins/bootstrap-touchspin/js/jquery.bootstrap-touchspin.min.js"></script>
    <%--<script src="../../theme/assets/pages/jquery.forms-advanced.js"></script>--%>

    <!--Wysiwig js-->
    <script src="../../theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="../../theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="../../theme/assets/js/jquery.core.js"></script>

</asp:Content>

