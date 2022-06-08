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

public partial class Ministry_Add : System.Web.UI.Page
{
    public string title = "Thêm mới bộ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddMinistry();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void AddMinistry()
    {
        try
        {
            MinistryRow _MinistryRow = new MinistryRow();
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
            _MinistryRow.CreateBy = MyUser.GetUser_ID();
            _MinistryRow.CreateDate = DateTime.Now;
            _MinistryRow.LastEditedBy = MyUser.GetUser_ID();
            _MinistryRow.LastEditedDate = DateTime.Now;
            _MinistryRow.Sort = Common.GenarateSort("Ministry");
            BusinessRulesLocator.GetMinistryBO().Insert(_MinistryRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMinistry", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = txtEmail.Text = txtName.Text = txtPhone.Text = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ministry_List.aspx", false);
    }
}