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
using Telerik.Web.UI;

public partial class Function_Add : System.Web.UI.Page
{
    public string title = "Thêm mới chức năng";
    private string FunctionGroup_ID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDLFunctionGroup();
            FillDDLFunctionGroupMenu();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(GetFunctionGroup_ID()))
            {
                AddFuntion();
            }
            else
            {
                lblMessage.Text = "Bạn chưa chọn nhóm chức năng";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckExsitsCategoryName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }

    protected void AddFuntion()
    {
        try
        {
            if (CheckExsitsCategoryName(txtName.Text.Trim()))
            {
                FunctionRow _FunctionRow = new FunctionRow();
                _FunctionRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                //_FunctionRow.FunctionGroup_ID = string.IsNullOrEmpty(ddlFunctionGroup.Text) ? "" : GetFunctionGroup_ID();
                _FunctionRow.FunctionGroup_ID = GetFunctionGroup_ID();
                _FunctionRow.FunctionGroupMenu_ID = Convert.ToInt32(ddlFunctionGroupMenu.SelectedValue);
                if (ckActive.Checked)
                {
                    _FunctionRow.Active = 1;
                }
                else
                {
                    _FunctionRow.Active = 0;
                }
                _FunctionRow.CreateBy = MyUser.GetUser_ID();
                _FunctionRow.CreateDate = DateTime.Now;
                _FunctionRow.LastEditBy = MyUser.GetUser_ID();
                _FunctionRow.LastEditDate = DateTime.Now;
                _FunctionRow.Sort = Common.GenarateSort("Function");
                BusinessRulesLocator.GetFunctionBO().Insert(_FunctionRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Đã tồn tại chức năng " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateFunction", ex.ToString());
        }
    }
    protected void ClearForm()
    {

        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Function_List.aspx", false);
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
    protected void ddlFunctionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlFunctionGroup.SelectedValue != "0")
        //{
        //    FunctionGroup_ID = ddlFunctionGroup.SelectedValue;
        //}
        string listItem = string.Empty;
        foreach (RadComboBoxItem item in ddlFunctionGroup.Items)
        {
            if (item.Checked)
            {
                listItem += item.Value + ",";
            }
        }
    }

}