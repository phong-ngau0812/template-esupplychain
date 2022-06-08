using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Quality_Edit : System.Web.UI.Page
{
    int Quality_ID = 0;
    public string title = "Thông tin chất lượng";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!string.IsNullOrEmpty(Request["Quality_ID"]))
            int.TryParse(Request["Quality_ID"].ToString(), out Quality_ID);
        if (!IsPostBack)
        {
            FillInfoQuality();
        }
    }

    protected void FillInfoQuality()
    {
        try
        {
            if (Quality_ID != 0)
            {
                QualityRow _QualityRow = new QualityRow();
                _QualityRow = BusinessRulesLocator.GetQualityBO().GetByPrimaryKey(Quality_ID);
                if (_QualityRow != null)
                {
                    txtName.Text = _QualityRow.IsNameNull ? string.Empty : _QualityRow.Name;
                    txtFullName.Text = _QualityRow.IsOrganizationNameNull ? string.Empty : _QualityRow.OrganizationName;
                    txtNote.Text = _QualityRow.IsDescriptionNull ? string.Empty : _QualityRow.Description;
                    txtAddress.Text = _QualityRow.IsOrganizationAddressNull ? string.Empty : _QualityRow.OrganizationAddress;
                    if (!_QualityRow.IsImageNull)
                    {
                        imganh.ImageUrl = _QualityRow.Image;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
        }
    }

    protected void UpdateQuality()
    {
        try
        {
            QualityRow _QualityRow = new QualityRow();
            if (Quality_ID != 0)
            {
                _QualityRow = BusinessRulesLocator.GetQualityBO().GetByPrimaryKey(Quality_ID);
                if (_QualityRow != null)
                {
                    _QualityRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                    _QualityRow.OrganizationName = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;
                    _QualityRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
                    _QualityRow.OrganizationAddress = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
                    _QualityRow.LastEditBy = MyUser.GetUser_ID();
                    _QualityRow.LastEditDate = DateTime.Now;
                    string fileimage = "";
                    if (fulAnh.HasFile)
                    {
                        //Xóa file
                        if (!_QualityRow.IsImageNull)
                        {
                            if (_QualityRow.Image != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _QualityRow.Image.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = "../../data/quality/" + Common.CreateImgName(fulAnh.FileName);
                        fulAnh.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _QualityRow.Image = fileimage;
                        }
                    }
                    BusinessRulesLocator.GetQualityBO().Update(_QualityRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoQuality();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateQuality", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateQuality();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quality_List.aspx", false);
    }
}