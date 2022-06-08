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

public partial class TaskStepProduct_Setup : System.Web.UI.Page
{
    public string name = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtStart.Attributes.Add("readonly", "readonly");
        //txtEnd.Attributes.Add("readonly", "readonly");
        if (!string.IsNullOrEmpty(Request["TaskStep_ID"]))
            int.TryParse(Request["TaskStep_ID"].ToString(), out TaskStep_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        Init();
    }
    private void Init()
    {

        if (Product_ID != 0)
        {
            TaskStepRow _TaskStepRow = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID);
            name = _TaskStepRow.Name;
            if (!IsPostBack)
            {
                if (!_TaskStepRow.IsTypeNull)
                {
                    string[] array = _TaskStepRow.Type.Split(',');
                    foreach (string value in array)
                    {
                        foreach (ListItem item in ddlType.Items)
                        {
                            if (value == item.Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/Product/TaskStepProduct_List.aspx?Product_ID=" + Product_ID, false);
    }
    protected string GetType()
    {
        string Material_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlType.Items)
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
            Log.writeLog("GetFunctionGroup_ID", ex.ToString());
        }
        return Material_ID;
    }
    protected void btnAddMaterial_Click(object sender, EventArgs e)
    {
        try
        {
            TaskStepRow _TaskStepRow = BusinessRulesLocator.GetTaskStepBO().GetByPrimaryKey(TaskStep_ID);
            _TaskStepRow.Type = GetType();
            BusinessRulesLocator.GetTaskStepBO().Update(_TaskStepRow);
            lblMessage1.Text = "Cập nhật thành công";
            lblMessage1.Visible = true;
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
}