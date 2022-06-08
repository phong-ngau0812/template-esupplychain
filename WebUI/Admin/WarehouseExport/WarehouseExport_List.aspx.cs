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

public partial class WarehouseExport_List : System.Web.UI.Page
{
    int Zone_ID;
    int Area_ID;
    int ProductPackage_ID;
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            
            FillDDlddlProductPackageOrder();
            LoadZone();
            Common.CheckAccountTypeZone(ddlZone);
            LoadArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            FillDllWarehouse();
            LoadWarehouseExport();
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


    private void FillDDlddlProductPackageOrder()
    {
        string where = "";

        try
        {
            ddlProductPackageOrder.Items.Clear();
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;

                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select ProductPackageOrder_ID, Name from ProductPackageOrder where Active=1 and Approve=1" + where); ;
                ddlProductPackageOrder.DataSource = dt;
                ddlProductPackageOrder.DataTextField = "Name";
                ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
                ddlProductPackageOrder.DataBind();
            }
            else
            {
                ddlProductPackageOrder.DataSource = null;
            }
            ddlProductPackageOrder.Items.Insert(0, new ListItem("-- Chọn lệnh --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void LoadZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Zone_ID, Name from Zone where Active=1" + where);
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Area_ID, Name from Area where 1=1" + where);
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }

    protected void FillDllWarehouse()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Warehouse_ID, Name from Warehouse where Active=1" + where);
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }

    private void LoadWarehouseExport()
    {
        try
        {
            string userId = string.Empty;
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetAccountType_ID() == "7")
                {
                    userId = MyUser.GetUser_ID().ToString();
                }

            }
            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().spGetWarehouseExport_paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue),  Convert.ToInt32(ddlProductPackageOrder.SelectedValue), txtName.Text.Trim(), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlWarehouse.SelectedValue) ,ctlDatePicker1.FromDate, ctlDatePicker1.ToDate , "CreateDate DESC", userId);
            //dtSet = BusinessRulesLocator.Conllection().GetWorkshop_paging(1, 1000, 7, 0, 0, "", "");
            rptWarehouseExport.DataSource = dtSet.Tables[0];
            rptWarehouseExport.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadFarm", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseExport_Add.aspx", false);
    }

    protected void rptWarehouseExport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int WarehouseExport_ID = Convert.ToInt32(e.CommandArgument);
        WarehouseExportRow _WarehouseExportRow = new WarehouseExportRow();
        _WarehouseExportRow = BusinessRulesLocator.GetWarehouseExportBO().GetByPrimaryKey(WarehouseExport_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_WarehouseExportRow != null)
                {
                    _WarehouseExportRow.Active = -1;
                }
                BusinessRulesLocator.GetWarehouseExportBO().Update(_WarehouseExportRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadWarehouseExport();
    }

    protected void rptWarehouseExport_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
        FillDDlddlProductPackageOrder();
        LoadZone();
        LoadArea();
        FillDllWarehouse();
    }

    protected void btnAddSP_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseExportProduct_Add.aspx", false);
    }

    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadWarehouseExport();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseExport();
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseExport();
    }
    //protected void ImportExcel_Click(object sender, EventArgs e)
    //{

    //    if (ddlProductBrand.SelectedValue != "0")
    //    {
    //        Response.Redirect("WarehouseExport_Download.aspx?ProductPackage_ID=" + Convert.ToInt32(ddlProductPackage.SelectedValue) + "&ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue) + "&Warehouse_ID="+Convert.ToInt32(ddlWarehouse.SelectedValue), false);
    //    }

    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
    }

    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadWarehouseExport();
    }

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadWarehouseExport();
        }
   
    }

    protected void ddlProductPackageOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
    }

    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseExport();
    }
}