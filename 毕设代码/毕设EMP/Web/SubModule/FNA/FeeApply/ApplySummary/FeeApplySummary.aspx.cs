using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;
using MCSFramework.UD_Control;

public partial class SubModule_FNA_FeeApply_ApplySummary_FeeApplySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                    ddl_State.SelectedValue = Request.QueryString["State"];
            }
            if (Request.QueryString["TabItem"] != null)
            {
                MCSTabControl1.SelectedIndex = int.Parse(Request.QueryString["TabItem"]);
                gv_List.Visible = MCSTabControl1.SelectedIndex == 0;
                gv_ListDetail.Visible = !gv_List.Visible;
            }
            BindDropDown();

            if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
            {
                //无查看营养教育费用权限
                ListItem item = ddl_FeeType.Items.FindByValue(ConfigHelper.GetConfigInt("CSOCostType").ToString());
                if (item != null) item.Enabled = false;
            }

            tr_AccountTitle.Enabled = gv_ListDetail.Visible;
            ddl_ApplyCostOP.Enabled = gv_ListDetail.Visible;
            tbx_ApplyCost.Enabled = gv_ListDetail.Visible;
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
        if ((int)ViewState["OrganizeCity"] > 0)
        {
            tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
        }
        tr_OrganizeCity_Selected(null, null);
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (int)ViewState["AccountMonth"] == 0 ? AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString() : ViewState["AccountMonth"].ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));

        tr_AccountTitle.SelectValue = "1";
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) > city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0)
            {
                ddl_Level.Items.Add(new ListItem("本级", city.Level.ToString()));
            }

            ddl_Level.Items.Add(new ListItem("经销商", "10"));
            ddl_Level.Items.Add(new ListItem("零售商", "20"));
        }
    }
    #endregion

    #region 仅查看待我审批的费用申请单
    private string GetNeedMeApproveTaskIDs()
    {
        string taskids = "";

        DataTable dt = EWF_Task_JobBLL.GetJobToDecision(int.Parse(Session["UserID"].ToString()));
        dt.DefaultView.RowFilter = "AppCode Like 'FNA_FeeApplyFlow%'";
        if (dt.DefaultView.Count == 0) return "";

        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            taskids += dt.DefaultView[i]["TaskID"].ToString() + ",";
        }

        if (taskids.EndsWith(",")) taskids = taskids.Substring(0, taskids.Length - 1);

        return taskids;

    }
    #endregion

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int feetype = int.Parse(ddl_FeeType.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int flag = int.Parse(ddl_Flag.SelectedValue);

        if (MCSTabControl1.SelectedIndex == 0)
        {
            #region 显示汇总单数据源
            Dictionary_Data dicFeeType = null;
            if (feetype > 0) dicFeeType = DictionaryBLL.GetDicCollections("FNA_FeeType")[feetype.ToString()];
            DataTable dtSummary_Sub;
            DataTable dtSummary = FNA_FeeApplyBLL.GetSummaryTotal(month, organizecity, level, feetype, state, flag, int.Parse(Session["UserID"].ToString()));
            if (dtSummary.Rows.Count == 0)
            {
                gv_List.DataBind();
                return;
            }
            else
                dtSummary_Sub = FNA_FeeApplyBLL.GetSummaryTotal_Sub(month, organizecity, level, feetype, state, flag, int.Parse(Session["UserID"].ToString()));
            #region 矩阵化数据表，扩展表数据列
            dtSummary.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
            if (level < 10)
            {
                foreach (DataRow row in dtSummary.Rows)
                {
                    row["ID"] = row["OrganizeCity"];
                }
                dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称" },
                    new string[] { "FeeTypeName", "AccountTitleName" }, "ApplyCost", true, true);
            }
            else
            {
                if (level == 10)
                {
                    //按经销商查看
                    #region 将经销商的ID赋至表ID列
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        row["ID"] = row["经销商ID"] == DBNull.Value ? 0 : row["经销商ID"];
                        row["经销商名称"] = row["经销商名称"] == DBNull.Value ? "无" : row["经销商名称"];
                    }
                    #endregion

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称", "经销商名称" },
                       new string[] { "FeeTypeName", "AccountTitleName" }, "ApplyCost", true, true);
                }
                else if (level == 20)
                {
                    //按门店查看
                    #region 将门店的ID赋至表ID列
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        row["ID"] = row["客户ID"] == DBNull.Value ? 0 : row["客户ID"];
                        row["客户名称"] = row["客户名称"] == DBNull.Value ? "无" : row["客户名称"];
                    }
                    #endregion

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称", "客户名称" },
                       new string[] { "FeeTypeName", "AccountTitleName" }, "ApplyCost", true, true);
                }
            }
            dtSummary = MatrixTable.ColumnSummaryTotal(dtSummary, new int[] { 1 }, new string[] { "sales"});
            dtSummary.Columns["合计"].ColumnName = "费用合计";
            
            dtSummary.Columns.Add("本月费用合计", Type.GetType("System.Decimal"));
            dtSummary.Columns.Add("预计销量", Type.GetType("System.Decimal"));
            dtSummary.Columns.Add("上月销量", Type.GetType("System.Decimal"));
            dtSummary.Columns.Add("平均销量", Type.GetType("System.Decimal"));
            dtSummary.Columns.Add("预计费点", Type.GetType("System.String"));

            if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
            {
                dtSummary.Columns.Add("预算总额", Type.GetType("System.Decimal"));
                dtSummary.Columns.Add("其中扩增额", Type.GetType("System.Decimal"));
                dtSummary.Columns.Add("预算余额", Type.GetType("System.Decimal"));
                dtSummary.Columns.Add("终结费用", Type.GetType("System.Decimal"));
            }

            if (dicFeeType != null && dicFeeType.Description == "FeeRateControl")
                dtSummary.Columns.Add("上月余额", Type.GetType("System.Decimal"));

            #endregion

            decimal sumTotalVolume = 0, sumAvgVolume = 0, sumSalesForcast = 0, sumHappenApplyCost = 0;
            decimal sumTotalBudget = 0, sumExtBudget = 0, sumPreMonthBudgetBalance = 0, sumUsableAmount = 0, sumCancelCost = 0;

            int premonth = month - 1;
            if (premonth >= AC_AccountMonthBLL.GetCurrentMonth())
                premonth = AC_AccountMonthBLL.GetCurrentMonth() - 1;

            foreach (DataRow row in dtSummary.Rows)
            {
                int id = 0;

                if (int.TryParse(row["ID"].ToString(), out id) && id > 0)
                {
                    string filter = "ID=" + id;
                    bool includechild = false;
                    if (level < 10 && new Addr_OrganizeCityBLL(id).Model.Level >= level) includechild = true;

                    #region 计算销量数据

                    decimal happenApplyCost = 0, forcast = 0, preSales = 0, aVGSales = 0, feeRate = 0;
                    DataRow[] drows = dtSummary_Sub.Select(filter);
                    if (drows.Length > 0)
                    {
                        decimal.TryParse(drows[0]["HappenApplyCost"].ToString(), out happenApplyCost);
                        decimal.TryParse(drows[0]["Forcast"].ToString(), out forcast);
                        decimal.TryParse(drows[0]["PreSales"].ToString(), out preSales);
                        decimal.TryParse(drows[0]["AVGSales"].ToString(), out aVGSales);
                        decimal.TryParse(drows[0]["FeeRate"].ToString(), out feeRate);
                    }
                    row["本月费用合计"] = happenApplyCost.ToString("0.##");
                    row["上月销量"] = preSales.ToString("0.##");
                    row["平均销量"] = aVGSales.ToString("0.##");
                    row["预计销量"] = forcast.ToString("0.##");
                    row["预计费点"] = feeRate.ToString("0.##%");

                    sumHappenApplyCost += (decimal)row["本月费用合计"];
                    sumTotalVolume += (decimal)row["上月销量"];
                    sumAvgVolume += (decimal)row["平均销量"];
                    sumSalesForcast += (decimal)row["预计销量"];
                    #endregion

                    #region 预算总额及余额
                    if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
                    {
                        row["预算总额"] = (FNA_BudgetBLL.GetSumBudgetAmount(month, id, feetype, includechild) + FNA_BudgetBLL.GetSumBudgetAmount(month, id, 0, includechild));
                        sumTotalBudget += (decimal)row["预算总额"];

                        row["其中扩增额"] = (FNA_BudgetExtraApplyBLL.GetExtraAmount(month, id, feetype, includechild) + FNA_BudgetExtraApplyBLL.GetExtraAmount(month, id, 0, includechild));
                        sumExtBudget += (decimal)row["其中扩增额"];

                        row["预算余额"] = (FNA_BudgetBLL.GetUsableAmount(month, id, feetype, includechild) + FNA_BudgetBLL.GetUsableAmount(month, id, 0, includechild));
                        sumUsableAmount += (decimal)row["预算余额"];

                        row["终结费用"] = (FNA_FeeApplyBLL.GetCancelCost(month, id, feetype, includechild) + FNA_FeeApplyBLL.GetCancelCost(month, id, 0, includechild));
                        sumCancelCost += (decimal)row["终结费用"];
                    }
                    #endregion

                    //#region 计算预计费点
                    //if (dicFeeType != null && dicFeeType.Description == "FeeRateControl" && level < 10)
                    //{
                    //    row["上月余额"] = FNA_BudgetBLL.GetUsableAmount(month - 1, id, feetype, includechild);
                    //    sumPreMonthBudgetBalance += (decimal)row["上月余额"];

                    //    if ((decimal)row["预计销量"] != 0)
                    //        row["预计费点"] = (((decimal)row["费用合计"] - (decimal)row["上月余额"]) / (decimal)row["预计销量"]).ToString("0.00%");
                    //}
                    //else if ((decimal)row["预计销量"] != 0)
                    //    row["预计费点"] = ((decimal)row["费用合计"] / (decimal)row["预计销量"]).ToString("0.00%");
                    //#endregion
                }

                #region 求合计行
                if (id == 0)
                {
                    row["本月费用合计"] = sumHappenApplyCost.ToString("0.##");
                    row["上月销量"] = sumTotalVolume.ToString("0.##");
                    row["平均销量"] = sumAvgVolume.ToString("0.##");
                    row["预计销量"] = sumSalesForcast.ToString("0.##");

                    #region 预算总额及余额
                    if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
                    {
                        row["预算总额"] = sumTotalBudget.ToString("0.##");
                        row["其中扩增额"] = sumExtBudget.ToString("0.##");
                        row["预算余额"] = sumUsableAmount.ToString("0.##");
                        row["终结费用"] = sumCancelCost.ToString("0.##");

                    }
                    #endregion

                    #region 预计费点
                    if (sumSalesForcast != 0)
                    {
                        if (dicFeeType != null && dicFeeType.Description == "FeeRateControl" && level < 10)
                        {
                            row["上月余额"] = sumPreMonthBudgetBalance;
                            row["预计费点"] = (((decimal)row["费用合计"] - sumPreMonthBudgetBalance) / sumSalesForcast).ToString("0.##%");
                        }
                        else
                            row["预计费点"] = ((decimal)row["费用合计"] / sumSalesForcast).ToString("0.##%");
                    }
                    #endregion
                }
                #endregion

            }


            gv_List.DataSource = dtSummary;
            gv_List.DataBind();

            if (dtSummary.Columns.Count >= 24)
                gv_List.Width = new Unit(dtSummary.Columns.Count * 55);
            else
                gv_List.Width = new Unit(100, UnitType.Percentage);

            MatrixTable.GridViewMatric(gv_List);

            if (level == 20)
            {
                //按终端门店显示时，将上下行相同管理片区合并
                MatrixTable.GridViewMergSampeValueRow(gv_List, 0);
                MatrixTable.GridViewMergSampeValueRow(gv_List, 1);
            }

            #region 是否可以批量审批
            if (state != 1 || level >= 10)
            {
                gv_List.Columns[0].ItemStyle.Width = new Unit(1);
                foreach (GridViewRow row in gv_List.Rows)
                {
                    row.FindControl("bt_Approved").Visible = false;
                    row.FindControl("bt_UnApproved").Visible = false;
                }
            }
            else
            {
                gv_List.Columns[0].ItemStyle.Width = new Unit(68);
            }
            #endregion

            #endregion
        }
        else
        {
            string condition = "1=1";

            #region 组织明细记录的查询条件
            //管理片区及所有下属管理片区
            if (tr_OrganizeCity.SelectValue == "0")
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
                if ((int)ViewState["OrganizeCity"] > 0)
                {
                    tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
                }
                #endregion
            }
            if (tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
            }

            //会计月条件
            condition += " AND FNA_FeeApply.AccountMonth = " + ddl_Month.SelectedValue;

            //费用类型
            if (ddl_FeeType.SelectedValue != "0")
            {
                condition += " AND FNA_FeeApply.FeeType = " + ddl_FeeType.SelectedValue;
            }

            int accounttile = 0;
            int.TryParse(tr_AccountTitle.SelectValue, out accounttile);
            decimal _cost = 0;
            decimal.TryParse(tbx_ApplyCost.Text, out _cost);

            //费用科目
            if (accounttile > 1)
            {
                DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", accounttile.ToString());
                string ids = "";
                foreach (DataRow dr in dt.Rows)
                {
                    ids += dr["ID"].ToString() + ",";
                }
                ids += accounttile.ToString();

                if (_cost == 0)
                    condition += " AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE AccountTitle IN(" + ids + ") AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";
                else
                    condition += " AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE AccountTitle IN(" + ids + ") AND (ApplyCost+AdjustCost)" + ddl_ApplyCostOP.SelectedValue + "  " + _cost.ToString() + " AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";

            }
            else if (_cost != 0)//金额判断
            {
                condition += " AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE (ApplyCost+AdjustCost)" + ddl_ApplyCostOP.SelectedValue + "  " + _cost.ToString() + " AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";
            }

            //审批状态
            if (ddl_State.SelectedValue == "0")
                condition += " AND FNA_FeeApply.State IN (2,3) ";
            else if (ddl_State.SelectedValue == "1")
                condition +=
                @" AND FNA_FeeApply.State = 2 AND FNA_FeeApply.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
    MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
    EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";
            else if (ddl_State.SelectedValue == "2")
                condition += " AND FNA_FeeApply.State = 3 ";
            else if (ddl_State.SelectedValue == "3")
            {
                AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
                condition +=
                @" AND FNA_FeeApply.State IN (2,3) AND FNA_FeeApply.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
	MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
	EWF_Task_JobDecision.DecisionResult IN(2,5,6) AND 
	EWF_Task_JobDecision.DecisionTime BETWEEN DATEADD(month,-1,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"') AND 
		DATEADD(month,3,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"'))";
            }
            #endregion

            gv_ListDetail.ConditionString = condition;
            gv_ListDetail.BindGrid();

            btn_Approve.Visible = state == 1;
            btn_UnApprove.Visible = state == 1;
            gv_ListDetail.Columns[gv_ListDetail.Columns.Count - 1].Visible = state == 1;
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            gv_List.HeaderRow.Cells[1].Text = "&nbsp";
            foreach (GridViewRow r in gv_List.Rows)
            {
                r.Cells[1].Text = "&nbsp";

                #region 金额数据格式化
                if (r.Cells.Count > 2)
                {
                    for (int i = 2; i < r.Cells.Count - 1; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            r.Cells[i].Text = d.ToString("#,#.##");
                        }
                    }
                }
                #endregion
            }
        }
    }
    protected void gv_ListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_ListDetail.DataKeys[e.Row.RowIndex]["FNA_FeeApply_ID"];
            UC_GridView gv_Detail = (UC_GridView)e.Row.FindControl("gv_Detail");
            if (gv_Detail != null)
            {
                IList<FNA_FeeApplyDetail> lists = new FNA_FeeApplyBLL(id).Items;

                //费用科目
                int accounttile = 0;
                int.TryParse(tr_AccountTitle.SelectValue, out accounttile);
                if (accounttile > 1)
                {
                    DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", accounttile.ToString());
                    int[] ids = new int[dt.Rows.Count + 1];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ids[i] = (int)dt.Rows[i]["ID"];
                    }
                    ids[ids.Length - 1] = accounttile;
                    lists = lists.Where(p => ids.Contains(p.AccountTitle)).ToList();
                }

                //申请金额判断
                decimal _cost = 0;
                decimal.TryParse(tbx_ApplyCost.Text, out _cost);
                if (_cost != 0)
                {
                    if (ddl_ApplyCostOP.SelectedValue == ">")
                        lists = lists.Where(p => p.ApplyCost + p.AdjustCost > _cost).ToList();
                    else if (ddl_ApplyCostOP.SelectedValue == "<")
                        lists = lists.Where(p => p.ApplyCost + p.AdjustCost < _cost).ToList();
                }

                gv_Detail.BindGrid(lists);
            }
        }
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "1":
            case "2":
                gv_List.Visible = MCSTabControl1.SelectedIndex == 0;
                gv_ListDetail.Visible = !gv_List.Visible;

                tr_AccountTitle.Enabled = gv_ListDetail.Visible;
                ddl_ApplyCostOP.Enabled = gv_ListDetail.Visible;
                tbx_ApplyCost.Enabled = gv_ListDetail.Visible;

                btn_Approve.Visible = gv_ListDetail.Visible;
                btn_UnApprove.Visible = gv_ListDetail.Visible;
                ddl_Flag.SelectedIndex = 1;
                ddl_Flag.Enabled = false;
                BindGrid();
                break;            
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        #region 仅查看待我审批的费用申请单
        string taskids = "";
        if (ddl_State.SelectedValue == "1")
        {
            taskids = GetNeedMeApproveTaskIDs();

            if (taskids == "")
            {
                MessageBox.Show(this, "对不起，没有需要待您审批的费用申请单!");
                return;
            }
        }
        #endregion
        string[] TaskIDs = taskids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        int organizecity = (int)gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        string condition = " FNA_FeeApply.State = 2 ";

        #region 组织明细记录的查询条件
        //管理片区及所有下属管理片区
        if (organizecity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += organizecity.ToString();

            condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND FNA_FeeApply.AccountMonth = " + ddl_Month.SelectedValue;

        //费用类型
        if (ddl_FeeType.SelectedValue != "0")
        {
            condition += " AND FNA_FeeApply.FeeType = " + ddl_FeeType.SelectedValue;
        }
        #endregion

        IList<FNA_FeeApply> lists = FNA_FeeApplyBLL.GetModelList(condition);
        foreach (FNA_FeeApply fee in lists)
        {
            if (TaskIDs.Contains(fee.ApproveTask.ToString()))
            {
                int jobid = EWF_TaskBLL.StaffCanApproveTask(fee.ApproveTask, (int)Session["UserID"]);
                EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                if (job.Model != null)
                {
                    int decision = job.StaffCanDecide((int)Session["UserID"]);
                    if (decision > 0)
                        job.Decision(decision, (int)Session["UserID"], 2, "汇总单批量审批通过!");       //2:审批已通过
                }
            }
        }

        BindGrid();
        MessageBox.Show(this, "审批成功！");
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        #region 仅查看待我审批的费用申请单
        string taskids = "";
        if (ddl_State.SelectedValue == "1")
        {
            taskids = GetNeedMeApproveTaskIDs();

            if (taskids == "")
            {
                MessageBox.Show(this, "对不起，没有需要待您审批的费用申请单!");
                return;
            }
        }
        #endregion
        string[] TaskIDs = taskids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        int organizecity = (int)gv_List.DataKeys[e.RowIndex]["ID"];
        string condition = " FNA_FeeApply.State = 2 ";

        #region 组织明细记录的查询条件
        //管理片区及所有下属管理片区
        if (organizecity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += organizecity.ToString();

            condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND FNA_FeeApply.AccountMonth = " + ddl_Month.SelectedValue;

        //费用类型
        if (ddl_FeeType.SelectedValue != "0")
        {
            condition += " AND FNA_FeeApply.FeeType = " + ddl_FeeType.SelectedValue;
        }
        #endregion

        IList<FNA_FeeApply> lists = FNA_FeeApplyBLL.GetModelList(condition);
        foreach (FNA_FeeApply fee in lists)
        {
            if (TaskIDs.Contains(fee.ApproveTask.ToString()))
            {
                int jobid = EWF_TaskBLL.StaffCanApproveTask(fee.ApproveTask, (int)Session["UserID"]);
                EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                if (job.Model != null)
                {
                    int decision = job.StaffCanDecide((int)Session["UserID"]);
                    if (decision > 0)
                        job.Decision(decision, (int)Session["UserID"], 3, "汇总单批量未能审批通过!");    //3:审批未通过
                }
            }
        }


        BindGrid();
        MessageBox.Show(this, "已成功将选择区域的申请单，设为批复未通过！");
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        DoApprove(2, "汇总单批量审批通过!");
    }
    protected void btn_UnApprove_Click(object sender, EventArgs e)
    {
        DoApprove(3, "汇总单批量未能审批通过!");
    }
    private void DoApprove(int State, string remark)
    {
        foreach (GridViewRow gr in gv_ListDetail.Rows)
        {
            if (gr.FindControl("chk_ID") != null && ((CheckBox)gr.FindControl("chk_ID")).Checked)
            {
                int taskid = (int)gv_ListDetail.DataKeys[gr.RowIndex]["FNA_FeeApply_ApproveTask"];
                if (taskid > 0)
                {
                    int jobid = EWF_TaskBLL.StaffCanApproveTask(taskid, (int)Session["UserID"]);
                    if (jobid > 0)
                    {
                        EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                        if (job != null)
                        {
                            int decisionid = job.StaffCanDecide((int)Session["UserID"]);
                            if (decisionid > 0)
                            {
                                if (State == 2)
                                    job.Decision(decisionid, (int)Session["UserID"], 2, "汇总单批量审批通过!");         //2:审批已通过
                                else
                                    job.Decision(decisionid, (int)Session["UserID"], 3, "汇总单批量审批不通过!");       //3:审批不通过
                            }
                        }
                    }
                }
            }
        }
        BindGrid();
        MessageBox.Show(this, "审批成功！");
    }

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        UC_GridView gv;
        if (gv_List.Visible)
            gv = gv_List;
        else
            gv = gv_ListDetail;

        gv.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

}
