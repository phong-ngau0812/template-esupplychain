using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class QRCodePackeProductPackageregister_List : System.Web.UI.Page
{
    //chưa phân trang theo sql
    public string Message = "";
    public string NameProductPackage = "";
    public string NameProduct = "";
    public string TotalTem = "";
    public string SerialStart = "";
    public string SerialEnd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // btnAdd.Visible = MyActionPermission.CanAdd();
        if (!IsPostBack)
        {
           
        }
        ResetMsg();
    }
    protected void ResetMsg()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QRCodePackeProductPackageregister_Add.aspx", false);
    }


    protected void rptMateria_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Materia_ID = Convert.ToInt32(e.CommandArgument);
        MaterialRow _MaterialRow = new MaterialRow();
        _MaterialRow = BusinessRulesLocator.GetMaterialBO().GetByPrimaryKey(Materia_ID);
        switch (e.CommandName)
        {
            case "Delete":


                if (MyActionPermission.CanDeleteMaterial(Materia_ID, ref Message))
                {
                    if (_MaterialRow != null)
                    {
                        _MaterialRow.Active = false;
                    }
                    BusinessRulesLocator.GetMaterialBO().Update(_MaterialRow);
                    MyActionPermission.WriteLogSystem(Materia_ID, "Xóa - " + _MaterialRow.Name);
                    lblMessage.Text = ("Xóa bản ghi thành công !");
                }
                else
                {
                    lblMessage.Text = Message;
                    lblMessage.Style.Add("background", "wheat");
                    lblMessage.ForeColor = Color.Red;
                }
                break;
                //case "Active":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 1;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Kích hoạt thành công !");
                //    break;
                //case "Deactive":
                //    if (_StaffTypeRow != null)
                //    {
                //        _StaffTypeRow.Active = 0;
                //    }
                //    BusinessRulesLocator.GetStaffTypeBO().Update(_StaffTypeRow);
                //    lblMessage.Text = ("Ngừng kích hoạt thành công !");
                //    break;

        }
        lblMessage.Visible = true; ;
       
    }

    protected void rptMateria_ItemDataBound(object sender, RepeaterItemEventArgs e)
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


  
}