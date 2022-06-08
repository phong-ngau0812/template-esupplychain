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

public partial class Quality_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadQuality();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadQuality()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" ", " Quality_ID DESC");
            rptQuality.DataSource = dt;
            rptQuality.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quality_Add.aspx", false);
    }

  

    protected void rptQuality_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
        switch (e.CommandName)
        {
            case "Delete":
                int Quality_ID = Convert.ToInt32(e.CommandArgument);
                 BusinessRulesLocator.GetQualityBO().DeleteByPrimaryKey(Quality_ID);
                lblMessage.Text = Common.GetSuccessMsg("Xóa bản ghi thành công !");
                lblMessage.Visible = true; ;
                LoadQuality();
                break;
            
        }
    }

  
}