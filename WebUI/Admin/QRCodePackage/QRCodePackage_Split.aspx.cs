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

public partial class QRCodePackage_Split : System.Web.UI.Page
{
    private int QRCodePackageSource_ID = 0;
    private int QRCodePackageType_ID = 0;
    public string title = "Phân chia lô mã vô danh";
    public string avatar = "";
    public int No = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackageSource_ID"]))
            int.TryParse(Request["QRCodePackageSource_ID"].ToString(), out QRCodePackageSource_ID);
        LoadData();
        if (!IsPostBack)
        {
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable(" UserID='" + MyUser.GetUser_ID() + "'", "");
                if (dt.Rows.Count == 1)
                {
                    ProductBrandList.Value = dt.Rows[0]["ProductBrand_ID_List"].ToString();
                }
            }
            LoadProductPackage();
            LoadWorkshop();
            LoadStatus();
            FillDDLddlProductBrand();
            LoadProduct();
            //LoadProductCategory();
            //LoadProduct();
        }
        if (MyUser.GetFunctionGroup_ID() == "5")
        {
            rdoProduct.Checked = true;
            product.Visible = true;
            productPackage.Visible = false;
            rdoProduct.Enabled = false;
            rdoProductPackage.Enabled = false;

        }
        else
        {
            if (rdoProductPackage.Checked)
            {
                product.Visible = false;
                productPackage.Visible = true;
            }
            if (rdoProduct.Checked)
            {
                product.Visible = true;
                productPackage.Visible = false;
            }
        }
    }
    public string number, start, end = string.Empty;
    private void LoadData()
    {
        try
        {
            if (QRCodePackageSource_ID > 0)
            {
                HDQRCodePackageSource_ID.Value = QRCodePackageSource_ID.ToString();
                QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackageSource_ID);
                if (_QRCodePackageRow != null)
                {
                    number = _QRCodePackageRow.QRCodeNumber.ToString();
                    start = _QRCodePackageRow.SerialNumberStart.ToString();
                    end = _QRCodePackageRow.SerialNumberEnd.ToString();
                    QRCodePackageType_ID = _QRCodePackageRow.QRCodePackageType_ID;
                }

                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList("select * from QRCodePackageFragment where QRCodePackage_ID=" + QRCodePackageSource_ID);
                rptHistory.DataSource = dt;
                rptHistory.DataBind();
                //if (!IsPostBack)
                {
                    if (!IsPostBack)
                        txtKhoiLuong.Text = txtHSD.Text = "0";
                    if (rdoSoluong.Checked)
                    {
                        txtSerialStart.Enabled = txtSerialEnd.Enabled = txtListSerial.Enabled = false;
                        txtAmount.Enabled = true;
                        if (!IsPostBack)
                            txtAmount.Text = "0";
                    }
                    if (rdoLienMach.Checked)
                    {
                        txtSerialStart.Enabled = txtSerialEnd.Enabled = true;
                        txtAmount.Text = "0";
                        txtAmount.Enabled = false;
                        txtListSerial.Enabled = false;
                    }
                    if (rdoKhongLienMach.Checked)
                    {
                        txtSerialStart.Enabled = txtSerialEnd.Enabled = false;
                        txtAmount.Text = "0";
                        txtAmount.Enabled = false;
                        txtListSerial.Enabled = true;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }
    private void LoadProduct()
    {
        try
        {
            string where = string.Empty;
            //if (ddlProducCategory.SelectedValue != "")
            //{
            //    where += " And ProductCategory_ID=" + ddlProducCategory.SelectedValue;
            //}
            //if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
            //{
            //    where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            //}
            //bool flag = false;
            //int product_ID = 0;
            //if (ddlProductPackage.SelectedValue != "" && ddlProductPackage.SelectedValue != "-1")
            //{
            //    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackage.SelectedValue));
            //    if (_ProductPackageRow != null)
            //    {
            //        if (!_ProductPackageRow.IsProduct_IDNull)
            //        {
            //            where += " and Product_ID=" + _ProductPackageRow.Product_ID;
            //            flag = true;
            //            product_ID = _ProductPackageRow.Product_ID;
            //        }
            //    }
            //}
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "And ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
            //ddlProduct.Items.Insert(1, new ListItem("#Sản phẩm chưa xác định#", "-1"));
            //if (flag)
            //{
            //    ddlProduct.SelectedValue = product_ID.ToString();
            //}



        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadWorkshop()
    {
        try
        {
            string where = string.Empty;
            DataTable dt = new DataTable();

            if (Common.GetFunctionGroupDN())
            {
                where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            }
            else if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Workshop_ID from Workshop where (Active<>-1 or Active is null) " + where + " order by Name ASC");
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên, hộ sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadWorkshop", ex.ToString());
        }
    }
    private void LoadProductPackage()
    {
        try
        {
            string where = string.Empty;
            if (Common.GetFunctionGroupDN())
            {
                where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            }
            else if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlWorkshop.SelectedValue != "")
            {
                where += "and Workshop_ID=" + ddlWorkshop.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackage_ID from ProductPackage where 1=1 and ProductPackageStatus_ID =1 " + where + " order by ProductPackage_ID DESC");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.Items.Insert(0, new ListItem("-- Chọn Lô sản xuất --", ""));
            ddlProductPackage.Items.Insert(1, new ListItem("# Lô chưa xác định #", "-1"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
    //private void LoadProduct()
    //{

    //    try
    //    {
    //        string where = string.Empty;
    //        if (ddlProducCategory.SelectedValue != "")
    //        {
    //            where += " And ProductCategory_ID=" + ddlProducCategory.SelectedValue;
    //        }
    //        if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
    //        {
    //            where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
    //        }
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
    //        ddlProduct.DataSource = dt;
    //        ddlProduct.DataTextField = "Name";
    //        ddlProduct.DataValueField = "Product_ID";
    //        ddlProduct.DataBind();
    //        ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
    //        ddlProduct.Items.Insert(1, new ListItem("#Sản phẩm chưa xác định#", "-1"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillDDLCha", ex.ToString());
    //    }
    //}
    //protected void LoadProductCategory()
    //{
    //    try
    //    {
    //        ddlProducCategory.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
    //        ddlProducCategory.DataTextField = "Name";
    //        ddlProducCategory.DataValueField = "ProductCategory_ID";
    //        ddlProducCategory.DataBind();
    //        ddlProducCategory.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
    //    }
    //    catch (Exception ex)
    //    {

    //        Log.writeLog("LoadUser", ex.ToString());
    //    }
    //}
    private void FillDDLddlProductBrand()
    {
        try
        {
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, " ");
            }
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    private void LoadStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQRCodeStatusBO().GetAsDataTable("QRCodeStatus_ID in (-1,0,1,2) ", "");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "QRCodeStatus_ID";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadStatus", ex.ToString());
        }
    }
    public string GenerateProductPackageName()
    {
        string ProductName = "Lo-chua-xac-dinh" + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + "-" + txtAmount.Text.Replace(",", "");
        return ProductName;
    }
    public string GenerateProductPackageNameIdent()
    {
        string ProductName = ddlProductPackage.SelectedItem.Text + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + "-" + txtAmount.Text.Replace(",", "");
        return ProductName;
    }
    public string GenerateProductPackageNameIdent1()
    {
        string ProductName = Common.RemoveFont(ddlProduct.SelectedItem.Text) + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + "-" + txtAmount.Text;
        return ProductName;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
                _QRCodePackageRow.QRCodePackageType_ID = QRCodePackageType_ID;
                _QRCodePackageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                //if (ddlProductPackage.SelectedValue != "")
                //{
                //    if (ddlProductPackage.SelectedValue == "-1")
                //    {
                //        _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                //        _QRCodePackageRow.Product_ID = -1;
                //        _QRCodePackageRow.ProductName = "#Sản phẩm chưa xác định#";
                //        _QRCodePackageRow.Name = GenerateProductPackageName();
                //    }
                //    else
                //    {

                //        _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                //        if (_QRCodePackageRow.ProductPackage_ID > 0)
                //        {
                //            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(_QRCodePackageRow.ProductPackage_ID);
                //            if (_ProductPackageRow != null)
                //            {
                //                if (!_ProductPackageRow.IsProduct_IDNull)
                //                {
                //                    _QRCodePackageRow.Product_ID = _ProductPackageRow.Product_ID;
                //                    _QRCodePackageRow.ProductName = _ProductPackageRow.IsProductNameNull ? string.Empty : _ProductPackageRow.ProductName;
                //                }
                //            }
                //        }
                //        _QRCodePackageRow.Name = GenerateProductPackageNameIdent();
                //    }
                //}
                if (rdoProductPackage.Checked)
                {
                    if (ddlProductPackage.SelectedValue != "")
                    {
                        if (ddlProductPackage.SelectedValue == "-1")
                        {
                            _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                            _QRCodePackageRow.Product_ID = -1;
                            _QRCodePackageRow.ProductName = "#Sản phẩm chưa xác định#";
                            _QRCodePackageRow.Name = GenerateProductPackageName();
                        }
                        else
                        {

                            _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                            if (_QRCodePackageRow.ProductPackage_ID > 0)
                            {
                                ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(_QRCodePackageRow.ProductPackage_ID);
                                if (_ProductPackageRow != null)
                                {
                                    if (!_ProductPackageRow.IsProduct_IDNull)
                                    {
                                        _QRCodePackageRow.Product_ID = _ProductPackageRow.Product_ID;
                                        _QRCodePackageRow.ProductName = _ProductPackageRow.IsProductNameNull ? string.Empty : _ProductPackageRow.ProductName;
                                    }
                                }
                            }
                            _QRCodePackageRow.Name = GenerateProductPackageNameIdent();// 
                        }
                    }
                }
                if (rdoProduct.Checked)
                {
                    if (ddlProduct.SelectedValue != "")
                    {
                        _QRCodePackageRow.Product_ID = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                        _QRCodePackageRow.ProductName = ddlProduct.SelectedItem.Text;
                        _QRCodePackageRow.Name = GenerateProductPackageNameIdent1();
                    }
                    else
                    {
                        _QRCodePackageRow.Product_ID = -1;
                        _QRCodePackageRow.ProductName = "#Sản phẩm chưa xác định#";
                        _QRCodePackageRow.Name = GenerateProductPackageName();
                    }
                }
                _QRCodePackageRow.Store_ID = 0;
                _QRCodePackageRow.Factory_ID = 0;
                _QRCodePackageRow.Description = "";
                _QRCodePackageRow.SoundEnable = ckSound.Checked;
                _QRCodePackageRow.SMS = ckSound.Checked;
                _QRCodePackageRow.Active = true;
                _QRCodePackageRow.QRCodeStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);

                _QRCodePackageRow.ViewCount = 0;
                _QRCodePackageRow.SellCount = 0;
                _QRCodePackageRow.QRCodeNumber = Convert.ToInt32(txtAmount.Text.Replace(",", ""));
                _QRCodePackageRow.QRCodeUsed = 0;
                _QRCodePackageRow.QRCodeAvailable = Convert.ToInt32(txtAmount.Text.Replace(",", ""));
                _QRCodePackageRow.CreateBy = MyUser.GetUser_ID();
                _QRCodePackageRow.CreateDate = DateTime.Now;
                _QRCodePackageRow.LastEditBy = MyUser.GetUser_ID();
                _QRCodePackageRow.LastEditDate = DateTime.Now;
                _QRCodePackageRow.ProfileQRCode = "";
                //if (ddlProductPackage.SelectedValue!="0")
                //{
                //    _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                //}
                if (ddlWorkshop.SelectedValue != "")
                {
                    _QRCodePackageRow.FarmWorkshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
                    _QRCodePackageRow.FarmWorkshopName = ddlWorkshop.SelectedItem.Text;
                }
                _QRCodePackageRow.ExpiryByDate = Convert.ToInt32(txtHSD.Text.Replace(",", "").Replace(",", ""));
                _QRCodePackageRow.AmountSum = Convert.ToInt32(txtKhoiLuong.Text.Replace(",", "").Replace(",", ""));
                // Add Date for QRCodePackage
                _QRCodePackageRow.ManufactureDate = DateTime.Now;
                _QRCodePackageRow.HarvestDate = DateTime.Now;
                _QRCodePackageRow.StampDate = DateTime.Now;
                QRCodePackageRow QRSource = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackageSource_ID);
                //if (QRSource != null)
                //{
                //    if (!QRSource.IsManufactureDateNull)
                //    {
                //        _QRCodePackageRow.ManufactureDate = QRSource.ManufactureDate;
                //    }
                //    if (!QRSource.IsHarvestDateNull)
                //    {
                //        _QRCodePackageRow.HarvestDate = QRSource.HarvestDate;
                //    }
                //    if (!QRSource.IsWarrantyEndDateNull)
                //    {
                //        _QRCodePackageRow.WarrantyEndDate = QRSource.WarrantyEndDate;
                //    }
                //    if (!QRSource.IsStampDateNull)
                //    {
                //        _QRCodePackageRow.StampDate = QRSource.StampDate;
                //    }
                //}
                _QRCodePackageRow.FarmGroupInter_ID = 0;
                _QRCodePackageRow.FarmGroup_ID = 0;
                BusinessRulesLocator.GetQRCodePackageBO().Insert(_QRCodePackageRow);
                if (!_QRCodePackageRow.IsQRCodePackage_IDNull)
                {
                    int QRCodeSplitType_ID = 1;
                    if (rdoSoluong.Checked)
                    {
                        QRCodeSplitType_ID = 1;
                    }
                    else if (rdoLienMach.Checked)
                    {
                        QRCodeSplitType_ID = 2;
                    }
                    else
                    {
                        QRCodeSplitType_ID = 3;
                    }
                    //Công khai
                    if (_QRCodePackageRow.QRCodePackageType_ID == 1)
                    {
                        BusinessRulesLocator.Conllection().QRCodePublicSplit(Convert.ToInt32(txtAmount.Text.Replace(",", "")), txtSerialStart.Text.Trim(), txtSerialEnd.Text.Trim(), txtListSerial.Text, _QRCodePackageRow.Product_ID, QRCodePackageSource_ID, _QRCodePackageRow.QRCodePackage_ID, QRCodeSplitType_ID, 0);
                    }
                    else//Bí mật
                    {
                        BusinessRulesLocator.Conllection().QRCodeSecretSplit(Convert.ToInt32(txtAmount.Text.Replace(",", "")), txtSerialStart.Text.Trim(), txtSerialEnd.Text.Trim(), txtListSerial.Text, _QRCodePackageRow.Product_ID, QRCodePackageSource_ID, _QRCodePackageRow.QRCodePackage_ID, QRCodeSplitType_ID, 0);
                    }
                    if (_QRCodePackageRow.QRCodeStatus_ID == 2)
                    {
                        BusinessRulesLocator.Conllection().QRCodePackage_SetStatus(_QRCodePackageRow.QRCodePackage_ID, 2);
                    }
                    BusinessRulesLocator.Conllection().QRCodePackage_FragmentCalculator(QRCodePackageSource_ID);
                    Response.Redirect("QRCodePackage_List.aspx", false);
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
        Response.Redirect("QRCodePackage_List.aspx", false);
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();
        LoadProductPackage();
        LoadWorkshop();
    }
    protected void ddlWorkShop_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
        // LoadWorkshop();
    }
}