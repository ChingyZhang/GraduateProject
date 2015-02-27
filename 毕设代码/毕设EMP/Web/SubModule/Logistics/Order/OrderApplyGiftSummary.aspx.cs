using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using System.Data;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;
using System.IO;
using System.Text;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;

public partial class SubModule_Logistics_Order_OrderApplyGiftSummary : System.Web.UI.Page
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
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGrid();
        Timer1.Enabled = false;
    }
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_GiftClassify.DataSource = DictionaryBLL.GetDicCollections("ORD_GiftClassify");
        ddl_GiftClassify.DataBind();
        ddl_GiftClassify.Items.Insert(0, new ListItem("请选择", "0"));

        IList<PDT_Brand> _listbrand = PDT_BrandBLL.GetModelList("ID in (SELECT Brand FROM MCS_Pub.dbo.PDT_Brand_Manager WHERE Manager=" + Session["UserID"].ToString() + ")");
        if (_listbrand.Count == 0)
        {
            _listbrand = PDT_BrandBLL.GetModelList("ISNULL(ISGift,0)=1");
        }


        ddl_Brand.DataSource = _listbrand;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("请选择", "0"));
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) > city.Level && int.Parse(p.Key) <= 3).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0)
            {
                ddl_Level.Items.Add(new ListItem("本级", city.Level.ToString()));
            }

            ddl_Level.Items.Add(new ListItem("经销商", "10"));
        }
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_Summary.PageIndex = 0;
        BindGrid();
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int classify = int.Parse(ddl_GiftClassify.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int pdtbrand = int.Parse(ddl_Brand.SelectedValue);
        int client = 0;
        int.TryParse(select_Client.SelectValue, out client);

        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                #region 显示统计分析
                DataTable dtSummary = ORD_OrderApplyBLL.GetGiftSummary(month, organizecity, level, state, classify, (int)Session["UserID"], pdtbrand);

                if (level < 10)
                {
                    dtSummary.Columns.Add("Key", Type.GetType("System.String"), "OrganizeCity+'-'+ProductBrand");
                    dtSummary.Columns.Add("赠品费率", Type.GetType("System.Decimal"), "IIF(实际销量>0,当月生成赠品额/实际销量,0)");
                    dtSummary.Columns.Add("申请费率", Type.GetType("System.Decimal"), "IIF(实际销量>0,申请总额/实际销量,0)");

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区", 
                    "订单品牌", "赠品费用类别", "Key", "实际销量", "赠品费率", "当月生成赠品额",
                    "上月余额", "赠品抵扣额", "申请总额", "申请费率", "当月余额" },
                        new string[] { "GiftBrandName" }, new string[] { "申请金额", "申请占比" }, true, false);
                    MatrixTable.TableAddRowSubTotal_Matric(dtSummary, new string[] { "管理片区" }, 4, new string[] { }, false);

                }
                else
                {
                    dtSummary.Columns.Add("Key", Type.GetType("System.String"), "ClientID+'-'+ProductBrand");
                    dtSummary.Columns.Add("赠品费率", Type.GetType("System.Decimal"), "IIF(实际销量>0,当月生成赠品额/实际销量,0)");
                    dtSummary.Columns.Add("申请费率", Type.GetType("System.Decimal"), "IIF(实际销量>0,申请总额/实际销量,0)");

                    dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "管理片区", "经销商名称", 
                   "订单品牌", "赠品费用类别", "Key", "实际销量", "赠品费率", "当月生成赠品额",
                   "上月余额", "赠品抵扣额", "申请总额", "申请费率", "当月余额" },
                        new string[] { "GiftBrandName" }, new string[] { "申请金额", "申请占比" }, true, false);
                    MatrixTable.TableAddRowSubTotal_Matric(dtSummary, new string[] { "管理片区", "经销商名称" }, 5, new string[] { }, false);

                }

                decimal totalsalesvolume = 0, totalavailableamount = 0, totalbalance = 0, totalapplycost = 0, prebalance = 0, deductibleamount = 0;
                if (dtSummary.Rows.Count == 0)
                {
                    gv_Summary.DataBind();
                    return;
                }


                for (int i = 0; i < dtSummary.Rows.Count - 1; i++)
                {

                    if (dtSummary.Rows[i][1].ToString() == "小计" || dtSummary.Rows[i][2].ToString() == "小计")
                    {
                        totalsalesvolume += (decimal)dtSummary.Rows[i]["实际销量"];
                        totalavailableamount += (decimal)dtSummary.Rows[i]["当月生成赠品额"];
                        totalbalance += (decimal)dtSummary.Rows[i]["当月余额"];
                        totalapplycost += (decimal)dtSummary.Rows[i]["申请总额"];
                        prebalance += (decimal)dtSummary.Rows[i]["上月余额"];
                        deductibleamount += (decimal)dtSummary.Rows[i]["赠品抵扣额"];

                        dtSummary.Rows[i]["赠品费率"] = (decimal)dtSummary.Rows[i]["实际销量"] == 0 ? 0 :
                            (decimal)dtSummary.Rows[i]["当月生成赠品额"] / (decimal)dtSummary.Rows[i]["实际销量"];
                        dtSummary.Rows[i]["申请费率"] = (decimal)dtSummary.Rows[i]["实际销量"] == 0 ? 0 :
                            (decimal)dtSummary.Rows[i]["申请总额"] / (decimal)dtSummary.Rows[i]["实际销量"];
                        for (int j = 0; j < dtSummary.Columns.Count; j++)
                        {
                            if (dtSummary.Columns[j].ColumnName.EndsWith("占比"))
                            {
                                dtSummary.Rows[i][j] = (decimal)dtSummary.Rows[i]["申请总额"] == 0 ? 0 :
                                    (decimal)dtSummary.Rows[i][j - 1] / (decimal)dtSummary.Rows[i]["申请总额"];
                            }
                        }
                    }
                }

                dtSummary.Rows[dtSummary.Rows.Count - 1]["实际销量"] = totalsalesvolume;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["当月生成赠品额"] = totalavailableamount;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["当月余额"] = totalbalance;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["申请总额"] = totalapplycost;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["上月余额"] = prebalance;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["赠品抵扣额"] = deductibleamount
                    ;

                dtSummary.Rows[dtSummary.Rows.Count - 1]["赠品费率"] = totalsalesvolume == 0 ? 0 :
                    totalavailableamount / totalsalesvolume;
                dtSummary.Rows[dtSummary.Rows.Count - 1]["申请费率"] = totalsalesvolume == 0 ? 0 :
                    totalapplycost / totalsalesvolume;

                for (int j = 0; j < dtSummary.Columns.Count; j++)
                {
                    if (dtSummary.Columns[j].ColumnName.EndsWith("占比"))
                    {
                        dtSummary.Rows[dtSummary.Rows.Count - 1][j] = totalapplycost == 0 ? 0 :
                            (decimal)dtSummary.Rows[dtSummary.Rows.Count - 1][j - 1] / totalapplycost;
                    }
                }


                bt_Approve.Enabled = (dtSummary.Rows.Count > 0 && state == 1);
                bt_UnApprove.Enabled = (dtSummary.Rows.Count > 0 && state == 1);
                chkHeader1.Visible = (dtSummary.Rows.Count > 0 && state == 1);
                chkHeader1.Checked = false;

                gv_Summary.DataSource = dtSummary;
                gv_Summary.DataBind();
                MatrixTable.GridViewMatric(gv_Summary);
                #endregion
                break;
            case "1":
                #region 显示申请明细

                DataTable dt_detail = ORD_OrderApplyBLL.GetGiftDetail(month, organizecity, client, state, (int)Session["UserID"], pdtbrand, classify);
                if (dt_detail.Rows.Count == 0)
                {
                    gv_ListDetail.DataBind();
                    return;
                }
                gv_ListDetail.DataSource = dt_detail;
                gv_ListDetail.BindGrid();

                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 0);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 1);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 2);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 3);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 4);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 5);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 6);
                MatrixTable.GridViewMergSampeValueRow(gv_ListDetail, 7);



                bt_Approve.Enabled = false;
                bt_UnApprove.Enabled = false;
                #endregion
                break;
            case "2":
                #region 显示统计汇总

                DataTable dt = ORD_OrderApplyBLL.GetGiftSummaryTotal(month, organizecity, client, state, (int)Session["UserID"], pdtbrand);
                if (dt.Rows.Count == 0)
                {
                    gv_Total.DataBind();
                    return;
                }
                gv_Total.DataSource = dt;
                gv_Total.DataBind();
                #endregion
                break;
            default:
                break;
        }

        if (state != 1)
        {
            bt_Approve.Visible = false;
            bt_UnApprove.Visible = false;
        }
        else
        {
            Org_StaffBLL _staff = new Org_StaffBLL((int)Session["UserID"]);
            DataTable dt = _staff.GetLowerPositionTask(5, int.Parse(tr_OrganizeCity.SelectValue), month);
            if (AC_AccountMonthBLL.GetCurrentMonth() - 1 > int.Parse(ddl_Month.SelectedValue))
            {
                bt_UnApprove.Enabled = false;
            }

            if (dt.Rows.Count > 0)
            {
                bt_Approve.Enabled = false;
                //bt_UnApprove.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/Pop_ShowLowerPositionTask.aspx") +
                "?Type=5&StaffID=0&Month=" + ddl_Month.SelectedValue + "&City=" + tr_OrganizeCity.SelectValue + "&tempid='+tempid, window, 'dialogWidth:520px;DialogHeight=600px;status:yes;resizable=no');</script>", false);
            }
        }
    }
    protected void gv_Summary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Summary.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_Total_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Total.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_Summary.PageIndex = 0;
        gv_ListDetail.PageIndex = 0;
        gv_Total.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_Summary.Visible = MCSTabControl1.SelectedTabItem.Value == "0";
        gv_ListDetail.Visible = MCSTabControl1.SelectedTabItem.Value == "1";
        gv_Total.Visible = MCSTabControl1.SelectedTabItem.Value == "2";

        select_Client.Enabled = MCSTabControl1.SelectedTabItem.Value != "0";
        if (!select_Client.Enabled) select_Client.SelectText = "";

        ddl_Level.Enabled = MCSTabControl1.SelectedTabItem.Value != "2";
        ddl_GiftClassify.Enabled = MCSTabControl1.SelectedTabItem.Value != "2";
        BindGrid();
    }


    protected void bt_Export_Click(object sender, EventArgs e)
    {
        string filename = HttpUtility.UrlEncode("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        GridView gv = null;
        if (gv_Summary.Visible)
            gv = gv_Summary;
        else if (gv_ListDetail.Visible)
        {
            int month = int.Parse(ddl_Month.SelectedValue);
            int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
            int level = int.Parse(ddl_Level.SelectedValue);
            int classify = int.Parse(ddl_GiftClassify.SelectedValue);
            int state = int.Parse(ddl_State.SelectedValue);
            int pdtbrand = int.Parse(ddl_Brand.SelectedValue);
            int client = 0;
            int.TryParse(select_Client.SelectValue, out client);
            DataTable dt_detail = ORD_OrderApplyBLL.GetGiftDetail(month, organizecity, client, state, (int)Session["UserID"], pdtbrand, classify);
            dt_detail.Columns.Remove("ORD_OrderApply_ID");
            dt_detail.Columns.Remove("ORD_OrderApply_TaskID");
            filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss")));
            CreateExcel(dt_detail, filename);
            return;
        }
        else
            gv = gv_Total;

        gv.AllowPaging = false;
        BindGrid();


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
        if (gv_ListDetail.Visible)
        {
            gv.Columns[1].Visible = true;
        }
        gv.AllowPaging = true;
        BindGrid();

    }
    private void CreateExcel(DataTable dt, string fileName)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.Charset = "UTF-8";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.Default;
        string colHeaders = "", ls_item = "";

        ////定义表对象与行对象，同时用DataSet对其值进行初始化
        //DataTable dt = ds.Tables[0];
        DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
        int i = 0;
        int cl = dt.Columns.Count;

        //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            }

        }
        resp.Write(colHeaders);
        //向HTTP输出流中写入取得的数据信息

        //逐行处理数据 
        foreach (DataRow row in myRow)
        {
            //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据   
            for (i = 0; i < cl; i++)
            {
                string content = row[i].ToString();
                if (content.Contains('\t')) content = content.Replace('\t', ' ');

                long l = 0;
                if (content.Length >= 8 && long.TryParse(content, out l))
                    content = "'" + content;

                ls_item += content;

                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += "\r\n";
                }
                else
                {
                    ls_item += "\t";
                }

            }
            resp.Write(ls_item);
            ls_item = "";

        }
        resp.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void gv_Summary_DataBound(object sender, EventArgs e)
    {
        if (gv_Summary.HeaderRow != null)
        {
            int keycolumnindex = 4;
            if (ddl_Level.SelectedValue == "10") keycolumnindex = 5;
            gv_Summary.HeaderRow.Cells[keycolumnindex].Visible = false;
            foreach (GridViewRow r in gv_Summary.Rows)
            {
                r.Cells[keycolumnindex].Visible = false;
                if (r.Cells[1].Text == "总计" || r.Cells[2].Text == "小计" || r.Cells[3].Text == "小计")
                {
                    r.Cells[0].Text = "&nbsp";
                    for (int i = 0; i < r.Cells.Count; i++)
                    {
                        if (r.Cells[1].Text == "总计") { r.Cells[i].ForeColor = System.Drawing.Color.Red; r.Cells[i].Font.Bold = true; }
                        if (r.Cells[2].Text == "小计") { r.Cells[i].ForeColor = System.Drawing.Color.Blue; }
                        if (r.Cells[3].Text == "小计") { r.Cells[i].ForeColor = System.Drawing.Color.Brown; }
                    }
                }




                #region 金额数据格式化
                if (r.Cells.Count > 5)
                {
                    for (int i = 5; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            if (gv_Summary.HeaderRow.Cells[i].Text.EndsWith("占比") ||
                                gv_Summary.HeaderRow.Cells[i].Text.EndsWith("费率"))
                                r.Cells[i].Text = d.ToString("#,0.##%");
                            else
                                r.Cells[i].Text = d.ToString("#,0.##");
                            r.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                }
                #endregion
            }
        }
    }

    #region 批量审批
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        DoApprove(true);
        gv_Summary.PageIndex = 0;
        BindGrid();
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        DoApprove(false);
        gv_Summary.PageIndex = 0;
        BindGrid();
    }
    private void DoApprove(bool ApproveFlag)
    {
        foreach (GridViewRow row in gv_Summary.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                string key = gv_Summary.DataKeys[row.RowIndex]["Key"].ToString();
                string[] tasks = GetApproveTaskIDsByKey(key).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string taskid in tasks)
                {
                    int jobid = EWF_TaskBLL.StaffCanApproveTask(int.Parse(taskid), (int)Session["UserID"]);
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
                                    job.Decision(decisionid, (int)Session["UserID"], 3, "汇总单批量审批不通过!");     //3:审批不通过
                            }
                        }
                    }
                }
            }


        }
    }

    #region 获取指定Key范围内赠品申请单的工作流ID
    private string GetApproveTaskIDsByKey(string Key)
    {
        string[] keys = Key.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (keys.Length < 2) return "";

        int brand = 0, city = 0, client = 0;
        int.TryParse(keys[1], out brand);

        string condition = "ORD_OrderApply.State=2 AND AccountMonth = " + ddl_Month.SelectedValue +
                    " AND MCS_SYS.dbo.UF_Spilt2('MCS_Logistics.dbo.ORD_OrderApply',ORD_OrderApply.ExtPropertys,'ProductBrand') = " + brand.ToString() +
                    @" AND MCS_SYS.dbo.UF_Spilt2('MCS_Logistics.dbo.ORD_OrderApply',ORD_OrderApply.ExtPropertys,'TaskID') IN (
				SELECT EWF_Task_Job.Task
				FROM  MCS_EWF.dbo.EWF_Task_JobDecision INNER JOIN
					MCS_EWF.dbo.EWF_Task_Job ON EWF_Task_JobDecision.Job = EWF_Task_Job.ID 
				WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + @" AND
					EWF_Task_JobDecision.DecisionResult=1 and EWF_Task_Job.Status=3)";

        if (ddl_GiftClassify.SelectedValue != "0")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_Logistics.dbo.ORD_OrderApply',ORD_OrderApply.ExtPropertys,'GiftClassify') = " + ddl_GiftClassify.SelectedValue;
        }
        if (int.Parse(ddl_Level.SelectedValue) < 10)
        {
            int.TryParse(keys[0], out city);

            condition += " AND (ORD_OrderApply.OrganizeCity=" + city.ToString() +
                " OR MCS_SYS.dbo.UF_IsChildOrganizeCity(" + city.ToString() + ",ORD_OrderApply.OrganizeCity) = 0)";
        }
        else
        {
            int.TryParse(keys[0], out client);
            condition += " AND ORD_OrderApply.Client=" + client.ToString();
        }
        IList<ORD_OrderApply> lists = ORD_OrderApplyBLL.GetModelList(condition);

        string taskids = "";
        foreach (ORD_OrderApply apply in lists)
        {
            taskids += apply["TaskID"] + ",";
        }
        return taskids;
    }
    #endregion
    #endregion


    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
