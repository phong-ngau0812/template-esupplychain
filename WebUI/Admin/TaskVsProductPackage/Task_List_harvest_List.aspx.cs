using DbObj;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

public partial class Task_List_harvest_List : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    public decimal TotalThuhoach = 0;
    public string name, code = string.Empty;
    public string TitleNKTH = "Nhật ký nhập kho";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        if (MyUser.GetFunctionGroup_ID() == "3")
        {
            btnAdd.Visible = false;
        }
        if (MyUser.GetFunctionGroup_ID() == "6")
        {
            Response.Redirect("/Admin/ProductPackage/ProductPackage_List", false);
        }
        else
        {
            Init();
        }
        if (!IsPostBack)
        {
            FillProductBrand();
            LoadProductPackage();
            LoadData();
        }
        ResetMsg();
    }
    private void Init()
    {
        ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
        if (_ProductPackageRow != null)
        {
            //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
            MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
            name = _ProductPackageRow.Name;
            ProductBrand_ID = _ProductPackageRow.ProductBrand_ID;
            code = _ProductPackageRow.Code;
        }
    }
    private void FillProductBrand()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ProductBrand_ID, " Sort, Name ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            //  ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", "0"));
            ddlProductBrand.SelectedValue = ProductBrand_ID.ToString();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadProductPackage()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            //ddlProductPackage.DataSource = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "ProductPackage_ID DESC");
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select * From ProductPackage where 1=1 and ProductPackageStatus_ID<>6" + where + " Order by ProductPackage_ID DESC");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.SelectedValue = ProductPackage_ID.ToString();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void LoadData()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Unit,T.Name,P.Code, P.SGTIN,P.Name as PackageName, Q.Name as Quality ,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.HarvestDayRemain ,U.UserName,  T.HarvestVolume  
from Task T 
left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
left join aspnet_Users U on U.UserId= T.CreateBy
left join ProductPackage P on P.ProductPackage_ID= T.ProductPackage_ID
left join Product PR on P.Product_ID= PR.Product_ID
left join Quality Q on Q.Quality_ID= PR.Quality_ID
where T.TaskType_ID=3 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");
            rptTask.DataSource = dt;
            rptTask.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Task_List_harvest_Add?ProductPackage_ID=" + ProductPackage_ID, false);
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void rptTask_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Task_ID = Convert.ToInt32(e.CommandArgument);
        TaskRow _TaskRow = BusinessRulesLocator.GetTaskBO().GetByPrimaryKey(Task_ID);
        switch (e.CommandName)
        {
            case "Delete":
                BusinessRulesLocator.GetTaskBO().DeleteByPrimaryKey(Task_ID);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;

        }
        lblMessage.Visible = true;
        LoadData();
    }
    protected void rptTask_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblTongThuHoach = e.Item.FindControl("lblTongThuHoach") as Literal;
            if (lblTongThuHoach != null)
            {
                TotalThuhoach += decimal.Parse(lblTongThuHoach.Text);
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string tendoanhnghiep = "";
            string diachi = "";
            int ProductBrand_ID = 0;

            if (ddlProductBrand.SelectedValue != "0")
            {
                ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ddlProductBrand.SelectedValue));
                if (_ProductBrandRow != null)
                {
                    ProductBrand_ID = _ProductBrandRow.ProductBrand_ID;
                    tendoanhnghiep = _ProductBrandRow.Name;
                    diachi = _ProductBrandRow.Address;

                }
            }

            string NamePackge = "";
            string NameStatus = "";
            string NameProduct = "";
            string NameQuality = "";
            string SGTIN = "";
            string StartDate = "";
            string EndtDate = "";
            string ProductBrandAddress = "";
            string Acreage = "";
            if (ddlProductPackage.SelectedValue != "0")
            {
                DataTable dtPP = BusinessRulesLocator.Conllection().GetAllList(@"select distinct  PR.Name as NameProduct, P.Name as PackageName,P.Code,P.SGTIN,CONVERT( varchar,P.StartDate,103) as StartDate , CONVERT( varchar,P.EndDate,103) as EndDate, PB.Address as ProductBrandAddress,  ISNULL( P.Acreage,'0')as Acreage ,  ISNULL( Q.Name,N'Tự công bố') as Quality, PS.Name as NameStatus  from ProductPackage P
            left join Product PR on P.Product_ID= PR.Product_ID
			 left join ProductPackageStatus PS on PS.ProductPackageStatus_ID= P.ProductPackageStatus_ID
            left join Quality Q on Q.Quality_ID= PR.Quality_ID
            left join ProductBrand PB on PB.ProductBrand_ID=P.ProductBrand_ID
                        where  P.ProductPackage_ID = " + ddlProductPackage.SelectedValue);
                if (dtPP.Rows.Count != 0)
                {
                    NamePackge += dtPP.Rows[0]["PackageName"].ToString();
                    NameProduct += dtPP.Rows[0]["NameProduct"].ToString();
                    NameQuality += dtPP.Rows[0]["Quality"].ToString();
                    SGTIN += dtPP.Rows[0]["Code"].ToString() + "|" + dtPP.Rows[0]["SGTIN"].ToString();
                    StartDate += dtPP.Rows[0]["StartDate"].ToString();
                    EndtDate += dtPP.Rows[0]["EndDate"].ToString();
                    ProductBrandAddress += dtPP.Rows[0]["ProductBrandAddress"].ToString();
                    Acreage += dtPP.Rows[0]["Acreage"].ToString();
                    NameStatus += dtPP.Rows[0]["NameStatus"].ToString();
                }




                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select DM.Name as NameDepartment, TS.Name as StatusName,P.Name as PackageName,P.Code, P.SGTIN, Q.Name as Quality ,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
left join ProductPackage P on P.ProductPackage_ID= T.ProductPackage_ID
left join Product PR on P.Product_ID= PR.Product_ID
left join TaskStep TSP on T.TaskStep_ID =TSP.TaskStep_ID
left join Department DM on TSP.Department_ID =DM.Department_ID
left join Quality Q on Q.Quality_ID= PR.Quality_ID
              where T.TaskType_ID=1 and T.ProductPackage_ID=" + ddlProductPackage.SelectedValue + " order by StartDate DESC");




                ExcelPackage excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add("bao-cao-san-xuat");
                ws.Cells.Style.Font.Name = "Times New Roman";
                ws.Cells.Style.Font.Size = 13;


                ws.Cells[1, 1].Value = "BÁO CÁO TRUY XUẤT NGUỒN GỐC SẢN PHẨM";
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1].Style.WrapText = true;
                ws.Cells[1, 1, 1, 8].Merge = true;
                ws.Cells[2, 1, 2, 8].Merge = true;
                ws.Cells[3, 1, 3, 8].Merge = true;
                ws.Cells[4, 1, 4, 8].Merge = true;
                ws.Cells[5, 1, 5, 8].Merge = true;
                ws.Cells[6, 1, 6, 8].Merge = true;

                ws.Cells[3, 1].Value = "Tên Công ty sản xuất: " + tendoanhnghiep;
                ws.Cells[4, 1].Value = "Địa chỉ: " + diachi;
                ws.Cells[5, 1].Value = "Sản phẩm truy xuất: " + NameProduct;
                ws.Cells[6, 1].Value = "Lô sản xuất: " + NamePackge;
                ws.Cells[7, 1].Value = "Khách hàng:";
                ws.Cells[8, 1].Value = "Tiêu chuẩn: " + NameQuality;
                ws.Cells[9, 1].Value = "Thời gian sản xuất: " + StartDate + " đến " + EndtDate;
                ws.Cells[10, 1].Value = "Số lệnh sản xuất: ";
                ws.Cells[11, 1].Value = "Trạng thái: " + NameStatus;

                ws.Cells[12, 1, 12, 8].Merge = true;


                ws.Cells[13, 1, 13, 8].Merge = true;
                ws.Cells[13, 1].Value = "III. Nhập kho thành phẩm";

                ws.Column(1).Width = 10;
                ws.Column(2).Width = 40;
                ws.Column(3).Width = 40;
                ws.Column(4).Width = 45;
                ws.Column(5).Width = 20;
                ws.Column(6).Width = 40;
                ws.Column(7).Width = 40;

                int Count = 14;




                ws.Cells[Count, 1, Count, 1].Value = "Lot";
                ws.Cells[Count, 2, Count, 2].Value = "Số lượng";
                ws.Cells[Count, 3, Count, 3].Value = "Người nhập";
                ws.Cells[Count, 4, Count, 4].Value = "Thủ kho nhận";
                ws.Cells[Count, 5, Count, 5].Value = "Ngày nhập";
                ws.Cells[Count, 1, Count, 5].Style.Fill.SetBackground(Color.DarkGray);

                Count += 1;

                DataTable dt2 = BusinessRulesLocator.Conllection().GetAllList(@"Select T.HarvestVolume,T.Importer,T.Stocker,T.StartDate,T.Unit from Task T left join ProductPackage P on T.ProductPackage_ID = P.ProductPackage_ID where T.TaskType_ID = 3 and T.ProductPackage_ID=" + ddlProductPackage.SelectedValue);

                if (dt2.Rows.Count > 0)
                {
                    int Flag = 1;
                    foreach (DataRow dtRow1 in dt2.Rows)
                    {
                        ws.Cells[Count, 1, Count, 1].Value = "Lot " + Flag;
                        ws.Cells[Count, 2, Count, 2].Value = Decimal.Parse(dtRow1["HarvestVolume"].ToString()).ToString("N0") + " " + dtRow1["Unit"].ToString();
                        ws.Cells[Count, 3, Count, 3].Value = dtRow1["Importer"].ToString();
                        ws.Cells[Count, 4, Count, 4].Value = dtRow1["Stocker"].ToString(); ;
                        ws.Cells[Count, 5, Count, 5].Value = DateTime.Parse(dtRow1["StartDate"].ToString()).ToString("dd/MM/yyyy");
                        Count++;
                        Flag++;
                    }

                }




                string filename = "TestExcel";

                filename = "bao-cao-dong-goi-" + Common.ConvertTitleDomain(DateTime.Now.ToString("dd/MM/yyyy")) + ".xlsx";

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("Export", ex.ToString());
        }

    }
    //    protected void btnExport_Click(object sender, EventArgs e)
    //    {
    //        int Tong = 0;
    //        string tendoanhnghiep = "";
    //        int ProductBrand_ID = 0;

    //        if (ddlProductBrand.SelectedValue != "0")
    //        {
    //            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ddlProductBrand.SelectedValue));
    //            if (_ProductBrandRow != null)
    //            {
    //                ProductBrand_ID = _ProductBrandRow.ProductBrand_ID;
    //                tendoanhnghiep = _ProductBrandRow.Name;

    //            }

    //            string NamePackge = "";
    //            string NameProduct = "";
    //            string NameQuality = "";
    //            string SGTIN = "";
    //            string StartDate = "";
    //            string EndtDate = "";
    //            string ProductBrandAddress = "";
    //            string Acreage = "";
    //            if (ddlProductPackage.SelectedValue != "0")
    //            {
    //                DataTable dtPP = BusinessRulesLocator.Conllection().GetAllList(@"select distinct  PR.Name as NameProduct, P.Name as PackageName,P.Code,P.SGTIN,CONVERT( varchar,P.StartDate,103) as StartDate , CONVERT( varchar,P.EndDate,103) as EndDate, PB.Address as ProductBrandAddress,  ISNULL( P.Acreage,'0')as Acreage ,  ISNULL( Q.Name,N'Tự công bố') as Quality  from ProductPackage P
    //left join Product PR on P.Product_ID= PR.Product_ID
    //left join Quality Q on Q.Quality_ID= PR.Quality_ID
    //left join ProductBrand PB on PB.ProductBrand_ID=P.ProductBrand_ID
    //            where  P.ProductPackage_ID = " + ddlProductPackage.SelectedValue);
    //                if (dtPP.Rows.Count != 0)
    //                {
    //                    NamePackge += dtPP.Rows[0]["PackageName"].ToString();
    //                    NameProduct += dtPP.Rows[0]["NameProduct"].ToString();
    //                    NameQuality += dtPP.Rows[0]["Quality"].ToString();
    //                    SGTIN += dtPP.Rows[0]["Code"].ToString() + "|" + dtPP.Rows[0]["SGTIN"].ToString();
    //                    StartDate += dtPP.Rows[0]["StartDate"].ToString();
    //                    EndtDate += dtPP.Rows[0]["EndDate"].ToString();
    //                    ProductBrandAddress += dtPP.Rows[0]["ProductBrandAddress"].ToString();
    //                    Acreage += dtPP.Rows[0]["Acreage"].ToString();
    //                }

    //                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@" select CONVERT( varchar,T.StartDate,103) as StartDate, T.Name,ISNULL( T.HarvestDayRemain,0) as DayRemain ,CONVERT(int, T.HarvestVolume) as HarvestVolume,  U.UserName as Account ,TS.Name as Status
    //from Task T 
    //left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
    //left join aspnet_Users U on U.UserId= T.CreateBy
    //where T.TaskType_ID=3 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");

    //                string attachment = "attachment; filename=file_excle_nhat_ky_san_xuat" + ProductBrand_ID + ".xls";
    //                Response.ClearContent();
    //                Response.AddHeader("content-disposition", attachment);
    //                Response.ContentEncoding = System.Text.Encoding.Unicode;
    //                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
    //                Response.ContentType = "application/vnd.ms-excel";
    //                string tab = "NHẬT KÝ THEO DÕI QUÁ TRÌNH SẢN XUẤT \t Lô/Mẻ: " + SGTIN + "  \n (Nhật ký thu hoạch) \t Ngày áp dụng: " + StartDate + "\n";
    //                tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
    //                tab += "Sản Phẩm: " + NameProduct + "\n";
    //                tab += "Tiêu chuẩn: " + NameQuality + "\n";
    //                tab += "Lô: " + NamePackge + "\n \n";
    //                tab += "1.Vị trí lô trồng: " + ProductBrandAddress + "\t" + "2.Diện tích trồng: " + Acreage + "(m2)\t" + "3.Ngày trồng: " + StartDate + "\t" + "4.Ngày dự kiến thu hoạch: " + EndtDate + "\n\n";
    //                tab += "Tổng: " + dt.Rows.Count + "\n \n";
    //                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
    //                foreach (DataColumn dc in dt.Columns)
    //                {
    //                    Response.Write(tab + dc.ColumnName.Replace("Name", "Thu hoạch").Replace("Account", "Người thực hiện").Replace("StartDate", "Ngày thực hiện").Replace("Status", "Trạng thái").Replace("HarvestVolume", "Số lượng").Replace("DayRemain", "Ngày thu hoạch còn lại"));
    //                    tab = "\t";
    //                }
    //                Response.Write("\n");
    //                int i;
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    Tong += Convert.ToInt32(dr["HarvestVolume"]);
    //                    tab = "";

    //                    for (i = 0; i < dt.Columns.Count; i++)
    //                    {

    //                        if (i == 4)
    //                        {
    //                            if (!string.IsNullOrEmpty(dr[i].ToString()))
    //                            {

    //                                Response.Write(tab + dr[i].ToString() + "(" + new MyUser().FullNameFromUserName(dr[i].ToString()) + ")");
    //                            }
    //                            else
    //                            {
    //                                Response.Write(tab + "NULL");
    //                            }
    //                            //Response.Write(tab + dr[i].ToString());
    //                            //tab = "\t";
    //                        }
    //                        else
    //                        {
    //                            Response.Write(tab + dr[i].ToString());
    //                            tab = "\t";
    //                        }
    //                    }
    //                    Response.Write("\n");
    //                }
    //                Response.Write("Tổng \t\t\t" + Tong);
    //                //Response.Write("\t" + TongTien);
    //                Response.End();
    //            }
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Bạn chưa chọn doanh nghiệp ";
    //            lblMessage.ForeColor = Color.Red;
    //            lblMessage.BackColor = Color.Wheat;
    //            lblMessage.Visible = true;
    //        }
    //    }
}