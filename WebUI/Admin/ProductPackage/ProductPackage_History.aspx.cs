using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackage_History : System.Web.UI.Page
{
    public string name, namepackage, demuc, code, lenhsx, hosx = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtStart.Attributes.Add("readonly", "readonly");
        //txtEnd.Attributes.Add("readonly", "readonly");
        if (!string.IsNullOrEmpty(Request["TaskStep_ID"]))
            int.TryParse(Request["TaskStep_ID"].ToString(), out TaskStep_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);

        ResetMsg();
        if (!IsPostBack)
        {
            LoadSGTIN();
            Init();
        }
    
    }

    private void Init()
    {
        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
  
            if (_ProductPackageRow != null)
            {
                if (!_ProductPackageRow.IsSGTIN_HistoryNull)
                {
                    string[] array = _ProductPackageRow.SGTIN_History.Split(',');
                    foreach (string value in array)
                    {
                        foreach (ListItem item in ddlSGTIN.Items)
                        {
                            if (value == item.Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                //if (!_ProductPackageRow.IsProductPackageOrder_IDNull)
                //{

                //    ProductPackageOrderRow _ProductPackageOrderRow = new ProductPackageOrderRow();
                //    _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(_ProductPackageRow.ProductPackageOrder_ID);
                //    if (_ProductPackageOrderRow != null)
                //    {
                //Response.Write(_ProductPackageOrderRow.SGTIN_LIST);
                if (!_ProductPackageRow.IsSGTIN_HistoryNull)
                {
                    string[] array = _ProductPackageRow.SGTIN_History.Split(',');
                    DataTable dt = new DataTable();
                    dt.Clear();
                    dt.Columns.Add("ProductPackage_ID");
                    dt.Columns.Add("ProductPackageName");
                    dt.Columns.Add("ProductBrandName");
                    dt.Columns.Add("SGTIN");
                    foreach (string value in array)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            int ProductPackage_ID_History = Convert.ToInt32(value);
                            ProductPackageRow _ProductPackageRowHistory = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID_History);
                            string brandName = string.Empty;
                            if (!_ProductPackageRowHistory.IsProductBrand_IDNull)
                            {
                                brandName = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRowHistory.ProductBrand_ID).Name;
                            }
                            DataRow _row = dt.NewRow();
                            _row["ProductPackage_ID"] = ProductPackage_ID_History;
                            _row["ProductPackageName"] = _ProductPackageRowHistory.Name;
                            _row["ProductBrandName"] = brandName;
                            _row["SGTIN"] = _ProductPackageRowHistory.SGTIN;
                            dt.Rows.Add(_row);

                        }
                    }
                    //Response.Write(dt.Rows.Count);
                    if (dt.Rows.Count > 0)
                    {
                        rptPackage.DataSource = dt;
                        rptPackage.DataBind();
                        rptPackage1.DataSource = dt;
                        rptPackage1.DataBind();
                    }
                    else
                    {
                        rptPackage.DataSource = null;
                        rptPackage.DataBind();
                        rptPackage1.DataSource = null;
                        rptPackage1.DataBind();
                    }
                }
                //    }
                //}
                if (!_ProductPackageRow.IsProductBrand_IDNull)
                {
                    name = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRow.ProductBrand_ID).Name;
                }
            }

        }
    }
    protected void rptPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Repeater rptTaskHistory = e.Item.FindControl("rptTaskHistory") as Repeater;

            if (lblProductPackage_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
              where T.TaskType_ID=1 and T.Task_ID in (select Task_ID from Task where ProductPackage_ID=" + lblProductPackage_ID.Text + ") order by StartDate ASC");
                if (dt.Rows.Count > 0)
                {
                    rptTaskHistory.DataSource = dt;
                    rptTaskHistory.DataBind();
                }
            }
        }
    }
    protected void rptPackage1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Repeater rptTaskHistory = e.Item.FindControl("rptTaskHistory1") as Repeater;

            if (lblProductPackage_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
              where T.TaskType_ID=4 and T.Task_ID in (select Task_ID from Task where ProductPackage_ID=" + lblProductPackage_ID.Text + ") order by StartDate ASC");
                if (dt.Rows.Count > 0)
                {
                    rptTaskHistory.DataSource = dt;
                    rptTaskHistory.DataBind();
                }
            }
        }
    }
    protected void ResetMsg()
    {
    }


    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code=" + code, false);
    }
    protected string GetSGTIN()
    {
        string SGTIN = string.Empty;
        try
        {
            foreach (ListItem item in ddlSGTIN.Items)
            {
                if (item.Selected)
                {
                    SGTIN += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(SGTIN))
            {
                SGTIN = "," + SGTIN;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetSGTIN", ex.ToString());
        }
        return SGTIN;
    }
    private void LoadSGTIN()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductPackage_ID, SGTIN from ProductPackage where ProductPackage_ID<>" + ProductPackage_ID + " and  ProductBrand_ID=" + MyUser.GetProductBrand_ID() + "  order by ProductPackage_ID DESC");
            ddlSGTIN.DataSource = dt;
            ddlSGTIN.DataTextField = "SGTIN";
            ddlSGTIN.DataValueField = "ProductPackage_ID";
            ddlSGTIN.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void ddlSGTIN_SelectedIndexChanged(object sender, EventArgs e)
    {
        Init();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            _ProductPackageRow.SGTIN_History = GetSGTIN();
            BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
            lblMessage.Text = ("Cập nhật thành công !");
            lblMessage.Visible = true;
            Init();
        }
    }
}