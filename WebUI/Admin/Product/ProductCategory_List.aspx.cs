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

public partial class ProductCategory_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProductCategory();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadProductCategory()
    {
        try
        {
            //DataTable dtProductCategoryParent = new DataTable();//
            //dtProductCategoryParent.Clear();
            //dtProductCategoryParent.Columns.Add("ProductCategory_ID");
            //dtProductCategoryParent.Columns.Add("Parent_ID");
            //dtProductCategoryParent.Columns.Add("Name");
            //dtProductCategoryParent.Columns.Add("Image");
            //dtProductCategoryParent.Columns.Add("Active");

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID is null ", " Sort ASC");

            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRow itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //    itemProductCategoryParent["ProductCategory_ID"] = item["ProductCategory_ID"];
            //    itemProductCategoryParent["Parent_ID"] = item["Parent_ID"];
            //    itemProductCategoryParent["Name"] = "<span style='color:#47557a'>" + item["Name"]+"</span>";
            //    itemProductCategoryParent["Image"] = item["Image"];
            //    itemProductCategoryParent["Active"] = item["Active"];
            //    dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //    if (item["ProductCategory_ID"] != null)
            //    {
            //        DataTable dtChild = new DataTable();
            //        dtChild = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID =" + item["ProductCategory_ID"], " Sort ASC");
            //        if (dtChild.Rows.Count > 0)
            //        {
            //            foreach (DataRow itemChild in dtChild.Rows)
            //            {
            //                itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //                itemProductCategoryParent["ProductCategory_ID"] = itemChild["ProductCategory_ID"];
            //                itemProductCategoryParent["Parent_ID"] = item["ProductCategory_ID"];
            //                itemProductCategoryParent["Name"] = " &nbsp;&nbsp;&nbsp;  - " + itemChild["Name"];
            //                itemProductCategoryParent["Image"] = itemChild["Image"];
            //                itemProductCategoryParent["Active"] = itemChild["Active"];
            //                dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //            }

            //        }
            //    }
            //}

            rptProductCategory.DataSource =BusinessRulesLocator.Conllection().GetProductCategory();
            rptProductCategory.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductCategory_Add.aspx", false);
    }



    protected void rptProductCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductCategory_ID = Convert.ToInt32(e.CommandArgument);
        ProductCategoryRow _ProductCategoryRow = new ProductCategoryRow();
        _ProductCategoryRow = BusinessRulesLocator.GetProductCategoryBO().GetByPrimaryKey(ProductCategory_ID);
        switch (e.CommandName)
        {
            case "Delete":
                BusinessRulesLocator.GetProductCategoryBO().DeleteByPrimaryKey(ProductCategory_ID);
                lblMessage.Text =("Xóa bản ghi thành công !");
                LoadProductCategory();
                break;
            case "Active":
                _ProductCategoryRow.Active = true;
                BusinessRulesLocator.GetProductCategoryBO().Update(_ProductCategoryRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _ProductCategoryRow.Active = false;
                BusinessRulesLocator.GetProductCategoryBO().Update(_ProductCategoryRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;
        }
        lblMessage.Visible = true;
        LoadProductCategory();
    }

   
    protected void rptProductCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "False")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else
                {
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                }

                
            }
        }
    }
}