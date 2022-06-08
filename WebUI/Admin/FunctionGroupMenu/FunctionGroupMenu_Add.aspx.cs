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

public partial class FunctionGroupMenu_Add : System.Web.UI.Page
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
            AddFuntionGroupMenu();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckExsitsFuntionGroupMenuName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupMenuBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddFuntionGroupMenu()
    {
        try
        {
            if (CheckExsitsFuntionGroupMenuName(txtName.Text.Trim()))
            {
                FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();
                _FunctionGroupMenuRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                if (ckActive.Checked)
                {
                    _FunctionGroupMenuRow.Active = 1;
                }
                else
                {
                    _FunctionGroupMenuRow.Active = 0;
                }
                _FunctionGroupMenuRow.CreateBy = MyUser.GetUser_ID();
                _FunctionGroupMenuRow.CreateDate = DateTime.Now;
                _FunctionGroupMenuRow.LastEditBy = MyUser.GetUser_ID();
                _FunctionGroupMenuRow.LastEditDate = DateTime.Now;
                _FunctionGroupMenuRow.Sort = Common.GenarateSort("FunctionGroupMenu");
                BusinessRulesLocator.GetFunctionGroupMenuBO().Insert(_FunctionGroupMenuRow);
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

        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FunctionGroupMenu_List.aspx", false);
    }
}