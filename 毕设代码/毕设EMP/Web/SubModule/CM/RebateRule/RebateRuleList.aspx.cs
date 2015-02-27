﻿// ===================================================================
// 文件路径:SubModule/CM/RebateRule/RebateRuleList.aspx.cs 
// 生成日期:2012/1/18 12:38:24 
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

public partial class SubModule_CM_RebateRule_RebateRuleList : System.Web.UI.Page
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
		Response.Redirect("RebateRuleDetail.aspx");
    }

}