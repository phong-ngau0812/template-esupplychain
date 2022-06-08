using DbObj;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using SystemFrameWork;

/// <summary>
/// Summary description for GetLocation
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class GetLocation : System.Web.Services.WebService
{

    public GetLocation()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string GetAddressLocation(string Latitude, string Longitude)
    {
        string Address = "";
        string ResponseString = "";
        string URL = "https://maps.googleapis.com/maps/api/geocode/json";
        string Key = "AIzaSyBrEsVnV1XM8_Q0PX7PhJF67Lr2gSnmt_0";

        URL += "?latlng=" + Latitude + "," + Longitude + "&key=" + Key;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write("");
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            ResponseString = streamReader.ReadToEnd();
        }

        var a = (JObject)JsonConvert.DeserializeObject(ResponseString);
        Address = a["results"][0]["formatted_address"].ToString();

        return Address;
    }
    [WebMethod]
    public string UpdateLocation(string Address, int QRCodeTrackingID)
    {
        string i = "0";
        try
        {
            if (QRCodeTrackingID > 0)
            {

                QRCodeTrackingRow _QRCodeTrackingRow = BusinessRulesLocator.GetQRCodeTrackingBO().GetByPrimaryKey(QRCodeTrackingID);
                if (_QRCodeTrackingRow != null)
                {
                    _QRCodeTrackingRow.Location = Address;
                    if (BusinessRulesLocator.GetQRCodeTrackingBO().Update(_QRCodeTrackingRow))
                    {
                        i = "1";
                    }

                }
            }
        }
        catch (Exception ex)
        {
            i = "0";
            Log.writeLog("UpdateLocation", ex.ToString());
        }
        return i;
    }

    [WebMethod]
    public string Timekeeping1(string Address,string QRCode)
    {
        string i = "0";
        try
        {
            if (QRCode == "KHCNBT")
            {
                TimeKeepingRow _TimeKeepingRow = new TimeKeepingRow();
                _TimeKeepingRow.User_ID = MyUser.GetUser_ID();
                _TimeKeepingRow.DateCheckIn = DateTime.Now;
                _TimeKeepingRow.LocationCheckIn = Address;
                _TimeKeepingRow.IP = GetIp();
                _TimeKeepingRow.Status = true;
                BusinessRulesLocator.GetTimeKeepingBO().Insert(_TimeKeepingRow);
                if (!_TimeKeepingRow.IsTimeKeeping_IDNull)
                {
                    i = "1";
                }
            }
        }
        catch (Exception ex)
        {
            i = "0";
            Log.writeLog("UpdateLocation", ex.ToString());
        }
        return i;
    }
    public string GetIp()
    {
        string ip =
        System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }
}
