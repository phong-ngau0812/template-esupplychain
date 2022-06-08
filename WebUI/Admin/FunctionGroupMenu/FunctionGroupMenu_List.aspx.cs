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

public partial class FunctionGroupMenu_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFunctionGroupCategory();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadFunctionGroupCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupMenuBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            rptFunctionGroupMenu.DataSource = dt;
            rptFunctionGroupMenu.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroupCategory", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("FunctionGroupMenu_Add.aspx", false);
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
                FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _FunctionGroupMenuRow = BusinessRulesLocator.GetFunctionGroupMenuBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _FunctionGroupMenuRow.Active = 1;

                        BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _FunctionGroupMenuRow.Active = 0;
                        BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadFunctionGroupCategory();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptFunctionGroupMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int FunctionGroupMenu_ID = Convert.ToInt32(e.CommandArgument);
        FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();
        _FunctionGroupMenuRow = BusinessRulesLocator.GetFunctionGroupMenuBO().GetByPrimaryKey(FunctionGroupMenu_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_FunctionGroupMenuRow != null)
                {
                    _FunctionGroupMenuRow.Active = -1;
                }
                BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_FunctionGroupMenuRow != null)
                {
                    _FunctionGroupMenuRow.Active = 1;
                }
                BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_FunctionGroupMenuRow != null)
                {
                    _FunctionGroupMenuRow.Active = 0;
                }
                BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadFunctionGroupCategory();
    }

    protected void rptFunctionGroupMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
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