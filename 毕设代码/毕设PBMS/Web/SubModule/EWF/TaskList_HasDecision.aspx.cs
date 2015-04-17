// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using System.Collections.Generic;
using MCSFramework.Model.EWF;
using System.Data;
using MCSFramework.BLL;
using System;

public partial class SubModule_EWF_TaskList_HasDecision : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();
            BindGrid();

            #region 权限判断
            //Right right = new Right();
            //string strUserName = Session["UserName"].ToString();
            //if (!right.GetAccessPermission(strUserName, 110, 0)) Response.Redirect("../noaccessright.aspx");        //有无查看的权限
            //if (!right.GetAccessPermission(strUserName, 0, 1001)) bt_Add.Visible = false;                              //有无新增的权限
            #endregion
        }
    }

    private void BindDropDown()
    {
        string condition = " EnableFlag='Y'";

        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(condition);
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_DecisionResult.DataSource = DictionaryBLL.GetDicCollections("EWF_Task_DecisionResult");
        ddl_DecisionResult.DataBind();
        ddl_DecisionResult.Items.Insert(0, new ListItem("全部", "0"));
    }

    private void BindGrid()
    {
        DateTime dtBegin = DateTime.Parse(tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(tbx_end.Text).AddDays(1);

        DataTable dt = EWF_Task_JobBLL.GetJobHasDecision(int.Parse(Session["UserID"].ToString()), dtBegin, dtEnd);

        string condition = " 1 = 1 ";
        if (ddl_App.SelectedValue != "0")
        {
            condition += " AND App = '" + ddl_App.SelectedValue + "'";

        }
        if (tbx_MessageSubject.Text != "")
        {
            condition += " AND (Title like '%" + tbx_MessageSubject.Text + "%' OR MessageSubject like '%" + tbx_MessageSubject.Text + "%')";
        }
        if (ddl_DecisionResult.SelectedValue != "0")
        {
            condition += " AND (DecisionResultName='" + ddl_DecisionResult.SelectedItem.Text + "')";
        }

        dt.DefaultView.RowFilter = condition;
        dt.DefaultView.Sort = " TaskID desc ";
        gv_List.DataSource = dt.DefaultView;
        gv_List.TotalRecordCount = dt.DefaultView.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.DataBind();
    }

    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int TaskID = (int)this.gv_List.DataKeys[e.NewSelectedIndex]["TaskID"];
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
                break;
            case "2":
                Response.Redirect("TaskList_InitByMe.aspx");
                break;
            case "3":
                Response.Redirect("TaskHistoryList.aspx");
                break;
        }
    }
    protected void ddl_App_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }

    protected void btn_Search_Click(object sender, System.EventArgs e)
    {
        BindGrid();
    }
}