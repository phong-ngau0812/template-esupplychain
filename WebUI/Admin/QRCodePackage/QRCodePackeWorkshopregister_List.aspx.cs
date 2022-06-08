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

public partial class QRCodePackeWorkshopregister_List : System.Web.UI.Page
{
    //chưa phân trang theo sql
    public string Message = "";
    public string NameQRCodePackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    public string NameQRCodeStatus = "";

    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang

    public int QRCodePackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        // btnAdd.Visible = MyActionPermission.CanAdd();
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);

        if (!IsPostBack)
        {
            // FillWorkshop();
            LoadWorkshop();
            //FillQRCodePackage();
        }
        FillQRCodePackage();
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }


    protected void FillWorkshop()
    {
        string where = "";
        if (Common.GetFunctionGroupDN())
        {
            where = "and W.ProductBrand_ID =" + Convert.ToInt32(MyUser.GetProductBrand_ID());
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select ROW_NUMBER() OVER (ORDER BY Name) AS STT, W.Name 
  from Workshop W
  where 1=1" + where);
        rptWorkshop.DataSource = dt;
        rptWorkshop.DataBind();


    }



    protected void FillQRCodePackage()
    {
        int QRCodeStatus_ID = 0;
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
                QRCodeStatus_ID = _QRCodePackageRow.QRCodeStatus_ID;
                CheckSound.Checked = _QRCodePackageRow.SoundEnable;
                switch (QRCodeStatus_ID)
                {
                    case -3:
                        NameQRCodeStatus = "Tem chờ duyệt";
                        break;
                    case -2:
                        NameQRCodeStatus = "Tem đang tạo...";
                        break;
                    case -1:
                        NameQRCodeStatus = "Tem bị hủy";
                        break;
                    case 0:
                        NameQRCodeStatus = " <span class=\"badge badge-danger\">Tem mới tạo </span>";
                        break;
                    case 1:
                        NameQRCodeStatus = "Tem đã giao cho nhà in";
                        break;
                    case 2:
                        NameQRCodeStatus = " <span class=\"badge badge-success\"> Kích hoạt đưa mã tem ra thị trường </span>";
                        break;
                    case 3:
                        NameQRCodeStatus = "Tem đã sử dụng";
                        break;

                    default:
                        // code block
                        break;
                }

            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select QR.QRCodeWorkshopRegister_ID,  QR.SerialNumberStart , QR.SerialNumberEnd, QR.QRCodeNumber, W.Name as NameWorkshop
  from QRCodeWorkshopRegister QR
  left join Workshop W on QR.Workshop_ID = W.Workshop_ID
  where QR.QRCodePackage_ID = " + QRCodePackage_ID + "ORDER by QR.CreateDate DESC");
            rptQRCodePackeWorkshop.DataSource = dt;
            rptQRCodePackeWorkshop.DataBind();

        }

    }
    protected void rptQRCodePackeWorkshop_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int QRCodeWorkshopRegister_ID = Convert.ToInt32(e.CommandArgument);
        QRCodeWorkshopRegisterRow _QRCodeWorkshopRegisterRow = new QRCodeWorkshopRegisterRow();
        _QRCodeWorkshopRegisterRow = BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().GetByPrimaryKey(QRCodeWorkshopRegister_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_QRCodeWorkshopRegisterRow != null)
                {
                    BusinessRulesLocator.GetQRCodeWorkshopRegisterBO().DeleteByPrimaryKey(QRCodeWorkshopRegister_ID);
                    MyActionPermission.WriteLogSystem(QRCodeWorkshopRegister_ID, "Xóa - " + _QRCodeWorkshopRegisterRow.QRCodeWorkshopRegister_ID);
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
        FillQRCodePackage();
    }

    protected void rptQRCodePackeWorkshop_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeWorkshopregister_Add.aspx?QRCodePackage_ID=" + QRCodePackage_ID, false);
    }



    private void LoadWorkshop()
    {
        int ProductBrand_ID = 0;
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }

            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().GetWorkshop_paging(Pager1.CurrentIndex, pageSize, 7, ProductBrand_ID, 0, 0, txtSearch.Text,0, 0, 0, 0, "RowNum ASC", 0, 0);
            //dtSet = BusinessRulesLocator.Conllection().GetWorkshop_paging(1, 1000, 7, 0, 0, "", "");
            rptWorkshop.DataSource = dtSet.Tables[0];
            rptWorkshop.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadFarm", ex.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }

    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadWorkshop();
    }
}