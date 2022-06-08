using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Supplier_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
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
            FillDDLddlProductBrand();
            LoadSupplier();
        }
        ResetMsg();
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, where);
            }
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadSupplier()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                where += " and ProductBrand_ID in (" + ProductBrandList.Value + ")";
            }
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetAccountType_ID() == "7")
                {
                    where += " and CreateBy ='" + MyUser.GetUser_ID() + "'";
                }

            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetSupplierBO().GetAsDataTable(" Active<>-1 " + where, " CreateDate DESC");
            rptSupplier.DataSource = dt;
            rptSupplier.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_Add.aspx", false);
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
                SupplierRow _SupplierRow = new SupplierRow();
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                    if (ckActive.Checked)
                    {
                        _SupplierRow.Active = 1;
                        BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _SupplierRow.Active = 0;
                        BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadSupplier();
                }

            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptSupplier_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        int Supplier_ID = Convert.ToInt32(e.CommandArgument);
        SupplierRow _SupplierRow = new SupplierRow();
        _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);

        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteSupplier(Supplier_ID, ref Message))
                {
                    if (_SupplierRow != null)
                    {
                        _SupplierRow.Active = -1;
                    }
                    BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                    MyActionPermission.WriteLogSystem(Supplier_ID, "Xóa - " + _SupplierRow.Name);
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
                _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
                if (_SupplierRow != null)
                {
                    _SupplierRow.Active = 1;
                }
                BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
                if (_SupplierRow != null)
                {
                    _SupplierRow.Active = 0;
                }
                BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadSupplier();
    }

    protected void rptSupplier_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSupplier();
    }
}