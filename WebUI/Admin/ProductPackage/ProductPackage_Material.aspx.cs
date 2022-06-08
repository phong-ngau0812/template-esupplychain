using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackage_Material : System.Web.UI.Page
{
    public string name, namepackage, demuc, code, lenhsx, hosx = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtStart.Attributes.Add("readonly", "readonly");
        //txtEnd.Attributes.Add("readonly", "readonly");
        if (!string.IsNullOrEmpty(Request["TaskStep_ID"]))
            int.TryParse(Request["TaskStep_ID"].ToString(), out TaskStep_ID);
        if (!string.IsNullOrEmpty(Request["Product_ID"]))
            int.TryParse(Request["Product_ID"].ToString(), out Product_ID);
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
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
        ResetMsg();
        if (!IsPostBack)
        {
            FillddlMaterial();
        }
        Init();
    }

    protected void LoadMaterialPackage(int ProductPackageOrder_ID, int Product_ID)
    {
        DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList("Select WE.*, CONCAT(WI.Name +' - ',WI.CodeMaterialPackage) as NameMaterial,WI.Unit from WarehouseExportMaterial WE left join WarehouseImport WI on WE.WarehouseImport_ID = Wi.WarehouseImport_ID  where WE.WarehouseExport_ID in (Select WarehouseExport_ID from WarehouseExport where Active=1 and Product_ID = " + Product_ID + " and ProductPackageOrder_ID = " + ProductPackageOrder_ID + ") ");
        if (dtMaterial.Rows.Count > 0)
        {
            Data.Visible = true;
            rptMaterial.DataSource = dtMaterial;
            rptMaterial.DataBind();
        }
        else
        {
            Data.Visible = false;
            rptMaterial.DataSource = null;
            rptMaterial.DataBind();
        }
    }
    private void Init()
    {

        if (Product_ID != 0)
        {
            name = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID).Name;
        }

        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow != null)
            {
                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                if (!IsPostBack)
                {
                    if (!_ProductPackageRow.IsProductPackageOrder_IDNull)
                    {
                        ProductPackageOrder_ID = _ProductPackageRow.ProductPackageOrder_ID;
                        LoadMaterialPackage(ProductPackageOrder_ID, _ProductPackageRow.Product_ID);
                    }
                    LoadMaterialCollapse(ProductPackage_ID);
                }
            }
        }
    }
    protected void ResetMsg()
    {
        lblMessage1.Text = lblMessage.Text = "";
        lblMessage1.Visible = lblMessage.Visible = false;
    }
    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code=" + code, false);
    }
    protected void btnSaveMaterial_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessRulesLocator.GetProductPackageVsMaterialBO().Delete(" ProductPackage_ID=" + ProductPackage_ID);
                foreach (RepeaterItem item in rptMaterial.Items)
                {
                    DropDownList ddlNumber = item.FindControl("ddlNumber") as DropDownList;
                    Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                    Literal lblWarehouseExportMaterial_ID = item.FindControl("lblWarehouseExportMaterial_ID") as Literal;

                    ProductPackageVsMaterialRow _ProductPackageVsMaterialRow = new ProductPackageVsMaterialRow();
                    _ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                    _ProductPackageVsMaterialRow.WarehouseExportMaterial_ID = Convert.ToInt32(lblWarehouseExportMaterial_ID.Text);
                    _ProductPackageVsMaterialRow.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                    _ProductPackageVsMaterialRow.CreateBy = MyUser.GetUser_ID();
                    _ProductPackageVsMaterialRow.CreateDate = DateTime.Now;
                    _ProductPackageVsMaterialRow.Active = 1;
                    _ProductPackageVsMaterialRow.Quantity = Convert.ToInt32(ddlNumber.SelectedValue);
                    BusinessRulesLocator.GetProductPackageVsMaterialBO().Insert(_ProductPackageVsMaterialRow);
                }
                ProductPackageRow _ProductPackageRow = new ProductPackageRow();
                _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                if (_ProductPackageRow != null)
                {
                    _ProductPackageRow.ProductPackageStatus_ID = 1;
                    BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                }
                lblMessage.Visible = true;
                lblMessage.Text = "Cập nhật định mức vật tư và kích hoạt cho lô thành công";
                foreach (RepeaterItem item in rptMaterial.Items)
                {
                    Literal lblCheck = item.FindControl("lblCheck") as Literal;
                    lblCheck.Text = "";

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
    protected void rptMaterial_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblMaterial_ID = e.Item.FindControl("lblMaterial_ID") as Literal;
            Literal lblCheck = e.Item.FindControl("lblCheck") as Literal;
            Literal lblWarehouseExportMaterial_ID = e.Item.FindControl("lblWarehouseExportMaterial_ID") as Literal;
            DropDownList ddlNumber = e.Item.FindControl("ddlNumber") as DropDownList;

            DataTable dtCheck = BusinessRulesLocator.Conllection().GetAllList(@"Select * from ProductPackageVsMaterial where Active = 1 and ProductPackage_ID =" + ProductPackage_ID + " and WarehouseExportMaterial_ID =" + lblWarehouseExportMaterial_ID.Text + " and Material_ID =" + lblMaterial_ID.Text + "");
            if (dtCheck.Rows.Count < 1)
            {
                lblCheck.Text = "<span class='red'>(Vật tư chưa cập nhật)</span>";
            }

            int number = 0;
            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("  select IsNull(SUM(Amount),0) AS SUM from WarehouseExportMaterial where WarehouseExport_ID in(  select WarehouseExport_ID from WarehouseExport where ProductPackageOrder_ID=" + ProductPackageOrder_ID + " and Active=1)  and Material_ID = " + lblMaterial_ID.Text + " ");
            DataTable dtPick = BusinessRulesLocator.Conllection().GetAllList(@"  select ISNULL( SUM(Quantity),0) as SUM from ProductPackageVsMaterial where ProductPackage_ID in (select ProductPackage_ID from ProductPackage where ProductPackageOrder_ID=" + ProductPackageOrder_ID + " and ProductPackageOrder_ID not in(4,5,6))"
  + " and Material_ID = " + lblMaterial_ID.Text + " and Active = 1 and ProductPackage_ID <> " + ProductPackage_ID);
            if (dt.Rows.Count > 0)
            {
                number = Convert.ToInt32(dt.Rows[0]["Sum"]);
                if (dtPick.Rows.Count == 1)
                {
                    number = number - Convert.ToInt32(dtPick.Rows[0]["Sum"]);
                }
            }
            ddlNumber.Items.Add(new ListItem("--Chọn định mức --", "0"));
            for (int i = 1; i <= number; i++)
            {
                ddlNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ProductPackageVsMaterialRow _ProductPackageVsMaterialRow = BusinessRulesLocator.GetProductPackageVsMaterialBO().GetByPrimaryKey(ProductPackage_ID, Convert.ToInt32(lblMaterial_ID.Text));
            if (_ProductPackageVsMaterialRow != null)
            {
                if (!_ProductPackageVsMaterialRow.IsQuantityNull)
                {
                    ddlNumber.SelectedValue = _ProductPackageVsMaterialRow.Quantity.ToString();
                }
            }
        }
    }
    private void FillddlMaterial()
    {
        try
        {
            int ProductBrand_ID = 0;
            string Where = "";
            if (ProductPackage_ID != 0)
            {
                ProductPackageRow _ProductPackageRow = new ProductPackageRow();
                _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                if (_ProductPackageRow != null)
                {
                    ProductBrand_ID = _ProductPackageRow.IsProductBrand_IDNull ? 0 : _ProductPackageRow.ProductBrand_ID;
                }

            }
            if (ProductBrand_ID != 0)
            {
                Where += " and M.ProductBrand_ID =" + ProductBrand_ID;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  SELECT M.Material_ID,  M.Name , M.Unit  
from Material M
left join ProductBrand PB on M.ProductBrand_ID = PB.ProductBrand_ID 
where PB.Active<>-1 and M.Active = 1 " + Where);
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataTextField = "Name";
            ddlMaterial.DataValueField = "Material_ID";
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
            dt.Columns.Add("Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Quantity");

            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int Material_ID = Convert.ToInt32(value);
                    MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                    string brandName = string.Empty;
                    if (_MaterialRow != null)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Material_ID"] = Material_ID;
                        _row["Name"] = _MaterialRow.Name;
                        _row["Unit"] = _MaterialRow.Unit;

                        DataTable Quantity = new DataTable();
                        Quantity = BusinessRulesLocator.GetProductPackageVsMaterialBO().GetAsDataTable("ProductPackage_ID=" + ProductPackage_ID + " and Material_ID=" + Material_ID, "");
                        if (Quantity.Rows.Count == 1)
                        {
                            _row["Quantity"] = Quantity.Rows[0]["Quantity"];
                        }
                        else
                        {
                            _row["Quantity"] = null;
                        }


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
    protected void LoadMaterialCollapse(int ProductPackage_ID)
    {
        DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList("select M.Material_ID, M.Unit, M.Name , PPM.Quantity from ProductPackageVsMaterial PPM left join Material M on PPM.Material_ID = M.Material_ID where PPM.Active = 1 and ProductPackage_ID = " + ProductPackage_ID);
        if (dtMaterial.Rows.Count > 0)
        {
            Data1.Visible = true;
            rptMaterialCollapse.DataSource = dtMaterial;
            rptMaterialCollapse.DataBind();
            foreach (DataRow item in dtMaterial.Rows)
            {
                foreach (ListItem item1 in ddlMaterial.Items)
                {
                    if (item1.Value.ToString() == item["Material_ID"].ToString())
                    {
                        item1.Selected = true;
                    }
                }
            }
        }
        else
        {
            Data1.Visible = false;
            rptMaterialCollapse.DataSource = null;
            rptMaterialCollapse.DataBind();
        }
    }
    protected void rptMaterialCollapse_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTableMaterial();

    }
    protected void btnAddMaterial_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessRulesLocator.GetProductPackageVsMaterialBO().Delete(" ProductPackage_ID=" + ProductPackage_ID);
                foreach (RepeaterItem item in rptMaterialCollapse.Items)
                {

                    Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    if (lblMaterial_ID != null)
                    {
                        ProductPackageVsMaterialRow _ProductPackageVsMaterialRow = new ProductPackageVsMaterialRow();
                        _ProductPackageVsMaterialRow.ProductPackage_ID = ProductPackage_ID;
                        _ProductPackageVsMaterialRow.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                        _ProductPackageVsMaterialRow.WarehouseExportMaterial_ID = Convert.ToInt32(lblMaterial_ID.Text); // Nhóm 4 không dùng --- gán tạm ID Vật tư
                        _ProductPackageVsMaterialRow.CreateBy = MyUser.GetUser_ID();
                        _ProductPackageVsMaterialRow.CreateDate = DateTime.Now;
                        _ProductPackageVsMaterialRow.Active = 1;
                        if (!string.IsNullOrEmpty(txtQuantity.Text))
                        {
                            _ProductPackageVsMaterialRow.Quantity = decimal.Parse(txtQuantity.Text.Replace(",", "."));
                        }
                        BusinessRulesLocator.GetProductPackageVsMaterialBO().Insert(_ProductPackageVsMaterialRow);
                    }
                }
                ProductPackageRow _ProductPackageRow = new ProductPackageRow();
                _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                if (_ProductPackageRow != null)
                {
                    _ProductPackageRow.ProductPackageStatus_ID = 1;
                    BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
                    lblMessage1.Visible = true;
                    lblMessage1.Text = "Cập nhật định mức vật tư và kích hoạt cho lô thành công";
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("btnSaveMaterial_Click", ex.ToString());
        }
    }
}