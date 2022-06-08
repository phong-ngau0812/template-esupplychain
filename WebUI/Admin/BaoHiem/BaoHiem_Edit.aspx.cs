using DbObj;
using evointernal;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Supplier_Edit : System.Web.UI.Page
{
    int BaoHiem_ID = 0;
    public string title = "Thông tin bảo hiểm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["BaoHiem_ID"]))
            int.TryParse(Request["BaoHiem_ID"].ToString(), out BaoHiem_ID);
        if (!IsPostBack)
        {
            FillInfoSupplier();
        }
    }
   
    protected void FillInfoSupplier()
    {
        try
        {
            if (BaoHiem_ID != 0)
            {
               
                BaoHiemRow _SupplierRow = new BaoHiemRow();
                _SupplierRow = BusinessRulesLocator.GetBaoHiemBO().GetByPrimaryKey(BaoHiem_ID);
                if (_SupplierRow != null)
                {
                    txtName.Text = _SupplierRow.IsHoTenNull ? string.Empty : _SupplierRow.HoTen;
                    txtPhone.Text = _SupplierRow.IsSDTNull ? string.Empty : _SupplierRow.SDT;
                    txtAddress.Text = _SupplierRow.IsDiaChiNull ? string.Empty : _SupplierRow.DiaChi;

                    txtBienXe.Text = _SupplierRow.IsBienXeNull ? string.Empty : _SupplierRow.BienXe;
                    txtSoKhung.Text = _SupplierRow.IsSoKhungNull ? string.Empty : _SupplierRow.SoKhung;
                    txtSoMay.Text = _SupplierRow.IsSoMayNull ? string.Empty : _SupplierRow.SoMay;
                    txtNote.Text = _SupplierRow.IsGhiChuNull ? string.Empty : _SupplierRow.GhiChu;
                    txtPhiBH.Text = _SupplierRow.IsPhiBaoHiemNull ? string.Empty : _SupplierRow.PhiBaoHiem;
                    ddlType.SelectedValue = _SupplierRow.LoaiXe;
                    ddlType.Enabled = false;
                    QRCode(_SupplierRow.LoaiXe);
                    if (_SupplierRow.LoaiXe=="2")
                    {
                        txtTrongTai.Text = _SupplierRow.IsTrongTaiNull ? string.Empty : _SupplierRow.TrongTai;
                        txtSoCho.Text = _SupplierRow.IsSoChoNgoiNull ? string.Empty : _SupplierRow.SoChoNgoi;
                        txtMucDich.Text = _SupplierRow.IsMucDichSuDungNull ? string.Empty : _SupplierRow.MucDichSuDung;

                    }
                    if (_SupplierRow.Active == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                   
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoSupplier", ex.ToString());
        }
    }

    protected void UpdateSupplier()
    {
        try
        {
            BaoHiemRow _SupplierRow = new BaoHiemRow();
            if (BaoHiem_ID != 0)
            {
                _SupplierRow = BusinessRulesLocator.GetBaoHiemBO().GetByPrimaryKey(BaoHiem_ID);
                if (_SupplierRow != null)
                {
                    _SupplierRow.HoTen = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _SupplierRow.SDT = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _SupplierRow.DiaChi = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    _SupplierRow.BienXe = string.IsNullOrEmpty(txtBienXe.Text) ? string.Empty : txtBienXe.Text;
                    _SupplierRow.SoKhung = string.IsNullOrEmpty(txtSoKhung.Text) ? string.Empty : txtSoKhung.Text;
                    _SupplierRow.SoMay = string.IsNullOrEmpty(txtSoMay.Text) ? string.Empty : txtSoMay.Text;
                    _SupplierRow.GhiChu = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _SupplierRow.LoaiXe = ddlType.SelectedValue;
                    if (ddlType.SelectedValue != "1")
                    {
                        _SupplierRow.TrongTai = string.IsNullOrEmpty(txtTrongTai.Text) ? string.Empty : txtTrongTai.Text;
                        _SupplierRow.SoChoNgoi = string.IsNullOrEmpty(txtSoCho.Text) ? string.Empty : txtSoCho.Text;
                        _SupplierRow.MucDichSuDung = string.IsNullOrEmpty(txtMucDich.Text) ? string.Empty : txtMucDich.Text;
                    }
                    _SupplierRow.ThoiHanBaoHiem = txtHanBH.SelectedValue;
                    _SupplierRow.PhiBaoHiem = string.IsNullOrEmpty(txtPhiBH.Text) ? string.Empty : txtPhiBH.Text;
                   
                    if (ckActive.Checked)
                    {
                        _SupplierRow.Active = 1;
                    }
                    else
                    {
                        _SupplierRow.Active = 0;
                    }

                    _SupplierRow.CreateDate = DateTime.Now;
                    _SupplierRow.LastEditedBy = MyUser.GetUser_ID();
                    _SupplierRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoSupplier();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateSupplier", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateSupplier();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BaoHiem_List.aspx", false);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue != "1")
        {
            Oto.Visible = Oto1.Visible = Oto2.Visible = true;
        }
        else
        {
            Oto.Visible = Oto1.Visible = Oto2.Visible = false;

        }
    }
    public void QRCode(string loaixe)
    {
        string domain = string.Empty;
        if (loaixe == "1")
        {
            domain = "https://esupplychain.vn/8938538734369/";
        }
        else
        {
            domain = "https://esupplychain.vn/8938538734383/";
        }
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(domain + DateTime.Now.Year.ToString()+ BaoHiem_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                lblQR.Text = "<img width='150px' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
    }

}

