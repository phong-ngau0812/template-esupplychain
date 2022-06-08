using DbObj;
using OnBarcode.Barcode;
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
using SystemFrameWork;

public partial class TruyXuatProduct : System.Web.UI.Page
{
    private int Product_ID = 0;
    public int No = 0;
    public int NoCB = 0;
    public int NoKN = 0;
    public int NoDT = 0;
    public string audio;
    public string message;
    public string logo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductID"]))
            Product_ID = Convert.ToInt32(Request["ProductID"].ToString());
        // if (!IsPostBack)
        {
            LoadData();
        }
        //Response.Write(qrcodecontent);
    }
    public DataTable dtImg;
    public string storysp, storybrand, storebrand, ProductName, SGTIN, Image, ProductBrandName, ProductInfo, review, ProductQT, LogoBrand, TenGiaoDich, DiaChi, Phone, Web, Price, PriceOld, discount, video = string.Empty;
    private void LoadData()
    {
        try
        {
            LogoBrand = "/images/no-image-icon.png";
            if (Product_ID > 0)
            {
                ConfigAudioVsMessageRow _ConfigRow = BusinessRulesLocator.GetConfigAudioVsMessageBO().GetByPrimaryKey(1);
                ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                if (_ProductRow != null)
                {
                    Nodata.Visible = false;
                    Data.Visible = true;
                    QRCode();
                    //Product Info
                    DataTable dtInfo = BusinessRulesLocator.GetProductInfoBO().GetAsDataTable("Product_ID=" + Product_ID, "");
                    ProductInfoRow _ProductInfoRow = new ProductInfoRow();
                    if (dtInfo.Rows.Count == 0)
                    {
                        //Fill info product

                        _ProductInfoRow.Product_ID = Product_ID;
                        _ProductInfoRow.Price = 0;
                        _ProductInfoRow.Discount = 0;
                        _ProductInfoRow.CreateBy = MyUser.GetUser_ID();
                        _ProductInfoRow.CreateDate = DateTime.Now;
                        BusinessRulesLocator.GetProductInfoBO().Insert(_ProductInfoRow);
                        _ProductInfoRow = BusinessRulesLocator.GetProductInfoBO().GetByPrimaryKey(_ProductInfoRow.ProductInfo_ID);
                    }
                    dtInfo = BusinessRulesLocator.GetProductInfoBO().GetAsDataTable("Product_ID=" + Product_ID, "");
                    if (dtInfo.Rows.Count == 1)
                    {
                        Price = float.Parse(dtInfo.Rows[0]["Price"].ToString()).ToString("N0") + " đ";
                        PriceOld = (float.Parse(dtInfo.Rows[0]["Price"].ToString()) - (float.Parse(dtInfo.Rows[0]["Price"].ToString()) / 100 * Convert.ToInt32(dtInfo.Rows[0]["Discount"].ToString()))).ToString("N0") + " đ";
                        discount = dtInfo.Rows[0]["Discount"].ToString() + " %";
                        video = string.IsNullOrEmpty(dtInfo.Rows[0]["VideoYoutube"].ToString()) ? "" : @"<div class='embed-responsive embed-responsive-16by9'>
                        <iframe class='embed-responsive-item' src='" + dtInfo.Rows[0]["VideoYoutube"].ToString() + "' allowfullscreen></iframe></div>";

                        //ProductReview
                        productinfo_id.Value = dtInfo.Rows[0]["ProductInfo_ID"].ToString();
                        DataTable Review = BusinessRulesLocator.Conllection().GetReview(Convert.ToInt32(dtInfo.Rows[0]["ProductInfo_ID"].ToString()));
                        rptReview.DataSource = Review;
                        rptReview.DataBind();
                        review = Review.Rows.Count.ToString();

                        DataTable dtSum = BusinessRulesLocator.Conllection().GetAllList("   select SUM (Star) as Total from ProductReview where ProductInfo_ID=" + dtInfo.Rows[0]["ProductInfo_ID"].ToString() + " and Approved=1 and Status=1");
                        if (dtSum.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dtSum.Rows[0]["Total"].ToString()))
                            {
                                double star = double.Parse(dtSum.Rows[0]["Total"].ToString()) / Review.Rows.Count;
                                //Response.Write(star);
                                if (1.5 < star && star < 2.5)
                                {
                                    lblStar.Text = @"<div>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star'></span>
                                <span class='fa fa-star'></span>
                                <span class='fa fa-star'></span>
                            </div>";
                                }
                                else if (2.5 <= star && star < 3.5)
                                {
                                    lblStar.Text = @"<div>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star'></span>
                                <span class='fa fa-star'></span>
                            </div>";
                                }
                                else if (3.5 <= star && star < 4.5)
                                {
                                    lblStar.Text = @"<div>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star'></span>
                            </div>";
                                }
                                else if (4.5 <= star)
                                {
                                    lblStar.Text = @"<div>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                                <span class='fa fa-star checked'></span>
                            </div>";
                                }
                            }
                        }
                    }

                    //LoadBanner
                    dtImg = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Type=1 and Product_ID=" + Product_ID, " SORT ASC");
                    if (dtImg.Rows.Count > 0)
                    {
                        ImgThumbnail.Visible = false;
                        rptGallery.Visible = true;
                        rptGallery.DataSource = dtImg;
                        rptGallery.DataBind();
                    }
                    else
                    {
                        ImgThumbnail.Visible = true;
                        rptGallery.Visible = false;
                        Image = _ProductRow.IsImageNull ? "/images/no-image-icon.png" : "/data/product/mainimages/original/" + _ProductRow.Image;
                    }

                    ProductName = _ProductRow.IsNameNull ? "" : _ProductRow.Name.ToUpper();
                    SGTIN = _ProductRow.IsSGTINNull ? "" : "Mã vạch : " + _ProductRow.SGTIN;
                    ProductInfo = _ProductRow.IsContentNull ? "Đang cập nhật" : _ProductRow.Content;
                    ProductQT = _ProductRow.IsQualityDescriptionNull ? "Đang cập nhật" : _ProductRow.QualityDescription;
                    storysp = _ProductRow.IsStoryNull ? "Đang cập nhật" : _ProductRow.Story;
                    //Load Quality
                    if (!_ProductRow.IsQuality_IDNull)
                    {
                        DataTable dtQuality = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" Quality_ID=" + _ProductRow.Quality_ID.ToString(), "");
                        rptQuality.DataSource = dtQuality;
                        rptQuality.DataBind();
                    }
                    //Load ProductBrand
                    if (!_ProductRow.IsProductBrand_IDNull)
                    {

                        ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductRow.ProductBrand_ID);
                        if (_ProductBrandRow != null)
                        {
                            ProductBrandName = _ProductBrandRow.IsNameNull ? string.Empty : _ProductBrandRow.Name;
                            LogoBrand = _ProductBrandRow.IsImageNull ? "/images/no-image-icon.png" : "/data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                            TenGiaoDich = _ProductBrandRow.IsTradingNameNull ? string.Empty : _ProductBrandRow.TradingName;
                            DiaChi = _ProductBrandRow.IsAddressNull ? string.Empty : _ProductBrandRow.Address;
                            Phone = _ProductBrandRow.IsTelephoneNull ? string.Empty : _ProductBrandRow.Telephone;
                            Web = _ProductBrandRow.IsWebsiteNull ? string.Empty : _ProductBrandRow.Website;
                            storybrand = _ProductBrandRow.IsStoryNull ? "Đang cập nhật" : _ProductBrandRow.Story;
                            storebrand = _ProductBrandRow.IsDescriptionNull ? "Đang cập nhật" : _ProductBrandRow.Description;
                            if (_ProductBrandRow.ShowLogo == true)
                            {
                                if (!_ProductBrandRow.IsImageNull)
                                {
                                    logo = "../data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                                }
                                else
                                {
                                    logo = "/images/logo-login.png?v=2";
                                }
                            }
                            else
                            {
                                logo = "/images/logo-login.png?v=2";
                            }
                        }
                        //Load Sản phẩm cùng doanh nghiệp
                        DataTable dtProductOther = BusinessRulesLocator.Conllection().GetProductOther(_ProductRow.ProductBrand_ID, Product_ID);
                        rptProduct.DataSource = dtProductOther;
                        rptProduct.DataBind();
                    }

                    //Ảnh công bố SP
                    //LoadBanner
                    DataTable dtCB = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Type=2 and Product_ID=" + Product_ID, "");
                    rptCongBoSP.DataSource = dtCB;
                    rptCongBoSP.DataBind();

                    DataTable dtKN = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Type=3 and Product_ID=" + Product_ID, "");
                    rptKN.DataSource = dtKN;
                    rptKN.DataBind();

                    DataTable dtDT = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Type=4 and Product_ID=" + Product_ID, "");
                    rptDT.DataSource = dtDT;
                    rptDT.DataBind();


                    if (!_ProductRow.IsAudioPublicNull)
                    {
                        audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ProductRow.AudioPublic).Note;
                    }
                    else
                    {
                        audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioPublic).Note;
                    }
                    if (!_ProductRow.IsMessagePublicNull)
                    {
                        message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ProductRow.MessagePublic).Note;
                    }
                    else
                    {
                        message = " Bạn đang truy xuất nguồn gốc trên Hệ thống quản lý truy xuất nguồn gốc trong chuỗi cung ứng của CheckVN";
                    }

                    //string[] ProductBrand = { "1619", "1608", "1596" };
                    //var result = Array.Find(ProductBrand, element => element == Convert.ToString(_ProductRow.ProductBrand_ID));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductRow.ProductBrand_ID);
                    //    if (!_ProductBrandRow.IsImageNull)
                    //    {
                    //        logo = "../data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                    //    }
                    //    else
                    //    {
                    //        logo = "/images/logo-login.png?v=2";
                    //    }
                    //}
                    //else
                    //{
                    //    logo = "/images/logo-login.png?v=2";
                    //}
                }
                else
                {
                    Nodata.Visible = true;
                    Data.Visible = false;
                }

            }
            //if (!string.IsNullOrEmpty(qrcodecontent))
            //{
            //    QRCodePublicRow _QRCodePublicRow = BusinessRulesLocator.GetQRCodePublicBO().GetByPrimaryKey(qrcodecontent);
            //    if (_QRCodePublicRow != null)
            //    {
            //        if (_QRCodePublicRow.QRCodeStatus_ID == 0)
            //        {
            //            Nodata.Visible = true;
            //            Data.Visible = false;
            //        }
            //        else
            //        {
            //            Nodata.Visible = false;
            //            Data.Visible = true;
            //            if (_QRCodePublicRow != null)
            //            {
            //                Product_ID = _QRCodePublicRow.Product_ID;
            //                //Response.Write(Product_ID);

            //            }
            //        }
            //    }
            //    else
            //    {
            //        //Tem không tồn tại trên hệ thống
            //        Nodata.Visible = true;
            //        Data.Visible = false;

            //    }
            //}
        }
        catch (Exception ex)
        {

            Log.writeLog(ex.ToString(), "LoadData");
        }
    }
    public void QRCode()
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("https://esupplychain.vn/p/" + Product_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                lblQR.Text = "<img class='qr-code' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
    }

}