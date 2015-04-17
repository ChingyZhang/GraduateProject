using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Common;

public partial class SubModule_OA_BBS_CatalogManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
        }
    }
    private void BindData()
    {
        BBS_CatalogBLL bll = new BBS_CatalogBLL((int)ViewState["ID"]);
        UC_Catalog.BindData(bll.Model);

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        BBS_CatalogBLL _catalogbll = null;
        if (bt_OK.Text == "添加")
        {
            _catalogbll = new BBS_CatalogBLL();
        }
        else
        {
            _catalogbll = new BBS_CatalogBLL((int)ViewState["ID"]);
        }

        UC_Catalog.GetData(_catalogbll.Model);

        if (bt_OK.Text == "添加")
        {
            ViewState["ID"] = _catalogbll.Add();
        }
        else
        {
            _catalogbll.Update();
        }
        MessageBox.ShowAndRedirect(this, "保存成功", "index.aspx");
    }
    protected void bt_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search/index.aspx");
    }
}

