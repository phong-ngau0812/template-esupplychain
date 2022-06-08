<%@ page title="" language="C#" masterpagefile="~/Template/CMS.master" autoeventwireup="true" inherits="ProductInfo, App_Web_quwo134q" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <link href="/theme/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/theme/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="/theme/plugins/timepicker/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="/theme/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
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
                                <li class="breadcrumb-item"><a href="Product_List">Quản lý sản phẩm</a></li>
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
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Giá </label>
                                            <asp:TextBox runat="server" ID="txtPrice" ClientIDMode="Static" CssClass="form-control formatMoney"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Giảm giá (%)</label>
                                            <asp:TextBox runat="server" ID="txtDiscount" ClientIDMode="Static" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>OCOP</label>
                                            <asp:DropDownList runat="server" ID="ddlOCOP" CssClass="form-control select2">
                                                <asp:ListItem Value="0" Text="--Chọn--"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="OCOP 2 sao"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="OCOP 3 sao"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="OCOP 4 sao"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="OCOP 5 sao"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Link nhúng youtube </label>
                                            <asp:TextBox runat="server" ID="txtYoutube" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Hạn sử dụng (dùng để hiện thị trên thông tin sản phẩm) </label>
                                            <asp:TextBox runat="server" ID="txtHSD" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Hạn sử dụng tính theo ngày (để tính hạn xử dụng khi xác thực)</label>
                                            <div class=" row form-group">
                                                <div class="col-lg-10">
                                                    <asp:TextBox runat="server" ID="txtDate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-2 mt-2">
                                                    ngày
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Thời gian bảo hành</label>
                                            <div class=" row form-group">
                                                <div class="col-lg-10">
                                                    <asp:TextBox runat="server" ID="txtMonth" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-2 mt-2">
                                                    tháng
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Tag Sản phẩm (mỗi tag cách nhau dấu ",") </label>
                                            <asp:TextBox runat="server" ID="txtTag" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label>Thư viện ảnh (jpg, png, gif) </label>
                                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" Width="300px" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Thư viện ảnh"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Bản công bố sản phẩm"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Mẫu kiểm nghiệm"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Đối tác"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:FileUpload ID="file1" runat="server" AllowMultiple="true" onchange="UploadFake();" /><br>
                                            <br>
                                            <%--<input type="button" value="Upload File" onclick="UploadFile()" />--%>
                                            <progress id="progressBar" value="0" max="100" style="width: 100%; margin-top: 5px;"></progress>
                                            <h3 id="status"></h3>
                                            <p id="loaded_n_total"></p>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <h6><%=ddlType.SelectedItem.Text %></h6>
                                    </div>
                                    <asp:Repeater runat="server" ID="rptImg" OnItemCommand="rptImg_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-lg-1" style="border: 1px solid #ccc;">
                                                <asp:LinkButton runat="server" CommandName="Delete" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này không?')" CommandArgument='<%#Eval("ProductGallery_ID") %>' ID="btnDelete" Style="float: right;"><img src="../../img/png_remove.png"/></asp:LinkButton>
                                                <br />
                                                <a href='<%#"/data/product/product_info/"+ Eval("Image") %>' target="_blank">
                                                    <img src='<%#"/data/product/product_info/"+ Eval("Image") %>' class="img-fluid" /></a>
                                                <br />
                                                Thứ tự 
                                                <asp:TextBox runat="server" ID="txtOrder" CssClass="form-control" Width="80px" Text='<%#Eval("Sort")%>' TextMode="Number"></asp:TextBox>
                                                <asp:Literal runat="server" ID="lblID" Text='<%#Eval("ProductGallery_ID") %>' Visible="false"></asp:Literal>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>Thông tin sản phẩm</label>
                                <CKEditor:CKEditorControl ID="txtNote" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>Công dụng</label>
                                <CKEditor:CKEditorControl ID="txtCongDung" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                            </div>

                            <div class="form-group mb-0">
                                <asp:Button runat="server" ID="btnSave" OnClientClick="UploadFile()" ClientIDMode="Static" OnClick="btnSave_Click" CssClass="btn btn-gradient-primary waves-effect waves-light" Text="Lưu" />
                                <asp:Button runat="server" ID="btnFake" OnClientClick="UploadFile()" ClientIDMode="Static" OnClick="Button1_Click" CssClass="btn btn-gradient-primary waves-effect waves-light none" Text="Lưu" />
                                <a class="btn btn-gradient-primary waves-effect waves-light" target="_blank" href="https://esupplychain.vn/p/<%=Product_ID %>">Xem trước </a>
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
    <script src="../../js/Function.js"></script>
    <script>
        function UploadFake() {
            $("#btnFake").click();
        }
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 100);
        })
        function Init() {
            setTimeout(function () { $('#lblMessage').fadeOut(); }, 3000);
            $(function () {
                $(".formatMoney").keyup(function (e) {
                    $(this).val(format($(this).val()));
                });
            });
        }
        $(document).ready(function () {
            Init();
            $(".select2").select2({
                width: '100%'
            });
        });
    </script>
    <script>
        var counter;
        function UploadFile() {
            var files = $("#<%=file1.ClientID%>").get(0).files;
            counter = 0;
            var TypeID = $("#ddlType").val();
            // Loop through files
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var formdata = new FormData();
                formdata.append("file1", file);
                var ajax = new XMLHttpRequest();
                //console.log(file.name);
                ajax.upload.addEventListener("progress", progressHandler, false);
                ajax.addEventListener("load", completeHandler, false);
                ajax.addEventListener("error", errorHandler, false);
                ajax.addEventListener("abort", abortHandler, false);
                ajax.open("POST", "/FileUploadHandler.ashx?ProductID=<%=Product_ID%>&Type=" + TypeID);
                ajax.send(formdata);
                //setTimeout(function () { }, 0);
                $("#spinner").show();
                location.reload();
            }
        }
        function progressHandler(event) {
            $("#loaded_n_total").html("Uploaded " + event.loaded + " bytes of " + event.total);
            var percent = (event.loaded / event.total) * 100;
            $("#progressBar").val(Math.round(percent));
            $("#status").html(Math.round(percent) + "% uploaded... please wait");
        }
        function completeHandler(event) {
            counter++
            $("#status").html(counter + " " + event.target.responseText);
        }
        function errorHandler(event) {
            $("#status").html("Upload Failed");
        } function abortHandler(event) {
            $("#status").html("Upload Aborted");
        }
    </script>
</asp:Content>

