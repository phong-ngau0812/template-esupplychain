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

public partial class CustomerType_Edit : System.Web.UI.Page
{
    int CustomerType_ID = 0;
    public string title = "Thông tin loại khách hàng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["CustomerType_ID"]))
            int.TryParse(Request["CustomerType_ID"].ToString(), out CustomerType_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlDiscount();
            FillInfoDiscount();
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
    private void FillDDLddlDiscount()
    {
        try
        {
            string where = " Active<>-1";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetDiscountBO().GetAsDataTable(where, " Sort ASC");
            ddlDiscount.DataSource = dt;
            ddlDiscount.DataTextField = "Name";
            ddlDiscount.DataValueField = "Discount_ID";
            ddlDiscount.DataBind();
            ddlDiscount.Items.Insert(0, new ListItem("-- Chọn chiết khấu --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void FillInfoDiscount()
    {
        try
        {
            if (CustomerType_ID != 0)
            {
                CustomerTypeRow _CustomerTypeRow = new CustomerTypeRow();
                _CustomerTypeRow = BusinessRulesLocator.GetCustomerTypeBO().GetByPrimaryKey(CustomerType_ID);

                if (_CustomerTypeRow != null)
                {
                    txtName.Text = _CustomerTypeRow.IsNameNull ? string.Empty : _CustomerTypeRow.Name;
                    ddlDiscount.SelectedValue = _CustomerTypeRow.IsDiscount_IDNull ? "" : _CustomerTypeRow.Discount_ID.ToString();
                    ddlProductBrand.SelectedValue = _CustomerTypeRow.ProductBrand_ID.ToString();
                    txtNote.Text = _CustomerTypeRow.IsDescriptionNull ? string.Empty : _CustomerTypeRow.Description;

                    if (_CustomerTypeRow.Active == 1)
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

    protected void UpdateCustomerType()
    {
        try
        {
            CustomerTypeRow _CustomerTypeRow = new CustomerTypeRow();
            if (CustomerType_ID != 0)
            {
                _CustomerTypeRow = BusinessRulesLocator.GetCustomerTypeBO().GetByPrimaryKey(CustomerType_ID);
                if (_CustomerTypeRow != null)
                {
                    _CustomerTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    if (ddlDiscount.SelectedValue != "")
                    {
                        _CustomerTypeRow.Discount_ID = Convert.ToInt32(ddlDiscount.SelectedValue);
                    }
                    _CustomerTypeRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _CustomerTypeRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

                    if (ckActive.Checked)
                    {
                        _CustomerTypeRow.Active = 1;
                    }
                    else
                    {
                        _CustomerTypeRow.Active = 0;
                    }
                    _CustomerTypeRow.LastEditedBy = MyUser.GetUser_ID();
                    _CustomerTypeRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetCustomerTypeBO().Update(_CustomerTypeRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoDiscount();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomerType", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateCustomerType();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerType_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}