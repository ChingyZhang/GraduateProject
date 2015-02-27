using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.IO;
using System.Text;

public partial class SubModule_FNA_FeeApply_ApplySummary_FLFeeApplySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);

            BindDropDown();

            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                {
                    ddl_State.SelectedValue = Request.QueryString["State"];
                }
            }
            if (Request.QueryString["Selected"] != null)
            {
                int selected = 0;
                if (int.TryParse(Request.QueryString["Selected"], out selected) && selected==1)
                {
                    MCSTabControl1.SelectedIndex = 1;
                }
            }
            
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGrid();
        Timer1.Enabled = false;
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (int)ViewState["AccountMonth"] == 0 ? (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString() : ViewState["AccountMonth"].ToString();

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
    }


    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) >= city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0)
            {
                ddl_Level.Items.Add(new ListItem("本级", city.Level.ToString()));
            }
        }
        BindGrid();
    }
    #endregion
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int state = int.Parse(ddl_State.SelectedValue);
        if (MCSTabControl1.SelectedTabItem.Value == "0")
        {
        #region 显示汇总数据
            gv_List.Visible = true; gv_DetailList.Visible = false;
            DataTable dtSummary = FNA_FeeApplyBLL.GetFLFeeSummary2(month, organizecity, int.Parse(ddl_Level.SelectedValue), state, int.Parse(Session["UserID"].ToString()));
            if (dtSummary.Rows.Count == 0)
            {
                gv_List.DataBind();
                return;
            }

            //dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区名称", "行政属性" },
            //             new string[] { "RTChannel", "DisplayFeeType", "Title" }, "Summary", true, false);
            //MatrixTable.TableAddRowSubTotal_Matric(dtSummary, new string[] { "管理片区名称" }, 2, new string[] { "RTChannel", "DisplayFeeType", "Title" }, false);
            
            #region 重新计算总计行的费率
            //if (dtSummary.Rows.Count > 1)
            //{
            //    foreach (DataRow dr in dtSummary.Rows)
            //    {
            //        if (dr[1].ToString().EndsWith("计") || dr[2].ToString().EndsWith("计"))
            //        {
            //            foreach (DataColumn dc in dtSummary.Columns)
            //            {
            //                if (dc.ColumnName.EndsWith("费率%"))
            //                {
            //                    string title = dc.ColumnName;
            //                    int pos = title.IndexOf('→');
            //                    if (pos > 0)
            //                    {
            //                        title = title.Substring(0, pos);
            //                        if (dtSummary.Columns.Contains(title + "→1.返利费总计(元/月)→A.我司承担") &&
            //                            dtSummary.Columns.Contains(title + "→2.销量及费率→A.实际进货(元/月)") &&
            //                            (decimal)dr[title + "→2.销量及费率→A.实际进货(元/月)"] != 0)
            //                        {
            //                            dr[dc.ColumnName] = Math.Round((decimal)dr[title + "→1.返利费总计(元/月)→A.我司承担"] / (decimal)dr[title + "→2.销量及费率→A.实际进货(元/月)"] * 100, 1, MidpointRounding.AwayFromZero);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            gv_List.DataSource = dtSummary;
            gv_List.DataBind();

            if (dtSummary.Columns.Count >= 24)
                gv_List.Width = new Unit(dtSummary.Columns.Count * 65);
            else
                gv_List.Width = new Unit(100, UnitType.Percentage);

            MatrixTable.GridViewMatric(gv_List);

            MatrixTable.GridViewMergSampeValueRow(gv_List, 0);

        #endregion
        }
        else
        {
            gv_List.Visible = false; gv_DetailList.Visible = true;
            if (ddl_State.SelectedValue=="1" && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1510, "BatApproveFee"))
            {
                bt_Approve.Visible = false;
                bt_UnApprove.Visible = true;
                bt_Approve.OnClientClick = "return confirm('是否确认将所选中所有申请单批量设为审批通过？注意该操作可能耗时较长，请耐心等待！')";
                bt_UnApprove.OnClientClick = "return confirm('是否确认将所选中所有申请单批量设为审批不通过？注意该操作可能耗时较长，请耐心等待！')";
            }
            #region 组织明细查询条件
            string condition = "";
            condition += @"FNA_FeeApply.FeeType=7 AND EXISTS(SELECT 1 FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE FNA_FeeApplyDetail.AccountTitle=82
                AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID AND BeginMonth=" + ddl_Month.SelectedValue + ")";
            if (tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += organizecity;

                condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
            }
            if (state == 0)
                condition += " AND FNA_FeeApply.State IN (2,3) ";
            else if (state == 1)
                condition +=
                @" AND	FNA_FeeApply.State = 2 AND 
			EXISTS (
				SELECT EWF_Task_Job.Task
				FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
					MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
					 INNER JOIN MCS_EWF.dbo.EWF_Task ON	 EWF_Task_Job.Task= EWF_Task.ID AND EWF_Task.App='4eb9e905-3502-4caf-88d0-aadcfec6e4dd'
				WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
					EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3 AND FNA_FeeApply.ApproveTask=EWF_Task_Job.Task)";
            else if (state == 2)
                condition += " AND FNA_FeeApply.State = 3 ";
            else if (state == 3)
            {
                AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
                condition +=
                @" AND FNA_FeeApply.State IN (2,3) AND EXISTS 
            (SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
	        MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
            INNER JOIN MCS_EWF.dbo.EWF_Task ON=EWF_Task_Job.Task=EWF_Task.ID AND EWF_Task.App='4eb9e905-3502-4caf-88d0-aadcfec6e4dd'
            WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
	            EWF_Task_JobDecision.DecisionResult IN(2,5,6) AND FNA_FeeApply.ApproveTask=EWF_Task_Job.Task)";
            }
            #endregion
            gv_DetailList.ConditionString = condition;
            gv_DetailList.BindGrid();
          
        }


        #region 是否可以批量审批
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1510, "BatApproveFee"))
        {
            bt_Approve.Visible = (ddl_State.SelectedValue == "1");
            bt_UnApprove.Visible = (ddl_State.SelectedValue == "1");
            bt_Approve.Enabled = (ddl_State.SelectedValue == "1");
            bt_UnApprove.Enabled = (ddl_State.SelectedValue == "1");

            #region 判断费用申请进度
            if (ddl_State.SelectedValue == "1")
            {
                Org_StaffBLL _staff = new Org_StaffBLL((int)Session["UserID"]);
                DataTable dt = _staff.GetLowerPositionTask(2, int.Parse(tr_OrganizeCity.SelectValue), month);
                if (AC_AccountMonthBLL.GetCurrentMonth() - 1 <= int.Parse(ddl_Month.SelectedValue))
                {

                    string[] allowdays1 = Addr_OrganizeCityParamBLL.GetValueByType(1, 5).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                    string[] allowdays2 = Addr_OrganizeCityParamBLL.GetValueByType(1, 6).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
                    string date = DateTime.Now.Day.ToString();
                    if (allowdays1.Contains(date))
                        bt_Approve.Enabled = false;
                    else if (allowdays2.Contains(date))
                    {
                        DataTable dt2 = new DataTable();
                        if (_staff.Model.Position == 210)
                            dt2 = _staff.GetFillProcessDetail(2);
                        if (dt.Rows.Count > 0 || dt2.Rows.Count > 0)
                        {
                            bt_Approve.Enabled = false;
                        }
                    }
                    else
                    {
                        bt_UnApprove.Enabled = false;
                    }

                }
                else
                {
                    bt_UnApprove.Enabled = false;
                }
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/Pop_ShowLowerPositionTask.aspx") +
                       "?Type=2&StaffID=0&Month=" + ddl_Month.SelectedValue + "&City=" + tr_OrganizeCity.SelectValue + "&tempid='+tempid, window, 'dialogWidth:520px;DialogHeight=600px;status:yes;resizable=no');</script>", false);
                    bt_Approve.Enabled = false;

                }
            }
            #endregion
        }
        else
        {
            bt_Approve.Visible = false;
            bt_UnApprove.Visible = false;
        }
        #endregion

    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_List.Rows)
            {
                #region 金额数据格式化
                if (r.Cells.Count > 2)
                {
                    for (int i = 2; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            r.Cells[i].Text = d.ToString("0.##");
                        }
                    }
                }
                #endregion
            }
        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    #region 批量审批操作
    protected void bt_Approve_Click(object sender, EventArgs e)
    {

        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int counts = 0;
        if (gv_List.Visible)
            counts = DoApprove(organizecity, true);
        else
            counts = DoApprove(true);
        BindGrid();
        MessageBox.Show(this, "已成功将" + counts.ToString() + "个申请单，设为审批通过！");

    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int counts = 0;
        if (gv_List.Visible)
            counts = DoApprove(organizecity, false);
        else
            counts = DoApprove(false);

        BindGrid();
        MessageBox.Show(this, "已成功将" + counts.ToString() + "个申请单，设为审批未通过！");
    }
    protected int DoApprove(bool ApproveFlag)
    {
        int counts = 0;
        foreach(GridViewRow row in gv_DetailList.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbx = row.FindControl("cb_Selected") as CheckBox;
                if (cbx.Checked)
                {
                    int taskid = (int)gv_DetailList.DataKeys[row.RowIndex]["FNA_FeeApply_ApproveTask"];
                    int jobid = EWF_TaskBLL.StaffCanApproveTask(taskid, (int)Session["UserID"]);
                    if (jobid > 0)
                    {
                        EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                        if (job != null)
                        {
                            int decisionid = job.StaffCanDecide((int)Session["UserID"]);
                            if (decisionid > 0)
                            {
                                if (ApproveFlag)
                                    job.Decision(decisionid, (int)Session["UserID"], 2, "汇总单批量审批通过!");       //2:审批已通过
                                else
                                    job.Decision(decisionid, (int)Session["UserID"], 3, "汇总单批量审批不通过!");       //3:审批不通过
                                counts++;
                            }
                        }
                    }
                }
            }
        }
        return counts;
    }
    protected int DoApprove(int OrganizeCity, bool ApproveFlag)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int state = 1;              //待我审批
        int RTChannel = 0;          //所有渠道
        DataTable dtSummary = FNA_FeeApplyBLL.GetRTChannelFLFee
            (month, OrganizeCity, state, int.Parse(Session["UserID"].ToString()), RTChannel);

        int counts = 0;
        if (dtSummary != null)
        {
            string TaskColumnName = "";
            foreach (DataColumn c in dtSummary.Columns)
            {
                if (c.ColumnName.EndsWith("→审批工作流"))
                {
                    TaskColumnName = c.ColumnName;
                    break;
                }
            }
            if (TaskColumnName == "")
            {
                MessageBox.Show(this, "未找到列名[审批工作流]的数据列!");
                return -1;
            }

            IList<int> TaskIDs = new List<int>();
            foreach (DataRow row in dtSummary.Rows)
            {
                int taskid = (int)row["费用情况→审批工作流"];
                if (TaskIDs.Contains(taskid)) continue;

                TaskIDs.Add(taskid);

                int jobid = EWF_TaskBLL.StaffCanApproveTask(taskid, (int)Session["UserID"]);
                if (jobid > 0)
                {
                    EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                    if (job != null)
                    {
                        int decisionid = job.StaffCanDecide((int)Session["UserID"]);
                        if (decisionid > 0)
                        {
                            if (ApproveFlag)
                                job.Decision(decisionid, (int)Session["UserID"], 2, "汇总单批量审批通过!");       //2:审批已通过
                            else
                                job.Decision(decisionid, (int)Session["UserID"], 3, "汇总单批量审批不通过!");       //3:审批不通过

                            counts++;
                        }
                    }
                }
            }
        }

        return counts;
    }
    #endregion

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0": 
                BindGrid();
                break;
            case "1":
                BindGrid();
                break;
            case "2":
                Response.Redirect("GetRTChannelFLFee.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue + "&State=" + ddl_State.SelectedValue);
                break;
        }
    }

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_List.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
}
