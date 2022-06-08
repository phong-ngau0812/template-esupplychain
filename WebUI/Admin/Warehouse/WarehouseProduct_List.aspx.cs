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

public partial class Admin_Warehouse_WarehouseProduct_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadZone();
            Common.CheckAccountTypeZone(ddlZone);
            LoadArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            LoadWarehouse();
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
    protected void LoadZone()
    {
        try
        {
            string where = " Active=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(where, " Name ASC");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));


            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    ddlZone.SelectedValue = MyUser.GetZone_ID();
                    if (MyUser.GetAccountType_ID() == "7")
                    {
                        ddlZone.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            string where = " 1=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAreaBO().GetAsDataTable(where, " Name ASC");
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                {
                    if (MyUser.GetArea_ID() != "0")
                    {
                        ddlArea.SelectedValue = MyUser.GetArea_ID();
                        ddlArea.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }

    protected void LoadWarehouse()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            //if (Convert.ToInt32(ddlWarehouse.SelectedValue) != 0)
            //{
            //    where += " and W.Type =" + ddlWarehouse.SelectedValue;
            //}
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetAccountType_ID() == "7")
                {
                    where += " and W.CreateBy ='" + MyUser.GetUser_ID() + "'";
                }

            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and W.Zone_ID = " + ddlZone.SelectedValue;
            }
            if (ddlArea.SelectedValue != "0")
            {
                where += " and W.Area_ID = " + ddlArea.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select W.* from Warehouse W inner join ProductBrand PB on W.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and W.Active =1 and W.Type = 2 " + where + " ORDER BY Name ASC");
            rptWarehouse.DataSource = dt;
            rptWarehouse.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Warehouse_Add.aspx", false);
    }


    protected void rptWarehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Warehouse_ID = Convert.ToInt32(e.CommandArgument);
        WarehouseRow _WarehouseRow = new WarehouseRow();
        _WarehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Warehouse_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_WarehouseRow != null)
                {
                    _WarehouseRow.Active = -1;
                }
                BusinessRulesLocator.GetWarehouseBO().Update(_WarehouseRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
                //case "Active":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 1;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Kích hoạt thành công !");
                //    break;
                //case "Deactive":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 0;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;

        }
        lblMessage.Visible = true; ;
        LoadWarehouse();
    }

    protected void rptWarehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadWarehouse();
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
}