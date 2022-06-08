using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TaskType_Add : System.Web.UI.Page
{
    public string title = "Thêm mới loại nhật ký";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddTaskType();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckExsitsCategoryName(string Name)
    {
        bool flag = true;
        string where = "Status <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddTaskType()
    {
        try
        {
            if (CheckExsitsCategoryName(txtName.Text.Trim()))
            {
                TaskTypeRow _TaskTypeRow = new TaskTypeRow();
                _TaskTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                _TaskTypeRow.Status = 1;
                BusinessRulesLocator.GetTaskTypeBO().Insert(_TaskTypeRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
               
            }
            else
            {
                lblMessage.Text = "Đã tồn tại nhóm chức năng " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateTaskType", ex.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskType_List.aspx", false);
    }
}