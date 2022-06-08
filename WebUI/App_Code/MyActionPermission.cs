using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using DbObj;
using System.IO;
using System.Drawing;
using System.Reflection;
using SystemFrameWork;
using System.Net.Mail;
using System.Net;
using EvoPdf.HtmlToPdf;
using Geotargeting;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Xml;
using evointernal;

/// <summary>
/// Summary description for Common
/// </summary>
public class MyActionPermission
{
    public MyActionPermission()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int CheckURLPermission()
    {
        string UserID = MyUser.GetUser_ID().ToString();
        string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
        string urlDefaul = "default.aspx,User_ChangePassword, QRCode, Notification_Detail, ProductPackage_HTML, User_Edit ,QRCodePackage_Add ,QRCodePackage_Edit,QRCodePackage_List,QRCodePackage_Edit_Status,QRCodePackage_Edit_Pendingactive,QRCodePackage_Edit_Info,QRCodePackeLocationregister_List,QRCodePackeLocationregister_Add,QRCodePackeWarehouseregister_List,QRCodePackeWarehouseregister_Add,QRCodePackeWorkshopregister_List,QRCodePackeWorkshopregister_Add,QRCodePackage_Split,QRCodePackeCustomerregister_List,QRCodePacke_Edit_ProductPackage, ProductPackageVsTaskType, ProductPackage_Trace, ReportExpectedProductivity, SalesInformation_Add,QRCodePackeCustomerregister_Add , QRCodePackeProductPackageregister_Add,QRCodePackeProductPackageregister_List, ProductInfo, ProductReview_List ,ProductReview_Edit, ProductBrand_Config,QRCodePackageReport_List,TaskStepProductPackage_List, TaskStepProduct_Copy ,QRCodePackeCancel, TaskStepProduct_Setup,TaskStepQuestion_List,TaskStepQuestion_Edit, TaskStepQuestion_Add ,TaskStepAnswer_Edit,TaskStepAnswer_Add ,BaoHiem_Add,BaoHiem_Edit, BaoHiem_List, RequestProduct_List, RequestProductBrand_List, Product_ChangeEdit ,ProductBrand_ChangeEdit,QRCodepackage_print_landing,qrcodepackage_print,qrcodepackage_print1,Timekeeping_List,ManufactureTechVsTask,QRCodePackageChange_Edit,AudioRequest_Edit,MessageRequest_Edit";
        int check = 0;
        if (urlDefaul.Contains(url))
        {
            check = 1;
        }
        else
        {
            DataTable dt = BusinessRulesLocator.Conllection().CheckPermission(UserID, url + ".aspx");

            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
        }
        return check;
    }
    public static void CheckPermission(string ProductBrand_ID, string userID, string url)
    {
        if (Common.GetFunctionGroupDN())
        {
            if (ProductBrand_ID != MyUser.GetProductBrand_ID())
            {
                HttpContext.Current.Response.Redirect(url, false);
            }
            else
            {
                if (MyUser.GetDepartmentProductBrand_ID() != "1")
                {
                    //if (userID != MyUser.GetUser_ID().ToString())
                    //{
                    //    //Kiểm tra xem userID đăng nhập hệ thống có trùng với người tạo của đối tượng ko
                    //    //Ko trùng thì out
                    //    HttpContext.Current.Response.Redirect(url, false);
                    //}
                }
            }
        }
    }
    public static bool CanAdd()
    {
        //Kiểm tra xem tài khoản có quyền sửa không 
        string UserID = MyUser.GetUser_ID().ToString();
        string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
        url = url.Split('/').Last().Replace("_List", "_Add");
        DataTable dt = BusinessRulesLocator.Conllection().CheckPermission(UserID, url + ".aspx");
        if (dt.Rows.Count == 1)
        {
            int check = Convert.ToInt32(dt.Rows[0]["Count"]);
            if (check == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    public static bool CanEdit()
    {
        //Kiểm tra xem tài khoản có quyền sửa không 
        string UserID = MyUser.GetUser_ID().ToString();
        string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
        url = url.Split('/').Last().Replace("_List", "_Edit");
        DataTable dt = BusinessRulesLocator.Conllection().CheckPermission(UserID, url + ".aspx");
        if (dt.Rows.Count == 1)
        {
            int check = Convert.ToInt32(dt.Rows[0]["Count"]);
            if (check == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    private static DataTable GetRoleURL()
    {
        string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
        url = url.Split('/').Last().Replace("_List", "_Edit");
        DataTable dt = BusinessRulesLocator.Conllection().CheckPermission(MyUser.GetUser_ID().ToString(), url + ".aspx");
        return dt;
    }
    public static bool CanDeleteUser(string UserName, ref string Message)
    {
        if (MyUser.GetFunctionGroup_ID() == "1")
        {
            return true;
        }
        else
        {
            if (MyUser.GetCreateBy_ByUserName(UserName) != "")
            {
                if (Guid.Parse(MyUser.GetCreateBy_ByUserName(UserName)) == MyUser.GetUser_ID())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
    public static bool CanDeleteProductPackage(int ProductPackage_ID, ref string Message)
    {
        if (ProductPackage_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1" || MyUser.GetAccountType_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    //if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    //{
                    //    return true;
                    //}
                    //else
                    {
                        if (!_ProductPackageRow.IsCreateByNull)
                        {
                            if (_ProductPackageRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa lô của người khác";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteProduct(int Product_ID, ref string Message)
    {
        if (Product_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                ProductRow _ProductRow = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    //if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    if (!_ProductRow.IsCreateByNull)
                    {
                        if (_ProductRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                        {
                            return true;
                        }
                        else
                        {
                            Message = "Bạn không có quyền cập nhật phẩm người khác";
                            return false;
                        }
                    }
                    //}
                }
            }
        }
        return false;
    }
    public static bool CanDeleteArea(int Area_ID, ref string Message)
    {
        if (Area_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                AreaRow _AreaRow = BusinessRulesLocator.GetAreaBO().GetByPrimaryKey(Area_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_AreaRow.IsCreateByNull)
                        {
                            if (_AreaRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteCustomer(int Customer_ID, ref string Message)
    {
        if (Customer_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                CustomerRow _CustomerRow = BusinessRulesLocator.GetCustomerBO().GetByPrimaryKey(Customer_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        if (!_CustomerRow.IsCreateByNull)
                        {
                            if (_CustomerRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa khách hàng của người khác";
                                return false;
                            }
                        }

                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteCustomerType(int CustomerType_ID, ref string Message)
    {
        if (CustomerType_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                CustomerTypeRow _CustomerTypeRow = BusinessRulesLocator.GetCustomerTypeBO().GetByPrimaryKey(CustomerType_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_CustomerTypeRow.IsCreateByNull)
                        {
                            if (_CustomerTypeRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteAgecy(int Agecy_ID, ref string Message)
    {
        if (Agecy_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                AgencyRow _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Agecy_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_AgencyRow.IsCreateByNull)
                        {
                            if (_AgencyRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public static bool CanDeleteTestingCertificates(int TestingCertificates_ID, ref string Message)
    {
        if (TestingCertificates_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                TestingCertificatesRow _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_TestingCertificatesRow.IsCreateByNull)
                        {
                            if (_TestingCertificatesRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteDiscount(int Discount_ID, ref string Message)
    {
        if (Discount_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                DiscountRow _DiscountRow = BusinessRulesLocator.GetDiscountBO().GetByPrimaryKey(Discount_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        if (!_DiscountRow.IsCreateByNull)
                        {

                            if (_DiscountRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteFarm(int Farm_ID, ref string Message)
    {
        if (Farm_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                FarmRow _Farm = BusinessRulesLocator.GetFarmBO().GetByPrimaryKey(Farm_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        if (!_Farm.IsCreateByNull)
                        {
                            if (_Farm.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }

                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteProductPackageOrderCategory(int ProductPackageOrderCategory_ID, ref string Message)
    {
        if (ProductPackageOrderCategory_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                ProductPackageOrderCategoryRow _ProductPackageOrderCategoryRow = BusinessRulesLocator.GetProductPackageOrderCategoryBO().GetByPrimaryKey(ProductPackageOrderCategory_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        if (!_ProductPackageOrderCategoryRow.IsCreateByNull)
                        {
                            if (_ProductPackageOrderCategoryRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa loại lệnh của người khác";
                                return false;
                            }
                        }

                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteProductPackageOrder(int ProductPackageOrder_ID, ref string Message)
    {
        if (ProductPackageOrder_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                ProductPackageOrderRow _ProductPackageOrderRow = BusinessRulesLocator.GetProductPackageOrderBO().GetByPrimaryKey(ProductPackageOrder_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        if (!_ProductPackageOrderRow.IsCreateByNull)
                        {
                            if (_ProductPackageOrderRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa lệnh của người khác";
                                return false;
                            }
                        }

                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteMaterial(int Material_ID, ref string Message)
    {
        if (Material_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                MaterialRow _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Material_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_MaterialRow.IsCreateByNull)
                        {
                            if (_MaterialRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteSupplier(int Supplier_ID, ref string Message)
    {
        if (Supplier_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                SupplierRow _SupplierRow = BusinessRulesLocator.GetSupplierBO().GetByPrimaryKey(Supplier_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_SupplierRow.IsCreateByNull)
                        {
                            if (_SupplierRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteTransporter(int Transporter_ID, ref string Message)
    {
        if (Transporter_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                TransporterRow _TransporterRow = BusinessRulesLocator.GetTransporterBO().GetByPrimaryKey(Transporter_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_TransporterRow.IsCreateByNull)
                        {
                            if (_TransporterRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteWorkshop(int Workshop_ID, ref string Message)
    {
        if (Workshop_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                WorkshopRow _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(Workshop_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_WorkshopRow.IsCreateByNull)
                        {
                            if (_WorkshopRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa nhân viên của người khác";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteZone(int Zone_ID, ref string Message)
    {
        if (Zone_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                ZoneRow _ZoneRow = BusinessRulesLocator.GetZoneBO().GetByPrimaryKey(Zone_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_ZoneRow.IsCreateByNull)
                        {
                            if (_ZoneRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteDepartment(int Department_ID, ref string Message)
    {
        if (Department_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                DepartmentRow _DepartmentRow = BusinessRulesLocator.GetDepartmentBO().GetByPrimaryKey(Department_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_DepartmentRow.IsCreateByNull)
                        {
                            if (_DepartmentRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa phòng ban của doanh nghiệp khác";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public static bool CanDeleteBusinessType(int BusinessType_ID, ref string Message)
    {
        if (BusinessType_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                BusinessTypeRow _BusinessTypeRow = BusinessRulesLocator.GetBusinessTypeBO().GetByPrimaryKey(BusinessType_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_BusinessTypeRow.IsCreateByNull)
                        {
                            if (_BusinessTypeRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    public static bool CanDeleteSalesShift(int SalesShift_ID, ref string Message)
    {
        if (SalesShift_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                SalesShiftRow _SalesShiftRow = BusinessRulesLocator.GetSalesShiftBO().GetByPrimaryKey(SalesShift_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_SalesShiftRow.IsCreateByNull)
                        {
                            if (_SalesShiftRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa ca làm việc của doanh nghiệp khác";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    public static bool CanDeleteQRCodePackeLocationregister(int QRCodeLocationRegister_ID, ref string Message)
    {
        if (QRCodeLocationRegister_ID > 0)
        {
            DataTable dt = GetRoleURL();
            int check = 0;
            if (dt.Rows.Count == 1)
            {
                check = Convert.ToInt32(dt.Rows[0]["Count"]);
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                QRCodeLocationRegisterRow _QRCodeLocationRegisterRow = BusinessRulesLocator.GetQRCodeLocationRegisterBO().GetByPrimaryKey(QRCodeLocationRegister_ID);
                //Nhóm quyền Admin của hệ thống full quyền
                if (MyUser.GetFunctionGroup_ID() == "1")
                {
                    return true;
                }
                else
                {
                    //Nhóm quyền quản trị hệ thống doanh nghiệp full quyền doanh nghiệp
                    if (MyUser.GetDepartmentProductBrand_ID() == "1")
                    {
                        return true;
                    }
                    else
                    {

                        if (!_QRCodeLocationRegisterRow.IsCreateByNull)
                        {
                            if (_QRCodeLocationRegisterRow.CreateBy.ToString() == MyUser.GetUser_ID().ToString())
                            {
                                return true;
                            }
                            else
                            {
                                Message = "Bạn không có quyền xóa";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public static void WriteLogSystem(int ID_Object, string Name)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
            SystemLogRow _SystemLogRow = new SystemLogRow();
            _SystemLogRow.SystemLogType_ID = 0;
            _SystemLogRow.Name = Name;
            _SystemLogRow.URL = url;
            if (!string.IsNullOrEmpty(MyUser.GetProductBrand_ID()))
            {
                _SystemLogRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
            else
            {
                _SystemLogRow.ProductBrand_ID = 0;
            }
            if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
            {
                _SystemLogRow.DepartmentProductBrand_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
            }
            else
            {
                _SystemLogRow.DepartmentProductBrand_ID = 0;
            }
            _SystemLogRow.UserName = HttpContext.Current.User.Identity.Name;
            _SystemLogRow.CreateDate = DateTime.Now;
            _SystemLogRow.CreateBy = MyUser.GetUser_ID();
            _SystemLogRow.ID_Object = ID_Object;
            BusinessRulesLocator.GetSystemLogBO().Insert(_SystemLogRow);
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    public static void WriteLogSystemLogin(int ID_Object, string Name, string username)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/').Last();
            SystemLogRow _SystemLogRow = new SystemLogRow();
            _SystemLogRow.SystemLogType_ID = 1;
            _SystemLogRow.Name = Name;
            _SystemLogRow.URL = url;
            if (!string.IsNullOrEmpty(MyUser.GetProductBrand_ID()))
            {
                _SystemLogRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
            }
            else
            {
                _SystemLogRow.ProductBrand_ID = 0;
            }
            if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
            {
                _SystemLogRow.DepartmentProductBrand_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
            }
            else
            {
                _SystemLogRow.DepartmentProductBrand_ID = 0;
            }
            _SystemLogRow.UserName = HttpContext.Current.User.Identity.Name;
            _SystemLogRow.CreateDate = DateTime.Now;
            _SystemLogRow.CreateBy = MyUser.GetUser_IDByUserName(username);
            _SystemLogRow.ID_Object = ID_Object;
            BusinessRulesLocator.GetSystemLogBO().Insert(_SystemLogRow);
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadUser", ex.ToString());
        }
    }
}

