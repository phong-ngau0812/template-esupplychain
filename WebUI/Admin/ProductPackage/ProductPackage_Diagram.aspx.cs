using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_ProductPackage_ProductPackage_Diagram : System.Web.UI.Page
{
    private int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    protected void LoadData()
    {
        try
        {
            if (ProductPackage_ID>0)
            {
            
            string sql = @"select top 1 P.Name,P.ItemCount, P.Product_ID,PB.Name as ProductBrandName,Q.Name as QualityName,PB.Address as ProductBrandAddress, P.ProductPackage_ID, P.Code, P.SGTIN , U.UserName, UE.UserName as NguoiSua, PO.Name as TenLenh
	, P.CreateDate, P.CreateBy, P.LastEditBy, w.Name as HoSX, w.Type
	, P.LastEditDate , PD.Name as ProductName,PD.Image, PS.Name as TrangThai, P.StartDate, P.EndDate 
	from ProductPackage P
	left join aspnet_Users U on U.UserId=P.CreateBy
	left join aspnet_Users UE on UE.UserId=P.LastEditBy
	left join Product PD on PD.Product_ID= P.Product_ID
	left join ProductPackageStatus PS on PS.ProductPackageStatus_ID= P.ProductPackageStatus_ID
	left join Workshop w on w.Workshop_ID= P.Workshop_ID
	left join ProductPackageOrder PO on PO.ProductPackageOrder_ID = P.ProductPackageOrder_ID
	left join ProductBrand PB on PB.ProductBrand_ID=P.ProductBrand_ID
	left join Quality Q on Q.Quality_ID=PD.Quality_ID where P.ProductPackageStatus_ID<>6  and P.ProductPackage_ID=" + ProductPackage_ID;
                rptData.DataSource = BusinessRulesLocator.Conllection().GetAllList(sql) ;
                rptData.DataBind();
            }
        }
        catch (Exception ex)
        {

            Log.writeLog(ex.ToString(), "LoadData");
        }
    }
}