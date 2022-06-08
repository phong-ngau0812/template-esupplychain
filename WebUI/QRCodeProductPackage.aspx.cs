using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class QRCodeProductPackage : System.Web.UI.Page
{
    private int ProductPackageID = 0;
    public string info = string.Empty;
    public int NoQuestion = 1;
    public string PO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackageID"]))
            ProductPackageID = Convert.ToInt32(Request["ProductPackageID"].ToString());
        if (!IsPostBack)
        {
            LoadData();
            LoadInfo();
        }
    }
    protected void LoadInfo()
    {
        try
        {
            DataSet dtSet = BusinessRulesLocator.Conllection().GetProductPackageV3(1, 1, 1, ProductPackageID, 0, 0,0, 0, 0, 0, 0, 0, 0,0, "", new DateTime(2000, 01, 01), new DateTime(2099, 01, 01), "", "", 0 ,"", "", " LastEditDate DESC");
            rptProductPackage.DataSource = dtSet.Tables[0];
            rptProductPackage.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), "LoadData");
        }
    }
    public string ReturnStatus(string status)
    {
        string st = "";
        switch (status)
        {
            case "Đang sản xuất":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-success\">" + status + "</span>";
                break;
            case "Đang thu hoạch":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-primary\">" + status + "</span>";
                break;
            case "Thu hoạch xong":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-primary\">" + status + "</span>";
                break;
            case "Hủy":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-danger\">" + status + "</span>";
                break;
            case "Chưa kích hoạt":
                st = "<span style='font-size: 13px !important; padding: 5px; margin-top: 10px;' class=\"badge badge-warning\">" + status + "</span>";
                break;
            default:
                st = "";
                break;
        }

        return st;

    }
    protected void LoadData()
    {
        try
        {
            if (ProductPackageID > 0)
            {
                ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackageID);
                if (_ProductPackageRow != null)
                {
                    info = _ProductPackageRow.IsNameNull ? string.Empty : _ProductPackageRow.Name;
                    if (!_ProductPackageRow.IsCodePONull)
                    {
                        xuanhoa.Visible = true;
                        PO = _ProductPackageRow.CodePO;
                    }
                    else
                    {
                        xuanhoa.Visible = false;
                    }
                    lblCode.Text = _ProductPackageRow.IsCodeNull ? string.Empty : _ProductPackageRow.Code;
                    LoatDataTaskHistorySX(ProductPackageID);
                    LoatDataTaskHistoryVT(ProductPackageID);
                    LoatDataTaskHistoryTH(ProductPackageID);
                    LoatDataTaskHistoryCB(ProductPackageID);
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog(ex.ToString(), "LoadData");
        }
    }
    private void LoatDataTaskHistorySX(int ProductPackage_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.TaskStep_ID,T.ProductPackage_ID, T.CreateBy,T.Image,T.Description, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
        left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 1 order by T.StartDate ASC");
        if (dt.Rows.Count > 0)
        {
            rptSX.DataSource = dt;
            rptSX.DataBind();
            LoadAnswer();
        }
        else
        {
            rptSX.DataSource = null;
            rptSX.DataBind();
        }
    }
    private void LoatDataTaskHistoryVT(int ProductPackage_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select M.Unit, T.Task_ID,T.ProductPackage_ID,T.Description, T.StartDate,T.Image, T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , T.UserName, T.Name  from Task T 
left join Material M on M.Material_ID= T.Material_ID
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 2");

        if (dt.Rows.Count > 0)
        {
            rptVT.DataSource = dt;
            rptVT.DataBind();
        }
        else
        {
            rptVT.DataSource = null;
            rptVT.DataBind();
        }
    }
    private void LoatDataTaskHistoryTH(int ProductPackage_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select T.Name,T.Task_ID, T.Unit,T.ProductPackage_ID,T.Description, T.StartDate, T.Image,T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
from Task T  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID =3");
        rptTaskHistoryTH.DataSource = dt;
        rptTaskHistoryTH.DataBind();
    }
    private void LoatDataTaskHistoryCB(int ProductPackage_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Task_ID,T.ProductPackage_ID,T.Unit, T.CreateBy,T.Description, T.StartDate, T.Image,T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = 4");
        rptTaskHistoryCB.DataSource = dt;
        rptTaskHistoryCB.DataBind();
    }
    protected void rptSX_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblTaskStep_ID = e.Item.FindControl("lblTaskStep_ID") as Literal;

            Repeater rptQuestion = e.Item.FindControl("rptQuestion") as Repeater;
            if (lblTaskStep_ID != null && rptQuestion != null)
            {
                //DataTable dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable("TaskStep_ID=" + ddlTask.SelectedValue + " and  Type like '%,4,%'", "");
                //if (dt.Rows.Count == 1)
                //{
                //    divQuestion.Visible = true;
                DataTable dtQuestion = BusinessRulesLocator.GetTaskStepQuestionBO().GetAsDataTable("Active =1 and TaskStep_ID=" + lblTaskStep_ID.Text, "");
                if (dtQuestion.Rows.Count > 0)
                {
                    rptQuestion.DataSource = dtQuestion;
                    rptQuestion.DataBind();
                }
                //}
                //else
                //{
                //    divQuestion.Visible = false;
                //}
            }
        }

    }
    protected void LoadAnswer()
    {
        foreach (RepeaterItem item in rptSX.Items)
        {
            Literal lblTask_ID = item.FindControl("lblTask_ID") as Literal;
            Repeater rptQuestion = item.FindControl("rptQuestion") as Repeater;
            foreach (RepeaterItem itemAns in rptQuestion.Items)
            {
                CheckBoxList ckAnswer = itemAns.FindControl("ckAnswer") as CheckBoxList;
                foreach (ListItem itemAnswer in ckAnswer.Items)
                {
                    DataTable dt = BusinessRulesLocator.GetTaskBO().GetAsDataTable("Task_ID=" + lblTask_ID.Text + " and TaskStepAnswer_ID_List like '%," + itemAnswer.Value + ",%'", "");

                    if (dt.Rows.Count == 1)
                    {
                        itemAnswer.Selected = true;
                        itemAnswer.Enabled = false;
                    }
                    else
                    {
                        itemAnswer.Text = "";
                        itemAnswer.Attributes.Add("class", "none");
                    }
                }

            }
        }
    }
    protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblQuestionID = e.Item.FindControl("lblQuestionID") as Literal;
            CheckBoxList ckAnswer = e.Item.FindControl("ckAnswer") as CheckBoxList;
            if (lblQuestionID != null && ckAnswer != null)
            {
                DataTable dtAnswer = BusinessRulesLocator.GetTaskStepAnswerBO().GetAsDataTable("Active=1 and TaskStepQuestion_ID=" + lblQuestionID.Text, "");
                if (dtAnswer.Rows.Count > 0)
                {
                    ckAnswer.DataSource = dtAnswer;
                    ckAnswer.DataTextField = "Name";
                    ckAnswer.DataValueField = "TaskStepAnswer_ID";
                    ckAnswer.DataBind();
                }
            }

        }
    }
}