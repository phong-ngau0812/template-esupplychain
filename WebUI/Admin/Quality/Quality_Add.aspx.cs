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

public partial class Quality_Add : System.Web.UI.Page
{
    int Quality_ID = 0;
    public string title = "Thêm mới chất lượng";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);
        if (!IsPostBack)
        {
        }
    }
    protected void AddQuality()
    {
        try
        {

            QualityRow _QualityRow = new QualityRow();
            _QualityRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            _QualityRow.OrganizationName = string.IsNullOrEmpty(txtFullName.Text) ? string.Empty : txtFullName.Text;
            _QualityRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;
            _QualityRow.OrganizationAddress = string.IsNullOrEmpty(txtAddress.Text) ? string.Empty : txtAddress.Text;
            _QualityRow.CreateBy = MyUser.GetUser_ID();
            _QualityRow.CreateDate = DateTime.Now;
            _QualityRow.LastEditBy = MyUser.GetUser_ID();
            _QualityRow.LastEditDate = DateTime.Now;
            string fileimage = "";
            if (fulAnh.HasFile)
            {
                fileimage = "../../data/quality/" + Common.CreateImgName(fulAnh.FileName);
                fulAnh.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _QualityRow.Image = fileimage;
                }
            }
            BusinessRulesLocator.GetQualityBO().Insert(_QualityRow);
            lblMessage.Text = "Thêm mới thành công!";
            lblMessage.Visible = true;
            ClearForm();
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateQuality", ex.ToString());
        }
    }
    protected void ClearForm()
    {
        txtAddress.Text = txtName.Text = txtFullName.Text = txtNote.Text = "";
    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                AddQuality();
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