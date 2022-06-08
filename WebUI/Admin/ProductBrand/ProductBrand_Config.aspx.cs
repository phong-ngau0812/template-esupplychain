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

public partial class ProductBrand_Config : System.Web.UI.Page
{
    public int ProductBrand_ID = 0;
    public string title = "Cấu hình lô mã doanh nghiệp";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductBrand_ID"]))
            int.TryParse(Request["ProductBrand_ID"].ToString(), out ProductBrand_ID);
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        try
        {
            if (ProductBrand_ID != 0)
            {
                ProductBrandRow _ProductBrandRow = new ProductBrandRow();
                _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
                if (_ProductBrandRow != null)
                {
                    title += ": " + _ProductBrandRow.Name;
                    if (MyUser.GetFunctionGroup_ID() == "1")
                    {
                        //Kiểm tra nếu chưa có thông tin cấu hình lô mã thì insert bản ghi mới
                        DataTable dt = BusinessRulesLocator.GetQRCodePackageRuleBO().GetAsDataTable("ProductBrand_ID =" + ProductBrand_ID, "");
                        QRCodePackageRuleRow _QRCodePackageRuleRow = new QRCodePackageRuleRow();
                        if (dt.Rows.Count != 1)
                        {
                            _QRCodePackageRuleRow.ProductBrand_ID = ProductBrand_ID;
                            _QRCodePackageRuleRow.Number = 0;
                            _QRCodePackageRuleRow.IsCreate = true;
                            _QRCodePackageRuleRow.IsDownload = true;
                            _QRCodePackageRuleRow.CreateBy = MyUser.GetUser_ID();
                            _QRCodePackageRuleRow.CreateDate = DateTime.Now;
                            txtNumber.Text = "0";
                            BusinessRulesLocator.GetQRCodePackageRuleBO().Insert(_QRCodePackageRuleRow);
                        }
                        else
                        {
                            txtNumber.Text = dt.Rows[0]["Number"].ToString();
                            ckActive.Checked = bool.Parse(dt.Rows[0]["IsCreate"].ToString());
                            ckDownload.Checked = bool.Parse(dt.Rows[0]["IsDownload"].ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("/Admin/ProductBrand/ProductBrand_List", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    protected void UpdateProduct()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.GetQRCodePackageRuleBO().GetAsDataTable("ProductBrand_ID =" + ProductBrand_ID, "");
            if (dt.Rows.Count == 1)
            {
                QRCodePackageRuleRow _QRCodePackageRuleRow = new QRCodePackageRuleRow();
                _QRCodePackageRuleRow = BusinessRulesLocator.GetQRCodePackageRuleBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["QRCodePackageRule_ID"]));
                _QRCodePackageRuleRow.Number = Convert.ToInt32(txtNumber.Text);
                _QRCodePackageRuleRow.IsCreate = ckActive.Checked;
                _QRCodePackageRuleRow.IsDownload = ckDownload.Checked;
                _QRCodePackageRuleRow.LastEditBy = MyUser.GetUser_ID();
                _QRCodePackageRuleRow.LastEditDate = DateTime.Now;
                BusinessRulesLocator.GetQRCodePackageRuleBO().Update(_QRCodePackageRuleRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
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
        Response.Redirect("ProductBrand_List.aspx", false);
    }

}