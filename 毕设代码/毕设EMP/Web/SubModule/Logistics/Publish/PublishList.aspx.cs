using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;

public partial class SubModule_Logistics_Publish_PublishList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<DateAdd(month,1,GetDate())");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<DateAdd(month,3,GetDate())");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(1)).ToString();

    }
    #endregion

    private void BindGrid()
    {
        string condition = "MCS_Logistics.dbo.ORD_ApplyPublish.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void bt_Add1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PublishDetail.aspx?Type=1");
    }
    protected void bt_Add2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PublishDetail.aspx?Type=2");
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}
