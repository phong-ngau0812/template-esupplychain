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

public partial class Discount_Add : System.Web.UI.Page
{
    public string title = "Thêm mới chiết khấu";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtDescription);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
        }
    }


    private void FillDDLddlProductBrand()
    {
        try
        {

            Common.FillProductBrand_Null(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
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
            AddDiscount();
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
            dt = BusinessRulesLocator.GetDiscountBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddDiscount()
    {
        try
        {
            //if (CheckExsitsCategoryName(txtName.Text.Trim()))
            //{
            DiscountRow _DiscountRow = new DiscountRow();
            _DiscountRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _DiscountRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            //string percent = string.IsNullOrEmpty(txtPercent.Text) ? "0" : txtPercent.Text;
            //_DiscountRow.Percent = Convert.ToInt32(percent);

            if (!string.IsNullOrEmpty(txtPercent.Text))
            {
                _DiscountRow.Percent = Convert.ToInt32(txtPercent.Text.Trim());
            }
            else
            {
                _DiscountRow.Percent = 0;
            }
            _DiscountRow.Description = string.IsNullOrEmpty(txtDescription.Text) ? string.Empty : txtDescription.Text;


            if (ckActive.Checked)
            {
                _DiscountRow.Active = 1;
            }
            else
            {
                _DiscountRow.Active = 0;
            }
            _DiscountRow.CreateBy = MyUser.GetUser_ID();
            _DiscountRow.CreateDate = DateTime.Now;
            _DiscountRow.LastEditedBy = MyUser.GetUser_ID();
            _DiscountRow.LastEditedDate = DateTime.Now;
            _DiscountRow.Sort = Common.GenarateSort("Discount");
            BusinessRulesLocator.GetDiscountBO().Insert(_DiscountRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            //}
            //else
            //{
            //    lblMessage.Text = "Đã tồn tại nhóm chức năng " + txtName.Text.Trim();
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.PaleTurquoise;
            //    lblMessage.Visible = true;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateDiscount", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        txtPercent.Text = "";
        txtDescription.Text = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Discount_List.aspx", false);
    }
}