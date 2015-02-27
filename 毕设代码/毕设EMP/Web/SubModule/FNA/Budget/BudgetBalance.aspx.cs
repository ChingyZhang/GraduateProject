using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using System.Data;
using MCSFramework.Common;

public partial class SubModule_FNA_Budget_BudgetBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            if (Request.QueryString["OrganizeCity"] != null) tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"];
            if (Request.QueryString["AccountMonth"] != null) ddl_Month.SelectedValue = Request.QueryString["AccountMonth"];
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
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
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
        ddl_Level.DataBind();
        ddl_Level.Items.Insert(0, new ListItem("所有", "0"));

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));
    }
    protected void ddl_FeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_BudgetList.PageIndex = 0;
        gv_ChangeList.PageIndex = 0;
        BindDetailGrid();
    }
    #endregion

    private void BindGrid()
    {
        int OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        int Month = int.Parse(ddl_Month.SelectedValue);       //会计月条件
        int Level = int.Parse(ddl_Level.SelectedValue);

        DataTable dt = FNA_BudgetBalanceBLL.GetBalance(OrganizeCity, Month, Level);
        DataTable dt_Matrix = MatrixTable.Matrix(dt, new string[] { "OrganizeCity", "区域名称", "区域级别" }, "FeeTypeName", "CostBalance");

        gv_BalanceList.SelectedIndex = -1;
        gv_BalanceList.DataSource = dt_Matrix;
        gv_BalanceList.DataBind();

        tr_BalanceChangeList.Visible = false;
    }
    protected void gv_BalanceList_DataBound(object sender, EventArgs e)
    {
        if (gv_BalanceList.HeaderRow != null)
        {
            gv_BalanceList.HeaderRow.Cells[1].Text = "";
            foreach (GridViewRow r in gv_BalanceList.Rows)
            {
                r.Cells[1].Text = "";
            }
        }
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_BalanceList.PageIndex = 0;
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_BudgetList.PageIndex = 0;
        gv_ChangeList.PageIndex = 0;
        BindDetailGrid();
    }

    private void BindDetailGrid()
    {
        int OrganizeCity = (int)gv_BalanceList.DataKeys[gv_BalanceList.SelectedIndex][0];
        int Month = int.Parse(ddl_Month.SelectedValue);       //会计月条件
        int FeeType = int.Parse(ddl_FeeType.SelectedValue);

        if (OrganizeCity > 0)
        {
            if (MCSTabControl1.SelectedIndex == 0)
            {
                gv_ChangeList.ConditionString = "FNA_BudgetChangeHistory.AccountMonth=" + Month.ToString() +
                " AND FNA_BudgetChangeHistory.OrganizeCity=" + OrganizeCity.ToString();
                if (FeeType > 0) gv_ChangeList.ConditionString += " AND FNA_BudgetChangeHistory.FeeType=" + FeeType.ToString();
                gv_ChangeList.BindGrid();

                gv_ChangeList.Visible = true;
                gv_BudgetList.Visible = false;
            }
            else
            {
                gv_BudgetList.ConditionString = "FNA_Budget.AccountMonth=" + Month.ToString() +
                " AND FNA_Budget.OrganizeCity=" + OrganizeCity.ToString();
                if (FeeType > 0) gv_BudgetList.ConditionString += " AND FNA_Budget.FeeType=" + FeeType.ToString();
                gv_BudgetList.BindGrid();

                gv_ChangeList.Visible = false;
                gv_BudgetList.Visible = true;
            }
        }
        else
        {
            tr_BalanceChangeList.Visible = false;
            gv_BalanceList.SelectedIndex = -1;
        }
    }
    protected void gv_BalanceList_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_BalanceChangeList.Visible = true;
        gv_BudgetList.PageIndex = 0;
        gv_ChangeList.PageIndex = 0;
        BindDetailGrid();
    }
    protected void gv_BalanceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_BalanceList.PageIndex = e.NewPageIndex;
        BindGrid();
    }

}
