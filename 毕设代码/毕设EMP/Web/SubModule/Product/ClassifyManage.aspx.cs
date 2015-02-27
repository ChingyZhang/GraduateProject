using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_Product_PDT_ClassifyManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["BrandID"] = Request.QueryString["BrandID"] == null ? 0 : int.Parse(Request.QueryString["BrandID"]);
            if ((int)ViewState["ID"] > 0)
                BindData();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        if (ViewState["BrandID"].ToString() != "0")
        {
            ud_grid.ConditionString = " PDT_Classify.Brand=" + ViewState["BrandID"].ToString();
            ((DropDownList)pl_detail.FindControl(new PDT_Classify().ModelName + "_" + "Brand")).SelectedValue = ViewState["BrandID"].ToString();
        }
        ud_grid.BindGrid();
    }

    protected void ud_grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Redirect("ClassifyManage.aspx?ID=" + ud_grid.DataKeys[e.NewSelectedIndex][0].ToString() + "&BrandID=" + ViewState["BrandID"].ToString());
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (ViewState["ID"].ToString() != "0")
        {
            new PDT_ClassifyBLL(int.Parse(ViewState["ID"].ToString())).Delete();
            BindGrid();
        }
    }

    private void BindData()
    {
        MCSFramework.Model.Pub.PDT_Classify md = new PDT_ClassifyBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(md);
        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PDT_ClassifyBLL _Classify = null;
        if (bt_OK.Text == "添加")
        {
            _Classify = new PDT_ClassifyBLL();
        }
        else
        {
            _Classify = new PDT_ClassifyBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_Classify.Model);
        if (bt_OK.Text == "添加")
        {
            ViewState["ID"] = _Classify.Add();
        }
        else
        {
            _Classify.Update();
        }
        Response.Redirect("ClassifyManage.aspx?ID=" + ViewState["ID"].ToString() + "&BrandID=" + ViewState["BrandID"].ToString());
    }
}
