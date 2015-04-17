using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.SQLDAL;
using MCSFramework.Model;

public partial class SubModule_UDM_WebPageList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string condition = "";
        if (tbx_Find.Text != "")
            condition = "Title like '%" + tbx_Find.Text + "%' OR Path like '%" + tbx_Find.Text + "%'";

        IList<UD_WebPage> source = UD_WebPageBLL.GetModelList(condition);
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.TotalRecordCount = source.Count;

        gv_List.BindGrid<UD_WebPage>(source);

    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebPageDetail.aspx");
    }
}
