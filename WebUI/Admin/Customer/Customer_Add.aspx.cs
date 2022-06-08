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

public partial class Customer_Add : System.Web.UI.Page
{
    public string title = "Thêm mới khách hàng ";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlCustomerType();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddCustomer();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    //protected bool CheckStaffPhone(string Phone)
    //{
    //    bool flag = true;
    //    string where = "Active <>-1";
    //    if (!string.IsNullOrEmpty(Phone))
    //    {
    //        where += " and Phone=N'" + Phone.Trim() + "'";
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.GetStaffBO().GetAsDataTable(where, "");
    //        if (dt.Rows.Count > 0)
    //        {
    //            flag = false;
    //        }

    //    }


    //    return flag;
    //}
    protected void UpdateCustomer(int Customer_ID)
    {
        CustomerRow _CustomerRoww = new CustomerRow();
        _CustomerRoww = BusinessRulesLocator.GetCustomerBO().GetByPrimaryKey(Customer_ID);
        string fileimage = "";
        if (_CustomerRoww != null)
        {
            _CustomerRoww.GSRN = "GSRN-" + _CustomerRoww.Customer_ID.ToString();

            if (fulAnh.HasFile)
            {
                ////Xóa file
                //if (UserProfile.AvatarUrl != null)
                //{
                //    string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + UserProfile.AvatarUrl.Replace("../", "");
                //    if (File.Exists(strFileFullPath))
                //    {
                //        File.Delete(strFileFullPath);
                //    }
                //}
                fileimage = "../../data/customer/" + Customer_ID + "-" + (fulAnh.FileName);
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _CustomerRoww.Logo = fileimage;
                }
            }
            BusinessRulesLocator.GetCustomerBO().Update(_CustomerRoww);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            Response.Redirect("Customer_List.aspx", false);
        }

    }


    //protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlFunctionGroup.SelectedValue != "0")
    //    //{
    //    //    FunctionGroup_ID = ddlFunctionGroup.SelectedValue;
    //    //}
    //    string listItem = string.Empty;
    //    foreach (RadComboBoxItem item in ddlCustomerType.Items)
    //    {
    //        if (item.Checked)
    //        {
    //            listItem += item.Value + ",";
    //        }
    //    }
    //}

    //protected string GetCustomerType_ID()
    //{
    //    string CustomerType_ID = string.Empty;
    //    try
    //    {
    //        foreach (RadComboBoxItem item in ddlCustomerType.Items)
    //        {
    //            if (item.Checked)
    //            {
    //                CustomerType_ID += item.Value + ",";
    //            }
    //        }
    //        if (!string.IsNullOrEmpty(CustomerType_ID))
    //        {
    //            CustomerType_ID = "," + CustomerType_ID;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("GetCustomerType_ID", ex.ToString());
    //    }
    //    return CustomerType_ID;
    //}



    protected string GetProductBrandList_ID()
    {
        string ProductBrandList_ID = string.Empty;
        try
        {
            //foreach (RadComboBoxItem item in ddlProductBrand.Items)
            //{
            //    if (item.Checked)
            //    {
            //        ProductBrandList_ID += item.Value + ",";
            //    }
            //}
            //if (!string.IsNullOrEmpty(ProductBrandList_ID))
            //{
            //    ProductBrandList_ID = "," + ProductBrandList_ID;
            //}
            //ProductBrandList_ID = HdProductBrand.Value;

            foreach (ListItem item in ddlProductBrand.Items)
            {
                if (item.Selected)
                {
                     ProductBrandList_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ProductBrandList_ID))
            {
                ProductBrandList_ID = "," + ProductBrandList_ID;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("GetProductBrandList_ID", ex.ToString());
        }
        return ProductBrandList_ID;
    }



    protected void AddCustomer()
    {
        try
        {
            //if (CheckStaffPhone(txtPhone.Text.Trim()))
            //{
            CustomerRow _CustomerRow = new CustomerRow();
            _CustomerRow.CustomerType_ID = Convert.ToInt32(ddlCustomerType.SelectedValue);
            //_CustomerRow.ProductBrandList_ID =  GetProductBrandList_ID();
            _CustomerRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _CustomerRow.Name = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;
            _CustomerRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _CustomerRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
            _CustomerRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
            _CustomerRow.Website = string.IsNullOrEmpty(txttWebsite.Text) ? string.Empty : txttWebsite.Text;
            _CustomerRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

            _CustomerRow.CreateBy = MyUser.GetUser_ID();
            _CustomerRow.CreateDate = DateTime.Now;

            _CustomerRow.LastEditBy = MyUser.GetUser_ID();
            _CustomerRow.LastEditDate = DateTime.Now;

            BusinessRulesLocator.GetCustomerBO().Insert(_CustomerRow);
            if (_CustomerRow != null)
            {
                UpdateCustomer(_CustomerRow.Customer_ID);
            }
            //}
            //else
            //{
            //    lblMessage.Text = "Đã tồn tại số điện thoại " + txtPhone.Text.Trim();
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.PaleTurquoise;
            //    lblMessage.Visible = true;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomer", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtFullName.Text = "";
        txttWebsite.Text = "";

        ddlCustomerType.SelectedValue = "";
        //ddlProductBrand.Items.Clear();

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