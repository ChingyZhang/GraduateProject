// ===================================================================
// 文件路径:SubModule/PBM/Visit/RouteList.aspx.cs 
// 生成日期:2015-04-05 22:16:37 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;

public partial class SubModule_PBM_Visit_RouteList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {

            BindDropDown();

            BindGrid();

            //TDP维护自己的路线
            if ((int)Session["OwnerType"] == 3) Header.Attributes["WebPageSubCode"] = "OwnerType=3";
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " VST_Route.OwnerType=" + Session["OwnerType"].ToString() + " AND VST_Route.OwnerClient=" + Session["OwnerClient"].ToString();

        if (tbx_FindTxt.Text != "")
        {
            ConditionStr += " AND (VST_Route.Code LIKE '%" + tbx_FindTxt.Text + "%' OR VST_Route.Name LIKE '%" + tbx_FindTxt.Text + "%') ";
        }

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("RouteDetail.aspx");
    }

}