using DbObj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Audio_Audio_Edit : System.Web.UI.Page
{
    int Audio_ID = 0;
    public string title = "Thông tin âm thanh";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["Audio_ID"]))
            int.TryParse(Request["Audio_ID"].ToString(), out Audio_ID);
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
                AudioRow _AudioRow = new AudioRow();
                _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);
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
            if (Audio_ID != 0)
            {
                _AudioRow = BusinessRulesLocator.GetAudioBO().GetByPrimaryKey(Audio_ID);
                if (_AudioRow != null)
                {
                    _AudioRow.Title = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _AudioRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_AudioRow.IsNoteNull)
                        {
                            if (_AudioRow.Note != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _AudioRow.Note.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }

                        fileimage = "/data/audio/" + Audio_ID + "-" + Common.RemoveFont(fulAnh.FileName);
                        fulAnh.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _AudioRow.Note = Audio_ID + "-" + Common.RemoveFont(fulAnh.FileName);
                        }
                    }


                    if (ckActive.Checked)
                    {
                        _AudioRow.Status = 1;
                    }
                    else
                    {
                        _AudioRow.Status = 0;
                    }
                    _AudioRow.LastEditBy = MyUser.GetUser_ID();
                    _AudioRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetAudioBO().Update(_AudioRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoAudio();
                Response.Redirect("Audio_List.aspx", false);
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
        Response.Redirect("Audio_List.aspx", false);
    }
}