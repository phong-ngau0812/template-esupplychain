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

public partial class Ministry_Edit : System.Web.UI.Page
{
    int Ministry_ID = 0;
    public string title = "Thông tin bộ";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Ministry_ID"]))
            int.TryParse(Request["Ministry_ID"].ToString(), out Ministry_ID);
        if (!IsPostBack)
        {
            FillInfoMinistry();
        }
    }

    protected void FillInfoMinistry()
    {
        try
        {
            if (Ministry_ID != 0)
            {
                MinistryRow _MinistryRow = new MinistryRow();
                _MinistryRow = BusinessRulesLocator.GetMinistryBO().GetByPrimaryKey(Ministry_ID);
                if (_MinistryRow != null)
                {
                    txtName.Text = _MinistryRow.IsNameNull ? string.Empty : _MinistryRow.Name;
                    txtEmail.Text = _MinistryRow.IsEmailNull ? string.Empty : _MinistryRow.Email;
                    txtPhone.Text = _MinistryRow.IsPhoneNull ? string.Empty : _MinistryRow.Phone;
                    txtAddress.Text = _MinistryRow.IsAddressNull ? string.Empty : _MinistryRow.Address;
                    if (_MinistryRow.Active == 1)
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

    protected void UpdateMinistry()
    {
        try
        {
            MinistryRow _MinistryRow = new MinistryRow();
            if (Ministry_ID != 0)
            {
                _MinistryRow = BusinessRulesLocator.GetMinistryBO().GetByPrimaryKey(Ministry_ID);
                if (_MinistryRow != null)
                {
                    _MinistryRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _MinistryRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
                    _MinistryRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _MinistryRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    if (ckActive.Checked)
                    {
                        _MinistryRow.Active = 1;
                    }
                    else
                    {
                        _MinistryRow.Active = 0;
                    }
                    _MinistryRow.LastEditedBy = MyUser.GetUser_ID();
                    _MinistryRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoMinistry();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMinistry", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateMinistry();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ministry_List.aspx", false);
    }
}