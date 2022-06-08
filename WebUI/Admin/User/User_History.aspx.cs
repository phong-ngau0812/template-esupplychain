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

public partial class Admin_User_User_History : System.Web.UI.Page
{
    public int TotalPage = 1;
    public int TotalItem = 0;
    int pageSize = 10;//Số bản ghi 1 trang
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDLProducrBrand();
            FillDDLDeparment();
            LoadDepartmentUser();
            FillDDLFunctionGroup();
            LoadZone();
            LoadArea();
            LoadHistory();

        }
        ResetMsg();
    }
    private void FillDDLProducrBrand()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }
            Common.FillProductBrand(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
                FillFunctionGroup.Visible = false;

            }
            else
            {
                FillFunctionGroup.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLDeparment", ex.ToString());
        }
    }
    protected void LoadArea()
    {
        try
        {
            string where = " 1=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }
            if (ddlZone.SelectedValue != "0")
            {
                where += " and Zone_ID=" + ddlZone.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAreaBO().GetAsDataTable(where, " Name ASC");
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "Name";
            ddlArea.DataValueField = "Area_ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                {
                    ddlArea.SelectedValue = MyUser.GetArea_ID();
                    ddlArea.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void LoadDepartmentUser()
    {
        try
        {
            DataTable dt = new DataTable();
            ddlDepartmentUser.Items.Clear();
            if (ddlProductBrand.SelectedValue != "0")
            {
                dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ddlProductBrand.SelectedValue, " Sort ASC");
                ddlDepartmentUser.DataSource = dt;
                ddlDepartmentUser.DataTextField = "Name";
                ddlDepartmentUser.DataValueField = "Department_ID";
                ddlDepartmentUser.DataBind();
                ddlDepartmentUser.Items.Insert(0, new ListItem("-- Chọn phòng ban --", "0"));
                if (MyUser.GetDepartment_ID() != "")
                {
                    ddlDepartmentUser.SelectedValue = MyUser.GetDepartment_ID();
                    ddlDepartmentUser.Enabled = false;
                }
            }
            else
            {
                ddlDepartmentUser.Items.Insert(0, new ListItem("-- Chọn phòng ban --", "0"));
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }
    private void FillDDLFunctionGroup()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetFunctionGroupBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlFunctionGroup.DataSource = dt;
            ddlFunctionGroup.DataTextField = "Name";
            ddlFunctionGroup.DataValueField = "FunctionGroup_ID";
            ddlFunctionGroup.DataBind();
            ddlFunctionGroup.Items.Insert(0, new ListItem("-- Chọn nhóm chức năng --", "0"));
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
    private void FillDDLDeparment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAccountTypeBO().GetAsDataTable(" Active=1", " Sort ASC");
            ddlType.DataSource = dt;
            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "AccountType_ID";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("-- Chọn loại tài khoản --", "0"));
            if (MyUser.GetAccountType_ID() != "0")
            {
                if (MyUser.GetAccountType_ID() != "1")
                {
                    ddlType.SelectedValue = MyUser.GetAccountType_ID();
                    ddlType.Enabled = false;
                }
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLDeparment", ex.ToString());
        }
    }
    protected void LoadZone()
    {
        try
        {
            string where = " Active=1";
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID =" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetZoneBO().GetAsDataTable(where, " Name ASC");
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Name";
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("-- Chọn vùng --", "0"));
            ddlArea.Items.Insert(0, new ListItem("-- Chọn khu --", "0"));

            if (Common.GetFunctionGroupDN())
            {
                if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                {
                    ddlZone.SelectedValue = MyUser.GetZone_ID();
                    ddlZone.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadFunctionGroup", ex.ToString());
        }
    }
    protected void LoadUser()
    {
        try
        {


        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    MembershipUser user;
    private void LoadHistory()
    {
        try
        {
            string where = string.Empty;
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                string ProductBrand_ID = ddlProductBrand.SelectedValue;

                var dtList = Membership.GetAllUsers();

                if (ProductBrand_ID != "" && ProductBrand_ID != "0")
                {
                    var dtSource = Membership.GetAllUsers();
                    foreach (MembershipUser item in dtSource)
                    {
                        UserProfile ProfileUser = UserProfile.GetProfile(item.UserName);
                        if (Request.QueryString["IsSale"] == null)
                        {
                            if (ProfileUser.IsSale == "1")
                            {
                                dtList.Remove(ProfileUser.UserName);
                            }
                            if (ProfileUser.ProductBrand_ID != ProductBrand_ID && ProductBrand_ID != "")
                            {
                                dtList.Remove(ProfileUser.UserName);

                            }
                            if (ddlZone.SelectedValue != "0")
                            {
                                if (ProfileUser.Zone_ID != ddlZone.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }

                            }
                            if (ddlArea.SelectedValue != "0")
                            {
                                if (ProfileUser.Area_ID != ddlArea.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }


                            if (ddlType.SelectedValue != "0")
                            {
                                if (ProfileUser.AccountType_ID != ddlType.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                            if (ddlDepartmentUser.SelectedValue != "0")
                            {
                                if (ProfileUser.Department_ID != ddlDepartmentUser.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                            if (ddlProductBrand.SelectedValue != "0")
                            {
                                if (ProfileUser.ProductBrand_ID != ddlProductBrand.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                            if (ddlFunctionGroup.SelectedValue != "0")
                            {
                                if (ProfileUser.FunctionGroup_ID != ddlFunctionGroup.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }

                        }
                        else
                        {
                            if (ProfileUser.IsSale != "1")
                            {
                                dtList.Remove(ProfileUser.UserName);
                            }
                        }
                    }
                }
                else
                {

                    var dtSource = Membership.GetAllUsers();
                    foreach (MembershipUser item in dtSource)
                    {
                        UserProfile ProfileUser = UserProfile.GetProfile(item.UserName);

                        if (Request.QueryString["IsSale"] == null)
                        {
                            if (ProfileUser.IsSale == "1")
                            {
                                dtList.Remove(ProfileUser.UserName);
                            }
                            if (ddlType.SelectedValue != "0")
                            {
                                if (ProfileUser.AccountType_ID != ddlType.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                            if (ddlProductBrand.SelectedValue != "0")
                            {
                                if (ProfileUser.ProductBrand_ID != ddlProductBrand.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                            if (MyUser.GetFunctionGroup_ID() == "8")
                            {
                                if (MyUser.GetRank_ID() == "2")
                                {
                                    if (ProfileUser.DepartmentMan_ID != MyUser.GetDepartmentMan_ID() || ProfileUser.Location_ID != MyUser.GetLocation_ID())
                                    {
                                        dtList.Remove(ProfileUser.UserName);
                                    }
                                }
                                else if (MyUser.GetRank_ID() == "3")
                                {
                                    if (ProfileUser.District_ID != MyUser.GetDistrict_ID())
                                    {
                                        dtList.Remove(ProfileUser.UserName);
                                    }
                                }
                                else if (MyUser.GetRank_ID() == "4")
                                {
                                    if (ProfileUser.Ward_ID != MyUser.GetWard_ID())
                                    {
                                        dtList.Remove(ProfileUser.UserName);
                                    }
                                }
                            }
                            if (ddlFunctionGroup.SelectedValue != "0")
                            {
                                if (ProfileUser.FunctionGroup_ID != ddlFunctionGroup.SelectedValue)
                                {
                                    dtList.Remove(ProfileUser.UserName);
                                }
                            }
                        }
                        else
                        {
                            if (ProfileUser.IsSale != "1")
                            {
                                dtList.Remove(ProfileUser.UserName);
                            }
                        }
                    }
                }

                int Flag = 1;
                foreach (var item in dtList.Cast<MembershipUser>().OrderByDescending(x => x.CreationDate).ToList())
                {
                    if (Flag > 1)
                    {
                        where += " OR ";
                    }
                    where += "Name like '%Login success - " + item.UserName + "%'";
                    Flag++;
                }

            }
            else
            {
                where += "Name like '%Login success%'";
            }


            pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            Pager1.PageSize = pageSize = Convert.ToInt32(ddlItem.SelectedValue);
            DataSet dtSet = new DataSet();
            DataTable dt = new DataTable();
            dtSet = BusinessRulesLocator.Conllection().GetHistoryLogin(Pager1.CurrentIndex, pageSize, 7, where);
            //dtSet = BusinessRulesLocator.Conllection().GetFarmV2(1, 1000, 7, 0, 0, 0, "", "");
            rptFarm.DataSource = dtSet.Tables[0];
            rptFarm.DataBind();
            if (dtSet.Tables[0].Rows.Count > 0)
            {
                TotalItem = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                if (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) % pageSize != 0)
                {
                    TotalPage = (Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize) + 1;
                }
                else
                {
                    TotalPage = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]) / pageSize;
                }
                Pager1.ItemCount = Convert.ToInt32(dtSet.Tables[1].Rows[0]["TotalRecord"]);
                x_box_pager.Visible = Pager1.ItemCount > pageSize ? true : false;
            }
            else
            {
                x_box_pager.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadHistory", ex.ToString());
        }
    }
    protected void Pager1_Command(object sender, CommandEventArgs e)
    {
        int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
        Pager1.CurrentIndex = currnetPageIndx;
        LoadHistory();
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadHistory();
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pager1.CurrentIndex = 1;
        LoadHistory();
    }



    protected void rptFarm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblProductBrand = e.Item.FindControl("lblProductBrand") as Literal;
            Literal lblUserName = e.Item.FindControl("lblName") as Literal;
            if (lblUserName != null)
            {
                string name = lblUserName.Text.Split('-')[1].Trim();
                int ProductBrand_ID = MyUser.GetProductBrand_IDByUserName(name);
                if (ProductBrand_ID != 0)
                {
                    ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(ProductBrand_ID);
                    if (_ProductBrandRow != null)
                    {
                        if (!_ProductBrandRow.IsNameNull)
                        {
                            lblProductBrand.Text = " của <b>" + _ProductBrandRow.Name + "</b>";
                        }
                    }
                }
            }
        }
    }
}