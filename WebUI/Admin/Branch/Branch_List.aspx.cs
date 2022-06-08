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

public partial class Branch_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranch();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadBranch()
    {
        try
        {

            //rptBranch.DataSource =BusinessRulesLocator.GetBranchBO().GetAsDataTable("Active<>-1", "Sort asc");
            rptBranch.DataSource = BusinessRulesLocator.Conllection().GetBranch();
            rptBranch.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch_Add.aspx", false);
    }



    protected void rptBranch_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Branch_ID = Convert.ToInt32(e.CommandArgument);
        BranchRow _BranchRow = new BranchRow();
        _BranchRow = BusinessRulesLocator.GetBranchBO().GetByPrimaryKey(Branch_ID);
        switch (e.CommandName)
        {
            case "Delete":
                _BranchRow.Active = -1;
                BusinessRulesLocator.GetBranchBO().Update(_BranchRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                _BranchRow.Active = 1;
                BusinessRulesLocator.GetBranchBO().Update(_BranchRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _BranchRow.Active = 0;
                BusinessRulesLocator.GetBranchBO().Update(_BranchRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
        }
        lblMessage.Visible = true;
        LoadBranch();
    }


    protected void rptBranch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "False")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else
                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";

                }
            }
        }
    }
}