// ===================================================================
// 文件路径:SubModule/PBM/Partner/Vehicle/VehicleList.aspx.cs 
// 生成日期:2015-02-04 16:10:25 
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

public partial class SubModule_PBM_Partner_Vehicle_VehicleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {

            BindDropDown();

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " 1 = 1 ";

        //管理员登录可查看全部车辆
        int OwnerClient = Session["OwnerClient"] != null && int.TryParse(Session["OwnerClient"].ToString(), out OwnerClient) ? OwnerClient : 0;
        if (OwnerClient > 0) ConditionStr += "AND CM_Vehicle.Client=" + (int)Session["OwnerClient"];

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }
    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("VehicleDetail.aspx?ID=" + _id.ToString());
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("VehicleDetail.aspx");
    }

}