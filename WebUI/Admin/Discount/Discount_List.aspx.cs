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

public partial class Discount_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadDiscount();
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
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    protected void LoadDiscount()
    {
        try
        {
            string where = " Active<>-1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDiscountBO().GetAsDataTable( where , " Sort ASC");
            rptDiscount.DataSource = dt;
            rptDiscount.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Discount_Add.aspx", false);
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
                DiscountRow _DiscounRow = new DiscountRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _DiscounRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                  
                    if (ckActive.Checked)
                    {
                        _DiscounRow.Active = 1;
                    
                        BusinessRulesLocator.GetDiscountBO().Update(_DiscounRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _DiscounRow.Active = 0;
                        BusinessRulesLocator.GetDiscountBO().Update(_DiscounRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadDiscount();
                }
            }
          
        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptDiscount_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Discount_ID = Convert.ToInt32(e.CommandArgument);
        DiscountRow _DiscountRow = new DiscountRow();
        _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteDiscount(Discount_ID, ref Message))
                {
                    if (_DiscountRow != null)
                    {
                        _DiscountRow.Active = -1;
                    }
                    BusinessRulesLocator.GetDiscountBO().Update(_DiscountRow);
                    MyActionPermission.WriteLogSystem(Discount_ID, "Xóa - " + _DiscountRow.Name);
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
                _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
                if (_DiscountRow != null)
                {
                    _DiscountRow.Active = 1;
                }
                BusinessRulesLocator.GetDiscountBO().Update(_DiscountRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
                if (_DiscountRow != null)
                {
                    _DiscountRow.Active = 0;
                }
                BusinessRulesLocator.GetDiscountBO().Update(_DiscountRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadDiscount();
    }

    protected void rptDiscount_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadDiscount();
    }
}