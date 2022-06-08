using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_BusinessType_BusinessType_Edit : System.Web.UI.Page
{
    int BusinessType_ID = 0;
    public string title = "Thông tin lĩnh vực kinh doanh";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["BusinessType_ID"]))
            int.TryParse(Request["BusinessType_ID"].ToString(), out BusinessType_ID);
        if (!IsPostBack)
        {
            LoadBusinessType();
            FillIntoBussinessType();
        }
    }
    //protected void LoadBranch()
    //{

    //    DataTable dtBranch = new DataTable();
    //    dtBranch = BusinessRulesLocator.GetBranchBO().GetAsDataTable(" Active=1", " Sort ASC");
    //    ddlBranch.DataSource = dtBranch;
    //    ddlBranch.DataTextField = "Name";
    //    ddlBranch.DataValueField = "Branch_ID";
    //    ddlBranch.DataBind();
    //    ddlBranch.Items.Insert(0, new ListItem("--Chọn Ngành --", ""));

    //}
    protected void LoadBusinessType()
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetBusinessType();
        ddlCha.DataSource = dt;
        ddlCha.DataTextField = "Name";
        ddlCha.DataValueField = "BusinessType_ID";
        ddlCha.DataBind();
        ddlCha.Items.Insert(0, new ListItem("--Chọn Danh Mục--", "0"));
    }
    protected void FillIntoBussinessType()
	{
		try
		{
            if (BusinessType_ID != 0)
            {
                BusinessTypeRow _BusinessTypeRow = new BusinessTypeRow();
                _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
                if (_BusinessTypeRow != null)
                {
                    txtTitle.Text = _BusinessTypeRow.IsTitleNull ? string.Empty : _BusinessTypeRow.Title;
                    txtNote.Text = _BusinessTypeRow.IsDescriptionNull ? string.Empty : _BusinessTypeRow.Description;
                    ddlCha.SelectedValue = _BusinessTypeRow.IsParent_IDNull ? "" : _BusinessTypeRow.Parent_ID.ToString();
                    if (_BusinessTypeRow.Active == 1)
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

            Log.writeLog("FillIntoBussinessType", ex.ToString());
        }
	}
    protected void UpdateBusinessType()
	{
		try
		{
            BusinessTypeRow _BusinessTypeRow = new BusinessTypeRow();
            if (BusinessType_ID != 0)
            {
                _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
                if (_BusinessTypeRow != null)
                {
                    _BusinessTypeRow.Title = string.IsNullOrEmpty(txtTitle.Text) ? string.Empty : txtTitle.Text;
                    _BusinessTypeRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    if (ckActive.Checked)
                    {
                        _BusinessTypeRow.Active = 1;
                    }
                    else
                    {
                        _BusinessTypeRow.Active = 0;
                    }
                    if (ddlCha.SelectedValue != "0")
                    {
                        _BusinessTypeRow.Parent_ID = Convert.ToInt32(ddlCha.SelectedValue);
                    }
                    _BusinessTypeRow.LastEditBy = MyUser.GetUser_ID();
                    _BusinessTypeRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetBusinessTypeBO().Update(_BusinessTypeRow);
                }
                lblMessage.Text = "Cập nhật thành công";
                lblMessage.Visible = true;
                FillIntoBussinessType();
            }
        }
		catch (Exception ex)
		{

            Log.writeLog("UpdateBusinessType", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateBusinessType();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BusinessType_List.aspx", false);
    }
}