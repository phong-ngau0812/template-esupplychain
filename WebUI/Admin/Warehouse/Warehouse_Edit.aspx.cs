using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Warehouse_Edit : System.Web.UI.Page
{
    int Warehouse_ID = 0;
    public string title = "Thông tin kho";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(Request["Warehouse_ID"]))
            int.TryParse(Request["Warehouse_ID"].ToString(), out Warehouse_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();

            FillDLLZone();
            FillDDLArea();
            LoadDepartment();
            FillInfoWarehouse();
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
            DataTable dtZone = BusinessRulesLocator.GetZoneBO().GetAsDataTable("1=1 and Active =1" + where, "Name ASC");
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
    protected void FillInfoWarehouse()
    {
        try
        {
            if (Warehouse_ID != 0)
            {
                WarehouseRow _WarehouseRow = new WarehouseRow();
                _WarehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Warehouse_ID);

                if (_WarehouseRow != null)
                {
                    txtName.Text = _WarehouseRow.IsNameNull ? string.Empty : _WarehouseRow.Name;
                    txtCode.Text = _WarehouseRow.IsCodeNull ? string.Empty : _WarehouseRow.Code;
                    txtAddress.Text = _WarehouseRow.IsAddressNull ? string.Empty : _WarehouseRow.Address;
                    txtTelephone.Text = _WarehouseRow.IsTelephoneNull ? string.Empty : _WarehouseRow.Telephone;
                    ddlProductBrand.SelectedValue = _WarehouseRow.ProductBrand_ID.ToString();
                    ddlZone.SelectedValue = _WarehouseRow.IsZone_IDNull ? "0" : _WarehouseRow.Zone_ID.ToString();
                    ddlArea.SelectedValue = _WarehouseRow.IsArea_IDNull ? "0" : _WarehouseRow.Area_ID.ToString();
                    ddlDepartment.SelectedValue = _WarehouseRow.IsDepartment_IDNull ? "" : _WarehouseRow.Department_ID.ToString();

                    txtGLN.Text = _WarehouseRow.IsGLNNull ? string.Empty : _WarehouseRow.GLN;
                    if (_WarehouseRow.Type == 1)
                    {
                        rdovattu.Checked = true;

                    }
                    else if (_WarehouseRow.Type == 2)
                    {
                        rdosanpham.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateWarehouse()
    {
        try
        {
            WarehouseRow _WarehouseRow = new WarehouseRow();
            if (Warehouse_ID != 0)
            {
                _WarehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Warehouse_ID);
                if (_WarehouseRow != null)
                {
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
                    _WarehouseRow.Active = 1;
                    if (rdovattu.Checked)
                    {
                        _WarehouseRow.Type = 1;
                    }
                    else if (rdosanpham.Checked)
                    {
                        _WarehouseRow.Type = 2;
                    }
                    //if (_WarehouseRow.IsCreateDateNull)
                    //{
                    //    _WarehouseRow.CreateDate = DateTime.Now;
                    //}
                    //if (_WarehouseRow.IsCreateByNull)
                    //{
                    //    _WarehouseRow.CreateBy = MyUser.GetUser_ID();
                    //}
                    _WarehouseRow.LastEditBy = MyUser.GetUser_ID();
                    _WarehouseRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetWarehouseBO().Update(_WarehouseRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoWarehouse();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateWarehouse", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateWarehouse();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
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