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

public partial class QRCodePackeWarehouseregister_List : System.Web.UI.Page
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
        // btnAdd.Visible = MyActionPermission.CanAdd();

        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            FillQRCodeWarehouseregister();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
 

    protected void FillQRCodeWarehouseregister()
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
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select QR.QRCodeWarehouseRegister_ID,  QR.SerialNumberStart , QR.SerialNumberEnd, QR.QRCodeNumber, W.Name as NameWarehouse
  from QRCodeWarehouseRegister QR
  left join Warehouse W on QR.Warehouse_ID = W.Warehouse_ID
  where QR.QRCodePackage_ID =" + QRCodePackage_ID + "ORDER by QR.CreateDate DESC");
            rptQRCodeWarehouse.DataSource = dt;
            rptQRCodeWarehouse.DataBind();

        }

    }
    protected void rptQRCodeWarehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int QRCodeWarehouseRegister_ID = Convert.ToInt32(e.CommandArgument);
        QRCodeWarehouseRegisterRow _QRCodeWarehouseRegisterRow = new QRCodeWarehouseRegisterRow();
        _QRCodeWarehouseRegisterRow = BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().GetByPrimaryKey(QRCodeWarehouseRegister_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_QRCodeWarehouseRegisterRow != null)
                {
                    BusinessRulesLocator.GetQRCodeWarehouseRegisterBO().DeleteByPrimaryKey(QRCodeWarehouseRegister_ID);
                    MyActionPermission.WriteLogSystem(QRCodeWarehouseRegister_ID, "Xóa - " + _QRCodeWarehouseRegisterRow.QRCodeWarehouseRegister_ID);
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
        FillQRCodeWarehouseregister();
    }

    protected void rptQRCodeWarehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeWarehouseregister_Add.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
    }



}