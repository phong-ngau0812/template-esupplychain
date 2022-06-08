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

/// <summary>
/// Summary description for Common
/// </summary>
public partial class UserProfile
{
    private System.Web.Profile.ProfileBase _profileBase;

    public UserProfile()
    {
        this._profileBase = new System.Web.Profile.ProfileBase();
    }

    public UserProfile(System.Web.Profile.ProfileBase profileBase)
    {
        this._profileBase = profileBase;
    }

    // Các thuộc tính của Profile
    public virtual string FullName
    {
        get
        {
            return ((string)(this.GetPropertyValue("FullName")));
        }
        set
        {
            this.SetPropertyValue("FullName", value);
        }
    }

    public virtual string Gender
    {
        get
        {
            return ((string)(this.GetPropertyValue("Gender")));
        }
        set
        {
            this.SetPropertyValue("Gender", value);
        }
    }

    public virtual DateTime BirthDate
    {
        get
        {
            if (this.GetPropertyValue("BirthDate") != null)
            {
                return Convert.ToDateTime(this.GetPropertyValue("BirthDate"));
            }
            else return DateTime.Now;
        }
        set
        {
            this.SetPropertyValue("BirthDate", value);
        }
    }

    public virtual string Company
    {
        get
        {
            return ((string)(this.GetPropertyValue("Company")));
        }
        set
        {
            this.SetPropertyValue("Company", value);
        }
    }
    public virtual string ProductBrand_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("ProductBrand_ID")));
        }
        set
        {
            this.SetPropertyValue("ProductBrand_ID", value);
        }
    }

    public virtual string FunctionGroup_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("FunctionGroup_ID")));
        }
        set
        {
            this.SetPropertyValue("FunctionGroup_ID", value);
        }
    }
    public virtual string FarmGroup_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("FarmGroup_ID")));
        }
        set
        {
            this.SetPropertyValue("FarmGroup_ID", value);
        }
    }
    public virtual string FarmGroupInter_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("FarmGroupInter_ID")));
        }
        set
        {
            this.SetPropertyValue("FarmGroupInter_ID", value);
        }
    }
    public virtual string Zone_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Zone_ID")));
        }
        set
        {
            this.SetPropertyValue("Zone_ID", value);
        }
    }
    public virtual string Workshop_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Workshop_ID")));
        }
        set
        {
            this.SetPropertyValue("Workshop_ID", value);
        }
    }
    public virtual string Department_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Department_ID")));
        }
        set
        {
            this.SetPropertyValue("Department_ID", value);
        }
    }
    public virtual string AccountType_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("AccountType_ID")));
        }
        set
        {
            this.SetPropertyValue("AccountType_ID", value);
        }
    }
    public virtual string Rank_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Rank_ID")));
        }
        set
        {
            this.SetPropertyValue("Rank_ID", value);
        }
    }
    public virtual string Address
    {
        get
        {
            return ((string)(this.GetPropertyValue("Address")));
        }
        set
        {
            this.SetPropertyValue("Address", value);
        }
    }

    public virtual string Location_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Location_ID")));
        }
        set
        {
            this.SetPropertyValue("Location_ID", value);
        }
    }
    public virtual string District_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("District_ID")));
        }
        set
        {
            this.SetPropertyValue("District_ID", value);
        }
    }
    public virtual string Ward_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Ward_ID")));
        }
        set
        {
            this.SetPropertyValue("Ward_ID", value);
        }
    }
    public virtual string DepartmentMan_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("DepartmentMan_ID")));
        }
        set
        {
            this.SetPropertyValue("DepartmentMan_ID", value);
        }
    }

    public virtual string Phone
    {
        get
        {
            return ((string)(this.GetPropertyValue("Phone")));
        }
        set
        {
            this.SetPropertyValue("Phone", value);
        }
    }

    public virtual string Fax
    {
        get
        {
            return ((string)(this.GetPropertyValue("Fax")));
        }
        set
        {
            this.SetPropertyValue("Fax", value);
        }
    }

    public virtual string Website
    {
        get
        {
            return ((string)(this.GetPropertyValue("Website")));
        }
        set
        {
            this.SetPropertyValue("Website", value);
        }
    }

    public virtual string YM
    {
        get
        {
            return ((string)(this.GetPropertyValue("YM")));
        }
        set
        {
            this.SetPropertyValue("YM", value);
        }
    }

    public virtual string Skype
    {
        get
        {
            return ((string)(this.GetPropertyValue("Skype")));
        }
        set
        {
            this.SetPropertyValue("Skype", value);
        }
    }

    public virtual string AvatarUrl
    {
        get
        {
            return ((string)(this.GetPropertyValue("AvatarUrl")));
        }
        set
        {
            this.SetPropertyValue("AvatarUrl", value);
        }
    }

    public virtual string Signature
    {
        get
        {
            return ((string)(this.GetPropertyValue("Signature")));
        }
        set
        {
            this.SetPropertyValue("Signature", value);
        }
    }

    public virtual string Area_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Area_ID")));
        }
        set
        {
            this.SetPropertyValue("Area_ID", value);
        }
    }
    public virtual string Farm_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Farm_ID")));
        }
        set
        {
            this.SetPropertyValue("Farm_ID", value);
        }
    }
    public virtual string Module_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Module_ID")));
        }
        set
        {
            this.SetPropertyValue("Module_ID", value);
        }
    }
    public virtual string IsSale
    {
        get
        {
            return ((string)(this.GetPropertyValue("IsSale")));
        }
        set
        {
            this.SetPropertyValue("IsSale", value);
        }
    }
    public virtual string AccountCheckVN_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("AccountCheckVN_ID")));
        }
        set
        {
            this.SetPropertyValue("AccountCheckVN_ID", value);
        }
    }
    public virtual string Warehouse_ID
    {
        get
        {
            return ((string)(this.GetPropertyValue("Warehouse_ID")));
        }
        set
        {
            this.SetPropertyValue("Warehouse_ID", value);
        }
    }
    public virtual string CreateBy
    {
        get
        {
            return ((string)(this.GetPropertyValue("CreateBy")));
        }
        set
        {
            this.SetPropertyValue("CreateBy", value);
        }
    }
    //public virtual string DepartmentUser_ID
    //{
    //    get
    //    {
    //        return ((string)(this.GetPropertyValue("DepartmentUser_ID")));
    //    }
    //    set
    //    {
    //        this.SetPropertyValue("DepartmentUser_ID", value);
    //    }
    //}
    // Kết thúc các thuộc tính của Profile

    public static UserProfile Current
    {
        get
        {
            return new UserProfile(System.Web.HttpContext.Current.Profile);
        }
    }

    public virtual System.Web.Profile.ProfileBase ProfileBase
    {
        get
        {
            return this._profileBase;
        }
    }

    public virtual object this[string propertyName]
    {
        get
        {
            return this._profileBase[propertyName];
        }
        set
        {
            this._profileBase[propertyName] = value;
        }
    }

    public virtual string UserName
    {
        get
        {
            return this._profileBase.UserName;
        }
    }

    public virtual bool IsAnonymous
    {
        get
        {
            return this._profileBase.IsAnonymous;
        }
    }

    public virtual bool IsDirty
    {
        get
        {
            return this._profileBase.IsDirty;
        }
    }

    public virtual System.DateTime LastActivityDate
    {
        get
        {
            return this._profileBase.LastActivityDate;
        }
    }

    public virtual System.DateTime LastUpdatedDate
    {
        get
        {
            return this._profileBase.LastUpdatedDate;
        }
    }

    public virtual System.Configuration.SettingsProviderCollection Providers
    {
        get
        {
            return this._profileBase.Providers;
        }
    }

    public virtual System.Configuration.SettingsPropertyValueCollection PropertyValues
    {
        get
        {
            return this._profileBase.PropertyValues;
        }
    }

    public virtual System.Configuration.SettingsContext Context
    {
        get
        {
            return this._profileBase.Context;
        }
    }

    public virtual bool IsSynchronized
    {
        get
        {
            return this._profileBase.IsSynchronized;
        }
    }

    public static System.Configuration.SettingsPropertyCollection Properties
    {
        get
        {
            return System.Web.Profile.ProfileBase.Properties;
        }
    }

    public static UserProfile GetProfile(string username)
    {
        return new UserProfile(System.Web.Profile.ProfileBase.Create(username));
    }

    public static UserProfile GetProfile(string username, bool authenticated)
    {
        return new UserProfile(System.Web.Profile.ProfileBase.Create(username, authenticated));
    }

    public virtual object GetPropertyValue(string propertyName)
    {
        return this._profileBase.GetPropertyValue(propertyName);
    }

    public virtual void SetPropertyValue(string propertyName, object propertyValue)
    {
        this._profileBase.SetPropertyValue(propertyName, propertyValue);
    }

    public virtual System.Web.Profile.ProfileGroupBase GetProfileGroup(string groupName)
    {
        return this._profileBase.GetProfileGroup(groupName);
    }

    public virtual void Initialize(string username, bool isAuthenticated)
    {
        this._profileBase.Initialize(username, isAuthenticated);
    }

    public virtual void Save()
    {
        this._profileBase.Save();
    }

    public virtual void Initialize(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyCollection properties, System.Configuration.SettingsProviderCollection providers)
    {
        this._profileBase.Initialize(context, properties, providers);
    }

    public static System.Configuration.SettingsBase Synchronized(System.Configuration.SettingsBase settingsBase)
    {
        return System.Web.Profile.ProfileBase.Synchronized(settingsBase);
    }

    public static System.Web.Profile.ProfileBase Create(string userName)
    {
        return System.Web.Profile.ProfileBase.Create(userName);
    }

    public static System.Web.Profile.ProfileBase Create(string userName, bool isAuthenticated)
    {
        return System.Web.Profile.ProfileBase.Create(userName, isAuthenticated);
    }
}
