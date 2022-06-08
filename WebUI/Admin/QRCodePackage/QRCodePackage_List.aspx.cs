using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class QRCodePackage_List : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    private int productCategory_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnAdd.Visible = MyActionPermission.CanAdd();
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
            FillProductBrand();
            FillDepartment();
            FillLocation();
            LoadStatus();
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

        LoadProductPackage();
        ResetMsg();
    }
    public string ReturnStatus(string status)
    {
        string st = "";
        switch (status)
        {
            case "Kích hoạt đưa mã tem ra thị trường":
                st = "<span class=\"badge badge-success\">" + status + "</span>";
                break;
            //case "Đang thu hoạch":
            //    st = "<span class=\"badge badge-primary\">" + status + "</span>";
            //    break;
            //case "Thu hoạch xong":
            //    st = "<span class=\"badge badge-primary\">" + status + "</span>";
            //    break;
            case "Tem mới tạo":
                st = "<span class=\"badge badge-danger\">" + status + "</span>";
                break;
            case "Tem đang tạo...":
                st = "<span class=\"badge badge-warning\">" + status + "</span>";
                break;
            default:
                st = "";
                break;
        }

        return st;

    }
    public string Message = "";

    private void LoadProductPackage()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                where += " OR P.CreateBy ='" + MyUser.GetUser_ID() + "'";

            }
            //pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                where = " and P.ProductBrand_ID in (" + ProductBrandList.Value + ")";
                dtSet = BusinessRulesLocator.Conllection().GetQRCodePackageV2(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtName.Text, txtSerial.Text, where, " LastEditDate DESC");
            }
            else
            {
                dtSet = BusinessRulesLocator.Conllection().GetQRCodePackageV2(Pager1.CurrentIndex, pageSize, 7, 0, Convert.ToInt32(ddlProductBrand.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlSo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlWard.SelectedValue), "", ctlDatePicker1.FromDate, ctlDatePicker1.ToDate, txtName.Text, txtSerial.Text, where, " LastEditDate DESC");
            }



            grdProductPackage.DataSource = dtSet.Tables[0];
            grdProductPackage.DataBind();
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
            Log.writeLog("LoadProductPackage", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadProductPackage();
    }
    private void FillProductBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }

            if (!string.IsNullOrEmpty(ProductBrandList.Value))
            {
                Common.FillProductBrand(ddlProductBrand, " and ProductBrand_ID in (" + ProductBrandList.Value + ")");
            }
            else
            {
                Common.FillProductBrand(ddlProductBrand, where);
            }

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

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadStatus()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetQRCodeStatusBO().GetAsDataTable(" QRCodeStatus_ID>=0", " QRCodeStatus_ID ASC");
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "QRCodeStatus_ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- Chọn trạng thái --", "-1"));
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackage_Add.aspx", false);
    }



    protected void rptProductCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }


    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Pager1.CurrentIndex = 1;
            LoadProductPackage();
        }

    }

    protected void ddlCha_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlTieuChuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void grdProductPackage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblType = e.Item.FindControl("lblType") as Literal;

            Label lbText = e.Item.FindControl("lbText") as Label;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            if (lblType != null)
            {
                if (MyUser.GetFunctionGroup_ID() == "8")
                {
                    lbText.Visible = false;
                }
                else
                {
                    if (lblType.Text == "1")
                    {
                        lbText.Visible = true;
                    }
                    else
                    {
                        lbText.Visible = false;
                    }

                }

            }
        }
    }

    protected void grdProductPackage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Literal lblProductBrand_ID = e.Item.FindControl("lblProductBrand_ID") as Literal;
        int QRCodePackage_ID = Convert.ToInt32(e.CommandArgument);
        QRCodePackageRow _QRCodePackageRow = new QRCodePackageRow();
        _QRCodePackageRow = BusinessRulesLocator.GetQRCodePackageBO().GetByPrimaryKey(QRCodePackage_ID);
        switch (e.CommandName)
        {
            //case "Delete":
            //    if (MyActionPermission.CanDeleteProductPackage(ProductPackage_ID, ref Message))
            //    {
            //        _ProductPackageRow.ProductPackageStatus_ID = 6;
            //        BusinessRulesLocator.GetProductPackageBO().Update(_ProductPackageRow);
            //        //BusinessRulesLocator.GetProductPackageBO().DeleteByPrimaryKey(ProductPackage_ID);
            //        MyActionPermission.WriteLogSystem(ProductPackage_ID, "Xóa - " + _ProductPackageRow.Name);
            //        lblMessage.Text = ("Xóa bản ghi thành công !");
            //    }
            //    else
            //    {
            //        lblMessage.Text = Message;
            //        lblMessage.Style.Add("background", "wheat");
            //        lblMessage.ForeColor = Color.Red;
            //    }
            //    break;
            case "Undo":
                BusinessRulesLocator.Conllection().QRCodeUndoSplit(QRCodePackage_ID, Convert.ToInt32(lblProductBrand_ID.Text), 0);
                lblMessage.Text = ("Khôi phục lô mã thành công !");
                break;
            case "Active":
                BusinessRulesLocator.Conllection().QRCodePackage_SetStatus(QRCodePackage_ID, 2);
                lblMessage.Text = ("Kích hoạt đưa mã tem ra thị trường thành công !");
                break;
            case "Delete":
                BusinessRulesLocator.Conllection().QRCodePackage_SetStatus(QRCodePackage_ID, -1);
                if (_QRCodePackageRow != null)
                {
                    _QRCodePackageRow.LastEditBy = MyUser.GetUser_ID();
                    _QRCodePackageRow.LastEditDate = DateTime.Now;
                    _QRCodePackageRow.Active = false;
                    BusinessRulesLocator.GetQRCodePackageBO().Update(_QRCodePackageRow);
                }

                //BusinessRulesLocator.Conllection().QRCodePackage_Delete(QRCodePackage_ID);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
        }
        lblMessage.Visible = true;
        LoadProductPackage();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }

    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    public string NotificationTask(int ProductPackage_ID)
    {
        string link = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetTaskNotification(ProductPackage_ID, 3);
        if (dt.Rows.Count == 1)
        {
            link = dt.Rows[0]["Link"].ToString();
        }
        return link;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackageReport_List.aspx", false);
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
        LoadProductPackage();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillDistrict();
        LoadProductPackage();
    }
    protected void ddlSo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        FillWard();
        LoadProductPackage();
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadProductPackage();
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