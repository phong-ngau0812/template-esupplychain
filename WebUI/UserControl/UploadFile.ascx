<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="UserControl_UploadFile" %>
<asp:FileUpload ID="FilUpl" runat="server" />
<asp:CustomValidator ID="ErrorMsg" runat="server" ErrorMessage="CustomValidator" OnServerValidate="ErrorMsg_ServerValidate"></asp:CustomValidator>
