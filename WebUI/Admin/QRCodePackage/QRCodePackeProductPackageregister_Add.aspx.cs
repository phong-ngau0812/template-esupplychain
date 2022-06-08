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

public partial class QRCodePackeProductPackageregister_Add : System.Web.UI.Page
{
   
    public string NameProductPackageQR = "";
    public string NameProductQR = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    public string Message = "";
    private int ProductBrand_ID = 0;
    private int QRCodePackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            LoadQRCodePackageID();
            FillDDLddlProductBrand();
            FillDDlProduct();
            FillDDlProductPackage();
            LoadItem();
        }
    }

    private void LoadQRCodePackageID()
    {
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                NameProductPackageQR = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProductQR = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;  

            }
        }

    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
     private void FillDDlProduct()
    {
        try
        {
            string where = "1=1";

            if (ProductBrand_ID != 0)
            {
                where += "and ProductBrand_ID =" + ProductBrand_ID;

            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBO().GetAsDataTable(where, "");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDlProduct", ex.ToString());
        }

    }

    private void FillDDlProductPackage()
    {
        try
        {
            string where = "1=1";
            if (ProductBrand_ID != 0)
            {
                where += "and ProductBrand_ID =" + ProductBrand_ID;
            }
            if (ddlProduct.SelectedValue != "")
            {
                where += "and Product_ID =" + ddlProduct.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.Items.Insert(0, new ListItem("-- Chọn lô sản xuất --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDlProductPackage", ex.ToString());
        }

    }

    private void LoadItem()
    {
        try
        {
            if (ddlProductPackage.SelectedValue != "")
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductItem_ID, QRCodePublicContent from ProductItem where  ProductPackage_ID=" + Convert.ToInt32(ddlProductPackage.SelectedValue));
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.RemoveAt(0);
                }
                ddlItem.DataSource = dt;
                ddlItem.DataTextField = "QRCodePublicContent";
                ddlItem.DataValueField = "ProductItem_ID";
                ddlItem.DataBind();
                Count.Value = dt.Rows.Count.ToString();
                //Count.Value = dt.Rows.Count.ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
               
               
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeProductPackageregister_List.aspx", false);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDlProductPackage();
    }

    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItem();
    }
}