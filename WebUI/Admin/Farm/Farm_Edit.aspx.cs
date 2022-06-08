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

public partial class Farm_Edit : System.Web.UI.Page
{
    int Farm_ID = 0;
    public string title = "Thông tin thửa/ruộng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(Request["Farm_ID"]))
            int.TryParse(Request["Farm_ID"].ToString(), out Farm_ID);
        if (!IsPostBack)
        {
            FillDDLLocation();
            FillInfoFarm();
            FillDDLddlProductBrand();
            FillDDLddlddlZone();
            FillDDLddlArea();
            FillDDLddlWorkshop();
            FillDDLddlFramStatus();

        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
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

    private void FillDDLddlddlZone()
    {
        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select Z.* from Zone Z inner join ProductBrand PB on Z.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and Z.Active<>-1  " + where);
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


    private void FillDDLddlArea()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and A.Zone_ID = " + ddlZone.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select A.* from Area A inner join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 " + where);
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu sản xuất --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLLocation()
    {
        DataTable dtLocation = new DataTable();
        dtLocation = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", " Name ASC");
        ddlLocation.DataSource = dtLocation;
        ddlLocation.DataTextField = "Name";
        ddlLocation.DataValueField = "Location_ID";
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, new ListItem("-- Chọn tỉnh/ thành phố --", ""));
        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/ huyện --", ""));
        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/ xã --", ""));
    }
    private void FillDDLddlWorkshop()
    {
        try
        {
            string where = string.Empty;
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                if (ddlZone.SelectedValue != "")
                {
                    where += " and Zone_ID =" + ddlZone.SelectedValue;
                }
                if (ddlArea.SelectedValue != "")
                {
                    where += " and Area_ID =" + ddlArea.SelectedValue;
                }
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Workshop_ID from Workshop where (Active<>-1 or Active is null) " + where + " order by Name ASC");

            }
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên, hộ sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLddlFramStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFarmStatusBO().GetAsDataTable("", "");
            ddlFarmStatus.DataSource = dt;
            ddlFarmStatus.DataTextField = "Name";
            ddlFarmStatus.DataValueField = "FarmStatus_ID";
            ddlFarmStatus.DataBind();
            ddlFarmStatus.Items.Insert(0, new ListItem("-- Trạng thái thửa/lô --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillInfoFarm()
    {
        try
        {
            if (Farm_ID != 0)
            {
                FarmRow _FarmRow = new FarmRow();
                _FarmRow = BusinessRulesLocator.GetFarmBO().GetByPrimaryKey(Farm_ID);

                if (_FarmRow != null)
                {

                    ddlProductBrand.SelectedValue = _FarmRow.IsProductBrand_IDNull ? string.Empty : _FarmRow.ProductBrand_ID.ToString();
                    if (_FarmRow.Zone_ID != 0)
                    {
                        ddlZone.SelectedValue = _FarmRow.Zone_ID.ToString();
                    }
                    if (_FarmRow.Area_ID != 0)
                    {
                        ddlArea.SelectedValue = _FarmRow.Area_ID.ToString(); ;
                    }
                    ddlWorkshop.SelectedValue = _FarmRow.IsWorkshop_IDNull ? "" : _FarmRow.Workshop_ID.ToString();
                    ddlFarmStatus.SelectedValue = _FarmRow.IsFarmStatus_IDNull ? "" : _FarmRow.FarmStatus_ID.ToString();
                    txtName.Text = _FarmRow.IsNameNull ? string.Empty : _FarmRow.Name;
                    txtAcreage.Text = _FarmRow.IsAcreageNull ? "0" : _FarmRow.Acreage.ToString();
                    txtAddress.Text = _FarmRow.IsAddressNull ? string.Empty : _FarmRow.Address;
                    //txtEmail.Text = _FarmRow.IsEmailNull ? string.Empty : _FarmRow.Email;
                    //txtPhone.Text = _FarmRow.IsPhoneNull ? string.Empty : _FarmRow.Phone;
                    txtHistoryFarm.Text = _FarmRow.IsHistoryFarmNull ? string.Empty : _FarmRow.HistoryFarm;
                    txtNote.Text = _FarmRow.IsDescriptionNull ? string.Empty : _FarmRow.Description;
                    txtGLN.Text = _FarmRow.IsGLNNull ? string.Empty : _FarmRow.GLN;
                    if (!_FarmRow.IsLocation_IDNull)
                    {
                        ddlLocation.SelectedValue = _FarmRow.Location_ID.ToString();
                        DataTable dtDistrict = new DataTable();
                        dtDistrict = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
                        ddlDistrict.DataSource = dtDistrict;
                        ddlDistrict.DataTextField = "Name";
                        ddlDistrict.DataValueField = "District_ID";
                        ddlDistrict.DataBind();
                        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
                    }
                    if (!_FarmRow.IsDistrict_IDNull)
                    {
                        ddlDistrict.SelectedValue = _FarmRow.District_ID.ToString();

                        DataTable dtward = new DataTable();
                        dtward = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
                        ddlWard.DataSource = dtward;
                        ddlWard.DataTextField = "Name";
                        ddlWard.DataValueField = "Ward_ID";
                        ddlWard.DataBind();
                        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
                    }
                    if (!_FarmRow.IsWard_IDNull)
                    {
                        ddlWard.SelectedValue = _FarmRow.Ward_ID.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoFarm", ex.ToString());
        }
    }


    protected void UpdateFarm()
    {
        try
        {
            FarmRow _FarmRow = new FarmRow();
            if (Farm_ID != 0)
            {
                _FarmRow = BusinessRulesLocator.GetFarmBO().GetByPrimaryKey(Farm_ID);
                if (_FarmRow != null)
                {
                    _FarmRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);

                    if (!string.IsNullOrEmpty(ddlZone.SelectedValue))
                    {
                        _FarmRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
                    }
                    else
                    {
                        _FarmRow.Zone_ID = 0;
                    }

                    if (!string.IsNullOrEmpty(ddlArea.SelectedValue))
                    {
                        _FarmRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
                    }
                    else
                    {
                        _FarmRow.Area_ID = 0;
                    }

                    if (ddlWorkshop.SelectedValue != "")
                    {
                        _FarmRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(ddlFarmStatus.SelectedValue))
                    {
                        _FarmRow.FarmStatus_ID = Convert.ToInt32(ddlFarmStatus.SelectedValue);
                    }
                    else
                    {
                        _FarmRow.FarmStatus_ID = 1;
                    }
                    _FarmRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _FarmRow.Acreage = string.IsNullOrEmpty(txtAcreage.Text) ? 0 : Convert.ToInt32(txtAcreage.Text);
                    _FarmRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    //_FarmRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
                    //_FarmRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _FarmRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _FarmRow.HistoryFarm = string.IsNullOrEmpty(txtHistoryFarm.Text) ? string.Empty : txtHistoryFarm.Text;
                    _FarmRow.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    _FarmRow.District_ID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    _FarmRow.Ward_ID = Convert.ToInt32(ddlWard.SelectedValue);
                    _FarmRow.LastEditBy = MyUser.GetUser_ID();
                    _FarmRow.LastEditDate = DateTime.Now;

                }
                BusinessRulesLocator.GetFarmBO().Update(_FarmRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoFarm();
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
                UpdateFarm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Farm_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlddlZone();
        FillDDLddlArea();
        FillDDLddlWorkshop();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlArea();
        FillDDLddlWorkshop();

    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDLddlWorkshop();
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrict.Items.Clear();
        if (ddlLocation.SelectedValue != "0")
        {
            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetDistrictBO().GetAsDataTable(" Location_ID=" + ddlLocation.SelectedValue, " Name ASC");
            ddlDistrict.DataSource = dtLocation;
            ddlDistrict.DataTextField = "Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        ddlDistrict.Items.Insert(0, new ListItem("-- Chọn quận/huyện --", ""));
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWard.Items.Clear();
        if (ddlDistrict.SelectedValue != "0")
        {
            DataTable dtLocation = new DataTable();
            dtLocation = BusinessRulesLocator.GetWardBO().GetAsDataTable(" District_ID=" + ddlDistrict.SelectedValue, " Name ASC");
            ddlWard.DataSource = dtLocation;
            ddlWard.DataTextField = "Name";
            ddlWard.DataValueField = "Ward_ID";
            ddlWard.DataBind();
        }
        ddlWard.Items.Insert(0, new ListItem("-- Chọn phường/xã --", ""));
    }
}