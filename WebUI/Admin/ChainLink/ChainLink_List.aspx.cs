using DbObj;
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

public partial class ChainLink_List : System.Web.UI.Page
{
    string Gerder;
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {

            loadChainLink();
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }

   

    protected void loadChainLink()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = BusinessRulesLocator.GetChainLinkBO().GetAsDataTable("Active<>-1", " CreateDate DESC");
            rptChainLink.DataSource = dt;
            rptChainLink.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog("FillDDLCha", ex.ToString());
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChainLink_Add.aspx", false);
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
                AgencyRow _AgencyRow = new AgencyRow();

                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    _AgencyRow = BusinessRulesLocator.GetAgencyBO().GetByPrimaryKey(Convert.ToInt32(lblID.Text));

                    if (ckActive.Checked)
                    {
                        _AgencyRow.Active = 1;

                        BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                        lblMessage.Text = Common.GetSuccessMsg("Kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        _AgencyRow.Active = 0;
                        BusinessRulesLocator.GetAgencyBO().Update(_AgencyRow);
                        lblMessage.Text = Common.GetSuccessMsg("Ngừng kích hoạt thành công !");
                        lblMessage.Visible = true;
                    }
                    loadChainLink();
                }
            }

        }
        catch (Exception Ex)
        {
            Log.writeLog("grdCouncil_DeleteCommand", Ex.ToString());
        }
    }

    protected void rptChainLink_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ChainLink_ID = Convert.ToInt32(e.CommandArgument);
        ChainLinkRow _ChainLinkRow = new ChainLinkRow();
        _ChainLinkRow = BusinessRulesLocator.GetChainLinkBO().GetByPrimaryKey(ChainLink_ID);
        switch (e.CommandName)
        {
            case "Delete":

                if (_ChainLinkRow != null)
                {
                    _ChainLinkRow.Active = -1;
                    MyActionPermission.WriteLogSystem(ChainLink_ID, "Xóa - " + _ChainLinkRow.Name);
                    BusinessRulesLocator.GetChainLinkBO().Update(_ChainLinkRow);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
            case "Active":
                if (_ChainLinkRow != null)
                {
                    _ChainLinkRow.Active = 1;
                }
                BusinessRulesLocator.GetChainLinkBO().Update(_ChainLinkRow);
                lblMessage.Text = ("Kích hoạt thành công !");
                break;
            case "Deactive":
                if (_ChainLinkRow != null)
                {
                    _ChainLinkRow.Active = 0;
                }
                BusinessRulesLocator.GetChainLinkBO().Update(_ChainLinkRow);
                lblMessage.Text = ("Ngừng kích hoạt thành công !");
                break;

        }
        lblMessage.Visible = true; ;
        loadChainLink();
    }

    protected void rptChainLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblApproved = e.Item.FindControl("lblApproved") as Literal;
            LinkButton btnActive = e.Item.FindControl("btnActive") as LinkButton;
            LinkButton btnDeactive = e.Item.FindControl("btnDeactive") as LinkButton;
            // CheckBox ckActive = e.Item.FindControl("ckActive") as CheckBox;
            Literal lblText = e.Item.FindControl("lblText") as Literal;
            if (lblApproved != null)
            {
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

    protected void ExportFile()
    {

        string ASProductBrandName = "DANH SÁCH CHUỖI LIÊN KẾT";
        DataTable dt = new DataTable();
        dt = BusinessRulesLocator.Conllection().GetAllList(@"  select ROW_NUMBER() OVER (ORDER BY CL.Name) AS RowNum, CL.Name , CL.Summary
from ChainLink CL
where Active = 1 ");
        dt.Columns["RowNum"].ColumnName = "RowNum";
        dt.Columns["Name"].ColumnName = "Chuỗi liên kết";
        dt.Columns["Summary"].ColumnName = "Mô tả ngắn";
        dt.AcceptChanges();

        string tab = ASProductBrandName + "<br>";
        tab += "(Tổng: " + dt.Rows.Count + ")<br><br>";
        GridView gvDetails = new GridView();

        gvDetails.DataSource = dt;

        gvDetails.AllowPaging = false;

        gvDetails.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "danh-sach-chuoi-lien-ket.xls"));
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //Change the Header Row back to white color
        // gvDetails.HeaderRow.Style.Add("background-color", "#7e94eb");
        gvDetails.HeaderRow.Style.Add("color", "#fff");
        //Applying stlye to gridview header cells
        for (int i = 0; i < gvDetails.HeaderRow.Cells.Count; i++)
        {
            gvDetails.HeaderRow.Cells[i].Style.Add("TextAlign", "top");
            gvDetails.HeaderRow.Cells[i].Style.Add("background-color", "#7e94eb");
        }
        gvDetails.RenderControl(htw);

        Response.Write(tab + sw.ToString());
        Response.End();

    }
    protected void btnExportFile_Click(object sender, EventArgs e)
    {
        ExportFile();
    }
}