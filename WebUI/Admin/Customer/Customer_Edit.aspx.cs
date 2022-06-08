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

public partial class Customer_Edit : System.Web.UI.Page
{
    int Customer_ID = 0;
    public string title = "Thông tin khách hàng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["Customer_ID"]))
            int.TryParse(Request["Customer_ID"].ToString(), out Customer_ID);
        if (!IsPostBack)
        {
            
            
            FillDDLddlProductBrand();
            FillInfoCustomer();
        }
    }


   
    protected void FillInfoCustomer()
    {
        try
        {
            if (Customer_ID != 0)
            {
                CustomerRow _CustomerRow = new CustomerRow();
                _CustomerRow = BusinessRulesLocator.GetCustomerBO().GetByPrimaryKey(Customer_ID);

                if (_CustomerRow != null)
                {

                    
                    ddlProductBrand.SelectedValue = _CustomerRow.ProductBrand_ID.ToString();
                    FillDDLddlCustomerType();
                    ddlCustomerType.SelectedValue = _CustomerRow.CustomerType_ID.ToString();
                    txtFullName.Text = _CustomerRow.IsNameNull ? string.Empty : _CustomerRow.Name;
                    txtGSRN.Text = _CustomerRow.IsGSRNNull ? string.Empty : _CustomerRow.GSRN;
                    if (!_CustomerRow.IsLogoNull)
                    {
                        avatar = _CustomerRow.Logo;
                        if (avatar != null)
                        {
                            imganh.ImageUrl = avatar;
                        }
                        else
                        {
                            avatar = "../../images/no-image-icon.png";
                        }
                    }

                    txtEmail.Text = _CustomerRow.IsEmailNull ? string.Empty : _CustomerRow.Email;
                    txtAddress.Text = _CustomerRow.IsAddressNull ? string.Empty : _CustomerRow.Address;
                    txtPhone.Text = _CustomerRow.IsPhoneNull ? string.Empty : _CustomerRow.Phone;
                    txttWebsite.Text = _CustomerRow.IsWebsiteNull ? string.Empty : _CustomerRow.Website;
                    txtNote.Text = _CustomerRow.IsDescriptionNull ? string.Empty : _CustomerRow.Description;

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    private void FillDDLddlCustomerType()
    {
        try
        {
            string where = " Active<>-1";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetCustomerTypeBO().GetAsDataTable(where, " Sort ASC");
            ddlCustomerType.DataSource = dt;
            ddlCustomerType.DataTextField = "Name";
            ddlCustomerType.DataValueField = "CustomerType_ID";
            ddlCustomerType.DataBind();
            ddlCustomerType.Items.Insert(0, new ListItem("-- Chọn nhóm khách hàng --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void UpdateCustomer()
    {
        try
        {
            CustomerRow _CustomerRow = new CustomerRow();
            if (Customer_ID != 0)
            {
                _CustomerRow = BusinessRulesLocator.GetCustomerBO().GetByPrimaryKey(Customer_ID);
                if (_CustomerRow != null)
                {
                    _CustomerRow.CustomerType_ID =  Convert.ToInt32(ddlCustomerType.SelectedValue);
                    //_CustomerRow.ProductBrandList_ID =  GetProductBrandList_ID();
                    _CustomerRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _CustomerRow.Name = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;

                    _CustomerRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    _CustomerRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _CustomerRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
                    _CustomerRow.Website = string.IsNullOrEmpty(txttWebsite.Text) ? string.Empty : txttWebsite.Text;
                    _CustomerRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_CustomerRow.IsLogoNull)
                        {
                            if (_CustomerRow.Logo != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _CustomerRow.Logo.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = "../../data/customer/" + Customer_ID + "-"+(fulAnh.FileName);
                        fulAnh.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _CustomerRow.Logo = fileimage;
                        }
                    }

                    _CustomerRow.LastEditBy = MyUser.GetUser_ID();
                    _CustomerRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetCustomerBO().Update(_CustomerRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoCustomer();
                Response.Redirect("Customer_List.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomer", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateCustomer();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customer_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlCustomerType();
    }
}