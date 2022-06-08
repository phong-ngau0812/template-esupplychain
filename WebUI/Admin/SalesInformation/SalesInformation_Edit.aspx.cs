﻿using DbObj;
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

public partial class SalesInformation_Edit : System.Web.UI.Page
{
    public string title = "Thông tin hóa đơn bán lẻ";
    public string avatar = "";
    public int SalesInformation_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["SalesInformation_ID"]))
            int.TryParse(Request["SalesInformation_ID"].ToString(), out SalesInformation_ID);
        if (!IsPostBack)
        {
            FillMaterial();

            FillShift();
            FillDDLWorkshop();
            LoadData();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    private void LoadData()
    {
        try
        {
            if (SalesInformation_ID != 0)
            {
                SalesInformationRow _SalesInformationRow = new SalesInformationRow();
                _SalesInformationRow = BusinessRulesLocator.GetSalesInformationBO().GetByPrimaryKey(SalesInformation_ID);
                if (_SalesInformationRow != null)
                {
                    if (!_SalesInformationRow.IsCustomer_IDNull)
                    {
                        ddlType.SelectedValue = "2";
                        divCustomerLe.Visible = false;
                        DivCustomer.Visible = true;
                        ddlType_SelectedIndexChanged(null, null);
                        ddlType.Enabled = false;
                        ddlCustomer.SelectedValue = _SalesInformationRow.Customer_ID.ToString();
                        ddlCustomer.Enabled = false;
                    }
                    else
                    {
                        ddlType.SelectedValue = "1";
                        divCustomerLe.Visible = true;
                        DivCustomer.Visible = false;
                        ddlType.Enabled = false;
                        txtCustomerName.Text = _SalesInformationRow.IsCustomerNameNull ? string.Empty : _SalesInformationRow.CustomerName;
                        txtCustomerName.Enabled = false;
                    }
                }
                txtCode.Text = _SalesInformationRow.IsBarcodeNull ? string.Empty : _SalesInformationRow.Barcode;
                txtCode.Enabled = false;
                txtChietKhau.Text = _SalesInformationRow.IsDiscountNull ? string.Empty : _SalesInformationRow.Discount.ToString();
                txtChietKhau.Enabled = false;
                ddlWorkshop.SelectedValue = _SalesInformationRow.Workshop_ID.ToString();
                ddlWorkshop.Enabled = false;
                ddlCa.SelectedValue = _SalesInformationRow.SalesShift_ID.ToString();
                ddlCa.Enabled = false;

                LoadSGTIN();
                DataTable dtSgtin = BusinessRulesLocator.GetSalesInformationVsProductPackageBO().GetAsDataTable("SalesInformation_ID =" + SalesInformation_ID, "");
                foreach (DataRow value in dtSgtin.Rows)
                {
                    foreach (ListItem item in ddlSGTIN.Items)
                    {
                        if (value["ProductPackage_ID"].ToString() == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                ddlSGTIN.Enabled = false;


                DataTable dtProduct = BusinessRulesLocator.GetSalesInformationVsProductBO().GetAsDataTable("SalesInformation_ID =" + SalesInformation_ID, "");
                foreach (DataRow value in dtProduct.Rows)
                {
                    foreach (ListItem item in ddlProduct.Items)
                    {
                        if (value["Product_ID"].ToString() == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                ddlProduct.Enabled = false;
                tbl.Visible = true;

                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select SP.Unit, SP.Quantity, SP.Price, P.Name from SalesInformationVsProduct SP
  inner join Product P on P.Product_ID=SP.Product_ID
  where P.Active=1 and SP.SalesInformation_ID=" + SalesInformation_ID);
                if (dt.Rows.Count > 0)
                {
                    rptMaterial.DataSource = dt;
                    rptMaterial.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadData", ex.ToString());
        }
    }

    private void FillShift()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {

                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetSalesShiftBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + Convert.ToInt32(MyUser.GetProductBrand_ID()), " Name ASC");
                ddlCa.DataSource = dt;
                ddlCa.DataValueField = "SalesShift_ID";
                ddlCa.DataTextField = "Name";
                ddlCa.DataBind();
                ddlCa.Items.Insert(0, new ListItem("-- Chọn ca--", ""));
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("FillMaterial", ex.ToString());
        }
    }
    private void FillMaterial()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {

                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetProductBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + Convert.ToInt32(MyUser.GetProductBrand_ID()), " Name ASC");
                ddlProduct.DataSource = dt;
                ddlProduct.DataValueField = "Product_ID";
                ddlProduct.DataTextField = "Name";
                ddlProduct.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("FillMaterial", ex.ToString());
        }
    }
    private void FillDDLWorkshop()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetWorkshopBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + Convert.ToInt32(MyUser.GetProductBrand_ID()), " Name ASC");
                ddlWorkshop.DataSource = dt;
                ddlWorkshop.DataTextField = "Name";
                ddlWorkshop.DataValueField = "Workshop_ID";
                ddlWorkshop.DataBind();
                ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên --", ""));
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadSGTIN()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductPackage_ID, SGTIN from ProductPackage  order by ProductPackage_ID DESC");
            ddlSGTIN.DataSource = dt;
            ddlSGTIN.DataTextField = "SGTIN";
            ddlSGTIN.DataValueField = "ProductPackage_ID";
            ddlSGTIN.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    protected string GetSGTIN()
    {
        string SGTIN = string.Empty;
        try
        {
            foreach (ListItem item in ddlSGTIN.Items)
            {
                if (item.Selected)
                {
                    SGTIN += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(SGTIN))
            {
                SGTIN = "," + SGTIN;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetSGTIN", ex.ToString());
        }
        return SGTIN;
    }
    protected string GetProductID()
    {
        string Material = string.Empty;
        try
        {
            foreach (ListItem item in ddlProduct.Items)
            {
                if (item.Selected)
                {
                    Material += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Material))
            {
                Material = "," + Material;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetMaterial", ex.ToString());
        }
        return Material;
    }

    protected void LoadTableProduct()
    {
        if (string.IsNullOrEmpty(GetProductID()))
        {
            rptMaterial.DataSource = null;
            rptMaterial.DataBind();
            tbl.Visible = false;
        }
        else
        {
            tbl.Visible = true;
            string[] array = GetProductID().Split(',');
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Product_ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Columns.Add("Unit");

            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int Material_ID = Convert.ToInt32(value);
                    ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Material_ID);
                    if (_ProductRow != null)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Product_ID"] = Material_ID;
                        _row["Name"] = _ProductRow.Name;
                        _row["Quantity"] = "";
                        _row["Price"] = "";
                        _row["Unit"] = "";
                        dt.Rows.Add(_row);
                    }
                }
            }
            //Response.Write(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                rptMaterial.DataSource = dt;
                rptMaterial.DataBind();
            }
        }
    }


    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTableProduct();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                SalesInformationRow _SalesInformationRow = new SalesInformationRow();
                _SalesInformationRow.Barcode = txtCode.Text.Trim();
                if (ddlType.SelectedValue == "2")
                {
                    if (ddlCustomer.SelectedValue != "0")
                    {
                        _SalesInformationRow.Customer_ID = Convert.ToInt32(ddlCustomer.SelectedValue);
                        _SalesInformationRow.CustomerName = ddlCustomer.SelectedItem.Text;
                    }

                }
                else
                {
                    _SalesInformationRow.CustomerName = txtCustomerName.Text;
                }
                if (!string.IsNullOrEmpty(txtChietKhau.Text))
                {
                    _SalesInformationRow.Discount = Convert.ToInt32(txtChietKhau.Text);
                }
                else
                {
                    _SalesInformationRow.Discount = 0;
                }

                _SalesInformationRow.CreateBy = MyUser.GetUser_ID();
                _SalesInformationRow.CreateDate = DateTime.Now;
                _SalesInformationRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
                _SalesInformationRow.SalesShift_ID = Convert.ToInt32(ddlCa.SelectedValue);
                _SalesInformationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                _SalesInformationRow.Active = 1;

                BusinessRulesLocator.GetSalesInformationBO().Insert(_SalesInformationRow);

                if (!_SalesInformationRow.IsSalesInformation_IDNull)
                {
                    foreach (RepeaterItem item in rptMaterial.Items)
                    {
                        Literal lblProduct_ID = item.FindControl("lblProduct_ID") as Literal;
                        TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                        TextBox txtPrice = item.FindControl("txtPrice") as TextBox;
                        TextBox txtUit = item.FindControl("txtUnit") as TextBox;
                        if (lblProduct_ID != null)
                        {
                            SalesInformationVsProductRow _SalesInformationVsProductRow = new SalesInformationVsProductRow();
                            _SalesInformationVsProductRow.Product_ID = Convert.ToInt32(lblProduct_ID.Text);
                            _SalesInformationVsProductRow.SalesInformation_ID = _SalesInformationRow.SalesInformation_ID;
                            if (!string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                _SalesInformationVsProductRow.Quantity = Convert.ToInt32(txtQuantity.Text.Replace(",", ""));
                            }
                            else
                            {
                                _SalesInformationVsProductRow.Quantity = 0;
                            }
                            if (!string.IsNullOrEmpty(txtPrice.Text))
                            {
                                _SalesInformationVsProductRow.Price = Convert.ToInt32(txtPrice.Text.Replace(",", ""));
                            }
                            else
                            {
                                _SalesInformationVsProductRow.Price = 0;
                            }
                            _SalesInformationVsProductRow.Unit = txtUit.Text;
                            _SalesInformationVsProductRow.CreateBy = MyUser.GetUser_ID();
                            _SalesInformationVsProductRow.CreateDate = DateTime.Now;
                            BusinessRulesLocator.GetSalesInformationVsProductBO().Insert(_SalesInformationVsProductRow);
                        }
                    }

                    //SGTIN

                    string[] array = GetSGTIN().Split(',');

                    foreach (string value in array)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            SalesInformationVsProductPackageRow _SalesInformationVsProductPackageRow = new SalesInformationVsProductPackageRow();
                            _SalesInformationVsProductPackageRow.SalesInformation_ID = _SalesInformationRow.SalesInformation_ID;
                            _SalesInformationVsProductPackageRow.ProductPackage_ID = Convert.ToInt32(value);
                            _SalesInformationVsProductPackageRow.CreateBy = MyUser.GetUser_ID();
                            _SalesInformationVsProductPackageRow.CreateDate = DateTime.Now;
                            BusinessRulesLocator.GetSalesInformationVsProductPackageBO().Insert(_SalesInformationVsProductPackageRow);
                        }
                    }
                }
                // lblMessage.Text = "Cập nhật thông tin thành công!";
                Response.Redirect("SalesInformation_List", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "2")
        {
            DivCustomer.Visible = true;
            divCustomerLe.Visible = false;
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetCustomerBO().GetAsDataTable(" ProductBrand_ID=" + MyUser.GetProductBrand_ID(), "");
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "Customer_ID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("-- Chọn khách hàng--", "0"));
        }
        else
        {
            DivCustomer.Visible = false;
            divCustomerLe.Visible = true;
            txtChietKhau.Text = "";
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedValue != "0")
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL([Percent],0) as [Percent] from Discount where ProductBrand_ID=" + MyUser.GetProductBrand_ID() +
  " and  Discount_ID= (select Discount_ID from CustomerType where CustomerType_ID=(select CustomerType_ID from Customer where Customer_ID=" + ddlCustomer.SelectedValue + "))");
            if (dt.Rows.Count == 1)
            {
                txtChietKhau.Text = dt.Rows[0]["Percent"].ToString();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesInformation_List", false);
    }
}