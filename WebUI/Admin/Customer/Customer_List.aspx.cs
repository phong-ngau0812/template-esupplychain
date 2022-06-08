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

public partial class Customer_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLddlCustomerType();
            loadCustomer();
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


    private void FillDDLddlCustomerType()
    {
        try
        {
            string where = " Active<>-1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += "and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetCustomerTypeBO().GetAsDataTable(where, " Sort ASC");
            ddlCustomerType.DataSource = dt;
            ddlCustomerType.DataTextField = "Name";
            ddlCustomerType.DataValueField = "CustomerType_ID";
            ddlCustomerType.DataBind();
            ddlCustomerType.Items.Insert(0, new ListItem("-- Chọn nhóm khách hàng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void loadCustomer()
    {
        string where = "";
        string whereCustomer_ID = "";
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
           
        }
        if (ddlCustomerType.SelectedValue != "0"){
            whereCustomer_ID += "and CT.CustomerType_ID =" + ddlCustomerType.SelectedValue;
        }
        if (Common.GetFunctionGroupDN())
        {
            if (MyUser.GetAccountType_ID() == "7")
            {
                where += " and C.CreateBy ='" + MyUser.GetUser_ID() + "'";
            }

        }
        where += "and  C.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select C.*
from Customer C
left join ProductBrand PB on C.ProductBrand_ID = PB.ProductBrand_ID
left join CustomerType CT on C.CustomerType_ID = CT.CustomerType_ID
where PB.Active = 1 " + whereCustomer_ID + where );
            rptCustomer.DataSource = dt;
            rptCustomer.DataBind();

            

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetCustomerBO().GetAsDataTable(where + whereCustomer_ID, " Customer_ID DESC");
            //rptCustomer.DataSource = dt;
            //rptCustomer.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customer_Add.aspx", false);
    }

    
    protected void rptCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Customer_ID = Convert.ToInt32(e.CommandArgument);
        CustomerRow _CustomerRow = new CustomerRow();
        _CustomerRow = BusinessRulesLocator.GetCustomerBO().GetByPrimaryKey(Customer_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteCustomer(Customer_ID, ref Message))
                {
                    MyActionPermission.WriteLogSystem(Customer_ID, "Xóa - " + _CustomerRow.Name);
                    BusinessRulesLocator.GetCustomerBO().DeleteByPrimaryKey(Customer_ID);
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
        loadCustomer();
    }

    protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        FillDDLddlCustomerType();
        loadCustomer();
    }

    protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCustomer();
    }

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            loadCustomer();
        }
    }

    protected void ExportFile()
    {

        string where = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and C.ProductBrand_ID =" + ddlProductBrand.SelectedValue ;
            ASProductBrandName += "TỔNG DANH SÁCH KHÁCH HÀNG DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC KHÁCH HÀNG CỦA DOANH NGHIỆP \n ";
        }
        if (ddlCustomerType.SelectedValue != "0")
        {
            where += " and C.CustomerType_ID=" +ddlCustomerType.SelectedValue;
            ASProductBrandName += "Nhóm khách hàng" + ddlCustomerType.SelectedItem.ToString();

        }
        ASProductBrandName += "Thời gian từ: "+ DateTime.Parse(ctlDatePicker1.FromDate.ToString()).ToString("dd/MM/yyyy") +"-"+ DateTime.Parse(ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59).ToString()).ToString("dd/MM/yyyy") + "\n";
        where += "and C.CreateDate BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select C.Name , C.Phone ,  C.Email , C.Address from Customer C
 left join CustomerType CT on CT.CustomerType_ID = C.CustomerType_ID
where CT.Active=1 " + where);
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
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên Khách hàng ").Replace("Phone", "Số điện thoại ").Replace("Email", "Email").Replace("Address", "Địa chỉ"));

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