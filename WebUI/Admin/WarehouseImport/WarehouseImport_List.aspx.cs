using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class WarehouseImport_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlWareHouse();
            FillDDLddlMaterial();
            FillDllProduct();
            FillddlZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            // FillDDLddlMaterialCategory();
            LoadWarehouseImport();
        }
        if (Common.CheckUserXuanHoa1())
        {
            xuanhoa.Visible = true;
            noxuanhoa.Visible = false;
            nodatexuanhoa.Visible = false;
        }
        else
        {
            xuanhoa.Visible = false;
            noxuanhoa.Visible = true;
            nodatexuanhoa.Visible = true;
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


    private void FillDDLddlMaterial()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMaterialBO().GetAsDataTable("Active = 1 and ProductBrand_ID=" + ddlProductBrand.SelectedValue, "");
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataTextField = "Name";
            ddlMaterial.DataValueField = "Material_ID";
            ddlMaterial.DataBind();
            ddlMaterial.Items.Insert(0, new ListItem("-- Chọn vật tư --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillDllProduct()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("Select Product_ID, Name from Product where Active=1" + where);
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDllProduct", ex.ToString()); ;
            
        }
    }
    private void FillDDLddlWareHouse()
    {
        try
        {
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "0")
            {
                string where = string.Empty;
                if (Common.GetFunctionGroupDN())
                {
                    if (MyUser.GetAccountType_ID() == "7")
                    {
                        where += " and CreateBy ='" + MyUser.GetUser_ID() + "'";
                    }

                }
                if (MyUser.GetWarehouse_ID() != "")
                {
                    where += " and W.Warehouse_ID in (" + MyUser.GetWarehouse_ID() + ")";
                }
                dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable(" ProductBrand_ID=" + ddlProductBrand.SelectedValue + where, "");
                ddlWareHouse.DataSource = dt;
                ddlWareHouse.DataTextField = "Name";
                ddlWareHouse.DataValueField = "Warehouse_ID";
                ddlWareHouse.DataBind();
                //if (MyUser.GetWarehouse_ID() != "")
                //{
                //    ddlWareHouse.SelectedValue = MyUser.GetWarehouse_ID();
                //    ddlWareHouse.Enabled = false;
                //}
            }
            ddlWareHouse.Items.Insert(0, new ListItem("-- Chọn kho --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLddlMaterialCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable("Active<>-1", " Sort ASC");
            ddlMaterialCategory.DataSource = dt;
            ddlMaterialCategory.DataTextField = "Name";
            ddlMaterialCategory.DataValueField = "MaterialCategory_ID";
            ddlMaterialCategory.DataBind();
            ddlMaterialCategory.Items.Insert(0, new ListItem("-- Chọn nhóm vật tư --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void FillddlZone()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Zone_ID, Name from Zone where Active=1" + where);
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }
    protected void FillArea()
    {
        string where = "";
        try
        {
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"SElect Area_ID, Name from Area where 1=1" + where);
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("FillddlZone", ex.ToString());
        }
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadWarehouseImport();
        }
    }
    
    protected void LoadWarehouseImport()
    {
        try
        {

            string userId = string.Empty;
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetAccountType_ID() == "7")
                {
                    userId = MyUser.GetUser_ID().ToString();
                }

            }
            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().spGetWarehouseImport_paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlWareHouse.SelectedValue), Convert.ToInt32(ddlMaterial.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue) ,Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue) ,txtName.Text, ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, "CreateDate DESC", userId);
            rptWarehouseImport.DataSource = dtSet.Tables[0];
            rptWarehouseImport.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseImport_Add.aspx", false);
    }


    protected void rptWarehouseImport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int WarehouseImport_ID = Convert.ToInt32(e.CommandArgument);
        WarehouseImportRow _WarehouseImportRow = new WarehouseImportRow();
        _WarehouseImportRow = BusinessRulesLocator.GetWarehouseImportBO().GetByPrimaryKey(WarehouseImport_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_WarehouseImportRow != null)
                {
                    _WarehouseImportRow.Active = -1;
                }
                BusinessRulesLocator.GetWarehouseImportBO().Update(_WarehouseImportRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
                //case "Active":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 1;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Kích hoạt thành công !");
                //    break;
                //case "Deactive":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 0;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;

        }
        lblMessage.Visible = true; ;
        LoadWarehouseImport();
    }

    protected void rptWarehouseImport_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            if (Common.CheckUserXuanHoa1())
            {
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdnoxuanhoa")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdxuanhoa")).Visible = true;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdnodatexuanhoa")).Visible = false;
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdnoxuanhoa")).Visible = true;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdxuanhoa")).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdnodatexuanhoa")).Visible = true;
            }
        }
    }



    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseImport();
        FillDllProduct();
        FillddlZone();
        FillArea();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseImport();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseImport();
    }
    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseImport();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillArea();
        LoadWarehouseImport();
    } 
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseImport();
    }

    protected void ddlMaterialCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouseImport();
    }
    protected void btnAddSP_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseImport_Add.aspx", false);
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseImport();
    }

    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadWarehouseImport();
    }
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWarehouseImport();
    }

    protected void ExportFile()
    {
        long Tongtien = 0;
        string wheresql = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "")
        {
            wheresql += " and PB.ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH NGUYÊN VẬT LIỆU CỦA DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC NGUYÊN VẬT LIỆU CỦA DOANH NGHIỆP \n ";
        }
        if (ddlWareHouse.SelectedValue != "0")
        {
            wheresql += " and W.Warehouse_ID=" + ddlWareHouse.SelectedValue;
            ASProductBrandName += "Kho : " + ddlWareHouse.SelectedItem.ToString();

        }
        wheresql += " And(WI.CreateDate  BETWEEN '" + ctlDatePicker1.FromDate + " ' And '" + ctlDatePicker1.ToDate + " ') ";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  SELECT  WI.Name, WI.Amount, M.Unit as MaterialUnit ,WI.CreateDate, WI.Importer,
   ISNULL((select top 1 Price from MaterialPrice where Active=1 and (WI.CreateDate between FromDate and ToDate)  and Material_ID=M.Material_ID),0) as Price
 , (WI.Amount *ISNULL((select top 1 Price from MaterialPrice where Active=1 and (WI.CreateDate between FromDate and ToDate)  and Material_ID=M.Material_ID),0))as Total
  
