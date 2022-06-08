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

public partial class ProductBrand_ChangeEdit : System.Web.UI.Page
{
    int ProductBrandChange_ID = 0;
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
        if (!string.IsNullOrEmpty(Request["ProductBrandChange_ID"]))
            int.TryParse(Request["ProductBrandChange_ID"].ToString(), out ProductBrandChange_ID);

        if (Common.GetFunctionGroupDN())
        {
            btnBack.Visible = btnCancel.Visible = btnSave.Visible = false;
            btnResend.Visible = true;
            txtChange.Enabled = true;
            txtNoteChange.Enabled = false;
        }

        if (MyUser.GetFunctionGroup_ID() == "1")
        {
            ddlPackage.Enabled = true;
        }
        else
        {
            ddlPackage.Enabled = false;
            //ddlRole.Enabled = false;
            //ddlRank.Enabled = false;
        }
        //if (Request.QueryString["mode"] != null)
        //{
        //    if (MyUser.GetProductBrandRole_ID() == "1")
        //    {
        //        lblMessage.Text = "Gửi yêu cầu thay đổi thông tin thành công !";
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Cập nhật thành công";
        //    }
        //    lblMessage.Visible = true;
        //}
        if (!IsPostBack)
        {
            //if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            //{
            //    if (MyUser.GetProductBrandRole_ID() == "1")
            //    {
            //        btnSave.Text = "Gửi yêu cầu thay đổi thông tin";
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
            FillDDL();
            //LoadRank();
            //LoadRole();
            LoadBusinessType();
            FillInfoProductBrand();
        }
    }
    //protected void LoadRole()
    //{
    //    try
    //    {
    //        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from ProductBrandRole");
    //        ddlRole.DataSource = dt;
    //        ddlRole.DataTextField = "Title";
    //        ddlRole.DataValueField = "ProductBrandRole_ID";
    //        ddlRole.DataBind();
    //        ddlRole.Items.Insert(0, new ListItem("-- Chọn quyền sửa đổi thông tin --", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("LoadModule", ex.ToString());
    //    }
    //}
    //protected void LoadRank()
    //{
    //    try
    //    {
    //        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from Rank");
    //        ddlRank.DataSource = dt;
    //        ddlRank.DataTextField = "Title";
    //        ddlRank.DataValueField = "Rank_ID";
    //        ddlRank.DataBind();
    //        ddlRank.Items.Insert(0, new ListItem("-- Chọn cấp bậc --", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("LoadModule", ex.ToString());
    //    }
    //}
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
            dtBranch = BusinessRulesLocator.GetBranchBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlBranch.DataSource = dtBranch;
            ddlBranch.DataTextField = "Name";
            ddlBranch.DataValueField = "Branch_ID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("-- Chọn ngành --", ""));


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
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillInfoProductBrand()
    {
        try
        {
            if (ProductBrandChange_ID != 0)
            {
                ProductBrandChangeRow _ProductBrandRow = new ProductBrandChangeRow();
                _ProductBrandRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(ProductBrandChange_ID);
                if (_ProductBrandRow != null)
                {
                    if (_ProductBrandRow.ProductBrandChange_Status == 1)
                    {
                        btnSave.Visible = btnCancel.Visible = false;
                    }
                    if (_ProductBrandRow.ProductBrandChange_Status == 2)
                    {
                        btnSave.Visible = btnCancel.Visible = false;
                    }


                    //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                    // MyActionPermission.CheckPermission(_ProductBrandRow.ProductBrandChange_ID.ToString(), _ProductBrandRow.CreateBy.ToString(), "/Admin/ProductBrand/ProductBrand_List");

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
                    ddlPackage.SelectedValue = _ProductBrandRow.IsFunctionGroup_IDNull ? "" : _ProductBrandRow.FunctionGroup_ID.ToString();
                    txtLinhVucKD.Text = _ProductBrandRow.IsBusinessAreaNull ? string.Empty : _ProductBrandRow.BusinessArea;
                    txtAddress.Text = _ProductBrandRow.IsAddressNull ? string.Empty : _ProductBrandRow.Address;
                    txtPhone.Text = _ProductBrandRow.IsTelephoneNull ? string.Empty : _ProductBrandRow.Telephone;
                    txtMobile.Text = _ProductBrandRow.IsMobileNull ? string.Empty : _ProductBrandRow.Mobile;
                    txtEmail.Text = _ProductBrandRow.IsEmailNull ? string.Empty : _ProductBrandRow.Email;
                    txtWebsite.Text = _ProductBrandRow.IsWebsiteNull ? string.Empty : _ProductBrandRow.Website;
                    txtFacebookID.Text = _ProductBrandRow.IsFacebookNull ? string.Empty : _ProductBrandRow.Facebook;
                    txtZalo.Text = _ProductBrandRow.IsZaloNull ? string.Empty : _ProductBrandRow.Zalo;
                    txtYoutube.Text = _ProductBrandRow.IsYouTubeNull ? string.Empty : _ProductBrandRow.YouTube;
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
                    //if (!_ProductBrandRow.IsRank_IDNull)
                    //{
                    //    ddlRank.SelectedValue = _ProductBrandRow.Rank_ID.ToString();
                    //}
                    //if (!_ProductBrandRow.IsProductBrandRole_IDNull)
                    //{
                    //    ddlRole.SelectedValue = _ProductBrandRow.ProductBrandRole_ID.ToString();
                    //}
                    if (!_ProductBrandRow.IsBusinessType_IDNull)
                    {
                        ddlBussines.SelectedValue = _ProductBrandRow.BusinessType_ID.ToString();
                    }

                    if (!_ProductBrandRow.IsChainLink_IDNull)
                    {
                        ddlChainlink.SelectedValue = _ProductBrandRow.ChainLink_ID.ToString();
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
                    txtChange.Text = _ProductBrandRow.ProductBrandChange_Note;
                    txtNoteChange.Text = _ProductBrandRow.IsProductBrandChange_ApprovedNoteNull ? string.Empty : _ProductBrandRow.ProductBrandChange_ApprovedNote;
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
            ProductBrandChangeRow _ProductBrandChangeRow = new ProductBrandChangeRow();
            if (ProductBrandChange_ID != 0)
            {
                _ProductBrandChangeRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(ProductBrandChange_ID);
                _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductBrandChangeRow.ProductBrand_ID);
                if (_ProductBrandRow != null)
                {

                    _ProductBrandRow.Name = _ProductBrandChangeRow.Name = txtName.Text;
                    if (ddlChainlink.SelectedValue != "0")
                    {
                        _ProductBrandRow.ChainLink_ID = _ProductBrandChangeRow.ChainLink_ID = Convert.ToInt32(ddlChainlink.SelectedValue);
                    }
                    _ProductBrandRow.BrandName = _ProductBrandChangeRow.BrandName = txtThuongHieu.Text;
                    _ProductBrandRow.TradingName = _ProductBrandChangeRow.TradingName = txtTenGiaoDich.Text.Trim();
                    _ProductBrandRow.TaxCode = _ProductBrandChangeRow.TaxCode = txtMST.Text.Trim();
                    _ProductBrandRow.RegistrationNumber = _ProductBrandChangeRow.RegistrationNumber = txtDKKD.Text.Trim();
                    if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandRow.IssuedDate = _ProductBrandChangeRow.IssuedDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
                    {
                        DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandRow.CreateDate = _ProductBrandChangeRow.CreateDate = date;
                    }
                    //_ProductBrandRow.Rank_ID = Convert.ToInt32(ddlRank.SelectedValue);
                    //_ProductBrandRow.ProductBrandRole_ID = Convert.ToInt32(ddlRole.SelectedValue);
                    _ProductBrandRow.FunctionGroup_ID = _ProductBrandChangeRow.FunctionGroup_ID = Convert.ToInt32(ddlPackage.SelectedValue);
                    _ProductBrandRow.Branch_ID = _ProductBrandChangeRow.Branch_ID = Convert.ToInt32(ddlBranch.SelectedValue);
                    _ProductBrandRow.BusinessType_ID = _ProductBrandChangeRow.BusinessType_ID = Convert.ToInt32(ddlBussines.SelectedValue);
                    _ProductBrandRow.ProductBrandType_ID_List = _ProductBrandChangeRow.ProductBrandType_ID_List = GetProductBrandType_ID();

                    _ProductBrandRow.BusinessArea = _ProductBrandChangeRow.BusinessArea = txtLinhVucKD.Text.Trim();
                    _ProductBrandRow.DepartmentMan_ID = _ProductBrandChangeRow.DepartmentMan_ID = Convert.ToInt32(ddlSo.SelectedValue);
                    _ProductBrandRow.Address = _ProductBrandChangeRow.Address = txtAddress.Text;
                    _ProductBrandRow.Location_ID = _ProductBrandChangeRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    _ProductBrandRow.District_ID = _ProductBrandChangeRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    _ProductBrandRow.Ward_ID = _ProductBrandChangeRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
                    _ProductBrandRow.Telephone = _ProductBrandChangeRow.Telephone = txtPhone.Text;
                    _ProductBrandRow.Mobile = _ProductBrandChangeRow.Mobile = txtMobile.Text;
                    _ProductBrandRow.Email = _ProductBrandChangeRow.Email = txtEmail.Text;
                    _ProductBrandRow.Website = _ProductBrandChangeRow.Website = txtWebsite.Text;
                    _ProductBrandRow.Facebook = _ProductBrandChangeRow.Facebook = txtFacebookID.Text;
                    _ProductBrandRow.YouTube = _ProductBrandChangeRow.YouTube = txtYoutube.Text;
                    _ProductBrandRow.Zalo = _ProductBrandChangeRow.Zalo = txtZalo.Text;
                    _ProductBrandRow.Hotline = _ProductBrandChangeRow.Hotline = txtHotline.Text;
                    _ProductBrandRow.Skype = _ProductBrandChangeRow.Skype = txtSkype.Text;
                    _ProductBrandRow.Description = _ProductBrandChangeRow.Description = txtHoso.Text;


                    // Thông tin người đại diện pháp nhân
                    _ProductBrandChangeRow.DirectorName = _ProductBrandRow.DirectorName = txtHotenPhapNhan.Text.Trim();
                    //_ProductBrandRow.DirectorBirthday = rdDirectorBirthday.SelectedDate;
                    if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
                    {
                        DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductBrandRow.DirectorBirthday = _ProductBrandChangeRow.DirectorBirthday = birth;
                    }
                    _ProductBrandRow.DirectorAddress = _ProductBrandChangeRow.DirectorAddress = txtDiaChiPhapNhan.Text.Trim();
                    _ProductBrandRow.DirectorMobile = _ProductBrandChangeRow.DirectorMobile = txtDienThoaiPhapNhan.Text.Trim();
                    _ProductBrandRow.DirectorEmail = _ProductBrandChangeRow.DirectorEmail = txtEmailPhapNhan.Text.Trim();
                    _ProductBrandRow.DirectorPosition = _ProductBrandChangeRow.DirectorPosition = txtChucVuPhapNhan.Text.Trim();

                    // Thông tin tài khoản
                    _ProductBrandRow.AccountUserName = _ProductBrandChangeRow.AccountUserName = txtTaiKhoan.Text.Trim();
                    _ProductBrandRow.AccountEmail = _ProductBrandChangeRow.AccountEmail = txtTaiKhoan.Text.Trim();

                    // Thông tin người liên hệ
                    _ProductBrandRow.PersonName = _ProductBrandChangeRow.PersonName = txtHoTenLienHe.Text;
                    _ProductBrandRow.PersonAddress = _ProductBrandChangeRow.PersonAddress = txtDiaChiLienHe.Text;
                    _ProductBrandRow.PersonMobile = _ProductBrandChangeRow.PersonMobile = txtDienThoaiLienHe.Text;
                    _ProductBrandRow.PersonEmail = _ProductBrandChangeRow.PersonEmail = txtEmailLienHe.Text;

                    _ProductBrandRow.PRInfo = _ProductBrandChangeRow.PRInfo = txtQuangBa.Text;
                    _ProductBrandRow.Story = _ProductBrandChangeRow.Story = txtStory.Text;
                    _ProductBrandRow.Agency = _ProductBrandChangeRow.Agency = txtDaiLy.Text;

                    _ProductBrandRow.GLN = _ProductBrandChangeRow.GLN = txtGLN.Text;
                    _ProductBrandRow.GCP = _ProductBrandChangeRow.GCP = txtGCP.Text;
                    // Hiển thị logo lên cổng truy xuất

                    _ProductBrandChangeRow.ShowLogo = _ProductBrandRow.ShowLogo = ckShowlogo.Checked == true ? true : false;
                    //if (_ProductBrandRow.IsURLNull)
                    //{
                    //    _ProductBrandChangeRow.URL= _ProductBrandRow.URL = Common.ConvertTitleDomain(_ProductBrandRow.Name) + "-" + ProductBrandChange_ID;
                    //}
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_ProductBrandChangeRow.IsImageNull)
                        {
                            if (_ProductBrandChangeRow.Image != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandChangeRow.Image.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = ProductBrandChange_ID + "-" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _ProductBrandRow.Image = _ProductBrandChangeRow.Image = fileimage;
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
                        if (!_ProductBrandChangeRow.IsProductionUnitCodeNull)
                        {
                            if (_ProductBrandChangeRow.ProductionUnitCode != null)
                            {
                                string strImgPUCPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandChangeRow.ProductionUnitCode.Replace("../", "");
                                if (File.Exists(strImgPUCPath))
                                {
                                    File.Delete(strImgPUCPath);
                                }
                            }
                        }
                        ImgPUC = ProductBrandChange_ID + "-" + ProductionUnitCode.FileName;
                        ProductionUnitCode.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + ImgPUC));
                        if (!string.IsNullOrEmpty(ImgPUC))
                        {
                            _ProductBrandRow.ProductionUnitCode = _ProductBrandChangeRow.ProductionUnitCode = ImgPUC;
                        }
                    }
                    else
                    {
                        if (!_ProductBrandChangeRow.IsProductionUnitCodeNull)
                            _ProductBrandRow.ProductionUnitCode = _ProductBrandChangeRow.ProductionUnitCode;
                    }

                    _ProductBrandRow.LastEditDate = _ProductBrandChangeRow.LastEditDate = DateTime.Now;
                    _ProductBrandRow.LastEditBy = _ProductBrandChangeRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductBrandChangeRow.ProductBrandChange_Status = 1;
                    _ProductBrandChangeRow.ProductBrandChange_DateApproved = DateTime.Now;
                    _ProductBrandChangeRow.ProductBrandChange_Approved = MyUser.GetUser_ID();
                    _ProductBrandChangeRow.ProductBrandChange_ApprovedNote = txtNoteChange.Text;
                    //
                    msg = "Duyệt thành công !";
                    BusinessRulesLocator.GetProductBrandChangeBO().Update(_ProductBrandChangeRow);
                    BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
                    //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thay đổi thông tin doanh nghiệp";
                    _NotificationRow.Summary = _ProductBrandChangeRow.ProductBrandChange_Note;
                    _NotificationRow.Body = _ProductBrandChangeRow.ProductBrandChange_ID.ToString();
                    _NotificationRow.NotificationType_ID = 12;
                    _NotificationRow.UserID = _ProductBrandChangeRow.ProductBrandChange_By;
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = _ProductBrandChangeRow.ProductBrand_ID;
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Admin/ProductBrand/ProductBrand_Edit?ProductBrand_ID=" + _ProductBrandChangeRow.ProductBrand_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
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
                    Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                    if (master != null)
                        master.LoadNotification();
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
        Response.Redirect("/Admin/Notification/RequestProductBrand_List.aspx", false);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ProductBrandChangeRow _ProductBrandChangeRow = new ProductBrandChangeRow();
        if (ProductBrandChange_ID != 0)
        {
            _ProductBrandChangeRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(ProductBrandChange_ID);
            if (_ProductBrandChangeRow != null)
            {
                _ProductBrandChangeRow.ProductBrandChange_Status = 2;
                _ProductBrandChangeRow.ProductBrandChange_DateApproved = DateTime.Now;
                _ProductBrandChangeRow.ProductBrandChange_Approved = MyUser.GetUser_ID();
                _ProductBrandChangeRow.ProductBrandChange_ApprovedNote = txtNoteChange.Text;


                BusinessRulesLocator.GetProductBrandChangeBO().Update(_ProductBrandChangeRow);

                NotificationRow _NotificationRow = new NotificationRow();

                //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                _NotificationRow.Summary = txtChange.Text;
                _NotificationRow.Body = _ProductBrandChangeRow.ProductBrandChange_ID.ToString();
                _NotificationRow.NotificationType_ID = 12;
                _NotificationRow.UserID = _ProductBrandChangeRow.ProductBrandChange_By;
                if (MyUser.GetFunctionGroup_ID() != "1")
                    _NotificationRow.ProductBrand_ID = _ProductBrandChangeRow.ProductBrand_ID;
                //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRow.Name = "Cơ quan quản lý đã không duyệt yêu cầu thay đổi thông tin doanh nghiệp";
                _NotificationRow.Url = "/Admin/ProductBrand/ProductBrand_ChangeEdit?ProductBrandChange_ID=" + _ProductBrandChangeRow.ProductBrandChange_ID;
                //}
                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                _NotificationRow.CreateDate = DateTime.Now;
                _NotificationRow.Active = 1;
                _NotificationRow.Alias = Guid.NewGuid();
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);

                lblMessage.Text = "Từ chối duyệt và gửi phản hồi thành công!";
                lblMessage.Visible = true;
            }
        }
    }

    protected void btnResend_Click(object sender, EventArgs e)
    {
        if (ProductBrandChange_ID != 0)
        {
            ProductBrandChangeRow _ProductBrandChangeRow = new ProductBrandChangeRow();
            _ProductBrandChangeRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(ProductBrandChange_ID);

            _ProductBrandChangeRow.Name = txtName.Text;
            if (ddlChainlink.SelectedValue != "0")
            {
                _ProductBrandChangeRow.ChainLink_ID = Convert.ToInt32(ddlChainlink.SelectedValue);
            }
            _ProductBrandChangeRow.BrandName = txtThuongHieu.Text;
            _ProductBrandChangeRow.TradingName = txtTenGiaoDich.Text.Trim();
            _ProductBrandChangeRow.TaxCode = txtMST.Text.Trim();
            _ProductBrandChangeRow.RegistrationNumber = txtDKKD.Text.Trim();
            if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
            {
                DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandChangeRow.IssuedDate = ngaycap;
            }
            if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandChangeRow.CreateDate = date;
            }
            //_ProductBrandRow.Rank_ID = Convert.ToInt32(ddlRank.SelectedValue);
            //_ProductBrandRow.ProductBrandRole_ID = Convert.ToInt32(ddlRole.SelectedValue);
            _ProductBrandChangeRow.FunctionGroup_ID = Convert.ToInt32(ddlPackage.SelectedValue);
            _ProductBrandChangeRow.Branch_ID = Convert.ToInt32(ddlBranch.SelectedValue);
            _ProductBrandChangeRow.BusinessType_ID = Convert.ToInt32(ddlBussines.SelectedValue);
            _ProductBrandChangeRow.ProductBrandType_ID_List = GetProductBrandType_ID();

            _ProductBrandChangeRow.BusinessArea = txtLinhVucKD.Text.Trim();
            _ProductBrandChangeRow.DepartmentMan_ID = Convert.ToInt32(ddlSo.SelectedValue);
            _ProductBrandChangeRow.Address = txtAddress.Text;
            _ProductBrandChangeRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
            _ProductBrandChangeRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
            _ProductBrandChangeRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
            _ProductBrandChangeRow.Telephone = txtPhone.Text;
            _ProductBrandChangeRow.Mobile = txtMobile.Text;
            _ProductBrandChangeRow.Email = txtEmail.Text;
            _ProductBrandChangeRow.Website = txtWebsite.Text;
            _ProductBrandChangeRow.Facebook = txtFacebookID.Text;
            _ProductBrandChangeRow.Zalo = txtZalo.Text;
            _ProductBrandChangeRow.Hotline = txtHotline.Text;
            _ProductBrandChangeRow.Skype = txtSkype.Text;
            _ProductBrandChangeRow.Description = txtHoso.Text;


            // Thông tin người đại diện pháp nhân
            _ProductBrandChangeRow.DirectorName = txtHotenPhapNhan.Text.Trim();
            //_ProductBrandRow.DirectorBirthday = rdDirectorBirthday.SelectedDate;
            if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
            {
                DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductBrandChangeRow.DirectorBirthday = birth;
            }
            _ProductBrandChangeRow.DirectorAddress = txtDiaChiPhapNhan.Text.Trim();
            _ProductBrandChangeRow.DirectorMobile = txtDienThoaiPhapNhan.Text.Trim();
            _ProductBrandChangeRow.DirectorEmail = txtEmailPhapNhan.Text.Trim();
            _ProductBrandChangeRow.DirectorPosition = txtChucVuPhapNhan.Text.Trim();

            // Thông tin tài khoản
            _ProductBrandChangeRow.AccountUserName = txtTaiKhoan.Text.Trim();
            _ProductBrandChangeRow.AccountEmail = txtTaiKhoan.Text.Trim();

            // Thông tin người liên hệ
            _ProductBrandChangeRow.PersonName = txtHoTenLienHe.Text;
            _ProductBrandChangeRow.PersonAddress = txtDiaChiLienHe.Text;
            _ProductBrandChangeRow.PersonMobile = txtDienThoaiLienHe.Text;
            _ProductBrandChangeRow.PersonEmail = txtEmailLienHe.Text;

            _ProductBrandChangeRow.PRInfo = txtQuangBa.Text;
            _ProductBrandChangeRow.Story = txtStory.Text;
            _ProductBrandChangeRow.Agency = txtDaiLy.Text;

            _ProductBrandChangeRow.GLN = txtGLN.Text;
            _ProductBrandChangeRow.GCP = txtGCP.Text;

            //if (_ProductBrandRow.IsURLNull)
            //{
            //    _ProductBrandChangeRow.URL= _ProductBrandRow.URL = Common.ConvertTitleDomain(_ProductBrandRow.Name) + "-" + ProductBrandChange_ID;
            //}
            string fileimage = "";
            if (fulAnh.HasFile)
            {
                //Xóa file
                if (!_ProductBrandChangeRow.IsImageNull)
                {
                    if (_ProductBrandChangeRow.Image != null)
                    {
                        string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandChangeRow.Image.Replace("../", "");
                        if (File.Exists(strFileFullPath))
                        {
                            File.Delete(strFileFullPath);
                        }
                    }
                }
                fileimage = ProductBrandChange_ID + "-" + fulAnh.FileName;
                fulAnh.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _ProductBrandChangeRow.Image = fileimage;
                }
            }


            string ImgPUC = "";
            if (ProductionUnitCode.HasFile)
            {
                if (!_ProductBrandChangeRow.IsProductionUnitCodeNull)
                {
                    if (_ProductBrandChangeRow.ProductionUnitCode != null)
                    {
                        string strImgPUCPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductBrandChangeRow.ProductionUnitCode.Replace("../", "");
                        if (File.Exists(strImgPUCPath))
                        {
                            File.Delete(strImgPUCPath);
                        }
                    }
                }
                ImgPUC = ProductBrandChange_ID + "-" + ProductionUnitCode.FileName;
                ProductionUnitCode.SaveAs(Server.MapPath("../../data/productbrand/mainimages/original/" + ImgPUC));
                if (!string.IsNullOrEmpty(ImgPUC))
                {
                    _ProductBrandChangeRow.ProductionUnitCode = ImgPUC;
                }
            }

            _ProductBrandChangeRow.ProductBrandChange_Status = 0;
            _ProductBrandChangeRow.ProductBrandChange_Note = string.IsNullOrEmpty(txtChange.Text) ? "Thay đổi thông tin doanh nghiệp " : txtChange.Text;
            _ProductBrandChangeRow.ProductBrandChange_By = MyUser.GetUser_ID();
            _ProductBrandChangeRow.ProductBrandChange_Date = DateTime.Now;

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
                        lblMessage.Text = "Gửi yêu cầu thay đổi thông tin thành công !";
                        lblMessage.Visible = true;
                    }
                }
            }

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