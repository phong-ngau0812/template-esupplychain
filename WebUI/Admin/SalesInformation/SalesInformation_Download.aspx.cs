using DbObj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class SalesInformation_Download : System.Web.UI.Page
{
    public string title = "Xuất hóa đơn bán lẻ";
    public int SalesInformation_ID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["SalesInformation_ID"]))
            int.TryParse(Request["SalesInformation_ID"].ToString(), out SalesInformation_ID);

        if (!IsPostBack)
        {
            LoadData();
        }
        // ResetMsg();
    }

    private void LoadData()
    {
        try
        {
            if (SalesInformation_ID != 0)
            {
                SalesInformationRow _SalesInformationRow = new SalesInformationRow();
                _SalesInformationRow = BusinessRulesLocator.GetSalesInformationBO().GetByPrimaryKey(SalesInformation_ID);
                if (_SalesInformationRow != null)
                {
                    lblSoHoaDon.Text = _SalesInformationRow.SalesInformation_ID.ToString();
                    lblDate.Text = _SalesInformationRow.CreateDate.ToString("dd/MM/yyyy hh:mm:ss");
                    lblKhachHang.Text = _SalesInformationRow.IsCustomerNameNull ? string.Empty : _SalesInformationRow.CustomerName;
                    Discount.Text = lblDiscount.Value = _SalesInformationRow.Discount.ToString(); ;
                    if (_SalesInformationRow.ProductBrand_ID != 0)
                    {
                        ProductBrandRow _ProductBrandRow = BusinessRulesLocator.GetProductBrandBO().GetByPrimaryKey(_SalesInformationRow.ProductBrand_ID);
                        if (_ProductBrandRow != null)
                        {
                            lblCompany.Text = _ProductBrandRow.IsNameNull ? string.Empty : _ProductBrandRow.Name.ToUpper();
                            lblAddress.Text = _ProductBrandRow.IsAddressNull ? string.Empty : _ProductBrandRow.Address;
                            lblPhone.Text = _ProductBrandRow.IsTelephoneNull ? string.Empty : _ProductBrandRow.Telephone;
                        }
                    }
                    if (_SalesInformationRow.Workshop_ID != 0)
                    {
                        WorkshopRow _WorkshopRow = BusinessRulesLocator.GetWorkshopBO().GetByPrimaryKey(_SalesInformationRow.Workshop_ID);
                        if (_WorkshopRow != null)
                        {
                            lblNV.Text = _WorkshopRow.IsNameNull ? string.Empty : _WorkshopRow.Name;
                            lblMaNV.Text = _WorkshopRow.Workshop_ID.ToString();
                        }
                    }

                    DataTable dt = BusinessRulesLocator.Conllection().GetAllList(@"  select SP.Unit, SP.Quantity, SP.Price, P.Name from SalesInformationVsProduct SP
  inner join Product P on P.Product_ID=SP.Product_ID
  where P.Active=1 and SP.SalesInformation_ID=" + SalesInformation_ID);
                    if (dt.Rows.Count > 0)
                    {
                        rptProduct.DataSource = dt;
                        rptProduct.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Log.writeLog("LoadData", ex.ToString());
        }
    }
  
    public decimal total = 0;
    protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lblPrice = e.Item.FindControl("lblPrice") as Literal;
            Literal lblQuantity = e.Item.FindControl("lblQuantity") as Literal;
            if (lblPrice != null && lblQuantity != null)
            {
                total += ((decimal.Parse(lblPrice.Text) * decimal.Parse(lblQuantity.Text))) - ((decimal.Parse(lblPrice.Text) * decimal.Parse(lblQuantity.Text) * decimal.Parse(lblDiscount.Value) / 100));
            }
        }
    }
}