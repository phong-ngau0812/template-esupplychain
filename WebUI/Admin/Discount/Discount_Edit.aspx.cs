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

public partial class Discount_Edit : System.Web.UI.Page
{
    int Discount_ID = 0;
    public string title = "Thông tin chiết khấu";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtDescription);
        if (!string.IsNullOrEmpty(Request["Discount_ID"]))
            int.TryParse(Request["Discount_ID"].ToString(), out Discount_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
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

    protected void FillInfoDiscount()
    {
        try
        {
            if (Discount_ID != 0)
            {
                DiscountRow _DiscountRow = new DiscountRow();
                _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
                if (_DiscountRow != null)
                {
                    ddlProductBrand.SelectedValue = _DiscountRow.ProductBrand_ID.ToString();
                    txtName.Text = _DiscountRow.IsNameNull ? string.Empty : _DiscountRow.Name;
                    txtPercent.Text = _DiscountRow.IsPercentNull ? "0" :  _DiscountRow.Percent.ToString();
                    txtDescription.Text = _DiscountRow.IsDescriptionNull ? string.Empty : _DiscountRow.Description;

                    if (_DiscountRow.Active == 1)
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
            Log.writeLog("FillInfoDiscount", ex.ToString());
        }
    }

    protected void UpdateDiscount()
    {
        try
        {
            DiscountRow _DiscountRow = new DiscountRow();
            if (Discount_ID != 0)
            {
                _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
                if (_DiscountRow != null)
                {
                    _DiscountRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _DiscountRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    //string percent = string.IsNullOrEmpty(txtPercent.Text) ? "0" : txtPercent.Text;
                    if (!string.IsNullOrEmpty(txtPercent.Text))
                    {
                        _DiscountRow.Percent = Convert.ToInt32(txtPercent.Text.Trim());
                    }
                    else
                    {
                        _DiscountRow.Percent = 0;
                    }
                    _DiscountRow.Description = string.IsNullOrEmpty(txtDescription.Text) ? string.Empty : txtDescription.Text;

                    if (ckActive.Checked)
                    {
                        _DiscountRow.Active = 1;
                    }
                    else
                    {
                        _DiscountRow.Active = 0;
                    }
                    _DiscountRow.LastEditedBy = MyUser.GetUser_ID();
                    _DiscountRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetDiscountBO().Update(_DiscountRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoDiscount();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateDiscount", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateDiscount();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Discount_List.aspx", false);
    }
}