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

public partial class QRCodePackageReport_List : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {

            FillProductBrand();
            FillProduct();
            LoadStatus();
            LoadQRCodeReport();
        }
   

        ResetMsg();
    }
   
    public string Message = "";


    private void FillProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
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


    private void FillProduct()
    {
        string where = "";
        string Index = "-1";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
           // Index = "-" + ddlProductBrand.SelectedValue;
        }

        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select Name, Product_ID from Product where Active = 1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", "0"));
            ddlProduct.Items.Insert(1, new ListItem("# Sản phẩm chưa xác định #", Index));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
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

    private void LoadQRCodeReport()
    {
        try
        {
        
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().spGetQRCodeReport(Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue) , Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);
            rptQRCodeReport.DataSource = dt;
            rptQRCodeReport.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
   
    

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            LoadQRCodeReport();
        }

    }



    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQRCodeReport();
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQRCodeReport();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQRCodeReport();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQRCodeReport();
    }


    protected void ExportFile()
    {

        string where = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG SỐ LƯỢNG TEM DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG SỐ LƯỢNG TEM \n ";
        }
        ASProductBrandName += "Thời gian từ: " + DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") + "-" + DateTime.Parse(ctlDatePicker1.ToDate.ToString()).ToString("dd/MM/yyyy") + "\n";
        // where += "and A.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate + "'";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().spGetQRCodeReport(Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate);
        string attachment = "attachment; filename= Tong-so-luong-tem" + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = ASProductBrandName + "\n";
        //tab += "(Tổng: " + dt.Rows.Count + ")\n\n";

        //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName.Replace("T1", "Tháng 1 ").Replace("T2", "Tháng 2").Replace("T3", "Tháng 3").Replace("T4", "Tháng 4").Replace("T5", "Tháng 5").Replace("Tháng 6", "Tháng 6").Replace("T7", "Tháng 7 ").Replace("T8", "Tháng 8").Replace("T9", "Tháng 9").Replace("T10", "Tháng 10").Replace("T11", "Tháng 11").Replace("T12", "Tháng 12"));

            tab = "\t";
        }
        Response.Write("\n");
        int i;
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
        Response.End();

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFile();
    }
}