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

public partial class Warehouse_Inventory_List : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDlddlWarehouse();
            FillddlZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            LoadWarehouse();
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

    private void FillDDlddlWarehouse()
    {
        

        string where = "";
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable("Active=1 and Type = 1 " + where, "");
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
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
    protected void LoadWarehouse()
    {
        try
        {
            string where = "";
            
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and  WI.ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }
            if(ddlWarehouse.SelectedValue != "0")
            {
                where += " and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and W.Zone_ID =" + ddlZone.SelectedValue;
            }
            if (ddlArea.SelectedValue != "0")
            {
                where += " and W.Area_ID=" + ddlArea.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select DISTINCT WI.Material_ID, WI.Name,MI.Unit,  (select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=WI.Material_ID and Active=1 ) as TongNhap
  , (select ISNULL( SUM(Amount),0) from WarehouseExportMaterial where Material_ID=WI.Material_ID and WarehouseExport_ID in (select WarehouseExport_ID from WarehouseExport where Active=1) ) as TongXuat,
  ((select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=WI.Material_ID  and Active=1 ) - (select ISNULL( SUM(Amount),0) from WarehouseExportMaterial where Material_ID=WI.Material_ID  and WarehouseExport_ID in (select WarehouseExport_ID from WarehouseExport where Active=1))) as TonKho 
   from WarehouseImport WI 
   left join Material  MI on WI.Material_ID = MI.Material_ID
   left join Warehouse W on W.Warehouse_ID = MI.Warehouse_ID
   where WI.Active = 1 " + where + "  ORDER BY  WI.Name ASC");
            rptWarehouse.DataSource=dt;
            rptWarehouse.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }


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
            string where = " and WI.ProductBrand_ID= " + ProductBrand_ID;
            string NameWarehouse = "";
             if (ddlWarehouse.SelectedValue != "0")
            {
                where += "and WI.Warehouse_ID = " + ddlWarehouse.SelectedValue;
                NameWarehouse += ddlWarehouse.SelectedItem;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  WI.Name as TenVatTu ,  (select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=WI.Material_ID and Active=1 ) as TongNhap
  , (select ISNULL( SUM(Amount),0) from WarehouseExportMaterial where Material_ID=WI.Material_ID  ) as TongXuat,
  ((select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=WI.Material_ID  and Active=1 ) - (select ISNULL( SUM(Amount),0) from WarehouseExportMaterial where Material_ID=WI.Material_ID   )) as TonKho ,MI.Unit as DonViTinh
   from WarehouseImport WI 
   left join Material  MI on WI.Material_ID = MI.Material_ID  
   where WI.Active = 1 " + where + "  ORDER BY  WI.Name ASC");

            //          dt = BusinessRulesLocator.Conllection().GetAllList(@"  select  M.Name as Tênvậttư ,(select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=M.Material_ID and Active=1) as TổngNhập
            //, (select ISNULL( SUM(Amount),0) from WarehouseExport where Material_ID=M.Material_ID  and Active=1) as TổngXuất,
            //((select ISNULL( SUM(Amount),0) from WarehouseImport where Material_ID=M.Material_ID  and Active=1) - (select ISNULL( SUM(Amount),0) from WarehouseExport where Material_ID=M.Material_ID  and Active=1)) as TồnKho , M.Unit as ĐơnVịTính
            // from Material M where M.Active = 1 " + where + " ORDER BY  M.Name ASC");

            string attachment = "attachment; filename= file_excle_ton_kho_vat_tu " + ProductBrand_ID + NameWarehouse + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "DANH SÁCH TỒN KHO VẬT TƯ\n ";
            tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
            tab += "kho: " + NameWarehouse + "\n";
            
            //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName.Replace("TenVatTu", "Tên Vật Tư").Replace("TongNhap", "Tổng Nhập").Replace("TongXuat", "Tổng Xuất ").Replace("TonKho", "Tồn kho").Replace("DonViTinh", "Đơn Vị Tính"));
            
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        else
        {
            lblMessage.Text = "Bạn chưa chọn doanh nghiệp " ;
            lblMessage.ForeColor = Color.Red;
            lblMessage.BackColor = Color.Wheat;
            lblMessage.Visible = true;
        }


    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlZone();
        FillArea();
        LoadWarehouse();
        
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillArea();
        LoadWarehouse();
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
}