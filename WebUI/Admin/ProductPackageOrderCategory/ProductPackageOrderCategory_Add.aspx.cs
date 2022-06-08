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

public partial class ProductPackageOrderCategory_Add : System.Web.UI.Page
{
    public string title = "Thêm mới loại lệnh ";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

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
            AddProductPackageOrderCategory();
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
            dt = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddProductPackageOrderCategory()
    {
        try
        {
            //if (CheckExsitsCategoryName(txtName.Text.Trim()))
            {
                ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = new ProductPackageOrderCategoryRow();
                _ProductPackageOrderCategoryRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                _ProductPackageOrderCategoryRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                _ProductPackageOrderCategoryRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                if (ckActive.Checked)
                {
                    _ProductPackageOrderCategoryRow.Active = 1;
                }
                else
                {
                    _ProductPackageOrderCategoryRow.Active = 0;
                }
                _ProductPackageOrderCategoryRow.CreateBy = MyUser.GetUser_ID();
                _ProductPackageOrderCategoryRow.CreateDate = DateTime.Now;
                _ProductPackageOrderCategoryRow.LastEditedBy = MyUser.GetUser_ID();
                _ProductPackageOrderCategoryRow.LastEditedDate = DateTime.Now;
                _ProductPackageOrderCategoryRow.Sort = Common.GenarateSort("ProductPackageOrderCategory");
                BusinessRulesLocator.GetProductPackageOrderCategoryBO().Insert(_ProductPackageOrderCategoryRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            //else
            //{
            //    lblMessage.Text = "Đã tồn tại nhóm chức năng " + txtName.Text.Trim();
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.Wheat;
            //    lblMessage.Visible = true;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProductPackageOrderCategory", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        ddlProductBrand.SelectedValue = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackageOrderCategory_List.aspx", false);
    }
}