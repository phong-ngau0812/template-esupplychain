using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using ZXing.QrCode.Internal;
using System.Text;
using ZXing.QrCode;
using System.Collections;
using ZXing.Common;
using ZXing;
using System.Drawing.Imaging;

public partial class ProductPackage_Trace : System.Web.UI.Page
{
    public string name, namepackage, demuc, code, lenhsx, hosx, product, gln = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        txtDate.Attributes.Add("readonly", "true");
        ResetMsg();
        Init();
    }

    //protected string LoadHtml()
    //{
    //    string html = string.Empty;
    //    try
    //    {

    //        DataTable dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable("Status=1", "");
    //        foreach (DataRow item in dt.Rows)
    //        {
    //            html += "<p style='margin-top: 10px; margin-bottom: 0px;'><b>" + item["Name"] + "</b></p>";
    //            DataTable dtTask = BusinessRulesLocator.GetTaskBO().GetAsDataTable(" TaskType_ID=" + item["TaskType_ID"] + " and ProductPackage_ID=" + ProductPackage_ID, "");

    //            foreach (DataRow itemTask in dtTask.Rows)
    //            {
    //                html += "<table style='width: 100%;'>" +
    //                                              "<tbody><tr>" +
    //                                                  "<td class='col-left'>Đề mục công việc</td>" +
    //                                                  "<td><b>" + itemTask["Name"] + "</b></td>" +
    //                                              "</tr>" +
    //                                              "<tr>" +
    //                                                  "<td class='col-left'>Thời gian</td>" +
    //                                                  "<td>" + DateTime.Parse(itemTask["StartDate"].ToString()).ToString("dd/MM/yyyy") + "</td>" +
    //                                              "</tr>" +
    //                                              "<tr>" +
    //                                                  "<td class='col-left'>Nội dung</td>" +
    //                                                  "<td>" + itemTask["Description"] + "</td>" +
    //                                              "</tr>" +
    //                                              "<tr>" +
    //                                                  "<td class='col-left'>Vị trí</td>" +
    //                                                  "<td>" + itemTask["Location"] + "</td>" +
    //                                              "</tr>" +
    //                                                "<tr>" +
    //                                                  "<td class='col-left'>Ảnh minh họa</td>" +
    //                                                  "<td><img class='mainimage' src='" + (string.IsNullOrEmpty( itemTask["Image"].ToString())?"": "https://esupplychain.vn/data/task/"+ itemTask["Image"]) + "'/></td>" +
    //                                              "</tr>" +
    //                                          "</tbody></table>" +
    //                                          "<div class='solline'></div>";
    //            }

    //        }


    //    }
    //    catch (Exception ex)
    //    {

    //        Log.writeLog(ex.ToString(), "LoadHtml");
    //    }
    //    return html;
    //}
    private void Init()
    {

        if (Product_ID != 0)
        {
            name = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID).Name;
        }

        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow != null)
            {
                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                product = txtProductName.Text = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_ProductPackageRow.Product_ID).Name;
                txtProductName.Enabled = false;
                ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRow.ProductBrand_ID);
                if (_ProductBrandRow != null)
                {
                    gln = _ProductBrandRow.IsGLNNull ? string.Empty : _ProductBrandRow.GLN;
                }

                //    clipboardTextarea.Text = LoadHtml();
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetProductPackageHistoryBO().GetAsDataTable("ProductPackage_ID= " + ProductPackage_ID, "");
                if (dt.Rows.Count > 0)
                {
                    btnExportQR.Visible = true;
                    if (!IsPostBack)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["SSCC_Logistic"].ToString()))
                        {
                            txtSSCC_Logistic.Text = dt.Rows[0]["SSCC_Logistic"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["GLN_Receive"].ToString()))
                        {
                            txtGLN_Receive.Text = dt.Rows[0]["GLN_Receive"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["GLN_From"].ToString()))
                        {
                            txtGLN_From.Text = dt.Rows[0]["GLN_From"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["GLN_To"].ToString()))
                        {
                            txtGLN_To.Text = dt.Rows[0]["GLN_To"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["DeliveryDate"].ToString()))
                        {
                            txtDate.Text = DateTime.Parse(dt.Rows[0]["DeliveryDate"].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {
                    btnExportQR.Visible = false;
                }


            }
        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
    }

    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code=" + code, false);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetProductPackageHistoryBO().GetAsDataTable("ProductPackage_ID= " + ProductPackage_ID, "");
        if (dt.Rows.Count > 0)
        {
            ProductPackageHistoryRow _ProductPackageHistoryRow = new ProductPackageHistoryRow();
            if (!string.IsNullOrEmpty(dt.Rows[0]["ProductPackageHistory_ID"].ToString()))
            {
                _ProductPackageHistoryRow = BusinessRulesLocator.GetProductPackageHistoryBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["ProductPackageHistory_ID"].ToString()));
                if (_ProductPackageHistoryRow != null)
                {
                    _ProductPackageHistoryRow.SSCC_Logistic = txtSSCC_Logistic.Text.Trim();
                    _ProductPackageHistoryRow.GLN_Receive = txtGLN_Receive.Text.Trim();
                    _ProductPackageHistoryRow.GLN_From = txtGLN_From.Text.Trim();
                    _ProductPackageHistoryRow.GLN_To = txtGLN_To.Text.Trim();
                    if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _ProductPackageHistoryRow.DeliveryDate = ngaycap;
                    }
                    _ProductPackageHistoryRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductPackageHistoryRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetProductPackageHistoryBO().Update(_ProductPackageHistoryRow);
                }
            }
        }
        else
        {
            ProductPackageHistoryRow _ProductPackageHistoryRow = new ProductPackageHistoryRow();
            _ProductPackageHistoryRow.ProductPackage_ID = ProductPackage_ID;
            _ProductPackageHistoryRow.SSCC_Logistic = txtSSCC_Logistic.Text.Trim();
            _ProductPackageHistoryRow.GLN_Receive = txtGLN_Receive.Text.Trim();
            _ProductPackageHistoryRow.GLN_From = txtGLN_From.Text.Trim();
            _ProductPackageHistoryRow.GLN_To = txtGLN_To.Text.Trim();
            if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                DateTime ngaycap = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _ProductPackageHistoryRow.DeliveryDate = ngaycap;
            }
            _ProductPackageHistoryRow.CreateBy = MyUser.GetUser_ID();
            _ProductPackageHistoryRow.CreateDate = DateTime.Now;
            BusinessRulesLocator.GetProductPackageHistoryBO().Insert(_ProductPackageHistoryRow);
        }
        lblMessage.Text = "Cập nhật dữ liệu thành công";
        lblMessage.Visible = true;
        Init();
    }
    protected void btnExportQR_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetProductPackageHistoryBO().GetAsDataTable("ProductPackage_ID= " + ProductPackage_ID, "");
        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dt.Rows[0]["ProductPackageHistory_ID"].ToString()))
            {
                ProductPackageHistoryRow _ProductPackageHistoryRow = new ProductPackageHistoryRow();
                _ProductPackageHistoryRow = BusinessRulesLocator.GetProductPackageHistoryBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["ProductPackageHistory_ID"].ToString()));
                if (_ProductPackageHistoryRow != null)
                {
                    string qr_code = string.Empty;

                    qr_code += "- Mã truy vết đơn vị logistic: " + (_ProductPackageHistoryRow.IsSSCC_LogisticNull ? string.Empty : _ProductPackageHistoryRow.SSCC_Logistic);
                    qr_code += "\n- Mã lô sản xuất: " + code;
                    qr_code += "\n- Tên hàng hóa: " + product;
                    qr_code += "\n- Địa điểm gửi đi: " + (_ProductPackageHistoryRow.IsGLN_FromNull ? string.Empty : _ProductPackageHistoryRow.GLN_From);
                    qr_code += "\n- Ngày chuyển hàng: " + (_ProductPackageHistoryRow.IsDeliveryDateNull ? string.Empty : _ProductPackageHistoryRow.DeliveryDate.ToString("dd/MM/yyyy"));
                    qr_code += "\n- Mã truy vết bên gửi: " + gln;
                    qr_code += "\n\n Link truy cập hệ thống truy xuất nguồn gốc: http://esupplychain.vn/";

                    var writer = new BarcodeWriter();
                    var options = new QrCodeEncodingOptions
                    {
                        DisableECI = true,
                        CharacterSet = "UTF-8",
                        Width = 500,
                        Height = 500,
                    };
                    writer.Format = BarcodeFormat.QR_CODE;
                    writer.Options = options;
                    var result = writer.Write(qr_code);

                    string path = Server.MapPath("~/images/QRImage.jpg");
                    var barcodeBitmap = new Bitmap(result);
                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            Response.ContentType = "image/png";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "QR_file" + ".png");
                            barcodeBitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

        }
    }
}