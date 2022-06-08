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

public partial class FCMMessage_List : System.Web.UI.Page
{
    // bảng không dùng 
    string Gerder;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            LoadTransporter();
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
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            ddlProductBrand.DataSource = dt;
            ddlProductBrand.DataTextField = "Name";
            ddlProductBrand.DataValueField = "ProductBrand_ID";
            ddlProductBrand.DataBind();
            ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void LoadTransporter()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTransporterBO().GetAsDataTable(" Active <>-1" + where, "Sort ASC");
            rptTransporter.DataSource = dt;
            rptTransporter.DataBind();
         
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    public void loadGerder()
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Transporter_Add.aspx", false);
    }

    protected void rptTransporter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Transporter_ID = Convert.ToInt32(e.CommandArgument);
        TransporterRow _TransporterRow = new TransporterRow();
        _TransporterRow = BusinessRulesLocator.GetTransporterBO().GetByPrimaryKey(Transporter_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_TransporterRow != null)
                {
                    _TransporterRow.Active = -1;
                }
                BusinessRulesLocator.GetTransporterBO().Update(_TransporterRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_TransporterRow != null)
                {
                    _TransporterRow.Active = 1;
                }
                BusinessRulesLocator.GetTransporterBO().Update(_TransporterRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_TransporterRow != null)
                {
                    _TransporterRow.Active = 0;
                }
                BusinessRulesLocator.GetTransporterBO().Update(_TransporterRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadTransporter();
    }

    protected void rptTransporter_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadTransporter();
    }
}