using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
using MCSFramework.UD_Control;


public partial class SubModule_FNA_FeeWriteoff_FeeWriteOffSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                    ddl_State.SelectedValue = Request.QueryString["State"];
            }
            BindDropDown();

            tr_AccountTitle.Enabled = gv_ListDetail.Visible;
            ddl_WriteOffCostOP.Enabled = gv_ListDetail.Visible;
            tbx_WriteOffCost.Enabled = gv_ListDetail.Visible;

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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<=GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

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

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int feetype = int.Parse(ddl_FeeType.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);

        if (MCSTabControl1.SelectedIndex == 0)
        {
            #region 显示汇总单数据源
            Dictionary_Data dicFeeType = null;
            if (feetype > 0) dicFeeType = DictionaryBLL.GetDicCollections("FNA_FeeType")[feetype.ToString()];
            #region 额外条件
            string condition = "";
            //费用代垫客户
            if (select_Client.SelectValue != "")
            {
                condition += " AND FNA_FeeWriteOff.InsteadPayClient=" + select_Client.SelectValue;
            }

            //费用代垫员工
            if (Select_InsteadPayStaff.SelectValue != "")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeWriteOff',FNA_FeeWriteOff.ExtPropertys,'InsteadPayStaff')=" + Select_InsteadPayStaff.SelectValue;
            }

            //核销申请人
            if (Select_InsertStaff.SelectValue != "")
            {
                condition += " AND FNA_FeeWriteOff.InsertStaff=" + Select_InsertStaff.SelectValue;
            }
            #endregion
            DataTable dtSummary = FNA_FeeWriteOffBLL.GetSummaryTotal(month, organizecity, level, feetype, state, int.Parse(Session["UserID"].ToString()), condition);
            if (dtSummary.Rows.Count == 0)
            {
                gv_List.DataBind();
                return;
            }

            #region 矩阵化数据表，扩展表数据列
            dtSummary.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
            if (level < 10)
            {
                foreach (DataRow row in dtSummary.Rows)
                {
                    row["ID"] = row["OrganizeCity"];
                }
                dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称" },
                    new string[] { "FeeTypeName", "AccountTitleName" }, "WriteOffCost", true, true);
            }
            else
            {
                if (level == 10)
                {
                    //按经销商查看
                    #region 将经销商的所属管理片区赋至管理片区列
                    Dictionary<int, string> dicFullOrganizeCityName = new Dictionary<int, string>();
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        row["ID"] = row["经销商ID"] == DBNull.Value ? 0 : row["经销商ID"];
                        row["经销商名称"] = row["经销商名称"] == DBNull.Value ? "无" : row["经销商名称"];

                        if ((int)row["ID"] > 0)
                        {
                            CM_Client c = new CM_ClientBLL((int)row["ID"]).Model;
                            if (c != null)
                            {
                                if (!dicFullOrganizeCityName.ContainsKey(c.OrganizeCity))
                                {
                                    dicFullOrganizeCityName.Add(c.OrganizeCity, TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", c.OrganizeCity));
                                }
                                row["管理片区名称"] = dicFullOrganizeCityName[c.OrganizeCity];
                            }
                        }
                    }
                    #endregion

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称", "经销商名称" },
                       new string[] { "FeeTypeName", "AccountTitleName" }, "WriteOffCost", true, true);
                }
                else if (level == 20)
                {
                    //按门店查看
                    #region 将门店的所属管理片区赋至管理片区列
                    Dictionary<int, string> dicFullOrganizeCityName = new Dictionary<int, string>();
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        row["ID"] = row["客户ID"] == DBNull.Value ? 0 : row["客户ID"];
                        row["客户名称"] = row["客户名称"] == DBNull.Value ? "无" : row["客户名称"];

                        if ((int)row["ID"] > 0)
                        {
                            CM_Client c = new CM_ClientBLL((int)row["ID"]).Model;
                            if (c != null)
                            {
                                if (!dicFullOrganizeCityName.ContainsKey(c.OrganizeCity))
                                {
                                    dicFullOrganizeCityName.Add(c.OrganizeCity, TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", c.OrganizeCity));
                                }
                                row["管理片区名称"] = dicFullOrganizeCityName[c.OrganizeCity];
                            }
                        }
                    }
                    #endregion

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "ID", "管理片区名称", "客户名称" },
                       new string[] { "FeeTypeName", "AccountTitleName" }, "WriteOffCost", true, true);
                }
            }
            dtSummary = MatrixTable.ColumnSummaryTotal(dtSummary, new int[] { 1 }, new string[] { "WriteOffCost" });
            #endregion

            #region 暂不计算分析数据
            //dtSummary.Columns["合计"].ColumnName = "费用合计";
            //dtSummary.Columns.Add("预计销量", Type.GetType("System.Decimal"));

            //if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
            //{
            //    dtSummary.Columns.Add("预算总额", Type.GetType("System.Decimal"));
            //    dtSummary.Columns.Add("其中扩增额", Type.GetType("System.Decimal"));
            //    dtSummary.Columns.Add("预算余额", Type.GetType("System.Decimal"));
            //}

            //if (dicFeeType != null && dicFeeType.Description == "FeeRateControl")
            //    dtSummary.Columns.Add("上月余额", Type.GetType("System.Decimal"));

            //dtSummary.Columns.Add("费点", Type.GetType("System.String"));


            //decimal sumTotalVolume = 0, sumTotalBudget = 0, sumExtBudget = 0, sumPreMonthBudgetBalance = 0, sumUsableAmount = 0;
            //foreach (DataRow row in dtSummary.Rows)
            //{
            //    int id = 0;

            //    if (int.TryParse(row[0].ToString(), out id) && id > 0)
            //    {
            //        bool includechild = true;
            //        if (level < 10 && new Addr_OrganizeCityBLL(id).Model.Level < level) includechild = false;

            //        #region 计算费点
            //        if (level < 10)
            //            row["预计销量"] = SVM_SalesForcastBLL.GetTotalVolume(month, id, includechild);
            //        else if (level == 10 || level == 20)
            //            row["预计销量"] = SVM_SalesForcastBLL.GetTotalVolumeByClient(month, id);

            //        sumTotalVolume += (decimal)row["预计销量"];

            //        if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
            //        {
            //            row["预算总额"] = (FNA_BudgetBLL.GetSumBudgetAmount(month, id, feetype, includechild) + FNA_BudgetBLL.GetSumBudgetAmount(month, id, 0, includechild));
            //            sumTotalBudget += (decimal)row["预算总额"];

            //            row["其中扩增额"] = (FNA_BudgetExtraApplyBLL.GetExtraAmount(month, id, feetype, includechild) + FNA_BudgetExtraApplyBLL.GetExtraAmount(month, id, 0, includechild));
            //            sumExtBudget += (decimal)row["其中扩增额"];

            //            row["预算余额"] = (FNA_BudgetBLL.GetUsableAmount(month, id, feetype, includechild) + FNA_BudgetBLL.GetUsableAmount(month, id, 0, includechild));
            //            sumUsableAmount += (decimal)row["预算余额"];
            //        }

            //        if (dicFeeType != null && dicFeeType.Description == "FeeRateControl" && level < 10)
            //        {
            //            row["上月余额"] = FNA_BudgetBLL.GetUsableAmount(month - 1, id, feetype, includechild);
            //            sumPreMonthBudgetBalance += (decimal)row["上月余额"];

            //            if ((decimal)row["预计销量"] != 0)
            //                row["费点"] = (((decimal)row["费用合计"] - (decimal)row["上月余额"]) / (decimal)row["预计销量"]).ToString("0.00%");
            //        }
            //        else if ((decimal)row["预计销量"] != 0)
            //            row["费点"] = ((decimal)row["费用合计"] / (decimal)row["预计销量"]).ToString("0.00%");
            //        #endregion
            //    }

            //    #region 求合计行
            //    if (id == 0)
            //    {
            //        row["预计销量"] = sumTotalVolume.ToString("0.00");

            //        #region 预算总额及余额
            //        if (dicFeeType != null && dicFeeType.Description == "BudgetControl" && level < 10)
            //        {
            //            row["预算总额"] = sumTotalBudget.ToString("0.00");
            //            row["其中扩增额"] = sumExtBudget.ToString("0.00");
            //            row["预算余额"] = sumUsableAmount.ToString("0.00");
            //        }
            //        #endregion

            //        #region 费点
            //        if (sumTotalVolume != 0)
            //        {
            //            if (dicFeeType != null && dicFeeType.Description == "FeeRateControl" && level < 10)
            //            {
            //                row["上月余额"] = sumPreMonthBudgetBalance;
            //                row["费点"] = (((decimal)row["费用合计"] - sumPreMonthBudgetBalance) / sumTotalVolume).ToString("0.##%");
            //            }
            //            else
            //                row["费点"] = ((decimal)row["费用合计"] / sumTotalVolume).ToString("0.##%");
            //        }
            //        #endregion
            //    }
            //    #endregion

            //}
            #endregion

            #endregion

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
        }
        else
        {
            string condition = "1=1";

            #region 组织明细记录的查询条件
            //管理片区及所有下属管理片区
            if (tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND FNA_FeeWriteOff.OrganizeCity IN (" + orgcitys + ")";
            }

            //会计月条件
            condition += " AND FNA_FeeWriteOff.AccountMonth = " + ddl_Month.SelectedValue;

            int accounttile = 0;
            int.TryParse(tr_AccountTitle.SelectValue, out accounttile);
            decimal _cost = 0;
            decimal.TryParse(tbx_WriteOffCost.Text, out _cost);
            //费用类型
            if (ddl_FeeType.SelectedValue != "0" && !(accounttile > 1))
            {
                IList<AC_AccountTitleInFeeType> titles = AC_AccountTitleInFeeTypeBLL.GetModelList("FeeType=" + ddl_FeeType.SelectedValue);
                string ids = "";
                foreach (AC_AccountTitleInFeeType item in titles)
                {
                    ids += item.AccountTitle + ",";
                }
                if (ids.EndsWith(",")) ids = ids.Substring(0, ids.Length - 1);

                condition += " AND FNA_FeeWriteOff.ID IN (SELECT WriteOffID FROM MCS_FNA.dbo.FNA_FeeWriteOffDetail WHERE AccountTitle IN(" + ids + ") AND FNA_FeeWriteOffDetail.WriteOffID=FNA_FeeWriteOff.ID)";
            }

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
                    condition += " AND FNA_FeeWriteOff.ID IN (SELECT WriteOffID FROM MCS_FNA.dbo.FNA_FeeWriteOffDetail WHERE AccountTitle IN(" + ids + ") AND FNA_FeeWriteOffDetail.WriteOffID=FNA_FeeWriteOff.ID)";
                else
                    condition += " AND FNA_FeeWriteOff.ID IN (SELECT WriteOffID FROM MCS_FNA.dbo.FNA_FeeWriteOffDetail WHERE AccountTitle IN(" + ids + ") AND (WriteOffCost+AdjustCost)" + ddl_WriteOffCostOP.SelectedValue + "  " + _cost.ToString() + " AND FNA_FeeWriteOffDetail.WriteOffID=FNA_FeeWriteOff.ID)";
            }
            else if (_cost != 0)
            {   //核销金额判断
                condition += " AND FNA_FeeWriteOff.ID IN (SELECT WriteOffID FROM MCS_FNA.dbo.FNA_FeeWriteOffDetail WHERE (WriteOffCost+AdjustCost)" + ddl_WriteOffCostOP.SelectedValue + "  " + _cost.ToString() + " AND FNA_FeeWriteOffDetail.WriteOffID=FNA_FeeWriteOff.ID)";
            }

            //审批状态
            if (ddl_State.SelectedValue == "0")
                condition += " AND FNA_FeeWriteOff.State IN (2,3) ";
            else if (ddl_State.SelectedValue == "1")
                condition +=
                @" AND FNA_FeeWriteOff.State = 2 AND FNA_FeeWriteOff.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
    MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
    EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";
            else if (ddl_State.SelectedValue == "2")
                condition += " AND FNA_FeeWriteOff.State = 3 ";
            else if (ddl_State.SelectedValue == "3")
            {
                AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
                condition +=
                @" AND FNA_FeeWriteOff.State IN (2,3) AND FNA_FeeWriteOff.ApproveTask IN 
(SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
	MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
	EWF_Task_JobDecision.DecisionResult IN(2,5,6) AND 
	EWF_Task_JobDecision.DecisionTime BETWEEN DATEADD(month,-1,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"') AND 
		DATEADD(month,3,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"'))";
            }

            //费用代垫客户
            if (select_Client.SelectValue != "")
            {
                condition += " AND FNA_FeeWriteOff.InsteadPayClient=" + select_Client.SelectValue;
            }

            //费用代垫员工
            if (Select_InsteadPayStaff.SelectValue != "")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeWriteOff',FNA_FeeWriteOff.ExtPropertys,'InsteadPayStaff')=" + Select_InsteadPayStaff.SelectValue;
            }

            //核销申请人
            if (Select_InsertStaff.SelectValue != "")
            {
                condition += " AND FNA_FeeWriteOff.InsertStaff=" + Select_InsertStaff.SelectValue;
            }
            #endregion

            gv_ListDetail.ConditionString = condition;
            gv_ListDetail.BindGrid();
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            gv_List.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow r in gv_List.Rows)
            {
                r.Cells[1].Visible = false;

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
            }
        }
    }

    protected void gv_ListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_ListDetail.DataKeys[e.Row.RowIndex][0];
            UC_GridView gv_Detail = (UC_GridView)e.Row.FindControl("gv_Detail");
            if (gv_Detail != null)
            {
                IList<FNA_FeeWriteOffDetail> lists = new FNA_FeeWriteOffBLL(id).Items;

                //费用类型
                int accounttile = 0;
                int.TryParse(tr_AccountTitle.SelectValue, out accounttile);
                if (ddl_FeeType.SelectedValue != "0" && !(accounttile > 1))
                {
                    IList<AC_AccountTitleInFeeType> titles = AC_AccountTitleInFeeTypeBLL.GetModelList("FeeType=" + ddl_FeeType.SelectedValue);
                    int[] ids = new int[titles.Count];
                    for (int i = 0; i < titles.Count; i++)
                    {
                        ids[i] = titles[i].AccountTitle;
                    }
                    lists = lists.Where(p => ids.Contains(p.AccountTitle)).ToList();
                }

                //费用科目
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

                //核销金额判断
                decimal _cost = 0;
                decimal.TryParse(tbx_WriteOffCost.Text, out _cost);
                if (_cost != 0)
                {
                    if (ddl_WriteOffCostOP.SelectedValue == ">")
                        lists = lists.Where(p => p.WriteOffCost + p.AdjustCost > _cost).ToList();
                    else if (ddl_WriteOffCostOP.SelectedValue == "<")
                        lists = lists.Where(p => p.WriteOffCost + p.AdjustCost < _cost).ToList();
                }

                gv_Detail.BindGrid(lists);
            }
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.Visible = MCSTabControl1.SelectedIndex == 0;
        gv_ListDetail.Visible = !gv_List.Visible;

        tr_AccountTitle.Enabled = gv_ListDetail.Visible;
        ddl_WriteOffCostOP.Enabled = gv_ListDetail.Visible;
        tbx_WriteOffCost.Enabled = gv_ListDetail.Visible;

        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
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

        string filename = HttpUtility.UrlEncode("费用核销汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    #region 仅查看待我审批的费用核销单
    private string GetNeedMeApproveTaskIDs()
    {
        string taskids = "";

        DataTable dt = EWF_Task_JobBLL.GetJobToDecision(int.Parse(Session["UserID"].ToString()));
        dt.DefaultView.RowFilter = "AppCode='FNA_FeeWriteoffFlow'";
        if (dt.DefaultView.Count == 0) return "";

        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            taskids += dt.DefaultView[i]["TaskID"].ToString() + ",";
        }

        if (taskids.EndsWith(",")) taskids = taskids.Substring(0, taskids.Length - 1);

        return taskids;

    }
    #endregion


    protected string GetISDelay(int FeeApplyDetailID)
    {
        if (FeeApplyDetailID == 0) return "";
        FNA_FeeApplyDetail detail = new FNA_FeeApplyBLL().GetDetailModel(FeeApplyDetailID);
        int month = int.Parse(ddl_Month.SelectedValue);
        return month > detail.LastWriteOffMonth ? "<font color=red>逾期" + (month - detail.LastWriteOffMonth).ToString() + "个月</font>" : "<font color=blue></font>";
    }


    protected void gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Approved" && e.CommandName != "UnApproved")
        {
            BindGrid();
            return;
        }
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

        int organizecity = int.Parse(e.CommandArgument.ToString());
        string condition = " FNA_FeeWriteOff.State = 2 ";

        #region 组织明细记录的查询条件
        //管理片区及所有下属管理片区
        if (organizecity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += organizecity.ToString();

            condition += " AND FNA_FeeWriteOff.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND FNA_FeeWriteOff.AccountMonth = " + ddl_Month.SelectedValue;

        #endregion
        if (e.CommandName == "Approved")
        {

            IList<FNA_FeeWriteOff> lists = FNA_FeeWriteOffBLL.GetModelList(condition);
            foreach (FNA_FeeWriteOff fee in lists)
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
        else if (e.CommandName == "UnApproved")
        {

            IList<FNA_FeeWriteOff> lists = FNA_FeeWriteOffBLL.GetModelList(condition);
            foreach (FNA_FeeWriteOff fee in lists)
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
    }

}
