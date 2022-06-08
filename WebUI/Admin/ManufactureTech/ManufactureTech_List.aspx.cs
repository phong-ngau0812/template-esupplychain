using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_ManufactureTech_ManufactureTech_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable(" UserID='" + MyUser.GetUser_ID() + "'", "");
                if (dt.Rows.Count == 1)
                {
                    ProductBrandList.Value = dt.Rows[0]["ProductBrand_ID_List"].ToString();
                }
            }
            FillProductBrand();
            LoadManuTech();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void LoadManuTech()
    {
        try
        {
            DataTable dt = new DataTable();
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += "and M.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                where += " and M.ProductBrand_ID in (" + ProductBrandList.Value + ")";
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select M.ManufactureTech_ID,M.Code_ID,PB.Name as ProductBrandName,M.Name,U.UserName,U1.UserName as'NguoiSua',M.Active from ManufactureTech M 
  left join ProductBrand PB on M.ProductBrand_ID = PB.ProductBrand_ID 
  left join aspnet_Users U on U.UserId = M.CreateBy 
  left join aspnet_Users U1 on U1.UserId = M.LastEditBy 
  where M.Active <> -1 " + where);
            }
            else
            {
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select M.ManufactureTech_ID,M.Code_ID,PB.Name as ProductBrandName,M.Name,U.UserName,U1.UserName as'NguoiSua',M.Active from ManufactureTech M 
  left join ProductBrand PB on M.ProductBrand_ID = PB.ProductBrand_ID 
  left join aspnet_Users U on U.UserId = M.CreateBy 
  left join aspnet_Users U1 on U1.UserId = M.LastEditBy 
  where M.Active <> -1 " + where);
            }
            rptManufactureTech.DataSource = dt;
            rptManufactureTech.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadManuTech", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManufactureTech_Add.aspx", false);
    }

    protected void rptManufactureTech_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ManufactureTech_ID = Convert.ToInt32(e.CommandArgument);
        ManufactureTechRow _ManufactureRow = new ManufactureTechRow();
        _ManufactureRow = BusinessRulesLocator.GetManufactureTechBO().GetByPrimaryKey(ManufactureTech_ID);
        switch (e.CommandName)
        {
            case "Delete":

                BusinessRulesLocator.GetManufactureTechBO().DeleteByPrimaryKey(ManufactureTech_ID);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_ManufactureRow != null)
                {
                    _ManufactureRow.Active = true;
                }
                BusinessRulesLocator.GetManufactureTechBO().Update(_ManufactureRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_ManufactureRow != null)
                {
                    _ManufactureRow.Active = false;
                }
                BusinessRulesLocator.GetManufactureTechBO().Update(_ManufactureRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadManuTech();
    }

    protected void rptManufactureTech_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                if (lblApproved.Text == "False")
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
    private void FillProductBrand()
    {
        try
        {
            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active=1", " Sort, Name ASC");
            //ddlProductBrand.DataSource = dt;
            //ddlProductBrand.DataTextField = "Name";
            //ddlProductBrand.DataValueField = "ProductBrand_ID";
            //ddlProductBrand.DataBind();
            //ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", "0"));
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, " ");
            }
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
    protected void ddlPageFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadManuTech();
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Pager1.CurrentIndex = 1;
        LoadManuTech();

    }
}