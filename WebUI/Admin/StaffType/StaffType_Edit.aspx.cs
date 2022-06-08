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

public partial class StaffType_Edit : System.Web.UI.Page
{
    int StaffType_ID = 0;
    public string title = "Thông tin nhóm nhân viên";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["StaffType_ID"]))
            int.TryParse(Request["StaffType_ID"].ToString(), out StaffType_ID);
        if (!IsPostBack)
        {
            //FillDDLddlProductBrand();
            FillInfoStaffType();
        }
    }


    //private void FillDDLddlProductBrand()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active<>-1", " Sort ASC");
    //        ddlProductBrand.DataSource = dt;
    //        ddlProductBrand.DataTextField = "Name";
    //        ddlProductBrand.DataValueField = "ProductBrand_ID";
    //        ddlProductBrand.DataBind();
    //        ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillDDLCha", ex.ToString());
    //    }
    //}
    protected void FillInfoStaffType()
    {
        try
        {
            if (StaffType_ID != 0)
            {
                StaffTypeRow _StaffTypeRow = new StaffTypeRow();
                _StaffTypeRow = BusinessRulesLocator.GetStaffTypeBO().GetByPrimaryKey(StaffType_ID);

                if (_StaffTypeRow != null)
                {
                    txtName.Text = _StaffTypeRow.IsNameNull ? string.Empty : _StaffTypeRow.Name;
                    //ddlProductBrand.SelectedValue = _StaffTypeRow.ProductBrand_ID.ToString();
                    txtNote.Text = _StaffTypeRow.IsDescriptionNull ? string.Empty : _StaffTypeRow.Description;

                    if (_StaffTypeRow.Active == 1)
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

    protected void UpdateFunctionGroup()
    {
        try
        {
            StaffTypeRow _StaffTypeRow = new StaffTypeRow();
            if (StaffType_ID != 0)
            {
                _StaffTypeRow = BusinessRulesLocator.GetStaffTypeBO().GetByPrimaryKey(StaffType_ID);
                if (_StaffTypeRow != null)
                {
                    _StaffTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    //_StaffTypeRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _StaffTypeRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

                    if (ckActive.Checked)
                    {
                        _StaffTypeRow.Active = 1;
                    }
                    else
                    {
                        _StaffTypeRow.Active = 0;
                    }
                    _StaffTypeRow.LastEditedBy = MyUser.GetUser_ID();
                    _StaffTypeRow.LastEditedDate = DateTime.Now;
                    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoStaffType();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaffType", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateFunctionGroup();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffType_List.aspx", false);
    }
}