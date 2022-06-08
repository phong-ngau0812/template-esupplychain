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

public partial class ChainLink_Add : System.Web.UI.Page
{
    public string title = "Thêm mới chuỗi liên kết ";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!IsPostBack)
        {


        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddChainLink();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckNameChainLink(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            //where += " and Name =N'" + Name.Trim() + "'";
            where += " and Name =N'" + Name + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }


    protected void AddChainLink()
    {
        try
        {
            if (CheckNameChainLink(txtName.Text))
            {
                ChainLinkRow _ChainLinkRow = new ChainLinkRow();

                
                _ChainLinkRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;

                _ChainLinkRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                _ChainLinkRow.Summary = string.IsNullOrEmpty(txtSummary.Text) ? string.Empty : txtSummary.Text;
                
                if (ckActive.Checked)
                {
                    _ChainLinkRow.Active = 1;
                }
                else
                {
                    _ChainLinkRow.Active = 0;
                }

                _ChainLinkRow.Sort = Common.GenarateSort("ChainLink");
                _ChainLinkRow.LastEditBy = _ChainLinkRow.CreateBy = MyUser.GetUser_ID();
                _ChainLinkRow.LastEditDate = _ChainLinkRow.CreateDate = DateTime.Now;
                BusinessRulesLocator.GetChainLinkBO().Insert(_ChainLinkRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
                Response.Redirect("ChainLink_List.aspx", false);

            }
            else
            {
                lblMessage.Text = "Đã tồn tại chuỗi liên kết " + txtName.Text;
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.PaleTurquoise;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateChain", ex.ToString());
        }
    }
    protected void ClearForm()
    {

        txtName.Text = "";
        txtNote.Text = "";
        txtSummary.Text = "";
        //ddlProductBrand.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChainLink_List.aspx", false);
    }

}