using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackageVsTaskType : System.Web.UI.Page
{
    public string  namepackage,  code;
    public int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
      
        ResetMsg();
        if (!IsPostBack)
        {
            FillddlTaskType();
        }
        Init();
    }

 
    private void Init()
    {
        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow != null)
            {
                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                if (!IsPostBack)
                {
                    LoadTaskType(ProductPackage_ID);
                }

            }

        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible =  false;
    }



    private void FillddlTaskType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable("Status = 1", "Name ASC");
            ddlTaskType.DataSource = dt;
            ddlTaskType.DataTextField = "Name";
            ddlTaskType.DataValueField = "TaskType_ID";
            ddlTaskType.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLTestingCertificates", ex.ToString());
        }
    }

    protected string GetTaskType()
    {
        string Material_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlTaskType.Items)
            {
                if (item.Selected)
                {
                    Material_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Material_ID))
            {
                Material_ID = "," + Material_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("TaskType_ID", ex.ToString());
        }
        return Material_ID;
    }

    protected void LoadTableTaskType()
    {
        if (string.IsNullOrEmpty(GetTaskType()))
        {
            rptTaskType.DataSource = null;
            rptTaskType.DataBind();
            // tbl.Visible = false;
        }
        else
        {
            //tbl.Visible = true;
            string[] array = GetTaskType().Split(',');
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("TaskType_ID");
            dt.Columns.Add("Name");

            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int TaskType_ID = Convert.ToInt32(value);
                    TaskTypeRow _TaskTypeRow = BusinessRulesLocator.GetTaskTypeBO().GetByPrimaryKey(TaskType_ID);
                    string brandName = string.Empty;
                    if (_TaskTypeRow != null)
                    {
                        DataRow _row = dt.NewRow();
                        _row["TaskType_ID"] = TaskType_ID;
                        _row["Name"] = _TaskTypeRow.Name;

                        dt.Rows.Add(_row);
                    }
                }
            }
            //Response.Write(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                rptTaskType.DataSource = dt;
                rptTaskType.DataBind();
                Data1.Visible = true;

            }
        }
    }
    protected void LoadTaskType(int ProductPackage_ID)
    {
        DataTable dtTaskType = BusinessRulesLocator.Conllection().GetAllList(@"select * from ProductPackageVsTaskType PPT left join TaskType T on PPT.TaskType_ID = T.TaskType_ID Where PPT.ProductPackage_ID = " + ProductPackage_ID);
        if (dtTaskType.Rows.Count > 0)
        {
            Data1.Visible = true;
            rptTaskType.DataSource = dtTaskType;
            rptTaskType.DataBind();
            foreach (DataRow item in dtTaskType.Rows)
            {
                foreach (ListItem item1 in ddlTaskType.Items)
                {
                    if (item1.Value.ToString() == item["TaskType_ID"].ToString())
                    {
                        item1.Selected = true;
                    }
                }
            }
        }
        else
        {
            Data1.Visible = false;
            rptTaskType.DataSource = null;
            rptTaskType.DataBind();
        }
    }


    protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTableTaskType();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
       Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?", false);
      
    }

    protected void rptTaskType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnAddTaskType_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessRulesLocator.GetProductPackageVsTaskTypeBO().Delete(" ProductPackage_ID=" + ProductPackage_ID);
                foreach (RepeaterItem item in rptTaskType.Items)
                {

                    Literal lblTaskType_ID = item.FindControl("lblTaskType_ID") as Literal;

                    if (lblTaskType_ID != null)
                    {
                        ProductPackageVsTaskTypeRow _ProductPackageVsTaskTypeRow = new ProductPackageVsTaskTypeRow();
                        _ProductPackageVsTaskTypeRow.ProductPackage_ID = ProductPackage_ID;
                        _ProductPackageVsTaskTypeRow.TaskType_ID = Convert.ToInt32(lblTaskType_ID.Text);
                        BusinessRulesLocator.GetProductPackageVsTaskTypeBO().Insert(_ProductPackageVsTaskTypeRow);
                        lblMessage.Visible = true;
                        lblMessage.Text = "Thêm mới thành công!";
                    }

                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
}