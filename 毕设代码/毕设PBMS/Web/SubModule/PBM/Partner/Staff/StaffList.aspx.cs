using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;

public partial class SubModule_PBM_Partner_Staff_StaffList : System.Web.UI.Page
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
        string ConditionStr = " Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString();

        if (ddl_Dimission.SelectedValue != "0")
            ConditionStr += " AND Org_Staff.Dimission=" + ddl_Dimission.SelectedValue;

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("StaffDetail.aspx?ID=" + _id.ToString());
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffDetail.aspx");
    }

    protected void ddl_Dimission_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}