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

public partial class WarehouseProduct_Inventory : System.Web.UI.Page
{

    public decimal TotalTonkho = 0;
    public decimal TotalThuhoach = 0;
    public decimal TotalBanHang = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillddlZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            LoadProduct();
            LoadProductPackage();
            LoadWarehouse();
            LoadWarehouseProduct();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
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
    protected void FillddlZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Zone_ID, Name from Zone where Active=1" + where);
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }
    protected void FillArea()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Area_ID, Name from Area where 1=1" + where);
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }
    private void LoadProduct()
    {
        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
        }

        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select Name, Product_ID from Product where Active = 1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void LoadProductPackage()
    {

        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlProduct.SelectedValue != "0")
            {
                where += "and Product_ID = " + ddlProduct.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackage_ID from ProductPackage where ProductPackageStatus_ID in (1,2,3)" + where + " order by Name ASC");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.Items.Insert(0, new ListItem("-- Chọn Lô sản xuất --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }



    private void LoadWarehouse()
    {

        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable("Type = 2 and Active = 1 " + where, "");
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho sản phẩm  --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected decimal CalculateAmountImport(string Product_ID)
    {
        return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongNhap from WarehouseImportProductPackage where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.Active =1 and WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongNhap"].ToString());
    }
    protected decimal CalculateAmountExport(string Product_ID)
    {
        return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.Active =1 and WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
    }
    private void LoadWarehouseProduct()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " And WI.ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlProduct.SelectedValue != "0")
            {
                where += " And WI.Product_ID=" + ddlProduct.SelectedValue;
            }
            if (ddlWarehouse.SelectedValue != "0")
            {
                where += " And WI.Warehouse_ID=" + ddlWarehouse.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and W.Zone_ID=" + ddlZone.SelectedValue;
            }
            if (ddlArea.SelectedValue != "0")
            {
                where += " and W.Area_ID=" + ddlArea.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("WarehouseName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ImportAmount");
            dt.Columns.Add("ExportAmount");
            dt.Columns.Add("ProductInventory");
            dt.Columns.Add("ProductUnit");
            TotalTonkho = 0;
            TotalThuhoach = 0;
            TotalBanHang = 0;
            DataTable dtValues = BusinessRulesLocator.Conllection().GetAllList(@"Select distinct WI.Product_ID, P.Name, P.ExpectedProductivityDescription,W.Name as WarehouseName from WarehouseImport WI
left join Product P on WI.Product_ID = P.Product_ID 
left join Warehouse W on WI.Warehouse_ID = W.Warehouse_ID
where WI.Active = 1 and WI.WarehouseImportType_ID = 2" + where);


            if (dtValues.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtValues.Rows)
                {
                    DataRow _row = dt.NewRow();
                    _row["WarehouseName"] = dtRow["WarehouseName"].ToString();
                    _row["ProductName"] = dtRow["Name"].ToString();
                    _row["ProductUnit"] = dtRow["ExpectedProductivityDescription"].ToString();
                    _row["ImportAmount"] = CalculateAmountImport(dtRow["Product_ID"].ToString());
                    _row["ExportAmount"] = CalculateAmountExport(dtRow["Product_ID"].ToString());
                    _row["ProductInventory"] = (CalculateAmountImport(dtRow["Product_ID"].ToString()) - CalculateAmountExport(dtRow["Product_ID"].ToString())).ToString("N0");
                    dt.Rows.Add(_row);
                }
                rptWarehouseExport.DataSource = dt;
                rptWarehouseExport.DataBind();

            }
            else
            {
                rptWarehouseExport.DataSource = null;
                rptWarehouseExport.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadFarm", ex.ToString());
        }
    }


    decimal TongThuHoach = 0;
    decimal TongBanHang = 0;
    decimal TongTonKho = 0;
    protected void ExportFile()
    {
        string tendoanhnghiep = "";
        int ProductBrand_ID = 0;

        if (ddlProductBrand.SelectedValue != "0")
        {
            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ddlProductBrand.SelectedValue));
            if (_ProductBrandRow != null)
            {
                ProductBrand_ID = _ProductBrandRow.ProductBrand_ID;
                tendoanhnghiep = _ProductBrandRow.Name;

            }

            string NameWarehouse = "";
            if (ddlWarehouse.SelectedValue != "0")
            {
                NameWarehouse += ddlWarehouse.SelectedItem;
            }
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " And WI.ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlProduct.SelectedValue != "0")
            {
                where += " And WI.Product_ID=" + ddlProduct.SelectedValue;
            }
            if (ddlWarehouse.SelectedValue != "0")
            {
                where += " And WI.Warehouse_ID=" + ddlWarehouse.SelectedValue;
            }
            DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.Conllection().spGetWarehouseProduct_paging(Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWarehouse.SelectedValue), Convert.ToInt32(ddlProductPackage.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, "");
            
            dt.Clear();
            dt.Columns.Add("WarehouseName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ImportAmount");
            dt.Columns.Add("ExportAmount");
            dt.Columns.Add("ProductInventory");
            dt.Columns.Add("ProductUnit");
            TotalTonkho = 0;
            TotalThuhoach = 0;
            TotalBanHang = 0;
            DataTable dtValues = BusinessRulesLocator.Conllection().GetAllList(@"Select distinct WI.Product_ID, P.Name, P.ExpectedProductivityDescription,W.Name as WarehouseName from WarehouseImport WI
left join Product P on WI.Product_ID = P.Product_ID 
left join Warehouse W on WI.Warehouse_ID = W.Warehouse_ID
where WI.Active = 1 and WI.WarehouseImportType_ID = 2" + where);


            if (dtValues.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtValues.Rows)
                {
                    DataRow _row = dt.NewRow();
                    _row["WarehouseName"] = dtRow["WarehouseName"].ToString();
                    _row["ProductName"] = dtRow["Name"].ToString();
                    _row["ProductUnit"] = dtRow["ExpectedProductivityDescription"].ToString();
                    _row["ImportAmount"] = CalculateAmountImport(dtRow["Product_ID"].ToString());
                    _row["ExportAmount"] = CalculateAmountExport(dtRow["Product_ID"].ToString());
                    _row["ProductInventory"] = (CalculateAmountImport(dtRow["Product_ID"].ToString()) - CalculateAmountExport(dtRow["Product_ID"].ToString())).ToString("N0");
                    dt.Rows.Add(_row);
                }

            }
            string attachment = "attachment; filename= file_excle_ton_kho_san_pham " + ProductBrand_ID + NameWarehouse + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "DANH SÁCH TỒN KHO Sản Phẩm \n ";
            tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
            tab += "kho: " + NameWarehouse + "\n";

            //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName.Replace("WarehouseName", "Tên kho").Replace("ProductName", "Tên lô").Replace("ImportAmount", "Tổng nhập").Replace("ExportAmount", "Tổng xuất").Replace("ProductInventory", "Tồn kho").Replace("ProductUnit","Đơn vị"));
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                TongThuHoach += decimal.Parse(dr["ImportAmount"].ToString());
                TongBanHang += decimal.Parse(dr["ExportAmount"].ToString());
                TongTonKho += decimal.Parse(dr["ProductInventory"].ToString());
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";

                }
                Response.Write("\n");
            }
            Response.Write("\t");
            Response.Write("" + "Tổng");
            Response.Write("\t" + TongThuHoach);
            Response.Write("\t" + TongBanHang);
            Response.Write("\t" + TongTonKho);
            Response.End();
        }
        else
        {
            lblMessage.Text = "Bạn chưa chọn doanh nghiệp ";
            lblMessage.ForeColor = Color.Red;
            lblMessage.BackColor = Color.Wheat;
            lblMessage.Visible = true;
        }


    }

    protected void rptWarehouseExport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblTongThuHoach = e.Item.FindControl("lblTongThuHoach") as Literal;
            Literal lblTongBanHang = e.Item.FindControl("lblTongBanHang") as Literal;
            Literal lblTonKho = e.Item.FindControl("lblTonKho") as Literal;

            if (lblTongThuHoach != null)
            {
                TotalThuhoach += decimal.Parse(lblTongThuHoach.Text);
            }
            if (lblTongBanHang != null)
            {
                TotalBanHang += decimal.Parse(lblTongBanHang.Text);
            }
            if (lblTonKho != null)
            {
                TotalTonkho += decimal.Parse(lblTonKho.Text);
            }
        }
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlZone();
        FillArea();
        LoadWarehouseProduct();
        

    }


    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
            LoadWarehouseProduct();
    }



    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseProduct();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillArea();
        LoadWarehouseProduct();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseProduct();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFile();
        //LoadWarehouseProduct();
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseProduct();
    }


    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseProduct();
    }


}