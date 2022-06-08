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

public partial class ProductPackageOrderCategory_Edit : System.Web.UI.Page
{
    int ProductPackageOrderCategory_ID = 0;
    public string title = "Thông tin loại lệnh";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["ProductPackageOrderCategory_ID"]))
            int.TryParse(Request["ProductPackageOrderCategory_ID"].ToString(), out ProductPackageOrderCategory_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillInfoProductPackageOrderCategory();
        }
    }


    private void FillDDLddlProductBrand()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));
            
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void FillInfoProductPackageOrderCategory()
    {
        try
        {
            if (ProductPackageOrderCategory_ID != 0)
            {
                ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = new ProductPackageOrderCategoryRow();
                _ProductPackageOrderCategoryRow = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetByPrimaryKey(ProductPackageOrderCategory_ID);

                if (_ProductPackageOrderCategoryRow != null)
                {
                    txtName.Text = _ProductPackageOrderCategoryRow.IsNameNull ? string.Empty : _ProductPackageOrderCategoryRow.Name;
                    ddlProductBrand.SelectedValue = _ProductPackageOrderCategoryRow.ProductBrand_ID.ToString();
                    //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                    MyActionPermission.CheckPermission(_ProductPackageOrderCategoryRow.ProductBrand_ID.ToString(), _ProductPackageOrderCategoryRow.CreateBy.ToString(), "/Admin/ProductPackageOrderCategory/ProductPackageOrderCategory_List");
                    ddlProductBrand.Enabled = false;
                    txtNote.Text = _ProductPackageOrderCategoryRow.IsDescriptionNull ? string.Empty : _ProductPackageOrderCategoryRow.Description;

                    if (_ProductPackageOrderCategoryRow.Active == 1)
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

    protected void UpdateFunctionGroup()
    {
        try
        {
            ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = new ProductPackageOrderCategoryRow();
            if (ProductPackageOrderCategory_ID != 0)
            {
                _ProductPackageOrderCategoryRow = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetByPrimaryKey(ProductPackageOrderCategory_ID);
                if (_ProductPackageOrderCategoryRow != null)
                {
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
                    _ProductPackageOrderCategoryRow.LastEditedBy = MyUser.GetUser_ID();
                    _ProductPackageOrderCategoryRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetProductPackageOrderCategoryBO().Update(_ProductPackageOrderCategoryRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoProductPackageOrderCategory();
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
        Response.Redirect("ProductPackageOrderCategory_List.aspx", false);
    }
}