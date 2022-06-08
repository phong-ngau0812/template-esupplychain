using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TaskStepProduct_List : System.Web.UI.Page
{
    public string name = string.Empty;
    int Product_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (Product_ID != 0)
        {
            ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
            if (_ProductRow != null)
            {
                name = _ProductRow.Name;
                if (!_ProductRow.IsProductBrand_IDNull)
                {
                    MyActionPermission.CheckPermission(_ProductRow.ProductBrand_ID.ToString(), _ProductRow.CreateBy.ToString(), "/Admin/Product/Product_List");
                }
            }
        }

        if (!IsPostBack)
        {
            LoadTaskStep();
        }
        ResetMsg();
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
            dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " Sort ASC, TaskStep_ID DESC");
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

    protected void rptTaskStep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int TaskStep_ID = Convert.ToInt32(e.CommandArgument);
        TaskStepRow _TaskStepRow = new TaskStepRow();
        _TaskStepRow = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID);
        switch (e.CommandName)
        {
            case "Delete":
             
                BusinessRulesLocator.GetTaskStepBO().DeleteByPrimaryKey(TaskStep_ID);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
        }
        lblMessage.Visible = true; ;
        LoadTaskStep();
    }


    protected void btnListTaskStepQuestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/TaskStepQuestion/TaskStepQuestion_List.aspx?Product_ID=" + Product_ID, false);
    }
}