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
using Telerik.Web.UI;

public partial class SalesShift_Add : System.Web.UI.Page
{
    public string title = "Thêm mới ca bán hàng";
    string HourFrom = "";
    string HourTo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            //FillHourFrom();
            //FillHourTo();
        }
    }
    
  
    private void FillHourFrom()
    {
        for (int i = 24; i >= 1; i--)
        {
            ddlHourFrom.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        
        for (int i = 59; i >= 0; i--)
        {
            ddlMinutesFrom.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        ddlHourFrom.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinutesFrom.SelectedValue = DateTime.Now.Minute.ToString();
    }

    private void FillHourTo()
    {
        for (int i = 24; i >= 1; i--)
        {
            ddHourTo.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }

        for (int i = 59; i >= 0; i--)
        {
            ddlMinutesTo.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        ddHourTo.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinutesTo.SelectedValue = DateTime.Now.Minute.ToString();
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, "");
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
   

    protected void AddSalesShift()
    {
        SalesShiftRow _SalesShiftRow = new SalesShiftRow();
        _SalesShiftRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
        _SalesShiftRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
        
        //HourFrom = ddlHourFrom.SelectedValue + ":" + ddlMinutesFrom.SelectedValue;
        //_SalesShiftRow.FromHour = string.IsNullOrEmpty(HourFrom) ? string.Empty : HourFrom;

        //HourTo = ddHourTo.SelectedValue + ":" + ddlMinutesTo.SelectedValue;
        //_SalesShiftRow.ToHour = string.IsNullOrEmpty(HourTo) ? string.Empty : HourTo;

        _SalesShiftRow.FromHour = timepickerStart.Text;
        _SalesShiftRow.ToHour = timepickerEnd.Text;

        _SalesShiftRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
        _SalesShiftRow.CreateBy = MyUser.GetUser_ID();
        _SalesShiftRow.CreateDate = DateTime.Now;
        //_SalesShiftRow.LastEditBy = MyUser.GetUser_ID();
        //_SalesShiftRow.LastEditDate = DateTime.Now;
        if (ckSound.Checked)
        {
            _SalesShiftRow.Active = 1;
        }
        else
        {
            _SalesShiftRow.Active = 0;
        }

        _SalesShiftRow.Sort = Common.GenarateSort("SalesShift");
        BusinessRulesLocator.GetSalesShiftBO().Insert(_SalesShiftRow);
        lblMessage.Text = "Thêm mới thành công!";
        lblMessage.Visible = true;
        ClearForm();
    }
     protected void ClearForm()
    {
        txtName.Text = "";
        txtNote.Text = "";
        timepickerEnd.Text = "00:00";
        timepickerStart.Text = "00:00";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddSalesShift();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesShift_List.aspx", false);
    }
   
}