using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Audio_Audio_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {

            FillDDLddlProductBrand();
            LoadAudio();
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
    protected void LoadAudio()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "0")
            {
                where += " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }

            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetAudioBO().GetAsDataTable(" Status<>-1 " + where, " CreateDate DESC");
            rptAudio.DataSource = dt;
            rptAudio.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Audio_Add.aspx", false);
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
                AudioRow _AudioRow = new AudioRow();
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                    if (ckActive.Checked)
                    {
                        _AudioRow.Status = 1;
                        BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _AudioRow.Status = 0;
                        BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadAudio();
                }

            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptAudio_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        int Audio_ID = Convert.ToInt32(e.CommandArgument);
        AudioRow _AudioRow = new AudioRow();
        _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);

        switch (e.CommandName)
        {
            case "Delete":

                // if (MyActionPermission.CanDeleteAudio(Audio_ID, ref Message))
                {
                    if (_AudioRow != null)
                    {
                        _AudioRow.Status = -1;
                    }
                    BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                    MyActionPermission.WriteLogSystem(Audio_ID, "Xóa - " + _AudioRow.Title);
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
                _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);
                if (_AudioRow != null)
                {
                    _AudioRow.Status = 1;
                }
                BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);
                if (_AudioRow != null)
                {
                    _AudioRow.Status = 0;
                }
                BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadAudio();
    }

    protected void rptAudio_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        LoadAudio();
    }
}