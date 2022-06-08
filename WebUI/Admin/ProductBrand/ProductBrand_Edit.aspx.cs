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

public partial class ProductBrand_Edit : System.Web.UI.Page
{
    int ProductBrand_ID = 0;
    public string title = "Thông tin doanh nghiệp";
    public string avatar = "";
    public string MaVungtrong = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtStory);
        _FileBrowser.SetupCKEditor(txtHoso);
        _FileBrowser.SetupCKEditor(txtQuangBa);
        _FileBrowser.SetupCKEditor(txtDaiLy);

        if (!string.IsNullOrEmpty(Request["ProductBrand_ID"]))
            int.TryParse(Request["ProductBrand_ID"].ToString(), out ProductBrand_ID);
        if (MyUser.GetFunctionGroup_ID() == "1")
        {
            ddlPackage.Enabled = true;
            ddlSo.Enabled = true;
        }
        else
        {
            ddlSo.Enabled = false;
            ddlPackage.Enabled = false;
            ddlRole.Enabled = false;
            ddlRank.Enabled = false;
        }
        if (Request.QueryString["mode"] != null)
        {
            if (MyUser.GetProductBrandRole_ID() == "1")
            {
                lblMessage.Text = "Gửi yêu cầu thay đổi thông tin thành công !";
            }
            else
            {
                lblMessage.Text = "Cập nhật thành công";
            }
            lblMessage.Visible = true;
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    btnSave.Text = "Gửi yêu cầu thay đổi thông tin";
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
            FillDDL();
            LoadRank();
            LoadRole();
            LoadBusinessType();
            FillInfoProductBrand();

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
    protected void LoadRank()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from Rank");
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
            dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", " Sort ASC");
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

            DataTable dtBranch = new DataTable();
            //dtBranch = BusinessRulesLocator.GetBranchBO().GetAsDataTable(" Active=1", " Sort ASC");
            dtBranch = BusinessRulesLocator.Conllection().GetBranch();
            ddlBranch.DataSource = dtBranch;
            ddlBranch.DataTextField = "Name";
            ddlBranch.DataValueField = "Branch_ID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("-- Chọn ngành --", "0"));


            DataTable dtChainLink = new DataTable();
            dtChainLink = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlChainlink.DataSource = dtChainLink;
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
    protected void FillInfoProductBrand()
    {
        try
        {
            if (ProductBrand_ID != 0)
            {
                ProductBrandRow _ProductBrandRow = new ProductBrandRow();
                _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
                if (_ProductBrandRow != null)
                {
                    //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                    MyActionPermission.CheckPermission(_ProductBrandRow.ProductBrand_ID.ToString(), _ProductBrandRow.CreateBy.ToString(), "/Admin/ProductBrand/ProductBrand_List");

                    if (!_ProductBrandRow.IsProductBrandType_ID_ListNull)
                    {
                        string[] array = _ProductBrandRow.ProductBrandType_ID_List.Split(',');
                        foreach (string value in array)
                        {

                            foreach (RadComboBoxItem item in ddlType.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Checked = true;
                                }
                            }
                        }
                    }

                    txtGLN.Text = _ProductBrandRow.IsGLNNull ? string.Empty : _ProductBrandRow.GLN;
                    txtGCP.Text = _ProductBrandRow.IsGCPNull ? string.Empty : _ProductBrandRow.GCP;
                    txtName.Text = _ProductBrandRow.IsNameNull ? string.Empty : _ProductBrandRow.Name;
                    txtTenGiaoDich.Text = _ProductBrandRow.IsTradingNameNull ? string.Empty : _ProductBrandRow.TradingName;
                    txtThuongHieu.Text = _ProductBrandRow.IsBrandNameNull ? string.Empty : _ProductBrandRow.BrandName;
                    txtMST.Text = _ProductBrandRow.IsTaxCodeNull ? string.Empty : _ProductBrandRow.TaxCode;
                    txtDKKD.Text = _ProductBrandRow.IsRegistrationNumberNull ? string.Empty : _ProductBrandRow.RegistrationNumber;
                    if (!_ProductBrandRow.IsIssuedDateNull)
                        txtNgayCap.Text = _ProductBrandRow.IssuedDate.ToString("dd/MM/yyyy");
                    else
                    {
                        txtNgayCap.Text = "";
                    }
                    if (!_ProductBrandRow.IsCreateDateNull)
                        txtDate.Text = _ProductBrandRow.CreateDate.ToString("dd/MM/yyyy");
                    else
                    {
                        txtDate.Text = "";
                    }

                    if (!_ProductBrandRow.IsContractSigningDateNull)
                        txtContractSigningDate.Text = _ProductBrandRow.CreateDate.ToString("dd/MM/yyyy");
                    else
                    {
                        txtContractSigningDate.Text = "";
                    }
                    txtProductCount.Text = _ProductBrandRow.IsProductCountNull ? "0" : _ProductBrandRow.ProductCount.ToString();
                    txtStampCount.Text = _ProductBrandRow.IsStampCountNull ? "0" : _ProductBrandRow.StampCount.ToString();
                    ddlSales.SelectedValue = _ProductBrandRow.IsSales_IDNull ? "0" : _ProductBrandRow.Sales_ID.ToString();
                    ddlPackage.SelectedValue = _ProductBrandRow.IsFunctionGroup_IDNull ? "" : _ProductBrandRow.FunctionGroup_ID.ToString();
                    txtLinhVucKD.Text = _ProductBrandRow.IsBusinessAreaNull ? string.Empty : _ProductBrandRow.BusinessArea;
                    txtAddress.Text = _ProductBrandRow.IsAddressNull ? string.Empty : _ProductBrandRow.Address;
                    txtPhone.Text = _ProductBrandRow.IsTelephoneNull ? string.Empty : _ProductBrandRow.Telephone;
                    txtMobile.Text = _ProductBrandRow.IsMobileNull ? string.Empty : _ProductBrandRow.Mobile;
                    txtEmail.Text = _ProductBrandRow.IsEmailNull ? string.Empty : _ProductBrandRow.Email;
                    txtWebsite.Text = _ProductBrandRow.IsWebsiteNull ? string.Empty : _ProductBrandRow.Website;
                    txtFacebookID.Text = _ProductBrandRow.IsFacebookNull ? string.Empty : _ProductBrandRow.Facebook;
                    txtYoutube.Text = _ProductBrandRow.IsYouTubeNull ? string.Empty : _ProductBrandRow.YouTube;
                    txtZalo.Text = _ProductBrandRow.IsZaloNull ? string.Empty : _ProductBrandRow.Zalo;
                    txtHotline.Text = _ProductBrandRow.IsHotlineNull ? string.Empty : _ProductBrandRow.Hotline;
                    txtSkype.Text = _ProductBrandRow.IsSkypeNull ? string.Empty : _ProductBrandRow.Skype;
                    if (!_ProductBrandRow.IsLocation_IDNull)
                    {
                        ddlLocation.SelectedValue = _ProductBrandRow.Location_ID.ToString();
                        DataTable dtDistrict = new DataTable();
                        dtDistrict = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
                        ddlDistrict.DataSource = dtDistrict;
                        ddlDistrict.DataTextField = "Name";
                        ddlDistrict.DataValueField = "District_ID";
                        ddlDistrict.DataBind();
                        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
                    }
                    if (!_ProductBrandRow.IsDistrict_IDNull)
                    {
                        ddlDistrict.SelectedValue = _ProductBrandRow.District_ID.ToString();

                        DataTable dtward = new DataTable();
                        dtward = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
                        ddlWard.DataSource = dtward;
                        ddlWard.DataTextField = "Name";
                        ddlWard.DataValueField = "Ward_ID";
                        ddlWard.DataBind();
                        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
                    }
                    if (!_ProductBrandRow.IsWard_IDNull)
                    {
                        ddlWard.SelectedValue = _ProductBrandRow.Ward_ID.ToString();
                    }
                    if (!_ProductBrandRow.IsProductBrandType_IDNull)
                    {
                        ddlType.SelectedValue = _ProductBrandRow.ProductBrandType_ID.ToString();
                    }
                    if (!_ProductBrandRow.IsBranch_IDNull)
                    {
                        ddlBranch.SelectedValue = _ProductBrandRow.Branch_ID.ToString();
                        ddlBranch_SelectedIndexChanged(null, null);
                    }
                    if (!_ProductBrandRow.IsRank_IDNull)
                    {
                        ddlRank.SelectedValue = _ProductBrandRow.Rank_ID.ToString();

                    }
                    ddlRank_SelectedIndexChanged(null, null);
                    if (!_ProductBrandRow.IsProductBrandRole_IDNull)
                    {
                        ddlRole.SelectedValue = _ProductBrandRow.ProductBrandRole_ID.ToString();
                    }
                    if (!_ProductBrandRow.IsBusinessType_IDNull)
                    {
                        ddlBussines.SelectedValue = _ProductBrandRow.BusinessType_ID.ToString();
                    }

                    if (!_ProductBrandRow.IsChainLink_IDNull)
                    {
                        ddlChainlink.SelectedValue = _ProductBrandRow.ChainLink_ID.ToString();
                        if (ddlChainlink.SelectedValue != "0")
                        {
                            Vaitro.Visible = true;
                            if (!_ProductBrandRow.IsRoleChain_IDNull)
                            {
                                ddlRoleChain.SelectedValue = _ProductBrandRow.RoleChain_ID.ToString();
                            }
                        }

                    }
                    ddlSo.SelectedValue = _ProductBrandRow.DepartmentMan_ID.ToString();
                    txtHoso.Text = _ProductBrandRow.IsDescriptionNull ? string.Empty : _ProductBrandRow.Description;
                    txtStory.Text = _ProductBrandRow.IsStoryNull ? string.Empty : _ProductBrandRow.Story;
                    if (!_ProductBrandRow.IsImageNull)
                    {
                        avatar = "../../data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                        imganh.ImageUrl = avatar;
                    }
                    else
                    {
                        avatar = "../../images/no-image-icon.png";
                    }

                    if (!_ProductBrandRow.IsProductionUnitCodeNull)
                    {
                        MaVungtrong = "../../data/productbrand/mainimages/original/" + _ProductBrandRow.ProductionUnitCode;
                        ImgPUC.ImageUrl = MaVungtrong;
                    }
                    else
                    {
                        MaVungtrong = "../../images/no-image-icon.png";
                    }

                    txtHotenPhapNhan.Text = _ProductBrandRow.IsDirectorNameNull ? string.Empty : _ProductBrandRow.DirectorName;
                    if (!_ProductBrandRow.IsDirectorBirthdayNull)
                        txtBirth.Text = _ProductBrandRow.DirectorBirthday.ToString("dd/MM/yyyy");
                    else
                    {
                        txtBirth.Text = "";
                    }
                    txtDiaChiPhapNhan.Text = _ProductBrandRow.IsDirectorAddressNull ? string.Empty : _ProductBrandRow.DirectorAddress;
                    txtDienThoaiPhapNhan.Text = _ProductBrandRow.IsDirectorMobileNull ? string.Empty : _ProductBrandRow.DirectorMobile;
                    txtEmailPhapNhan.Text = _ProductBrandRow.IsDirectorEmailNull ? string.Empty : _ProductBrandRow.DirectorEmail;
                    txtChucVuPhapNhan.Text = _ProductBrandRow.IsDirectorPositionNull ? string.Empty : _ProductBrandRow.DirectorPosition;

                    txtHoTenLienHe.Text = _ProductBrandRow.IsPersonNameNull ? string.Empty : _ProductBrandRow.PersonName;
                    txtDiaChiLienHe.Text = _ProductBrandRow.IsPersonAddressNull ? string.Empty : _ProductBrandRow.PersonAddress;
                    txtDienThoaiLienHe.Text = _ProductBrandRow.IsPersonMobileNull ? string.Empty : _ProductBrandRow.PersonMobile;
                    txtEmailLienHe.Text = _ProductBrandRow.IsPersonEmailNull ? string.Empty : _ProductBrandRow.PersonEmail;


                    txtTaiKhoan.Text = _ProductBrandRow.IsAccountUserNameNull ? string.Empty : _ProductBrandRow.AccountUserName;
                    txtEmailLogin.Text = _ProductBrandRow.IsAccountEmailNull ? string.Empty : _ProductBrandRow.AccountEmail;


                    txtQuangBa.Text = _ProductBrandRow.IsPRInfoNull ? string.Empty : _ProductBrandRow.PRInfo;
                    txtDaiLy.Text = _ProductBrandRow.IsAgencyNull ? string.Empty : _ProductBrandRow.Agency;

                    if (_ProductBrandRow.Active == true)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                    if (_ProductBrandRow.ShowLogo == true)
                    {
                        ckShowlogo.Checked = true;
                    }
                    else
                    {
                        ckShowlogo.Checked = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }
    protected string GetMinisty_ID()
    {
        string FunctionGroup_ID = string.Empty;
        //try
        //{
        //    foreach (RadComboBoxItem item in ddlMinistry.Items)
        //    {
        //        if (item.Checked)
        //        {
        //            FunctionGroup_ID += item.Value + ",";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(FunctionGroup_ID))
        //    {
        //        FunctionGroup_ID = "," + FunctionGroup_ID;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        //}
        return FunctionGroup_ID;
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
    protected void UpdateBranch()
    {
        try
        {
            string msg = string.Empty;
            ProductBrandRow _ProductBrandRow = new ProductBrandRow();
            if (ProductBrand_ID != 0)
            {
                _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
                if (_ProductBrandRow != null)
                {
                    ProductBrandChangeRow _ProductBrandChangeRow = new ProductBrandChangeRow();
                    _ProductBrandChangeRow.Name = _ProductBrandRow.Name = txtName.Text;
                    if (ddlChainlink.SelectedValue != "0")
                    {
                        _ProductBrandChangeRow.ChainLink_ID = _ProductBrandRow.ChainLink_ID = Convert.ToInt32(ddlChainlink.SelectedValue);
                    }
                    if (ddlRoleChain.SelectedValue != "-1")
                    {
                        _ProductBrandChangeRow.RoleChain_ID = _ProductBrandRow.RoleChain_ID = Convert.ToInt32(ddlRoleChain.SelectedValue);
                    }
                    _ProductBrandChangeRow.BrandName = _ProductBrandRow.BrandName = txtThuongHieu.Text;
                    _ProductBrandChangeRow.TradingName = _ProductBrandRow.TradingName = txtTenGiaoDich.Text.Trim();
                    _ProductBrandChangeRow.TaxCode = _ProductBrandRow.TaxCode = txtMST.Text.Trim();
                    _ProductBrandChangeRow.RegistrationNumber = _ProductBrandRow.RegistrationNumber = txtDKKD.Text.Trim();
                    if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandChangeRow.IssuedDate = _ProductBrandRow.IssuedDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
                    {
                        DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandChangeRow.CreateDate = _ProductBrandRow.CreateDate = date;
                    }
                    //if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
                    //{
                    //    DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //    _ProductBrandChangeRow.CreateDate = _ProductBrandRow.CreateDate = date;
                    //}
                    if (ddlSales.SelectedValue != "0")
                    {
                        _ProductBrandRow.Sales_ID = Guid.Parse(ddlSales.SelectedValue.ToString());
                    }
                    _ProductBrandRow.Rank_ID = Convert.ToInt32(ddlRank.SelectedValue);
                    _ProductBrandRow.ProductBrandRole_ID = Convert.ToInt32(ddlRole.SelectedValue);
                    _ProductBrandChangeRow.FunctionGroup_ID = _ProductBrandRow.FunctionGroup_ID = Convert.ToInt32(ddlPackage.SelectedValue);
                    _ProductBrandChangeRow.Branch_ID = _ProductBrandRow.Branch_ID = Convert.ToInt32(string.IsNullOrEmpty(ddlBranch.SelectedValue.ToString()) ? string.Empty : ddlBranch.SelectedValue.ToString()); /*Convert.ToInt32(ddlBranch.SelectedValue);*/
                    _ProductBrandChangeRow.BusinessType_ID = _ProductBrandRow.BusinessType_ID = Convert.ToInt32(ddlBussines.SelectedValue);
                    _ProductBrandChangeRow.ProductBrandType_ID_List = _ProductBrandRow.ProductBrandType_ID_List = GetProductBrandType_ID();
                    _ProductBrandChangeRow.BusinessArea = _ProductBrandRow.BusinessArea = txtLinhVucKD.Text.Trim();
                    _ProductBrandChangeRow.DepartmentMan_ID = _ProductBrandRow.DepartmentMan_ID = Convert.ToInt32(ddlSo.SelectedValue);
                    _ProductBrandChangeRow.Address = _ProductBrandRow.Address = txtAddress.Text;
                    _ProductBrandChangeRow.Location_ID = _ProductBrandRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    _ProductBrandChangeRow.District_ID = _ProductBrandRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    _ProductBrandChangeRow.Ward_ID = _ProductBrandRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
                    _ProductBrandChangeRow.Telephone = _ProductBrandRow.Telephone = txtPhone.Text;
                    _ProductBrandChangeRow.Mobile = _ProductBrandRow.Mobile = txtMobile.Text;
                    _ProductBrandChangeRow.Email = _ProductBrandRow.Email = txtEmail.Text;
                    _ProductBrandChangeRow.Website = _ProductBrandRow.Website = txtWebsite.Text;
                    _ProductBrandChangeRow.Facebook = _ProductBrandRow.Facebook = txtFacebookID.Text;
                    _ProductBrandChangeRow.YouTube = _ProductBrandRow.YouTube = txtYoutube.Text;

                    _ProductBrandChangeRow.Zalo = _ProductBrandRow.Zalo = txtZalo.Text;
                    _ProductBrandChangeRow.Hotline = _ProductBrandRow.Hotline = txtHotline.Text;
                    _ProductBrandChangeRow.Skype = _ProductBrandRow.Skype = txtSkype.Text;
                    _ProductBrandChangeRow.Description = _ProductBrandRow.Description = txtHoso.Text;
                    // Thông tin người đại diện pháp nhân
                    _ProductBrandChangeRow.DirectorName = _ProductBrandRow.DirectorName = txtHotenPhapNhan.Text.Trim();
                    //_ProductBrandRow.DirectorBirthday = rdDirectorBirthday.SelectedDate;
                    if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
                    {
                        DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandChangeRow.DirectorBirthday = _ProductBrandRow.DirectorBirthday = birth;
                    }
                    _ProductBrandChangeRow.DirectorAddress = _ProductBrandRow.DirectorAddress = txtDiaChiPhapNhan.Text.Trim();
                    _ProductBrandChangeRow.DirectorMobile = _ProductBrandRow.DirectorMobile = txtDienThoaiPhapNhan.Text.Trim();
                    _ProductBrandChangeRow.DirectorEmail = _ProductBrandRow.DirectorEmail = txtEmailPhapNhan.Text.Trim();
                    _ProductBrandChangeRow.DirectorPosition = _ProductBrandRow.DirectorPosition = txtChucVuPhapNhan.Text.Trim();

                    // Thông tin tài khoản
                    _ProductBrandChangeRow.AccountUserName = _ProductBrandRow.AccountUserName = txtTaiKhoan.Text.Trim();
                    _ProductBrandChangeRow.AccountEmail = _ProductBrandRow.AccountEmail = txtTaiKhoan.Text.Trim();

                    // Thông tin người liên hệ
                    _ProductBrandChangeRow.PersonName = _ProductBrandRow.PersonName = txtHoTenLienHe.Text;
                    _ProductBrandChangeRow.PersonAddress = _ProductBrandRow.PersonAddress = txtDiaChiLienHe.Text;
                    _ProductBrandChangeRow.PersonMobile = _ProductBrandRow.PersonMobile = txtDienThoaiLienHe.Text;
                    _ProductBrandChangeRow.PersonEmail = _ProductBrandRow.PersonEmail = txtEmailLienHe.Text;

                    _ProductBrandChangeRow.PRInfo = _ProductBrandRow.PRInfo = txtQuangBa.Text;
                    _ProductBrandChangeRow.Story = _ProductBrandRow.Story = txtStory.Text;
                    _ProductBrandChangeRow.Agency = _ProductBrandRow.Agency = txtDaiLy.Text;

                    _ProductBrandChangeRow.GLN = _ProductBrandRow.GLN = txtGLN.Text;
                    _ProductBrandChangeRow.GCP = _ProductBrandRow.GCP = txtGCP.Text;
                    // Hiển thị logo lên cổng truy xuất

                    _ProductBrandChangeRow.ShowLogo = _ProductBrandRow.ShowLogo = ckShowlogo.Checked == true ? true : false;
                    if (_ProductBrandRow.IsURLNull)
                    {
                        _ProductBrandChangeRow.URL = _ProductBrandRow.URL = Common.ConvertTitleDomain(_ProductBrandRow.Name) + "-" + ProductBrand_ID;
                    }
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_ProductBrandRow.IsImageNull)
                        {
                            if (_ProductBrandRow.Image != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandRow.Image.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = ProductBrand_ID + "-" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _ProductBrandChangeRow.Image = _ProductBrandRow.Image = fileimage;
                        }
                    }
                    else
                    {
                        if (!_ProductBrandRow.IsImageNull)
                            _ProductBrandChangeRow.Image = _ProductBrandRow.Image;
                    }

                    string ImgPUC = "";
                    if (ProductionUnitCode.HasFile)
                    {
                        if (!_ProductBrandRow.IsProductionUnitCodeNull)
                        {
                            if (_ProductBrandRow.ProductionUnitCode != null)
                            {
                                string strImgPUCPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandRow.ProductionUnitCode.Replace("../", "");
                                if (File.Exists(strImgPUCPath))
                                {
                                    File.Delete(strImgPUCPath);
                                }
                            }
                        }
                        ImgPUC = ProductBrand_ID + "-" + ProductionUnitCode.FileName;
                        ProductionUnitCode.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + ImgPUC));
                        if (!string.IsNullOrEmpty(ImgPUC))
                        {
                            _ProductBrandChangeRow.ProductionUnitCode = _ProductBrandRow.ProductionUnitCode = ImgPUC;
                        }
                    }
                    else
                    {
                        if (!_ProductBrandRow.IsProductionUnitCodeNull)
                            _ProductBrandChangeRow.ProductionUnitCode = _ProductBrandRow.ProductionUnitCode;
                    }

                    _ProductBrandChangeRow.LastEditDate = _ProductBrandRow.LastEditDate = DateTime.Now;
                    _ProductBrandChangeRow.LastEditBy = _ProductBrandRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductBrandChangeRow.ProductBrand_ID = ProductBrand_ID;
                    //BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);

                    _ProductBrandChangeRow.ProductBrandChange_Note = string.IsNullOrEmpty(txtChange.Text) ? "Thay đổi thông tin doanh nghiệp " : txtChange.Text;
                    _ProductBrandChangeRow.ProductBrandChange_By = MyUser.GetUser_ID();
                    _ProductBrandChangeRow.ProductBrandChange_Date = DateTime.Now;
                    _ProductBrandChangeRow.ProductBrandChange_Status = 0;
                    if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
                    {
                        if (MyUser.GetProductBrandRole_ID() == "1")
                        {
                            //Lưu bảng tạm thay đổi thông tin
                            BusinessRulesLocator.GetProductBrandChangeBO().Insert(_ProductBrandChangeRow);
                            if (!_ProductBrandChangeRow.IsProductBrandChange_IDNull)
                            {
                                //Gửi thông báo thay đổi thông tin cho cấp trên
                                NotificationRow _NotificationRow = new NotificationRow();
                                _NotificationRow.Name = "Chỉnh sửa thông tin doanh nghiệp";
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
                                _NotificationRow.Url = "/Admin/Notification/RequestProductBrand_List";
                                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                                _NotificationRow.CreateDate = DateTime.Now;
                                _NotificationRow.Active = 1;
                                _NotificationRow.Alias = Guid.NewGuid();
                                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                                msg = "Gửi yêu cầu thay đổi thông tin thành công !";
                            }
                        }
                        else
                        {
                            BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
                        }
                    }
                    else
                    {
                        BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
                    }
                    if (!string.IsNullOrEmpty(msg))
                    {
                        lblMessage.Text = msg;
                    }
                    else
                    {
                        lblMessage.Text = "Cập nhật thông tin thành công!";
                    }
                    lblMessage.Visible = true;
                    FillInfoProductBrand();
                    Response.Redirect("ProductBrand_List.aspx?mode=edit", false);
                }
            }
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
                UpdateBranch();
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

    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRank.SelectedValue != "1")
        {
            SoPT.Visible = true;
        }
        else
        {
            SoPT.Visible = false;
        }
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
}