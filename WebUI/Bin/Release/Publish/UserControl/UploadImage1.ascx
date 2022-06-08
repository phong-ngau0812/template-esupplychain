<%@ control language="C#" autoeventwireup="true" inherits="UserControl_UploadImage1, App_Web_tellkycl" %>
<asp:FileUpload ID="FilUpl" runat="server" />
<asp:CustomValidator ID="ErrorMsg" runat="server" ErrorMessage="CustomValidator" OnServerValidate="ErrorMsg_ServerValidate"></asp:CustomValidator>
