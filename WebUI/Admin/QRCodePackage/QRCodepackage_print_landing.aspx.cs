using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QRCodePackage_QRCodepackage_print_landing : System.Web.UI.Page
{
    public string title = "IN TEM CHO LÔ MÃ";
    public int QRCodePackage_ID = 0;
    public int QRCodePackageType_ID = 0;
    public int Product_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!string.IsNullOrEmpty(Request["QRCodePackageType_ID"]))
            int.TryParse(Request["QRCodePackageType_ID"].ToString(), out QRCodePackageType_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
    }
}