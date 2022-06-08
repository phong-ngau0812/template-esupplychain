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

public partial class Material_List : System.Web.UI.Page
{
    //chưa phân trang theo sql
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlMateriaType();
            FillDDWarehouse();
            LoadMateria();
        }
        if (Common.CheckUserXuanHoa1())
        {
            hidenXuanHoa.Visible = false;
        }
        else
        {
            hidenXuanHoa.Visible = true;
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


    private void FillDDLddlMateriaType()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable("Active<>-1", " Sort ASC");
            ddlMateriaType.DataSource = dt;
            ddlMateriaType.DataTextField = "Name";
            ddlMateriaType.DataValueField = "MaterialCategory_ID";
            ddlMateriaType.DataBind();
            ddlMateriaType.Items.Insert(0, new ListItem("-- Chọn loại vật tư --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDWarehouse()
    {

        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (MyUser.GetWarehouse_ID() != "")
            {
                where += " and Warehouse_ID in (" + MyUser.GetWarehouse_ID() + ")";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable("Active <>-1" + where, " CreateDate ASC");
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void LoadMateria()
    {
        try
        {
            string where = "";
            string whereZone = "";

            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and M.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlWarehouse.SelectedValue != "")
            {
                where += " and M.Warehouse_ID like '%," + ddlWarehouse.SelectedValue + ",%'";
            }
            else if (MyUser.GetWarehouse_ID() != "")
            {
                int count = 1;
                string[] values = MyUser.GetWarehouse_ID().Split(',');
                foreach (var item in values)
                {
                    if (item != "")
                    {
                        if (count == 1)
                        {
                            where += " and M.Warehouse_ID like '%," + item + ",%' ";
                        }
                        else
                        {
                            where += " or M.Warehouse_ID like '%," + item + ",%' ";
                        }
                        count++;
                    }
                }

            }
            if (ddlMateriaType.SelectedValue != "")
            {
                whereZone += " and M.MaterialCategory_ID =" + ddlMateriaType.SelectedValue;
            }
            if (Common.GetFunctionGroupDN())
            {
                if (MyUser.GetAccountType_ID() == "7")
                {
                    where += " and M.CreateBy ='" + MyUser.GetUser_ID() + "'";
                }

            }
            where += "and  M.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
            // chưa duyệt theo Workshop.Active
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select M.*, ISNULL((select top 1 Price from MaterialPrice where Active=1 and (GETDATE() between FromDate and ToDate) and Material_ID= M.Material_ID),0) as MaterialPrice ,MC.Name as NameMaterialCategory
from Material M   
left join MaterialCategory MC on MC.MaterialCategory_ID = M.MaterialCategory_ID 
where  M.Active = 1 " + where + whereZone);
            rptMateria.DataSource = dt;
            rptMateria.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Material_Add.aspx", false);
    }


    protected void rptMateria_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Materia_ID = Convert.ToInt32(e.CommandArgument);
        MaterialRow _MaterialRow = new MaterialRow();
        _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Materia_ID);
        switch (e.CommandName)
        {
            case "Delete":


                if (MyActionPermission.CanDeleteMaterial(Materia_ID, ref Message))
                {
                    if (_MaterialRow != null)
                    {
                        _MaterialRow.Active = false;
                    }
                    BusinessRulesLocator.GetMaterialBO().Update(_MaterialRow);
                    MyActionPermission.WriteLogSystem(Materia_ID, "Xóa - " + _MaterialRow.Name);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
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
        LoadMateria();
    }

    protected void rptMateria_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdnoxuanhoa")).Visible = true;

            }
        }
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMateria();
    }

    protected void ddlMateriaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMateria();
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LoadMateria();
        }
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LoadMateria();
        }
    }

    protected void ExportFile()
    {

        string wheresql = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "0")
        {
            wheresql += " and M.ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH VẬT TƯ CỦA DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC VẬT TƯ CỦA DOANH NGHIỆP \n ";
        }
        if (ddlMateriaType.SelectedValue != "")
        {
            wheresql += " and M.MaterialCategory_ID=" + ddlMateriaType.SelectedValue;
            ASProductBrandName += "Loại vật tư: " + ddlMateriaType.SelectedItem.ToString();

        }
        ASProductBrandName += "Thời gian từ: " + DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") + "-" + DateTime.Parse(ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59).ToString()).ToString("dd/MM/yyyy") + "\n";
        wheresql += "and  M.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select M.Name, MC.Name as MaterialCategory, M.Unit ,ISNULL((select top 1 Price from MaterialPrice where Active=1 and (GETDATE() between FromDate and ToDate) and Material_ID= M.Material_ID),0) as MaterialPrice ,  M.IsolationDay
from Material M 
left join MaterialCategory MC on MC.MaterialCategory_ID = M.MaterialCategory_ID 
where  M.Active = 1 " + wheresql);
        string attachment = "attachment; filename= file_excle_tong_so_vat_tu" + ".xls";
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
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên Vật tư ").Replace("MaterialCategory", "Loại vật tư ").Replace("Unit", "Đơn vị tính ").Replace("MaterialPrice", "Giá vật tư (vnđ)").Replace("IsolationDay", " Cách ly (ngày)"));

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

    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }
}