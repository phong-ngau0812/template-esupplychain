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

public partial class Supplier_Edit : System.Web.UI.Page
{
    int Supplier_ID = 0;
    public string title = "Thông tin nhà cung cấp";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Supplier_ID"]))
            int.TryParse(Request["Supplier_ID"].ToString(), out Supplier_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillInfoSupplier();
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
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void FillInfoSupplier()
    {
        try
        {
            if (Supplier_ID != 0)
            {
                SupplierRow _SupplierRow = new SupplierRow();
                _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
                if (_SupplierRow != null)
                {
                    txtName.Text = _SupplierRow.IsNameNull ? string.Empty : _SupplierRow.Name;
                    txtWebsite.Text = _SupplierRow.IsWebsiteNull ? string.Empty : _SupplierRow.Website;
                    txtEmail.Text = _SupplierRow.IsEmailNull ? string.Empty : _SupplierRow.Email;
                    txtPhone.Text = _SupplierRow.IsPhoneNull ? string.Empty : _SupplierRow.Phone;
                    txtAddress.Text = _SupplierRow.IsAddressNull ? string.Empty : _SupplierRow.Address;
                    txtGDTI.Text = _SupplierRow.IsGDTINull ? string.Empty : _SupplierRow.GDTI;
                    avatar = _SupplierRow.IsAvatarUrlNull ? string.Empty:_SupplierRow.AvatarUrl ;
                    if (avatar != null)
                    {
                        imganh.ImageUrl = avatar;
                    }
                    else
                    {
                        avatar = "../../images/no-image-icon.png";
                        imganh.ImageUrl = avatar;
                    }
                    if (_SupplierRow.Active == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                    if (!_SupplierRow.IsProductBrand_IDNull)
                    {
                        ddlProductBrand.SelectedValue = _SupplierRow.ProductBrand_ID.ToString();
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoSupplier", ex.ToString());
        }
    }

    protected void UpdateSupplier()
    {
        try
        {
            SupplierRow _SupplierRow = new SupplierRow();
            if (Supplier_ID != 0)
            {
                _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
                if (_SupplierRow != null)
                {
                    _SupplierRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _SupplierRow.Website = string.IsNullOrEmpty(txtWebsite.Text) ? string.Empty : txtWebsite.Text;
                    
                    _SupplierRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
                    _SupplierRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _SupplierRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    _SupplierRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_SupplierRow.IsAvatarUrlNull)
                        {
                            if (_SupplierRow.AvatarUrl != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _SupplierRow.AvatarUrl.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
               
                        fileimage = "../../data/supplier/" + Supplier_ID  + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _SupplierRow.AvatarUrl = fileimage;
                        }
                    }


                    if (ckActive.Checked)
                    {
                        _SupplierRow.Active = 1;
                    }
                    else
                    {
                        _SupplierRow.Active = 0;
                    }
                    _SupplierRow.LastEditedBy = MyUser.GetUser_ID();
                    _SupplierRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoSupplier();
                Response.Redirect("Supplier_List.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateSupplier", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateSupplier();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_List.aspx", false);
    }
}

