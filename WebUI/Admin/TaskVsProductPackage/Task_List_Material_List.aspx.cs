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

public partial class Task_List_Material_List : System.Web.UI.Page
{
    public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    public string name, code = string.Empty;
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
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select M.Unit,P.Name as PackageName, Q.Name as Quality ,P.Code, P.SGTIN, TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , U.UserName, T.Name  from Task T 
left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
left join aspnet_Users U on U.UserId= T.CreateBy
left join Material M on M.Material_ID= T.Material_ID
left join ProductPackage P on P.ProductPackage_ID= T.ProductPackage_ID
left join Product PR on P.Product_ID= PR.Product_ID
left join Quality Q on Q.Quality_ID= PR.Quality_ID
where T.TaskType_ID=2 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");
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
        Response.Redirect("Task_List_Material_Add?ProductPackage_ID=" + ProductPackage_ID, false);
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
    public decimal TotalThuhoach = 0;
    protected void rptTask_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblQuantity = e.Item.FindControl("lblQuantity") as Literal;
            Literal lblPrice = e.Item.FindControl("lblPrice") as Literal;
            if (lblQuantity != null && lblPrice != null)
            {
                TotalThuhoach += (decimal.Parse(lblQuantity.Text) * decimal.Parse(lblPrice.Text));
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


                ws.Cells[1, 1].Value = "BÁO CÁO NHẬT KÝ VẬT TƯ";
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

                ws.Cells[7, 1, 7, 8].Merge = true;

                ws.Column(1).Width = 20;
                ws.Column(2).Width = 50;
                ws.Column(3).Width = 50;
                ws.Column(4).Width = 30;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 50;
                ws.Column(7).Width = 40;


                ws.Cells[8, 1, 8, 1].Value = "Kho";
                ws.Cells[8, 2, 8, 2].Value = "Tên Phiếu Xuất - Mã Phiếu Xuất";
                ws.Cells[8, 3, 8, 3].Value = "Vật tư xuất kho";
                ws.Cells[8, 4, 8, 4].Value = "Mã vật tư";
                ws.Cells[8, 5, 8, 5].Value = "Tên nhà cung cấp";
                ws.Cells[8, 6, 8, 6].Value = "Lô sản xuất vật tư";
                ws.Cells[8, 7, 8, 7].Value = "Ngày xuất kho";
                ws.Cells[8, 1, 8, 7].Style.Fill.SetBackground(Color.DarkGray);

                int Count = 9;
                DataTable dtM = BusinessRulesLocator.Conllection().GetAllList(@" Select WE.Name,WE.WarehouseExport_ID, WH.Name as NameWareHouse,M.Name as NameMaterial,M.CodePrivate,S.Name as NameNCC,WI.CodeMaterialPackage,WE.CreateDate from WarehouseExportMaterial WM left join WarehouseExport WE on WM.WarehouseExport_ID = WE.WarehouseExport_ID 
  left join WarehouseImport WI on WM.WarehouseImport_ID = WI.WarehouseImport_ID   
 left join Warehouse WH on WI.Warehouse_ID = WH.Warehouse_ID 
 left join Material M on WM.Material_ID = M.Material_ID
  left join Supplier S on S.Supplier_ID = WI.Supplier_ID
 where M.Active=1 and WE.Active=1 and WM.WarehouseExportMaterial_ID in (Select WarehouseExportMaterial_ID from ProductPackageVsMaterial where ProductPackage_ID =" + ProductPackage_ID + ")");
                if (dtM.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in dtM.Rows)
                    {
                        ws.Cells[Count, 1, Count, 1].Value = dtRow["NameWareHouse"].ToString();
                        ws.Cells[Count, 2, Count, 2].Value = dtRow["Name"].ToString();
                        ws.Cells[Count, 3, Count, 3].Value = dtRow["NameMaterial"].ToString();
                        ws.Cells[Count, 4, Count, 4].Value = dtRow["CodePrivate"].ToString();
                        ws.Cells[Count, 5, Count, 5].Value = dtRow["NameNCC"].ToString();
                        ws.Cells[Count, 6, Count, 6].Value = dtRow["CodeMaterialPackage"].ToString();
                        ws.Cells[Count, 7, Count, 7].Value = DateTime.Parse(dtRow["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                        Count++;
                    }
                }


                string filename = "TestExcel";

                filename = "bao-cao-vat-tu-" + Common.ConvertTitleDomain(DateTime.Now.ToString("dd/MM/yyyy")) + ".xlsx";

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
    //        int TongTien = 0;
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

    //                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select CONVERT( varchar,T.StartDate,103) as StartDate, T.Name , CONVERT (varchar, CONVERT(int,Quantity))+' '+M.Unit as Quantity ,CONVERT(int,T.Price) as Price ,  CONVERT(int, ISNULL(T.Quantity,0) * ISNULL(T.Price,0)) as total ,ISNULL( M.IsolationDay,0) as IsolationDay, ISNULL( U.UserName,N'Không xác định') as Account  , TS.Name as Status from Task T 
    //left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
    //left join aspnet_Users U on U.UserId= T.CreateBy
    //left join Material M on M.Material_ID= T.Material_ID
    //where T.TaskType_ID=2 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");

    //                string attachment = "attachment; filename= file_excle_nhat_ky_san_xuat" + ProductBrand_ID + ".xls";
    //                Response.ClearContent();
    //                Response.AddHeader("content-disposition", attachment);
    //                Response.ContentEncoding = System.Text.Encoding.Unicode;
    //                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
    //                Response.ContentType = "application/vnd.ms-excel";
    //                string tab = "NHẬT KÝ THEO DÕI QUÁ TRÌNH SẢN XUẤT \t Lô/Mẻ: " + SGTIN + "  \n (Nhật ký vật tư)  \t Ngày áp dụng: " + StartDate + "\n";
    //                tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
    //                tab += "Sản Phẩm: " + NameProduct + "\n";
    //                tab += "Tiêu chuẩn: " + NameQuality + "\n";
    //                tab += "Lô: " + NamePackge + "\n \n";
    //                tab += "1.Vị trí lô trồng: " + ProductBrandAddress + "\t" + "2.Diện tích trồng: " + Acreage + "(m2)\t" + "3.Ngày trồng: " + StartDate + "\t" + "4.Ngày dự kiến thu hoạch: " + EndtDate + "\n\n";
    //                tab += "Tổng: " + dt.Rows.Count + "\n \n";
    //                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
    //                foreach (DataColumn dc in dt.Columns)
    //                {
    //                    Response.Write(tab + dc.ColumnName.Replace("Name", "Vật tư sử dụng").Replace("Account", "Người thực hiện").Replace("StartDate", "Ngày thực hiện").Replace("Status", "Trạng thái").Replace("Quantity", "Số lượng").Replace("Price", "Đơn giá").Replace("total", " Thành tiền").Replace("IsolationDay", "Thời gian cách ly"));
    //                    tab = "\t";
    //                }
    //                Response.Write("\n");
    //                int i;
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    TongTien += Convert.ToInt32(dr["total"]);
    //                    tab = "";
    //                    for (i = 0; i < dt.Columns.Count; i++)
    //                    {
    //                        if (i == 6)
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

    //                    //Response.Write("\n\t\t\t" + "Tổng: \t" + TongTien);
    //                    //Response.Write("\t" + TongTien);
    //                    Response.Write("\n");
    //                }
    //                Response.Write("Tổng \t\t\t\t" + TongTien);
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