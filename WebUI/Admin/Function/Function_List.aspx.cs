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

public partial class Function_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLFunctionGroup();
            LoadFunction();
        }
        ResetMsg();
    }

    private void FillDDLFunctionGroup()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlFunctionGroup.DataSource = dt;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }



    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    //Lấy mane functiongroupmenu theo FunctionGroupMenu_ID trong bảng function
    public string ReturnFouctionGroupName(int FunctionGroupMenu_ID)
    {

        string name = string.Empty;
        if (FunctionGroupMenu_ID != 0)
        {
            FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();
            _FunctionGroupMenuRow = BusinessRulesLocator.GetFunctionGroupMenuBO().GetByPrimaryKey(FunctionGroupMenu_ID);
            if (_FunctionGroupMenuRow != null)
            {
                name = _FunctionGroupMenuRow.IsNameNull ? "" : _FunctionGroupMenuRow.Name;
            }
        }

        return name;

    }

    //Lấy mane functiongroupmenu theo FunctionGroupMenu_ID trong bảng function theo câu lệnh truy vấn.
    protected void LoadFunction()
    {
        try
        {
            string where = " where V.Active<>-1";
            if (ddlFunctionGroup.SelectedValue != "0")
            {
                where += " and V.FunctionGroup_ID like '%" + ddlFunctionGroup.SelectedValue + "%'";
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select V.* from Function V  " + where +" order by V.Sort asc");

            rptFunction.DataSource = dt;
            rptFunction.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Function_Add.aspx", false);
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
                FunctionGroupRow _FunctionGroupRow = new FunctionGroupRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _FunctionGroupRow = BusinessRulesLocator.GetFunctionGroupBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _FunctionGroupRow.Active = 1;

                        BusinessRulesLocator.GetFunctionGroupBO().Update(_FunctionGroupRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _FunctionGroupRow.Active = 0;
                        BusinessRulesLocator.GetFunctionGroupBO().Update(_FunctionGroupRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadFunction();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptFunction_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Function_ID = Convert.ToInt32(e.CommandArgument);
        FunctionRow _FunctionRow = new FunctionRow();
        _FunctionRow = BusinessRulesLocator.GetFunctionBO().GetByPrimaryKey(Function_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_FunctionRow != null)
                {
                    _FunctionRow.Active = -1;
                }
                BusinessRulesLocator.GetFunctionBO().Update(_FunctionRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_FunctionRow != null)
                {
                    _FunctionRow.Active = 1;
                }
                BusinessRulesLocator.GetFunctionBO().Update(_FunctionRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_FunctionRow != null)
                {
                    _FunctionRow.Active = 0;
                }
                BusinessRulesLocator.GetFunctionBO().Update(_FunctionRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadFunction();
    }

    protected void rptFunction_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void ddlFunctionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFunction();
    }


}