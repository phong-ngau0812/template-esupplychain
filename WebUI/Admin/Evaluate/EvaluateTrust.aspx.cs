using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class Admin_Evaluate_EvaluateTrust : System.Web.UI.Page
{
    public string title = "Quản lý đánh giá chỉ số tin cậy";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDLddlProductBrand();
        }
    }



    private void FillDDLddlProductBrand()
    {
        try
        {
            string where = "";
            if (MyUser.GetFunctionGroup_ID() == "8")
            {
                if (MyUser.GetRank_ID() == "2")
                {
                    where += " and Location_ID =" + MyUser.GetLocation_ID();
                }
                if (MyUser.GetRank_ID() == "3")
                {
                    where += " and District_ID =" + MyUser.GetDistrict_ID();
                }
                if (MyUser.GetRank_ID() == "4")
                {
                    where += " and Ward_ID =" + MyUser.GetWard_ID();
                }
            }


            Common.FillProductBrand_Null(ddlProductBrand, where);
            if (Common.GetFunctionGroupDN())
            {
                ddlProductBrand.SelectedValue = MyUser.GetProductBrand_ID();
                ddlProductBrand.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("FillProductBrand", ex.ToString());
        }
    }


    protected void AddEvaluate()
    {
        DataTable dt = BusinessRulesLocator.GetEvaluateBO().GetAsDataTable(" ProductBrand_ID=" + ddlProductBrand.SelectedValue, "");

        try
        {
            EvaluateRow _EvaluateRow = new EvaluateRow();
            if (_EvaluateRow!= null)
            {
                if (ddlProductBrand.SelectedValue != "")
                {
                    _EvaluateRow.ProductBrand_ID = Convert.ToInt32(ddlProductBrand.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtSoSP.Text))
                {
                    _EvaluateRow.Product_Number = Convert.ToInt32(txtSoSP.Text);
                }
                if (!string.IsNullOrEmpty(txtTemcongkhai.Text))
                {
                    _EvaluateRow.Stamp_Number = Convert.ToInt32(txtTemcongkhai.Text);
                }
                if (!string.IsNullOrEmpty(txtTembimat.Text))
                {
                    _EvaluateRow.Stamp_Number_Secret = Convert.ToInt32(txtTembimat.Text);
                }
                if (ddlChiso.SelectedValue != "")
                {
                    _EvaluateRow.Star = Convert.ToInt32(ddlChiso.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtNote.Text))
                {
                    _EvaluateRow.Note = txtNote.Text;
                }
                _EvaluateRow.Active = 0;
                _EvaluateRow.LastEditDate = _EvaluateRow.CreateDate = DateTime.Now;
                _EvaluateRow.LastEditBy = _EvaluateRow.CreateBy = MyUser.GetUser_ID();
                BusinessRulesLocator.GetEvaluateBO().Insert(_EvaluateRow);

                //Thông báo 
                NotificationRow _NotificationRow = new NotificationRow();
                _NotificationRow.Name = "Cơ quan quản lý đã đánh giá chỉ số tin cậy cho doanh nghiệp";
                _NotificationRow.Summary = _EvaluateRow.Note;
                _NotificationRow.Body = _EvaluateRow.Evaluate_ID.ToString();
                _NotificationRow.NotificationType_ID = 2;
                _NotificationRow.UserID = _EvaluateRow.LastEditBy;
                if (MyUser.GetFunctionGroup_ID() != "1")
                    _NotificationRow.ProductBrand_ID = _EvaluateRow.ProductBrand_ID;
                _NotificationRow.CreateBy = MyUser.GetUser_ID();
                _NotificationRow.CreateDate = DateTime.Now;
                _NotificationRow.Active = 1;
                _NotificationRow.Alias = Guid.NewGuid();
                BusinessRulesLocator.GetNotificationBO().Insert(_NotificationRow);

            }



            lblMessage.Text = "Đánh giá và gửi phản hồi thành công!";
            lblMessage.Visible = true;
            ClearForm();

        }
        catch (Exception ex)
        {
            Log.writeLog("AddEvaluate", ex.ToString());
        }
    }

    protected void ClearForm()
    {
        ddlProductBrand.SelectedIndex = 0;
        txtSoSP.Text = txtTemcongkhai.Text = txtTembimat.Text = "";
        txtNote.Text = "";
        ddlChiso.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

            }
            AddEvaluate();

        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workshop_List.aspx", false);
    }
    protected void btnXemTT_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ProductBrand/ProductBrand_Edit?ProductBrand_ID=" + ddlProductBrand.SelectedValue, false);
    }
    protected void ddlProductBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SoSP = 0;
        int SoTemCongKhai = 0;
        int SoTemBiMat = 0;

        if (ddlProductBrand.SelectedValue != "")
        {
            DataTable dtProduct = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROM Product where ProductBrand_ID=" + ddlProductBrand.SelectedValue);
            DataTable dtTemCongKhai = BusinessRulesLocator.GetQRCodePackageBO().GetAsDataTable(" QRCodePackageType_ID = 1 and ProductBrand_ID=" + ddlProductBrand.SelectedValue, "");
            DataTable dtTemBiMat = BusinessRulesLocator.GetQRCodePackageBO().GetAsDataTable(" QRCodePackageType_ID = 2 and ProductBrand_ID=" + ddlProductBrand.SelectedValue, "");
            foreach (DataRow dt in dtTemCongKhai.Rows)
            {
                SoTemCongKhai += Convert.ToInt32(dt["QRCodeNumber"]);
            }
            foreach (DataRow dt in dtTemBiMat.Rows)
            {
                SoTemBiMat += Convert.ToInt32(dt["QRCodeNumber"]);
            }
            SoSP = dtProduct.Rows.Count;
            XemTT.Visible = true;
            txtSoSP.Visible = txtTembimat.Visible = txtTemcongkhai.Visible = true;

            txtSoSP.Text = SoSP.ToString();
            txtTembimat.Text = SoTemBiMat.ToString();
            txtTemcongkhai.Text = SoTemCongKhai.ToString();
            DataTable dtEva = BusinessRulesLocator.Conllection().GetAllList("SELECT * FROM Evaluate where ProductBrand_ID=" + ddlProductBrand.SelectedValue + " Order by LastEditDate DESC OFFSET 0 ROWS FETCH FIRST 1 ROW ONLY");
            if (dtEva.Rows.Count == 1)
            {
                ddlChiso.SelectedValue = dtEva.Rows[0]["Star"].ToString();
                txtNote.Text = dtEva.Rows[0]["Note"].ToString();
            }
            else
            {
                txtNote.Text = "";
                ddlChiso.SelectedIndex = 0;
            }

        }
        else
        {
            XemTT.Visible = false;
            txtSoSP.Text = txtTemcongkhai.Text = txtTembimat.Text = "";
            txtNote.Text = "";
            ddlChiso.SelectedIndex = 0;

        }

    }

}