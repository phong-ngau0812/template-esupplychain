using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QRCodePackage_QRCodePackage_Download : System.Web.UI.Page
{
    public int QRCodePackage_ID = 0;
    public int QRCodePackageType_ID = 0;
    public int Product_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string host = HttpContext.Current.Request.Url.Host;
        //Response.Write(host);
        string tensp = string.Empty;
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!string.IsNullOrEmpty(Request["QRCodePackageType_ID"]))
            int.TryParse(Request["QRCodePackageType_ID"].ToString(), out QRCodePackageType_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (QRCodePackage_ID > 0)
        {
            QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                tensp = _QRCodePackageRow.Name;
            }

            DataTable dt;
            // if (Product_ID != -1)
            // {

            if (QRCodePackageType_ID == 2)
            {
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select QS.SerialNumber as SerialNumberCode,QS.QRCodePublicContent  as QRCodePublic,QS.QRCodeSecretContent as QRCodeSecret, SMSContent from QRCodeSecret QS where QS.QRCodePackage_ID=" + QRCodePackage_ID + " order by QS.SerialNumber ASC");
                DataTable dtPrimary = BusinessRulesLocator.Conllection().GetAllList("select QRCodePublicContent from QRCodePublic where QRCodePackage_ID="+ QRCodePackage_ID);
               
                string attachment = "attachment; filename=" + tensp + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
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
                        if (i == 1)
                        {
                            if (dtPrimary.Rows.Count==1)
                            {
                                Response.Write(tab + "https://esupplychain.vn/t1/" + dtPrimary.Rows[0]["QRCodePublicContent"].ToString());
                            }
                            else
                            {
                                Response.Write(tab + "https://esupplychain.vn/t1/" + dr[i].ToString());
                            }
                            
                        }
                        else if (i == 2)
                        {
                            Response.Write(tab + "https://esupplychain.vn/t2/" + dr[i].ToString());
                        }
                        else
                        {
                            Response.Write(tab + dr[i].ToString());
                        }
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            if (QRCodePackageType_ID == 1)
            {
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select QS.SerialNumber as SerialNumberCode,QS.QRCodePublicContent as QRCodePublic,'' as QRCodeSecret from QRCodePublic QS where QS.QRCodePackage_ID=" + QRCodePackage_ID + " order by QS.SerialNumber ASC");

                string attachment = "attachment; filename=" + tensp + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
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
                        if (i == 1)
                        {
                            Response.Write(tab + "https://esupplychain.vn/t1/" + dr[i].ToString());
                        }
                        else if (i == 2)
                        {
                            Response.Write(tab + "" + dr[i].ToString());
                        }
                        else
                        {
                            Response.Write(tab + dr[i].ToString());
                        }
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }

            //}
            //            else
            //            {
            //                if (QRCodePackageType_ID == 2)
            //                {
            //                    dt = BusinessRulesLocator.Conllection().GetAllList(@"select QR.SerialNumber as SerialNumberCode,
            //QR.QRCodePublicContent as QRCodePublic,
            //QS.QRCodeSecretContent as QRCodeSecret,
            //QS.SMSContent
            //from QRCodePublic QR 
            //inner join QRCodeSecret QS on QS.QRCodePackage_ID=QR.QRCodePackage_ID
            //where QR.QRCodePackage_ID=" + QRCodePackage_ID + " and QS.QRCodePackage_ID=" + QRCodePackage_ID + " and QR.SerialNumber= QS.SerialNumber order by QR.SerialNumber ASC");
            //                    string attachment = "attachment; filename=" + tensp + ".xls";
            //                    Response.ClearContent();
            //                    Response.AddHeader("content-disposition", attachment);
            //                    Response.ContentEncoding = System.Text.Encoding.Unicode;
            //                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //                    Response.ContentType = "application/vnd.ms-excel";
            //                    string tab = "";
            //                    foreach (DataColumn dc in dt.Columns)
            //                    {
            //                        Response.Write(tab + dc.ColumnName);
            //                        tab = "\t";
            //                    }
            //                    Response.Write("\n");
            //                    int i;
            //                    foreach (DataRow dr in dt.Rows)
            //                    {
            //                        tab = "";
            //                        for (i = 0; i < dt.Columns.Count; i++)
            //                        {
            //                            if (i == 1)
            //                            {
            //                                Response.Write(tab + "https://esupplychain.vn/t1/" + dr[i].ToString());
            //                            }
            //                            else if (i == 2)
            //                            {
            //                                Response.Write(tab + "https://esupplychain.vn/t2/" + dr[i].ToString());
            //                            }
            //                            else
            //                            {
            //                                Response.Write(tab + dr[i].ToString());
            //                            }
            //                            tab = "\t";
            //                        }
            //                        Response.Write("\n");
            //                    }
            //                    Response.End();
            //                }
            //                if (QRCodePackageType_ID == 1)
            //                {
            //                    dt = BusinessRulesLocator.Conllection().GetAllList(@"select QS.SerialNumber as SerialNumberCode,QS.QRCodePublicContent as QRCodePublic,'' as QRCodeSecret from QRCodePublic QS where QS.QRCodePackage_ID=" + QRCodePackage_ID + " order by QS.SerialNumber ASC");

            //                    string attachment = "attachment; filename=" + tensp + ".xls";
            //                    Response.ClearContent();
            //                    Response.AddHeader("content-disposition", attachment);
            //                    Response.ContentEncoding = System.Text.Encoding.Unicode;
            //                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //                    Response.ContentType = "application/vnd.ms-excel";
            //                    string tab = "";
            //                    foreach (DataColumn dc in dt.Columns)
            //                    {
            //                        Response.Write(tab + dc.ColumnName);
            //                        tab = "\t";
            //                    }
            //                    Response.Write("\n");
            //                    int i;
            //                    foreach (DataRow dr in dt.Rows)
            //                    {
            //                        tab = "";
            //                        for (i = 0; i < dt.Columns.Count; i++)
            //                        {
            //                            if (i == 1)
            //                            {
            //                                Response.Write(tab + "https://esupplychain.vn/t1/" + dr[i].ToString());
            //                            }
            //                            else if (i == 2)
            //                            {
            //                                Response.Write(tab + "" + dr[i].ToString());
            //                            }
            //                            else
            //                            {
            //                                Response.Write(tab + dr[i].ToString());
            //                            }
            //                            tab = "\t";
            //                        }
            //                        Response.Write("\n");
            //                    }
            //                    Response.End();

            //                }
            //            }
        }
    }
}