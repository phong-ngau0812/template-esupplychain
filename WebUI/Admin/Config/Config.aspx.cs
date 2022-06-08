using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Config_Config : System.Web.UI.Page
{
    public string title = "Cấu hình chung";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            FillDDLAudioAndMessgade();
            LoadData();
        }
    }

    private void LoadData()
    {
        try
        {
            ConfigAudioVsMessageRow _ConfigAudioVsMessageRow = BusinessRulesLocator.GetConfigAudioVsMessageBO().GetByPrimaryKey(1);
            if (_ConfigAudioVsMessageRow != null)
            {
                // txtTitle.Text = _ConfigAudioVsMessageRow.IsTraceabilityTitleNull ? string.Empty : _ConfigAudioVsMessageRow.TraceabilityTitle;
                ddlAudioPublic.SelectedValue = _ConfigAudioVsMessageRow.IsAudioPublicNull ? "" : _ConfigAudioVsMessageRow.AudioPublic.ToString();
                ddlAudioSold.SelectedValue = _ConfigAudioVsMessageRow.IsAudioSoldNull ? "" : _ConfigAudioVsMessageRow.AudioSold.ToString();
                ddlAudioIDE.SelectedValue = _ConfigAudioVsMessageRow.IsAudioIDENull ? "" : _ConfigAudioVsMessageRow.AudioIDE.ToString();
                ddlAudioNoExsits.SelectedValue = _ConfigAudioVsMessageRow.IsAudioNoExsitsNull ? "" : _ConfigAudioVsMessageRow.AudioNoExsits.ToString();
                ddlAudioCancel.SelectedValue = _ConfigAudioVsMessageRow.IsAudioCancelNull ? "" : _ConfigAudioVsMessageRow.AudioCancel.ToString();
                ddlAudioPrint.SelectedValue = _ConfigAudioVsMessageRow.IsAudioPrintNull ? "" : _ConfigAudioVsMessageRow.AudioPrint.ToString();
                ddlAudioActive.SelectedValue = _ConfigAudioVsMessageRow.IsAudioActiveNull ? "" : _ConfigAudioVsMessageRow.AudioActive.ToString();


                ddlMessagePublic.SelectedValue = _ConfigAudioVsMessageRow.IsMessagePublicNull ? "" : _ConfigAudioVsMessageRow.MessagePublic.ToString();
                ddlMessageSold.SelectedValue = _ConfigAudioVsMessageRow.IsMessageSoldNull ? "" : _ConfigAudioVsMessageRow.MessageSold.ToString();
                ddlMessageIDE.SelectedValue = _ConfigAudioVsMessageRow.IsMessageIDENull ? "" : _ConfigAudioVsMessageRow.MessageIDE.ToString();
                ddlMessageNoExsit.SelectedValue = _ConfigAudioVsMessageRow.IsMessageNoExsitsNull ? "" : _ConfigAudioVsMessageRow.MessageNoExsits.ToString();
                ddlMessageCancel.SelectedValue = _ConfigAudioVsMessageRow.IsMessageCancelNull ? "" : _ConfigAudioVsMessageRow.MessageCancel.ToString();
                ddlMessagePrint.SelectedValue = _ConfigAudioVsMessageRow.IsMessagePrintNull ? "" : _ConfigAudioVsMessageRow.MessagePrint.ToString();
                ddlMessageActive.SelectedValue = _ConfigAudioVsMessageRow.IsMessageActiveNull ? "" : _ConfigAudioVsMessageRow.MessageActive.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("LoadData", ex.ToString());
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
    private void FillDDLAudioAndMessgade()
    {
        try
        {
            string where = string.Empty;
            if (ddlProductBrand.SelectedValue != "")
            {
                where = " and ProductBrand_ID=" + ddlProductBrand.SelectedValue;
            }
            DataTable dtAudio = BusinessRulesLocator.GetAudioBO().GetAsDataTable(" Status=1" + where, " Title ASC");
            DataTable dtMessage = BusinessRulesLocator.GetMessageBO().GetAsDataTable(" Status=1" + where, " Title ASC");

            ddlAudioPublic.DataSource = dtAudio;
            ddlAudioPublic.DataTextField = "Title";
            ddlAudioPublic.DataValueField = "Audio_ID";
            ddlAudioPublic.DataBind();
            ddlAudioPublic.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioSold.DataSource = dtAudio;
            ddlAudioSold.DataTextField = "Title";
            ddlAudioSold.DataValueField = "Audio_ID";
            ddlAudioSold.DataBind();
            ddlAudioSold.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioIDE.DataSource = dtAudio;
            ddlAudioIDE.DataTextField = "Title";
            ddlAudioIDE.DataValueField = "Audio_ID";
            ddlAudioIDE.DataBind();
            ddlAudioIDE.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioNoExsits.DataSource = dtAudio;
            ddlAudioNoExsits.DataTextField = "Title";
            ddlAudioNoExsits.DataValueField = "Audio_ID";
            ddlAudioNoExsits.DataBind();
            ddlAudioNoExsits.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioCancel.DataSource = dtAudio;
            ddlAudioCancel.DataTextField = "Title";
            ddlAudioCancel.DataValueField = "Audio_ID";
            ddlAudioCancel.DataBind();
            ddlAudioCancel.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));

            ddlAudioPrint.DataSource = dtAudio;
            ddlAudioPrint.DataTextField = "Title";
            ddlAudioPrint.DataValueField = "Audio_ID";
            ddlAudioPrint.DataBind();
            ddlAudioPrint.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));


            ddlAudioActive.DataSource = dtAudio;
            ddlAudioActive.DataTextField = "Title";
            ddlAudioActive.DataValueField = "Audio_ID";
            ddlAudioActive.DataBind();
            ddlAudioActive.Items.Insert(0, new ListItem("-- Chọn âm thanh --", ""));



            ddlMessagePublic.DataSource = dtMessage;
            ddlMessagePublic.DataTextField = "Title";
            ddlMessagePublic.DataValueField = "Message_ID";
            ddlMessagePublic.DataBind();
            ddlMessagePublic.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageSold.DataSource = dtMessage;
            ddlMessageSold.DataTextField = "Title";
            ddlMessageSold.DataValueField = "Message_ID";
            ddlMessageSold.DataBind();
            ddlMessageSold.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageIDE.DataSource = dtMessage;
            ddlMessageIDE.DataTextField = "Title";
            ddlMessageIDE.DataValueField = "Message_ID";
            ddlMessageIDE.DataBind();
            ddlMessageIDE.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageNoExsit.DataSource = dtMessage;
            ddlMessageNoExsit.DataTextField = "Title";
            ddlMessageNoExsit.DataValueField = "Message_ID";
            ddlMessageNoExsit.DataBind();
            ddlMessageNoExsit.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));


            ddlMessageCancel.DataSource = dtMessage;
            ddlMessageCancel.DataTextField = "Title";
            ddlMessageCancel.DataValueField = "Message_ID";
            ddlMessageCancel.DataBind();
            ddlMessageCancel.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));


            ddlMessagePrint.DataSource = dtMessage;
            ddlMessagePrint.DataTextField = "Title";
            ddlMessagePrint.DataValueField = "Message_ID";
            ddlMessagePrint.DataBind();
            ddlMessagePrint.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

            ddlMessageActive.DataSource = dtMessage;
            ddlMessageActive.DataTextField = "Title";
            ddlMessageActive.DataValueField = "Message_ID";
            ddlMessageActive.DataBind();
            ddlMessageActive.Items.Insert(0, new ListItem("-- Chọn thông điệp --", ""));

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
            UpdateInfo();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void UpdateInfo()
    {
        try
        {

            ConfigAudioVsMessageRow _ConfigAudioVsMessageRow = BusinessRulesLocator.GetConfigAudioVsMessageBO().GetByPrimaryKey(1);
            // _ConfigAudioVsMessageRow.TraceabilityTitle = txtTitle.Text;
            
            _ConfigAudioVsMessageRow.AudioPublic = Convert.ToInt32(ddlAudioPublic.SelectedValue);
            _ConfigAudioVsMessageRow.AudioSold = Convert.ToInt32(ddlAudioSold.SelectedValue);
            _ConfigAudioVsMessageRow.AudioIDE = Convert.ToInt32(ddlAudioIDE.SelectedValue);
            _ConfigAudioVsMessageRow.AudioNoExsits = Convert.ToInt32(ddlAudioNoExsits.SelectedValue);
            _ConfigAudioVsMessageRow.AudioCancel = Convert.ToInt32(ddlAudioCancel.SelectedValue);
            _ConfigAudioVsMessageRow.AudioPrint = Convert.ToInt32(ddlAudioPrint.SelectedValue);
            _ConfigAudioVsMessageRow.AudioActive = Convert.ToInt32(ddlAudioActive.SelectedValue);

            _ConfigAudioVsMessageRow.MessagePublic = Convert.ToInt32(ddlMessagePublic.SelectedValue);
            _ConfigAudioVsMessageRow.MessageSold = Convert.ToInt32(ddlMessageSold.SelectedValue);
            _ConfigAudioVsMessageRow.MessageIDE = Convert.ToInt32(ddlMessageIDE.SelectedValue);
            _ConfigAudioVsMessageRow.MessageNoExsits = Convert.ToInt32(ddlMessageNoExsit.SelectedValue);
            _ConfigAudioVsMessageRow.MessageCancel = Convert.ToInt32(ddlMessageCancel.SelectedValue);
            _ConfigAudioVsMessageRow.MessagePrint = Convert.ToInt32(ddlMessagePrint.SelectedValue);
            _ConfigAudioVsMessageRow.MessageActive = Convert.ToInt32(ddlMessageActive.SelectedValue);

            _ConfigAudioVsMessageRow.LastEditDate = DateTime.Now;
            _ConfigAudioVsMessageRow.LastEditBy = MyUser.GetUser_ID();

            if(BusinessRulesLocator.GetConfigAudioVsMessageBO().Update(_ConfigAudioVsMessageRow));
            {
                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateInfo", ex.ToString());
        }
    }


    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLAudioAndMessgade();
    }
}