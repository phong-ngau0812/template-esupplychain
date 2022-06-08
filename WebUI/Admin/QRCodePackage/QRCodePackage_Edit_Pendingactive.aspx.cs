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
using Telerik.Web.UI;

public partial class QRCodePackage_Edit_Pendingactive : System.Web.UI.Page
{
    int QRCodePackage_ID = 0;
    public string title = "Hẹn giờ kích hoạt lô mã";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            // FillDDLddlProductBrand();
            // FillGio();
            LoadNameCodePackage();

        }
    }


    //private void FillGio()
    //{
    //    for (int i = 24; i >= 1; i--)
    //    {
    //        ddlHour.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
    //    }

    //    for (int i = 59; i >= 0; i--)
    //    {
    //        ddlMinutes.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
    //    }
    //    ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
    //    ddlMinutes.SelectedValue = DateTime.Now.Minute.ToString();
    //    txtKichHoat.Attributes.Add("readonly", "true");
    //    //txtHSD.Attributes.Add("readonly", "true");
    //    //txtNgayDukien.Attributes.Add("readonly", "true");
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateQRPendingactive();

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void LoadNameCodePackage()
    {
        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        if (QRCodePackage_ID != 0)
        {
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                txtName.Text = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                if (!_QRCodePackageRow.IsActiveDateNull)
                {
                    txtDayKichHoat.Text = _QRCodePackageRow.ActiveDate.ToString("dd/MM/yyyy");
                    timepicker.Text = DateTime.Parse(_QRCodePackageRow.ActiveDate.ToString()).ToString("HH:mm");
                }

                //if (_QRCodePackageRow.SoundEnable == true)
                //{
                //    ckActive.Checked = true;
                //}
                //else
                //{
                //    ckActive.Checked = false;
                //}
            }
        }
    }
    
    protected void UpdateQRPendingactive()
    {

        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        if (QRCodePackage_ID != 0)
        {
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                if (!string.IsNullOrEmpty(txtDayKichHoat.Text.Trim()))
                {
                    DateTime s = DateTime.ParseExact(txtDayKichHoat.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Convert.ToInt32(timepicker.Text.Split(':').First())).AddMinutes(Convert.ToInt32(timepicker.Text.Split(':').Last()));
                    _QRCodePackageRow.ActiveDate = s;
                }


                //if (ckActive.Checked)
                //{
                //    _QRCodePackageRow.SoundEnable = true;
                //}
                //else
                //{
                //    _QRCodePackageRow.SoundEnable = false;
                //}
                _QRCodePackageRow.LastEditBy = MyUser.GetUser_ID();
                _QRCodePackageRow.LastEditDate = DateTime.Now;
                BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                LoadNameCodePackage();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_List.aspx", false);
    }
}