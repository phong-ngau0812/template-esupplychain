using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class BaoHiem_Add : System.Web.UI.Page
{
    public string title = "Thêm mới bảo hiểm";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddSupplier();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void AddSupplier()
    {
        try
        {

            BaoHiemRow _SupplierRow = new BaoHiemRow();
            _SupplierRow.HoTen = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _SupplierRow.SDT = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
            _SupplierRow.DiaChi = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _SupplierRow.BienXe = string.IsNullOrEmpty(txtBienXe.Text) ? string.Empty : txtBienXe.Text;
            _SupplierRow.SoKhung = string.IsNullOrEmpty(txtSoKhung.Text) ? string.Empty : txtSoKhung.Text;
            _SupplierRow.SoMay = string.IsNullOrEmpty(txtSoMay.Text) ? string.Empty : txtSoMay.Text;
            _SupplierRow.GhiChu = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            if (ddlType.SelectedValue != "1")
            {
                _SupplierRow.TrongTai = string.IsNullOrEmpty(txtTrongTai.Text) ? string.Empty : txtTrongTai.Text;
                _SupplierRow.SoChoNgoi = string.IsNullOrEmpty(txtSoCho.Text) ? string.Empty : txtSoCho.Text;
                _SupplierRow.MucDichSuDung = string.IsNullOrEmpty(txtMucDich.Text) ? string.Empty : txtMucDich.Text;
            }
            _SupplierRow.ThoiHanBaoHiem = txtHanBH.SelectedValue;
            _SupplierRow.PhiBaoHiem = string.IsNullOrEmpty(txtPhiBH.Text) ? string.Empty : txtPhiBH.Text;
            _SupplierRow.LoaiXe = ddlType.SelectedValue;
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

            BusinessRulesLocator.GetBaoHiemBO().Insert(_SupplierRow);
            if (_SupplierRow != null)
            {
                Response.Redirect("BaoHiem_List.aspx", false);
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateSupplier", ex.ToString());
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
}