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
using SystemFrameWork;

public partial class TruyXuat : System.Web.UI.Page
{
    public string red = string.Empty;
    public string none = string.Empty;
    public string audio;
    public string message;
    public string logo;
    private string qrcodecontent = string.Empty;
    private int Product_ID = 0;
    public int No = 0;
    public int NoCB = 0;
    public int NoKN = 0;
    public int NoDT = 0;
    public int NoQuestion = 1;
    public int NoHistory = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["qrcodecontent"]))
            qrcodecontent = Request["qrcodecontent"].ToString();
        // if (!IsPostBack)
        {
            LoadData();
        }
        //Response.Write(qrcodecontent);
    }
    public DataTable dtImg;
    public DataTable dtImgProduct;
    public DataTable dtFlag;
    public string storysp, storybrand, prinfo, supplier, storebrand, ProductName, SGTIN, Image, vungtrong, ProductBrandName, ProductInfo, review, ProductQT, LogoBrand, TenGiaoDich, DiaChi, Phone, Web, Price, PriceOld, discount, video, Chuoi, Vaitro, LoaihinhDN, DoitacDN, SoSao = string.Empty;
    public string tenlo, losx, PO = string.Empty;
    public string noneSX, noneVattu, noneThuhoach, noneCheBien, noneVanchuyen, nsx, hsd = string.Empty;
    private void LoadData()
    {
        try
        {
            ConfigAudioVsMessageRow _ConfigRow = BusinessRulesLocator.GetConfigAudioVsMessageBO().GetByPrimaryKey(1);

            LogoBrand = "/images/no-image-icon.png";
            if (!string.IsNullOrEmpty(qrcodecontent))
            {
                QRCodePublicRow _QRCodePublicRow = BusinessRulesLocator.GetQRCodePublicBO().GetByPrimaryKey(qrcodecontent);
                if (_QRCodePublicRow != null)
                {
                    _QRCodePublicRow.ViewCount = _QRCodePublicRow.IsViewCountNull ? 0 : _QRCodePublicRow.ViewCount + 1;
                    BusinessRulesLocator.GetQRCodePublicBO().Update(_QRCodePublicRow);
                    if (_QRCodePublicRow.QRCodeStatus_ID == 0)
                    {

                        Data.Visible = false;
                        //Tem chưa kích hoạt
                        if (_ConfigRow != null)
                        {
                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioActive).Note;
                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.MessageActive).Note;
                            red = "red";
                        }
                    }
                    else if (_QRCodePublicRow.QRCodeStatus_ID == -1)
                    {
                        Data.Visible = false;
                        //Tem bị hủy
                        if (_ConfigRow != null)
                        {
                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioCancel).Note;
                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.MessageCancel).Note;
                            red = "red";
                        }
                    }
                    else if (_QRCodePublicRow.QRCodeStatus_ID == 1)
                    {
                        //Tem giao cho nhà in
                        if (_ConfigRow != null)
                        {
                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioPrint).Note;
                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.AudioPrint).Note;
                            red = "red";
                        }
                    }
                    else
                    {
                        // Nodata.Visible = false;
                        Data.Visible = true;
                        if (_QRCodePublicRow != null)
                        {
                            Product_ID = _QRCodePublicRow.Product_ID;
                            //Response.Write(Product_ID);
                            TrackingLocation(Product_ID, qrcodecontent, _QRCodePublicRow.SerialNumber);
                            if (Product_ID > 0)
                            {
                                ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                                if (_ProductRow != null)
                                {
                                    QRCode();
                                    //Fill logo Doanh nghiệp
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
                                    //CreateTable Flag
                                    dtFlag = new DataTable();
                                    dtFlag.Clear();
                                    dtFlag.Columns.Add("STT");
                                    dtFlag.Columns.Add("Name");

                                    dtImg = BusinessRulesLocator.GetProductGalleryBO().GetAsDataTable("Type=1 and Product_ID=" + Product_ID, " SORT ASC");
                                    dtImgProduct = BusinessRulesLocator.GetProductBO().GetAsDataTable("Active=1 and Product_ID=" + Product_ID, "");
                                    int CountP = 1;
                                    if (dtImgProduct.Rows.Count > 0)
                                    {
                                        foreach (DataRow dtRow in dtImgProduct.Rows)
                                        {
                                            DataRow dtRowFlag = dtFlag.NewRow();
                                            dtRowFlag["STT"] = CountP;
                                            if (!string.IsNullOrEmpty(dtRow["Image"].ToString()))
                                            {
                                                dtRowFlag["Name"] = "../data/product/mainimages/original/" + dtRow["Image"];
                                            }
                                            else
                                            {
                                                dtRowFlag["Name"] = "../images/no-image-icon.png";
                                            }
                                            dtFlag.Rows.Add(dtRowFlag);
                                            CountP++;
                                        }
                                    }
                                    if (dtImg.Rows.Count > 0)
                                    {
                                        foreach (DataRow dtRow in dtImg.Rows)
                                        {
                                            DataRow dtRowFlag = dtFlag.NewRow();
                                            dtRowFlag["STT"] = CountP;
                                            dtRowFlag["Name"] = "../data/product/product_info/" + dtRow["Image"];
                                            dtFlag.Rows.Add(dtRowFlag);
                                            CountP++;
                                        }
                                    }
                                    if (dtFlag.Rows.Count > 0)
                                    {
                                        rptGallery.Visible = true;
                                        rptGallery.DataSource = dtFlag;
                                        rptGallery.DataBind();

                                    }

                                    //if (dtImg.Rows.Count > 0)
                                    //{
                                    //    ImgThumbnail.Visible = false;
                                    //    rptGallery.Visible = true;
                                    //    rptGallery.DataSource = dtImg;
                                    //    rptGallery.DataBind();
                                    //}
                                    //else
                                    //{
                                    //    ImgThumbnail.Visible = true;
                                    //    rptGallery.Visible = false;
                                    //    Image = _ProductRow.IsImageNull ? "/images/no-image-icon.png" : "/data/product/mainimages/original/" + _ProductRow.Image;
                                    //}

                                    ProductName = _ProductRow.IsNameNull ? "" : _ProductRow.Name.ToUpper() + "- <b style='color:#0dc31a'>(Cá thể: " + _QRCodePublicRow.SerialNumber + ")</b>";
                                    SGTIN = _ProductRow.IsSGTINNull ? "" : "Mã vạch : " + _ProductRow.SGTIN;
                                    storysp = _ProductRow.IsStoryNull ? "Đang cập nhật" : _ProductRow.Story;
                                    ProductInfo = _ProductRow.IsContentNull ? "Đang cập nhật" : _ProductRow.Content;
                                    ProductQT = _ProductRow.IsQualityDescriptionNull ? "Đang cập nhật" : _ProductRow.QualityDescription;
                                    //Load Quality
                                    string quality = string.Empty;
                                    if (!_ProductRow.IsQuality_IDNull)
                                    {
                                        DataTable dtQuality = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" Quality_ID=" + _ProductRow.Quality_ID.ToString(), "");
                                        rptQuality.DataSource = dtQuality;
                                        rptQuality.DataBind();
                                        if (dtQuality.Rows.Count > 0)
                                        {
                                            if (!string.IsNullOrEmpty(dtQuality.Rows[0]["Name"].ToString()))
                                            {
                                                quality = dtQuality.Rows[0]["Name"].ToString();
                                            }
                                            if (!string.IsNullOrEmpty(dtQuality.Rows[0]["Image"].ToString()))
                                            {
                                                quality += "&emsp;<img src='" + dtQuality.Rows[0]["Image"].ToString() + "' width='50px'/>";
                                            }
                                        }

                                    }
                                    //Load ProductBrand
                                    if (!_QRCodePublicRow.IsQRCodePackage_IDNull)
                                    {
                                        QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(_QRCodePublicRow.QRCodePackage_ID);
                                        if (!_QRCodePublicRow.IsProductBrand_IDNull)
                                        {
                                            hsd = _QRCodePackageRow.IsWarrantyEndDateNull ? "" : _QRCodePackageRow.WarrantyEndDate.ToString("dd-MM-yyyy");
                                            nsx = _QRCodePackageRow.IsManufactureDateNull ? "" : _QRCodePackageRow.ManufactureDate.ToString("dd-MM-yyyy");
                                            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_QRCodePackageRow.ProductBrand_ID);
                                            if (_ProductBrandRow != null)
                                            {

                                                ProductBrandName = _ProductBrandRow.IsNameNull ? string.Empty : _ProductBrandRow.Name;
                                                LogoBrand = _ProductBrandRow.IsImageNull ? "/images/no-image-icon.png" : "/data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                                                TenGiaoDich = _ProductBrandRow.IsTradingNameNull ? string.Empty : _ProductBrandRow.TradingName;
                                                DiaChi = _ProductBrandRow.IsAddressNull ? string.Empty : _ProductBrandRow.Address;
                                                Phone = _ProductBrandRow.IsTelephoneNull ? string.Empty : _ProductBrandRow.Telephone;
                                                Web = _ProductBrandRow.IsWebsiteNull ? string.Empty : _ProductBrandRow.Website;
                                                //Fill Logo Doanh Nghiệp
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
                                                DataTable dtDanhgia = BusinessRulesLocator.Conllection().GetAllList("SELECT e.Star as SoSao From ProductBrand pb LEft JOiN Evaluate e on pb.ProductBrand_ID = e.ProductBrand_ID WHERE pb.ProductBrand_ID=" + _ProductBrandRow.ProductBrand_ID);
                                                if (dtDanhgia.Rows[0]["SoSao"].ToString() == "1")
                                                {
                                                    SoSao = "<img src='../../theme/assets/images/1.jpg'/>";
                                                }
                                                else if (dtDanhgia.Rows[0]["SoSao"].ToString() == "2")
                                                {
                                                    SoSao = "<img src='../../theme/assets/images/2.jpg'/>";
                                                }
                                                else if (dtDanhgia.Rows[0]["SoSao"].ToString() == "3")
                                                {
                                                    SoSao = "<img src='../../theme/assets/images/3.jpg'/>";
                                                }
                                                else if (dtDanhgia.Rows[0]["SoSao"].ToString() == "4")
                                                {
                                                    SoSao = "<img src='../../theme/assets/images/4.jpg'/>";
                                                }
                                                else if (dtDanhgia.Rows[0]["SoSao"].ToString() == "5")
                                                {
                                                    SoSao = "<img src='../../theme/assets/images/5.jpg'/>";
                                                }
                                                else
                                                {
                                                    SoSao = "";
                                                }


                                                if (!_ProductBrandRow.IsChainLink_IDNull)
                                                {
                                                    ChainLinkRow _ChainLinkrow = BusinessRulesLocator.GetChainLinkBO().GetByPrimaryKey(_ProductBrandRow.ChainLink_ID);
                                                    if (_ChainLinkrow != null)
                                                    {
                                                        Chuoi = _ChainLinkrow.IsNameNull ? string.Empty : _ChainLinkrow.Name;
                                                    }

                                                }
                                                if (!string.IsNullOrEmpty(_ProductBrandRow.ProductBrandType_ID_List))
                                                {
                                                    string ProductTypeList = _ProductBrandRow.ProductBrandType_ID_List.Substring(1, _ProductBrandRow.ProductBrandType_ID_List.Length - 2);
                                                    DataTable dtProductBrandType = BusinessRulesLocator.GetProductBrandTypeBO().GetAsDataTable(" ProductBrandType_ID in (" + ProductTypeList + ")", "");
                                                    foreach (DataRow dtRow in dtProductBrandType.Rows)
                                                    {
                                                        LoaihinhDN += dtRow["Name"] + ", ";
                                                    }
                                                    LoaihinhDN = LoaihinhDN.Remove(LoaihinhDN.Length - 2);
                                                }
                                                Vaitro = _ProductBrandRow.IsRoleChain_IDNull ? string.Empty : _ProductBrandRow.RoleChain_ID.ToString() == "0" ? "Chủ chuỗi" : "Thành viên";
                                                if (!_ProductBrandRow.IsChainLink_IDNull && _ProductBrandRow.ChainLink_ID != 0)
                                                {
                                                    DataTable dtDN = BusinessRulesLocator.Conllection().GetAllList("SELECT DISTINCT ProductBrand_ID,Name from ProductBrand WHERE ChainLink_ID =" + _ProductBrandRow.ChainLink_ID + " and ProductBrand_ID <>" + _ProductBrandRow.ProductBrand_ID);
                                                    foreach (DataRow dtRow in dtDN.Rows)
                                                    {
                                                        DoitacDN += dtRow["Name"] + ", ";
                                                    }
                                                    DoitacDN = DoitacDN.Remove(DoitacDN.Length - 2);
                                                }

                                                storybrand = _ProductBrandRow.IsStoryNull ? "Đang cập nhật" : _ProductBrandRow.Story;
                                                prinfo = _ProductBrandRow.IsStoryNull ? "Đang cập nhật" : _ProductBrandRow.PRInfo;
                                                storebrand = _ProductBrandRow.IsDescriptionNull ? "Đang cập nhật" : _ProductBrandRow.Description;
                                            }
                                            //Load Sản phẩm cùng doanh nghiệp
                                            DataTable dtProductOther = BusinessRulesLocator.Conllection().GetProductOther(_QRCodePublicRow.ProductBrand_ID, Product_ID);
                                            rptProduct.DataSource = dtProductOther;
                                            rptProduct.DataBind();
                                        }

                                        //LoadNhatKySX
                                        if (!_QRCodePackageRow.IsProductPackage_IDNull)
                                        {
                                            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(_QRCodePackageRow.ProductPackage_ID);
                                            GetTransport(_QRCodePackageRow.ProductPackage_ID);
                                            GetPP(_QRCodePackageRow.ProductPackage_ID);
                                            LoadHistory(_QRCodePackageRow.ProductPackage_ID);
                                            if (_ProductPackageRow != null)
                                            {
                                                lblCode.Text = _ProductPackageRow.IsCodeNull ? string.Empty : _ProductPackageRow.Code;
                                                losx = tenlo = _ProductPackageRow.IsSGTINNull ? string.Empty : _ProductPackageRow.SGTIN;
                                                //tenlo += "<br><b>&emsp;+ Ngày sản xuất:</b> " + _ProductPackageRow.StartDate.ToString("dd/MM/yyy") + "<br><b>&emsp;+ Ngày thu hoạch:</b> " + (_QRCodePackageRow.IsHarvestDateNull ? string.Empty : _QRCodePackageRow.HarvestDate.ToString("dd/MM/yyy"));
                                                tenlo += "<br><b>&emsp;+ Ngày sản xuất:</b> " + _ProductPackageRow.StartDate.ToString("dd/MM/yyy");
                                                if (!_ProductPackageRow.IsCodePONull)
                                                {
                                                    xuanhoa.Visible = true;
                                                    PO = _ProductPackageRow.CodePO;
                                                }
                                                else
                                                {
                                                    xuanhoa.Visible = false;
                                                }
                                                if (!_ProductPackageRow.IsWorkshop_IDNull)
                                                {
                                                    WorkshopRow _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(_ProductPackageRow.Workshop_ID);
                                                    if (_WorkshopRow != null)
                                                    {
                                                        if (!_ProductPackageRow.IsZone_IDNull)
                                                        {
                                                            vungtrong += "<br><b>&emsp;+ Vùng sản xuất:</b> " + BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(_ProductPackageRow.Zone_ID).Name + "";
                                                        }

                                                        vungtrong += "<br><b>&emsp;+ Hộ sản xuất (nhân viên):</b> " + _WorkshopRow.Name + "";
                                                        vungtrong += "<br><b>&emsp;+ Email :</b> " + (_WorkshopRow.IsEmailNull ? string.Empty : _WorkshopRow.Email);
                                                        vungtrong += "<br><b>&emsp;+ Số điện thoại :</b> " + (_WorkshopRow.IsPhoneNull ? string.Empty : _WorkshopRow.Phone);
                                                        vungtrong += "<br><b>&emsp;+ Địa chỉ: </b>" + (_WorkshopRow.IsAddressNull ? string.Empty : _WorkshopRow.Address);
                                                        tenlo += "<br><b>&emsp;+ Chứng nhận: </b>" + quality;
                                                    }
                                                }
                                                if (!_QRCodePackageRow.IsSupplier_IDNull)
                                                {
                                                    SupplierRow _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(_QRCodePackageRow.Supplier_ID);
                                                    if (_SupplierRow != null)
                                                    {
                                                        supplier += @"<b>&emsp;+ Tên đơn vị:</b> " + _SupplierRow.Name + "<br/><b>&emsp;+ Địa chỉ: </b>" + _SupplierRow.Address + "";
                                                    }
                                                    else
                                                    {
                                                        supplier += @"<b>&emsp;+ Tên đơn vị:</b> " + ProductBrandName + "<br/><b>&emsp;+ Địa chỉ: </b>" + DiaChi + "";
                                                    }

                                                }
                                            }
                                            LoatDataTaskHistorySX(_QRCodePackageRow.ProductPackage_ID, rptSX);
                                            LoatDataTaskHistoryVT(_QRCodePackageRow.ProductPackage_ID, rptVT);
                                            LoatDataTaskHistoryTH(_QRCodePackageRow.ProductPackage_ID, rptTaskHistoryTH);
                                            LoatDataTaskHistoryCB(_QRCodePackageRow.ProductPackage_ID, rptTaskHistoryCB);
                                            LoatDataTaskHistoryTransport(_QRCodePackageRow.ProductPackage_ID, rptVC);
                                            LoatDataTaskHistoryPP(_QRCodePackageRow.ProductPackage_ID, rptPhanPhoi);
                                        }
                                        else
                                        {
                                            none = "none1";
                                        }

                                        if (!_QRCodePackageRow.IsAudioPublicNull)
                                        {
                                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_QRCodePackageRow.AudioPublic).Note;
                                        }
                                        else if (!_ProductRow.IsAudioPublicNull)
                                        {
                                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ProductRow.AudioPublic).Note;
                                        }
                                        else
                                        {
                                            audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioPublic).Note;
                                        }
                                        if (!_QRCodePackageRow.IsMessagePublicNull)
                                        {
                                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_QRCodePackageRow.MessagePublic).Note;
                                        }
                                        else if (!_ProductRow.IsMessagePublicNull)
                                        {
                                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ProductRow.MessagePublic).Note;
                                        }
                                        else
                                        {
                                            message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.MessagePublic).Note;
                                        }


                                    }
                                    else
                                    {
                                        audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioPublic).Note;
                                        message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.MessagePublic).Note;
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
                                }

                            }
                            else
                            {
                                // Nodata.Visible = true;
                                Data.Visible = false;
                                message = "Tem thuộc sản phẩm không xác định, vui lòng phân chia tem cho lô sản xuất !";
                            }
                        }
                    }
                }
                else
                {
                    //Tem không tồn tại trên hệ thống
                    if (_ConfigRow != null)
                    {
                        audio = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(_ConfigRow.AudioNoExsits).Note;
                        message = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(_ConfigRow.MessageNoExsits).Note;
                        //lblTitle.Text = message;
                        red = "red";
                    }
                    //Nodata.Visible = true;
                    Data.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "LoadData");
        }
    }

    protected void TrackingLocation(int Product_ID, string QRCodeContent, string Serial)
    {
        try
        {
            QRCodeTrackingRow _QRCodeTrackingRow = new QRCodeTrackingRow
            {
                QRCodeSerial = Serial,
                QRCodeContent = QRCodeContent,
                Product_ID = Product_ID,
                Type = "QR1",
                Location = "",
                TrackingDate = DateTime.Now,
                Status = 1,
                IP = Common.GetIp()
            };
            BusinessRulesLocator.GetQRCodeTrackingBO().Insert(_QRCodeTrackingRow);
            if (!_QRCodeTrackingRow.IsQRCodeTracking_IDNull)
            {
                lblTrackingID.Value = _QRCodeTrackingRow.QRCodeTracking_ID.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "TrackingLocation");
        }
    }

    private void LoatDataTaskHistorySX(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.TaskStep_ID,T.ProductPackage_ID, T.CreateBy,T.Image,T.Description, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
        left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 1 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
            LoadAnswer();
        }
        else
        {
            noneSX = "none1";
            rpt.DataSource = null;
            rpt.DataBind();
        }
    }
    private void LoatDataTaskHistorySX1(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.TaskStep_ID,T.ProductPackage_ID, T.CreateBy,T.Image,T.Description, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
        left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 1 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
            LoadAnswerHistory();
        }
        else
        {
            rpt.DataSource = null;
            rpt.DataBind();
        }
    }
    private void LoatDataTaskHistoryVT(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select M.Unit, T.Task_ID,T.ProductPackage_ID,T.Description, T.StartDate, T.Image,T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , T.UserName, T.Name  from Task T 
left join Material M on M.Material_ID= T.Material_ID
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 2 order by T.StartDate ASC");

        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            noneVattu = "none1";
            rpt.DataSource = null;
            rpt.DataBind();
        }
    }
    private void LoatDataTaskHistoryTH(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select T.Name,T.Task_ID,T.ProductPackage_ID,T.Description,T.Unit, T.StartDate,T.Image, T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
from Task T  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID =3 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            noneThuhoach = "none1";
            rpt.DataSource = null;
            rpt.DataBind();
        }

    }
    private void LoatDataTaskHistoryCB(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Task_ID,T.ProductPackage_ID, T.CreateBy,T.Description, T.StartDate,T.Image, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 4 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            noneCheBien = "none1";
            rpt.DataSource = null;
            rpt.DataBind();
        }
    }

    private void LoatDataTaskHistoryTransport(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as NhaVC, T.Task_ID,T.ProductPackage_ID, T.CreateBy,T.Description, T.StartDate,T.Image, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.BuyerName, T.ShopAddress,T.StartingPoint, T.Destination, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         left join aspnet_Users U on U.UserId= T.CreateBy
left join Transporter TS on TS.Transporter_ID= T.Transporter_ID
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 5 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            noneVanchuyen = "none1";
            rpt.DataSource = null;
            rpt.DataBind();
        }
    }
    private void LoatDataTaskHistoryPP(int ProductPackage_ID, Repeater rpt)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select C.Name as TenKhach, T.Task_ID,T.ProductPackage_ID, T.CreateBy,T.Description, T.StartDate,T.Image, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.BuyerName, T.ShopAddress,T.StartingPoint, T.Destination, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         left join aspnet_Users U on U.UserId= T.CreateBy
