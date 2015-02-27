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
            ViewState["IsOpponent"] = 1;// Request.QueryString["IsOpponent"] == null ? 0 : int.Parse(Request.QueryString["IsOpponent"]);
            BindDropdown();
            BindGrid();

            Header.Attributes["WebPageSubCode"] = "IsOpponent=" + ViewState["IsOpponent"].ToString();
        }
    }

    private void BindDropdown()
    {
        //获取品牌
        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=" + ViewState["IsOpponent"].ToString());
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有", "0"));
        ddl_Brand_SelectedIndexChanged(null, null);
        
        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";

        rbl_State.DataSource = DictionaryBLL.GetDicCollections("PDT_State");
        rbl_State.DataBind();
        rbl_State.SelectedValue = "1";
    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = PDT_ClassifyBLL.GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("所有", "0"));
        BindGrid();
    }
    protected void ddl_Classify_SelectedIndexChanged(object sender, EventArgs e)
    {
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        string condition = "PDT_Product.State = " + rbl_State.SelectedValue;

        if (ddl_Brand.SelectedValue != "" && ddl_Brand.SelectedValue != "0")
            condition += " AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        else
            condition += " AND PDT_Product.Brand IN (SELECT ID FROM MCS_PUB.dbo.PDT_Brand WHERE IsOpponent=1) ";

        if (ddl_Classify.SelectedValue != "" && ddl_Classify.SelectedValue != "0")
            condition += " AND PDT_Product.Classify=" + ddl_Classify.SelectedValue;

        if (tbx_Search.Text != "")
        {
            condition += "  AND (PDT_Product.FullName like '%" + tbx_Search.Text + "%' OR PDT_Product.ShortName like '%" + tbx_Search.Text + "%' OR PDT_Product.Code like '%" + tbx_Search.Text + "%') ";
        }

        if (rbl_ApproveFlag.SelectedValue != "0" && rbl_ApproveFlag.SelectedValue != "")
        {
            condition += "  AND PDT_Product.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
        }
        
        ud_grid.ConditionString = condition;
        ud_grid.BindGrid();
    }

    //protected void ud_grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    Response.Redirect("ProductDetail.aspx?ID=" + ud_grid.DataKeys[e.NewSelectedIndex][0].ToString() + "&IsOpponent=" + ViewState["IsOpponent"].ToString());
    //}

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductDetail.aspx?Brand=" + ddl_Brand.SelectedValue + "&IsOpponent=" + ViewState["IsOpponent"].ToString());
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                new PDT_ProductBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex]["PDT_Product_ID"].ToString())).Delete();
            }
        }
        ud_grid.PageIndex = 0;
        BindGrid();
    }

}
