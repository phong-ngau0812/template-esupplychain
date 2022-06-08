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

public partial class Material_Edit : System.Web.UI.Page
{
    int Material_ID = 0;
    private int MaterialPrice_ID = 0;
    public string title = "Thông tin vật tư";
    public string avatar = "";
    public string mode = string.Empty;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Material_ID"]))
            int.TryParse(Request["Material_ID"].ToString(), out Material_ID);
        if (!string.IsNullOrEmpty(Request["mode"]))
            mode = Request["mode"].ToString();

        if (mode == "setPrice")
        {
            T2.Attributes.Add("class", "nav-link active");
            T1.Attributes.Add("class", "nav-link ");
            tab2.Attributes.Add("class", "tab-pane mt-3 active");
            tab1.Attributes.Add("class", "tab-pane mt-3 ");

        }
        if (!string.IsNullOrEmpty(Request["MaterialPrice_ID"]))
            int.TryParse(Request["MaterialPrice_ID"].ToString(), out MaterialPrice_ID);
        CheckUserXuanHoa();
        if (!IsPostBack)
        {

            FillInfoMateria();
            LoadMaterialPrice();
            FiddMaterialPrice();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    protected void CheckUserXuanHoa()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select UserId,UserName from aspnet_Users where UserId = '" + MyUser.GetUser_ID() + "'");
        if (dt.Rows.Count > 0)
        {
            if (MyUser.GetFunctionGroup_ID() == "2" && MyUser.GetProductBrand_ID() == "1524")
            {
                xuanhoa.Visible = true;
            }
            else
            {
                xuanhoa.Visible = false;
            }
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
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    private void FillDDLddlMateriaType()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable("Active<>-1", " Sort ASC");
            ddlMateriaType.DataSource = dt;
            ddlMateriaType.DataTextField = "Name";
            ddlMateriaType.DataValueField = "MaterialCategory_ID";
            ddlMateriaType.DataBind();
            ddlMateriaType.Items.Insert(0, new ListItem("-- Chọn loại vật tư --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    private void FillDDLddlSupplier()
    {

        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetSupplierBO().GetAsDataTable("Active=1" + where, " Sort ASC");
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "Supplier_ID";
            ddlSupplier.DataBind();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLSupplier", ex.ToString());
        }
    }

    private void FillDDLddWarehouse()
    {

        try
        {
            string where = "";
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID = " + ddlProductBrand.SelectedValue;
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetWarehouseBO().GetAsDataTable("Active=1 and Type = 1" + where, " ");
            ddWarehouse.DataSource = dt;
            ddWarehouse.DataTextField = "Name";
            ddWarehouse.DataValueField = "Warehouse_ID";
            ddWarehouse.DataBind();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLWarehouse", ex.ToString());
        }
    }
    protected void FillInfoMateria()
    {
        try
        {
            if (Material_ID != 0)
            {
                MaterialRow _MaterialRow = new MaterialRow();
                _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);

                if (_MaterialRow != null)
                {
                    FillDDLddlProductBrand();
                    ddlProductBrand.SelectedValue = _MaterialRow.IsProductBrand_IDNull ? string.Empty : _MaterialRow.ProductBrand_ID.ToString();
                    FillDDLddlMateriaType();
                    FillDDLddlSupplier();
                    FillDDLddWarehouse();
                    ddlMateriaType.SelectedValue = _MaterialRow.IsMaterialCategory_IDNull ? string.Empty : _MaterialRow.MaterialCategory_ID.ToString();
                    txtcode.Text = _MaterialRow.IsCodeNull ? string.Empty : _MaterialRow.Code;
                    txtName.Text = _MaterialRow.IsNameNull ? string.Empty : _MaterialRow.Name;
                    txtPackingType.Text = _MaterialRow.IsPackingTypeNull ? string.Empty : _MaterialRow.PackingType;
                    txtUnit.Text = _MaterialRow.IsUnitNull ? string.Empty : _MaterialRow.Unit;
                    txtIsolationDay.Text = _MaterialRow.IsIsolationDayNull ? string.Empty : _MaterialRow.IsolationDay.ToString();
                    txtNote.Text = _MaterialRow.IsDescriptionNull ? string.Empty : _MaterialRow.Description;
                    txtGTIN.Text = _MaterialRow.IsCodeNull ? string.Empty : _MaterialRow.Code;
                    txtCodePrivate.Text = _MaterialRow.IsCodePrivateNull ? string.Empty : _MaterialRow.CodePrivate;

                    if (!_MaterialRow.IsSupplier_IDNull)

                    {
                        string[] array = _MaterialRow.Supplier_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddlSupplier.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }


                    if (!_MaterialRow.IsWarehouse_IDNull)

                    {
                        string[] array = _MaterialRow.Warehouse_ID.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddWarehouse.Items)
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
        catch (Exception ex)
        {
            Log.writeLog("FillInfoFarm", ex.ToString());
        }
    }


    private string ADDSupplier_ID()
    {
        string Supplier_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlSupplier.Items)
            {
                if (item.Selected)
                {
                    Supplier_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Supplier_ID))
            {
                Supplier_ID = "," + Supplier_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("Supplier_ID", ex.ToString());
        }
        return Supplier_ID;
    }

    private string ADDWarehouse_ID()
    {
        string Warehouse_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddWarehouse.Items)
            {
                if (item.Selected)
                {
                    Warehouse_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Warehouse_ID))
            {
                Warehouse_ID = "," + Warehouse_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("Warehouse_ID", ex.ToString());
        }
        return Warehouse_ID;
    }

    protected void UpdateMateria()
    {
        try
        {
            MaterialRow _MaterialRow = new MaterialRow();
            if (Material_ID != 0)
            {
                _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                if (_MaterialRow != null)
                {
                    _MaterialRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _MaterialRow.MaterialCategory_ID = Convert.ToInt32(ddlMateriaType.SelectedValue);
                    _MaterialRow.Supplier_ID = ADDSupplier_ID();
                    _MaterialRow.Code = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
                    _MaterialRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _MaterialRow.PackingType = string.IsNullOrEmpty(txtPackingType.Text) ? string.Empty : txtPackingType.Text;
                    _MaterialRow.Unit = string.IsNullOrEmpty(txtUnit.Text) ? string.Empty : txtUnit.Text;
                    _MaterialRow.IsolationDay = string.IsNullOrEmpty(txtIsolationDay.Text) ? 0 : Convert.ToInt32(txtIsolationDay.Text);
                    _MaterialRow.Active = true;
                    _MaterialRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _MaterialRow.LastEditBy = MyUser.GetUser_ID();
                    _MaterialRow.LastEditDate = DateTime.Now;
                    _MaterialRow.CodePrivate = string.IsNullOrEmpty(txtCodePrivate.Text) ? string.Empty : txtCodePrivate.Text;
                    if (!string.IsNullOrEmpty(ADDWarehouse_ID()))
                    {
                        _MaterialRow.Warehouse_ID = ADDWarehouse_ID();
                        BusinessRulesLocator.GetMaterialBO().Update(_MaterialRow);
                        lblMessage.Text = "Cập nhật thông tin thành công!";
                        lblMessage.Visible = true;
                        FillInfoMateria();
                    }
                    else
                    {
                        lblMessage.Text = "Bạn chưa chọn kho !";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateWarehouse", ex.ToString());
        }
    }

    protected void AddMaterialPrice()
    {
        MaterialPriceRow _MaterialPriceRow = new MaterialPriceRow();
        if (Material_ID != 0)
        {
            _MaterialPriceRow.Material_ID = Material_ID;
            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _MaterialPriceRow.FromDate = FromDate;
            }
            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _MaterialPriceRow.ToDate = ToDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            _MaterialPriceRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Decimal.Parse((txtPrice.Text.Replace(",", "")));
            _MaterialPriceRow.CreateBy = MyUser.GetUser_ID();
            _MaterialPriceRow.CreateDate = DateTime.Now;
            _MaterialPriceRow.Active = 1;

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text) && !string.IsNullOrEmpty(txtPrice.Text))
            {
                BusinessRulesLocator.GetMaterialPriceBO().Insert(_MaterialPriceRow);
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Bạn chưa nhập hết trường *, vui lòng nhập lại!";
                lblMessage.Visible = true;
            }


        }
    }

    protected void LoadMaterialPrice()
    {
        try
        {

            string where = "";
            if (Material_ID != 0)
            {

                where += "and MP.Material_ID =" + Material_ID;
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"select MP.*, M.Name as NameMaterial
from MaterialPrice MP  
left join Material M on MP.Material_ID = M.Material_ID 
where  M.Active = 1 and MP.Active = 1 " + where);
                rptMaterialPrice.DataSource = dt;
                rptMaterialPrice.DataBind();
            }

        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }

    protected void Clear()
    {
        txtPrice.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }

    protected void FiddMaterialPrice()
    {
        MaterialPriceRow _MaterialPriceRow = new MaterialPriceRow();
        if (Material_ID != 0 && MaterialPrice_ID != 0)
        {
            _MaterialPriceRow = BusinessRulesLocator.GetMaterialPriceBO().GetByPrimaryKey(MaterialPrice_ID);
            if (_MaterialPriceRow != null)
            {
                txtFromDate.Text = _MaterialPriceRow.FromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = _MaterialPriceRow.ToDate.ToString("dd/MM/yyyy");
                txtPrice.Text = _MaterialPriceRow.Price.ToString("N0");
            }
        }
    }
    protected void UpdateMaterialPrice()
    {
        MaterialPriceRow _MaterialPriceRow = new MaterialPriceRow();
        if (Material_ID != 0 && MaterialPrice_ID != 0)
        {
            _MaterialPriceRow = BusinessRulesLocator.GetMaterialPriceBO().GetByPrimaryKey(MaterialPrice_ID);

            if (_MaterialPriceRow != null)
            {
                _MaterialPriceRow.Material_ID = Material_ID;
                if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
                {
                    DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _MaterialPriceRow.FromDate = FromDate;
                }
                if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
                {
                    DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _MaterialPriceRow.ToDate = ToDate.AddHours(23).AddMinutes(59).AddSeconds(59); ;
                }
                _MaterialPriceRow.Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Decimal.Parse((txtPrice.Text.Replace(",", "")));
                _MaterialPriceRow.LastEditBy = MyUser.GetUser_ID();
                _MaterialPriceRow.LastEditDate = DateTime.Now;
                _MaterialPriceRow.Active = 1;
                if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text) && !string.IsNullOrEmpty(txtPrice.Text))
                {
                    BusinessRulesLocator.GetMaterialPriceBO().Update(_MaterialPriceRow);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.Visible = true;
                    FiddMaterialPrice();
                }
                else
                {
                    lblMessage.Text = "Bạn chưa nhập hết trường *, vui lòng nhập lại!";
                    lblMessage.Visible = true;
                }


            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateMateria();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }

    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Material_List.aspx", false);
    }

    protected void btnSaveMaterial_Click(object sender, EventArgs e)
    {

    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (Material_ID != 0 && MaterialPrice_ID == 0)
        {
            AddMaterialPrice();
            lblMessage.Text = "Cập nhật thông tin thành công!";
            lblMessage.Visible = true;
        }

        if (MaterialPrice_ID != 0 && Material_ID != 0)
        {
            UpdateMaterialPrice();
        }
        LoadMaterialPrice();
        //Response.Redirect("Material_Edit?Material_ID=" + Material_ID + "&mode=setPrice", false);
        Response.Redirect("Material_List.aspx", false);
        //Response.Redirect("Material_Edit?Material_ID=" + Material_ID + "&mode=setPrice", false);
    }

    protected void btnAddPrice_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void rptMaterialPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int MaterialPriceID = Convert.ToInt32(e.CommandArgument);
        MaterialPriceRow _MaterialPriceRow = new MaterialPriceRow();
        _MaterialPriceRow = BusinessRulesLocator.GetMaterialPriceBO().GetByPrimaryKey(MaterialPriceID);
        switch (e.CommandName)
        {
            case "Delete":
                _MaterialPriceRow.Active = -1;
                BusinessRulesLocator.GetMaterialPriceBO().Update(_MaterialPriceRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;

        }
        lblMessage.Visible = true;
        LoadMaterialPrice();
        Response.Redirect("Material_Edit?Material_ID=" + Material_ID + "&mode=setPrice", false);

    }

    protected void rptMaterialPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
}