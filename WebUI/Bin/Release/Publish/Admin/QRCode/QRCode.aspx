<%@ page title="" language="C#" masterpagefile="~//Template/CMS.master" autoeventwireup="true" inherits="QRCode, App_Web_4pia4moy" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <title>Đổi mật khẩu</title>
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1"  class="form-parsley">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Tạo QRCode</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Tạo QRCode</h4>
                    </div>
                    <!--end page-title-box-->
                </div>

                <!--end col-->
            </div>
            <!-- end page title end breadcrumb -->
            <div class="row">
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-body">

                            <div class="form-group">
                                <label>Nhập mã (ví dụ: PI423-1#)</label>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="ChangePasswordPushButton" CssClass="btn btn-gradient-primary waves-effect waves-light" runat="server" Text="Tạo ảnh QRCode" OnClick="ChangePasswordPushButton_Click" />
                                <asp:Button ID="CancelPushButton" CssClass="btn btn-gradient-danger waves-effect m-l-5" runat="server" CausesValidation="False" UseSubmitBehavior="false" OnClick="CancelPushButton_Click" Text="Thoát" />
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- container -->

        <!--  Modal content for the above example -->

    </form>
    <!-- /.modal -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceBottom" runat="Server">
    <!-- Buttons examples -->
        <!-- Parsley js -->
   <%-- <script src="/theme/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/theme/assets/pages/jquery.validation.init.js"></script>--%>

    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
    </script>
</asp:Content>
