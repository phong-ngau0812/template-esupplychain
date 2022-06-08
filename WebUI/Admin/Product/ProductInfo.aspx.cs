using DbObj;
using evointernal;
using QRCoder;
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

public partial class ProductInfo : System.Web.UI.Page
{
    public int Product_ID = 0;
    public string title = "Thông tin sản phẩm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        _FileBrowser.SetupCKEditor(txtCongDung);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        //if (!IsPostBack)
        {
            FillInfoProduct();
        }
    }


    protected void FillInfoProduct()
    {
        try
        {
            if (Product_ID != 0)
            {
                ProductRow _ProductRow = new ProductRow();
                _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                if (_ProductRow != null)
                {
                    title += ": " + _ProductRow.Name;
                    if (!IsPostBack)
                    {
                        //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                        MyActionPermission.CheckPermission(_ProductRow.ProductBrand_ID.ToString(), _ProductRow.CreateBy.ToString(), "/Admin/Product/Product_List");



                        //Kiểm tra nếu chưa có thông tin product thì insert bản ghi mới
                        DataTable dt = BusinessRulesLocator.GetProductInfoBO().GetAsDataTable("Product_ID =" + Product_ID, "");
                        ProductInfoRow _ProductInfoRow = new ProductInfoRow();
                        if (dt.Rows.Count != 1)
                        {
                            //Fill info product

                            _ProductInfoRow.Product_ID = Product_ID;
                            _ProductInfoRow.Price = 0;
                            _ProductInfoRow.Discount = 0;
                            _ProductInfoRow.CreateBy = MyUser.GetUser_ID();
                            _ProductInfoRow.CreateDate = DateTime.Now;
                            BusinessRulesLocator.GetProductInfoBO().Insert(_ProductInfoRow);
                            _ProductInfoRow = BusinessRulesLocator.GetProductInfoBO().GetByPrimaryKey(_ProductInfoRow.ProductInfo_ID);
                        }
                        else
                        {
                            _ProductInfoRow = BusinessRulesLocator.GetProductInfoBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["ProductInfo_ID"]));
                        }

                        txtPrice.Text = _ProductInfoRow.IsPriceNull ? string.Empty : _ProductInfoRow.Price.ToString("N0");
                        txtDiscount.Text = _ProductInfoRow.IsDiscountNull ? string.Empty : _ProductInfoRow.Discount.ToString();
                        ddlOCOP.SelectedValue = _ProductInfoRow.IsOCOPNull ? "0" : _ProductInfoRow.OCOP.ToString();
                        txtNote.Text = _ProductInfoRow.IsDescriptionNull ? string.Empty : _ProductInfoRow.Description;
                        txtCongDung.Text = _ProductInfoRow.IsUsesNull ? string.Empty : _ProductInfoRow.Uses;
                        txtYoutube.Text = _ProductInfoRow.IsVideoYoutubeNull ? string.Empty : _ProductInfoRow.VideoYoutube;
                        txtHSD.Text = _ProductInfoRow.IsExpirationDateNull ? string.Empty : _ProductInfoRow.ExpirationDate;
                        txtDate.Text = _ProductInfoRow.IsExpirationByDateNull ? "0" : _ProductInfoRow.ExpirationDate;
                        txtMonth.Text = _ProductInfoRow.IsGuaranteeByMonthNull ? "0" : _ProductInfoRow.GuaranteeByMonth.ToString();
                        txtTag.Text = _ProductInfoRow.IsTagsNull ? string.Empty : _ProductInfoRow.Tags.ToString();
                        LoadImg();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }
    protected void LoadImg()
    {
        DataTable dtImg = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Product_ID=" + Product_ID + " and Type=" + ddlType.SelectedValue, " SORT asc");
        rptImg.DataSource = dtImg;
        rptImg.DataBind();
    }
    protected void UpdateProduct()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.GetProductInfoBO().GetAsDataTable("Product_ID =" + Product_ID, "");
            if (dt.Rows.Count == 1)
            {
                ProductInfoRow _ProductInfoRow = new ProductInfoRow();
                _ProductInfoRow = BusinessRulesLocator.GetProductInfoBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["ProductInfo_ID"]));
                _ProductInfoRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToInt32(txtPrice.Text.Replace(",", ""));
                _ProductInfoRow.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToInt32(txtDiscount.Text);
                if (ddlOCOP.SelectedValue != "0")
                {
                    _ProductInfoRow.OCOP = Convert.ToInt32(ddlOCOP.SelectedValue);
                }
                _ProductInfoRow.VideoYoutube = string.IsNullOrEmpty(txtYoutube.Text) ? "" : txtYoutube.Text;
                _ProductInfoRow.Description = string.IsNullOrEmpty(txtNote.Text) ? "" : txtNote.Text;
                _ProductInfoRow.Uses = string.IsNullOrEmpty(txtCongDung.Text) ? "" : txtCongDung.Text;
                _ProductInfoRow.ExpirationDate = string.IsNullOrEmpty(txtHSD.Text) ? "" : txtHSD.Text;
                _ProductInfoRow.ExpirationByDate = Convert.ToInt32(string.IsNullOrEmpty(txtDate.Text) ? "0" : txtDate.Text);
                _ProductInfoRow.GuaranteeByMonth = Convert.ToInt32(string.IsNullOrEmpty(txtMonth.Text) ? "0" : txtMonth.Text);
                _ProductInfoRow.Tags = string.IsNullOrEmpty(txtTag.Text) ? "" : txtTag.Text;
                _ProductInfoRow.LastEditBy = MyUser.GetUser_ID();
                _ProductInfoRow.LastEditDate = DateTime.Now;
                BusinessRulesLocator.GetProductInfoBO().Update(_ProductInfoRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                foreach (RepeaterItem item in rptImg.Items)
                {
                    Literal lblID = item.FindControl("lblID") as Literal;
                    TextBox txtOrder = item.FindControl("txtOrder") as TextBox;
                    if (lblID != null)
                    {
                        ProductGalleryRow _ProductGalleryRow = BusinessRulesLocator.GetProductGalleryBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                        if (_ProductGalleryRow != null)
                        {
                            _ProductGalleryRow.Sort = string.IsNullOrEmpty(txtOrder.Text) ? 0 : Convert.ToInt32(txtOrder.Text);
                            BusinessRulesLocator.GetProductGalleryBO().Update(_ProductGalleryRow);
                        }
                    }
                }
                LoadImg();
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateProduct();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product_List.aspx", false);
    }

    protected void rptImg_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Delete":
                BusinessRulesLocator.GetProductGalleryBO().DeleteByPrimaryKey(ID);
                LoadImg();
                break;
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadImg();
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("https://esupplychain.vn/p/"+Product_ID,false);
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "Tải ảnh thành công!";
        lblMessage.Visible = true;
        LoadImg();
    }
}