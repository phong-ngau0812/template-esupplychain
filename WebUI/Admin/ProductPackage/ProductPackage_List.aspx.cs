using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackage_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    private int productCategory_ID;
    private string Code = string.Empty;
    public string none = string.Empty;
    public string TitleNKTH = string.Empty;
    public string TitleXH = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Code"]))
            Code = Request["Code"].ToString();
        btnAdd.Visible = MyActionPermission.CanAdd();
        CheckUserXuanHoa();
        if (!IsPostBack)
        {
            txtCODE.Text = Code;
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dtList = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable("UserId='" + MyUser.GetUser_ID() + "'", "");
                if (dtList.Rows.Count == 1)
                {
                    ProductBrandList.Value = dtList.Rows[0]["ProductBrand_ID_List"].ToString();
                }
                btnAdd.Visible = false;
            }
            FillLocation();
            FillDepartment();
            FillProductBrand();
            LoadZone();
            Common.CheckAccountTypeZone(ddlZone);
            LoadArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            LoadStatus();
            LoadWorkshop();
            FillProductPackageOrder();
            FillProduct();
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    FillDistrict();
                    ddlLocation.Enabled = false;
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                }
                else if (MyUser.GetRank_ID() == "4")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                    ddlWard.SelectedValue = MyUser.GetWard_ID();
                    ddlWard.Enabled = false;
                }

            }

            if (Common.CheckUserXuanHoa1())
            {
                noxuanhoa.Visible = false;
                xuanhoaLenh.Visible = true;
            }
            else
            {
                noxuanhoa.Visible = true;
                xuanhoaLenh.Visible = false;
            }
            if (Common.GetFunctionGroupDN())
            {
                noxuanhoa.Visible = false;
            }
        }

        LoadProductPackage();
        ResetMsg();
    }
    protected void CheckUserXuanHoa()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select UserId,UserName from aspnet_Users where UserId = '" + MyUser.GetUser_ID() + "'");
        if (dt.Rows.Count > 0)
        {
            if (MyUser.GetFunctionGroup_ID() == "2" && MyUser.GetProductBrand_ID() == "1524")
            {
                none = "none1";
                TitleNKTH = "Nhật ký nhập kho...";
                TitleXH = "Cập nhật vật tư xuất kho";
            }
            else
            {
                TitleNKTH = "Nhật ký Thu Hoạch...";
                TitleXH = "Cài đặt định mức vật tư";
            }
        }
    }
    protected string CheckStatus()
    {
        string st = string.Empty;
        if (Common.CheckUserXuanHoa1())
        {
            st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-primary\">Đã sản xuất xong</span>";
        }
        else
        {
            st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-primary\">Thu hoạch xong</span>";
        }
        return st;
    }
    public string ReturnStatus(string status)
    {
        string st = "";
        switch (status)
        {
            case "Đang sản xuất":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-success\">" + status + "</span>";
                break;
            case "Đang thu hoạch":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-primary\">" + status + "</span>";
                break;
            case "Thu hoạch xong":
                st = CheckStatus();
                break;
            case "Hủy":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-danger\">" + status + "</span>";
                break;
            case "Chưa kích hoạt":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-warning\">" + status + "</span>";
                break;
            default:
                st = "";
                break;
        }

        return st;

    }
    public string Message = "";
    private void LoadWorkshop()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                if (MyUser.GetFunctionGroup_ID() == "3")
                {
                    if (!string.IsNullOrEmpty(ProductBrandList.Value))
                    {
                        where += " and ProductBrand_ID in (" + ProductBrandList.Value + ")";
                    }
                }
                else
                {
                    where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                    string workshop_list = string.Empty;
                    string workshop = string.Empty;
                    if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    {
                        workshop = MyUser.GetZone_ID();
                        workshop_list = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Convert.ToInt32(MyUser.GetZone_ID())).Workshop_ID;
                        if (workshop_list != "")
                        {
                            workshop_list = workshop_list.Substring(1, workshop_list.Length - 1);
                            workshop_list = workshop_list.Remove(workshop_list.Length - 1);
                            where += " and Workshop_ID in (" + workshop_list + ")";
                        }
                       
                        //Response.Write(workshop_list);
                       
                    }
                }
            }
            else
            {
                if (MyUser.GetFunctionGroup_ID() == "3")
                {
                    if (!string.IsNullOrEmpty(ProductBrandList.Value))
                    {
                        where += " and ProductBrand_ID in (" + ProductBrandList.Value + ")";
                    }

                }
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Workshop_ID from Workshop where  (Active<>-1 or Active is null )" + where + " order by Name ASC");
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên, hộ sản xuất --", "0"));

            //UserProfile UserProfile;
            //UserProfile = UserProfile.GetProfile(Context.User.Identity.Name);
            //if (UserProfile.Workshop_ID != null)
            //{
            //    ddlWorkshop.SelectedValue = UserProfile.Workshop_ID;
            //}

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadWorkshop", ex.ToString());
        }
    }

    private void LoadProductPackage()
    {
        try
        {

            //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            //int Zone_ID = 0;
            //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
            //{
            //    Zone_ID =Convert.ToInt32( MyUser.GetZone_ID());
            //}
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                if (!string.IsNullOrEmpty(ProductBrandList.Value))
                {
                    dtSet = BusinessRulesLocator.Conllection().GetProductPackageV3(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlProductPackageOrder.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtCODE.Text.Trim(), txtSGTIN.Text.Trim(), Convert.ToInt32(ddlProduct.SelectedValue) ,txtName.Text, ProductBrandList.Value, " LastEditDate DESC");
                }
            }
            else
            {
                dtSet = BusinessRulesLocator.Conllection().GetProductPackageV3(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlProductPackageOrder.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtCODE.Text.Trim(), txtSGTIN.Text.Trim(), Convert.ToInt32(ddlProduct.SelectedValue) ,txtName.Text, "", " LastEditDate DESC");
            }

            grdProductPackage.DataSource = dtSet.Tables[0];
            grdProductPackage.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadProductPackage();
    }
    private void FillProductBrand()
    {
        try
        {

            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, "");
            }

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

    
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageStatusBO().GetAsDataTable(" ProductPackageStatus_ID<>6", " ProductPackageStatus_ID ASC");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "ProductPackageStatus_ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- Chọn trạng thái --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackage_Add.aspx", false);
    }



    protected void rptProductCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }


    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProductPackage();
        }

    }

    protected void ddlCha_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlTieuChuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void grdProductPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
        //    LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
        //    LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
        //    Literal lblText = e.Item.FindControl("lblText") as Literal;
        //    Literal lblProductPackage = e.Item.FindControl("lblProductPackage") as Literal;
        //    // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
        //    //if (lblApproved != null)
        //    //{
        //    //    if (lblApproved.Text == "False")
        //    //    {
        //    //        btnDeactive.Visible = true;
        //    //        btnActive.Visible = false;
        //    //        lblText.Text = "Ngừng kích hoạt";
        //    //    }
        //    //    else
        //    //    {
        //    //        lblText.Text = "Đang kích hoạt";
        //    //        btnDeactive.Visible = false;
        //    //        btnActive.Visible = true;
        //    //    }
        //    //}
        //}
    }

    protected void grdProductPackage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductPackage_ID = Convert.ToInt32(e.CommandArgument);
        ProductPackageRow _ProductPackageRow = new ProductPackageRow();
        _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
        switch (e.CommandName)
        {
            case "Delete":
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROM QRCodePackage where ProductPackage_ID=" + ProductPackage_ID);
                if (dt.Rows.Count == 0)
                {
                    if (MyActionPermission.CanDeleteProductPackage(ProductPackage_ID, ref Message))
                    {
                        _ProductPackageRow.ProductPackageStatus_ID = 6;
                        BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                        //BusinessRulesLocator.GetProductPackageBO().DeleteByPrimaryKey(ProductPackage_ID);
                        MyActionPermission.WriteLogSystem(ProductPackage_ID, "Xóa - " + _ProductPackageRow.Name);
                        lblMessage.Text = ("Xóa bản ghi thành công !");
                    }
                    else
                    {
                        lblMessage.Text = Message;
                        lblMessage.Style.Add("background", "wheat");
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = ("Lô sản xuất đã được tạo lô mã định danh không xóa được");
                }

                break;
            case "Active":
                _ProductPackageRow.ProductPackageStatus_ID = 1;
                BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                lblMessage.Text = ("Kích hoạt thành công thành công !");
                break;
        }
        lblMessage.Visible = true;
        LoadProductPackage();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
        LoadProductPackage();
        FillProduct();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    public string NotificationTask(int ProductPackage_ID)
    {
        string link = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetTaskNotification(ProductPackage_ID, 3);
        if (dt.Rows.Count == 1)
        {
            link = dt.Rows[0]["Link"].ToString();
        }
        return link;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet dtSet = new DataSet();
        DataTable dt = new DataTable();
        if (MyUser.GetFunctionGroup_ID() == "3")
        {
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                dtSet = BusinessRulesLocator.Conllection().GetProductPackageV3(1, 5000, 7, 0, Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlProductPackageOrder.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtCODE.Text.Trim(), txtSGTIN.Text.Trim(), Convert.ToInt32(ddlProduct.SelectedValue) ,txtName.Text, ProductBrandList.Value, " LastEditDate DESC");
            }
        }
        else
        {
            dtSet = BusinessRulesLocator.Conllection().GetProductPackageV3(1, 5000, 7, 0, Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlProductPackageOrder.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtCODE.Text.Trim(), txtSGTIN.Text.Trim(), Convert.ToInt32(ddlProduct.SelectedValue) ,txtName.Text, "", " LastEditDate DESC");
        }
        dt = dtSet.Tables[0];
        string attachment = "attachment; filename= danh_sach_lo_san_xuat.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "DANH SÁCH LÔ SẢN XUẤT (Tổng: " + dt.Rows.Count + ") \n\n";
        tab += "Thời gian từ: " + DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") + "-" + DateTime.Parse(ctlDatePicker1.ToDate.ToString()).ToString("dd/MM/yyyy") + "\n";
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName == "Name" || dc.ColumnName == "ProductName" || dc.ColumnName == "RowNum" || dc.ColumnName == "Code" || dc.ColumnName == "SGTIN" || dc.ColumnName == "SGTIN" || dc.ColumnName == "HoSX" || dc.ColumnName == "TrangThai" || dc.ColumnName == "SGTIN" || dc.ColumnName == "StartDate" || dc.ColumnName == "EndDate")
            {
                Response.Write(tab + dc.ColumnName.Replace("RowNum", "STT").Replace("HoSX", "Hộ sản xuất/ nhân viên").Replace("ProductName", "Tên sản phẩm").Replace("Name", "Tên lô").Replace("TrangThai", "Trạng thái").Replace("StartDate", "Ngày bắt đầu").Replace("EndDate", "Ngày thu hoạch"));
                tab = "\t";
            }
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                if (i == 0 || i == 1 || i == 23 || i== 24 || i == 26 || i == 25 || i == 8 || i == 9 || i == 17)
                {
                    if (i == 25 || i == 26)
                    {
                        if (!string.IsNullOrEmpty(dr[i].ToString()))
                        {
                            Response.Write(tab + DateTime.Parse(dr[i].ToString()).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            Response.Write(tab + "NULL");
                        }
                    }
                    else
                    {
                        Response.Write(tab + dr[i].ToString());
                    }

                    tab = "\t";
                }

            }
            Response.Write("\n");
        }
        Response.End();
    }
    public string QRCode(string ProductPackage_ID)
    {

        string img = string.Empty;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("https://esupplychain.vn/product-package/" + ProductPackage_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                img = "<img width='150px' class='mt-2' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
        return img;
    }
    protected void LoadZone()
    {
        try
        {
            string where = " Active=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(where, " Name ASC");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));


            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    ddlZone.SelectedValue = MyUser.GetZone_ID();
                    if (MyUser.GetAccountType_ID() == "7")
                    {
                        ddlZone.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }
    private void FillWard()

    {
        string where = string.Empty;

        if (ddlDistrict.SelectedValue != "0")
        {
            where = "District_ID = " + ddlDistrict.SelectedValue;
        }
        ddlWard.DataSource = BusinessRulesLocator.GetWardBO().GetAsDataTable("" + where, "");
        ddlWard.DataValueField = "Ward_ID";
        ddlWard.DataTextField = "Name";
        ddlWard.DataBind();
        ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));
    }

    private void FillDistrict()
    {
        string where = string.Empty;
        if (ddlLocation.SelectedValue != "0")
        {
            where += "Location_ID = " + ddlLocation.SelectedValue;
        }
        ddlDistrict.DataSource = BusinessRulesLocator.GetDistrictBO().GetAsDataTable("" + where, "");
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataTextField = "Name";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
    }

    private void FillDepartment()
    {
        ddlSo.DataSource = BusinessRulesLocator.GetDepartmentManBO().GetAsDataTable("", "");
        ddlSo.DataValueField = "DepartmentMan_ID";
        ddlSo.DataTextField = "Name";
        ddlSo.DataBind();
        ddlSo.Items.Insert(0, new ListItem("-- Lọc theo sở ngành--", "0"));
    }
    private void FillProductPackageOrder()
    {
        string where = string.Empty;
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
        }
        ddlProductPackageOrder.DataSource = BusinessRulesLocator.GetProductPackageOrderBO().GetAsDataTable(" Active <>-1 " + where, "");
        ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
        ddlProductPackageOrder.DataTextField = "Name";
        ddlProductPackageOrder.DataBind();
        ddlProductPackageOrder.Items.Insert(0, new ListItem("-- Lọc theo Lệnh --", "0"));
    }

    protected void FillProduct()
    {
        string where = string.Empty;
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
        }
        ddlProduct.DataSource = BusinessRulesLocator.GetProductBO().GetAsDataTable(" Active = 1 " + where, " CreateDate DESc");
        ddlProduct.DataValueField = "Product_ID";
        ddlProduct.DataTextField = "Name";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new ListItem("-- Lọc theo sản phẩm --","0"));
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDistrict();
        LoadProductPackage();
    }
    protected void ddlSo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillWard();
        LoadProductPackage();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    protected void ddlProductPackageOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    protected void FillLocation()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", "Name ASC");
            if (dt.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "Location_ID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-- Tỉnh thành --", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
                ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillLocation", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            string where = " 1=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAreaBO().GetAsDataTable(where, " Name ASC");
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                {
                    if (MyUser.GetArea_ID() != "0")
                    {
                        ddlArea.SelectedValue = MyUser.GetArea_ID();
                        ddlArea.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected string LoadNameQTCN(string Manufature_ID)
    {
        string Name = string.Empty;
        ManufactureTechRow _ManufactureTechRow = BusinessRulesLocator.GetManufactureTechBO().GetByPrimaryKey(Convert.ToInt32(Manufature_ID));
        if (_ManufactureTechRow != null)
        {
            Name = _ManufactureTechRow.Name;
        }
        return Name;
    }
    protected string CheckUserQL(int TaskType_ID)
    {
        string check = string.Empty;
        if (MyUser.GetFunctionGroup_ID() == "2" && MyUser.GetAccountType_ID() == "2")
        {
            if (MyUser.GetDepartment_ID() != "")
            {
                DepartmentRow _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Convert.ToInt32(MyUser.GetDepartment_ID()));
                if (_DepartmentRow != null)
                {
                    string[] array = _DepartmentRow.IsListTaskType_IDNull ? null : _DepartmentRow.ListTaskType_ID.Split(',');
                    if (array != null)
                    {
                        var Result = Array.Find(array, element => element == Convert.ToString(TaskType_ID));
                        if (Result != null)
                        {
                            check = "";
                        }
                        else
                        {
                            check = "none1";

                        }
                    }

                }
            }
        }
        return check;
    }
    protected string NotiUpdateMaterial(int ProductPackage_ID, int Product_ID)
    {
        string Link = string.Empty;
        ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
        if (_ProductPackageRow != null)
        {
            if (_ProductPackageRow.ProductPackageStatus_ID == 1 || _ProductPackageRow.ProductPackageStatus_ID == 5)
            {
                if (!_ProductPackageRow.IsProductPackageOrder_IDNull)
                {
                    DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList("Select WE.*, CONCAT(WI.Name +' - ',WI.CodeMaterialPackage) as NameMaterial,WI.Unit from WarehouseExportMaterial WE left join WarehouseImport WI on WE.WarehouseImport_ID = Wi.WarehouseImport_ID  where WE.WarehouseExport_ID in (Select WarehouseExport_ID from WarehouseExport where Active = 1 and Product_ID = " + Product_ID + " and ProductPackageOrder_ID = " + _ProductPackageRow.ProductPackageOrder_ID + ") ");
                    DataTable dtMaterialExcept = BusinessRulesLocator.GetProductPackageVsMaterialBO().GetAsDataTable("ProductPackage_ID =" + ProductPackage_ID, "");
                    if (dtMaterial.Rows.Count != dtMaterialExcept.Rows.Count)
                    {
                        Link = "Vui lòng cài đặt định mức vật tư !";
                    }

                }
            }
        }
        return Link;
    }
}