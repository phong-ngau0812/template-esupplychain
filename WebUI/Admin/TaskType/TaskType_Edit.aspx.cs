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

public partial class TaskType_Edit : System.Web.UI.Page
{
    int TaskType_ID = 0;
    public string title = "Thông tin loại nhật ký";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["TaskType_ID"]))
            int.TryParse(Request["TaskType_ID"].ToString(), out TaskType_ID);
        if (!IsPostBack)
        {
            FillInfoTaskType();
        }
    }

    protected void FillInfoTaskType()
    {
        try
        {
            if (TaskType_ID != 0)
            {
                TaskTypeRow _TaskTypeRow = new TaskTypeRow();
                _TaskTypeRow = BusinessRulesLocator.GetTaskTypeBO().GetByPrimaryKey(TaskType_ID);
                if (_TaskTypeRow != null)
                {
                    txtName.Text = _TaskTypeRow.IsNameNull ? string.Empty : _TaskTypeRow.Name;
                    
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateTaskType()
    {
        try
        {
            TaskTypeRow _TaskTypeRow = new TaskTypeRow();
            if (TaskType_ID != 0)
            {
                _TaskTypeRow = BusinessRulesLocator.GetTaskTypeBO().GetByPrimaryKey(TaskType_ID);
                if (_TaskTypeRow != null)
                {
                    _TaskTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                   
                    //if (ckActive.Checked)
                    //{
                    //    _FunctionGroupRow.Active = 1;
                    //}
                    //else
                    //{
                    //    _FunctionGroupRow.Active = 0;
                    //}
                    //_FunctionGroupRow.LastEditBy = MyUser.GetUser_ID();
                    //_FunctionGroupRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetTaskTypeBO().Update(_TaskTypeRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoTaskType();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateTaskType", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateTaskType();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskType_List.aspx", false);
    }
}