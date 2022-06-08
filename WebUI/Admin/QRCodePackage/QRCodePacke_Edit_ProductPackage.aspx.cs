using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class QRCodePacke_Edit_ProductPackage : System.Web.UI.Page
{

    public string NameProductPackageQR = "";
    public string AddressProductBrand = "";
    public string NameProductBrand = "";
    public string TelephoneProductBrand = "";
    public string EmailProductBrand = "";
    public string WebsiteProductBrand = "";
    public string NameProduct = "";
    private int ProductPackage_ID_Search = 0;
    private int ProductBrand_ID = 0;
    private int QRCodePackage_ID = 0;
    private string TaskType_ID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["QRCodePackage_ID"]))
            int.TryParse(Request["QRCodePackage_ID"].ToString(), out QRCodePackage_ID);
        if (!IsPostBack)
        {

            LoadQRCodePackageID();
            LoadDataProductBrand();
            FillDDLddlProductBrand();
            LoadDataProductPackage();
            //LoadDataTaskType();
            LoadListQRCodePackageVsProductPakage();

        }
        CheckCodeProductPackage();
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    private void LoadQRCodePackageID()
    {
        if (QRCodePackage_ID != 0)
        {
            QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
            _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
            if (_QRCodePackageRow != null)
            {
                NameProductPackageQR += _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;

            }
        }

    }

    protected bool CheckCodeProductPackage()
    {
        bool flag = false;
        string where = "1=1";
        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
        {
            where += " and Code =N'" + txtSearch.Text.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = true;
                ProductPackage_ID_Search = Convert.ToInt32(dt.Rows[0]["ProductPackage_ID"]);
                ProductBrand_ID = Convert.ToInt32(dt.Rows[0]["ProductBrand_ID"]);

                lblMessage.Text = "";
                lblMessage.Visible = false;
                Data.Visible = true;
            }
            else
            {
                lblMessage.Text = "Mã sản xuất không tồn tại trong hệ thống!";
                // lblMessage.BackColor = Color.Red;
                lblMessage.BackColor = Color.FromArgb(243, 91, 105);
                lblMessage.Visible = true;
                Data.Visible = false;
            }
        }

        return flag;
    }


    protected bool checkTaskType_ID()
    {
        bool flag = false;
        string where = "1=1";
        if (CheckCodeProductPackage())
        {
            if (ProductPackage_ID_Search != 0)
            {
                where += "and ProductPackage_ID = " + ProductPackage_ID_Search;
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.GetProductPackageVsTaskTypeBO().GetAsDataTable(where, "");
                if (dt.Rows.Count > 0)
                {

                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["TaskType_ID"]) != 0)
                            {
                                TaskType_ID += dt.Rows[i]["TaskType_ID"] + ",";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.writeLog("GetSGTIN", ex.ToString());
                    }
                    flag = true;
                }
            }
        }
        return flag;
    }

    protected void LoadDataTaskType()
    {
        if (checkTaskType_ID())
        {
            if (!string.IsNullOrEmpty(TaskType_ID))
            {
                string[] array = TaskType_ID.Split(',');

                foreach (string value in array)
                {
                    if (!string.IsNullOrEmpty(value))
                    {

                        switch (Convert.ToInt32(value))
                        {
                            case 1:
                                NKSX.Visible = true;
                                LoatDataTaskHistorySX(Convert.ToInt32(value));
                                break;
                            case 2:
                                NKVT.Visible = true;
                                LoatDataTaskHistoryVT(Convert.ToInt32(value));
                                break;
                            case 3:
                                NKTH.Visible = true;
                                LoatDataTaskHistoryTH(Convert.ToInt32(value));
                                break;
                            case 4:
                                NKSCCB.Visible = true;
                                LoatDataTaskHistoryCB(Convert.ToInt32(value));
                                break;
                            case 5:
                                NKVC.Visible = true;
                                LoatDataTaskHistoryVC(Convert.ToInt32(value));
                                break;
                            case 6:
                                NKBH.Visible = true;
                                LoatDataTaskHistoryBH(Convert.ToInt32(value));
                                break;
                            default:
                                // code block
                                break;
                        }

                    }
                }
            }

            DataTask.Visible = true;
        }
        else
        {
            DataTask.Visible = false;
            lblMessage.Text = "Không tìm thấy dữ liệu nhật ký trên hệ thống!";
            lblMessage.Visible = true;
        }
        //else
        //{
        //    lblMessage.Text = "Mã SGTIN không tồn tại trong hệ thống!";
        //    lblMessage.Visible = true;
        //}
    }

    private void LoatDataTaskHistorySX(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 

        left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);
        if (dt.Rows.Count > 0)
        {
            rptTaskHistorySX.DataSource = dt;
            rptTaskHistorySX.DataBind();
        }
        else
        {
            rptTaskHistorySX.DataSource = null;
            rptTaskHistorySX.DataBind();
        }

    }
    private void LoatDataTaskHistoryVT(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select M.Unit, T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , T.UserName, T.Name  from Task T 

left join Material M on M.Material_ID= T.Material_ID
  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);

        if (dt.Rows.Count > 0)
        {
            rptTaskHistoryVT.DataSource = dt;
            rptTaskHistoryVT.DataBind();
        }
        else
        {
            rptTaskHistoryVT.DataSource = null;
            rptTaskHistoryVT.DataBind();
        }
    }
    private void LoatDataTaskHistoryTH(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"select T.Name,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
from Task T 

  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryTH.DataSource = dt;
        rptTaskHistoryTH.DataBind();
    }
    private void LoatDataTaskHistoryCB(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         
         left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryCB.DataSource = dt;
        rptTaskHistoryCB.DataBind();
    }
    private void LoatDataTaskHistoryVC(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryVC.DataSource = dt;
        rptTaskHistoryVC.DataBind();
    }
    private void LoatDataTaskHistoryBH(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = " + ProductPackage_ID_Search + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryBH.DataSource = dt;
        rptTaskHistoryBH.DataBind();

    }

    protected void LoadDataProductPackage()
    {
        string where = "1=1";
        if (ProductPackage_ID_Search != 0)
        {
            where += "and P.ProductPackage_ID =" + ProductPackage_ID_Search;

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select P.ProductPackage_ID , P.Name , P.Code ,P.CreateDate, Z.Name as ZoneName ,F.Name as FarmName ,A.Name as AreaName , W.Name as WorkshopName
  from ProductPackage p
  left join Zone Z on P.Zone_ID = Z.Zone_ID
  left join Area A on P.Area_ID = A.Area_ID
  left join Farm F on P.Farm_ID = F.Farm_ID
  left join Workshop W on P.Workshop_ID = W.Workshop_ID
  where " + where);
            rptProductPackage.DataSource = dt;
            rptProductPackage.DataBind();
        }
        else
        {
            rptProductPackage.DataSource = null;
            rptProductPackage.DataBind();
        }
    }



    protected void LoadDataProductBrand()
    {
        string where = "1=1";

        if (ProductBrand_ID != 0)
        {
            where += " and PB.ProductBrand_ID = " + ProductBrand_ID;
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  PB.ProductBrand_ID , PB.Name , PB.Address, PB.Telephone ,PB.Email  ,PB.Website , L.Name as NameLocation , D.Name as NameDistrict , W.Name as NameWard
  from ProductBrand PB
  left join Location L on PB.Location_ID = L.Location_ID
  left join District D on PB.District_ID = D.District_ID
  left join Ward W on PB.Ward_ID = W.Ward_ID
  where " + where);
            rptProductBrand.DataSource = dt;
            rptProductBrand.DataBind();
        }
    }

    protected bool CheckQRCodePackageVsProductPakage()
    {
        bool flag = false;
        string where = "1=1";
        if (QRCodePackage_ID != 0 && ProductPackage_ID_Search != 0)
        {
            where += " and QRCodePackage_ID =" + QRCodePackage_ID;
            where += " and ProductPackage_ID =" + ProductPackage_ID_Search;
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQRCodePackageVsProductPakageBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = true;
                lblMessage.Text = "";
                lblMessage.Visible = false;

            }
            else
            {
                lblMessage.Text = "Lô sản xuất đã đã được gán cho lô mã "+ NameProductPackageQR + "!";
                // lblMessage.BackColor = Color.Red;
                lblMessage.BackColor = Color.FromArgb(243, 91, 105);
                lblMessage.Visible = true;

            }
        }
        return flag;
    }




    protected void UpdateQRCodeProductPackage()
    {
        if (!CheckQRCodePackageVsProductPakage())
        {
            if (QRCodePackage_ID != 0 && ProductPackage_ID_Search != 0)
            {
                QRCodePackageVsProductPakageRow _QRCodePackageVsProductPakageRow = new QRCodePackageVsProductPakageRow();
                _QRCodePackageVsProductPakageRow.QRCodePackage_ID = QRCodePackage_ID;
                _QRCodePackageVsProductPakageRow.ProductPackage_ID = ProductPackage_ID_Search;
                BusinessRulesLocator.GetQRCodePackageVsProductPakageBO().Insert(_QRCodePackageVsProductPakageRow);
                LoadQRCodePackageID();
                LoadDataProductBrand();
                LoadDataProductPackage();
                LoadDataTaskType();
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.BackColor = Color.FromArgb(103, 245, 191);
                lblMessage.Visible = true;
                LoadListQRCodePackageVsProductPakage();
                Data.Visible = false;

            }
        }
        else
        {
            if (QRCodePackage_ID != 0)
            {
                QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
                _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
                if (_QRCodePackageRow != null)
                {
                    NameProductPackageQR += _QRCodePackageRow.IsNameNull ? string.Empty : _QRCodePackageRow.Name;

                }
            }
            lblMessage.Text = "Lô sản xuất đã được gán cho lô mã "+NameProductPackageQR+" !";
            // lblMessage.BackColor = Color.Red;
            lblMessage.BackColor = Color.FromArgb(243, 91, 105);
            lblMessage.Visible = true;
        }

        //QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        //_QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
        //if(_QRCodePackageRow != null)
        //{
        //    if(ProductPackage_ID_Search != 0)
        //    {
        //        _QRCodePackageRow.ProductPackage_ID = ProductPackage_ID_Search;
        //        _QRCodePackageRow.LastEditBy = MyUser.GetUser_ID();
        //        _QRCodePackageRow.LastEditDate = DateTime.Now;
        //        BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
        //        LoadQRCodePackageID();
        //        LoadDataProductBrand();
        //        LoadDataProductPackage();
        //        LoadDataTaskType();
        //        lblMessage.Text = "Cập nhật thông tin thành công!";
        //        lblMessage.Visible = true;
        //    }
        //}
    }


    protected void LoadListQRCodePackageVsProductPakage()
    {
        string where = "1=1";
        if (QRCodePackage_ID != 0)
        {
            where += "and QRCodePackage_ID =" + QRCodePackage_ID;
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select *
  from QRCodePackageVsProductPakage
  where " + where);
            if (dt.Rows.Count > 0)
            {
                LoadListQRCode.Visible = true;
                rptList.DataSource = dt;
                rptList.DataBind();
            }
            else
            {
                LoadListQRCode.Visible = false;
                rptList.DataSource = null;
                rptList.DataBind();
            }

        }
    }


    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (lblProductPackage_ID != null)
            {
                //load data ProductBrand
                Repeater rptProductBrandlist = (Repeater)e.Item.FindControl("rptProductBrandlist");
                int GetProductBrand_ID = 0;
                ProductPackageRow _ProductPackageRow = new ProductPackageRow();
                _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(lblProductPackage_ID.Text));
                if (_ProductPackageRow != null)
                {
                    GetProductBrand_ID = _ProductPackageRow.IsProductBrand_IDNull ? 0 : _ProductPackageRow.ProductBrand_ID;
                }
                string where = "1=1";

                if (GetProductBrand_ID != 0)
                {
                    where += " and PB.ProductBrand_ID = " + GetProductBrand_ID;
                    DataTable dt = new DataTable();
                    dt = BusinessRulesLocator.Conllection().GetAllList(@"select  PB.ProductBrand_ID , PB.Name , PB.Address, PB.Telephone ,PB.Email  ,PB.Website , L.Name as NameLocation , D.Name as NameDistrict , W.Name as NameWard
  from ProductBrand PB
  left join Location L on PB.Location_ID = L.Location_ID
  left join District D on PB.District_ID = D.District_ID
  left join Ward W on PB.Ward_ID = W.Ward_ID
  where " + where);
                    rptProductBrandlist.DataSource = dt;
                    rptProductBrandlist.DataBind();
                }

                // load data ProductPackge
                Repeater rptProductPackgelist = (Repeater)e.Item.FindControl("rptProductPackgelist");
                string whereProductPackge = "1=1";
                whereProductPackge += "and P.ProductPackage_ID =" + Convert.ToInt32(lblProductPackage_ID.Text);
                DataTable dtPP = new DataTable();
                dtPP = BusinessRulesLocator.Conllection().GetAllList(@"select P.ProductPackage_ID , P.Name , P.Code ,P.CreateDate, Z.Name as ZoneName ,F.Name as FarmName ,A.Name as AreaName , W.Name as WorkshopName
  from ProductPackage p
  left join Zone Z on P.Zone_ID = Z.Zone_ID
  left join Area A on P.Area_ID = A.Area_ID
  left join Farm F on P.Farm_ID = F.Farm_ID
  left join Workshop W on P.Workshop_ID = W.Workshop_ID
  where " + whereProductPackge);
                rptProductPackgelist.DataSource = dtPP;
                rptProductPackgelist.DataBind();

                // load Task 

                HtmlGenericControl DataTask2 = e.Item.FindControl("DataTask2") as HtmlGenericControl;
                string whereTask = "1=1";
                string _TaskType_ID = string.Empty;
                whereTask += "and ProductPackage_ID = " + Convert.ToInt32(lblProductPackage_ID.Text);
                DataTable dtTask = new DataTable();
                dtTask = BusinessRulesLocator.GetProductPackageVsTaskTypeBO().GetAsDataTable(whereTask, "");
                if (dtTask.Rows.Count > 0)
                {

                    try
                    {
                        for (int i = 0; i < dtTask.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtTask.Rows[i]["TaskType_ID"]) != 0)
                            {
                                _TaskType_ID += dtTask.Rows[i]["TaskType_ID"] + ",";

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.writeLog("GetSGTIN", ex.ToString());
                    }

                }
                //HtmlAnchor li = e.Item.FindControl("A1") as HtmlAnchor;
                //li.ID = "li1";
                //li.ClientIDMode = ClientIDMode.Static;
               

                if (!string.IsNullOrEmpty(_TaskType_ID))
                {
                    string[] array = _TaskType_ID.Split(',');

                    foreach (string value in array)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {

                            switch (Convert.ToInt32(value))
                            {

                                case 1:
                                    
                                    //HtmlAnchor li1 = e.Item.FindControl("li1"+lblProductPackage_ID.Text) as HtmlAnchor;
                                    //li1.Visible = true;
                                    Repeater rptNTSX1 = (Repeater)e.Item.FindControl("rptNTSX1");
                                    DataTable dtNKSX = new DataTable();
                                    dtNKSX = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 

  left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNTSX1.DataSource = dtNKSX;
                                    rptNTSX1.DataBind();
                                    break;
                                case 2:
                                    //HtmlGenericControl li2 = e.Item.FindControl("li2") as HtmlGenericControl;
                                    //li2.Visible = true;
                                    Repeater rptNKVT1 = (Repeater)e.Item.FindControl("rptNKVT1");
                                    DataTable dtvt = new DataTable();
                                    dtvt = BusinessRulesLocator.Conllection().GetAllList(@" select M.Unit, T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName, T.Quantity, T.Price , T.UserName, T.Name  from Task T 

left join Material M on M.Material_ID= T.Material_ID
  where T.ProductPackage_ID = " + Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNKVT1.DataSource = dtvt;
                                    rptNKVT1.DataBind();
                                    break;
                                case 3:
                                    //HtmlGenericControl li3 = e.Item.FindControl("li3") as HtmlGenericControl;
                                    //li3.Visible = true;
                                    Repeater rptNKTH = (Repeater)e.Item.FindControl("rptNKTH");
                                    DataTable dtTH = new DataTable();
                                    dtTH = BusinessRulesLocator.Conllection().GetAllList(@"select T.Name,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
from Task T 
  where T.ProductPackage_ID = " + Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNKTH.DataSource = dtTH;
                                    rptNKTH.DataBind();
                                    break;
                                case 4:
                                    //HtmlGenericControl li4 = e.Item.FindControl("li4") as HtmlGenericControl;
                                    //li4.Visible = true;
                                    Repeater rptNKSC1 = (Repeater)e.Item.FindControl("rptNKSC1");
                                    DataTable dtCB = new DataTable();
                                    dtCB = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 

         left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " +Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNKSC1.DataSource = dtCB;
                                    rptNKSC1.DataBind();
                                    break;
                                case 5:
                                    //HtmlGenericControl li5 = e.Item.FindControl("li5") as HtmlGenericControl;
                                    //li5.Visible = true;
                                    Repeater rptNKVC1 = (Repeater)e.Item.FindControl("rptNKVC1");
                                    DataTable dtVC = new DataTable();
                                    dtVC = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = "+Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNKVC1.DataSource = dtVC;
                                    rptNKVC1.DataBind();
                                    break;
                                case 6:
                                    //HtmlGenericControl li6 = e.Item.FindControl("li6") as HtmlGenericControl;
                                    //li6.Visible = true;
                                    Repeater rptNKBH1 = (Repeater)e.Item.FindControl("rptNKBH1");
                                    DataTable dtBH = new DataTable();
                                    dtBH = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = " + Convert.ToInt32(lblProductPackage_ID.Text) + " and T.TaskType_ID = " + value);
                                    rptNKBH1.DataSource = dtBH;
                                    rptNKBH1.DataBind();
                                    break;
                                default:
                                    // code block
                                    break;
                            }

                        }
                    }
                    DataTask2.Visible = true;
                }
                else
                {
                    DataTask2.Visible = false;
                }
            }
        }
    }


    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ProductPackage_ID = Convert.ToInt32(e.CommandArgument);
        QRCodePackageVsProductPakageRow _QRCodePackageVsProductPakageRow = new QRCodePackageVsProductPakageRow();
        _QRCodePackageVsProductPakageRow = BusinessRulesLocator.GetQRCodePackageVsProductPakageBO().GetByPrimaryKey(QRCodePackage_ID, ProductPackage_ID);
        switch (e.CommandName)
        {
            case "Delete":
                if (_QRCodePackageVsProductPakageRow != null)
                {
                    BusinessRulesLocator.GetQRCodePackageVsProductPakageBO().DeleteByPrimaryKey(QRCodePackage_ID, ProductPackage_ID);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                    lblMessage.Visible = true;
                }
                break;
        }
        LoadListQRCodePackageVsProductPakage();
        LoadQRCodePackageID();
        LoadDataProductBrand();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateQRCodeProductPackage();

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_List.aspx", false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckCodeProductPackage();
        LoadQRCodePackageID();
        LoadDataProductBrand();
        LoadDataProductPackage();
        LoadDataTaskType();
    }

}