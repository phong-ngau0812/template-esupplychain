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

public partial class FunctionGroupMenu_Edit : System.Web.UI.Page
{
    int FunctionGroupMenu_ID = 0;
    public string title = "Thông tin nhóm quyền";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["FunctionGroupMenu_ID"]))
            int.TryParse(Request["FunctionGroupMenu_ID"].ToString(), out FunctionGroupMenu_ID);
        if (!IsPostBack)
        {
            FillInfoFunctionGroupCategory();
        }
    }

    protected void FillInfoFunctionGroupCategory()
    {
        try
        {
            if (FunctionGroupMenu_ID != 0)
            {
                FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();
                _FunctionGroupMenuRow = BusinessRulesLocator.GetFunctionGroupMenuBO().GetByPrimaryKey(FunctionGroupMenu_ID);
                if (_FunctionGroupMenuRow != null)
                {
                    txtName.Text = _FunctionGroupMenuRow.IsNameNull ? string.Empty : _FunctionGroupMenuRow.Name;
                    
                
                    if (_FunctionGroupMenuRow.Active == 1)
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
            Log.writeLog("FillInfoFunctionGroupMenu", ex.ToString());
        }
    }

    protected void UpdateFunctionGroupMenu()
    {
        try
        {
            FunctionGroupMenuRow _FunctionGroupMenuRow = new FunctionGroupMenuRow();
            if (FunctionGroupMenu_ID != 0)
            {
                _FunctionGroupMenuRow = BusinessRulesLocator.GetFunctionGroupMenuBO().GetByPrimaryKey(FunctionGroupMenu_ID);
                if (_FunctionGroupMenuRow != null)
                {
                    _FunctionGroupMenuRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                   
                    if (ckActive.Checked)
                    {
                        _FunctionGroupMenuRow.Active = 1;
                    }
                    else
                    {
                        _FunctionGroupMenuRow.Active = 0;
                    }
                    _FunctionGroupMenuRow.LastEditBy = MyUser.GetUser_ID();
                    _FunctionGroupMenuRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetFunctionGroupMenuBO().Update(_FunctionGroupMenuRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoFunctionGroupCategory();
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
                UpdateFunctionGroupMenu();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FunctionGroupMenu_List.aspx", false);
    }
}