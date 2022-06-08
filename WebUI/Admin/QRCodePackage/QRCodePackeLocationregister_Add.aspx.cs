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

public partial class QRCodePackeLocationregister_Add : System.Web.UI.Page
{

    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    private int QRCodePackage_ID = 0;
    private int QRCodeLocationRegister_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);


        if (!string.IsNullOrEmpty(Request["QRCodeLocationRegister_ID"]))
            int.TryParse(Request["QRCodeLocationRegister_ID"].ToString(), out QRCodeLocationRegister_ID);
        //if (!IsPostBack)
        {
            LoadQRCodeLocationRegister();
            LoadData();
        }
    }
    private void LoadData()
    {
        try
        {
            if (QRCodePackage_ID > 0)
            {
                QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                NameProductPackage = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProduct = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                TotalTem = _QRCodePackageRow.QRCodeNumber.ToString();
                SerialStart = _QRCodePackageRow.SerialNumberStart;
                SerialEnd = _QRCodePackageRow.SerialNumberEnd;
                txtList.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }



    private void LoadQRCodeLocationRegister()
    {
        if (QRCodeLocationRegister_ID != 0)
        {
            QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = new QRCodeLocationRegisterRow();
            _QRCodeLocationRegisterRow = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetByPrimaryKey(QRCodeLocationRegister_ID);
            if (_QRCodeLocationRegisterRow != null)
            {

                //TotalTem = _QRCodeCustomerRegisterRow.IsQRCodeNumberNull ? string.Empty : _QRCodeCustomerRegisterRow.QRCodeNumber.ToString();
                //SerialStart =_QRCodeCustomerRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberStart;
                //SerialEnd = _QRCodeCustomerRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberEnd;
                txtName.Text = _QRCodeLocationRegisterRow.IsLocationRegisterNull ? string.Empty : _QRCodeLocationRegisterRow.LocationRegister;
                txtStart.Text = _QRCodeLocationRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeLocationRegisterRow.SerialNumberStart;
                txtEnd.Text = _QRCodeLocationRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeLocationRegisterRow.SerialNumberEnd;
                QRCodePackage_ID = _QRCodeLocationRegisterRow.IsQRCodePackage_IDNull ? 0 : _QRCodeLocationRegisterRow.QRCodePackage_ID;

                if (!_QRCodeLocationRegisterRow.IsSerialNumberListNull)
                {
                    txtList.Text = _QRCodeLocationRegisterRow.SerialNumberList;
                }

                if (Convert.ToInt32(_QRCodeLocationRegisterRow.QRCodeSplitType_ID) == 1)
                {
                    rdoLienMach.Checked = true;
                    rdoKhongLienMach.Checked = false;
                    txtList.Enabled = false;
                    txtStart.Enabled = true;
                    txtEnd.Enabled = true;
                }
                else
                {
                    rdoLienMach.Checked = false;
                    rdoKhongLienMach.Checked = true;
                    txtList.Enabled = true;
                    txtStart.Enabled = false;
                    txtEnd.Enabled = false;
                }
                ckActive.Checked = _QRCodeLocationRegisterRow.LocationFailedWarning;

            }
        }
    }

    private void AddQRCodeLocationRegister()
    {
        QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = new QRCodeLocationRegisterRow();
      
        _QRCodeLocationRegisterRow.LocationRegister = txtName.Text;
        _QRCodeLocationRegisterRow.SerialNumberStart = txtStart.Text;
        _QRCodeLocationRegisterRow.SerialNumberEnd = txtEnd.Text;
        _QRCodeLocationRegisterRow.SerialNumberList = txtList.Text;
        _QRCodeLocationRegisterRow.LocationFailedWarning = ckActive.Checked;
        _QRCodeLocationRegisterRow.QRCodePackage_ID = QRCodePackage_ID;

        if (rdoLienMach.Checked)
        {
            _QRCodeLocationRegisterRow.QRCodeSplitType_ID = 1;
            _QRCodeLocationRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeLocationRegisterRow.SerialNumberStart, _QRCodeLocationRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
        }
        else
        {
            _QRCodeLocationRegisterRow.QRCodeSplitType_ID = 2;
            _QRCodeLocationRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
        }
        _QRCodeLocationRegisterRow.CreateBy = MyUser.GetUser_ID();
        _QRCodeLocationRegisterRow.CreateDate = DateTime.Now;
      
        BusinessRulesLocator.GetQRCodeLocationRegisterBO().Insert(_QRCodeLocationRegisterRow);
        if (!_QRCodeLocationRegisterRow.IsQRCodeLocationRegister_IDNull)
        {
            Response.Redirect("QRCodePackeLocationregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        }
    }


    private void UpdateQRCodeLocationRegister()
    {
        if (QRCodeLocationRegister_ID != 0)
        {
            QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = new QRCodeLocationRegisterRow();
            _QRCodeLocationRegisterRow = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetByPrimaryKey(QRCodeLocationRegister_ID);
            if (_QRCodeLocationRegisterRow != null)
            {

                _QRCodeLocationRegisterRow.LocationRegister = txtName.Text;
                _QRCodeLocationRegisterRow.SerialNumberStart = txtStart.Text;
                _QRCodeLocationRegisterRow.SerialNumberEnd = txtEnd.Text;
                _QRCodeLocationRegisterRow.SerialNumberList = txtList.Text;
                _QRCodeLocationRegisterRow.LocationFailedWarning = ckActive.Checked;

                if (rdoLienMach.Checked)
                {
                    _QRCodeLocationRegisterRow.QRCodeSplitType_ID = 1;
                    _QRCodeLocationRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeLocationRegisterRow.SerialNumberStart, _QRCodeLocationRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
                }
                else
                {
                    _QRCodeLocationRegisterRow.QRCodeSplitType_ID = 2;
                    _QRCodeLocationRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
                }

                _QRCodeLocationRegisterRow.LastEditBy = MyUser.GetUser_ID();
                _QRCodeLocationRegisterRow.LastEditDate = DateTime.Now;
                BusinessRulesLocator.GetQRCodeLocationRegisterBO().Insert(_QRCodeLocationRegisterRow);
                if (!_QRCodeLocationRegisterRow.IsQRCodeLocationRegister_IDNull)
                {
                    Response.Redirect("QRCodePackeLocationregister_List.aspx?QRCodePackage_ID=" + _QRCodeLocationRegisterRow.QRCodePackage_ID, false);
                }
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (QRCodeLocationRegister_ID != 0)
                {
                    UpdateQRCodeLocationRegister();
                }
                if (QRCodePackage_ID != 0)
                {
                    AddQRCodeLocationRegister();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeLocationregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        if (QRCodeLocationRegister_ID != 0)
        {
            QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = new QRCodeLocationRegisterRow();
            _QRCodeLocationRegisterRow = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetByPrimaryKey(QRCodeLocationRegister_ID);
            if (_QRCodeLocationRegisterRow != null)
            {
                Response.Redirect("QRCodePackeLocationregister_List.aspx?QRCodePackage_ID=" + _QRCodeLocationRegisterRow.QRCodePackage_ID, false);
            }
        }
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        txtStart.Text = SerialStart;
        txtEnd.Text = SerialEnd;
    }

    protected void rdoLienMach_CheckedChanged(object sender, EventArgs e)
    {
        txtList.Enabled = false;
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
    }

    protected void rdoKhongLienMach_CheckedChanged(object sender, EventArgs e)
    {
        txtList.Enabled = true;
        txtStart.Enabled = false;
        txtEnd.Enabled = false;
    }
}