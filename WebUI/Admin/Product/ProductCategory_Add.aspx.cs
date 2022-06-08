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
using Telerik.Web.UI.ImageEditor;

public partial class ProductCategory_Add : System.Web.UI.Page
{
    public string title = "Thêm mới danh mục sản phẩm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLCha();
            txtOrder.Text = Common.GenarateSort("ProductCategory").ToString();
        }
    }

    private void FillDDLCha()
    {
        try
        {
            DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID is null", " Sort ASC");//
            dt = BusinessRulesLocator.Conllection().GetProductCategory();
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

    protected void Clear()
    {
        txtName.Text = txtNote.Text = txtOrder.Text = "";
        ddlCha.SelectedIndex = 0;
    }

    protected void SaveProductCategory()
    {
        try
        {
            ProductCategoryRow _ProductCategoryRow = new ProductCategoryRow();

            _ProductCategoryRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            if (ddlCha.SelectedValue != "0")
            {
                _ProductCategoryRow.Parent_ID = Convert.ToInt32(ddlCha.SelectedValue);
            }

            _ProductCategoryRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                _ProductCategoryRow.Sort = Convert.ToInt32(txtOrder.Text);
            }

            _ProductCategoryRow.CreateBy = MyUser.GetUser_ID();
            _ProductCategoryRow.CreateDate = DateTime.Now;
            _ProductCategoryRow.LastEditedBy = MyUser.GetUser_ID();
            _ProductCategoryRow.LastEditedDate = DateTime.Now;
            _ProductCategoryRow.Language_ID = 1;
            _ProductCategoryRow.CanDelete = true;
            _ProductCategoryRow.Active = true;
            string fileimage = "";
            if (fulAnh.HasFile)
            {
             
                fileimage = "../../data/product/" + Common.CreateImgName(fulAnh.FileName);
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _ProductCategoryRow.Image = fileimage;
                }
            }
            BusinessRulesLocator.GetProductCategoryBO().Insert(_ProductCategoryRow);

            lblMessage.Text = "Thêm mới thông tin thành công!";
            lblMessage.Visible = true;
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
                SaveProductCategory();
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