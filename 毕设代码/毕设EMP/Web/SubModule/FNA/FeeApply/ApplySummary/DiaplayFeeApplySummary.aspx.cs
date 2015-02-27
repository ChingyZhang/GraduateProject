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
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_FNA_FeeApply_ApplySummary_DiaplayFeeApplySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["SelectedIndex"] = Request.QueryString["SelectedIndex"] == null ? 0 : int.Parse(Request.QueryString["SelectedIndex"]);

            BindDropDown();
            MCSTabControl1.SelectedIndex = (int)ViewState["SelectedIndex"];

            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                {
                    ddl_State.SelectedValue = Request.QueryString["State"];
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
        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (int)ViewState["AccountMonth"] == 0 ? AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString() : ViewState["AccountMonth"].ToString();

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
    }
    #endregion

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        DataTable dtSummary = new DataTable();
        int state = int.Parse(ddl_State.SelectedValue);
        string accountname = ddl_Month.SelectedItem.Text;
        string preaccountname = DateTime.Parse(accountname + "-01").AddMonths(-3).ToString("yyyy-MM");
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                {
                    dtSummary = FNA_FeeApplyBLL.GetDiaplayFeeSummary(month, organizecity, int.Parse(ddl_Level.SelectedValue), state, int.Parse(Session["UserID"].ToString()));
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        return;
                    }

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区名称", "责任人员", "行政属性" },
                                 new string[] { "RTChannel", "DisplayFeeType", "Title" }, "Summary", true, false);
                    MatrixTable.TableAddRowSubTotal_Matric(dtSummary, new string[] { "责任人员" }, 3, new string[] { "RTChannel", "DisplayFeeType", "Title" }, false);

                    #region 重新计算总计行的费率
                    if (dtSummary.Rows.Count > 1)
                    {
                        
                        foreach (DataRow dr in dtSummary.Rows)
                        {
                            if (dr[0].ToString().EndsWith("计") || dr[2].ToString().EndsWith("计"))
                            {
                                foreach (DataColumn dc in dtSummary.Columns)
                                {
                                    if (dc.ColumnName.EndsWith("费率%"))
                                    {
                                        string title = dc.ColumnName;
                                        int pos = title.IndexOf('→');
                                        if (pos > 0)
                                        {
                                            title = title.Substring(0, pos);
                                            if (dtSummary.Columns.Contains(title + "→2.陈列费总计(元/月)→A.我司承担") &&
                                                dtSummary.Columns.Contains(title + "→3.销量及费率→C." + accountname + "预计销量(元/月)") &&
                                                (decimal)dr[title + "→3.销量及费率→C." + accountname + "预计销量(元/月)"] != 0)
                                            {
                                                dr[dc.ColumnName] = Math.Round((decimal)dr[title + "→2.陈列费总计(元/月)→A.我司承担"] / (decimal)dr[title + "→3.销量及费率→C." + accountname + "预计销量(元/月)"] * 100, 1, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                    if (dc.ColumnName.EndsWith("E.费率(较" + preaccountname + ")%"))
                                    {
                                        string title = dc.ColumnName;
                                        int pos = title.IndexOf('→');
                                        if (pos > 0)
                                        {
                                            title = title.Substring(0, pos);
                                            if (dtSummary.Columns.Contains(title + "→2.陈列费总计(元/月)→A.我司承担") &&
                                                dtSummary.Columns.Contains(title + "→3.销量及费率→A." + preaccountname + "实际销量(元/月)") &&
                                                (decimal)dr[title + "→3.销量及费率→A." + preaccountname + "实际销量(元/月)"] != 0)
                                            {
                                                dr[dc.ColumnName] = Math.Round((decimal)dr[title + "→2.陈列费总计(元/月)→A.我司承担"] / (decimal)dr[title + "→3.销量及费率→A." + preaccountname + "实际销量(元/月)"] * 100, 1, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                break;
            case "1":
                {
                    dtSummary = FNA_FeeApplyBLL.GetDiaplayFeeByDisplay(month, organizecity, int.Parse(ddl_Level.SelectedValue), state, int.Parse(Session["UserID"].ToString()));
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        return;
                    }

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区名称", "责任人员" },
                                 new string[] { "DisplayMode", "Title" }, "Summary", true, false);
                    #region 重新计算总计行的费率
                    if (dtSummary.Rows.Count > 1)
                    {
                        foreach (DataRow dr in dtSummary.Rows)
                        {
                            if (dr[0].ToString().EndsWith("计"))
                            {
                                foreach (DataColumn dc in dtSummary.Columns)
                                {
                                    if (dc.ColumnName.EndsWith("费率%"))
                                    {
                                        string title = dc.ColumnName;
                                        int pos = title.IndexOf('→');
                                        if (pos > 0)
                                        {
                                            title = title.Substring(0, pos);
                                            if (dtSummary.Columns.Contains(title + "→B.我司费用") &&
                                                dtSummary.Columns.Contains(title + "→D."+accountname+"预计销量(元/月)") &&
                                                (decimal)dr[title + "→D." + accountname + "预计销量(元/月)"] != 0)
                                            {
                                                dr[dc.ColumnName] = Math.Round((decimal)dr[title + "→B.我司费用"] / (decimal)dr[title + "→D." + accountname + "预计销量(元/月)"] * 100, 1, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                }
                break;
            case "2":
                {
                    dtSummary = FNA_FeeApplyBLL.GetByPayMode(month, organizecity, int.Parse(ddl_Level.SelectedValue), state, int.Parse(Session["UserID"].ToString()));
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        return;
                    }

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区名称", "责任人员" },
                                 new string[] { "付款周期", "Title" }, "Summary", true, false);
                    #region 重新计算总计行的费率
                    if (dtSummary.Rows.Count > 1)
                    {
                        foreach (DataRow dr in dtSummary.Rows)
                        {
                            if (dr[0].ToString().EndsWith("计"))
                            {
                                foreach (DataColumn dc in dtSummary.Columns)
                                {
                                    if (dc.ColumnName.EndsWith("比"))
                                    {
                                        string title = dc.ColumnName;
                                        int pos = title.IndexOf('→');
                                        if (pos > 0)
                                        {
                                            title = title.Substring(0, pos);
                                            if (dtSummary.Columns.Contains(title + "→我司费用") &&
                                                dtSummary.Columns.Contains("总计→我司费用") &&
                                                (decimal)dr["总计→我司费用"] != 0)
                                            {
                                                dr[dc.ColumnName] = Math.Round((decimal)dr[title + "→我司费用"] / (decimal)dr["总计→我司费用"] * 100, 1, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                }
                break;
        }


        gv_List.DataSource = dtSummary;
        gv_List.DataBind();

        if (dtSummary.Columns.Count >= 24)
            gv_List.Width = new Unit(dtSummary.Columns.Count * 65);
        else
            gv_List.Width = new Unit(100, UnitType.Percentage);

        MatrixTable.GridViewMatric(gv_List);

        MatrixTable.GridViewMergSampeValueRow(gv_List, 0);

        MatrixTable.GridViewMergSampeValueRow(gv_List, 1);

        #region 是否可以批量审批
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1509, "BatApproveFee"))
        {
            bt_Approve.Visible = (ddl_State.SelectedValue == "1");
            bt_UnApprove.Visible = (ddl_State.SelectedValue == "1");
            bt_Approve.Enabled = (ddl_State.SelectedValue == "1");
            bt_UnApprove.Enabled = (ddl_State.SelectedValue == "1");   
            #region 判断费用申请进度
            if (ddl_State.SelectedValue == "1")
            {
                Org_StaffBLL _staff = new Org_StaffBLL((int)Session["UserID"]);
                DataTable dt = _staff.GetLowerPositionTask(1,organizecity, month);
            
                if (dt.Rows.Count > 0)
                {
                    bt_Approve.Enabled = false;
                    //bt_UnApprove.Enabled = false;                   
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/Pop_ShowLowerPositionTask.aspx") +
                    "?Type=1&StaffID=0&Month="+ddl_Month.SelectedValue+"&City=" + tr_OrganizeCity.SelectValue + "&tempid='+tempid, window, 'dialogWidth:520px;DialogHeight=600px;status:yes;resizable=no');</script>", false);
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
                        if (decimal.TryParse(r.Cells[i].Text, out d) && d > 1)
                        {
                            r.Cells[i].Text = d.ToString("#,#.##");
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
        int counts = DoApprove(organizecity, true);

        BindGrid();
        MessageBox.Show(this, "已成功将" + counts.ToString() + "个申请单，设为审批通过！");
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int counts = DoApprove(organizecity, false);

        BindGrid();
        MessageBox.Show(this, "已成功将" + counts.ToString() + "个申请单，设为审批未通过！");
    }
    protected int DoApprove(int OrganizeCity, bool ApproveFlag)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int state = 1;              //待我审批
        int RTChannel = 0;          //所有渠道
        DataTable dtSummary = FNA_FeeApplyBLL.GetRTChannelDiaplayFee
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
                int taskid = (int)row[TaskColumnName];
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
            case "3":
                Response.Redirect("GetRTChannelDisplayFee.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue + "&State=" + ddl_State.SelectedValue);
                break;
            default:
                BindGrid();
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
