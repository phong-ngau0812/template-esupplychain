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

public partial class Task_List_Material_Add : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    int Product_ID = 0;
    int Workshop_ID = 0;
    int Farm_ID = 0;
    string ProductName = string.Empty;
    public string name, code = string.Empty;
    public string avatar = "";
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
            FillMaterial();
            LoadProductPackage();
            FillDllStatus();
           // LoadData();
            CheckProductBrand();
        }

        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
        txtDes.config.height = "8em";
    }


    protected void FillDllStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskStatusBO().GetAsDataTable(" TaskStatus_ID<>2", " TaskStatus_ID DESC");
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
            Workshop_ID = _ProductPackageRow.IsWorkshop_IDNull ? 0 : _ProductPackageRow.Workshop_ID;
            if (!_ProductPackageRow.IsFarm_IDNull)
            {
                Farm_ID = _ProductPackageRow.Farm_ID;
            }
            ProductName = _ProductPackageRow.ProductName;
            code = _ProductPackageRow.Code;
            if (!IsPostBack)
            {
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
    private void FillMaterial()
    {
        try
        {
            DataTable dtDinhMuc = new DataTable();
            dtDinhMuc = BusinessRulesLocator.GetProductPackageVsMaterialBO().GetAsDataTable("ProductPackage_ID=" + ProductPackage_ID, "");
            DataTable dt = new DataTable();
            if (dtDinhMuc.Rows.Count > 0)
            {
                dt = BusinessRulesLocator.GetMaterialBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ProductBrand_ID + " and Material_ID in (select Material_ID from ProductPackageVsMaterial where ProductPackage_ID=" + ProductPackage_ID + " )", "  Name ASC");
                ddlMaterial.DataSource = dt;
                ddlMaterial.DataTextField = "Name";
                ddlMaterial.DataValueField = "Material_ID";
                ddlMaterial.DataBind();

            }
            ddlMaterial.Items.Insert(0, new ListItem("-- Chọn vật tư --", ""));
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
//            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.Name
//from Task T 
//left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
//where T.TaskType_ID=2 and T.ProductPackage_ID = " + ProductPackage_ID + " order by StartDate DESC");
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
        Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Material_List?ProductPackage_ID=" + ProductPackage_ID, false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Decimal Quantity = 0;
        Decimal Pricetity = 0;
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
                _TaskRow.TaskType_ID = 2;
                _TaskRow.TaskStep_ID = 0;
                _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                _TaskRow.ProductName = ProductName;
                _TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                _TaskRow.CustomerName = "";
                _TaskRow.Description = txtDes.Text;

                if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                {
                    DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    _TaskRow.StartDate = s;
                }

                _TaskRow.Name = ddlMaterial.SelectedItem.Text;
                if (ddlMaterial.SelectedValue != "")
                {
                    _TaskRow.Material_ID = Convert.ToInt32(ddlMaterial.SelectedValue);
                }
                else
                {
                    _TaskRow.Material_ID = 0;
                }


                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    string quantity = txtQuantity.Text.Replace(",", ".");
                    Quantity = Convert.ToDecimal(quantity);
                }
                else
                {
                    Quantity = 0;
                }

                //if (txtQuantity.SelectedValue != "")
                //{
                //    _TaskRow.Quantity = Convert.ToDecimal(txtQuantity.SelectedValue);
                //}
                //else
                //{
                //    _TaskRow.Quantity = 0;
                //}
                if (!string.IsNullOrEmpty(txtPrice.Text))
                {
                    string price = txtPrice.Text.Replace(",", "");
                    Pricetity = Convert.ToDecimal(price);
                }
                else
                {
                    Pricetity = 0;
                }
                _TaskRow.Price = Pricetity;
                _TaskRow.Quantity = Quantity;
                if (Quantity != 0 && Quantity != 0)
                {
                    Decimal SumMoney = Quantity * Pricetity;
                    _TaskRow.SumMoney = SumMoney;
                }
                else
                {
                    _TaskRow.SumMoney = 0;
                }
                _TaskRow.UserName = txtUserName.Text;
                _TaskRow.BuyerName = txtBuyerName.Text;
                _TaskRow.ShopName = txtShopName.Text;
                _TaskRow.ShopAddress = txtShopAddress.Text;

                _TaskRow.LastEditBy = _TaskRow.CreateBy = MyUser.GetUser_ID();
                _TaskRow.LastEditDate = _TaskRow.CreateDate = DateTime.Now;
                _TaskRow.Location = txtLocation.Text;
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
                    if (MyUser.GetFunctionGroup_ID() !="1")
                    {
                        _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                    }
                    if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                        _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                        _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                        _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                        _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Material_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    _NotificationRow.ProductPackage_ID = ProductPackage_ID;
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    if (!_NotificationRow.IsNotification_IDNull)
                    {
                        _NotificationRow = BusinessRulesLocator.GetNotificationBO().GetByPrimaryKey(_NotificationRow.Notification_ID);
                        _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Material_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID + "&Notification_ID=" + _NotificationRow.Notification_ID;
                        BusinessRulesLocator.GetNotificationBO().Update(_NotificationRow);
                    }
                }
                //lblMessage.Text = "Thêm mới thành công!";
                //lblMessage.Visible = true;
                //LoadData();
                //ClearData();
                Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Material_List?ProductPackage_ID=" + ProductPackage_ID, false);
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

    private void ClearData()
    {
        txtBuyerName.Text = txtDes.Text = txtPrice.Text = txtShopAddress.Text = txtShopName.Text = "";
        ddlMaterial.SelectedValue = "";
        //txtQuantity.Items.Clear();
        txtQuantity.Text = "";
    }

    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // txtQuantity.Items.Clear();
            if (ddlMaterial.SelectedValue != "")
            {
                DataTable dtUse = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Quantity),0) as SUM from Task where ProductPackage_ID=" + ProductPackage_ID + " and Material_ID=" + ddlMaterial.SelectedValue);

                DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Quantity),0) AS SUM from ProductPackageVsMaterial where  ProductPackage_ID=" + ProductPackage_ID + " and Material_ID = " + ddlMaterial.SelectedValue);
               // txtQuantity.Items.Insert(0, new ListItem("-- Chọn số lượng --", "0"));
                if (dt.Rows.Count > 0)
                {
                    int number = Convert.ToInt32(dt.Rows[0]["Sum"]);
                    
                    if (dtUse.Rows.Count == 1)
                    {
                        number = Convert.ToInt32(dt.Rows[0]["Sum"]) - Convert.ToInt32(dtUse.Rows[0]["Sum"]);
                        lblDInhMuc.Text = "Định mức còn lại tối đa: "+ number +" "+ txtDonVi.Text;
                    }
                    //for (int i = 1; i <= number; i++)
                    //{
                    //    txtQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    //}
                }

                DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DataTable dtPrice = BusinessRulesLocator.Conllection().GetAllList("select top 1 Price from MaterialPrice where ('" + s + "' between FromDate and ToDate) and Material_ID=" + ddlMaterial.SelectedValue);
                if (dtPrice.Rows.Count == 1)
                {
                    txtPrice.Text = decimal.Parse(dtPrice.Rows[0]["Price"].ToString()).ToString("N0");
                }
                else
                {
                    txtPrice.Text = "0";
                }
            }
            else
            {
                //txtQuantity.Items.Insert(0, new ListItem("-- Chọn số lượng --", "0"));
                txtPrice.Text = "0";
            }
            int Material_ID = Convert.ToInt32(ddlMaterial.SelectedValue);
            MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
            if (_MaterialRow != null)
            {
                txtDonVi.Text = "(" + (_MaterialRow.IsUnitNull ? string.Empty : _MaterialRow.Unit) + ")";
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("ddlMaterial_SelectedIndexChanged", ex.ToString());
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