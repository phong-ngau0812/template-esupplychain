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

public partial class TaskStepProduct_Add : System.Web.UI.Page
{
    public int Product_ID = 0;
    public string title = "Thêm mới đề mục công việc ";
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
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
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
        if (!IsPostBack)
        {
            LoadDepartment();
        }

    }

    protected void LoadDepartment()
    {
        string where = string.Empty;
        if (Common.GetFunctionGroupDN())
        {
            where = " and ProductBrand_ID = " + MyUser.GetProductBrand_ID();
        }
        //if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
        //{
        //    where = " and ProductBrand_ID = " + MyUser.GetProductBrand_ID();
        //}
        DataTable dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable("Active = 1" + where, " CreateDate DESC");
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataValueField = "Department_ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-- Chọn phòng ban / bộ phận --", ""));
    }
    protected void AddTaskStep()
    {
        try
        {
            TaskStepRow _TaskStepRow = new TaskStepRow();
            _TaskStepRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _TaskStepRow.Product_ID = Product_ID;
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                _TaskStepRow.Sort = Convert.ToInt32(txtOrder.Text);
            }
            else
            {
                _TaskStepRow.Sort = 1;
            }
            if (ddlDepartment.SelectedValue != "")
            {
                _TaskStepRow.Department_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            _TaskStepRow.Description = txtNote.Text;
            BusinessRulesLocator.GetTaskStepBO().Insert(_TaskStepRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            txtName.Text = txtOrder.Text = "";
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
                AddTaskStep();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepProduct_List.aspx?Product_ID=" + Product_ID, false);
    }
}