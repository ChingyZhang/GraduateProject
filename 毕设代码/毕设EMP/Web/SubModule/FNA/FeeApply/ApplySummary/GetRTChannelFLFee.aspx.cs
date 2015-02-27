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

public partial class SubModule_FNA_FeeApply_ApplySummary_GetRTChannelFLFee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);

            BindDropDown();

            if (Request.QueryString["State"] != null &&
                ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
            {
                ddl_State.SelectedValue = Request.QueryString["State"];
            }
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
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (int)ViewState["AccountMonth"] == 0 ? AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString() : ViewState["AccountMonth"].ToString();

        ddl_RTChannel.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Channel");
        ddl_RTChannel.DataBind();
        ddl_RTChannel.Items.Insert(0, new ListItem("所有", "0"));
        ddl_RTChannel.SelectedValue = "0";

        ddl_RTType.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Classify");
        ddl_RTType.DataBind();
        ddl_RTType.Items.Insert(0, new ListItem("所有", "0"));
        ddl_RTType.SelectedValue = "0";

    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    #endregion

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGrid();
        Timer1.Enabled = false;
    }

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int RTChannel = int.Parse(ddl_RTChannel.SelectedValue);
        int RTType = int.Parse(ddl_RTType.SelectedValue);

        string condition = "";

        if (tbx_ApplyCost.Text != "0")
            condition = "TotalApplyCost" + ddl_OP.SelectedValue + tbx_ApplyCost.Text;

        if (txt_FeeRate.Text != "0")
        {
            if (condition != "") condition += " AND ";
            condition += "CONVERT(DECIMAL(10,3),REPLACE(ApplyCostRate,'%',''))" + ddl_FeeRateOP.SelectedValue + txt_FeeRate.Text;
        }
        DataTable dtSummary = FNA_FeeApplyBLL.GetRTChannelFLFee
            (month, organizecity, state, int.Parse(Session["UserID"].ToString()), RTChannel, condition, RTType);
        if (dtSummary.Rows.Count == 0)
        {
            gv_List.DataBind();
            return;
        }

        #region 求行小计
        MatrixTable.TableAddRowSubTotal(dtSummary, new string[] { "区域信息→大区", "区域信息→营业部", "区域信息→办事处", "零售店基本情况→费用代垫客户" },
            new string[] { "卖场销售额→上月", "卖场销售额→本月", "费用情况→总费用", "费用情况→我司费用", "费用情况→经销商费用" }, true);
        //计算小计行费率
        foreach (DataRow dr in dtSummary.Rows)
        {
            if (dr[0].ToString() == "总计" ||
                dr[1].ToString() == "小计" ||
                dr[2].ToString() == "小计" ||
                dr[3].ToString() == "小计" ||
                dr[4].ToString() == "小计")
            {
                dr["费用情况→我司费率"] = (decimal)dr["卖场销售额→本月"] == 0 ? "100%" : ((decimal)dr["费用情况→我司费用"] / (decimal)dr["卖场销售额→本月"]).ToString("0.#%");
                dr["费用情况→经销商费率"] = (decimal)dr["卖场销售额→本月"] == 0 ? "100%" : ((decimal)dr["费用情况→经销商费用"] / (decimal)dr["卖场销售额→本月"]).ToString("0.#%");
            }
        }
        #endregion

        gv_List.DataSource = dtSummary;
        gv_List.DataBind();

        if (dtSummary.Columns.Count >= 24)
            gv_List.Width = new Unit(dtSummary.Columns.Count * 60);
        else
            gv_List.Width = new Unit(100, UnitType.Percentage);

        MatrixTable.GridViewMatric(gv_List);

        for (int i = 0; i < 4; i++)
        {
            MatrixTable.GridViewMergSampeValueRow(gv_List, i);
        }

        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1510, "BatApproveFee"))
        {
            bt_Approve.Visible = (ddl_State.SelectedValue == "1");
            bt_UnApprove.Visible = (ddl_State.SelectedValue == "1");
            bt_ExcludeApplyDetail.Visible = (ddl_State.SelectedValue == "1");

            if (state == 1 && condition == "" && RTChannel == 0 && RTType == 0)
            {
                bt_Approve.Enabled = true;
                bt_UnApprove.Enabled = true;

                #region 判断能否审批

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
                            if (dt2.Rows.Count > 0)
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
                        bt_UnApprove.Enabled = false;

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
                bt_Approve.Enabled = false;
                bt_UnApprove.Enabled = false;
            }


        }
        else
        {
            bt_Approve.Visible = false;
            bt_UnApprove.Visible = false;
            bt_ExcludeApplyDetail.Visible = false;
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_List.Rows)
            {
                if (r.Cells.Count > 9)
                {
                    #region 金额数据格式化
                    for (int i = 2; i < r.Cells.Count - 2; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            r.Cells[i].Text = d.ToString("#,#.##");
                        }
                    }
                    #endregion

                    #region 增加打开单个审批工作流页面
                    int taskid = 0;
                    if (int.TryParse(r.Cells[r.Cells.Count - 2].Text, out taskid) && taskid > 0)
                    {
                        HyperLink hy = new HyperLink();
                        hy.Text = taskid.ToString();
                        hy.NavigateUrl = "~/SubModule/EWF/TaskDetail.aspx?TaskID=" + taskid.ToString();
                        hy.Target = "_blank";
                        hy.ToolTip = "新窗口中打开该申请单，单独审批该费用！";
                        hy.CssClass = "listViewTdLinkS1";
                        r.Cells[r.Cells.Count - 2].Controls.Add(hy);
                    }
                    #endregion

                    #region 增加勾选复选框
                    int detailid = 0;
                    if (int.TryParse(r.Cells[r.Cells.Count - 1].Text, out detailid) && detailid > 0 && ddl_State.SelectedValue == "1")
                    {
                        CheckBox cb_Exclude = new CheckBox();
                        cb_Exclude.Attributes["onclick"] = "checkonclick(this," + detailid.ToString() + ")";
                        r.Cells[r.Cells.Count - 1].Controls.Add(cb_Exclude);
                    }
                    else
                    {
                        r.Cells[r.Cells.Count - 1].Text = "";
                    }
                    #endregion

                }

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
    #region 批量扣除选中的费用
    protected void bt_ExcludeApplyDetail_Click(object sender, EventArgs e)
    {
        string excludeids = tbx_SelectedApplyDetailIDs.Text;
        if (excludeids == "")
        {
            MessageBox.Show(this, "请勾选要扣除掉的费用项");
            BindGrid();
        }

        string[] ids = excludeids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string _id in ids)
        {
            int detailid = 0;
            if (int.TryParse(_id, out detailid) && detailid > 0)
            {
                FNA_FeeApplyDetail m = new FNA_FeeApplyBLL().GetDetailModel(detailid);

                decimal OldAdjustCost = m.AdjustCost;

                m.AdjustCost = 0 - m.ApplyCost;
                m.DIAdjustCost = 0 - m.DICost;
                m.AdjustReason = "批量扣减";

                FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL(m.ApplyID);
                bll.UpdateDetail(m);

                if (m.Client > 0)
                {
                    CM_Client _c = new CM_ClientBLL(m.Client).Model;
                    if (_c != null) m.AdjustReason += " 客户:" + _c.FullName;
                }

                //保存调整记录
                FNA_FeeApplyBLL.UpdateAdjustRecord(m.ApplyID, (int)Session["UserID"], m.AccountTitle, OldAdjustCost.ToString("0.##"), m.AdjustCost.ToString("0.##"), m.AdjustReason);
            }

            MessageBox.Show(this, "扣减成功!");
            BindGrid();
        }
    }
    #endregion

    #region 批量审批通过或不通过
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        DoApprove(true);
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        DoApprove(false);
    }
    private void DoApprove(bool ApproveFlag)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int state = 1;
        int RTChannel = 0;
        DataTable dtSummary = FNA_FeeApplyBLL.GetRTChannelFLFee
            (month, organizecity, state, int.Parse(Session["UserID"].ToString()), RTChannel);

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
                return;
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
                        }
                    }
                }
            }
        }

        BindGrid();
    }
    #endregion

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

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                Response.Redirect("FLFeeApplySummary.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue + "&State=" + ddl_State.SelectedValue);
                break;
            case "1":
                Response.Redirect("FLFeeApplySummary.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue + "&State=" + ddl_State.SelectedValue + "&Selected=1");
                break;
            case "2":
                BindGrid();
                break;
        }
    }
}