left join Customer C on C.Customer_ID= T.Customer_ID
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 6 order by T.StartDate ASC");
        rpt.DataSource = dt;
        rpt.DataBind();
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

    protected void rptSX_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblTaskStep_ID = e.Item.FindControl("lblTaskStep_ID") as Literal;

            Repeater rptQuestion = e.Item.FindControl("rptQuestion") as Repeater;
            if (lblTaskStep_ID != null && rptQuestion != null)
            {
                //DataTable dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable("TaskStep_ID=" + ddlTask.SelectedValue + " and  Type like '%,4,%'", "");
                //if (dt.Rows.Count == 1)
                //{
                //    divQuestion.Visible = true;
                DataTable dtQuestion = BusinessRulesLocator.GetTaskStepQuestionBO().GetAsDataTable("Active =1 and TaskStep_ID=" + lblTaskStep_ID.Text, "");
                if (dtQuestion.Rows.Count > 0)
                {
                    rptQuestion.DataSource = dtQuestion;
                    rptQuestion.DataBind();
                }
                //}
                //else
                //{
                //    divQuestion.Visible = false;
                //}
            }
        }

    }
    protected void LoadAnswer()
    {
        foreach (RepeaterItem item in rptSX.Items)
        {
            Literal lblTask_ID = item.FindControl("lblTask_ID") as Literal;
            Repeater rptQuestion = item.FindControl("rptQuestion") as Repeater;
            foreach (RepeaterItem itemAns in rptQuestion.Items)
            {
                CheckBoxList ckAnswer = itemAns.FindControl("ckAnswer") as CheckBoxList;
                foreach (ListItem itemAnswer in ckAnswer.Items)
                {
                    DataTable dt = BusinessRulesLocator.GetTaskBO().GetAsDataTable("Task_ID=" + lblTask_ID.Text + " and TaskStepAnswer_ID_List like '%," + itemAnswer.Value + ",%'", "");

                    if (dt.Rows.Count != 0)
                    {
                        itemAnswer.Selected = true;
                        itemAnswer.Enabled = false;
                    }
                    else
                    {
                        itemAnswer.Text = "";
                        itemAnswer.Attributes.Add("class", "none");
                    }
                }

            }
        }
    }
    protected void LoadAnswerHistory()
    {
        try
        {


            foreach (RepeaterItem itemCha in rptHistory.Items)
            {
                Repeater rptSX = itemCha.FindControl("rptSX") as Repeater;

                foreach (RepeaterItem item in rptSX.Items)
                {
                    Literal lblTask_ID = item.FindControl("lblTask_ID") as Literal;
                    Repeater rptQuestion = item.FindControl("rptQuestion") as Repeater;
                    foreach (RepeaterItem itemAns in rptQuestion.Items)
                    {
                        CheckBoxList ckAnswer = itemAns.FindControl("ckAnswer") as CheckBoxList;
                        foreach (ListItem itemAnswer in ckAnswer.Items)
                        {
                            DataTable dt = BusinessRulesLocator.GetTaskBO().GetAsDataTable("Task_ID=" + lblTask_ID.Text + " and TaskStepAnswer_ID_List like '%," + itemAnswer.Value + ",%'", "");

                            if (dt.Rows.Count != 0)
                            {
                                itemAnswer.Selected = true;
                                itemAnswer.Enabled = false;
                            }
                            else
                            {

                                itemAnswer.Text = "";
                                itemAnswer.Attributes.Add("class", "none");
                            }

                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "LoadAnswerHistory");
        }
    }
    protected void LoadHistory(int ProductPackage_ID)
    {
        try
        {
            if (ProductPackage_ID != 0)
            {
                ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);

                if (_ProductPackageRow != null)
                {
                    if (!_ProductPackageRow.IsSGTIN_HistoryNull)
                    {
                        string[] array = _ProductPackageRow.SGTIN_History.Split(',');
                        DataTable dt = new DataTable();
                        dt.Clear();
                        dt.Columns.Add("ProductPackage_ID");
                        dt.Columns.Add("ProductPackageName");
                        dt.Columns.Add("ProductBrandName");
                        dt.Columns.Add("VungTrong");
                        dt.Columns.Add("DiaChi");
                        dt.Columns.Add("ChatLuong");
                        dt.Columns.Add("SGTIN");
                        dt.Columns.Add("ChungNhan");
                        foreach (string value in array)
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                int ProductPackage_ID_History = Convert.ToInt32(value);
                                ProductPackageRow _ProductPackageRowHistory = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID_History);
                                string brandName = string.Empty;
                                if (!_ProductPackageRowHistory.IsProductBrand_IDNull)
                                {
                                    brandName = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRowHistory.ProductBrand_ID).Name;
                                }
                                DataRow _row = dt.NewRow();


                                if (!_ProductPackageRowHistory.IsWorkshop_IDNull)
                                {
                                    WorkshopRow _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(_ProductPackageRowHistory.Workshop_ID);
                                    if (_WorkshopRow != null)
                                    {
                                        if (!_ProductPackageRowHistory.IsZone_IDNull)
                                        {
                                            _row["VungTrong"] = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(_ProductPackageRowHistory.Zone_ID).Name;
                                        }
                                        _row["DiaChi"] = (_WorkshopRow.IsAddressNull ? string.Empty : _WorkshopRow.Address);
                                    }
                                }
                                if (!_ProductPackageRowHistory.IsProduct_IDNull)
                                {
                                    if (!BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_ProductPackageRowHistory.Product_ID).IsQuality_IDNull)
                                    {
                                        _row["ChungNhan"] = BusinessRulesLocator.GetQualityBO().GetByPrimaryKey(BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_ProductPackageRowHistory.Product_ID).Quality_ID).Name;
                                    }
                                }
                                _row["ProductPackage_ID"] = ProductPackage_ID_History;
                                _row["ProductPackageName"] = _ProductPackageRowHistory.Name;
                                _row["ProductBrandName"] = brandName;
                                _row["SGTIN"] = _ProductPackageRowHistory.SGTIN;
                                dt.Rows.Add(_row);

                            }
                        }
                        //Response.Write(dt.Rows.Count);
                        if (dt.Rows.Count > 0)
                        {
                            rptHistory.DataSource = dt;
                            rptHistory.DataBind();
                        }
                        else
                        {
                            rptHistory.DataSource = null;
                            rptHistory.DataBind();
                        }
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "LoadHistory");
        }
    }
    protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblQuestionID = e.Item.FindControl("lblQuestionID") as Literal;
            CheckBoxList ckAnswer = e.Item.FindControl("ckAnswer") as CheckBoxList;
            if (lblQuestionID != null && ckAnswer != null)
            {
                DataTable dtAnswer = BusinessRulesLocator.GetTaskStepAnswerBO().GetAsDataTable("Active=1 and TaskStepQuestion_ID=" + lblQuestionID.Text, "");
                if (dtAnswer.Rows.Count > 0)
                {
                    ckAnswer.DataSource = dtAnswer;
                    ckAnswer.DataTextField = "Name";
                    ckAnswer.DataValueField = "TaskStepAnswer_ID";
                    ckAnswer.DataBind();
                }
            }
        }
    }

    protected void rptHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Repeater rptSX_History = e.Item.FindControl("rptSX") as Repeater;
            Repeater rptTaskHistoryTH_History = e.Item.FindControl("rptTaskHistoryTH") as Repeater;
            Repeater rptTaskHistoryCB_History = e.Item.FindControl("rptTaskHistoryCB") as Repeater;
            if (lblProductPackage_ID != null)
            {
                LoatDataTaskHistorySX1(Convert.ToInt32(lblProductPackage_ID.Text), rptSX_History);
                LoatDataTaskHistoryTH(Convert.ToInt32(lblProductPackage_ID.Text), rptTaskHistoryTH_History);
                LoatDataTaskHistoryCB(Convert.ToInt32(lblProductPackage_ID.Text), rptTaskHistoryCB_History);
                //    rptSX_History.ItemDataBound += new RepeaterItemEventHandler(rptSX_ItemDataBound);
            }
        }
    }
    public int NoTransport = 1;
    protected void GetTransport(int ProductPackage_ID)
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select Name, Address, Phone from Transporter where Active=1 and Transporter_ID in (  select distinct Transporter_ID from Task where ProductPackage_ID=" + ProductPackage_ID + " and Transporter_ID is not null)");
            rptVanChuyen.DataSource = dt;
            rptVanChuyen.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "GetTransport");
        }
    }

    public int NoPP = 1;
    protected void GetPP(int ProductPackage_ID)
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select Name, Address, Phone from Customer where  Customer_ID in (  select distinct Customer_ID from Task where ProductPackage_ID=" + ProductPackage_ID + " and Customer_ID is not null)");
            rptPP.DataSource = dt;
            rptPP.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "GetPP");
        }
    }
}