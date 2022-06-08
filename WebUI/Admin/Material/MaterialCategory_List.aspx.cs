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

public partial class MaterialCategory_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMateriaCategory();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadMateriaCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            rptMateria.DataSource = dt;
            rptMateria.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialCategory_Add.aspx", false);
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
                MaterialCategoryRow _MaterialCategoryRow = new MaterialCategoryRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _MaterialCategoryRow = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _MaterialCategoryRow.Active = 1;

                        BusinessRulesLocator.GetMaterialCategoryBO().Update(_MaterialCategoryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _MaterialCategoryRow.Active = 0;
                        BusinessRulesLocator.GetMaterialCategoryBO().Update(_MaterialCategoryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadMateriaCategory();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptMateria_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int MaterialCategory_ID = Convert.ToInt32(e.CommandArgument);
        MaterialCategoryRow _MaterialCategoryRow = new MaterialCategoryRow();
        _MaterialCategoryRow = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(MaterialCategory_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_MaterialCategoryRow != null)
                {
                    _MaterialCategoryRow.Active = -1;
                }
                BusinessRulesLocator.GetMaterialCategoryBO().Update(_MaterialCategoryRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                _MaterialCategoryRow = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(MaterialCategory_ID);
                if (_MaterialCategoryRow != null)
                {
                    _MaterialCategoryRow.Active = 1;
                }
                BusinessRulesLocator.GetMaterialCategoryBO().Update(_MaterialCategoryRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _MaterialCategoryRow = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(MaterialCategory_ID);
                if (_MaterialCategoryRow != null)
                {
                    _MaterialCategoryRow.Active = 0;
                }
                BusinessRulesLocator.GetMaterialCategoryBO().Update(_MaterialCategoryRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadMateriaCategory();
    }

    protected void rptMateria_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            //CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
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