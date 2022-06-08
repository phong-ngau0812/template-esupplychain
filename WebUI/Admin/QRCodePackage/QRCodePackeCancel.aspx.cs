using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class QRCodePackeCancel : System.Web.UI.Page
{

    public string NameProductPackageQR = "";
    public string NameProductQR = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    public string Message = "";
    private int ProductBrand_ID = 0;
    private int QRCodePackage_ID = 0;
    public string SerialCancel ="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            //LoadQRCodePackageID();
            // FillDDLddlProductBrand();
        }
        LoadQRCodePackageID();
    }

    private void LoadQRCodePackageID()
    {
        if (QRCodePackage_ID != 0)
        {

            HDQRCodePackageSource_ID.Value = QRCodePackage_ID.ToString();

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.Conllection().GetQRCodeSerialNumCancel(QRCodePackage_ID, -1);
            //if (dt.Rows.Count != 0)
            //{
            //    SerialCancel += dt.Rows[0]["ASCSerialNumber"].ToString() + "-" + dt.Rows[0]["DESCSerialNumber"].ToString();
            //}

            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                NameProductPackageQR = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProductQR = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                TotalTem = _QRCodePackageRow.IsQRCodeNumberNull ? string.Empty : _QRCodePackageRow.QRCodeNumber.ToString();
                SerialStart = _QRCodePackageRow.IsSerialNumberStartNull ? string.Empty : _QRCodePackageRow.SerialNumberStart;
                SerialEnd = _QRCodePackageRow.IsSerialNumberEndNull ? string.Empty : _QRCodePackageRow.SerialNumberEnd;
            }
            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetQRCodePackageBO().GetAsDataTable(" QRCodePackage_ID =" + QRCodePackage_ID +"and ", "");
            //if (dt.Rows.Count != 0)
            //{
            //    NameProductPackageQR = dt.Rows[0]["Name"].ToString();
            //    NameProductQR = dt.Rows[0]["ProductName"].ToString();
            //    TotalTem = dt.Rows[0]["QRCodeNumber"].ToString();
            //    SerialStart = dt.Rows[0]["SerialNumberStart"].ToString();
            //    SerialEnd = dt.Rows[0]["SerialNumberEnd"].ToString();
            //}

            //if (rdoSerial.Checked)
            //{
            //    txtSeria.Enabled = true;
            //    txtSerialStart.Enabled = false;
            //    txtSerialEnd.Enabled = false;
            //}
            //else
            //{
            //    txtSeria.Enabled = false;
            //    txtSerialStart.Enabled = true;
            //    txtSerialEnd.Enabled = true;
            //}
        }

    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    private void UpdateStatusCancel()
    {
        int QRCodeNumberCancel = 0;
        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        if (QRCodePackage_ID != 0)
        {
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                if (!string.IsNullOrEmpty(txtSerialStart.Text) && !string.IsNullOrEmpty(txtSerialEnd.Text))
                {
                    BusinessRulesLocator.Conllection().QRCodePackage_SetCancel(QRCodePackage_ID, -1, txtSerialStart.Text, txtSerialEnd.Text);


                    DataTable dt = new DataTable();
                    dt = BusinessRulesLocator.Conllection().QRCodeCountByRangeCancel(QRCodePackage_ID, -1);
                    if (dt.Rows.Count > 0)
                    {
                        QRCodeNumberCancel = Convert.ToInt32(dt.Rows[0]["Number"].ToString());
                    }
                    //string SerialStart = Regex.Replace(txtSerialStart.Text, "[^0-9]", "");
                    //string SerialEnd = Regex.Replace(txtSerialEnd.Text, "[^0-9]", "");

                    _QRCodePackageRow.QRCodeNumber = QRCodeNumberCancel;
                    _QRCodePackageRow.QRCodeAvailable = QRCodeNumberCancel;
                    BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);

                    lblMessage.Text = "Hủy tem thành công!";
                    lblMessage.Visible = true;
                }

                //LoadNameCodePackage();
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateStatusCancel();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeProductPackageregister_List.aspx", false);
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        txtSerialStart.Text = SerialStart;
        txtSerialEnd.Text = SerialEnd;
    }

    protected void rdoSerial_CheckedChanged(object sender, EventArgs e)
    {
        txtSeria.Enabled = true;
        txtSerialStart.Enabled = false;
        txtSerialEnd.Enabled = false;
    }

    protected void rdoKhoangSerial_CheckedChanged(object sender, EventArgs e)
    {
        txtSeria.Enabled = false;
        txtSerialStart.Enabled = true;
        txtSerialEnd.Enabled = true;
    }
}