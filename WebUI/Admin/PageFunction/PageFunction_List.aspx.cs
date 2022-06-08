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

public partial class PageFunction_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLFunction();
            LoadPageFunction();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }



    private void FillDDLFunction()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlPageFunction.DataSource = dt;
            ddlPageFunction.DataTextField = "Name";
            ddlPageFunction.DataValueField = "Function_ID";
            ddlPageFunction.DataBind();
            ddlPageFunction.Items.Insert(0, new ListItem("-- Chọn chức năng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLFunction", ex.ToString());
        }
    }

    protected void LoadPageFunction()
    {
        try
        {
            string where = "";
            if (ddlPageFunction.SelectedValue != "")
            {
                where += " and PF.Function_ID =" + ddlPageFunction.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select PF.* from PageFunction PF inner join Function F on F.Function_ID= PF.Function_ID where PF.Active<>-1 and F.Active<>-1 " + where + " order by PF.Sort asc");
            rptPageFunction.DataSource = dt;
            rptPageFunction.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageFunction_Add.aspx", false);
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
                PageFunctionRow _PageFunctionRow = new PageFunctionRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _PageFunctionRow = BusinessRulesLocator.GetPageFunctionBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _PageFunctionRow.Active = 1;

                        BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _PageFunctionRow.Active = 0;
                        BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadPageFunction();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptPageFunction_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int PageFunction_ID = Convert.ToInt32(e.CommandArgument);
        PageFunctionRow _PageFunctionRow = new PageFunctionRow();
        _PageFunctionRow = BusinessRulesLocator.GetPageFunctionBO().GetByPrimaryKey(PageFunction_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_PageFunctionRow != null)
                {
                    _PageFunctionRow.Active = -1;
                }
                BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_PageFunctionRow != null)
                {
                    _PageFunctionRow.Active = 1;
                }
                BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_PageFunctionRow != null)
                {
                    _PageFunctionRow.Active = 0;
                }
                BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadPageFunction();
    }

    protected void rptPageFunction_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void ddlPageFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPageFunction();
    }
}