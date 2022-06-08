﻿using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_QRCodePackage_QRCodePackageChange_Edit : System.Web.UI.Page
{
    public string title = "Thông tin cấp tem";
    public int QRCodePackage_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackageChange_ID"]))
            int.TryParse(Request["QRCodePackageChange_ID"].ToString(), out QRCodePackage_ID);
        //if (MyUser.GetFunctionGroup_ID() == "5")
        //{
        //    rdoProduct.Checked = true;
        //    product.Visible = true;
        //    productPackage.Visible = false;
        //    rdoProduct.Enabled = false;
        //    rdoProductPackage.Enabled = false;
        //    rdoKhongLienMach.Enabled = false;
        //}
        //else
        //{
        //    if (rdoProductPackage.Checked)
        //    {
        //        product.Visible = false;
        //        productPackage.Visible = true;
        //    }
        //    if (rdoProduct.Checked)
        //    {
        //        product.Visible = true;
        //        productPackage.Visible = false;
        //    }
        //}
        if (!IsPostBack)
        {
            //if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            //{
            //    if (MyUser.GetProductBrandRole_ID() == "1")
            //    {
            //        btnSave.Text = "Gửi yêu cầu cấp tem";
            //    }
            //    else
            //    {
            //        Role.Visible = false;
            //    }
            //}
            //else
            //{
            //    Role.Visible = false;
            //}
            LoadProductCategory();
            FillDDLddlProductBrand();
            LoadProductPackage();
            LoadProduct();
            LoadSupplier();
            FillGio();
            LoadData();
        }

    }
    private void LoadData()
    {
        try
        {
            if (QRCodePackage_ID != 0)
            {
                QRCodePackageChangeRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageChangeBO().GetByPrimaryKey(QRCodePackage_ID);
                if (_QRCodePackageRow != null)
                {
                    if (_QRCodePackageRow.IsProductPackage_IDNull)
                    {
                        rdoProduct.Checked = true;
                        rdoProductPackage.Checked = false;
                        rdoProductPackage.Enabled = false;
                        rdoKhongLienMach.Enabled = false;
                        product.Visible = true;
                        productPackage.Visible = false;
                        ddlProduct.SelectedValue = _QRCodePackageRow.Product_ID.ToString();
                    }
                    else
                    {
                        ddlProductPackage.SelectedValue = _QRCodePackageRow.ProductPackage_ID.ToString();
                        rdoProductPackage.Checked = true;
                        rdoProduct.Checked = false;
                        rdoProduct.Enabled = false;
                        rdoKhongLienMach.Enabled = false;
                        product.Visible = false;
                        productPackage.Visible = true;
                    }
                    txtName.Text = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                    txtAmount.Text = _QRCodePackageRow.IsQRCodeNumberNull ? "0" : _QRCodePackageRow.QRCodeNumber.ToString();
                    txtAmount.Enabled = false;
                    txtLength.Enabled = false;
                    ckSound.Checked = _QRCodePackageRow.SoundEnable;
                    ckSMS.Checked = _QRCodePackageRow.SMS;
                    ddlProductBrand.SelectedValue = _QRCodePackageRow.ProductBrand_ID.ToString();
                    txtKhoiLuong.Text = _QRCodePackageRow.IsAmountSumNull ? "0" : _QRCodePackageRow.AmountSum.ToString();
                    ddlSupplier.SelectedValue = _QRCodePackageRow.IsSupplier_IDNull ? "0" : _QRCodePackageRow.Supplier_ID.ToString();
                    ddlProductBrand.Enabled = false;
                    ddlTemType.SelectedValue = _QRCodePackageRow.QRCodePackageType_ID.ToString();
                    ddlTemType.Enabled = false;
                    txtNote.Text = _QRCodePackageRow.IsDescriptionNull ? string.Empty : _QRCodePackageRow.Description;
                    txtHistoryProductPackage.Text = _QRCodePackageRow.IsProfileQRCodeNull ? string.Empty : _QRCodePackageRow.ProfileQRCode;
                    txtSX.Text = _QRCodePackageRow.IsManufactureDateNull ? string.Empty : _QRCodePackageRow.ManufactureDate.ToString("dd/MM/yyyy");
                    txtThuHoach.Text = _QRCodePackageRow.IsHarvestDateNull ? string.Empty : _QRCodePackageRow.HarvestDate.ToString("dd/MM/yyyy");
                    txtHSD.Text = _QRCodePackageRow.IsWarrantyEndDateNull ? string.Empty : _QRCodePackageRow.WarrantyEndDate.ToString("dd/MM/yyyy");
                    txtNgayDukien.Text = _QRCodePackageRow.IsStampDateNull ? string.Empty : _QRCodePackageRow.StampDate.ToString("dd/MM/yyyy");

                    if (!_QRCodePackageRow.IsStampDateNull)
                    {
                        ddlHour.SelectedValue = _QRCodePackageRow.StampDate.Hour.ToString();
                        ddlMinutes.SelectedValue = _QRCodePackageRow.StampDate.Minute.ToString();
                    }
                    txtChange.Text = _QRCodePackageRow.QRCodePackageChange_Note.ToString();
                    txtLength.Text = _QRCodePackageRow.Length.ToString();
                    if (_QRCodePackageRow.QRCodePackageChange_Status.ToString() == "1")
                    {
                        btnSave.Visible = false;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }
    private void LoadSupplier()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() != "1")
            {
                if (!string.IsNullOrEmpty(MyUser.GetProductBrand_ID()))
                {
                    where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID(); ;
                }
            }

            DataTable dt = BusinessRulesLocator.GetSupplierBO().GetAsDataTable(" Active=1 " + where, " Name ASC");
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataValueField = "Supplier_ID";
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-- Chọn Đơn vị sơ chế và đóng gói thành phẩm --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadSupplier", ex.ToString());
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
            else
            {
                if (ddlProductBrand.SelectedValue != "")
                {
                    where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                }
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackage_ID from ProductPackage where 1=1 " + where + " order by ProductPackage_ID DESC");
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
    protected void LoadProductCategory()
    {
        try
        {
            ddlProducCategory.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
            ddlProducCategory.DataTextField = "Name";
            ddlProducCategory.DataValueField = "ProductCategory_ID";
            ddlProducCategory.DataBind();
            ddlProducCategory.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    private void FillGio()
    {
        for (int i = 23; i >= 0; i--)
        {
            ddlHour.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }

        for (int i = 59; i >= 0; i--)
        {
            ddlMinutes.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinutes.SelectedValue = DateTime.Now.Minute.ToString();
        txtSX.Attributes.Add("readonly", "true");
        txtHSD.Attributes.Add("readonly", "true");
        txtNgayDukien.Attributes.Add("readonly", "true");
        txtThuHoach.Attributes.Add("readonly", "true");
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, "");
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
    public string GenerateProductPackageName()
    {
        string ProductName = "Lo-chua-xac-dinh" + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + "-" + txtAmount.Text;
        return ProductName;
    }
    public string GenerateProductPackageNameIdent()
    {
        string ProductName = ddlProductPackage.SelectedItem.Text + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + "-" + txtAmount.Text;
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
            string msg = string.Empty;
            if (Page.IsValid)
            {
                QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
                QRCodePackageChangeRow _QRCodePackageChangeRow = BusinessRulesLocator.GetQRCodePackageChangeBO().GetByPrimaryKey(QRCodePackage_ID);
                _QRCodePackageChangeRow.QRCodePackageType_ID = _QRCodePackageRow.QRCodePackageType_ID = Convert.ToInt32(ddlTemType.SelectedValue);
                _QRCodePackageChangeRow.ProductBrand_ID = _QRCodePackageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                if (rdoProductPackage.Checked)
                {
                    if (ddlProductPackage.SelectedValue != "")
                    {
                        if (ddlProductPackage.SelectedValue == "-1")
                        {
                            _QRCodePackageChangeRow.ProductPackage_ID = _QRCodePackageRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                            _QRCodePackageChangeRow.Product_ID = _QRCodePackageRow.Product_ID = -1;
                            _QRCodePackageChangeRow.ProductName = _QRCodePackageRow.ProductName = "#Sản phẩm chưa xác định#";
                            _QRCodePackageChangeRow.Name = _QRCodePackageRow.Name = GenerateProductPackageName();
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
                                        _QRCodePackageChangeRow.Product_ID = _QRCodePackageRow.Product_ID = _ProductPackageRow.Product_ID;
                                        _QRCodePackageChangeRow.ProductName = _QRCodePackageRow.ProductName = _ProductPackageRow.IsProductNameNull ? string.Empty : _ProductPackageRow.ProductName;
                                    }
                                }
                            }
                            _QRCodePackageChangeRow.Name = _QRCodePackageRow.Name = GenerateProductPackageNameIdent();// 
                        }
                    }
                }
                if (rdoProduct.Checked)
                {
                    if (ddlProduct.SelectedValue != "")
                    {
                        _QRCodePackageChangeRow.Product_ID = _QRCodePackageRow.Product_ID = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                        _QRCodePackageChangeRow.ProductName = _QRCodePackageRow.ProductName = ddlProduct.SelectedItem.Text;
                        _QRCodePackageChangeRow.Name = _QRCodePackageRow.Name = GenerateProductPackageNameIdent1();
                    }
                    else
                    {
                        _QRCodePackageChangeRow.Product_ID = _QRCodePackageRow.Product_ID = -1;
                        _QRCodePackageChangeRow.ProductName = _QRCodePackageRow.ProductName = "#Sản phẩm chưa xác định#";
                        _QRCodePackageChangeRow.Name = _QRCodePackageRow.Name = GenerateProductPackageName();
                    }
                }
                _QRCodePackageChangeRow.Store_ID = _QRCodePackageRow.Store_ID = 0;
                _QRCodePackageChangeRow.Factory_ID = _QRCodePackageRow.Factory_ID = 0;
                _QRCodePackageChangeRow.Description = _QRCodePackageRow.Description = txtNote.Text;
                _QRCodePackageChangeRow.SoundEnable = _QRCodePackageRow.SoundEnable = ckSound.Checked;
                _QRCodePackageChangeRow.SMS = _QRCodePackageRow.SMS = ckSMS.Checked;
                _QRCodePackageChangeRow.Active = _QRCodePackageRow.Active = true;
                _QRCodePackageChangeRow.QRCodeStatus_ID = _QRCodePackageRow.QRCodeStatus_ID = -2;
                _QRCodePackageChangeRow.ViewCount = _QRCodePackageRow.ViewCount = 0;
                _QRCodePackageChangeRow.SellCount = _QRCodePackageRow.SellCount = 0;
                _QRCodePackageChangeRow.QRCodeNumber = _QRCodePackageRow.QRCodeNumber = Convert.ToInt32(txtAmount.Text);
                _QRCodePackageChangeRow.QRCodeUsed = _QRCodePackageRow.QRCodeUsed = 0;
                _QRCodePackageChangeRow.QRCodeAvailable = _QRCodePackageRow.QRCodeAvailable = Convert.ToInt32(txtAmount.Text);
                _QRCodePackageRow.CreateBy = _QRCodePackageChangeRow.CreateBy;
                _QRCodePackageChangeRow.CreateDate = _QRCodePackageRow.CreateDate = DateTime.Now;
                _QRCodePackageChangeRow.LastEditBy = _QRCodePackageRow.LastEditBy = MyUser.GetUser_ID();
                _QRCodePackageChangeRow.LastEditDate = _QRCodePackageRow.LastEditDate = DateTime.Now;
                _QRCodePackageChangeRow.ProfileQRCode = _QRCodePackageRow.ProfileQRCode = txtHistoryProductPackage.Text;
                if (!string.IsNullOrEmpty(txtKhoiLuong.Text))
                {
                    _QRCodePackageChangeRow.AmountSum = _QRCodePackageRow.AmountSum = Convert.ToInt32(txtKhoiLuong.Text.Replace(",", "").Replace(",", ""));
                }
                if (ddlSupplier.SelectedValue != "0")
                {
                    _QRCodePackageChangeRow.Supplier_ID = _QRCodePackageRow.Supplier_ID = Convert.ToInt32(ddlSupplier.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtSX.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtSX.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _QRCodePackageChangeRow.ManufactureDate = _QRCodePackageRow.ManufactureDate = ngaycap;
                }
                if (!string.IsNullOrEmpty(txtThuHoach.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtThuHoach.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _QRCodePackageChangeRow.HarvestDate = _QRCodePackageRow.HarvestDate = ngaycap;
                }
                if (!string.IsNullOrEmpty(txtHSD.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtHSD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _QRCodePackageChangeRow.WarrantyEndDate = _QRCodePackageRow.WarrantyEndDate = ngaycap;
                }
                if (!string.IsNullOrEmpty(txtNgayDukien.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtNgayDukien.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                    ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                    ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);
                    _QRCodePackageChangeRow.StampDate = _QRCodePackageRow.StampDate = ngaycap;
                }
                _QRCodePackageChangeRow.FarmGroupInter_ID = _QRCodePackageRow.FarmGroupInter_ID = 0;
                _QRCodePackageChangeRow.FarmGroup_ID = _QRCodePackageRow.FarmGroup_ID = 0;
                _QRCodePackageChangeRow.Length = string.IsNullOrEmpty(txtLength.Text) ? 0 : Convert.ToInt32(txtLength.Text);
                _QRCodePackageChangeRow.QRCodePackageChange_ApprovedNote = txtNoteChange.Text;
                _QRCodePackageChangeRow.QRCodePackageChange_Status = 1;
                _QRCodePackageChangeRow.QRCodePackageChange_Date = DateTime.Now;
                _QRCodePackageChangeRow.QRCodePackageChange_By = MyUser.GetUser_ID();
                BusinessRulesLocator.GetQRCodePackageChangeBO().Update(_QRCodePackageChangeRow);
                BusinessRulesLocator.GetQRCodePackageBO().Insert(_QRCodePackageRow);

                if (!_QRCodePackageRow.IsQRCodePackage_IDNull)
                {
                    //Sinh mã 
                    //Sản phẩm không xác định
                    if (ddlProductPackage.SelectedValue == "-1")
                    {
                        //Tem công khai
                        if (ddlTemType.SelectedValue == "1")
                        {
                            BusinessRulesLocator.Conllection().QRCodePublicAnonymousCreate_V2(_QRCodePackageRow.QRCodeNumber, Convert.ToInt32(txtLength.Text), -1, Convert.ToInt32(ddlProductBrand.SelectedValue), _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                        }
                        else
                        {
                            BusinessRulesLocator.Conllection().QRCodeSecretAnonymousCreate_V2(_QRCodePackageRow.QRCodeNumber, Convert.ToInt32(txtLength.Text), -1, Convert.ToInt32(ddlProductBrand.SelectedValue), _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, _QRCodePackageRow.SMS, MyUser.GetUser_ID());
                        }
                    }
                    else
                    {
                        //Tem công khai
                        if (ddlTemType.SelectedValue == "1")
                        {
                            BusinessRulesLocator.Conllection().QRCodePublicCreate_V2(_QRCodePackageRow.QRCodeNumber, Convert.ToInt32(txtLength.Text), _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                        }
                        else
                        {
                            BusinessRulesLocator.Conllection().QRCodeSecretCreate_V2(_QRCodePackageRow.QRCodeNumber, Convert.ToInt32(txtLength.Text), _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, _QRCodePackageRow.SMS, MyUser.GetUser_ID());
                            //  BusinessRulesLocator.Conllection().QRCodePublicCreate_V2(1, Convert.ToInt32(txtLength.Text), _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                            //BusinessRulesLocator.Conllection().QRCodePublicPrimaryCreate(_QRCodePackageRow.Product_ID, _QRCodePackageRow.SoundEnable,_QRCodePackageRow.CreateBy, "");
                        }
                    }
                    // update serial đầu cuối
                    BusinessRulesLocator.Conllection().QRCodePackageUpdateSerialNumber(_QRCodePackageRow.QRCodePackage_ID.ToString());
                }

                //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                NotificationRow _NotificationRow = new NotificationRow();
                _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu cấp tem";
                _NotificationRow.Summary = _QRCodePackageChangeRow.QRCodePackageChange_ApprovedNote;
                _NotificationRow.Body = _QRCodePackageChangeRow.QRCodePackageChange_ID.ToString();
                _NotificationRow.NotificationType_ID = 14;
                _NotificationRow.UserID = _QRCodePackageChangeRow.QRCodePackageChange_By;
                if (MyUser.GetFunctionGroup_ID() != "1")
                    _NotificationRow.ProductBrand_ID = _QRCodePackageChangeRow.ProductBrand_ID;
                //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRow.Url = "/QRCodePackage/QRCodePackage_Edit?QRCodePackage_ID=" + _QRCodePackageRow.QRCodePackage_ID;
                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                _NotificationRow.CreateDate = DateTime.Now;
                _NotificationRow.Active = 1;
                _NotificationRow.Alias = Guid.NewGuid();
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                lblMessage.Text = ("Duyệt yêu cầu cấp tem thành công !");
            }
            lblMessage.Visible = true;
            //  Response.Redirect("/Admin/Notification/RequestProductBrand_List?update=true", false);
            Admin_Template_CMS master = this.Master as Admin_Template_CMS;
            if (master != null)
                master.LoadNotification();
            Response.Redirect("/Admin/Notification/RequestQRCodePackage_List.aspx?update=sc", false);
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void ddlProducCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();
    }
    protected void ddlProducBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();
        LoadProductPackage();
    }
    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();

    }

    protected void ddlProductPackage_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ProductPackageRow _ProductPackageRow = new ProductPackageRow();
        if (ddlProductPackage.SelectedValue != "")
        {
            if (Convert.ToInt32(ddlProductPackage.SelectedValue) > 0)
            {
                _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackage.SelectedValue));
                if (_ProductPackageRow != null)
                {
                    DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select ISNULL( SUM(QRCodeNumber),0) as Total from QRCodePackage where ProductPackage_ID=" + Convert.ToInt32(ddlProductPackage.SelectedValue) + " and Product_ID>0");
                    if (((_ProductPackageRow.ItemCount) - Convert.ToInt32(dt.Rows[0]["Total"].ToString())) > 0)
                    {
                        lblNote.Text = "Tổng <b>" + _ProductPackageRow.ItemCount.ToString() + "</b> cá thể. Còn lại <b>" + ((_ProductPackageRow.ItemCount) - Convert.ToInt32(dt.Rows[0]["Total"].ToString())) + "</b> cá thể chưa được cấp tem.";
                    }
                    else
                    {
                        lblNote.Text = "";
                    }

                    //Response.Write(ddlProductPackage.SelectedValue);
                }
            }
            else
            {
                lblNote.Text = "";
            }
        }
        else
        {
            lblNote.Text = "";
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/Notification/RequestQRCodePackage_List.aspx", false);
    }
}