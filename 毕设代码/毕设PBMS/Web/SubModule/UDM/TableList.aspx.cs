using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_UDM_TableList : System.Web.UI.Page
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
            condition = "UD_TableList.Name like '%" + tbx_Find.Text + "%' OR UD_TableList.DisplayName like '%" + tbx_Find.Text + "%' OR UD_TableList.ModelName like '%" + tbx_Find.Text + "%'";
        
        IList<UD_TableList> source = UD_TableListBLL.GetModelList(condition);
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.TotalRecordCount = source.Count;

        gv_List.BindGrid<UD_TableList>(source);
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
        Response.Redirect("TableDetail.aspx");
    }
}
