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

public partial class Admin_POManage_PO_Edit : System.Web.UI.Page
{
    int PO_ID = 0;
    public string title = "Thông tin danh sách PO";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!string.IsNullOrEmpty(Request["PO_ID"]))
            int.TryParse(Request["PO_ID"].ToString(), out PO_ID);
        if (!IsPostBack)
        {
            LoadProductBrand();
            FillDDLCustomer();
            FillInfoCustomer();
        }

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
    private void FillInfoCustomer()
    {
        try
        {
            if (PO_ID != 0)
            {
                POManageRow _POManageRow = new POManageRow();
                _POManageRow = BusinessRulesLocator.GetPOManageBO().GetByPrimaryKey(PO_ID);

                if (_POManageRow != null)
                {
                    txtName.Text = _POManageRow.IsCodeNull ? string.Empty : _POManageRow.Code;
                    ddlCustomer.SelectedValue = _POManageRow.IsCustomer_IDNull ? "" : _POManageRow.Customer_ID.ToString();
                    ddlProductBrand.SelectedValue = _POManageRow.IsProductBrand_IDNull ? "" : _POManageRow.ProductBrand_ID.ToString();

                    //LoadProduct
                    FillDDLProduct();
                    DataTable dt = BusinessRulesLocator.GetPOVsProductBO().GetAsDataTable("POManage_ID = " + _POManageRow.PO_ID, "");
                    if (dt.Rows.Count > 0)
                    {
                        Data1.Visible = true;
                        rptProduct.DataSource = dt;
                        rptProduct.DataBind();
                        foreach (DataRow dtRow in dt.Rows)
                        {
                            foreach (ListItem item in ddlProduct.Items)
                            {
                                if (dtRow["Product_ID"].ToString() == item.Value.ToString())
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                        foreach (RepeaterItem item in rptProduct.Items)
                        {
                            Telerik.Web.UI.RadDatePicker RadDatePicker1 = item.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker;
                            RadDatePicker1.Culture = new CultureInfo("vi-VN");

                        }
                    }
                    txtDateStart.Text = _POManageRow.IsCreateDateNull ? string.Empty : _POManageRow.CreateDate.ToString("dd/MM/yyyy");
                    txtSummary.Text = _POManageRow.IsDescriptionNull ? string.Empty : _POManageRow.Description;
                    txtNote.Text = _POManageRow.IsContentNull ? string.Empty : _POManageRow.Content;


                    if (_POManageRow.Active)
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
                UpdatePOManage_PO();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
        //Response.Redirect("PO_List.aspx", false);
    }

    private void UpdatePOManage_PO()
    {
        try
        {
            POManageRow _POManageRow = new POManageRow();
            if (PO_ID != 0)
            {
                _POManageRow = BusinessRulesLocator.GetPOManageBO().GetByPrimaryKey(PO_ID);
                if (_POManageRow != null)
                {
                    _POManageRow.Code = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _POManageRow.Content = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;


                    if (ddlCustomer.SelectedValue != "")
                    {
                        _POManageRow.Customer_ID = Convert.ToInt32(ddlCustomer.SelectedValue);
                    }
                    if (ddlProductBrand.SelectedValue != "")
                    {
                        _POManageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    }
                    _POManageRow.Description = string.IsNullOrEmpty(txtSummary.Text) ? string.Empty : txtSummary.Text;

                    if (ckActive.Checked)
                    {
                        _POManageRow.Active = true;
                    }
                    else
                    {
                        _POManageRow.Active = false;
                    }
                    _POManageRow.CreateDate = DateTime.ParseExact(txtDateStart.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _POManageRow.LastEditBy = MyUser.GetUser_ID();
                    _POManageRow.LastCreateDate = DateTime.Now;
                }
                if (BusinessRulesLocator.GetPOManageBO().Update(_POManageRow))
                {
                    UpdatePOVsProduct(_POManageRow.PO_ID);
                }
                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.Visible = true;
                FillInfoCustomer();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdatePOManage_PO", ex.ToString());
        }
    }
    private void UpdatePOVsProduct(int PO_ID)
    {
        try
        {
            foreach (RepeaterItem item in rptProduct.Items)
            {

                Literal lblProduct_ID = item.FindControl("lblProduct_ID") as Literal;
                Literal lblPOVsProduct_ID = item.FindControl("lblPOVsProduct_ID") as Literal;
                TextBox txtContent = item.FindControl("txtContent") as TextBox;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                TextBox txtPrice = item.FindControl("txtPrice") as TextBox;
                TextBox txtTotalPrice = item.FindControl("txtTotalPrice") as TextBox;
                Telerik.Web.UI.RadDatePicker RadDatePicker1 = item.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker;
                if (!string.IsNullOrEmpty(lblPOVsProduct_ID.Text))
                {
                    POVsProductRow _POVsProductRow = BusinessRulesLocator.GetPOVsProductBO().GetByPrimaryKey(Convert.ToInt32(lblPOVsProduct_ID.Text));
                    _POVsProductRow.Product_ID = Convert.ToInt32(lblProduct_ID.Text);
                    _POVsProductRow.POManage_ID = PO_ID;
                    _POVsProductRow.Description = string.IsNullOrEmpty(txtContent.Text) ? string.Empty : txtContent.Text;
                    _POVsProductRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToInt32(txtPrice.Text.Replace(",", ""));
                    _POVsProductRow.Amount = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt32(txtQuantity.Text);
                    _POVsProductRow.TotalPrice = string.IsNullOrEmpty(txtTotalPrice.Text) ? 0 : Convert.ToInt32(txtTotalPrice.Text.ToString().Replace("VND", "").Replace(".", "").Replace(",", "").Trim());
                    if (!string.IsNullOrEmpty(RadDatePicker1.SelectedDate.ToString()))
                    {
                        _POVsProductRow.SendDate = DateTime.ParseExact(RadDatePicker1.SelectedDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    BusinessRulesLocator.GetPOVsProductBO().Update(_POVsProductRow);
                }
                else
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
        FillDDLProduct();
        FillDDLCustomer();
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
    protected string NameProduct(string Product_ID)
    {
        return BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Convert.ToInt32(Product_ID)).Name;
    }
    protected void LoadTableProduct()
    {
        try
        {
            DataTable dtProduct = new DataTable();

            dtProduct.Clear();
            dtProduct.Columns.Add("POVsProduct_ID");
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
                        DataTable dtCheck = BusinessRulesLocator.GetPOVsProductBO().GetAsDataTable("Product_ID =" + value + " and POManage_ID = " + PO_ID, "");
                        if (dtCheck.Rows.Count > 0)
                        {
                            DataRow _row = dtProduct.NewRow();
                            _row["POVsProduct_ID"] = dtCheck.Rows[0]["POVsProduct_ID"].ToString();
                            _row["Product_ID"] = dtCheck.Rows[0]["Product_ID"].ToString();
                            _row["Description"] = dtCheck.Rows[0]["Description"].ToString();
                            _row["SendDate"] = dtCheck.Rows[0]["SendDate"].ToString();
                            _row["Amount"] = dtCheck.Rows[0]["Amount"].ToString();
                            _row["Price"] = dtCheck.Rows[0]["Price"].ToString();
                            _row["TotalPrice"] = dtCheck.Rows[0]["TotalPrice"].ToString();
                            dtProduct.Rows.Add(_row);
                        }
                        else
                        {
                            int Product_ID = Convert.ToInt32(value);
                            ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                            if (_ProductRow != null)
                            {
                                DataRow _row = dtProduct.NewRow();
                                _row["POVsProduct_ID"] = "";
                                _row["Product_ID"] = Product_ID;
                                _row["Description"] = "";
                                _row["SendDate"] = "";
                                _row["Amount"] = "0";
                                _row["Price"] = "0";
                                _row["TotalPrice"] = "0";
                                dtProduct.Rows.Add(_row);
                            }
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