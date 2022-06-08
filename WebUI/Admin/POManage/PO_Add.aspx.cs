
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
using Telerik.Web.UI.Skins;

public partial class Admin_POManage_PO_Add : Page
{
    public string title = "Thêm mới danh sách PO ";
    public string avatar = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!IsPostBack)
        {
            LoadProductBrand();
            FillDDLCustomer();
            FillDDLProduct();
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);


    }
    private void LoadProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            Common.FillProductBrand(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductBrand", ex.ToString());
        }
    }
    protected void FillDDLProduct()
    {
        string where = string.Empty;

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and ProductBrand_ID =  " + ddlProductBrand.SelectedValue;
        }
        DataTable dt = BusinessRulesLocator.GetProductBO().GetAsDataTable("Active = 1 " + where, " CreateDate DESC");
        ddlProduct.DataSource = dt;
        ddlProduct.DataValueField = "Product_ID";
        ddlProduct.DataTextField = "Name";
        ddlProduct.DataBind();

    }
    protected void FillDDLCustomer()
    {
        string where = string.Empty;
        if (ddlProductBrand.SelectedValue != "")
        {
            where += " ProductBrand_ID = " + ddlProductBrand.SelectedValue;
        }
        DataTable dt = BusinessRulesLocator.GetCustomerBO().GetAsDataTable("" + where, " CreateDate DESC");
        ddlCustomer.DataSource = dt;
        ddlCustomer.DataValueField = "Customer_ID";
        ddlCustomer.DataTextField = "Name";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new ListItem("-- Khách hàng --", "0"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddPOManage_PO();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected bool CheckPO(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name =N'" + Name + "'";
            DataTable dt = new DataTable();
            //   dt = BusinessRulesLocator.GetPOManageBO().GetAsDataTable(where, "");

            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }

    protected void AddPOManage_PO()
    {
        try
        {

            POManageRow _POManageRow = new POManageRow();


            _POManageRow.Code = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;

            _POManageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _POManageRow.Customer_ID = Convert.ToInt32(ddlCustomer.SelectedValue);
            _POManageRow.Content = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _POManageRow.Description = string.IsNullOrEmpty(txtSummary.Text) ? string.Empty : txtSummary.Text;

            if (ckActive.Checked)
            {
                _POManageRow.Active = true;
            }
            else
            {
                _POManageRow.Active = false;
            }


            _POManageRow.CreateBy = MyUser.GetUser_ID();
            _POManageRow.CreateDate = DateTime.ParseExact(txtDateStart.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            BusinessRulesLocator.GetPOManageBO().Insert(_POManageRow);
            if (!_POManageRow.IsPO_IDNull)
            {
                UpdatePOVsProduct(_POManageRow.PO_ID);
            }
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;

            //Response.Redirect("PO_List.aspx", false);




        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateChain", ex.ToString());
        }
    }

    private void UpdatePOVsProduct(int PO_ID)
    {
        try
        {
            foreach (RepeaterItem item in rptProduct.Items)
            {
                Literal lblProduct_ID = item.FindControl("lblProduct_ID") as Literal;
                TextBox txtContent = item.FindControl("txtContent") as TextBox;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = item.FindControl("txtPrice") as TextBox;
                TextBox txtTotalPrice = item.FindControl("txtTotalPrice") as TextBox;
                Telerik.Web.UI.RadDatePicker RadDatePicker1 = item.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker;
                if (lblProduct_ID.Text != "")
                {
                    POVsProductRow _POVsProductRow = new POVsProductRow();
                    _POVsProductRow.Product_ID = Convert.ToInt32(lblProduct_ID.Text);
                    _POVsProductRow.POManage_ID = PO_ID;
                    _POVsProductRow.Description = string.IsNullOrEmpty(txtContent.Text) ? string.Empty : txtContent.Text;
                    _POVsProductRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToInt32(txtPrice.Text.Replace(",", ""));
                    _POVsProductRow.Amount = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt32(txtQuantity.Text);
                    _POVsProductRow.TotalPrice = string.IsNullOrEmpty(txtTotalPrice.Text) ? 0 : Convert.ToInt32(txtTotalPrice.Text.ToString().Replace("VND", "").Replace(".", "").Trim());
                    if (!string.IsNullOrEmpty(RadDatePicker1.SelectedDate.ToString()))
                    {
                        _POVsProductRow.SendDate = DateTime.ParseExact(RadDatePicker1.SelectedDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    BusinessRulesLocator.GetPOVsProductBO().Insert(_POVsProductRow);
                }


            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProductInPO", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PO_List.aspx", false);
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTableProduct();
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLCustomer();
        FillDDLProduct();
    }
    protected void LoadTableProduct()
    {
        try
        {
            DataTable dtProduct = new DataTable();

            dtProduct.Clear();
            dtProduct.Columns.Add("Product_ID");
            dtProduct.Columns.Add("ProductName");
            dtProduct.Columns.Add("Description");
            dtProduct.Columns.Add("SendDate");
            dtProduct.Columns.Add("Amount");
            dtProduct.Columns.Add("Price");
            dtProduct.Columns.Add("TotalPrice");

            string[] arrayProduct = GetProduct().Split(',');

            if (!string.IsNullOrEmpty(GetProduct()))
            {

                foreach (string value in arrayProduct)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int Product_ID = Convert.ToInt32(value);
                        ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                        if (_ProductRow != null)
                        {
                            DataRow _row = dtProduct.NewRow();
                            _row["Product_ID"] = Product_ID;
                            _row["ProductName"] = _ProductRow.IsNameNull ? string.Empty : _ProductRow.Name;
                            _row["Price"] = string.IsNullOrEmpty(GetPriceProduct(_ProductRow.Product_ID)) ? "0" : GetPriceProduct(_ProductRow.Product_ID);
                            dtProduct.Rows.Add(_row);
                        }
                    }
                }
                if (dtProduct.Rows.Count > 0)
                {
                    Data1.Visible = true;
                    rptProduct.DataSource = dtProduct;
                    rptProduct.DataBind();


                    foreach (RepeaterItem item in rptProduct.Items)
                    {
                        Telerik.Web.UI.RadDatePicker RadDatePicker1 = item.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker;
                        RadDatePicker1.Culture = new CultureInfo("vi-VN");

                    }
                }
            }
            else
            {
                Data1.Visible = false;
                rptProduct.DataSource = null;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadTableProduct", ex.ToString());
        }

    }
    protected string GetPriceProduct(int Product_ID)
    {
        string Price = string.Empty;

        DataTable dt = BusinessRulesLocator.GetProductInfoBO().GetAsDataTable("Product_ID = " + Product_ID, "");
        if (dt.Rows.Count > 0)
        {
            Price = dt.Rows[0]["Price"].ToString();
        }

        return Price;
    }
    protected string GetProduct()
    {
        string Product_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlProduct.Items)
            {
                if (item.Selected)
                {
                    Product_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Product_ID))
            {
                Product_ID = "," + Product_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return Product_ID;
    }
}