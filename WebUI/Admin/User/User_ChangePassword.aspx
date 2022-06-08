<%@ Page Title="" Language="C#" MasterPageFile="~//Template/CMS.master" AutoEventWireup="true" CodeFile="User_ChangePassword.aspx.cs" Inherits="User_ChangePassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHead" runat="Server">
    <title>Đổi mật khẩu</title>
    <!-- DataTables -->
    <link href="/theme/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/theme/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <form runat="server" id="frm1">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item active"><a>Đổi mật khẩu</a></li>
                                <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
                            </ol>
                        </div>
                        <h4 class="page-title">Đổi mật khẩu</h4>
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
                            <asp:ChangePassword ID="ChangePassword1" runat="server" ChangePasswordFailureText="Mật khẩu cũ chưa chính xác" Width="100%">
                                <ChangePasswordTemplate>
                                    <div class="form-group">
                                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Mật khẩu cũ </asp:Label>
                                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Yêu cầu nhập mật khẩu cũ." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Mật khẩu mới </asp:Label>
                                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="Yêu cầu nhập mật khẩu mới" ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Xác nhận mật khẩu mới </asp:Label>
                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Yêu cầu xác nhận mật khẩu mới" ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Mật khẩu xác nhận chưa chính xác" ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="ChangePasswordPushButton" CssClass="btn btn-gradient-primary waves-effect waves-light" runat="server" CommandName="ChangePassword" Text="Thay đổi" ValidationGroup="ChangePassword1" />
                                        <asp:Button ID="CancelPushButton" CssClass="btn btn-gradient-danger waves-effect m-l-5" runat="server" CausesValidation="False" CommandName="Cancel" OnClick="CancelPushButton_Click" Text="Thoát" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ValidationGroup="ChangePassword1" ShowSummary="False" />
                                    </div>
                                    <asp:Label ID="FailureText" runat="server" ForeColor="Red" />
                                </ChangePasswordTemplate>
                                <SuccessTemplate>
                                    <div class="alert icon-custom-alert alert-outline-success alert-success-shadow" role="alert"><i class="mdi mdi-check-all alert-icon"></i>
                                        <div class="alert-text"><strong>Bạn đã thay đổi mật khẩu thành công!</strong></div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true"><i class="mdi mdi-close text-danger"></i></span></button>
                                        </div>
                                    </div>
                                      <asp:Button ID="CancelPushButton" CssClass="btn btn-gradient-danger waves-effect m-l-5" runat="server" CausesValidation="False" CommandName="Cancel" OnClick="CancelPushButton_Click" Text="Thoát" />
                                </SuccessTemplate>
                            </asp:ChangePassword>
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
    <script>
        $(window).on('load', function () {
            setTimeout(function () { $('#spinner').fadeOut(); }, 200);
        })
    </script>
</asp:Content>
