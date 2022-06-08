using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ctlDatePicker : System.Web.UI.UserControl
{
    public delegate void DateChangeHandle(object sender, EventArgs e);
    public event DateChangeHandle DateChange;

    public int DateType
    {
        //-1:   Tất cả
        //0:    Tùy chọn
        //1:    Tuần này
        //2:    Tuần trước
        //3:    Tháng này
        //4:    Tháng trước
        //5:    Hôm nay

        get { return Session["DateType"] == null ? -1 : int.Parse(Session["DateType"].ToString()); }
        set
        {
            Session.Add("DateType", value);

        }
    }
    public DateTime FromDate
    {
        get
        {
            return Session["FromDate"] == null ? DateTime.ParseExact("01011900", "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None) : (DateTime)(Session["FromDate"]);
        }
        set { Session.Add("FromDate", value); }
    }

    public DateTime ToDate
    {
        get
        {
            return Session["ToDate"] == null ? DateTime.ParseExact("31129999", "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None) : (DateTime)(Session["ToDate"]);
        }
        set { Session.Add("ToDate", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DateType = 5;

            //dpkFromDate.SelectedDate = DateTime.Now.Date.AddDays(-30);
            dpkFromDate.SelectedDate = new DateTime(2015, 01, 01);
            dpkToDate.SelectedDate = DateTime.Now.Date;

            if (Session["FromDate"] != null)
                dpkFromDate.SelectedDate = FromDate;
            if (Session["ToDate"] != null)
                dpkToDate.SelectedDate = ToDate;

            FromDate = (DateTime)dpkFromDate.SelectedDate;
            ToDate = (DateTime)dpkToDate.SelectedDate;

            if (this.DateChange != null)
                this.DateChange(this, e);
        }

        //cboDateType.SelectedValue = DateType.ToString();
    }

    protected void cboDateType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DateType = int.Parse(cboDateType.SelectedValue);
        if (cboDateType.SelectedValue == "5")
            GetThisDay();
        if (cboDateType.SelectedValue == "1")
            GetThisWeek();
        if (cboDateType.SelectedValue == "2")
            GetLastWeek();
        if (cboDateType.SelectedValue == "3")
            GetThisMonth();
        if (cboDateType.SelectedValue == "4")
            GetLastMonth();
        if (cboDateType.SelectedValue == "-1")
            GetAllTime();

        FromDate = (DateTime)dpkFromDate.SelectedDate;
        ToDate = (DateTime)dpkToDate.SelectedDate;

        if (this.DateChange != null)
            this.DateChange(this, e);
    }

    protected void dpkFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        FromDate = (DateTime)dpkFromDate.SelectedDate;
        if (this.DateChange != null)
            this.DateChange(this, e);
    }

    protected void dpkToDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ToDate = (DateTime)dpkToDate.SelectedDate;
        if (this.DateChange != null)
            this.DateChange(this, e);
    }

    void GetThisDay()
    {
        DateTime StartThisWeek = DateTime.Today;
        DateTime EndThisWeek = DateTime.Today;

        dpkFromDate.SelectedDate = StartThisWeek;
        dpkToDate.SelectedDate = EndThisWeek;
    }

    void GetThisWeek()
    {
        DateTime StartThisWeek = DateTime.Today;
        DateTime EndThisWeek = DateTime.Today;

        if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
        {
            StartThisWeek = DateTime.Today.AddDays(0);
            EndThisWeek = DateTime.Today.AddDays(6);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
        {
            StartThisWeek = DateTime.Today.AddDays(-1);
            EndThisWeek = DateTime.Today.AddDays(5);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
        {
            StartThisWeek = DateTime.Today.AddDays(-2);
            EndThisWeek = DateTime.Today.AddDays(4);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
        {
            StartThisWeek = DateTime.Today.AddDays(-3);
            EndThisWeek = DateTime.Today.AddDays(3);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
        {
            StartThisWeek = DateTime.Today.AddDays(-4);
            EndThisWeek = DateTime.Today.AddDays(2);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
        {
            StartThisWeek = DateTime.Today.AddDays(-5);
            EndThisWeek = DateTime.Today.AddDays(1);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
        {
            StartThisWeek = DateTime.Today.AddDays(-6);
            EndThisWeek = DateTime.Today.AddDays(0);
        }

        dpkFromDate.SelectedDate = StartThisWeek;
        dpkToDate.SelectedDate = EndThisWeek;
    }

    void GetLastWeek()
    {
        DateTime StartThisWeek = DateTime.Today;
        DateTime EndThisWeek = DateTime.Today;

        if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
        {
            StartThisWeek = DateTime.Today.AddDays(0);
            EndThisWeek = DateTime.Today.AddDays(6);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
        {
            StartThisWeek = DateTime.Today.AddDays(-1);
            EndThisWeek = DateTime.Today.AddDays(5);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
        {
            StartThisWeek = DateTime.Today.AddDays(-2);
            EndThisWeek = DateTime.Today.AddDays(4);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
        {
            StartThisWeek = DateTime.Today.AddDays(-3);
            EndThisWeek = DateTime.Today.AddDays(3);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
        {
            StartThisWeek = DateTime.Today.AddDays(-4);
            EndThisWeek = DateTime.Today.AddDays(2);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
        {
            StartThisWeek = DateTime.Today.AddDays(-5);
            EndThisWeek = DateTime.Today.AddDays(1);
        }
        if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
        {
            StartThisWeek = DateTime.Today.AddDays(-6);
            EndThisWeek = DateTime.Today.AddDays(0);
        }

        dpkFromDate.SelectedDate = StartThisWeek.AddDays(-7);
        dpkToDate.SelectedDate = EndThisWeek.AddDays(-7);
    }

    void GetThisMonth()
    {
        DateTime StartThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        DateTime EndThisMonth = StartThisMonth.AddMonths(1).AddDays(-1);

        dpkFromDate.SelectedDate = StartThisMonth;
        dpkToDate.SelectedDate = EndThisMonth;
    }

    void GetLastMonth()
    {
        DateTime StartLastMonth = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
        DateTime EndLastMonth = StartLastMonth.AddMonths(1).AddDays(-1);

        dpkFromDate.SelectedDate = StartLastMonth;
        dpkToDate.SelectedDate = EndLastMonth;
    }

    void GetAllTime()
    {
        dpkFromDate.SelectedDate = new DateTime(2015, 01, 01);
        dpkToDate.SelectedDate = DateTime.Today;
    }
}