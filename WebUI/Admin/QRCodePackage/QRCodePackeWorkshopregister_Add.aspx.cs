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

public partial class QRCodePackeWorkshopregister_Add : System.Web.UI.Page
{
   
    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    public string Message = "";
    public int QRCodePackage_ID = 0;
    public int QRCodeWorkshopRegister_ID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!string.IsNullOrEmpty(Request["QRCodeWorkshopRegister_ID"]))
            int.TryParse(Request["QRCodeWorkshopRegister_ID"].ToString(), out QRCodeWorkshopRegister_ID);
        if (!IsPostBack)
        {
            // FillDDLddlProductBrand();
            FillWorkshop();
            LoadQRCodeWorkshopRegister();

        }
        //txtList.Enabled = false;
        LoadData();
    }

    protected void FillWorkshop()
    {
        string where = "";
        if (Common.GetFunctionGroupDN())
        {
            where = "and ProductBrand_ID =" + Convert.ToInt32(MyUser.GetProductBrand_ID());
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable("Active=1" + where, "");
        ddlWorkshop.DataSource = dt;
        ddlWorkshop.DataTextField = "Name";
        ddlWorkshop.DataValueField = "Workshop_ID";
        ddlWorkshop.DataBind();
        ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên/ hộ sản xuất --", ""));

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (QRCodePackage_ID != 0)
                {
                    AddQRCodeWorkshopRegister();
                }
                if (QRCodeWorkshopRegister_ID != 0)
                {
                    UpdateQRCodeWorkshopRegister();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    private void AddQRCodeWorkshopRegister()
    {
        QRCodeWorkshopRegisterRow _QRCodeWorkshopRegisterRow = new QRCodeWorkshopRegisterRow();

        if (ddlWorkshop.SelectedValue != "")
        {
            _QRCodeWorkshopRegisterRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
        }
        _QRCodeWorkshopRegisterRow.SerialNumberStart = txtStart.Text;
        _QRCodeWorkshopRegisterRow.SerialNumberEnd = txtEnd.Text;
        _QRCodeWorkshopRegisterRow.SerialNumberList = txtList.Text;
        _QRCodeWorkshopRegisterRow.QRCodePackage_ID = QRCodePackage_ID;


        if (rdoLienMach.Checked)
        {
            _QRCodeWorkshopRegisterRow.QRCodeRegisterType_ID = 1;
            _QRCodeWorkshopRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeWorkshopRegisterRow.SerialNumberStart, _QRCodeWorkshopRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
        }
        else
        {
            _QRCodeWorkshopRegisterRow.QRCodeRegisterType_ID = 2;
            _QRCodeWorkshopRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
        }
        _QRCodeWorkshopRegisterRow.CreateBy = MyUser.GetUser_ID();
        _QRCodeWorkshopRegisterRow.CreateDate = DateTime.Now;

        BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().Insert(_QRCodeWorkshopRegisterRow);
        if (!_QRCodeWorkshopRegisterRow.IsQRCodeWorkshopRegister_IDNull)
        {
            Response.Redirect("QRCodePackeWorkshopregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        }
    }


    private void UpdateQRCodeWorkshopRegister()
    {

        QRCodeWorkshopRegisterRow _QRCodeWorkshopRegisterRow = new QRCodeWorkshopRegisterRow();
        _QRCodeWorkshopRegisterRow = BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().GetByPrimaryKey(QRCodeWorkshopRegister_ID);
        if (_QRCodeWorkshopRegisterRow != null)
        {
            if (ddlWorkshop.SelectedValue != "")
            {
                _QRCodeWorkshopRegisterRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);

            }
            _QRCodeWorkshopRegisterRow.SerialNumberStart = txtStart.Text;
            _QRCodeWorkshopRegisterRow.SerialNumberEnd = txtEnd.Text;
            _QRCodeWorkshopRegisterRow.SerialNumberList = txtList.Text;

            if (rdoLienMach.Checked)
            {
                _QRCodeWorkshopRegisterRow.QRCodeRegisterType_ID = 1;
                _QRCodeWorkshopRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeWorkshopRegisterRow.SerialNumberStart, _QRCodeWorkshopRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
            }
            else
            {
                _QRCodeWorkshopRegisterRow.QRCodeRegisterType_ID = 2;
                _QRCodeWorkshopRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
            }

            _QRCodeWorkshopRegisterRow.LastEditBy = MyUser.GetUser_ID();
            _QRCodeWorkshopRegisterRow.LastEditDate = DateTime.Now;

            BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().Update(_QRCodeWorkshopRegisterRow);
            if (!_QRCodeWorkshopRegisterRow.IsQRCodeWorkshopRegister_IDNull)
            {
                Response.Redirect("QRCodePackeWorkshopregister_List.aspx?QRCodePackage_ID=" + _QRCodeWorkshopRegisterRow.QRCodePackage_ID, false);
            }
        }
    }


    private void LoadQRCodeWorkshopRegister()
    {
        if (QRCodeWorkshopRegister_ID != 0)
        {
            QRCodeWorkshopRegisterRow _QRCodeWorkshopRegisterRow = new QRCodeWorkshopRegisterRow();
            _QRCodeWorkshopRegisterRow = BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().GetByPrimaryKey(QRCodeWorkshopRegister_ID);
            if (_QRCodeWorkshopRegisterRow != null)
            {
                ddlWorkshop.SelectedValue = _QRCodeWorkshopRegisterRow.IsWorkshop_IDNull ? "" : _QRCodeWorkshopRegisterRow.Workshop_ID.ToString();
                //TotalTem = _QRCodeCustomerRegisterRow.IsQRCodeNumberNull ? string.Empty : _QRCodeCustomerRegisterRow.QRCodeNumber.ToString();
                //SerialStart =_QRCodeCustomerRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberStart;
                //SerialEnd = _QRCodeCustomerRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberEnd;

                txtStart.Text = _QRCodeWorkshopRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeWorkshopRegisterRow.SerialNumberStart;
                txtEnd.Text = _QRCodeWorkshopRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeWorkshopRegisterRow.SerialNumberEnd;
                QRCodePackage_ID = _QRCodeWorkshopRegisterRow.IsQRCodePackage_IDNull ? 0 : _QRCodeWorkshopRegisterRow.QRCodePackage_ID;

                if (!_QRCodeWorkshopRegisterRow.IsSerialNumberListNull)
                {
                    txtList.Text = _QRCodeWorkshopRegisterRow.SerialNumberList;
                }

                if (Convert.ToInt32(_QRCodeWorkshopRegisterRow.QRCodeRegisterType_ID) == 1)
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

            }
        }
    }

    private void LoadData()
    {
        try
        {
            if (QRCodePackage_ID != 0)
            {
                QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                NameProductPackage = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProduct = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                TotalTem = _QRCodePackageRow.QRCodeNumber.ToString();
                SerialStart = _QRCodePackageRow.SerialNumberStart;
                SerialEnd = _QRCodePackageRow.SerialNumberEnd;
                // txtList.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeWorkshopregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        if (QRCodeWorkshopRegister_ID != 0)
        {
            QRCodeWorkshopRegisterRow _QRCodeWorkshopRegisterRow = new QRCodeWorkshopRegisterRow();
            _QRCodeWorkshopRegisterRow = BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().GetByPrimaryKey(QRCodeWorkshopRegister_ID);
            if (_QRCodeWorkshopRegisterRow != null)
            {
                Response.Redirect("QRCodePackeWorkshopregister_List.aspx?QRCodePackage_ID=" + _QRCodeWorkshopRegisterRow.QRCodePackage_ID, false);
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