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

public partial class QRCodePackage_Edit_Status : System.Web.UI.Page
{
    int QRCodePackage_ID = 0;
    public string title = "Cập nhập trạng thái lô mã";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            // FillDDLddlProductBrand();
            LoadStatus();
            LoadNameCodePackage();

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                UpdateQRCodeStatus();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void LoadNameCodePackage()
    {
        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        if (QRCodePackage_ID != 0)
        {
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                txtName.Text = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                txtNote.Text = _QRCodePackageRow.IsDescriptionNull ? string.Empty : _QRCodePackageRow.Description;
                ddlStatus.SelectedValue = _QRCodePackageRow.QRCodeStatus_ID.ToString();



                if (_QRCodePackageRow.SoundEnable == true)
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

    private void LoadStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQRCodeStatusBO().GetAsDataTable("QRCodeStatus_ID in (-1,0,1,2) ", "");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "QRCodeStatus_ID";
            ddlStatus.DataBind();

            //ddlProducCategory.Items.Insert(0, new ListItem(" -- Kích hoạt đưa mã tem ra thị  trường -- ", "2"));
            //ddlProducCategory.Items.Insert(1, new ListItem(" -- Tem bị hủy -- ", "-1"));
            //ddlProducCategory.Items.Insert(2, new ListItem(" -- Tem mới tạo -- ", "0"));
            //ddlProducCategory.Items.Insert(3, new ListItem(" -- Tem đã giao cho nhà in -- ", "1"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadStatus", ex.ToString());
        }
    }

    protected void UpdateQRCodeStatus()
    {

        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        if (QRCodePackage_ID != 0)
        {
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
               
                //if (ckActive.Checked)
                //{
                //    _QRCodePackageRow.SoundEnable = true;
                //}
                //else
                //{
                //    _QRCodePackageRow.SoundEnable = false;
                //}
                //BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
                BusinessRulesLocator.Conllection().QRCodePackage_SetSoundEnable(QRCodePackage_ID,ckActive.Checked);
                BusinessRulesLocator.Conllection().QRCodePackage_SetStatus(QRCodePackage_ID, Convert.ToInt32(ddlStatus.SelectedValue));
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                LoadNameCodePackage();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_List.aspx", false);
    }

}