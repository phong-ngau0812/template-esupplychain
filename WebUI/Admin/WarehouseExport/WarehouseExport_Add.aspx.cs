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
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class WarehouseExport_Add : System.Web.UI.Page
{
    public string title = "Xuất kho ";

    int Zone_ID;
    int Area_ID;
    public string FillAmount = "";
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!string.IsNullOrEmpty(Request["ProductPackageOrder_ID"]))
            int.TryParse(Request["ProductPackageOrder_ID"].ToString(), out ProductPackageOrder_ID);
        if (MyUser.GetFunctionGroup_ID() == "2")
        {
            Nhom2.Visible = true;
            Nhom4.Visible = false;
        }
        if (MyUser.GetFunctionGroup_ID() == "4" || MyUser.GetFunctionGroup_ID() == "6")
        {
            Nhom4.Visible = true;
            Nhom2.Visible = false;
        }
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            if (MyUser.GetFunctionGroup_ID() == "2")
            {
                FillDDLOrder();
            }
            FillDDLddlWorkshop();
            FillDDLImporter();
            FillDDLddlWarehouseNhom4();
            FillDDLddlWarehouseNhom2();
            FillGio();
        }

        // txtNgayCap.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void RadTreeList1_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
    {
        LoadTreeList();

    }
    private void FillGio()
    {
        for (int i = 23; i >= 0; i--)
        {
            ddlHour.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }

        for (int i = 59; i >= 0; i--)
        {
            ddlMinutes.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
        ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
        ddlMinutes.SelectedValue = DateTime.Now.Minute.ToString();
    }
    private void FillDDLddlWarehouseNhom4()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;


            }
            if (MyUser.GetAccountType_ID() == "7")
            {
                where += " and W.Zone_ID=" + MyUser.GetZone_ID();
            }
            if (MyUser.GetAccountType_ID() == "8")
            {
                where += " and W.Zone_ID=" + MyUser.GetZone_ID() + " and W.Area_ID=" + MyUser.GetArea_ID();
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select W.* from Warehouse W inner join ProductBrand PB on W.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and W.Active<>-1" + where);
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLddlWarehouseNhom2()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

            }
            if (MyUser.GetWarehouse_ID() != "")
            {
                where += " and W.Warehouse_ID in (" + MyUser.GetWarehouse_ID() + ")";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select W.* from Warehouse W inner join ProductBrand PB on W.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and W.Active<>-1" + where);
            ddlWarehouseNhom2.DataSource = dt;
            ddlWarehouseNhom2.DataTextField = "Name";
            ddlWarehouseNhom2.DataValueField = "Warehouse_ID";
            ddlWarehouseNhom2.DataBind();
            ddlWarehouseNhom2.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
            //if (MyUser.GetWarehouse_ID() != "")
            //{
            //    ddlWarehouseNhom2.SelectedValue = MyUser.GetWarehouse_ID();
            //    ddlWarehouseNhom2.Enabled = false;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLddlWorkshop()
    {
        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and WS.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        if (ddlProductBrand.SelectedValue.ToString() == "1524")
        {
            where += "and WS.Department_ID=249";
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select WS.* from Workshop WS inner join ProductBrand PB on WS.ProductBrand_ID = PB.ProductBrand_ID where PB.Active=1 " + where);
            ddlWorkshop.DataSource = dt;
            ddlWorkshop.DataTextField = "Name";
            ddlWorkshop.DataValueField = "Workshop_ID";
            ddlWorkshop.DataBind();
            ddlWorkshop.Items.Insert(0, new ListItem("-- Chọn người xuất kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLImporter()
    {
        string where = "";

        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and WS.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

        }
        if (ddlProductBrand.SelectedValue.ToString() == "1524")
        {
            where += "and WS.Department_ID=249";
        }
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select WS.* from Workshop WS inner join ProductBrand PB on WS.ProductBrand_ID = PB.ProductBrand_ID where PB.Active=1 " + where);
            ddlImporter.DataSource = dt;
            ddlImporter.DataTextField = "Name";
            ddlImporter.DataValueField = "Workshop_ID";
            ddlImporter.DataBind();
            ddlImporter.Items.Insert(0, new ListItem("-- Chọn người nhập kho --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void LoadMaterial()
    {
        if (ProductPackageOrder_ID != 0)
        {

            ddlProductPackageOrder.SelectedValue = ProductPackageOrder_ID.ToString();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("WarehouseImport_ID");
            dt.Columns.Add("Material_ID");
            dt.Columns.Add("AmountM");
            dt.Columns.Add("Unit");
            dt.Columns.Add("NameMaterial");
            string whereProductBrand = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                whereProductBrand += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(@"select Material_ID from ProductPackageOrderMaterial where ProductPackageOrder_ID = " + ProductPackageOrder_ID);
            if (dtMaterial.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtMaterial.Rows)
                {
                    if (dtRow["Material_ID"].ToString() != null)
                    {


                        DataTable dtMaterialByWarehouse = BusinessRulesLocator.Conllection().GetAllList(@"select Material_ID from Material where Active = 1 and Warehouse_ID like N'%" + ddlWarehouseNhom2.SelectedValue + "%'" + whereProductBrand);
                        if (dtMaterialByWarehouse.Rows.Count > 0)
                        {
                            foreach (DataRow dtRowWarehouse in dtMaterialByWarehouse.Rows)
                            {
                                if (dtRowWarehouse["Material_ID"].ToString() == dtRow["Material_ID"].ToString())
                                {
                                    string where = string.Empty;
                                    where += " And  WH.Material_ID  = " + dtRow["Material_ID"].ToString();
                                    if (ddlProductBrand.SelectedValue != "")
                                    {
                                        where += " And  WH.ProductBrand_ID = " + ddlProductBrand.SelectedValue;
                                    }
                                    if (ddlWarehouseNhom2.SelectedValue != "")
                                    {
                                        where += " And WH.Warehouse_ID = " + ddlWarehouseNhom2.SelectedValue;
                                    }
                                    DataTable dtcheck = BusinessRulesLocator.Conllection().GetAllList(" Select  WH.WarehouseImport_ID, WH.Material_ID, ISNULL(CAST(WH.Amount AS int), 0) as AmountM ,WH.Unit, CONCAT(WH.Name +' - ', WH.CodeMaterialPackage) as NameMaterial  from WarehouseImport WH where WH.Active = 1" + where);

                                    if (dtcheck.Rows.Count > 0)
                                    {

                                        foreach (DataRow rowcheck in dtcheck.Rows)
                                        {
                                            DataRow Row = dt.NewRow();
                                            Row["WarehouseImport_ID"] = rowcheck["WarehouseImport_ID"].ToString();
                                            Row["Material_ID"] = rowcheck["Material_ID"].ToString();
                                            Row["AmountM"] = rowcheck["AmountM"].ToString();
                                            Row["Unit"] = rowcheck["Unit"].ToString();
                                            Row["NameMaterial"] = rowcheck["NameMaterial"].ToString();
                                            dt.Rows.Add(Row);
                                        }

                                    }
                                    else
                                    {
                                        DataRow Row = dt.NewRow();
                                        MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Convert.ToInt32(dtRow["Material_ID"].ToString()));
                                        Row["WarehouseImport_ID"] = 0;
                                        Row["Material_ID"] = dtRow["Material_ID"].ToString();
                                        Row["AmountM"] = 0;
                                        Row["Unit"] = _MaterialRow.IsUnitNull ? string.Empty : _MaterialRow.Unit;
                                        string WarehouseName = string.Empty;
                                        if (ddlWarehouseNhom2.SelectedValue != "")
                                        {
                                            WarehouseName = "(" + ddlWarehouseNhom2.SelectedItem.Text + ")";
                                        }
                                        Row["NameMaterial"] = _MaterialRow.IsNameNull ? string.Empty : _MaterialRow.Name + " - <span class='badge badge-danger'> Chưa nhập kho " + WarehouseName + " </span> ";

                                        dt.Rows.Add(Row);

                                    }
                                }
                            }
                        }


                    }

                }
            }

            if (dt.Rows.Count > 0)
            {
                rptMaterial.DataSource = dt;
                rptMaterial.DataBind();
            }
            else
            {
                rptMaterial.DataSource = null;
                rptMaterial.DataBind();
            }
        }
        else
        {
            rptMaterial.DataSource = null;
            rptMaterial.DataBind();
        }

    }
    private void FillDDLOrder()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;

            }
            if (ProductPackageOrder_ID != 0)
            {
                where += " and ProductPackageOrder_ID =" + ProductPackageOrder_ID;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select ProductPackageOrder_ID, Name from ProductPackageOrder where Active=1 and Approve=1" + where); ;
            ddlProductPackageOrder.DataSource = dt;
            ddlProductPackageOrder.DataTextField = "Name";
            ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
            ddlProductPackageOrder.DataBind();
            ddlProductPackageOrder.Items.Insert(0, new ListItem("-- Chọn lệnh --", ""));
            LoadMaterial();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void AddNoti()
    {
        if (MyUser.GetFunctionGroup_ID() == "2")
        {
            ProductPackageOrderRow _ProductPackageOrderRow = new ProductPackageOrderRow();
            _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackageOrder.SelectedValue));
            if (_ProductPackageOrderRow != null)
            {
                //Tạo thông báo
                ///Gửi thông báo cho người tạo lệnh
                NotificationRow _NotificationRowAccept = new NotificationRow();
                Guid g1 = Guid.NewGuid();
                _NotificationRowAccept.Name = "Kho đã xuất phiếu";
                _NotificationRowAccept.Summary = "Lệnh sản xuất " + ddlProductPackageOrder.SelectedItem.Text + " đã được bộ phận kho xuất phiếu " + txtName.Text;
                _NotificationRowAccept.Body = ddlProductPackageOrder.SelectedValue.ToString();
                _NotificationRowAccept.NotificationType_ID = 8;
                _NotificationRowAccept.UserID = _ProductPackageOrderRow.CreateBy;
                _NotificationRowAccept.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                _NotificationRowAccept.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    _NotificationRowAccept.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    _NotificationRowAccept.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    _NotificationRowAccept.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                _NotificationRowAccept.Url = "/Admin/Notification/Notification_Detail?Alias=" + g1;
                _NotificationRowAccept.CreateBy = MyUser.GetUser_ID();
                _NotificationRowAccept.CreateDate = DateTime.Now;
                _NotificationRowAccept.Active = 1;
                _NotificationRowAccept.Alias = g1;
                _NotificationRowAccept.ProductPackageOrder_ID = _ProductPackageOrderRow.ProductPackageOrder_ID;
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRowAccept);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddWarehouseExporterMaterial();

        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void AddWarehouseExporterMaterial()
    {
        try
        {
            WarehouseExportRow _WarehouseExportRow = new WarehouseExportRow();
            _WarehouseExportRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _WarehouseExportRow.WarehouseExportType_ID = 1;
            //  _WarehouseExportRow.Product_ID = Product_ID;
            if (MyUser.GetFunctionGroup_ID() == "2")
            {
                _WarehouseExportRow.Product_ID = Convert.ToInt32(ddlProduct.SelectedValue);
                _WarehouseExportRow.ProductPackageOrder_ID = Convert.ToInt32(ddlProductPackageOrder.SelectedValue);
                //GenName nhóm 2
                ProductPackageOrderRow _ProductPackageOrder = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(Convert.ToInt32(ddlProductPackageOrder.SelectedValue));

                DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);

                string Name = "PX-" + ddlWarehouseNhom2.SelectedValue + "-" + _ProductPackageOrder.CodePO + "-" + Common.RemoveSignature(ngaycap.ToString());


                _WarehouseExportRow.Name = string.IsNullOrEmpty(Name) ? string.Empty : Name;
                if (ddlWarehouseNhom2.SelectedValue != "")
                {
                    _WarehouseExportRow.Warehouse_ID = Convert.ToInt32(ddlWarehouseNhom2.SelectedValue);
                }
            }

            //_WarehouseExportRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
            _WarehouseExportRow.Workshop_ID = Convert.ToInt32(ddlWorkshop.SelectedValue);
            //    _WarehouseExportRow.Farm_ID = Farm_ID;

            _WarehouseExportRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _WarehouseExportRow.Importer = string.IsNullOrEmpty(ddlImporter.SelectedItem.Text) ? string.Empty : ddlImporter.SelectedItem.Text;


            if (MyUser.GetFunctionGroup_ID() == "4")
            {
                //GenName Nhóm 4
                DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);

                string Name = "PX-" + ddlWarehouse.SelectedValue + "-" + Common.RemoveSignature(ngaycap.ToString());


                _WarehouseExportRow.Name = string.IsNullOrEmpty(Name) ? string.Empty : Name;
                if (ddlWarehouse.SelectedValue != "")
                {
                    _WarehouseExportRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
            }

            _WarehouseExportRow.Exporter = string.IsNullOrEmpty(ddlWorkshop.SelectedItem.Text) ? string.Empty : ddlWorkshop.SelectedItem.Text.ToString();
            //_WarehouseExportRow.Exporter = string.IsNullOrEmpty(txtExporter.Text) ? string.Empty : txtExporter.Text;
            //_WarehouseImportRow.Exporter = "";
            _WarehouseExportRow.Active = 1;
            _WarehouseExportRow.LastEditBy = _WarehouseExportRow.CreateBy = MyUser.GetUser_ID();
            _WarehouseExportRow.LastEditDate = DateTime.Now;
            if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
            {
                DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);
                _WarehouseExportRow.CreateDate = ngaycap;
            }
            _WarehouseExportRow.Comment = string.IsNullOrEmpty(txtComment.Text) ? string.Empty : txtComment.Text;
            BusinessRulesLocator.GetWarehouseExportBO().Insert(_WarehouseExportRow);
            //Lưu các loại vật tư 
            if (!_WarehouseExportRow.IsWarehouseExport_IDNull)
            {
                if (MyUser.GetFunctionGroup_ID() == "2")
                {
                    Add_WarehouseExportMaterial(_WarehouseExportRow.WarehouseExport_ID);
                }

                if (MyUser.GetFunctionGroup_ID() == "4")
                {

                    //AddMaterialInWarehouseExport(_WarehouseExportRow.WarehouseExport_ID);
                    if (hdfType.Value == "2")
                    {
                        AddProductInWarehouseExport(_WarehouseExportRow.WarehouseExport_ID);
                    }
                }
            }

            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            AddNoti();
            ClearForm();
            Admin_Template_CMS master = this.Master as Admin_Template_CMS;
            if (master != null)
                master.LoadNotification();
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateAddWarehouseExporterMaterial", ex.ToString());
        }
    }
    protected void Add_WarehouseExportMaterial(int WarehouseExport_ID)
    {
        try
        {
            foreach (RepeaterItem item in rptMaterial.Items)
            {
                Literal lblMaterialID = (Literal)item.FindControl("lblMaterialID");
                Literal lblWarehouseImport_ID = (Literal)item.FindControl("lblWarehouseImport_ID");
                CheckBox ckMaterialID1 = (CheckBox)item.FindControl("ckMaterialID1");
                Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                DropDownList ddlNumber = item.FindControl("ddlNumber") as DropDownList;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;


                if (ckMaterialID1.Checked)
                {
                    WarehouseExportMaterialRow _WarehouseExportMaterial = new WarehouseExportMaterialRow();
                    _WarehouseExportMaterial.WarehouseExport_ID = WarehouseExport_ID;
                    _WarehouseExportMaterial.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                    _WarehouseExportMaterial.WarehouseImport_ID = Convert.ToInt32(lblWarehouseImport_ID.Text);
                    _WarehouseExportMaterial.Amount = double.Parse(string.IsNullOrEmpty(txtQuantity.Text) ? "0" : txtQuantity.Text);
                    BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterial);
                }
                //if (!string.IsNullOrEmpty(ddlNumber.SelectedValue))
                //{
                //    WarehouseExportMaterialRow _WarehouseExportMaterial = new WarehouseExportMaterialRow();
                //    _WarehouseExportMaterial.WarehouseExport_ID = WarehouseExport_ID;
                //    _WarehouseExportMaterial.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                //    _WarehouseExportMaterial.Amount = Convert.ToDouble(ddlNumber.SelectedValue);
                //    BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterial);
                //}

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("ProductPackageOrderMaterial", ex.ToString());
        }
    }

    protected void ClearForm()
    {
        txtNote.Text = "";
        //txtImprot.Text = " ";
        ddlProductPackageOrder.SelectedValue = "";
        LoadMaterial();
        txtName.Text = "";
        ddlWorkshop.SelectedValue = "";
        //  Vattu.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseExport_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLOrder();
        FillDDLddlWorkshop();

    }



    /// <summary>
    /// Nhóm 4
    /// </summary>
    /// <returns></returns>
    private string ADDListWarehouse_ID()
    {
        string WareHouse_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlWarehouse.Items)
            {
                if (item.Selected)
                {
                    WareHouse_ID += item.Value + ",";
                }
            }
            //if (!string.IsNullOrEmpty(WareHouse_ID))
            //{
            //    WareHouse_ID = "," + WareHouse_ID;
            //}

        }
        catch (Exception ex)
        {
            Log.writeLog("Customer_ID", ex.ToString());
        }
        return WareHouse_ID;
    }
    private string ADDListMaterial4_ID()
    {
        string Material4_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlMaterial.Items)
            {
                if (item.Selected)
                {
                    Material4_ID += item.Value + ",";
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("Customer_ID", ex.ToString());
        }
        return Material4_ID;
    }
    string ListWareHouse_ID = string.Empty;
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarehouse.SelectedValue != "")
        {
            //ListWareHouse_ID = ADDListWarehouse_ID().Substring(0, ADDListWarehouse_ID().Length - 1);
            //ListWareHouse_ID = ADDListWarehouse_ID();
            WarehouseRow _WarehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Convert.ToInt32(ddlWarehouse.SelectedValue));
            if (_WarehouseRow.Type == 1)
            {
                hdfType.Value = "1";
                FillddlMaterialVT();
                Data1.Visible = true;
                Data2.Visible = false;
            }
            else
            {
                hdfType.Value = "2";
                FillddlMaterialSP();
                Data2.Visible = true;
                Data1.Visible = false;
            }
        }



        //LoadTableMaterial();
    }


    protected void ddlProductPackageOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductPackageOrder.SelectedValue != "")
        {
            ProductPackageOrder_ID = Convert.ToInt32(ddlProductPackageOrder.SelectedValue);

            ProductPackageOrderRow _ProductPackageOrder = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_ID);

            if (!_ProductPackageOrder.IsProduct_IDNull)
            {
                DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select Product_ID, Name from Product where Active <>-1 and Product_ID in (" + _ProductPackageOrder.Product_ID.Trim().Substring(1, _ProductPackageOrder.Product_ID.Length - 2) + ")");
                if (dt.Rows.Count > 0)
                {
                    ddlProduct.DataSource = dt;

                    ddlProduct.DataValueField = "Product_ID";
                    ddlProduct.DataTextField = "Name";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
                }
            }
            else
            {
                ddlProduct.DataSource = null;
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
            }
        }

        LoadMaterial();
    }

    protected void rptMaterial_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblAmount = e.Item.FindControl("lblAmount") as Literal;
            Literal lblWarehouseImport_ID = e.Item.FindControl("lblWarehouseImport_ID") as Literal;
            Literal lblMaterial_ID = e.Item.FindControl("lblMaterial_ID") as Literal;
            DropDownList ddlNumber = e.Item.FindControl("ddlNumber") as DropDownList;
            CheckBox ckMaterialID1 = e.Item.FindControl("ckMaterialID1") as CheckBox;

            if (lblWarehouseImport_ID != null)
            {
                if (lblWarehouseImport_ID.Text == "0")
                {
                    ckMaterialID1.Visible = false;
                }
            }



            //if (lblAmount != null)
            //{
            //    int number = Convert.ToInt32(lblAmount.Text.ToString());
            //    DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Amount),0) AS SUM from WarehouseExportMaterial where WarehouseExport_ID in(  select WarehouseExport_ID from WarehouseExport where ProductPackageOrder_ID=" + ddlProductPackageOrder.SelectedValue + " and Active=1)  and Material_ID = " + lblMaterial_ID.Text + " ");
            //    if (dt.Rows.Count > 0)
            //    {
            //        number = number - Convert.ToInt32(dt.Rows[0]["Sum"]);
            //    }
            //    for (int i = 1; i <= number; i++)
            //    {
            //        ddlNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //    }
            //}
        }
    }

    //protected void ddlWarehouseImprot_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (hdfType.Value == "1")
        {
            LoadTableMaterial();
        }
        else if (hdfType.Value == "2")
        {
            RadTreeList1_NeedDataSource(null, null);
        }
    }
    public decimal Calculate(string Warehouse4_ID, string Product4_ID)
    {
        return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"Select (SUM(P.Total)) as TotalInWarehouse from (Select (Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage where WarehouseImport_ID =WI.WarehouseImport_ID)  as Total,WI.Product_ID, PP.Name  from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where WI.Active = 1 and WI.Warehouse_ID =" + Warehouse4_ID + " and WI.Product_ID in (" + Product4_ID + ")) p").Rows[0]["TotalInWarehouse"].ToString());
    }
    protected decimal CalculateAmountExport(string Product_ID)
    {
        return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
    }
    private void LoadTreeList()
    {
        if (string.IsNullOrEmpty(GetMaterial()))
        {
            RadTreeList1.DataSource = null;

        }
        else
        {
            DataTable dtTreeList = new DataTable();
            dtTreeList.Clear();
            dtTreeList.Columns.Add("ID");
            dtTreeList.Columns.Add("Name");
            dtTreeList.Columns.Add("Unit");
            dtTreeList.Columns.Add("AmountInWareHouse");
            dtTreeList.Columns.Add("Parent_ID");


            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"Select Distinct P.Product_ID, P.Name ,P.ExpectedProductivityDescription from WarehouseImport WI left join Product P on WI.Product_ID = P.Product_ID where WI.Active = 1 and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue + "and WI.Product_ID in (" + ADDListMaterial4_ID().Substring(0, ADDListMaterial4_ID().Length - 1) + ")");

            foreach (DataRow item in dt.Rows)
            {
                DataRow itemTreeList = dtTreeList.NewRow();
                itemTreeList["ID"] = item["Product_ID"];
                itemTreeList["Parent_ID"] = "";
                itemTreeList["Name"] = item["Name"];
                itemTreeList["AmountInWareHouse"] = (Calculate(ddlWarehouse.SelectedValue, item["Product_ID"].ToString()) - CalculateAmountExport(item["Product_ID"].ToString())).ToString("N0");
                itemTreeList["Unit"] = item["ExpectedProductivityDescription"];
                dtTreeList.Rows.Add(itemTreeList);
                if (item["Product_ID"] != null)
                {
                    DataTable dtChild = new DataTable();
                    dtChild = BusinessRulesLocator.Conllection().GetAllList(@"Select ((Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage
where WarehouseImport_ID =WI.WarehouseImport_ID) - (Select IsNull(SUM(Amount),0) from WarehouseExportMaterial
where WarehouseImport_ID =WI.WarehouseImport_ID)) as Total ,WI.Product_ID as Product_IDChild,CONCAT(WI.Name +' - ',WI.Code) as NameW,PP.ExpectedProductivityDescription, WI.WarehouseImport_ID 
from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where WI.Active = 1 and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue + " and WI.Product_ID in (" + item["Product_ID"].ToString() + ") ORDER BY WI.CREATEDATE ASC");
                    if (dtChild.Rows.Count > 0)
                    {
                        foreach (DataRow itemChild in dtChild.Rows)
                        {
                            if (decimal.Parse(itemChild["Total"].ToString()) > 0)
                            {
                                itemTreeList = dtTreeList.NewRow();
                                itemTreeList["ID"] = itemChild["WarehouseImport_ID"];
                                itemTreeList["Parent_ID"] = itemChild["Product_IDChild"];
                                itemTreeList["Name"] = itemChild["NameW"];
                                itemTreeList["AmountInWareHouse"] = decimal.Parse(itemChild["Total"].ToString()).ToString("N0");
                                itemTreeList["Unit"] = itemChild["ExpectedProductivityDescription"];
                                dtTreeList.Rows.Add(itemTreeList);
                            }
                        }

                    }
                }
            }
            RadTreeList1.DataSource = dtTreeList;

        }
    }



    /// <summary>
    /// Load vật tư nhập kho
    /// </summary>
    private void FillddlMaterialSP()
    {
        try
        {

            string Where = "";

            if (Convert.ToInt32(ddlProductBrand.SelectedValue) != 0)
            {
                Where += " and WI.ProductBrand_ID =" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            if (ddlWarehouse.SelectedValue != "")
            {
                Where += "and WI.Warehouse_ID = " + ddlWarehouse.SelectedValue + "";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" Select Distinct P.Product_ID, P.Name  from WarehouseImport WI left join Product P on WI.Product_ID = P.Product_ID where WI.Active = 1 " + Where);
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataTextField = "Name";
            ddlMaterial.DataValueField = "Product_ID";
            ddlMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLTestingCertificates", ex.ToString());
        }
    }

    private void FillddlMaterialVT()
    {
        try
        {

            string Where = "";

            if (Convert.ToInt32(ddlProductBrand.SelectedValue) != 0)
            {
                Where += " and WI.ProductBrand_ID =" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            if (ddlWarehouse.SelectedValue != "")
            {
                Where += "and WI.Warehouse_ID = " + ddlWarehouse.SelectedValue + "";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" Select WI.WarehouseImport_ID, WI.Name,WI.Material_ID  from WarehouseImport WI where WI.Active = 1 " + Where);
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataTextField = "Name";
            ddlMaterial.DataValueField = "WarehouseImport_ID";
            ddlMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLTestingCertificates", ex.ToString());
        }
    }


    protected string GetMaterial()
    {
        string Material_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlMaterial.Items)
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


    protected void LoadTableMaterial()
    {
        if (string.IsNullOrEmpty(GetMaterial()))
        {
            rptMaterialCollapse.DataSource = null;
            rptMaterialCollapse.DataBind();
            // tbl.Visible = false;
        }
        else
        {
            //tbl.Visible = true;
            string[] array = GetMaterial().Split(',');
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Material_ID");
            dt.Columns.Add("WarehouseImport_ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("AmountInWareHouse");

            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int WarehouseImport_ID = Convert.ToInt32(value);
                    WarehouseImportRow _WarehouseImportRow = BusinessRulesLocator.GetWarehouseImportBO().GetByPrimaryKey(WarehouseImport_ID);
                    string brandName = string.Empty;
                    if (_WarehouseImportRow != null)
                    {
                        DataRow _row = dt.NewRow();
                        _row["WarehouseImport_ID"] = WarehouseImport_ID;
                        _row["Material_ID"] = _WarehouseImportRow.IsMaterial_IDNull ? string.Empty : _WarehouseImportRow.Material_ID.ToString();
                        _row["Name"] = _WarehouseImportRow.IsNameNull ? string.Empty : _WarehouseImportRow.Name.ToString();

                        if (_WarehouseImportRow.WarehouseImportType_ID == 1)
                        {
                            _row["Unit"] = _WarehouseImportRow.IsUnitNull ? string.Empty : _WarehouseImportRow.Unit.ToString();
                            _row["AmountInWareHouse"] = _WarehouseImportRow.IsAmountNull ? string.Empty : _WarehouseImportRow.Amount.ToString();
                        }
                        else
                        {
                            ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(_WarehouseImportRow.Product_ID);
                            _row["Unit"] = _ProductRow.IsExpectedProductivityDescriptionNull ? "" : _ProductRow.ExpectedProductivityDescription.ToString();
                            _row["AmountInWareHouse"] = double.Parse(BusinessRulesLocator.Conllection().GetAllList("select ISNULL( SUM(Amount),0) as TongNhap from WarehouseImportProductPackage where WarehouseImport_ID = " + WarehouseImport_ID).Rows[0]["TongNhap"].ToString()) - double.Parse(BusinessRulesLocator.Conllection().GetAllList("select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID = " + WarehouseImport_ID).Rows[0]["TongXuat"].ToString());
                        }
                        //DataTable Quantity = new DataTable();
                        //Quantity = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetAsDataTable("WarehouseExport_ID=" + WarehouseExport_ID + " and Material_ID=" + Material_ID, "");
                        //if (Quantity.Rows.Count == 1)
                        //{
                        //    _row["Quantity"] = Quantity.Rows[0]["Quantity"];
                        //}
                        //else
                        //{
                        //    _row["Quantity"] = null;
                        //}


                        dt.Rows.Add(_row);
                    }
                }
            }
            //Response.Write(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                rptMaterialCollapse.DataSource = dt;
                rptMaterialCollapse.DataBind();
                Data1.Visible = true;
            }
            else
            {
                Data1.Visible = false;
            }
            //LoadMaterialCollapse(ProductPackage_ID);
        }
    }

    protected void AddMaterialInWarehouseExport(int WarehouseExport_ID)
    {
        try
        {
            if (Page.IsValid)
            {
                foreach (RepeaterItem item in rptMaterialCollapse.Items)
                {

                    Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    Literal lblWarehouseImport_ID = item.FindControl("lblWarehouseImport_ID") as Literal;
                    if (lblMaterial_ID != null)
                    {
                        WarehouseExportMaterialRow _WarehouseExportMaterialRow = new WarehouseExportMaterialRow();
                        //_ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                        _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                        _WarehouseExportMaterialRow.Material_ID = string.IsNullOrEmpty(lblMaterial_ID.Text) ? 0 : Convert.ToInt32(lblMaterial_ID.Text);
                        _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(lblWarehouseImport_ID.Text);

                        //Update Amount for Productpackage import
                        if (!string.IsNullOrEmpty(txtQuantity.Text))
                        {
                            _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantity.Text);
                        }
                        BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterialRow);
                        Response.Redirect("WarehouseExport_List.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
    //sản phẩm Xuất kho
    protected void AddProductInWarehouseExport(int WarehouseExport_ID)
    {
        try
        {
            if (Page.IsValid)
            {
                string Flag = ",";
                foreach (TreeListDataItem parentitem in RadTreeList1.Items)
                {
                    if (Flag.Contains("," + parentitem["ID"].Text + ",") != true)
                    {
                        Flag += parentitem["ID"].Text + ",";
                        TextBox txtQuantity = parentitem.FindControl("txtQuantity") as TextBox;
                        if (parentitem.ChildItems.Count > 0)
                        {
                            foreach (TreeListDataItem Child in parentitem.ChildItems)
                            {
                                TextBox txtQuantityChild = Child.FindControl("txtQuantity") as TextBox;
                                Flag += Child["ID"].Text + ",";
                                if (!string.IsNullOrEmpty(txtQuantityChild.Text))
                                {
                                    if (decimal.Parse(txtQuantityChild.Text) > 0)
                                    {
                                        WarehouseExportMaterialRow _WarehouseExportMaterialRow = new WarehouseExportMaterialRow();

                                        _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                                        _WarehouseExportMaterialRow.Material_ID = 0;
                                        _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(Child["ID"].Text);
                                        //Update Amount for Productpackage import
                                        _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantityChild.Text);
                                        BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterialRow);

                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable dtChild = new DataTable();
                            dtChild = BusinessRulesLocator.Conllection().GetAllList(@"Select (Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage
where WarehouseImport_ID =WI.WarehouseImport_ID)  as Total,WI.Product_ID as Product_IDChild,CONCAT(WI.Name +' - ',WI.Code) as NameW,PP.ExpectedProductivityDescription, WI.WarehouseImport_ID 
from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where WI.Active = 1 and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue + " and WI.Product_ID in (" + parentitem["ID"].Text + ") ORDER BY WI.CREATEDATE ASC");
                            if (dtChild.Rows.Count > 0)
                            {
                                foreach (DataRow dtRow in dtChild.Rows)
                                {
                                    if (decimal.Parse(dtRow["Total"].ToString()) > 0 && decimal.Parse(txtQuantity.Text) > 0)
                                    {
                                        WarehouseExportMaterialRow _WarehouseExportMaterialRow = new WarehouseExportMaterialRow();

                                        _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                                        _WarehouseExportMaterialRow.Material_ID = 0;
                                        _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(dtRow["WarehouseImport_ID"].ToString());

                                        //Update Amount for Productpackage import
                                        if (decimal.Parse(dtRow["Total"].ToString()) <= decimal.Parse(txtQuantity.Text))
                                        {
                                            _WarehouseExportMaterialRow.Amount = Convert.ToDouble(dtRow["Total"].ToString());
                                            // Số lượng còn lại
                                            txtQuantity.Text = (decimal.Parse(txtQuantity.Text) - decimal.Parse(dtRow["Total"].ToString())).ToString();
                                        }
                                        else if (decimal.Parse(dtRow["Total"].ToString()) >= decimal.Parse(txtQuantity.Text))
                                        {
                                            _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantity.Text);
                                            txtQuantity.Text = "0";
                                        }
                                        BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterialRow);
                                        if (!_WarehouseExportMaterialRow.IsWarehouseExportMaterial_IDNull)
                                        {

                                        }

                                    }
                                }
                            }
                        }

                    }
                }
                //foreach (RepeaterItem item in rptMaterialCollapse.Items)
                //{

                //    Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                //    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                //    Literal lblWarehouseImport_ID = item.FindControl("lblWarehouseImport_ID") as Literal;
                //    if (lblMaterial_ID != null)
                //    {
                //        WarehouseExportMaterialRow _WarehouseExportMaterialRow = new WarehouseExportMaterialRow();
                //        //_ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                //        _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                //        _WarehouseExportMaterialRow.Material_ID = string.IsNullOrEmpty(lblMaterial_ID.Text) ? 0 : Convert.ToInt32(lblMaterial_ID.Text);
                //        _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(lblWarehouseImport_ID.Text);

                //        //Update Amount for Productpackage import
                //        if (!string.IsNullOrEmpty(txtQuantity.Text))
                //        {
                //            _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantity.Text);
                //        }
                //        BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterialRow);
                //        Response.Redirect("WarehouseExport_List.aspx", false);
                //    }
                //}
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }

}