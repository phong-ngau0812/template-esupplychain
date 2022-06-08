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

public partial class Department_Edit : System.Web.UI.Page
{
    int Department_ID = 0;
    public string title = "Thông tin phòng ban ";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["Department_ID"]))
            int.TryParse(Request["Department_ID"].ToString(), out Department_ID);
        if (!IsPostBack)
        {

            FillDDLddlProductBrand();

            FillDLLZone();
            FillDDLArea();
            FilldllTaskType();
            FillInfoDepartment();

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
    protected void FillInfoDepartment()
    {
        try
        {
            if (Department_ID != 0)
            {
                DepartmentRow _DepartmentRow = new DepartmentRow();
                _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);

                if (_DepartmentRow != null)
                {
                    txtName.Text = _DepartmentRow.IsNameNull ? string.Empty : _DepartmentRow.Name;
                    ddlProductBrand.SelectedValue = _DepartmentRow.ProductBrand_ID.ToString();
                    ddlZone.SelectedValue = _DepartmentRow.IsZone_IDNull ? "0" : _DepartmentRow.Zone_ID.ToString();
                    ddlArea.SelectedValue = _DepartmentRow.IsArea_IDNull ? "0" : _DepartmentRow.Area_ID.ToString();
                    txtNote.Text = _DepartmentRow.IsDescriptionNull ? string.Empty : _DepartmentRow.Description;
                    if (!_DepartmentRow.IsListTaskType_IDNull)
                    {
                        string[] array = _DepartmentRow.ListTaskType_ID.Split(',');
                        foreach (string value in array)
                        {
                            if (value != "")
                            {
                                foreach (ListItem item in ddlTaskType.Items)
                                {
                                    if (value == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    if (_DepartmentRow.Active == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
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

    protected void UpdateDepartment()
    {
        try
        {
            DepartmentRow _DepartmentRow = new DepartmentRow();
            if (Department_ID != 0)
            {
                _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);
                if (_DepartmentRow != null)
                {
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
                    _DepartmentRow.LastEditedBy = MyUser.GetUser_ID();
                    _DepartmentRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetDepartmentBO().Update(_DepartmentRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoDepartment();
            }
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateDepartment();
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
        Response.Redirect("Department_List.aspx", false);
    }
}