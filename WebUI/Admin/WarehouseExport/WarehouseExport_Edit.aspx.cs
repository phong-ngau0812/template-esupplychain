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

public partial class WarehouseExport_Edit : System.Web.UI.Page
{
    int WarehouseExport_ID = 0;
    public string title = "Thông tin xuất kho";
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["WarehouseExport_ID"]))
            int.TryParse(Request["WarehouseExport_ID"].ToString(), out WarehouseExport_ID);
        if (MyUser.GetFunctionGroup_ID() == "2")
        {
            Nhom2.Visible = true;
            Nhom4.Visible = false;
        }
        if (MyUser.GetFunctionGroup_ID() == "4")
        {
            Nhom4.Visible = true;
            Nhom2.Visible = false;
        }

        if (!IsPostBack)
        {

            FillDDLddlProductBrand();
            if (MyUser.GetFunctionGroup_ID() == "4")
            {
                FillDDLddlWarehouseNhom4();
                FillddlMaterial();
                //  LoadMaterialCollapse();
                //   LoadTableMaterial();
            }
            FillDDLddlWarehouseNhom2();
            FillDDLOrder();
            FillDDLImporter();
            FillDDLddlWorkshop();
            FillInfoWarehouseExport();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void RadTreeList1_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
    {
        LoadTreeList();

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
            //  LoadMaterial();
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


    protected void FillInfoWarehouseExport()
    {
        try
        {
            if (WarehouseExport_ID != 0)
            {
                WarehouseExportRow _WarehouseExportRow = new WarehouseExportRow();
                _WarehouseExportRow = BusinessRulesLocator.GetWarehouseExportBO().GetByPrimaryKey(WarehouseExport_ID);

                if (_WarehouseExportRow != null)
                {

                    ddlProductBrand.SelectedValue = _WarehouseExportRow.IsProductBrand_IDNull ? string.Empty : _WarehouseExportRow.ProductBrand_ID.ToString();
                    if (MyUser.GetFunctionGroup_ID() == "2")
                    {

                        txtName.Text = _WarehouseExportRow.IsNameNull ? string.Empty : _WarehouseExportRow.Name;
                        if (!_WarehouseExportRow.IsWarehouse_IDNull)
                        {
                            ddlWarehouseNhom2.SelectedValue = _WarehouseExportRow.Warehouse_ID.ToString();
                        }
                        FillDDLOrder(_WarehouseExportRow.ProductPackageOrder_ID);
                        LoadProduct(_WarehouseExportRow.ProductPackageOrder_ID);
                        ddlProduct.SelectedValue = _WarehouseExportRow.Product_ID.ToString();
                        FillMaterialChoose(WarehouseExport_ID);

                    }
                    if (MyUser.GetFunctionGroup_ID() == "4")
                    {

                        if (!_WarehouseExportRow.IsWarehouse_IDNull)
                        {
                            ddlWarehouse.SelectedValue = _WarehouseExportRow.Warehouse_ID.ToString();
                        }
                        ddlWarehouse_SelectedIndexChanged(null, null);
                        if (hdfType.Value == "2")
                        {
                            LoadProductByWarehouseExport();
                            LoadProductCollapse();
                        }
                        txtTenPhieu.Text = _WarehouseExportRow.IsNameNull ? string.Empty : _WarehouseExportRow.Name;
                    }
                    //ddlImporter.Text = _WarehouseExportRow.IsImporterNull ? "" : _WarehouseExportRow.Importer.ToString();
                    //ddlWorkshop.Text = _WarehouseExportRow.IsExporterNull ? "" : _WarehouseExportRow.Workshop_ID.ToString();
                    txtNote.Text = _WarehouseExportRow.IsDescriptionNull ? string.Empty : _WarehouseExportRow.Description;
                    if (!_WarehouseExportRow.IsCreateDateNull)
                    {
                        txtNgayCap.Text = _WarehouseExportRow.CreateDate.ToString("dd/MM/yyyy");
                        ddlHour.SelectedValue = _WarehouseExportRow.CreateDate.Hour.ToString();
                        ddlMinutes.SelectedValue = _WarehouseExportRow.CreateDate.Minute.ToString();
                    }
                    txtComment.Text = _WarehouseExportRow.IsCommentNull ? string.Empty : _WarehouseExportRow.Comment.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void LoadProductByWarehouseExport()
    {
        if (BusinessRulesLocator.GetWarehouseExportMaterialBO().GetAsDataTable("WarehouseExport_ID = " + WarehouseExport_ID, "").Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" Select Distinct P.Product_ID, P.Name  from WarehouseImport WI left join Product P on WI.Product_ID = P.Product_ID where WI.Active = 1 and WI.WarehouseImport_ID in ( Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ")");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dt.Rows)
                {
                    foreach (ListItem itemM in ddlMaterial.Items)
                    {
                        if (dtRow["Product_ID"].ToString() == itemM.Value)
                        {
                            itemM.Selected = true;
                        }
                    }
                }

            }

        }
    }

    protected void LoadProduct(int ProductPackageOrder_Id)
    {
        ProductPackageOrderRow _ProductPackageOrder = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_Id);

        if (!_ProductPackageOrder.IsProduct_IDNull)
        {
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select Product_ID, Name from Product where Active <>-1 and Product_ID in (" + _ProductPackageOrder.Product_ID.Trim().Substring(1, _ProductPackageOrder.Product_ID.Length - 2) + ")");
            if (dt.Rows.Count > 0)
            {
                ddlProduct.DataSource = dt;
                ddlProduct.DataBind();
                ddlProduct.DataValueField = "Product_ID";
                ddlProduct.DataTextField = "Name";
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
    private void FillMaterialChoose(int warehouseExport_ID)
    {
        foreach (RepeaterItem item in rptMaterial.Items)
        {
            CheckBox ckMaterialID1 = (CheckBox)item.FindControl("ckMaterialID1");
            Literal lblMaterialID = (Literal)item.FindControl("lblMaterialID");
            Literal lblWarehouseExportMaterial = (Literal)item.FindControl("lblWarehouseExportMaterial");

            DataTable dt = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetAsDataTable("WarehouseExport_ID =" + warehouseExport_ID + " and Material_ID =" + lblMaterialID.Text, "");
            if (dt.Rows.Count > 0)
            {
                if (lblMaterialID.Text.ToString() == dt.Rows[0]["Material_ID"].ToString())
                {
                    ckMaterialID1.Checked = true;
                    lblWarehouseExportMaterial.Text = dt.Rows[0]["WarehouseExportMaterial_ID"].ToString();
                }
            }
        }
    }

    private void FillDDLOrder(int ProductPackageOrder_ID)
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;

            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select ProductPackageOrder_ID, Name from ProductPackageOrder where Active=1 and Approve=1" + where); ;
            ddlProductPackageOrder.DataSource = dt;
            ddlProductPackageOrder.DataTextField = "Name";
            ddlProductPackageOrder.DataValueField = "ProductPackageOrder_ID";
            ddlProductPackageOrder.DataBind();
            ddlProductPackageOrder.Items.Insert(0, new ListItem("-- Chọn lệnh --", ""));
            ddlProductPackageOrder.SelectedValue = ProductPackageOrder_ID.ToString();


            LoadMaterial(ProductPackageOrder_ID);
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    public string ReturnSLXuat(object Material_ID)
    {
        string sl = "";
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select Amount from WarehouseExportMaterial where WarehouseExport_ID=" + WarehouseExport_ID + " and Material_ID=" + Material_ID);
        if (dt.Rows.Count == 1)
        {
            sl = dt.Rows[0]["Amount"].ToString();
        }
        return sl;
    }
    //protected void LoadMaterial(int ProductPackageOrder_ID)
    //{

    //    DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(" select A.*,M.Unit, M.Name as NameM from ProductPackageOrderMaterial A left join Material M on A.Material_ID= M.Material_ID where A.ProductPackageOrder_ID=" + ProductPackageOrder_ID);
    //    if (dtMaterial.Rows.Count > 0)
    //    {
    //        rptMaterial.DataSource = dtMaterial;
    //        rptMaterial.DataBind();
    //    }
    //    else
    //    {
    //        rptMaterial.DataSource = null;
    //        rptMaterial.DataBind();
    //    }

    //}


    protected void UpdateWarehouseExport()
    {
        try
        {
            WarehouseExportRow _WarehouseExportRow = new WarehouseExportRow();
            if (WarehouseExport_ID != 0)
            {
                _WarehouseExportRow = BusinessRulesLocator.GetWarehouseExportBO().GetByPrimaryKey(WarehouseExport_ID);

                if (_WarehouseExportRow != null)
                {
                    _WarehouseExportRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _WarehouseExportRow.WarehouseExportType_ID = 1;
                    //  _WarehouseExportRow.Product_ID = Product_ID;
                    if (MyUser.GetFunctionGroup_ID() == "2")
                    {
                        _WarehouseExportRow.ProductPackageOrder_ID = Convert.ToInt32(ddlProductPackageOrder.SelectedValue);
                        //  _WarehouseExportRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
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
                        // _WarehouseExportRow.Name = string.IsNullOrEmpty(txtTenPhieu.Text) ? string.Empty : txtTenPhieu.Text;
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
                    _WarehouseExportRow.Comment = string.IsNullOrEmpty(txtComment.Text) ? string.Empty : txtComment.Text;
                    if (!string.IsNullOrEmpty(txtNgayCap.Text.Trim()))
                    {
                        DateTime ngaycap = DateTime.ParseExact(txtNgayCap.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                        ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                        ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);
                        _WarehouseExportRow.CreateDate = ngaycap;
                    }
                    //Lưu các loại vật tư 
                    if (!_WarehouseExportRow.IsWarehouseExport_IDNull)
                    {
                        if (MyUser.GetFunctionGroup_ID() == "2")
                        {
                            Update_WarehouseExportMaterial(_WarehouseExportRow.WarehouseExport_ID);
                        }

                        if (MyUser.GetFunctionGroup_ID() == "4")
                        {
                            // AddMaterialInWarehouseExport(_WarehouseExportRow.WarehouseExport_ID);
                            if (hdfType.Value == "2")
                            {
                                AddProductInWarehouseExport(_WarehouseExportRow.WarehouseExport_ID);
                            }
                        }
                    }

                    BusinessRulesLocator.GetWarehouseExportBO().Update(_WarehouseExportRow);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FillInfoWarehouseExport();
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateWarehouse", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateWarehouseExport();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseExport_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlWorkshop();
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
            //Literal lblAmount = e.Item.FindControl("lblAmount") as Literal;
            //Literal lblMaterial_ID = e.Item.FindControl("lblMaterial_ID") as Literal;
            //DropDownList ddlNumber = e.Item.FindControl("ddlNumber") as DropDownList;
            //if (lblAmount != null)
            //{
            //    int number = Convert.ToInt32(lblAmount.Text);
            //    //DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Amount),0) AS SUM from WarehouseExportMaterial where WarehouseExport_ID in(  select WarehouseExport_ID from WarehouseExport where ProductPackageOrder_ID=" + ddlProductPackageOrder.SelectedValue + ")  and Material_ID = " + lblMaterial_ID.Text + "");
            //    //if (dt.Rows.Count > 0)
            //    //{
            //    //    number = number - Convert.ToInt32(dt.Rows[0]["Sum"]);
            //    //}
            //    for (int i = 1; i <= number; i++)
            //    {
            //        ddlNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //    }
            //}
        }
    }

    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadTreeList1_NeedDataSource(null, null);
    }


    /// <summary>
    /// Load vật tư nhập kho
    /// </summary>
    private void FillddlMaterial()
    {
        try
        {

            string Where = "";

            if (Convert.ToInt32(ddlProductBrand.SelectedValue) != 0)
            {
                Where += " and WI.ProductBrand_ID =" + Convert.ToInt32(ddlProductBrand.SelectedValue);
            }
            if (ListWareHouse_ID != "")
            {
                Where += "and WI.Warehouse_ID in (" + ListWareHouse_ID + ")";
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



    protected void LoadProductCollapse()
    {
        //DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(@"select WEM.WarehouseExportMaterial_ID as WarehouseExportMaterial_ID , WEM.Amount as Quantity , WEM.WarehouseImport_ID, (Select CONCAT(Name +' - ',Code) as NameW from WarehouseImport where WarehouseImport_ID = WEM.WarehouseImport_ID) as Name ,(Select Product.ExpectedProductivityDescription  from WarehouseImport left join Product on WarehouseImport.Product_ID = Product.Product_ID  where WarehouseImport_ID = WEM.WarehouseImport_ID) as Unit from WarehouseExportMaterial WEM where WarehouseExport_ID = " + WarehouseExport_ID);
        //if (dtMaterial.Rows.Count > 0)
        //{

        //    rptProductCollapse.DataSource = dtMaterial;
        //    rptProductCollapse.DataBind();

        //}
    }
    protected void LoadMaterialCollapse()
    {
        DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(@"select WEM.WarehouseExportMaterial_ID as WarehouseExportMaterial_ID ,M.Material_ID, M.Unit, M.Name , WEM.Amount as Quantity ,WEM.WarehouseImport_ID from WarehouseExportMaterial WEM left join Material M on WEM.Material_ID = M.Material_ID where WarehouseExport_ID = " + WarehouseExport_ID);
        if (dtMaterial.Rows.Count > 0)
        {
            //Data1.Visible = true;
            rptMaterialCollapse.DataSource = dtMaterial;
            rptMaterialCollapse.DataBind();
            //foreach (DataRow item in dtMaterial.Rows)
            //{
            //    foreach (ListItem item1 in rptMaterialCollapse.Items)
            //    {
            //        if (item1.Value.ToString() == item["Material_ID"].ToString())
            //        {
            //            item1.Selected = true;
            //        }
            //    }
            //}
        }
        else
        {
            Data1.Visible = false;
            rptMaterialCollapse.DataSource = null;
            rptMaterialCollapse.DataBind();
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
                            _row["AmountInWareHouse"] = BusinessRulesLocator.Conllection().GetAllList("select ISNULL( SUM(Amount),0) as TongNhap from WarehouseImportProductPackage where WarehouseImport_ID = " + WarehouseImport_ID).Rows[0]["TongNhap"].ToString();
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
                    Literal lblWarehouseExportMaterial_ID = item.FindControl("lblWarehouseExportMaterial_ID") as Literal;

                    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    if (lblMaterial_ID != null)
                    {
                        if (lblWarehouseExportMaterial_ID.Text != "")
                        {
                            WarehouseExportMaterialRow _WarehouseExportMaterialRow = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetByPrimaryKey(Convert.ToInt32(lblWarehouseExportMaterial_ID.Text));
                            //_ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                            _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                            _WarehouseExportMaterialRow.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                            if (!string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantity.Text);
                            }
                            BusinessRulesLocator.GetWarehouseExportMaterialBO().Update(_WarehouseExportMaterialRow);
                        }
                        else
                        {
                            WarehouseExportMaterialRow _WarehouseExportMaterialRow = new WarehouseExportMaterialRow(); ;
                            //_ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                            _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                            _WarehouseExportMaterialRow.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                            if (!string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantity.Text);
                            }
                            BusinessRulesLocator.GetWarehouseExportMaterialBO().Insert(_WarehouseExportMaterialRow);
                        }
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
    string ListWareHouse_ID = string.Empty;
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlWarehouse.SelectedValue != "")
        {
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
    protected void ddlProductPackageOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductPackageOrder.SelectedValue != "")
        {
            ProductPackageOrder_ID = Convert.ToInt32(ddlProductPackageOrder.SelectedValue);
            LoadProduct(ProductPackageOrder_ID);
        }

        LoadMaterial(ProductPackageOrder_ID);
        FillMaterialChoose(WarehouseExport_ID);
    }
    protected void LoadMaterial(int ProductPackageOrder_ID)
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

            DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(@"select Material_ID from ProductPackageOrderMaterial where ProductPackageOrder_ID = " + ProductPackageOrder_ID);
            if (dtMaterial.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtMaterial.Rows)
                {
                    if (dtRow["Material_ID"].ToString() != null)
                    {

                        DataTable dtMaterialByWarehouse = BusinessRulesLocator.Conllection().GetAllList(@"select Material_ID from Material where Warehouse_ID like N'%" + ddlWarehouseNhom2.SelectedValue + "%'");
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
    private void FillDDLddlWarehouseNhom4()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

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
    protected void Update_WarehouseExportMaterial(int WarehouseExport_ID)
    {
        try
        {
            foreach (RepeaterItem item in rptMaterial.Items)
            {
                Literal lblMaterialID = (Literal)item.FindControl("lblMaterialID");
                Literal lblWarehouseImport_ID = (Literal)item.FindControl("lblWarehouseImport_ID");
                Literal lblWarehouseExportMaterial = (Literal)item.FindControl("lblWarehouseExportMaterial");
                CheckBox ckMaterialID1 = (CheckBox)item.FindControl("ckMaterialID1");
                Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                DropDownList ddlNumber = item.FindControl("ddlNumber") as DropDownList;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;


                if (ckMaterialID1.Checked)
                {
                    if (lblWarehouseExportMaterial.Text != "")
                    {
                        WarehouseExportMaterialRow _WarehouseExportMaterial = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetByPrimaryKey(Convert.ToInt32(lblWarehouseExportMaterial.Text));
                        _WarehouseExportMaterial.WarehouseExport_ID = WarehouseExport_ID;
                        _WarehouseExportMaterial.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                        _WarehouseExportMaterial.WarehouseImport_ID = Convert.ToInt32(lblWarehouseImport_ID.Text);
                        _WarehouseExportMaterial.Amount = double.Parse(string.IsNullOrEmpty(txtQuantity.Text) ? "0" : txtQuantity.Text);
                        BusinessRulesLocator.GetWarehouseExportMaterialBO().Update(_WarehouseExportMaterial);
                    }

                }


            }
        }
        catch (Exception ex)
        {
            Log.writeLog("ProductPackageOrderMaterial", ex.ToString());
        }
    }
    public decimal Calculate(string Warehouse4_ID, string Product4_ID, string curent)
    {
        if (!string.IsNullOrEmpty(curent))
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"Select ISNULL((SUM(P.Total)),0) as TotalInWarehouse from (Select (Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage where WarehouseImport_ID =WI.WarehouseImport_ID)  as Total,WI.Product_ID, PP.Name  from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where  WI.WarehouseImport_ID in (Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ") and WI.Active = 1 and WI.Warehouse_ID =" + Warehouse4_ID + " and WI.Product_ID in (" + Product4_ID + ")) p").Rows[0]["TotalInWarehouse"].ToString());
        }
        else
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"Select ISNULL((SUM(P.Total)),0) as TotalInWarehouse from (Select (Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage where WarehouseImport_ID =WI.WarehouseImport_ID)  as Total,WI.Product_ID, PP.Name  from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where  WI.Active = 1 and WI.Warehouse_ID =" + Warehouse4_ID + " and WI.Product_ID in (" + Product4_ID + ")) p").Rows[0]["TotalInWarehouse"].ToString());
        }
    }
    protected decimal CalculateAmountExport(string Product_ID, string curentEx)
    {
        if (!string.IsNullOrEmpty(curentEx))
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseExport_ID = " + WarehouseExport_ID + " and WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.WarehouseImport_ID in (Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ") and WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
        }
        else
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where  WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
        }
    }
    protected decimal CalculateAmountExportAll(string Product_ID, string curentEx)
    {
        if (!string.IsNullOrEmpty(curentEx))
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.WarehouseImport_ID in (Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ") and WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
        }
        else
        {
            return decimal.Parse(BusinessRulesLocator.Conllection().GetAllList(@"select ISNULL( SUM(Amount),0) as TongXuat from WarehouseExportMaterial where WarehouseImport_ID  in (Select WIC.WarehouseImport_ID from WarehouseImport WIC where  WIC.Product_ID =" + Product_ID + ")").Rows[0]["TongXuat"].ToString());
        }
    }
    protected int CheckCurentMaterial(string Product_ID)
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"Select WIC.WarehouseImport_ID from WarehouseImport WIC where WIC.WarehouseImport_ID in (Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ") and WIC.Product_ID =" + Product_ID + "");
        if (dt.Rows.Count > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
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
            dtTreeList.Columns.Add("WarehouseExportMaterial_ID");
            dtTreeList.Columns.Add("Name");
            dtTreeList.Columns.Add("Unit");
            dtTreeList.Columns.Add("AmountInWareHouse");
            dtTreeList.Columns.Add("AmountExport");
            dtTreeList.Columns.Add("Parent_ID");


            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"Select Distinct P.Product_ID, P.Name ,P.ExpectedProductivityDescription from WarehouseImport WI left join Product P on WI.Product_ID = P.Product_ID where WI.Active = 1 and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue + "and WI.Product_ID in (" + ADDListMaterial4_ID().Substring(0, ADDListMaterial4_ID().Length - 1) + ")");

            foreach (DataRow item in dt.Rows)
            {
                if (CheckCurentMaterial(item["Product_ID"].ToString()) > 0)
                {
                    DataRow itemTreeList = dtTreeList.NewRow();
                    itemTreeList["ID"] = item["Product_ID"];
                    itemTreeList["Parent_ID"] = "";
                    itemTreeList["Name"] = item["Name"] + " - <b>Lô tổng</b>";
                    itemTreeList["AmountInWareHouse"] = (Calculate(ddlWarehouse.SelectedValue, item["Product_ID"].ToString(), "Curent") - CalculateAmountExportAll(item["Product_ID"].ToString(), "Curent")).ToString("N0");
                    itemTreeList["AmountExport"] = CalculateAmountExport(item["Product_ID"].ToString(), "Curent");
                    itemTreeList["Unit"] = item["ExpectedProductivityDescription"];
                    dtTreeList.Rows.Add(itemTreeList);
                    if (item["Product_ID"] != null)
                    {
                        DataTable dtChild = new DataTable();
                        dtChild = BusinessRulesLocator.Conllection().GetAllList(@"Select ((Select IsNull(SUM(Amount),0) from WarehouseImportProductPackage where WarehouseImport_ID =WI.WarehouseImport_ID) - (Select IsNull(SUM(Amount),0) from WarehouseExportMaterial where WarehouseImport_ID =WI.WarehouseImport_ID )) as Total, (Select IsNull(SUM(Amount),0) from WarehouseExportMaterial where WarehouseImport_ID =WI.WarehouseImport_ID and WarehouseExport_ID = " + WarehouseExport_ID + ") as AmountExport, (Select WarehouseExportMaterial_ID from WarehouseExportMaterial where WarehouseImport_ID =WI.WarehouseImport_ID and WarehouseExport_ID = " + WarehouseExport_ID + ") as WarehouseExportMaterial_ID ,WI.Product_ID as Product_IDChild,CONCAT(WI.Name +' - ',WI.Code) as NameW,PP.ExpectedProductivityDescription, WI.WarehouseImport_ID from WarehouseImport WI left join Product PP on WI.Product_ID = PP.Product_ID where WI.WarehouseImport_ID in (Select WarehouseImport_ID from WarehouseExportMaterial where WarehouseExport_ID =" + WarehouseExport_ID + ") and WI.Active = 1 and WI.Warehouse_ID =" + ddlWarehouse.SelectedValue + " and WI.Product_ID in (" + item["Product_ID"].ToString() + ") ORDER BY WI.CREATEDATE ASC");
                        {
                            foreach (DataRow itemChild in dtChild.Rows)
                            {
                                if (decimal.Parse(itemChild["Total"].ToString()) >= 0)
                                {
                                    itemTreeList = dtTreeList.NewRow();
                                    itemTreeList["ID"] = itemChild["WarehouseImport_ID"];
                                    itemTreeList["Parent_ID"] = itemChild["Product_IDChild"];
                                    itemTreeList["Name"] = itemChild["NameW"];
                                    itemTreeList["AmountInWareHouse"] = decimal.Parse(itemChild["Total"].ToString()).ToString("N0");
                                    itemTreeList["AmountExport"] = decimal.Parse(itemChild["AmountExport"].ToString()).ToString("N0");
                                    itemTreeList["Unit"] = itemChild["ExpectedProductivityDescription"];
                                    itemTreeList["WarehouseExportMaterial_ID"] = itemChild["WarehouseExportMaterial_ID"];
                                    dtTreeList.Rows.Add(itemTreeList);
                                }
                            }

                        }
                    }
                }
                else
                {
                    DataRow itemTreeList = dtTreeList.NewRow();
                    itemTreeList["ID"] = item["Product_ID"];
                    itemTreeList["Parent_ID"] = "";
                    itemTreeList["Name"] = item["Name"] + " - <b>Lô tổng</b>";
                    itemTreeList["AmountInWareHouse"] = (Calculate(ddlWarehouse.SelectedValue, item["Product_ID"].ToString(), "") - CalculateAmountExport(item["Product_ID"].ToString(), "")).ToString("N0");
                    itemTreeList["AmountExport"] = "0";
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
                                    itemTreeList["AmountExport"] = "0";
                                    itemTreeList["Unit"] = itemChild["ExpectedProductivityDescription"];
                                    dtTreeList.Rows.Add(itemTreeList);
                                }
                            }

                        }
                    }
                }
            }
            RadTreeList1.DataSource = dtTreeList;

        }
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
                                Literal lblWarehouseExportMaterial_ID = Child.FindControl("lblWarehouseExportMaterial_ID") as Literal;
                                Flag += Child["ID"].Text + ",";
                                if (!string.IsNullOrEmpty(txtQuantityChild.Text))
                                {
                                    if (decimal.Parse(txtQuantityChild.Text) > 0 && !string.IsNullOrEmpty(lblWarehouseExportMaterial_ID.Text))
                                    {
                                        WarehouseExportMaterialRow _WarehouseExportMaterialRow = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetByPrimaryKey(Convert.ToInt32(lblWarehouseExportMaterial_ID.Text));

                                        _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                                        _WarehouseExportMaterialRow.Material_ID = 0;
                                        _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(Child["ID"].Text);
                                        //Update Amount for Productpackage import
                                        _WarehouseExportMaterialRow.Amount = Convert.ToDouble(txtQuantityChild.Text);
                                        BusinessRulesLocator.GetWarehouseExportMaterialBO().Update(_WarehouseExportMaterialRow);

                                    }
                                    else if (decimal.Parse(txtQuantityChild.Text) > 0 && string.IsNullOrEmpty(lblWarehouseExportMaterial_ID.Text))
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
                                        DataTable dtWarehouseExportMaterial = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetAsDataTable("WarehouseImport_ID = " + dtRow["WarehouseImport_ID"].ToString() + " and WarehouseExport_ID = " + WarehouseExport_ID, "");
                                        if (dtWarehouseExportMaterial.Rows.Count > 0)
                                        {
                                            foreach (DataRow dtWarehouseExportMaterialRow in dtWarehouseExportMaterial.Rows)
                                            {

                                                WarehouseExportMaterialRow _WarehouseExportMaterialRow = BusinessRulesLocator.GetWarehouseExportMaterialBO().GetByPrimaryKey(Convert.ToInt32(dtWarehouseExportMaterialRow["WarehouseExportMaterial_ID"].ToString()));

                                                _WarehouseExportMaterialRow.WarehouseExport_ID = WarehouseExport_ID;
                                                _WarehouseExportMaterialRow.Material_ID = 0;
                                                _WarehouseExportMaterialRow.WarehouseImport_ID = Convert.ToInt32(dtRow["WarehouseImport_ID"].ToString());
                                                if (decimal.Parse(dtRow["Total"].ToString()) <= decimal.Parse(txtQuantity.Text))
                                                {
                                                    _WarehouseExportMaterialRow.Amount = Convert.ToDouble(_WarehouseExportMaterialRow.Amount) + Convert.ToDouble(dtRow["Total"].ToString());
                                                    // Số lượng còn lại
                                                    txtQuantity.Text = (decimal.Parse(txtQuantity.Text) - decimal.Parse(dtRow["Total"].ToString())).ToString();
                                                }
                                                else if (decimal.Parse(dtRow["Total"].ToString()) >= decimal.Parse(txtQuantity.Text))
                                                {
                                                    _WarehouseExportMaterialRow.Amount = Convert.ToDouble(_WarehouseExportMaterialRow.Amount) + Convert.ToDouble(txtQuantity.Text);
                                                    txtQuantity.Text = "0";
                                                }
                                                BusinessRulesLocator.GetWarehouseExportMaterialBO().Update(_WarehouseExportMaterialRow);

                                            }
                                        }
                                        else
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
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
}