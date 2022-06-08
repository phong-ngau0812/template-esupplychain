using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Notification_Default : System.Web.UI.Page
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

                lblMessage.Text = "Duyệt yêu cầu cấp tem thành công !";
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

            dtSet = BusinessRulesLocator.Conllection().GetRequestQRCodePackageList_Paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), Convert.ToInt32(ddlStatus.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);

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
            Common.FillProductBrand(ddlProductBrand, " ");
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
            Literal lblQRCodePackageChange_Status = e.Item.FindControl("lblQRCodePackageChange_Status") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblQRCodePackageChange_Status != null)
            {
                if (lblQRCodePackageChange_Status.Text == "0")
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


            int QRCodePackageChange_ID = Convert.ToInt32(e.CommandArgument);
            QRCodePackageChangeRow _QRCodePackageChangeRow = new QRCodePackageChangeRow();
            _QRCodePackageChangeRow = BusinessRulesLocator.GetQRCodePackageChangeBO().GetByPrimaryKey(QRCodePackageChange_ID);


            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();

            switch (e.CommandName)
            {

                case "Active":

                    //Thêm mới Cấp tem ở bạng tạm vào bảng chính
                    _QRCodePackageRow.QRCodePackageType_ID = _QRCodePackageChangeRow.QRCodePackageType_ID;
                    _QRCodePackageRow.ProductBrand_ID = _QRCodePackageChangeRow.ProductBrand_ID;
                    if (!_QRCodePackageChangeRow.IsProductPackage_IDNull)
                    {
                        _QRCodePackageRow.ProductPackage_ID = _QRCodePackageChangeRow.ProductPackage_ID;
                    }
                    _QRCodePackageRow.Product_ID = _QRCodePackageChangeRow.Product_ID;
                    _QRCodePackageRow.ProductName = _QRCodePackageChangeRow.ProductName;
                    _QRCodePackageRow.Name = _QRCodePackageChangeRow.Name;

                    _QRCodePackageRow.Store_ID = _QRCodePackageChangeRow.Store_ID;
                    _QRCodePackageRow.Factory_ID = _QRCodePackageChangeRow.Factory_ID;
                    _QRCodePackageRow.Description = _QRCodePackageChangeRow.Description;
                    _QRCodePackageRow.SoundEnable = _QRCodePackageChangeRow.SoundEnable;
                    _QRCodePackageRow.SMS = _QRCodePackageChangeRow.SMS;
                    _QRCodePackageRow.Active = _QRCodePackageChangeRow.Active;
                    _QRCodePackageRow.QRCodeStatus_ID = _QRCodePackageChangeRow.QRCodeStatus_ID;
                    _QRCodePackageRow.ViewCount = _QRCodePackageChangeRow.ViewCount;
                    _QRCodePackageRow.SellCount = _QRCodePackageChangeRow.SellCount;
                    _QRCodePackageRow.QRCodeNumber = _QRCodePackageChangeRow.QRCodeNumber;
                    _QRCodePackageRow.QRCodeUsed = _QRCodePackageChangeRow.QRCodeUsed;
                    _QRCodePackageRow.QRCodeAvailable = _QRCodePackageChangeRow.QRCodeAvailable;
                    _QRCodePackageRow.CreateBy = _QRCodePackageChangeRow.CreateBy;
                    _QRCodePackageRow.CreateDate = _QRCodePackageChangeRow.CreateDate;
                    _QRCodePackageRow.ProfileQRCode = _QRCodePackageChangeRow.ProfileQRCode;
                    if (!_QRCodePackageChangeRow.IsAmountSumNull)
                    {
                        _QRCodePackageRow.AmountSum = _QRCodePackageRow.AmountSum;
                    }
                    if (!_QRCodePackageChangeRow.IsSupplier_IDNull)
                    {
                        _QRCodePackageRow.Supplier_ID = _QRCodePackageChangeRow.Supplier_ID;
                    }

                    if (!_QRCodePackageChangeRow.IsManufactureDateNull)
                    {
                        _QRCodePackageRow.ManufactureDate = _QRCodePackageChangeRow.ManufactureDate;
                    }
                    if (!_QRCodePackageChangeRow.IsHarvestDateNull)
                    {
                        _QRCodePackageRow.HarvestDate = _QRCodePackageChangeRow.HarvestDate;
                    }
                    if (!_QRCodePackageChangeRow.IsWarrantyEndDateNull)
                    {
                        _QRCodePackageRow.WarrantyEndDate = _QRCodePackageChangeRow.WarrantyEndDate;
                    }
                    if (!_QRCodePackageChangeRow.IsStampDateNull)
                    {
                        _QRCodePackageRow.StampDate = _QRCodePackageChangeRow.StampDate;
                    }

                    _QRCodePackageRow.FarmGroupInter_ID = _QRCodePackageChangeRow.FarmGroupInter_ID;
                    _QRCodePackageRow.FarmGroup_ID = _QRCodePackageChangeRow.FarmGroup_ID;

                    _QRCodePackageChangeRow.QRCodePackageChange_By = MyUser.GetUser_ID();
                    _QRCodePackageChangeRow.QRCodePackageChange_Date = DateTime.Now;
                    _QRCodePackageChangeRow.QRCodePackageChange_Status = 1;
                    _QRCodePackageChangeRow.QRCodePackageChange_Approved = MyUser.GetUser_ID();
                    BusinessRulesLocator.GetQRCodePackageChangeBO().Update(_QRCodePackageChangeRow);
                    BusinessRulesLocator.GetQRCodePackageBO().Insert(_QRCodePackageRow);
                    if (!_QRCodePackageRow.IsQRCodePackage_IDNull)
                    {
                        //Sinh mã 
                        //Sản phẩm không xác định
                        if (!_QRCodePackageRow.IsProductPackage_IDNull)
                        {
                            if (_QRCodePackageRow.ProductPackage_ID.ToString() == "-1")
                            {
                                //Tem công khai
                                if (_QRCodePackageRow.QRCodePackageType_ID.ToString() == "1")
                                {
                                    BusinessRulesLocator.Conllection().QRCodePublicAnonymousCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, -1, Convert.ToInt32(ddlProductBrand.SelectedValue), _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                                }
                                else
                                {
                                    BusinessRulesLocator.Conllection().QRCodeSecretAnonymousCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, -1, Convert.ToInt32(ddlProductBrand.SelectedValue), _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, _QRCodePackageRow.SMS, MyUser.GetUser_ID());
                                }
                            }
                            else
                            {
                                //Tem công khai
                                if (_QRCodePackageRow.QRCodePackageType_ID.ToString() == "1")
                                {
                                    BusinessRulesLocator.Conllection().QRCodePublicCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                                }
                                else
                                {
                                    BusinessRulesLocator.Conllection().QRCodeSecretCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, _QRCodePackageRow.SMS, MyUser.GetUser_ID());
                                    //  BusinessRulesLocator.Conllection().QRCodePublicCreate_V2(1, Convert.ToInt32(txtLength.Text), _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                                    //BusinessRulesLocator.Conllection().QRCodePublicPrimaryCreate(_QRCodePackageRow.Product_ID, _QRCodePackageRow.SoundEnable,_QRCodePackageRow.CreateBy, "");
                                }
                            }
                        }
                        else
                        {
                            if (_QRCodePackageRow.QRCodePackageType_ID.ToString() == "1")
                            {
                                BusinessRulesLocator.Conllection().QRCodePublicCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, MyUser.GetUser_ID());
                            }
                            else
                            {
                                BusinessRulesLocator.Conllection().QRCodeSecretCreate_V2(_QRCodePackageRow.QRCodeNumber, _QRCodePackageChangeRow.Length, _QRCodePackageRow.Product_ID, _QRCodePackageRow.QRCodePackage_ID, _QRCodePackageRow.SoundEnable, _QRCodePackageRow.SMS, MyUser.GetUser_ID());
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
                    _NotificationRow.Summary = _QRCodePackageChangeRow.QRCodePackageChange_Note;
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
                    _NotificationRow.Url = "/Admin/QRCodePackage/QRCodePackage_Edit?QRCodePackage_ID=" + _QRCodePackageRow.QRCodePackage_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    lblMessage.Text = ("Duyệt yêu cầu cấp tem thành công !");
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