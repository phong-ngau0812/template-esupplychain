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

public partial class FunctionGroup_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhóm quyền";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddFuntionGroup();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckExsitsFuntionGroupName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddFuntionGroup()
    {
        try
        {
            if (CheckExsitsFuntionGroupName(txtName.Text.Trim()))
            {
                FunctionGroupRow _FunctionGroupRow = new FunctionGroupRow();
                _FunctionGroupRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                
                _FunctionGroupRow.Active = 1;               
                _FunctionGroupRow.CreateBy = MyUser.GetUser_ID();
                _FunctionGroupRow.CreateDate = DateTime.Now;
                _FunctionGroupRow.LastEditBy = MyUser.GetUser_ID();
                _FunctionGroupRow.LastEditDate = DateTime.Now;
                _FunctionGroupRow.Sort = Common.GenarateSort("FunctionGroup");
                BusinessRulesLocator.GetFunctionGroupBO().Insert(_FunctionGroupRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Đã tồn tại nhóm chức năng " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("AddFuntionGroupCategory", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FunctionGroup_List.aspx", false);
    }
}