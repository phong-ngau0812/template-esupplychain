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

public partial class TestingCertificates_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable(" UserID='" + MyUser.GetUser_ID() + "'", "");
                if (dt.Rows.Count == 1)
                {
                    ProductBrandList.Value = dt.Rows[0]["ProductBrand_ID_List"].ToString();
                }
            }
            FillDDLddlProductBrand();

            loadTestingCertificates();
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
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, " ");
            }
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

    protected void loadTestingCertificates()
    {
        string where = "";
       
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
           
        }
        if (!string.IsNullOrEmpty(ProductBrandList.Value))
        {
            where += " and PB.ProductBrand_ID in (" + ProductBrandList.Value + ")";
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TC.*
from TestingCertificates TC
left join ProductBrand PB on TC.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and TC.Active <>-1"  + where );
            rptTestingCertificates.DataSource = dt;
            rptTestingCertificates.DataBind();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestingCertificates_Add.aspx", false);
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
                TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _TestingCertificatesRow.Active = 1;

                        BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _TestingCertificatesRow.Active = 0;
                        BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    loadTestingCertificates();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptTestingCertificates_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int TestingCertificates_ID = Convert.ToInt32(e.CommandArgument);
        TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();
        _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (MyActionPermission.CanDeleteCustomerType(TestingCertificates_ID, ref Message))
                {
                    if (_TestingCertificatesRow != null)
                    {
                        _TestingCertificatesRow.Active = -1;
                    }
                    MyActionPermission.WriteLogSystem(TestingCertificates_ID, "Xóa - " + _TestingCertificatesRow.Name);
                    BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
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
                if (_TestingCertificatesRow != null)
                {
                    _TestingCertificatesRow.Active = 1;
                }
                BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_TestingCertificatesRow != null)
                {
                    _TestingCertificatesRow.Active = 0;
                }
                BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        loadTestingCertificates();
    }

    protected void rptTestingCertificates_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        loadTestingCertificates();
    }


    protected void ExportFile()
    {

        string where = "";

        string ASProductBrandName = "";


        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            ASProductBrandName += "TỔNG DANH SÁCH PHIẾU KIỂM NGHIỆM DOANH NGHIỆP" + " (" + ddlProductBrand.SelectedItem.ToString() + ")\n";
        }
        else
        {
            ASProductBrandName += "TỔNG DANH SÁCH TẤT CẢ CÁC PHIẾU KIỂM NGHIỆM CỦA DOANH NGHIỆP \n ";
        }
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TC.Name, TC.StartDate ,TC.EndDate
from TestingCertificates TC
left join ProductBrand PB on TC.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and TC.Active <>-1" + where);
        string attachment = "attachment; filename= Danh sach phieu kiem nghiem " + ".xls";
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
            Response.Write(tab + dc.ColumnName.Replace("Name", "Tên phiếu kiểm nghiệm ").Replace("StartDate", "Ngày có hiệu lực của phiếu").Replace("EndDate", "Ngày hết hiệu lục của phiếu"));

            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
              
                if (i == 1 || i == 2)
                {
                    if (!string.IsNullOrEmpty(dr[i].ToString()))
                    {
                        Response.Write(tab + DateTime.Parse(dr[i].ToString()).ToString("dd/MM/yyyy"));
                        tab = "\t";
                    }
                    else
                    {
                        Response.Write(tab + "NULL");
                        tab = "\t";
                    }
                }
                else
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
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