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

public partial class Ministry_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMinistry();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadMinistry()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMinistryBO().GetAsDataTable(" Active<>-1", " Sort ASC");
            rptMinistry.DataSource = dt;
            rptMinistry.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ministry_Add.aspx", false);
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
                MinistryRow _MinistryRow = new MinistryRow();
                if (!string.IsNullOrEmpty( lblID.Text))
                {
                    _MinistryRow = BusinessRulesLocator.GetMinistryBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                    if (ckActive.Checked)
                    {
                        _MinistryRow.Active = 1;
                        BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _MinistryRow.Active = 0;
                        BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadMinistry();
                }
                
            }
          
        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptMinistry_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Ministry_ID = Convert.ToInt32(e.CommandArgument);
        MinistryRow _MinistryRow = new MinistryRow();
        _MinistryRow = BusinessRulesLocator.GetMinistryBO().GetByPrimaryKey(Ministry_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_MinistryRow != null)
                {
                    _MinistryRow.Active = -1;
                }
                BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_MinistryRow != null)
                {
                    _MinistryRow.Active = 1;
                }
                BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_MinistryRow != null)
                {
                    _MinistryRow.Active = 0;
                }
                BusinessRulesLocator.GetMinistryBO().Update(_MinistryRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
        }
        lblMessage.Visible = true; 
        LoadMinistry();
    }

    protected void rptMinistry_ItemDataBound(object sender, RepeaterItemEventArgs e)
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