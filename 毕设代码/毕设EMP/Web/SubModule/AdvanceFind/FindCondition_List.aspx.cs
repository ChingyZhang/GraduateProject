// ===================================================================
// 文件路径:SubMoudle/CM/FindCodition/FindCondition_List.aspx.cs 
// 生成日期:2008/3/1 21:45:07 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Collections.Generic;
using MCSFramework.Model.Pub;

public partial class SubMoudle_AdvanceFind_FindCondition_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindDropDown();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_PanelList.DataSource = UD_PanelBLL.GetModelList("AdvanceFind='Y'");
        ddl_PanelList.DataBind();
        ddl_PanelList.Items.Insert(0, new ListItem("请选择...", "0"));
        ddl_PanelList_SelectedIndexChanged(null, null);

        if (Request.QueryString["Panel"] != null && ddl_PanelList.Items.FindByValue(Request.QueryString["Panel"]) != null)
        {
            ddl_PanelList.SelectedValue = Request.QueryString["Panel"];
            BindGrid();
        }

    }
    #endregion

    private void BindGrid()
    {
        if (ddl_PanelList.SelectedValue != "0")
        {
            string _conditionstring = "Panel='" + ddl_PanelList.SelectedValue + "'";
            if (tbx_Find.Text.Trim() != "")
                _conditionstring += " and Name like '%" + tbx_Find.Text.Trim() + "%'";
            IList<ADFind_FindCondition> _conditioins = ADFind_FindConditionBLL.GetModelList(_conditionstring);
            if (ViewState["PageIndex"] != null)
            {
                gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            }
            gv_List.DataSource = _conditioins;
            gv_List.DataBind();
        }
    }

    #region 分页、排序、选中等事件
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = int.Parse(gv_List.DataKeys[e.NewSelectedIndex].Value.ToString());
        Response.Redirect("FindCondition_Detail.aspx?ID=" + _id.ToString());
    }

    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (ddl_PanelList.SelectedValue != "0")
            Response.Redirect("FindCondition_Detail.aspx?Panel=" + ddl_PanelList.SelectedValue);
        else
            Response.Redirect("FindCondition_Detail.aspx");
    }

    protected void ddl_PanelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}