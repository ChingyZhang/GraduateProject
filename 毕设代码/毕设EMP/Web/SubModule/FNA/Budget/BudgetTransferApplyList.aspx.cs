﻿// ===================================================================
// 文件路径:SubModule/FNA/Budget/BudgetTransferApplyList.aspx.cs 
// 生成日期:2010/8/19 13:19:48 
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
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_Budget_BudgetTransferApplyList : System.Web.UI.Page
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
		Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();
	}
	#endregion
	
	private void BindGrid()
    {
        string ConditionStr = " FNA_BudgetTransferApply.AccountMonth = " + ddl_Month.SelectedValue;
		
        #region 判断当前可查询管理片区的范围
		string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND FNA_BudgetTransferApply.ToOrganizeCity IN (" + orgcitys + ")";
        }
        #endregion
        
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
		Response.Redirect("BudgetTransferApplyDetail.aspx");
    }

}