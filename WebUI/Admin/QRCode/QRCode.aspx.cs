using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class QRCode : System.Web.UI.Page
{
    public string username;
    MembershipUser user;
    UserProfile ProfileUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //    Response.Redirect("/");

        username = Context.User.Identity.Name;
        user = Membership.GetUser(username, false);
        ProfileUser = UserProfile.GetProfile(username);

        if (!Page.IsPostBack)
        {

        }
    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/", false);
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        string qr_code = txtCode.Text;
        //qr_code + dr_hd.SelectedValue.ToString() + "," + dr_san_pham.SelectedValue.ToString() + "," + dr_thanh_vien.SelectedValue.ToString();

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(qr_code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;

        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                //  img.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                Response.ContentType = "image/png";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "QR_file" + ".png");
                bitMap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

    }
}