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

public partial class FCMMessage_Edit : System.Web.UI.Page
{
    int Transporter_ID = 0;
    public string title = "Thông tin nhà vận chuyển";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(Request["Transporter_ID"]))
            int.TryParse(Request["Transporter_ID"].ToString(), out Transporter_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillInfoTransporter();
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

    protected void FillInfoTransporter()
    {
        try
        {
            if (Transporter_ID != 0)
            {
                TransporterRow _TransporterRow = new TransporterRow();
                _TransporterRow = BusinessRulesLocator.GetTransporterBO().GetByPrimaryKey(Transporter_ID);

                if (_TransporterRow != null)
                {
                    txtFullName.Text = _TransporterRow.IsNameNull ? string.Empty : _TransporterRow.Name;
                    ddlProductBrand.SelectedValue = _TransporterRow.ProductBrand_ID.ToString();

                    txtEmail.Text = _TransporterRow.IsEmailNull ? string.Empty : _TransporterRow.Email;
                    txtAddress.Text = _TransporterRow.IsAddressNull ? string.Empty : _TransporterRow.Address;
                    txtPhone.Text = _TransporterRow.IsPhoneNull ? string.Empty : _TransporterRow.Phone;
                   
                    if (_TransporterRow.Active == 1)
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



    protected void UpdateTransporter()
    {
        try
        {
            TransporterRow _TransporterRow = new TransporterRow();
            if (Transporter_ID != 0)
            {
                _TransporterRow = BusinessRulesLocator.GetTransporterBO().GetByPrimaryKey(Transporter_ID);
                if (_TransporterRow != null)
                {
                    _TransporterRow.Name = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;
                    _TransporterRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);

                    _TransporterRow.Address = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    _TransporterRow.Phone = string.IsNullOrEmpty(txtPhone.Text) ? string.Empty : txtPhone.Text;
                    _TransporterRow.Email = string.IsNullOrEmpty(txtEmail.Text) ? string.Empty : txtEmail.Text;

                    if (ckActive.Checked)
                    {
                        _TransporterRow.Active = 1;
                    }
                    else
                    {
                        _TransporterRow.Active = 0;
                    }
                    _TransporterRow.LastEditBy = MyUser.GetUser_ID();
                    _TransporterRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetTransporterBO().Update(_TransporterRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoTransporter();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaff", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateTransporter();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Transporter_List.aspx", false);
    }

}