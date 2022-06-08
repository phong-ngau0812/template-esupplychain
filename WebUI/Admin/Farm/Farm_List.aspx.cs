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

public partial class Farm_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    private int productCategory_ID;
    private string Code = string.Empty;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Code"]))
            Code = Request["Code"].ToString();

        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            //txtCODE.Text = Code;
            FillDDLddlProductBrand();
            FillDDLddlddlZone();
            Common.CheckAccountTypeZone(ddlZone);
            FillDDLddlArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            FillDDLddlWorkshop();
            LoadFarm();
        }
        ResetMsg();
    }

    private void FillDDLddlWorkshop()
    {

        try
        {
            string where = string.Empty;
            DataTable dt = new DataTable();
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " And ProductBrand_ID=" + ddlProductBrand.SelectedValue;
                if (ddlZone.SelectedValue != "")
                {
                    where += " and Zone_ID =" + ddlZone.SelectedValue;
                }
                if (ddlArea.SelectedValue != "")
                {
                    where += " and Area_ID =" + ddlArea.SelectedValue;
                }
                dt = BusinessRulesLocator.Conllection().GetAllList("select Name, Workshop_ID from Workshop where (Active<>-1 or Active is null) " + where + " order by Name ASC");

            }
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn nhân viên, hộ sản xuất --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
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
            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    ddlZone.SelectedValue = MyUser.GetZone_ID();
                    ddlZone.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    private void FillDDLddlArea()
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
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select A.* from Area A inner join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 " + where);
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu sản xuất --", "0"));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void LoadFarm()
    {
        try
        {

            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().GetFarmV2(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), txtName.Text, "CreateDate DESC");
            //dtSet = BusinessRulesLocator.Conllection().GetFarmV2(1, 1000, 7, 0, 0, 0, "", "");
            rptFarm.DataSource = dtSet.Tables[0];
            rptFarm.DataBind();
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
            Log.writeLog("LoadFarm", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadFarm();
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Farm_Add.aspx", false);
    }



    protected void rptFarm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
        //    LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
        //    LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
        //    Literal lblText = e.Item.FindControl("lblText") as Literal;
        //    // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
        //    if (lblApproved != null)
        //    {
        //        if (lblApproved.Text == "False")
        //        {
        //            btnDeactive.Visible = true;
        //            btnActive.Visible = false;
        //            lblText.Text = "Ngừng kích hoạt";
        //        }
        //        else
        //        {
        //            lblText.Text = "Đang kích hoạt";
        //            btnDeactive.Visible = false;
        //            btnActive.Visible = true;
        //        }
        //    }
        //}
    }

    protected void rptFarm_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Farm_ID = Convert.ToInt32(e.CommandArgument);
        FarmRow _FarmRow = new FarmRow();
        _FarmRow = BusinessRulesLocator.GetFarmBO().GetByPrimaryKey(Farm_ID);
        switch (e.CommandName)
        {
            case "Delete":


                if (MyActionPermission.CanDeleteFarm(Farm_ID, ref Message))
                {
                    MyActionPermission.WriteLogSystem(Farm_ID, "Xóa - " + _FarmRow.Name);
                    BusinessRulesLocator.GetFarmBO().DeleteByPrimaryKey(Farm_ID);
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
        LoadFarm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadFarm();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadFarm();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDDLddlArea();
        FillDDLddlddlZone();
        FillDDLddlWorkshop();
        LoadFarm();

    }



    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDDLddlArea();
        FillDDLddlWorkshop();
        LoadFarm();
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDDLddlWorkshop();
        LoadFarm();
    }
    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadFarm();
    }
    protected void ExportFile()
    {

        int ProductBrand_ID = 0;
        string where = "";

        string ASProductBrandName = "";
        if (ddlProductBrand.SelectedValue != "0")
        {

            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH THỬA LÔ SẢN XUẤT CỦA DOANH NGHIỆP \n ";
            ASProductBrandName += "Doanh nghiệp: " + ddlProductBrand.SelectedItem.ToString() + "\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH THỬA LÔ SẢN XUẤT TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }

        if (ddlZone.SelectedValue != "0")
        {
            where += "and F.Zone_ID =" + ddlZone.SelectedValue;
        }

        if (ddlArea.SelectedValue != "0")
        {
            where += "and  F.Area_ID =" + ddlArea.SelectedValue;
        }
        if (MyUser.GetFunctionGroup_ID() == "8")
        {
            where += " and  PB.ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID =" + MyUser.GetLocation_ID() + ")";
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"SELECT  F.Name, F.Phone, F.Address, F.Acreage, WS.Name as TENSX 
		   from Farm F
		   inner join ProductBrand PB on F.ProductBrand_ID = PB.ProductBrand_ID 
		   left join Workshop WS on F.Workshop_ID = WS.Workshop_ID
		   where 1=1" + where + " order by F.Name ASC");

        string attachment = "attachment; filename= file_excle_tong_so_nhan_vien " + ProductBrand_ID + ".xls";
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
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên thử đất").Replace("Acreage", "Diện tích (m2) ").Replace("Phone", "Số điện thoại ").Replace("Address", "Địa chỉ").Replace("TENSX", "Nhân viên sản xuất(hô gia đình)"));

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