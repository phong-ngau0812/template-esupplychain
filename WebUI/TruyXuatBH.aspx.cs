using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TruyXuatBH : System.Web.UI.Page
{
    public string qrcodecontent = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["qrcodecontent"]))
            qrcodecontent = Request["qrcodecontent"].ToString();
        if (!IsPostBack)
        {
            LoadData();
        }
     //   Response.Write(qrcodecontent.Substring(4, qrcodecontent.Length - 4));
    }
    public string cogioi, chuxe, diachi, dienthoai, bks, sokhung, somay, loaixe, thoihan, phibh, ngaycap = string.Empty;
    private void LoadData()
    {
        try
        {
            BaoHiemRow _BaoHiemRow = BusinessRulesLocator.GetBaoHiemBO().GetByPrimaryKey(Convert.ToInt32(qrcodecontent.Substring(4, qrcodecontent.Length - 4)));
            if (_BaoHiemRow!= null)
            {
                Nodata.Visible = false;
                Data.Visible = true;
                if (_BaoHiemRow.LoaiXe=="1")
                {
                    cogioi = "Xe máy";
                }
                else
                {
                    cogioi = "Xe Ô tô";
                }
                chuxe = _BaoHiemRow.IsHoTenNull ? string.Empty : _BaoHiemRow.HoTen;
                diachi = _BaoHiemRow.IsDiaChiNull ? string.Empty : _BaoHiemRow.DiaChi;
                dienthoai = _BaoHiemRow.IsSDTNull ? string.Empty : _BaoHiemRow.SDT;
                bks = _BaoHiemRow.IsBienXeNull ? string.Empty : _BaoHiemRow.BienXe;
                sokhung = _BaoHiemRow.IsSoKhungNull ? string.Empty : _BaoHiemRow.SoKhung;
                somay = _BaoHiemRow.IsSoMayNull ? string.Empty : _BaoHiemRow.SoMay;

                loaixe = _BaoHiemRow.IsGhiChuNull ? string.Empty : _BaoHiemRow.GhiChu;
                thoihan = _BaoHiemRow.IsThoiHanBaoHiemNull ? string.Empty : (_BaoHiemRow.ThoiHanBaoHiem +" năm từ ngày " + _BaoHiemRow.CreateDate.ToString("dd/MM/yyyy") +" đến " + _BaoHiemRow.CreateDate.AddYears(1).ToString("dd/MM/yyyy"));
                phibh = _BaoHiemRow.IsPhiBaoHiemNull ? string.Empty : _BaoHiemRow.PhiBaoHiem+" vnđ";
                ngaycap = _BaoHiemRow.IsCreateDateNull ? string.Empty : _BaoHiemRow.CreateDate.ToString("dd/MM/yyyy");
            }
            else
            {
                Nodata.Visible = true;
                Data.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            Log.writeLog("LoadData", Ex.ToString());
        }
    }
}