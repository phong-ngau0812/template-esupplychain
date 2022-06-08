using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Admin_usercontrol_ctlMenuHome : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["indexMenu"] = null;
        Session["indexMenu"] = 1;


        lblDayNow.Text = "To day: " + DateTime.Now.ToString("D"); ;
    }
    public static string GetDayOfWeekVN(System.DateTime dValue)
    {
        switch (dValue.DayOfWeek)
        {
            case DayOfWeek.Friday:
                return "Friday";
            case DayOfWeek.Monday:
                return "Monday";
            case DayOfWeek.Saturday:
                return "Saturday";
            case DayOfWeek.Sunday:
                return "Sunday";
            case DayOfWeek.Thursday:
                return "Thursday";
            case DayOfWeek.Tuesday:
                return "Tuesday";
            case DayOfWeek.Wednesday:
                return "Wednesday";
        }
        return dValue.DayOfWeek.ToString();
    }
}
