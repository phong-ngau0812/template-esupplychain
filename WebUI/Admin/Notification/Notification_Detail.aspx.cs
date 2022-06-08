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

public partial class Notification_Detail : System.Web.UI.Page
{
    public string name, nguoitao, nguoiduyet, thoigian,url= string.Empty;
    public string Alias = "";
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["Alias"]))
            Alias = Request["Alias"].ToString();
        Init();
    }

    public string NotiName, NotiDetail, NguoiXuat, NgayXuat, yeucau, duyet = string.Empty;
    private void Init()
    {
        if (!string.IsNullOrEmpty(Alias))
        {
            DataTable dt = BusinessRulesLocator.GetNotificationBO().GetAsDataTable(" Alias='" + Alias + "'", "");
            if (dt.Rows.Count > 0)
            {
                NotiName = dt.Rows[0]["Name"].ToString();
                NotiDetail = dt.Rows[0]["Summary"].ToString();
                NguoiXuat = (new MyUser()).FullNameFromUser_ID(dt.Rows[0]["CreateBy"].ToString());
                NgayXuat = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                if (!string.IsNullOrEmpty(dt.Rows[0]["ProductPackageOrder_ID"].ToString()))
                {
                    ProductPackageOrder_ID = Convert.ToInt32(dt.Rows[0]["ProductPackageOrder_ID"]);
                    ProductPackageOrderRow _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_ID);
                    if (_ProductPackageOrderRow != null)
                    {
                        name = _ProductPackageOrderRow.Name;
                        nguoitao = MyUser.UserNameFromUser_ID(_ProductPackageOrderRow.CreateBy.ToString());
                        nguoiduyet = MyUser.UserNameFromUser_ID(_ProductPackageOrderRow.AdminApproveBy.ToString());
                        thoigian = _ProductPackageOrderRow.CreateDate.ToString("dd/MM/yyyy");
                        DataTable dtMaterial = BusinessRulesLocator.Conllection().GetAllList(" select A.*,M.Unit, M.Name as NameM from ProductPackageOrderMaterial A left join Material M on A.Material_ID= M.Material_ID where A.ProductPackageOrder_ID=" + ProductPackageOrder_ID);
                        if (dtMaterial.Rows.Count > 0)
                        {
                            rptMaterial.DataSource = dtMaterial;
                            rptMaterial.DataBind();
                        }
                    }
                }

                NotificationRow _NotificationRow = BusinessRulesLocator.GetNotificationBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["Notification_ID"]));
                if (_NotificationRow != null)
                {
                    if (_NotificationRow.NotificationType_ID == 7)
                    {
                        btnExport.Visible = false;
                        NotiApproved.Visible = false;
                        ReadNotificationRow _ReadNotificationRow = new ReadNotificationRow
                        {
                            Notification_ID = Convert.ToInt32(dt.Rows[0]["Notification_ID"]),
                            UserID = MyUser.GetUser_ID(),
                            ViewDate = DateTime.Now
                        };
                        BusinessRulesLocator.GetReadNotificationBO().Insert(_ReadNotificationRow);

                    }
                    if (_NotificationRow.NotificationType_ID == 8)
                    {
                        Noti.Visible = false;
                        Export.Visible = true;
                        NotiApproved.Visible = false;
                        ReadNotificationRow _ReadNotificationRow = new ReadNotificationRow
                        {
                            Notification_ID = Convert.ToInt32(dt.Rows[0]["Notification_ID"]),
                            UserID = MyUser.GetUser_ID(),
                            ViewDate = DateTime.Now
                        };
                        BusinessRulesLocator.GetReadNotificationBO().Insert(_ReadNotificationRow);
                    }
                    if (_NotificationRow.NotificationType_ID == 12)
                    {
                        Noti.Visible = false;
                        Export.Visible = false;
                        NotiApproved.Visible = true;
                        name = dt.Rows[0]["Name"].ToString();
                        url = dt.Rows[0]["URL"].ToString();
                        //if (!string.IsNullOrEmpty(url))
                        {
                            if (url.Contains("ProductBrand"))
                            {
                                ProductBrandChangeRow _ProductBrandChangeRow = BusinessRulesLocator.GetProductBrandChangeBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["Body"].ToString()));
                                if (_ProductBrandChangeRow != null)
                                {
                                    yeucau = _ProductBrandChangeRow.IsProductBrandChange_NoteNull ? string.Empty : _ProductBrandChangeRow.ProductBrandChange_Note;
                                    duyet = _ProductBrandChangeRow.IsProductBrandChange_ApprovedNoteNull ? string.Empty : _ProductBrandChangeRow.ProductBrandChange_ApprovedNote;
                                    //if (_ProductBrandChangeRow.ProductBrandChange_Status==2)
                                    //{
                                    //    TT.Visible = false;
                                    //}
                                }
                            }
                            else
                            {
                                ProductChangeRow _ProductChangeRow = BusinessRulesLocator.GetProductChangeBO().GetByPrimaryKey(Convert.ToInt32(dt.Rows[0]["Body"].ToString()));
                                if (_ProductChangeRow != null)
                                {
                                    yeucau = _ProductChangeRow.IsProductChange_NoteNull ? string.Empty : _ProductChangeRow.ProductChange_Note;
                                    duyet = _ProductChangeRow.IsProductChange_ApprovedNoteNull ? string.Empty : _ProductChangeRow.ProductChange_ApprovedNote;
                                    //if (_ProductChangeRow.ProductChange_Status == 2)
                                    //{
                                    //    TT.Visible = false;
                                    //}
                                }
                            }
                        }
                        nguoitao = MyUser.UserNameFromUser_ID(dt.Rows[0]["UserID"].ToString());
                        nguoiduyet = MyUser.UserNameFromUser_ID(dt.Rows[0]["CreateBy"].ToString());
                        thoigian = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                        ReadNotificationRow _ReadNotificationRow = new ReadNotificationRow
                        {
                            Notification_ID = Convert.ToInt32(dt.Rows[0]["Notification_ID"]),
                            UserID = MyUser.GetUser_ID(),
                            ViewDate = DateTime.Now
                        };
                        BusinessRulesLocator.GetReadNotificationBO().Insert(_ReadNotificationRow);
                    }
                    Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                    if (master != null)
                        master.LoadNotification();
                }
            }
        }
    }

    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code=" + code, false);
        Response.Redirect("/", false);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/WarehouseExport/WarehouseExport_Add?ProductPackageOrder_ID=" + ProductPackageOrder_ID, false);
    }
}