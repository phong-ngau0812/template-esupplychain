<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadImage.ascx.cs" Inherits="UserControl_UploadImage" %>
<asp:FileUpload ID="FilUpl" runat="server" />
<asp:CustomValidator ID="ErrorMsg" runat="server" ErrorMessage="CustomValidator" OnServerValidate="ErrorMsg_ServerValidate"></asp:CustomValidator>
