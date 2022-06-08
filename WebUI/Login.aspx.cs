using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Login : System.Web.UI.Page
{
    public string ReturnURL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["ReturnURL"]))
            ReturnURL = (Request["ReturnURL"].ToString());
        if (Request.IsAuthenticated)
        {
            Login.Visible = false;
            LoggedView.Visible = true;
            if (!string.IsNullOrEmpty(ReturnURL))
            {
                Response.Redirect("/?ReturnURL=" + ReturnURL, false);
            }
        }
        else
        {
            Login.Visible = true;
            LoggedView.Visible = false;
        }

    }

}