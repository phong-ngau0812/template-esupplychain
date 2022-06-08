using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SystemFrameWork;
using DbObj;
using System.Net;
public partial class Admin_Template_CMS : System.Web.UI.MasterPage
{
    protected string indexmenu = "1";
    protected int _UserID = 0;
    protected string avatar, avatar_dn, ProductBrandName = string.Empty;
    protected string lang = Systemconstants.English;
    protected string password, key = string.Empty;
    public string MaDN = "";
    public string ReturnURL = string.Empty;
    public string date = string.Empty;
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ReturnURL"]))
            ReturnURL = (Request["ReturnURL"].ToString());
    }
    private void FillMenu()
    {
        try
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                //dt = BusinessRulesLocator.Conllection().GetAllList("select Function_ID, Name,CssClass from Function where Active=1 order by Sort ASC");
                //rptFunction.DataSource = dt;
                //rptFunction.DataBind();
                dt = BusinessRulesLocator.Conllection().GetMenu(MyUser.GetUser_ID().ToString());
                if (dt.Rows.Count > 0)
                {
                    lblMenu.Text = dt.Rows[0]["Menu"].ToString();
                }
            }
            string ProductBrand_ID = MyUser.GetProductBrand_ID();
            if (!string.IsNullOrEmpty(ProductBrand_ID) && ProductBrand_ID != "0")
            {
                ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(ProductBrand_ID));
                ProductBrandName = _ProductBrandRow.Name;
                MaDN = "<br/><small style='color:red;'>Mã doanh nghiệp: " + (_ProductBrandRow.IsGLNNull ? string.Empty : _ProductBrandRow.GCP) + "</small>";
                if (!_ProductBrandRow.IsImageNull)
                {
                    avatar_dn = "/data/productbrand/mainimages/original/" + _ProductBrandRow.Image;
                }
                else
                {
                    avatar_dn = avatar;
                }
            }
            else
            {
                ProductBrandName = new MyUser().FullNameFromUserName(Context.User.Identity.Name);
                if (string.IsNullOrEmpty(avatar_dn))
                    avatar_dn = avatar;

            }
            //ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccount();
        if (MyUser.GetFunctionGroup_ID() == "1")
        {
            Admin.Visible = true;
        }
        else
        {
            Admin.Visible = false;
        }
        FillMenu();
        if (!IsPostBack)
        {
            LoadNotification();
        }
        date = DateTime.Now.Year.ToString();

    }
    UserProfile ProfileUser;
    protected void CheckAccount()
    {
        try
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(ReturnURL))
                    Response.Redirect("~/Login.aspx?ReturnURL=" + ReturnURL, false);
                else
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }
            else
            {
                if (MyActionPermission.CheckURLPermission() == 0)
                {
                    Response.Redirect("/", false);
                }
                if (!string.IsNullOrEmpty(ReturnURL))
                {
                    Response.Redirect("/Admin/ProductPackage/ProductPackage_List?Code=" + ReturnURL, false);
                }
                ProfileUser = UserProfile.GetProfile(Context.User.Identity.Name);
                avatar = string.IsNullOrEmpty(ProfileUser.AvatarUrl) ? "../../images/no-image-icon.png" : ProfileUser.AvatarUrl;
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("~/Login.aspx", false);
            Log.writeLog(ex.ToString(), "CheckAccount");
        }
    }

    //protected void rptFunction_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Literal lblFunction_ID = e.Item.FindControl("lblFunction_ID") as Literal;
    //        Repeater rptPage = e.Item.FindControl("rptPage") as Repeater;

    //        if (lblFunction_ID != null)
    //        {
    //            DataTable dt = BusinessRulesLocator.Conllection().GetAllList("select PageFunction_ID, Name, Url, Folder,DisplayMenu from PageFunction where  Function_ID =" + lblFunction_ID.Text + " order by SORT ASC");
    //            if (dt.Rows.Count > 0)
    //            {
    //                rptPage.DataSource = dt;
    //                rptPage.DataBind();
    //            }
    //        }
    //    }
    //}
    public int Count = 0;
    public void LoadNotification()
    {
        try
        {
            if (Common.GetFunctionGroupDN())
            {
                DataTable dt = new DataTable();
                DataTable dtCheckQuyenDuyetLenh = new DataTable();
                dtCheckQuyenDuyetLenh = BusinessRulesLocator.Conllection().GetAllList("select * from UserVsPageFunction where (PageFunction_ID = 95 or PageFunction_ID = 96) and UserId= '" + MyUser.GetUser_ID() + "'");
                if (dtCheckQuyenDuyetLenh.Rows.Count > 0)
                    dt = BusinessRulesLocator.Conllection().GetNotification(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, 0, 0, MyUser.GetUser_ID().ToString());

                DataTable dtKho = new DataTable();
                DataTable dtCheckQuyenXuatKho = new DataTable();
                dtCheckQuyenXuatKho = BusinessRulesLocator.Conllection().GetAllList("select * from UserVsPageFunction where (PageFunction_ID = 78 or PageFunction_ID = 79) and UserId= '" + MyUser.GetUser_ID() + "'");
                if (dtCheckQuyenXuatKho.Rows.Count > 0)
                {
                    dtKho = BusinessRulesLocator.Conllection().GetNotificationWareHouse(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, 0, 0, MyUser.GetUser_ID().ToString());
                }
                if (dt.Rows.Count > 0)
                {
                    rptNotification.DataSource = dt;
                    rptNotification.DataBind();
                }
                if (dtKho.Rows.Count > 0)
                {
                    rptNotificationKho.DataSource = dtKho;
                    rptNotificationKho.DataBind();
                }
                DataTable dtDuyetLenh = new DataTable();
                dtDuyetLenh = BusinessRulesLocator.Conllection().GetNotificationAccept(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, 0, 0, MyUser.GetUser_ID().ToString());
                if (dtDuyetLenh.Rows.Count > 0)
                {
                    rptNotificationAccept.DataSource = dtDuyetLenh;
                    rptNotificationAccept.DataBind();
                }

                DataTable dtDuyetQL = new DataTable();
                dtDuyetQL = BusinessRulesLocator.Conllection().GetNotificationAcceptQL(MyUser.GetUser_ID().ToString());
                if (dtDuyetQL.Rows.Count > 0)
                {
                    rptNotificationAcceptQL.DataSource = dtDuyetQL;
                    rptNotificationAcceptQL.DataBind();
                }
                DataTable dtExport = new DataTable();
                dtExport = BusinessRulesLocator.Conllection().GetNotificationExport(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, 0, 0, MyUser.GetUser_ID().ToString());
                if (dtExport.Rows.Count > 0)
                {
                    rptNotificationExport.DataSource = dtExport;
                    rptNotificationExport.DataBind();
                }


                DataTable dtGiamsat = new DataTable();
                if (MyUser.GetFunctionGroup_ID() == "3")
                {
                    dtGiamsat = BusinessRulesLocator.Conllection().GetNotificationGiamSat(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, 0, 0, MyUser.GetUser_ID().ToString());
                }
                if (dtGiamsat.Rows.Count > 0)
                {
                    rptGiamSat.DataSource = dtGiamsat;
                    rptGiamSat.DataBind();
                }
                Count = dt.Rows.Count + dtKho.Rows.Count + dtDuyetLenh.Rows.Count + dtExport.Rows.Count + dtGiamsat.Rows.Count + dtDuyetQL.Rows.Count;
                if (Count > 0)
                {
                    divNoti.Visible = true;
                }
                else
                {
                    //  divNoti.Visible = false;
                }
                //Get thông báo quản lý vùng
                if (MyUser.GetAccountType_ID() == "7")
                {
                    DataTable dtQLVung = new DataTable();
                    dtQLVung = BusinessRulesLocator.Conllection().GetNotificationQLVung(Convert.ToInt32(MyUser.GetProductBrand_ID()), 0, Convert.ToInt32(MyUser.GetZone_ID()), 0, MyUser.GetUser_ID().ToString());
                    if (dtQLVung.Rows.Count > 0)
                    {
                        rptVung.DataSource = dtQLVung;
                        rptVung.DataBind();
                    }
                    Count = dtQLVung.Rows.Count;
                    if (Count > 0)
                    {
                        divNoti.Visible = true;
                    }
                    else
                    {
                        //  divNoti.Visible = false;
                    }
                }
            }

            if (MyUser.GetFunctionGroup_ID() == "3")
            {
                DataTable dtGiamsat = new DataTable();
                dtGiamsat = BusinessRulesLocator.Conllection().GetNotificationGiamSat(0, 0, 0, 0, MyUser.GetUser_ID().ToString());
                if (dtGiamsat.Rows.Count > 0)
                {
                    rptGiamSat.DataSource = dtGiamsat;
                    rptGiamSat.DataBind();
                }
                Count = dtGiamsat.Rows.Count;
                if (Count > 0)
                {
                    divNoti.Visible = true;
                }
                else
                {
                    //   divNoti.Visible = false;
                }
            }

            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                // QL.Visible = true;
                DataTable dtGiamsat = new DataTable();
                DataSet dtSet = new DataSet();
                DataTable dt = new DataTable();
                DataSet dtSetPB = new DataSet();
                DataSet dtSetCaptem = new DataSet();
                DataSet dtSetAudio = new DataSet();
                DataSet dtSetMessage = new DataSet();
                DataTable dtPB = new DataTable();
                dtSet = BusinessRulesLocator.Conllection().GetRequestProductList_Paging(1, 9999, 7, 0, Convert.ToInt32(MyUser.GetLocation_ID()), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), 0, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));

                if (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptQuanly.DataSource = dtSet.Tables[1];
                    rptQuanly.DataBind();
                    Count = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                }

                dtSetPB = BusinessRulesLocator.Conllection().GetRequestProductBrandList_Paging(1, 9999, 7, 0, Convert.ToInt32(MyUser.GetLocation_ID()), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), 0, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));

                if (Convert.ToInt32(dtSetPB.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptQuanlyDN.DataSource = dtSetPB.Tables[1];
                    rptQuanlyDN.DataBind();
                    Count += Convert.ToInt32(dtSetPB.Tables[1].Rows[0]["TotalRecord"]);
                }
                dtSetCaptem = BusinessRulesLocator.Conllection().GetRequestQRCodePackageList_Paging(1, 9999, 7, 0, Convert.ToInt32(MyUser.GetLocation_ID()), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), 0, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));

                if (Convert.ToInt32(dtSetCaptem.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptQuanlyCaptem.DataSource = dtSetCaptem.Tables[1];
                    rptQuanlyCaptem.DataBind();
                    Count += Convert.ToInt32(dtSetCaptem.Tables[1].Rows[0]["TotalRecord"]);
                }
                dtSetAudio = BusinessRulesLocator.Conllection().GetRequestAudio_Paging(1, 9999, 7, 0, Convert.ToInt32(MyUser.GetLocation_ID()), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), 0, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));

                if (Convert.ToInt32(dtSetAudio.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptQuanlyAudio.DataSource = dtSetAudio.Tables[1];
                    rptQuanlyAudio.DataBind();
                    Count += Convert.ToInt32(dtSetAudio.Tables[1].Rows[0]["TotalRecord"]);
                }
                dtSetMessage = BusinessRulesLocator.Conllection().GetRequestMessage_Paging(1, 9999, 7, 0, Convert.ToInt32(MyUser.GetLocation_ID()), Convert.ToInt32(MyUser.GetDistrict_ID()), Convert.ToInt32(MyUser.GetRank_ID()), Convert.ToInt32(MyUser.GetDepartmentMan_ID()), 0, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));

                if (Convert.ToInt32(dtSetMessage.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptQuanlyMessage.DataSource = dtSetMessage.Tables[1];
                    rptQuanlyMessage.DataBind();
                    Count += Convert.ToInt32(dtSetMessage.Tables[1].Rows[0]["TotalRecord"]);
                }
            }
            DataSet dtSetDanhGia = new DataSet();
            DataTable dtE = new DataTable();
            if (Common.GetFunctionGroupDN())
            {
                dtSetDanhGia = BusinessRulesLocator.Conllection().GetRequestEvaluaList_Pagingv2(1, 9999, 7,Convert.ToInt32( MyUser.GetProductBrand_ID()));

                if (Convert.ToInt32(dtSetDanhGia.Tables[1].Rows[0]["TotalRecord"]) > 0)
                {
                    rptDanhgiaDN.DataSource = dtSetDanhGia.Tables[1];
                    rptDanhgiaDN.DataBind();
                    Count += Convert.ToInt32(dtSetDanhGia.Tables[1].Rows[0]["TotalRecord"]);
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("~/Login.aspx", false);
            Log.writeLog(ex.ToString(), "LoadNotification");
        }
    }
}