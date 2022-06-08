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

public partial class ProductBrandType_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

            LoadProductBrandType();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    

    protected void LoadProductBrandType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandTypeBO().GetAsDataTable("Active<>-1", "Sort asc");
            rptProductBrandType.DataSource = dt;
            rptProductBrandType.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadProductBrandType", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductBrandType_Add.aspx", false);
    }
   

    protected void rptProductBrandType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductBrandType_ID = Convert.ToInt32(e.CommandArgument);
        ProductBrandTypeRow _ProductBrandTypeRow = new ProductBrandTypeRow();
        _ProductBrandTypeRow = BusinessRulesLocator.GetProductBrandTypeBO().GetByPrimaryKey(ProductBrandType_ID);
        switch (e.CommandName)
        {
            case "Delete":
                _ProductBrandTypeRow.Active = -1;
                BusinessRulesLocator.GetProductBrandTypeBO().Update(_ProductBrandTypeRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                _ProductBrandTypeRow.Active = 1;
                BusinessRulesLocator.GetProductBrandTypeBO().Update(_ProductBrandTypeRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _ProductBrandTypeRow.Active = 0;
                BusinessRulesLocator.GetProductBrandTypeBO().Update(_ProductBrandTypeRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
        }
        lblMessage.Visible = true; ;
        LoadProductBrandType();
    }

    protected void rptProductBrandType_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

}