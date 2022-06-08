using DbObj;
using evointernal;
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

public partial class Product_ChangeEdit : System.Web.UI.Page
{
    int ProductChange_ID = 0;
    public string title = "Thông tin sản phẩm";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        _FileBrowser.SetupCKEditor(txtQuyTrinh);
        _FileBrowser.SetupCKEditor(txtstory);
        if (!string.IsNullOrEmpty(Request["ProductChange_ID"]))
            int.TryParse(Request["ProductChange_ID"].ToString(), out ProductChange_ID);
        if (!IsPostBack)
        {

            if (Common.GetFunctionGroupDN())
            {
                btnBack.Visible = btnCancel.Visible = btnSave.Visible = false;
                btnResend.Visible = true;
                txtChange.Enabled = true;
                txtNoteChange.Enabled = false;
            }
            //if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            //{
            //    if (MyUser.GetProductBrandRole_ID() == "1")
            //    {
            //        btnSave.Text = "Gửi yêu cầu thay đổi thông tin";
            //    }
            //    else
            //    {
            //        Role.Visible = false;
            //    }
            //}
            //else
            //{
            //    Role.Visible = false;
            //}
            LoadProductCategory();
            FillDDLQuality();
            LoadProductBrand();
            FillAudio();
            FillInfoProduct();
            QRCode();
        }
    }
    protected void FillAudio()
    {
        string where = string.Empty;
        if (ddlProductBrand.SelectedValue != "")
        {
            where = " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
        }
        DataTable dtAudio = BusinessRulesLocator.GetAudioBO().GetAsDataTable(" Status=1" + where, " Title ASC");
        DataTable dtMessage = BusinessRulesLocator.GetMessageBO().GetAsDataTable(" Status=1" + where, " Title ASC");

        ddlAudioPublic.DataSource = dtAudio;
        ddlAudioPublic.DataTextField = "Title";
        ddlAudioPublic.DataValueField = "Audio_ID";
        ddlAudioPublic.DataBind();
        ddlAudioPublic.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));


        ddlAudioSecret.DataSource = dtAudio;
        ddlAudioSecret.DataTextField = "Title";
        ddlAudioSecret.DataValueField = "Audio_ID";
        ddlAudioSecret.DataBind();
        ddlAudioSecret.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

        ddlAudioSold.DataSource = dtAudio;
        ddlAudioSold.DataTextField = "Title";
        ddlAudioSold.DataValueField = "Audio_ID";
        ddlAudioSold.DataBind();
        ddlAudioSold.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

        ddlMessagePublic.DataSource = dtMessage;
        ddlMessagePublic.DataTextField = "Title";
        ddlMessagePublic.DataValueField = "Message_ID";
        ddlMessagePublic.DataBind();
        ddlMessagePublic.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));


        ddlMessageSecret.DataSource = dtMessage;
        ddlMessageSecret.DataTextField = "Title";
        ddlMessageSecret.DataValueField = "Message_ID";
        ddlMessageSecret.DataBind();
        ddlMessageSecret.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));


        ddlMessageSold.DataSource = dtMessage;
        ddlMessageSold.DataTextField = "Title";
        ddlMessageSold.DataValueField = "Message_ID";
        ddlMessageSold.DataBind();
        ddlMessageSold.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));


    }
    private void LoadProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            Common.FillProductBrand(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductBrand", ex.ToString());
        }
    }
    private void FillDDLQuality()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" ", " Name ASC");
            ddlTieuChuan.DataSource = dt;
            ddlTieuChuan.DataTextField = "Name";
            ddlTieuChuan.DataValueField = "Quality_ID";
            ddlTieuChuan.DataBind();
            ddlTieuChuan.Items.Insert(0, new ListItem("-- Chọn tiêu chuẩn --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void LoadProductCategory()
    {
        try
        {
            //DataTable dtProductCategoryParent = new DataTable();
            //dtProductCategoryParent.Clear();
            //dtProductCategoryParent.Columns.Add("ProductCategory_ID");
            //dtProductCategoryParent.Columns.Add("Parent_ID");
            //dtProductCategoryParent.Columns.Add("Name");
            //dtProductCategoryParent.Columns.Add("Image");
            //dtProductCategoryParent.Columns.Add("Active");

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID is null ", " Sort ASC");

            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRow itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //    itemProductCategoryParent["ProductCategory_ID"] = item["ProductCategory_ID"];
            //    itemProductCategoryParent["Parent_ID"] = item["Parent_ID"];
            //    itemProductCategoryParent["Name"] = item["Name"];
            //    itemProductCategoryParent["Image"] = item["Image"];
            //    itemProductCategoryParent["Active"] = item["Active"];
            //    dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //    if (item["ProductCategory_ID"] != null)
            //    {
            //        DataTable dtChild = new DataTable();
            //        dtChild = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID =" + item["ProductCategory_ID"], " Sort ASC");
            //        if (dtChild.Rows.Count > 0)
            //        {
            //            foreach (DataRow itemChild in dtChild.Rows)
            //            {
            //                itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //                itemProductCategoryParent["ProductCategory_ID"] = itemChild["ProductCategory_ID"];
            //                itemProductCategoryParent["Parent_ID"] = item["ProductCategory_ID"];
            //                itemProductCategoryParent["Name"] = Server.HtmlDecode("&nbsp;&nbsp;&nbsp;") + " -" + itemChild["Name"];
            //                itemProductCategoryParent["Image"] = itemChild["Image"];
            //                itemProductCategoryParent["Active"] = itemChild["Active"];
            //                dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //            }

            //        }
            //    }
            //}

            ddlCha.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "ProductCategory_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void FillInfoProduct()
    {
        try
        {
            if (ProductChange_ID != 0)
            {


                ProductChangeRow _ProductRow = new ProductChangeRow();
                _ProductRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(ProductChange_ID);
                if (_ProductRow != null)
                {
                    if (_ProductRow.ProductChange_Status == 1)
                    {
                        btnSave.Visible = btnCancel.Visible = false;
                    }
                    if (_ProductRow.ProductChange_Status == 2)
                    {
                        btnSave.Visible = btnCancel.Visible = false;
                    }
                    //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                    // MyActionPermission.CheckPermission(_ProductRow.ProductBrand_ID.ToString(), _ProductRow.CreateBy.ToString(), "/Admin/Product/Product_List");

                    txtName.Text = _ProductRow.IsNameNull ? string.Empty : _ProductRow.Name;
                    ddlCha.SelectedValue = _ProductRow.IsProductCategory_IDNull ? "0" : _ProductRow.ProductCategory_ID.ToString();
                    ddlTieuChuan.SelectedValue = _ProductRow.IsQuality_IDNull ? "0" : _ProductRow.Quality_ID.ToString();
                    ddlDienTich.SelectedValue = _ProductRow.IsAcreageNull ? "0" : _ProductRow.Acreage.ToString();
                    txtNote.Text = _ProductRow.IsContentNull ? string.Empty : _ProductRow.Content;
                    txtstory.Text = _ProductRow.IsStoryNull ? string.Empty : _ProductRow.Story;
                    txtQuyTrinh.Text = _ProductRow.IsQualityDescriptionNull ? string.Empty : _ProductRow.QualityDescription.ToString();
                    txtGrowthByDay.Text = _ProductRow.IsGrowthByDayNull ? "0" : _ProductRow.GrowthByDay.ToString();
                    txtNangSuat.Text = _ProductRow.IsExpectedProductivityNull ? "0" : _ProductRow.ExpectedProductivity.ToString();
                    txtSanLuong.Text = _ProductRow.IsExpectedOutputNull ? "0" : _ProductRow.ExpectedOutput.ToString();
                    txtGTIN.Text = _ProductRow.IsSGTINNull ? "" : _ProductRow.SGTIN.ToString();

                    txtNangSuat1.Text = _ProductRow.IsExpectedProductivityDescriptionNull ? "" : _ProductRow.ExpectedProductivityDescription.ToString();
                    txtSanLuong1.Text = _ProductRow.IsExpectedOutputDescriptionNull ? "" : _ProductRow.ExpectedOutputDescription.ToString();
                    txtTrackingCode.Text = _ProductRow.IsTrackingCodeNull ? "" : _ProductRow.TrackingCode.ToString();
                    ddlProductBrand.SelectedValue = _ProductRow.ProductBrand_ID.ToString();
                    txtSpecitication.Text = _ProductRow.IsSpecificationNull ? "" : _ProductRow.Specification.ToString();
                    txtWeight.Text = _ProductRow.IsWeightDefaultNull ? "0" : _ProductRow.WeightDefault.ToString();
                    FillAudio();
                    txtChange.Text = _ProductRow.IsProductChange_NoteNull ? string.Empty : _ProductRow.ProductChange_Note;
                    txtNoteChange.Text = _ProductRow.IsProductChange_ApprovedNoteNull ? string.Empty : _ProductRow.ProductChange_ApprovedNote;
                    //Gán âm thanh vs thông điệp

                    ddlAudioPublic.SelectedValue = _ProductRow.IsAudioPublicNull ? "" : _ProductRow.AudioPublic.ToString();
                    ddlMessagePublic.SelectedValue = _ProductRow.IsMessagePublicNull ? "" : _ProductRow.MessagePublic.ToString();

                    ddlAudioSecret.SelectedValue = _ProductRow.IsAudioSecretNull ? "" : _ProductRow.AudioSecret.ToString();
                    ddlMessageSecret.SelectedValue = _ProductRow.IsMessageSecretNull ? "" : _ProductRow.MessageSecret.ToString();


                    ddlAudioSold.SelectedValue = _ProductRow.IsAudioSoldNull ? "" : _ProductRow.AudioSold.ToString();
                    ddlMessageSold.SelectedValue = _ProductRow.IsMessageSoldNull ? "" : _ProductRow.MessageSold.ToString();

                    if (!_ProductRow.IsImageNull)
                    {
                        imganh.ImageUrl = "../../data/product/mainimages/original/" + _ProductRow.Image;
                    }
                    if (_ProductRow.Active == true)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateProduct()
    {
        try
        {
            string msg = string.Empty;
            ProductRow _ProductRow = new ProductRow();
            ProductChangeRow _ProductChangeRow = new ProductChangeRow();
            if (ProductChange_ID != 0)
            {
                _ProductChangeRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(ProductChange_ID);
                _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_ProductChangeRow.Product_ID);

                //  if (_ProductRow != null)
                {
                    if (_ProductRow == null)
                    {
                        _ProductRow = new ProductRow();
                    }
                    //_ProductChangeRow.ProductChange_ID = ProductChange_ID;
                    _ProductRow.SGTIN = _ProductChangeRow.SGTIN = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
                    _ProductRow.Name = _ProductChangeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    if (ddlCha.SelectedValue != "0")
                    {
                        _ProductRow.ProductCategory_ID = _ProductChangeRow.ProductCategory_ID = Convert.ToInt32(ddlCha.SelectedValue);
                    }
                    if (ddlTieuChuan.SelectedValue != "0")
                    {
                        _ProductRow.Quality_ID = _ProductChangeRow.Quality_ID = Convert.ToInt32(ddlTieuChuan.SelectedValue);
                    }
                    _ProductRow.Content = _ProductChangeRow.Content = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _ProductRow.Story = _ProductChangeRow.Story = string.IsNullOrEmpty(txtstory.Text) ? string.Empty : txtstory.Text;
                    _ProductRow.QualityDescription = _ProductChangeRow.QualityDescription = string.IsNullOrEmpty(txtQuyTrinh.Text) ? string.Empty : txtQuyTrinh.Text;
                    _ProductRow.GrowthByDay = _ProductChangeRow.GrowthByDay = string.IsNullOrEmpty(txtGrowthByDay.Text) ? 0 : Convert.ToInt32(txtGrowthByDay.Text);
                    _ProductRow.ProductType_ID = _ProductChangeRow.ProductType_ID = 1;
                    _ProductRow.Country_ID = _ProductChangeRow.Country_ID = 1;
                    _ProductRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _ProductRow.Acreage = _ProductChangeRow.Acreage = Convert.ToInt32(ddlDienTich.SelectedValue);
                    _ProductRow.ExpectedProductivity = _ProductChangeRow.ExpectedProductivity = string.IsNullOrEmpty(txtNangSuat.Text) ? 0 : Convert.ToInt32(txtNangSuat.Text);
                    _ProductRow.ExpectedOutput = _ProductChangeRow.ExpectedOutput = string.IsNullOrEmpty(txtSanLuong.Text) ? 0 : Convert.ToInt32(txtSanLuong.Text);
                    _ProductRow.ExpectedOutputDescription = _ProductChangeRow.ExpectedOutputDescription = string.IsNullOrEmpty(txtSanLuong1.Text) ? "" : (txtSanLuong1.Text);
                    _ProductRow.ExpectedProductivityDescription = _ProductChangeRow.ExpectedProductivityDescription = string.IsNullOrEmpty(txtNangSuat1.Text) ? "" : (txtNangSuat1.Text);
                    //_ProductChangeRow.URL = _ProductRow.URL = Common.ConvertTitleDomain(_ProductRow.Name + "-" + ProductChange_ID);
                    _ProductRow.CreateBy = _ProductChangeRow.CreateBy;
                    _ProductRow.CreateDate = _ProductChangeRow.CreateDate;

                    _ProductRow.WeightDefault = _ProductChangeRow.WeightDefault = string.IsNullOrEmpty(txtWeight.Text) ? 0 : Convert.ToInt32(txtWeight.Text);
                    _ProductRow.Specification = _ProductChangeRow.Specification = string.IsNullOrEmpty(txtSpecitication.Text) ? string.Empty : txtSpecitication.Text;

                    //Duyệt âm thanh và thông điệp
                    if (!_ProductChangeRow.IsAudioPublicNull)
                    {
                        _ProductRow.AudioPublic = _ProductChangeRow.AudioPublic;
                    }
                    if (!_ProductChangeRow.IsAudioSecretNull)
                    {
                        _ProductRow.AudioSecret = _ProductChangeRow.AudioSecret;
                    }
                    if (!_ProductChangeRow.IsAudioSoldNull)
                    {
                        _ProductRow.AudioSold = _ProductChangeRow.AudioSold;
                    }
                    if (!_ProductChangeRow.IsMessagePublicNull)
                    {
                        _ProductRow.MessagePublic = _ProductChangeRow.MessagePublic;
                    }
                    if (!_ProductChangeRow.IsMessageSecretNull)
                    {
                        _ProductRow.MessageSecret = _ProductChangeRow.MessageSecret;
                    }
                    if (!_ProductChangeRow.IsMessageSoldNull)
                    {
                        _ProductRow.MessageSold = _ProductChangeRow.MessageSold;
                    }
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_ProductChangeRow.IsImageNull)
                        {
                            if (_ProductChangeRow.Image != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductChangeRow.Image.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = ProductChange_ID + "_" + (fulAnh.FileName);
                        fulAnh.SaveAs(Server.MapPath("../../data/product/mainimages/original/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {

                            _ProductRow.Image = _ProductChangeRow.Image = fileimage;
                        }
                    }
                    else
                    {
                        if (!_ProductChangeRow.IsImageNull)
                            _ProductRow.Image = _ProductChangeRow.Image;
                    }
                    if (ckActive.Checked)
                    {

                        _ProductRow.Active = _ProductChangeRow.Active = true;
                    }
                    else
                    {
                        _ProductRow.Active = _ProductChangeRow.Active = false;
                    }

                    _ProductRow.TrackingCode = _ProductChangeRow.TrackingCode = string.IsNullOrEmpty(txtTrackingCode.Text) ? string.Empty : txtTrackingCode.Text;

                    //Kiểm tra quyền tài khoản
                    _ProductRow.Customer_ID = _ProductChangeRow.Customer_ID;
                    _ProductRow.Price = _ProductChangeRow.Price;
                    _ProductRow.PriceOld = _ProductChangeRow.PriceOld;

                    _ProductChangeRow.ProductChange_Status = 1;
                    _ProductChangeRow.ProductChange_DateApproved = DateTime.Now;
                    _ProductChangeRow.ProductChange_Approved = MyUser.GetUser_ID();
                    _ProductChangeRow.ProductChange_ApprovedNote = txtNoteChange.Text;
                    //
                    msg = "Duyệt thành công !";
                    NotificationRow _NotificationRow = new NotificationRow();
                    if (_ProductChangeRow.Product_ID == 0)
                    {
                        BusinessRulesLocator.GetProductChangeBO().Update(_ProductChangeRow);
                        BusinessRulesLocator.GetProductBO().Insert(_ProductRow);
                        _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thêm mới sản phẩm";
                    }
                    else
                    {
                        _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thay đổi thông tin sản phẩm";
                        BusinessRulesLocator.GetProductChangeBO().Update(_ProductChangeRow);
                        BusinessRulesLocator.GetProductBO().Update(_ProductRow);
                    }

                    //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                    _NotificationRow.Summary = txtChange.Text;
                    _NotificationRow.Body = _ProductChangeRow.ProductChange_ID.ToString();
                    _NotificationRow.NotificationType_ID = 12;
                    _NotificationRow.UserID = _ProductChangeRow.ProductChange_By;
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Admin/Product/Product_Edit?Product_ID=" + _ProductRow.Product_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);

                }

                if (!string.IsNullOrEmpty(msg))
                {
                    lblMessage.Text = msg;
                }
                else
                {
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                }

                lblMessage.Visible = true;
                FillInfoProduct();
                Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                if (master != null)
                    master.LoadNotification();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateProduct();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    public void QRCode()
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("https://esupplychain.vn/p/" + ProductChange_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                lblQR.Text = "<img width='150px' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/Notification/RequestProduct_List.aspx", false);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ProductChangeRow _ProductChangeRow = new ProductChangeRow();
        if (ProductChange_ID != 0)
        {
            _ProductChangeRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(ProductChange_ID);
            if (_ProductChangeRow != null)
            {
                _ProductChangeRow.ProductChange_Status = 2;
                _ProductChangeRow.ProductChange_DateApproved = DateTime.Now;
                _ProductChangeRow.ProductChange_Approved = MyUser.GetUser_ID();
                _ProductChangeRow.ProductChange_ApprovedNote = txtNoteChange.Text;
                BusinessRulesLocator.GetProductChangeBO().Update(_ProductChangeRow);

                NotificationRow _NotificationRow = new NotificationRow();

                //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                _NotificationRow.Summary = txtChange.Text;
                _NotificationRow.Body = _ProductChangeRow.ProductChange_ID.ToString();
                _NotificationRow.NotificationType_ID = 12;
                _NotificationRow.UserID = _ProductChangeRow.ProductChange_By;
                if (MyUser.GetFunctionGroup_ID() != "1")
                    _NotificationRow.ProductBrand_ID = _ProductChangeRow.ProductBrand_ID;
                //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRow.Url = "/Admin/Product/Product_ChangeEdit?ProductChange_ID=" + _ProductChangeRow.ProductChange_ID;
                if (_ProductChangeRow.Product_ID != 0)
                {
                    _NotificationRow.Name = "Cơ quan quản lý đã không duyệt yêu cầu thay đổi thông tin sản phẩm";

                }
                else
                {
                    _NotificationRow.Name = "Cơ quan quản lý đã không duyệt yêu cầu thêm mới sản phẩm";

                }
                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                _NotificationRow.CreateDate = DateTime.Now;
                _NotificationRow.Active = 1;
                _NotificationRow.Alias = Guid.NewGuid();
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);

                lblMessage.Text = "Từ chối duyệt và gửi phản hồi thành công!";
                lblMessage.Visible = true;
            }
        }
    }

    protected void btnResend_Click(object sender, EventArgs e)
    {
        if (ProductChange_ID != 0)
        {
            ProductChangeRow _ProductChangeRow = new ProductChangeRow();
            _ProductChangeRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(ProductChange_ID);


            _ProductChangeRow.SGTIN = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
            _ProductChangeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            if (ddlCha.SelectedValue != "0")
            {
                _ProductChangeRow.ProductCategory_ID = Convert.ToInt32(ddlCha.SelectedValue);
            }
            if (ddlTieuChuan.SelectedValue != "0")
            {
                _ProductChangeRow.Quality_ID = Convert.ToInt32(ddlTieuChuan.SelectedValue);
            }
            _ProductChangeRow.Content = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _ProductChangeRow.Story = string.IsNullOrEmpty(txtstory.Text) ? string.Empty : txtstory.Text;
            _ProductChangeRow.QualityDescription = string.IsNullOrEmpty(txtQuyTrinh.Text) ? string.Empty : txtQuyTrinh.Text;
            _ProductChangeRow.GrowthByDay = string.IsNullOrEmpty(txtGrowthByDay.Text) ? 0 : Convert.ToInt32(txtGrowthByDay.Text);
            _ProductChangeRow.ProductType_ID = 1;
            _ProductChangeRow.Country_ID = 1;
            _ProductChangeRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _ProductChangeRow.Acreage = Convert.ToInt32(ddlDienTich.SelectedValue);
            _ProductChangeRow.ExpectedProductivity = string.IsNullOrEmpty(txtNangSuat.Text) ? 0 : Convert.ToInt32(txtNangSuat.Text);
            _ProductChangeRow.ExpectedOutput = string.IsNullOrEmpty(txtSanLuong.Text) ? 0 : Convert.ToInt32(txtSanLuong.Text);
            _ProductChangeRow.ExpectedOutputDescription = string.IsNullOrEmpty(txtSanLuong1.Text) ? "" : (txtSanLuong1.Text);
            _ProductChangeRow.ExpectedProductivityDescription = string.IsNullOrEmpty(txtNangSuat1.Text) ? "" : (txtNangSuat1.Text);
            _ProductChangeRow.LastEditBy = MyUser.GetUser_ID();
            _ProductChangeRow.LastEditDate = DateTime.Now;
            string fileimage = "";
            if (fulAnh.HasFile)
            {
                //Xóa file
                if (!_ProductChangeRow.IsImageNull)
                {
                    if (_ProductChangeRow.Image != null)
                    {
                        string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _ProductChangeRow.Image.Replace("../", "");
                        if (File.Exists(strFileFullPath))
                        {
                            File.Delete(strFileFullPath);
                        }
                    }
                }
                fileimage = ProductChange_ID + "_" + (fulAnh.FileName);
                fulAnh.SaveAs(Server.MapPath("../../data/product/mainimages/original/" + fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {

                    _ProductChangeRow.Image = fileimage;
                }
            }

            if (ckActive.Checked)
            {

                _ProductChangeRow.Active = true;
            }
            else
            {

                _ProductChangeRow.Active = false;
            }

            _ProductChangeRow.TrackingCode = string.IsNullOrEmpty(txtTrackingCode.Text) ? string.Empty : txtTrackingCode.Text;

            //Kiểm tra quyền tài khoản
            _ProductChangeRow.CreateDate = DateTime.Now;
            _ProductChangeRow.CreateBy = MyUser.GetUser_ID();


            _ProductChangeRow.ProductChange_Note = string.IsNullOrEmpty(txtChange.Text) ? "Thay đổi thông tin sản phẩm " : txtChange.Text;
            _ProductChangeRow.ProductChange_By = MyUser.GetUser_ID();
            _ProductChangeRow.ProductChange_Date = DateTime.Now;
            _ProductChangeRow.ProductChange_Status = 0;
            //
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    //Lưu bảng tạm thay đổi thông tin
                    BusinessRulesLocator.GetProductChangeBO().Insert(_ProductChangeRow);
                    if (!_ProductChangeRow.IsProductChange_IDNull)
                    {
                        //Gửi thông báo thay đổi thông tin cho cấp trên
                        NotificationRow _NotificationRow = new NotificationRow();
                        if (_ProductChangeRow.Product_ID != 0)
                        {
                            _NotificationRow.Name = "Chỉnh sửa thông tin sản phẩm";
                        }
                        else
                        {
                            _NotificationRow.Name = "Thêm mới sản phẩm";
                        }
                        _NotificationRow.Summary = txtChange.Text;
                        //  _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                        _NotificationRow.NotificationType_ID = 11;
                        _NotificationRow.UserID = MyUser.GetUser_ID();
                        if (MyUser.GetFunctionGroup_ID() != "1")
                            _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                        //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                        //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                        //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                        //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                        _NotificationRow.Url = "/Admin/Notification/RequestProduct_List";
                        _NotificationRow.CreateBy = MyUser.GetUser_ID();
                        _NotificationRow.CreateDate = DateTime.Now;
                        _NotificationRow.Active = 1;
                        _NotificationRow.Alias = Guid.NewGuid();
                        BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                        lblMessage.Text = "Gửi yêu cầu thay đổi thông tin thành công !";
                        lblMessage.Visible = true;
                    }
                }

            }

        }
    }
}