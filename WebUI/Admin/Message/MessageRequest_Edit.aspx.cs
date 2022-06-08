using DbObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Message_MessageRequest_Edit : System.Web.UI.Page
{

    int Message_ID = 0;
    public string title = "Thông tin thông điệp";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["MessageRequest_ID"]))
            int.TryParse(Request["MessageRequest_ID"].ToString(), out Message_ID);
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
                MessageRequestRow _MessageRow = new MessageRequestRow();
                _MessageRow = BusinessRulesLocator.GetMessageRequestBO().GetByPrimaryKey(Message_ID);
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
                    txtChange.Text = _MessageRow.IsMessageRequest_NoteNull ? string.Empty : _MessageRow.MessageRequest_Note;

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
            MessageRequestRow _MessageRequestRow = new MessageRequestRow();
            if (Message_ID != 0)
            {
                _MessageRequestRow = BusinessRulesLocator.GetMessageRequestBO().GetByPrimaryKey(Message_ID);
                if (_MessageRequestRow != null)
                {
                    _MessageRow.Title = _MessageRequestRow.Title;
                    _MessageRow.Note = _MessageRequestRow.Note;
                    _MessageRow.ProductBrand_ID = _MessageRequestRow.ProductBrand_ID;
                    _MessageRow.Status = _MessageRequestRow.Status;
                    _MessageRow.CreateBy = _MessageRequestRow.CreateBy;
                    _MessageRow.CreateDate = _MessageRequestRow.CreateDate;
                    _MessageRequestRow.MessageRequest_Status = 1;
                    _MessageRequestRow.MessageRequest_Approved = MyUser.GetUser_ID();
                    _MessageRequestRow.MessageRequest_ApprovedNote = txtNoteChange.Text;
                    _MessageRequestRow.MessageRequest_DateApproved = DateTime.Now;
                    BusinessRulesLocator.GetMessageBO().Insert(_MessageRow);
                    BusinessRulesLocator.GetMessageRequestBO().Update(_MessageRequestRow);
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thêm mới thông điệp";
                    _NotificationRow.Summary = _MessageRequestRow.MessageRequest_ApprovedNote;
                    _NotificationRow.Body = "";
                    _NotificationRow.NotificationType_ID = 18;
                    _NotificationRow.UserID = _MessageRequestRow.MessageRequest_Approved;
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = _MessageRow.ProductBrand_ID;
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Message/Message_Edit?Message_ID=" + _MessageRow.Message_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    lblMessage.Text = ("Duyệt yêu cầu thêm mới thông điệp thành công !");
                }
                lblMessage.Visible = true;
                //  Response.Redirect("/Admin/Notification/RequestProductBrand_List?update=true", false);
                Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                if (master != null)
                    master.LoadNotification();
                Response.Redirect("/Admin/Notification/RequestMessage_List.aspx?update=sc", false);
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
        Response.Redirect("/Admin/Notification/RequestMessage_List.aspx", false);
    }
}