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

public partial class CustomerType_Add : System.Web.UI.Page
{
    public string title = "Thêm mới loại khách hàng ";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlDiscount();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddCustomerType();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckCustomerTypeName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetCustomerTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddCustomerType()
    {
        try
        {
            if (CheckCustomerTypeName(txtName.Text.Trim()))
            {
                CustomerTypeRow _CustomerTypeRow = new CustomerTypeRow();
                _CustomerTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                _CustomerTypeRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                if (ddlDiscount.SelectedValue != "")
                {
                    _CustomerTypeRow.Discount_ID = Convert.ToInt32(ddlDiscount.SelectedValue);
                }
                _CustomerTypeRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                if (ckActive.Checked)
                {
                    _CustomerTypeRow.Active = 1;
                }
                else
                {
                    _CustomerTypeRow.Active = 0;
                }
                _CustomerTypeRow.CreateBy = MyUser.GetUser_ID();
                _CustomerTypeRow.CreateDate = DateTime.Now;
                _CustomerTypeRow.LastEditedBy = MyUser.GetUser_ID();
                _CustomerTypeRow.LastEditedDate = DateTime.Now;
                _CustomerTypeRow.Sort = Common.GenarateSort("CustomerType");
                BusinessRulesLocator.GetCustomerTypeBO().Insert(_CustomerTypeRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Đã tồn tại nhóm nhân viên " + txtName.Text.Trim();
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomerType", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        
        txtNote.Text = "";
        ckActive.Checked = false;
        ddlDiscount.SelectedValue = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerType_List.aspx", false);
    }


    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlDiscount();
    }
}