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
/// Summary description for Product
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Product : System.Web.Services.WebService
{

    public Product()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string GetInfoProduct(int Product_ID)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"   select P.Name,P.Image, PC.Name as ProductCategoryName,Q.Name as TieuChuan,P.Content, P.GrowthByDay  from Product P
  left join ProductCategory PC on PC.ProductCategory_ID= P.ProductCategory_ID
  left join Quality Q on Q.Quality_ID= P.Quality_ID
   where P.Product_ID=" + Product_ID);
        if (dt.Rows.Count > 0)
        {
            result = JsonConvert.SerializeObject(dt);
        }
        return result;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string GetInfoManu(int ManufactureTechVsTask_ID)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"Select ManufactureTechVsTask_ID,ManufactureTech_ID,Name,Description,Active,Sort,Hour,Minute from ManufactureTechVsTask where Active = 1 and ManufactureTechVsTask_ID = " + ManufactureTechVsTask_ID);
        if (dt.Rows.Count > 0)
        {
            result = JsonConvert.SerializeObject(dt);
        }
        return result;
    }
    [WebMethod]
    public decimal PushCM(string productinfo_id, string name, string description, string star, int type)
    {
        decimal i = 0;
        try
        {
            ProductReviewRow _tblCommentRow = new ProductReviewRow();
            _tblCommentRow.ProductInfo_ID = Convert.ToInt32(productinfo_id);
            _tblCommentRow.FullName = name;
            _tblCommentRow.Title = "";
            _tblCommentRow.Description = description;
            _tblCommentRow.CreateDate = DateTime.Now;
            _tblCommentRow.Star = Convert.ToInt32(star);
            _tblCommentRow.Status = 1;
            _tblCommentRow.Approved = 0;
            _tblCommentRow.Type = type;

            BusinessRulesLocator.GetProductReviewBO().Insert(_tblCommentRow);
            if (!_tblCommentRow.IsProductReview_IDNull)
            {
                i = _tblCommentRow.ProductReview_ID;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("PushCM", ex.ToString());
        }
        return i;
    }

}
