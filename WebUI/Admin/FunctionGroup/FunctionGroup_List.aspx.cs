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

public partial class FunctionGroup_List : System.Web.UI.Page
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
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            rptFunctionGroup.DataSource = dt;
            rptFunctionGroup.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroupCategory", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("FunctionGroup_Add.aspx", false);
    }



    protected void rptFunctionGroup_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int FunctionGroup_ID = Convert.ToInt32(e.CommandArgument);
        FunctionGroupRow _FunctionGroupRow = new FunctionGroupRow();
        _FunctionGroupRow = BusinessRulesLocator.GetFunctionGroupBO().GetByPrimaryKey(FunctionGroup_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_FunctionGroupRow != null)
                {
                    _FunctionGroupRow.Active = -1;
                }
                BusinessRulesLocator.GetFunctionGroupBO().Update(_FunctionGroupRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
           

        }
        lblMessage.Visible = true; ;
        LoadFunctionGroupCategory();
    }

    protected void rptFunctionGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
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