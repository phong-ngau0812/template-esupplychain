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

public partial class TaskStepAnswer_Edit : System.Web.UI.Page
{
    int TaskStepAnswer_ID = 0;
    public string title = "Thông tin câu hỏi";
    public string avatar = "";
    public string LinkCallBack = "";
    public string Question = "";
    public int Srot = 1;
   // public int TaskStepQuestion_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["TaskStepAnswer_ID"]))
            int.TryParse(Request["TaskStepAnswer_ID"].ToString(), out TaskStepAnswer_ID);
        if (!IsPostBack)
        {
            FillTaskStepAnswer();
            LoadTaskStepAnswer();
        }

       
    }


    protected void FillTaskStepAnswer()
    {
        if (TaskStepAnswer_ID != 0)
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  TA.Name, TQ.Name AS NameQuestion ,TA.TaskStepQuestion_ID
from TaskStepAnswer TA
left join TaskStepQuestion TQ on TA.TaskStepQuestion_ID = TQ.TaskStepQuestion_ID
Where TQ.Active <>-1 and TA.TaskStepAnswer_ID =" + TaskStepAnswer_ID);
            if (dt.Rows.Count != 0)
            {
                Question = dt.Rows[0]["NameQuestion"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                TaskStepQuestion_ID.Value = dt.Rows[0]["TaskStepQuestion_ID"].ToString();
            }
        }
    }

    protected void LoadTaskStepAnswer()
    {
        if (!string.IsNullOrEmpty( TaskStepQuestion_ID.Value ))
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  TQ.Name,  TS.Product_ID
from TaskStepQuestion TQ
left join TaskStep TS on TQ.TaskStep_ID = TS.TaskStep_ID
where TQ.TaskStepQuestion_ID =" + TaskStepQuestion_ID.Value);
            if (dt.Rows.Count != 0)
            {
                //Question += dt.Rows[0]["Name"].ToString();
                ProductID.Value = (dt.Rows[0]["Product_ID"].ToString());
                LinkCallBack += "<a href = \" " + "TaskStepQuestion_List.aspx?Product_ID=" + dt.Rows[0]["Product_ID"].ToString() + " \"> Quản lý câu hỏi </a > ";
            }


            DataTable dts = new DataTable();
            dts = BusinessRulesLocator.GetTaskStepAnswerBO().GetAsDataTable("Active <>-1 and TaskStepQuestion_ID = " + TaskStepQuestion_ID.Value, "CreateDate DESC ");
            rptTaskStepAnswer.DataSource = dts;
            rptTaskStepAnswer.DataBind();
        }
    }

    protected void Update(){
        if (TaskStepAnswer_ID != 0)
        {
            try
            {
                TaskStepAnswerRow _TaskStepAnswerRow = new TaskStepAnswerRow();
                _TaskStepAnswerRow = BusinessRulesLocator.GetTaskStepAnswerBO().GetByPrimaryKey(TaskStepAnswer_ID);
                if (_TaskStepAnswerRow != null)
                {
                    _TaskStepAnswerRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _TaskStepAnswerRow.LastEditBy = MyUser.GetUser_ID();
                    _TaskStepAnswerRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetTaskStepAnswerBO().Update(_TaskStepAnswerRow);
                    lblMessage.Text = "Cập nhập thành công!";
                    lblMessage.Visible = true;
                    FillTaskStepAnswer();
                    LoadTaskStepAnswer();
                }

            }
            catch (Exception ex)
            {
                Log.writeLog("UpdateTaskStepQuestion", ex.ToString());
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                Update();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        
       
        if (!string.IsNullOrEmpty( ProductID.Value))
        {
            Response.Redirect("TaskStepQuestion_List.aspx?Product_ID=" + ProductID.Value, false);
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
        lblMessage.Visible = true;
        FillTaskStepAnswer();
        LoadTaskStepAnswer();

    }

    protected void rptTaskStepAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {


    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(TaskStepQuestion_ID.Value))
        {
            Response.Redirect("TaskStepAnswer_Add.aspx?TaskStepQuestion_ID=" + TaskStepQuestion_ID.Value, false);
        }
    }
}