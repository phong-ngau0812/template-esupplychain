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
using System.Xml.Linq;
using SystemFrameWork;

public partial class ProductBrandType_Add : System.Web.UI.Page
{
    public string title = "Thêm mới loại hình doanh nhiệp  ";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

           
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddProductBrandType();
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }

    protected bool CheckProductBrandType(string Name)
    {
        bool flag = true;
        string where = "Active <>-1";
        if (!string.IsNullOrEmpty(Name))
        {
            where += " and Name=N'" + Name.Trim() + "'";
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetProductBrandTypeBO().GetAsDataTable(where, "");
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }

        }


        return flag;
    }

    protected void AddProductBrandType()
    {
        
        try
        {
            if (CheckProductBrandType(txtName.Text.Trim()))
            {
                ProductBrandTypeRow _ProductBrandTypeRow = new ProductBrandTypeRow();
                _ProductBrandTypeRow.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
               

                if (!string.IsNullOrEmpty(txtOrder.Text))
                {
                    _ProductBrandTypeRow.Sort = Convert.ToInt32(txtOrder.Text);
                }
                else
                {
                    _ProductBrandTypeRow.Sort = Common.GenarateSort("ProductBrandType");
                }

                if (ckActive.Checked)
                {
                    _ProductBrandTypeRow.Active = 1;
                }
                else
                {
                    _ProductBrandTypeRow.Active = 0;
                }
                _ProductBrandTypeRow.CreateBy = MyUser.GetUser_ID();
                _ProductBrandTypeRow.CreateDate = DateTime.Now;
                _ProductBrandTypeRow.LastEditedDate = DateTime.Now;
                _ProductBrandTypeRow.LastEditedBy = MyUser.GetUser_ID();
                BusinessRulesLocator.GetProductBrandTypeBO().Insert(_ProductBrandTypeRow);
                lblMessage.Text = "Thêm mới thành công!";
                lblMessage.Visible = true;
                ClearForm();
            }
            else
            {
                lblMessage.Text = "Loại hình doanh nghiệp"+ txtName.Text + "đã tồn tại!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Wheat;
                lblMessage.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateArea", ex.ToString());
        }
    }

    protected void ClearForm()
    {
        txtName.Text = "";
        txtOrder.Text = "";

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductBrandType_List.aspx", false);
    }

}