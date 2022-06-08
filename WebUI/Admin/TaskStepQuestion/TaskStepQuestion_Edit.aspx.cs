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
using Telerik.Web.UI;

public partial class TaskStepQuestion_Edit : System.Web.UI.Page
{
    int Product_ID = 0;
    int TaskStepQuestion_ID = 0;
    public string title = "Thông tin câu hỏi";
    public string avatar = "";
    public string LinkCallBack = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["TaskStepQuestion_ID"]))
            int.TryParse(Request["TaskStepQuestion_ID"].ToString(), out TaskStepQuestion_ID);
        LoadProduct_ID();
        if (!IsPostBack)
        {
            
            FillTask();
            FillInfoTaskStepQuestion();
        }

       
    }

    private void LoadProduct_ID()
    {
        if (TaskStepQuestion_ID != 0)
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  TS.Product_ID
from TaskStepQuestion TQ
left join TaskStep TS on TQ.TaskStep_ID = TS.TaskStep_ID
where TQ.TaskStepQuestion_ID =" + TaskStepQuestion_ID);
            if(dt.Rows.Count != 0)
            {
                Product_ID = Convert.ToInt32(dt.Rows[0]["Product_ID"]);
                LinkCallBack += "<a href = \" " + "TaskStepQuestion_List.aspx?Product_ID=" + Product_ID + " \"> Quản lý câu hỏi </a > ";
            }

        }
    }


    protected void FillTask()
    {
        if (Product_ID != 0)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " SORT ASC");
                ddlTask.DataSource = dt;
                ddlTask.DataTextField = "Name";
                ddlTask.DataValueField = "TaskStep_ID";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("-- Chọn đề mục công việc --", "0"));
            }
            catch (Exception ex)
            {
                Log.writeLog("FillDllStatus", ex.ToString());
            }
        }

    }

    protected void FillInfoTaskStepQuestion()
    {
        try
        {
            if (TaskStepQuestion_ID != 0)
            {
                TaskStepQuestionRow _TaskStepQuestionRow = new TaskStepQuestionRow();
                _TaskStepQuestionRow = BusinessRulesLocator.GetTaskStepQuestionBO().GetByPrimaryKey(TaskStepQuestion_ID);

                if (_TaskStepQuestionRow != null)
                {
                    ddlTask.SelectedValue = _TaskStepQuestionRow.IsTaskStep_IDNull? "0" : _TaskStepQuestionRow.TaskStep_ID.ToString();
                    FillTask();
                    txtName.Text = _TaskStepQuestionRow.IsNameNull ? string.Empty : _TaskStepQuestionRow.Name;
                    if (_TaskStepQuestionRow.Active == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                    

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoTaskStepQuestion", ex.ToString());
        }
    }



    protected void UpdateTaskStepQuestion()
    {
        try
        {
            if (TaskStepQuestion_ID != 0)
            {
                TaskStepQuestionRow _TaskStepQuestionRow = new TaskStepQuestionRow();
                _TaskStepQuestionRow = BusinessRulesLocator.GetTaskStepQuestionBO().GetByPrimaryKey(TaskStepQuestion_ID);
                if (_TaskStepQuestionRow != null)
                {
                    _TaskStepQuestionRow.TaskStep_ID = Convert.ToInt32(ddlTask.SelectedValue);
                    _TaskStepQuestionRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    if (ckActive.Checked)
                    {
                        _TaskStepQuestionRow.Active = 1;
                    }
                    else
                    {
                        _TaskStepQuestionRow.Active = 0;
                    }

                    _TaskStepQuestionRow.LastEditBy = MyUser.GetUser_ID();
                    _TaskStepQuestionRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetTaskStepQuestionBO().Update(_TaskStepQuestionRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoTaskStepQuestion();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomer", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateTaskStepQuestion();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Product_ID != 0)
        {
            Response.Redirect("TaskStepQuestion_List.aspx?Product_ID=" + Product_ID, false);
        }
    }
}