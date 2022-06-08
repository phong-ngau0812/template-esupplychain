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

public partial class ProductPackageOrder_Edit : System.Web.UI.Page
{
    int ProductPackageOrder_ID = 0;
    public string title = "Thông tin lệnh sản xuất";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackageOrder_ID"]))
            int.TryParse(Request["ProductPackageOrder_ID"].ToString(), out ProductPackageOrder_ID);
        if (!IsPostBack)
        {
            FillDDLStatus();
            FillDDLddlProductBrand();
            FillDDLddlWarehouse();
            FillProduct();
            FillInfoProductPackageOrder();
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void FillProduct()
    {
        string where = "";
        if (ddlProductBrand.SelectedValue != "")
        {
            where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
        }
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"select  Name, Product_ID from Product where Active=1" + where + " order by Name ASC");
        ddlProduct.DataSource = dt;
        ddlProduct.DataValueField = "Product_ID";
        ddlProduct.DataTextField = "Name";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
    }
    private void FillDDLStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageOrderStatusBO().GetAsDataTable(" ", " ");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "ProductPackageOrderStatus_ID";
            ddlStatus.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
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

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"select W.* from Warehouse W inner join ProductBrand PB on W.ProductBrand_ID = PB.ProductBrand_ID where PB.Active<>-1 and W.Active<>-1 and Type = 1" + where);
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
    private void FillDDLCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            string where = string.Empty;
            if (MyUser.GetProductBrand_ID() != "0" && MyUser.GetProductBrand_ID() != "")
            {
                where += " and ProductBrand_ID=" + MyUser.GetProductBrand_ID();
            }
            if (ddlProductBrand.SelectedValue != "")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            dt = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetAsDataTable(" ACTIVE=1" + where, " SORT ASC ");
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ProductPackageOrderCategory_ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Chọn danh mục lệnh --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void LoadSGTIN()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(" select ProductPackage_ID, SGTIN from ProductPackage  order by ProductPackage_ID DESC");
            ddlSGTIN.DataSource = dt;
            ddlSGTIN.DataTextField = "SGTIN";
            ddlSGTIN.DataValueField = "ProductPackage_ID";
            ddlSGTIN.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }

    protected void FillInfoProductPackageOrder()
    {
        try
        {
            if (ProductPackageOrder_ID != 0)
            {
                ProductPackageOrderRow _ProductPackageOrderRow = new ProductPackageOrderRow();
                _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_ID);

                if (_ProductPackageOrderRow != null)
                {
                    if (!_ProductPackageOrderRow.IsProductBrand_IDNull)
                    {
                        ddlProductBrand.SelectedValue = _ProductPackageOrderRow.ProductBrand_ID.ToString();
                        ddlProductBrand.Enabled = false;
                        //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                        MyActionPermission.CheckPermission(_ProductPackageOrderRow.ProductBrand_ID.ToString(), _ProductPackageOrderRow.CreateBy.ToString(), "/Admin/ProductPackageOrder/ProductPackageOrder_List");
                        FillDDLCategory();
                    }
                    LoadSGTIN();
                    if (!_ProductPackageOrderRow.IsSGTIN_LISTNull)
                    {
                        string[] array = _ProductPackageOrderRow.SGTIN_LIST.Split(',');
                        foreach (string value in array)
                        {
                            foreach (ListItem item in ddlSGTIN.Items)
                            {
                                if (value == item.Value)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    if (ddlSGTIN.SelectedIndex > -1)
                    {
                        viewmore.Visible = true;
                        LoadHistory();
                    }
                    else
                    {
                        viewmore.Visible = false;
                    }

                    txtSGTINTEXT.Text = _ProductPackageOrderRow.IsSGTIN_TEXTNull ? string.Empty : _ProductPackageOrderRow.SGTIN_TEXT;
                    if (!_ProductPackageOrderRow.IsImagesNull)
                    {
                        imganh.ImageUrl = "../../data/productpackageorder/" + _ProductPackageOrderRow.Images;
                    }

                    txtName.Text = _ProductPackageOrderRow.IsNameNull ? string.Empty : _ProductPackageOrderRow.Name;
                    txtCode.Text = _ProductPackageOrderRow.IsCodeNull ? string.Empty : _ProductPackageOrderRow.Code;
                    txtPO.Text = _ProductPackageOrderRow.IsCodePONull ? string.Empty : _ProductPackageOrderRow.CodePO;
                    txtSX.Text = _ProductPackageOrderRow.IsEndDateNull ? DateTime.Now.ToString("dd/MM/yyyy") : _ProductPackageOrderRow.EndDate.ToString("dd/MM/yyyy");
                    if (!_ProductPackageOrderRow.IsProduct_IDNull)
                    {
                        string[] array = _ProductPackageOrderRow.Product_ID.Split(',');
                        foreach (string value in array)
                        {
                            if (value != "")
                            {
                                foreach (ListItem item in ddlProduct.Items)
                                {
                                    if (value == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    txtItemcount.Text = _ProductPackageOrderRow.IsItemCountNull ? "0" : _ProductPackageOrderRow.ItemCount.ToString();
                    if (!_ProductPackageOrderRow.IsProductPackageOrderCategory_IDNull)
                    {
                        ddlCategory.SelectedValue = _ProductPackageOrderRow.ProductPackageOrderCategory_ID.ToString();
                    }
                    if (!_ProductPackageOrderRow.IsProductPackageOrderStatus_IDNull)
                        ddlStatus.SelectedValue = _ProductPackageOrderRow.ProductPackageOrderStatus_ID.ToString();
                    FillMaterial();
                    LoadMaterial(ProductPackageOrder_ID);
                    LoadTableMaterial();
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    private void LoadMaterial(int productPackageOrder_ID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductPackageOrderMaterialBO().GetAsDataTable(" ProductPackageOrder_ID=" + productPackageOrder_ID, "");
            foreach (DataRow item in dt.Rows)
            {
                foreach (RadComboBoxItem itemM in ddlMaterial.Items)
                {
                    if (item["Material_ID"].ToString() == itemM.Value)
                    {
                        itemM.Checked = true;
                    }
                }
            }
            DataTable dtWarehouse = BusinessRulesLocator.Conllection().GetAllList(@"Select Warehouse_ID from Material where Material_ID in(select Material_ID FROM [checkproductman_dev].[dbo].[ProductPackageOrderMaterial] where ProductPackageOrder_ID=" + productPackageOrder_ID + ")");
            string Values = string.Empty;
            foreach (DataRow dtRow in dtWarehouse.Rows)
            {
                Values += dtRow["Warehouse_ID"].ToString();
            }
            if (!string.IsNullOrEmpty(Values))
            {
                string[] words = Values.Trim().Split(',');
                foreach (string word in words)
                {
                    foreach (ListItem item in ddlWarehouse.Items)
                    {
                        if (word != "")
                        {
                            if (word == item.Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
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
                //FillMaterial(Convert.ToInt32(MyUser.GetProductBrand_ID()));
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLCategory();
        FillProduct();
    }
    protected string GetSGTIN()
    {
        string SGTIN = string.Empty;
        try
        {
            foreach (ListItem item in ddlSGTIN.Items)
            {
                if (item.Selected)
                {
                    SGTIN += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(SGTIN))
            {
                SGTIN = "," + SGTIN;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("GetSGTIN", ex.ToString());
        }
        return SGTIN;
    }
    protected string AddProduct_ID()
    {
        string Product_ID = string.Empty;
        try
        {
            foreach (ListItem item in ddlProduct.Items)
            {
                if (item.Selected)
                {
                    Product_ID += item.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(Product_ID))
            {
                Product_ID = "," + Product_ID;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("AddProduct_ID", ex.ToString());
        }
        return Product_ID;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //if (ddlMaterial.CheckedItems.Count > 0)
                //{
                ProductPackageOrderRow _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_ID);
                if (_ProductPackageOrderRow != null)
                {
                    _ProductPackageOrderRow.Name = txtName.Text;
                    _ProductPackageOrderRow.Code = txtCode.Text;
                    _ProductPackageOrderRow.CodePO = txtPO.Text;
                    _ProductPackageOrderRow.ProductPackageOrderStatus_ID = Convert.ToInt32(ddlStatus.SelectedValue);
                    _ProductPackageOrderRow.ProductPackageOrderCategory_ID = Convert.ToInt32(ddlCategory.SelectedValue);
                    _ProductPackageOrderRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductPackageOrderRow.LastEditDate = DateTime.Now;
                    _ProductPackageOrderRow.SGTIN_LIST = GetSGTIN();
                    _ProductPackageOrderRow.Product_ID = AddProduct_ID();
                    _ProductPackageOrderRow.SGTIN_TEXT = txtSGTINTEXT.Text;
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        fileimage = _ProductPackageOrderRow.ProductPackageOrder_ID + "_" + fulAnh.FileName;
                        fulAnh.SaveAs(Server.MapPath("../../data/productpackageorder/" + fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _ProductPackageOrderRow.Images = fileimage;
                        }
                    }
                    BusinessRulesLocator.GetProductPackageOrderBO().Update(_ProductPackageOrderRow);
                    AddMaterial(ProductPackageOrder_ID);
                    lblMessage.Text = "Cập nhật thông tin thành công!";
                    lblMessage.ForeColor = Color.White;
                    lblMessage.Visible = true;
                }
                //}
                //else
                //{
                //    lblMessage.Text = "Vui lòng chọn vật tư cho lệnh!";
                //    lblMessage.ForeColor = Color.Red;
                //    lblMessage.Visible = true;
                //}
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    private void AddMaterial(int ProductPackageOrder_ID)
    {
        try
        {
            if (ddlMaterial.CheckedItems.Count > 0)
            {
                BusinessRulesLocator.GetProductPackageOrderMaterialBO().Delete("ProductPackageOrder_ID=" + ProductPackageOrder_ID);
            }
            foreach (RepeaterItem item in rptMaterial.Items)
            {
                Literal lblMaterial_ID = item.FindControl("lblMaterial_ID") as Literal;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                if (lblMaterial_ID != null)
                {
                    ProductPackageOrderMaterialRow _ProductPackageOrderMaterialRow = new ProductPackageOrderMaterialRow();
                    _ProductPackageOrderMaterialRow.ProductPackageOrder_ID = ProductPackageOrder_ID;
                    _ProductPackageOrderMaterialRow.Material_ID = Convert.ToInt32(lblMaterial_ID.Text);
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        _ProductPackageOrderMaterialRow.Amount = Convert.ToInt32(txtQuantity.Text);
                    }
                    else
                    {
                        _ProductPackageOrderMaterialRow.Amount = 0;
                    }

                    _ProductPackageOrderMaterialRow.CreateBy = MyUser.GetUser_ID();
                    _ProductPackageOrderMaterialRow.CreateDate = DateTime.Now;
                    BusinessRulesLocator.GetProductPackageOrderMaterialBO().Insert(_ProductPackageOrderMaterialRow);
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("AddMaterial", ex.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPackageOrder_List.aspx", false);
    }
    protected void LoadHistory()
    {

        string[] array = GetSGTIN().Split(',');
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("ProductPackage_ID");
        dt.Columns.Add("ProductPackageName");
        dt.Columns.Add("ProductBrandName");
        dt.Columns.Add("SGTIN");
        foreach (string value in array)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int ProductPackage_ID_History = Convert.ToInt32(value);
                ProductPackageRow _ProductPackageRowHistory = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID_History);
                string brandName = string.Empty;
                if (!_ProductPackageRowHistory.IsProductBrand_IDNull)
                {
                    brandName = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_ProductPackageRowHistory.ProductBrand_ID).Name;
                }
                DataRow _row = dt.NewRow();
                _row["ProductPackage_ID"] = ProductPackage_ID_History;
                _row["ProductPackageName"] = _ProductPackageRowHistory.Name;
                _row["ProductBrandName"] = brandName;
                _row["SGTIN"] = _ProductPackageRowHistory.SGTIN;
                dt.Rows.Add(_row);

            }
        }
        //Response.Write(dt.Rows.Count);
        if (dt.Rows.Count > 0)
        {
            rptPackage.DataSource = dt;
            rptPackage.DataBind();
            rptPackage1.DataSource = dt;
            rptPackage1.DataBind();
        }
    }
    protected void ddlSGTIN_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSGTIN.SelectedIndex > -1)
        {
            viewmore.Visible = true;
            LoadHistory();
        }
        else
        {
            viewmore.Visible = false;
        }
    }
    protected void rptPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Repeater rptTaskHistory = e.Item.FindControl("rptTaskHistory") as Repeater;

            if (lblProductPackage_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
              where T.TaskType_ID=1 and T.Task_ID in (select Task_ID from Task where ProductPackage_ID=" + lblProductPackage_ID.Text + ") order by StartDate DESC");
                if (dt.Rows.Count > 0)
                {
                    rptTaskHistory.DataSource = dt;
                    rptTaskHistory.DataBind();
                }
            }
        }
    }
    protected void rptPackage1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductPackage_ID = e.Item.FindControl("lblProductPackage_ID") as Literal;
            Repeater rptTaskHistory = e.Item.FindControl("rptTaskHistory1") as Repeater;

            if (lblProductPackage_ID != null)
            {
                DataTable dt = new DataTable();
                dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TS.Name as StatusName,T.Task_ID,T.ProductPackage_ID, T.CreateBy, T.StartDate, T.EndDate, T.Name,T.Location, T.ProductPackageName, T.ProductPackage_ID, ISNULL( U.UserName,N'Không xác định') as UserName from Task T 
              left join TaskStatus  TS on T.TaskStatus_ID= TS.TaskStatus_ID
              left join aspnet_Users U on U.UserId= T.CreateBy
              where T.TaskType_ID=4 and T.Task_ID in (select Task_ID from Task where ProductPackage_ID=" + lblProductPackage_ID.Text + ") order by StartDate DESC");
                if (dt.Rows.Count > 0)
                {
                    rptTaskHistory.DataSource = dt;
                    rptTaskHistory.DataBind();
                }
            }
        }
    }
    private void FillMaterial()
    {
        try
        {
            string where1 = string.Empty;
            string where = string.Empty;
            if (ListWareHouse_ID != "")
            {
                string plus = " OR ";
                int count = 1;
                string[] values = ListWareHouse_ID.Trim().Split(',');
                foreach (var item in values)
                {
                    if (item.ToString() != "")
                    {
                        if (count == 1)
                        {
                            where1 += " M.Warehouse_ID like '%" + item.ToString() + "%'";
                            count = 2;
                        }
                        else
                        {
                            where1 += plus + " M.Warehouse_ID like '%" + item.ToString() + "%'";
                        }

                    }
                }
                where1 = where1 + " and ";

            }
            if (ddlProductBrand.SelectedValue != "")
            {
                where += "And M.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"Select M.* from Material M where" + where1 + " M.Active = 1" + where);
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataValueField = "Material_ID";
            ddlMaterial.DataTextField = "Name";
            ddlMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillMaterial", ex.ToString());
        }
    }
    protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTableMaterial();
    }
    protected string GetMaterial()
    {
        string Material = string.Empty;
        try
        {
            foreach (RadComboBoxItem item in ddlMaterial.Items)
            {
                if (item.Checked)
                {
                    Material += item.Value + ",";
                }
            }
            //if (!string.IsNullOrEmpty(Material))
            //{
            //    Material = "," + Material;
            //}
        }
        catch (Exception ex)
        {
            Log.writeLog("GetMaterial", ex.ToString());
        }
        return Material;
    }
    protected void LoadTableMaterial()
    {
        if (string.IsNullOrEmpty(GetMaterial()))
        {
            rptMaterial.DataSource = null;
            rptMaterial.DataBind();
            tbl.Visible = false;
        }
        else
        {
            tbl.Visible = true;
            string[] array = GetMaterial().Split(',');
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Material_ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Amount");
            foreach (string value in array)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int Material_ID = Convert.ToInt32(value);
                    MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                    int Amount = 0;
                    DataTable dtProductPackageOrderMaterial = BusinessRulesLocator.GetProductPackageOrderMaterialBO().GetAsDataTable("Material_ID=" + Material_ID + " and ProductPackageOrder_ID=" + ProductPackageOrder_ID, "");
                    if (dtProductPackageOrderMaterial.Rows.Count > 0)
                    {
                        Amount = Convert.ToInt32(dtProductPackageOrderMaterial.Rows[0]["Amount"]);
                    }

                    string brandName = string.Empty;
                    if (_MaterialRow != null)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Material_ID"] = Material_ID;
                        _row["Name"] = _MaterialRow.Name;
                        _row["Unit"] = _MaterialRow.Unit;
                        _row["Amount"] = Amount;
                        dt.Rows.Add(_row);
                    }
                }
            }
            //Response.Write(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                rptMaterial.DataSource = dt;
                rptMaterial.DataBind();
            }
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
        if (GetMaterial() != "")
        {
            hdfList_ID.Value = GetMaterial();
        }
        if (ddlWarehouse.SelectedValue != "")
        {
            // ListWareHouse_ID = ADDListWarehouse_ID().Substring(0, ADDListWarehouse_ID().Length - 1);
            ListWareHouse_ID = ADDListWarehouse_ID();
        }
        else
        {
            ListWareHouse_ID = string.Empty;
            hdfList_ID.Value = "";
        }
        FillMaterial();
        FillMaterial1();
        LoadTableMaterial();
    }
    protected void FillMaterial1()
    {
        string[] array = hdfList_ID.Value.Split(',');
        foreach (string value in array)
        {
            foreach (RadComboBoxItem item in ddlMaterial.Items)
            {
                if (value == item.Value)
                {
                    item.Checked = true;
                }
            }
        }
    }
}