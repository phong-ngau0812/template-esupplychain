using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackageOrder_List : System.Web.UI.Page
{
    public string Message = "";
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackageOrder_ID"]))
            int.TryParse(Request["ProductPackageOrder_ID"].ToString(), out ProductPackageOrder_ID);
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLProductBrand();
            FillDDLStatus();
            FillDDLCategory();
            GetProductPackageOrder();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void FillDDLProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
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

    private void FillDDLStatus()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageOrderStatusBO().GetAsDataTable("", " ");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "ProductPackageOrderStatus_ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- Chọn trạng thái lệnh --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetAsDataTable(" ACTIVE=1", " SORT ASC ");
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ProductPackageOrderCategory_ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Chọn danh mục lệnh --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected string CheckAmountMaterial(string ProductPackageOrder_ID)
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@" Select ISNULL(Sum(Amount),0) as Total from ProductPackageOrderMaterial where ProductPackageOrder_ID = " + ProductPackageOrder_ID);
        string Content = string.Empty;
        if (decimal.Parse(dt.Rows[0]["Total"].ToString()) > 0)
        {
            Content = "<i class='fas fa-check text-success font-16'>";
        }
        else
        {
            Content = "<i class='fas fa-stop text-danger font-16'></i>";
        }
        return Content;
    }
    protected void GetProductPackageOrder()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetProductPackageOrder(Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), ProductPackageOrder_ID, Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlApproved.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, "", "CreateDate DESC"); ;
            rptProductPackageOrder.DataSource = dt;
            rptProductPackageOrder.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("GetProductPackageOrder", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackageOrder_Add.aspx", false);
    }


    protected void rptProductPackageOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Area_ID = Convert.ToInt32(e.CommandArgument);
        ProductPackageOrderRow _ProductPackageOrderRow = new ProductPackageOrderRow();
        _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(Area_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_ProductPackageOrderRow != null)
                {
                    _ProductPackageOrderRow.Active = -1;
                }
                BusinessRulesLocator.GetProductPackageOrderBO().Update(_ProductPackageOrderRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Approved":
                //DataTable dt = BusinessRulesLocator.GetProductPackageOrderMaterialBO().GetAsDataTable("ProductPackageOrder_ID=" + Area_ID, "");
                //if (dt.Rows.Count > 0)
                //{
                if (_ProductPackageOrderRow != null)
                {
                    _ProductPackageOrderRow.AdminApproveBy = MyUser.GetUser_ID();
                    _ProductPackageOrderRow.AdminApproveDate = DateTime.Now;
                    _ProductPackageOrderRow.Approve = 1;
                }
                BusinessRulesLocator.GetProductPackageOrderBO().Update(_ProductPackageOrderRow);
                lblMessage.Text = ("Duyệt lệnh thành công !");
                Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                if (master != null)
                    master.LoadNotification();
                //Gửi thông báo xuống cho bộ phận kho
                NotificationRow _NotificationRow = new NotificationRow();
                Guid g = Guid.NewGuid();
                _NotificationRow.Name = "Yêu cầu xuất kho";
                _NotificationRow.Summary = "Lệnh sản xuất " + _ProductPackageOrderRow.Name + " đã được duyệt, ấn vào đây để xem thông tin và tạo phiếu xuất kho";
                _NotificationRow.Body = _ProductPackageOrderRow.ProductPackageOrder_ID.ToString();
                _NotificationRow.NotificationType_ID = 6;
                _NotificationRow.UserID = MyUser.GetUser_ID();
                _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRow.Url = "/Admin/Notification/Notification_Detail?Alias=" + g;
                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                _NotificationRow.CreateDate = DateTime.Now;
                _NotificationRow.Active = 1;
                _NotificationRow.Alias = g;
                _NotificationRow.ProductPackageOrder_ID = _ProductPackageOrderRow.ProductPackageOrder_ID;
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);

                ///Gửi thông báo cho người tạo lệnh
                NotificationRow _NotificationRowAccept = new NotificationRow();
                Guid g1 = Guid.NewGuid();
                _NotificationRowAccept.Name = "Lệnh đã được duyệt";
                _NotificationRowAccept.Summary = "Lệnh sản xuất " + _ProductPackageOrderRow.Name + " đã được duyệt thành công.";
                _NotificationRowAccept.Body = _ProductPackageOrderRow.ProductPackageOrder_ID.ToString();
                _NotificationRowAccept.NotificationType_ID = 7;
                _NotificationRowAccept.UserID = _ProductPackageOrderRow.CreateBy;
                _NotificationRowAccept.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                _NotificationRowAccept.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    _NotificationRowAccept.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    _NotificationRowAccept.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    _NotificationRowAccept.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRowAccept.Url = "/Admin/Notification/Notification_Detail?Alias=" + g1;
                _NotificationRowAccept.CreateBy = MyUser.GetUser_ID();
                _NotificationRowAccept.CreateDate = DateTime.Now;
                _NotificationRowAccept.Active = 1;
                _NotificationRowAccept.Alias = g1;
                _NotificationRowAccept.ProductPackageOrder_ID = _ProductPackageOrderRow.ProductPackageOrder_ID;
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRowAccept);
                //}
                //else
                //{
                //    lblMessage.Text = ("Không có vật tư trong lệnh. Duyệt Lệnh không thành công !");
                //    lblMessage.ForeColor = Color.Red;
                //    lblMessage.BackColor = Color.Wheat;

                //}
                break;
                //case "Deactive":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 0;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;

        }
        lblMessage.Visible = true; ;
        GetProductPackageOrder();
    }



    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetProductPackageOrder();
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            GetProductPackageOrder();
        }
    }
}