using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class _Default : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 5;//Số bản ghi 1 trang
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetFunctionGroup_ID() == "5" || MyUser.GetFunctionGroup_ID() == "6")
                {
                    divDoanhNghiep.Visible = false;
                    Admin.Visible = false;
                    Giamsat.Visible = false;
                }
                else
                {
                    divDoanhNghiep.Visible = true;
                    Admin.Visible = false;
                    Giamsat.Visible = false;
                    ThongKeNhanhDoanhNghiep();
                    LoadChartWarehouseExportByYear();
                    LoadChartWarehouseProductExportByYear();
                    LoadChartZone();
                    LoadChartProductPackageOrder();
                    LoadChartProductPackage();
                    LoadChartQRCode();
                }

            }
            else if (MyUser.GetFunctionGroup_ID() == "1")
            {
                Admin.Visible = true;
                divDoanhNghiep.Visible = false;
                Giamsat.Visible = false;
                LoadChartProductBrandByYear();

                LoadChartProductBrand();
                LoadChartProduct();
                LoadChartProductPackage();
                LoadChartQRCode();
                LoadReport();

            }
            else
            {
                if (MyUser.GetFunctionGroup_ID() == "8" || MyUser.GetFunctionGroup_ID() == "9")
                {
                    Giamsat.Visible = false;
                }
                else
                {
                    Giamsat.Visible = true;
                }

                divDoanhNghiep.Visible = false;
                Admin.Visible = false;
                LoadDataGS();
            }
        }
    }
    public int index = 0;
    public string listdn = "";
    protected string ReturnProductBrandName(int ProductBrand_ID)
    {
        string name = string.Empty;
        if (ProductBrand_ID != 0)
        {
            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
            if (_ProductBrandRow != null)
            {
                name = _ProductBrandRow.Name;
            }
        }
        return name;
    }
    protected void LoadDataGS()
    {
        DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable(" UserID='" + MyUser.GetUser_ID() + "'", "");
        if (dt.Rows.Count == 1)
        {
            string list = dt.Rows[0]["ProductBrand_ID_List"].ToString();
            string[] array = list.Split(',');
            index = 0;
            foreach (string value in array)
            {
                index++;
                listdn += "- " + ReturnProductBrandName(Convert.ToInt32(value)) + " <br/>";
            }
        }
    }
    public int TongPhong = 0;
    public int TongNV = 0;
    public int TongKhach = 0;
    public int TongNCC = 0;
    public int TongVC = 0;
    protected void ThongKeNhanhDoanhNghiep()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("  select COUNT(*) as Count from Department where Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID());
            TongPhong = Convert.ToInt32(dt.Rows[0]["Count"].ToString());

            string workshop_list = string.Empty;
            string where = string.Empty;
            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    if (!BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Convert.ToInt32(MyUser.GetZone_ID())).IsWorkshop_IDNull)
                    {
                        workshop_list = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Convert.ToInt32(MyUser.GetZone_ID())).Workshop_ID;
                        workshop_list = workshop_list.Substring(1, workshop_list.Length - 1);
                        workshop_list = workshop_list.Remove(workshop_list.Length - 1);
                        where += " and Workshop_ID in (" + workshop_list + ")";
                    }
                }
            }
            dt = BusinessRulesLocator.Conllection().GetAllList("  select COUNT(*) as Count from Workshop where Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID() + where);
            TongNV = Convert.ToInt32(dt.Rows[0]["Count"].ToString());

            dt = BusinessRulesLocator.Conllection().GetAllList("  select COUNT(*) as Count from Customer where ProductBrand_ID=" + MyUser.GetProductBrand_ID());
            TongKhach = Convert.ToInt32(dt.Rows[0]["Count"].ToString());

            dt = BusinessRulesLocator.Conllection().GetAllList("  select COUNT(*) as Count from Supplier where Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID());
            TongNCC = Convert.ToInt32(dt.Rows[0]["Count"].ToString());

            dt = BusinessRulesLocator.Conllection().GetAllList("  select COUNT(*) as Count from Transporter where Active=1 and ProductBrand_ID=" + MyUser.GetProductBrand_ID());
            TongVC = Convert.ToInt32(dt.Rows[0]["Count"].ToString());
        }
        catch (Exception ex)
        {
            Log.writeLog("ThongKeNhanhDoanhNghiep", ex.ToString());
        }
    }
    protected void LoadChartProductBrandByYear()
    {
        int Year = DateTime.Now.Year;
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetChartProductBrand(Year);
            if (dt.Rows.Count == 1)
            {
                NowYear.Value = dt.Rows[0]["DataNow"].ToString();
                LastYear.Value = dt.Rows[0]["DataLast"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChartProductBrandByYear", ex.ToString());
        }
    }
    protected void LoadChartZone()
    {
        int Year = DateTime.Now.Year;
        int ProductBrand_ID = 0;

        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                DataTable dt = BusinessRulesLocator.Conllection().GetChartZone(ProductBrand_ID);
                if (dt.Rows.Count != 0)
                {
                    Zone.Value = dt.Rows[0]["Zone"].ToString();
                    Area.Value = dt.Rows[0]["Area"].ToString();
                    Farm.Value = dt.Rows[0]["Farm"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChartProductBrandByYear", ex.ToString());
        }
    }
    protected void LoadChartWarehouseExportByYear()
    {
        int Year = DateTime.Now.Year;
        int ProductBrand_ID = 0;

        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());

                DataTable dt = BusinessRulesLocator.Conllection().GetChartWarehouseExport(Year, ProductBrand_ID);
                if (dt.Rows.Count != 0)
                {
                    precious1.Value = dt.Rows[0]["precious1"].ToString();
                    precious2.Value = dt.Rows[0]["precious2"].ToString();
                    precious3.Value = dt.Rows[0]["precious3"].ToString();
                    precious4.Value = dt.Rows[0]["precious4"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChartProductBrandByYear", ex.ToString());
        }
    }
    protected void LoadChartWarehouseProductExportByYear()
    {
        int Year = DateTime.Now.Year;
        int ProductBrand_ID = 0;
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());

                DataTable dt = BusinessRulesLocator.Conllection().GetChartWarehouseProductExport(Year, ProductBrand_ID);
                if (dt.Rows.Count != 0)
                {
                    Product_precious1.Value = dt.Rows[0]["precious1"].ToString();
                    Product_precious2.Value = dt.Rows[0]["precious2"].ToString();
                    Product_precious3.Value = dt.Rows[0]["precious3"].ToString();
                    Product_precious4.Value = dt.Rows[0]["precious4"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChartProductBrandByYear", ex.ToString());
        }
    }
    public string dn, lo, product = string.Empty;
    protected void LoadChartProductBrand()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select (select count(*) from ProductBrand where Active=1 and Branch_ID=1) NongNghiep, 
(select count(*)  from ProductBrand where Active=1 and Branch_ID=2) TrongTrot,
(select count(*)  from ProductBrand where Active=1 and Branch_ID=4) ThuyHaiSan");
        NongNghiep.Value = dt.Rows[0]["NongNghiep"].ToString();
        ChanNuoi.Value = dt.Rows[0]["TrongTrot"].ToString();
        ThuyHaiSan.Value = dt.Rows[0]["ThuyHaiSan"].ToString();
    }
    protected void LoadChartProduct()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select (select count(*) from Product where Active=1 and ProductBrand_ID in ( select ProductBrand_ID from ProductBrand where  Active=1 and Branch_ID=1)) NongNghiep, 
(select count(*) from Product where Active=1 and ProductBrand_ID in ( select ProductBrand_ID from ProductBrand where  Active=1 and Branch_ID=2)) TrongTrot,
(select count(*) from Product where Active=1 and ProductBrand_ID in ( select ProductBrand_ID from ProductBrand where  Active=1 and Branch_ID=4)) ThuyHaiSan");
        NongNghiepSP.Value = dt.Rows[0]["NongNghiep"].ToString();
        ChanNuoiSP.Value = dt.Rows[0]["TrongTrot"].ToString();
        ThuyHaiSanSP.Value = dt.Rows[0]["ThuyHaiSan"].ToString();
    }
    protected void LoadChartProductPackage()
    {
        string where = string.Empty;
        if (Common.GetFunctionGroupDN())
        {
            where = " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
        }
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select (select count(*) from ProductPackage where ProductPackageStatus_ID=1 " + where + ") DangSanXuat, " +
"(select count(*) from ProductPackage where ProductPackageStatus_ID=2 " + where + " ) DangThuHoach, " +
"(select count(*) from ProductPackage where ProductPackageStatus_ID=3 " + where + " ) ThuHoachXong, " +
"(select count(*) from ProductPackage where ProductPackageStatus_ID=4 " + where + " ) Huy");
        DangSanXuat.Value = dt.Rows[0]["DangSanXuat"].ToString();
        DangThuHoach.Value = dt.Rows[0]["DangThuHoach"].ToString();
        ThuHoachXong.Value = dt.Rows[0]["ThuHoachXong"].ToString();
        Huy.Value = dt.Rows[0]["Huy"].ToString();
    }
    protected void LoadChartProductPackageOrder()
    {

        string where = "";
        if (Common.GetFunctionGroupDN())
        {
            where += " and ProductBrand_ID = " + Convert.ToInt32(MyUser.GetProductBrand_ID());

        }
        DataTable dtLSXCD = BusinessRulesLocator.Conllection().GetAllList(@"select (select count(*) as Count from ProductPackageOrder where Active=1 and Approve=0" + where + ") Lenhchuaduyet, " +
            "(select count(*) as Count from ProductPackageOrder where Active=1 and Approve=1" + where + ") Lenhdaduyet");
        Lenhchuaduyet.Value = dtLSXCD.Rows[0]["Lenhchuaduyet"].ToString();
        // DataTable dtLSXDD = BusinessRulesLocator.Conllection().GetAllList("select count(*) as Count from ProductPackageOrder where Active=1 and Approve=1" + where);
        Lenhdaduyet.Value = dtLSXCD.Rows[0]["Lenhdaduyet"].ToString();
    }
    private void LoadData()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select count(*) as Count from ProductBrand where Active=1");
            if (dt.Rows.Count > 0)
            {
                dn = dt.Rows[0]["Count"].ToString();
            }
            dt = BusinessRulesLocator.Conllection().GetAllList("select count(*) as Count from Product where Active=1");
            if (dt.Rows.Count > 0)
            {
                product = dt.Rows[0]["Count"].ToString();
            }
            dt = BusinessRulesLocator.Conllection().GetAllList("select count(*) as Count from ProductPackage");
            if (dt.Rows.Count > 0)
            {
                lo = dt.Rows[0]["Count"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    protected void LoadChartQRCode()
    {
        int Year = DateTime.Now.Year;
        int ProductBrand_ID = 0;
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
            DataTable dt = BusinessRulesLocator.Conllection().GetChartQRCode(Year, ProductBrand_ID);
            if (dt.Rows.Count != 0)
            {
                QRCode.Value = dt.Rows[0]["TongTem"].ToString();
                QRCodeActive.Value = dt.Rows[0]["TemKichHoat"].ToString();
                QRCodeNotActive.Value = dt.Rows[0]["TemChuaKichHoat"].ToString();

            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadChartQRcodeByYear", ex.ToString());
        }


    }
    protected void LoadReport()
    {
        try
        {
            string where = string.Empty;
            where = " CreateDate between '" + ctlDatePicker1.FromDate + "' And '" + ctlDatePicker1.ToDate + "'";

            rptReport.DataSource = BusinessRulesLocator.Conllection().GetAllList(@"Select L.Name, (Select Count(Product_ID) from Product where ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID = L.Location_ID) and Active = 1) as TotalActive,(Select Count(Product_ID) from Product where ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID = L.Location_ID) and Active <> 1) as TotalNoActive, ISNULL((Select Count(Product_ID) from Product where ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID = L.Location_ID) and Active = 1  and" + where + "), '0')  as TotalProductNew from Location L order by TotalActive DESC");
            rptReport.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadReport", ex.ToString());
        }
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LoadReport();
        }

    }
    protected void rptReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Literal lblText = e.Item.FindControl("lblText") as Literal;
            Literal lblTotalActive = e.Item.FindControl("lblTotalActive") as Literal;
            Literal lblTotalProductNew = e.Item.FindControl("lblTotalProductNew") as Literal;
            if (lblTotalProductNew != null)
            {
                if (lblTotalProductNew.Text != "0")
                {
                    if (lblTotalProductNew.Text != lblTotalActive.Text)
                    {
                        lblText.Text = "<span style='color: green;'>" + (Convert.ToInt32(lblTotalProductNew.Text)) + "</span>";
                    }
                    else
                    {
                        lblText.Text = "0";
                    }

                }
                else
                {
                    lblText.Text = "0";
                }
            }



        }
    }


}