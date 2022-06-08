using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class QRCodePackage_Edit : System.Web.UI.Page
{
    int QRCodePackage_ID = 0;
    public string title = "Thông tin yêu cầu cấp tem";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {
            //LoadProductCategory();
            //LoadProduct();
            FillGio();
            FillDDLProductBrand();
            LoadSupplier();
            LoadData();
        }
    }
    private void FillDDLAudioAndMessgade()
    {
        try
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

            ddlAudioSold.DataSource = dtAudio;
            ddlAudioSold.DataTextField = "Title";
            ddlAudioSold.DataValueField = "Audio_ID";
            ddlAudioSold.DataBind();
            ddlAudioSold.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioIDE.DataSource = dtAudio;
            ddlAudioIDE.DataTextField = "Title";
            ddlAudioIDE.DataValueField = "Audio_ID";
            ddlAudioIDE.DataBind();
            ddlAudioIDE.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));


            ddlMessagePublic.DataSource = dtMessage;
            ddlMessagePublic.DataTextField = "Title";
            ddlMessagePublic.DataValueField = "Message_ID";
            ddlMessagePublic.DataBind();
            ddlMessagePublic.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageSold.DataSource = dtMessage;
            ddlMessageSold.DataTextField = "Title";
            ddlMessageSold.DataValueField = "Message_ID";
            ddlMessageSold.DataBind();
            ddlMessageSold.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageIDE.DataSource = dtMessage;
            ddlMessageIDE.DataTextField = "Title";
            ddlMessageIDE.DataValueField = "Message_ID";
            ddlMessageIDE.DataBind();
            ddlMessageIDE.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));



        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    private void LoadSupplier()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() != "1")
            {
                if (!string.IsNullOrEmpty(MyUser.GetProductBrand_ID()))
                {
                    where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID(); ;
                }
            }

            DataTable dt = BusinessRulesLocator.GetSupplierBO().GetAsDataTable(" Active=1 " + where, " Name ASC");
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataValueField = "Supplier_ID";
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-- Chọn đơn vị sơ chế và đóng gói thành phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadSupplier", ex.ToString());
        }
    }
    private void LoadData()
    {
        try
        {
            if (QRCodePackage_ID != 0)
            {
                QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                if (_QRCodePackageRow != null)
                {
                    txtName.Text = _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;
                    txtAmount.Text = _QRCodePackageRow.IsQRCodeNumberNull ? "0" : _QRCodePackageRow.QRCodeNumber.ToString();
                    txtAmount.Enabled = false;
                    txtLength.Enabled = false;
                    ckSound.Checked = _QRCodePackageRow.SoundEnable;
                    ckSMS.Checked = _QRCodePackageRow.SMS;
                    ddlProductBrand.SelectedValue = _QRCodePackageRow.ProductBrand_ID.ToString();
                    FillDDLAudioAndMessgade();
                    txtKhoiLuong.Text = _QRCodePackageRow.IsAmountSumNull ? "0" : _QRCodePackageRow.AmountSum.ToString();
                    ddlSupplier.SelectedValue = _QRCodePackageRow.IsSupplier_IDNull ? "0" : _QRCodePackageRow.Supplier_ID.ToString();
                    //ddlProduct.SelectedValue = _QRCodePackageRow.Product_ID.ToString();
                    //if (ddlProduct.SelectedValue !="0" && ddlProduct.SelectedValue!="-1")
                    //{
                    //    ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Convert.ToInt32(ddlProduct.SelectedValue));
                    //    if (_ProductRow != null)
                    //    {
                    //        ddlProducCategory.SelectedValue = _ProductRow.ProductCategory_ID.ToString();
                    //    }
                    //}
                    ddlProductBrand.Enabled = false;
                    ddlTemType.SelectedValue = _QRCodePackageRow.QRCodePackageType_ID.ToString();
                    ddlTemType.Enabled = false;
                    txtNote.Text = _QRCodePackageRow.IsDescriptionNull ? string.Empty : _QRCodePackageRow.Description;
                    txtHistoryProductPackage.Text = _QRCodePackageRow.IsProfileQRCodeNull ? string.Empty : _QRCodePackageRow.ProfileQRCode;
                    txtSX.Text = _QRCodePackageRow.IsManufactureDateNull ? string.Empty : _QRCodePackageRow.ManufactureDate.ToString("dd/MM/yyyy");
                    txtThuHoach.Text = _QRCodePackageRow.IsHarvestDateNull ? string.Empty : _QRCodePackageRow.HarvestDate.ToString("dd/MM/yyyy");
                    if (!_QRCodePackageRow.IsExpiryByDateNull && _QRCodePackageRow.ExpiryByDate != 0)
                    {
                        if (!_QRCodePackageRow.IsWarrantyEndDateNull)
                        {
                            txtHSD.Text = _QRCodePackageRow.WarrantyEndDate.AddDays(_QRCodePackageRow.ExpiryByDate).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtHSD.Text = _QRCodePackageRow.IsManufactureDateNull ? string.Empty : _QRCodePackageRow.ManufactureDate.AddDays(_QRCodePackageRow.ExpiryByDate).ToString("dd/MM/yyyy");
                        }
                    }
                    else if (!_QRCodePackageRow.IsWarrantyEndDateNull)
                    {
                        txtHSD.Text = _QRCodePackageRow.IsWarrantyEndDateNull ? string.Empty : _QRCodePackageRow.WarrantyEndDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtHSD.Text = string.Empty;
                    }
                    txtNgayDukien.Text = _QRCodePackageRow.IsStampDateNull ? string.Empty : _QRCodePackageRow.StampDate.ToString("dd/MM/yyyy");
                    if (_QRCodePackageRow.Product_ID == -1)
                    {
                        txtProductName.Text = "#Sản phẩm chưa xác định#";
                    }
                    else
                    {
                        ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_QRCodePackageRow.Product_ID);
                        if (_ProductRow != null)
                        {
                            txtProductName.Text = _ProductRow.IsNameNull ? string.Empty : _ProductRow.Name;
                        }

                    }
                    if (!_QRCodePackageRow.IsStampDateNull)
                    {
                        ddlHour.SelectedValue = _QRCodePackageRow.StampDate.Hour.ToString();
                        ddlMinutes.SelectedValue = _QRCodePackageRow.StampDate.Minute.ToString();
                    }

                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }

    //private void LoadProduct()
    //{

    //    try
    //    {
    //        string where = string.Empty;
    //        if (ddlProducCategory.SelectedValue != "")
    //        {
    //            where += " And ProductCategory_ID=" + ddlProducCategory.SelectedValue;
    //        }
    //        if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
    //        {
    //            where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
    //        }
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
    //        ddlProduct.DataSource = dt;
    //        ddlProduct.DataTextField = "Name";
    //        ddlProduct.DataValueField = "Product_ID";
    //        ddlProduct.DataBind();
    //        //ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
    //        ddlProduct.Items.Insert(0, new ListItem("#Sản phẩm chưa xác định#", "-1"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillDDLCha", ex.ToString());
    //    }
    //}
    //protected void LoadProductCategory()
    //{
    //    try
    //    {
    //        ddlProducCategory.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
    //        ddlProducCategory.DataTextField = "Name";
    //        ddlProducCategory.DataValueField = "ProductCategory_ID";
    //        ddlProducCategory.DataBind();
    //        ddlProducCategory.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
    //    }
    //    catch (Exception ex)
    //    {

    //        Log.writeLog("LoadUser", ex.ToString());
    //    }
    //}

    private void FillGio()
    {
        for (int i = 23; i >= 0; i--)
        {
            ddlHour.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }

        for (int i = 59; i >= 0; i--)
        {
            ddlMinutes.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinutes.SelectedValue = DateTime.Now.Minute.ToString();
        //txtSX.Attributes.Add("readonly", "true");
        //txtHSD.Attributes.Add("readonly", "true");
        //txtNgayDukien.Attributes.Add("readonly", "true");
        //txtThuHoach.Attributes.Add("readonly", "true");
    }

    private void FillDDLProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, "");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (QRCodePackage_ID != 0)
                {
                    QRCodePackageRow _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                    _QRCodePackageRow.SoundEnable = ckSound.Checked;
                    _QRCodePackageRow.SMS = ckSMS.Checked;
                    _QRCodePackageRow.ProductName = string.IsNullOrEmpty(txtProductName.Text) ? string.Empty : txtProductName.Text;
                    if (!string.IsNullOrEmpty(txtSX.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtSX.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _QRCodePackageRow.ManufactureDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtThuHoach.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtThuHoach.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _QRCodePackageRow.HarvestDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtHSD.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtHSD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _QRCodePackageRow.WarrantyEndDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtNgayDukien.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtNgayDukien.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                        ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                        ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);
                        _QRCodePackageRow.StampDate = ngaycap;
                    }
                    if (!string.IsNullOrEmpty(txtKhoiLuong.Text))
                    {
                        _QRCodePackageRow.AmountSum = Convert.ToInt32(txtKhoiLuong.Text.Replace(",", "").Replace(",", ""));
                    }
                    if (ddlSupplier.SelectedValue != "")
                    {
                        _QRCodePackageRow.Supplier_ID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    }
                    _QRCodePackageRow.Description = txtNote.Text;
                    _QRCodePackageRow.ProfileQRCode = txtHistoryProductPackage.Text;

                    if (ddlAudioPublic.SelectedValue != "")
                        _QRCodePackageRow.AudioPublic = Convert.ToInt32(ddlAudioPublic.SelectedValue);
                    if (ddlAudioSold.SelectedValue != "")
                        _QRCodePackageRow.AudioSold = Convert.ToInt32(ddlAudioSold.SelectedValue);
                    if (ddlAudioIDE.SelectedValue != "")
                        _QRCodePackageRow.AudioIDE = Convert.ToInt32(ddlAudioIDE.SelectedValue);

                    if (ddlMessagePublic.SelectedValue != "")
                        _QRCodePackageRow.MessagePublic = Convert.ToInt32(ddlMessagePublic.SelectedValue);
                    if (ddlMessageSold.SelectedValue != "")
                        _QRCodePackageRow.MessageSold = Convert.ToInt32(ddlMessageSold.SelectedValue);
                    if (ddlMessageIDE.SelectedValue != "")
                        _QRCodePackageRow.MessageIDE = Convert.ToInt32(ddlMessageIDE.SelectedValue);

                    BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
                    lblMessage.Text = "Cập nhật thành công!";
                    lblMessage.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_List.aspx", false);
    }
    protected void ddlProducCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}