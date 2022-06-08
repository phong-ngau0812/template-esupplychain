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

public partial class Task_List_Manufacturing_Edit : System.Web.UI.Page
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
    public string LinkTaskStep = "";
    public string MotaDMCV = "";
    public int No = 1;
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
            LoadWorkshop();
            LoadItem();
            LoadProductPackage();
            FillDllStatus();
            FillInfoTask();
            LoadQuestion();
            LoadAnswer();
            // LoadData();
            CheckProductBrand();
            loadDataStepTask();
            if (Common.CheckUserXuanHoa1())
            {
                xuanhoa.Visible = true;
            }
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
        //txtMotaDMCV.config.toolbar = "Basic";
    }

    protected void LoadQuestion()
    
    {
        if (!string.IsNullOrEmpty(ddlTask.SelectedValue))
        {
            DataTable dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable("TaskStep_ID=" + ddlTask.SelectedValue + " and  Type like '%,4,%'", "");
            if (dt.Rows.Count == 1)
            {
                divQuestion.Visible = true;
                DataTable dtQuestion = BusinessRulesLocator.GetTaskStepQuestionBO().GetAsDataTable("Active =1 and TaskStep_ID=" + ddlTask.SelectedValue, "");
                if (dtQuestion.Rows.Count > 0)
                {
                    rptQuestion.DataSource = dtQuestion;
                    rptQuestion.DataBind();
                }
            }
            else
            {
                divQuestion.Visible = false;
            }
        }
    }

    protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblQuestionID = e.Item.FindControl("lblQuestionID") as Literal;
            CheckBoxList ckAnswer = e.Item.FindControl("ckAnswer") as CheckBoxList;
            if (lblQuestionID != null && ckAnswer != null)
            {
                DataTable dtAnswer = BusinessRulesLocator.GetTaskStepAnswerBO().GetAsDataTable("Active=1 and TaskStepQuestion_ID=" + lblQuestionID.Text, "");
                if (dtAnswer.Rows.Count > 0)
                {
                    ckAnswer.DataSource = dtAnswer;
                    ckAnswer.DataTextField = "Name";
                    ckAnswer.DataValueField = "TaskStepAnswer_ID";
                    ckAnswer.DataBind();
                }
            }
        }
    }
    private void LoadItem()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductItem_ID, QRCodePublicContent from ProductItem where  ProductPackage_ID=" + ProductPackage_ID);
            ddlItem.DataSource = dt;
            if (dt.Rows.Count > 1)
            {
                dt.Rows.RemoveAt(0);
            }
            ddlItem.DataTextField = "QRCodePublicContent";
            ddlItem.DataValueField = "ProductItem_ID";
            ddlItem.DataBind();
            Count.Value = dt.Rows.Count.ToString();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void FillInfoTask()
    {
        try
        {
            if (Task_ID != 0)
            {
                TaskRow _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);
                ddlTask.SelectedValue = _TaskRow.TaskStep_ID.ToString();
                txtStart.Text = _TaskRow.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                txtLocation.Text = _TaskRow.Location;
                txtWeather.Text = _TaskRow.IsWeatherNull ? string.Empty : _TaskRow.Weather;
                //txtLocation.Enabled = false;
                ddlStatus.SelectedValue = _TaskRow.TaskStatus_ID.ToString();
                txtDes.Text = _TaskRow.IsDescriptionNull ? "" : _TaskRow.Description;
                if (!_TaskRow.IsStaff_IDNull)
                {
                    string[] array = _TaskRow.Staff_ID.Split(',');
                    foreach (string value in array)
                    {
                        foreach (RadComboBoxItem item in ddlWorkshop.Items)
                        {
                            if (value == item.Value)
                            {
                                item.Checked = true;
                            }
                        }
                    }
                }

                if (!_TaskRow.IsProductItem_ID_ListNull)
                {
                    string[] array = _TaskRow.ProductItem_ID_List.Split(',');
                    foreach (string value in array)
                    {

                        foreach (RadComboBoxItem item in ddlItem.Items)
                        {
                            if (value == item.Value)
                            {
                                item.Checked = true;
                            }
                        }
                    }
                }

                if (!_TaskRow.IsImageNull)
                {
                    imganh.ImageUrl = "../../data/task/" + _TaskRow.Image;
                }
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
                //Đọc thông báo
                int Notification_ID;
                if (!string.IsNullOrEmpty(Request["Notification_ID"]))
                {
                    int.TryParse(Request["Notification_ID"].ToString(), out Notification_ID);
                    Common.ReadNotification(Notification_ID);
                }

            }

        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoTask", ex.ToString());
        }
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
            dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable("(Active<>-1 or Active is null)" + where, "  Name ASC");
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            //foreach (RadComboBoxItem item in ddlWorkshop.Items)
            //{
            //    if (Workshop_ID.ToString() == item.Value)
            //    {
            //        item.Checked = true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadWorkshop", ex.ToString());
        }
    }

    protected void FillTask(int Product_ID)
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "3" && MyUser.GetAccountType_ID() == "2")
            {
                if (MyUser.GetDepartment_ID() != "")
                {
                    where += " and Department_ID =" + MyUser.GetDepartment_ID();
                }
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " SORT ASC");
            ddlTask.DataSource = dt;
            ddlTask.DataTextField = "Name";
            ddlTask.DataValueField = "TaskStep_ID";
            ddlTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillTask", ex.ToString());
        }
    }
    protected void FillDllStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStatusBO().GetAsDataTable("", "");
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
            if (_ProductPackageRow.ItemCount <= 1)
            {
                ChooseItem.Visible = false;
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
                    txtTranportDateStart.Text = _ProductPackageRow.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (!_ProductPackageRow.IsEndDateNull)
                {
                    YearEnd.Value = _ProductPackageRow.EndDate.Year.ToString();
                    MonthEnd.Value = _ProductPackageRow.EndDate.Month.ToString();
                    DayEnd.Value = _ProductPackageRow.EndDate.Day.ToString();
                    txtEnd.Text = _ProductPackageRow.EndDate.ToString("dd/MM/yyyy HH:mm:ss");
                    txtTranportDateEnd.Text = _ProductPackageRow.EndDate.ToString("dd/MM/yyyy HH:mm:ss");
                }
                SetDateTaskByTask();
                //if (Common.CheckUserXuanHoa1())
                //{
                //    SetDateTaskByTask();
                //}
                //else
                //{
                //    SetDateTask();
                //}


            }
        }
    }

    private void SetDateTaskByTask()
    {
        try
        {
            if (ddlTask.SelectedValue != "")
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetTaskBO().GetAsDataTable(" Task_ID=" + Task_ID + " and ProductPackage_ID=" + ProductPackage_ID, "");
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["StartDate"] != null)
                    {
                        txtStart.Text = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                    }
                    else
                    {
                        txtStart.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["EndDate"].ToString()))
                    {
                        txtEnd.Text = DateTime.Parse(dt.Rows[0]["EndDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        txtEnd.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["TransportDateStart"].ToString()))
                    {
                        txtTranportDateStart.Text = DateTime.Parse(dt.Rows[0]["TransportDateStart"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        txtTranportDateStart.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["TransportDateEnd"].ToString()))
                    {
                        txtTranportDateEnd.Text = DateTime.Parse(dt.Rows[0]["TransportDateEnd"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        txtTranportDateEnd.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("SetDateTaskByTask", ex.ToString());
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
                else
                {
                    txtStart.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (dt.Rows[0]["EndTime"] != null)
                {
                    YearEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Year.ToString();
                    MonthEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Month.ToString();
                    DayEnd.Value = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Day.ToString();
                    txtEnd.Text = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).Day.ToString();
                }
                else
                {
                    txtEnd.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (dt.Rows[0]["TransportDateStart"] != null)
                {
                    txtTranportDateStart.Text = DateTime.Parse(dt.Rows[0]["TransportDateStart"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    txtTranportDateStart.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (dt.Rows[0]["TransportDateEnd"] != null)
                {
                    txtTranportDateEnd.Text = DateTime.Parse(dt.Rows[0]["TransportDateEnd"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    txtTranportDateEnd.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
            //DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName, T.Task_ID,T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
            //  left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
            //  left join aspnet_Users U on U.UserId= T.CreateBy
            //  where T.TaskType_ID=1 and T.ProductPackage_ID=" + ProductPackage_ID + " order by StartDate DESC");
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
        Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Manufacturing_List?ProductPackage_ID=" + ProductPackage_ID, false);
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
    protected string GetProductItem_ID()
    {
        string ProductItem_ID = string.Empty;
        try
        {
            int index = 0;
            foreach (RadComboBoxItem item in ddlItem.Items)
            {
                if (item.Checked)
                {
                    index++;
                    ProductItem_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ProductItem_ID))
            {
                ProductItem_ID = "," + ProductItem_ID;
            }
            CountCheck.Value = index.ToString();
        }
        catch (Exception ex)
        {
            Log.writeLog("GetProductItem_ID", ex.ToString());
        }
        return ProductItem_ID;
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
                    _TaskRow.TaskStep_ID = Convert.ToInt32(ddlTask.SelectedValue);
                    _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                    _TaskRow.Name = ddlTask.SelectedItem.Text;
                    //_TaskRow.ProductName =ProductName;
                    //_TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                    //_TaskRow.CustomerName ="";
                    _TaskRow.Description = txtDes.Text;
                    if (!string.IsNullOrEmpty(GetProductItem_ID()))
                    {
                        if (CountCheck.Value != Count.Value)
                        {
                            _TaskRow.ProductItem_ID_List = GetProductItem_ID();
                        }
                        else
                        {
                            _TaskRow.ProductItem_ID_List = null;
                        }
                    }
                    else
                    {
                        _TaskRow.ProductItem_ID_List = null;
                    }
                    if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.StartDate = s;
                    }
                    if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtEnd.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.EndDate = s;
                    }
                    if (!string.IsNullOrEmpty(txtTranportDateStart.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtTranportDateStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.TransportDateStart = s;
                    }
                    if (!string.IsNullOrEmpty(txtTranportDateEnd.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtTranportDateEnd.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.TransportDateEnd = s;
                    }
                    _TaskRow.LastEditBy = MyUser.GetUser_ID();
                    _TaskRow.LastEditDate = DateTime.Now;
                    _TaskRow.Location = txtLocation.Text;
                    _TaskRow.Staff_ID = GetListStaff_ID();
                    _TaskRow.TaskStepAnswer_ID_List = GetAnswer();
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
                }
                BusinessRulesLocator.GetTaskBO().Update(_TaskRow);
                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.Visible = true;
                FillInfoTask();
                LoadData();
                Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Manufacturing_List?ProductPackage_ID=" + ProductPackage_ID, false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetDateTaskByTask();
        //SetDateTask();
        loadDataStepTask();
        LoadQuestion();
        LoadAnswer();
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
                ProducBrand.Visible = false;
                ProductPackage.Visible = false;
            }
            else
            {
                ProducBrand.Visible = true;
                ProductPackage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void LoadAnswer()
    {
        foreach (RepeaterItem item in rptQuestion.Items)
        {
            CheckBoxList ckAnswer = item.FindControl("ckAnswer") as CheckBoxList;
            foreach (ListItem itemAnswer in ckAnswer.Items)
            {
                DataTable dt = BusinessRulesLocator.GetTaskBO().GetAsDataTable("Task_ID=" + Task_ID + " and TaskStepAnswer_ID_List like '%," + itemAnswer.Value + ",%'", "");
                if (dt.Rows.Count == 1)
                {
                    itemAnswer.Selected = true;
                }
            }
        }
    }
    protected string GetAnswer()
    {
        string value = string.Empty;
        try
        {
            foreach (RepeaterItem item in rptQuestion.Items)
            {
                CheckBoxList ckAnswer = item.FindControl("ckAnswer") as CheckBoxList;
                foreach (ListItem itemAnswer in ckAnswer.Items)
                {
                    if (itemAnswer.Selected)
                    {
                        value += "," + itemAnswer.Value;
                    }
                }

            }
            if (!string.IsNullOrEmpty(value))
            {
                value += ",";
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
        return value;

    }
    private void loadDataStepTask()
    {
        if (ddlTask.SelectedValue != "")
        {
            MotaDMCV = Common.loadDataStepTaskNote(Convert.ToInt32(ddlTask.SelectedValue));
        }
    }
}