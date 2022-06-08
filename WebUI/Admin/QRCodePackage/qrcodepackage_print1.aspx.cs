using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QRCodePackage_qrcodepackage_print1 : System.Web.UI.Page
{
    public int QRCodePackage_ID = 0;
    public int QRCodePackageType_ID = 0;
    public int Product_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string host = HttpContext.Current.Request.Url.Host;
        //Response.Write(host);
        string tensp = string.Empty;
        string hsd = string.Empty;
        string ndg = string.Empty;
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!string.IsNullOrEmpty(Request["QRCodePackageType_ID"]))
            int.TryParse(Request["QRCodePackageType_ID"].ToString(), out QRCodePackageType_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        DataTable dtQR = new DataTable();
        dtQR.Clear();
        dtQR.Columns.Add("Name");
        dtQR.Columns.Add("HSD");
        dtQR.Columns.Add("NDG");
        dtQR.Columns.Add("QRCode");
        dtQR.Columns.Add("Serial");
        if (QRCodePackage_ID > 0)
        {
            QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                tensp = _QRCodePackageRow.ProductName;
                hsd = _QRCodePackageRow.IsWarrantyEndDateNull ? string.Empty : _QRCodePackageRow.WarrantyEndDate.ToString("dd/MM/yyyy");
                ndg = _QRCodePackageRow.IsHarvestDateNull ? string.Empty : _QRCodePackageRow.HarvestDate.ToString("dd/MM/yyyy");
            }

            DataTable dt;
            // if (Product_ID != -1)
            // {

            if (QRCodePackageType_ID == 2)
            {
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select QS.SerialNumber as SerialNumberCode,QS.QRCodePublicContent  as QRCodePublic,QS.QRCodeSecretContent as QRCodeSecret, SMSContent from QRCodeSecret QS where QS.QRCodePackage_ID=" + QRCodePackage_ID + " order by QS.SerialNumber ASC");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        DataRow workRow = dtQR.NewRow();
                        workRow["Name"] = tensp.ToString();
                        workRow["HSD"] = hsd;
                        workRow["NDG"] = ndg;
                        workRow["Serial"] = dtRow["SerialNumberCode"].ToString();
                        workRow["QRCode"] = QRCode("/t2/" + dtRow["QRCodeSecret"].ToString());
                        dtQR.Rows.Add(workRow);

                    }
                }
            }
            if (QRCodePackageType_ID == 1)
            {
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select QS.SerialNumber as SerialNumberCode,QS.QRCodePublicContent as QRCodePublic,'' as QRCodeSecret from QRCodePublic QS where QS.QRCodePackage_ID=" + QRCodePackage_ID + " order by QS.SerialNumber ASC");
                foreach (DataRow dtRow in dt.Rows)
                {
                    DataRow workRow = dtQR.NewRow();
                    workRow["Name"] = tensp.ToString();
                    workRow["HSD"] = hsd;
                    workRow["NDG"] = ndg;
                    workRow["Serial"] = dtRow["SerialNumberCode"].ToString();
                    workRow["QRCode"] = QRCode("/t1/" + dtRow["QRCodePublic"].ToString());
                    dtQR.Rows.Add(workRow);
                }
            }
        }
        if (dtQR.Rows.Count > 0)
        {
            rptQRCode.DataSource = dtQR;
            rptQRCode.DataBind();
        }
    }
    public string QRCode(string txtQRCode)
    {
        string img = string.Empty;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("https://esupplychain.vn" + txtQRCode.Trim().ToString(), QRCodeGenerator.ECCLevel.L);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 200;
        imgBarCode.Width = 200;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                img = "<img style='width:100%;' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
        return img;
    }
}