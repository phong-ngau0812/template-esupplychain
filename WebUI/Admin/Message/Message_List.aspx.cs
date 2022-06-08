using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Message_Message_List : System.Web.UI.Page
{

    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {

            FillDDLddlProductBrand();
            LoadMessage();
        }
        ResetMsg();
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, " ");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadMessage()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetMessageBO().GetAsDataTable(" Status<>-1 " + where, " CreateDate DESC");
            rptMessage.DataSource = dt;
            rptMessage.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Message_Add.aspx", false);
    }

    protected void ckActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckActive = (CheckBox)sender;
        RepeaterItem row = (RepeaterItem)ckActive.NamingContainer;
        Literal lblID = (Literal)row.FindControl("lblID");
        try
        {
            if (lblID != null)
            {
                MessageRow _MessageRow = new MessageRow();
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                    if (ckActive.Checked)
                    {
                        _MessageRow.Status = 1;
                        BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _MessageRow.Status = 0;
                        BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadMessage();
                }

            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        int Message_ID = Convert.ToInt32(e.CommandArgument);
        MessageRow _MessageRow = new MessageRow();
        _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);

        switch (e.CommandName)
        {
            case "Delete":

                // if (MyActionPermission.CanDeleteMessage(Message_ID, ref Message))
                {
                    if (_MessageRow != null)
                    {
                        _MessageRow.Status = -1;
                    }
                    BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                    MyActionPermission.WriteLogSystem(Message_ID, "Xóa - " + _MessageRow.Title);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                //else
                //{
                //    lblMessage.Text = Message;
                //    lblMessage.Style.Add("background", "wheat");
                //    lblMessage.ForeColor = Color.Red;
                //}
                break;
            case "Active":
                _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);
                if (_MessageRow != null)
                {
                    _MessageRow.Status = 1;
                }
                BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);
                if (_MessageRow != null)
                {
                    _MessageRow.Status = 0;
                }
                BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadMessage();
    }

    protected void rptMessage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            //CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
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

    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMessage();
    }
}