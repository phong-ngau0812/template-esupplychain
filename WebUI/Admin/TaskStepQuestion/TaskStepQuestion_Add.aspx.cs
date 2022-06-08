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

public partial class TaskStepQuestion_Add : System.Web.UI.Page
{
    public string title = "Thêm mới câu hỏi";
    public int Product_ID = 0;
    public string LinkCallBack = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!IsPostBack)
        {
            FillTask();
        }
        if (Product_ID != 0)
        {
            LinkCallBack += "<a href = \" "+"TaskStepQuestion_List.aspx?Product_ID="+Product_ID +" \"> Quản lý câu hỏi </a > ";
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
                ddlTask.Items.Insert(0, new ListItem("-- Chọn đề mục công việc --", ""));
            }
            catch (Exception ex)
            {
                Log.writeLog("FillDllStatus", ex.ToString());
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddTaskStepQuestion();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void AddTaskStepQuestion()
    {
        try
        {
            TaskStepQuestionRow _TaskStepQuestionRow = new TaskStepQuestionRow();
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

            _TaskStepQuestionRow.Sort = Common.GenarateSort("TaskStepQuestion");
            _TaskStepQuestionRow.CreateBy = MyUser.GetUser_ID();
            _TaskStepQuestionRow.CreateDate = DateTime.Now;

            BusinessRulesLocator.GetTaskStepQuestionBO().Insert(_TaskStepQuestionRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
        }
        catch (Exception ex)
        {
            Log.writeLog("AddTaskStepQuestion", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        ddlTask.SelectedValue = "";
        txtName.Text = "";
        //ddlProductBrand.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepQuestion_List.aspx?Product_ID=" + Product_ID, false);
    }

   
}