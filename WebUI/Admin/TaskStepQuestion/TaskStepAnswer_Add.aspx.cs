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

public partial class TaskStepAnswer_Add : System.Web.UI.Page
{
    public string title = "Thêm mới câu trả lời";
    public int TaskStepQuestion_ID = 0;
    public string LinkCallBack = "";
    public int Srot = 1;
    public int Product_ID = 0;
    public string Question = "";
    private int TaskAnswer_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["TaskStepQuestion_ID"]))
            int.TryParse(Request["TaskStepQuestion_ID"].ToString(), out TaskStepQuestion_ID);
        
        if (!IsPostBack)
        {
            LoadTaskStepAnswer();
        }
    }
    protected void LoadTaskStepAnswer()
    {
        if (TaskStepQuestion_ID != 0)
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  TQ.Name,  TS.Product_ID
from TaskStepQuestion TQ
left join TaskStep TS on TQ.TaskStep_ID = TS.TaskStep_ID
where TQ.TaskStepQuestion_ID =" + TaskStepQuestion_ID);
            if (dt.Rows.Count != 0)
            {
                Question +=dt.Rows[0]["Name"].ToString() ;
                Product_ID = Convert.ToInt32(dt.Rows[0]["Product_ID"]);
                LinkCallBack += "<a href = \" " + "TaskStepQuestion_List.aspx?Product_ID=" + Product_ID + " \"> Quản lý câu hỏi </a > ";
            }
            DataTable dts = new DataTable();
            dts = BusinessRulesLocator.GetTaskStepAnswerBO().GetAsDataTable("Active <>-1 and TaskStepQuestion_ID = " + TaskStepQuestion_ID, "CreateDate DESC ");
            rptTaskStepAnswer.DataSource = dts;
            rptTaskStepAnswer.DataBind();
        }
    }
    protected void AddTaskStepAnswer()
    {
        if (TaskStepQuestion_ID != 0)
        {
                try
                {
                    TaskStepAnswerRow _TaskStepAnswerRow = new TaskStepAnswerRow();

                    // _TaskStepAnswerRow.Name = string.IsNullOrEmpty(txtNameAnswer.Text) ? string.Empty : txtNameAnswer.Text;
                    _TaskStepAnswerRow.TaskStepQuestion_ID = TaskStepQuestion_ID;
                    _TaskStepAnswerRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _TaskStepAnswerRow.Active = 1;
                    _TaskStepAnswerRow.Sort = Common.GenarateSort("TaskStepAnswer");
                    _TaskStepAnswerRow.CreateBy = MyUser.GetUser_ID();
                    _TaskStepAnswerRow.CreateDate = DateTime.Now;

                    BusinessRulesLocator.GetTaskStepAnswerBO().Insert(_TaskStepAnswerRow);
                    lblMessage.Text = "Thêm mới thành công!";
                    lblMessage.Visible = true;
                    ClearForm();
                    LoadTaskStepAnswer();
                }
                catch (Exception ex)
                {
                    Log.writeLog("AddTaskStepQuestion", ex.ToString());
                }
        }
    }
    protected void ClearForm()
    {
       
        txtName.Text = "";
        //ddlProductBrand.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Product_ID != 0)
        {
            Response.Redirect("TaskStepQuestion_List.aspx?Product_ID=" + Product_ID, false);
        }
    }

    protected void rptTaskStepAnswer_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int TaskStepAnswer_ID = Convert.ToInt32(e.CommandArgument);
        TaskStepAnswerRow _TaskStepAnswerRow = new TaskStepAnswerRow();
        _TaskStepAnswerRow = BusinessRulesLocator.GetTaskStepAnswerBO().GetByPrimaryKey(TaskStepAnswer_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_TaskStepAnswerRow != null)
                {
                    MyActionPermission.WriteLogSystem(TaskStepAnswer_ID, "Xóa - " + _TaskStepAnswerRow.Name);
                    _TaskStepAnswerRow.Active = -1;
                    BusinessRulesLocator.GetTaskStepAnswerBO().Update(_TaskStepAnswerRow);
                    //BusinessRulesLocator.GetTaskStepQuestionBO().DeleteByPrimaryKey(TaskStepQuestion_ID);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
        }
        lblMessage.Visible = true; ;
        LoadTaskStepAnswer();
    }

    protected void rptTaskStepAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddTaskStepAnswer();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

}