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

public partial class Workshop_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    public string Message = "";
    public string Title = string.Empty;
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        Title = "Nhân viên/chủ hộ";
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlDepartment();
            LoadZone();
            Common.CheckAccountTypeZone(ddlZone);
            LoadArea();
            Common.CheckAccountTypeArea(ddlZone, ddlArea);
            FillLocation();
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    FillDistrict();
                    ddlLocation.Enabled = false;

                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                }
                else if (MyUser.GetRank_ID() == "4")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                    ddlWard.SelectedValue = MyUser.GetWard_ID();
                    ddlWard.Enabled = false;
                }

            }
            if (Common.GetFunctionGroupDN())
            {
                ddlLocation.Visible = ddlDistrict.Visible = ddlWard.Visible = false;
            }
            LoadWorkshop();
        }

        ResetMsg();
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            Common.FillProductBrand(ddlProductBrand, where);
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
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    private void FillDDLddlDepartment()
    {

        try
        {
            DataTable dt = new DataTable();

            string where = "";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
                dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active = 1 " + where, "");
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "Name";
                ddlDepartment.DataValueField = "Department_ID";
                ddlDepartment.DataBind();
                if (!string.IsNullOrEmpty(MyUser.GetDepartment_ID()))
                {
                    if (MyUser.GetDepartment_ID() != "0")
                    {
                        ddlDepartment.SelectedValue = MyUser.GetDepartment_ID();
                        ddlDepartment.Enabled = false;
                    }
                }

            }
            ddlDepartment.Items.Insert(0, new ListItem("-- Chọn phòng ban --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void LoadWorkshop()
    {
        try
        {
            //string workshop_list = string.Empty;
            //if (ddlZone.SelectedValue != "0")
            //{
            //    workshop_list = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Convert.ToInt32(ddlZone.SelectedValue)).Workshop_ID;
            //    workshop_list = workshop_list.Substring(1, workshop_list.Length - 1);
            //    workshop_list = workshop_list.Remove(workshop_list.Length - 1);
            //    //Response.Write(workshop_list);
            //}
            int workshopID = 0;
            //if (!string.IsNullOrEmpty(MyUser.GetWorkshop_ID()))
            //{
            //    workshopID = Convert.ToInt32(MyUser.GetWorkshop_ID());
            //}
            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().GetWorkshop_paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), workshopID, txtName.Text, type, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), "CreateDate DESC", Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlArea.SelectedValue));
            //dtSet = BusinessRulesLocator.Conllection().GetWorkshop_paging(1, 1000, 7, 0, 0, "", "");
            rptWorkshop.DataSource = dtSet.Tables[0];
            rptWorkshop.DataBind();
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
    public void CheckNhanVien_Check(Object sender, EventArgs e)
    {
        if (cknhanvien.Checked == true)
        {
            type = 1;
            LoadWorkshop();
            Title = "Nhân Viên";

        }
        else if (ckhsx.Checked == true)
        {
            type = 2;
            LoadWorkshop();
            Title = "Hộ sản xuất";
        }
        else
        {
            type = 0;
            LoadWorkshop();
            Title = "Nhân viên/chủ hộ ";
        }
        //else
        //{
        //    type = 0;
        //    LoadWorkshop();
        //}
        //if (ckhsx.Checked == true)
        //{
        //    type = 2;
        //    LoadWorkshop();
        //}
        //else
        //{
        //    type = 0;
        //    LoadWorkshop();
        //}

    }
    //    protected void LoadWorkshop()
    //    {
    //        try
    //        {
    //            string where = " where (w.active<>-1 or w.active is null) ";
    //            if (ddlProductBrand.SelectedValue != "0")
    //            {
    //                where += " and w. ProductBrand_ID="+ ddlProductBrand.SelectedValue;
    //            }
    //            rptWorkshop.DataSource =BusinessRulesLocator.Conllection().GetAllList(@"select w.Workshop_ID,w.ProductBrand_ID, w.Name, p.Name as DN from Workshop w
    //left join ProductBrand P on P.ProductBrand_ID = w.ProductBrand_ID "+where +" order by Workshop_ID DESC ");
    //            rptWorkshop.DataBind();
    //        }
    //        catch (Exception ex)
    //        {

    //            Log.writeLog("LoadUser", ex.ToString());
    //        }
    //    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workshop_Add.aspx", false);
    }
    protected void rptWorkshop_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Workshop_ID = Convert.ToInt32(e.CommandArgument);
        WorkshopRow _WorkshopRow = new WorkshopRow();
        _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(Workshop_ID);
        switch (e.CommandName)
        {
            case "Delete":


                if (MyActionPermission.CanDeleteWorkshop(Workshop_ID, ref Message))
                {
                    MyActionPermission.WriteLogSystem(Workshop_ID, "Xóa - " + _WorkshopRow.Name);
                    BusinessRulesLocator.GetWorkshopBO().DeleteByPrimaryKey(Workshop_ID);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
                //case "Active":
                //    _WorkshopRow.Active = true;
                //    BusinessRulesLocator.GetWorkshopBO().Update(_WorkshopRow);
                //    lblMessage.Text = ("Kích hoạt thành công !");
                //    break;
                //case "Deactive":
                //    _WorkshopRow.Active = false;
                //    BusinessRulesLocator.GetWorkshopBO().Update(_WorkshopRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;
        }
        lblMessage.Visible = true;
        LoadWorkshop();
    }


    protected void rptWorkshop_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        //            lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
        //        }
        //        else
        //        {
        //            lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
        //            btnDeactive.Visible = false;
        //            btnActive.Visible = true;
        //        }


        //    }
        //}
    }


    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDDLddlDepartment();
        LoadWorkshop();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }

    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadWorkshop();
    }



    protected void ExportFile()
    {

        int ProductBrand_ID = 0;
        string where = "";

        string ASProductBrandName = "";
        if (ddlProductBrand.SelectedValue != "0")
        {

            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH NHÂN VIÊN,HỘ SẢN XUẤT CỦA DOANH NGHIỆP \n ";
            ASProductBrandName += "Doanh nghiệp: " + ddlProductBrand.SelectedItem.ToString() + "\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH NHÂN VIÊN,HỘ SẢN XUẤT TẤT CẢ CÁC DOANH NGHIỆP \n ";
        }

        if (ddlDepartment.SelectedValue != "0")
        {
            where += "and  D.Department_ID =" + ddlDepartment.SelectedValue;
        }
        if (ddlLocation.SelectedValue != "0")
        {
            where += " and  PB.ProductBrand_ID in (Select ProductBrand_ID from ProductBrand where Location_ID =" + ddlLocation.SelectedValue + ")";
        }

        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  SELECT W.Name, D.Name as Department ,PB.Name as ProductBrandT, W.[Address], W.Phone ,(CAST( DAY( W.BirthDate) as varchar) +'/'+CAST( MonTH( W.BirthDate) as varchar) +'/'+ cast( YEAR(W.BirthDate) as varchar)) as Date
		   from Workshop W
		   left join ProductBrand PB on W.ProductBrand_ID = PB.ProductBrand_ID 
		   left join Department D on W.Department_ID = D.Department_ID
		   where PB.Active = 1 and W.Active = 1" + where + " order by W.Name ASC");

        string attachment = "attachment; filename= file_excle_tong_so_nhan_vien " + ProductBrand_ID + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = ASProductBrandName + "\n";


        //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên nhân viên/Hộ sản xuất  ").Replace("Department", "Phòng Ban công tác ").Replace("ProductBrandT", "Doanh nghiệp").Replace("Phone", "Số điện thoại ").Replace("Address", "Địa chỉ").Replace("Date", "Ngày sinh"));

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

    protected void LoadZone()
    {
        try
        {
            string where = " Active=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(where, " Name ASC");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    if (MyUser.GetZone_ID() != "0")
                    {
                        ddlZone.SelectedValue = MyUser.GetZone_ID();
                        ddlZone.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            string where = " 1=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAreaBO().GetAsDataTable(where, " Name ASC");
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                {
                    if (MyUser.GetArea_ID() != "0")
                    {
                        ddlArea.SelectedValue = MyUser.GetArea_ID();
                        ddlArea.Enabled = false;
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }


    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadWorkshop();

    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkshop();
    }
    private void FillWard()

    {
        string where = string.Empty;

        if (ddlDistrict.SelectedValue != "0")
        {
            where = "District_ID = " + ddlDistrict.SelectedValue;
        }
        ddlWard.DataSource = BusinessRulesLocator.GetWardBO().GetAsDataTable("" + where, "");
        ddlWard.DataValueField = "Ward_ID";
        ddlWard.DataTextField = "Name";
        ddlWard.DataBind();
        ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));
    }

    private void FillDistrict()
    {
        string where = string.Empty;
        if (ddlLocation.SelectedValue != "0")
        {
            where += "Location_ID = " + ddlLocation.SelectedValue;
        }
        ddlDistrict.DataSource = BusinessRulesLocator.GetDistrictBO().GetAsDataTable("" + where, "");
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataTextField = "Name";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDistrict();
        LoadWorkshop();
    }
    protected void ddlSo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillWard();
        LoadWorkshop();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadWorkshop();
    }
    protected void FillLocation()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", "Name ASC");
            if (dt.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "Location_ID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-- Tỉnh thành --", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
                ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillLocation", ex.ToString());
        }
    }
}