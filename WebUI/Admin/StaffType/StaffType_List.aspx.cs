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

public partial class StaffType_List : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //FillDDLddlProductBrand();
            LoadStaffType();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    //private void FillDDLddlProductBrand()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active<>-1", " Sort ASC");
    //        ddlProductBrand.DataSource = dt;
    //        ddlProductBrand.DataTextField = "Name";
    //        ddlProductBrand.DataValueField = "ProductBrand_ID";
    //        ddlProductBrand.DataBind();
    //        ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillDDLCha", ex.ToString());
    //    }
    //}


    protected void LoadStaffType()
    {
        try
        {
            string where = "";
            //if (ddlProductBrand.SelectedValue != "0")
            //{
            //    where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            //}

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            rptStaffType.DataSource = dt;
            rptStaffType.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffType_Add.aspx", false);
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
                StaffTypeRow _StaffTypeRow = new StaffTypeRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _StaffTypeRow = BusinessRulesLocator.GetStaffTypeBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _StaffTypeRow.Active = 1;

                        BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _StaffTypeRow.Active = 0;
                        BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadStaffType();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptStaffType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int StaffType_ID = Convert.ToInt32(e.CommandArgument);
        StaffTypeRow _StaffTypeRow = new StaffTypeRow();
        _StaffTypeRow = BusinessRulesLocator.GetStaffTypeBO().GetByPrimaryKey(StaffType_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_StaffTypeRow != null)
                {
                    _StaffTypeRow.Active = -1;
                }
                BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_StaffTypeRow != null)
                {
                    _StaffTypeRow.Active = 1;
                }
                BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_StaffTypeRow != null)
                {
                    _StaffTypeRow.Active = 0;
                }
                BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadStaffType();
    }

    protected void rptStaffType_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadStaffType();
    }
}