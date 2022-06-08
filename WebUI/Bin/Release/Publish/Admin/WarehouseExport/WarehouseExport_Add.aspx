<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="WarehouseExport_Add, App_Web_ijujwaxe" validaterequest="false" enableeventvalidation="false" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <%--<link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../theme/plugins/bootstrap-touchspin/css/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />--%>

    <link href="../../theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <style>
        #datatableNoSort_filter {
            float: right !important;
        }

        #datatableNoSort_info {
            display: none !important;
        }

        #datatableNoSort_paginate {
            display: none !important;
        }
    </style>
    <style type="text/css">
        .RadTreeList td {
            vertical-align: top;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="WarehouseExport_List.aspx">Quản lý xuất kho  </a></li>
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

                            <div class="form-group">
                                <label>Tên doanh nghiệp <span class="red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlProductBrand" OnSelectedIndexChanged="ddlProductBrand_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn doanh nghiệp"></asp:DropDownList>
                            </div>
                            <%--<div class="form-group">
								<label>Chọn kho<span class="red">*</span> </label>
								<asp:DropDownList runat="server" ID="ddlWarehouseImprot" OnSelectedIndexChanged="ddlWarehouseImprot_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn lệnh sản xuất"></asp:DropDownList>
							</div>--%>
                            <div class="form-group" runat="server" id="Nhom4">
                                <div class="form-group">
                                    <label>Tên phiếu xuất<span class="red">*</span> </label>
                                    <asp:TextBox runat="server" ID="txtTenPhieu" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Chọn kho </label>
                                    <asp:DropDownList runat="server" ID="ddlWarehouse" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn kho"></asp:DropDownList>

                                    <%--<asp:ListBox runat="server" ClientIDMode="Static" ID="ddlWarehouse" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn kho --"></asp:ListBox>--%>
                                </div>
                                <div class="form-group">
                                    <label>Chọn vật tư </label>
                                    <asp:ListBox runat="server" ClientIDMode="Static" ID="ddlMaterial" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" SelectionMode="Multiple" CssClass="select2 form-control" Width="100%" data-placeholder="-- Chọn các vật tư --"></asp:ListBox>
                                </div>
                                <div runat="server" id="Data2" visible="false">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
                                        <telerik:RadTreeList RenderMode="Lightweight" runat="server" ID="RadTreeList1" DataKeyNames="ID" ParentDataKeyNames="Parent_ID"
                                            OnNeedDataSource="RadTreeList1_NeedDataSource" AutoGenerateColumns="false">
                                            <Columns>
                                                <telerik:TreeListBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID">
                                                </telerik:TreeListBoundColumn>
                                                <telerik:TreeListBoundColumn DataField="Name" HeaderText="Tên" UniqueName="Name">
                                                </telerik:TreeListBoundColumn>
                                                <telerik:TreeListBoundColumn DataField="AmountInWareHouse" HeaderText="Số lượng trong kho" UniqueName="AmountInWareHouse">
                                                </telerik:TreeListBoundColumn>
                                                <telerik:TreeListTemplateColumn DataField="AmountExport" HeaderText="Số lượng xuất kho" UniqueName="AmountExport">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control" Text="0" min="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:TreeListTemplateColumn>
                                                <telerik:TreeListBoundColumn DataField="Unit" HeaderText="Đơn vị" UniqueName="Unit">
                                                </telerik:TreeListBoundColumn>
                                            </Columns>
                                        </telerik:RadTreeList>
                                    </telerik:RadAjaxPanel>
                                </div>

                                <div runat="server" id="Data1" visible="false">
                                    <table class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>Tên vật tư</th>
                                                <th width="20%">Số lượng trong kho </th>
                                                <th width="20%">Số lượng xuất</th>
                                                <th width="10%">Đơn vị</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptMaterialCollapse">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("Name") %>
                                                            <asp:Literal runat="server" ID="lblMaterial_ID" Text='<%#Eval("Material_ID") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblWarehouseImport_ID" Text='<%#Eval("WarehouseImport_ID") %>' Visible="false"></asp:Literal>
                                                        </td>
                                                        <td><%#string.IsNullOrEmpty(Eval("AmountInWareHouse").ToString())? "0" : Eval("AmountInWareHouse").ToString() %></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtQuantity" ClientIDMode="Static" CssClass="form-control " min="0"></asp:TextBox>
                                                            <%--<asp:DropDownList runat="server" ID="ddlNumber" CssClass="form-control select2"></asp:DropDownList>--%>
                                                        </td>
                                                        <td><%#Eval("Unit") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="Nhom2">
                                <div class="form-group">
                                    <label>Chọn kho<span class="red">*</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlWarehouseNhom2" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn kho"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Lệnh sản xuất<span class="red">*</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlProductPackageOrder" OnSelectedIndexChanged="ddlProductPackageOrder_SelectedIndexChanged" AutoPostBack="true" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn lệnh sản xuất"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Sản phẩm<span class="red">*</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn sản phẩm "></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Tên phiếu xuất<span class="red">*</span> </label>
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Tự động..." Enabled="false"></asp:TextBox>
                                </div>

                                <label>Vật tư trong lệnh</label>
                                <div runat="server" id="tbl">
                                    <table id="datatableNoSort" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                        <thead>
                                            <tr>
                                                <th width="5%"></th>
                                                <th>Tên vật tư</th>
                                                <th width="15%">Số lượng trong kho</th>
                                                <th width="15%">Nhập số lượng xuất</th>
                                                <th width="10%">Đơn vị</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptMaterial" OnItemDataBound="rptMaterial_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="lblMaterialID" runat="server" Text='<%#Eval("Material_ID")%>' Visible="false"></asp:Literal>
                                                            <asp:CheckBox ID="ckMaterialID1" runat="server" onclick='GetCheck(this.id);' />
                                                        </td>
                                                        <td><%#Eval("NameMaterial") %>
                                                            <asp:Literal runat="server" ID="lblMaterial_ID" Text='<%#Eval("Material_ID") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblWarehouseImport_ID" Text='<%#Eval("WarehouseImport_ID") %>' Visible="false"></asp:Literal>
                                                            <asp:Literal runat="server" ID="lblAmount" Text='<%#string.IsNullOrEmpty(Eval("AmountM").ToString())? "0" : Eval("AmountM").ToString()%>' Visible="false"></asp:Literal>
                                                        </td>
                                                        <td><%#string.IsNullOrEmpty(Eval("AmountM").ToString())? "0" : Eval("AmountM").ToString() %></td>
                                                        <td>

                                                            <asp:TextBox runat="server" ID="txtQuantity" TextMode="Number" max='<%#string.IsNullOrEmpty(Eval("AmountM").ToString())? "0" : Eval("AmountM").ToString() %>' min="0" ClientIDMode="Static" CssClass="form-control"></asp:TextBox></td>
                                                        <td><%#Eval("Unit") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Người xuất <span class="red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlWorkshop" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn người xuất vật tư"></asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label>Ngày xuất</label>
                                </div>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <div class="row" style="width: 100%">
                                            <div class="col-lg-6 row">
                                                <div class="col-lg-9" style="padding-right: 0px;">
                                                    <asp:TextBox runat="server" ID="txtNgayCap" ClientIDMode="Static" CssClass="form-control" name="birthday" />
                                                </div>
                                                <div class="col-lg-3" style="padding-left: 0px;">
                                                    <div class="input-group-append ">
                                                        <span class="input-group-text"><i class="dripicons-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlHour" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Giờ</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 row">
                                                <div class="col-lg-8">
                                                    <asp:DropDownList runat="server" ID="ddlMinutes" CssClass="select2 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label>Phút</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Người nhận <span class="red">*</span> </label>
                                <asp:DropDownList runat="server" ID="ddlImporter" CssClass="select2 form-control" Width="100%" data-parsley-required="true" data-parsley-allselected="true" data-parsley-required-message="Bạn chưa chọn người nhận"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Diễn giải</label>
                                <asp:TextBox runat="server" ID="txtComment" ClientIDMode="Static" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Mô tả</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>

                            <!--end form-group-->


                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />

                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                            <!--end form-group-->

                            <!--end form-->
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>

            </div>

        </div>
        <asp:HiddenField runat="server" ID="hdfType" Value="0" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <%-- <script>
        $(".rtlExpand").click(function () {
            alert("Handler for .click() called.");
        });
    </script>--%>
    <%--  <script>
         $(document).ready(function () {

             $("#myTable").on('input', '#txtQuantity', function () {
                 var calculated_total_sum = 0;
                 $("#myTable #txtQuantity").each(function () {
                     var get_textbox_value = $(this).val();
                     if ($.isNumeric(get_textbox_value)) {
                         calculated_total_sum += parseFloat(get_textbox_value);
                     }
                 });
                 console.log(calculated_total_sum);
                 $("#<%=total_sum_value.ClientID%>").html(calculated_total_sum);
            });

        });
     </script>--%>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        $(document).ready(function () {
            $('#txtNgayCap').daterangepicker({
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
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });

            function Init() {
                $("#ckActive").addClass("custom-control-input");
                setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            }


            $(".select2").select2({
                width: '100%'
            });
            Init();
        });
        function GetCheck(element) {
            var checkBox = document.getElementById(element);
            /* alert(element);*/
            /* var ID = "#" + ($(element).parent().parent().attr("id")) + " input[type='checkbox']";*/
            if (checkBox.checked) {
                $(checkBox).attr('checked', true);
                /*alert("1");*/
            }
            else {
                $(checkBox).attr('checked', false);
                /*alert("2");*/
            }
        }
    </script>

    <script src="../../js/Function.js"></script>

    <script src="/theme/plugins/select2/select2.min.js"></script>

    <!-- Parsley js -->
    <script src="../../theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="../../theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <%--<script src="../../theme/plugins/select2/select2.min.js"></script>--%>
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

</asp:Content>

