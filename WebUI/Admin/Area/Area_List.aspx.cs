using DbObj;
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

public partial class Area_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlddlZone();
            LoadArea();

        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void FillDDLddlProductBrand()
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

    private void FillDDLddlddlZone()
    {
        string where = "";

        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select Z.* from Zone Z inner join ProductBrand PB on Z.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and Z.Active<>-1  " + where);
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng sản xuất --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    public string ReturnCount(int Area_ID)
    {
        int count = 0;
        AreaRow _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);
        if (!_AreaRow.IsWorkshop_IDNull)
        {

            string[] array = _AreaRow.Workshop_ID.Split(',');
            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    count++;
                }
            }
        }

        return count.ToString();

    }

    protected void LoadArea()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and A.Zone_ID =" + ddlZone.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select A.*,isnull((Select COUNT(*)  from Workshop W where W.Area_ID = A.Area_ID),0) as Soluong, PB.Name as ProductBrandName , Z.Name as ZoneName  from Area A
inner join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
left join Zone Z on A.Zone_ID = Z.Zone_ID
where PB.Active=1  " + where + " order by CreateDate DESC");
            rptArea.DataSource = dt;
            rptArea.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Area_Add.aspx", false);
    }


    protected void rptArea_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Area_ID = Convert.ToInt32(e.CommandArgument);
        AreaRow _AreaRow = new AreaRow();
        _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteArea(Area_ID, ref Message))
                {
                    MyActionPermission.WriteLogSystem(Area_ID, "Xóa - " + _AreaRow.Name);
                    BusinessRulesLocator.GetAreaBO().DeleteByPrimaryKey(Area_ID);
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
        LoadArea();
    }

    protected void rptArea_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "0")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    //lblText.Text = "<span class=\"badge badge-soft-danger\">Ngừng kích hoạt</span>";
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else

                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                    //lblText.Text = "<span class=\"badge badge-soft-success\">Đã kích hoạt</span>";
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                }
            }
        }
    }
    protected void ExportFileNumberWorkshop()
    {
        DataTable dtAreaParent = new DataTable();
        dtAreaParent.Clear();
        dtAreaParent.Columns.Add("ZoneName");
        //dtAreaParent.Columns.Add("Area_ID");
        dtAreaParent.Columns.Add("AreaName");
        dtAreaParent.Columns.Add("Acreage");
        dtAreaParent.Columns.Add("NumberWorkshop");


        string where = "";
        string ASProductBrandName = "";
        if (ddlProductBrand.SelectedValue != "0")
        {

            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH KHU VỰC SẢN XUẤT CỦA DOANH NGHIỆP \n ";
            ASProductBrandName += "Doanh nghiệp: " + ddlProductBrand.SelectedItem.ToString() + "\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH KHU VỰC SẢN XUẤT TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }
        if (ddlZone.SelectedValue != "0")
        {
            where += " and A.Zone_ID =" + ddlZone.SelectedValue;
        }
        if (MyUser.GetFunctionGroup_ID() == "8")
        {
            where += " and  PB.ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID =" + MyUser.GetLocation_ID() + ")";
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select A.Area_ID, Z.Name as ZoneName , A.Name as AreaName , A.Acreage,PB.Name as ProductBrandN,,isnull((Select COUNT(*)  from Workshop W where W.Area_ID = A.Area_ID),0) as Soluong from Area A
inner join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
left join Zone Z on A.Zone_ID = Z.Zone_ID
where PB.Active=1   " + where + " order by A.Name ASC");

        foreach (DataRow item in dt.Rows)
        {
            DataRow itemAreaParent = dtAreaParent.NewRow();
            itemAreaParent["ZoneName"] = item["ZoneName"];
            //itemAreaParent["Area_ID"] = item["Area_ID"];
            itemAreaParent["AreaName"] = item["AreaName"];
            itemAreaParent["Acreage"] = item["Acreage"];
            itemAreaParent["NumberWorkshop"] = /*ReturnCount(Convert.ToInt32(item["Area_ID"]))*/item["Soluong"];
            dtAreaParent.Rows.Add(itemAreaParent);

        }
        string attachment = "attachment; filename= file_excle_Tong_so_Khu_vuc_san_xuat " + ddlProductBrand.SelectedValue + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = ASProductBrandName + "\n";

        foreach (DataColumn dc in dtAreaParent.Columns)
        {
            Response.Write(tab + dc.ColumnName.Replace("ZoneName", "Tên Vùng sản xuất").Replace("AreaName", "Tên Khu sản xuất").Replace("Acreage", "Diện tích (m2) ").Replace("NumberWorkshop", "Số nhân viên ").Replace("ProductBrandN", "Doanh nghiệp"));

            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dtAreaParent.Rows)
        {
            tab = "";
            for (i = 0; i < dtAreaParent.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }


    protected void ExportFile()
    {

        int ProductBrand_ID = 0;
        string where = "";
        string ASProductBrandName = "";

        DataTable dtAreaParent = new DataTable();
        dtAreaParent.Clear();
        dtAreaParent.Columns.Add("RowNum");
        dtAreaParent.Columns.Add("ZoneName");
        dtAreaParent.Columns.Add("AreaName");
        dtAreaParent.Columns.Add("Acreage");
        dtAreaParent.Columns.Add("NumberWorkshop");

        if (ddlProductBrand.SelectedValue != "0")
        {
            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ddlProductBrand.SelectedValue));
            if (_ProductBrandRow != null)
            {
                ProductBrand_ID = _ProductBrandRow.ProductBrand_ID;
            }
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "SÁCH KHU VỰC SẢN XUẤT CỦA DOANH NGHIỆP \n ";
        }
        else
        {
            ASProductBrandName += "SÁCH KHU VỰC SẢN XUẤT TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }
        if (ddlZone.SelectedValue != "0")
        {
            where += " and A.Zone_ID =" + ddlZone.SelectedValue;
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select ROW_NUMBER() OVER (ORDER BY A.Name) AS RowNum,A.Area_ID,  Z.Name as ZoneName , A.Name as AreaName , A.Acreage from Area A
inner join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
left join Zone Z on A.Zone_ID = Z.Zone_ID
where PB.Active=1  " + where + " order by A.Name ASC");

        foreach (DataRow item in dt.Rows)
        {
            DataRow itemAreaParent = dtAreaParent.NewRow();
            itemAreaParent["RowNum"] = item["RowNum"];
            itemAreaParent["ZoneName"] = item["ZoneName"];
            itemAreaParent["AreaName"] = item["AreaName"];
            itemAreaParent["Acreage"] = item["Acreage"];
            itemAreaParent["NumberWorkshop"] = ReturnCount(Convert.ToInt32(item["Area_ID"]));
            dtAreaParent.Rows.Add(itemAreaParent);

        }
        dtAreaParent.Columns["RowNum"].ColumnName = "STT";
        dtAreaParent.Columns["ZoneName"].ColumnName = "Tên Vùng sản xuất";
        dtAreaParent.Columns["AreaName"].ColumnName = "Tên Khu sản xuất";
        dtAreaParent.Columns["Acreage"].ColumnName = "Diện tích (m2)";
        dtAreaParent.Columns["NumberWorkshop"].ColumnName = "Số nhân viên";
        dtAreaParent.AcceptChanges();

        string tab = ASProductBrandName + "<br>";
        tab += "(Tổng: " + dt.Rows.Count + ")<br><br>";
        GridView gvDetails = new GridView();

        gvDetails.DataSource = dtAreaParent;

        gvDetails.AllowPaging = false;

        gvDetails.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Khu-vuc-san-xuat.xls"));
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
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
    }

    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        //ExportFileNumberWorkshop();
        ExportFile();
    }
}