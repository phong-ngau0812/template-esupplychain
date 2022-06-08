using DbObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_ManufactureTech_ManufactureTechVsTask : System.Web.UI.Page
{
    public string title = "Đề mục công việc";
    public string manufactureTech = "Đề mục công việc";
    public int ManufactureTech_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ManufactureTech_ID"]))
            int.TryParse(Request["ManufactureTech_ID"].ToString(), out ManufactureTech_ID);
        LoadData();
        if (!IsPostBack) { hdfIdManu.Value = "0"; }
        Reset();
    }
    protected void Reset()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadData()
    {
        ManufactureTechRow _ManufactureTechRow = BusinessRulesLocator.GetManufactureTechBO().GetByPrimaryKey(ManufactureTech_ID);
        if (_ManufactureTechRow != null)
        {
            manufactureTech = _ManufactureTechRow.Name;

        }
        rptManufactureTech.DataSource = BusinessRulesLocator.Conllection().GetAllList(@"Select ManufactureTechVsTask_ID,ManufactureTech_ID,Name,Description,Active,Sort,Hour,Minute from ManufactureTechVsTask where Active = 1 and ManufactureTech_ID =" + ManufactureTech_ID + " order by Sort ASC");
        rptManufactureTech.DataBind();
    }
    protected void Clear()
    {
        txtName.Text = "";
        txtNote.Text = "";
        txtNumber.Text = "";
        txtMinute.Text = "";
        txtHour.Text = "";
        hdfIdManu.Value = "0";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdfIdManu.Value == "0")
            {
                ManufactureTechVsTaskRow _Row = new ManufactureTechVsTaskRow();
                _Row.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                _Row.ManufactureTech_ID = ManufactureTech_ID;
                _Row.Sort = string.IsNullOrEmpty(txtNumber.Text) ? 0 : Convert.ToInt32(txtNumber.Text);
                _Row.Description = string.IsNullOrEmpty(txtNote.Text) ? "" : txtNote.Text;
                _Row.Hour = string.IsNullOrEmpty(txtHour.Text) ? 0 : Convert.ToInt32(txtHour.Text);
                _Row.Minute = string.IsNullOrEmpty(txtMinute.Text) ? 0 : Convert.ToInt32(txtMinute.Text);
                _Row.CreateBy = MyUser.GetUser_ID();
                _Row.Active = true;
                _Row.CreateDate = DateTime.Now;

                BusinessRulesLocator.GetManufactureTechVsTaskBO().Insert(_Row);
                lblMessage.Text = "Thêm mới thành công";
                lblMessage.Visible = true;
            }
            else
            {
                ManufactureTechVsTaskRow _Row = BusinessRulesLocator.GetManufactureTechVsTaskBO().GetByPrimaryKey(Convert.ToInt32(hdfIdManu.Value));

                if (_Row != null)
                {

                    _Row.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _Row.ManufactureTech_ID = ManufactureTech_ID;
                    _Row.Sort = string.IsNullOrEmpty(txtNumber.Text) ? 0 : Convert.ToInt32(txtNumber.Text);
                    _Row.Description = string.IsNullOrEmpty(txtNote.Text) ? "" : txtNote.Text;
                    _Row.Hour = string.IsNullOrEmpty(txtHour.Text) ? 0 : Convert.ToInt32(txtHour.Text);
                    _Row.Minute = string.IsNullOrEmpty(txtMinute.Text) ? 0 : Convert.ToInt32(txtMinute.Text);
                    _Row.LastEditBy = MyUser.GetUser_ID();
                    _Row.Active = true;
                    _Row.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetManufactureTechVsTaskBO().Update(_Row);
                    lblMessage.Text = "Cập nhật thành công";
                    lblMessage.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
        LoadData();
        Clear();
        hdfIdManu.Value = "0";
    }
    protected void rptManufactureTech_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ManufactureTech_ID = Convert.ToInt32(e.CommandArgument);
        ManufactureTechRow _ManufactureRow = new ManufactureTechRow();
        _ManufactureRow = BusinessRulesLocator.GetManufactureTechBO().GetByPrimaryKey(ManufactureTech_ID);
        switch (e.CommandName)
        {
            case "Delete":

                BusinessRulesLocator.GetManufactureTechVsTaskBO().DeleteByPrimaryKey(ManufactureTech_ID);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_ManufactureRow != null)
                {
                    _ManufactureRow.Active = true;
                }
                BusinessRulesLocator.GetManufactureTechBO().Update(_ManufactureRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_ManufactureRow != null)
                {
                    _ManufactureRow.Active = false;
                }
                BusinessRulesLocator.GetManufactureTechBO().Update(_ManufactureRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManufactureTech_List.aspx", false);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
}