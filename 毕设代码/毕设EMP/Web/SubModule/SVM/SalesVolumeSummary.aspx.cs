using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.BLL.SVM;
using System.Data;
using MCSFramework.Common;
using System.Text;
using System.IO;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
public partial class SubModule_SVM_SalesVolumeSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["Type"] = Request.QueryString["Type"] == null ? 1 : int.Parse(Request.QueryString["Type"]);//1为进货 2为销量
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 3 : int.Parse(Request.QueryString["ClientType"]);
            if ((int)ViewState["Type"] == 1)
            {
                lbl_Message.Text = ((int)ViewState["ClientType"] == 3 ? "零售商" : "经销商") + "进货汇总信息";
            }
            else
            {
                lbl_Message.Text = ((int)ViewState["ClientType"] == 3 ? "零售商" : "经销商") + "销售汇总信息";
            }
            BindDropDown();
            if (Request.QueryString["Flag"] != null)
            {
                if (ddl_Flag.Items.FindByValue(Request.QueryString["Flag"]) != null)
                    ddl_Flag.SelectedValue = Request.QueryString["Flag"];
            }
            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                    ddl_State.SelectedValue = Request.QueryString["State"];
            }
            ddl_State_SelectedIndexChanged(null, null);

            if (Request.QueryString["TabItem"] != null)
            {
                MCSTabControl1.SelectedIndex = int.Parse(Request.QueryString["TabItem"]);
                gv_Summary.Visible = MCSTabControl1.SelectedIndex == 0;
                gv_List.Visible = !gv_Summary.Visible;
            }

            Header.Attributes["WebPageSubCode"] = "ClientType=" + ViewState["ClientType"].ToString() + "&Type=" + ViewState["Type"].ToString();
        }
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

        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10)).ToString();

        tr_OrganizeCity_Selected(null, null);
    
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

            int level = city.Level + 1;
            if (city.Level == 3) level = city.Level + 2;

            if (ddl_Level.Items.FindByValue(level.ToString()) != null)
                ddl_Level.SelectedValue = level.ToString();
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        bt_Submit.Enabled = ddl_State.SelectedValue == "1";
        bt_Approve.Enabled = ddl_State.SelectedValue == "2";
        if (sender != null) BindGrid();
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int flag = int.Parse(ddl_Flag.SelectedValue);

        #region 判断是否可以审批通过
        string[] allowdays = Addr_OrganizeCityParamBLL.GetValueByType(1, 7).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
        string date = DateTime.Now.Day.ToString();
        if (allowdays.Contains(date))
        {
            bt_Approve.Enabled = false;
            bt_Approve.ToolTip = "每月21-25号不可对进销存审批通过！";
        }
        #endregion

        if (new Addr_OrganizeCityBLL(organizecity).Model.Level >= 2
            && month == AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddDays(-10)))
        {
            if ((int)ViewState["ClientType"] == 3 && (int)ViewState["Type"] == 2)
            {
                DataTable dtFillDataProgress = Org_StaffBLL.GetFillDataProgress((int)Session["UserID"], true);
                DataRow[] rows;
                if (flag == 1)
                    rows = dtFillDataProgress.Select("ItemCode='006'");
                else
                    rows = dtFillDataProgress.Select("ItemCode='012'");

                if (rows.Length > 0)
                {
                    int ItemTargetCount = (int)rows[0]["ItemTargetCount"];
                    int ItemCompleteCount = (int)rows[0]["ItemCompleteCount"];
                    if (ItemTargetCount > ItemCompleteCount)
                    {
                        MessageBox.Show(this, "对不起，还有" + (ItemTargetCount - ItemCompleteCount).ToString() +
                            "家门店销量尚未录入,无法提交或审核!具体请查看桌面填报进度表.");
                        bt_Approve.Enabled = false;
                        bt_Submit.Enabled = false;
                    }
                }
            }
        }

        if (MCSTabControl1.SelectedIndex == 0)
        {
            DataTable dt_summary = SVM_SalesVolumeBLL.GetSummaryTotal(organizecity, month, (int)ViewState["ClientType"], flag, level, state, (int)ViewState["Type"], (int)Session["UserID"]);
            dt_summary = MatrixTable.Matrix(dt_summary, new string[] { "管理片区名称" }, new string[] { "品牌", "段位" }, "金额", true, true);
            gv_Summary.DataSource = dt_summary;
            gv_Summary.DataBind();
            MatrixTable.GridViewMatric(gv_Summary);
            if (dt_summary.Columns.Count >= 24)
                gv_Summary.Width = new Unit(dt_summary.Columns.Count * 60);
            else
                gv_Summary.Width = new Unit(100, UnitType.Percentage);

        }
        else if (MCSTabControl1.SelectedIndex == 1)
        {
            if (organizecity == 1 || new Addr_OrganizeCityBLL(organizecity).Model.Level < 2)
            {
                MessageBox.Show(this, "按客户及SKU查询时，不能按总部及大区级别查询!");
                return;
            }
            DataTable dt_summary = SVM_SalesVolumeBLL.GetSummaryTotal2(organizecity, month, (int)ViewState["ClientType"], flag, state, (int)ViewState["Type"], (int)Session["UserID"]);
            dt_summary = MatrixTable.Matrix(dt_summary, new string[] { "管理片区名称", "客户名称", "责任业代" }, new string[] { "品牌", "产品名称" }, "数量", true, true);
            gv_Summary.DataSource = dt_summary;
            gv_Summary.DataBind();
            MatrixTable.GridViewMatric(gv_Summary);
            if (dt_summary.Columns.Count >= 24)
                gv_Summary.Width = new Unit(dt_summary.Columns.Count * 60);
            else
                gv_Summary.Width = new Unit(100, UnitType.Percentage);
        }
        else
        {
            string condition = " SVM_SalesVolume.AccountMonth=" + ddl_Month.SelectedValue;
            IList<CM_Client> cmlist = new List<CM_Client>();
            if (organizecity > 1)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;
                cmlist = CM_ClientBLL.GetModelList("OrganizeCity IN (" + orgcitys + ") AND ActiveFlag=1 AND CM_Client.ClientType=" + ViewState["ClientType"].ToString());

                if (CM_ClientBLL.GetModelList("OrganizeCity IN (" + orgcitys + ") AND ActiveFlag=1 AND CM_Client.ClientType=" + ViewState["ClientType"].ToString()).Count != 0)
                {
                    condition += " AND SVM_SalesVolume.OrganizeCity IN (" + orgcitys + ")";
                }
            }

            condition += ddl_Flag.SelectedValue == "1" ? " AND SVM_SalesVolume.Flag<6" : " AND SVM_SalesVolume.Flag>6";

            AC_AccountMonthBLL _monthbll = new AC_AccountMonthBLL(month);

            if (ViewState["ClientType"] != null)
            {
                switch ((int)ViewState["ClientType"])
                {
                    case 2:
                        if ((int)ViewState["Type"] == 1)
                        {
                            condition += " AND SVM_SalesVolume.Client IN( Select ID FROM MCS_CM.dbo.CM_Client Where ClientType=2 AND  MCS_SYS.dbo.UF_Spilt2('MCS_CM.dbo.CM_Client',CM_Client.ExtPropertys,'DIClassify')='2'";
                        }
                        else
                        {
                            condition += " AND SVM_SalesVolume.Supplier IN( Select ID FROM MCS_CM.dbo.CM_Client Where ClientType=2";
                        }
                        break;
                    case 3:
                        condition += (int)ViewState["Type"] == 1 ? " AND SVM_SalesVolume.Client IN" : " AND SVM_SalesVolume.Supplier IN";
                        condition += "( Select ID FROM MCS_CM.dbo.CM_Client Where ClientType=3 ";
                        break;
                }
                if (cmlist.Count == 0)
                {
                    condition += " AND CM_Client.ClientManager=" + Session["UserID"].ToString();
                }
                condition += " AND ApproveFlag=1 AND OpenTime<='" + _monthbll.Model.EndDate + "'AND ISNULL(CloseTime,GETDATE())>='" + _monthbll.Model.BeginDate + "')";
            }
            switch (ddl_State.SelectedValue)
            {
                case "1":
                    condition += "AND SVM_SalesVolume.ApproveFlag=2 AND  MCS_SYS.dbo.UF_Spilt2('MCS_SVM.dbo.SVM_SalesVolume',SVM_SalesVolume.ExtPropertys,'SubmitFlag')='2'";
                    break;
                case "2":
                    condition += "AND SVM_SalesVolume.ApproveFlag=2 AND  MCS_SYS.dbo.UF_Spilt2('MCS_SVM.dbo.SVM_SalesVolume',SVM_SalesVolume.ExtPropertys,'SubmitFlag')='1'";
                    break;
                case "3":
                    condition += " AND SVM_SalesVolume.ApproveFlag=1";
                    break;
            }
            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
    }
    protected void gv_Summary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gv_Summary.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_Summary.Rows)
            {
                #region 金额数据格式化
                if (r.Cells.Count > 1)
                {
                    for (int i = 1; i < r.Cells.Count; i++)
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

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGrid();
        Timer1.Enabled = false;
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int month = int.Parse(ddl_Month.SelectedValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int flag = int.Parse(ddl_Flag.SelectedValue);
        int result = SVM_SalesVolumeBLL.ApproveByStaff(organizecity, (int)Session["UserID"], month, (int)ViewState["ClientType"], flag, (int)ViewState["Type"]);
        if (result == -1)
        {
            MessageBox.Show(this, "请确认客户的进或销都已提交！");
        }
        else
        {
            MessageBox.Show(this, "审核成功！");
        }
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_Summary.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_Summary.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_Summary.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    public string GetTotalValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalValue().ToString("f2");
    }

    public string GetTotalFactoryPriceValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalFactoryPriceValue().ToString("f2");
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_Summary.Visible = MCSTabControl1.SelectedIndex != 2;
        gv_List.Visible = !gv_Summary.Visible;

        BindGrid();
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int month = int.Parse(ddl_Month.SelectedValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int flag = int.Parse(ddl_Flag.SelectedValue);
        int result = SVM_SalesVolumeBLL.SubmitByStaff(organizecity, (int)Session["UserID"], month, (int)ViewState["ClientType"], flag, (int)ViewState["Type"]);

        if (result < 0)
        {
            MessageBox.Show(this, "您还有" + (-result).ToString() + "个客户的销量没有填写，无法提交! 填报进度请参照桌面的【填报进度表】");
        }
        else
        {
            MessageBox.Show(this, "共成功提交" + result.ToString() + "条销量！");
            BindGrid();
        }
    }
}
