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

public partial class QRCodePackage_Edit_Info : System.Web.UI.Page
{
    int QRCodePackage_ID = 0;
    public string title = "Cập nhập thông tin lô mã";
    public string avatar = "";
    public string NameQRCodePackage = "";
    public string NameProduct = "";
    public string NameQRCodeStatus = "";


    public string ProduceInfoEditDate = "Đang chờ";
    public string QualityInfoEditDate = "Đang chờ";
    public string DeliveryInfoEditDate = "Đang chờ";

    public string VerifyApproveBy = "Đang chờ";
    public string VerifyApproveDate = "Đang chờ";

    public string CheckApproveBy  = "Đang chờ";
    public string CheckApproveDate = "Đang chờ";

    public string AdminApproveBy = "Đang chờ";
    public string AdminApproveDate = "Đang chờ";

    public int ProductBrand_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            //FillDDLddlProductBrand();
            //LoadWarehouse();
            //LoadInfoQRCodePackage();
            //FillQRCodePackage();
        }
        FillDDLddlProductBrand();
        LoadWarehouse();
        LoadInfoQRCodePackage();
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    //left join aspnet_Users U on U.UserId= T.CreateBy

    protected void LoadWarehouse()
    {
        try
        {
            string where = string.Empty;
            if (ProductBrand_ID != 0)
            {
                where += "and ProductBrand_ID= " + ProductBrand_ID;
            }
            ddlWarehouse.DataSource = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable(" Active=1  " + where, "");
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    private void LoadInfoQRCodePackage()
    {
        int QRCodeStatus_ID = 0;
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                NameQRCodePackage = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProduct = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                QRCodeStatus_ID = _QRCodePackageRow.QRCodeStatus_ID;
                CheckSound.Checked = _QRCodePackageRow.SoundEnable;
                switch (QRCodeStatus_ID)
                {
                    case -3:
                        NameQRCodeStatus = "Tem chờ duyệt";
                        break;
                    case -2:
                        NameQRCodeStatus = "Tem đang tạo...";
                        break;
                    case -1:
                        NameQRCodeStatus = "Tem bị hủy";
                        break;
                    case 0:
                        NameQRCodeStatus = " <span class=\"badge badge-danger\">Tem mới tạo </span>";
                        break;
                    case 1:
                        NameQRCodeStatus = "Tem đã giao cho nhà in";
                        break;
                    case 2:
                        NameQRCodeStatus = " <span class=\"badge badge-success\"> Kích hoạt đưa mã tem ra thị trường </span>";
                        break;
                    case 3:
                        NameQRCodeStatus = "Tem đã sử dụng";
                        break;

                    default:
                        // code block
                        break;
                }

                txtProduceInfo.Text = _QRCodePackageRow.IsProduceInfoNull ? string.Empty : _QRCodePackageRow.ProduceInfo;
                txtCustomerInfo.Text = _QRCodePackageRow.IsCustomerInfoNull ? string.Empty : _QRCodePackageRow.CustomerInfo;
                checkProduceInfo.Checked = _QRCodePackageRow.IsProduceInfoDisplayNull ? false : _QRCodePackageRow.ProduceInfoDisplay;

                txtHSD.Text = _QRCodePackageRow.IsWarrantyEndDateNull ? string.Empty : DateTime.Parse(_QRCodePackageRow.WarrantyEndDate.ToString()).ToString("dd/MM/yyyy");
                txtSX.Text = _QRCodePackageRow.IsManufactureDateNull ? string.Empty : DateTime.Parse(_QRCodePackageRow.ManufactureDate.ToString()).ToString("dd/MM/yyyy");

                txtQualityInfo.Text = _QRCodePackageRow.IsQualityInfoNull ? string.Empty : _QRCodePackageRow.QualityInfo;
                checkQualityInfo.Checked = _QRCodePackageRow.IsQualityInfoDisplayNull ? false : _QRCodePackageRow.QualityInfoDisplay;

                txtDeliveryInfo.Text = _QRCodePackageRow.IsDeliveryInfoNull ? string.Empty : _QRCodePackageRow.DeliveryInfo;

                CheckSound.Checked = _QRCodePackageRow.IsSoundEnableNull ? false : _QRCodePackageRow.SoundEnable;

                ddlWarehouse.SelectedValue = _QRCodePackageRow.IsWarehouse_IDNull ? "" : _QRCodePackageRow.Warehouse_ID.ToString();


                ProduceInfoEditDate = _QRCodePackageRow.IsProduceInfoEditDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.ProduceInfoEditDate.ToString()).ToString("dd/MM/yyyy");
                QualityInfoEditDate = _QRCodePackageRow.IsQualityInfoEditDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.QualityInfoEditDate.ToString()).ToString("dd/MM/yyyy");
                DeliveryInfoEditDate = _QRCodePackageRow.IsDeliveryInfoEditDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.DeliveryInfoEditDate.ToString()).ToString("dd/MM/yyyy");

                if (!_QRCodePackageRow.IsVerifyApproveByNull)
                {
                    VerifyApproveBy = MyUser.UserNameFromUser_ID(_QRCodePackageRow.VerifyApproveBy.ToString());
                }
                VerifyApproveDate = _QRCodePackageRow.IsVerifyApproveDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.VerifyApproveDate.ToString()).ToString("dd/MM/yyyy");

                if (!_QRCodePackageRow.IsCheckApproveByNull)
                {
                    CheckApproveBy = MyUser.UserNameFromUser_ID(_QRCodePackageRow.CheckApproveBy.ToString());
                }
                CheckApproveDate = _QRCodePackageRow.IsCheckApproveDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.CheckApproveDate.ToString()).ToString("dd/MM/yyyy");

                if (!_QRCodePackageRow.IsAdminApproveByNull)
                {
                    AdminApproveBy = MyUser.UserNameFromUser_ID(_QRCodePackageRow.AdminApproveBy.ToString());
                }
                AdminApproveDate = _QRCodePackageRow.IsAdminApproveDateNull ? "Đang chờ" : DateTime.Parse(_QRCodePackageRow.AdminApproveDate.ToString()).ToString("dd/MM/yyyy");

            }
            
        }

    }

    protected void FillQRCodePackage()
    {
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {

                txtProduceInfo.Text = _QRCodePackageRow.IsProduceInfoNull ? string.Empty : _QRCodePackageRow.ProduceInfo;
                txtCustomerInfo.Text = _QRCodePackageRow.IsCustomerInfoNull ? string.Empty : _QRCodePackageRow.CustomerInfo;
                checkProduceInfo.Checked = _QRCodePackageRow.IsProduceInfoDisplayNull ? false : _QRCodePackageRow.ProduceInfoDisplay;

                txtHSD.Text = _QRCodePackageRow.IsWarrantyEndDateNull ? string.Empty : DateTime.Parse(_QRCodePackageRow.WarrantyEndDate.ToString()).ToString("dd/MM/yyyy");
                txtSX.Text = _QRCodePackageRow.IsManufactureDateNull ? string.Empty : DateTime.Parse(_QRCodePackageRow.ManufactureDate.ToString()).ToString("dd/MM/yyyy");

                txtQualityInfo.Text = _QRCodePackageRow.IsQualityInfoNull ? string.Empty : _QRCodePackageRow.QualityInfo;
                checkQualityInfo.Checked = _QRCodePackageRow.IsQualityInfoDisplayNull ? false : _QRCodePackageRow.QualityInfoDisplay;

                txtDeliveryInfo.Text = _QRCodePackageRow.IsDeliveryInfoNull ? string.Empty : _QRCodePackageRow.DeliveryInfo;

                CheckSound.Checked = _QRCodePackageRow.IsSoundEnableNull ? false : _QRCodePackageRow.SoundEnable;

                ddlWarehouse.SelectedValue = _QRCodePackageRow.IsWarehouse_IDNull ? "" : _QRCodePackageRow.Warehouse_ID.ToString();

            }
        }
    }

    protected void UpdateInfo()
    {
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                _QRCodePackageRow.ProduceInfo = txtProduceInfo.Text.ToString();
                _QRCodePackageRow.CustomerInfo = txtCustomerInfo.Text.ToString();
                _QRCodePackageRow.ProduceInfoDisplay = checkProduceInfo.Checked;
                if (!string.IsNullOrEmpty(txtHSD.Text.Trim()))
                {
                    DateTime ngaySD = DateTime.ParseExact(txtHSD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _QRCodePackageRow.WarrantyEndDate = ngaySD;
                }
                if (!string.IsNullOrEmpty(txtSX.Text.Trim()))
                {
                    DateTime ngaySX = DateTime.ParseExact(txtSX.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _QRCodePackageRow.ManufactureDate = ngaySX;
                }
                _QRCodePackageRow.ProduceInfoEditDate = DateTime.Now;


                _QRCodePackageRow.QualityInfo = txtQualityInfo.Text.ToString();
                _QRCodePackageRow.QualityInfoDisplay = checkQualityInfo.Checked;
                _QRCodePackageRow.QualityInfoEditDate = DateTime.Now;

                _QRCodePackageRow.DeliveryInfo = txtDeliveryInfo.Text.ToString();
                _QRCodePackageRow.DeliveryInfoEditDate = DateTime.Now;

                if(ddlWarehouse.SelectedValue != "")
                {
                    _QRCodePackageRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
                BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
            }

            BusinessRulesLocator.Conllection().QRCodePackage_SetSoundEnable(QRCodePackage_ID, CheckSound.Checked);
            lblMessage.Text = "Cập nhập thông tin lô mã thành công!";
            lblMessage.Visible = true;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateInfo();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_List.aspx", false);
    }
}