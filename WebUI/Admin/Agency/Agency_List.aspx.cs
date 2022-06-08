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

public partial class Agency_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            loadAgency();
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

    protected void loadAgency()
    {
        string where = "";
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        where += "and  A.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select A.*
from Agency A
left join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and A.Active <>-1" + where);
            rptAgency.DataSource = dt;
            rptAgency.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agency_Add.aspx", false);
    }

    protected void ckActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckActive = (CheckBox)sender;
        RepeaterItem row = (RepeaterItem)ckActive.NamingContainer;
        Literal lblID = (Literal)row.FindControl("lblID");
        try
        {
            if (lblID != null)
            {
                AgencyRow _AgencyRow = new AgencyRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _AgencyRow.Active = 1;

                        BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _AgencyRow.Active = 0;
                        BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    loadAgency();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptAgency_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Agency_ID = Convert.ToInt32(e.CommandArgument);
        AgencyRow _AgencyRow = new AgencyRow();
        _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Agency_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteCustomerType(Agency_ID, ref Message))
                {
                    if (_AgencyRow != null)
                    {
                        _AgencyRow.Active = -1;
                    }
                    MyActionPermission.WriteLogSystem(Agency_ID, "Xóa - " + _AgencyRow.Name);
                    BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            case "Active":
                if (_AgencyRow != null)
                {
                    _AgencyRow.Active = 1;
                }
                BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_AgencyRow != null)
                {
                    _AgencyRow.Active = 0;
                }
                BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        loadAgency();
    }

    protected void rptAgency_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        loadAgency();
    }

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            loadAgency();
        }
    }


    protected void ExportFile()
    {

        string where = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH CHI NHÁNH DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC CHI NHÁNH CỦA DOANH NGHIỆP \n ";
        }
        ASProductBrandName += "Thời gian từ: " + DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") + "-" + DateTime.Parse(ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59).ToString()).ToString("dd/MM/yyyy") + "\n";
        where += "and A.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select A.Name , A.Phone, A.Email, A.Address , A.Level , A.PersonName as Person
from Agency A
left join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and A.Active <>-1" + where);



        string attachment = "attachment; filename= Danh sach chi nhanh " + ".xls";
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
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên đại lý ").Replace("Phone", "Số điện thoại ").Replace("Email", "Email").Replace("Address", "Địa chỉ").Replace("Level", "Cấp đại lý").Replace("Person", "Người đại diện"));

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
    protected void ExportFile1()
    {

        string where = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH CHI NHÁNH DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC CHI NHÁNH CỦA DOANH NGHIỆP \n ";
        }
        ASProductBrandName += "Thời gian từ: " + DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") + "-" + DateTime.Parse(ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59).ToString()).ToString("dd/MM/yyyy") + "\n";
        where += "and A.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select A.Name , A.Phone, A.Email, A.Address , A.Level , A.PersonName as Person
from Agency A
left join ProductBrand PB on A.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and A.Active <>-1" + where);


        dt.Columns["Name"].ColumnName = "Tên đại lý";
        dt.Columns["Phone"].ColumnName = "Số điện thoại ";
        dt.Columns["Email"].ColumnName = "Email";
        dt.Columns["Address"].ColumnName = "Địa chỉ";
        dt.Columns["Level"].ColumnName = "Cấp bậc đại lý";
        dt.Columns["Person"].ColumnName = "Người đại diện";
        dt.AcceptChanges();

        string tab = ASProductBrandName + "<br>";
        tab += "(Tổng: " + dt.Rows.Count + ")<br><br>";
        GridView gvDetails = new GridView();

        gvDetails.DataSource = dt;

        gvDetails.AllowPaging = false;

        gvDetails.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "danh-sach-chi-nhanh-doanh-nghiep.xls"));
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
    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile1();
    }
}