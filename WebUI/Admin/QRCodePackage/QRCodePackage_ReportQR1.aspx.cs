using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_QRCodePackage_QRCodePackage_ReportQR1 : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    private int productCategory_ID;
    public int No = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillProductBrand();
            FillDepartment();
            FillLocation();
            LoadProductPackageDll();
            LoadProduct();
            if (MyUser.GetFunctionGroup_ID() == "8" || MyUser.GetFunctionGroup_ID() == "2")
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
        }

        LoadProductPackage();
        ResetMsg();
    }
    private void FillProductBrand()
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

            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, where);
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
    //public string ReturnStatus(string status)
    //{
    //    string st = "";
    //    switch (status)
    //    {
    //        case "Kích hoạt đưa mã tem ra thị trường":
    //            st = "<span class=\"badge badge-success\">" + status + "</span>";
    //            break;
    //        //case "Đang thu hoạch":
    //        //    st = "<span class=\"badge badge-primary\">" + status + "</span>";
    //        //    break;
    //        //case "Thu hoạch xong":
    //        //    st = "<span class=\"badge badge-primary\">" + status + "</span>";
    //        //    break;
    //        case "Tem mới tạo":
    //            st = "<span class=\"badge badge-danger\">" + status + "</span>";
    //            break;
    //        case "Tem đang tạo...":
    //            st = "<span class=\"badge badge-warning\">" + status + "</span>";
    //            break;
    //        default:
    //            st = "";
    //            break;
    //    }

    //    return st;

    //}
    //public string Message = "";
    public int TinhTong = 0;
    protected void LoadProductPackage()
    {
        try
        {
            //    int ProductBrand_ID = 0;
            //    if (Common.GetFunctionGroupDN())
            //    {
            //        ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            //    }

            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().GetQRCodePackageReportQR1(1, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlProductPackage.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);
            grdProductPackage.DataSource = dtSet.Tables[0];
            grdProductPackage.DataBind();
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

                //foreach (DataRow item in dtSet.Tables[0].Rows)
                //{
                //    TinhTong += Convert.ToInt32(item["QRCodeNumber"].ToString());
                //}
                // x_box_pager.Visible = false;
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

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void LoadProductPackageDll()
    {
        try
        {

            string where = string.Empty;
            if (Common.GetFunctionGroupDN())
            {
                where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            }
            else
            {
                //if (ddlProductBrand.SelectedValue != "")
                //{
                //    where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                //}
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select Name, ProductPackage_ID from ProductPackage where 1=1 " + where + " order by ProductPackage_ID DESC");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.Items.Insert(0, new ListItem("-- Chọn Lô sản xuất --", "0"));
            ddlProductPackage.Items.Insert(1, new ListItem("# Lô chưa xác định #", "-1"));
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
    private void LoadProduct()
    {
        try
        {
            string where = string.Empty;
            //if (ddlProducCategory.SelectedValue != "")
            //{
            //    where += " And ProductCategory_ID=" + ddlProducCategory.SelectedValue;
            //}
            //if (MyUser.GetFunctionGroup_ID() == "2" || MyUser.GetFunctionGroup_ID() == "4")
            //{
            //    where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            //}
            //bool flag = false;
            //int product_ID = 0;
            //if (ddlProductPackage.SelectedValue != "" && ddlProductPackage.SelectedValue != "-1")
            //{
            //    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackage.SelectedValue));
            //    if (_ProductPackageRow != null)
            //    {
            //        if (!_ProductPackageRow.IsProduct_IDNull)
            //        {
            //            where += " and Product_ID=" + _ProductPackageRow.Product_ID;
            //            flag = true;
            //            product_ID = _ProductPackageRow.Product_ID;
            //        }
            //    }
            //}
            //if (ddlProductBrand.SelectedValue != "")
            //{
            //    where += "And ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            //}
            if (Common.GetFunctionGroupDN())
            {
                where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", "0"));
            ddlProduct.Items.Insert(1, new ListItem("#Sản phẩm chưa xác định#", "-1"));
            //if (flag)
            //{
            //    ddlProduct.SelectedValue = product_ID.ToString();
            //}



        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProductPackage();
        }

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
        LoadProductPackage();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackageReport_List.aspx", false);
    }


    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        //int ProductBrand_ID = 0;
        //if (Common.GetFunctionGroupDN())
        //{
        //    ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
        //}
        DataSet dtSet = BusinessRulesLocator.Conllection().GetQRCodePackageReportQR1(1, 9999, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlProductPackage.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);
        DataTable dt = dtSet.Tables[0];
        dt.Columns["RowNum"].ColumnName = "STT";
        dt.Columns["QRCodeSerial"].ColumnName = "Serial Number";
        dt.Columns["Name"].ColumnName = "Tên sản phẩm";
        dt.Columns["Malo"].ColumnName = "Lô sản xuất";
        dt.Columns["TenDN"].ColumnName = "Tên doanh nghiệp";
        dt.Columns["NgayTaoTem"].ColumnName = "Ngày tạo tem";
        dt.Columns["Location"].ColumnName = "Địa chỉ xác thực";
        dt.Columns["TrackingDate"].ColumnName = "Thời gian xác thực";
        dt.Columns.Remove("QRCodeContent");
        dt.Columns.Remove("ProductPackage_ID");

        dt.AcceptChanges();
        string attachment = "attachment; filename=Bao_cao_san_pham_da_xac_thuc_tem_lop_1_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "Báo cáo sản phẩm đã xác thực tem lớp 1 \nThời gian báo cáo từ ngày: " + ctlDatePicker1.FromDate.ToString("dd-MM-yyyy") + " đến ngày: " + ctlDatePicker1.ToDate.ToString("dd-MM-yyyy") + " \n";
        tab += "Tên lô: " + (ddlProductPackage.SelectedItem.Text.Contains("--") ? "Tất cả" : ddlProductPackage.SelectedItem.Text);
        tab += "\nTên sản phẩm: " + (ddlProduct.SelectedItem.Text.Contains("--") ? "Tất cả" : ddlProduct.SelectedItem.Text) + " \n\n";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        int Tong = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        //Response.Write("Tổng: ");
        //Response.Write("\t\t\t" + Tong.ToString());
        Response.End();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlProductSet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void btnSearch_Click2(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
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
}