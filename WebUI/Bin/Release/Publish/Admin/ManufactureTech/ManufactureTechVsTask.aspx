<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="Admin_ManufactureTech_ManufactureTechVsTask, App_Web_5kqeq2lh" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />

    <style>
        label {
            line-height: 23px;
            margin-top: .5rem;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1" class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><%=title %></li>
                                <li class="breadcrumb-item"><a href="ManufartureTech_List">Quản lý QTCN</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title"><%=title %> : <%=manufactureTech %></h4>
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
                                    <div class="form-group">
                                        <label>Thứ tự công việc</label>
                                        <asp:TextBox runat="server" ID="txtNumber" ClientIDMode="Static" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Tên đề mục công việc <span class="red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="form-control" required data-parsley-required-message="Chưa nhập đề mục công việc"></asp:TextBox>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label>Định mức thời gian</label>
                                        </div>
                                        <div class="col-lg-9 row">
                                            <div class="col-lg-6 row">
                                                <div class="col-lg-9">
                                                    <asp:TextBox runat="server" ID="txtHour" ClientIDMode="Static" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>Giờ</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 row">
                                                <div class="col-lg-9">
                                                    <asp:TextBox runat="server" ID="txtMinute" ClientIDMode="Static" CssClass="form-control" TextMode="Number" min="0" max="60"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>Phút</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Nội dung</label>
                                        <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                        </CKEditor:CKEditorControl>
                                    </div>

                                </div>
                            </div>

                            <!--end form-group-->
                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Quay lại" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" UseSubmitBehavior="false" runat="server" ClientIDMode="Static" Text="Clear" CssClass="btn btn-gradient-danger waves-effect m-l-5"></asp:Button>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMessage" CssClass="msg-sc-edit" ClientIDMode="Static" Visible="false"></asp:Label>
                            </div>
                            <!--end form-group-->
                            <div class="form-group row">
                                <div class="col-12">
                                    <asp:ScriptManager runat="server" ID="src"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server" ID="up">
                                        <ContentTemplate>
                                            <table id="datatable" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th>Thứ tự</th>
                                                        <th>Tên Đề Mục Công Việc</th>
                                                        <th>Thời gian thực hiện</th>
                                                        <th width="8%">Chức năng</th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater runat="server" ID="rptManufactureTech" OnItemCommand="rptManufactureTech_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr id="<%#Eval("ManufactureTechVsTask_ID") %>" onclick="getValueManu(this.id)">
                                                                <td><%#Eval("Sort") %></td>
                                                                <td><b><%#Eval("Name").ToString().ToUpper()%></b></td>
                                                                <td>
                                                                    <%#Eval("Hour") %> Giờ <%# Eval("Minute")%> Phút
                                                                    <asp:Literal runat="server" ID="lblID" Text='<%#Eval("ManufactureTechVsTask_ID") %>' Visible="false"></asp:Literal></td>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CssClass="mr-2" ToolTip="Xóa" CommandArgument='<%#Eval("ManufactureTechVsTask_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?')"><i class="fas fa-trash-alt text-danger font-16"></i></asp:LinkButton>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- end col -->
                            </div>
                            <!--end form-->
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdfIdManu" ClientIDMode="Static" />
            <asp:Label runat="server" ID="lblText"></asp:Label>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Parsley js -->
    <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>

    <!----date---->
    <%--<script src="/theme/plugins/select2/select2.min.js"></script>--%>
    <script src="/theme/plugins/moment/moment.js"></script>
    <script src="/theme/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"></script>
    <script src="/theme/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="/theme/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <script src="/theme/plugins/timepicker/bootstrap-material-datetimepicker.js"></script>
    <script src="/theme/plugins/bootstrap-touchspin/js/jquery.bootstrap-touchspin.min.js"></script>
    <%--<script src="/theme/assets/pages/jquery.forms-advanced.js"></script>--%>
    <script src="/theme/plugins/select2/select2.min.js"></script>
    <!--Wysiwig js-->
    <script src="/theme/plugins/tinymce/tinymce.min.js"></script>
    <script src="/theme/assets/pages/jquery.form-editor.init.js"></script>
    <!-- App js -->
    <script src="/theme/assets/js/jquery.core.js"></script>

    <script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>
    <script src="//cdn.ckeditor.com/4.5.9/standard/ckeditor.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/ckeditor/4.5.9/adapters/jquery.js"></script>
    <script>

        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            $("#ckActive").addClass("custom-control-input");
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
        }
        $(document).ready(function () {
            Init();
            $(".select2").select2({
                width: '100%'
            });

        });
    </script>
    <script>
        function getValueManu(ManuID) {
            $.ajax({
                url: "/WebServices/Product.asmx/GetInfoManu",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: { ManufactureTechVsTask_ID: ManuID },
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        const obj = JSON.parse(result.d);
                        //console.log(data.length);
                        if (obj.length > 0) {
                            console.log(obj);
                            $('#txtName').val(obj[0].Name);
                            // $('#txtNote').val(obj[0].Description);
                            $('#txtNumber').val(obj[0].Sort);
                            $('#txtHour').val(obj[0].Hour);
                            $('#txtMinute').val(obj[0].Minute);
                            $('#hdfIdManu').val(obj[0].ManufactureTechVsTask_ID);
                            $('#txtNote').(obj[0].Description);
                            console.log($('#txtNote'));
                            //CKEDITOR.instances['txtNote'].setData(obj[0].Description);

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
</asp:Content>

