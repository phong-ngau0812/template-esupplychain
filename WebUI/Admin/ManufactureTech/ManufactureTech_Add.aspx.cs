using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_ManufactureTech_ManufactureTech_Add : System.Web.UI.Page
{
    public string title = "Thêm mới quy trình công nghệ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadProduct();
        }
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    private void LoadProduct()
    {
        try
        {
            string where = string.Empty;

            if (ddlProductBrand.SelectedValue != "")
            {
                where += "And ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProduct", ex.ToString());
        }
    }
    protected void ddlProducBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();
    }
    public string GenerateNameIdent()
    {
        string ProductName = Common.RemoveFont(txtName.Text) + "-" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
        return ProductName;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ManufactureTechRow _ManufactureTechRow = new ManufactureTechRow();           
            _ManufactureTechRow.Code_ID = GenerateNameIdent();
            _ManufactureTechRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _ManufactureTechRow.Name = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text;
            _ManufactureTechRow.Product_ID = GetProductList_ID();
            _ManufactureTechRow.Description = string.IsNullOrEmpty(txtNote.Text) ? "" : txtNote.Text;
            _ManufactureTechRow.CreateBy = MyUser.GetUser_ID();
            _ManufactureTechRow.CreateDate = DateTime.Now;
            _ManufactureTechRow.Active = true;
            BusinessRulesLocator.GetManufactureTechBO().Insert(_ManufactureTechRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
        Clear();
    }
    protected void Clear()
    {
        txtName.Text = "";
        txtNote.Text = "";
    }
    protected string GetProductList_ID()
    {
        string Material_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlProduct.Items)
            {
                if (item.Selected)
                {
                    Material_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Material_ID))
            {
                Material_ID = "," + Material_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetProductList_ID", ex.ToString());
        }
        return Material_ID;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManufactureTech_List.aspx", false);
    }
}