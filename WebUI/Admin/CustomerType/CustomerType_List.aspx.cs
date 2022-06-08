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

public partial class CustomerType_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlDiscount();
            LoadCustomerType();
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
    private void FillDDLddlDiscount()
    {


        try
        {
            string where = " Active<>-1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDiscountBO().GetAsDataTable(where, " Sort ASC");
            ddlDiscount.DataSource = dt;
            ddlDiscount.DataTextField = "Name";
            ddlDiscount.DataValueField = "Discount_ID";
            ddlDiscount.DataBind();
            ddlDiscount.Items.Insert(0, new ListItem("-- Chọn chiết khấu --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void LoadCustomerType()
    {
        try
        {
            string where = "";
            string whereProductBrand_ID = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                whereProductBrand_ID += "and CT.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlDiscount.SelectedValue != "0")
            {
                where += " and D.Discount_ID =" + ddlDiscount.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  select CT.CustomerType_ID,CT.Name,CT.Active,ISNULL(CT.Discount_ID,NULL),CT.Name, ISNULL(D.Name, NULL) as DiscountName , ISNULL(D.[Percent],NULL )  as DiscountPercent, PB.Name as ProductBrandName
from CustomerType CT 
left join Discount D on D.Discount_ID = CT.Discount_ID 
left join ProductBrand PB on CT.ProductBrand_ID = PB.ProductBrand_ID
where CT.Active<>-1  and PB.Active =1 " + whereProductBrand_ID + where + " order by CT.Sort asc");
            rptCustomerType.DataSource = dt;
            rptCustomerType.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerType_Add.aspx", false);
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
                CustomerTypeRow _CustomerTypeRow = new CustomerTypeRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _CustomerTypeRow = BusinessRulesLocator.GetCustomerTypeBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _CustomerTypeRow.Active = 1;

                        BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _CustomerTypeRow.Active = 0;
                        BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadCustomerType();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptCustomerType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int CustomerType_ID = Convert.ToInt32(e.CommandArgument);
        CustomerTypeRow _CustomerTypeRow = new CustomerTypeRow();
        _CustomerTypeRow = BusinessRulesLocator.GetCustomerTypeBO().GetByPrimaryKey(CustomerType_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteCustomerType(CustomerType_ID, ref Message))
                {
                    if (_CustomerTypeRow != null)
                    {
                        _CustomerTypeRow.Active = -1;
                    }
                    MyActionPermission.WriteLogSystem(CustomerType_ID, "Xóa - " + _CustomerTypeRow.Name);
                    BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
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
                if (_CustomerTypeRow != null)
                {
                    _CustomerTypeRow.Active = 1;
                }
                BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_CustomerTypeRow != null)
                {
                    _CustomerTypeRow.Active = 0;
                }
                BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadCustomerType();
    }

    protected void rptCustomerType_ItemDataBound(object sender, RepeaterItemEventArgs e)
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


    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomerType();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomerType();
        FillDDLddlDiscount();
    }
}