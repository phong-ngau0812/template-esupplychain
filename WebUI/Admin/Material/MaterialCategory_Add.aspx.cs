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

public partial class MaterialCategory_Add : System.Web.UI.Page
{
    public string title = "Thêm mới danh mục vật tư";
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
            AddMateriaCategory();
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
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddMateriaCategory()
    {
        try
        {
            if (CheckExsitsCategoryName(txtName.Text.Trim()))
            {
                MaterialCategoryRow _MaterialCategoryRow = new MaterialCategoryRow();
                _MaterialCategoryRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;

                if (ckActive.Checked)
                {
                    _MaterialCategoryRow.Active = 1;
                }
                else
                {
                    _MaterialCategoryRow.Active = 0;
                }
                _MaterialCategoryRow.CreateBy = MyUser.GetUser_ID();
                _MaterialCategoryRow.CreateDate = DateTime.Now;

                _MaterialCategoryRow.LastEditBy = MyUser.GetUser_ID();
                _MaterialCategoryRow.LastEditDate = DateTime.Now;
                _MaterialCategoryRow.Sort = Common.GenarateSort("MaterialCategory");
                BusinessRulesLocator.GetMaterialCategoryBO().Insert(_MaterialCategoryRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Đã tồn tại danh mục vật tư " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMinistry", ex.ToString());
        }
    }
    protected void ClearForm()
    {

        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialCategory_List.aspx", false);
    }
}