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

public partial class Workshop_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhân viên, hộ sản xuất";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLDepartment();
            FillDLLZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillDDLArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
        }
    }



    private void FillDDLddlProductBrand()
    {
        try
        {
            string where = "";
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }

            //DataTable dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable("1=1"+where, "");
            //ddlProductBrand.DataSource = dt;
            //ddlProductBrand.DataValueField = "ProductBrand_ID";
            //ddlProductBrand.DataTextField = "Name";
            //ddlProductBrand.DataBind();
            //ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));


            Common.FillProductBrand_Null(ddlProductBrand, where);
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

            if (MyUser.GetDepartment_ID() != "")
            {
                ddlDepartment.SelectedValue = MyUser.GetDepartment_ID();
            }
            ddlDepartment.Items.Insert(0, new ListItem("-- Chọn phòng ban --", ""));
            if (MyUser.GetDepartment_ID() != "")
            {
                ddlDepartment.SelectedValue = MyUser.GetDepartment_ID();
                ddlDepartment.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
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
    protected void AddWorkShop()
    {
        try
        {
            WorkshopRow _WorkshopRow = new WorkshopRow();
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
            _WorkshopRow.Active = 1;
            _WorkshopRow.Address = txtDiaChi.Text;
            _WorkshopRow.Phone = txtPhone.Text;
            _WorkshopRow.Email = txtEmail.Text;

            if (!string.IsNullOrEmpty(txtBirth.Text.Trim()))
            {
                DateTime birth = DateTime.ParseExact(txtBirth.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _WorkshopRow.BirthDate = birth;
            }
            if (rdonhanvien.Checked)
            {
                _WorkshopRow.Type = 1;
            }
            else if (rdohsx.Checked)
            {
                _WorkshopRow.Type = 2;
            }

            _WorkshopRow.Gender = Convert.ToInt32(ddlGioiTinh.SelectedIndex);
            _WorkshopRow.LastEditDate = _WorkshopRow.CreateDate = DateTime.Now;
            _WorkshopRow.LastEditBy = _WorkshopRow.CreateBy = MyUser.GetUser_ID();
            BusinessRulesLocator.GetWorkshopBO().Insert(_WorkshopRow);
            lblMessage.Text = "Thêm mới thông tin thành công!";
            lblMessage.Visible = true;
            ClearForm();
            Response.Redirect("Workshop_List.aspx", false);
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateBranch", ex.ToString());
        }
    }

    protected void ClearForm()
    {

        txtName.Text = "";
        txtPhone.Text = "";
        txtDiaChi.Text = "";
        txtBirth.Text = "";
        txtEmail.Text = "";
        txtNote.Text = "";
        ddlGioiTinh.SelectedIndex = 0;
        ddlDepartment.SelectedValue = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddWorkShop();

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workshop_List.aspx", false);
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

}