using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Product_List : System.Web.UI.Page
{
    int ProductCategory_ID = 0;
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 5;//Số bản ghi 1 trang
    private int productCategory_ID;
    public string Message = "";
    public string style = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MyUser.GetFunctionGroup_ID() != "1")
        {
            btnCopy.Visible = false;
        }
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!string.IsNullOrEmpty(Request["ProductCategory_ID"]))
            int.TryParse(Request["ProductCategory_ID"].ToString(), out ProductCategory_ID);
        if (!IsPostBack)
        {
            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dt = BusinessRulesLocator.GetUserVsProductBrandBO().GetAsDataTable(" UserID='" + MyUser.GetUser_ID() + "'", "");
                if (dt.Rows.Count == 1)
                {
                    ProductBrandList.Value = dt.Rows[0]["ProductBrand_ID_List"].ToString();
                }
            }
            LoadProductCategory();
            if (ProductCategory_ID != 0)
            {
                ddlCha.SelectedValue = ProductCategory_ID.ToString();
            }
            FillDepartment();
            FillDDLQuality();
            FillProductBrand();
            FillLocation();
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    FillDistrict();
                    ddlLocation.Enabled = false;
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                }
                else if (MyUser.GetRank_ID() == "3")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                }
                else if (MyUser.GetRank_ID() == "4")
                {
                    ddlLocation.SelectedValue = MyUser.GetLocation_ID();
                    ddlLocation.Enabled = false;
                    FillDistrict();
                    ddlSo.SelectedValue = MyUser.GetDepartmentMan_ID();
                    ddlSo.Enabled = false;
                    ddlDistrict.SelectedValue = MyUser.GetDistrict_ID();
                    ddlDistrict.Enabled = false;
                    FillWard();
                    ddlWard.SelectedValue = MyUser.GetWard_ID();
                    ddlWard.Enabled = false;
                }

            }
        }
        if (ckChung.Checked)
        {
            ddlProductBrand.SelectedValue = "0";
            style = "none";
        }
        else
        {
            style = "";
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }

        LoadProduct();
        ResetMsg();
    }

    private void LoadProduct()
    {
        try
        {

            //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                string where = " and P.ProductBrand_ID in (" + ProductBrandList.Value + ")";
                dtSet = BusinessRulesLocator.Conllection().GetProductV3(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlTieuChuan.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), 0, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, listCategory_ID, txtName.Text, where, " CreateDate DESC");
            }
            else
            {
                dtSet = BusinessRulesLocator.Conllection().GetProductV3(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlTieuChuan.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), 0, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, listCategory_ID, txtName.Text, "", " CreateDate DESC");
            }

            grdProduct.DataSource = dtSet.Tables[0];
            grdProduct.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[2].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadProduct", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadProduct();
    }
    private void FillProductBrand()
    {
        try
        {
            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductBrandBO().GetAsDataTable(" Active=1", " Sort, Name ASC");
            //ddlProductBrand.DataSource = dt;
            //ddlProductBrand.DataTextField = "Name";
            //ddlProductBrand.DataValueField = "ProductBrand_ID";
            //ddlProductBrand.DataBind();
            //ddlProductBrand.Items.Insert(0, new ListItem("-- Chọn doanh nghiệp --", "0"));
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, "");
            }
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
            else
            {
                ckChung.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillWard()

    {
        string where = string.Empty;

        if (ddlDistrict.SelectedValue != "0")
        {
            where = "District_ID = " + ddlDistrict.SelectedValue;
        }
        ddlWard.DataSource = BusinessRulesLocator.GetWardBO().GetAsDataTable("" + where, "");
        ddlWard.DataValueField = "Ward_ID";
        ddlWard.DataTextField = "Name";
        ddlWard.DataBind();
        ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));
    }

    private void FillDistrict()
    {
        string where = string.Empty;
        if (ddlLocation.SelectedValue != "0")
        {
            where += "Location_ID = " + ddlLocation.SelectedValue;
        }
        ddlDistrict.DataSource = BusinessRulesLocator.GetDistrictBO().GetAsDataTable("" + where, "");
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataTextField = "Name";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
    }

    private void FillDepartment()
    {
        ddlSo.DataSource = BusinessRulesLocator.GetDepartmentManBO().GetAsDataTable("", "");
        ddlSo.DataValueField = "DepartmentMan_ID";
        ddlSo.DataTextField = "Name";
        ddlSo.DataBind();
        ddlSo.Items.Insert(0, new ListItem("-- Lọc theo sở ngành--", "0"));
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }
    private void FillDDLQuality()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQualityBO().GetAsDataTable(" ", " Name ASC");
            ddlTieuChuan.DataSource = dt;
            ddlTieuChuan.DataTextField = "Name";
            ddlTieuChuan.DataValueField = "Quality_ID";
            ddlTieuChuan.DataBind();
            ddlTieuChuan.Items.Insert(0, new ListItem("-- Chọn tiêu chuẩn --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadProductCategory()
    {
        try
        {
            //DataTable dtProductCategoryParent = new DataTable();
            //dtProductCategoryParent.Clear();
            //dtProductCategoryParent.Columns.Add("ProductCategory_ID");
            //dtProductCategoryParent.Columns.Add("Parent_ID");
            //dtProductCategoryParent.Columns.Add("Name");
            //dtProductCategoryParent.Columns.Add("Image");
            //dtProductCategoryParent.Columns.Add("Active");

            //DataTable dt = new DataTable();
            //dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Active <>-1 ", " Sort ASC");

            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRow itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //    itemProductCategoryParent["ProductCategory_ID"] = item["ProductCategory_ID"];
            //    itemProductCategoryParent["Parent_ID"] = item["Parent_ID"];
            //    itemProductCategoryParent["Name"] = item["Name"];
            //    itemProductCategoryParent["Image"] = item["Image"];
            //    itemProductCategoryParent["Active"] = item["Active"];
            //    dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //    if (item["ProductCategory_ID"] != null)
            //    {
            //        DataTable dtChild = new DataTable();
            //        dtChild = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID =" + item["ProductCategory_ID"], " Sort ASC");
            //        if (dtChild.Rows.Count > 0)
            //        {
            //            foreach (DataRow itemChild in dtChild.Rows)
            //            {
            //                itemProductCategoryParent = dtProductCategoryParent.NewRow();
            //                itemProductCategoryParent["ProductCategory_ID"] = itemChild["ProductCategory_ID"];
            //                itemProductCategoryParent["Parent_ID"] = item["ProductCategory_ID"];
            //                itemProductCategoryParent["Name"] = Server.HtmlDecode("&nbsp;&nbsp;&nbsp;") + " -" + itemChild["Name"];
            //                itemProductCategoryParent["Image"] = itemChild["Image"];
            //                itemProductCategoryParent["Active"] = itemChild["Active"];
            //                dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
            //            }

            //        }
            //    }
            //}

            ddlCha.DataSource = BusinessRulesLocator.Conllection().GetProductCategory();
            //ddlCha.DataSource = dtProductCategoryParent;
            ddlCha.DataTextField = "Name";
            ddlCha.DataValueField = "ProductCategory_ID";
            ddlCha.DataBind();
            ddlCha.Items.Insert(0, new ListItem("-- Chọn danh mục --", "0"));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product_Add.aspx", false);
    }



    protected void rptProductCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }


    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProduct();
        }
    }

    protected void ddlCha_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCha.SelectedValue != "0")
        {
            GetListID(Convert.ToInt32(ddlCha.SelectedValue));
        }
        LoadProduct();
        Pager1.CurrentIndex = 1;
    }

    protected void ddlTieuChuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }

    protected void grdProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            Literal lblID = e.Item.FindControl("lblID") as Literal;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "False")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    lblText.Text = "Ngừng kích hoạt";
                }
                else
                {
                    lblText.Text = "Đang kích hoạt";
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                }
            }


            //if (!MyActionPermission.CanDeleteProduct(Convert.ToInt32(lblID.Text), ref Message))
            //{

            //    btnDeactive.Visible = MyActionPermission.CanDeleteProduct(Convert.ToInt32(lblID.Text), ref Message);
            //}
            //else
            //{
            //    btnActive.Visible = MyActionPermission.CanDeleteProduct(Convert.ToInt32(lblID.Text), ref Message);
            //}
        }
    }

    protected void grdProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Product_ID = Convert.ToInt32(e.CommandArgument);
        ProductRow _ProductRow = new ProductRow();
        _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
        switch (e.CommandName)
        {
            case "Copy":

                _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                if (_ProductRow != null)
                {
                    _ProductRow.Name = _ProductRow.Name + " Copy";
                    _ProductRow.CreateBy = MyUser.GetUser_ID();
                    _ProductRow.CreateDate = DateTime.Now;
                    _ProductRow.LastEditBy = MyUser.GetUser_ID();
                    _ProductRow.LastEditDate = DateTime.Now;
                    _ProductRow.Product_ID_Parent = Product_ID;
                    if (Common.GetFunctionGroupDN())
                        _ProductRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                    BusinessRulesLocator.GetProductBO().Insert(_ProductRow);
                    if (_ProductRow != null)
                    {
                        DataTable dt = BusinessRulesLocator.GetTaskStepBO().GetAsDataTable(" Product_ID=" + Product_ID, " Sort ASC");
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                TaskStepRow _TaskStepRow = new TaskStepRow();
                                _TaskStepRow.Product_ID = _ProductRow.Product_ID;
                                _TaskStepRow.Name = item["Name"].ToString();
                                _TaskStepRow.Sort = Convert.ToInt32(item["Sort"].ToString());
                                BusinessRulesLocator.GetTaskStepBO().Insert(_TaskStepRow);
                            }
                        }
                    }
                    lblMessage.Text = "Nhân bản sản phẩm thành công!";
                }
                else
                {
                    lblMessage.Text = "Nhân bản sản phẩm thất bại!";
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            case "Delete":
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt1 = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROM ProductPackage where Product_ID=" + Product_ID);
                dt2 = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROm QRCodePackage where Product_ID=" + Product_ID);
                if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
                {
                    if (MyActionPermission.CanDeleteProduct(Product_ID, ref Message))
                    {
                        MyActionPermission.WriteLogSystem(Product_ID, "Xóa - " + _ProductRow.Name);
                        BusinessRulesLocator.GetProductBO().DeleteByPrimaryKey(Product_ID);
                        lblMessage.Text = ("Xóa bản ghi thành công !");
                    }
                    else
                    {
                        lblMessage.Text = Message;
                        lblMessage.Style.Add("background", "wheat");
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = ("Sản phẩm đã được tạo lô mã không xóa được");
                }
                
                break;
            case "Active":
                if (MyActionPermission.CanDeleteProduct(Product_ID, ref Message))
                {
                    _ProductRow.Active = true;
                    BusinessRulesLocator.GetProductBO().Update(_ProductRow);
                    lblMessage.Text = ("Kích hoạt thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            case "Deactive":
                if (MyActionPermission.CanDeleteProduct(Product_ID, ref Message))
                {
                    _ProductRow.Active = false;
                    BusinessRulesLocator.GetProductBO().Update(_ProductRow);
                    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
        }
        lblMessage.Visible = true;
        LoadProduct();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet dtSet = new DataSet();
        if (!string.IsNullOrEmpty(ProductBrandList.Value))
        {
            string where = " and P.ProductBrand_ID in (" + ProductBrandList.Value + ")";
            dtSet = BusinessRulesLocator.Conllection().GetProductV3(Pager1.CurrentIndex, 5000, 7, 0, Convert.ToInt32(ddlTieuChuan.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), 0, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, listCategory_ID, txtName.Text, ProductBrandList.Value, " CreateDate DESC");
        }
        else
        {
            dtSet = BusinessRulesLocator.Conllection().GetProductV3(Pager1.CurrentIndex, 5000, 7, 0, Convert.ToInt32(ddlTieuChuan.SelectedValue), Convert.ToInt32(ddlProductBrand.SelectedValue), 0, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, listCategory_ID, txtName.Text, "", " CreateDate DESC");
        }
        dt = dtSet.Tables[0];
        string attachment = "attachment; filename=danh_sach_san_pham.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "DANH SÁCH SẢN PHẨM (Tổng: " + dt.Rows.Count + ")\n\n";

        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName == "Name" || dc.ColumnName == "RowNum" || dc.ColumnName == "ProductCategoryName")
            {
                Response.Write(tab + dc.ColumnName.Replace("RowNum", "STT").Replace("ProductCategoryName", "Danh mục sản phẩm").Replace("Name", "Tên sản phẩm"));
                tab = "\t";
            }
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                if (i == 0 || i == 1 || i == 12)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
            }
            Response.Write("\n");
        }
        Response.End();
    }
    public string QRCode(string Product_ID)
    {

        string img = string.Empty;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("https://esupplychain.vn/p/" + Product_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                img = "<img width='70px' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
        return img;
    }
    protected DataTable DeQuyDanhMucListID(DataTable dt, int ParentID)
    {

        try
        {

            if (ParentID > 0)
            {

                DataTable dtChild = new DataTable();
                dtChild = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable(" Parent_ID =" + ParentID + "  and Active= 1", " Sort ASC");
                if (dtChild.Rows.Count > 0)
                {
                    foreach (DataRow itemChild in dtChild.Rows)
                    {

                        DataRow itemProductCategoryParent = dt.NewRow();
                        itemProductCategoryParent["ProductCategory_ID"] = itemChild["ProductCategory_ID"];
                        dt.Rows.Add(itemProductCategoryParent);
                        DeQuyDanhMucListID(dt, Convert.ToInt32(itemProductCategoryParent["ProductCategory_ID"]));
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("DeQuyDanhMuc", ex.ToString());
        }
        return dt;
    }
    protected void GetListID(int NewsCategory_ID)
    {
        try
        {
            DataTable dtProductCategoryParent = new DataTable();
            dtProductCategoryParent.Clear();
            dtProductCategoryParent.Columns.Add("ProductCategory_ID");
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductCategoryBO().GetAsDataTable("Active=1 and ProductCategory_ID=" + NewsCategory_ID, " ProductCategory_ID ASC");
            foreach (DataRow item in dt.Rows)
            {
                DataRow itemProductCategoryParent = dtProductCategoryParent.NewRow();
                itemProductCategoryParent["ProductCategory_ID"] = item["ProductCategory_ID"];
                dtProductCategoryParent.Rows.Add(itemProductCategoryParent);
                DeQuyDanhMucListID(dtProductCategoryParent, Convert.ToInt32(itemProductCategoryParent["ProductCategory_ID"]));
            }
            //DataTable dt = BusinessRulesLocator.GetNewsCategoryBO().GetAsDataTable("Status=1 and Type=1", " Title ASC");
            if (dtProductCategoryParent.Rows.Count > 0)
            {
                foreach (DataRow item in dtProductCategoryParent.Rows)
                {
                    listCategory_ID += item["ProductCategory_ID"] + ",";
                }
                listCategory_ID = listCategory_ID.Remove(listCategory_ID.Length - 1);
                //   Response.Write(listCategoryNews_ID);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadCateNews", ex.ToString());
        }
    }
    string listCategory_ID = string.Empty;

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskStepProduct_Copy", false);
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDistrict();
        LoadProduct();
    }
    protected void ddlSo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillWard();
        LoadProduct();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProduct();
    }
    protected void FillLocation()
    {
        try
        {
            DataTable dt = BusinessRulesLocator.GetLocationBO().GetAsDataTable("", "Name ASC");
            if (dt.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "Location_ID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-- Tỉnh thành --", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("-- Lọc theo quận / huyện --", "0"));
                ddlWard.Items.Insert(0, new ListItem("-- Lọc theo quận xã / phường --", "0"));

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillLocation", ex.ToString());
        }
    }
}