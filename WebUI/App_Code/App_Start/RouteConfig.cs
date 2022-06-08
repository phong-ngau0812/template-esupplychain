using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.Resolvers;

namespace ASP
{
    //public static class RouteConfig
    //{
    //    public static void RegisterRoutes(RouteCollection routes)
    //    {
    //        var settings = new FriendlyUrlSettings();
    //        settings.AutoRedirectMode = RedirectMode.Permanent;
    //        routes.EnableFriendlyUrls(new FriendlyUrlSettings
    //        {
    //            AutoRedirectMode = RedirectMode.Permanent
    //        });
    //    }
    //    public class CKFinderWebFormsFriendlyUrlResolver : WebFormsFriendlyUrlResolver
    //    {
    //        public override string ConvertToFriendlyUrl(string path)
    //        {
    //            if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/ckfinder"))
    //            {
    //                return path;
    //            }

    //            return base.ConvertToFriendlyUrl(path);
    //        }
    //    }
    //}
    public class MyWebFormsFriendlyUrlResolver : WebFormsFriendlyUrlResolver
    {
        public MyWebFormsFriendlyUrlResolver() : base()
        {
        }

        public override string ConvertToFriendlyUrl(string path)
        {
            if (!string.IsNullOrEmpty(path) & path.ToLower().Contains("/ckfinder"))
                return path;

            return base.ConvertToFriendlyUrl(path);
        }
    }
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("P", "P", "~/P.aspx");

            FriendlyUrlSettings settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings, new Microsoft.AspNet.FriendlyUrls.Resolvers.IFriendlyUrlResolver[] { new MyWebFormsFriendlyUrlResolver() });
        }
    }
}
