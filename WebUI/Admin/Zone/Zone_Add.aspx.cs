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
using Telerik.Web.UI;

public partial class Zone_Add : System.Web.UI.Page
{
    public string title = "Thêm mới vùng sản xuất  ";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDllLocation();
            FillDDLddlProductBrand();
            FillDDlddlWorkshop();
        }
    }


    private void FillDllLocation()
    {
        DataTable dtLocation = new DataTable();
        dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", "Name ASC");
        ddlLocation.DataSource = dtLocation;
        ddlLocation.DataTextField = "Name";
        ddlLocation.DataValueField = "Location_ID";
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, new ListItem("-- Chọn tỉnh/ thành phố --", ""));
        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/ huyện --", ""));
        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/ xã --", ""));
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddZone();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckStaffTypeName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }


    private void FillDDlddlWorkshop()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable(where, "");
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    private string ADDListWorkshop_ID()
    {
        string Workshop_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlWorkshop.Items)
            {
                if (item.Selected)
                {
                    Workshop_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Workshop_ID))
            {
                Workshop_ID = "," + Workshop_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("Customer_ID", ex.ToString());
        }
        return Workshop_ID;
    }


    protected void UpdateZone(int Zone_ID)
    {
        try
        {
            ZoneRow _ZoneRow = new ZoneRow();
            if (Zone_ID != 0)
            {
                _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);
                if (_ZoneRow != null)
                {
                    _ZoneRow.GLN = "GLN-" + _ZoneRow.Zone_ID.ToString();

                    BusinessRulesLocator.GetZoneBO().Update(_ZoneRow);
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

    protected void AddZone()
    {
        try
        {
            //if (CheckStaffTypeName(txtName.Text.Trim()))
            //{
            ZoneRow _ZoneRow = new ZoneRow();
            _ZoneRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _ZoneRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _ZoneRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _ZoneRow.LinkGoogleMap = string.IsNullOrEmpty(txtgooglemap.Text) ? string.Empty : txtgooglemap.Text;
            _ZoneRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
            _ZoneRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
            _ZoneRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
            if (!string.IsNullOrEmpty(txtAcreage.Text))
            {
                _ZoneRow.Acreage = Convert.ToInt32(txtAcreage.Text);
            }
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                _ZoneRow.Address = txtAddress.Text;
            }

            //if (!string.IsNullOrEmpty(ADDListWorkshop_ID()))
            //{
            _ZoneRow.Workshop_ID = ADDListWorkshop_ID();
            _ZoneRow.Active = 1;
            _ZoneRow.LastEditBy = _ZoneRow.CreateBy = MyUser.GetUser_ID();
            _ZoneRow.LastEditDate = _ZoneRow.CreateDate = DateTime.Now;

            BusinessRulesLocator.GetZoneBO().Insert(_ZoneRow);



            if (!_ZoneRow.IsZone_IDNull)
            {
                UpdateZone(_ZoneRow.Zone_ID);
                Response.Redirect("Zone_List.aspx", false);

            }

            //}

            //else
            //{
            //    lblMessage.Text = "Bạn chưa chọn nhân viên nào! ";
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.Wheat;
            //    lblMessage.Visible = true;
            //}


        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateZone", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        //ddlProductBrand.SelectedValue = "";
        txtNote.Text = "";
        txtAcreage.Text = "";
        txtgooglemap.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Zone_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDlddlWorkshop();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrict.Items.Clear();
        if (ddlLocation.SelectedValue != "0")
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDistrictBO().GetAsDataTable("Location_ID =" + ddlLocation.SelectedValue, "Name ASC");
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/ huyện --", ""));
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWard.Items.Clear();
        if (ddlDistrict.SelectedValue != "0")
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWardBO().GetAsDataTable("District_ID =" + ddlDistrict.SelectedValue, "Name ASC");
            ddlWard.DataSource = dt;
            ddlWard.DataTextField = "Name";
            ddlWard.DataValueField = "Ward_ID";
            ddlWard.DataBind();
        }
        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/ xã --", ""));
    }
}