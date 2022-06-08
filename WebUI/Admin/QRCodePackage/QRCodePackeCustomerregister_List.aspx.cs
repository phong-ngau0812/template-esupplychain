using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class QRCodePackeCustomerregister_List : System.Web.UI.Page
{
    //chưa phân trang theo sql
    public string Message = "";
    public string NameQRCodePackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";

    public int QRCodePackage_ID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);

        // btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillQRCodeCustomerRegister();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void FillQRCodeCustomerRegister()
    {
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                NameQRCodePackage = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProduct = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                TotalTem = _QRCodePackageRow.IsQRCodeNumberNull ? "0" : _QRCodePackageRow.QRCodeNumber.ToString();
                SerialStart = _QRCodePackageRow.IsSerialNumberStartNull ? string.Empty : _QRCodePackageRow.SerialNumberStart;
                SerialEnd = _QRCodePackageRow.IsSerialNumberEndNull ? string.Empty : _QRCodePackageRow.SerialNumberEnd;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select QR.QRCodeCustomerRegister_ID, QR.CustomerName, QR.SerialNumberStart , QR.SerialNumberEnd, QR.QRCodeNumber , C.Name as NameCustomer
  from QRCodeCustomerRegister QR
  left join Customer C on QR.Customer_ID = C.Customer_ID
  where QR.QRCodePackage_ID = " + QRCodePackage_ID + "ORDER by QR.CreateDate DESC");
            rptQRCodeCR.DataSource = dt;
            rptQRCodeCR.DataBind();

        }

    }
    protected void rptQRCodeCR_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int QRCodeCustomerRegister_ID = Convert.ToInt32(e.CommandArgument);
        QRCodeCustomerRegisterRow _QRCodeCustomerRegisterRow = new QRCodeCustomerRegisterRow();
        _QRCodeCustomerRegisterRow = BusinessRulesLocator.GetQRCodeCustomerRegisterBO().GetByPrimaryKey(QRCodeCustomerRegister_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_QRCodeCustomerRegisterRow != null)
                {
                    BusinessRulesLocator.GetQRCodeCustomerRegisterBO().DeleteByPrimaryKey(QRCodeCustomerRegister_ID);
                    MyActionPermission.WriteLogSystem(QRCodeCustomerRegister_ID, "Xóa - " + _QRCodeCustomerRegisterRow.CustomerName);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
                //case "Active":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 1;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Kích hoạt thành công !");
                //    break;
                //case "Deactive":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 0;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;

        }
        lblMessage.Visible = true;
        FillQRCodeCustomerRegister();
    }

    protected void rptQRCodeCR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeCustomerregister_Add.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
    }
}