from WarehouseImport WI   
left join ProductBrand PB on WI.ProductBrand_ID = PB.ProductBrand_ID 
left join Warehouse W on WI.Warehouse_ID = W.Warehouse_ID
left join Material M on WI.Material_ID = M.Material_ID
where PB.Active<>-1 and WI.Active = 1 and M.Active =1 and W.Active=1 " + wheresql);
        string attachment = "attachment; filename= file_excle_tong_so_doanh_nhiep " + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = ASProductBrandName + "\n";
        tab += "(Tổng: " + dt.Rows.Count + ")\n\n";

        //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên nguyên vật liệu ").Replace("MaterialUnit", "Đơn vị tính ").Replace("Amount", "Số lượng ").Replace("Price", "Giá vật tư").Replace("Importer", "Tên người nhập").Replace("CreateDate", "Ngày nhập").Replace("Total", "Thanh tiền"));

            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            Tongtien += Convert.ToInt64(dr["Total"]);
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());

                tab = "\t";
            }

            Response.Write("\n");
        }
        Response.Write("" + "Tổng Tiền");
        Response.Write("\t\t\t\t\t\t" + Tongtien);
        Response.End();

    }


    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }
    protected string LoadAmount(string WarehouseImport_ID)
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  Select (Select P.ExpectedProductivityDescription from Product P where P.Product_ID =WI.Product_ID ) as Unit, (Select SUM([Amount]) from WarehouseImportProductPackage WIP where WIP.WarehouseImport_ID = WI.WarehouseImport_ID) as Total from WarehouseImport WI where WI.WarehouseImport_ID =" + WarehouseImport_ID);
        if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["Total"].ToString()))
        {
            return decimal.Parse(dt.Rows[0]["Total"].ToString()).ToString("N0") + " "+ dt.Rows[0]["Unit"].ToString();
        }
        else
        {
            return "";
        }
    }
}