// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.EWF;

public partial class SubModule_EWF_FlowAppList : System.Web.UI.Page
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
        string condition = "";
        if (MCSTabControl1.SelectedIndex == 0)//启用流程列表
        {
            condition = "EnableFlag = 'Y' ";
        }
        else //禁用流程列表
        {
            condition = "EnableFlag = 'N' ";
        }
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

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("FlowAppDetail.aspx");
    }
    
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
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