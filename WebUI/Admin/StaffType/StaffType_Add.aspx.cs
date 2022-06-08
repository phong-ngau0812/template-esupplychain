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

public partial class StaffType_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhóm nhân viên ";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            //FillDDLddlProductBrand();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddStaffType();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckStaffTypeName(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }
    protected void AddStaffType()
    {
        try
        {
            //if (CheckStaffTypeName(txtName.Text.Trim()))
            //{
            StaffTypeRow _StaffTypeRow = new StaffTypeRow();
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
            _StaffTypeRow.CreateBy = MyUser.GetUser_ID();
            _StaffTypeRow.CreateDate = DateTime.Now;
            _StaffTypeRow.LastEditedBy = MyUser.GetUser_ID();
            _StaffTypeRow.LastEditedDate = DateTime.Now;
            _StaffTypeRow.Sort = Common.GenarateSort("StaffType");
            BusinessRulesLocator.GetStaffTypeBO().Insert(_StaffTypeRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            //}
            //else
            //{
            //    lblMessage.Text = "Đã tồn tại nhóm nhân viên " + txtName.Text.Trim();
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.BackColor = Color.PaleTurquoise;
            //    lblMessage.Visible = true;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaffType", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        //ddlProductBrand.SelectedValue = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffType_List.aspx", false);
    }
}