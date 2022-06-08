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

public partial class Agency_Add : System.Web.UI.Page
{
    public string title = "Thêm mới đại lý ";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();

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
            AddAgency();
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
    protected void UpdateAgency(int Agency_ID)
    {
        AgencyRow _AgencyRow = new AgencyRow();
        _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Agency_ID);
        if (_AgencyRow != null)
        {
            _AgencyRow.Code = "GSRN-" + _AgencyRow.Agency_ID.ToString();
            BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
        }

    }

    protected void AddAgency()
    {
        try
        {
            //if (CheckStaffPhone(txtPhone.Text.Trim()))
            //{
            AgencyRow _AgencyRow = new AgencyRow();
            
            _AgencyRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _AgencyRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _AgencyRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _AgencyRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
            _AgencyRow.Level = string.IsNullOrEmpty(txtLevel.Text) ? string.Empty : txtLevel.Text;
            _AgencyRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
            _AgencyRow.PersonName = string.IsNullOrEmpty(txtPersonName.Text) ? string.Empty : txtPersonName.Text;
            _AgencyRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _AgencyRow.Longitude = string.IsNullOrEmpty(txtLongitude.Text) ? string.Empty : txtLongitude.Text;
            _AgencyRow.Latitude = string.IsNullOrEmpty(txtLatitude.Text) ? string.Empty : txtLatitude.Text;
            if (ckActive.Checked)
            {
                _AgencyRow.Active = 1;
            }
            else
            {
                _AgencyRow.Active = 0;
            }

            _AgencyRow.Sort = Common.GenarateSort("Agency");
            _AgencyRow.CreateBy = MyUser.GetUser_ID();
            _AgencyRow.CreateDate = DateTime.Now;

            _AgencyRow.LastEditBy = MyUser.GetUser_ID();
            _AgencyRow.LastEditDate = DateTime.Now;
            BusinessRulesLocator.GetAgencyBO().Insert(_AgencyRow);
            if (_AgencyRow != null)
            {
                UpdateAgency(_AgencyRow.Agency_ID);
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
            Log.writeLog("UpdateAgency", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtName.Text = "";
        txtLevel.Text = "";
        txtNote.Text = "";
        //ddlProductBrand.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agency_List.aspx", false);
    }

}