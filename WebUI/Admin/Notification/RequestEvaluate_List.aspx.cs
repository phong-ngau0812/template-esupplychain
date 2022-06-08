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

public partial class Admin_Notification_RequestEvaluate_List : System.Web.UI.Page
{
    int ProductCategory_ID = 0;
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 5;//Số bản ghi 1 trang
    private int productCategory_ID;
    public string Message = "";
    public string style = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["update"] != null)
            {

                lblMessage.Text = "Duyệt yêu cầu thay đổi thông tin doanh nghiệp thành công !";
                lblMessage.Visible = true;
            };
            LoadRequestEvaluate();
        }
        //   ResetMsg();
        //Admin_Template_CMS master = this.Master as Admin_Template_CMS;
        //if (master != null)
        //    master.LoadNotification();
    }

    private void LoadRequestEvaluate()
    {
        try
        {
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();

            dtSet = BusinessRulesLocator.Conllection().GetRequestEvaluaList_Paging(Pager1.CurrentIndex, pageSize, 7, Convert.ToInt32(MyUser.GetProductBrand_ID().ToString()));

            rptDanhgiaDN.DataSource = dtSet.Tables[0];
            rptDanhgiaDN.DataBind();

            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : true;
            }
            else
            {
                x_box_pager.Visible = false;
            }

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadRequestEvaluate", ex.ToString());
        }
    }


    protected void rptDanhgiaDN_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int Evaluate_ID = Convert.ToInt32(e.CommandArgument);
        try
        {
            switch (e.CommandName)
            {
                case "Duyet":
                    EvaluateRow _EvaluateRow = new EvaluateRow();
                    _EvaluateRow = BusinessRulesLocator.GetEvaluateBO().GetByPrimaryKey(Evaluate_ID);
                    if (_EvaluateRow != null)
                    {
                        _EvaluateRow.Active = 1;
                        BusinessRulesLocator.GetEvaluateBO().Update(_EvaluateRow);
                        //lblMessage.Text = ("Duyệt yêu cầu thêm mới sản phẩm thành công !");
                    }
                    break;
            }
            LoadRequestEvaluate();
            Admin_Template_CMS master = this.Master as Admin_Template_CMS;
            if (master != null)
                master.LoadNotification();
        }
        catch (Exception ex)
        {
            Log.writeLog("grdCouncil_ItemCommand", ex.ToString());
        }

    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadRequestEvaluate();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadRequestEvaluate();
    }
    public string Sosao(int Star)
    {
        string Chiso = string.Empty;
        switch (Star)
        {
            case 1:
                Chiso = "<img src='../../theme/assets/images/1.jpg'/>";
                break;
            case 2:
                Chiso = "<img src='../../theme/assets/images/2.jpg'/>";
                break;
            case 3:
                Chiso = "<img src='../../theme/assets/images/3.jpg'/>";
                break;
            case 4:
                Chiso = "<img src='../../theme/assets/images/4.jpg'/>";
                break;
            case 5:
                Chiso = "<img src='../../theme/assets/images/5.jpg'/>";
                break;
            default:
                Chiso = "";
                break;
        }
        return Chiso;
    }
}