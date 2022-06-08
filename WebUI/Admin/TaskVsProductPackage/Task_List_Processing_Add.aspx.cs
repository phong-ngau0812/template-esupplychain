using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class Task_List_Processing_Add : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    int Product_ID = 0;
    int Workshop_ID = 0;
    int Farm_ID = 0;
    string ProductName = string.Empty;
    public string name, code = string.Empty;
    public string avatar = "";
    public string LinkTaskStep = "";
    public string MotaDMCV = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtDes);
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);

        Init();
        if (!IsPostBack)
        {
            FillProductBrand();
            LoadWorkshop();
            LoadProductPackage();
            FillDllStatus();
           // LoadData();
            CheckProductBrand();
            loadDataStepTask();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
        //txtMotaDMCV.config.toolbar = "Basic";
        //txtMotaDMCV.config.height = "8em";
    }
    private void LoadWorkshop()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable(" (Active<>-1 or Active is null) " + where, "  Name ASC");
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            foreach (RadComboBoxItem item in ddlWorkshop.Items)
            {
                if (Workshop_ID.ToString() == item.Value)
                {
                    item.Checked = true;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void FillTask(int Product_ID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " SORT ASC");

            ddlTask.DataSource = dt;
            ddlTask.DataTextField = "Name";
            ddlTask.DataValueField = "TaskStep_ID";
            ddlTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDllStatus", ex.ToString());
        }
    }
    protected void FillDllStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStatusBO().GetAsDataTable(" TaskStatus_ID<>2", "TaskStatus_ID DESC");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "TaskStatus_ID";
            ddlStatus.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDllStatus", ex.ToString());
        }
    }
    private void Init()
    {
        ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
        if (_ProductPackageRow != null)
        {
            //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
            MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
            name = _ProductPackageRow.Name;
            ProductBrand_ID = _ProductPackageRow.ProductBrand_ID;
            Product_ID = _ProductPackageRow.Product_ID;
            if (Product_ID != 0)
            {
                LinkTaskStep = "<a class = \"breadcrumb-item active \"  target=\"_blank\" href = \"TaskStepProductPackage_List?Product_ID=" + Product_ID + "\">xem toàn bộ quy trình >></a>";
            }

            Workshop_ID = _ProductPackageRow.IsWorkshop_IDNull ? 0 : _ProductPackageRow.Workshop_ID;
            if (!_ProductPackageRow.IsFarm_IDNull)
            {
                Farm_ID = _ProductPackageRow.Farm_ID;
            }
            ProductName = _ProductPackageRow.ProductName;
            code = _ProductPackageRow.Code;
            if (!IsPostBack)
            {
                FillTask(_ProductPackageRow.Product_ID);
                if (!_ProductPackageRow.IsStartDateNull)
                {
                    YearStart.Value = _ProductPackageRow.StartDate.Year.ToString();
                    MonthStart.Value = _ProductPackageRow.StartDate.Month.ToString();
                    DayStart.Value = _ProductPackageRow.StartDate.Day.ToString();
                    txtStart.Text = _ProductPackageRow.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (!_ProductPackageRow.IsEndDateNull)
                {
                    YearEnd.Value = _ProductPackageRow.EndDate.Year.ToString();
                    MonthEnd.Value = _ProductPackageRow.EndDate.Month.ToString();
                    DayEnd.Value = _ProductPackageRow.EndDate.Day.ToString();
                }
                SetDateTask();
            }
        }
    }
    protected void SetDateTask()
    {
        if (ddlTask.SelectedValue != "")
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStepVsProductPackageBO().GetAsDataTable(" TaskStep_ID=" + ddlTask.SelectedValue + " and ProductPackage_ID=" + ProductPackage_ID, "");
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["StartTime"] != null)
                {
                    YearStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Year.ToString();
                    MonthStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Month.ToString();
                    DayStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Day.ToString();
                    txtStart.Text = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (dt.Rows[0]["EndTime"] != null)
                {
                    YearEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Year.ToString();
                    MonthEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Month.ToString();
                    DayEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Day.ToString();
                }
            }
        }

    }
    private void FillProductBrand()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ProductBrand_ID, " Sort, Name ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            //  ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", "0"));
            ddlProductBrand.SelectedValue = ProductBrand_ID.ToString();
            ddlProductBrand.Enabled = false;

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void LoadProductPackage()
    {
        try
        {
            string where = string.Empty;

            ddlProductPackage.DataSource = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(" ProductPackage_ID=" + ProductPackage_ID, "ProductPackage_ID DESC");
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.SelectedValue = ProductPackage_ID.ToString();
            ddlProductPackage.Enabled = false;
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void LoadData()
    {
        try
        {
            //DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
            //  left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
            //  left join aspnet_Users U on U.UserId= T.CreateBy
            //  where T.TaskType_ID=4 and T.ProductPackage_ID=" + ProductPackage_ID + " order by StartDate DESC");
            //rptTask.DataSource = dt;
            //rptTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Processing_List?ProductPackage_ID=" + ProductPackage_ID, false);
    }
    protected string GetListStaff_ID()
    {
        string Staff_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlWorkshop.Items)
            {
                if (item.Checked)
                {
                    Staff_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Staff_ID))
            {
                Staff_ID = "," + Staff_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetListStaff_ID", ex.ToString());
        }
        return Staff_ID;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                TaskRow _TaskRow = new TaskRow();
                _TaskRow.ProductBrand_ID = ProductBrand_ID;
                _TaskRow.Product_ID = Product_ID;
                _TaskRow.ProductPackage_ID = ProductPackage_ID;
                _TaskRow.Workshop_ID = Workshop_ID;
                _TaskRow.Farm_ID = Farm_ID;
                _TaskRow.Customer_ID = 0;
                _TaskRow.TaskType_ID = 4;
                _TaskRow.TaskStep_ID = Convert.ToInt32(ddlTask.SelectedValue);
                _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                _TaskRow.Name = ddlTask.SelectedItem.Text;
                _TaskRow.ProductName = ProductName;
                _TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                _TaskRow.CustomerName = "";
                _TaskRow.Description = txtDes.Text;
                if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                {
                    DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    _TaskRow.StartDate = s;
                }
                _TaskRow.LastEditBy = _TaskRow.CreateBy = MyUser.GetUser_ID();
                _TaskRow.LastEditDate = _TaskRow.CreateDate = DateTime.Now;
                _TaskRow.Location = txtLocation.Text;
                _TaskRow.Weather = txtWeather.Text;
                _TaskRow.Staff_ID = GetListStaff_ID();
                BusinessRulesLocator.GetTaskBO().Insert(_TaskRow);
                if (_TaskRow != null)
                {
                    UpdateTask(_TaskRow.Task_ID);
                    //Add thông báo
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Nhật ký được khởi tạo";
                    _NotificationRow.Summary = "Nhật ký " + _TaskRow.Name + " đã được tạo, ấn vào đây để xem chi tiết và nhập nhận xét";
                    _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                    _NotificationRow.NotificationType_ID = 9;
                    _NotificationRow.UserID = MyUser.GetUser_ID();
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                        _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                        _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                        _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                        _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Processing_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    _NotificationRow.ProductPackage_ID = ProductPackage_ID;
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    if (!_NotificationRow.IsNotification_IDNull)
                    {
                        _NotificationRow = BusinessRulesLocator.GetNotificationBO().GetByPrimaryKey(_NotificationRow.Notification_ID);
                        _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Processing_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID + "&Notification_ID=" + _NotificationRow.Notification_ID;
                        BusinessRulesLocator.GetNotificationBO().Update(_NotificationRow);
                    }
                }
                //lblMessage.Text = "Thêm mới thành công!";
                //lblMessage.Visible = true;
                //LoadData();
                Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Processing_List?ProductPackage_ID=" + ProductPackage_ID, false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void UpdateTask(int Task_ID)
    {
        try
        {
            TaskRow _TaskRow = new TaskRow();
            if (Task_ID != 0)
            {
                _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);
                if (_TaskRow != null)
                {
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        fileimage = _TaskRow.Task_ID + "_" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/task/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _TaskRow.Image = fileimage;
                        }
                    }

                    BusinessRulesLocator.GetTaskBO().Update(_TaskRow);
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }

    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetDateTask();
    }

    private void CheckProductBrand()
    {
        try
        {
            //Common.FillProductBrand(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                //ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                //ddlProductBrand.Enabled = false;
                ProductBrand.Visible = false;
                ProductPackage.Visible = false;
            }
            else
            {
                ProductBrand.Visible = true;
                ProductPackage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    private void loadDataStepTask()
    {
        if (ddlTask.SelectedValue != "")
        {
            MotaDMCV = Common.loadDataStepTaskNote(Convert.ToInt32(ddlTask.SelectedValue));
        }

    }
}