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

public partial class FCMMessage_Add : System.Web.UI.Page
{
    public string title = "Thêm mới nhà vận chuyển ";
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
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));
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
            AddTransporter();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void AddTransporter()
    {
        try
        {
             TransporterRow _TransporterRow = new TransporterRow();
            _TransporterRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _TransporterRow.Name = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;
            _TransporterRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _TransporterRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;
            _TransporterRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;

            if (ckActive.Checked)
            {
                _TransporterRow.Active = 1;
            }
            else
            {
                _TransporterRow.Active = 0;
            }
            _TransporterRow.CreateBy = MyUser.GetUser_ID();
            _TransporterRow.CreateDate = DateTime.Now;

            _TransporterRow.LastEditBy = MyUser.GetUser_ID();
            _TransporterRow.LastEditDate = DateTime.Now;

            _TransporterRow.Sort = Common.GenarateSort("Transporter");
            BusinessRulesLocator.GetTransporterBO().Insert(_TransporterRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();

    }
        catch (Exception ex)
        {
            Log.writeLog("AddTransporter", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtFullName.Text = "";
        ddlProductBrand.SelectedValue = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Transporter_List.aspx", false);
    }
}