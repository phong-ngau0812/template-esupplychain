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

public partial class Zone_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadZone();
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

    public string ReturnCount(int Zone_ID)
    {
        int count = 0;
        ZoneRow _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);
        if (!_ZoneRow.IsWorkshop_IDNull)
        {

            string[] array = _ZoneRow.Workshop_ID.Split(',');
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

    protected void LoadZone()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    where += " and Z.Zone_ID=" + MyUser.GetZone_ID();
                }
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select Z.*,isnull((Select COUNT(*)  from Workshop W where W.Zone_ID = Z.Zone_ID),0) as Soluong, isnull((Select SUM( PP.ExpectedProductivity)  from ProductPackage PP where PP.Zone_ID = Z.Zone_ID),0) as Sanluongdukien,  PB.Name as ProductBrandName  from Zone Z
inner join ProductBrand PB on Z.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and Z.Active <>-1 " + where + " ORDER BY CreateDate DESC ");

            rptZone.DataSource = dt;
            rptZone.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Zone_Add.aspx", false);
    }


    protected void rptZone_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Zone_ID = Convert.ToInt32(e.CommandArgument);
        ZoneRow _ZoneRow = new ZoneRow();
        _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (MyActionPermission.CanDeleteZone(Zone_ID, ref Message))
                {
                    if (_ZoneRow != null)
                    {
                        _ZoneRow.Active = -1;
                    }
                    BusinessRulesLocator.GetZoneBO().Update(_ZoneRow);
                    MyActionPermission.WriteLogSystem(Zone_ID, "Xóa - " + _ZoneRow.Name);
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
        LoadZone();
    }

    protected void rptZone_ItemDataBound(object sender, RepeaterItemEventArgs e)
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



    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadZone();
    }




    protected void ExportFileNumberWorkshop()
    {
        DataTable dtAreaParent = new DataTable();
        dtAreaParent.Clear();
        dtAreaParent.Columns.Add("ZoneName");
        dtAreaParent.Columns.Add("Acreage");
        dtAreaParent.Columns.Add("NumberWorkshop");


        string where = "";
        string ASProductBrandName = "";
        if (ddlProductBrand.SelectedValue != "0")
        {

            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH VÙNG SẢN XUẤT CỦA DOANH NGHIỆP \n ";
            ASProductBrandName += "Doanh nghiệp: " + ddlProductBrand.SelectedItem.ToString() + "\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH VÙNG SẢN XUẤT TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }
        if (MyUser.GetFunctionGroup_ID() == "8")
        {
            where += " and  PB.ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID =" + MyUser.GetLocation_ID() + ")";
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select Z.Zone_ID, Z.Name as ZoneName,PB.Name as ProductBrandN, Z.Acreage,isnull((Select COUNT(*)  from Workshop W where W.Zone_ID = Z.Zone_ID),0) as Soluong  from Zone Z
inner join ProductBrand PB on Z.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and Z.Active <>-1  " + where + " order by Z.Name ASC");

        foreach (DataRow item in dt.Rows)
        {
            DataRow itemAreaParent = dtAreaParent.NewRow();
            itemAreaParent["ZoneName"] = item["ZoneName"];
            itemAreaParent["Acreage"] = item["Acreage"];
            itemAreaParent["NumberWorkshop"] = /*ReturnCount(Convert.ToInt32(item["Zone_ID"]))*/item["Soluong"];
            dtAreaParent.Rows.Add(itemAreaParent);

        }
        string attachment = "attachment; filename= file_excle_Tong_so_vung_san_xuat " + ddlProductBrand.SelectedValue + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = ASProductBrandName + "\n";
        tab += "(Tổng: " + dt.Rows.Count + ")\n\n";
        foreach (DataColumn dc in dtAreaParent.Columns)
        {
            Response.Write(tab + dc.ColumnName.Replace("ZoneName", "Tên Vùng sản xuất").Replace("ProductBrandN", "Doanh Nghiệp").Replace("Acreage", "Diện tích (m2) ").Replace("NumberWorkshop", "Số nhân viên "));

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


    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFileNumberWorkshop();
    }
}