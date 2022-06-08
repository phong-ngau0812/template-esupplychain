using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TaskStepProduct_Edit : System.Web.UI.Page
{

    int TaskStep_ID = 0;
    public int Product_ID = 0;
    public string title = "Cập nhật đề mục công việc ";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Common.CheckUserXuanHoa1())
        {
            xuanhoa.Visible = true;
        }
        else
        {
            xuanhoa.Visible = false;
        }
        if (!string.IsNullOrEmpty(Request["TaskStep_ID"]))
            int.TryParse(Request["TaskStep_ID"].ToString(), out TaskStep_ID);
        if (!IsPostBack)
        {
            LoadDepartment();
            FillInfoTaskStep();
        }
    }
    protected void LoadDepartment()
    {
        string where = string.Empty;
        var dtProduct = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
        if (!Common.GetFunctionGroupDN())
        {
            where = " and ProductBrand_ID = " + dtProduct.ProductBrand_ID;
        }
        else if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
        {
            where = " and ProductBrand_ID = " + MyUser.GetProductBrand_ID();
        }
        DataTable dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable("Active = 1" + where, " CreateDate DESC");
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataValueField = "Department_ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-- Chọn phòng ban / bộ phận --", ""));
    }
    protected void FillInfoTaskStep()
    {
        try
        {
            if (TaskStep_ID != 0)
            {
                TaskStepRow _TaskStepRow = new TaskStepRow();
                _TaskStepRow = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID);
                if (_TaskStepRow != null)
                {

                    Product_ID = _TaskStepRow.IsProduct_IDNull ? 0 : _TaskStepRow.Product_ID;
                    HdProduct_ID.Value = _TaskStepRow.IsProduct_IDNull ? "0" : _TaskStepRow.Product_ID.ToString();
                    if (Product_ID != 0)
                    {
                        ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                        if (_ProductRow != null)
                        {
                            txtTenSanPham.Text = _ProductRow.Name;
                            if (!_ProductRow.IsProductBrand_IDNull)
                            {
                                MyActionPermission.CheckPermission(_ProductRow.ProductBrand_ID.ToString(), _ProductRow.CreateBy.ToString(), "/Admin/Product/Product_List");
                            }
                        }
                    }
                    ddlDepartment.SelectedValue = _TaskStepRow.IsDepartment_IDNull ? "" : _TaskStepRow.Department_ID.ToString();
                    txtName.Text = _TaskStepRow.IsNameNull ? string.Empty : _TaskStepRow.Name;
                    txtNote.Text = _TaskStepRow.IsDescriptionNull ? string.Empty : _TaskStepRow.Description;
                    txtOrder.Text = _TaskStepRow.IsSortNull ? string.Empty : _TaskStepRow.Sort.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateTaskStep()
    {
        try
        {
            TaskStepRow _TaskStepRow = new TaskStepRow();
            if (TaskStep_ID != 0)
            {
                _TaskStepRow = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID);
                if (_TaskStepRow != null)
                {
                    _TaskStepRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _TaskStepRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    if (!string.IsNullOrEmpty(txtOrder.Text))
                    {
                        _TaskStepRow.Sort = Convert.ToInt32(txtOrder.Text);
                    }
                    if (ddlDepartment.SelectedValue != "")
                    {
                        _TaskStepRow.Department_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    }
                    BusinessRulesLocator.GetTaskStepBO().Update(_TaskStepRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoTaskStep();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateTaskStep", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateTaskStep();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepProduct_List.aspx?Product_ID=" + HdProduct_ID.Value, false);
    }
}