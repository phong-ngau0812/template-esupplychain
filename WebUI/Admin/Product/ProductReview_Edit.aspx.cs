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

public partial class ProductReview_Edit : System.Web.UI.Page
{
    int ProductReview_ID = 0;
    int Product_ID = 0;
    public string title = "Thông tin bình luận";
    public string avatar = "";
    public string NameProduct = "Quản lý bình luận sản phẩm: ";
    public string BackLink = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!string.IsNullOrEmpty(Request["ProductReview_ID"]))
            int.TryParse(Request["ProductReview_ID"].ToString(), out ProductReview_ID);
        if (!IsPostBack)
        {
            FillProduct();
            FillProductReview();
        }
    }

    protected void FillProduct()
    {
        try
        {
            if (Product_ID != 0)
            {
                ProductRow _ProductRow = new ProductRow();
                _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                if (_ProductRow != null)
                {
                    NameProduct += _ProductRow.IsNameNull ? string.Empty : _ProductRow.Name;
                    BackLink = "<a href='ProductReview_List.aspx?Product_ID=" + Product_ID + "'</a>"+ NameProduct ;
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProduct", ex.ToString());
        }

    }

    protected void FillProductReview()
    {
        try
        {
            if (ProductReview_ID != 0)
            {
                ProductReviewRow _ProductReviewRow = new ProductReviewRow();
                _ProductReviewRow = BusinessRulesLocator.GetProductReviewBO().GetByPrimaryKey(ProductReview_ID);

                if (_ProductReviewRow != null)
                {
                    txtFullName.Text = _ProductReviewRow.IsFullNameNull ? string.Empty : _ProductReviewRow.FullName;
                    txtTitle.Text = _ProductReviewRow.IsTitleNull ? string.Empty : _ProductReviewRow.Title;
                    txtCreateDate.Text = _ProductReviewRow.IsCreateDateNull ? string.Empty :  DateTime.Parse(_ProductReviewRow.CreateDate.ToString()).ToString("dd/MM/yyyy");
                    txtDescription.Text = _ProductReviewRow.IsDescriptionNull ? string.Empty : _ProductReviewRow.Description;
                    txtApprovedDate.Text = _ProductReviewRow.IsApprovedDateNull ? string.Empty : DateTime.Parse(_ProductReviewRow.ApprovedDate.ToString()).ToString("dd/MM/yyyy");

                    if (!_ProductReviewRow.IsApprovedUserNull)
                    {
                        txtApprovedUser.Text = MyUser.UserNameFromUser_ID(_ProductReviewRow.ApprovedUser.ToString());
                    }

                    if (_ProductReviewRow.Approved == 1)
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
            Log.writeLog("FillProductReview", ex.ToString());
        }
    }


    protected void UpdateProductReview()
    {
        try
        {
            ProductReviewRow _ProductReviewRow = new ProductReviewRow();
            if (ProductReview_ID != 0)
            {
                _ProductReviewRow = BusinessRulesLocator.GetProductReviewBO().GetByPrimaryKey(ProductReview_ID);
                if (_ProductReviewRow != null)
                {

                  
                    if (ckActive.Checked)
                    {
                        _ProductReviewRow.Approved = 1;
                    }
                    else
                    {
                        _ProductReviewRow.Approved = 0;
                    }


                    _ProductReviewRow.ApprovedUser = MyUser.GetUser_ID();
                    _ProductReviewRow.ApprovedDate = DateTime.Now;
                    BusinessRulesLocator.GetProductReviewBO().Update(_ProductReviewRow);
                    Response.Redirect("ProductReview_List.aspx?Product_ID=" + Product_ID, false);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
               
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateAgecy", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateProductReview();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductReview_List.aspx?Product_ID=" + Product_ID, false);
    }


}