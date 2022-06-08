using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Notification_RequestMessage_List : System.Web.UI.Page
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

                lblMessage.Text = "Duyệt yêu cầu thêm mới thông điệp thành công !";
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

            dtSet = BusinessRulesLocator.Conllection().GetRequestMessage_Paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), 0, Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), Convert.ToInt32(ddlStatus.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);

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


            int MessageRequest_ID = Convert.ToInt32(e.CommandArgument);
            MessageRequestRow _MessageRequestRow = new MessageRequestRow();
            _MessageRequestRow = BusinessRulesLocator.GetMessageRequestBO().GetByPrimaryKey(MessageRequest_ID);
            Literal lblProduct_ID = e.Item.FindControl("lblProduct_ID") as Literal;
            MessageRow _MessageRow = new MessageRow();

            switch (e.CommandName)
            {

                case "Active":
                    _MessageRow.Title = _MessageRequestRow.Title;
                    _MessageRow.Note = _MessageRequestRow.Note;
                    _MessageRow.ProductBrand_ID = _MessageRequestRow.ProductBrand_ID;
                    _MessageRow.Status = _MessageRequestRow.Status;
                    _MessageRow.CreateBy = _MessageRequestRow.CreateBy;
                    _MessageRow.CreateDate = _MessageRequestRow.CreateDate;
                    _MessageRequestRow.MessageRequest_Status = 1;
                    _MessageRequestRow.MessageRequest_Approved = MyUser.GetUser_ID();
                    _MessageRequestRow.MessageRequest_DateApproved = DateTime.Now;
                    BusinessRulesLocator.GetMessageBO().Insert(_MessageRow);
                    BusinessRulesLocator.GetMessageRequestBO().Update(_MessageRequestRow);
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thêm mới thông điệp";
                    _NotificationRow.Summary = "";
                    _NotificationRow.Body = "";
                    _NotificationRow.NotificationType_ID = 18;
                    _NotificationRow.UserID = _MessageRequestRow.MessageRequest_Approved;
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = _MessageRow.ProductBrand_ID;
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Message/Message_Edit?Message_ID=" + _MessageRow.Message_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    lblMessage.Text = ("Duyệt yêu cầu thêm mới thông điệp thành công !");
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