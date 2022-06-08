using DbObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Message_Message_Add : System.Web.UI.Page
{
    public string title = "Thêm mới thông điệp";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    btnSave.Text = "Gửi yêu cầu thêm mới thông điệp";
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
            AddMessage();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    //protected void UpdaMessage(int Message_ID)
    //{
    //    MessageRow _MessageRow = new MessageRow();
    //    _MessageRow = BusinessRulesLocator.GetMessageBO().GetByPrimaryKey(Message_ID);
    //    string fileimage = "";
    //    if (_MessageRow != null)
    //    {

    //        BusinessRulesLocator.GetMessageBO().Update(_MessageRow);
    //        lblMessage.Text = "Thêm mới thành công!";
    //        lblMessage.Visible = true;
    //        ClearForm();
    //        Response.Redirect("Message_List.aspx", false);
    //    }

    //}
    protected void AddMessage()
    {
        try
        {
            string msg = "";
            MessageRequestRow _MessageRequestRow = new MessageRequestRow();
            MessageRow _MessageRow = new MessageRow();
            _MessageRequestRow.ProductBrand_ID = _MessageRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _MessageRequestRow.Title = _MessageRow.Title = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _MessageRequestRow.Note = _MessageRow.Note = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            if (ckActive.Checked)
            {
                _MessageRequestRow.Status = _MessageRow.Status = 1;
            }
            else
            {
                _MessageRequestRow.Status = _MessageRow.Status = 0;
            }
            _MessageRequestRow.CreateBy = _MessageRow.CreateBy = MyUser.GetUser_ID();
            _MessageRequestRow.CreateDate = _MessageRow.CreateDate = DateTime.Now;
            _MessageRequestRow.MessageRequest_Status = 0;
            _MessageRequestRow.MessageRequest_Note = txtChange.Text;
            if (!string.IsNullOrEmpty(MyUser.GetProductBrandRole_ID()))
            {
                if (MyUser.GetProductBrandRole_ID() == "1")
                {
                    //Lưu bảng tạm thay đổi thông tin
                    BusinessRulesLocator.GetMessageRequestBO().Insert(_MessageRequestRow);
                    if (!_MessageRequestRow.IsMessageRequest_IDNull)
                    {
                        //Gửi thông báo thay đổi thông tin cho cấp trên
                        NotificationRow _NotificationRow = new NotificationRow();
                        _NotificationRow.Name = "Yêu cầu duyệt Thông Điệp";
                        _NotificationRow.Summary = txtChange.Text;
                        //  _NotificationRow.Body = _TaskRow.Task_ID.ToString();
                        _NotificationRow.NotificationType_ID = 17;
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
                        _NotificationRow.Url = "/Admin/Notification/RequestMessage_List";
                        _NotificationRow.CreateBy = MyUser.GetUser_ID();
                        _NotificationRow.CreateDate = DateTime.Now;
                        _NotificationRow.Active = 1;
                        _NotificationRow.Alias = Guid.NewGuid();
                        BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);
                        msg = "Gửi yêu thêm mới Thông điệp thành công !";
                        ClearForm();
                    }
                }
                else
                {
                    BusinessRulesLocator.GetMessageBO().Insert(_MessageRow);
                    Response.Redirect("Message_List.aspx", false);
                }
            }
            else
            {
                BusinessRulesLocator.GetMessageBO().Insert(_MessageRow);
                Response.Redirect("Message_List.aspx", false);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                lblMessage.Text = msg;
                lblMessage.Visible = true;
            }



        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMessage", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtName.Text = "";
        ckActive.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Message_List.aspx", false);
    }
}