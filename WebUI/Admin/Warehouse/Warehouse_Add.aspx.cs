using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Warehouse_Add : System.Web.UI.Page
{
    public string title = "Thêm mới kho ";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDLLZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillDDLArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            LoadDepartment();
        }
    }



    private void FillDDLddlProductBrand()
    {
        try
        {

            Common.FillProductBrand_Null(ddlProductBrand, "");
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
    protected void LoadDepartment()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }
            DataTable dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active = 1 " + where, "");
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataValueField = "Department_ID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new ListItem("-- Phòng Ban --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadDepartment", ex.ToString());
        }
    }
    protected void FillDLLZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            //if (Workshop_ID != 0)
            //{
            //    where += " and WorkShop_ID like '%," + Workshop_ID + ",%'";
            //}
            DataTable dtZone = BusinessRulesLocator.GetZoneBO().GetAsDataTable("1=1 and Active=1" + where, "Name ASC");
            ddlZone.DataSource = dtZone;
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataTextField = "Name";

            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDLLZone", ex.ToString());
        }
    }


    protected void FillDDLArea()
    {
        string where = "";
        try
        {
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + Convert.ToInt32(ddlZone.SelectedValue);
            }
            DataTable dtArea = BusinessRulesLocator.GetAreaBO().GetAsDataTable("1=1" + where, "Name ASC");
            ddlArea.DataSource = dtArea;
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataTextField = "Name";

            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLArea", ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddWarehouse();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckStaffTypeName(string Code)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Code))
        {
            where += " and Name=N'" + Code.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }

    protected void UpdateWarehouse(int Warehouse_ID)
    {
        try
        {
            WarehouseRow _warehouseRow = new WarehouseRow();
            if (Warehouse_ID != 0)
            {
                _warehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Warehouse_ID);
                if (_warehouseRow != null)
                {
                    _warehouseRow.GLN = "GLN-" + _warehouseRow.Warehouse_ID.ToString();

                    BusinessRulesLocator.GetWarehouseBO().Update(_warehouseRow);
                }
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }
    protected void AddWarehouse()
    {
        try
        {
            //if (CheckStaffTypeName(txtCode.Text.Trim()))
            //{
            WarehouseRow _WarehouseRow = new WarehouseRow();
            _WarehouseRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _WarehouseRow.Code = string.IsNullOrEmpty(txtCode.Text) ? string.Empty : txtCode.Text;
            _WarehouseRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _WarehouseRow.Telephone = string.IsNullOrEmpty(txtTelephone.Text) ? string.Empty : txtTelephone.Text;
            _WarehouseRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            if (ddlDepartment.SelectedValue != "")
            {
                _WarehouseRow.Department_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            if (ddlZone.SelectedValue != "")
            {
                _WarehouseRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
            }
            if (ddlArea.SelectedValue != "")
            {
                _WarehouseRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
            }
            if (rdovattu.Checked)
            {
                _WarehouseRow.Type = 1;
            }
            else if (rdosanpham.Checked)
            {
                _WarehouseRow.Type = 2;
            }
            _WarehouseRow.Active = 1;
            _WarehouseRow.CreateBy = MyUser.GetUser_ID();
            _WarehouseRow.CreateDate = DateTime.Now;

            _WarehouseRow.LastEditBy = MyUser.GetUser_ID();
            _WarehouseRow.LastEditDate = DateTime.Now;
            BusinessRulesLocator.GetWarehouseBO().Insert(_WarehouseRow);
            if (_WarehouseRow != null)
            {
                UpdateWarehouse(_WarehouseRow.Warehouse_ID);
                Response.Redirect("Warehouse_List.aspx", false);
            }
            //}
            //else
            //{
            //    lblMessage.Text = "Đã tồn tại mã kho " + txtCode.Text.Trim();
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.PaleTurquoise;
            //    lblMessage.Visible = true;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaffType", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";

        txtCode.Text = "";
        txtAddress.Text = "";
        txtTelephone.Text = "";

    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDLLZone();

    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLArea();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Warehouse_List.aspx", false);
    }
}