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

public partial class TaskStepProductPackage_List : System.Web.UI.Page
{
    public string name = string.Empty;
    int Product_ID = 0;
    public int STT = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);

        LoadDataProduct();
        if (!IsPostBack)
        {
            LoadTaskStep();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadDataProduct()
    {
        if (Product_ID != 0)
        {
            ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
            if (_ProductRow != null)
            {
                name = _ProductRow.Name;
                //if (!_ProductRow.IsProductBrand_IDNull)
                //{
                //    MyActionPermission.CheckPermission(_ProductRow.ProductBrand_ID.ToString(), _ProductRow.CreateBy.ToString(), "/Admin/Product/Product_List");
                //}
            }
        }
    }

    protected void LoadTaskStep()
    {
        try
        {
            if (Product_ID != 0)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " Sort ASC, TaskStep_ID DESC");
                rptTaskStep.DataSource = dt;
                rptTaskStep.DataBind();
            }
            
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }


}