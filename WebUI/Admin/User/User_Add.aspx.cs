using DbObj;
using evointernal;
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

public partial class User_Add : System.Web.UI.Page
{
    public string title = "Thêm mới tài khoản";
    public string avatar = "";
    MembershipUser user;
    UserProfile UserProfile;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtBirth.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {

            LoadRank();
            LoadModule();
            LoadCity();
            LoadDistrict();
            LoadWard();
            FillDDLFunctionGroup();
            LoadChainLink();
            FillDDLSale();
            if (Common.GetFunctionGroupDN())
            {
                //ddlFunctionGroup.SelectedValue = "2";
                //if (MyUser.GetFunctionGroup_ID() == "4")
                //{
                //    ddlFunctionGroup.SelectedValue = "4";
                //}

                ddlFunctionGroup.SelectedValue = MyUser.GetFunctionGroup_ID();
                ddlFunctionGroup.Enabled = false;
                ddlProductBrand.Enabled = false;
                ddlProductBrand.Visible = true;
                ddlDepartment.Visible = true;
                HideCapBac.Visible = HideGioiTinh.Visible = HideNgaySinh.Visible = HideSdt.Visible = false;


            }


            LoadProductBrand();
            // LoadProductBrandList();
            FillDDLDeparment();
            FillDDLFunction();

        }
        lblMessage.Text = lblMessage1.Text = lblMsg.Text = "";
    }

    private void LoadChainLink()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlChainLink.DataSource = dt;
            ddlChainLink.DataTextField = "Name";
            ddlChainLink.DataValueField = "ChainLink_ID";
            ddlChainLink.DataBind();
            ddlChainLink.Items.Insert(0, new ListItem("-- Chọn chuỗi liên kết--", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChainLink", ex.ToString());
        }
    }
    protected void LoadProductBrand()
    {
        if (!string.IsNullOrEmpty(ddlFunctionGroup.SelectedValue))
        {
            Common.FillProductBrand_Null(ddlProductBrand, " and FunctionGroup_ID=" + ddlFunctionGroup.SelectedValue);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                LoadWorkShop();
            }
        }
    }
    protected void LoadProductBrandList()
    {
        string where = "";
        if (ddlChainLink.SelectedValue != "0" && ddlChainLink.SelectedValue != "")
        {
            where += " and ChainLink_ID=" + ddlChainLink.SelectedValue;
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductBrand_ID, Name from ProductBrand where  Active=1 " + where + " order by Sort, Name ASC");
        ddlProductBrandList.DataSource = dt;
        ddlProductBrandList.DataTextField = "Name";
        ddlProductBrandList.DataValueField = "ProductBrand_ID";
        ddlProductBrandList.DataBind();
    }
    protected string GetFunction()
    {

        string FunctionGroup_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlFunction.Items)
            {
                if (item.Checked)
                {
                    FunctionGroup_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(FunctionGroup_ID))
            {
                FunctionGroup_ID = FunctionGroup_ID.Substring(0, FunctionGroup_ID.Length - 1);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return FunctionGroup_ID;
    }
    private void FillDDLFunctionGroup()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2" || MyUser.GetRank_ID() == "3" || MyUser.GetRank_ID() == "4" || MyUser.GetRank_ID() == "5")
                {
                    where += " and FunctionGroup_ID not in(1) ";
                }
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active=1" + where, " Sort ASC");
            ddlFunctionGroup.DataSource = dt;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", ""));
            if (MyUser.GetFunctionGroup_ID() == "9")
            {
                if (MyUser.GetAccountCheckVN_ID() == "1")
                {
                    ddlFunctionGroup.SelectedValue = "9";
                    ddlFunctionGroup_SelectedIndexChanged(null, null);
                    ddlFunctionGroup.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLDeparment()
    {
        try
        {
            DataTable dt = new DataTable();

            dt = BusinessRulesLocator.GetAccountTypeBO().GetAsDataTable(" Active=1", " Sort ASC");

            if (MyUser.GetAccountType_ID() == "2" || MyUser.GetAccountType_ID() == "7" || MyUser.GetAccountType_ID() == "8")
            {
                dt = BusinessRulesLocator.GetAccountTypeBO().GetAsDataTable(" Active=1 and AccountType_ID >=" + MyUser.GetAccountType_ID(), " Sort ASC");
            }
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataValueField = "AccountType_ID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- Chọn loại tài khoản --", "0"));


        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLDeparment", ex.ToString());
        }
    }
    private void FillDDLFunction()
    {
        try
        {
            string Where = string.Empty;
            if (ddlFunctionGroup.SelectedValue != "")
            {
                Where += " and FunctionGroup_ID like '%," + ddlFunctionGroup.SelectedValue + ",%'";
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionBO().GetAsDataTable(" Active=1 " + Where, " Sort ASC");
            ddlFunction.DataSource = dt;
            ddlFunction.DataTextField = "Name";
            ddlFunction.DataValueField = "Function_ID";
            ddlFunction.DataBind();
            //ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLSale()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAccount_CheckVNBO().GetAsDataTable(" Active=1", "");
            ddlSaleCheckVN.DataSource = dt;
            ddlSaleCheckVN.DataTextField = "Name";
            ddlSaleCheckVN.DataValueField = "Account_ID";
            ddlSaleCheckVN.DataBind();
            ddlSaleCheckVN.Items.Insert(0, new ListItem("-- Chọn nhóm tài khoản --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLFunctionDetail()
    {
        try
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(GetFunction()))
            {
                where += " and Function_ID in (" + GetFunction() + ")";
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetFunctionBO().GetAsDataTable(" Active=1" + where, " Sort ASC");
                rptFunction.DataSource = dt;
                rptFunction.DataBind();
                //btnSaveRole1.Visible = true;
                //btnSaveRole.Visible = true;
            }
            else
            {
                rptFunction.DataSource = null;
                rptFunction.DataBind();
            }

            //ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx", false);
        }
        catch (Exception ex)
        {
            Log.writeLog("btnReset_Click", ex.ToString());
        }
    }
    private void UpdateProfile()
    {
        UserProfile = UserProfile.GetProfile(txtUser.Text);
        UserProfile.FullName = txtFullName.Text;
        if (ddlGioiTinh.SelectedIndex > 0)
        {
            UserProfile.Gender = ddlGioiTinh.SelectedValue;
        }
        if (ddlHeThong.SelectedValue != "")
        {
            UserProfile.Module_ID = (ddlHeThong.SelectedValue);
        }

        if (ddlSaleCheckVN.SelectedValue != "")
        {
            UserProfile.AccountCheckVN_ID = (ddlSaleCheckVN.SelectedValue);
        }
        UserProfile.DepartmentMan_ID = (ddlDepartmentMan.SelectedValue);
        //UserProfile.Company = txtCompany.Text;
        UserProfile.Address = txtAddress.Text;
        UserProfile.Phone = txtPhone.Text;
        if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
        {
            DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            UserProfile.BirthDate = birth;
        }
        string fileimage = "";
        if (fulAnh.HasFile)
        {
            ////Xóa file
            //if (UserProfile.AvatarUrl != null)
            //{
            //    string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + UserProfile.AvatarUrl.Replace("../", "");
            //    if (File.Exists(strFileFullPath))
            //    {
            //        File.Delete(strFileFullPath);
            //    }
            //}
            fileimage = "../../data/user/avatar/original/" + Common.CreateImgName(fulAnh.FileName);
            fulAnh.SaveAs(Server.MapPath(fileimage));
            if (!string.IsNullOrEmpty(fileimage))
            {
                UserProfile.AvatarUrl = fileimage;
            }
        }
        if (ddlProductBrand.SelectedValue != "")
        {
            UserProfile.ProductBrand_ID = (ddlProductBrand.SelectedValue);
        }
        if (ddlFunctionGroup.SelectedValue != "")
        {
            UserProfile.FunctionGroup_ID = (ddlFunctionGroup.SelectedValue);
            if (ddlFunctionGroup.SelectedValue == "9")
            {
                UserProfile.IsSale = "1";
            }
        }
        if (ddlLocation.SelectedValue != "")
        {
            UserProfile.Location_ID = (ddlLocation.SelectedValue);
        }
        if (ddlDistrict.SelectedValue != "")
        {
            UserProfile.District_ID = (ddlDistrict.SelectedValue);
        }
        if (ddlWard.SelectedValue != "")
        {
            UserProfile.Ward_ID = (ddlWard.SelectedValue);
        }
        if (ddlRank.SelectedValue != "")
        {
            UserProfile.Rank_ID = (ddlRank.SelectedValue);
        }
        if (MyUser.GetRank_ID() == "5")
        {
            UserProfile.Rank_ID = "5";
            UserProfile.Location_ID = MyUser.GetLocation_ID();
            UserProfile.District_ID = MyUser.GetDistrict_ID();
            UserProfile.Ward_ID = MyUser.GetWard_ID();
        }
        if (ddlDepartment.SelectedValue != "")
        {
            UserProfile.AccountType_ID = (ddlDepartment.SelectedValue);
            if (ddlDepartment.SelectedValue == "2")
            {
                UserProfile.Department_ID = (ddlDepartmentUser.SelectedValue);
                UserProfile.Zone_ID = (ddlZone.SelectedValue);
                UserProfile.Area_ID = (ddlArea.SelectedValue);
                UserProfile.Farm_ID = (ddlFarm.SelectedValue);
                if (ddlWarehouse.CheckedItems.Count > 0)
                {
                    UserProfile.Warehouse_ID = ListWarehouse_ID();
                }
                if (ddlWorkshop.SelectedValue != "0")
                {
                    UserProfile.Workshop_ID = (ddlWorkshop.SelectedValue);
                }
                //HideGioiTinh.Visible = false;
                //HideHoTen.Visible = false;
                //HideNgaySinh.Visible = false;
            }
            if (ddlDepartment.SelectedValue == "7")
            {
                UserProfile.Department_ID = (ddlDepartmentUser.SelectedValue);
                UserProfile.Zone_ID = (ddlZone.SelectedValue);
                //UserProfile.Area_ID = (ddlArea.SelectedValue);
                //UserProfile.Farm_ID = (ddlFarm.SelectedValue);
                //if (ddlWorkshop.SelectedValue != "0")
                //{
                //    UserProfile.Workshop_ID = (ddlWorkshop.SelectedValue);
                //}
                //HideGioiTinh.Visible = false;
                //HideHoTen.Visible = false;
                //HideNgaySinh.Visible = false;
            }
            if (ddlDepartment.SelectedValue == "8")
            {
                UserProfile.Department_ID = (ddlDepartmentUser.SelectedValue);
                UserProfile.Zone_ID = (ddlZone.SelectedValue);
                UserProfile.Area_ID = (ddlArea.SelectedValue);
            }
            else
            {
                //HideGioiTinh.Visible = true;
                //HideHoTen.Visible = true;
                //HideNgaySinh.Visible = true;
            }
        }
        UserProfile.CreateBy = Convert.ToString(MyUser.GetUser_ID());
        UserProfile.Save();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            if (Page.IsValid)
            {
                MembershipCreateStatus createStatus = MembershipCreateStatus.Success;
                try
                {
                    MembershipUser newUser = Membership.CreateUser(txtUser.Text, txtPass.Text, txtEmail.Text);
                }
                catch (MembershipCreateUserException ex)
                {
                    createStatus = ex.StatusCode;
                    lblMessage.Visible = true;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }

                switch (createStatus)
                {
                    case MembershipCreateStatus.Success:
                        UpdateProfile();
                        SaveRole(MyUser.GetUser_IDByUserName(txtUser.Text).ToString());
                        if (Request.QueryString["IsSale"] != null)
                        {
                            Response.Redirect("User_ListKD.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("User_List.aspx", false);
                        }

                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        lblMessage.Text = "Người dùng này đã tồn tại";

                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        lblMessage.Text = "Email này đã tồn tại";
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        lblMessage.Text = "Email chưa đúng";
                        break;
                    case MembershipCreateStatus.InvalidQuestion:
                        lblMessage.Text = "Câu hỏi bảo mật chưa đúng";
                        break;
                    case MembershipCreateStatus.InvalidAnswer:
                        lblMessage.Text = "Câu trả lời bảo mật chưa đúng";
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        lblMessage.Text = "Mật khẩu không đúng";
                        break;
                    default:
                        lblMessage.Text = "Có lỗi xảy ra.";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    private string GetListProductBrand_ID()
    {
        string ProductBrand_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlProductBrandList.Items)
            {
                if (item.Selected)
                {
                    ProductBrand_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ProductBrand_ID))
            {
                ProductBrand_ID = ProductBrand_ID.Remove(ProductBrand_ID.Length - 1);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetListProductBrand_ID", ex.ToString());
        }
        return ProductBrand_ID;
    }
    protected void SaveRole(string UserId)
    {
        if (ddlFunctionGroup.SelectedValue == "2" && MyUser.GetFunctionGroup_ID() == "1")
        {
            BusinessRulesLocator.Conllection().UpdateUserVsPageFunctionProductBrandV2(UserId, "2");
        }
        else if (ddlFunctionGroup.SelectedValue == "4" && MyUser.GetFunctionGroup_ID() == "1")
        {
            BusinessRulesLocator.Conllection().UpdateUserVsPageFunctionProductBrandV2(UserId, "4");
        }
        else if (ddlFunctionGroup.SelectedValue == "5" && MyUser.GetFunctionGroup_ID() == "1")
        {
            BusinessRulesLocator.Conllection().UpdateUserVsPageFunctionProductBrandV2(UserId, "5");
        }
        else if (ddlFunctionGroup.SelectedValue == "6" && MyUser.GetFunctionGroup_ID() == "1")
        {
            BusinessRulesLocator.Conllection().UpdateUserVsPageFunctionProductBrandV2(UserId, "6");
        }
        else
        {
            string roleID = string.Empty;
            foreach (RepeaterItem item in rptFunction.Items)
            {
                Repeater rptPage = item.FindControl("rptPage") as Repeater;
                foreach (RepeaterItem itemRole in rptPage.Items)
                {
                    Literal lblPageFunction_ID = itemRole.FindControl("lblPageFunction_ID") as Literal;
                    CheckBox ckRole = itemRole.FindControl("ckRole") as CheckBox;
                    if (ckRole.Checked)
                    {
                        roleID += lblPageFunction_ID.Text + ",";
                    }
                }
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                roleID = roleID.Remove(roleID.Length - 1);
                BusinessRulesLocator.Conllection().UpdateUserVsPageFunction(UserId, roleID);
            }

            UserVsProductBrandRow _UserVsProductBrandRow = new UserVsProductBrandRow();
            if (!string.IsNullOrEmpty(GetListProductBrand_ID()))
            {
                _UserVsProductBrandRow.ProductBrand_ID_List = GetListProductBrand_ID();
                _UserVsProductBrandRow.UserId = new Guid(UserId);
                BusinessRulesLocator.GetUserVsProductBrandBO().Insert(_UserVsProductBrandRow);
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("User_List.aspx", false);
    }

    protected void rptFunction_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblFunction_ID = e.Item.FindControl("lblFunction_ID") as Literal;
            Repeater rptPage = e.Item.FindControl("rptPage") as Repeater;

            if (lblFunction_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetPageFunctionBO().GetAsDataTable("Function_ID =" + lblFunction_ID.Text, " SORT ASC");
                if (dt.Rows.Count > 0)
                {
                    rptPage.DataSource = dt;
                    rptPage.DataBind();
                }

            }
        }

    }
    protected void ddlFunction_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FillDDLFunctionDetail();
    }
    protected void ddlFunctionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        RolePermission.Visible = true;
        LoadProductBrand();
        FillDDLFunction();
        if (ddlFunctionGroup.SelectedValue == "1" || ddlFunctionGroup.SelectedValue == "8" || ddlFunctionGroup.SelectedValue == "9" || ddlFunctionGroup.SelectedValue == "10")
        {
            ddlProductBrand.Visible = false;
            ddlDepartment.Visible = false;
            ddlChainLink.Visible = false;
            ddlProductBrandList.Visible = false;
            if (ddlFunctionGroup.SelectedValue == "9")
            {
                HideCapBac.Visible = false;
                u.Visible = false;
                ddlHeThong.Enabled = false;
                DivSale.Visible = true;
            }
            else
            {
                DivSale.Visible = false;
                HideCapBac.Visible = true;
                u.Visible = true;
                ddlHeThong.Enabled = true;
            }
            if (ddlFunctionGroup.SelectedValue == "10")
            {
                HideCapBac.Visible = false;
            }
        }
        else
        {
            if (ddlFunctionGroup.SelectedValue == "3")
            {
                ddlChainLink.Visible = true;
                LoadProductBrandList();
                ddlProductBrandList.Visible = true;
                ddlDepartment.Visible = false;
                ddlProductBrand.Visible = false;
                DivSale.Visible = false;
            }
            else
            {
                ddlChainLink.Visible = false;
                ddlProductBrandList.Visible = false;
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    RolePermission.Visible = false;
                    ddlDepartment.SelectedValue = "1";
                    ddlDepartment.Enabled = false;
                }
                else
                {
                    RolePermission.Visible = true;
                    ddlDepartment.SelectedValue = "0";
                    ddlDepartment.Enabled = true;
                }
                ddlProductBrand.Visible = true;
                ddlDepartment.Visible = true;
            }

        }
    }
    protected void LoadDepartmentUser()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID(), " Sort ASC");
            ddlDepartmentUser.DataSource = dt;
            ddlDepartmentUser.DataTextField = "Name";
            ddlDepartmentUser.DataValueField = "Department_ID";
            ddlDepartmentUser.DataBind();
            ddlDepartmentUser.Items.Insert(0, new ListItem("-- Chọn phòng ban --", ""));
            // ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

            if (MyUser.GetAccountType_ID() == "2" || MyUser.GetAccountType_ID() == "7" || MyUser.GetAccountType_ID() == "8")
            {
                if (MyUser.GetDepartment_ID() != "")
                {
                    ddlDepartmentUser.SelectedValue = MyUser.GetDepartment_ID();
                    LoadWorkShop();
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void LoadWarehouse()
    {
        try
        {
            string where = string.Empty;
            if (ddlDepartmentUser.SelectedValue != "")
            {
                where += " and Department_ID = " + ddlDepartmentUser.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID() + where, " Zone_ID DESC");
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            //  ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void LoadZone()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID(), " Zone_ID DESC");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadZone", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAreaBO().GetAsDataTable("  ProductBrand_ID=" + MyUser.GetProductBrand_ID(), " Area_ID DESC");
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu vực --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadArea", ex.ToString());
        }
    }
    protected void LoadFarm()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFarmBO().GetAsDataTable("  ProductBrand_ID=" + MyUser.GetProductBrand_ID(), " Farm_ID DESC");
            ddlFarm.DataSource = dt;
            ddlFarm.DataTextField = "Name";
            ddlFarm.DataValueField = "Farm_ID";
            ddlFarm.DataBind();
            ddlFarm.Items.Insert(0, new ListItem("-- Chọn thửa --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadArea", ex.ToString());
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "2")
        {
            phongban.Visible = true;
            LoadDepartmentUser();
            LoadZone();
            LoadArea();
            LoadFarm();
            LoadWarehouse();
            HideGioiTinh.Visible = false;
            HideHoTen.Visible = false;
            HideNgaySinh.Visible = false;
            ddlWorkshop.Visible = ddlArea.Visible = ddlFarm.Visible = ddlWarehouse.Visible = true;
        }
        else if (ddlDepartment.SelectedValue == "7")
        {
            phongban.Visible = true;
            LoadDepartmentUser();
            LoadZone();
            ddlWorkshop.Visible = ddlArea.Visible = ddlFarm.Visible = ddlWarehouse.Visible = ddlDepartmentUser.Visible = false;
            //LoadArea();
            //LoadFarm();

            HideGioiTinh.Visible = false;
            HideHoTen.Visible = false;
            HideNgaySinh.Visible = false;
        }
        else if (ddlDepartment.SelectedValue == "8")
        {
            phongban.Visible = true;
            LoadDepartmentUser();
            LoadZone();
            LoadArea();
            ddlArea.Visible = true;
            ddlWorkshop.Visible = ddlFarm.Visible = ddlWarehouse.Visible = ddlDepartmentUser.Visible = false;
            HideGioiTinh.Visible = false;
            HideHoTen.Visible = false;
            HideNgaySinh.Visible = false;
        }
        else
        {
            HideGioiTinh.Visible = true;
            HideHoTen.Visible = true;
            HideNgaySinh.Visible = true;
            phongban.Visible = false;
        }
    }
    protected void ddlDepartmentUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkShop();
        LoadWarehouse();
    }
    private void LoadWorkShop()
    {
        try
        {
            DataTable dt = new DataTable();

            string where = " Active=1 and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            if (ddlDepartmentUser.SelectedValue != "")
            {
                where += " and Department_ID=" + ddlDepartmentUser.SelectedValue;
            }
            dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                ddlWorkshop.DataSource = dt;
                ddlWorkshop.DataValueField = "Workshop_ID";
                ddlWorkshop.DataTextField = "Name";
                ddlWorkshop.DataBind();
                ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên/ hộ sx --", ""));
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadWorkShop", ex.ToString());
        }
    }
    protected void ddlChainLink_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductBrandList();
    }
    protected void LoadCity()
    {
        try
        {
            DataTable dtLocation = new DataTable();
            string Location = MyUser.GetLocation_ID();
            dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", " Name ASC");
            ddlLocation.DataSource = dtLocation;
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataValueField = "Location_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("-- Chọn tỉnh/ thành phố --", ""));
            ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/ huyện --", ""));
            ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/ xã --", ""));
            if (MyUser.GetRank_ID() == "5")
            {
                u.Visible = false;

            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadCity", ex.ToString());
        }

    }
    protected void LoadDistrict()
    {
        try
        {
            ddlDistrict.Items.Clear();
            string where = string.Empty;
            if (ddlLocation.SelectedValue != "")
            {
                where = " Location_ID = " + ddlLocation.SelectedValue;
            }
            DataTable dtDitrict = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(where, " Name ASC");
            ddlDistrict.DataSource = dtDitrict;
            ddlDistrict.DataTextField = "Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadDistrict", ex.ToString());
        }
    }
    protected void LoadWard()
    {
        try
        {
            ddlWard.Items.Clear();
            DataTable dtLocation = new DataTable();
            string where = string.Empty;
            if (ddlDistrict.SelectedValue != "")
            {
                where = " District_ID=" + ddlDistrict.SelectedValue;
            }
            dtLocation = BusinessRulesLocator.GetWardBO().GetAsDataTable(where, " Name ASC");
            ddlWard.DataSource = dtLocation;
            ddlWard.DataTextField = "Name";
            ddlWard.DataValueField = "Ward_ID";
            ddlWard.DataBind();
            ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadWard", ex.ToString());
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWard();
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
            ddlRank.Items.Insert(0, new ListItem("-- Chọn cấp bậc tài khoản --", ""));

            DataTable dt1 = new DataTable();
            dt1 = BusinessRulesLocator.GetDepartmentManBO().GetAsDataTable("", " ");
            ddlDepartmentMan.DataSource = dt1;
            ddlDepartmentMan.DataTextField = "Name";
            ddlDepartmentMan.DataValueField = "DepartmentMan_ID";
            ddlDepartmentMan.DataBind();
            ddlDepartmentMan.Items.Insert(0, new ListItem("-- Chọn sở --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadModule", ex.ToString());
        }
    }
    protected void LoadModule()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select * from Module");
            ddlHeThong.DataSource = dt;
            ddlHeThong.DataTextField = "Title";
            ddlHeThong.DataValueField = "Module_ID";
            ddlHeThong.DataBind();
            ddlHeThong.Items.Insert(0, new ListItem("-- Chọn hệ thống --", ""));
            if (!string.IsNullOrEmpty(MyUser.GetModule_ID()))
            {
                ddlHeThong.SelectedValue = MyUser.GetModule_ID();
                if (MyUser.GetFunctionGroup_ID() != "1")
                {
                    if (ddlHeThong.SelectedValue != "3")
                    {
                        ddlHeThong.Enabled = false;
                    }
                }
            }
            else
            {
                ddlHeThong.SelectedValue = "1";
                if (MyUser.GetFunctionGroup_ID() != "1")
                    ddlHeThong.Enabled = false;
            }
            ddlHeThong_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadModule", ex.ToString());
        }
    }
    protected void ddlHeThong_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHeThong.SelectedValue == "1")
        {
            pnEs.Visible = true;
            pnCTT.Visible = false;
        }
        else if (ddlHeThong.SelectedValue == "2")
        {
            pnEs.Visible = false;
            pnCTT.Visible = true;
        }
        else if (ddlHeThong.SelectedValue == "2")
        {
            pnEs.Visible = false;
            pnCTT.Visible = false;
        }
        else
        {
            pnEs.Visible = false;
            pnCTT.Visible = false;
        }
    }

    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        CapSo.Visible = true;
        if (ddlRank.SelectedValue == "1")
        {
            u.Visible = false;
            CapSo.Visible = false;
        }
        if (ddlRank.SelectedValue == "2")
        {
            u.Visible = true;
            Huyen.Visible = PhuongXa.Visible = false;
            Tinh.Visible = true;
        }
        if (ddlRank.SelectedValue == "3")
        {
            PhuongXa.Visible = false;
            Tinh.Visible = Huyen.Visible = true;
            u.Visible = true;
        }
        if (ddlRank.SelectedValue == "4" || ddlRank.SelectedValue == "")
        {
            Tinh.Visible = Huyen.Visible = PhuongXa.Visible = true;
            u.Visible = true;
        }
        string where = string.Empty;
        if (MyUser.GetFunctionGroup_ID() == "8")
        {
            if (MyUser.GetRank_ID() == "3" || MyUser.GetRank_ID() == "4")
            {
                where += " and FunctionGroup_ID not in(1) ";
            }
        }
        if (ddlRank.SelectedValue == "5")
        {
            CapSo.Visible = u.Visible = false;
            DataTable dtFunctionGroup = new DataTable();
            dtFunctionGroup = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active=1 And Type=1 ", " Sort ASC");
            ddlFunctionGroup.DataSource = dtFunctionGroup;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", ""));
        }
        else
        {
            DataTable dtFunctionGroup = new DataTable();
            dtFunctionGroup = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active=1" + where, " Sort ASC");
            ddlFunctionGroup.DataSource = dtFunctionGroup;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", ""));
        }

    }
    protected string ListWarehouse_ID()
    {
        string ListWareHouse_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlWarehouse.Items)
            {
                if (item.Checked)
                {
                    ListWareHouse_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ListWareHouse_ID))
            {
                ListWareHouse_ID = ListWareHouse_ID.Substring(0, ListWareHouse_ID.Length - 1);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return ListWareHouse_ID;
    }
}