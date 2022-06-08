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

public partial class TaskStepVsProductPackage_List : System.Web.UI.Page
{
    public string name, namepackage, demuc,code = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtStart.Attributes.Add("readonly", "readonly");
        //txtEnd.Attributes.Add("readonly", "readonly");
        if (!string.IsNullOrEmpty(Request["TaskStep_ID"]))
            int.TryParse(Request["TaskStep_ID"].ToString(), out TaskStep_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        Init();
        LoadTaskStep();
        ResetMsg();
    }

    private void Init()
    {
        if (TaskStep_ID != 0)
        {
            IDUPDATE.Visible = true;
            txtName.Text = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID).Name;
            txtName.Enabled = false;
        }
        else
        {
            IDUPDATE.Visible = false;
        }
        if (Product_ID != 0)
        {
            name = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID).Name;
        }

        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow != null)
            {
                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                if (!IsPostBack)
                {
                    if (!_ProductPackageRow.IsStartDateNull)
                    {
                        YearStart.Value = _ProductPackageRow.StartDate.Year.ToString();
                        MonthStart.Value = _ProductPackageRow.StartDate.Month.ToString();
                        DayStart.Value = _ProductPackageRow.StartDate.Day.ToString();
                    }
                    if (!_ProductPackageRow.IsEndDateNull)
                    {
                        YearEnd.Value = _ProductPackageRow.EndDate.Year.ToString();
                        MonthEnd.Value = _ProductPackageRow.EndDate.Month.ToString();
                        DayEnd.Value = _ProductPackageRow.EndDate.Day.ToString();
                    }
                    if (TaskStep_ID != 0)
                    {
                        DataTable dt = new DataTable();
                        dt = BusinessRulesLocator.GetTaskStepVsProductPackageBO().GetAsDataTable(" ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=" + TaskStep_ID, "");
                        if (dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[0]["StartTime"].ToString()))
                            {

                                txtStart.Text = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).ToString("dd/MM/yyyy");
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[0]["EndTime"].ToString()))
                            {

                                txtEnd.Text = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            if (!_ProductPackageRow.IsStartDateNull)
                                txtStart.Text = _ProductPackageRow.StartDate.ToString("dd/MM/yyyy");
                         
                            if (!_ProductPackageRow.IsStartDateNull)
                                txtEnd.Text = _ProductPackageRow.EndDate.ToString("dd/MM/yyyy");
                         
                        }
                    }
                }
            }

        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadTaskStep()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Name, T.TaskStep_ID, T.Product_ID, (select StartTime from TaskStepVsProductPackage where Product_ID=" + Product_ID + " and  ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=T.TaskStep_ID) as StartTime,  (select EndTime from TaskStepVsProductPackage where Product_ID=" + Product_ID + " and  ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=T.TaskStep_ID) as EndTime  from TaskStep T  where T.Product_ID=" + Product_ID);
            //dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " Sort ASC");
            rptTaskStep.DataSource = dt;
            rptTaskStep.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepProduct_Add.aspx?Product_ID=" + Product_ID, false);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected string GetStartTime(int ProductPackage_ID, int TaskStep_ID)
    {
        string time = string.Empty;
        DataTable dt = new DataTable();
        if (ProductPackage_ID != 0 && TaskStep_ID != 0)
        {
            dt = BusinessRulesLocator.GetTaskStepVsProductPackageBO().GetAsDataTable("ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=" + TaskStep_ID, "");
            if (dt.Rows.Count == 1)
            {
                time = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        return time;
    }
    protected string GetEndTime(int ProductPackage_ID, int TaskStep_ID)
    {
        string time = string.Empty;
        DataTable dt = new DataTable();
        if (ProductPackage_ID != 0 && TaskStep_ID != 0)
        {
            dt = BusinessRulesLocator.GetTaskStepVsProductPackageBO().GetAsDataTable("ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=" + TaskStep_ID, "");
            if (dt.Rows.Count == 1)
            {
                time = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        return time;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //Xóa thông tin 
                BusinessRulesLocator.GetTaskStepVsProductPackageBO().Delete("ProductPackage_ID=" + ProductPackage_ID + " and TaskStep_ID=" + TaskStep_ID);

                TaskStepVsProductPackageRow _TaskStepVsProductPackageRow = new TaskStepVsProductPackageRow();
                _TaskStepVsProductPackageRow.ProductPackage_ID = ProductPackage_ID;
                _TaskStepVsProductPackageRow.TaskStep_ID = TaskStep_ID;
                if (!string.IsNullOrEmpty(txtStart.Text.Trim()))
                {
                    DateTime start = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _TaskStepVsProductPackageRow.StartTime = start;
                }
                if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
                {
                    DateTime end = DateTime.ParseExact(txtEnd.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _TaskStepVsProductPackageRow.EndTime = end;
                }
                _TaskStepVsProductPackageRow.LastEditedDate = DateTime.Now;
                _TaskStepVsProductPackageRow.LastEditedBy = MyUser.GetUser_ID();
                BusinessRulesLocator.GetTaskStepVsProductPackageBO().Insert(_TaskStepVsProductPackageRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                LoadTaskStep();
            }

        }
        catch (Exception ex)
        {

            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code="+code,false);
    }
}