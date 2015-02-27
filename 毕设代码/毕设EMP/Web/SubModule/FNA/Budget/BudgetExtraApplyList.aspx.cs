// ===================================================================
// 文件路径:SubModule/FNA/Budget/BudgetExtraApplyList.aspx.cs 
// 生成日期:2010/8/19 13:17:22 
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

public partial class SubModule_FNA_Budget_BudgetExtraApplyList : System.Web.UI.Page
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

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));

        ddl_ExtraType.DataSource = DictionaryBLL.GetDicCollections("FNA_BudgetExtraType").OrderBy(p => p.Value.Name);
        ddl_ExtraType.DataBind();
        ddl_ExtraType.Items.Insert(0, new ListItem("全部", "0"));
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " FNA_BudgetExtraApply.AccountMonth = " + ddl_Month.SelectedValue;

        if (!string.IsNullOrEmpty(tbx_SheetCode.Text))
        {
            ConditionStr += " and MCS_SYS.dbo.UF_Spilt(FNA_BudgetExtraApply.ExtPropertys,'|',2)='" + tbx_SheetCode.Text + "'";
        }
        if (!string.IsNullOrEmpty(Select_InsertStaff.SelectValue))
        {
            ConditionStr += " and FNA_BudgetExtraApply.InsertStaff=" + Select_InsertStaff.SelectValue;
        }
        if (ddl_FeeType.SelectedValue != "0")
        {
            ConditionStr += " and FeeType=" + ddl_FeeType.SelectedValue;
        }
        if (ddl_ExtraType.SelectedValue != "0")
        {
            ConditionStr += " and MCS_SYS.dbo.UF_Spilt(FNA_BudgetExtraApply.ExtPropertys,'|',1)='" + ddl_ExtraType.SelectedValue + "'";
        }
        #region 判断当前可查询管理片区的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND FNA_BudgetExtraApply.OrganizeCity IN (" + orgcitys + ")";
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
        Response.Redirect("BudgetExtraApplyDetail.aspx");
    }

}