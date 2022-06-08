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

public partial class User_ListKD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            FillDDLProducrBrand();
            FillDDLDeparment();
            LoadDepartmentUser();
            FillDDLFunctionGroup();
            LoadUser();
        }
        //if (Request.QueryString["IsSale"] != null)
        //{
        //    kd1.Visible = kd2.Visible = kd3.Visible = FillFunctionGroup.Visible = AddAccountAdmin.Visible = false;
        //}
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadDepartmentUser()
    {
        try
        {
            DataTable dt = new DataTable();
            ddlDepartmentUser.Items.Clear();
            if (ddlProductBrand.SelectedValue!="0")
            {
                dt = BusinessRulesLocator.GetDepartmentBO().GetAsDataTable(" Active=1 and ProductBrand_ID=" + ddlProductBrand.SelectedValue, " Sort ASC");
                ddlDepartmentUser.DataSource = dt;
                ddlDepartmentUser.DataTextField = "Name";
                ddlDepartmentUser.DataValueField = "Department_ID";
                ddlDepartmentUser.DataBind();
                ddlDepartmentUser.Items.Insert(0, new ListItem("-- Chọn phòng ban --", "0"));
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
    protected void LoadUser()
    {
        try
        {
            string ProductBrand_ID = ddlProductBrand.SelectedValue;

            var dtList = Membership.GetAllUsers();

            if (ProductBrand_ID != "" && ProductBrand_ID != "0")
            {
                var dtSource = Membership.GetAllUsers();
                foreach (MembershipUser item in dtSource)
                {
                    UserProfile ProfileUser = UserProfile.GetProfile(item.UserName);
                    //if (Request.QueryString["IsSale"] == null)
                    //{
                    //    if (ProfileUser.IsSale == "1")
                    //    {
                    //        dtList.Remove(ProfileUser.UserName);
                    //    }
                    //    if (ProfileUser.ProductBrand_ID != ProductBrand_ID && ProductBrand_ID != "")
                    //    {
                    //        dtList.Remove(ProfileUser.UserName);

                    //    }
                    //    if (Common.GetFunctionGroupDN())
                    //    {
                    //        if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //        {
                    //            if (ProfileUser.Zone_ID != MyUser.GetZone_ID())
                    //            {
                    //                dtList.Remove(ProfileUser.UserName);
                    //            }
                    //        }
                    //    }

                    //    if (ddlType.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.AccountType_ID != ddlType.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //    if (ddlDepartmentUser.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.Department_ID != ddlDepartmentUser.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //    if (ddlProductBrand.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.ProductBrand_ID != ddlProductBrand.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //    if (ddlFunctionGroup.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.FunctionGroup_ID != ddlFunctionGroup.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        if (ProfileUser.IsSale != "1")
                        {
                            dtList.Remove(ProfileUser.UserName);
                        }
                    //}
                }
            }
            else
            {

                var dtSource = Membership.GetAllUsers();
                foreach (MembershipUser item in dtSource)
                {
                    UserProfile ProfileUser = UserProfile.GetProfile(item.UserName);

                    //if (Request.QueryString["IsSale"] == null)
                    //{
                    //    if (ProfileUser.IsSale == "1")
                    //    {
                    //        dtList.Remove(ProfileUser.UserName);
                    //    }
                    //    if (ddlType.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.AccountType_ID != ddlType.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //    if (ddlProductBrand.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.ProductBrand_ID != ddlProductBrand.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //    if (ddlFunctionGroup.SelectedValue != "0")
                    //    {
                    //        if (ProfileUser.FunctionGroup_ID != ddlFunctionGroup.SelectedValue)
                    //        {
                    //            dtList.Remove(ProfileUser.UserName);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        if (ProfileUser.IsSale != "1")
                        {
                            dtList.Remove(ProfileUser.UserName);
                        }
                    //}
                }
            }
            rptUser.DataSource = dtList.Cast<MembershipUser>().OrderByDescending(x=>x.CreationDate).ToList();
            rptUser.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }
    MembershipUser user;
    protected void rptUser_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MembershipUser User = Membership.GetUser(e.CommandArgument.ToString(), false);
        switch (e.CommandName)
        {
            case "Delete":
                MyActionPermission.WriteLogSystem(0, "Xóa - " + User.UserName + MyUser.GetUser_IDByUserName(User.UserName));
                Membership.DeleteUser(e.CommandArgument.ToString(), true);
                LoadUser();
                lblMessage.Text = Common.GetSuccessMsg("Xóa bản ghi thành công !");
                lblMessage.Visible = true; ;
                break;
            case "Reset":
                user = Membership.GetUser(e.CommandArgument.ToString(), false);
                user.ChangePassword(user.ResetPassword(), "checkvn@123");
                Membership.UpdateUser(user);
                MyActionPermission.WriteLogSystem(0, "Khôi phục mật khẩu - " + User.UserName + " - " + MyUser.GetUser_IDByUserName(User.UserName));
                lblMessage.Text = Common.GetSuccessMsg("Tài khoản " + e.CommandArgument.ToString() + " đã được khôi phục mật khẩu về: checkvn@123");
                lblMessage.Visible = true; ;
                break;
            case "Active":
                User.IsApproved = true;
                lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                MyActionPermission.WriteLogSystem(0, "Kích hoạt - " + User.UserName +" - "+ MyUser.GetUser_IDByUserName(User.UserName));
                Membership.UpdateUser(User);
                lblMessage.Visible = true;
                User.UnlockUser();
                break;
            case "Deactive":
                User.IsApproved = false;
                Membership.UpdateUser(User);
                lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                MyActionPermission.WriteLogSystem(0, "Ngừng kích hoạt - " + User.UserName + " - " + MyUser.GetUser_IDByUserName(User.UserName));
                lblMessage.Visible = true;
                break;
        }
        LoadUser();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("User_Add.aspx?IsSale=true", false);
    }

    protected void ckActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckActive = (CheckBox)sender;
        RepeaterItem row = (RepeaterItem)ckActive.NamingContainer;
        Literal lblID = (Literal)row.FindControl("lblID");
        try
        {

            MembershipUser User = Membership.GetUser(lblID.Text, false);
            if (ckActive.Checked)
            {
                User.IsApproved = true;
                lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                Membership.UpdateUser(User);
                lblMessage.Visible = true;
                User.UnlockUser();
            }
            else
            {
                User.IsApproved = false;
                Membership.UpdateUser(User);
                lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                lblMessage.Visible = true;
            }

            LoadUser();
        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                if (lblApproved.Text == "0")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    //lblText.Text = "<span class=\"badge badge-soft-danger\">Ngừng kích hoạt</span>";
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else

                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                    //lblText.Text = "<span class=\"badge badge-soft-success\">Đã kích hoạt</span>";
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                }
            }
        }
    }
    private void FillDDLProducrBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
              //  AddAccountProductBrand.Visible = true;
                FillFunctionGroup.Visible = false;
                AddAccountAdmin.Visible = false;
            }
            else
            {
                FillFunctionGroup.Visible = true;
                AddAccountAdmin.Visible = true;
              //  AddAccountProductBrand.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLDeparment", ex.ToString());
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
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
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLDeparment", ex.ToString());
        }
    }

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDepartmentUser();
        LoadUser();
        
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

    protected void ddlFunctionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }
}