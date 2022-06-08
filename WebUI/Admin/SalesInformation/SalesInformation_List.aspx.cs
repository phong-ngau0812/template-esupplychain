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

public partial class SalesInformation_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {

            LoadInfo();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void LoadInfo()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  select SI.SalesInformation_ID, SI.Barcode, SI.CustomerName, w.Name from SalesInformation SI
  inner join Workshop w on w.Workshop_ID = Si.Workshop_ID where SI.Active <>-1 and SI.ProductBrand_ID= "+MyUser.GetProductBrand_ID());
            rptTnfo.DataSource = dt;
            rptTnfo.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesInformation_Add.aspx", false);
    }

  
    protected void rptTnfo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ChainLink_ID = Convert.ToInt32(e.CommandArgument);
        SalesInformationRow _SalesInformationRow = new SalesInformationRow();
        _SalesInformationRow = BusinessRulesLocator.GetSalesInformationBO().GetByPrimaryKey(ChainLink_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_SalesInformationRow != null)
                {
                    _SalesInformationRow.Active = -1;
                    MyActionPermission.WriteLogSystem(ChainLink_ID, "Xóa - " + _SalesInformationRow.SalesInformation_ID);
                    BusinessRulesLocator.GetSalesInformationBO().Update(_SalesInformationRow);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            

        }
        lblMessage.Visible = true; ;
        LoadInfo();
    }

  
//    protected void ExportFile()
//    {

//        string ASProductBrandName = "DANH SÁCH CHUỖI LIÊN KẾT";
//        DataTable dt = new DataTable();
//        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select CL.Name , CL.Summary
//from ChainLink CL
//where Active = 1 ");
//        string attachment = "attachment; filename= Danh sach chi nhanh " + ".xls";
//        Response.ClearContent();
//        Response.AddHeader("content-disposition", attachment);
//        Response.ContentEncoding = System.Text.Encoding.Unicode;
//        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
//        Response.ContentType = "application/vnd.ms-excel";
//        string tab = ASProductBrandName + "\n";
//        tab += "(Tổng: " + dt.Rows.Count + ")\n\n";

//        //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
//        foreach (DataColumn dc in dt.Columns)
//        {
//            Response.Write(tab + dc.ColumnName.Replace("Name", "Chuỗi liên kết ").Replace("Summary", "Mô tả ngắn "));

//            tab = "\t";
//        }
//        Response.Write("\n");
//        int i;
//        foreach (DataRow dr in dt.Rows)
//        {
//            tab = "";
//            for (i = 0; i < dt.Columns.Count; i++)
//            {
//                Response.Write(tab + dr[i].ToString());

//                tab = "\t";
//            }

//            Response.Write("\n");
//        }
//        Response.End();

//    }

//    protected void btnExportFile_Click(object sender, EventArgs e)
//    {
//        ExportFile();
//    }
}