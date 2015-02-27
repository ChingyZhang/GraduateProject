// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Model;
using MCSFramework.BLL;
using System.Collections.Generic;

public partial class SubModule_EWF_TaskList_InitByMe : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindDropDown();
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindGrid();
        }
    }
    private void BindDropDown()
    {
        Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;

        string condition = "EnableFlag='Y'";// " ID IN (SELECT APP FROM EWF_Flow_InitPosition WHERE Position = " + staff.Position.ToString() + ")";

        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(condition);
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_Status.DataSource = DictionaryBLL.GetDicCollections("EWF_Task_TaskStatus");
        ddl_Status.DataBind();
        ddl_Status.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_FinishStatus.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_FinishStatus");
        ddl_FinishStatus.DataBind();
        ddl_FinishStatus.Items.Insert(0, new ListItem("全部", "0"));
    }

    private void BindGrid()
    {
        Guid App = Guid.Empty;
        if (ddl_App.SelectedValue != "0") App = new Guid(ddl_App.SelectedValue);
        DateTime dtBegin = DateTime.Parse(tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(tbx_end.Text).AddDays(1);

        IList<EWF_Task> list = EWF_TaskBLL.GetByInitiator(App, (int)Session["UserID"], dtBegin, dtEnd,
            int.Parse(ddl_Status.SelectedValue), int.Parse(ddl_FinishStatus.SelectedValue));

        gv_List.TotalRecordCount = list.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.BindGrid<EWF_Task>(list);
    }

    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int TaskID = (int)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        Response.Redirect("TaskDetail.aspx?TaskID=" + TaskID.ToString());

    }
    #endregion

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                Response.Redirect("TaskList_NeedDecision.aspx");
                break;
            case "1":
                Response.Redirect("TaskList_HasDecision.aspx");
                break;
            case "2":
                break;
            case "3":
                Response.Redirect("TaskHistoryList.aspx");
                break;
        }
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
}