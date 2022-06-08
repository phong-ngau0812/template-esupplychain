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

public partial class TaskType_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTaskType();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadTaskType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable(" Status<>-1", "");
            rptTaskType.DataSource = dt;
            rptTaskType.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskType_Add.aspx", false);
    }

    protected void ckActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckActive = (CheckBox)sender;
        RepeaterItem row = (RepeaterItem)ckActive.NamingContainer;
        Literal lblID = (Literal)row.FindControl("lblID");
        try
        {
            if (lblID != null)
            {
               TaskTypeRow _TaskTypeRow = new TaskTypeRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _TaskTypeRow = BusinessRulesLocator.GetTaskTypeBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    //if (ckActive.Checked)
                    //{
                    //    _TaskTypeRow.Active = 1;

                    //    BusinessRulesLocator.GetFunctionGroupBO().Update(_TaskTypeRow);
                    //    lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                    //    lblMessage.Visible = true;
                    //}
                    //else
                    //{
                    //    _FunctionGroupRow.Active = 0;
                    //    BusinessRulesLocator.GetFunctionGroupBO().Update(_FunctionGroupRow);
                    //    lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                    //    lblMessage.Visible = true;
                    //}
                    LoadTaskType();
                }
            }
          
        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptTaskType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
        switch (e.CommandName)
        {
            case "Delete":
                int FunctionGroup_ID = Convert.ToInt32(e.CommandArgument);
                TaskTypeRow _TaskTypeRow = new TaskTypeRow();
                _TaskTypeRow = BusinessRulesLocator.GetTaskTypeBO().GetByPrimaryKey(FunctionGroup_ID);
                if (_TaskTypeRow != null)
                {
                    _TaskTypeRow.Status = -1;
                }
                BusinessRulesLocator.GetTaskTypeBO().Update(_TaskTypeRow);
                lblMessage.Text = Common.GetSuccessMsg("Xóa bản ghi thành công !");
                lblMessage.Visible = true; ;
                LoadTaskType();
                break;
            
        }
    }

    protected void rptTaskType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "0")
                {
                    ckActive.Checked = false;
                }
                else
                {
                    ckActive.Checked = true;
                }
            }
        }
    }
}