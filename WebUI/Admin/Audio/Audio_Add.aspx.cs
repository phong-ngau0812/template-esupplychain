using DbObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Audio_Audio_Add : System.Web.UI.Page
{
    public string title = "Thêm mới âm thanh";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    btnSave.Text = "Gửi yêu cầu thêm mới âm thanh";
                }
                else
                {
                    Role.Visible = false;
                }
            }
            else
            {
                Role.Visible = false;
            }
            FillDDLddlProductBrand();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddAudio();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void UpdaAudio(int Audio_ID)
    {
        AudioRow _AudioRow = new AudioRow();
        _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);
        string fileimage = "";
        if (_AudioRow != null)
        {

            if (fulAnh.HasFile)
            {

                fileimage = "/data/audio/" + Audio_ID + "-" + (Common.RemoveFont(fulAnh.FileName));
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _AudioRow.Note = Audio_ID + "-" + (Common.RemoveFont(fulAnh.FileName));
                }
            }
            BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
            Response.Redirect("Audio_List.aspx", false);
        }

    }

    protected void UpdaAudioRequest(int Audio_ID)
    {
        AudioRequestRow _AudioRow = new AudioRequestRow();
        _AudioRow = BusinessRulesLocator.GetAudioRequestBO().GetByPrimaryKey(Audio_ID);
        string fileimage = "";
        if (_AudioRow != null)
        {

            if (fulAnh.HasFile)
            {

                fileimage = "/data/audio/" + Audio_ID + "-" + (Common.RemoveFont(fulAnh.FileName));
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _AudioRow.Note = Audio_ID + "-" + (Common.RemoveFont(fulAnh.FileName));
                }
            }
            BusinessRulesLocator.GetAudioRequestBO().Update(_AudioRow);
            ClearForm();
        }

    }
    protected void AddAudio()
    {
        try
        {
            string msg = "";
            AudioRow _AudioRow = new AudioRow();
            AudioRequestRow _AudioRequestRow = new AudioRequestRow();

            _AudioRequestRow.ProductBrand_ID = _AudioRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _AudioRequestRow.Title = _AudioRow.Title = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;

            if (ckActive.Checked)
            {
                _AudioRequestRow.Status = _AudioRow.Status = 1;
            }
            else
            {
                _AudioRequestRow.Status = _AudioRow.Status = 0;
            }
            _AudioRequestRow.CreateBy = _AudioRow.CreateBy = MyUser.GetUser_ID();
            _AudioRequestRow.CreateDate = _AudioRow.CreateDate = DateTime.Now;
            _AudioRequestRow.AudioRequest_Note = txtChange.Text;
            _AudioRequestRow.AudioRequest_Status = 0;
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    //Lưu bảng tạm thay đổi thông tin
                    BusinessRulesLocator.GetAudioRequestBO().Insert(_AudioRequestRow);
                    if (!_AudioRequestRow.IsAudioRequest_IDNull)
                    {
                        UpdaAudioRequest(_AudioRequestRow.AudioRequest_ID);
                        //Gửi thông báo thay đổi thông tin cho cấp trên
                        NotificationRow _NotificationRow = new NotificationRow();
                        _NotificationRow.Name = "Yêu cầu duyệt Audio";
                        _NotificationRow.Summary = txtChange.Text;
                        //  _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                        _NotificationRow.NotificationType_ID = 15;
                        _NotificationRow.UserID = MyUser.GetUser_ID();
                        if (MyUser.GetFunctionGroup_ID() != "1")
                            _NotificationRow.ProductBrand_ID = Convert.ToInt32(MyUser.GetProductBrand_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetDepartmentProductBrand_ID()))
                        //    _NotificationRow.Department_ID = Convert.ToInt32(MyUser.GetDepartmentProductBrand_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetZone_ID()))
                        //    _NotificationRow.Zone_ID = Convert.ToInt32(MyUser.GetZone_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetArea_ID()))
                        //    _NotificationRow.Area_ID = Convert.ToInt32(MyUser.GetArea_ID());
                        //if (!string.IsNullOrEmpty(MyUser.GetFarm_ID()))
                        //    _NotificationRow.Farm_ID = Convert.ToInt32(MyUser.GetFarm_ID());
                        _NotificationRow.Url = "/Admin/Notification/RequestAudio_List";
                        _NotificationRow.CreateBy = MyUser.GetUser_ID();
                        _NotificationRow.CreateDate = DateTime.Now;
                        _NotificationRow.Active = 1;
                        _NotificationRow.Alias = Guid.NewGuid();
                        BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                        msg = "Gửi yêu thêm mới Audio thành công !";
                    }
                }
                else
                {
                    BusinessRulesLocator.GetAudioBO().Insert(_AudioRow);
                    if (_AudioRow != null)
                    {
                        UpdaAudio(_AudioRow.Audio_ID);
                        Response.Redirect("Audio_List.aspx", false);
                    }
                }
            }
            else
            {
                BusinessRulesLocator.GetAudioBO().Insert(_AudioRow);
                if (_AudioRow != null)
                {
                    UpdaAudio(_AudioRow.Audio_ID);
                    Response.Redirect("Audio_List.aspx", false);
                }
            }
            if (!string.IsNullOrEmpty(msg))
            {
                lblMessage.Text = msg;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateAudio", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Audio_List.aspx", false);
    }
}