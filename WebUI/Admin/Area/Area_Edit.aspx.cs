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

public partial class Area_Edit : System.Web.UI.Page
{
    int Area_ID = 0;
    public string title = "Thông tin khu sản xuất";
    public string avatar = "";
    public string googlemap = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!string.IsNullOrEmpty(Request["Area_ID"]))
            int.TryParse(Request["Area_ID"].ToString(), out Area_ID);
        if (!IsPostBack)
        {
            FillDllLocation();
            FillInfoArea();
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

    protected void FillInfoArea()
    {
        try
        {
            if (Area_ID != 0)
            {
                AreaRow _AreaRow = new AreaRow();
                _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);

                if (_AreaRow != null)
                {
                    FillDDLddlProductBrand();
                    txtName.Text = _AreaRow.IsNameNull ? string.Empty : _AreaRow.Name;
                    ddlProductBrand.SelectedValue = _AreaRow.IsProductBrand_IDNull ? "" : _AreaRow.ProductBrand_ID.ToString();
                    FillDDlddlWorkshop();
                    FillDDLddlZone();
                    if (_AreaRow.Zone_ID != 0)
                    {
                        ddlZone.SelectedValue = _AreaRow.Zone_ID.ToString();
                    }
                    else
                    {
                        ddlZone.SelectedValue = "";
                    }
                    if (!_AreaRow.IsLocation_IDNull)
                    {
                        ddlLocation.SelectedValue = _AreaRow.Location_ID.ToString();
                        DataTable dtDistrict = new DataTable();
                        dtDistrict = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
                        ddlDistrict.DataSource = dtDistrict;
                        ddlDistrict.DataTextField = "Name";
                        ddlDistrict.DataValueField = "District_ID";
                        ddlDistrict.DataBind();
                        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
                    }
                    if (!_AreaRow.IsDistrict_IDNull)
                    {
                        ddlDistrict.SelectedValue = _AreaRow.District_ID.ToString();

                        DataTable dtward = new DataTable();
                        dtward = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
                        ddlWard.DataSource = dtward;
                        ddlWard.DataTextField = "Name";
                        ddlWard.DataValueField = "Ward_ID";
                        ddlWard.DataBind();
                        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
                    }
                    if (!_AreaRow.IsWard_IDNull)
                    {
                        ddlWard.SelectedValue = _AreaRow.Ward_ID.ToString();
                    }
                    txtGLN.Text = _AreaRow.IsGLNNull ? string.Empty : _AreaRow.GLN.ToString();
                    txtAcreage.Text = _AreaRow.IsAcreageNull ? string.Empty : _AreaRow.Acreage.ToString();
                    txtAddress.Text = _AreaRow.IsAddressNull ? string.Empty : _AreaRow.Address.ToString();
                    txtNote.Text = _AreaRow.IsDescriptionNull ? string.Empty : _AreaRow.Description;
                    txtgooglemap.Text = _AreaRow.IsLinkGoogleMapNull ? string.Empty : _AreaRow.LinkGoogleMap;
                    googlemap = _AreaRow.IsLinkGoogleMapNull ? string.Empty : _AreaRow.LinkGoogleMap;
                    if (!_AreaRow.IsWorkshop_IDNull)
                    {
                        string[] array = _AreaRow.Workshop_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddlWorkshop.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoArea", ex.ToString());
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

    protected void UpdateArea()
    {
        try
        {
            AreaRow _AreaRow = new AreaRow();
            if (Area_ID != 0)
            {
                _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);
                if (_AreaRow != null)
                {
                    _AreaRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _AreaRow.ProductBrand_ID = string.IsNullOrEmpty(ddlZone.SelectedValue) ? 0 : Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _AreaRow.Zone_ID = string.IsNullOrEmpty(ddlZone.SelectedValue) ? 0 : Convert.ToInt32(ddlZone.SelectedValue);
                    _AreaRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _AreaRow.LinkGoogleMap = string.IsNullOrEmpty(txtgooglemap.Text) ? string.Empty : txtgooglemap.Text;
                    _AreaRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    _AreaRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    _AreaRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
                    if (!string.IsNullOrEmpty(txtAcreage.Text))
                    {
                        _AreaRow.Acreage = Convert.ToInt32(txtAcreage.Text);
                    }
                    else
                    {
                        _AreaRow.Acreage = 0;
                    }
                    if (!string.IsNullOrEmpty(txtAddress.Text))
                    {
                        _AreaRow.Address = txtAddress.Text;
                    }


                    //if (!string.IsNullOrEmpty(ADDListWorkshop_ID()))
                    //{

                    _AreaRow.Workshop_ID = ADDListWorkshop_ID();
                    _AreaRow.LastEditBy = MyUser.GetUser_ID();
                    _AreaRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetAreaBO().Update(_AreaRow);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FillInfoArea();

                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Bạn chưa chọn nhân viên nào! ";
                    //    lblMessage.ForeColor = Color.Red;
                    //    lblMessage.BackColor = Color.Wheat;
                    //    lblMessage.Visible = true;
                    //}

                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateZone_ID", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateArea();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
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