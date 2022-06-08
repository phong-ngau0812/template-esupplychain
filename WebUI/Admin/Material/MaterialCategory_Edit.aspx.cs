using DbObj;
using evointernal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemFrameWork;

public partial class MaterialCategory_Edit : System.Web.UI.Page
{
    int MaterialCategory_ID = 0;
    public string title = "Thông tin dạnh mục vật tư";
    public string avatar = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["MaterialCategory_ID"]))
            int.TryParse(Request["MaterialCategory_ID"].ToString(), out MaterialCategory_ID);
        if (!IsPostBack)
        {
            FillInfoMateriaCategory();
        }
    }

    protected void FillInfoMateriaCategory()
    {
        try
        {
            if (MaterialCategory_ID != 0)
            {
                MaterialCategoryRow _MateriaCategory = new MaterialCategoryRow();
                _MateriaCategory = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(MaterialCategory_ID);
                if (_MateriaCategory != null)
                {
                    txtName.Text = _MateriaCategory.IsNameNull ? string.Empty : _MateriaCategory.Name;
                    
                
                    if (_MateriaCategory.Active == 1)
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
            Log.writeLog("FillInfoMateriaCategory", ex.ToString());
        }
    }

    protected void UpdateMateriaCategory()
    {
        try
        {
            MaterialCategoryRow _MateriaCategory = new MaterialCategoryRow();
            if (MaterialCategory_ID != 0)
            {
                _MateriaCategory = BusinessRulesLocator.GetMaterialCategoryBO().GetByPrimaryKey(MaterialCategory_ID);
                if (_MateriaCategory != null)
                {
                    _MateriaCategory.Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text;
                   
                    if (ckActive.Checked)
                    {
                        _MateriaCategory.Active = 1;
                    }
                    else
                    {
                        _MateriaCategory.Active = 0;
                    }
                    _MateriaCategory.LastEditBy = MyUser.GetUser_ID();
                    _MateriaCategory.LastEditDate = DateTime.Now;
                    BusinessRulesLocator.GetMaterialCategoryBO().Update(_MateriaCategory);
                }
                lblMessage.Text = "Cập nhật thông tin thành công!";
                lblMessage.Visible = true;
                FillInfoMateriaCategory();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("UpdateMateriaCategory", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateMateriaCategory();
            }
        }
        catch (Exception ex)
        {
            Log.writeLog("btnSave_Click", ex.ToString());
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialCategory_List.aspx", false);
    }
}