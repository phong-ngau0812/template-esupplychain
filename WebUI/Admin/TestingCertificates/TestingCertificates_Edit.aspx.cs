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

public partial class TestingCertificates_Edit : System.Web.UI.Page
{
    int TestingCertificates_ID = 0;
    public string title = "Thông tin phiếu kiểm nghiệm";
    public string UploadFile = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "/ckfinder";
        _FileBrowser.SetupCKEditor(txtNote);

        if (!string.IsNullOrEmpty(Request["TestingCertificates_ID"]))
            int.TryParse(Request["TestingCertificates_ID"].ToString(), out TestingCertificates_ID);
        if (!IsPostBack)
        {
            
            FillDDLddlProductBrand();
            FillInfoTestingCertificates();
        }
    }


   
    protected void FillInfoTestingCertificates()
    {
        try
        {
            if (TestingCertificates_ID != 0)
            {
                TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();
                _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);

                if (_TestingCertificatesRow != null)
                {

                    
                    ddlProductBrand.SelectedValue = _TestingCertificatesRow.ProductBrand_ID.ToString();
                    txtName.Text = _TestingCertificatesRow.IsNameNull ? string.Empty : _TestingCertificatesRow.Name;
                    txtFromDate.Text = _TestingCertificatesRow.StartDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = _TestingCertificatesRow.EndDate.ToString("dd/MM/yyyy");
                    if (!_TestingCertificatesRow.IsUploadFileNull)
                    {
                        UploadFile = _TestingCertificatesRow.UploadFile;
                        //if (UploadFile != null)
                        //{
                        //    UploadFile = UploadFile;
                        //}
                        //else
                        //{
                        //    UploadFile = "";
                        //}
                    }

                   
                    txtNote.Text = _TestingCertificatesRow.IsDescriptionNull ? string.Empty : _TestingCertificatesRow.Description;

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoUser", ex.ToString());
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


    protected void UpdateTestingCertificates()
    {
        try
        {
            TestingCertificatesRow _TestingCertificatesRow = new TestingCertificatesRow();
            if (TestingCertificates_ID != 0)
            {
                _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);
                if (_TestingCertificatesRow != null)
                {

                    //_CustomerRow.ProductBrandList_ID =  GetProductBrandList_ID();
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
                    
                    string fileimage = "";
                    if (UpFile.HasFile)
                    {
                        //Xóa file
                        if (!_TestingCertificatesRow.IsUploadFileNull)
                        {
                            if (_TestingCertificatesRow.UploadFile != null)
                            {
                                string strFileFullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + _TestingCertificatesRow.UploadFile.Replace("../", "");
                                if (File.Exists(strFileFullPath))
                                {
                                    File.Delete(strFileFullPath);
                                }
                            }
                        }
                        fileimage = "../../data/customer/" + TestingCertificates_ID + "-"+(UpFile.FileName);
                        UpFile.SaveAs(Server.MapPath(fileimage));
                        if (!string.IsNullOrEmpty(fileimage))
                        {
                            _TestingCertificatesRow.UploadFile = fileimage;
                        }
                    }

                    _TestingCertificatesRow.LastEditBy = MyUser.GetUser_ID();
                    _TestingCertificatesRow.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetTestingCertificatesBO().Update(_TestingCertificatesRow);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoTestingCertificates();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateCustomer", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateTestingCertificates();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestingCertificates_List.aspx", false);
    }

}