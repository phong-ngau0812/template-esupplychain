using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProductPackage_ProductPackage_Download : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string tenlo = string.Empty;
        string tensp = string.Empty;
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        if (ProductPackage_ID > 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow !=null)
            {
                tenlo = _ProductPackageRow.Name;
                tensp = _ProductPackageRow.ProductName;
            }
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(" select QRCodePublicContent from ProductItem where ProductPackage_ID=" + ProductPackage_ID);
            string attachment = "attachment; filename="+ tenlo + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "DANH SÁCH QR-CODE CÁC CÁ THỂ\n ";
            tab += "Lô sản xuất: "+tenlo+"\n";
            tab += "Sản phẩm: "+tensp+"\n";
            //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
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
    }
}