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
using Telerik.Web.UI;

public partial class Workshop_Edit : System.Web.UI.Page
{
    int Workshop_ID = 0;
    public string title = "Thông tin nhân viên, hộ sản xuất";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Workshop_ID"]))
            int.TryParse(Request["Workshop_ID"].ToString(), out Workshop_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();

            FillDDLDepartment();
            FillDLLZone();
            FillDDLArea();

            FillInfoWorkshop();

        }
    }

    private void FillDDLDepartment()
    {

        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active=1 " + where, " Sort, Name ASC");
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataValueField = "Department_ID";
            ddlDepartment.DataBind();
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetDepartment_ID() != "")
                {
                    ddlDepartment.SelectedValue = MyUser.GetDepartment_ID();
                    ddlDepartment.Enabled = false;
                }
            }

            ddlDepartment.Items.Insert(0, new ListItem("-- Chọn phòng ban --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
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
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void FillInfoWorkshop()
    {
        try
        {
            if (Workshop_ID != 0)
            {
                WorkshopRow _WorkshopRow = new WorkshopRow();
                _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(Workshop_ID);
                if (_WorkshopRow != null)
                {
                    txtName.Text = _WorkshopRow.IsNameNull ? string.Empty : _WorkshopRow.Name;
                    ddlProductBrand.SelectedValue = _WorkshopRow.IsProductBrand_IDNull ? "" : _WorkshopRow.ProductBrand_ID.ToString();
                    FillDDLDepartment();
                    FillDLLZone();
                    FillDDLArea();
                    ddlDepartment.SelectedValue = _WorkshopRow.IsDepartment_IDNull ? "0" : _WorkshopRow.Department_ID.ToString();
                    ddlZone.SelectedValue = _WorkshopRow.IsZone_IDNull ? "0" : _WorkshopRow.Zone_ID.ToString();
                    ddlArea.SelectedValue = _WorkshopRow.IsArea_IDNull ? "0" : _WorkshopRow.Area_ID.ToString();

                    txtDiaChi.Text = _WorkshopRow.IsAddressNull ? string.Empty : _WorkshopRow.Address;
                    txtPhone.Text = _WorkshopRow.IsPhoneNull ? string.Empty : _WorkshopRow.Phone;
                    txtEmail.Text = _WorkshopRow.IsEmailNull ? string.Empty : _WorkshopRow.Email;
                    txtNote.Text = _WorkshopRow.IsDescriptionNull ? string.Empty : _WorkshopRow.Description;

                    ddlGioiTinh.SelectedValue = _WorkshopRow.IsDepartment_IDNull ? "0" : _WorkshopRow.Gender.ToString();
                    if (!_WorkshopRow.IsBirthDateNull)
                    {
                        txtBirth.Text = _WorkshopRow.BirthDate.ToString("dd/MM/yyyy");
                    }

                    if (!_WorkshopRow.IsTypeNull)
                    {
                        if (_WorkshopRow.Type == 1)
                        {
                            rdonhanvien.Checked = true;
                        }
                        if (_WorkshopRow.Type == 2)
                        {
                            rdohsx.Checked = true;
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
    protected void FillDLLZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            DataTable dtZone = BusinessRulesLocator.GetZoneBO().GetAsDataTable("1=1 and Active = 1" + where, "Name ASC");
            ddlZone.DataSource = dtZone;
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataTextField = "Name";
            ddlZone.DataBind();

            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetZone_ID() != "")
                {
                    ddlZone.SelectedValue = MyUser.GetZone_ID();
                    ddlZone.Enabled = false;
                }
            }
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", ""));
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", ""));
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
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + Convert.ToInt32(ddlZone.SelectedValue);
            }
            DataTable dtArea = BusinessRulesLocator.GetAreaBO().GetAsDataTable("1=1" + where, "Name ASC");
            ddlArea.DataSource = dtArea;
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataTextField = "Name";
            ddlArea.DataBind();
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetArea_ID() != "")
                {
                    ddlArea.SelectedValue = MyUser.GetArea_ID();
                    ddlArea.Enabled = false;
                }
            }

            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLArea", ex.ToString());
        }
    }

    protected void UpdateBranch()
    {
        try
        {

            WorkshopRow _WorkshopRow = new WorkshopRow();
            if (Workshop_ID != 0)
            {
                _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(Workshop_ID);
                if (_WorkshopRow != null)
                {
                    _WorkshopRow.Name = txtName.Text;
                    _WorkshopRow.Description = txtNote.Text;



                    if (ddlProductBrand.SelectedValue != "")
                    {
                        _WorkshopRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    }
                    if (ddlDepartment.SelectedValue != "")
                    {
                        _WorkshopRow.Department_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    }
                    if (ddlZone.SelectedValue != "")
                    {
                        _WorkshopRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
                    }
                    if (ddlArea.SelectedValue != "")
                    {
                        _WorkshopRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
                    }

                    _WorkshopRow.Address = txtDiaChi.Text;
                    _WorkshopRow.Phone = txtPhone.Text;
                    _WorkshopRow.Email = txtEmail.Text;

                    if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
                    {
                        DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _WorkshopRow.BirthDate = birth;
                    }
                    _WorkshopRow.Gender = Convert.ToInt32(ddlGioiTinh.SelectedIndex);

                    if (rdonhanvien.Checked)
                    {
                        _WorkshopRow.Type = 1;
                    }
                    else if (rdohsx.Checked)
                    {
                        _WorkshopRow.Type = 2;
                    }

                    _WorkshopRow.LastEditDate = DateTime.Now;
                    _WorkshopRow.LastEditBy = MyUser.GetUser_ID();
                    BusinessRulesLocator.GetWorkshopBO().Update(_WorkshopRow);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FillInfoWorkshop();
                    Response.Redirect("Workshop_List.aspx", false);
                }

            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateBranch", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateBranch();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLDepartment();
        FillDLLZone();

    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLArea();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workshop_List.aspx", false);
    }
}