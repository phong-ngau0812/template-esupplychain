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
using System.Xml.Linq;
using SystemFrameWork;

public partial class Area_Add : System.Web.UI.Page
{
    public string title = "Thêm mới khu sản xuất  ";
    public string googlemap = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillDDlddlWorkshop();
            FillDllLocation();

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
    private void FillDDLddlZone()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(" Active<>-1" + where, "");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddArea();

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


    protected void UpdateArea(int Area_ID)
    {
        try
        {
            AreaRow _AreaRow = new AreaRow();
            if (Area_ID != 0)
            {
                _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);
                if (_AreaRow != null)
                {
                    _AreaRow.GLN = "GLN-" + _AreaRow.Area_ID.ToString();

                    BusinessRulesLocator.GetAreaBO().Update(_AreaRow);
                }
                lblMessage.Text = "Thêm mới khu vực thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }


    protected void AddArea()
    {

        try
        {
            //if (CheckStaffTypeName(txtName.Text.Trim()))
            //{
            AreaRow _AreaRow = new AreaRow();
            _AreaRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _AreaRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _AreaRow.Zone_ID = string.IsNullOrEmpty(ddlZone.SelectedValue) ? 0 : Convert.ToInt32(ddlZone.SelectedValue);
            _AreaRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _AreaRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
            _AreaRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
            _AreaRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
            if (!string.IsNullOrEmpty(txtAcreage.Text))
            {
                _AreaRow.Acreage = Convert.ToInt32(txtAcreage.Text);
            }
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                _AreaRow.Address = txtAddress.Text;
            }
            _AreaRow.LinkGoogleMap = string.IsNullOrEmpty(txtgooglemap.Text) ? string.Empty : txtgooglemap.Text;
            //if (!string.IsNullOrEmpty(ADDListWorkshop_ID()))
            //{
            _AreaRow.Workshop_ID = ADDListWorkshop_ID();
            _AreaRow.CreateBy = MyUser.GetUser_ID();
            _AreaRow.CreateDate = DateTime.Now;

            _AreaRow.LastEditBy = MyUser.GetUser_ID();
            _AreaRow.LastEditDate = DateTime.Now;

            BusinessRulesLocator.GetAreaBO().Insert(_AreaRow);
            //lblMessage.Text = "Thêm mới thành công!";
            //lblMessage.Visible = true;
            if (!_AreaRow.IsArea_IDNull)
            {
                UpdateArea(_AreaRow.Area_ID);
                Response.Redirect("Area_List.aspx", false);
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
            Log.writeLog("UpdateArea", ex.ToString());
        }
    }

    protected void ClearForm()
    {
        txtName.Text = "";
        //if (MyUser.GetFunctionGroup_ID() == "1")
        //{
        //    ddlProductBrand.SelectedValue = "";
        //}
        txtNote.Text = "";
        ddlZone.SelectedValue = "";
        txtAcreage.Text = "";
        txtgooglemap.Text = "";
        ddlWorkshop.Items.Clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Area_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlZone();
        FillDDlddlWorkshop();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
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