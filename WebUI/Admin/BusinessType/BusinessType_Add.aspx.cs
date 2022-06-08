using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_BusinessType_BusinessType_Add : System.Web.UI.Page
{
    public string title = "Thêm mới lĩnh vực kinh doanh";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            LoadBusinessType();

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

    protected void AddBusinessType()
    {
        BusinessTypeRow _BusinessTypeRow = new BusinessTypeRow();
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


        _BusinessTypeRow.Language_ID = 1;
        _BusinessTypeRow.Status = 1;
        //_BusinessTypeRow.Branch_ID = Convert.ToInt32(ddlBranch.SelectedValue);
        _BusinessTypeRow.Sort = Common.GenarateSort("BusinessType");
        _BusinessTypeRow.CreateBy = MyUser.GetUser_ID();
        _BusinessTypeRow.CreateDate = DateTime.Now;
        BusinessRulesLocator.GetBusinessTypeBO().Insert(_BusinessTypeRow);
        lblMessage.Text = "Thêm mới thành công";
        lblMessage.Visible = true;
        ClearForm();
        Response.Redirect("BusinessType_List.aspx", false);
    }
    protected void ClearForm()
    {
        txtTitle.Text = txtNote.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BusinessType_List.aspx", false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddBusinessType();


        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
}