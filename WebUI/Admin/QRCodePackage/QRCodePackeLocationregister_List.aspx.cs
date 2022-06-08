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

public partial class QRCodePackeLocationregister_List : System.Web.UI.Page
{
    private int QRCodePackage_ID = 0;
    //chưa phân trang theo sql
    public string Message = "";
    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // btnAdd.Visible = MyActionPermission.CanAdd();
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            LoadData();
        }
        ResetMsg();
    }

    private void LoadData()
    {
        try
        {
            if (QRCodePackage_ID > 0)
            {
                QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                NameProductPackage = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                NameProduct = _QRCodePackageRow.IsProductNameNull ? string.Empty : _QRCodePackageRow.ProductName;
                TotalTem = _QRCodePackageRow.QRCodeNumber.ToString();
                SerialStart = _QRCodePackageRow.SerialNumberStart;
                SerialEnd = _QRCodePackageRow.SerialNumberEnd;
                DataTable dt = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetAsDataTable("QRCodePackage_ID=" + QRCodePackage_ID, "");
                rptLocation.DataSource = dt;
                rptLocation.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeLocationregister_Add.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
    }
    protected void rptLocation_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int QRCodeCustomerRegister_ID = Convert.ToInt32(e.CommandArgument);
        QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetByPrimaryKey(QRCodeCustomerRegister_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (MyActionPermission.CanDeleteQRCodePackeLocationregister(QRCodeCustomerRegister_ID, ref Message))
                {

                    BusinessRulesLocator.GetQRCodeLocationRegisterBO().DeleteByPrimaryKey(QRCodeCustomerRegister_ID);
                    MyActionPermission.WriteLogSystem(QRCodeCustomerRegister_ID, "Xóa - " + _QRCodeLocationRegisterRow.LocationRegister);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;

        }
        lblMessage.Visible = true;
        LoadData();
    }
    protected void rptLocation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
}