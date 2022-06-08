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

public partial class Zone_Edit : System.Web.UI.Page
{
    int Zone_ID = 0;
    public string title = "Thông tin vùng sản xuất";
    public string avatar = "";
    public string googlemap = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["Zone_ID"]))
            int.TryParse(Request["Zone_ID"].ToString(), out Zone_ID);
        if (!IsPostBack)
        {
            FillDllLocation();
            FillInfoZone();

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
    protected void FillInfoZone()
    {
        try
        {
            if (Zone_ID != 0)
            {
                ZoneRow _ZoneRow = new ZoneRow();
                _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);

                if (_ZoneRow != null)
                {
                    FillDDLddlProductBrand();
                    txtName.Text = _ZoneRow.IsNameNull ? string.Empty : _ZoneRow.Name;
                    ddlProductBrand.SelectedValue = _ZoneRow.ProductBrand_ID.ToString();
                    FillDDlddlWorkshop();
                    txtAcreage.Text = _ZoneRow.IsAcreageNull ? string.Empty : _ZoneRow.Acreage.ToString();
                    txtAddress.Text = _ZoneRow.IsAddressNull ? string.Empty : _ZoneRow.Address.ToString();
                    txtNote.Text = _ZoneRow.IsDescriptionNull ? string.Empty : _ZoneRow.Description;
                    txtGLN.Text = _ZoneRow.IsGLNNull ? string.Empty : _ZoneRow.GLN;
                    txtgooglemap.Text = _ZoneRow.IsLinkGoogleMapNull ? string.Empty : _ZoneRow.LinkGoogleMap;
                    googlemap = _ZoneRow.IsLinkGoogleMapNull ? string.Empty : _ZoneRow.LinkGoogleMap;
                    if (!_ZoneRow.IsLocation_IDNull)
                    {
                        ddlLocation.SelectedValue = _ZoneRow.Location_ID.ToString();
                        DataTable dtDistrict = new DataTable();
                        dtDistrict = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
                        ddlDistrict.DataSource = dtDistrict;
                        ddlDistrict.DataTextField = "Name";
                        ddlDistrict.DataValueField = "District_ID";
                        ddlDistrict.DataBind();
                        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
                    }
                    if (!_ZoneRow.IsDistrict_IDNull)
                    {
                        ddlDistrict.SelectedValue = _ZoneRow.District_ID.ToString();

                        DataTable dtward = new DataTable();
                        dtward = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
                        ddlWard.DataSource = dtward;
                        ddlWard.DataTextField = "Name";
                        ddlWard.DataValueField = "Ward_ID";
                        ddlWard.DataBind();
                        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
                    }
                    if (!_ZoneRow.IsWard_IDNull)
                    {
                        ddlWard.SelectedValue = _ZoneRow.Ward_ID.ToString();
                    }
                    if (!_ZoneRow.IsWorkshop_IDNull)

                    {
                        string[] array = _ZoneRow.Workshop_ID.Split(',');
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
            Log.writeLog("FillInfoUser", ex.ToString());
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

    protected void UpdateZone()
    {
        try
        {
            ZoneRow _ZoneRow = new ZoneRow();
            if (Zone_ID != 0)
            {
                _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);
                if (_ZoneRow != null)
                {
                    _ZoneRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _ZoneRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _ZoneRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _ZoneRow.LinkGoogleMap = string.IsNullOrEmpty(txtgooglemap.Text) ? string.Empty : txtgooglemap.Text;
                    if (!string.IsNullOrEmpty(txtAcreage.Text))
                    {
                        _ZoneRow.Acreage = Convert.ToInt32(txtAcreage.Text);
                    }
                    else
                    {
                        _ZoneRow.Acreage = 0;
                    }
                    if (!string.IsNullOrEmpty(txtAddress.Text))
                    {
                        _ZoneRow.Address = txtAddress.Text;
                    }
                    _ZoneRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    _ZoneRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    _ZoneRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);




                    //if (!string.IsNullOrEmpty(ADDListWorkshop_ID()))
                    //{

                    _ZoneRow.LastEditBy = MyUser.GetUser_ID();
                    _ZoneRow.LastEditDate = DateTime.Now;
                    _ZoneRow.Workshop_ID = ADDListWorkshop_ID();
                    BusinessRulesLocator.GetZoneBO().Update(_ZoneRow);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FillInfoZone();

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
                UpdateZone();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
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