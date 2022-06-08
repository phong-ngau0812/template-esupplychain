using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Task_List_Selling_List : System.Web.UI.Page
{
   public int ProductPackage_ID = 0;
    int ProductBrand_ID = 0;
    public string name,code = string.Empty;
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
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,P.Code, P.SGTIN,P.Name as PackageName, Q.Name as Quality ,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , U.UserName, T.Name  from Task T 
left join aspnet_Users U on U.UserId= T.CreateBy
left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
left join ProductPackage P on P.ProductPackage_ID= T.ProductPackage_ID
left join Product PR on P.Product_ID= PR.Product_ID
left join Quality Q on Q.Quality_ID= PR.Quality_ID
where T.TaskType_ID=6 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");
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
        Response.Redirect("Task_List_Selling_Add?ProductPackage_ID=" + ProductPackage_ID, false);
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int TongSp = 0;
        int tongtien = 0;
       
        string tendoanhnghiep = "";
        int ProductBrand_ID = 0;

        if (ddlProductBrand.SelectedValue != "0")
        {
            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ddlProductBrand.SelectedValue));
            if (_ProductBrandRow != null)
            {
                ProductBrand_ID = _ProductBrandRow.ProductBrand_ID;
                tendoanhnghiep = _ProductBrandRow.Name;

            }

            string NamePackge = "";
            string NameProduct = "";
            string NameQuality = "";
            string SGTIN = "";
            string StartDate = "";
            string EndtDate = "";
            string ProductBrandAddress = "";
            string Acreage = "";
            if (ddlProductPackage.SelectedValue != "0")
            {
                DataTable dtPP = BusinessRulesLocator.Conllection().GetAllList(@"select distinct  PR.Name as NameProduct, P.Name as PackageName,P.Code,P.SGTIN,CONVERT( varchar,P.StartDate,103) as StartDate , CONVERT( varchar,P.EndDate,103) as EndDate, PB.Address as ProductBrandAddress,  ISNULL( P.Acreage,'0')as Acreage ,  ISNULL( Q.Name,N'Tự công bố') as Quality  from ProductPackage P
left join Product PR on P.Product_ID= PR.Product_ID
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
                }

                DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select CONVERT( varchar,T.StartDate,103) as StartDate, T.Name, ISNULL( U.UserName,N'Không xác định') as Account, T.BuyerName as Buyer    ,convert(int,ISNUll( (select 100 - (SumMoney /(Quantity*Price) *100 )
from Task  
where TaskType_ID= 6 and ProductPackage_ID = " + ddlProductPackage.SelectedValue + " and Quantity <> 0 and Price <> 0),0)) as Discount , CONVERT(int ,T.Price) as Price  ,CONVERT(int,T.Quantity ) as Quantity, CONVERT(int,T.SumMoney)as SumMoney " +
"from Task T " +
"left join aspnet_Users U on U.UserId= T.CreateBy " +
"left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID" +
" where T.TaskType_ID=6 and T.ProductPackage_ID =" + ddlProductPackage.SelectedValue + " order by StartDate DESC");

                string attachment = "attachment; filename= file_excle_nhat_ky_san_xuat" + ProductBrand_ID + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "NHẬT KÝ THEO DÕI QUÁ TRÌNH SẢN XUẤT \t Lô/Mẻ: " + SGTIN + " \n (Nhật ký bán hàng)  \t Ngày áp dụng: " + StartDate + "\n";
                //tab +=    "\t Lô/Mẻ: " + SGTIN + "  \n  \t Ngày áp dụng: " + StartDate + "\n";
                tab += "Doanh nghiệp: " + tendoanhnghiep + "\n";
                tab += "Sản Phẩm: " + NameProduct + "\n";
                tab += "Tiêu chuẩn: " + NameQuality + "\n";
                tab += "Lô: " + NamePackge + "\n \n";
                tab += "1.Vị trí lô trồng: " + ProductBrandAddress + "\t" + "2.Diện tích trồng: " + Acreage + "(m2)\t" + "3.Ngày trồng: " + StartDate + "\t" + "4.Ngày dự kiến thu hoạch: " + EndtDate + "\n\n";
                tab += "Tổng: " + dt.Rows.Count + "\n \n";
                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName.Replace("Name", "Đề mục công việc").Replace("Account", "Người thực hiện").Replace("StartDate", "Ngày thực hiện").Replace("Buyer", "Người mua").Replace("Quantity", " Số lượng").Replace("Price", "Giá tiền").Replace("SumMoney", "Thành tiền").Replace("Discount", " Phần trăm chiết khấu (%)"));
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
               
                foreach (DataRow dr in dt.Rows)
                {
                    TongSp += Convert.ToInt32(dr["Quantity"]);
                    tongtien += Convert.ToInt32(dr["SumMoney"]);

                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 2)
                        {
                            if (!string.IsNullOrEmpty(dr[i].ToString()))
                            {

                                Response.Write(tab + dr[i].ToString() + "(" + new MyUser().FullNameFromUserName(dr[i].ToString()) + ")");
                            }
                            else
                            {
                                Response.Write(tab + "NULL");
                            }
                            //Response.Write(tab + dr[i].ToString());
                            //tab = "\t";
                        }
                        else
                        {
                           
                            Response.Write(tab + dr[i].ToString());
                            tab = "\t";
                        }
                    }
                    Response.Write("\n");
                }
                Response.Write("\n\t\t\t\t\t " + "Tổng:\t" + TongSp +" \t" + tongtien);
                //Response.Write("\t" + TongTien);
                Response.Write("\n");
                Response.End();
            }
        }
        else
        {
            lblMessage.Text = "Bạn chưa chọn doanh nghiệp ";
            lblMessage.ForeColor = Color.Red;
            lblMessage.BackColor = Color.Wheat;
            lblMessage.Visible = true;
        }
    }
}