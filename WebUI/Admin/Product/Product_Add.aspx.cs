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

public partial class Product_Add : System.Web.UI.Page
{
    public string title = "Thêm mới sản phẩm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        _FileBrowser.SetupCKEditor(txtQuyTrinh);

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    btnSave.Text = "Gửi yêu cầu thêm mới sản phẩm";
                }
                else
                {
                    Role.Visible = false;
                }
            }
            else
            {
                Role.Visible = false;
            }
            LoadProductCategory();
            LoadProductBrand();
            FillDDLQuality();

        }
    }
    private void LoadProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            Common.FillProductBrand(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductBrand", ex.ToString());
        }
    }
    private void FillDDLQuality()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" ", " Name ASC");
            ddlTieuChuan.DataSource = dt;
            ddlTieuChuan.DataTextField = "Name";
            ddlTieuChuan.DataValueField = "Quality_ID";
            ddlTieuChuan.DataBind();
            ddlTieuChuan.Items.Insert(0, new ListItem("-- Chọn tiêu chuẩn --", "0"));
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
            //DataTable dtProductCategoryParent = new DataTable();
            //dtProductCategoryParent.Clear();
            //dtProductCategoryParent.Columns.Add("ProductCategory_ID");
            //dtProductCategoryParent.Columns.Add("Parent_ID");
            //dtProductCategoryParent.Columns.Add("Name");
            //dtProductCategoryParent.Columns.Add("Image");
            //dtProductCategoryParent.Columns.Add("Active");

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Active <>-1 ", " Sort ASC");

            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRow itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //    itemProductCategoryParent["ProductCategory_ID"] = item["ProductCategory_ID"];
            //    itemProductCategoryParent["Parent_ID"] = item["Parent_ID"];
            //    itemProductCategoryParent["Name"] = item["Name"];
            //    itemProductCategoryParent["Image"] = item["Image"];
            //    itemProductCategoryParent["Active"] = item["Active"];
            //    dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //    if (item["ProductCategory_ID"] != null)
            //    {
            //        DataTable dtChild = new DataTable();
            //        dtChild = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID =" + item["ProductCategory_ID"], " Sort ASC");
            //        if (dtChild.Rows.Count > 0)
            //        {
            //            foreach (DataRow itemChild in dtChild.Rows)
            //            {
            //                itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //                itemProductCategoryParent["ProductCategory_ID"] = itemChild["ProductCategory_ID"];
            //                itemProductCategoryParent["Parent_ID"] = item["ProductCategory_ID"];
            //                itemProductCategoryParent["Name"] = Server.HtmlDecode("&nbsp;&nbsp;&nbsp;") + " -" + itemChild["Name"];
            //                itemProductCategoryParent["Image"] = itemChild["Image"];
            //                itemProductCategoryParent["Active"] = itemChild["Active"];
            //                dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //            }

            //        }
            //    }
            //}

            ddlCha.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
            // ddlCha.DataSource = dtProductCategoryParent;
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "ProductCategory_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void UpdateProduct(int Product_ID)
    {
        try
        {
            ProductRow _ProductRow = new ProductRow();
            if (Product_ID != 0)
            {
                _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                if (_ProductRow != null)
                {

                    _ProductRow.URL = Common.ConvertTitleDomain(_ProductRow.Name + "-" + Product_ID);
                    _ProductRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductRow.LastEditDate = DateTime.Now;
                    //_ProductRow.SGTIN = "GTIN-" + _ProductRow.Product_ID.ToString();
                    //     _ProductRow.SGTIN = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        fileimage = _ProductRow.Product_ID + "_" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/product/mainimages/original/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _ProductRow.Image = fileimage;
                        }
                    }

                    BusinessRulesLocator.GetProductBO().Update(_ProductRow);
                }
                lblMessage.Text = "Thêm mới sản phẩm thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }
    protected void SaveProduct()
    {
        try
        {

            ProductRow _ProductRow = new ProductRow();
            ProductChangeRow _ProductChangeRow = new ProductChangeRow();
            _ProductChangeRow.Product_ID = 0;
            _ProductChangeRow.SGTIN = _ProductRow.SGTIN = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
            _ProductChangeRow.Name = _ProductRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            if (ddlCha.SelectedValue != "0")
            {
                _ProductChangeRow.ProductCategory_ID = _ProductRow.ProductCategory_ID = Convert.ToInt32(ddlCha.SelectedValue);
            }
            if (ddlTieuChuan.SelectedValue != "0")
            {
                _ProductChangeRow.Quality_ID = _ProductRow.Quality_ID = Convert.ToInt32(ddlTieuChuan.SelectedValue);
            }
            _ProductChangeRow.Content = _ProductRow.Content = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _ProductChangeRow.Story = _ProductRow.Story = string.IsNullOrEmpty(txtstory.Text) ? string.Empty : txtstory.Text;
            _ProductChangeRow.QualityDescription = _ProductRow.QualityDescription = string.IsNullOrEmpty(txtQuyTrinh.Text) ? string.Empty : txtQuyTrinh.Text;
            _ProductChangeRow.GrowthByDay = _ProductRow.GrowthByDay = string.IsNullOrEmpty(txtGrowthByDay.Text) ? 0 : Convert.ToInt32(txtGrowthByDay.Text);
            _ProductChangeRow.ProductType_ID = _ProductRow.ProductType_ID = 1;
            _ProductChangeRow.Country_ID = _ProductRow.Country_ID = 1;
            _ProductChangeRow.ProductBrand_ID = _ProductRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _ProductChangeRow.Acreage = _ProductRow.Acreage = Convert.ToInt32(ddlDienTich.SelectedValue);
            _ProductChangeRow.Price = _ProductRow.Price = 0;
            _ProductChangeRow.PriceOld = _ProductRow.PriceOld = 0;
            _ProductChangeRow.Store_ID = _ProductRow.Store_ID = 0;
            _ProductChangeRow.ExpiryByDate =
            _ProductChangeRow.Customer_ID = _ProductRow.Customer_ID = 0;
            _ProductChangeRow.ProductType_ID = _ProductRow.ProductType_ID = 1;
            _ProductChangeRow.ExpectedProductivity = _ProductRow.ExpectedProductivity = string.IsNullOrEmpty(txtNangSuat.Text) ? 0 : Convert.ToInt32(txtNangSuat.Text);
            _ProductChangeRow.ExpectedOutput = _ProductRow.ExpectedOutput = string.IsNullOrEmpty(txtSanLuong.Text) ? 0 : Convert.ToInt32(txtSanLuong.Text);
            _ProductChangeRow.ExpectedOutputDescription = _ProductRow.ExpectedOutputDescription = string.IsNullOrEmpty(txtSanLuong1.Text) ? "" : (txtSanLuong1.Text);
            _ProductChangeRow.ExpectedProductivityDescription = _ProductRow.ExpectedProductivityDescription = string.IsNullOrEmpty(txtNangSuat1.Text) ? "" : (txtNangSuat1.Text);
            //_ProductRow.URL = Common.ConvertTitleDomain(_ProductRow.Name + "-" + Product_ID);
            _ProductChangeRow.CreateBy = _ProductRow.CreateBy = MyUser.GetUser_ID();
            _ProductChangeRow.CreateDate = _ProductRow.CreateDate = DateTime.Now;
            _ProductChangeRow.LastEditBy = _ProductRow.LastEditBy = MyUser.GetUser_ID();
            _ProductChangeRow.LastEditDate = _ProductRow.LastEditDate = DateTime.Now;
            _ProductChangeRow.WeightDefault = _ProductRow.WeightDefault = string.IsNullOrEmpty(txtWeight.Text) ? 0 : Convert.ToInt32(txtWeight.Text);
            _ProductChangeRow.Specification = _ProductRow.Specification = string.IsNullOrEmpty(txtSpecitication.Text) ? string.Empty : txtSpecitication.Text;
            // _ProductRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            if (ckActive.Checked)
            {
                _ProductChangeRow.Active = _ProductRow.Active = true;
            }
            else
            {
                _ProductChangeRow.Active = _ProductRow.Active = false;
            }

            _ProductChangeRow.TrackingCode = _ProductRow.TrackingCode = string.IsNullOrEmpty(txtTrackingCode.Text) ? string.Empty : txtTrackingCode.Text;
            _ProductChangeRow.ProductChange_By = MyUser.GetUser_ID();
            _ProductChangeRow.ProductChange_Date = DateTime.Now;
            _ProductChangeRow.ProductChange_Status = 0;
            _ProductChangeRow.ProductChange_Note = txtChange.Text;
            if (MyUser.GetProductBrandRole_ID() == "1")
            {
                //Lưu bảng tạm thay đổi thông tin
                BusinessRulesLocator.GetProductChangeBO().Insert(_ProductChangeRow);
                if (!_ProductChangeRow.IsProductChange_IDNull)
                {
                    //Gửi thông báo thay đổi thông tin cho cấp trên
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Thêm mới sản phẩm";
                    _NotificationRow.Summary = txtChange.Text;
                    //  _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                    _NotificationRow.NotificationType_ID = 11;
                    _NotificationRow.UserID = MyUser.GetUser_ID();
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Admin/Notification/RequestProduct_List";
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    lblMessage.Text = "Gửi yêu cầu thêm mới sản phẩm thành công !!";
                    lblMessage.Visible = true;
                }
            }
            else
            {
                BusinessRulesLocator.GetProductBO().Insert(_ProductRow);
                if (!_ProductRow.IsProduct_IDNull)
                {
                    UpdateProduct(_ProductRow.Product_ID);
                    Response.Redirect("Product_List.aspx", false);
                }
            }
            //lblMessage.Text = "Thêm sản phẩm thành công!";
            //lblMessage.Visible = true;


        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }

    private void ClearForm()
    {
        txtGrowthByDay.Text = "";
        txtName.Text = "";
        txtNote.Text = "";
        txtQuyTrinh.Text = "";
        txtNangSuat.Text = "";
        txtNangSuat1.Text = "";
        txtSanLuong.Text = "";
        txtSanLuong1.Text = "";
        txtTrackingCode.Text = "";
        ddlCha.SelectedIndex = ddlTieuChuan.SelectedIndex = 0;
        ckActive.Checked = true;
        txtGTIN.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                SaveProduct();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product_List.aspx", false);
    }
}