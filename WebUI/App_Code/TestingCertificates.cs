using DbObj;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Product
/// </summary>
//[WebService(Namespace = "http://microsoft.com/webservices/")]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TestingCertificates : System.Web.Services.WebService
{

    public TestingCertificates()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string GetInfoTestingCertificates(int ProductPackage_ID)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);

        DataTable dts = new DataTable();
        dts.Clear();
        dts.Columns.Add("Name");
        dts.Columns.Add("StartDate");
        dts.Columns.Add("EndDate");
        dts.Columns.Add("LinkFile");

        if (_ProductPackageRow != null)
        {
            if (!_ProductPackageRow.IsTestingCertificates_IDNull)
            {
                string[] array = _ProductPackageRow.TestingCertificates_ID.Split(',');
                foreach (string value in array)
                {

                    if (value != "")
                    {
                        dt = BusinessRulesLocator.Conllection().GetAllList(@"select TC.Name, TC.StartDate ,TC.EndDate , TC.UploadFile as LinkFile
from TestingCertificates TC
where  TC.Active = 1 and TC.TestingCertificates_ID =" + value);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                DataRow _row = dts.NewRow();
                                _row["Name"] = item["Name"];
                                _row["StartDate"] = DateTime.Parse( item["StartDate"].ToString()).ToString("dd/MM/yyyy");
                                _row["EndDate"] = DateTime.Parse(item["EndDate"].ToString()).ToString("dd/MM/yyyy") ;
                                _row["LinkFile"] = item["LinkFile"];
                                dts.Rows.Add(_row);
                            }
                        }
                    }

                }
            }
            result = JsonConvert.SerializeObject(dts);
        }
        return result;
    }
}
