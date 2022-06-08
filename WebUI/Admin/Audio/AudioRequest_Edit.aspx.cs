using DbObj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Audio_AudioRequest_Edit : System.Web.UI.Page
{
    int Audio_ID = 0;
    public string title = "Thông tin âm thanh";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["AudioRequest_ID"]))
            int.TryParse(Request["AudioRequest_ID"].ToString(), out Audio_ID);
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillInfoAudio();
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
    public string audio;
    protected void FillInfoAudio()
    {
        try
        {
            if (Audio_ID != 0)
            {
                AudioRequestRow _AudioRow = new AudioRequestRow();
                _AudioRow = BusinessRulesLocator.GetAudioRequestBO().GetByPrimaryKey(Audio_ID);
                if (_AudioRow != null)
                {
                    txtName.Text = _AudioRow.IsTitleNull ? string.Empty : _AudioRow.Title;

                    audio = _AudioRow.IsNoteNull ? string.Empty : _AudioRow.Note;
                    if (_AudioRow.Status == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }
                    if (!_AudioRow.IsProductBrand_IDNull)
                    {
                        ddlProductBrand.SelectedValue = _AudioRow.ProductBrand_ID.ToString();
                    }
                    if (_AudioRow.AudioRequest_Status.ToString() == "1")
                    {
                        btnSave.Visible = false;
                    }
                    if (!_AudioRow.IsAudioRequest_NoteNull)
                    {
                        txtChange.Text = _AudioRow.AudioRequest_Note;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoAudio", ex.ToString());
        }
    }

    protected void UpdateAudio()
    {
        try
        {
            AudioRow _AudioRow = new AudioRow();
            AudioRequestRow _AudioRequestRow = new AudioRequestRow();
            if (Audio_ID != 0)
            {
                _AudioRequestRow = BusinessRulesLocator.GetAudioRequestBO().GetByPrimaryKey(Audio_ID);
                if (_AudioRequestRow != null)
                {
                    _AudioRow.Title = _AudioRequestRow.Title = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _AudioRow.ProductBrand_ID = _AudioRequestRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    _AudioRow.CreateBy = _AudioRequestRow.CreateBy;
                    _AudioRow.CreateDate = _AudioRequestRow.CreateDate;
                    _AudioRow.Status = _AudioRequestRow.Status;
                    _AudioRow.Note = _AudioRequestRow.Note;
                    _AudioRequestRow.LastEditBy = MyUser.GetUser_ID();
                    _AudioRequestRow.LastEditDate = DateTime.Now;
                    _AudioRequestRow.AudioRequest_Approved = MyUser.GetUser_ID();
                    _AudioRequestRow.AudioRequest_ApprovedNote = txtNoteChange.Text;
                    _AudioRequestRow.AudioRequest_DateApproved = DateTime.Now;
                    _AudioRequestRow.AudioRequest_Status = 1;
                    BusinessRulesLocator.GetAudioBO().Insert(_AudioRow);
                    BusinessRulesLocator.GetAudioRequestBO().Update(_AudioRequestRow);

                    //Gửi thông báo đã duyệt thay đổi thông tin cho doanh nghiệp
                    NotificationRow _NotificationRow = new NotificationRow();
                    _NotificationRow.Name = "Cơ quan quản lý đã duyệt yêu cầu thêm mới âm thanh";
                    _NotificationRow.Summary = _AudioRequestRow.AudioRequest_ApprovedNote;
                    _NotificationRow.Body = "";
                    _NotificationRow.NotificationType_ID = 16;
                    _NotificationRow.UserID = _AudioRequestRow.AudioRequest_Approved;
                    if (MyUser.GetFunctionGroup_ID() != "1")
                        _NotificationRow.ProductBrand_ID = _AudioRow.ProductBrand_ID;
                    //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                    //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                    //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                    //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                    //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                    //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                    _NotificationRow.Url = "/Audio/Audio_Edit?Audio_ID=" + _AudioRow.Audio_ID;
                    _NotificationRow.CreateBy = MyUser.GetUser_ID();
                    _NotificationRow.CreateDate = DateTime.Now;
                    _NotificationRow.Active = 1;
                    _NotificationRow.Alias = Guid.NewGuid();
                    BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                    lblMessage.Text = ("Duyệt yêu cầu thêm mới âm thanh thành công !");
                }
                lblMessage.Visible = true;
                //  Response.Redirect("/Admin/Notification/RequestProductBrand_List?update=true", false);
                Admin_Template_CMS master = this.Master as Admin_Template_CMS;
                if (master != null)
                    master.LoadNotification();
                Response.Redirect("/Admin/Notification/RequestAudio_List.aspx?update=sc", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateAudio", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateAudio();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/Notification/RequestAudio_List.aspx", false);
    }
}