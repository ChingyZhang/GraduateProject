// ===================================================================
// 文件路径:SubModule/PBM/Account/BalanceUsageList.aspx.cs 
// 生成日期:2015/1/27 14:31:53 
// 作者:	  Jace
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

public partial class SubModule_PBM_Account_BalanceUsageList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
		// 在此处放置用户代码以初始化页面
		if (!Page.IsPostBack)
		{
            ViewState["TradeClient"] = Request.QueryString["TradeClient"] != null ? int.Parse(Request.QueryString["TradeClient"]) : 0;

			tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");
			
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
        string ConditionStr = " AC_BalanceUsageList.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59' ";
        if ((int)Session["OwnerType"] == 3) ConditionStr += " AND AC_BalanceUsageList.OwnerClient = " + Session["OwnerClient"].ToString();

        if ((int)ViewState["TradeClient"] != 0) ConditionStr += " AND AC_BalanceUsageList.TradeClient=" + ViewState["TradeClient"].ToString();

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

	}
	#region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("BalanceUsageDetail.aspx?ID=" + _id.ToString());
    }
    #endregion
	
	protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

}