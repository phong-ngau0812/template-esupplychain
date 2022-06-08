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

public partial class ProductPackageOrderCategory_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadProductPackageOrderCategory();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void LoadProductPackageOrderCategory()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select PPOC.* from ProductPackageOrderCategory PPOC inner join ProductBrand PB on PB.ProductBrand_ID = PPOC.ProductBrand_ID where PB.Active=1 and PPOC.Active<>-1  " + where + " order by PPOC.Sort asc");
            rptProductPackageOrderCategory.DataSource = dt;
            rptProductPackageOrderCategory.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackageOrderCategory_Add.aspx", false);
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
                ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = new ProductPackageOrderCategoryRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _ProductPackageOrderCategoryRow = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _ProductPackageOrderCategoryRow.Active = 1;

                        BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _ProductPackageOrderCategoryRow.Active = 0;
                        BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadProductPackageOrderCategory();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptProductPackageOrderCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductPackageOrderCategory_ID = Convert.ToInt32(e.CommandArgument);
        ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = new ProductPackageOrderCategoryRow();
        _ProductPackageOrderCategoryRow = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetByPrimaryKey(ProductPackageOrderCategory_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_ProductPackageOrderCategoryRow != null)
                {
                    _ProductPackageOrderCategoryRow.Active = -1;
                }
                BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_ProductPackageOrderCategoryRow != null)
                {
                    _ProductPackageOrderCategoryRow.Active = 1;
                }
                BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_ProductPackageOrderCategoryRow != null)
                {
                    _ProductPackageOrderCategoryRow.Active = 0;
                }
                BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadProductPackageOrderCategory();
    }

    protected void rptProductPackageOrderCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
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



    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackageOrderCategory();
    }
}