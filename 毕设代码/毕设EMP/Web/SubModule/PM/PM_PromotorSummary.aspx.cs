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
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.Promotor;
using MCSFramework.Model.Pub;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_PM_PM_PromotorSummary : System.Web.UI.Page
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

            BindGrid();
        }
        #region 注册弹出窗口脚本
        string script = "function Pop_ShowLowerPositionTask(type,sid,City,Month){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/Pop_ShowLowerPositionTask.aspx") +
            "?Type=' + type+'&Month='+Month+'&City='+City+'&StaffID='+sid +'&tempid='+tempid, window, 'dialogWidth:520px;DialogHeight=600px;status:yes;resizable=no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pop_ShowLowerPositionTask", script, true);
        #endregion
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

        if (Request.QueryString["OrganizeCity"] != null)
        {
            tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"].ToString();
        }
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1).ToString();
        if (Request.QueryString["AccountMonth"] != null)
        {
            ddl_Month.SelectedValue = Request.QueryString["AccountMonth"].ToString();
        }


        #region 绑定导购员工资类别
        ddl_SalaryClassify.DataSource = DictionaryBLL.GetDicCollections("PM_SalaryClassify");
        ddl_SalaryClassify.DataBind();
        ddl_SalaryClassify.Items.Insert(0, new ListItem("所有", "0"));
        ddl_SalaryClassify.SelectedValue = "0";
        #endregion

        ddl_RTChannel.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Channel");
        ddl_RTChannel.DataBind();
        ddl_RTChannel.Items.Insert(0, new ListItem("所有", "0"));
        ddl_RTChannel.SelectedValue = "0";

    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) >= city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0 || tr_OrganizeCity.SelectValue == "1")
            {
                ddl_Level.Items.Insert(0, new ListItem("总部", city.Level.ToString()));
            }
        }
    }
    #endregion

    #region 仅查看待我审批的费用申请单
    private string GetNeedMeApproveTaskIDs()
    {
        string taskids = "";

        DataTable dt = EWF_Task_JobBLL.GetJobToDecision(int.Parse(Session["UserID"].ToString()));
        dt.DefaultView.RowFilter = "AppCode='PM_SalaryApplyFlow'";
        if (dt.DefaultView.Count == 0) return "";

        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            taskids += dt.DefaultView[i]["TaskID"].ToString() + ",";
        }

        if (taskids.EndsWith(",")) taskids = taskids.Substring(0, taskids.Length - 1);

        return taskids;

    }
    #endregion
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 4)
        {
            Response.Redirect("PM_GetAnalysisOverview.aspx?AccountMonth=" + ddl_Month.SelectedValue + "&OrganizeCity=" + tr_OrganizeCity.SelectValue);
        }
        gv_List.Visible = (MCSTabControl1.SelectedIndex == 0 || MCSTabControl1.SelectedIndex == 1);
        gv_ListDetail.Visible = MCSTabControl1.SelectedIndex == 2;
        gv_PromotorSalary.Visible = MCSTabControl1.SelectedIndex == 3;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int rtchannel = int.Parse(ddl_RTChannel.SelectedValue);


        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                {
                    #region 显示统计分析
                    DataTable dtSummary = PM_SalaryBLL.GetSummary(month, organizecity, level, state, int.Parse(Session["UserID"].ToString()), int.Parse(ddl_SalaryClassify.SelectedValue));
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        return;
                    }
                    //MatrixTable.TableAddSummaryRow(dtSummary, "管理片区名称",
                    //    new string[] { "导购员人数", "我司实发额", "上上月总销量", "上月总销量", "上月与上上月增长量", "本月总销量", "本月与上月增长量", "本月婴儿粉销量", "费用A", "费用B", "经销商承担薪资合计" });


                    //dtSummary.Columns.Add("婴儿粉比(%)", Type.GetType("System.Decimal"), "IIF(本月总销量=0,0,本月婴儿粉销量/本月总销量*100)").SetOrdinal(10);
                    //dtSummary.Columns.Add("费率A(%)", Type.GetType("System.Decimal"), "IIF(本月总销量=0,0,费用A/本月总销量*100)").SetOrdinal(13);
                    //dtSummary.Columns.Add("费率B(%)", Type.GetType("System.Decimal"), "IIF(本月总销量=0,0,费用B/本月总销量*100)").SetOrdinal(14);

                    //gv_List.Width = new Unit(100, UnitType.Percentage);
                    if (txt_FeeRate.Text != "0")
                    {
                        dtSummary.DefaultView.RowFilter = "[奶粉部费率A(%)]" + ddl_FeeRateOP.SelectedValue + txt_FeeRate.Text;
                    }

                    gv_List.DataSource = dtSummary.DefaultView;

                    gv_List.DataBind();
                    GridViewMergSampeValueRow(gv_List, 0, "", 0, "");
                    GridViewMergSampeValueRow(gv_List, 1, "", 0, "");
                    GridViewMergSampeValueRow(gv_List, 2, "", 0, "");
                    #endregion
                }
                break;
            case "1":
                {
                    #region 显示汇总单数据源
                    DataTable dtSummary = PM_SalaryBLL.GetSummaryTotal(month, organizecity, level, state, int.Parse(Session["UserID"].ToString()), int.Parse(ddl_SalaryClassify.SelectedValue));
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        return;
                    }
                    if (dtSummary.Columns.Count >= 24)
                        gv_List.Width = new Unit(dtSummary.Columns.Count * 65);
                    else
                        gv_List.Width = new Unit(100, UnitType.Percentage);

                    dtSummary.Columns.Add("费率A(%)", Type.GetType("System.Decimal"), "IIF(本月总销量=0,0,费用A/本月总销量*100)").SetOrdinal(dtSummary.Columns.Count - 1);
                    if (txt_FeeRate.Text != "0")
                    {
                        dtSummary.DefaultView.RowFilter = "[费率A(%)]" + ddl_FeeRateOP.SelectedValue + txt_FeeRate.Text;
                    }

                    #region 增加合计行
                    if (dtSummary.Rows.Count > 0)
                    {
                        DataRow dr = dtSummary.NewRow();
                        dr[0] = 0;
                        dr["管理片区名称"] = "合计";

                        for (int column = 2; column < dtSummary.Columns.Count - 1; column++)
                        {
                            decimal sum = 0;
                            for (int i = 0; i < dtSummary.Rows.Count; i++)
                            {
                                decimal d = 0;
                                if (decimal.TryParse(dtSummary.Rows[i][column].ToString(), out d))
                                {
                                    sum += d;
                                }
                            }
                            dr[column] = sum;
                        }
                        dtSummary.Rows.Add(dr);
                    }
                    #endregion
                    gv_List.DataSource = dtSummary.DefaultView;
                    gv_List.DataBind();

                    MatrixTable.GridViewMatric(gv_List);
                    #endregion
                }
                break;
            case "2":
                {
                    //string condition = " MCS_SYS.dbo.UF_Spilt(PM_SalaryDetail.ExtPropertys,'|',31)!='1'	";//--FlagCancel	是否取消已生成的导购人员导购工(	31)
                    string condition = " 1=1 ";

                    #region 组织明细记录的查询条件
                    //管理片区及所有下属管理片区
                    if (tr_OrganizeCity.SelectValue != "1")
                    {
                        Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                        string orgcitys = orgcity.GetAllChildNodeIDs();
                        if (orgcitys != "") orgcitys += ",";
                        orgcitys += tr_OrganizeCity.SelectValue;

                        condition += " AND PM_Salary.OrganizeCity IN (" + orgcitys + ")";
                    }
                    if (ddl_SalaryClassify.SelectedValue != "0")
                    {
                        condition += "AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',PM_Salary.ExtPropertys,'PMClassfiy')=" + ddl_SalaryClassify.SelectedValue;
                    }
                    //会计月条件
                    condition += " AND PM_Salary.AccountMonth = " + ddl_Month.SelectedValue;

                    //审批状态
                    if (ddl_State.SelectedValue == "0")
                        condition += " AND PM_Salary.State IN (2,3) ";
                    else if (ddl_State.SelectedValue == "1")
                        condition +=
                        @" AND	PM_Salary.State = 2 AND 
			MCS_SYS.dbo.UF_Spilt(PM_Salary.ExtPropertys,'|',1) IN (
				SELECT EWF_Task_Job.Task
				FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
					MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
					 INNER JOIN MCS_EWF.dbo.EWF_Task ON	 EWF_Task_Job.Task= EWF_Task.ID 
                     INNER JOIN MCS_EWF.dbo.EWF_Flow_App ON EWF_Task.App=EWF_Flow_App.ID AND EWF_Flow_App.Code='PM_SalaryApplyFlow'
				WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
					EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";
                    else if (ddl_State.SelectedValue == "2")
                        condition += " AND PM_Salary.State = 3 ";
                    else if (ddl_State.SelectedValue == "3")
                    {
                        AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
                        condition +=
                        @" AND PM_Salary.State IN (2,3) AND MCS_SYS.dbo.UF_Spilt(PM_Salary.ExtPropertys,'|',1) IN 
                (SELECT EWF_Task_Job.Task FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
	                MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
                 INNER JOIN MCS_EWF.dbo.EWF_Task ON	 EWF_Task_Job.Task= EWF_Task.ID 
                 INNER JOIN MCS_EWF.dbo.EWF_Flow_App ON EWF_Task.App=EWF_Flow_App.ID AND EWF_Flow_App.Code='PM_SalaryApplyFlow'
                WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
	                EWF_Task_JobDecision.DecisionResult IN(2,5,6) AND 
	                EWF_Task_JobDecision.DecisionTime BETWEEN DATEADD(month,-1,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"') AND 
		                DATEADD(month,3,'" + m.BeginDate.ToString("yyyy-MM-dd") + @"'))";
                    }
                    #endregion

                    gv_ListDetail.ConditionString = condition;
                    gv_ListDetail.BindGrid();
                }
                break;
            case "3":
                {
                    DataTable dtSummary = PM_SalaryBLL.GetDetailByState(month, organizecity, level, state, int.Parse(Session["UserID"].ToString()), int.Parse(ddl_SalaryClassify.SelectedValue), rtchannel);
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_PromotorSalary.DataBind();
                        return;
                    }
                    if (dtSummary.Columns.Count >= 24)
                        gv_PromotorSalary.Width = new Unit(dtSummary.Columns.Count * 65);
                    else
                        gv_PromotorSalary.Width = new Unit(100, UnitType.Percentage);
                    dtSummary.Columns.Add("所在门店").SetOrdinal(6);
                    if (txt_FeeRate.Text != "0")
                    {
                        dtSummary.DefaultView.RowFilter = "[费率A%]" + ddl_FeeRateOP.SelectedValue + txt_FeeRate.Text;
                    }
                    gv_PromotorSalary.DataSource = dtSummary.DefaultView;
                    gv_PromotorSalary.DataBind();
                    MatrixTable.GridViewMatric(gv_PromotorSalary);
                }
                break;
        }

        #region 是否可以批量审批
        if (state != 1)
        {
            gv_List.Columns[0].ItemStyle.Width = new Unit(1);
            bt_Approved.Visible = false;
            bt_UnApproved.Visible = false;
        }
        else
        {
            bt_Approved.Visible = true;
            bt_UnApproved.Visible = true;
            gv_List.Columns[0].ItemStyle.Width = new Unit(68);
            Org_StaffBLL _staff = new Org_StaffBLL((int)Session["UserID"]);
            DataTable dt = _staff.GetLowerPositionTask(3, int.Parse(tr_OrganizeCity.SelectValue), month);

            if (AC_AccountMonthBLL.GetCurrentMonth() - 1 <= int.Parse(ddl_Month.SelectedValue))
            {
                string[] allowday1 = Addr_OrganizeCityParamBLL.GetValueByType(1, 3).Split(new char[] { ',', '，', ';', '；' });
                string[] allowday2 = Addr_OrganizeCityParamBLL.GetValueByType(1, 4).Split(new char[] { ',', '，', ';', '；' });
                int day = DateTime.Now.Day;
                if (allowday1.Contains(day.ToString()))
                    bt_Approved.Visible = false;
                else if (allowday2.Contains(day.ToString()))
                {
                    #region 判断费用申请进度
                    Org_StaffBLL bll = new Org_StaffBLL((int)Session["UserID"]);
                    DataTable dt2 = new DataTable();
                    if (bll.Model.Position == 210)
                        dt2 = bll.GetFillProcessDetail(3);
                    if (dt.Rows.Count > 0)
                    {
                        bt_Approved.Visible = false;
                    }
                    #endregion
                }
                else
                {
                    bt_UnApproved.Enabled = false;
                    bt_Approved.Enabled = dt.Rows.Count == 0;
                }
            }
            else
            {

                bt_UnApproved.Enabled = false;
                bt_Approved.Enabled = dt.Rows.Count == 0;
            }
            if (dt.Rows.Count > 0)
            {

                bt_Approved.Enabled = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/Pop_ShowLowerPositionTask.aspx") +
                "?Type=3&StaffID=0&Month=" + ddl_Month.SelectedValue + "&City=" + tr_OrganizeCity.SelectValue + "&tempid='+tempid, window, 'dialogWidth:520px;DialogHeight=600px;status:yes;resizable=no');</script>", false);
            }

        }
        #endregion
    }

    #region 合并行组中相邻行相同值
    public void GridViewMergSampeValueRow(GridView gv, int ColumnIndex, string BackColor, int BorderWidth, string BorderColor)
    {
        if (gv.Rows.Count == 0) return;
        #region 合并行组中相邻行相同值
        int rowspan = 1;
        for (int i = gv.Rows.Count - 1; i > 0; i--)
        {
            if (BackColor != "")
                gv.Rows[i].Cells[ColumnIndex].Style.Add("background-color", BackColor);
            else
                gv.Rows[i].Cells[ColumnIndex].Style.Add("background-color", "#ffffff");

            if (ColumnIsSampe(gv, i, ColumnIndex))
            {
                gv.Rows[i].Cells[ColumnIndex].Visible = false;
                rowspan++;
            }
            else
            {
                gv.Rows[i].Cells[ColumnIndex].RowSpan = rowspan;

                if (BorderWidth > 0)
                {
                    gv.Rows[i].Cells[ColumnIndex].Style.Add("border-top-style", "solid");
                    gv.Rows[i].Cells[ColumnIndex].Style.Add("border-top-width", BorderWidth.ToString() + "px");
                    if (BorderColor != "") gv.Rows[i].Cells[ColumnIndex].Style.Add("border-color", BorderColor);
                }
                rowspan = 1;
            }
        }
        gv.Rows[0].Cells[ColumnIndex].RowSpan = rowspan;

        if (BackColor != "")
            gv.Rows[0].Cells[ColumnIndex].Style.Add("background-color", BackColor);
        else
            gv.Rows[0].Cells[ColumnIndex].Style.Add("background-color", "#ffffff");

        if (BorderWidth > 0)
        {
            gv.Rows[0].Cells[ColumnIndex].Style.Add("border-top-style", "solid");
            gv.Rows[0].Cells[ColumnIndex].Style.Add("border-top-width", BorderWidth.ToString() + "px");
            if (BorderColor != "") gv.Rows[0].Cells[ColumnIndex].Style.Add("border-color", BorderColor);
        }
        #endregion
    }
    private bool ColumnIsSampe(GridView gv, int row, int column)
    {
        if (gv.Rows[row].Cells[column].Text.Trim() == "&nbsp;") return false;

        for (int i = column; i > 0; i--)
        {
            if (gv.Rows[row].Cells[i].Text != gv.Rows[row - 1].Cells[i].Text) return false;
        }

        return true;
    }
    #endregion

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void DoApprove(bool ApproveFlag)
    {

        #region 仅查看待我审批的工资申请单
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

        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        string condition = " PM_Salary.State = 2 ";

        #region 组织明细记录的查询条件
        //管理片区及所有下属管理片区
        if (organizecity != 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += organizecity.ToString();

            condition += " AND PM_Salary.OrganizeCity IN (" + orgcitys + ")";
        }

        if (ddl_SalaryClassify.SelectedValue != "0")
        {
            condition += "AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',PM_Salary.ExtPropertys,'PMClassfiy')=" + ddl_SalaryClassify.SelectedValue;
        }
        //会计月条件
        condition += " AND PM_Salary.AccountMonth = " + ddl_Month.SelectedValue;

        IList<PM_Salary> lists = PM_SalaryBLL.GetModelList(condition);
        #endregion

        foreach (PM_Salary salary in lists)
        {
            if (TaskIDs.Contains(salary["TaskID"]))
            {
                int jobid = EWF_TaskBLL.StaffCanApproveTask(int.Parse(salary["TaskID"]), (int)Session["UserID"]);
                EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                if (job.Model != null)
                {
                    int decision = job.StaffCanDecide((int)Session["UserID"]);
                    if (decision > 0)
                    {
                        if (ApproveFlag)
                        {
                            job.Decision(decision, (int)Session["UserID"], 2, "汇总单批量审批通过!");       //2:审批已通过
                        }
                        else
                        {
                            job.Decision(decision, (int)Session["UserID"], 3, "汇总单批量未能审批通过!");    //3:审批未通过
                        }
                    }
                }
            }
        }

        BindGrid();
        MessageBox.Show(this, ApproveFlag ? "审批成功！" : "已成功将选择区域的申请单，设为批复未通过！");
        return;

    }

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
            case "1":
                {
                    gv_List.AllowPaging = false;
                    BindGrid();

                    string filename = HttpUtility.UrlEncode("导购员工资汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
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
                }
                break;
            case "3":
                {
                    gv_PromotorSalary.AllowPaging = false;
                    BindGrid();
                    string filename = HttpUtility.UrlEncode("导购员工资明细导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.ContentType = "application/ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                    Page.EnableViewState = false;

                    StringWriter tw = new System.IO.StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(tw);
                    gv_PromotorSalary.RenderControl(hw);

                    StringBuilder outhtml = new StringBuilder(tw.ToString());
                    outhtml = outhtml.Replace("&nbsp;", "");


                    Response.Write(outhtml.ToString());
                    Response.End();

                    gv_List.AllowPaging = true;
                }
                break;
        }
        BindGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            gv_List.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow r in gv_List.Rows)
            {
                r.Cells[1].Visible = false;

                #region 金额数据格式化
                if (r.Cells.Count > 2)
                {
                    for (int i = 2; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            if (gv_List.HeaderRow.Cells[i].Text.EndsWith("(%)") || r.Cells[3].Text.EndsWith("%"))
                            {
                                r.Cells[i].Text = d.ToString("#,0.##");
                            }
                            else
                            {
                                r.Cells[i].Text = d.ToString("#,0.#");
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }

    protected void gv_PromotorSalary_DataBound(object sender, EventArgs e)
    {
        if (gv_PromotorSalary.HeaderRow != null)
        {
            IList<CM_Client> lists;
            string clientname = "";
            string RetailerS = "";
            gv_PromotorSalary.HeaderRow.Cells[0].Visible = false;
            int taskindex = 0;
            for (int i = 0; i < gv_PromotorSalary.HeaderRow.Cells.Count; i++)
                if (gv_PromotorSalary.HeaderRow.Cells[i].Text == "审批工作流")
                    taskindex = i;
            foreach (GridViewRow r in gv_PromotorSalary.Rows)
            {
                clientname = "";
                lists = new List<CM_Client>();
                RetailerS = "";
                r.Cells[0].Visible = false;
                RetailerS = new PM_SalaryBLL().GetDetailModel(int.Parse(gv_PromotorSalary.DataKeys[r.RowIndex].Value.ToString()))["RetailerS"];
                if (RetailerS.Equals(""))
                {
                    r.Cells[5].Text = clientname;
                    continue;
                }

                lists = CM_ClientBLL.GetModelList("ID IN (" + RetailerS + ")");
                int count = 0;
                foreach (CM_Client c in lists)
                {
                    if (count < 2)
                    {
                        clientname += "<a href='../CM/RT/RetailerDetail.aspx?ClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                            + c.FullName + "</a><br/>";
                    }
                    else
                    {
                        break;
                    }
                    count++;
                }
                if (count > 1) clientname += "共" + lists.Count.ToString() + "个门名";
                r.Cells[6].Text = clientname;
                if (taskindex > 0)
                    r.Cells[r.Cells.Count - 1].Text = "<a href='../EWF/TaskDetail.aspx?TaskID=" + r.Cells[taskindex].Text + "' target='_blank' class='listViewTdLinkS1'>"
                        + r.Cells[taskindex].Text + "</a>";
            }
        }
    }
    protected void gv_PromotorSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_PromotorSalary.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_UnApproved_Click(object sender, EventArgs e)
    {
        DoApprove(false);
    }
    protected void bt_Approved_Click(object sender, EventArgs e)
    {
        DoApprove(true);
    }
}
