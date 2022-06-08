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

public partial class FunctionGroup_Edit : System.Web.UI.Page
{
    int FunctionGroup_ID = 0;
    public string title = "Thông tin nhóm quyền";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["FunctionGroup_ID"]))
            int.TryParse(Request["FunctionGroup_ID"].ToString(), out FunctionGroup_ID);
        if (!IsPostBack)
        {
            FillInfoFunctionGroup();
        }
    }

    protected void FillInfoFunctionGroup()
    {
        try
        {
            if (FunctionGroup_ID != 0)
            {
                FunctionGroupRow _FunctionGroupRow = new FunctionGroupRow();
                _FunctionGroupRow = BusinessRulesLocator.GetFunctionGroupBO().GetByPrimaryKey(FunctionGroup_ID);
                if (_FunctionGroupRow != null)
                {
                    txtName.Text = _FunctionGroupRow.IsNameNull ? string.Empty : _FunctionGroupRow.Name;

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
            FunctionGroupRow _FunctionGroupRow = new FunctionGroupRow();
            if (FunctionGroup_ID != 0)
            {
                _FunctionGroupRow = BusinessRulesLocator.GetFunctionGroupBO().GetByPrimaryKey(FunctionGroup_ID);
                if (_FunctionGroupRow != null)
                {
                    _FunctionGroupRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                   
                    //_FunctionGroupRow.Active = 1;
                    
                    _FunctionGroupRow.LastEditBy = MyUser.GetUser_ID();
                    _FunctionGroupRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetFunctionGroupBO().Update(_FunctionGroupRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoFunctionGroup();
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
        Response.Redirect("FunctionGroup_List.aspx", false);
    }
}