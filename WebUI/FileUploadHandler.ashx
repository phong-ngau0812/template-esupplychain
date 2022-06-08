<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using DbObj;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string ProductID = (string)context.Request["ProductID"];
        string Type = (string)context.Request["Type"];
        if (!string.IsNullOrEmpty("ProductID"))
        {
            string time = DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
            HttpPostedFile file = context.Request.Files[0];
            string fname = context.Server.MapPath("data/product/product_info/" + ProductID.ToString() + "-" + time + "-" + file.FileName);
            file.SaveAs(fname);
            ProductGalleryRow _ProductPictureRow = new ProductGalleryRow();
            _ProductPictureRow.Product_ID = Convert.ToInt32(ProductID);
            _ProductPictureRow.Image = ProductID.ToString() + "-" + time + "-" + file.FileName;
            _ProductPictureRow.Title = "";
            _ProductPictureRow.Alt = "";
            _ProductPictureRow.Type = Convert.ToInt32(Type);
            _ProductPictureRow.Sort = 0;
            BusinessRulesLocator.GetProductGalleryBO().Insert(_ProductPictureRow);
            context.Response.ContentType = "text/plain";
            context.Response.Write("Tải file thành công!");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}