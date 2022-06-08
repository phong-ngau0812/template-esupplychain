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

public partial class SalesShift : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            loadSalesShift();
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


 

    protected void loadSalesShift()
    {
        string where = "";
       
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and SS.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
           
        }
        
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select SS.SalesShift_ID, SS.Name , SS.FromHour , SS.ToHour, SS.Active 
  from SalesShift SS 
  where SS.Active <>-1 " + where );
            rptSalesShift.DataSource = dt;
            rptSalesShift.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesShift_Add.aspx", false);
    }


    protected void rptSalesShift_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int SalesShift_ID = Convert.ToInt32(e.CommandArgument);
        SalesShiftRow _SalesShiftRow = new SalesShiftRow();
        _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (MyActionPermission.CanDeleteSalesShift(SalesShift_ID, ref Message))
                {
                    if (_SalesShiftRow != null)
                    {
                        _SalesShiftRow.Active = -1;
                    }
                    BusinessRulesLocator.GetSalesShiftBO().Update(_SalesShiftRow);
                    MyActionPermission.WriteLogSystem(SalesShift_ID, "Xóa - " + _SalesShiftRow.Name);
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
                _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);
                if (_SalesShiftRow != null)
                {
                    _SalesShiftRow.Active = 1;
                }
                BusinessRulesLocator.GetSalesShiftBO().Update(_SalesShiftRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);
                if (_SalesShiftRow != null)
                {
                    _SalesShiftRow.Active = 0;
                }
                BusinessRulesLocator.GetSalesShiftBO().Update(_SalesShiftRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true;
        loadSalesShift();
    }

    protected void rptSalesShift_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        loadSalesShift();
    }

}