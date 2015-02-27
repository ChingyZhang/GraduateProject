using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.BLL;

public partial class SubModule_OA_BBS_BoardManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["CatalogID"] = Request.QueryString["CatalogID"] == null ? 0 : int.Parse(Request.QueryString["CatalogID"]);
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
        }
    }
    private void BindData()
    {
        BBS_BoardBLL bll = new BBS_BoardBLL((int)ViewState["ID"]);
        UC_Board.BindData(bll.Model);

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        BBS_BoardBLL _bll = null;
        if (bt_OK.Text == "添加")
        {
            _bll = new BBS_BoardBLL();
        }
        else
        {
            _bll = new BBS_BoardBLL((int)ViewState["ID"]);
        }

        UC_Board.GetData(_bll.Model);


        if (bt_OK.Text == "添加")
        {
            _bll.Model.Catalog = (int)ViewState["CatalogID"];
            ViewState["ID"] = _bll.Add();
        }
        else
        {
            _bll.Update();
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
