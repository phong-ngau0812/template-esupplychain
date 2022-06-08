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

public partial class ProductCategory_Edit : System.Web.UI.Page
{
    int ProductCategory_ID = 0;
    public string title = "Thông tin danh mục sản phẩm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["ProductCategory_ID"]))
            int.TryParse(Request["ProductCategory_ID"].ToString(), out ProductCategory_ID);
        if (!IsPostBack)
        {
            FillDDLCha();
            FillInfoProductCategory();
        }
    }

    private void FillDDLCha()
    {
        try
        {
            DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID is null", " Sort ASC");
            dt = BusinessRulesLocator.Conllection().GetProductCategory(); //â //
            ddlCha.DataSource = dt;
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "ProductCategory_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillInfoProductCategory()
    {
        try
        {
            if (ProductCategory_ID != 0)
            {
                ProductCategoryRow _ProductCategoryRow = new ProductCategoryRow();
                _ProductCategoryRow = BusinessRulesLocator.GetProductCategoryBO().GetByPrimaryKey(ProductCategory_ID);
                if (_ProductCategoryRow != null)
                {
                    txtName.Text = _ProductCategoryRow.IsNameNull ? string.Empty : _ProductCategoryRow.Name;
                    ddlCha.SelectedValue = _ProductCategoryRow.IsParent_IDNull ? "0" : _ProductCategoryRow.Parent_ID.ToString();
                    txtNote.Text = _ProductCategoryRow.IsDescriptionNull ? string.Empty : _ProductCategoryRow.Description;
                    txtOrder.Text = _ProductCategoryRow.IsSortNull ? string.Empty : _ProductCategoryRow.Sort.ToString();
                    if (!_ProductCategoryRow.IsImageNull)
                    {
                        imganh.ImageUrl = _ProductCategoryRow.Image;
                    }
                    if (_ProductCategoryRow.Active == true)
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

    protected void UpdateProductCategory()
    {
        try
        {
            ProductCategoryRow _ProductCategoryRow = new ProductCategoryRow();
            if (ProductCategory_ID != 0)
            {
                _ProductCategoryRow = BusinessRulesLocator.GetProductCategoryBO().GetByPrimaryKey(ProductCategory_ID);
                if (_ProductCategoryRow != null)
                {
                    _ProductCategoryRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    if (ddlCha.SelectedValue !="0")
                    {
                        _ProductCategoryRow.Parent_ID = Convert.ToInt32(ddlCha.SelectedValue);
                    }
                    
                    _ProductCategoryRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    if (!string.IsNullOrEmpty(txtOrder.Text))
                    {
                        _ProductCategoryRow.Sort = Convert.ToInt32(txtOrder.Text);
                    }
                  
                    _ProductCategoryRow.LastEditedBy = MyUser.GetUser_ID();
                    _ProductCategoryRow.LastEditedDate = DateTime.Now;
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_ProductCategoryRow.IsImageNull)
                        {
                            if (_ProductCategoryRow.Image != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductCategoryRow.Image.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = "../../data/product/" + Common.CreateImgName(fulAnh.FileName);
                        fulAnh.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _ProductCategoryRow.Image = fileimage;
                        }
                    }
                    if (ckActive.Checked)
                    {
                        _ProductCategoryRow.Active = true;
                    }
                    else
                    {
                        _ProductCategoryRow.Active = false;
                    }
                    BusinessRulesLocator.GetProductCategoryBO().Update(_ProductCategoryRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoProductCategory();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProductCategory", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateProductCategory();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductCategory_List.aspx", false);
    }
}