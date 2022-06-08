using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TaskStepProduct_Copy : System.Web.UI.Page
{
    public string title = "Copy nhanh đề mục công việc ";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadProductCategory();
            LoadProduct();
            LoadProductList();
        }

    }
    private void LoadProductList()
    {

        try
        {
            string where = string.Empty;
            if (ddlCha.SelectedValue != "")
            {
                where += " And ProductCategory_ID=" + ddlCha.SelectedValue;
            }
            //if (ddlProduct.SelectedValue != "")
            //{
            //    where += " And Product_ID<>" + ddlProduct.SelectedValue;
            //}

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Product_ID not in (select Product_ID from TaskStep where Product_ID is not null) and  Active=1" + where + " order by Name ASC");
            ddlProductList.DataSource = dt;
            ddlProductList.DataTextField = "Name";
            ddlProductList.DataValueField = "Product_ID";
            ddlProductList.DataBind();
            ddlProductList.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadProduct()
    {

        try
        {
            string where = string.Empty;
            if (ddlCha.SelectedValue != "")
            {
                where += " And ProductCategory_ID=" + ddlCha.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Product_ID  in (select Product_ID from TaskStep where Product_ID is not null) and Active=1" + where + " order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void LoadProductCategory()
    {
        try
        {
            ddlCha.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "ProductCategory_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DataTable dtNguon = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable("Product_ID=" + ddlProduct.SelectedValue, "");
                TaskStepRow _TaskStepRow = new TaskStepRow();
                foreach (DataRow item in dtNguon.Rows)
                {
                    _TaskStepRow.Name = item["Name"].ToString();
                    _TaskStepRow.Sort = Convert.ToInt32(item["Sort"].ToString());
                    _TaskStepRow.Description = (item["Description"].ToString());
                    foreach (ListItem itemChild in ddlProductList.Items)
                    {
                        if (itemChild.Selected)
                        {
                            _TaskStepRow.Product_ID = Convert.ToInt32(itemChild.Value.ToString());
                            BusinessRulesLocator.GetTaskStepBO().Insert(_TaskStepRow);
                        }
                    }
                }
                lblMessage.Text = "Sao chép đầu mục công việc sản phẩm thành công";
                lblMessage.Visible = true;
                ddlCha.SelectedValue = "";
                ddlProduct.SelectedValue = "";
                ddlProductList.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product_List.aspx", false);
    }

    protected void ddlCha_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProduct();
        LoadProductList();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductList();
    }
}