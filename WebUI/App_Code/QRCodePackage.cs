using DbObj;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SystemFrameWork;

/// <summary>
/// Summary description for QRCodePackage
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class QRCodePackage : System.Web.Services.WebService
{

    public QRCodePackage()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string QRCodeCountByRange(int QRCodePackage_ID, string SerialNumberStart, string SerialNumberEnd)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().QRCodeCountByRange(QRCodePackage_ID, SerialNumberStart, SerialNumberEnd);
        if (dt.Rows.Count > 0)
        {
            result = JsonConvert.SerializeObject(dt);
        }
        return result;
    }
    [WebMethod]
    public string SaveBH(string name, string phone, string cmnd, string address, string bh, string storename, string storeaddress, string key)
    {
        string i = "";
        try
        {
            if (!string.IsNullOrEmpty(key))
            {
                QRCodeSecretRow _QRCodeSecretRow = new QRCodeSecretRow();
                _QRCodeSecretRow = BusinessRulesLocator.GetQRCodeSecretBO().GetByPrimaryKey(key);
                _QRCodeSecretRow.UsedName = name;
                _QRCodeSecretRow.UsedPhone = phone;
                _QRCodeSecretRow.UsedIdentityCard = cmnd;
                _QRCodeSecretRow.UsedAddress = address;
                _QRCodeSecretRow.WarrantySerial = bh;
                _QRCodeSecretRow.StoreName = storename;
                _QRCodeSecretRow.StoreAddress = storeaddress;
                _QRCodeSecretRow.WarrantyMonth = 12;

                BusinessRulesLocator.GetQRCodeSecretBO().Update(_QRCodeSecretRow);
                if (!_QRCodeSecretRow.IsQRCodeSecretContentNull)
                {
                    i = _QRCodeSecretRow.QRCodeSecretContent;
                }

            }
        }
        catch (Exception ex)
        {
            Log.writeLog("SaveBH", ex.ToString());
        }
        return i;
    }
}
