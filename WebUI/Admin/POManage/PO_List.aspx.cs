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


public partial class Admin_POManage_PO_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            LoadProductBrand();
            FillDDLddlCustomer();
            loadPO();

        }
        ResetMsg();

    }
    private void LoadProductBrand()
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
            Common.FillProductBrand(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductBrand", ex.ToString());
        }
    }
    private void FillDDLddlCustomer()
    {


        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += "ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = BusinessRulesLocator.GetCustomerBO().GetAsDataTable("" + where, " CreateDate DESC");
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataValueField = "Customer_ID";
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("-- Khách hàng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void loadPO()
    {

        try
        {
            string where = "";

            if (ddlCustomer.SelectedValue != "0")
            {
                where += " and C.Customer_ID = " + ddlCustomer.SelectedValue;
            }
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and P.ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }


            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" Select P.PO_ID, P.Code,P.Description,P.Active, P.Content, U.UserName as Nguoitao , C.Name from POManage P
left join Customer C on P.Customer_ID = C.Customer_ID 
left join aspnet_Users U on P.CreateBy = U.UserId  where P.Active <>-1 " + where + "  order by P.CreateDate DESC");
            rptPO.DataSource = dt;
            rptPO.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    private void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("PO_Add.aspx", false);
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPO();

    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlCustomer();
        loadPO();

    }
    protected void rptPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

    protected void rptPO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int PO_ID = Convert.ToInt32(e.CommandArgument);
        POManageRow _PORow = new POManageRow();
        _PORow = BusinessRulesLocator.GetPOManageBO().GetByPrimaryKey(PO_ID);
        switch (e.CommandName)
        {
            case "Delete":
                BusinessRulesLocator.GetPOManageBO().DeleteByPrimaryKey(PO_ID);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
            case "Active":
                if (_PORow != null)
                {
                    _PORow.Active = true;
                }
                BusinessRulesLocator.GetPOManageBO().Update(_PORow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_PORow != null)
                {
                    _PORow.Active = false;
                }
                BusinessRulesLocator.GetPOManageBO().Update(_PORow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true;
        loadPO();
    }




}