using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CSO;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using System.Data;
using MCSFramework.Common;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.Pub;
using MCSFramework.Model.CSO;

public partial class SubModule_CSO_CSO_OfferBalance_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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

            //ddl_ApplyCostOP.Enabled = gv_ListDetail.Visible;
            //tbx_ApplyCost.Enabled = gv_ListDetail.Visible;

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定管理片区
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
        if ((int)ViewState["OrganizeCity"] > 0)
        {
            tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
        }
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>DateAdd(year,-2,getdate()) AND EndDate<=Getdate()");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10)) - 1).ToString();
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
            ddl_Level.Items.Add(new ListItem("VIP", "20"));
        }
    }
    #endregion

    private void BindGrid()
    {
        int month = int.Parse(ddl_AccountMonth.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);

        decimal _cost = 0;
        decimal.TryParse(tbx_ApplyCost.Text, out _cost);

        if (MCSTabControl1.SelectedIndex == 0)
        {
            DataTable dtSummary = CSO_OfferBalanceBLL.Summary(month, organizecity, level, state, (int)Session["UserID"]);
            #region 矩阵化数据表，扩展表数据列
            dtSummary.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
            if (level < 10)
            {
                foreach (DataRow row in dtSummary.Rows)
                {
                    row["ID"] = row["OrganizeCity"];
                }
                dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称" },
                    new string[] { "结算标准", "派发品牌", "派发产品" }, new string[] { "有效名单", "支付费用" }, false, true);
            }
            else if (level == 10)
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
                   new string[] { "结算标准", "派发品牌", "派发产品" }, new string[] { "有效名单", "支付费用" }, false, true);
            }
            else if (level == 20)
            {
                //按医生查看
                #region 将医生的ID赋至表ID列
                foreach (DataRow row in dtSummary.Rows)
                {
                    row["ID"] = row["VIPID"] == DBNull.Value ? 0 : row["VIPID"];
                    row["VIP姓名"] = row["VIP姓名"] == DBNull.Value ? "无" : row["VIP姓名"];
                }
                #endregion

                dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称", "经销商名称", "VIP姓名" },
                   new string[] { "结算标准", "派发品牌", "派发产品" }, new string[] { "有效名单", "支付费用" }, false, true);
            }
            #endregion

            if (dtSummary != null && dtSummary.Rows.Count > 0)
            {
                dtSummary = MatrixTable.ColumnSummaryTotal(dtSummary, new int[] { 1 }, new string[] { "有效名单", "支付费用" });

                #region 增加本月新客数
                string thismonthnewclientcount = "Convert([3.新客标准→小计→→有效名单], 'System.Int32')";
                if (dtSummary.Columns.Contains("4.3段新客标准→小计→→有效名单"))
                    thismonthnewclientcount += " + Convert([4.3段新客标准→小计→→有效名单], 'System.Int32')";
                
                string thismonthnewclientfee = "Convert([3.新客标准→小计→→支付费用], 'System.Decimal')";
                if (dtSummary.Columns.Contains("4.3段新客标准→小计→→支付费用"))
                    thismonthnewclientfee += " + Convert([4.3段新客标准→小计→→支付费用], 'System.Decimal')";
                
                dtSummary.Columns.Add(new DataColumn("本月新客户数", Type.GetType("System.Int32"), thismonthnewclientcount));
                dtSummary.Columns.Add(new DataColumn("本月新客户费", Type.GetType("System.Decimal"), thismonthnewclientfee));
                #endregion

                #region 获取上月新客
                dtSummary.Columns.Add(new DataColumn("上月新客数", Type.GetType("System.Int32")));
                dtSummary.Columns.Add(new DataColumn("上月新客费", Type.GetType("System.Decimal")));
                string ids = "";
                foreach (DataRow row in dtSummary.Rows)
                {
                    ids += row["ID"].ToString() + ",";
                }
                if (ids.EndsWith(",")) ids = ids.Substring(0, ids.Length - 1);
                DataTable dtPreMonth = null;
                if (level < 10)
                    dtPreMonth = CSO_OfferBalanceBLL.GetNewClientAmountAndFee_ByMonthAndOrganzieCity(month - 1, ids);
                else if (level == 10)
                    dtPreMonth = CSO_OfferBalanceBLL.GetNewClientAmountAndFee_ByMonthAndDistributors(month - 1, ids);
                else if (level == 20)
                    dtPreMonth = CSO_OfferBalanceBLL.GetNewClientAmountAndFee_ByMonthAndDoctors(month - 1, ids);

                if (dtPreMonth != null)
                {
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        DataRow[] selectedrows = dtPreMonth.Select("ID=" + row["ID"].ToString());
                        if (selectedrows.Length > 0)
                        {
                            row["上月新客数"] = (int)selectedrows[0]["EffectiveAmount"];
                            row["上月新客费"] = (decimal)selectedrows[0]["PayFee"];
                        }
                        else
                        {
                            row["上月新客数"] = 0;
                            row["上月新客费"] = 0;
                        }
                    }
                }
                #region 增加上月新增增长比较
                dtSummary.Columns.Add("较上月新客增长量", Type.GetType("System.Int32"), "CONVERT([本月新客户数],'System.Int32') - [上月新客数]");
                dtSummary.Columns.Add("较上月新客增长率(%)", Type.GetType("System.String"),
                    "IIF([上月新客数]=0,'-',CONVERT(CONVERT((CONVERT([本月新客户数],'System.Int32') - [上月新客数])*100/[上月新客数],'System.Int32'),'System.String'))+'%'");
                #endregion
                #endregion


                #region 按金额条件过滤
                if (_cost != 0)
                {
                    switch (ddl_ComparerField.SelectedValue)
                    {
                        case "1":
                            dtSummary.DefaultView.RowFilter = "[1.派样185标准→小计→→有效名单]" + ddl_ApplyCostOP.SelectedValue + _cost;
                            break;
                        case "2":
                            dtSummary.DefaultView.RowFilter = "[2.派样400标准→小计→→有效名单]" + ddl_ApplyCostOP.SelectedValue + _cost;
                            break;
                        case "3":
                            dtSummary.DefaultView.RowFilter = "[本月新客户数]" + ddl_ApplyCostOP.SelectedValue + _cost;
                            break;
                        case "4":
                            dtSummary.DefaultView.RowFilter = "[合计→支付费用]" + ddl_ApplyCostOP.SelectedValue + _cost;
                            break;
                        case "5":
                            dtSummary.DefaultView.RowFilter = "[较上月新客增长量]" + ddl_ApplyCostOP.SelectedValue + _cost;
                            break;
                        case "6":
                            dtSummary.DefaultView.RowFilter = "上月新客数>0 AND (([较上月新客增长量]*100/[上月新客数])" + ddl_ApplyCostOP.SelectedValue + _cost + ")";
                            break;
                        default:
                            break;
                    }
                    dtSummary = dtSummary.DefaultView.ToTable();
                }
                #endregion

                #region 增加合计行
                List<string> valuecolumns = new List<string>();
                valuecolumns.Add("ID");
                valuecolumns.Add("上月新客数");
                valuecolumns.Add("上月新客费");
                foreach (DataColumn c in dtSummary.Columns)
                {
                    if (c.ColumnName.Contains("→")) valuecolumns.Add(c.ColumnName);
                }
                MatrixTable.TableAddSummaryRow(dtSummary, "管理片区名称", valuecolumns.ToArray());
                #endregion
            }

            gv_List.DataSource = dtSummary;
            gv_List.BindGrid();

            if (dtSummary.Columns.Count >= 24)
                gv_List.Width = new Unit(dtSummary.Columns.Count * 55);
            else
                gv_List.Width = new Unit(100, UnitType.Percentage);

            MatrixTable.GridViewMatric(gv_List);
        }
        else
        {
            #region 组织明细记录的查询条件
            string condition = "CSO_OfferBalance.AccountMonth=" + month.ToString();
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

                condition += " AND CSO_OfferBalance.OrganizeCity IN (" + orgcitys + ")";
            }
            //审批状态
            if (ddl_State.SelectedValue == "0")
                condition += " AND CSO_OfferBalance.State IN (2,3) ";
            else if (ddl_State.SelectedValue == "1")
                condition +=
                @" AND CSO_OfferBalance.State = 2 AND CSO_OfferBalance.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  [192.168.8.82].MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
    [192.168.8.82].MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
    EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";
            else if (ddl_State.SelectedValue == "2")
                condition += " AND CSO_OfferBalance.State = 3 ";
            else if (ddl_State.SelectedValue == "3")
            {
                AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
                condition +=
                @" AND CSO_OfferBalance.State IN (2,3) AND CSO_OfferBalance.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  [192.168.8.82].MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
	[192.168.8.82].MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
	EWF_Task_JobDecision.DecisionResult IN(2,5,6) AND 
	EWF_Task_JobDecision.DecisionTime BETWEEN DATEADD(month,-1,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"') AND 
		DATEADD(month,3,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"'))";
            }
            #endregion

            gv_ListDetail.ConditionString = condition;
            gv_ListDetail.BindGrid();

        }

        if (gv_List.Rows.Count > 0 || gv_ListDetail.Rows.Count > 0)
        {
            btn_Approve.Enabled = state == 1;
            btn_UnApprove.Enabled = state == 1;
        }
        else
        {
            btn_Approve.Enabled = false;
            btn_UnApprove.Enabled = false;
        }

    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = "&nbsp;";
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

                //ddl_ApplyCostOP.Enabled = gv_ListDetail.Visible;
                //tbx_ApplyCost.Enabled = gv_ListDetail.Visible;
                //btn_Approve.Visible = gv_ListDetail.Visible;
                //btn_UnApprove.Visible = gv_ListDetail.Visible;
                BindGrid();
                break;
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        if (gv_List.Visible) gv_List.PageIndex = 0;
        if (gv_ListDetail.Visible) gv_ListDetail.PageIndex = 0;
        BindGrid();
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
    private void DoApprove(int State, string Remark)
    {
        if (tbx_ApplyCost.Text != "0")
        {
            BindGrid();
            MessageBox.Show(this, "批量审批时，不可以设定申请金额条件，请将单笔申请金额设为0再批量审批!");
            return;
        }

        #region 组织明细记录的查询条件
        //会计月条件
        string condition = " CSO_OfferBalance.AccountMonth= " + ddl_AccountMonth.SelectedValue;

        //管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND CSO_OfferBalance.OrganizeCity IN (" + orgcitys + ")";
        }

        //审批状态
        if (ddl_State.SelectedValue == "1")
            condition +=
            @" AND CSO_OfferBalance.State = 2 AND CSO_OfferBalance.ApproveTask IN 
                    (SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
                        MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
                    WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
                        EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";
        #endregion

        IList<CSO_OfferBalance> list = CSO_OfferBalanceBLL.GetModelList(condition);
        foreach (CSO_OfferBalance apply in list)
        {
            DoApproveTask(apply.ApproveTask, State, Remark);
        }
        BindGrid();
        MessageBox.Show(this, "审批成功！");

    }
    private int DoApproveTask(int TaskID, int DessionResult, string DessionComment)
    {
        if (TaskID > 0)
        {
            int jobid = EWF_TaskBLL.StaffCanApproveTask(TaskID, (int)Session["UserID"]);
            if (jobid > 0)
            {
                EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                if (job != null)
                {
                    int decisionid = job.StaffCanDecide((int)Session["UserID"]);
                    if (decisionid > 0)
                    {
                        return job.Decision(decisionid, (int)Session["UserID"], DessionResult, DessionComment);
                    }
                }
            }
        }
        return -1;
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();
        ToExcel(gv_List, "ExtportFile.xls");

        gv_List.AllowPaging = true;
        BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

}
