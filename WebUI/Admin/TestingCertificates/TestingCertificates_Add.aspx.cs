using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;
using Telerik.Web.UI;

public partial class TestingCertificates_Add : System.Web.UI.Page
{
    public string title = "Thêm mới phiếu kiểm nghiệm ";
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
            
        }
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand_Null(ddlProductBrand, "");
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddTestingCertificates();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected void UpdateTestingCertificates(int TestingCertificates_ID)
    {
        TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();
        _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);
        string fileimage = "";
        if (_TestingCertificatesRow != null)
        {
            //_TestingCertificatesRow.GSRN = "GSRN-" + _TestingCertificatesRow.Customer_ID.ToString();

            if (UpFile.HasFile)
            {
                ////Xóa file
                //if (UserProfile.AvatarUrl != null)
                //{
                //    string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + UserProfile.AvatarUrl.Replace("../", "");
                //    if (File.Exists(strFileFullPath))
                //    {
                //        File.Delete(strFileFullPath);
                //    }
                //}
                fileimage = "../../data/TestingCertificates/" + TestingCertificates_ID + "-" + (UpFile.FileName);
                UpFile.SaveAs(Server.MapPath(fileimage));
                if (!string.IsNullOrEmpty(fileimage))
                {
                    _TestingCertificatesRow.UploadFile = fileimage;
                }
                BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
           
        }

    }


    protected void AddTestingCertificates()
    {
        try
        {
           
            TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();
            _TestingCertificatesRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
            _TestingCertificatesRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _TestingCertificatesRow.StartDate = FromDate;
            }
            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _TestingCertificatesRow.EndDate = ToDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            _TestingCertificatesRow.Description = string.IsNullOrEmpty(txtNote.Text) ? string.Empty : txtNote.Text;

            _TestingCertificatesRow.LastEditBy= _TestingCertificatesRow.CreateBy = MyUser.GetUser_ID();
            _TestingCertificatesRow.LastEditDate= _TestingCertificatesRow.CreateDate = DateTime.Now;

            
            if (ckActive.Checked)
            {
                _TestingCertificatesRow.Active = 1;
            }
            else
            {
                _TestingCertificatesRow.Active = 0;
            }

            _TestingCertificatesRow.Sort = Common.GenarateSort("TestingCertificates");

            BusinessRulesLocator.GetTestingCertificatesBO().Insert(_TestingCertificatesRow);
            if (_TestingCertificatesRow != null)
            {
                UpdateTestingCertificates(_TestingCertificatesRow.TestingCertificates_ID);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateTestingCertificates", ex.ToString());
        }
    }
    protected void ClearForm()
    {
      
        txtName.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";

        //ddlProductBrand.Items.Clear();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestingCertificates_List.aspx", false);
    }

}