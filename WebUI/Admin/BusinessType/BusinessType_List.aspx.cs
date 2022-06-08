using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_BusinessType_BussinessType_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            LoadBranch();
            LoadBusinessType();
        }
    }
    protected void LoadBranch()
    {
      
        DataTable dtBranch = new DataTable();
        dtBranch = BusinessRulesLocator.GetBranchBO().GetAsDataTable(" Active=1", " Sort ASC");
        ddlBranch.DataSource = dtBranch;
        ddlBranch.DataTextField = "Name";
        ddlBranch.DataValueField = "Branch_ID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("--Lọc theo Ngành --", "0"));

    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadBusinessType()
    {
        //string where = string.Empty;
        //if (ddlBranch.SelectedValue != "0")
        //{
        //    where += " and Branch_ID=" + ddlBranch.SelectedValue;
        //}

        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetBusinessType();
        rptBusinessType.DataSource = dt;
        rptBusinessType.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BusinessType_Add.aspx", false);
    }
    protected void rptBusinessType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int BusinessType_ID = Convert.ToInt32(e.CommandArgument);
        BusinessTypeRow _BusinessTypeRow = new BusinessTypeRow();
        _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (MyActionPermission.CanDeleteBusinessType(BusinessType_ID, ref Message))
                {
                    if (_BusinessTypeRow != null)
                    {
                        _BusinessTypeRow.Status = -1;
                    }
                    BusinessRulesLocator.GetBusinessTypeBO().Update(_BusinessTypeRow);
                    MyActionPermission.WriteLogSystem(BusinessType_ID, "Xóa - " + _BusinessTypeRow.Title);
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
                _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
                if (_BusinessTypeRow != null)
                {
                    _BusinessTypeRow.Active = 1;
                }
                BusinessRulesLocator.GetBusinessTypeBO().Update(_BusinessTypeRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
                if (_BusinessTypeRow != null)
                {
                    _BusinessTypeRow.Active = 0;
                }
                BusinessRulesLocator.GetBusinessTypeBO().Update(_BusinessTypeRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadBusinessType();
    }

    protected void rptBusinessType_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBusinessType();
    }
}