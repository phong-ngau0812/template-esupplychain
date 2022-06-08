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

public partial class Task_List_Material_Edit : System.Web.UI.Page
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
            FillDllStatus();
            FillMaterial();
            FillInfoTask();
            //  LoadData();
            CheckProductBrand();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        //  ResetMsg();
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
                string getName = string.Empty;
                if (MyUser.GetFunctionGroup_ID() == "4")
                {
                    getName = "WarehouseExportMaterial_ID"; // Trước đặt là Material_ID
                }
                else if (MyUser.GetFunctionGroup_ID() == "2")
                {
                    getName = "Material_ID";
                }
                else
                {
                    getName = "WarehouseExportMaterial_ID";
                }
                dt = BusinessRulesLocator.GetMaterialBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ProductBrand_ID + " and Material_ID in (select " + getName + " from ProductPackageVsMaterial where ProductPackage_ID=" + ProductPackage_ID + " )", "  Name ASC");
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
    protected void LoadNumber()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Quantity),0) AS SUM from ProductPackageVsMaterial where  ProductPackage_ID=" + ProductPackage_ID + " and Material_ID = " + ddlMaterial.SelectedValue);
        DataTable dtUse = BusinessRulesLocator.Conllection().GetAllList("  select ISNULL( SUM(Quantity),0) as SUM from Task where ProductPackage_ID=" + ProductPackage_ID + " and Material_ID=" + ddlMaterial.SelectedValue + " and Task_ID<>" + Task_ID);
        if (dt.Rows.Count > 0)
        {
            int number = Convert.ToInt32(dt.Rows[0]["Sum"]) - Convert.ToInt32(dtUse.Rows[0]["Sum"]);
            //for (int i = 1; i <= number; i++)
            //{
            //    txtQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //}
            //Count.Value = number.ToString();
            txtQuantity.Text = number.ToString();
        }

    }
    private void FillInfoTask()
    {
        try
        {
            if (Task_ID != 0)
            {
                TaskRow _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);
                //txtName.Text = _TaskRow.Name;
                //txtName.ReadOnly = true;
                ddlMaterial.SelectedValue = _TaskRow.Material_ID.ToString();

                int Material_ID = Convert.ToInt32(ddlMaterial.SelectedValue);
                MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                if (_MaterialRow != null)
                {
                    txtDonVi.Text = "(" + (_MaterialRow.IsUnitNull ? string.Empty : _MaterialRow.Unit) + ")";
                }
                LoadNumber();
                txtQuantity.Text = _TaskRow.Quantity.ToString("N2");
                //  SelectCount.Value = _TaskRow.Quantity.ToString("N0");
                txtPrice.Text = _TaskRow.Price.ToString("N0");
                txtBuyerName.Text = _TaskRow.BuyerName.ToString();
                txtUserName.Text = _TaskRow.UserName;
                txtShopName.Text = _TaskRow.ShopName;
                txtShopAddress.Text = _TaskRow.ShopAddress;
                if (!_TaskRow.IsStartDateNull)
                {
                    txtStart.Text = _TaskRow.StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                }

                txtLocation.Text = _TaskRow.Location;
                //txtLocation.Enabled = false;
                if ((_TaskRow.IsMaterial_IDNull ? 0 : Convert.ToInt32(_TaskRow.Material_ID)) != 0)
                {
                    ddlMaterial.SelectedValue = _TaskRow.Material_ID.ToString();
                }

                ddlStatus.SelectedValue = _TaskRow.TaskStatus_ID.ToString();
                txtDes.Text = _TaskRow.IsDescriptionNull ? "" : _TaskRow.Description;
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
    protected void LoadData()
    {
        try
        {
            //            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , T.UserName, T.Name  from Task T 
            //left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
            //where T.TaskType_ID=2 and T.ProductPackage_ID =" + ProductPackage_ID + " order by StartDate DESC");
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
                    //_TaskRow.TaskStep_ID = Convert.ToInt32(ddlTask.SelectedValue);
                    _TaskRow.TaskStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                    //_TaskRow.Name = ddlTask.SelectedItem.Text;
                    //_TaskRow.ProductName =ProductName;
                    //_TaskRow.ProductPackageName = ddlProductPackage.SelectedItem.Text;
                    //_TaskRow.CustomerName ="";

                    if (ddlMaterial.SelectedValue != "")
                    {
                        _TaskRow.Material_ID = Convert.ToInt32(ddlMaterial.SelectedValue);
                        _TaskRow.Name = ddlMaterial.SelectedItem.Text;
                    }

                    ////_TaskRow.Name = txtName.Text;
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        string quantity = txtQuantity.Text.Replace(",", ".");
                        Quantity = Convert.ToDecimal(quantity);
                    }
                    else
                    {
                        Quantity = 0;
                    }
                    //if (txtQuantity.SelectedValue !="")
                    //{
                    //    _TaskRow.Quantity = Convert.ToDecimal(txtQuantity.SelectedValue);
                    //}
                    //else
                    //{
                    //    _TaskRow.Quantity = 0;
                    //}
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
                    //_TaskRow.UserName = new MyUser().FullNameFromUserName(HttpContext.Current.User.Identity.Name);
                    _TaskRow.Location = txtLocation.Text;
                    //_TaskRow.Staff_ID = GetListStaff_ID();
                }
                BusinessRulesLocator.GetTaskBO().Update(_TaskRow);
                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.Visible = true;
                LoadData();
                FillInfoTask();
                Response.Redirect("/Admin/TaskVsProductPackage/Task_List_Material_List?ProductPackage_ID=" + ProductPackage_ID, false);
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