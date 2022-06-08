using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class User_ChangePassword : System.Web.UI.Page
{
    public string username;
    MembershipUser user;
    UserProfile ProfileUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //    Response.Redirect("/");

        username = Context.User.Identity.Name;
        user = Membership.GetUser(username, false);
        ProfileUser = UserProfile.GetProfile(username);

        if (!Page.IsPostBack)
        {

        }
    }

    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("User_List.aspx", false);
    }

}