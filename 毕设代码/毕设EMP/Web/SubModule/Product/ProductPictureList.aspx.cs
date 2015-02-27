using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;

public partial class SubModule_Product_ProductPictureList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                PDT_Product product = new PDT_ProductBLL(int.Parse(Request.QueryString["ID"])).Model;

                if (BindProduct(product))
                {
                    BindGrid();
                    select_ProductCode.Enabled = false;

                    if (product.Brand > 0)
                    {
                        PDT_Brand b = new PDT_BrandBLL(product.Brand).Model;
                        Header.Attributes["WebPageSubCode"] = "IsOpponent=" + b.IsOpponent;
                    }
                }
            }
            else
            {
                UploadFile1.Visible = false;
            }
        }
    }

    #region 绑定选择的产品
    private bool BindProduct(PDT_Product p)
    {
        if (p != null)
        {
            ViewState["ID"] = p.ID;
            select_ProductCode.SelectText = p.Code;
            select_ProductCode.SelectValue = p.ID.ToString();

            lb_ProductName.Text = p.FullName;
            lb_ProductName.ForeColor = System.Drawing.Color.Black;

            return true;
        }
        else
        {
            lb_ProductName.Text = "该产品不存在！！";
            lb_ProductName.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    protected void select_ProductCode_TextChange(object sender, MCSControls.MCSWebControls.TextChangeEventArgs e)
    {
        select_ProductCode.SelectValue = e.Code;

        PDT_Product _product = new PDT_ProductBLL(e.Code).Model;     
        BindProduct(_product);
    }
    protected void select_ProductCode_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        PDT_Product _product = new PDT_ProductBLL(select_ProductCode.SelectText).Model;
        BindProduct(_product);
    }

    #endregion

    private void BindGrid()
    {
        if (ViewState["ID"] != null)
        {
            UploadFile1.RelateID = (int)ViewState["ID"];
            UploadFile1.BindGrid();
            UploadFile1.Visible = true;
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            Response.Redirect("ProductDetail.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
}
