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

public partial class Department_Add : System.Web.UI.Page
{
    public string title = "Thêm mới phòng ban  ";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDLLZone();
            FillDDLArea();
            FilldllTaskType();
        }
    }


    protected void FilldllTaskType()
    {
        string where = string.Empty;
        if (Common.CheckUserXuanHoa1())
        {
            where += "TaskType_ID not in (4)";
        }
        DataTable dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable("" + where, "");
        ddlTaskType.DataSource = dt;
        ddlTaskType.DataTextField = "Name";
        ddlTaskType.DataValueField = "TaskType_ID";
        ddlTaskType.DataBind();

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
            Common.FillProductBrand_Null(ddlProductBrand, where);
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
    protected void FillDLLZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            DataTable dtZone = BusinessRulesLocator.GetZoneBO().GetAsDataTable("Active <>-1" + where, "Name ASC");
            ddlZone.DataSource = dtZone;
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataTextField = "Name";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));

            if (MyUser.GetAccountType_ID() == "7" || MyUser.GetAccountType_ID() == "8")
            {
                ddlZone.SelectedValue = MyUser.GetZone_ID().ToString();
                ddlZone.Enabled = false;
            }
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
            DataTable dtArea = new DataTable();
            if (MyUser.GetAccountType_ID() == "8")
            {
                dtArea = BusinessRulesLocator.Conllection().GetAllList("Select * from Area where Area_ID=" + MyUser.GetArea_ID() + "Order by Name ASC");
                ddlArea.DataSource = dtArea;
                ddlArea.DataTextField = "Name";
                ddlArea.DataValueField = "Area_ID";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));


                ddlArea.SelectedValue = MyUser.GetArea_ID().ToString();
                ddlArea.Enabled = false;
            }
            else
            {
                if (ddlProductBrand.SelectedValue != "")
                {
                    where += " and ProductBrand_ID=" + Convert.ToInt32(ddlProductBrand.SelectedValue);
                }
                if (ddlZone.SelectedValue != "0")
                {
                    where += " and Zone_ID=" + Convert.ToInt32(ddlZone.SelectedValue);
                }
                dtArea = BusinessRulesLocator.Conllection().GetAllList("Select * from Area where 1=1" + where + "Order by Name ASC");
                ddlArea.DataSource = dtArea;
                ddlArea.DataTextField = "Name";
                ddlArea.DataValueField = "Area_ID";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            }
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
            AddDepartment();


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
            where += " and Name like N'%" + Name.Trim() + "%'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddDepartment()
    {
        try
        {

            DepartmentRow _DepartmentRow = new DepartmentRow();
            _DepartmentRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _DepartmentRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            if (ddlZone.SelectedValue != "")
            {
                _DepartmentRow.Zone_ID = Convert.ToInt32(ddlZone.SelectedValue);
            }
            if (ddlArea.SelectedValue != "")
            {
                _DepartmentRow.Area_ID = Convert.ToInt32(ddlArea.SelectedValue);
            }
            _DepartmentRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

            if (ckActive.Checked)
            {
                _DepartmentRow.Active = 1;
            }
            else
            {
                _DepartmentRow.Active = 0;
            }
            _DepartmentRow.ListTaskType_ID = AddTaskType_ID();
            _DepartmentRow.CreateBy = MyUser.GetUser_ID();
            _DepartmentRow.CreateDate = DateTime.Now;
            _DepartmentRow.LastEditedBy = MyUser.GetUser_ID();
            _DepartmentRow.LastEditedDate = DateTime.Now;
            _DepartmentRow.Sort = Common.GenarateSort("Department");
            BusinessRulesLocator.GetDepartmentBO().Insert(_DepartmentRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            Response.Redirect("Department_List.aspx", false);
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateDepartment", ex.ToString());
        }
    }
    protected string AddTaskType_ID()
    {
        string TaskType_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlTaskType.Items)
            {
                if (item.Selected)
                {
                    TaskType_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(TaskType_ID))
            {
                TaskType_ID = "," + TaskType_ID;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("AddTaskType_ID", ex.ToString());
        }
        return TaskType_ID;
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        txtNote.Text = "";
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
        Response.Redirect("Department_List.aspx", false);
    }
}