using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_Product_ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Brand"] = Request.QueryString["Brand"] == null ? 0 : int.Parse(Request.QueryString["Brand"]);

            BindDropdown();
            BindGrid();
        }
    }

    private void BindDropdown()
    {
        //获取品牌
        if ((int)Session["OwnerType"] == 1)
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("");
        }
        else if ((int)Session["OwnerType"] == 2)
        {
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=1");
        }
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有", "0"));
        if ((int)ViewState["Brand"] > 0) ddl_Brand.SelectedValue = ViewState["Brand"].ToString();

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";

        rbl_State.DataSource = DictionaryBLL.GetDicCollections("PDT_State");
        rbl_State.DataBind();
        rbl_State.SelectedValue = "1";
    }

    private void BindGrid()
    {
        string condition = "PDT_Product.State = " + rbl_State.SelectedValue;

        if (ddl_Brand.SelectedValue != "" && ddl_Brand.SelectedValue != "0")
            condition += " AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;

        if (tbx_Search.Text != "")
        {
            condition += "  AND (PDT_Product.FullName like '%" + tbx_Search.Text + "%' OR PDT_Product.ShortName like '%" + tbx_Search.Text + "%' OR PDT_Product.Code like '%" + tbx_Search.Text + "%') ";
        }

        if (rbl_ApproveFlag.SelectedValue != "0")
        {
            condition += "  AND PDT_Product.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
        }

        if ((int)Session["OwnerType"] == 2)
        {
            condition += "  AND PDT_Product.OwnerType = 2 AND PDT_Product.OwnerClient =" + Session["OwnerClient"].ToString();
        }
        else if ((int)Session["OwnerType"] == 1)
        {
            condition += "  AND PDT_Product.OwnerType IN (1, 2) ";
        }

        ud_grid.ConditionString = condition;
        ud_grid.BindGrid();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductDetail.aspx?Brand=" + ddl_Brand.SelectedValue);
    }

}
