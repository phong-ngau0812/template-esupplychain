using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Common.DisposeUser();
        FormsAuthentication.SignOut();
        Session.Abandon();

        // clear authentication cookie
        HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        cookie1.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie1);

        // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
        SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
        HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
        cookie2.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie2);
        FormsAuthentication.RedirectToLoginPage();
        Response.Redirect("Login.aspx",false);
    }
}
