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

public partial class ChainLink_Edit : System.Web.UI.Page
{
    int ChainLink_ID = 0;
    public string title = "Thông tin chuỗi liên kết";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["ChainLink_ID"]))
            int.TryParse(Request["ChainLink_ID"].ToString(), out ChainLink_ID);
        if (!IsPostBack)
        {

            FillInfoChainLink();
        }
    }



    protected void FillInfoChainLink()
    {
        try
        {
            if (ChainLink_ID != 0)
            {
                ChainLinkRow _ChainLinkRow = new ChainLinkRow();
                _ChainLinkRow = BusinessRulesLocator.GetChainLinkBO().GetByPrimaryKey(ChainLink_ID);
                if (_ChainLinkRow != null)
                {
                    txtName.Text = _ChainLinkRow.IsNameNull ? string.Empty : _ChainLinkRow.Name;
                    txtSummary.Text = _ChainLinkRow.IsSummaryNull ? string.Empty : _ChainLinkRow.Summary;
                    txtNote.Text = _ChainLinkRow.IsDescriptionNull ? string.Empty : _ChainLinkRow.Description;
                    if (_ChainLinkRow.Active == 1)
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
            Log.writeLog("FillInfoUser", ex.ToString());
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
    protected void UpdateChainLink()
    {
        try
        {
            ChainLinkRow _ChainLinkRow = new ChainLinkRow();
            if (ChainLink_ID != 0)
            {
                //if (CheckNameChainLink(txtName.Text))
                {
                    _ChainLinkRow = BusinessRulesLocator.GetChainLinkBO().GetByPrimaryKey(ChainLink_ID);
                    if (_ChainLinkRow != null)
                    {
                        _ChainLinkRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                        _ChainLinkRow.Summary = string.IsNullOrEmpty(txtSummary.Text) ? string.Empty : txtSummary.Text;
                        _ChainLinkRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                        if (ckActive.Checked)
                        {
                            _ChainLinkRow.Active = 1;
                        }
                        else
                        {
                            _ChainLinkRow.Active = 0;
                        }
                        _ChainLinkRow.Sort = Common.GenarateSort("ChainLink");
                        _ChainLinkRow.LastEditBy = MyUser.GetUser_ID();
                        _ChainLinkRow.LastEditDate = DateTime.Now;
                        BusinessRulesLocator.GetChainLinkBO().Update(_ChainLinkRow);
                    }
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FillInfoChainLink();
                    Response.Redirect("ChainLink_List.aspx", false);
                }
                //else
                //{
                //    lblMessage.Text = "Đã tồn tại chuỗi liên kết " + txtName.Text;
                //    lblMessage.ForeColor = Color.Red;
                //    lblMessage.BackColor = Color.PaleTurquoise;
                //    lblMessage.Visible = true;
                //}
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateChainLink", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateChainLink();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChainLink_List.aspx", false);
    }


}