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

public partial class QRCodePackeWarehouseregister_Add : System.Web.UI.Page
{
   
    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";

    public int QRCodePackage_ID = 0;
    public int QRCodeWarehouseRegister_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(),out QRCodePackage_ID);
        if (!string.IsNullOrEmpty(Request["QRCodeWarehouseRegister_ID"]))
            int.TryParse(Request["QRCodeWarehouseRegister_ID"].ToString(), out QRCodeWarehouseRegister_ID);

        if (!IsPostBack)
        {
            // FillDDLddlProductBrand();
            LoadQRCodeCustomerRegister();
            FillWarehouse();

        }
        LoadData();
    }



    protected void FillWarehouse()
    {
        string where = "";
        if (Common.GetFunctionGroupDN())
        {
            where = "and ProductBrand_ID =" + Convert.ToInt32(MyUser.GetProductBrand_ID());
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable("Active=1" + where, "");
        ddlWarehouse.DataSource = dt;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "Warehouse_ID";
        ddlWarehouse.DataBind();
        ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (QRCodePackage_ID != 0)
                {
                    AddQRCodePackeWarehouseregister();
                }
                if (QRCodeWarehouseRegister_ID != 0)
                {
                    UpdateQRCodeCustomerRegister();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    private void AddQRCodePackeWarehouseregister()
    {
        QRCodeWarehouseRegisterRow _QRCodeWarehouseRegisterRow = new QRCodeWarehouseRegisterRow();

        if (ddlWarehouse.SelectedValue != "")
        {
            _QRCodeWarehouseRegisterRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
        }
        _QRCodeWarehouseRegisterRow.SerialNumberStart = txtStart.Text;
        _QRCodeWarehouseRegisterRow.SerialNumberEnd = txtEnd.Text;
        _QRCodeWarehouseRegisterRow.SerialNumberList = txtList.Text;
        _QRCodeWarehouseRegisterRow.QRCodePackage_ID = QRCodePackage_ID;


        if (rdoLienMach.Checked)
        {
            _QRCodeWarehouseRegisterRow.QRCodeSplitType_ID = 1;
            _QRCodeWarehouseRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeWarehouseRegisterRow.SerialNumberStart, _QRCodeWarehouseRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
        }
        else
        {
            _QRCodeWarehouseRegisterRow.QRCodeSplitType_ID = 2;
            _QRCodeWarehouseRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
        }
        _QRCodeWarehouseRegisterRow.CreateBy = MyUser.GetUser_ID();
        _QRCodeWarehouseRegisterRow.CreateDate = DateTime.Now;

        BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().Insert(_QRCodeWarehouseRegisterRow);
        if (!_QRCodeWarehouseRegisterRow.IsQRCodeWarehouseRegister_IDNull)
        {
            Response.Redirect("QRCodePackeWarehouseregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        }
    }


    private void UpdateQRCodeCustomerRegister()
    {

        QRCodeWarehouseRegisterRow _QRCodeWarehouseRegisterRow = new QRCodeWarehouseRegisterRow();
        _QRCodeWarehouseRegisterRow = BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().GetByPrimaryKey(QRCodeWarehouseRegister_ID);
        if (_QRCodeWarehouseRegisterRow != null)
        {
            if (ddlWarehouse.SelectedValue != "")
            {
                _QRCodeWarehouseRegisterRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
               
            }
            _QRCodeWarehouseRegisterRow.SerialNumberStart = txtStart.Text;
            _QRCodeWarehouseRegisterRow.SerialNumberEnd = txtEnd.Text;
            _QRCodeWarehouseRegisterRow.SerialNumberList = txtList.Text;

            if (rdoLienMach.Checked)
            {
                _QRCodeWarehouseRegisterRow.QRCodeSplitType_ID = 1;
                _QRCodeWarehouseRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeWarehouseRegisterRow.SerialNumberStart, _QRCodeWarehouseRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
            }
            else
            {
                _QRCodeWarehouseRegisterRow.QRCodeSplitType_ID = 2;
                _QRCodeWarehouseRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
            }

            _QRCodeWarehouseRegisterRow.LastEditBy = MyUser.GetUser_ID();
            _QRCodeWarehouseRegisterRow.LastEditDate = DateTime.Now;
            
            BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().Update(_QRCodeWarehouseRegisterRow);
            if (!_QRCodeWarehouseRegisterRow.IsQRCodeWarehouseRegister_IDNull)
            {
                Response.Redirect("QRCodePackeWarehouseregister_List.aspx?QRCodePackage_ID=" + _QRCodeWarehouseRegisterRow.QRCodePackage_ID, false);
            }
        }
    }


    private void LoadQRCodeCustomerRegister()
    {
        if (QRCodeWarehouseRegister_ID != 0)
        {
            QRCodeWarehouseRegisterRow _QRCodeWarehouseRegisterRow = new QRCodeWarehouseRegisterRow();
            _QRCodeWarehouseRegisterRow = BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().GetByPrimaryKey(QRCodeWarehouseRegister_ID);
            if (_QRCodeWarehouseRegisterRow != null)
            {
                ddlWarehouse.SelectedValue = _QRCodeWarehouseRegisterRow.IsWarehouse_IDNull ? "" : _QRCodeWarehouseRegisterRow.Warehouse_ID.ToString();
                //TotalTem = _QRCodeCustomerRegisterRow.IsQRCodeNumberNull ? string.Empty : _QRCodeCustomerRegisterRow.QRCodeNumber.ToString();
                //SerialStart =_QRCodeCustomerRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberStart;
                //SerialEnd = _QRCodeCustomerRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberEnd;

                txtStart.Text = _QRCodeWarehouseRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeWarehouseRegisterRow.SerialNumberStart;
                txtEnd.Text = _QRCodeWarehouseRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeWarehouseRegisterRow.SerialNumberEnd;
                QRCodePackage_ID = _QRCodeWarehouseRegisterRow.IsQRCodePackage_IDNull ? 0 : _QRCodeWarehouseRegisterRow.QRCodePackage_ID;

                if (!_QRCodeWarehouseRegisterRow.IsSerialNumberListNull)
                {
                    txtList.Text = _QRCodeWarehouseRegisterRow.SerialNumberList;
                }

                if (Convert.ToInt32(_QRCodeWarehouseRegisterRow.QRCodeSplitType_ID) == 1)
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
        Response.Redirect("QRCodePackeWarehouseregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        if (QRCodeWarehouseRegister_ID != 0)
        {
            QRCodeWarehouseRegisterRow _QRCodeWarehouseRegisterRow = new QRCodeWarehouseRegisterRow();
            _QRCodeWarehouseRegisterRow = BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().GetByPrimaryKey(QRCodeWarehouseRegister_ID);
            if (_QRCodeWarehouseRegisterRow != null)
            {
                Response.Redirect("QRCodePackeWarehouseregister_List.aspx?QRCodePackage_ID=" + _QRCodeWarehouseRegisterRow.QRCodePackage_ID, false);
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