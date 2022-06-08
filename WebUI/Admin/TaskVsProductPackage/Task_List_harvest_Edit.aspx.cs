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

public partial class Task_List_harvest_Edit : System.Web.UI.Page
{
    int Task_ID = 0;
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    int Product_ID = 0;
    int Workshop_ID = 0;
    int Farm_ID = 0;
    string ProductName = string.Empty;
    public string name, code = string.Empty;
    public string avatar = "";
    public string titleXuanHoa = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtDes);
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        if (!string.IsNullOrEmpty(Request["Task_ID"]))
            int.TryParse(Request["Task_ID"].ToString(), out Task_ID);

        Init();
        if (!IsPostBack)
        {
            FillProductBrand();

            LoadProductPackage();
            LoadWarehouse();
            FillDllStatus();
            FillWorkshopImport();
            FillThukho();
            //  LoadData();
            FillInfoTask();
            CheckProductBrand();
        }
        if (Common.CheckUserXuanHoa1())
        {
            titleXuanHoa = " nhập kho ";
            xuanhoa.Visible = true;
        }
        else
        {
            titleXuanHoa = " thu hoạch ";
            xuanhoa.Visible = false;
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
        txtDes.config.height = "8em";
    }
    protected void FillWorkshopImport()
    {

        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and WS.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        if (ddlProductBrand.SelectedValue.ToString() == "1524")
        {
            where += "and WS.Department_ID=249";
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select WS.* from Workshop WS inner join ProductBrand PB on WS.ProductBrand_ID = PB.ProductBrand_ID where PB.Active=1 " + where);
            ddlWorkshopImport.DataSource = dt;
            ddlWorkshopImport.DataTextField = "Name";
            ddlWorkshopImport.DataValueField = "Workshop_ID";
            ddlWorkshopImport.DataBind();
            ddlWorkshopImport.Items.Insert(0, new ListItem("-- Chọn người nhập kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void FillThukho()
    {

        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and WS.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        if (ddlProductBrand.SelectedValue.ToString() == "1524")
        {
            where += "and WS.Department_ID=249";
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select WS.* from Workshop WS inner join ProductBrand PB on WS.ProductBrand_ID = PB.ProductBrand_ID where PB.Active=1 " + where);
            ddlThukho.DataSource = dt;
            ddlThukho.DataTextField = "Name";
            ddlThukho.DataValueField = "Workshop_ID";
            ddlThukho.DataBind();
            ddlThukho.Items.Insert(0, new ListItem("-- Chọn thủ kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void FillDllStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStatusBO().GetAsDataTable(" TaskStatus_ID<>2", "");
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

    private void FillInfoTask()
    {
        try
        {
            if (Task_ID != 0)
            {
                TaskRow _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);
                txtName.Text = _TaskRow.Name;
                //txtName.ReadOnly = true;
                txtHarvestVolume.Text = _TaskRow.HarvestVolume.ToString("N0");
                txtRisk.Text = _TaskRow.Risk;
                txtUnit.Text = _TaskRow.IsUnitNull ? string.Empty : _TaskRow.Unit;
                txtStart.Text = _TaskRow.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                txtHarvestDayRemain.Text = _TaskRow.HarvestDayRemain.ToString();

                txtLocation.Text = _TaskRow.Location;
                //txtLocation.Enabled = false;
                txtWeather.Text = _TaskRow.IsWeatherNull ? string.Empty : _TaskRow.Weather;
                ddlStatus.SelectedValue = _TaskRow.TaskStatus_ID.ToString();
                ddlWarehouse.SelectedValue = _TaskRow.Warehouse_ID.ToString();
                txtDes.Text = _TaskRow.IsDescriptionNull ? "" : _TaskRow.Description;
                txtComment.Text = _TaskRow.IsCommentNull ? "" : _TaskRow.Comment;
                if (MyUser.GetFunctionGroup_ID() != "3" && MyUser.GetFunctionGroup_ID() != "8")
                {
                    if (MyUser.GetAccountType_ID() != "7")
                        txtComment.Enabled = false;
                }
                else
                {

                    txtComment.Enabled = true;
                }
                if (!_TaskRow.IsImageNull)
                {
                    imganh.ImageUrl = "../../data/task/" + _TaskRow.Image;
                }
                if (MyUser.GetFunctionGroup_ID() == "3")
                {
                    int Notification_ID = 0;
                    if (!string.IsNullOrEmpty(Request["Notification_ID"]))
                        int.TryParse(Request["Notification_ID"].ToString(), out Notification_ID);
                    ReadNotificationRow _ReadNotificationRow = new ReadNotificationRow();
                    _ReadNotificationRow.Notification_ID = Notification_ID;
                    _ReadNotificationRow.UserID = MyUser.GetUser_ID();
                    _ReadNotificationRow.ViewDate = DateTime.Now;
                    BusinessRulesLocator.GetReadNotificationBO().Insert(_ReadNotificationRow);
                }
                else
                {
                    if (MyUser.GetAccountType_ID() == "7")
                    {
                        int Notification_ID = 0;
                        if (!string.IsNullOrEmpty(Request["Notification_ID"]))
                            int.TryParse(Request["Notification_ID"].ToString(), out Notification_ID);
                        ReadNotificationRow _ReadNotificationRow = new ReadNotificationRow();
                        _ReadNotificationRow.Notification_ID = Notification_ID;
                        _ReadNotificationRow.UserID = MyUser.GetUser_ID();
                        _ReadNotificationRow.ViewDate = DateTime.Now;
                        BusinessRulesLocator.GetReadNotificationBO().Insert(_ReadNotificationRow);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoTask", ex.ToString());
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
            Workshop_ID = _ProductPackageRow.IsWorkshop_IDNull ? 0 : _ProductPackageRow.Workshop_ID;
            if (!_ProductPackageRow.IsFarm_IDNull)
            {
                Farm_ID = _ProductPackageRow.Farm_ID;
            }
            ProductName = _ProductPackageRow.ProductName;
            code = _ProductPackageRow.Code;
            if (!IsPostBack)
            {
                //FillTask(_ProductPackageRow.Product_ID);
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
        if (ddlProductPackage.SelectedValue != "")
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(" ProductPackage_ID=" + ProductPackage_ID, "");
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["StartDate"] != null)
                {
                    YearStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Year.ToString();
                    MonthStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Month.ToString();
                    DayStart.Value = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).Day.ToString();
                    txtStart.Text = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).ToString("dd/MM/yyyy");
                }
                if (dt.Rows[0]["EndDate"] != null)
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
    protected void LoadWarehouse()
    {
        try
        {
            string where = string.Empty;

            ddlWarehouse.DataSource = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable(" Active=1 and Type=2 and ProductBrand_ID=" + ProductBrand_ID, "");
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

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
            //            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Name,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
            //from Task T 
            //left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
            //where T.TaskType_ID=3 and T.ProductPackage_ID =" + ProductPackage_ID + " order by StartDate DESC");
            //            rptTask.DataSource = dt;
            //            rptTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/TaskVsProductPackage/Task_List_harvest_List?ProductPackage_ID=" + ProductPackage_ID, false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {
                TaskRow _TaskRow = new TaskRow();
                _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);

                if (MyUser.GetFunctionGroup_ID() == "3" || MyUser.GetFunctionGroup_ID() == "8")
                {
                    _TaskRow.Comment = string.IsNullOrEmpty(txtComment.Text) ? string.Empty : txtComment.Text;
                    if (!string.IsNullOrEmpty(_TaskRow.Comment))
                    {
                        NotificationRow _NotificationRow = new NotificationRow();
                        _NotificationRow.Name = "Giám sát nhận xét nhật ký";
                        _NotificationRow.Summary = "Nhật ký " + _TaskRow.Name + " đã được giám sát nhận xét, ấn vào đây để xem chi tiết";
                        _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                        _NotificationRow.NotificationType_ID = 10;
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
                        _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Harvest_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID;
                        _NotificationRow.CreateBy = MyUser.GetUser_ID();
                        _NotificationRow.CreateDate = DateTime.Now;
                        _NotificationRow.Active = 1;
                        _NotificationRow.Alias = Guid.NewGuid();
                        _NotificationRow.ProductPackage_ID = ProductPackage_ID;
                        //  BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    }
                }
                else
                {
                    //_TaskRow.ProductBrand_ID = ProductBrand_ID;
                    //_TaskRow.Product_ID = Product_ID;
                    //_TaskRow.ProductPackage_ID = ProductPackage_ID;
                    //_TaskRow.Workshop_ID = Workshop_ID;
                    //_TaskRow.Farm_ID = Farm_ID;
                    //_TaskRow.Customer_ID = 0;
                    //_TaskRow.TaskType_ID = 1;
                    //_TaskRow.TaskStep_ID = Convert.ToInt32(ddlTask.SelectedValue);
                    _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                    //_TaskRow.Name = ddlTask.SelectedItem.Text;
                    //_TaskRow.ProductName =ProductName;
                    //_TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                    //_TaskRow.CustomerName ="";

                    _TaskRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.StartDate = s;
                    }

                    if (!string.IsNullOrEmpty(txtHarvestVolume.Text))
                    {
                        string HarvestVolume = txtHarvestVolume.Text.Replace(",", "");
                        _TaskRow.HarvestVolume = Convert.ToDecimal(HarvestVolume);
                    }
                    else
                    {
                        _TaskRow.HarvestVolume = 0;
                    }

                    _TaskRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                    _TaskRow.Risk = string.IsNullOrEmpty(txtRisk.Text) ? string.Empty : txtRisk.Text;
                    _TaskRow.Unit = string.IsNullOrEmpty(txtUnit.Text) ? string.Empty : txtUnit.Text;
                    _TaskRow.HarvestDayRemain = string.IsNullOrEmpty(txtHarvestDayRemain.Text) ? 0 : Convert.ToInt32(txtHarvestDayRemain.Text);

                    _TaskRow.Description = txtDes.Text;
                    if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.StartDate = s;
                    }
                    _TaskRow.LastEditBy = MyUser.GetUser_ID();
                    _TaskRow.LastEditDate = DateTime.Now;
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {

                        fileimage = Task_ID + "_" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/task/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _TaskRow.Image = fileimage;
                        }
                    }
                    _TaskRow.Location = txtLocation.Text;
                    //_TaskRow.Staff_ID = GetListStaff_ID();
                }
                BusinessRulesLocator.GetTaskBO().Update(_TaskRow);
                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.Visible = true;
                LoadData();
                FillInfoTask();
                Response.Redirect("/Admin/TaskVsProductPackage/Task_List_harvest_List?ProductPackage_ID=" + ProductPackage_ID, false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
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
}