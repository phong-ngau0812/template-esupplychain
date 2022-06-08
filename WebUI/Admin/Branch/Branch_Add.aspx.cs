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
using Telerik.Web.UI;

public partial class Branch_Add : System.Web.UI.Page
{
    int Branch_ID = 0;
    public string title = "Thêm mới tin ngành";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDLCha();
            FillDDLChaBrand();
        }
    }
    private void FillDDLChaBrand()
    {
        try
        {
            DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID is null", " Sort ASC");//
            dt = BusinessRulesLocator.Conllection().GetBranch();
            ddlCha.DataSource = dt;
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "Branch_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLCha()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMinistryBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlMinistry.DataSource = dt;
            ddlMinistry.DataTextField = "Name";
            ddlMinistry.DataValueField = "Ministry_ID";
            ddlMinistry.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    //protected void FillInfoBranch()
    //{
    //    try
    //    {
    //        if (Branch_ID != 0)
    //        {
    //            BranchRow _BranchRow = new BranchRow();
    //            _BranchRow = BusinessRulesLocator.GetBranchBO().GetByPrimaryKey(Branch_ID);
    //            if (_BranchRow != null)
    //            {
    //                txtName.Text = _BranchRow.IsNameNull ? string.Empty : _BranchRow.Name;
    //                if (!_BranchRow.IsMinistry_IDNull)
    //                {
    //                    string[] array = _BranchRow.Ministry_ID.Split(',');
    //                    foreach (string value in array)
    //                    {

    //                        foreach (RadComboBoxItem item in ddlMinistry.Items)
    //                        {
    //                            if (value == item.Value)
    //                            {
    //                                item.Checked = true;
    //                            }
    //                        }
    //                    }
    //                }
    //                txtOrder.Text = _BranchRow.IsSortNull ? string.Empty : _BranchRow.Sort.ToString();
    //                if (_BranchRow.Active == 1)
    //                {
    //                    ckActive.Checked = true;
    //                }
    //                else
    //                {
    //                    ckActive.Checked = false;
    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillInfoUser", ex.ToString());
    //    }
    //}
    protected string GetMinisty_ID()
    {
        string FunctionGroup_ID = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlMinistry.Items)
            {
                if (item.Checked)
                {
                    FunctionGroup_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(FunctionGroup_ID))
            {
                FunctionGroup_ID = "," + FunctionGroup_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return FunctionGroup_ID;
    }
    protected void AddBranch()
    {
        try
        {
            if (!string.IsNullOrEmpty(GetMinisty_ID()))
            {
                BranchRow _BranchRow = new BranchRow();
                _BranchRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                if(ddlCha.SelectedValue != "0")
                {
                    _BranchRow.Parent_ID = Convert.ToInt32(ddlCha.SelectedValue);
                }
                _BranchRow.Ministry_ID = GetMinisty_ID();
                if (!string.IsNullOrEmpty(txtOrder.Text))
                {
                    _BranchRow.Sort = Convert.ToInt32(txtOrder.Text);
                }

                _BranchRow.CreateBy = MyUser.GetUser_ID();
                _BranchRow.CreateDate = DateTime.Now;
                _BranchRow.LastEditedBy = MyUser.GetUser_ID();
                _BranchRow.LastEditedDate = DateTime.Now;

                if (ckActive.Checked)
                {
                    _BranchRow.Active = 1;
                }
                else
                {
                    _BranchRow.Active = 0;
                }
                BusinessRulesLocator.GetBranchBO().Insert(_BranchRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Chưa chọn bộ!";
                lblMessage.Visible = true;
            }
       
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateBranch", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddBranch();
                
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch_List.aspx", false);
    }
}