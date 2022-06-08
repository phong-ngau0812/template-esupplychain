<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadImage1.ascx.cs" Inherits="UserControl_UploadImage1" %>
<asp:FileUpload ID="FilUpl" runat="server" />
<asp:CustomValidator ID="ErrorMsg" runat="server" ErrorMessage="CustomValidator" OnServerValidate="ErrorMsg_ServerValidate"></asp:CustomValidator>
