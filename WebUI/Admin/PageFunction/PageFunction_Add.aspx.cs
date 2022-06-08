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

public partial class PageFunction_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhóm chức năng";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLFunction();
        }
    }


    private void FillDDLFunction()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlFunction.DataSource = dt;
            ddlFunction.DataTextField = "Name";
            ddlFunction.DataValueField = "Function_ID";
            ddlFunction.DataBind();
            ddlFunction.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", ""));
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
            AddFuntion();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckExsitsPageFunctionUrl(string Url)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Url))
        {
            where += " and Name=N'" + Url.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(where, "");
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
            if (CheckExsitsPageFunctionUrl(txtUrl.Text.Trim()))
            {
                PageFunctionRow _PageFunctionRow = new PageFunctionRow();
                _PageFunctionRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                _PageFunctionRow.Url = string.IsNullOrEmpty(txtUrl.Text) ? string.Empty : txtUrl.Text;
                _PageFunctionRow.Function_ID = Convert.ToInt32(ddlFunction.SelectedValue);
               
                if (ckActive.Checked)
                {
                    _PageFunctionRow.Active = 1;
                }
                else
                {
                    _PageFunctionRow.Active = 0;
                }
                _PageFunctionRow.CreateBy = MyUser.GetUser_ID();
                _PageFunctionRow.CreateDate = DateTime.Now;
                _PageFunctionRow.LastEditBy = MyUser.GetUser_ID();
                _PageFunctionRow.LastEditDate = DateTime.Now;
                _PageFunctionRow.Sort = Common.GenarateSort("_PageFunction");
                BusinessRulesLocator.GetPageFunctionBO().Insert(_PageFunctionRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Đã tồn tại page chức năng " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdatePageFunction", ex.ToString());
        }
    }
    protected void ClearForm()
    {

        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageFunction_List.aspx", false);
    }

}