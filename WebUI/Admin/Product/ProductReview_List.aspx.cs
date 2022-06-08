using DbObj;
using QRCoder;
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

public partial class ProductReview_List : System.Web.UI.Page
{
    public string NameProduct = "";
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 5;//Số bản ghi 1 trang
    private int Product_ID = 0;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!IsPostBack)
        {
            FillApproved();
            
            LoadProductReview();
        }
        FillProduct();
        ResetMsg();
    }

    private void FillProduct()
    {
        if (Product_ID != 0)
        {
            ProductRow _ProductRow = new ProductRow();
            _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
            if (_ProductRow != null)
            {
                NameProduct = _ProductRow.IsNameNull ? string.Empty : _ProductRow.Name;
            }
        }
    }

    private void FillApproved()
    {
        ddlApproved.Items.Insert(0, new ListItem("-- Trạng thái --", "-1"));
        ddlApproved.Items.Insert(1, new ListItem("Chưa phê duyệt", "0"));
        ddlApproved.Items.Insert(2, new ListItem("Đã phê duyệt", "1"));
    }

    private void LoadProductReview()
    {
        if (Product_ID != 0)
        {
            try
            {
                //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
                Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
                DataSet dtSet = new DataSet();
                DataTable dt = new DataTable();
                dtSet = BusinessRulesLocator.Conllection().GetProductReview(Pager1.CurrentIndex, pageSize, 7, Product_ID, Convert.ToInt32(ddlApproved.SelectedValue), ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtName.Text);
                rptProductReview.DataSource = dtSet.Tables[0];
                rptProductReview.DataBind();
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
                Log.writeLog("LoadProductReview", ex.ToString());
            }
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadProductReview();
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProductReview();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductReview();
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductReview();
    }
    protected void ddlApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductReview();
    }

    protected void rptProductReview_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
                LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
                LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
                Literal lblText = e.Item.FindControl("lblText") as Literal;
                Literal lblID = e.Item.FindControl("lblID") as Literal;
                // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
                if (lblApproved != null)
                {
                    if (lblApproved.Text == "0")
                    {
                        btnDeactive.Visible = true;
                        btnActive.Visible = false;
                        lblText.Text = "<span class=\"badge badge-danger\">Chưa được phê duyệt </span> ";
                    }
                    else
                    {
                        lblText.Text = " <span class=\"badge badge-success\"> Đã được phê duyệt </span>";
                        btnDeactive.Visible = false;
                        btnActive.Visible = true;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("rptProductReview_ItemDataBound", ex.ToString());
        }

    }

    protected void rptProductReview_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductReview_ID = Convert.ToInt32(e.CommandArgument);
        ProductReviewRow _ProductReviewRow = new ProductReviewRow();
        _ProductReviewRow = BusinessRulesLocator.GetProductReviewBO().GetByPrimaryKey(ProductReview_ID);
        switch (e.CommandName)
        {
           
            case "Delete":
                if (_ProductReviewRow != null)
                {

                    MyActionPermission.WriteLogSystem(ProductReview_ID, "Xóa - " + _ProductReviewRow.FullName);
                    BusinessRulesLocator.GetProductReviewBO().DeleteByPrimaryKey(ProductReview_ID);
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
                if (_ProductReviewRow != null)
                {
                    _ProductReviewRow.ApprovedDate = DateTime.Now;
                    _ProductReviewRow.ApprovedUser = MyUser.GetUser_ID();
                    _ProductReviewRow.Approved = 1;
                    BusinessRulesLocator.GetProductReviewBO().Update(_ProductReviewRow);
                    lblMessage.Text = ("Đã được phê duyệt !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            case "Deactive":
                if (_ProductReviewRow != null)
                {
                    _ProductReviewRow.ApprovedDate = DateTime.Now;
                    _ProductReviewRow.ApprovedUser = MyUser.GetUser_ID();
                    _ProductReviewRow.Approved = 0 ;
                    BusinessRulesLocator.GetProductReviewBO().Update(_ProductReviewRow);
                    lblMessage.Text = ("Không phê duyệt !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
        }
        lblMessage.Visible = true;
        LoadProductReview();
    }
}