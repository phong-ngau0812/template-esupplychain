using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing;


public partial class UserControl_UploadFile : System.Web.UI.UserControl
{
    //Has the user browsed for a file?
    private bool pGotFile;
    //The file extension of the Posted File
    private string pFileExt;
    //The File itself
    private HttpPostedFile pFilePost;
    //Is the user required to upload an image?
    private bool pRequired;
    //The validation group that the Custom Validator belongs to on the page
    private string pVgroup;
    //Extensions you allow to be uploaded
    private string[] pFileTypeRange;
    //Boolean indicator to check if file extension is allowed
    private bool Ind = false;


    /*
     * Get and Sets for the above private variables.
     * */
    public bool GotFile
    {
        get { return pGotFile; }
    }
    public string FileExt
    {
        get { return pFileExt; }
    }
    public HttpPostedFile FilePost
    {
        get { return pFilePost; }
    }
    public bool Required
    {
        set { pRequired = value; }
    }
    public string Vgroup
    {
        set { pVgroup = value; }
    }
    public string FileTypeRange
    {
        set { pFileTypeRange = value.ToString().Split(','); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //here we assign the validation group to the Custom Validator, which I have inefficiently named ErrorMsg
        ErrorMsg.ValidationGroup = pVgroup;
    }

    protected void ErrorMsg_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (FilUpl.HasFile)
            {
                pGotFile = true;
                pFileExt = GetExtension(FilUpl.PostedFile.FileName);
                pFilePost = FilUpl.PostedFile;

                foreach (string str in pFileTypeRange)
                {
                    if (str.Equals(pFileExt))
                    {
                        Ind = true;
                        break;
                    }
                }

                if (!Ind)
                {
                    args.IsValid = false;
                    ErrorMsg.Text = "Type file is not supported!";
                    //Stop the function
                    return;
                }

                Int64 size = FilUpl.PostedFile.ContentLength;
                if (size / 1024 > 50000)
                {
                    args.IsValid = false;
                    ErrorMsg.Text = "Size file must less 45 MB!";
                    //Stop the function
                    return;
                }
            }
        }
        catch (Exception e)
        {
            e.Message.ToString();
        }
    }

    /// <summary>
    /// This returns the file extension.  It does not contain the preceding full stop
    /// so it would return jpg and NOT .jpg . Please adjust your coding accordingly.
    /// </summary>
    /// <param name="FileName">string</param>
    /// <returns>string: the file extension e.g. jpg</returns>
    private string GetExtension(string FileName)
    {
        string[] split = FileName.Split('.');
        string Extension = split[split.Length - 1];
        return Extension;
    }
}
