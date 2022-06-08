using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class RequestProductBrand_List : System.Web.UI.Page
{
    int ProductCategory_ID = 0;
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 5;//Số bản ghi 1 trang
    private int productCategory_ID;
    public string Message = "";
    public string style = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["update"] != null)
            {

                lblMessage.Text = "Duyệt yêu cầu thay đổi thông tin doanh nghiệp thành công !";
                lblMessage.Visible = true;
            }
            LoadLocation();
            FillProductBrand();
            LoadRequest();
        }
        //   ResetMsg();
        //Admin_Template_CMS master = this.Master as Admin_Template_CMS;
        //if (master != null)
        //    master.LoadNotification();
    }

    private void LoadLocation()
    {
        try
        {
            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", " Name ASC");
            ddlLocation.DataSource = dtLocation;
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataValueField = "Location_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("-- Chọn tỉnh/ thành phố --", "0"));
            if (MyUser.GetRank_ID() == "2")
            {
                ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                ddlLocation.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadLocation", ex.ToString());
        }
    }

    private void LoadRequest()
    {
        try
        {

            //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();

            dtSet = BusinessRulesLocator.Conllection().GetRequestProductBrandList_Paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), 0, Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), Convert.ToInt32(ddlStatus.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);

            grdData.DataSource = dtSet.Tables[0];
            grdData.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : true;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadRequest", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadRequest();
    }
    private void FillProductBrand()
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
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadRequest();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequest();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequest();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequest();
    }


    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequest();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequest();
    }

    protected void grdData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductBrandChange_Status = e.Item.FindControl("lblProductBrandChange_Status") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblProductBrandChange_Status != null)
            {
                if (lblProductBrandChange_Status.Text == "0")
                {
                    btnActive.Visible = true;
                }
                else
                {
                    btnActive.Visible = false;
                }
            }
            if (Common.GetFunctionGroupDN())
            {
                btnActive.Visible = false;
            }

        }
    }

    protected void grdData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {


            int ProductBrandChange_ID = Convert.ToInt32(e.CommandArgument);
            ProductBrandChangeRow _ProductBrandChangeRow = new ProductBrandChangeRow();
            _ProductBrandChangeRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(ProductBrandChange_ID);
            Literal lblProductBrand_ID = e.Item.FindControl("lblProductBrand_ID") as Literal;

            ProductBrandRow _ProductBrandRow = new ProductBrandRow();
            _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(lblProductBrand_ID.Text));
            switch (e.CommandName)
            {

                case "Active":

                    if (_ProductBrandRow != null && _ProductBrandChangeRow != null)
                    {
                        //Cập nhật thông tin doanh nghiệp ở bạng tạm vào bảng chính
                        _ProductBrandRow.Name = _ProductBrandChangeRow.Name;
                        _ProductBrandRow.ChainLink_ID = _ProductBrandChangeRow.IsChainLink_IDNull ? 0 : _ProductBrandChangeRow.ChainLink_ID;
                        _ProductBrandRow.BrandName = _ProductBrandChangeRow.IsBrandNameNull ? "" : _ProductBrandChangeRow.BrandName;
                        _ProductBrandRow.TradingName = _ProductBrandChangeRow.IsTradingNameNull ? "" : _ProductBrandChangeRow.TradingName;
                        _ProductBrandRow.TaxCode = _ProductBrandChangeRow.IsTaxCodeNull ? "" : _ProductBrandChangeRow.TaxCode;
                        _ProductBrandRow.RegistrationNumber = _ProductBrandChangeRow.IsRegistrationNumberNull ? "" : _ProductBrandChangeRow.RegistrationNumber;
                        if (!_ProductBrandChangeRow.IsIssuedDateNull)
                        {
                            _ProductBrandRow.IssuedDate = _ProductBrandChangeRow.IssuedDate;
                        }
                        _ProductBrandRow.CreateDate = _ProductBrandChangeRow.IsCreateDateNull ? DateTime.Now : _ProductBrandChangeRow.CreateDate;
                        _ProductBrandRow.FunctionGroup_ID = _ProductBrandChangeRow.IsFunctionGroup_IDNull ? 0 : _ProductBrandChangeRow.FunctionGroup_ID;
                        _ProductBrandRow.Branch_ID = _ProductBrandChangeRow.IsBranch_IDNull ? 0 : _ProductBrandChangeRow.Branch_ID;
                        _ProductBrandRow.BusinessType_ID = _ProductBrandChangeRow.IsBusinessType_IDNull ? 0 : _ProductBrandChangeRow.BusinessType_ID;
                        _ProductBrandRow.ProductBrandType_ID_List = _ProductBrandChangeRow.IsProductBrandType_ID_ListNull ? "" : _ProductBrandChangeRow.ProductBrandType_ID_List;

                        _ProductBrandRow.BusinessArea = _ProductBrandChangeRow.IsBusinessAreaNull ? "" : _ProductBrandChangeRow.BusinessArea;
                        _ProductBrandRow.DepartmentMan_ID = _ProductBrandChangeRow.IsDepartmentMan_IDNull ? 0 : _ProductBrandChangeRow.DepartmentMan_ID;
                        _ProductBrandRow.Address = _ProductBrandChangeRow.IsAddressNull ? "" : _ProductBrandChangeRow.Address;
                        _ProductBrandRow.Location_ID = _ProductBrandChangeRow.IsLocation_IDNull ? 0 : _ProductBrandChangeRow.Location_ID;
                        _ProductBrandRow.District_ID = _ProductBrandChangeRow.IsDistrict_IDNull ? 0 : _ProductBrandChangeRow.District_ID;
                        _ProductBrandRow.Ward_ID = _ProductBrandChangeRow.IsWard_IDNull ? 0 : _ProductBrandChangeRow.Ward_ID;
                        _ProductBrandRow.Telephone = _ProductBrandChangeRow.IsTelephoneNull ? "" : _ProductBrandChangeRow.Telephone;
                        _ProductBrandRow.Mobile = _ProductBrandChangeRow.IsMobileNull ? "" : _ProductBrandChangeRow.Mobile;
                        _ProductBrandRow.Email = _ProductBrandChangeRow.IsEmailNull ? "" : _ProductBrandChangeRow.Email;
                        _ProductBrandRow.Website = _ProductBrandChangeRow.IsWebsiteNull ? "" : _ProductBrandChangeRow.Website;
                        _ProductBrandRow.Facebook = _ProductBrandChangeRow.IsFacebookNull ? "" : _ProductBrandChangeRow.Facebook;
                        _ProductBrandRow.Zalo = _ProductBrandChangeRow.IsZaloNull ? "" : _ProductBrandChangeRow.Zalo;
                        _ProductBrandRow.Hotline = _ProductBrandChangeRow.IsHotlineNull ? "" : _ProductBrandChangeRow.Hotline;
                        _ProductBrandRow.Skype = _ProductBrandChangeRow.IsSkypeNull ? "" : _ProductBrandChangeRow.Skype;
                        _ProductBrandRow.Description = _ProductBrandChangeRow.IsDescriptionNull ? "" : _ProductBrandChangeRow.Description;


                        // Thông tin người đại diện pháp nhân
                        _ProductBrandRow.DirectorName = _ProductBrandChangeRow.IsDirectorNameNull ? "" : _ProductBrandChangeRow.DirectorName;
                        //_ProductBrandRow.DirectorBirthday = rdDirectorBirthday.SelectedDate;
                        _ProductBrandRow.DirectorBirthday = _ProductBrandChangeRow.IsDirectorBirthdayNull ? DateTime.Now : _ProductBrandChangeRow.DirectorBirthday;
                        _ProductBrandRow.DirectorAddress = _ProductBrandChangeRow.IsDirectorAddressNull ? "" : _ProductBrandChangeRow.DirectorAddress;
                        _ProductBrandRow.DirectorMobile = _ProductBrandChangeRow.IsDirectorMobileNull ? "" : _ProductBrandChangeRow.DirectorMobile;
                        _ProductBrandRow.DirectorEmail = _ProductBrandChangeRow.IsDirectorEmailNull ? "" : _ProductBrandChangeRow.DirectorEmail;
                        _ProductBrandRow.DirectorPosition = _ProductBrandChangeRow.IsDirectorPositionNull ? "" : _ProductBrandChangeRow.DirectorPosition;

                        // Thông tin tài khoản
                        _ProductBrandRow.AccountUserName = _ProductBrandChangeRow.AccountUserName;
                        _ProductBrandRow.AccountEmail = _ProductBrandChangeRow.AccountEmail;

                        // Thông tin người liên hệ
                        _ProductBrandRow.PersonName = _ProductBrandChangeRow.PersonName;
                        _ProductBrandRow.PersonAddress = _ProductBrandChangeRow.PersonAddress;
                        _ProductBrandRow.PersonMobile = _ProductBrandChangeRow.PersonMobile;
                        _ProductBrandRow.PersonEmail = _ProductBrandChangeRow.PersonEmail;

                        _ProductBrandRow.PRInfo = _ProductBrandChangeRow.PRInfo;
                        _ProductBrandRow.Story = _ProductBrandChangeRow.Story;
                        _ProductBrandRow.Agency = _ProductBrandChangeRow.Agency;

                        _ProductBrandRow.GLN = _ProductBrandChangeRow.GLN;
                        _ProductBrandRow.GCP = _ProductBrandChangeRow.GCP;
                        if (!_ProductBrandChangeRow.IsURLNull)
                            _ProductBrandRow.URL = _ProductBrandChangeRow.URL;
                        if (!_ProductBrandChangeRow.IsImageNull)
                            _ProductBrandRow.Image = _ProductBrandChangeRow.Image;

                        if (!_ProductBrandChangeRow.IsProductionUnitCodeNull)
                            _ProductBrandRow.ProductionUnitCode = _ProductBrandChangeRow.ProductionUnitCode;

                        _ProductBrandRow.LastEditDate = _ProductBrandChangeRow.LastEditDate;
                        _ProductBrandRow.LastEditBy = _ProductBrandChangeRow.LastEditBy;
                        //
                        _ProductBrandChangeRow.ProductBrandChange_Status = 1;
                        _ProductBrandChangeRow.ProductBrandChange_DateApproved = DateTime.Now;
                        _ProductBrandChangeRow.ProductBrandChange_Approved = MyUser.GetUser_ID();
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
                        lblMessage.Text = ("Duyệt yêu cầu thay đổi thông tin doanh nghiệp thành công !");

                    }

                    break;
            }

            lblMessage.Visible = true;
            //  Response.Redirect("/Admin/Notification/RequestProductBrand_List?update=true", false);
            LoadRequest();
            Admin_Template_CMS master = this.Master as Admin_Template_CMS;
            if (master != null)
                master.LoadNotification();

        }
        catch (Exception ex)
        {
            Log.writeLog("grdData_ItemCommand", ex.ToString());
        }
    }
}