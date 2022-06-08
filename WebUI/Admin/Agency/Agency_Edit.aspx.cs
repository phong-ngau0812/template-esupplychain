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

public partial class Agency_Edit : System.Web.UI.Page
{
    int Agency_ID = 0;
    public string title = "Thông tin đại lý";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["Agency_ID"]))
            int.TryParse(Request["Agency_ID"].ToString(), out Agency_ID);
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
            if (Agency_ID != 0)
            {
                AgencyRow _AgencyRow = new AgencyRow();
                _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Agency_ID);

                if (_AgencyRow != null)
                {
                    ddlProductBrand.SelectedValue = _AgencyRow.ProductBrand_ID.ToString();
                    txtName.Text = _AgencyRow.IsNameNull ? string.Empty : _AgencyRow.Name;
                    txtCode.Text = _AgencyRow.IsCodeNull ? string.Empty : _AgencyRow.Code;
                    txtEmail.Text = _AgencyRow.IsEmailNull ? string.Empty : _AgencyRow.Email;
                    txtAddress.Text = _AgencyRow.IsAddressNull ? string.Empty : _AgencyRow.Address;
                    txtPhone.Text = _AgencyRow.IsPhoneNull ? string.Empty : _AgencyRow.Phone;
                    txtPersonName.Text = _AgencyRow.IsPersonNameNull ? string.Empty : _AgencyRow.PersonName;
                    txtNote.Text = _AgencyRow.IsDescriptionNull ? string.Empty : _AgencyRow.Description;
                    txtLevel.Text = _AgencyRow.IsLevelNull ? string.Empty : _AgencyRow.Level;
                    txtLongitude.Text = _AgencyRow.IsLongitudeNull ? string.Empty : _AgencyRow.Longitude;
                    txtLatitude.Text = _AgencyRow.IsLatitudeNull ? string.Empty : _AgencyRow.Latitude;

                    if (_AgencyRow.Active == 1)
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


    protected void UpdateAgency()
    {
        try
        {
            AgencyRow _AgencyRow = new AgencyRow();
            if (Agency_ID != 0)
            {
                _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Agency_ID);
                if (_AgencyRow != null)
                {

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
                    _AgencyRow.LastEditBy = MyUser.GetUser_ID();
                    _AgencyRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoCustomer();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateAgecy", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateAgency();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agency_List.aspx", false);
    }
}