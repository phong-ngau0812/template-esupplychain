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
using Telerik.Web.UI.ImageEditor;

public partial class ProductBrand_Add : System.Web.UI.Page
{
    public string title = "Thêm mới doanh nghiệp";
    public string avatar = "javascript:void(0);";
    public string MaVungtrong = "javascript:void(0);";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtStory);
        _FileBrowser.SetupCKEditor(txtHoso);
        _FileBrowser.SetupCKEditor(txtQuangBa);
        _FileBrowser.SetupCKEditor(txtDaiLy);
        if (!IsPostBack)
        {
            txtContractSigningDate.Text = txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            FillDDL();
            LoadRank();
            LoadRole();
            LoadBusinessType();
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    FillDistrict();
                    ddlLocation.Enabled = false;

                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                }
                else if (MyUser.GetRank_ID() == "4")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                    ddlWard.SelectedValue = MyUser.GetWard_ID();
                    ddlWard.Enabled = false;
                }

            }
        }
    }
    protected void LoadRank()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " where Rank_ID not in (1)";
                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    where += " where Rank_ID not in (1,2)";
                }
                else
                {
                    where += " where Rank_ID not in (1,2,3)";
                }
            }
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from Rank" + where);
            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "Title";
            ddlRank.DataValueField = "Rank_ID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-- Chọn cấp bậc --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadModule", ex.ToString());
        }
    }
    protected void LoadRole()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from ProductBrandRole");
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "Title";
            ddlRole.DataValueField = "ProductBrandRole_ID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("-- Chọn quyền sửa đổi thông tin --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadModule", ex.ToString());
        }
    }
    private void FillDDL()
    {
        try
        {



            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDepartmentManBO().GetAsDataTable("", " ");
            ddlSo.DataSource = dt;
            ddlSo.DataTextField = "Name";
            ddlSo.DataValueField = "DepartmentMan_ID";
            ddlSo.DataBind();
            ddlSo.Items.Insert(0, new ListItem("-- Chọn sở --", "0"));

            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", " Name ASC");
            ddlLocation.DataSource = dtLocation;
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataValueField = "Location_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("-- Chọn tỉnh/ thành phố --", ""));
            ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/ huyện --", ""));
            ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/ xã --", ""));

            DataTable dtType = new DataTable();
            dtType = BusinessRulesLocator.Conllection().GetAllList(" select * from ProductBrandType where Active=1");
            ddlType.DataSource = dtType;
            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "ProductBrandType_ID";
            ddlType.DataBind();
            //ddlType.Items.Insert(0, new ListItem("-- Chọn loại hình --", ""));


            DataTable dtBranch = new DataTable();
            //dtBranch = BusinessRulesLocator.GetBranchBO().GetAsDataTable(" Active=1", " Sort ASC");
            dtBranch = BusinessRulesLocator.Conllection().GetBranch();
            ddlBranch.DataSource = dtBranch;
            ddlBranch.DataTextField = "Name";
            ddlBranch.DataValueField = "Branch_ID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("-- Chọn ngành --", "0"));


            DataTable dtChainLink = new DataTable();
            dtBranch = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlChainlink.DataSource = dtBranch;
            ddlChainlink.DataTextField = "Name";
            ddlChainlink.DataValueField = "ChainLink_ID";
            ddlChainlink.DataBind();
            ddlChainlink.Items.Insert(0, new ListItem("-- Chọn chuỗi liên kết --", "0"));


            DataTable dtGoiDN = new DataTable();
            dtGoiDN = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active=1 and Type=1", " Sort asc");
            ddlPackage.DataSource = dtGoiDN;
            ddlPackage.DataTextField = "Name";
            ddlPackage.DataValueField = "FunctionGroup_ID";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("-- Chọn gói doanh nghiệp --", ""));
            LoadSales();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void LoadSales()
    {
        try
        {
            DataTable dtList = BusinessRulesLocator.Conllection().GetAllList(" select * from aspnet_Users");

            //var dtSource = Membership.GetAllUsers();
            foreach (DataRow item in dtList.Rows.Cast<DataRow>().ToList())
            {
                UserProfile ProfileUser = UserProfile.GetProfile(item["UserName"].ToString());
                if (ProfileUser.IsSale != "1")
                {
                    dtList.Rows.Remove(item);
                    dtList.AcceptChanges();
                }
            }
            ddlSales.DataSource = dtList;
            ddlSales.DataTextField = "UserName";
            ddlSales.DataValueField = "UserId";
            ddlSales.DataBind();
            ddlSales.Items.Insert(0, new ListItem("-- Chọn nhân viên kinh doanh quản lý --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadSales", ex.ToString());
        }
    }
    protected string GetProductBrandType_ID()
    {
        string FunctionGroup_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlType.Items)
            {
                if (item.Checked)
                {
                    FunctionGroup_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(FunctionGroup_ID))
            {
                FunctionGroup_ID = "," + FunctionGroup_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetProductBrandType_ID", ex.ToString());
        }
        return FunctionGroup_ID;
    }
    protected void AddProductBrand()
    {
        try
        {

            ProductBrandRow _ProductBrandRow = new ProductBrandRow();

            _ProductBrandRow.Name = txtName.Text;
            _ProductBrandRow.BrandName = txtThuongHieu.Text;
            _ProductBrandRow.TradingName = txtTenGiaoDich.Text.Trim();
            _ProductBrandRow.TaxCode = txtMST.Text.Trim();
            _ProductBrandRow.RegistrationNumber = txtDKKD.Text.Trim();
            if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
            {
                DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandRow.IssuedDate = ngaycap;
            }
            _ProductBrandRow.ProductBrandRole_ID = Convert.ToInt32(ddlRole.SelectedValue);
            _ProductBrandRow.Branch_ID = Convert.ToInt32(ddlBranch.SelectedValue);
            _ProductBrandRow.BusinessType_ID = Convert.ToInt32(ddlBussines.SelectedValue);
            _ProductBrandRow.FunctionGroup_ID = Convert.ToInt32(ddlPackage.SelectedValue);
            _ProductBrandRow.Rank_ID = Convert.ToInt32(ddlRank.SelectedValue);
            _ProductBrandRow.ProductBrandType_ID_List = GetProductBrandType_ID();
            _ProductBrandRow.BusinessArea = txtLinhVucKD.Text.Trim();
            _ProductBrandRow.DepartmentMan_ID = Convert.ToInt32(ddlSo.SelectedValue);
            _ProductBrandRow.Address = txtAddress.Text;
            _ProductBrandRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
            _ProductBrandRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
            _ProductBrandRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
            _ProductBrandRow.Telephone = txtPhone.Text;
            _ProductBrandRow.Mobile = txtMobile.Text;
            _ProductBrandRow.Email = txtEmail.Text;
            _ProductBrandRow.Website = txtWebsite.Text;
            _ProductBrandRow.Facebook = txtFacebookID.Text;
            _ProductBrandRow.Zalo = txtZalo.Text;
            _ProductBrandRow.Hotline = txtHotline.Text;
            _ProductBrandRow.Skype = txtSkype.Text;
            _ProductBrandRow.Description = txtHoso.Text;
            _ProductBrandRow.YouTube = txtYoutube.Text;

            _ProductBrandRow.ChainLink_ID = Convert.ToInt32(ddlChainlink.SelectedValue);
            if (ddlRoleChain.SelectedValue != "")
            {
                _ProductBrandRow.RoleChain_ID = Convert.ToInt32(ddlRoleChain.SelectedValue);
            }
            if (ddlSales.SelectedValue != "0")
            {
                _ProductBrandRow.Sales_ID = Guid.Parse(ddlSales.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtProductCount.Text))
            {
                _ProductBrandRow.ProductCount = Convert.ToInt32(txtProductCount.Text);
            }
            else
            {
                _ProductBrandRow.ProductCount = 10;
            }
            if (!string.IsNullOrEmpty(txtStampCount.Text))
            {
                _ProductBrandRow.StampCount = Convert.ToInt32(txtStampCount.Text);
            }
            else
            {
                _ProductBrandRow.StampCount = 10000;
            }
            // Thông tin người đại diện pháp nhân
            _ProductBrandRow.DirectorName = txtHotenPhapNhan.Text.Trim();
            //_ProductBrandRow.DirectorBirthday = rdDirectorBirthday.SelectedDate;
            if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
            {
                DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandRow.DirectorBirthday = birth;
            }
            _ProductBrandRow.DirectorAddress = txtDiaChiPhapNhan.Text.Trim();
            _ProductBrandRow.DirectorMobile = txtDienThoaiPhapNhan.Text.Trim();
            _ProductBrandRow.DirectorEmail = txtEmailPhapNhan.Text.Trim();
            _ProductBrandRow.DirectorPosition = txtChucVuPhapNhan.Text.Trim();

            // Thông tin tài khoản
            _ProductBrandRow.AccountUserName = txtTaiKhoan.Text.Trim();
            _ProductBrandRow.AccountEmail = txtTaiKhoan.Text.Trim();

            // Thông tin người liên hệ
            _ProductBrandRow.PersonName = txtHoTenLienHe.Text;
            _ProductBrandRow.PersonAddress = txtDiaChiLienHe.Text;
            _ProductBrandRow.PersonMobile = txtDienThoaiLienHe.Text;
            _ProductBrandRow.PersonEmail = txtEmailLienHe.Text;

            _ProductBrandRow.Story = txtStory.Text;
            _ProductBrandRow.PRInfo = txtQuangBa.Text;
            _ProductBrandRow.Agency = txtDaiLy.Text;
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandRow.CreateDate = date;
                _ProductBrandRow.LastEditDate = date;
            }
            else
            {
                _ProductBrandRow.CreateDate = DateTime.Now;
                _ProductBrandRow.LastEditDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(txtContractSigningDate.Text))
            {
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandRow.ContractSigningDate = date;
            }
            else
            {

                _ProductBrandRow.ContractSigningDate = DateTime.Now;
            }
            _ProductBrandRow.LastEditBy = MyUser.GetUser_ID();
            _ProductBrandRow.CreateBy = MyUser.GetUser_ID();
            _ProductBrandRow.Active = ckActive.Checked;
            _ProductBrandRow.ShowLogo = false;

            _ProductBrandRow.HasQRCode = true;
            _ProductBrandRow.ViewCount = 0;
            _ProductBrandRow.SellCount = 0;
            BusinessRulesLocator.GetProductBrandBO().Insert(_ProductBrandRow);
            if (!_ProductBrandRow.IsProductBrand_IDNull)
            {
                UpdateProductBrand(_ProductBrandRow.ProductBrand_ID);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateBranch", ex.ToString());
        }
    }
    protected void UpdateProductBrand(int ProductBrand_ID)
    {
        try
        {

            ProductBrandRow _ProductBrandRow = new ProductBrandRow();
            _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
            _ProductBrandRow.URL = Common.ConvertTitleDomain(_ProductBrandRow.Name) + "-" + ProductBrand_ID;
            string fileimage = "";
            if (fulAnh.HasFile)
            {

                fileimage = ProductBrand_ID + "-" + fulAnh.FileName;
                fulAnh.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _ProductBrandRow.Image = fileimage;
                }
            }
            string ImgPUC = "";
            if (ProductionUnitCode.HasFile)
            {
                ImgPUC = ProductBrand_ID + "-" + ProductionUnitCode.FileName;
                ProductionUnitCode.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + ImgPUC));
                if (!string.IsNullOrEmpty(ImgPUC))
                {
                    _ProductBrandRow.ProductionUnitCode = ImgPUC;
                }
            }
            _ProductBrandRow.GLN = txtGLN.Text.Trim();
            _ProductBrandRow.GCP = txtGCP.Text.Trim();
            _ProductBrandRow.LastEditDate = DateTime.Now;
            _ProductBrandRow.LastEditBy = MyUser.GetUser_ID();
            BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
            lblMessage.Text = "Thêm mới doanh nghiệp thành công!";
            lblMessage.Visible = true;
            Response.Redirect("ProductBrand_List.aspx", false);
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateBranch", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddProductBrand();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductBrand_List.aspx", false);
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDistrict();
    }
    protected void FillDistrict()
    {
        ddlDistrict.Items.Clear();
        if (ddlLocation.SelectedValue != "0")
        {
            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
            ddlDistrict.DataSource = dtLocation;
            ddlDistrict.DataTextField = "Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillWard();
    }
    protected void FillWard()
    {
        ddlWard.Items.Clear();
        if (ddlDistrict.SelectedValue != "0")
        {
            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
            ddlWard.DataSource = dtLocation;
            ddlWard.DataTextField = "Name";
            ddlWard.DataValueField = "Ward_ID";
            ddlWard.DataBind();
        }
        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranch.SelectedValue != "")
        //{
        //    DataTable dtLocation = new DataTable();
        //    dtLocation = BusinessRulesLocator.GetBusinessTypeBO().GetAsDataTable(" Branch_ID=" + ddlBranch.SelectedValue, " Title ASC");
        //    ddlBussines.DataSource = dtLocation;
        //    ddlBussines.DataTextField = "Title";
        //    ddlBussines.DataValueField = "BusinessType_ID";
        //    ddlBussines.DataBind();
        //    ddlBussines.Items.Insert(0, new ListItem("-- Chọn lĩnh vực KD --", ""));
        //}
    }
    protected void ddlChainlink_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlChainlink.SelectedValue != "0")
        {
            Vaitro.Visible = true;
        }
        else
        {
            Vaitro.Visible = false;
        }
    }
    protected void LoadBusinessType()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetBusinessType();
            ddlBussines.DataSource = dt;
            ddlBussines.DataTextField = "Name";
            ddlBussines.DataValueField = "BusinessType_ID";
            ddlBussines.DataBind();
            ddlBussines.Items.Insert(0, new ListItem("-- Chọn Lĩnh vực Kinh Doanh --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadModule", ex.ToString());
        }
    }
}