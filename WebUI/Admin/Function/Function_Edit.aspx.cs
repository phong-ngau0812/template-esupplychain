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
using Telerik.Web.UI.Skins;

public partial class Function_Edit : System.Web.UI.Page
{
    int Function_ID = 0;
    public string title = "Thông tin chức năng";
    public string avatar = "";
    private string FunctionGroup_ID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Function_ID"]))
            int.TryParse(Request["Function_ID"].ToString(), out Function_ID);
        if (!IsPostBack)
        {
            FillDDLFunctionGroup();
            FillDDLFunctionGroupMenu();
            FillInfoFunction();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }


    private void FillDDLFunctionGroup()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlFunctionGroup.DataSource = dt;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            //ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    private void FillDDLFunctionGroupMenu()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupMenuBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlFunctionGroupMenu.DataSource = dt;
            ddlFunctionGroupMenu.DataTextField = "Name";
            ddlFunctionGroupMenu.DataValueField = "FunctionGroupMenu_ID";
            ddlFunctionGroupMenu.DataBind();
            ddlFunctionGroupMenu.Items.Insert(0, new ListItem("-- Chọn nhóm menu --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillInfoFunction()
    {
        try
        {
            if (Function_ID != 0)
            {
                FunctionRow _FunctionRow = new FunctionRow();
                _FunctionRow = BusinessRulesLocator.GetFunctionBO().GetByPrimaryKey(Function_ID);
                if (_FunctionRow != null)
                {
                    txtName.Text = _FunctionRow.IsNameNull ? string.Empty : _FunctionRow.Name;
                    ddlFunctionGroupMenu.SelectedValue = _FunctionRow.FunctionGroupMenu_ID.ToString();

                    //ddlFunctionGroup.SelectedValue = _FunctionRow.FunctionGroup_ID.ToString();
                    if (!_FunctionRow.IsFunctionGroup_IDNull)

                    {                 
                        string[] array = _FunctionRow.FunctionGroup_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (RadComboBoxItem item in ddlFunctionGroup.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Checked=true;
                                }
                            }
                        }
                    }

                    if (_FunctionRow.Active == 1)
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
    protected string GetFunctionGroup_ID()
    {
        string FunctionGroup_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlFunctionGroup.Items)
            {
                if (item.Checked)
                {
                    FunctionGroup_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(FunctionGroup_ID))
            {
                FunctionGroup_ID = "," + FunctionGroup_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return FunctionGroup_ID;
    }
    protected void UpdateMateriaCategory()
    {
        try
        {
            FunctionRow _FunctionRow = new FunctionRow();
            if (Function_ID != 0)
            {
                _FunctionRow = BusinessRulesLocator.GetFunctionBO().GetByPrimaryKey(Function_ID);
                if (_FunctionRow != null)
                {
                    _FunctionRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _FunctionRow.FunctionGroup_ID =  GetFunctionGroup_ID();
                    _FunctionRow.FunctionGroupMenu_ID = Convert.ToInt32(ddlFunctionGroupMenu.SelectedValue);
                   
                    if (ckActive.Checked)
                    {
                        _FunctionRow.Active = 1;
                    }
                    else
                    {
                        _FunctionRow.Active = 0;
                    }
                    _FunctionRow.LastEditBy = MyUser.GetUser_ID();
                    _FunctionRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetFunctionBO().Update(_FunctionRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoFunction();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateFunctionGroup", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(GetFunctionGroup_ID())){
                    UpdateMateriaCategory();
                }
                else
                {
                    lblMessage.Text = "Bạn chư chọn nhóm chức năng nào! vui lòng chọn nhóm chức năng";
                    lblMessage.Visible = true;
                }
                
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Function_List.aspx", false);
    }

}