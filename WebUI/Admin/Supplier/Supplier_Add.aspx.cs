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

public partial class Supplier_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhà cung cấp";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
        }
    }


    private void FillDDLddlProductBrand()
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
            Common.FillProductBrand_Null(ddlProductBrand, where);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddSupplier();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void UpdaSupplier(int Supplier_ID)
    {
        SupplierRow _SupplierRow = new SupplierRow();
        _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
        string fileimage = "";
        if (_SupplierRow != null)
        {

            _SupplierRow.GDTI = "GDTI-" + _SupplierRow.Supplier_ID.ToString();
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
                fileimage = "../../data/supplier/" + Supplier_ID + "-" + (fulAnh.FileName);
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _SupplierRow.AvatarUrl = fileimage;
                }
            }
            BusinessRulesLocator.GetSupplierBO().Update(_SupplierRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            Response.Redirect("Supplier_List.aspx", false);
        }

    }
    protected void AddSupplier()
    {
        try
        {

            SupplierRow _SupplierRow = new SupplierRow();
            _SupplierRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _SupplierRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _SupplierRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
            _SupplierRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
            _SupplierRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _SupplierRow.Website = string.IsNullOrEmpty(txtWebsite.Text) ? string.Empty : txtWebsite.Text;


            if (ckActive.Checked)
            {
                _SupplierRow.Active = 1;
            }
            else
            {
                _SupplierRow.Active = 0;
            }
            _SupplierRow.CreateBy = MyUser.GetUser_ID();
            _SupplierRow.CreateDate = DateTime.Now;
            _SupplierRow.LastEditedBy = MyUser.GetUser_ID();
            _SupplierRow.LastEditedDate = DateTime.Now;
            _SupplierRow.Sort = Common.GenarateSort("Supplier");
            BusinessRulesLocator.GetSupplierBO().Insert(_SupplierRow);
            if (_SupplierRow != null)
            {
                UpdaSupplier(_SupplierRow.Supplier_ID);
                Response.Redirect("Supplier_List.aspx", false);
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateSupplier", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = txtEmail.Text = txtName.Text = txtPhone.Text = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_List.aspx", false);
    }
}