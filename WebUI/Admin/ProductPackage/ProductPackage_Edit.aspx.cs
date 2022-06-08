﻿using DbObj;
using evointernal;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class ProductPackage_Edit : System.Web.UI.Page
{
    public string title = "Sửa thông tin lô";
    public string avatar = "";
    public int ProductPackage_ID = 0;
    public string titleEnd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (MyUser.GetFunctionGroup_ID() == "4" || MyUser.GetFunctionGroup_ID() == "6")
        {
            Nhom2.Visible = false;
        }

        if (!IsPostBack)
        {
            if (AddLoSanXuat.Checked)
            {
                QTCN.Visible = false;
                gropsx.Visible = true;
                UpdatePanel1.Visible = true;
                UpdatePanel2.Visible = false;
                NV.Visible = true;
                oneproduct.Visible = true;
                muntipleproduct.Visible = false;
            }
            else if (AddLoCheBien.Checked)
            {
                QTCN.Visible = true;
                gropsx.Visible = false;
                UpdatePanel1.Visible = false;
                UpdatePanel2.Visible = true;
                NV.Visible = false;
                oneproduct.Visible = false;
                muntipleproduct.Visible = true;
            }
            LoadProductPackage();
            QRCode();

        }

        lblMessage.Text = "";
        lblMessage.Visible = false;
        CheckUserXuanHoa();
    }
    protected void CheckUserXuanHoa()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select UserId,UserName from aspnet_Users where UserId = '" + MyUser.GetUser_ID() + "'");
        if (dt.Rows.Count > 0)
        {
            if (MyUser.GetFunctionGroup_ID() == "2" && MyUser.GetProductBrand_ID() == "1524")
            {
                trong.Visible = false;
                XuanHoa.Visible = true;
                titleEnd = "Ngày kết thúc";
                NV.Visible = false;
            }
            else
            {
                trong.Visible = true;
                XuanHoa.Visible = false;
                titleEnd = "Ngày dự kiến thu hoạch";
                NV.Visible = true;
            }
        }
    }
    protected void HideNgayTrong(int ProductBrand_ID)
    {

        if (ProductBrand_ID != 0)
        {
            ProductBrandRow _ProductBrandRow = new ProductBrandRow();
            _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
            if (_ProductBrandRow != null)
            {
                if (!_ProductBrandRow.IsBranch_IDNull)
                {
                    if (_ProductBrandRow.Branch_ID == 4)
                    {
                        trong.Visible = false;
                    }
                    else
                    {
                        CheckUserXuanHoa();
                        //trong.Visible = true;
                    }
                }
            }

        }

    }
    protected void LoadUser()
    {
        string ProductBrand_ID = ddlProductBrand.SelectedValue;
        var dtList = Membership.GetAllUsers();
        if (ProductBrand_ID != "" && ProductBrand_ID != "0")
        {
            var dtSource = Membership.GetAllUsers();
            foreach (MembershipUser item in dtSource)
            {
                UserProfile ProfileUser = UserProfile.GetProfile(item.UserName);
                if (ProfileUser.ProductBrand_ID != ProductBrand_ID && ProductBrand_ID != "")
                {
                    dtList.Remove(ProfileUser.UserName);
                }

            }
            ddlUser.DataSource = dtList;
            ddlUser.DataTextField = "UserName";
            ddlUser.DataValueField = "UserName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("-- Chọn tài khoản --", "0"));

            ddlUserGhiChep.DataSource = dtList;
            ddlUserGhiChep.DataTextField = "UserName";
            ddlUserGhiChep.DataValueField = "UserName";
            ddlUserGhiChep.DataBind();
            ddlUserGhiChep.Items.Insert(0, new ListItem("-- Chọn tài khoản --", "0"));
        }
        else
        {
            ddlUser.Items.Insert(0, new ListItem("-- Chọn tài khoản --", "0"));
            ddlUserGhiChep.Items.Insert(0, new ListItem("-- Chọn tài khoản --", "0"));
        }
    }
    private void LoadProductPackage()
    {
        try
        {
            if (ProductPackage_ID != 0)
            {
                ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                if (_ProductPackageRow != null)
                {
                    //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                    MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                    txtCode.Text = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                    txtC.Text = _ProductPackageRow.IsStorageTemperaturesNull ? "" : _ProductPackageRow.StorageTemperatures;
                    txtTenLo.Text = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                    txtSGIN.Text = _ProductPackageRow.IsSGTINNull ? "" : _ProductPackageRow.SGTIN;
                    LoadProductBrand(_ProductPackageRow.ProductBrand_ID);
                    if (_ProductPackageRow.Type.ToString() == "2")
                    {
                        AddLoCheBien.Checked = true;
                        AddLoSanXuat.Checked = false;
                        AddLoSanXuat.Enabled = false;
                        QTCN.Visible = true;
                        gropsx.Visible = false;
                        UpdatePanel1.Visible = false;
                        UpdatePanel2.Visible = true;
                        NV.Visible = false;
                        oneproduct.Visible = false;
                        muntipleproduct.Visible = true;

                        LoadQTCN();
                        ddlProductPackageProcessing.SelectedValue = _ProductPackageRow.IsManufactureTech_IDNull ? "" : _ProductPackageRow.ManufactureTech_ID.ToString();
                        LoadProductMuntiple();
                        if (!_ProductPackageRow.IsListProductPackage_IDNull)
                        {
                            string[] arrayListProduct = _ProductPackageRow.ListProductPackage_ID.Split(',');
                            foreach (string value in arrayListProduct)
                            {

                                foreach (ListItem item in ddlProductmultiple.Items)
                                {
                                    if (item.Value != "")
                                    {
                                        if (value == item.Value)
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (!_ProductPackageRow.IsStartDateNull)
                        {
                            txtStart2.Text = _ProductPackageRow.StartDate.ToString("dd/MM/yyyy");
                            timepicker.Text = (_ProductPackageRow.StartDate.Hour.ToString().Length < 2 ? "0" + _ProductPackageRow.StartDate.Hour.ToString() : _ProductPackageRow.StartDate.Hour.ToString()) + ":" + (_ProductPackageRow.StartDate.Minute.ToString().Length < 2 ? "0" + _ProductPackageRow.StartDate.Minute.ToString() : _ProductPackageRow.StartDate.Minute.ToString());
                        }
                        if (!_ProductPackageRow.IsEndDateNull)
                        {
                            txtEnd2.Text = _ProductPackageRow.EndDate.ToString("dd/MM/yyyy");
                            timepickerEnd.Text = (_ProductPackageRow.EndDate.Hour.ToString().Length < 2 ? "0" + _ProductPackageRow.EndDate.Hour.ToString() : _ProductPackageRow.EndDate.Hour.ToString()) + ":" + (_ProductPackageRow.EndDate.Minute.ToString().Length < 2 ? "0" + _ProductPackageRow.EndDate.Minute.ToString() : _ProductPackageRow.EndDate.Minute.ToString());
                        }

                    }
                    else
                    {
                        AddLoSanXuat.Checked = true;
                        AddLoCheBien.Checked = false;
                        AddLoCheBien.Enabled = false;
                        QTCN.Visible = false;
                        gropsx.Visible = true;
                        UpdatePanel1.Visible = true;
                        UpdatePanel2.Visible = false;
                        NV.Visible = true;
                        oneproduct.Visible = true;
                        muntipleproduct.Visible = false;


                        LoadProduct(_ProductPackageRow.Product_ID);
                        ddlProduct.Enabled = true;
                        ddlProduct.SelectedValue = _ProductPackageRow.Product_ID.ToString();

                    }


                    txtCaThe.Text = _ProductPackageRow.IsItemCountNull ? "0" : _ProductPackageRow.ItemCount.ToString();
                    //txtCaThe.Enabled = false;
                    txtDienTich.Text = _ProductPackageRow.IsAcreageNull ? "" : _ProductPackageRow.Acreage.ToString();
                    txtGiongSanPham.Text = _ProductPackageRow.IsCultivarNull ? "" : _ProductPackageRow.Cultivar;
                    txtDay.Text = _ProductPackageRow.IsGrowthByDateNull ? "" : _ProductPackageRow.GrowthByDate.ToString();
                    txtNangSuat.Text = _ProductPackageRow.IsExpectedProductivityNull ? "" : _ProductPackageRow.ExpectedProductivity.ToString();
                    txtSanLuong.Text = _ProductPackageRow.IsExpectedOutputNull ? "" : _ProductPackageRow.ExpectedOutput.ToString();
                    txtCamera.Text = _ProductPackageRow.IsCameraURLNull ? "" : _ProductPackageRow.CameraURL;
                    txtPO.Text = _ProductPackageRow.IsCodePONull ? "" : _ProductPackageRow.CodePO;
                    //LoadProduct(_ProductPackageRow.ProductPackageOrder_ID);
                    //ddlProduct.SelectedValue = _ProductPackageRow.Product_ID.ToString();
                    if (!_ProductPackageRow.IsStartDateNull)
                    {
                        txtStart.Text = _ProductPackageRow.StartDate.ToString("dd/MM/yyyy");
                        timepicker.Text = (_ProductPackageRow.StartDate.Hour.ToString().Length < 2 ? "0" + _ProductPackageRow.StartDate.Hour.ToString() : _ProductPackageRow.StartDate.Hour.ToString()) + ":" + (_ProductPackageRow.StartDate.Minute.ToString().Length < 2 ? "0" + _ProductPackageRow.StartDate.Minute.ToString() : _ProductPackageRow.StartDate.Minute.ToString());
                    }
                    if (!_ProductPackageRow.IsCropDateNull)
                    {
                        txtCropDate.Text = _ProductPackageRow.CropDate.ToString("dd/MM/yyyy");
                    }
                    if (!_ProductPackageRow.IsEndDateNull)
                    {
                        txtEnd.Text = _ProductPackageRow.EndDate.ToString("dd/MM/yyyy");
                        timepickerEnd.Text = (_ProductPackageRow.EndDate.Hour.ToString().Length < 2 ? "0" + _ProductPackageRow.EndDate.Hour.ToString() : _ProductPackageRow.EndDate.Hour.ToString()) + ":" + (_ProductPackageRow.EndDate.Minute.ToString().Length < 2 ? "0" + _ProductPackageRow.EndDate.Minute.ToString() : _ProductPackageRow.EndDate.Minute.ToString());
                    }

                    LoadUser();
                    if (!_ProductPackageRow.IsSecretary_UserIDNull)
                    {
                        ddlUserGhiChep.SelectedValue = MyUser.UserNameFromUser_ID(_ProductPackageRow.Secretary_UserID.ToString());
                    }
                    HideNgayTrong(_ProductPackageRow.ProductBrand_ID);
                    if (!_ProductPackageRow.IsProductPackageOrder_IDNull)
                        LoadProductPackageOrder(_ProductPackageRow.ProductPackageOrder_ID);
                    //LoadProductCategory();

                    LoadZone();
                    if (!_ProductPackageRow.IsZone_IDNull)
                        ddlZone.SelectedValue = _ProductPackageRow.Zone_ID.ToString();
                    if (MyUser.GetAccountType_ID() == "7")
                    {
                        ddlZone.Enabled = false;
                    }
                    LoadArea();
                    if (!_ProductPackageRow.IsArea_IDNull)
                        ddlArea.SelectedValue = _ProductPackageRow.Area_ID.ToString();
                    if (MyUser.GetAccountType_ID() == "8")
                    {
                        ddlZone.Enabled = false;
                        ddlArea.Enabled = false;
                    }
                    LoadWorkshop();
                    if (!_ProductPackageRow.IsWorkshop_IDNull)
                        ddlWorkshop.SelectedValue = _ProductPackageRow.Workshop_ID.ToString();
                    LoadFarm();
                    if (!_ProductPackageRow.IsFarm_IDNull)
                        ddlFarm.SelectedValue = _ProductPackageRow.Farm_ID.ToString();
                    LoadStatus();
                    ddlStatus.SelectedValue = _ProductPackageRow.ProductPackageStatus_ID.ToString();
                    if (_ProductPackageRow.ProductPackageStatus_ID == 5)
                    {
                        ddlStatus.Enabled = false;
                    }
                    else
                    {
                        ddlStatus.Enabled = true;
                    }
                    LoadCustomer();
                    if (!_ProductPackageRow.IsCustomer_IDNull)
                    {
                        string[] array = _ProductPackageRow.Customer_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddlCustomer.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    LoadSupplier();
                    if (!_ProductPackageRow.IsSupplier_IDNull)
                    {
                        string[] array = _ProductPackageRow.Supplier_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddlSupplier.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    LoadPKT();
                    //     ddlNhanVienKT.SelectedValue = _ProductPackageRow.IsTechnicalByNull ? "0" : _ProductPackageRow.TechnicalBy.ToString();
                    txtNguoiGhiChep.Text = _ProductPackageRow.IsSecretaryNull ? "" : _ProductPackageRow.Secretary;
                    txtNote.Text = _ProductPackageRow.IsDescriptionNull ? "" : _ProductPackageRow.Description;

                    FillDDLTestingCertificates();
                    if (!_ProductPackageRow.IsTestingCertificates_IDNull)
                    {
                        string[] arrayTestingCertificates_ID = _ProductPackageRow.TestingCertificates_ID.Split(',');
                        foreach (string value in arrayTestingCertificates_ID)
                        {

                            foreach (ListItem item in ddlTestingCertificates.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
    private void LoadSupplier()
    {
        try
        {
            DataTable dt = new DataTable();
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "  where ProductBrand_ID =" + ddlProductBrand.SelectedValue;

                dt = BusinessRulesLocator.Conllection().GetAllList(" select Supplier_ID, Name from Supplier " + where);
            }
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "Supplier_ID";
            ddlSupplier.DataBind();
            //  ddlCustomer.Items.Insert(0, new ListItem("-- Chọn khách hàng --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadPKT()
    {
        try
        {
            //string where = string.Empty;
            //DataTable dt = new DataTable();
            //if (ddlProductBrand.SelectedValue != "")
            //{
            //    where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            //    dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackageOrder_ID from ProductPackageOrder where ProductPackageOrderStatus_ID=2  " + where + " order by CreateDate DESC");

            //}
            //ddlProductPackageOrder.DataSource = dt;
            //ddlProductPackageOrder.DataTextField = "Name";
            //ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
            //ddlProductPackageOrder.DataBind();
            //ddlNhanVienKT.Items.Insert(0, new ListItem("-- Chọn nhân viên kỹ thuật --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadProductPackageOrder(int ProductPackageOrder_ID)
    {
        try
        {
            string where = "";
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackageOrder_ID from ProductPackageOrder where Approve=1 and Active <>-1 " + where + " order by CreateDate DESC");
            }
            ddlProductPackageOrder.DataSource = dt;
            ddlProductPackageOrder.DataTextField = "Name";
            ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
            ddlProductPackageOrder.DataBind();
            ddlProductPackageOrder.Enabled = false;
            //  ddlProductPackageOrder.Items.Insert(0, new ListItem("-- Chọn lệnh sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadFarm()
    {
        try
        {
            string where = string.Empty;
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " where ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                if (ddlZone.SelectedValue != "")
                {
                    where += " and Zone_ID =" + ddlZone.SelectedValue;
                }
                if (ddlArea.SelectedValue != "")
                {
                    where += " and Area_ID =" + ddlArea.SelectedValue;
                }
                if (ddlWorkshop.SelectedValue != "")
                {
                    where += " and Workshop_ID =" + ddlWorkshop.SelectedValue;
                }
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Farm_ID from Farm  " + where + " order by Name ASC");

            }
            ddlFarm.DataSource = dt;
            ddlFarm.DataTextField = "Name";
            ddlFarm.DataValueField = "Farm_ID";
            ddlFarm.DataBind();
            ddlFarm.Items.Insert(0, new ListItem("-- Chọn lô đất --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadCustomer()
    {
        try
        {
            DataTable dt = new DataTable();
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "  where ProductBrand_ID=" + ddlProductBrand.SelectedValue + "";

                dt = BusinessRulesLocator.Conllection().GetAllList(" select Customer_ID, Name from Customer " + where);
            }
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "Customer_ID";
            ddlCustomer.DataBind();
            //  ddlCustomer.Items.Insert(0, new ListItem("-- Chọn khách hàng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadStatus()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageStatusBO().GetAsDataTable(" ProductPackageStatus_ID<>6", "");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "ProductPackageStatus_ID";
            ddlStatus.DataBind();
            //  ddlStatus.Items.Insert(0, new ListItem("-- Chọn vùng --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadArea()
    {
        try
        {
            string where = " where 1=1 ";
            DataTable dt = new DataTable();
            if (ddlZone.SelectedValue != "")
            {
                where += "  and Zone_ID=" + ddlZone.SelectedValue;

            }
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "  and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                //if (ddlWorkshop.SelectedValue != "")
                //{
                //    where += " and WorkShop_ID like '%," + ddlWorkshop.SelectedValue + ",%'";
                //}
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Area_ID from Area  " + where + " order by Name ASC");

            }
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu vực --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadZone()
    {
        try
        {
            string where = string.Empty;
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                //if (ddlWorkshop.SelectedValue != "")
                //{
                //    where += " and WorkShop_ID like '%," + ddlWorkshop.SelectedValue + ",%'";
                //}
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Zone_ID from Zone where Active=1 " + where + " order by Name ASC");

            }
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", ""));
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
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                if (ddlZone.SelectedValue != "")
                {
                    where += " and Zone_ID =" + ddlZone.SelectedValue;
                }
                if (ddlArea.SelectedValue != "")
                {
                    where += " and Area_ID =" + ddlArea.SelectedValue;
                }
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Workshop_ID from Workshop where (Active<>-1 or Active is null) " + where + " order by Name ASC");

            }
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên, hộ sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadProduct(int Product_ID)
    {

        try
        {
            string where = string.Empty;
            //if (ddlCha.SelectedValue != "0")
            //{
            //    where += " And ProductCategory_ID=" + ddlCha.SelectedValue;
            //}
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Product_ID=" + Product_ID + " and Active=1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            //  ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
            ddlProduct.Enabled = false;

            ddlProduct_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void LoadProductBrand(int ProductBrand_ID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select ProductBrand_ID, Name from  ProductBrand where Active=1 and ProductBrand_ID=" + ProductBrand_ID + " order by Sort, Name ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            ddlProductBrand.Enabled = false;
            //ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    //protected void LoadProductCategory()
    //{
    //    try
    //    {

    //        ddlCha.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
    //        ddlCha.DataTextField = "Name";
    //        ddlCha.DataValueField = "ProductCategory_ID";
    //        ddlCha.DataBind();
    //        ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", "0"));
    //    }
    //    catch (Exception ex)
    //    {

    //        Log.writeLog("LoadUser", ex.ToString());
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (AddLoSanXuat.Checked)
                {
                    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                    if (_ProductPackageRow != null)
                    {
                        if (ddlZone.SelectedValue != "")
                        {
                            _ProductPackageRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
                        }
                        if (ddlArea.SelectedValue != "")
                        {
                            _ProductPackageRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
                        }
                        if (ddlWorkshop.SelectedValue != "")
                        {
                            _ProductPackageRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
                        }
                        if (ddlFarm.SelectedValue != "")
                        {
                            _ProductPackageRow.Farm_ID = Convert.ToInt32(ddlFarm.SelectedValue);
                        }
                        if (ddlStatus.SelectedValue != "0")
                        {
                            _ProductPackageRow.ProductPackageStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                        }
                        _ProductPackageRow.Name = string.IsNullOrEmpty(txtTenLo.Text) ? "" : txtTenLo.Text;
                        _ProductPackageRow.Description = txtNote.Text;
                        _ProductPackageRow.Cultivar = txtGiongSanPham.Text;
                        _ProductPackageRow.CameraURL = txtCamera.Text;
                        _ProductPackageRow.Secretary = txtNguoiGhiChep.Text;
                        _ProductPackageRow.StorageTemperatures = txtC.Text;
                        if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                        {
                            DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Convert.ToInt32(timepicker.Text.Split(':').First())).AddMinutes(Convert.ToInt32(timepicker.Text.Split(':').Last()));
                            _ProductPackageRow.StartDate = s;
                        }
                        if (!string.IsNullOrEmpty(txtCropDate.Text.Trim()))
                        {
                            DateTime s = DateTime.ParseExact(txtCropDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            _ProductPackageRow.CropDate = s;
                        }
                        if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
                        {
                            DateTime s = DateTime.ParseExact(txtEnd.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Convert.ToInt32(timepickerEnd.Text.Split(':').First())).AddMinutes(Convert.ToInt32(timepickerEnd.Text.Split(':').Last())); ;
                            _ProductPackageRow.EndDate = s;
                        }
                        _ProductPackageRow.LastEditBy = MyUser.GetUser_ID();
                        _ProductPackageRow.LastEditDate = DateTime.Now;
                        if (ddlUserGhiChep.SelectedValue != "0")
                        {
                            _ProductPackageRow.Secretary_UserID = MyUser.GetUser_IDByUserName(ddlUserGhiChep.SelectedValue);
                        }
                        if (!string.IsNullOrEmpty(txtDienTich.Text))
                        {
                            _ProductPackageRow.Acreage = Convert.ToInt32(txtDienTich.Text);
                        }
                        if (!string.IsNullOrEmpty(txtDay.Text))
                        {
                            _ProductPackageRow.GrowthByDate = Convert.ToInt32(txtDay.Text);
                        }
                        if (!string.IsNullOrEmpty(txtNangSuat.Text))
                        {
                            _ProductPackageRow.ExpectedProductivity = Convert.ToInt32(txtNangSuat.Text);
                        }
                        if (!string.IsNullOrEmpty(txtSanLuong.Text))
                        {
                            _ProductPackageRow.ExpectedOutput = Convert.ToInt32(txtSanLuong.Text);
                        }

                        if (!string.IsNullOrWhiteSpace(TestingCertificates_ID()))
                        {
                            _ProductPackageRow.TestingCertificates_ID = TestingCertificates_ID();
                        }
                        if (!string.IsNullOrEmpty(txtPO.Text))
                        {
                            _ProductPackageRow.CodePO = txtPO.Text;
                        }
                        _ProductPackageRow.Customer_ID = GetCustomer_ID();
                        _ProductPackageRow.Supplier_ID = GetSupplier_ID();
                        //if (ddlNhanVienKT.SelectedValue != "0")
                        //{
                        //    _ProductPackageRow.TechnicalBy = new Guid(ddlNhanVienKT.SelectedValue);
                        //}
                        //if (ddlUser.SelectedValue == "0")
                        //{
                        _ProductPackageRow.CreateBy = MyUser.GetUser_ID();
                        //}

                        _ProductPackageRow.TechnicalBy = MyUser.GetUser_ID();
                        BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                        lblMessage.Text = "Cập nhật thành công.";
                        lblMessage.Visible = true;
                        Response.Redirect("/Admin/ProductPackage/ProductPackage_List", false);
                    }
                }
                else if (AddLoCheBien.Checked)
                {
                    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                    if (_ProductPackageRow != null)
                    {
                        _ProductPackageRow.ListProductPackage_ID = AddProductMuti_ID();
                        if (ddlZone.SelectedValue != "")
                        {
                            _ProductPackageRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
                        }
                        if (ddlArea.SelectedValue != "")
                        {
                            _ProductPackageRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
                        }
                        if (ddlFarm.SelectedValue != "")
                        {
                            _ProductPackageRow.Farm_ID = Convert.ToInt32(ddlFarm.SelectedValue);
                        }
                        if (ddlWorkshop.SelectedValue != "")
                        {
                            _ProductPackageRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
                        }
                        if (ddlProductPackageProcessing.SelectedValue != "")
                        {
                            _ProductPackageRow.ManufactureTech_ID = Convert.ToInt32(ddlProductPackageProcessing.SelectedValue);
                        }
                        if (MyUser.GetFunctionGroup_ID() == "2")
                        {
                            _ProductPackageRow.ProductPackageOrder_ID = Convert.ToInt32(ddlProductPackageOrder.SelectedValue);
                        }
                        _ProductPackageRow.Description = txtNote.Text;
                        if (!string.IsNullOrEmpty(txtStart2.Text.Trim()))
                        {
                            DateTime s = DateTime.ParseExact(txtStart2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Convert.ToInt32(timepicker2.Text.Split(':').First())).AddMinutes(Convert.ToInt32(timepicker2.Text.Split(':').Last()));
                            _ProductPackageRow.StartDate = s;
                        }
                        if (!string.IsNullOrEmpty(txtEnd2.Text.Trim()))
                        {
                            DateTime s = DateTime.ParseExact(txtEnd2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Convert.ToInt32(timepickerEnd2.Text.Split(':').First())).AddMinutes(Convert.ToInt32(timepickerEnd2.Text.Split(':').Last()));
                            _ProductPackageRow.EndDate = s;
                        }

                        _ProductPackageRow.LastEditBy = _ProductPackageRow.CreateBy = MyUser.GetUser_ID();

                        _ProductPackageRow.LastEditDate = _ProductPackageRow.CreateDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(txtCaThe.Text))
                        {
                            _ProductPackageRow.ItemCount = Convert.ToInt32(txtCaThe.Text);
                        }
                        else
                        {
                            _ProductPackageRow.ItemCount = 1;
                        }
                        if (!string.IsNullOrEmpty(txtDienTich.Text))
                        {
                            _ProductPackageRow.Acreage = Convert.ToInt32(txtDienTich.Text);
                        }
                        _ProductPackageRow.TechnicalBy = MyUser.GetUser_ID();
                        _ProductPackageRow.Type = 2;
                        BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                        lblMessage.Text = "Cập nhật thành công.";
                        lblMessage.Visible = true;
                        Response.Redirect("/Admin/ProductPackage/ProductPackage_List", false);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected string GetCustomer_ID()
    {
        string Customer_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlCustomer.Items)
            {
                if (item.Selected)
                {
                    Customer_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Customer_ID))
            {
                Customer_ID = "," + Customer_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("Customer_ID", ex.ToString());
        }
        return Customer_ID;
    }
    protected string GetSupplier_ID()
    {
        string Supplier_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlSupplier.Items)
            {
                if (item.Selected)
                {
                    Supplier_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Supplier_ID))
            {
                Supplier_ID = "," + Supplier_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("Supplier_ID", ex.ToString());
        }
        return Supplier_ID;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackage_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadProductPackageOrder();
        LoadWorkshop();
        LoadZone();
        LoadArea();
        LoadFarm();
        LoadCustomer();
        LoadPKT();
        FillDDLTestingCertificates();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //int Product_ID = Convert.ToInt32(ddlProduct.SelectedValue);
            //if (Product_ID != 0)
            //{
            //    ProductRow _ProductRow = new ProductRow();
            //    _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
            //    if (_ProductRow != null)
            //    {
            //        txtDay.Text = _ProductRow.IsGrowthByDayNull ? "0" : _ProductRow.GrowthByDay.ToString();
            //        NangSuat.Value = _ProductRow.IsExpectedProductivityNull ? "0" : _ProductRow.ExpectedProductivity.ToString();
            //        txtSanLuong.Text = _ProductRow.IsExpectedOutputNull ? "0" : _ProductRow.ExpectedOutput.ToString();
            //        DienTich.Value = _ProductRow.IsAcreageNull ? "0" : _ProductRow.Acreage.ToString();
            //        Kg.Value = _ProductRow.IsExpectedProductivityDescriptionNull ? "" : _ProductRow.ExpectedProductivityDescription.ToString();
            //        if (!_ProductRow.IsGrowthByDayNull)
            //        {
            //            txtEnd.Text = DateTime.Now.AddDays(_ProductRow.GrowthByDay).ToString("dd/MM/yyyy");
            //        }
            //    }
            //}
            //else
            //{
            //    txtDay.Text = "0";
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("ddlProduct_SelectedIndexChanged", ex.ToString());
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadWorkshop();
        LoadFarm();

    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkshop();
        LoadFarm();
    }
    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadZone();
        //LoadArea();
        LoadFarm();
    }

    private void FillDDLTestingCertificates()
    {
        try
        {

            string Where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                Where += "and TC.ProductBrand_ID = " + Convert.ToInt32(ddlProductBrand.SelectedValue);

            }

            // Where += "and (GETDATE() between TC.StartDate And TC.EndDate)";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  SELECT  TC.*  
  from TestingCertificates TC   
left join ProductBrand PB on TC.ProductBrand_ID = PB.ProductBrand_ID 
where PB.Active<>-1 and TC.Active = 1 " + Where);
            ddlTestingCertificates.DataSource = dt;
            ddlTestingCertificates.DataTextField = "Name";
            ddlTestingCertificates.DataValueField = "TestingCertificates_ID";
            ddlTestingCertificates.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLTestingCertificates", ex.ToString());
        }
    }

    protected string TestingCertificates_ID()
    {
        string TestingCertificates_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlTestingCertificates.Items)
            {
                if (item.Selected)
                {
                    TestingCertificates_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(TestingCertificates_ID))
            {
                TestingCertificates_ID = "," + TestingCertificates_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return TestingCertificates_ID;
    }

    public string QRCode()
    {

        string img = string.Empty;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("http://esupplychain.vn/product-package/" + ProductPackage_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                img = "<img width='150px' class='mt-2' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
        return img;
    }
    protected void ddlProductPackageOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductPackageOrder.SelectedValue != "")
        {
            ProductPackageOrderRow _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackageOrder.SelectedValue));
            LoadProduct(Convert.ToInt32(ddlProductPackageOrder.SelectedValue));
            if (_ProductPackageOrderRow != null)
            {
                txtPO.Text = _ProductPackageOrderRow.IsCodePONull ? string.Empty : _ProductPackageOrderRow.CodePO;
                // ddlProduct.SelectedValue = _ProductPackageOrderRow.IsProduct_IDNull ? "" : _ProductPackageOrderRow.Product_ID.ToString();
                txtCaThe.Text = _ProductPackageOrderRow.IsItemCountNull ? "1" : _ProductPackageOrderRow.ItemCount.ToString();
                txtPO.Enabled = false;
                txtEnd.Text = _ProductPackageOrderRow.IsEndDateNull ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : _ProductPackageOrderRow.EndDate.ToString("dd/MM/yyyy HH:mm:ss");

            }

        }
    }
    protected void ddlProductPackageProcessing_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductPackageProcessing.SelectedValue != "")
        {
            LoadProductMuntiple();
        }
    }
    private void LoadProductMuntiple()
    {
        try
        {
            string where = string.Empty;

            //if (ddlProductBrand.SelectedValue != "")
            //{
            //    where += "And ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            //}
            if (ddlProductPackageProcessing.SelectedValue != "")
            {
                ManufactureTechRow _ManufatureTechRow = BusinessRulesLocator.GetManufactureTechBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackageProcessing.SelectedValue));
                if (_ManufatureTechRow != null)
                {
                    where += "and Product_ID in (" + _ManufatureTechRow.Product_ID.Substring(1, _ManufatureTechRow.Product_ID.Length - 2) + ")";
                }
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
            ddlProductmultiple.DataSource = dt;
            ddlProductmultiple.DataTextField = "Name";
            ddlProductmultiple.DataValueField = "Product_ID";
            ddlProductmultiple.DataBind();
            ddlProductmultiple.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductMuntiple", ex.ToString());
        }
    }
    protected void ddlProductmultiple_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductmultiple.SelectedIndex > 0)
        {
            int Count = 0;

            foreach (ListItem item in ddlProductmultiple.Items)
            {
                if (item.Selected)
                {
                    Count++;
                }
            }
            txtCaThe.Text = Count.ToString();
            txtCaThe.Enabled = false;
        }
        else
        {
            txtCaThe.Enabled = true;
        }
    }
    private void LoadQTCN()
    {
        try
        {
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ManufactureTech_ID from ManufactureTech where Active=1 and ProductBrand_ID= " + ddlProductBrand.SelectedValue + " order by CreateDate DESC");

            }
            ddlProductPackageProcessing.DataSource = dt;
            ddlProductPackageProcessing.DataTextField = "Name";
            ddlProductPackageProcessing.DataValueField = "ManufactureTech_ID";
            ddlProductPackageProcessing.DataBind();
            ddlProductPackageProcessing.Items.Insert(0, new ListItem("-- Chọn QTCN --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected string AddProductMuti_ID()
    {
        string ProductMuti_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlProductmultiple.Items)
            {
                if (item.Selected)
                {
                    ProductMuti_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ProductMuti_ID))
            {
                ProductMuti_ID = "," + ProductMuti_ID;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("AddProduct_ID", ex.ToString());
        }
        return ProductMuti_ID;
    }

}