using DbObj;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class BaoHiem_List : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSupplier();
        }
        ResetMsg();
    }

    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void LoadSupplier()
    {
        try
        {
            string where = string.Empty;
            if (ddlType.SelectedValue != "0")
            {
                where += " and LoaiXe='" + ddlType.SelectedValue + "'";
            }
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetBaoHiemBO().GetAsDataTable(" Active<>-1 " + where, " CreateDate DESC");
            rptSupplier.DataSource = dt;
            rptSupplier.DataBind();
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadUser", ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BaoHiem_Add.aspx", false);
    }

    protected void ckActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckActive = (CheckBox)sender;
        RepeaterItem row = (RepeaterItem)ckActive.NamingContainer;
        Literal lblID = (Literal)row.FindControl("lblID");
        try
        {
            if (lblID != null)
            {
                BaoHiemRow _SupplierRow = new BaoHiemRow();
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _SupplierRow = BusinessRulesLocator.GetBaoHiemBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));
                    if (ckActive.Checked)
                    {
                        _SupplierRow.Active = 1;
                        BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _SupplierRow.Active = 0;
                        BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    LoadSupplier();
                }

            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptSupplier_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        int Supplier_ID = Convert.ToInt32(e.CommandArgument);
        BaoHiemRow _SupplierRow = new BaoHiemRow();
        _SupplierRow = BusinessRulesLocator.GetBaoHiemBO().GetByPrimaryKey(Supplier_ID);

        switch (e.CommandName)
        {
            case "Delete":
                if (_SupplierRow != null)
                {
                    _SupplierRow.Active = -1;
                }
                BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                lblMessage.Text = ("Xóa bản ghi thành công !");
                break;
            case "Active":
                if (_SupplierRow != null)
                {
                    _SupplierRow.Active = 1;
                }
                BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_SupplierRow != null)
                {
                    _SupplierRow.Active = 0;
                }
                BusinessRulesLocator.GetBaoHiemBO().Update(_SupplierRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        LoadSupplier();
    }

    protected void rptSupplier_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            //CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
                // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
                if (lblApproved.Text == "0")
                {
                    btnDeactive.Visible = true;
                    btnActive.Visible = false;
                    //lblText.Text = "<span class=\"badge badge-soft-danger\">Ngừng kích hoạt</span>";
                    lblText.Text = "<span class=\" text-danger \">Ngừng kích hoạt</span>";
                }
                else

                {
                    btnDeactive.Visible = false;
                    btnActive.Visible = true;
                    //lblText.Text = "<span class=\"badge badge-soft-success\">Đã kích hoạt</span>";
                    lblText.Text = "<span class=\"text-success\">Đã kích hoạt</span>";
                }

            }
        }
    }

    public string QRCode(string Product_ID, string loaixe)
    {
        string domain = string.Empty;
        if (loaixe=="1")
        {
            domain = "https://esupplychain.vn/8938538734369/";
        }
        else
        {
            domain = "https://esupplychain.vn/8938538734383/";
        }
        string img = string.Empty;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(domain +DateTime.Now.Year.ToString()+ Product_ID.ToString(), QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                img = "<img width='150px' src='" + "data:image/png;base64," + Convert.ToBase64String(byteImage) + "'/>";
            }
        }
        return img;
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSupplier();
    }
}