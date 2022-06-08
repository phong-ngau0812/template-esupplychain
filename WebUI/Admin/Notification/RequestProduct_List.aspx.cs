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

public partial class RequestProduct_List : System.Web.UI.Page
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

                lblMessage.Text = "Duyệt yêu cầu thay đổi thông tin sản phẩm thành công !";
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

            dtSet = BusinessRulesLocator.Conllection().GetRequestProductList_Paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), 0, Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), Convert.ToInt32(ddlStatus.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);

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
            Literal lblProductChange_Status = e.Item.FindControl("lblProductChange_Status") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblProductChange_Status != null)
            {
                if (lblProductChange_Status.Text == "0")
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


            int Product_ID = Convert.ToInt32(e.CommandArgument);
            ProductChangeRow _ProductChangeRow = new ProductChangeRow();
            _ProductChangeRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(Product_ID);
            Literal lblProduct_ID = e.Item.FindControl("lblProduct_ID") as Literal;
            ProductRow _ProductRow = new ProductRow();

            switch (e.CommandName)
            {

                case "Active":

                    if (lblProduct_ID.Text != "0")
                    {
                        _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Convert.ToInt32(lblProduct_ID.Text));
                        if (_ProductRow != null && _ProductChangeRow != null)
                        {
                            //Cập nhật thông tin sản phẩm ở bạng tạm vào bảng chính
                            _ProductRow.SGTIN = _ProductChangeRow.SGTIN;
                            _ProductRow.Name = _ProductChangeRow.Name;
                            _ProductRow.ProductCategory_ID = _ProductChangeRow.ProductCategory_ID;
                            _ProductRow.Quality_ID = _ProductChangeRow.Quality_ID;
                            _ProductRow.Content = _ProductChangeRow.Content;
                            _ProductRow.Story = _ProductChangeRow.Story;
                            _ProductRow.QualityDescription = _ProductChangeRow.QualityDescription;
                            _ProductRow.GrowthByDay = _ProductChangeRow.GrowthByDay;
                            _ProductRow.ProductType_ID = _ProductChangeRow.ProductType_ID;
                            _ProductRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                            _ProductRow.Acreage = _ProductChangeRow.Acreage;
                            _ProductRow.ExpectedProductivity = _ProductChangeRow.ExpectedProductivity;
                            _ProductRow.ExpectedOutput = _ProductChangeRow.ExpectedOutput;
                            _ProductRow.ExpectedOutputDescription = _ProductChangeRow.ExpectedOutputDescription;
                            _ProductRow.ExpectedProductivityDescription = _ProductChangeRow.ExpectedProductivityDescription;
                            _ProductRow.URL = _ProductChangeRow.URL;
                            _ProductRow.LastEditBy = _ProductChangeRow.LastEditBy;
                            _ProductRow.LastEditDate = _ProductChangeRow.LastEditDate;
                            _ProductRow.WeightDefault = _ProductChangeRow.WeightDefault;
                            _ProductRow.Specification = _ProductChangeRow.Specification;
                            if (!_ProductChangeRow.IsImageNull)
                            {
                                _ProductRow.Image = _ProductChangeRow.Image;
                            }

                            _ProductRow.Active = _ProductChangeRow.Active;
                            _ProductRow.TrackingCode = _ProductChangeRow.TrackingCode;

                            //Duyệt âm thanh và thông điệp
                            if (!_ProductChangeRow.IsAudioPublicNull)
                            {
                                _ProductRow.AudioPublic = _ProductChangeRow.AudioPublic;
                            }
                            if (!_ProductChangeRow.IsAudioSecretNull)
                            {
                                _ProductRow.AudioSecret = _ProductChangeRow.AudioSecret;
                            }
                            if (!_ProductChangeRow.IsAudioSoldNull)
                            {
                                _ProductRow.AudioSold = _ProductChangeRow.AudioSold;
                            }
                            if (!_ProductChangeRow.IsMessagePublicNull)
                            {
                                _ProductRow.MessagePublic = _ProductChangeRow.MessagePublic;
                            }
                            if (!_ProductChangeRow.IsMessageSecretNull)
                            {
                                _ProductRow.MessageSecret = _ProductChangeRow.MessageSecret;
                            }
                            if (!_ProductChangeRow.IsMessageSoldNull)
                            {
                                _ProductRow.MessageSold = _ProductChangeRow.MessageSold;
                            }
                            //
                            _ProductChangeRow.ProductChange_Status = 1;
                            _ProductChangeRow.ProductChange_DateApproved = DateTime.Now;
                            _ProductChangeRow.ProductChange_Approved = MyUser.GetUser_ID();

                            BusinessRulesLocator.GetProductChangeBO().Update(_ProductChangeRow);
                            BusinessRulesLocator.GetProductBO().Update(_ProductRow);
                            //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                            NotificationRow _NotificationRow = new NotificationRow();
                            _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thay đổi thông tin sản phẩm";
                            _NotificationRow.Summary = _ProductChangeRow.ProductChange_Note;
                            _NotificationRow.Body = _ProductChangeRow.ProductChange_ID.ToString();
                            _NotificationRow.NotificationType_ID = 12;
                            _NotificationRow.UserID = _ProductChangeRow.ProductChange_By;
                            if (MyUser.GetFunctionGroup_ID() != "1")
                                _NotificationRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                            //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                            //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                            //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                            //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                            //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                            _NotificationRow.Url = "/Admin/Product/Product_Edit?Product_ID=" + _ProductChangeRow.Product_ID;
                            _NotificationRow.CreateBy = MyUser.GetUser_ID();
                            _NotificationRow.CreateDate = DateTime.Now;
                            _NotificationRow.Active = 1;
                            _NotificationRow.Alias = Guid.NewGuid();
                            BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                            lblMessage.Text = ("Duyệt yêu cầu thay đổi thông tin sản phẩm thành công !");
                        }
                    }
                    else
                    {
                        _ProductRow.SGTIN = _ProductChangeRow.IsSGTINNull ? string.Empty : _ProductChangeRow.SGTIN;
                        _ProductRow.Name = _ProductChangeRow.Name;
                        _ProductRow.ProductCategory_ID = _ProductChangeRow.ProductCategory_ID;
                        _ProductRow.Quality_ID = _ProductChangeRow.Quality_ID;
                        _ProductRow.Content = _ProductChangeRow.Content;
                        _ProductRow.Story = _ProductChangeRow.Story;
                        _ProductRow.QualityDescription = _ProductChangeRow.QualityDescription;
                        _ProductRow.GrowthByDay = _ProductChangeRow.GrowthByDay;
                        _ProductRow.ProductType_ID = _ProductChangeRow.ProductType_ID;
                        _ProductRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                        _ProductRow.Acreage = _ProductChangeRow.Acreage;
                        _ProductRow.ExpectedProductivity = _ProductChangeRow.ExpectedProductivity;
                        _ProductRow.ExpectedOutput = _ProductChangeRow.ExpectedOutput;
                        _ProductRow.ExpectedOutputDescription = _ProductChangeRow.ExpectedOutputDescription;
                        _ProductRow.ExpectedProductivityDescription = _ProductChangeRow.ExpectedProductivityDescription;
                        _ProductRow.URL = _ProductChangeRow.IsURLNull ? string.Empty : _ProductChangeRow.URL;
                        _ProductRow.CreateBy = _ProductChangeRow.CreateBy;
                        _ProductRow.CreateDate = _ProductChangeRow.CreateDate;
                        _ProductRow.WeightDefault = _ProductChangeRow.WeightDefault;
                        _ProductRow.Specification = _ProductChangeRow.Specification;
                        if (!_ProductChangeRow.IsImageNull)
                        {
                            _ProductRow.Image = _ProductChangeRow.Image;
                        }

                        _ProductRow.Active = _ProductChangeRow.Active;
                        _ProductRow.TrackingCode = _ProductChangeRow.TrackingCode;

                        _ProductChangeRow.ProductChange_Status = 1;
                        _ProductChangeRow.ProductChange_DateApproved = DateTime.Now;
                        _ProductChangeRow.ProductChange_Approved = MyUser.GetUser_ID();

                        BusinessRulesLocator.GetProductChangeBO().Update(_ProductChangeRow);
                        BusinessRulesLocator.GetProductBO().Insert(_ProductRow);
                        if (!_ProductRow.IsProduct_IDNull)
                        {
                            //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                            NotificationRow _NotificationRow = new NotificationRow();
                            _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thêm mới sản phẩm";
                            _NotificationRow.Summary = _ProductChangeRow.ProductChange_Note;
                            _NotificationRow.Body = _ProductChangeRow.ProductChange_ID.ToString();
                            _NotificationRow.NotificationType_ID = 12;
                            _NotificationRow.UserID = _ProductChangeRow.ProductChange_By;
                            if (MyUser.GetFunctionGroup_ID() != "1")
                                _NotificationRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                            //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                            //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                            //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                            //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                            //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                            //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                            _NotificationRow.Url = "/Admin/Product/Product_Edit?Product_ID=" + _ProductRow.Product_ID;
                            _NotificationRow.CreateBy = MyUser.GetUser_ID();
                            _NotificationRow.CreateDate = DateTime.Now;
                            _NotificationRow.Active = 1;
                            _NotificationRow.Alias = Guid.NewGuid();
                            BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                            lblMessage.Text = ("Duyệt yêu cầu thêm mới sản phẩm thành công !");

                        }
                    }
                    break;
            }

            lblMessage.Visible = true;
            //   Response.Redirect("/Admin/Notification/RequestProduct_List?update=true", false);
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