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

public partial class ReportExpectedProductivity : System.Web.UI.Page
{
    int ProductBrand_ID = 0;
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Common.GetFunctionGroupDN())
        {
            ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());

        }
        if (!IsPostBack)
        {
            LoadProduct();
            LoadZone();
            loadProductPackageEP();

        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        Thongkesanluong.Visible = false;
    }

    private void LoadProduct()
    {

        try
        {

            string where = "";
            if (ProductBrand_ID != 0)
            {
                where += "and ProductBrand_ID = " + ProductBrand_ID;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList("select  Name, Product_ID from Product where Active=1 " + where + "  order by Name ASC");
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Product_ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("-- Chọn sản phẩm --", ""));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    private void LoadZone()
    {
        try
        {
            ddlZone.Items.Insert(0, new ListItem("-- Chọn khu vực sản xuất --", "0"));
            ddlZone.Items.Insert(1, new ListItem(" Vùng ", "1"));
            ddlZone.Items.Insert(2, new ListItem(" Khu ", "2"));
            ddlZone.Items.Insert(3, new ListItem(" Thửa ", "3"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }


    protected void loadProductPackageEP()
    {
        string where = "";

        if (ProductBrand_ID != 0)
        {
            where += "and PP.ProductBrand_ID = " + ProductBrand_ID;
        }

        if (ddlProduct.SelectedValue != "")
        {
            where += " and PP.Product_ID =" + ddlProduct.SelectedValue;
        }
        where += " and Convert(date,PP.CreateDate)  BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";

        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@" select DISTINCT PP.ProductPackage_ID , PP.Name , PP.ExpectedProductivity, PP.Acreage, P.ExpectedProductivityDescription
from ProductPackage PP
left join Product P on PP.Product_ID = P.Product_ID
where  P.Active = 1 " + where);
            rptProduct.DataSource = dt;
            rptProduct.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    public string donvi = string.Empty;

    protected void LoadDataZone()
    {
        if (ddlProduct.SelectedValue != "")
        {
            string where = "and PP.Product_ID =" + ddlProduct.SelectedValue;

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name");
            dt.Columns.Add("DienTich");
            dt.Columns.Add("Sanluong");
            dt.Columns.Add("DonVi");

            if (ProductBrand_ID != 0)
            {
                where += " and PP.ProductBrand_ID =" + ProductBrand_ID;
            }
            where += " and Convert(date,PP.CreateDate)  BETWEEN '" + ctlDatePicker1.FromDate + "' and '" + ctlDatePicker1.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
            if (ddlZone.SelectedValue == "1")
            {
                DataTable dtZone = new DataTable();
                dtZone = BusinessRulesLocator.Conllection().GetAllList(@"SELECT DISTINCT Z.Name As NameZone,  PP.Acreage, P.ExpectedProductivityDescription,(select ISNULL( SUM(ExpectedProductivity),0) from ProductPackage where Zone_ID =Z.Zone_ID and Product_ID =" + Convert.ToInt32(ddlProduct.SelectedValue) + ") as SanLuongZone " +
                    ",(select ISNULL( SUM(Acreage),0) from ProductPackage where Zone_ID =Z.Zone_ID and Product_ID = " + Convert.ToInt32(ddlProduct.SelectedValue) + " ) as DienTichZone " +
                    "from ProductPackage PP " +
                    " left join Product P on PP.Product_ID = P.Product_ID " +
                    "left join Zone Z on PP.Zone_ID = Z.Zone_ID where  Z.Active =1" + where);

                if (dtZone.Rows.Count > 0)
                {
                    for (int i = 0; i < dtZone.Rows.Count; i++)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Name"] = dtZone.Rows[i]["NameZone"];
                        _row["Sanluong"] = dtZone.Rows[i]["SanLuongZone"];
                        _row["DienTich"] = dtZone.Rows[i]["DienTichZone"];
                        _row["DonVi"] = dtZone.Rows[i]["ExpectedProductivityDescription"] + "/" + dtZone.Rows[i]["Acreage"] + "m2";
                        donvi = dtZone.Rows[i]["ExpectedProductivityDescription"].ToString();
                        dt.Rows.Add(_row);
                    }
                }

                //Response.Write(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    rptDataKVSX.DataSource = dt;
                    rptDataKVSX.DataBind();
                    rptDataKVSX.Visible = true;
                }
                else
                {
                    rptDataKVSX.DataSource = null;
                    rptDataKVSX.DataBind();
                }
            }
            else if (ddlZone.SelectedValue == "2")
            {
                DataTable dtArea = new DataTable();
                dtArea = BusinessRulesLocator.Conllection().GetAllList(@"SELECT DISTINCT A.Name As NameArea,  PP.Acreage, P.ExpectedProductivityDescription,(select ISNULL( SUM(ExpectedProductivity),0) from ProductPackage where Area_ID =A.Area_ID and Product_ID= " + Convert.ToInt32(ddlProduct.SelectedValue) + ") as SanLuongArea " +
                    ",(select ISNULL( SUM(Acreage),0) from ProductPackage where Area_ID =A.Area_ID and Product_ID = " + Convert.ToInt32(ddlProduct.SelectedValue) + " ) as DienTichArea " +
                    "from ProductPackage PP " +
                        " left join Product P on PP.Product_ID = P.Product_ID " +
                    "left join Area A on PP.Area_ID = A.Area_ID where  1 =1" + where);

                if (dtArea.Rows.Count > 0)
                {

                    for (int i = 0; i < dtArea.Rows.Count; i++)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Name"] = dtArea.Rows[i]["NameArea"];
                        _row["Sanluong"] = dtArea.Rows[i]["SanLuongArea"];
                        _row["DienTich"] = dtArea.Rows[i]["DienTichArea"];
                        _row["DonVi"] = dtArea.Rows[i]["ExpectedProductivityDescription"] + "/" + dtArea.Rows[i]["Acreage"] + "m2";
                        donvi = dtArea.Rows[i]["ExpectedProductivityDescription"].ToString();
                        dt.Rows.Add(_row);
                    }

                }

                //Response.Write(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    rptDataKVSX.DataSource = dt;
                    rptDataKVSX.DataBind();
                    rptDataKVSX.Visible = true;

                }
                else
                {
                    rptDataKVSX.DataSource = null;
                    rptDataKVSX.DataBind();
                }
            }
            else if (ddlZone.SelectedValue == "3")
            {
                DataTable dtFarm = new DataTable();
                dtFarm = BusinessRulesLocator.Conllection().GetAllList(@"SELECT DISTINCT F.Name As NameFarm,  PP.Acreage, P.ExpectedProductivityDescription,(select ISNULL( SUM(ExpectedProductivity),0) from ProductPackage where Farm_ID = F.Farm_ID and Product_ID= " + Convert.ToInt32(ddlProduct.SelectedValue) + ") as SanLuongFarm " +
                    ",(select ISNULL( SUM(Acreage),0) from ProductPackage where Farm_ID = F.Farm_ID and Product_ID = " + Convert.ToInt32(ddlProduct.SelectedValue) + " ) as DienTichFarm " +
                    "from ProductPackage PP " +
                        " left join Product P on PP.Product_ID = P.Product_ID " +
                    "left join Farm F on PP.Farm_ID = F.Farm_ID where  1 =1" + where);

                if (dtFarm.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFarm.Rows.Count; i++)
                    {
                        DataRow _row = dt.NewRow();
                        _row["Name"] = dtFarm.Rows[i]["NameFarm"];
                        _row["Sanluong"] = dtFarm.Rows[i]["SanLuongFarm"];
                        _row["DienTich"] = dtFarm.Rows[i]["DienTichFarm"];
                        _row["DonVi"] = dtFarm.Rows[i]["ExpectedProductivityDescription"] + "/" + dtFarm.Rows[i]["Acreage"] + "m2";
                        donvi = dtFarm.Rows[i]["ExpectedProductivityDescription"].ToString() ;
                        dt.Rows.Add(_row);

                    }
                }

                //Response.Write(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    rptDataKVSX.DataSource = dt;
                    rptDataKVSX.DataBind();
                    rptDataKVSX.Visible = true;
                }
                else
                {
                    rptDataKVSX.DataSource = null;
                    rptDataKVSX.DataBind();
                }
            }
            else if (ddlZone.SelectedValue == "0")
            {
                rptDataKVSX.DataSource = null;
                rptDataKVSX.DataBind();
            }
            Thongkesanluong.Visible = true;
        }
        else
        {
            Thongkesanluong.Visible = false;
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProductPackageEP();
        LoadDataZone();
        //Thongkesanluong.Visible = true;
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProductPackageEP();
        LoadDataZone();
    }

    protected void ctlDatePicker1_DateChange(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            loadProductPackageEP();
            LoadDataZone();
        }

    }

    public decimal TotalProduct = 0;
    public decimal AmountProduct = 0;
    public int SortProduct = 0;

    public decimal TotalZone = 0;
    public decimal AmountZone = 0;
    public int SortZone = 0;
    public decimal AmountDT = 0;


    protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SortProduct++;
            Literal lblAmountProduct = e.Item.FindControl("lblAmountProduct") as Literal;
            if (lblAmountProduct != null)
            {
                if (!string.IsNullOrEmpty(lblAmountProduct.Text))
                {
                    AmountProduct += decimal.Parse(lblAmountProduct.Text);
                }
            }

        }
        if (SortProduct != 0)
        {
            TotalProduct = AmountProduct / decimal.Parse(SortProduct.ToString());
        }
    }

    protected void rptDataKVSX_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SortZone++;
            Literal lblAmountZone = e.Item.FindControl("lblAmountZone") as Literal;
            if (lblAmountZone != null)
            {
                if (!string.IsNullOrEmpty(lblAmountZone.Text))
                    AmountZone += decimal.Parse(lblAmountZone.Text);
            }
            Literal lblDienTich = e.Item.FindControl("lblDienTich") as Literal;
            if (lblDienTich != null)
            {
                if (!string.IsNullOrEmpty(lblDienTich.Text))
                    AmountDT += decimal.Parse(lblDienTich.Text);
            }
        }
        //if (SortZone != 0)
        //{
        //    TotalZone = AmountZone / decimal.Parse(SortZone.ToString());
        //}
    }


}