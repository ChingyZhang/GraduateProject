// ===================================================================
// 文件路径:SubModule/OA/Car/Car_DispatchRideList.aspx.cs 
// 生成日期:2011/6/20 8:12:25 
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
using MCSFramework.BLL.OA;

public partial class SubModule_OA_Car_Car_DispatchRideList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["State"] != null)
            {
                MCSTabControl1.SelectedIndex = int.Parse(Request.QueryString["State"]) - 1;
            }

            tbx_begin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Car.DataSource = Car_CarListBLL.GetModelList("State IN (1,2)");
        ddl_Car.DataBind();
        ddl_Car.Items.Insert(0, new ListItem("请选择", "0"));
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " ISNULL(Car_DispatchRide.ActGoOutTime,GETDATE()) BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'" +
            " AND Car_DispatchRide.State=" + MCSTabControl1.SelectedTabItem.Value;

        if (ddl_Car.SelectedValue != "0") ConditionStr += " AND Car_DispatchRide.CarID = " + ddl_Car.SelectedValue;

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }
    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("Car_DispatchRideDetail.aspx?ID=" + _id.ToString());
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("Car_DispatchRideDetail.aspx");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}