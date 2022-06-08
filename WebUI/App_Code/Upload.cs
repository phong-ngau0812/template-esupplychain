// According to http://msdn2.microsoft.com/en-us/library/system.web.httppostedfile.aspx
// "Files are uploaded in MIME multipart/form-data format. 
// By default, all requests, including form fields and uploaded files, 
// larger than 256 KB are buffered to disk, rather than held in server memory."
// So we can use an HttpHandler to handle uploaded files and not have to worry
// about the server recycling the request do to low memory. 
// don't forget to increase the MaxRequestLength in the web.config.
// If you server is still giving errors, then something else is wrong.
// I've uploaded a 1.3 gig file without any problems. One thing to note, 
// when the SaveAs function is called, it takes time for the server to 
// save the file. The larger the file, the longer it takes.
// So if a progress bar is used in the upload, it may read 100%, but the upload won't
// be complete until the file is saved.  So it may look like it is stalled, but it
// is not.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.IO;
using SystemFrameWork;
using DbObj;

/// <summary>
/// Upload handler for uploading files.
/// </summary>
public class Upload : IHttpHandler, IRequiresSessionState
{
    int AttID = 0;
    int HotelID = 0;
    int TourID = 0;
    int TestimonialID = 0;
    int ExclusiveID = 0;


    public Upload()
    {
    }

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        //Example of using a passed in value in the query string to set a categoryId
        //Now you can do anything you need to witht the file.
        //Get parameters
       
