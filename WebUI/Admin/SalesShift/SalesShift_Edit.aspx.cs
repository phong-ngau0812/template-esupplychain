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

public partial class SalesShift_Edit : System.Web.UI.Page
{
    int SalesShift_ID = 0;
    public string title = "Thông tin ca bán hàng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["SalesShift_ID"]))
            int.TryParse(Request["SalesShift_ID"].ToString(), out SalesShift_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            //FillHourFrom();
            //FillHourTo();
            FillInfoSalesShift();
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
            Common.FillProductBrand_Null(ddlProductBrand, "");
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


    protected void FillInfoSalesShift()
    {
        string TimeFrom = "";
        string TimeTo = "";
        try
        {
            if (SalesShift_ID != 0)
            {
                SalesShiftRow _SalesShiftRow = new SalesShiftRow();
                _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);

                if (_SalesShiftRow != null)
                {
                    txtName.Text = _SalesShiftRow.IsNameNull ? string.Empty : _SalesShiftRow.Name;
                    ddlProductBrand.SelectedValue = _SalesShiftRow.ProductBrand_ID.ToString();
                    txtNote.Text = _SalesShiftRow.IsDescriptionNull ? string.Empty : _SalesShiftRow.Description;
                    //TimeFrom = _SalesShiftRow.IsFromHourNull ? string.Empty : _SalesShiftRow.FromHour.ToString();

                    //if (!string.IsNullOrEmpty(TimeFrom))
                    //{
                    //    ddlHourFrom.SelectedValue = DateTime.Parse(TimeFrom).Hour.ToString();
                    //    ddlMinutesFrom.SelectedValue = DateTime.Parse(TimeFrom).Minute.ToString();
                    //}
                    //TimeTo = _SalesShiftRow.IsToHourNull ? string.Empty : _SalesShiftRow.ToHour.ToString();
                    //if ( !string.IsNullOrEmpty(TimeTo))
                    //{
                    //    ddHourTo.SelectedValue = DateTime.Parse(TimeTo).Hour.ToString();
                    //    ddlMinutesTo.SelectedValue = DateTime.Parse(TimeTo).Minute.ToString();
                    //}

                    if (!_SalesShiftRow.IsFromHourNull)
                    {
                        timepickerStart.Text = DateTime.Parse( _SalesShiftRow.FromHour.ToString()).ToString("HH:mm");
                    }


                    if (!_SalesShiftRow.IsToHourNull)
                    {
                        timepickerEnd.Text = DateTime.Parse( _SalesShiftRow.ToHour.ToString()).ToString("HH:mm");
                    }

                    if (_SalesShiftRow.Active == 1)
                    {
                        ckSound.Checked = true;
                    }
                    else
                    {
                        ckSound.Checked = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateSalesShift()
    {
        string HourFrom, HourTo;
        try
        {
            SalesShiftRow _SalesShiftRow = new SalesShiftRow();
            if (SalesShift_ID != 0)
            {
                _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);
                if (_SalesShiftRow != null)
                {
                    _SalesShiftRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _SalesShiftRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _SalesShiftRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

                    //HourFrom = ddlHourFrom.SelectedValue + ":" + ddlMinutesFrom.SelectedValue;
                    //_SalesShiftRow.FromHour = string.IsNullOrEmpty(HourFrom) ? string.Empty : HourFrom;

                    //HourTo = ddHourTo.SelectedValue + ":" + ddlMinutesTo.SelectedValue;
                    //_SalesShiftRow.ToHour = string.IsNullOrEmpty(HourTo) ? string.Empty : HourTo;

                    _SalesShiftRow.FromHour = timepickerStart.Text;
                    _SalesShiftRow.ToHour = timepickerEnd.Text;

                    if (ckSound.Checked)
                    {
                        _SalesShiftRow.Active = 1;
                    }
                    else
                    {
                        _SalesShiftRow.Active = 0;
                    }
                    _SalesShiftRow.LastEditBy = MyUser.GetUser_ID();
                    _SalesShiftRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetSalesShiftBO().Update(_SalesShiftRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoSalesShift();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateDepartment", ex.ToString());
        }
    }





    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateSalesShift();
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

    protected void ddlProducCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}