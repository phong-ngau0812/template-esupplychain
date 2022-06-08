using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TaskStepQuestion_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    public int Product_ID = 0;
    public int Srot = 1;
    public string LinkCallBack = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
       
        if (Product_ID != 0)
        {
            LinkCallBack += "<a href = \" " + "/Admin/Product/TaskStepProduct_List?Product_ID=" + Product_ID + " \"> Danh sách đề mục công việc sản phẩm </a > ";
        }
        if (!IsPostBack)
        {
           
            FillTask();
            loadTaskStepQuestion();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
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

    protected void loadTaskStepQuestion()
    {
        string where = "";
       
        if (ddlTask.SelectedValue != "")
        {
            where += "and TQ.TaskStep_ID =" + ddlTask.SelectedValue;
        }
        if (Product_ID != 0)
        {
            where += "and TS.Product_ID =" + Product_ID;
        }
       // where += "and  C.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select TQ.TaskStepQuestion_ID , TQ.Name ,TQ.Active , TS.Product_ID ,TS.Name as NameTaskStep
from TaskStepQuestion TQ
left join TaskStep TS on TQ.TaskStep_ID = TS.TaskStep_ID
where TQ.Active =1 " + where + " ORDER BY TQ.CreateDate DESC");
            rptTaskStepQuestion.DataSource = dt;
            rptTaskStepQuestion.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepQuestion_Add.aspx?Product_ID=" + Product_ID, false);
    }
    //protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    //{
    //    if (IsPostBack)
    //    {
    //        loadCustomer();
    //    }
    //}



    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTaskStepQuestion();
    }

    protected void rptTaskStepQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int TaskStepQuestion_ID = Convert.ToInt32(e.CommandArgument);
        TaskStepQuestionRow _TaskStepQuestionRow = new TaskStepQuestionRow();
        _TaskStepQuestionRow = BusinessRulesLocator.GetTaskStepQuestionBO().GetByPrimaryKey(TaskStepQuestion_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_TaskStepQuestionRow != null)
                {
                    MyActionPermission.WriteLogSystem(TaskStepQuestion_ID, "Xóa - " + _TaskStepQuestionRow.Name);
                    _TaskStepQuestionRow.Active = -1;
                    BusinessRulesLocator.GetTaskStepQuestionBO().Update(_TaskStepQuestionRow);
                    //BusinessRulesLocator.GetTaskStepQuestionBO().DeleteByPrimaryKey(TaskStepQuestion_ID);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;

            case "Active":
                if (_TaskStepQuestionRow != null)
                {
                    _TaskStepQuestionRow.Active = 1;
                }
                BusinessRulesLocator.GetTaskStepQuestionBO().Update(_TaskStepQuestionRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_TaskStepQuestionRow != null)
                {
                    _TaskStepQuestionRow.Active = 0;
                }
                BusinessRulesLocator.GetTaskStepQuestionBO().Update(_TaskStepQuestionRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        loadTaskStepQuestion();

    }

    protected void rptTaskStepQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "0")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    //lblText.Text = "<span class=\"badge badge-soft-danger\">Ngừng kích hoạt</span>";
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else

                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                    //lblText.Text = "<span class=\"badge badge-soft-success\">Đã kích hoạt</span>";
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                }
            }
        }

    }
}