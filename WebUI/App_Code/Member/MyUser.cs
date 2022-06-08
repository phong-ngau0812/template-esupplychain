using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for MyUser
/// </summary>
public class MyUser
{
    public MyUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static Guid GetUser_ID()
    {
        Guid user = new Guid();
        DataTable dt = BusinessRulesLocator.Conllection().GetAllList(" select UserId from aspnet_Users where UserName='" + HttpContext.Current.User.Identity.Name + "'");
        if (dt.Rows.Count == 1)
        {
            string User_ID = dt.Rows[0]["UserId"].ToString();
            user = new Guid(User_ID);
        }
        //User_ID = Membership.GetUser(HttpContext.Current.User.Identity, false).;
        return user;
    }
    public static Guid GetUser_IDByUserName(string userName)
    {
        string User_ID = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(" select UserId from aspnet_Users where UserName='" + userName + "'");
        if (dt.Rows.Count == 1)
        {
            User_ID = dt.Rows[0]["UserId"].ToString();
        }
        //User_ID = Membership.GetUser(HttpContext.Current.User.Identity, false).;
        return new Guid(User_ID);
    }
    public static string UserNameFromUser_ID(string User_ID)
    {
        string UserName = "";
        if (!string.IsNullOrEmpty(User_ID))
        {
            try
            {
                Guid userKey = new Guid(User_ID);
                MembershipUser mu = Membership.GetUser(userKey, false);
                UserName = mu.UserName;
            }
            catch { }

        }
        return UserName;
    }
    public static int GetProductBrand_IDByUserName(string UserName)
    {
        UserProfile ProfileUser;
        int ProductBrand_ID = 0;

        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.ProductBrand_ID))
            {
                ProductBrand_ID = Convert.ToInt32(ProfileUser.ProductBrand_ID);
            }
        }
        return ProductBrand_ID;
    }
    public static string GetCreateBy_ByUserName(string UserName)
    {
        UserProfile ProfileUser;
        string CreateBy = string.Empty;

        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.CreateBy))
            {
                CreateBy = (ProfileUser.CreateBy);
            }
        }
        return CreateBy;
    }
    public string FullNameFromUserName(string UserName)
    {
        UserProfile ProfileUser;
        string FullName = "Không xác định";
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FullName = ProfileUser.FullName;
        }
        if (string.IsNullOrEmpty(FullName))
            FullName = UserName;

        return FullName;
    }
    public static string GetProductBrandRole_ID()
    {
        string role = string.Empty;
        if (!string.IsNullOrEmpty(GetProductBrand_ID()))
        {
            ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(Convert.ToInt32(GetProductBrand_ID()));
            if (_ProductBrandRow != null)
            {
                role = _ProductBrandRow.IsProductBrandRole_IDNull ? string.Empty : _ProductBrandRow.ProductBrandRole_ID.ToString();
            }
        }
        return role;
    }
    public static string GetProductBrand_ID()
    {
        UserProfile ProfileUser;
        string ProductBrand_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            ProductBrand_ID = ProfileUser.ProductBrand_ID;
        }
        return ProductBrand_ID;
    }
    public static string GetRank_ID()
    {
        UserProfile ProfileUser;
        string Rank_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.Rank_ID))
            {
                Rank_ID = ProfileUser.Rank_ID;
            }
            else
            {
                Rank_ID = "0";
            }

        }
        return Rank_ID;
    }
    public static string GetDepartmentMan_ID()
    {
        UserProfile ProfileUser;
        string DepartmentMan_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.DepartmentMan_ID))
            {
                DepartmentMan_ID = ProfileUser.DepartmentMan_ID;
            }
            else
            {
                DepartmentMan_ID = "0";
            }

        }
        return DepartmentMan_ID;
    }
    public static string GetDistrict_ID()
    {
        UserProfile ProfileUser;
        string DepartmentMan_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.District_ID))
            {
                DepartmentMan_ID = ProfileUser.District_ID;
            }
            else
            {
                DepartmentMan_ID = "0";
            }

        }
        return DepartmentMan_ID;
    }
    public static string GetWard_ID()
    {
        UserProfile ProfileUser;
        string DepartmentMan_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.Ward_ID))
            {
                DepartmentMan_ID = ProfileUser.Ward_ID;
            }
            else
            {
                DepartmentMan_ID = "0";
            }

        }
        return DepartmentMan_ID;
    }
    public static string GetLocation_ID()
    {
        UserProfile ProfileUser;
        string Location_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            if (!string.IsNullOrEmpty(ProfileUser.Rank_ID))
            {
                Location_ID = ProfileUser.Location_ID;
            }
            else
            {
                Location_ID = "0";
            }

        }
        return Location_ID;
    }
    public static string GetFarm_ID()
    {
        UserProfile ProfileUser;
        string Farm_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            Farm_ID = ProfileUser.Farm_ID;
        }
        return Farm_ID;
    }
    public static string GetZone_ID()
    {
        UserProfile ProfileUser;
        string Zone_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            Zone_ID = ProfileUser.Zone_ID;
        }
        return Zone_ID;
    }
    public static string GetWarehouse_ID()
    {
        UserProfile ProfileUser;
        string WareHouse_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            WareHouse_ID = ProfileUser.Warehouse_ID;
        }
        return WareHouse_ID;
    }
    public static string GetArea_ID()
    {
        UserProfile ProfileUser;
        string Area_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            Area_ID = ProfileUser.Area_ID;
        }
        return Area_ID;
    }
    public static string GetWorkshop_ID()
    {
        UserProfile ProfileUser;
        string Area_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            Area_ID = ProfileUser.Workshop_ID;
        }
        return Area_ID;
    }
    public static string GetDepartmentProductBrand_ID()
    {
        UserProfile ProfileUser;
        string ProductBrand_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            ProductBrand_ID = ProfileUser.AccountType_ID;
        }
        return ProductBrand_ID;
    }
    public static string GetFunctionGroup_ID()
    {
        UserProfile ProfileUser;
        string FunctionGroup_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FunctionGroup_ID = ProfileUser.FunctionGroup_ID;
        }
        return FunctionGroup_ID;
    }
    public static string GetAccountCheckVN_ID()
    {
        UserProfile ProfileUser;
        string FunctionGroup_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FunctionGroup_ID = ProfileUser.AccountCheckVN_ID;
        }
        return FunctionGroup_ID;
    }
    public static string GetAccountType_ID()
    {
        UserProfile ProfileUser;
        string FunctionGroup_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FunctionGroup_ID = ProfileUser.AccountType_ID;
        }
        return FunctionGroup_ID;
    }
    public static string GetDepartment_ID()
    {
        UserProfile ProfileUser;
        string FunctionGroup_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FunctionGroup_ID = ProfileUser.Department_ID;
        }
        return FunctionGroup_ID;
    }
    public static string GetModule_ID()
    {
        UserProfile ProfileUser;
        string FunctionGroup_ID = string.Empty;
        string UserName = HttpContext.Current.User.Identity.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FunctionGroup_ID = ProfileUser.Module_ID;
        }
        return FunctionGroup_ID;
    }
    public string PhoneFromUserName(string UserName)
    {
        UserProfile ProfileUser;
        string FullName = "Chưa xác định";
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            FullName = ProfileUser.Phone;
        }
        return FullName;
    }
    public string AvatarFromUserName(string UserName)
    {
        UserProfile ProfileUser;
        string Ava = "<img src='../../images/no-image-icon.png'/>";
        if (!string.IsNullOrEmpty(UserName))
        {
            ProfileUser = UserProfile.GetProfile(UserName);
            Ava = "<img src='" + ProfileUser.AvatarUrl + "' width='50px;'/>";
        }
        return Ava;
    }
    public string FullNameFromUser_ID(string User_ID)
    {
        string FullName = "Chưa xác định";
        FullName = FullNameFromUserName(UserNameFromUser_ID(User_ID));
        return FullName;
    }

    public string OnlineStatus(string UserName)
    {
        string tmp = "<span class='badge badge-danger'>Offline</span>";
        bool Online = Membership.GetUser(UserName, false).IsOnline;

        if (Online)
        {
            tmp = "<span class='badge badge-success'>Online</span>";
        }
        return tmp;
    }
    public string ApprovedStatus(string UserName)
    {
        string tmp = "0";
        bool Online = Membership.GetUser(UserName, false).IsApproved;

        if (Online)
        {
            tmp = "1";
        }
        return tmp;
    }
}