using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Department_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadZone();
            LoadArea();
            LoadDepartment();
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
            string where = string.Empty;
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
            Common.FillProductBrand(ddlProductBrand, where);
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



    protected void LoadDepartment()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and Zone_ID =" + ddlZone.SelectedValue.ToString();
            }
            if (ddlArea.SelectedValue != "0")
            {
                where += " and Area_ID =" + ddlArea.SelectedValue.ToString();
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROM Department WHERE Active <>-1" + where + "ORDER BY Name ASC");
            rptDepartment.DataSource = dt;
            rptDepartment.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void LoadZone()
    {
        try
        {
            string where = " Active <>-1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dtZone = BusinessRulesLocator.GetZoneBO().GetAsDataTable(where, " Name ASC");
            ddlZone.DataSource = dtZone;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    if (MyUser.GetZone_ID() != "0")
                    {
                        ddlZone.SelectedValue = MyUser.GetZone_ID();
                        ddlZone.Enabled = false;
                    }
                }
                
                
            }
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadZone", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            string where = " 1=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }
            DataTable dtArea = new DataTable();
            dtArea = BusinessRulesLocator.GetAreaBO().GetAsDataTable( where ,"");
            ddlArea.DataSource = dtArea;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                {
                    if (MyUser.GetArea_ID() != "0")
                    {
                        ddlArea.SelectedValue = MyUser.GetArea_ID();
                        ddlArea.Enabled = false;
                    }
                }
                             
            }
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadZone", ex.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Department_Add.aspx", false);
    }


    protected void rptDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Department_ID = Convert.ToInt32(e.CommandArgument);
        DepartmentRow _DepartmentRow = new DepartmentRow();
        _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (MyActionPermission.CanDeleteDepartment(Department_ID, ref Message))
                {
                    if (_DepartmentRow != null)
                    {
                        _DepartmentRow.Active = -1;
                    }
                    BusinessRulesLocator.GetDepartmentBO().Update(_DepartmentRow);
                    MyActionPermission.WriteLogSystem(Department_ID, "Xóa - " + _DepartmentRow.Name);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }

                break;

            case "Active":
                _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);
                if (_DepartmentRow != null)
                {
                    _DepartmentRow.Active = 1;
                }
                BusinessRulesLocator.GetDepartmentBO().Update(_DepartmentRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);
                if (_DepartmentRow != null)
                {
                    _DepartmentRow.Active = 0;
                }
                BusinessRulesLocator.GetDepartmentBO().Update(_DepartmentRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadDepartment();
    }

    protected void rptDepartment_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadDepartment();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadDepartment();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDepartment();
    }
}