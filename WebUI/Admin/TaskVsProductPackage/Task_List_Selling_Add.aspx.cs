using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class Task_List_Selling_Add : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    int Product_ID = 0;
    int Workshop_ID = 0;
    int Farm_ID = 0;
    string ProductName = string.Empty;
    public string name, code = string.Empty;
    public string DiscountPercent = "";
    public string Address = "";
    public string FillAmount = "";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtDes);
        txtSumMoney.Attributes.Add("readonly", "true");
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);

        Init();
        if (!IsPostBack)
        {
            FillProductBrand();
            LoadProductPackage();
            FillDllStatus();
            LoadCustomer();
            LoadWarehouse();
            //LoadDataCustomer();
            // LoadData();
            CheckProductBrand();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
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
            Workshop_ID = _ProductPackageRow.IsWorkshop_IDNull ? 0 : _ProductPackageRow.Workshop_ID;
            if (!_ProductPackageRow.IsFarm_IDNull)
            {
                Farm_ID = _ProductPackageRow.Farm_ID;
            }
            ProductName = _ProductPackageRow.ProductName;
            code = _ProductPackageRow.Code;
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
            string where = "";
            if (ProductPackage_ID != 0)
            {
                where += "and Warehouse_ID in (select distinct Warehouse_ID from Task where  ProductPackage_ID=" + ProductPackage_ID + " and TaskType_ID=3)";
                ddlWarehouse.DataSource = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable(" Active=1 and Type=2 " + where, "");
                ddlWarehouse.DataTextField = "Name";
                ddlWarehouse.DataValueField = "Warehouse_ID";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
            }

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void LoadCustomer()
    {
        try
        {
            string where = string.Empty;

            ddlCustomer.DataSource = BusinessRulesLocator.GetCustomerBO().GetAsDataTable(" ProductBrand_ID=" + ProductBrand_ID, " Name ASC");
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "Customer_ID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("-- Chọn khách hàng --", ""));

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }


    protected void LoadDataCustomer()
    {
        try
        {
            string where = "";
            if (ddlCustomer.SelectedValue != "")
            {
                where += " and C.Customer_ID =" + ddlCustomer.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select C.*, CT.Discount_ID , D.[Percent] as DiscountPercent
from Customer C
left join CustomerType CT on C.CustomerType_ID = CT.CustomerType_ID 
left join Discount D on CT.Discount_ID = D.Discount_ID
where CT.Active<>-1 and D.Active <>-1 " + where);
            if (dt.Rows.Count == 1)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["DiscountPercent"].ToString()))
                {
                    DiscountPercent = ((dt.Rows[0]["DiscountPercent"].ToString()));
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Address"].ToString()))
                {
                    Address = (dt.Rows[0]["Address"].ToString());
                }
            }
            txtDiscountPercent.Text = DiscountPercent.ToString();
            txtShopAddress.Text = Address.ToString();
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
            //where T.TaskType_ID=6 and T.ProductPackage_ID = " + ProductPackage_ID + " order by StartDate DESC");
            //            rptTask.DataSource = dt;
            //            rptTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    private bool LoadAmountWarehouse(Decimal Amount)
    {
        bool fag = true;
        string where = "";

        if (ddlWarehouse.SelectedValue != "" && ProductPackage_ID != 0 && ProductBrand_ID != 0)
        {
            where += "and ProductPackage_ID =" + ProductPackage_ID + "and Warehouse_ID =" + Convert.ToInt32(ddlWarehouse.SelectedValue);
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SELECT distinct(( select ISNULL(SUM(HarvestVolume),0) from Task Where TaskType_ID=3 and TaskStatus_ID = 3 " + where + ") - (select ISNULL(SUM(Quantity),0) from Task Where TaskType_ID = 6" + where + "))AS Trongkho FROM Task Where ProductBrand_ID =" + ProductBrand_ID + where);
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["Trongkho"].ToString()))
                {
                    FillAmount += "( Số lượng tồn sản phẩm trong kho " + Decimal.Parse(dt.Rows[0]["Trongkho"].ToString()).ToString("N0") + ")";
                    if (Amount > Convert.ToDecimal(dt.Rows[0]["Trongkho"]))
                    {
                        fag = false;
                    }

                }
            }
        }
        return fag;
    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Selling_List?ProductPackage_ID=" + ProductPackage_ID, false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Decimal Quantity = 0;
        Decimal Pricetity = 0;
        try
        {
            if (Page.IsValid)
            {
                if (LoadAmountWarehouse(Convert.ToDecimal(txtQuantity.Text.Replace(",", ""))))
                {
                    TaskRow _TaskRow = new TaskRow();
                    _TaskRow.ProductBrand_ID = ProductBrand_ID;
                    _TaskRow.Product_ID = Product_ID;
                    _TaskRow.ProductPackage_ID = ProductPackage_ID;
                    _TaskRow.Workshop_ID = Workshop_ID;
                    _TaskRow.Farm_ID = Farm_ID;
                    _TaskRow.TaskType_ID = 6;
                    _TaskRow.TaskStep_ID = 0;
                    _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                    _TaskRow.ProductName = ProductName;
                    _TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                    _TaskRow.Customer_ID = Convert.ToInt32(ddlCustomer.SelectedValue);
                    _TaskRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                    _TaskRow.CustomerName = "";
                    _TaskRow.Description = txtDes.Text;

                    if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                    {
                        DateTime s = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        _TaskRow.StartDate = s;
                    }

                    _TaskRow.Name = txtName.Text;
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        string quantity = txtQuantity.Text.Replace(",", "");
                        Quantity = Convert.ToDecimal(quantity);
                    }
                    else
                    {
                        Quantity = 0;
                    }

                    _TaskRow.Quantity = Quantity;
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
                    //if (Quantity != 0 && Quantity !=0)
                    //{
                    //    Decimal SumMoney = Quantity * Pricetity;
                    //    _TaskRow.SumMoney = SumMoney;
                    //}
                    //else
                    //{
                    //    _TaskRow.SumMoney = 0;
                    //}
                    _TaskRow.SumMoney = Decimal.Parse(txtSumMoney.Text);
                    _TaskRow.BuyerName = txtBuyerName.Text;
                    _TaskRow.ShopName = string.IsNullOrEmpty(ddlCustomer.SelectedItem.ToString()) ? string.Empty : ddlCustomer.SelectedItem.ToString();
                    _TaskRow.ShopAddress = txtShopAddress.Text;
                    _TaskRow.KeyContainer = string.IsNullOrEmpty(txtContainer.Text) ? "" : txtContainer.Text;
                    _TaskRow.SoChi = string.IsNullOrEmpty(txtSoChi.Text) ? "" : txtSoChi.Text;

                    _TaskRow.UserName = new MyUser().FullNameFromUserName(HttpContext.Current.User.Identity.Name);
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
                        _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Selling_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID;
                        _NotificationRow.CreateBy = MyUser.GetUser_ID();
                        _NotificationRow.CreateDate = DateTime.Now;
                        _NotificationRow.Active = 1;
                        _NotificationRow.Alias = Guid.NewGuid();
                        _NotificationRow.ProductPackage_ID = ProductPackage_ID;
                        BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                        if (!_NotificationRow.IsNotification_IDNull)
                        {
                            _NotificationRow = BusinessRulesLocator.GetNotificationBO().GetByPrimaryKey(_NotificationRow.Notification_ID);
                            _NotificationRow.Url = "/Admin/TaskVsProductPackage/Task_List_Selling_Edit?Task_ID=" + _TaskRow.Task_ID + "&ProductPackage_ID=" + ProductPackage_ID + "&Notification_ID=" + _NotificationRow.Notification_ID;
                            BusinessRulesLocator.GetNotificationBO().Update(_NotificationRow);
                        }
                    }
                    //lblMessage.Text = "Thêm mới thành công!";
                    //lblMessage.Visible = true;
                    //LoadData();
                    //ClearData();
                    Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Selling_List?ProductPackage_ID=" + ProductPackage_ID, false);
                }
                else
                {
                    lblMessage.Text = "Bạn nhập quá số lượng trong kho!";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }

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
        txtName.Text = txtBuyerName.Text = txtDes.Text = txtPrice.Text = txtQuantity.Text = txtShopAddress.Text = "";
        ddlCustomer.SelectedValue = "";
        txtDiscountPercent.Text = "";
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataCustomer();
        LoadAmountWarehouse(0);
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAmountWarehouse(0);
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