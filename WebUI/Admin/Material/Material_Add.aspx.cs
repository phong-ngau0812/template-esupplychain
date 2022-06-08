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

public partial class Material_Add : System.Web.UI.Page
{
    public string title = "Thêm mới vật tư ";
    public int Material_ID = 0;
    public int Material_ID_Fill = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!string.IsNullOrEmpty(Request["Material_ID_Fill"]))
            int.TryParse(Request["Material_ID_Fill"].ToString(), out Material_ID_Fill);
        CheckUserXuanHoa();
        if (!IsPostBack)
        {

            FillDDLddlProductBrand();
            FillDDLddlMateriaType();
            FillDDLddlSupplier();
            FillDDLddWarehouse();
            //Fill copy
            FillInfoMateria();
            themmoi.Visible = false;
            DaCo.Visible = true;
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;

    }

    protected void CheckUserXuanHoa()
    {
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList("Select UserId,UserName from aspnet_Users where UserId = '" + MyUser.GetUser_ID() + "'");
        if (dt.Rows.Count > 0)
        {
            if (MyUser.GetFunctionGroup_ID() == "2" && MyUser.GetProductBrand_ID()=="1524")
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
            dt = BusinessRulesLocator.GetMaterialCategoryBO().GetAsDataTable("Active=1", " Sort ASC");
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
    public bool CheckName()
    {
        bool flag = false;
        DataTable dt = BusinessRulesLocator.GetMaterialBO().GetAsDataTable(" ProductBrand_ID=" + ddlProductBrand.SelectedValue + " and Name like'" + txtName.Text.Trim() + "'", "");
        if (dt.Rows.Count == 0)
        {
            flag = true;
        }
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckName())
            {

                if (rdoDaCo.Checked)
                {
                    AddMateria();
                }
                else
                {
                    AddMaterialType();
                }
                if (Material_ID != 0)
                {
                    //Response.Redirect("Material_Edit?Material_ID=" + Material_ID + "&mode=setPrice", false);
                    Response.Redirect("Material_List.aspx", false);
                }

            }
            else
            {
                lblMessage.Text = "Vật tư " + txtName.Text + " đã tồn tại, vui lòng chọn tên khác!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }
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
    protected void UpdateArea(int Material_ID)
    {
        try
        {
            MaterialRow _MaterialRow = new MaterialRow();
            if (Material_ID != 0)
            {
                _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                if (_MaterialRow != null)
                {
                    _MaterialRow.GIAI = "GIAI-" + _MaterialRow.Material_ID.ToString();

                    BusinessRulesLocator.GetMaterialBO().Update(_MaterialRow);
                    _MaterialRow.LastEditBy = MyUser.GetUser_ID();
                    _MaterialRow.LastEditDate = DateTime.Now;
                }
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateProduct", ex.ToString());
        }
    }



    protected void AddMaterialType()
    {
        try
        {

            MaterialCategoryRow _MaterialCategoryRow = new MaterialCategoryRow();

            _MaterialCategoryRow.Name = string.IsNullOrEmpty(txtMateriaType.Text) ? string.Empty : txtMateriaType.Text;

            _MaterialCategoryRow.Active = 1;
            _MaterialCategoryRow.Sort = Common.GenarateSort("MaterialCategory");
            _MaterialCategoryRow.CreateBy = MyUser.GetUser_ID();
            _MaterialCategoryRow.CreateDate = DateTime.Now;
            BusinessRulesLocator.GetMaterialCategoryBO().Insert(_MaterialCategoryRow);
            if (!_MaterialCategoryRow.IsMaterialCategory_IDNull)
            {
                AddMaterialTypeNew(_MaterialCategoryRow.MaterialCategory_ID);
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("AddMaterial", ex.ToString());
        }
    }


    protected void AddMaterialTypeNew(int MaterialCategory_ID)
    {

        try
        {
            MaterialRow _MaterialRow = new MaterialRow();
            _MaterialRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);

            if (MaterialCategory_ID != 0)
            {
                _MaterialRow.MaterialCategory_ID = MaterialCategory_ID;
            }
            _MaterialRow.Supplier_ID = ddlSupplier.SelectedValue;
            _MaterialRow.Code = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
            _MaterialRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _MaterialRow.PackingType = string.IsNullOrEmpty(txtPackingType.Text) ? string.Empty : txtPackingType.Text;
            _MaterialRow.Unit = string.IsNullOrEmpty(txtUnit.Text) ? string.Empty : txtUnit.Text;
            _MaterialRow.IsolationDay = string.IsNullOrEmpty(txtIsolationDay.Text) ? 0 : Convert.ToInt32(txtIsolationDay.Text);
            _MaterialRow.Active = true;
            _MaterialRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _MaterialRow.CreateBy = MyUser.GetUser_ID();
            _MaterialRow.CreateDate = DateTime.Now;
            _MaterialRow.Supplier_ID = ADDSupplier_ID();
            if (!string.IsNullOrEmpty(ADDWarehouse_ID()))
            {
                _MaterialRow.Warehouse_ID = ADDWarehouse_ID();
                BusinessRulesLocator.GetMaterialBO().Insert(_MaterialRow);
                if (!_MaterialRow.IsMaterial_IDNull)
                {
                    UpdateArea(_MaterialRow.Material_ID);
                    Material_ID = _MaterialRow.Material_ID;
                }
            }
            else
            {
                lblMessage.Text = "Bạn chưa chọn kho !";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }


        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMateria", ex.ToString());
        }


    }



    protected void AddMateria()
    {
        try
        {
            MaterialRow _MaterialRow = new MaterialRow();
            _MaterialRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);

            if (!string.IsNullOrEmpty(ddlMateriaType.SelectedValue))
            {
                _MaterialRow.MaterialCategory_ID = Convert.ToInt32(ddlMateriaType.SelectedValue);
            }
            _MaterialRow.Supplier_ID = ADDSupplier_ID();
            _MaterialRow.Code = string.IsNullOrEmpty(txtGTIN.Text) ? string.Empty : txtGTIN.Text;
            _MaterialRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _MaterialRow.PackingType = string.IsNullOrEmpty(txtPackingType.Text) ? string.Empty : txtPackingType.Text;
            _MaterialRow.Unit = string.IsNullOrEmpty(txtUnit.Text) ? string.Empty : txtUnit.Text;
            _MaterialRow.IsolationDay = string.IsNullOrEmpty(txtIsolationDay.Text) ? 0 : Convert.ToInt32(txtIsolationDay.Text);
            _MaterialRow.Active = true;
            _MaterialRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _MaterialRow.CodePrivate = string.IsNullOrEmpty(txtCodePrivate.Text) ? string.Empty : txtCodePrivate.Text;
            _MaterialRow.CreateBy = MyUser.GetUser_ID();
            _MaterialRow.CreateDate = DateTime.Now;

            if (!string.IsNullOrEmpty(ADDWarehouse_ID()))
            {
                _MaterialRow.Warehouse_ID = ADDWarehouse_ID();
                BusinessRulesLocator.GetMaterialBO().Insert(_MaterialRow);
                if (!_MaterialRow.IsMaterial_IDNull)
                {
                    UpdateArea(_MaterialRow.Material_ID);
                    Material_ID = _MaterialRow.Material_ID;
                }
            }
            else
            {
                lblMessage.Text = "Bạn chưa chọn kho !";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMateria", ex.ToString());
        }
    }

    protected void ClearForm()
    {

        //ddlProductBrand.SelectedValue = ""; 
        ddlMateriaType.SelectedValue = "";
        txtNote.Text = "";
        txtName.Text = " ";
        txtGTIN.Text = " ";
        txtPackingType.Text = " ";
        txtUnit.Text = " ";
        txtIsolationDay.Text = " ";
        txtMateriaType.Text = "";
        //ddlSupplier.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Material_List.aspx", false);
    }

    protected void rdoDaCo_CheckedChanged(object sender, EventArgs e)
    {
        themmoi.Visible = false;
        DaCo.Visible = true;

    }

    protected void rdo_CheckedChanged(object sender, EventArgs e)
    {
        themmoi.Visible = true;
        DaCo.Visible = false;
    }



    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLddlSupplier();
        FillDDLddWarehouse();
    }
    protected void FillInfoMateria()
    {
        try
        {
            if (Material_ID_Fill != 0)
            {
                MaterialRow _MaterialRow = new MaterialRow();
                _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID_Fill);

                if (_MaterialRow != null)
                {
                    FillDDLddlProductBrand();
                    ddlProductBrand.SelectedValue = _MaterialRow.IsProductBrand_IDNull ? string.Empty : _MaterialRow.ProductBrand_ID.ToString();
                    FillDDLddlMateriaType();
                    FillDDLddlSupplier();
                    FillDDLddWarehouse();
                    ddlMateriaType.SelectedValue = _MaterialRow.IsMaterialCategory_IDNull ? string.Empty : _MaterialRow.MaterialCategory_ID.ToString();
                    //txtcode.Text = _MaterialRow.IsCodeNull ? string.Empty : _MaterialRow.Code;
                    txtName.Text = _MaterialRow.IsNameNull ? string.Empty : _MaterialRow.Name;
                    txtPackingType.Text = _MaterialRow.IsPackingTypeNull ? string.Empty : _MaterialRow.PackingType;
                    txtUnit.Text = _MaterialRow.IsUnitNull ? string.Empty : _MaterialRow.Unit;
                    txtIsolationDay.Text = _MaterialRow.IsIsolationDayNull ? string.Empty : _MaterialRow.IsolationDay.ToString();
                    txtNote.Text = _MaterialRow.IsDescriptionNull ? string.Empty : _MaterialRow.Description;
                    txtGTIN.Text = _MaterialRow.IsCodeNull ? string.Empty : _MaterialRow.Code;

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
}