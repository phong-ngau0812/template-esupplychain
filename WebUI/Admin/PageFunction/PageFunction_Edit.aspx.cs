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

public partial class PageFunction_Edit : System.Web.UI.Page
{
    int PageFunction_ID = 0;
    public string title = "Thông tin page chức năng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["PageFunction_ID"]))
            int.TryParse(Request["PageFunction_ID"].ToString(), out PageFunction_ID);
        if (!IsPostBack)
        {
            FillDDLFunction();
            FillInfoPageFunction();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
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
            ddlFunction.Items.Insert(0, new ListItem("-- Chọn chức năng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void FillInfoPageFunction()
    {
        try
        {
            if (PageFunction_ID != 0)
            {
                PageFunctionRow _PageFunctionRow = new PageFunctionRow();
                _PageFunctionRow = BusinessRulesLocator.GetPageFunctionBO().GetByPrimaryKey(PageFunction_ID);
                if (_PageFunctionRow != null)
                {
                    txtName.Text = _PageFunctionRow.IsNameNull ? string.Empty : _PageFunctionRow.Name;
                    txtUrl.Text = _PageFunctionRow.IsUrlNull ? string.Empty : _PageFunctionRow.Url;
                    ddlFunction.SelectedValue = _PageFunctionRow.Function_ID.ToString();

                    if (_PageFunctionRow.Active == 1)
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
            Log.writeLog("FillInfoPageFunction", ex.ToString());
        }
    }

    protected void UpdateFunctionGroup()
    {
        try
        {
            PageFunctionRow _PageFunctionRow = new PageFunctionRow();
            if (PageFunction_ID != 0)
            {
                _PageFunctionRow = BusinessRulesLocator.GetPageFunctionBO().GetByPrimaryKey(PageFunction_ID);
                if (_PageFunctionRow != null)
                {
                    _PageFunctionRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _PageFunctionRow.Url = string.IsNullOrEmpty(txtUrl.Text) ? string.Empty : txtUrl.Text;
                    _PageFunctionRow.Function_ID = Convert.ToInt32(ddlFunction.SelectedValue);
                    //!string.IsNullOrEmpty(ddlFunctionGroupCategory.SelectedValue)
                    //if (!string.IsNullOrEmpty(ddlFunction.SelectedValue))
                    //{
                    //    _PageFunctionRow.Function_ID = Convert.ToInt32(ddlFunction.SelectedValue);

                    //}
                    //else
                    //{
                    //    _PageFunctionRow.Function_ID = 0;
                    //}

                    if (ckActive.Checked)
                    {
                        _PageFunctionRow.Active = 1;
                    }
                    else
                    {
                        _PageFunctionRow.Active = 0;
                    }
                    _PageFunctionRow.LastEditBy = MyUser.GetUser_ID();
                    _PageFunctionRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetPageFunctionBO().Update(_PageFunctionRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoPageFunction();
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
                UpdateFunctionGroup();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageFunction_List.aspx", false);
    }

    protected void ddlFunctionGroupCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}