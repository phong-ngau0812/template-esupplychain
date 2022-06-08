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
using SystemFrameWork;

public partial class ProductBrandType_Edit : System.Web.UI.Page
{
    int ProductBrandType_ID = 0;
    public string title = "Thông tin loại hình doanh nghiệp";
    public string avatar = "";
    public string googlemap = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        

        if (!string.IsNullOrEmpty(Request["ProductBrandType_ID"]))
            int.TryParse(Request["ProductBrandType_ID"].ToString(), out ProductBrandType_ID);
        if (!IsPostBack)
        {

            FillInfoProductBrandType();
            

        }
    }

   
    protected void FillInfoProductBrandType()
    {
        try
        {
            if (ProductBrandType_ID != 0)
            {
                ProductBrandTypeRow _ProductBrandTypeRow = new ProductBrandTypeRow();
                _ProductBrandTypeRow = BusinessRulesLocator.GetProductBrandTypeBO().GetByPrimaryKey(ProductBrandType_ID);

                if (_ProductBrandTypeRow != null)
                {
                    txtName.Text = _ProductBrandTypeRow.IsNameNull ? string.Empty : _ProductBrandTypeRow.Name;
                    txtOrder.Text = _ProductBrandTypeRow.IsSortNull ? string.Empty : _ProductBrandTypeRow.Sort.ToString();

                    if (_ProductBrandTypeRow.Active == 1)
                    {
                        ckActive.Checked = true;
                    }
                    else
                    {
                        ckActive.Checked = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillInfoArea", ex.ToString());
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


    
    protected void UpdateArea()
    {
        try
        {
            ProductBrandTypeRow _ProductBrandTypeRow = new ProductBrandTypeRow();
            if (ProductBrandType_ID != 0)
            {
                _ProductBrandTypeRow = BusinessRulesLocator.GetProductBrandTypeBO().GetByPrimaryKey(ProductBrandType_ID);
                if (_ProductBrandTypeRow != null)
                {

                    if (!CheckProductBrandType(txtName.Text.Trim()))
                    {
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

                        BusinessRulesLocator.GetProductBrandTypeBO().Update(_ProductBrandTypeRow);
                        lblMessage.Text = "Cập nhật thông tin thành công!";
                        lblMessage.Visible = true;
                        FillInfoProductBrandType();
                    }
                    else
                    {
                        lblMessage.Text = "Loại hình doanh nghiệp" + txtName.Text + "đã tồn tại!";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.BackColor = Color.Wheat;
                        lblMessage.Visible = true;
                    }


                }


            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateZone_ID", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateArea();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductBrandType_List.aspx", false);
    }


}