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

public partial class QRCodePackeCustomerregister_Add : System.Web.UI.Page
{
   
    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";

    private int QRCodePackage_ID = 0;
    private int QRCodeCustomerRegister_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);

        if (!string.IsNullOrEmpty(Request["QRCodeCustomerRegister_ID"]))
            int.TryParse(Request["QRCodeCustomerRegister_ID"].ToString(), out QRCodeCustomerRegister_ID);
        if (!IsPostBack)
        {
            // FillDDLddlProductBrand();
            LoadQRCodeCustomerRegister();
            FillCustomer();
        }
        
        LoadData();
    }
   

    protected void FillCustomer()
    {
        string where = "";
        if (Common.GetFunctionGroupDN())
        {
            where = "and ProductBrand_ID ="+ Convert.ToInt32(MyUser.GetProductBrand_ID());
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetCustomerBO().GetAsDataTable("1=1"+ where, "");
        ddlCustomerregister.DataSource = dt;
        ddlCustomerregister.DataTextField = "Name";
        ddlCustomerregister.DataValueField = "Customer_ID";
        ddlCustomerregister.DataBind();
        ddlCustomerregister.Items.Insert(0, new ListItem("-- Chọn khách hàng --", ""));

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if(QRCodePackage_ID != 0)
                {
                    AddQRCodeCustomerRegister();
                }
                if (QRCodeCustomerRegister_ID != 0)
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


    private void AddQRCodeCustomerRegister()
    {
        QRCodeCustomerRegisterRow _QRCodeCustomerRegisterRow = new QRCodeCustomerRegisterRow();
        
        if (ddlCustomerregister.SelectedValue != "")
        {
            _QRCodeCustomerRegisterRow.Customer_ID = Convert.ToInt32(ddlCustomerregister.SelectedValue);
            _QRCodeCustomerRegisterRow.CustomerName = ddlCustomerregister.SelectedItem.ToString();
        }
        _QRCodeCustomerRegisterRow.SerialNumberStart = txtStart.Text;
        _QRCodeCustomerRegisterRow.SerialNumberEnd = txtEnd.Text;
        _QRCodeCustomerRegisterRow.SerialNumberList = txtList.Text;
        _QRCodeCustomerRegisterRow.QRCodePackage_ID = QRCodePackage_ID;


        if (rdoLienMach.Checked)
        {
            _QRCodeCustomerRegisterRow.QRCodeSplitType_ID = 1;
            _QRCodeCustomerRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeCustomerRegisterRow.SerialNumberStart, _QRCodeCustomerRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
        }
        else
        {
            _QRCodeCustomerRegisterRow.QRCodeSplitType_ID = 2;
            _QRCodeCustomerRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
        }
        _QRCodeCustomerRegisterRow.CreateBy = MyUser.GetUser_ID();
        _QRCodeCustomerRegisterRow.CreateDate = DateTime.Now;
        
        BusinessRulesLocator.GetQRCodeCustomerRegisterBO().Insert(_QRCodeCustomerRegisterRow);
        if (!_QRCodeCustomerRegisterRow.IsQRCodeCustomerRegister_IDNull)
        {
            Response.Redirect("QRCodePackeCustomerregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        }
    }


    private void UpdateQRCodeCustomerRegister()
    {
      
        QRCodeCustomerRegisterRow _QRCodeCustomerRegisterRow = new QRCodeCustomerRegisterRow();
        _QRCodeCustomerRegisterRow = BusinessRulesLocator.GetQRCodeCustomerRegisterBO().GetByPrimaryKey(QRCodeCustomerRegister_ID);
        if (_QRCodeCustomerRegisterRow != null)
        {
            if (ddlCustomerregister.SelectedValue != "")
            {
                _QRCodeCustomerRegisterRow.Customer_ID = Convert.ToInt32(ddlCustomerregister.SelectedValue);
                _QRCodeCustomerRegisterRow.CustomerName = ddlCustomerregister.SelectedItem.ToString();
            }
            _QRCodeCustomerRegisterRow.SerialNumberStart = txtStart.Text;
            _QRCodeCustomerRegisterRow.SerialNumberEnd = txtEnd.Text;
            _QRCodeCustomerRegisterRow.SerialNumberList = txtList.Text;

            if (rdoLienMach.Checked)
            {
                _QRCodeCustomerRegisterRow.QRCodeSplitType_ID = 1;
                _QRCodeCustomerRegisterRow.QRCodeNumber = Convert.ToInt32(BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, _QRCodeCustomerRegisterRow.SerialNumberStart, _QRCodeCustomerRegisterRow.SerialNumberEnd).Rows[0]["Number"]);
            }
            else
            {
                _QRCodeCustomerRegisterRow.QRCodeSplitType_ID = 2;
                _QRCodeCustomerRegisterRow.QRCodeNumber = Convert.ToInt32(Count.Value);
            }

            _QRCodeCustomerRegisterRow.LastEditBy = MyUser.GetUser_ID();
            _QRCodeCustomerRegisterRow.LastEditDate = DateTime.Now;
           
            BusinessRulesLocator.GetQRCodeCustomerRegisterBO().Update(_QRCodeCustomerRegisterRow);
            if (!_QRCodeCustomerRegisterRow.IsQRCodeCustomerRegister_IDNull)
            {
                Response.Redirect("QRCodePackeCustomerregister_List.aspx?QRCodePackage_ID=" + _QRCodeCustomerRegisterRow.QRCodePackage_ID, false);
            }
        }
    }


    private void LoadQRCodeCustomerRegister()
    {
        if (QRCodeCustomerRegister_ID != 0)
        {
            QRCodeCustomerRegisterRow _QRCodeCustomerRegisterRow = new QRCodeCustomerRegisterRow();
            _QRCodeCustomerRegisterRow = BusinessRulesLocator.GetQRCodeCustomerRegisterBO().GetByPrimaryKey(QRCodeCustomerRegister_ID);
            if (_QRCodeCustomerRegisterRow != null)
            {
                ddlCustomerregister.SelectedValue = _QRCodeCustomerRegisterRow.IsCustomer_IDNull? "" : _QRCodeCustomerRegisterRow.Customer_ID.ToString();
                //TotalTem = _QRCodeCustomerRegisterRow.IsQRCodeNumberNull ? string.Empty : _QRCodeCustomerRegisterRow.QRCodeNumber.ToString();
                //SerialStart =_QRCodeCustomerRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberStart;
                //SerialEnd = _QRCodeCustomerRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberEnd;

                txtStart.Text = _QRCodeCustomerRegisterRow.IsSerialNumberStartNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberStart;
                txtEnd.Text = _QRCodeCustomerRegisterRow.IsSerialNumberEndNull ? string.Empty : _QRCodeCustomerRegisterRow.SerialNumberEnd;
                QRCodePackage_ID = _QRCodeCustomerRegisterRow.IsQRCodePackage_IDNull ? 0 : _QRCodeCustomerRegisterRow.QRCodePackage_ID;

                if (!_QRCodeCustomerRegisterRow.IsSerialNumberListNull)
                {
                    txtList.Text = _QRCodeCustomerRegisterRow.SerialNumberList;
                }
               
                if (Convert.ToInt32( _QRCodeCustomerRegisterRow.QRCodeSplitType_ID) ==1)
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
        Response.Redirect("QRCodePackeCustomerregister_List.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
        if (QRCodeCustomerRegister_ID != 0)
        {
            QRCodeCustomerRegisterRow _QRCodeCustomerRegisterRow = new QRCodeCustomerRegisterRow();
            _QRCodeCustomerRegisterRow = BusinessRulesLocator.GetQRCodeCustomerRegisterBO().GetByPrimaryKey(QRCodeCustomerRegister_ID);
            if (_QRCodeCustomerRegisterRow != null)
            {
                Response.Redirect("QRCodePackeCustomerregister_List.aspx?QRCodePackage_ID=" + _QRCodeCustomerRegisterRow.QRCodePackage_ID, false);
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