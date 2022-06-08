using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_QRCodePackage_ReportQRCodePackage : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    public int No = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProductPackageDll();
            LoadProduct();
            FillProductBrand();
            FillDepartment();
            FillDistrict();
            FillWard();
            LoadStatus();
            LoadProductPackage();
        }
        ResetMsg();
    }
    private void LoadProduct()
    {
        try
        {
            string where = string.Empty;
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
    private void LoadProductPackageDll()
    {
        try
        {

            string where = string.Empty;

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
    private void FillWard()

    {
        string where = string.Empty;
        if (ddlDistrict.SelectedValue != "0")
        {
            where = "and District_ID = " + ddlDistrict.SelectedValue;
        }
        ddlWard.DataSource = BusinessRulesLocator.GetWardBO().GetAsDataTable(" Location_ID = 92" + where, "");
        ddlWard.DataValueField = "Ward_ID";
        ddlWard.DataTextField = "Name";
        ddlWard.DataBind();
        ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));
    }

    private void FillDistrict()
    {
        ddlDistrict.DataSource = BusinessRulesLocator.GetDistrictBO().GetAsDataTable("Location_ID = 92", "");
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
    private void LoadProductPackage()
    {
        try
        {

            //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().ReportQRCodePackage(1, 9999, 7, Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlProductPackage.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtName.Text, txtSerial.Text, "", " P.LastEditDate DESC");
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

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
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
                Common.FillProductBrand(ddlProductBrand, " ");
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
            dt = BusinessRulesLocator.GetQRCodeStatusBO().GetAsDataTable(" QRCodeStatus_ID>=0", " QRCodeStatus_ID ASC");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "QRCodeStatus_ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- Chọn trạng thái --", "-1"));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_Add.aspx", false);
    }


    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LoadProductPackage();
        }

    }

    protected void ddlCha_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlTieuChuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackageReport_List.aspx", false);
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillWard();
        LoadProductPackage();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductPackage();
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        DataSet dtSet = BusinessRulesLocator.Conllection().ReportQRCodePackage(1, 9999, 7, Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlProductPackage.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtName.Text, txtSerial.Text, "", " P.LastEditDate DESC");
        DataTable dt = dtSet.Tables[0];
        dt.Columns["RowNum"].ColumnName = "STT";
        dt.Columns["Name"].ColumnName = "Tên lô mã";
        dt.Columns["ProductName"].ColumnName = "Tên sản phẩm";
        dt.Columns["UserName"].ColumnName = "Người tạo lô mã";
        dt.Columns["NgaySX"].ColumnName = "Ngày sản xuất";
        dt.Columns["NgayThuHoach"].ColumnName = "Ngày thu hoạch";
        dt.Columns["QRCodeNumber"].ColumnName = "Số lượng tem";
        dt.Columns["SerialNumberStart"].ColumnName = "Serial đầu";
        dt.Columns["SerialNumberEnd"].ColumnName = "Serial cuối";
        dt.Columns.Remove("LastEditDate");

        dt.AcceptChanges();
        string attachment = "attachment; filename=Bao_cao_thong_ke_danh_sach_lo_ma_dinh_danh_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "Báo cáo thống kê danh sách lô mã định danh \nThời gian báo cáo từ ngày: " + ctlDatePicker1.FromDate.ToString("dd-MM-yyyy") + " đến ngày: " + ctlDatePicker1.ToDate.ToString("dd-MM-yyyy") + " \n";
        tab += "Tên lô: " + (ddlProductPackage.SelectedItem.Text.Contains("--") ? "Tất cả" : ddlProductPackage.SelectedItem.Text);
        tab += "\nTên sản phẩm: " + (ddlProduct.SelectedItem.Text.Contains("--") ? "Tất cả" : ddlProduct.SelectedItem.Text) + " ";
        tab += "\nTên doanh nghiệp: " + (ddlProductBrand.SelectedItem.Text.Contains("--") ? "Tất cả" : ddlProductBrand.SelectedItem.Text) + " \n\n";
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
}