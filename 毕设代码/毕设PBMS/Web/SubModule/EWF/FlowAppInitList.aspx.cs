using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Model;
using MCSFramework.BLL;

public partial class SubModule_EWF_FlowAppInitList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindGrid();
        }
    }

    private void BindGrid()
    {
        Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;

        string condition = " ID IN (SELECT APP FROM EWF_Flow_InitPosition WHERE Position = " + staff.Position.ToString() + " AND " + DateTime.Today.Day.ToString() + " BETWEEN BeginDay AND EndDay)" + " AND EnableFlag='Y'";

        if (!string.IsNullOrEmpty(tbx_Condition.Text))
        {
            condition += "AND Name Like '%" + tbx_Condition.Text + "%'";
        }
        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(condition);

        gv_List.TotalRecordCount = apps.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.BindGrid<EWF_Flow_App>(apps);
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
