using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_Product_PDT_BrandManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
                BindData();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        if (ViewState["ConditionString"] != null)
            ud_grid.ConditionString = ViewState["ConditionString"].ToString();
        ud_grid.BindGrid();
    }

    protected void ud_grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Redirect("PDT_BrandManage.aspx?ID=" + ud_grid.DataKeys[e.NewSelectedIndex][0].ToString());
    }

    protected void ud_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("ClassifyManage.aspx?BrandID=" + ud_grid.DataKeys[e.NewEditIndex][0].ToString());
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ViewState["ConditionString"] = " PDT_Brand.Name like '%" + tbx_Search.Text + "%'";
        BindGrid();
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                new PDT_BrandBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex][0].ToString())).Delete();
            }
        }
        Response.Redirect("PDT_BrandManage.aspx");
    }

    private void BindData()
    {
        pl_detail.BindData(new PDT_BrandBLL((int)ViewState["ID"]).Model);
        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PDT_BrandBLL _Brand = null;
        if (bt_OK.Text == "添加")
        {
            _Brand = new PDT_BrandBLL();
        }
        else
        {
            _Brand = new PDT_BrandBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_Brand.Model);
        if (bt_OK.Text == "添加")
        {
            ViewState["ID"] = _Brand.Add();
        }
        else
        {
            _Brand.Update();
        }
        Response.Redirect("PDT_BrandManage.aspx");//?ID=" + ViewState["ID"].ToString());
    }

    #region 全选
    protected void btn_SelectAll_Click(object sender, EventArgs e)
    {
        CheckBox cb = null;
        try
        {
            foreach (GridViewRow gr in ud_grid.Rows)
            {
                cb = (CheckBox)gr.FindControl("chk_ID");
                cb.Checked = true;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region 反选
    protected void btn_SelectBack_Click(object sender, EventArgs e)
    {
        CheckBox cb = null;
        try
        {
            foreach (GridViewRow gr in ud_grid.Rows)
            {
                cb = (CheckBox)gr.FindControl("chk_ID");
                if (cb.Checked == true)
                    cb.Checked = false;
                else
                    cb.Checked = true;

            }
        }
        catch
        {
        }
    }
    #endregion
}