        if (!string.IsNullOrEmpty(context.Request.QueryString["AttID"]))
        {
            int.TryParse(context.Request.QueryString["AttID"], out AttID);
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["HotelID"]))
        {
            int.TryParse(context.Request.QueryString["HotelID"], out HotelID);
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["TourID"]))
        {
            int.TryParse(context.Request.QueryString["TourID"], out TourID);
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["TestimonialID"]))
        {
            int.TryParse(context.Request.QueryString["TestimonialID"], out TestimonialID);
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["ExclusiveID"]))
        {
            int.TryParse(context.Request.QueryString["ExclusiveID"], out ExclusiveID);
        }

        //if (context.Request.Files.Count > 0)
        //{
        //    // get the applications path
        //    string strFileImage = string.Empty;
        //    string strPathImage = string.Empty;

        //    strPathImage = GetPhysicalPath();

        //    // loop through all the uploaded files
        //    for (int j = 0; j < context.Request.Files.Count; j++)
        //    {
        //        // get the current file
        //        HttpPostedFile uploadFile = context.Request.Files[j];
        //        // if there was a file uploded
        //        if (uploadFile.ContentLength > 0)
        //        {
        //            // use this if using flash to upload
        //            string strTemp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute + DateTime.Now.Second;
        //            uploadFile.SaveAs(Path.Combine(strPathImage, strTemp + "-" + Common.RemoveSignature(uploadFile.FileName)));
        //            //
        //            try
        //            {
        //                ////Insert vao database.
        //                strFileImage = strTemp + "-" + Common.RemoveSignature(uploadFile.FileName);
        //                if (AttID != 0)
        //                {
        //                    tblAttractionsGalleryRow _tblAttractionsGalleryRow = GetInfoAttractionsGalleryRow(strFileImage);
        //                    BusinessRulesLocator.GettblAttractionsGalleryBO().Insert(_tblAttractionsGalleryRow);
        //                }
        //                else if (TourID != 0)
        //                {
        //                    tblTourGalleryRow _tblTourGalleryRow = GetInfoTourGalleryRow(strFileImage);
        //                    BusinessRulesLocator.GettblTourGalleryBO().Insert(_tblTourGalleryRow);
        //                }
        //                else if (TestimonialID != 0)
        //                {
        //                    tblTestimonialGalleryRow _tblTestimonialGalleryRow = GetInfoTestimonialGalleryRow(strFileImage);
        //                    BusinessRulesLocator.GettblTestimonialGalleryBO().Insert(_tblTestimonialGalleryRow);
        //                }
        //                else if (ExclusiveID != 0)
        //                {
        //                    tblProductVsGalleryRow _tblExclusiveGalleryRow = GetInfoExclusiveGalleryRow(strFileImage);
        //                    BusinessRulesLocator.GettblProductVsGalleryBO().Insert(_tblExclusiveGalleryRow);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.writeLog(ex.ToString(), "ProcessRequest");
        //            }
        //        }
        //    }
        //}
        HttpContext.Current.Response.Write(" ");
    }

    //protected tblAttractionsGalleryRow GetInfoAttractionsGalleryRow(string objFile)
    //{
    //    tblAttractionsGalleryRow _tblAttractionsGalleryRow = new tblAttractionsGalleryRow();
    //    _tblAttractionsGalleryRow.AttractionsID = AttID;
    //    _tblAttractionsGalleryRow.ImageFile = objFile;
    //    _tblAttractionsGalleryRow.Status = 0;
    //    _tblAttractionsGalleryRow.IsList = 0;
    //    _tblAttractionsGalleryRow.IsDetail = 0;
    //    _tblAttractionsGalleryRow.IsBanner = 0;
    //    _tblAttractionsGalleryRow.IsMap = 0;
    //    _tblAttractionsGalleryRow.ImageTitle = "";
    //    _tblAttractionsGalleryRow.ImageAlt = "";
    //    return _tblAttractionsGalleryRow;
    //}

    //protected tblTourGalleryRow GetInfoTourGalleryRow(string objFile)
    //{
    //    tblTourGalleryRow _tblTourGalleryRow = new tblTourGalleryRow();
    //    _tblTourGalleryRow.TourID = TourID;
    //    _tblTourGalleryRow.ImageFile = objFile;
    //    _tblTourGalleryRow.ImageTitle = "";
    //    _tblTourGalleryRow.ImageAlt = "";
    //    _tblTourGalleryRow.IsPrimary = 0;
    //    _tblTourGalleryRow.IsMap = 0;
    //    _tblTourGalleryRow.IsBanner = 0;
    //    _tblTourGalleryRow.IsPrograme = 0;
    //    _tblTourGalleryRow.IsGallery = 0;
    //    _tblTourGalleryRow.DayNumber = 0;
    //    _tblTourGalleryRow.Status = 0;
    //    return _tblTourGalleryRow;
    //}

    //protected tblTestimonialGalleryRow GetInfoTestimonialGalleryRow(string objFile)
    //{
    //    tblTestimonialGalleryRow _tblTestimonialGalleryRow = new tblTestimonialGalleryRow();
    //    _tblTestimonialGalleryRow.TestimonialID = TestimonialID;
    //    _tblTestimonialGalleryRow.ImageFile = objFile;
    //    _tblTestimonialGalleryRow.ImageTitle = "";
    //    _tblTestimonialGalleryRow.ImageAlt = "";
    //    _tblTestimonialGalleryRow.IsList = 0;
    //    _tblTestimonialGalleryRow.IsBanner = 0;
    //    _tblTestimonialGalleryRow.Status = 0;
    //    return _tblTestimonialGalleryRow;
    //}

    //protected tblProductVsGalleryRow GetInfoExclusiveGalleryRow(string objFile)
    //{
    //    tblProductVsGalleryRow _tblExclusiveGalleryRow = new tblProductVsGalleryRow();
    //    _tblExclusiveGalleryRow.ProductID = ExclusiveID;
    //    _tblExclusiveGalleryRow.ImageFile = objFile;
    //    _tblExclusiveGalleryRow.ImageTitle = "";
    //    _tblExclusiveGalleryRow.ImageAlt = "";
    //    _tblExclusiveGalleryRow.IsList = 0;
    //    _tblExclusiveGalleryRow.IsMap = 0;
    //    _tblExclusiveGalleryRow.IsDetail = 0;
    //    _tblExclusiveGalleryRow.IsBanner = 0;
    //    _tblExclusiveGalleryRow.Status = 0;
    //    return _tblExclusiveGalleryRow;
    //}
    //
    //directory is Album
    protected string GetPhysicalPath()
    {
        string PhysicalPath = string.Empty;
        if (AttID != 0)
            PhysicalPath = CommonBO.GetAttractionsFolderPhysicalPath.Replace("/", "\\");
        else if (HotelID != 0)
            PhysicalPath = CommonBO.GetHotelFolderPhysicalPath.Replace("/", "\\");
        else if (TourID != 0)
            PhysicalPath = CommonBO.GetTourFolderPhysicalPath.Replace("/", "\\");
        else if (TestimonialID != 0)
            PhysicalPath = CommonBO.GetTestimonialFolderPhysicalPath.Replace("/", "\\");
        else if (ExclusiveID != 0)
            PhysicalPath = CommonBO.GetExclusiveFolderPhysicalPath.Replace("/", "\\");

        if (!PhysicalPath.EndsWith("\\"))
            PhysicalPath += "\\";

        return PhysicalPath;
    }
}
