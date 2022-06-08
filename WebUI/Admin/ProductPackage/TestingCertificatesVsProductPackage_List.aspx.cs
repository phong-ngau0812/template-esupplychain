using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class TestingCertificatesVsProductPackage_List : System.Web.UI.Page
{
    string Gerder;
    public string namepackage, code = string.Empty;
    public string Message = "";
    public int ProductPackage_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);

        if (!IsPostBack)
        {

            FillDDLddlProductBrand();
            LoadInforProductPackage_ID(ProductPackage_ID);
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

    private void FillDDLddlProductBrand()
    {
        try
        {
            Common.FillProductBrand(ddlProductBrand, "");
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

    protected void LoadInforProductPackage_ID(int ProductPackage_ID)
    {

        DataTable dt = new DataTable();
        ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
        if (_ProductPackageRow != null)
        {
            ddlProductBrand.SelectedValue = _ProductPackageRow.IsProductBrand_IDNull ? "" : _ProductPackageRow.ProductBrand_ID.ToString();

            namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
            code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;

            if (!_ProductPackageRow.IsTestingCertificates_IDNull)
            {
                DataTable dts = new DataTable();
                dts.Clear();
                dts.Columns.Add("TestingCertificates_ID");
                dts.Columns.Add("Name");
                dts.Columns.Add("StartDate");
                dts.Columns.Add("EndDate");
                dts.Columns.Add("UploadFile");
                string[] array = _ProductPackageRow.TestingCertificates_ID.Split(',');
                foreach (string value in array)
                {
                    if (value != "")
                    {
                        // loadTestingCertificates(Convert.ToInt32(value));
                        int TestingCertificates_ID = Convert.ToInt32(value);
                        TestingCertificatesRow _TestingCertificatesRow = BusinessRulesLocator.GetTestingCertificatesBO().GetByPrimaryKey(TestingCertificates_ID);
                       
                        DataRow _row = dts.NewRow();
                        _row["TestingCertificates_ID"] = Convert.ToInt32(value);
                        _row["Name"] = _TestingCertificatesRow.Name;
                        _row["StartDate"] = _TestingCertificatesRow.StartDate;
                        _row["EndDate"] = _TestingCertificatesRow.EndDate;
                        _row["UploadFile"] = _TestingCertificatesRow.UploadFile;
                        dts.Rows.Add(_row);

                    }
                }
                //Response.Write(dt.Rows.Count);
                if (dts.Rows.Count > 0)
                {
                    rptTestingCertificates.DataSource = dts;
                    rptTestingCertificates.DataBind();
                   
                }

        }
    }

}
protected void loadTestingCertificates(int TestingCertificates_ID)
{
    if (TestingCertificates_ID != 0)
    {
        string where = "";
        if (ddlProductBrand.SelectedValue != "0")
        {
            where += " and PB.ProductBrand_ID =" + ddlProductBrand.SelectedValue;
        }
        where += "and TC.TestingCertificates_ID =" + TestingCertificates_ID;
        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.Conllection().GetAllList(@"  select TC.*
from TestingCertificates TC
left join ProductBrand PB on TC.ProductBrand_ID = PB.ProductBrand_ID
where PB.Active = 1 and TC.Active <>-1" + where);
            rptTestingCertificates.DataSource = dt;
            rptTestingCertificates.DataBind();

        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }
}
      
}