using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class WarehouseExport_Download : System.Web.UI.Page
{
    public string Exporter = "";
    public string title = " Phiếu xuất kho";
    public int No = 1;
    public string NameProductBrand = "";
    public string AddressProductBrand = "";
    public string sophieu = "";
    public string ProductPackageOrder = "";
    public int WarehouseExport_ID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["WarehouseExport_ID"]))
            int.TryParse(Request["WarehouseExport_ID"].ToString(), out WarehouseExport_ID);

        if (!IsPostBack)
        {
            loadProductBrand();
            LoadWarehouseExportFile();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void loadProductBrand()
    {
        int ProductPackageOrder_ID = 0;
        if (WarehouseExport_ID != 0)
        {
            WarehouseExportRow _WarehouseExportRow = new WarehouseExportRow();
            _WarehouseExportRow = BusinessRulesLocator.GetWarehouseExportBO().GetByPrimaryKey(WarehouseExport_ID);
            if (_WarehouseExportRow != null)
            {
                if (_WarehouseExportRow.ProductBrand_ID != 0)
                {
                    ProductBrandRow _ProductBrandRow = new ProductBrandRow();
                    _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_WarehouseExportRow.ProductBrand_ID);
                    NameProductBrand = _ProductBrandRow.Name;
                    AddressProductBrand = _ProductBrandRow.Address;

                }
                ProductPackageOrder_ID = _WarehouseExportRow.IsProductPackageOrder_IDNull ? 0 : _WarehouseExportRow.ProductPackageOrder_ID;
                if (ProductPackageOrder_ID != 0)
                {
                    ProductPackageOrderRow _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(_WarehouseExportRow.ProductPackageOrder_ID);
                    ProductPackageOrder = "Lệnh sản xuất: " + (_ProductPackageOrderRow.IsNameNull ? string.Empty : _ProductPackageOrderRow.Name);
                }



            }
            sophieu = "PX-" + WarehouseExport_ID;
        }
    }


    protected void LoadWarehouseExportFile()
    {
        try
        {
            if (WarehouseExport_ID != 0)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetPrintWarehouseExport(WarehouseExport_ID);
                rptMaterial.DataSource = dt;
                rptMaterial.DataBind();
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseExport_List.aspx", false);
    }

    public decimal Total = 0;
    protected void rptMaterial_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblPrice = e.Item.FindControl("lblPrice") as Literal;
            Literal lblAmount = e.Item.FindControl("lblAmount") as Literal;
            if (lblPrice != null && lblAmount != null)
            {
                Total += decimal.Parse(lblPrice.Text) * decimal.Parse(lblAmount.Text);
            }
        }
    }
}