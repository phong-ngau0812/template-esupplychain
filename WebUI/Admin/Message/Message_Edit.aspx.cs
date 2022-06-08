using DbObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Message_Message_Edit : System.Web.UI.Page
{
    int Message_ID = 0;
    public string title = "Thông tin thông điệp";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Message_ID"]))
            int.TryParse(Request["Message_ID"].ToString(), out Message_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillInfoMessage();
        }
    }
    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null_ChuaXD(ddlProductBrand, "");
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
    public string Message;
    protected void FillInfoMessage()
    {
        try
        {
            if (Message_ID != 0)
            {
                MessageRow _MessageRow = new MessageRow();
                _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);
                if (_MessageRow != null)
                {
                    txtName.Text = _MessageRow.IsTitleNull ? string.Empty : _MessageRow.Title;

                    txtNote.Text = _MessageRow.IsNoteNull ? string.Empty : _MessageRow.Note;
                    if (_MessageRow.Status == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                    if (!_MessageRow.IsProductBrand_IDNull)
                    {
                        ddlProductBrand.SelectedValue = _MessageRow.ProductBrand_ID.ToString();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoMessage", ex.ToString());
        }
    }

    protected void UpdateMessage()
    {
        try
        {
            MessageRow _MessageRow = new MessageRow();
            if (Message_ID != 0)
            {
                _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);
                if (_MessageRow != null)
                {
                    _MessageRow.Title = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _MessageRow.Note = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _MessageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);


                    if (ckActive.Checked)
                    {
                        _MessageRow.Status = 1;
                    }
                    else
                    {
                        _MessageRow.Status = 0;
                    }
                    _MessageRow.LastEditBy = MyUser.GetUser_ID();
                    _MessageRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoMessage();
                Response.Redirect("Message_List.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMessage", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateMessage();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Message_List.aspx", false);
    }
}