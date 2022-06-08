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

public partial class Task_List_Manufacturing_List : System.Web.UI.Page
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
            //LoadProductPackageCopy();
            //GetTaskHistory(ProductPackage_ID);
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
            if (_ProductPackageRow.ProductPackageStatus_ID == 3)
            {
                btnAdd.Visible = false;
            }
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
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select * From ProductPackage where 1=1 and ProductPackageStatus_ID<>6" + where + " Order by ProductPackage_ID DESC");
            //ddlProductPackage.DataSource = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "ProductPackage_ID DESC");
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
    //protected void LoadProductPackageCopy()
    //{
    //    try
    //    {
    //        string where = string.Empty;
    //        where += " ProductPackage_ID in (select distinct ProductPackage_ID from ProductPackageExport)";
    //        ddlProductPackageCopy.DataSource = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "ProductPackage_ID DESC");
    //        ddlProductPackageCopy.DataTextField = "Name";
    //        ddlProductPackageCopy.DataValueField = "ProductPackage_ID";
    //        ddlProductPackageCopy.DataBind();
    //        ddlProductPackageCopy.Items.Insert(0, new ListItem("-- Chọn lô --", "0"));
    //    }
    //    catch (Exception ex)
    //    {

    //        Log.writeLog("LoadUser", ex.ToString());
    //    }
    //}
    protected void LoadData()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"    select DM.Name as NameDepartment, TS.Name as StatusName,P.Name as PackageName,P.Code, P.SGTIN, Q.Name as Quality ,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
