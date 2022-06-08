using System;
using System.Collections.Generic;
using DbObj;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using SystemFrameWork;

public partial class LookUpInformationTask : System.Web.UI.Page
{
    private int ProductPackage_ID = 0;
    private string ListProductPackage_ID = string.Empty;
    protected string NameNK = "Nhật ký thu hoạch";

    public string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataTaskTypeES();
        }
        if (Common.CheckUserXuanHoa1())
        {
            NKSCCB.Visible = false;
            NameNK = "Đóng gói thành phẩm";
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;

        //hide();
    }

    //protected void hide()
    //{
    //    NKSX.Visible = false;
    //    NKVT.Visible = false;
    //    NKTH.Visible = false;
    //    NKSCCB.Visible = false;
    //    NKBH.Visible = false;
    //}

    protected bool CheckCodeSGTIN()
    {
        bool flag = false;
        string where = "1=1";
        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
        {
            where += " and SGTIN=N'" + txtSearch.Text.Trim() + "'" + " OR CodePO=N'" + txtSearch.Text.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = true;
                ProductPackage_ID = Convert.ToInt32(dt.Rows[0]["ProductPackage_ID"]);
                title = "Thông tin nhật ký lô: " + dt.Rows[0]["Name"].ToString();
                Data.Visible = true;
                lblMessage.Text = "";
                lblMessage.Visible = false;
            }
            else
            {
                Data.Visible = false;
                lblMessage.Text = "Mã SGTIN không tồn tại trong hệ thống!";
                lblMessage.Visible = true;
            }
        }

        return flag;
    }


    string TaskType_ID = string.Empty;


    protected bool checkTaskType_ID()
    {
        bool flag = false;
        string where = "1=1";
        if (CheckCodeSGTIN())
        {
            if (ProductPackage_ID != 0)
            {
                where += "and ProductPackage_ID = " + ProductPackage_ID;
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




    protected void LoadDataTaskTypeES()
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

            Data.Visible = true;
        }
        else
        {
            Data.Visible = false;
            lblMessage.Text = "Không tìm thấy dữ liệu trên hệ thống!";
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
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);
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
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);

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

  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryTH.DataSource = dt;
        rptTaskHistoryTH.DataBind();
    }
    private void LoatDataTaskHistoryCB(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
         
         left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryCB.DataSource = dt;
        rptTaskHistoryCB.DataBind();
    }
    private void LoatDataTaskHistoryVC(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryVC.DataSource = dt;
        rptTaskHistoryVC.DataBind();
    }
    private void LoatDataTaskHistoryBH(int TaskType_ID)
    {
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name  from Task T 

  where T.ProductPackage_ID = " + ProductPackage_ID + " and T.TaskType_ID = " + TaskType_ID);
        rptTaskHistoryBH.DataSource = dt;
        rptTaskHistoryBH.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
        {
            where += " and SGTIN=N'" + txtSearch.Text.Trim() + "'" + " OR CodePO=N'" + txtSearch.Text.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "");
            if (dt.Rows.Count == 1)
            {
                LoadDataTaskTypeES();
            }
            else if (dt.Rows.Count > 1)
            {
                es.Visible = false;
                LoadDataTaskXuanHoa();
            }
            else
            {
                Data.Visible = false;
                lblMessage.Text = "Mã SGTIN OR PO không tồn tại trong hệ thống!";
                lblMessage.Visible = true;
            }
        }


    }



    #region XuanHoa     
    protected void LoadDataTaskXuanHoa()
    {
        string where = "1=1";
        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
        {
            where += " and SGTIN=N'" + txtSearch.Text.Trim() + "'" + " OR CodePO=N'" + txtSearch.Text.Trim() + "' and ProductPackageStatus_ID <> 6";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["ProductPackage_ID"]) != 0)
                    {
                        ListProductPackage_ID += dt.Rows[i]["ProductPackage_ID"] + ",";
                    }
                }
                LoadHtml();

            }
        }
    }
    protected void LoadHtml()
    {
        string activeProductPackage;
        string activeTaskType = string.Empty;
        int CountProductPackage = 1;
        int CountTaskType = 1;
        string html = string.Empty;
        html += @" <div class='row' runat='server' id='Div1'>
                                            <div class='col-12'>
                                                <div class='card'>
                                                    <div class='card-body'>
                                                        <ul class='nav nav-tabs' role='tablist'>";
        if (ListProductPackage_ID != "")
        {
            string[] ProductPackageIDCast = ListProductPackage_ID.Trim().Split(',');
            foreach (var item in ProductPackageIDCast)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(item));
                    if (_ProductPackageRow != null)
                    {
                        html += @"
                                                            <li class='nav-item' runat='server' id='Li+" + _ProductPackageRow.ProductPackage_ID + @"'>
                                                            
                                                                <a class='nav-link' data-toggle='tab' href='#tab" + _ProductPackageRow.ProductPackage_ID + @"' role='tab'>" + _ProductPackageRow.Name + @"</a>
                                                            </li>";
                    }
                }
            }
        }
        html += @"
                                                        </ul>
                                                        <!-- Tab panes -->
                                                        <div class='tab-content'>";

        if (ListProductPackage_ID != "")
        {

            string[] ProductPackageIDCast = ListProductPackage_ID.Trim().Split(',');
            foreach (var item in ProductPackageIDCast)
            {
                if (item != "")
                {
                    ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(Convert.ToInt32(item));
                    if (_ProductPackageRow != null)
                    {

                        if (CountProductPackage == 1)
                        {
                            activeProductPackage = "active";
                        }
                        else
                        {
                            activeProductPackage = "";
                        }
                        html += @"

                                                            <div class='tab-pane mt-3 " + activeProductPackage + @"' id='tab" + _ProductPackageRow.ProductPackage_ID + @"' role='tabpanel'>
                                                                <div class='form-group'>
                                                                    <h5>Thông tin nhật ký lô: " + _ProductPackageRow.Name + @"</h5>
                                                                </div>
                                                                <div class='row' runat='server' id='Div2'>
                                                                    <div class='col-12'>
                                                                        <div class='card'>
                                                                            <div class='card-body'>";

                        html += @"
                                                                                <ul class='nav nav-tabs' role='tablist'>";
                        string ListTaskType_IDXH = string.Empty;
                        //DataTable dtTaskType = BusinessRulesLocator.GetProductPackageVsTaskTypeBO().GetAsDataTable(" 1=1 and ProductPackage_ID = " + _ProductPackageRow.ProductPackage_ID, " TaskType_ID ASC");
                        DataTable dtTaskType = BusinessRulesLocator.Conllection().GetAllList(@"Select PT.*,T.Name from ProductPackageVsTaskType PT left join TaskType T on PT.TaskType_ID = T.TaskType_ID where 1 = 1 and T.TaskType_ID not in (4) and PT.ProductPackage_ID = " + _ProductPackageRow.ProductPackage_ID + " order by PT.TaskType_ID ASC");
                        if (dtTaskType.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTaskType.Rows.Count; i++)
                            {
                                ListTaskType_IDXH += dtTaskType.Rows[i]["TaskType_ID"].ToString() + ",";
                                html += @"
                                                                                    <li class='nav-item' runat='server' id='Li" + dtTaskType.Rows[i]["ProductPackage_ID"] + dtTaskType.Rows[i]["TaskType_ID"] + @"'>
                                                                                        <a class='nav-link' data-toggle='tab' href='#tab" + dtTaskType.Rows[i]["ProductPackage_ID"] + dtTaskType.Rows[i]["TaskType_ID"] + @"' role='tab'>" + ChangeName(dtTaskType.Rows[i]["Name"].ToString()) + @"</a>
                                                                                    </li>";
                            }

                        }
                        html += @"                                        
                                                                                </ul>

                                                                                <!-- Tab panes -->
                                                                                <div class='tab-content'>";
                        if (!string.IsNullOrEmpty(ListTaskType_IDXH))
                        {
                            string[] arrayT = ListTaskType_IDXH.Split(',');

                            foreach (string valueT in arrayT)
                            {
                                if (valueT != "")
                                {

                                    switch (Convert.ToInt32(valueT))
                                    {
                                        case 1:
                                            html += LoadNKSXXH(_ProductPackageRow.ProductPackage_ID, Convert.ToInt32(valueT));
                                            break;
                                        case 2:
                                            html += LoadNKVTXH(_ProductPackageRow.ProductPackage_ID, Convert.ToInt32(valueT));
                                            break;
                                        case 3:
                                            html += LoadNKTHXH(_ProductPackageRow.ProductPackage_ID, Convert.ToInt32(valueT));
                                            break;
                                        case 5:
                                            html += LoadNKVCXH(_ProductPackageRow.ProductPackage_ID, Convert.ToInt32(valueT));
                                            break;
                                        case 6:
                                            html += LoadNKBHXH(_ProductPackageRow.ProductPackage_ID, Convert.ToInt32(valueT));
                                            break;
                                        default:
                                            // code block
                                            break;
                                    }


                                }
                            }

                        }

                        html += @"
                                                                                        </div>
                                                                                    </div>
                                                                                    <!--end card-body-->
                                                                                </div>
                                                                                <!--end card-->
                                                                            </div>
                                                                            <!--end col-->
                                                                        </div>
                                                                    </div>
                                                               ";

                    }
                }
                CountProductPackage++;

            }

        }
        html += @"        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>";

        htmlXH.Text = html;
    }
    protected string LoadNKSXXH(int ProductPackageSX, int TaskTypeSX)
    {
        string values = string.Empty;

        values += @"   <div class='tab-pane mt-3' id='tab" + ProductPackageSX + TaskTypeSX + @"' role='tabpanel'>

                                                                                        <table class='table table-responsive table-striped table-bordered dt-responsive nowrap' style='border-collapse: collapse; border-spacing: 0; width: 100%;'>
                                                                                            <thead>
    <tr>
                                                                                                    <th>Đầu mục công việc</th>
                                                                                                    <th>Người thực hiện</th>
                                                                                                    <th>Vị trí</th>
                                                                                                    <th>Ngày thực hiện</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>";

        DataTable dtSX = new DataTable();
        dtSX = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 

        left join aspnet_Users U on U.UserId= T.CreateBy
  where T.ProductPackage_ID = " + ProductPackageSX + " and T.TaskType_ID = " + TaskTypeSX);


        if (dtSX.Rows.Count > 0)
        {
            for (int i = 0; i < dtSX.Rows.Count; i++)
            {
                values += @"<tr>
                                    <td>" + dtSX.Rows[i]["Name"].ToString() + @"</td>
                                    <td>" + dtSX.Rows[i]["UserName"].ToString() + @"</td>
                                     <td><i class='dripicons-location font-14'></i>" + dtSX.Rows[i]["Location"].ToString() + @"</td>
                                    <td>" + DateTime.Parse(dtSX.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy") + @"</td>
                            </tr>";
            }
        }




        values += @"                                                                                     </tbody>
 </table>
                                                                                    </div>";



        return values;
    }

    protected string LoadNKVTXH(int ProductPackageVT, int TaskTypeVT)
    {
        string values = string.Empty;

        values += @"   <div class='tab-pane mt-3' id='tab" + ProductPackageVT + TaskTypeVT + @"' role='tabpanel'>

                                                                                        <table class='table table-responsive table-striped table-bordered dt-responsive nowrap' style='border-collapse: collapse; border-spacing: 0; width: 100%;'>
                                                                                            <thead>
    <tr>
                                                                                                      <th>Tên vật tư</th>
                                                                            <th>Người thực hiện</th>
                                                                            <th>Ngày thực hiện</th>
                                                                            <th width='5%'>Số lượng</th>
                                                                            <th width='5%' class='none'>Đơn giá</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>";

        DataTable dtVT = new DataTable();
        dtVT = BusinessRulesLocator.Conllection().GetAllList(@"Select WE.Name,WE.WarehouseExport_ID, WH.Name as NameWareHouse,M.Name as NameMaterial,M.CodePrivate,S.Name as NameNCC,WI.CodeMaterialPackage,WE.CreateDate,U.UserName,WE.Exporter,WM.Amount,M.Unit from WarehouseExportMaterial WM left join WarehouseExport WE on WM.WarehouseExport_ID = WE.WarehouseExport_ID 
 left join aspnet_Users U on U.UserId=WE.CreateBy
  left join WarehouseImport WI on WM.WarehouseImport_ID = WI.WarehouseImport_ID   
 left join Warehouse WH on WI.Warehouse_ID = WH.Warehouse_ID 
 left join Material M on WM.Material_ID = M.Material_ID
  left join Supplier S on S.Supplier_ID = WI.Supplier_ID
 where M.Active=1 and WE.Active=1 and WM.WarehouseExportMaterial_ID in (Select WarehouseExportMaterial_ID from ProductPackageVsMaterial where ProductPackage_ID = " + ProductPackageVT + ") ");


        if (dtVT.Rows.Count > 0)
        {
            for (int i = 0; i < dtVT.Rows.Count; i++)
            {
                values += @"<tr>
                                    <td>" + dtVT.Rows[i]["NameMaterial"].ToString() + @"</td>
                                    <td>" + dtVT.Rows[i]["Exporter"].ToString() + @"
</td>
                                     <td>" + DateTime.Parse(dtVT.Rows[i]["CreateDate"].ToString()).ToString("dd/MM/yyyy") + @"</td>
                                    <td>" + Decimal.Parse(dtVT.Rows[i]["Amount"].ToString()).ToString("N0") + " " + dtVT.Rows[i]["Unit"] + @"</td>
                            </tr>";
            }
        }




        values += @"                                                                                     </tbody>
 </table>
                                                                                    </div>";



        return values;
    }
    protected string LoadNKTHXH(int ProductPackageTH, int TaskTypeTH)
    {
        string values = string.Empty;

        values += @"   <div class='tab-pane mt-3' id='tab" + ProductPackageTH + TaskTypeTH + @"' role='tabpanel'>

                                                                                        <table class='table table-responsive table-striped table-bordered dt-responsive nowrap' style='border-collapse: collapse; border-spacing: 0; width: 100%;'>
                                                                                            <thead>
    <tr>
                                                                                                          <th>Nội dung đóng gói thành phẩm </th>
                                                                            <th>Số lượng đóng gói thành phẩm</th>
                                                                            <th>Số ngày đóng gói thành phẩm còn lại</th>
                                                                            <th>Ngày đóng gói thành phẩm</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>";


        DataTable dtTH = new DataTable();
        dtTH = BusinessRulesLocator.Conllection().GetAllList(@"select T.Name,T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.HarvestDayRemain , T.HarvestVolume  
from Task T 

  where T.ProductPackage_ID = " + ProductPackageTH + " and T.TaskType_ID = " + TaskTypeTH);


        if (dtTH.Rows.Count > 0)
        {
            for (int i = 0; i < dtTH.Rows.Count; i++)
            {
                values += @"<tr>
                                    <td>" + dtTH.Rows[i]["Name"].ToString() + @"</td>
                                    <td>" + Decimal.Parse(dtTH.Rows[i]["HarvestVolume"].ToString()).ToString("N0") + @"</td>
                                     <td>" + dtTH.Rows[i]["HarvestDayRemain"].ToString() + @"</td>
                                    <td>" + DateTime.Parse(dtTH.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy") + @"</td>
                            </tr>";
            }
        }




        values += @"                                                                                     </tbody>
 </table>
                                                                                    </div>";



        return values;
    }
    protected string LoadNKVCXH(int ProductPackageVC, int TaskTypeVC)
    {
        string values = string.Empty;

        values += @"   <div class='tab-pane mt-3' id='tab" + ProductPackageVC + TaskTypeVC + @"' role='tabpanel'>

                                                                                        <table class='table table-responsive table-striped table-bordered dt-responsive nowrap' style='border-collapse: collapse; border-spacing: 0; width: 100%;'>
                                                                                            <thead>
    <tr>
                                                                                                             <th>Nội dung vận chuyển </th>
                                                                            <th>Người giao</th>
                                                                            <th>Nhà vận chuyển</th>
                                                                            <th>Ngày vận chuyển</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>";


        DataTable dtVC = new DataTable();
        dtVC = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName , T.BuyerName , T.UserName, T.Name,TP.Name as TransporterName  from Task T left join Transporter TP on T.Transporter_ID =TP.Transporter_ID

  where T.ProductPackage_ID = " + ProductPackageVC + " and T.TaskType_ID = " + TaskTypeVC);



        if (dtVC.Rows.Count > 0)
        {
            for (int i = 0; i < dtVC.Rows.Count; i++)
            {
                values += @"<tr>
                                    <td>" + dtVC.Rows[i]["Name"].ToString() + @"</td>
                                 <td>" + dtVC.Rows[i]["UserName"].ToString() + @"
  <br /><i class='dripicons-location font-14'></i>" + dtVC.Rows[i]["Location"].ToString() + @"
</td>
                                     <td>" + dtVC.Rows[i]["TransporterName"].ToString() + @"</td>
                                    <td>" + DateTime.Parse(dtVC.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy") + @"</td>
                            </tr>";
            }
        }




        values += @"                                                                                     </tbody>
 </table>
                                                                                    </div>";



        return values;
    }


    protected string LoadNKBHXH(int ProductPackageBH, int TaskTypeBH)
    {
        string values = string.Empty;

        values += @"   <div class='tab-pane mt-3' id='tab" + ProductPackageBH + TaskTypeBH + @"' role='tabpanel'>

                                                                                        <table class='table table-responsive table-striped table-bordered dt-responsive nowrap' style='border-collapse: collapse; border-spacing: 0; width: 100%;'>
                                                                                            <thead>
    <tr>  <th>Nội dung bán hàng </th>
                                                                            <th>Người bán</th>
                                                                            <th>Người mua</th>
                                                                            <th>Ngày bán</th>
                                                                            <th>Số Container</th>
                                                                            <th>Số Chì</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>";


        DataTable dtBH = new DataTable();
        dtBH = BusinessRulesLocator.Conllection().GetAllList(@" select T.Task_ID,T.ProductPackage_ID, T.StartDate, T.Location, T.ProductPackageName ,T.ShopName, T.BuyerName , T.UserName, T.Name,T.KeyContainer,T.SoChi  from Task T 

  where T.ProductPackage_ID = " + ProductPackageBH + " and T.TaskType_ID = " + TaskTypeBH);



        if (dtBH.Rows.Count > 0)
        {
            for (int i = 0; i < dtBH.Rows.Count; i++)
            {
                values += @"<tr>
                                    <td>" + dtBH.Rows[i]["Name"].ToString() + @"</td>
                                 <td>" + dtBH.Rows[i]["UserName"].ToString() + @"
  <br /><i class='dripicons-location font-14'></i>" + dtBH.Rows[i]["Location"].ToString() + @"
</td>
                                     <td>" + dtBH.Rows[i]["ShopName"].ToString() + @"</td>
                                    <td>" + DateTime.Parse(dtBH.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy") + @"</td>
                                   <td>" + dtBH.Rows[i]["KeyContainer"].ToString() + @"</td>
                                   <td>" + dtBH.Rows[i]["SoChi"].ToString() + @"</td>
                            </tr>";
            }
        }




        values += @"                                                                                     </tbody>
 </table>
                                                                                    </div>";



        return values;
    }

    #endregion
    protected string ChangeName(string Name)
    {
        if (Name == "Nhật ký thu hoạch")
        {
            Name = "Đóng gói thành phẩm";
        }
        return Name;
    }
}