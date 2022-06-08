using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class ProductPackage_HTML : System.Web.UI.Page
{
    public string name, namepackage, demuc, code, lenhsx, hosx = string.Empty;
    public int Product_ID = 0;
    public int TaskStep_ID = 0;
    public int ProductPackage_ID = 0;
    int ProductPackageOrder_ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ProductPackage_ID"]))
            int.TryParse(Request["ProductPackage_ID"].ToString(), out ProductPackage_ID);

        ResetMsg();
        Init();
    }

    protected string LoadHtml()
    {
        string html = string.Empty;
        try
        {
         
            DataTable dt = BusinessRulesLocator.GetTaskTypeBO().GetAsDataTable("Status=1", "");
            foreach (DataRow item in dt.Rows)
            {
                html += "<p style='margin-top: 10px; margin-bottom: 0px;'><b>" + item["Name"] + "</b></p>";
                DataTable dtTask = BusinessRulesLocator.GetTaskBO().GetAsDataTable(" TaskType_ID=" + item["TaskType_ID"] + " and ProductPackage_ID=" + ProductPackage_ID, "");

                foreach (DataRow itemTask in dtTask.Rows)
                {
                    html += "<table style='width: 100%;'>" +
                                                  "<tbody><tr>" +
                                                      "<td class='col-left'>Đề mục công việc</td>" +
                                                      "<td><b>" + itemTask["Name"] + "</b></td>" +
                                                  "</tr>" +
                                                  "<tr>" +
                                                      "<td class='col-left'>Thời gian</td>" +
                                                      "<td>" + DateTime.Parse(itemTask["StartDate"].ToString()).ToString("dd/MM/yyyy") + "</td>" +
                                                  "</tr>" +
                                                  "<tr>" +
                                                      "<td class='col-left'>Nội dung</td>" +
                                                      "<td>" + itemTask["Description"] + "</td>" +
                                                  "</tr>" +
                                                  "<tr>" +
                                                      "<td class='col-left'>Vị trí</td>" +
                                                      "<td>" + itemTask["Location"] + "</td>" +
                                                  "</tr>" +
                                                    "<tr>" +
                                                      "<td class='col-left'>Ảnh minh họa</td>" +
                                                      "<td><img class='mainimage' src='" + (string.IsNullOrEmpty( itemTask["Image"].ToString())?"": "https://esupplychain.vn/data/task/"+ itemTask["Image"]) + "'/></td>" +
                                                  "</tr>" +
                                              "</tbody></table>" +
                                              "<div class='solline'></div>";
                }

            }
          

        }
        catch (Exception ex)
        {

            Log.writeLog(ex.ToString(), "LoadHtml");
        }
        return html;
    }
    private void Init()
    {

        if (Product_ID != 0)
        {
            name = BusinessRulesLocator.GetProductBO().GetByPrimaryKey(Product_ID).Name;
        }

        if (ProductPackage_ID != 0)
        {
            ProductPackageRow _ProductPackageRow = BusinessRulesLocator.GetProductPackageBO().GetByPrimaryKey(ProductPackage_ID);
            if (_ProductPackageRow != null)
            {
                //Kiểm tra xem đối tượng vào sửa có cùng doanh nghiệp hay không
                MyActionPermission.CheckPermission(_ProductPackageRow.ProductBrand_ID.ToString(), _ProductPackageRow.CreateBy.ToString(), "/Admin/ProductPackage/ProductPackage_List");
                namepackage = _ProductPackageRow.IsNameNull ? "" : _ProductPackageRow.Name;
                code = _ProductPackageRow.IsCodeNull ? "" : _ProductPackageRow.Code;
                clipboardTextarea.Text = LoadHtml();
                if (!IsPostBack)
                {

                }

            }
        }
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
    }

    protected void btnBackFix_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Admin/ProductPackage/ProductPackage_List.aspx?Code=" + code, false);
    }

}