left join ProductPackage P on P.ProductPackage_ID= T.ProductPackage_ID
left join Product PR on P.Product_ID= PR.Product_ID
left join TaskStep TSP on T.TaskStep_ID =TSP.TaskStep_ID
left join Department DM on TSP.Department_ID =DM.Department_ID
left join Quality Q on Q.Quality_ID= PR.Quality_ID
              where T.TaskType_ID=1 and T.ProductPackage_ID=" + ddlProductPackage.SelectedValue + "  order by StartDate DESC");
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
        Response.Redirect("Task_List_Manufacturing_Add?ProductPackage_ID=" + ProductPackage_ID, false);
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
    //  protected DataTable GetTaskCopy(int ProductPackage_ID)
    //  {
    //      DataTable dt = new DataTable();
    //      dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
    //            left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
    //            left join aspnet_Users U on U.UserId= T.CreateBy
    //            where T.TaskType_ID=1 and T.ProductPackage_ID=" + ProductPackage_ID + " order by StartDate DESC");
    //      return dt;
    //  }
    //  protected void GetTaskHistory(int ProductPackage_ID)
    //  {
    //      DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select distinct T.ProductPackage_ID,T.ProductBrand_ID, T.ProductPackageName, P.Name from Task T
    //left join ProductBrand P on P.ProductBrand_ID = T.ProductBrand_ID
    //where T.Task_ID  in (select Task_ID from TaskHistory where ProductPackage_ID=" + ProductPackage_ID + ")");
    //      rptPackage.DataSource = dt;
    //      rptPackage.DataBind();
    //      //DataTable dtHistory = new DataTable();
    //      //dtHistory = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
    //      //      left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
    //      //      left join aspnet_Users U on U.UserId= T.CreateBy
    //      //      where T.TaskType_ID=1 and T.Task_ID in (select Task_ID from TaskHistory where ProductPackage_ID=" + ProductPackage_ID + ") order by StartDate DESC");
    //      //if (dtHistory.Rows.Count > 0)
    //      //{
    //      //    rptTaskHistory.DataSource = dtHistory;
    //      //    rptTaskHistory.DataBind();
    //      //    btnDeleteHistory.Visible = true;
    //      //}
    //      //else
    //      //{
    //      //    rptTaskHistory.DataSource = null;
    //      //    rptTaskHistory.DataBind();
    //      //    btnDeleteHistory.Visible = false;
    //      //}
    //  }
    //  protected void ddlProductPackageCopy_SelectedIndexChanged(object sender, EventArgs e)
    //  {
    //      try
    //      {
    //          if (ddlProductPackageCopy.SelectedValue != "0")
    //          {
    //              ProductPackageRow _ProductPackageRow = new ProductPackageRow();
    //              _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackageCopy.SelectedValue));
    //              if (_ProductPackageRow != null)
    //              {
    //                  ProductBrandRow _ProductBrandRow = new ProductBrandRow();
    //                  _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRow.ProductBrand_ID);
    //                  if (_ProductBrandRow != null)
    //                  {
    //                      lblInfo.Text = "Nhật ký sản xuất lô: " + _ProductPackageRow.Name + " của " + _ProductBrandRow.Name;
    //                  }
    //                  DataTable dt = GetTaskCopy(Convert.ToInt32(ddlProductPackageCopy.SelectedValue));
    //                  if (dt.Rows.Count > 0)
    //                  {
    //                      data.Visible = true;
    //                      rptTaskCopy.DataSource = dt;
    //                      rptTaskCopy.DataBind();
    //                  }
    //                  else
    //                  {
    //                      rptTaskCopy.DataSource = null;
    //                      rptTaskCopy.DataBind();
    //                  }

    //              }
    //          }
    //          else
    //          {
    //              data.Visible = false;
    //              rptTaskCopy.DataSource = null;
    //              rptTaskCopy.DataBind();
    //              lblInfo.Text = "";
    //          }
    //      }
    //      catch (Exception ex)
    //      {
    //          Log.writeLog("ddlProductPackageCopy_SelectedIndexChanged", ex.ToString());
    //      }
    //  }

    //  protected void btnCopy_Click(object sender, EventArgs e)
    //  {
    //      try
    //      {
    //          DataTable dt = GetTaskCopy(Convert.ToInt32(ddlProductPackageCopy.SelectedValue));
    //          foreach (DataRow item in dt.Rows)
    //          {
    //              TaskHistoryRow _TaskRow = new TaskHistoryRow();
    //              _TaskRow.ProductPackage_ID = ProductPackage_ID;
    //              _TaskRow.Task_ID = Convert.ToInt32(item["Task_ID"]);
    //              _TaskRow.CreateBy = MyUser.GetUser_ID();
    //              _TaskRow.CreateDate = DateTime.Now;
    //              _TaskRow.Active = 1;
    //              if (!BusinessRulesLocator.GetTaskHistoryBO().Exist(ProductPackage_ID, Convert.ToInt32(item["Task_ID"])))
    //              {
    //                  BusinessRulesLocator.GetTaskHistoryBO().Insert(_TaskRow);
    //              }
    //              lblMessage.Text = "Copy nhật ký thành công";
    //              lblMessage.Visible = true;
    //          }
    //          GetTaskHistory(ProductPackage_ID);

    //      }
    //      catch (Exception ex)
    //      {
    //          Log.writeLog("btnCopy_Click", ex.ToString());
    //      }

    //  }

    //  protected void btnDeleteHistory_Click(object sender, EventArgs e)
    //  {
    //      BusinessRulesLocator.GetTaskHistoryBO().Delete("ProductPackage_ID =" + ProductPackage_ID);
    //      lblMessage.Text = "Xóa nhật ký thành công";
    //      lblMessage.Visible = true;
    //      GetTaskHistory(ProductPackage_ID);
    //  }

    protected void rptPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Label lblXuanHoa = e.Item.FindControl("lblXuanHoa") as Label;
            Repeater rptTaskHistory = e.Item.FindControl("rptTaskHistory") as Repeater;

            //Xuân Hòa
            if (Common.CheckUserXuanHoa1())
            {
                lblXuanHoa.Visible = true;
            }
            if (lblProductPackage_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
              where T.TaskType_ID=1 and T.Task_ID in (select Task_ID from Task where ProductPackage_ID=" + lblProductPackage_ID.Text + ") and T.Task_ID in (select Task_ID from TaskHistory where ProductPackage_ID=" + ProductPackage_ID + ") order by StartDate DESC");
                if (dt.Rows.Count > 0)
                {
                    rptTaskHistory.DataSource = dt;
                    rptTaskHistory.DataBind();
                }
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
                ws.Cells[13, 1].Value = "I. Phân xưởng sản xuất";

                ws.Column(1).Width = 10;
                ws.Column(2).Width = 40;
                ws.Column(3).Width = 40;
                ws.Column(4).Width = 45;
                ws.Column(5).Width = 20;
                ws.Column(6).Width = 40;
                ws.Column(7).Width = 40;

                int Count = 14;


                DataTable dt1 = BusinessRulesLocator.Conllection().GetAllList("select T.TaskStep_ID,T.Name,T.Sort,D.Name as NameDepartment from TaskStep T left join Department D  on T.Department_ID = D.Department_ID where Product_ID = (Select Product_ID from ProductPackage where ProductPackage_ID = " + ProductPackage_ID + ")   order by T.Sort asc");
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in dt1.Rows)
                    {
                        ws.Cells[Count, 1, Count, 3].Merge = true;
                        ws.Cells[Count, 4, Count, 5].Merge = true;
                        ws.Cells[Count, 1].Style.Font.Bold = true;
                        ws.Cells[Count, 1].Style.Font.Italic = true;
                        ws.Cells[Count, 1].Value = dtRow["Sort"].ToString() + ". " + dtRow["Name"].ToString() + "( " + dtRow["NameDepartment"].ToString() + ")";
                        ws.Cells[Count, 1, Count, 3].Style.Fill.SetBackground(Color.LightGreen);


                        Count += 1;

                        ws.Cells[Count, 1, Count, 1].Value = "Lot";
                        ws.Cells[Count, 2, Count, 2].Value = "Số lượng";
                        ws.Cells[Count, 3, Count, 3].Value = "Bộ phận tham gia sản xuất";
                        ws.Cells[Count, 4, Count, 4].Value = "Ngày bắt đầu";
                        ws.Cells[Count, 5, Count, 5].Value = "Ngày kết thúc";
                        ws.Cells[Count, 6, Count, 6].Value = "Ngày giao chuyển";
                        ws.Cells[Count, 7, Count, 7].Value = "Biên bản lỗi số ... (Nếu có) ";
                        ws.Cells[Count, 1, Count, 7].Style.Fill.SetBackground(Color.DarkGray);

                        Count += 1;
                        if (dtRow["TaskStep_ID"].ToString() != "")
                        {
                            DataTable dt2 = BusinessRulesLocator.Conllection().GetAllList(@"select DM.Name as NameDepartment,P.ItemCount,P.Product_ID, TS.Name as StatusName,P.Name as PackageName,P.Code, P.SGTIN, Q.Name as Quality ,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName, T.TransportDateStart,T.TransportDateEnd from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID = TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId = T.CreateBy
left join ProductPackage P on P.ProductPackage_ID = T.ProductPackage_ID
left join Product PR on P.Product_ID = PR.Product_ID
left join TaskStep TSP on T.TaskStep_ID = TSP.TaskStep_ID
left join Department DM on TSP.Department_ID = DM.Department_ID
left join Quality Q on Q.Quality_ID = PR.Quality_ID
              where T.TaskType_ID = 1 and T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskStep_ID = " + dtRow["TaskStep_ID"].ToString());

                            if (dt2.Rows.Count > 0)
                            {
                                int Flag = 1;
                                foreach (DataRow dtRow1 in dt2.Rows)
                                {
                                    ws.Cells[Count, 1, Count, 1].Value = "Lot " + Flag;
                                    ws.Cells[Count, 2, Count, 2].Value = dtRow1["ItemCount"].ToString();
                                    ws.Cells[Count, 3, Count, 3].Value = dtRow1["NameDepartment"].ToString();
                                    ws.Cells[Count, 4, Count, 4].Value = DateTime.Parse(dtRow1["StartDate"].ToString()).ToString("dd/MM/yyyy");
                                    ws.Cells[Count, 5, Count, 5].Value = DateTime.Parse(dtRow1["EndDate"].ToString()).ToString("dd/MM/yyyy");
                                    ws.Cells[Count, 6, Count, 6].Value = "từ " + DateTime.Parse(dtRow1["TransportDateStart"].ToString()).ToString("dd/MM/yyyy") + " đến " + DateTime.Parse(dtRow1["TransportDateEnd"].ToString()).ToString("dd/MM/yyyy");
                                    ws.Cells[Count, 7, Count, 7].Value = "";
                                    Count++;
                                    Flag++;
                                }

                            }

                        }
                        Count += 3;

                    }

                }


                string filename = "TestExcel";

                filename = "bao-cao-san-xuat-" + Common.ConvertTitleDomain(DateTime.Now.ToString("dd/MM/yyyy")) + ".xlsx";

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
    //                    NamePackge += dtPP.Rows[0]["PackageName"].ToString() ;
    //                    NameProduct += dtPP.Rows[0]["NameProduct"].ToString();
    //                    NameQuality += dtPP.Rows[0]["Quality"].ToString();
    //                    SGTIN += dtPP.Rows[0]["Code"].ToString() + "|" + dtPP.Rows[0]["SGTIN"].ToString();
    //                    StartDate += dtPP.Rows[0]["StartDate"].ToString();
    //                    EndtDate += dtPP.Rows[0]["EndDate"].ToString();
    //                    ProductBrandAddress += dtPP.Rows[0]["ProductBrandAddress"].ToString();
    //                    Acreage += dtPP.Rows[0]["Acreage"].ToString();
    //                }

    //                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select CONVERT( varchar,T.StartDate,103) as StartDate, T.Name,ISNULL( U.UserName,N'Không xác định') as Account,  TS.Name as Status from Task T 
    //left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
    //left join aspnet_Users U on U.UserId= T.CreateBy
    //where T.TaskType_ID=1 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");

    //                string attachment = "attachment; filename= file_excle_nhat_ky_san_xuat" + ProductBrand_ID + ".xls";
    //                Response.ClearContent();
    //                Response.AddHeader("content-disposition", attachment);
    //                Response.ContentEncoding = System.Text.Encoding.Unicode;
    //                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
    //                Response.ContentType = "application/vnd.ms-excel";
    //                string tab = "NHẬT KÝ THEO DÕI QUÁ TRÌNH SẢN XUẤT \t Lô/Mẻ: "+ SGTIN + "  \n(Nhật ký sản xuất)  \t Ngày áp dụng: "+ StartDate +"\n";
    //                tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
    //                tab += "Sản Phẩm: " + NameProduct + "\n";
    //                tab += "Tiêu chuẩn: " + NameQuality + "\n";
    //                tab += "Lô: " + NamePackge + "\n \n";
    //                tab += "1.Vị trí lô trồng: " + ProductBrandAddress + "\t" +"2.Diện tích trồng: "+ Acreage + "(m2)\t" + "3.Ngày trồng: " + StartDate + "\t" + "4.Ngày dự kiến thu hoạch: " + EndtDate +"\n\n";
    //                tab += "Tổng: " + dt.Rows.Count + "\n \n";
    //                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
    //                foreach (DataColumn dc in dt.Columns)
    //                {
    //                    Response.Write(tab + dc.ColumnName.Replace("Name", "Đề mục công việc").Replace("Account", "Người thực hiện").Replace("StartDate", "Ngày thực hiện").Replace("Status", "Trạng thái"));
    //                    tab = "\t";
    //                }
    //                Response.Write("\n");
    //                int i;
    //                foreach (DataRow dr in dt.Rows)
    //                {

    //                    tab = "";
    //                    for (i = 0; i < dt.Columns.Count; i++)
    //                    {

    //                        if (i == 2)
    //                        {
    //                            if (!string.IsNullOrEmpty(dr[i].ToString()))
    //                            {

    //                                Response.Write(tab + dr[i].ToString() +"("+  new MyUser().FullNameFromUserName(dr[i].ToString()) +")");
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