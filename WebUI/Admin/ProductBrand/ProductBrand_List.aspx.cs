using DbObj;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductBrand_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    public string SalesID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(MyUser.GetFunctionGroup_ID()))
        {
            if (MyUser.GetFunctionGroup_ID() == "9")
            {
                if (MyUser.GetAccountCheckVN_ID() != "1")
                {
                    SalesID = MyUser.GetUser_ID().ToString();
                }
            }
        }
        if (Common.GetFunctionGroupDN())
        {
            if (Request.QueryString["mode"] != null)
            {
                Response.Redirect("/Admin/ProductBrand/ProductBrand_Edit?ProductBrand_ID=" + MyUser.GetProductBrand_ID() + "&mode=sc", false);
            }
            else
            {
                Response.Redirect("/Admin/ProductBrand/ProductBrand_Edit?ProductBrand_ID=" + MyUser.GetProductBrand_ID(), false);
            }
            btnAdd.Visible = false;

        }

        if (!IsPostBack)
        {
            FillLocation();
            FillProductBrandType();
            FillFunctionGroup();
            FillChainLink();
            FillDepartment();
            //FillDistrict();
            //FillWard();
            if (MyUser.GetFunctionGroup_ID() == "8" || MyUser.GetFunctionGroup_ID() == "3")
            {
                // ddlFunctionGroup.SelectedValue = MyUser.GetFunctionGroup_ID();
                //ddlFunctionGroup.Enabled = false;
                if (MyUser.GetRank_ID() == "2")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    FillDistrict();
                    ddlLocation.Enabled = false;
                    ddlDepartment.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlDepartment.Enabled = false;
                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDepartment.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlDepartment.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                }
                else if (MyUser.GetRank_ID() == "4")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDepartment.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlDepartment.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                    ddlWard.SelectedValue = MyUser.GetWard_ID();
                    ddlWard.Enabled = false;
                }
                DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable("UserId ='" + MyUser.GetUser_ID() + "'", "");
                if (dt.Rows.Count > 0)
                {
                    ListProductBrand.Value = dt.Rows[0]["ProductBrand_ID_List"].ToString();
                }
            }
        }

        LoadProductBrand();
        ResetMsg();
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
        ddlDepartment.DataSource = BusinessRulesLocator.GetDepartmentManBO().GetAsDataTable("", "");
        ddlDepartment.DataValueField = "DepartmentMan_ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-- Lọc theo sở ngành--", "0"));
    }
    private void FillProductBrandType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select * from ProductBrandType where Active=1 ");
            ddlProductBrandType.DataSource = dt;
            ddlProductBrandType.DataTextField = "Name";
            ddlProductBrandType.DataValueField = "ProductBrandType_ID";
            ddlProductBrandType.DataBind();
            ddlProductBrandType.Items.Insert(0, new ListItem("-- Lọc theo loại hình doanh nghiệp --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillChainLink()
    {
        try
        {
            DataTable dtChainLink = new DataTable();
            dtChainLink = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlChainLink.DataSource = dtChainLink;
            ddlChainLink.DataTextField = "Name";
            ddlChainLink.DataValueField = "ChainLink_ID";
            ddlChainLink.DataBind();
            ddlChainLink.Items.Insert(0, new ListItem("-- Chọn chuỗi liên kết --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillChainLink", ex.ToString());
        }
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void FillFunctionGroup()
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable("Type = 1", "Sort ASC");
        ddlFunctionGroup.DataSource = dt;
        ddlFunctionGroup.DataTextField = "Name";
        ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
        ddlFunctionGroup.DataBind();
        ddlFunctionGroup.Items.Insert(0, new ListItem("-- Lọc theo gói doanh nghiệp --", "0"));
    }

    protected void LoadProductBrand()
    {
        try
        {
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            string where = string.Empty;
            DataTable dt = new DataTable();
            if (ListProductBrand.Value != "")
            {
                where = ListProductBrand.Value;
            }
            dtSet = BusinessRulesLocator.Conllection().GetProductBrand_PagingV2(Pager1.CurrentIndex, pageSize, pageSize, Convert.ToInt32(ddlProductBrandType.SelectedValue), txtName.Text, Convert.ToInt32(ddlFunctionGroup.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, where, SalesID, Convert.ToInt32(ddlChainLink.SelectedValue));
            rptProductBrand.DataSource = dtSet.Tables[0];
            rptProductBrand.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadProductBrand", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductBrand_Add.aspx", false);
    }

    protected void rptProductBrand_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductBrand_ID = Convert.ToInt32(e.CommandArgument);
        ProductBrandRow _ProductBrandRow = new ProductBrandRow();
        _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
        switch (e.CommandName)
        {
            case "Delete":
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList("Select * from Product where ProductBrand_ID=" + ProductBrand_ID);
                if (dt.Rows.Count == 0)
                {
                    MyActionPermission.WriteLogSystem(ProductBrand_ID, "Xóa - " + _ProductBrandRow.Name);
                    BusinessRulesLocator.GetProductBrandBO().DeleteByPrimaryKey(ProductBrand_ID);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = ("Doanh nghiệp đã có sản phẩm không xóa được");
                }

                break;
            case "Active":
                _ProductBrandRow.Active = true;
                BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _ProductBrandRow.Active = false;
                BusinessRulesLocator.GetProductBrandBO().Update(_ProductBrandRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
        }
        lblMessage.Visible = true;
        LoadProductBrand();
    }


    protected void rptProductBrand_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "False")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else
                {
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                }

                if (Common.GetFunctionGroupDN())
                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = false;
                }
            }
        }
    }

    protected void ddlProductBrandType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductBrand();
    }
    protected void ExportFile()
    {


        string ASProductBrandName = "";


        if (ddlProductBrandType.SelectedValue != "0")
        {

            ASProductBrandName += "DANH SÁCH DOANH NGHIỆP" + " ( loại hình:" + ddlProductBrandType.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "DANH SÁCH TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }
        DataTable dt = new DataTable();

        dt = BusinessRulesLocator.Conllection().GetProductBrand_PagingV2(1, 10000, 10000, Convert.ToInt32(ddlProductBrandType.SelectedValue), txtName.Text, Convert.ToInt32(ddlFunctionGroup.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, "", SalesID, Convert.ToInt32(ddlChainLink.SelectedValue)).Tables[0];

        dt.Columns.Remove("ProductBrand_ID");
        dt.Columns.Remove("ProductBrandType_ID_List");
        dt.Columns.Remove("Active");
        dt.Columns.Remove("CreateDate");
        dt.Columns.Remove("CreateBy");
        dt.Columns.Remove("LastEditDate");
        dt.Columns.Remove("FunctionGroupname");
        dt.Columns.Remove("UserName");
        dt.Columns.Remove("NguoiSua");
        dt.Columns.Remove("Vaoluc");
        dt.Columns.Remove("NguoiDanhGia");
        dt.Columns.Remove("SoSao");
        dt.Columns.Remove("RoleChain_ID");

     

        dt.Columns["RowNum"].ColumnName = "STT";
        dt.Columns["Name"].ColumnName = "Tên doanh nghiệp";
        dt.Columns["Telephone"].ColumnName = "Số Điện Thoại";
        dt.Columns["Mobile"].ColumnName = "Mobile";
        dt.Columns["Address"].ColumnName = "Địa chỉ";
        dt.Columns["Director"].ColumnName = "Người đại diện";
        dt.Columns["TenChuoi"].ColumnName = "Chuỗi liên kết";
        dt.Columns["RoleName"].ColumnName = "Vai trò trong chuỗi";
        dt.Columns["SoSP"].ColumnName = "Số sản phẩm đăng ký";
        dt.Columns["SoTemLop1"].ColumnName = "Số tem lớp 1 được cấp phát";
        dt.Columns["SoTemLop2"].ColumnName = "Số tem lớp 2 được cấp phát";
        //dt.Columns["FunctionGroupname"].ColumnName = "Gói Doanh Nghiệp";
        dt.AcceptChanges();

        string tab = ASProductBrandName + "<br>";
        tab += "(Tổng: " + dt.Rows.Count + ")<br><br>";
        GridView gvDetails = new GridView();

        gvDetails.DataSource = dt;


        gvDetails.AllowPaging = false;

        gvDetails.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "danh-sach-doanh-nghiep.xls"));
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);



        //Change the Header Row back to white color
        // gvDetails.HeaderRow.Style.Add("background-color", "#7e94eb");
        gvDetails.HeaderRow.Style.Add("color", "#fff");
        //Applying stlye to gridview header cells
        for (int i = 0; i < gvDetails.HeaderRow.Cells.Count; i++)
        {
            gvDetails.HeaderRow.Cells[i].Style.Add("background-color", "#7e94eb");
        }
        gvDetails.RenderControl(htw);

        Response.Write(tab + sw.ToString());
        Response.End();
    }

    protected void ExportFile1()
    {


        string ASProductBrandName = "";


        if (ddlProductBrandType.SelectedValue != "0")
        {

            ASProductBrandName += "DANH SÁCH DOANH NGHIỆP" + " ( loại hình:" + ddlProductBrandType.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "DANH SÁCH TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }
        DataTable dt = new DataTable();

        dt = BusinessRulesLocator.Conllection().GetAllList(@"Select PB.Name as DoanhNghiep,L.Name as Tinh,B.Name as Nganh, BT.Title as LinhVuc from ProductBrand PB left join Location L on PB.Location_ID = L.Location_ID left join Branch B on PB.Branch_ID = B.Branch_ID left join BusinessType BT on PB.BusinessType_ID = BT.BusinessType_ID where PB.Active= 1");



        dt.Columns["LinhVuc"].ColumnName = "Lĩnh Vực Kinh Doanh";
        dt.Columns["DoanhNghiep"].ColumnName = "Tên doanh nghiệp";
        dt.Columns["Tinh"].ColumnName = "Tỉnh Thành";
        dt.Columns["Nganh"].ColumnName = "Ngành";
       
        //dt.Columns["FunctionGroupname"].ColumnName = "Gói Doanh Nghiệp";
        dt.AcceptChanges();

        string tab = ASProductBrandName + "<br>";
        tab += "(Tổng: " + dt.Rows.Count + ")<br><br>";
        GridView gvDetails = new GridView();

        gvDetails.DataSource = dt;


        gvDetails.AllowPaging = false;

        gvDetails.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "danh-sach-doanh-nghiep.xls"));
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);



        //Change the Header Row back to white color
        // gvDetails.HeaderRow.Style.Add("background-color", "#7e94eb");
        gvDetails.HeaderRow.Style.Add("color", "#fff");
        //Applying stlye to gridview header cells
        for (int i = 0; i < gvDetails.HeaderRow.Cells.Count; i++)
        {
            gvDetails.HeaderRow.Cells[i].Style.Add("background-color", "#7e94eb");
        }
        gvDetails.RenderControl(htw);

        Response.Write(tab + sw.ToString());
        Response.End();
    }
    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductBrand();

    }

    protected void ddlFunctionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductBrand();
    }
    protected void ddlChainLink_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductBrand();
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProductBrand();
        }
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDistrict();
        LoadProductBrand();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductBrand();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillWard();
        LoadProductBrand();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductBrand();
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadProductBrand();
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductBrand();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductBrand();
    }
    public string ReturnStar(string Star)
    {
        string st = string.Empty;
        switch (Star)
        {
            case "1":
                st = "<img src='../../theme/assets/images/1.jpg'/>";
                break;
            case "2":
                st = "<img src='../../theme/assets/images/2.jpg'/>";
                break;
            case "3":
                st = "<img src='../../theme/assets/images/3.jpg'/>";
                break;
            case "4":
                st = "<img src='../../theme/assets/images/4.jpg'/>";
                break;
            case "5":
                st = "<img src='../../theme/assets/images/5.jpg'/>";
                break;
            default:
                st = "<img src='../../theme/assets/images/0.jpg'/>";
                break;
        }
        return st;
    }
}