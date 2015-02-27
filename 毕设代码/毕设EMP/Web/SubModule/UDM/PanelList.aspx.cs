using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.SQLDAL;
using MCSFramework.Model;

public partial class SubModule_UDM_PanelList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["DetailViewID"] != null)
                ViewState["DetailViewID"] = new Guid(Request.QueryString["DetailViewID"]);

            if (ViewState["DetailViewID"] != null)
            {
                ddl_DisplayType.SelectedValue = "0";
            }

            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string condition = " 1 = 1 ";

        if (ViewState["DetailViewID"] != null)
            condition += " AND DetailViewID = '" + ViewState["DetailViewID"].ToString() + "'";

        if (tbx_Find.Text != "")
            condition += " AND (Name like '%" + tbx_Find.Text + "%' OR Code like '%" + tbx_Find.Text + "%')";

        if (ddl_DisplayType.SelectedValue != "0")
            condition += " AND DisplayType = " + ddl_DisplayType.SelectedValue;

        IList<UD_Panel> list = UD_PanelBLL.GetModelList(condition);
        gv_List.TotalRecordCount = list.Count;

        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.BindGrid<UD_Panel>(list);
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if (ViewState["DetailViewID"] != null)
            Response.Redirect("PanelDetail.aspx?DetailViewID=" + ViewState["DetailViewID"].ToString());
        else
            Response.Redirect("PanelDetail.aspx");
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        new UD_PanelBLL((Guid)gv_List.DataKeys[e.RowIndex]["ID"]).Delete();
        ViewState["PageIndex"] = 0;
        BindGrid();
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
}
