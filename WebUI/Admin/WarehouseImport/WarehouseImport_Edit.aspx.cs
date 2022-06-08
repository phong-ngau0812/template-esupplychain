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

public partial class WarehouseImport_Edit : System.Web.UI.Page
{
    int WarehouseImport_ID = 0;
    public string title = "Thông tin nhập kho";
    public string avatar = "";
    public int Type;
    public decimal TotalProductPackage = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(Request["WarehouseImport_ID"]))
            int.TryParse(Request["WarehouseImport_ID"].ToString(), out WarehouseImport_ID);
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            UnitMaterila.Visible = false;
            Product.Visible = false;
            NameMaterial.Visible = false;
            NCC.Visible = false;
            // CheckUserXuanHoa();
            FillDDLddlProductBrand();
            FillDDLddlWarehouse();
            FillDDLddlMaterial();
            FillSupplier();
            FillProduct();
            // LoadWarehouse();
            //FillDDLddlMaterialType();
            FillProductPackage();
            FillGio();

            //  ThemMoi.Visible = false;
            //DaCo.Visible = true;

            FillData();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void CheckUserXuanHoa()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select UserId,UserName from aspnet_Users where UserId = '" + MyUser.GetUser_ID() + "'");
        if (dt.Rows.Count > 0)
        {
            if (MyUser.GetFunctionGroup_ID() == "2" && dt.Rows[0]["UserName"].ToString() == "tpphucyen.xuanhoa")
            {
                xuanhoa.Visible = true;
            }
            else
            {
                xuanhoa.Visible = false;
            }
        }
    }
    private void FillProduct()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"Select * from Product where Active = 1" + where);
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("--Chọn sản phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductPackage", ex.ToString());
        }
    }
    private void FillData()
    {
        WarehouseImportRow _WarehouseImportRow = BusinessRulesLocator.GetWarehouseImportBO().GetByPrimaryKey(WarehouseImport_ID);
        if (_WarehouseImportRow != null)
        {
            txtCode.Text = _WarehouseImportRow.IsCodeNull ? string.Empty : _WarehouseImportRow.Code;
            ddlWarehouse.SelectedValue = _WarehouseImportRow.IsWarehouse_IDNull ? "" : _WarehouseImportRow.Warehouse_ID.ToString();

            if (!_WarehouseImportRow.IsWarehouseImportType_IDNull)
            {
                if (_WarehouseImportRow.WarehouseImportType_ID == 2)
                {
                    Product.Visible = true;
                    NameMaterial.Visible = false;
                    NCC.Visible = false;
                    sp.Visible = true;
                    vattu.Visible = false;
                    DataTable dtWIvsProductPackage = BusinessRulesLocator.Conllection().GetAllList(@"Select WIP.*,P.Name,PP.ExpectedProductivityDescription as Unit from WarehouseImportProductPackage WIP left join ProductPackage P on WIP.ProductPackage_ID = P.ProductPackage_ID left join Product PP on P.Product_ID = PP.Product_ID where P.ProductPackageStatus_ID<>6 and WIP.WarehouseImport_ID=" + _WarehouseImportRow.WarehouseImport_ID);
                    if (dtWIvsProductPackage.Rows.Count > 0)
                    {
                        Data1.Visible = true;
                        rptProductPackage.DataSource = dtWIvsProductPackage;
                        rptProductPackage.DataBind();
                        foreach (DataRow dt in dtWIvsProductPackage.Rows)
                        {
                            foreach (ListItem item in ddlProductPackage.Items)
                            {
                                if (dt["ProductPackage_ID"].ToString() == item.Value.ToString())
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Data1.Visible = false;
                    }
                }
                else
                {
                    Product.Visible = false;
                    NameMaterial.Visible = true;
                    NCC.Visible = true;
                    xuanhoa.Visible = true;
                    sp.Visible = false;
                    vattu.Visible = true;
                }
            }

            ddlMaterial.SelectedValue = _WarehouseImportRow.IsMaterial_IDNull ? "" : _WarehouseImportRow.Material_ID.ToString();
            ddlSupplier.SelectedValue = _WarehouseImportRow.IsSupplier_IDNull ? "" : _WarehouseImportRow.Supplier_ID.ToString();
            txtPrice.Text = _WarehouseImportRow.IsPriceNull ? "" : _WarehouseImportRow.Price.ToString("N0");

            ddlProduct.SelectedValue = _WarehouseImportRow.IsProduct_IDNull ? "" : _WarehouseImportRow.Product_ID.ToString();

            txtMaterialPackage.Text = _WarehouseImportRow.IsCodeMaterialPackageNull ? "" : _WarehouseImportRow.CodeMaterialPackage.ToString();
            //ddlProductPackage.SelectedValue = _WarehouseImportRow.IsProductPackage_IDNull ? "" : _WarehouseImportRow.ProductPackage_ID.ToString();
            txtAmount.Text = _WarehouseImportRow.IsAmountNull ? "0" : _WarehouseImportRow.Amount.ToString();
            //txtWarrantyDate.Text = _WarehouseImportRow.IsWarrantyDateNull ? "0" : _WarehouseImportRow.WarrantyDate.ToString();
            txtNgayDukien.Text = _WarehouseImportRow.IsImportDateNull ? string.Empty : _WarehouseImportRow.ImportDate.ToString("dd/MM/yyyy");
            txtSX.Text = _WarehouseImportRow.IsWarrantyEndDateNull ? string.Empty : _WarehouseImportRow.WarrantyEndDate.ToString("dd/MM/yyyy");
            if (!_WarehouseImportRow.IsImportDateNull)
            {
                ddlHour.SelectedValue = _WarehouseImportRow.ImportDate.Hour.ToString();
                ddlMinutes.SelectedValue = _WarehouseImportRow.ImportDate.Minute.ToString();
            }
            txtImprot.Text = _WarehouseImportRow.IsImporterNull ? string.Empty : _WarehouseImportRow.Importer.ToString();
            txtSender.Text = _WarehouseImportRow.IsSenderNull ? string.Empty : _WarehouseImportRow.Sender.ToString();
            txtNote.Text = _WarehouseImportRow.IsDescriptionNull ? string.Empty : _WarehouseImportRow.Description.ToString();


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

    private void FillDDLddlMaterial()
    {
        try
        {
            string where = "";

            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;

            }
            if (ddlSupplier.SelectedValue != "")
            {
                where += " and M.Supplier_ID like N'%," + ddlSupplier.SelectedValue + "%'";
            }
            if (ddlWarehouse.SelectedValue != "")
            {
                where += " and M.Warehouse_ID like N'%," + ddlWarehouse.SelectedValue + "%'";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select  CONCAT(M.Name +' - ', M.CodePrivate) as NameMaterial,M.Material_ID from Material M inner join ProductBrand PB on M.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and M.Active=1 " + where);
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataTextField = "NameMaterial";
            ddlMaterial.DataValueField = "Material_ID";
            ddlMaterial.DataBind();
            ddlMaterial.Items.Insert(0, new ListItem("-- Chọn vật tư --", ""));

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillProductPackage()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = BusinessRulesLocator.GetProductPackageBO().GetAsDataTable("ProductPackageStatus_ID <> 6" + where, "");
            ddlProductPackage.DataSource = dt;
            ddlProductPackage.DataTextField = "Name";
            ddlProductPackage.DataValueField = "ProductPackage_ID";
            ddlProductPackage.DataBind();
            ddlProductPackage.Items.Insert(0, new ListItem("--Chọn Lô SX --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductPackage", ex.ToString());
        }
    }
    private void FillSupplier()
    {
        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            DataTable dt = BusinessRulesLocator.GetSupplierBO().GetAsDataTable("Active = 1 " + where, "");
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "Supplier_ID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("--Chọn nhà cung cấp--", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductPackage", ex.ToString());
        }
    }
    private void LoadWarehouse()
    {
        //ddlWarehouse.Items.Clear();
        //if (ddlMaterial.SelectedValue != "")
        //{
        //    MaterialRow _MaterialRow = new MaterialRow();
        //    _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Convert.ToInt32(ddlMaterial.SelectedValue));
        //    if (_MaterialRow != null)
        //    {
        //        if (!_MaterialRow.IsWarehouse_IDNull)

        //        {
        //            string[] array = _MaterialRow.Warehouse_ID.Split(',');
        //            foreach (string value in array)
        //            {
        //                WarehouseRow _WarehouseRow = new WarehouseRow();
        //                if (!string.IsNullOrEmpty(value))
        //                {
        //                    _WarehouseRow = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Convert.ToInt32(value));
        //                    if (_WarehouseRow != null)
        //                    {
        //                        ddlWarehouse.Items.Insert(0, new ListItem(_WarehouseRow.IsNameNull?string.Empty:_WarehouseRow.Name, value));
        //                    }
        //                }
        //                //foreach (DropDownList item in ddlWarehouse.SelectedItem)
        //                //{
        //                //    if (value == item.SelectedValue)
        //                //    {
        //                //        item.SelectedValue = ddlWarehouse.SelectedValue ;
        //                //    }
        //                //}
        //            }

        //        }
        //    }
        //}

        ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
    }
    private void FillDDLddlWarehouse()
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
            ddlWarehouse.DataSource = dt;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "Warehouse_ID";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
            if (MyUser.GetWarehouse_ID() != "")
            {
                ddlWarehouse.SelectedValue = MyUser.GetWarehouse_ID();
                ddlWarehouse.Enabled = false;
                LoadByWarehouse();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    //private void FillDDLddlMaterialType()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable(" Active<>-1", " Sort ASC");
    //        ddlMaterialType.DataSource = dt;
    //        ddlMaterialType.DataTextField = "Name";
    //        ddlMaterialType.DataValueField = "MaterialCategory_ID";
    //        ddlMaterialType.DataBind();
    //        ddlMaterialType.Items.Insert(0, new ListItem("-- Chọn loại vật tư --", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        Log.writeLog("FillDDLCha", ex.ToString());
    //    }
    //}


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddWarehouseImportMaterialhasbeen();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckStaffTypeName(string Code)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Code))
        {
            where += " and Name=N'" + Code.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetStaffTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }

    protected void AddMaterial()
    {
        try
        {

            MaterialRow _MaterialRow = new MaterialRow();
            _MaterialRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            //_MaterialRow.MaterialCategory_ID = Convert.ToInt32(ddlMaterialType.SelectedValue);
            //_MaterialRow.Name = string.IsNullOrEmpty(txtMaterial.Text) ? string.Empty : txtMaterial.Text;
            _MaterialRow.Unit = string.IsNullOrEmpty(txtUnit.Text) ? string.Empty : txtUnit.Text;
            _MaterialRow.Active = true;
            _MaterialRow.CreateBy = MyUser.GetUser_ID();
            _MaterialRow.CreateDate = DateTime.Now;
            BusinessRulesLocator.GetMaterialBO().Insert(_MaterialRow);
            if (!_MaterialRow.IsMaterial_IDNull)
            {
                AddWarehouseImportMaterialNew(_MaterialRow.Material_ID, _MaterialRow.Name);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("AddMaterial", ex.ToString());
        }
    }
    protected void AddWarehouseImportMaterialNew(int Material_ID, string NameMaterial)
    {
        try
        {
            //if (CheckStaffTypeName(txtCode.Text.Trim()))
            //{

            WarehouseImportRow _WarehouseImportRow = new WarehouseImportRow();
            _WarehouseImportRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _WarehouseImportRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
            _WarehouseImportRow.WarehouseImportType_ID = 1;
            _WarehouseImportRow.Product_ID = 0;
            _WarehouseImportRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _WarehouseImportRow.Importer = string.IsNullOrEmpty(txtImprot.Text) ? string.Empty : txtImprot.Text;
            //_WarehouseImportRow.Exporter = "";


            _WarehouseImportRow.Material_ID = Material_ID;
            _WarehouseImportRow.Name = string.IsNullOrEmpty(NameMaterial) ? string.Empty : NameMaterial;


            if (!string.IsNullOrEmpty(txtAmount.Text))
            {
                string Amount = txtAmount.Text.Replace(",", "");
                _WarehouseImportRow.Amount = Convert.ToDecimal(Amount);
            }
            else
            {
                _WarehouseImportRow.Amount = Convert.ToDecimal("0");
            }

            _WarehouseImportRow.Active = 1;

            _WarehouseImportRow.LastEditBy = _WarehouseImportRow.CreateBy = MyUser.GetUser_ID();
            _WarehouseImportRow.LastEditDate = _WarehouseImportRow.CreateDate = DateTime.Now;

            BusinessRulesLocator.GetWarehouseImportBO().Insert(_WarehouseImportRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaffType", ex.ToString());
        }
    }


    protected void AddWarehouseImportMaterialhasbeen()
    {
        try
        {
            //if (CheckStaffTypeName(txtCode.Text.Trim()))
            //{

            WarehouseImportRow _WarehouseImportRow = BusinessRulesLocator.GetWarehouseImportBO().GetByPrimaryKey(WarehouseImport_ID);
            if (_WarehouseImportRow != null)
            {
                _WarehouseImportRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                _WarehouseImportRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                _WarehouseImportRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                _WarehouseImportRow.Importer = string.IsNullOrEmpty(txtImprot.Text) ? string.Empty : txtImprot.Text;
                _WarehouseImportRow.Sender = string.IsNullOrEmpty(txtSender.Text) ? string.Empty : txtSender.Text;
                _WarehouseImportRow.Warehouse_ID = Convert.ToInt32(ddlWarehouse.SelectedValue);
                if (hdfWare.Value == "2")
                {
                    _WarehouseImportRow.Product_ID = Convert.ToInt32(ddlProduct.SelectedValue);
                    _WarehouseImportRow.WarehouseImportType_ID = 2;
                    _WarehouseImportRow.Name = string.IsNullOrEmpty(ddlProduct.SelectedItem.Text) ? string.Empty : ddlProduct.SelectedItem.Text.ToString();
                    ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (_ProductRow != null)
                    {
                        _WarehouseImportRow.Unit = string.IsNullOrEmpty(_ProductRow.Unit.ToString()) ? string.Empty : _ProductRow.Unit.ToString();

                    }
                }
                else
                {
                    if (ddlMaterial.SelectedValue != "")
                    {
                        MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Convert.ToInt32(ddlMaterial.SelectedValue));
                        if (_MaterialRow != null)
                        {
                            _WarehouseImportRow.Unit = string.IsNullOrEmpty(_MaterialRow.Unit.ToString()) ? string.Empty : _MaterialRow.Unit.ToString();
                        }
                        _WarehouseImportRow.Name = _MaterialRow.Name;
                    }
                    //_WarehouseImportRow.Name = string.IsNullOrEmpty(ddlMaterial.SelectedItem.Text) ? string.Empty : ddlMaterial.SelectedItem.Text.ToString();
                    _WarehouseImportRow.Material_ID = Convert.ToInt32(ddlMaterial.SelectedValue);
                    _WarehouseImportRow.Supplier_ID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    _WarehouseImportRow.CodeMaterialPackage = string.IsNullOrEmpty(txtMaterialPackage.Text) ? string.Empty : txtMaterialPackage.Text;
                    _WarehouseImportRow.WarehouseImportType_ID = 1;
                }
                //_WarehouseImportRow.WarrantyDate = string.IsNullOrEmpty(txtWarrantyDate.Text) ? 0 : Convert.ToInt32(txtWarrantyDate.Text);
                if (!string.IsNullOrEmpty(txtNgayDukien.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtNgayDukien.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ngaycap = ngaycap.AddHours(Convert.ToInt32(ddlHour.SelectedValue));
                    ngaycap = ngaycap.AddMinutes(Convert.ToInt32(ddlMinutes.SelectedValue));
                    ngaycap = ngaycap.AddSeconds(DateTime.Now.Second);
                    _WarehouseImportRow.ImportDate = ngaycap;
                }
                if (!string.IsNullOrEmpty(txtSX.Text.Trim()))
                {
                    DateTime ngaycap = DateTime.ParseExact(txtSX.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    _WarehouseImportRow.WarrantyEndDate = ngaycap;
                }
                _WarehouseImportRow.Code = string.IsNullOrEmpty(txtCode.Text) ? string.Empty : txtCode.Text;
                if (ddlProductPackage.SelectedValue != "")
                {
                    _WarehouseImportRow.ProductPackage_ID = Convert.ToInt32(ddlProductPackage.SelectedValue);
                }
                //_WarehouseImportRow.Exporter = "";


                _WarehouseImportRow.Name = string.IsNullOrEmpty(ddlMaterial.SelectedItem.Text) ? string.Empty : ddlMaterial.SelectedItem.Text.ToString();


                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    string Amount = txtAmount.Text.Replace(",", "");
                    _WarehouseImportRow.Amount = Convert.ToDecimal(Amount);
                }
                else
                {
                    _WarehouseImportRow.Amount = Convert.ToDecimal("0");
                }

                _WarehouseImportRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToDouble(txtPrice.Text.Replace(",", ""));
                _WarehouseImportRow.LastEditBy = MyUser.GetUser_ID();
                _WarehouseImportRow.LastEditDate = DateTime.Now;

                BusinessRulesLocator.GetWarehouseImportBO().Update(_WarehouseImportRow);
                lblMessage.Text = "Cập Nhật Thành Công!";
                lblMessage.Visible = true;
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateStaffType", ex.ToString());
        }
    }

    protected void ClearForm()
    {


        ddlWarehouse.SelectedValue = "";
        ddlMaterial.SelectedValue = "";
        txtNote.Text = "";
        txtAmount.Text = " ";
        txtImprot.Text = " ";
        //txtMaterial.Text = "";
        //ddlMaterialType.SelectedValue = "";

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
        //txtSX.Attributes.Add("readonly", "true");
        //txtHSD.Attributes.Add("readonly", "true");
        txtNgayDukien.Attributes.Add("readonly", "true");
        //txtThuHoach.Attributes.Add("readonly", "true");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseImport_List.aspx", false);
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlWarehouse();
        FillDDLddlMaterial();
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadByWarehouse();
        FillDDLddlMaterial();
    }
    protected void LoadByWarehouse()
    {

        if (ddlWarehouse.SelectedValue != "")
        {
            WarehouseRow _Row = BusinessRulesLocator.GetWarehouseBO().GetByPrimaryKey(Convert.ToInt32(ddlWarehouse.SelectedValue));
            if (_Row != null)
            {
                if (_Row.Type == 2)
                {

                    hdfWare.Value = "2";
                    Product.Visible = true;
                    NameMaterial.Visible = false;
                    NCC.Visible = false;
                    xuanhoa.Visible = false;
                    //vattu.Visible = false;
                    //sp.Visible = true;
                }
                else
                {
                    if (Common.CheckUserXuanHoa1())
                    {
                        hdfWare.Value = "1";
                        Product.Visible = false;
                        NameMaterial.Visible = true;
                        NCC.Visible = true;
                        xuanhoa.Visible = true;
                        //vattu.Visible = true;
                        //sp.Visible = false;
                    }
                    else
                    {
                        hdfWare.Value = "1";
                        Product.Visible = false;
                        NameMaterial.Visible = true;
                        NCC.Visible = true;
                        xuanhoa.Visible = false;
                        //vattu.Visible = true;
                        //sp.Visible = false;

                    }

                }
            }
        }
        else
        {
            Product.Visible = false;
            NameMaterial.Visible = false;
            NCC.Visible = false;
        }

    }
    protected void rdoDaCo_CheckedChanged(object sender, EventArgs e)
    {
        // ThemMoi.Visible = false;
        //  DaCo.Visible = true;
        UnitMaterila.Visible = false;

    }

    protected void rdo_CheckedChanged(object sender, EventArgs e)
    {
        // ThemMoi.Visible = true;
        // DaCo.Visible = false;
        UnitMaterila.Visible = true;

    }

    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  FillDDLddlMaterial();
    }
    protected void rptProductPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox lblTotal = e.Item.FindControl("txtQuantity") as TextBox;
            if (lblTotal != null)
            {
                TotalProductPackage += decimal.Parse(lblTotal.Text);
            }
        }
    }
    protected void ddlProductPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlProductPackage.SelectedValue != "")
        //{
        //    LoadTableProductPackage();
        //    //  nhom2.Visible = false;
        //}
        //else
        //{
        //    //nhom2.Visible = true;
        //    LoadTableProductPackage();
        //}
    }
}