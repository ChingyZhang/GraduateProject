using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.OA;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;

public partial class SubModule_OA_Journal_WorkingPlan_CalendarList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            BindDropDown();

            if ((int)ViewState["ID"] == 0)
            {
                tbx_begindate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                tbx_enddate.Text = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");

                select_PlanStaff.SelectText = Session["UserRealName"].ToString();
                select_PlanStaff.SelectValue = Session["UserID"].ToString();

                tr_adddetail.Visible = false;
                UploadFile1.Visible = false;
                bt_Save.Visible = false;
                bt_Delete.Visible = false;
                bt_Submit.Visible = false;
                bt_Journal.Visible = false;
            }
            else
            {
                //载入已有的工作计划
                BindData();
            }
        }
    }

    private void BindDropDown()
    {
        ddl_WorkingClassify.DataSource = DictionaryBLL.GetDicCollections("OA_WorkingClassify");
        ddl_WorkingClassify.DataBind();
        ddl_WorkingClassify.Items.Insert(0, new ListItem("请选择...", "0"));

        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
            cbx_GenarateSynergetic.Visible = false;
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        tr_OrganizeCity_Selected(null, null);

        //tr_OfficailCity.DataSource = Addr_OfficialCityBLL.GetAllOfficialCity();

        if (staff.Model.OfficialCity != 0)
        {
            tr_OfficailCity.SelectValue = staff.Model.OfficialCity.ToString();
            ViewState["StaffOfficialCity"] = staff.Model.OfficialCity;
        }
    }

    /// <summary>
    /// 载入已有的工作项
    /// </summary>
    private void BindData()
    {
        cb_DisplayCheckedOnly.Checked = true;

        JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL((int)ViewState["ID"]);

        tr_OrganizeCity.SelectValue = bll.Model.OrganizeCity.ToString();
        tbx_begindate.Text = bll.Model.BeginDate.ToString("yyyy-MM-dd");
        tbx_enddate.Text = bll.Model.EndDate.ToString("yyyy-MM-dd");
        select_PlanStaff.SelectValue = bll.Model.Staff.ToString();
        select_PlanStaff.SelectText = new Org_StaffBLL(bll.Model.Staff).Model.RealName;

        pl_detail.BindData(bll.Model);
        InitGridView(bll.Model.BeginDate, bll.Model.EndDate);

        ViewState["WorkingPlanData"] = LoadWorkingPlanDetail((int)ViewState["ID"]);
        ViewState["BeginDate"] = bll.Model.BeginDate;
        ViewState["EndDate"] = bll.Model.EndDate;

        BindGrid();

        tbx_begindate.Enabled = false;
        tbx_enddate.Enabled = false;
        select_PlanStaff.Enabled = false;
        tr_OrganizeCity.Enabled = false;
        cbx_GenarateSynergetic.Visible = false;
        bt_Create.Visible = false;

        tbx_Begin.Text = tbx_begindate.Text;
        tbx_End.Text = tbx_begindate.Text;

        if (bll.Model.BeginDate < DateTime.Today)
        {
            bt_Delete.Visible = false;
        }

        if (bll.Model.State == 2 || bll.Model.State == 3 || bll.Model.ApproveFlag == 1 || bll.Model.InsertStaff != (int)Session["UserID"])
        {
            //提交待审批、已审批
            tr_adddetail.Visible = false;
            pl_detail.SetControlsEnable(false);
            bt_Save.Visible = false;
            bt_Delete.Visible = false;
            bt_Submit.Visible = false;

            UploadFile1.CanUpload = false;
            UploadFile1.CanDelete = false;

            gv_List.Columns[0].Visible = false;
            gv_List.SetControlsEnable(false);
        }

        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.BindGrid();

    }

    private void BindGrid()
    {
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];
            gv_List.DataSource = dt;
            gv_List.DataBind();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime day = (DateTime)ViewState["BeginDate"];
                while (day <= (DateTime)ViewState["EndDate"])
                {
                    CheckBox checkbox = (CheckBox)gv_List.Rows[i].Cells[(int)ViewState["FirstDateColumnIndex"] + (day - (DateTime)ViewState["BeginDate"]).Days].Controls[0];
                    if (!(bool)dt.Rows[i]["P" + day.ToString("MMdd")] && cb_DisplayCheckedOnly.Checked)
                    {
                        checkbox.Visible = false;
                    }

                    if (day <= DateTime.Today)
                    {
                        checkbox.Enabled = false;
                    }
                    day = day.AddDays(1);

                }
            }
        }
    }

    #region 页面刷新控制
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_PlanStaff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_RelateClient.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_RelateStaff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=Y&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void select_PlanStaff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (!string.IsNullOrEmpty(select_PlanStaff.SelectValue))
        {
            Org_Staff staff = new Org_StaffBLL(int.Parse(select_PlanStaff.SelectValue)).Model;
            tr_OfficailCity.SelectValue = staff.OfficialCity.ToString();
            tr_OrganizeCity.SelectValue = staff.OrganizeCity.ToString();
            tr_OrganizeCity_Selected(null, null);
            ViewState["StaffOfficialCity"] = staff.OfficialCity;
        }
    }

    protected void ddl_WorkingClassify_SelectedIndexChanged(object sender, EventArgs e)
    {
        select_RelateClient.Visible = false;
        select_RelateClient.SelectValue = "";
        select_RelateClient.SelectText = "";

        select_RelateStaff.Visible = false;
        select_RelateStaff.SelectValue = "";
        select_RelateStaff.SelectText = "";

        switch (ddl_WorkingClassify.SelectedValue)
        {
            case "1":
                lb_WorkingContent.Text = "拜访客户";
                select_RelateClient.Visible = true;
                tr_OfficailCity.Enabled = false;
                break;
            case "2":
                lb_WorkingContent.Text = "协同拜访员工";
                select_RelateStaff.Visible = true;
                tr_OfficailCity.Enabled = false;
                break;
            default:
                lb_WorkingContent.Text = "";
                tr_OfficailCity.Enabled = true;
                break;
        }
    }

    protected void select_RelateClient_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (!string.IsNullOrEmpty(select_RelateClient.SelectValue))
        {
            int client = int.Parse(select_RelateClient.SelectValue);
            CM_Client c = new CM_ClientBLL(client).Model;

            if (c != null && c.OfficalCity != 0)
            {
                tr_OfficailCity.SelectValue = c.OfficalCity.ToString();
                tr_OfficailCity.Enabled = false;
            }
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];
            dt.Rows.RemoveAt(e.RowIndex);

            BindGrid();
        }
    }
    protected void cb_DisplayCheckedOnly_CheckedChanged(object sender, EventArgs e)
    {
        SaveDataTable();
        BindGrid();
    }
    #endregion

    #region 初始化GridView,生成日期列
    private void InitGridView(DateTime begindate, DateTime enddate)
    {
        DateTime day = begindate;
        ViewState["FirstDateColumnIndex"] = gv_List.Columns.Count;

        while (day <= enddate)
        {
            CheckBoxField cbfield = new CheckBoxField();

            #region 转换星期几的名称
            string week = "";
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "三";
                    break;
                case DayOfWeek.Thursday:
                    week = "四";
                    break;
                case DayOfWeek.Friday:
                    week = "五";
                    break;
                case DayOfWeek.Saturday:
                    week = "<font color=red>六</font>";
                    break;
                case DayOfWeek.Sunday:
                    week = "<font color=red>日</font>";
                    break;
                default:
                    break;
            }
            #endregion

            cbfield.HeaderText = day.ToString("M.d") + "<br/>" + week;
            cbfield.DataField = "P" + day.ToString("MMdd");
            gv_List.Columns.Add(cbfield);

            day = day.AddDays(1);
        }
        if (620 + (enddate - begindate).Days * 40 > 1024)
            gv_List.Width = new Unit(620 + (enddate - begindate).Days * 40);
    }
    protected void gv_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                TableCell cell = e.Row.Cells[i];
                if (cell.Controls.Count > 0 && cell.Controls[0].GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    ((CheckBox)cell.Controls[0]).Enabled = true;
                    ((CheckBox)cell.Controls[0]).ToolTip = gv_List.Columns[i].HeaderText.Replace("<br/>", " 周").Replace("<font color=red>", "").Replace("</font>", "");// "";
                }
            }
        }
    }
    #endregion

    #region 初始化空的DataTable
    private DataTable GenareateDataTable(DateTime begindate, DateTime enddate)
    {
        DataTable dt = new DataTable();

        DataColumn dc_SortID = new DataColumn("SortID", Type.GetType("System.Int32"));
        dt.Columns.Add(dc_SortID);
        dc_SortID.AutoIncrement = true;
        dc_SortID.AutoIncrementSeed = 1;
        dc_SortID.AutoIncrementStep = 1;
        DataColumn[] pk = { dc_SortID };
        dt.PrimaryKey = pk;

        dt.Columns.Add(new DataColumn("WorkingClassify", Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("WorkingClassifyName", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Description", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("RelateClient", Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("RelateClientName", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("RelateStaff", Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("RelateStaffName", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("OfficialCity", Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("OfficialCityName", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Cost1", Type.GetType("System.Decimal")));
        dt.Columns.Add(new DataColumn("Cost2", Type.GetType("System.Decimal")));

        string expression = "";
        DateTime day = begindate;
        while (day <= enddate)
        {
            DataColumn dc_data = new DataColumn("P" + day.ToString("MMdd"), Type.GetType("System.Boolean"));
            dc_data.DefaultValue = false;
            dt.Columns.Add(dc_data);

            expression += "IIF(" + dc_data.ColumnName + ",1,0) +";
            day = day.AddDays(1);
        }

        if (expression.EndsWith("+")) expression = expression.Substring(0, expression.Length - 1);
        dt.Columns.Add(new DataColumn("Counts", Type.GetType("System.Int32"), expression));

        return dt;
    }
    #endregion

    #region 将现有工作日志明细载入DataTable中
    private DataTable LoadWorkingPlanDetail(int planid)
    {
        JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL(planid);

        DataTable dt = GenareateDataTable(bll.Model.BeginDate, bll.Model.EndDate);

        foreach (JN_WorkingPlanDetail detail in bll.Items)
        {
            DataRow[] rows = dt.Select("WorkingClassify = " + detail.WorkingClassify.ToString() +
                " AND Description = '" + detail.Description + "' " +
                " AND RelateStaff = " + detail.RelateStaff.ToString() +
                " AND RelateClient = " + detail.RelateClient.ToString() +
                " AND OfficialCity = " + detail.OfficialCity.ToString());
            if (rows.Length > 0)
            {
                DateTime date = detail.BeginTime;
                while (date <= detail.EndTime)
                {
                    rows[0]["P" + date.ToString("MMdd")] = true;
                    date = date.AddDays(1);
                }
            }
            else
            {
                DataRow dr = dt.NewRow();

                dr["WorkingClassify"] = detail.WorkingClassify;
                dr["WorkingClassifyName"] = DictionaryBLL.GetDicCollections("OA_WorkingClassify")[detail.WorkingClassify.ToString()].Name;
                dr["Description"] = detail.Description;

                dr["RelateClient"] = detail.RelateClient;
                if (detail.RelateClient > 0) dr["RelateClientName"] = new CM_ClientBLL(detail.RelateClient).Model.FullName;

                dr["RelateStaff"] = detail.RelateStaff;
                if (detail.RelateStaff > 0) dr["RelateStaffName"] = new Org_StaffBLL(detail.RelateStaff).Model.RealName;

                dr["OfficialCity"] = detail.OfficialCity;

                if (detail.OfficialCity > 0) dr["OfficialCityName"] = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", detail.OfficialCity).Replace("->", " ");

                DateTime date = detail.BeginTime;
                while (date <= detail.EndTime)
                {
                    dr["P" + date.ToString("MMdd")] = true;
                    date = date.AddDays(1);
                }

                if (detail["Cost1"] != "")
                    dr["Cost1"] = decimal.Parse(detail["Cost1"]);
                else
                    dr["Cost1"] = 0;

                if (detail["Cost2"] != "")
                    dr["Cost2"] = decimal.Parse(detail["Cost2"]);
                else
                    dr["Cost2"] = 0;
                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
    #endregion

    #region 保存界面数据至DataTable
    private void SaveDataTable()
    {
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];
            DateTime begindate = (DateTime)ViewState["BeginDate"];
            DateTime enddate = (DateTime)ViewState["EndDate"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TextBox t = ((TextBox)gv_List.Rows[i].FindControl("tbx_Description"));
                if (t != null)
                    dt.Rows[i]["Description"] = t.Text;
                else
                    dt.Rows[i]["Description"] = "";

                t = ((TextBox)gv_List.Rows[i].FindControl("tbx_Cost1"));
                if (t != null && t.Text != "")
                    dt.Rows[i]["Cost1"] = decimal.Parse(t.Text);
                else
                    dt.Rows[i]["Cost1"] = 0;

                t = ((TextBox)gv_List.Rows[i].FindControl("tbx_Cost2"));
                if (t != null && t.Text != "")
                    dt.Rows[i]["Cost2"] = decimal.Parse(t.Text);
                else
                    dt.Rows[i]["Cost2"] = 0;

                DateTime day = begindate;
                while (day <= enddate)
                {
                    TableCell cell = gv_List.Rows[i].Cells[(int)ViewState["FirstDateColumnIndex"] + (day - (DateTime)ViewState["BeginDate"]).Days];
                    dt.Rows[i]["P" + day.ToString("MMdd")] = ((CheckBox)cell.Controls[0]).Checked;
                    day = day.AddDays(1);
                }
            }
        }
    }
    #endregion

    #region 生成新计划表
    protected void bt_Create_Click(object sender, EventArgs e)
    {
        DateTime begindate = DateTime.Parse(tbx_begindate.Text);
        DateTime enddate = DateTime.Parse(tbx_enddate.Text);

        #region 规则校验
        if (select_PlanStaff.SelectValue == "")
        {
            MessageBox.Show(this, "请正确选择要填报计划的员工!");
            return;
        }
        if (begindate < DateTime.Today)
        {
            MessageBox.Show(this, "开始日期不能小于今天！");
            return;
        }

        if (enddate < begindate)
        {
            MessageBox.Show(this, "截止日期必须大于开始日期！");
            return;
        }

        if ((enddate - begindate).Days > 31)
        {
            MessageBox.Show(this, "日期范围不能超过一个月！");
            return;
        }

        if (JN_WorkingPlanBLL.GetModelList("Staff = " + select_PlanStaff.SelectValue +
            " AND ( (BeginDate BETWEEN '" + begindate.ToString("yyyy-MM-dd") + "' AND '" + enddate.ToString("yyyy-MM-dd") +
            "') OR ('" + begindate.ToString("yyyy-MM-dd") + "' BETWEEN BeginDate AND EndDate) )").Count > 0)
        {
            MessageBox.Show(this, "日期范围与系统中已填报的计划有日期重叠！");
            return;
        }
        #endregion

        InitGridView(begindate, enddate);
        DataTable dt = GenareateDataTable(begindate, enddate);

        #region 页面控件使能控制
        tr_adddetail.Visible = true;
        tbx_begindate.Enabled = false;
        tbx_enddate.Enabled = false;
        select_PlanStaff.Enabled = false;
        tr_OrganizeCity.Enabled = false;

        cbx_GenarateSynergetic.Visible = false;
        bt_Create.Visible = false;
        bt_Save.Visible = true;
        #endregion

        #region 载入该操作员所有负责客户，加入客户拜访记录计划中
        IList<CM_Client> clients = CM_ClientBLL.GetModelList("ClientManager=" + select_PlanStaff.SelectValue + " AND ActiveFlag=1 AND ApproveFlag=1");

        foreach (CM_Client client in clients)
        {
            DataRow dr = dt.NewRow();

            dr["WorkingClassify"] = "1";
            dr["WorkingClassifyName"] = DictionaryBLL.GetDicCollections("OA_WorkingClassify")["1"].Name;

            dr["RelateClient"] = client.ID;
            dr["RelateClientName"] = client.FullName;

            dr["RelateStaff"] = 0;
            dr["RelateStaffName"] = "";

            dr["OfficialCity"] = client.OfficalCity;
            if (client.OfficalCity > 0)
            {
                dr["OfficialCityName"] = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", client.OfficalCity).Replace("->", " ");
            }

            dt.Rows.Add(dr);
        }
        #endregion

        #region 加入协同拜访工作项，管理片区非总部员工，将所有下级员工加入
        if (cbx_GenarateSynergetic.Checked && int.Parse(tr_OrganizeCity.SelectValue) > 1)
        {
            #region 所有下级职位(不含本级职位)
            string positions = "";
            int position = new Org_StaffBLL(int.Parse(select_PlanStaff.SelectValue)).Model.Position;
            DataTable dtAllChilePosition = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Org_Position", "ID", "SuperID", position.ToString());
            for (int i = 0; i < dtAllChilePosition.Rows.Count; i++)
            {
                positions += dtAllChilePosition.Rows[i]["ID"].ToString();
                if (i != dtAllChilePosition.Rows.Count - 1) positions += ",";
            }
            #endregion

            #region 所有本级及下级管理片区
            string orgcitys = "";
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            #endregion

            if (positions != "" && orgcitys != "")
            {
                IList<Org_Staff> staffs = Org_StaffBLL.GetStaffList("OrganizeCity IN (" + orgcitys + ") AND Position IN (" + positions + ") AND Dimission=1 AND ApproveFlag=1");

                foreach (Org_Staff staff in staffs)
                {
                    DataRow dr = dt.NewRow();

                    dr["WorkingClassify"] = "2";
                    dr["WorkingClassifyName"] = DictionaryBLL.GetDicCollections("OA_WorkingClassify")["2"].Name;

                    dr["RelateClient"] = 0;
                    dr["RelateClientName"] = "";

                    dr["RelateStaff"] = staff.ID;
                    dr["RelateStaffName"] = staff.RealName;

                    dr["OfficialCity"] = staff.OfficialCity;
                    if (staff.OfficialCity > 0)
                    {
                        dr["OfficialCityName"] = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", staff.OfficialCity).Replace("->", " ");
                    }

                    dt.Rows.Add(dr);
                }
            }
        }
        #endregion

        #region 加入除客户拜访以外的工作项
        foreach (ListItem item in ddl_WorkingClassify.Items)
        {
            if (int.Parse(item.Value) >= 3)
            {
                DataRow dr = dt.NewRow();
                dr["WorkingClassify"] = int.Parse(item.Value);
                dr["WorkingClassifyName"] = item.Text;
                dr["Description"] = "";
                dr["RelateClient"] = 0;
                dr["RelateClientName"] = "";

                dr["RelateStaff"] = 0;
                dr["RelateStaffName"] = "";

                dr["OfficialCity"] = (int)ViewState["StaffOfficialCity"];
                if (ViewState["StaffOfficialCity"] != null && (int)ViewState["StaffOfficialCity"] != 0)
                {
                    dr["OfficialCityName"] = new Addr_OfficialCityBLL((int)ViewState["StaffOfficialCity"]).Model.Name;
                }
                else
                {
                    dr["OfficialCity"] = 0;
                }

                dr["Cost1"] = 0;
                dr["Cost2"] = 0;
                dt.Rows.Add(dr);
            }
        }
        #endregion

        ViewState["WorkingPlanData"] = dt;
        ViewState["BeginDate"] = begindate;
        ViewState["EndDate"] = enddate;

        BindGrid();
    }
    #endregion

    #region 增加新工作计划项
    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        SaveDataTable();

        if (ddl_WorkingClassify.SelectedValue == "0")
        {
            MessageBox.Show(this, "请先选择要增加的工作类型!");
            return;
        }
        else if (ddl_WorkingClassify.SelectedValue == "1" && string.IsNullOrEmpty(select_RelateClient.SelectValue))
        {
            MessageBox.Show(this, "请先选择你要拜访的客户!");
            return;
        }
        else if (ddl_WorkingClassify.SelectedValue == "2" && string.IsNullOrEmpty(select_RelateStaff.SelectValue))
        {
            MessageBox.Show(this, "请先选择你要协同拜访的员工!");
            return;
        }

        DateTime begindate = DateTime.Parse(tbx_Begin.Text);
        DateTime enddate = DateTime.Parse(tbx_End.Text);

        if (begindate < (DateTime)ViewState["BeginDate"])
        {
            MessageBox.Show(this, "本工作项的开始日期不能小于整个计划开始日期!");
            return;
        }
        if (enddate > (DateTime)ViewState["EndDate"])
        {
            MessageBox.Show(this, "本工作项的截止日期不能大于整个计划截止日期!");
            return;
        }
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];

            DataRow dr = dt.NewRow();
            dr["WorkingClassify"] = int.Parse(ddl_WorkingClassify.SelectedValue);
            dr["WorkingClassifyName"] = ddl_WorkingClassify.SelectedItem.Text;

            switch (ddl_WorkingClassify.SelectedValue)
            {
                case "1":
                    #region 增加客户拜访计划
                    {
                        if (select_RelateClient.SelectValue == "" || select_RelateClient.SelectValue == "0")
                        {
                            MessageBox.Show(this, "请选择要拜访的客户！");
                            return;
                        }
                        if (dt.Select("WorkingClassify=1 AND RelateClient=" + select_RelateClient.SelectValue).Length > 0)
                        {
                            MessageBox.Show(this, "对不起，列表中已有相同的行了，不能重复添加！");
                            return;
                        }

                        dr["RelateClient"] = int.Parse(select_RelateClient.SelectValue);
                        dr["RelateClientName"] = select_RelateClient.SelectText;
                        dr["RelateStaff"] = 0;
                    }
                    #endregion
                    break;
                case "2":
                    #region 循环增加协同拜访员工计划
                    {
                        int staffid = 0;
                        if (!int.TryParse(select_RelateStaff.SelectValue, out staffid)) return;
                        if (staffid == int.Parse(select_PlanStaff.SelectValue)) return;
                        Org_Staff staff = new Org_StaffBLL(staffid).Model;
                        if (staff == null) return;

                        if (dt.Select("WorkingClassify=2 AND RelateStaff=" + staffid.ToString()).Length > 0)
                        {
                            MessageBox.Show(this, "对不起，列表中已有相同的行了，不能重复添加！");
                            return;
                        };


                        dr["RelateStaff"] = staffid;
                        dr["RelateStaffName"] = staff.RealName;
                        dr["RelateClient"] = 0;

                        if (staff.OfficialCity > 1)
                        {
                            dr["OfficialCity"] = staff.OfficialCity;
                            dr["OfficialCityName"] = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", staff.OfficialCity).Replace("->", " "); ;
                        }
                        dt.Rows.Add(dr);
                    }
                    #endregion
                    break;
                default:
                    #region 增加其他工作计划
                    {
                        if (dt.Select("WorkingClassify=" + ddl_WorkingClassify.SelectedValue +
                            " AND Description='" + tbx_Description.Text +
                            "' AND OfficialCity=" + tr_OfficailCity.SelectValue).Length > 0)
                        {
                            MessageBox.Show(this, "对不起，列表中已有相同的行了，不能重复添加！");
                            return;
                        }

                        dr["RelateStaff"] = 0;
                        dr["RelateClient"] = 0;
                        dr["Description"] = tbx_Description.Text;
                    }
                    #endregion
                    break;
            }

            if (!string.IsNullOrEmpty(tr_OfficailCity.SelectValue) && tr_OfficailCity.SelectValue != "0")
            {
                dr["OfficialCity"] = int.Parse(tr_OfficailCity.SelectValue);
                dr["OfficialCityName"] = new Addr_OfficialCityBLL(int.Parse(tr_OfficailCity.SelectValue)).Model.Name;
            }
            if (tbx_Cost1.Text != "")
                dr["Cost1"] = decimal.Parse(tbx_Cost1.Text);
            else
                dr["Cost1"] = 0;

            if (tbx_Cost2.Text != "")
                dr["Cost2"] = decimal.Parse(tbx_Cost2.Text);
            else
                dr["Cost2"] = 0;

            DateTime day = begindate;
            while (day <= enddate)
            {
                dr["P" + day.ToString("MMdd")] = true;
                day = day.AddDays(1);
            }

            dt.Rows.Add(dr);
            #region 清空已录入的工作计划数据，以便下次再录
            select_RelateStaff.SelectValue = "";
            select_RelateStaff.SelectText = "";
            select_RelateClient.SelectValue = "";
            select_RelateClient.SelectText = "";
            if (ViewState["StaffOfficialCity"] != null)
            {
                tr_OfficailCity.SelectValue = ViewState["StaffOfficialCity"].ToString();
            }
            #endregion

            cb_DisplayCheckedOnly.Checked = false;
            BindGrid();
        }
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SaveDataTable();

        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];
            Decimal totalcost = (decimal)dt.Compute("SUM(Cost1)+SUM(Cost2)", "");

            JN_WorkingPlanBLL bll;
            if ((int)ViewState["ID"] > 0)
                bll = new JN_WorkingPlanBLL((int)ViewState["ID"]);
            else
                bll = new JN_WorkingPlanBLL();

            bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            bll.Model.BeginDate = (DateTime)ViewState["BeginDate"];
            bll.Model.EndDate = (DateTime)ViewState["EndDate"];
            bll.Model.Staff = int.Parse(select_PlanStaff.SelectValue);
            bll.Model["TotalCost"] = totalcost.ToString("0.#");

            pl_detail.GetData(bll.Model);

            if (bll.Model.Staff == 0)
            {
                MessageBox.Show(this, "请选择该计划的员工!");
                return;
            }

            if ((int)ViewState["ID"] > 0)
            {
                bll.Update();
            }
            else
            {
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Model.State = 1;
                bll.Model.ApproveFlag = 2;
                ViewState["ID"] = bll.Add();
            }

            #region 将界面中已选择的计划放入IList中
            IList<JN_WorkingPlanDetail> PlanDetails = new List<JN_WorkingPlanDetail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JN_WorkingPlanDetail detail = null;
                bool savedcost = false;
                DateTime day = bll.Model.BeginDate;
                while (day <= bll.Model.EndDate)
                {
                    if ((bool)dt.Rows[i]["P" + day.ToString("MMdd")])
                    {
                        if (detail == null)
                        {
                            detail = new JN_WorkingPlanDetail();
                            detail.WorkingClassify = (int)dt.Rows[i]["WorkingClassify"];
                            detail.RelateClient = (int)dt.Rows[i]["RelateClient"];
                            detail.RelateStaff = (int)dt.Rows[i]["RelateStaff"];
                            detail.OfficialCity = (int)dt.Rows[i]["OfficialCity"];
                            detail.Description = (string)dt.Rows[i]["Description"];
                            detail.BeginTime = day;
                            detail.EndTime = day;
                            if (!savedcost)
                            {
                                detail["Cost1"] = ((decimal)dt.Rows[i]["Cost1"]).ToString("0.##");
                                detail["Cost2"] = ((decimal)dt.Rows[i]["Cost2"]).ToString("0.##");
                                savedcost = true;
                            }
                            else
                            {
                                detail["Cost1"] = "0";
                                detail["Cost2"] = "0";
                            }
                            PlanDetails.Add(detail);
                        }
                        else
                        {
                            detail.EndTime = day;
                        }
                    }
                    else
                    {
                        detail = null;
                    }
                    day = day.AddDays(1);
                }
            }
            #endregion

            #region 将数据库中已存的计划与IList作比较，相同的从List中移除，不存在于List中的从数据中移除
            foreach (JN_WorkingPlanDetail orgitem in bll.Items)
            {
                JN_WorkingPlanDetail m = PlanDetails.FirstOrDefault(p => p.WorkingClassify == orgitem.WorkingClassify &&
                    p.RelateClient == orgitem.RelateClient && p.RelateStaff == orgitem.RelateStaff &&
                    p.OfficialCity == orgitem.OfficialCity && p.Description == orgitem.Description &&
                    p.BeginTime == orgitem.BeginTime && p.EndTime == orgitem.EndTime &&
                    p["Cost1"] == orgitem["Cost1"] && p["Cost2"] == orgitem["Cost2"]);

                if (m == null)
                    bll.DeleteDetail(orgitem.ID);
                else
                    PlanDetails.Remove(m);
            }
            #endregion

            #region 将剩余下来的计划，新增到数据库中
            foreach (JN_WorkingPlanDetail item in PlanDetails)
            {
                bll.AddDetail(item);
            }
            #endregion

            Response.Redirect("WorkingPlan_CalendarList.aspx?ID=" + ViewState["ID"].ToString());
        }
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL((int)ViewState["ID"]);
            bll.DeleteDetail();
            bll.Delete();

            Response.Redirect("WorkingPlan_List.aspx");
        }
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL((int)ViewState["ID"]);

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("Position", new Org_StaffBLL(bll.Model.Staff).Model.Position.ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("TotalCost", bll.Model["TotalCost"]);

            string title = select_PlanStaff.SelectText + " 开始日期:" + tbx_begindate.Text + "至" + tbx_enddate.Text + bll.Model.Title;

            int TaskID = EWF_TaskBLL.NewTask("JN_WorkingPlan_Apply", (int)Session["UserID"], title, "~/SubModule/OA/Journal/WorkingPlan_CalendarList.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            #endregion

            if (TaskID > 0) bll.Submit((int)Session["UserID"], TaskID);

            Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());

        }
    }

    protected void bt_Journal_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalOnWorkingPlan.aspx?PlanID=" + ViewState["ID"].ToString());
    }
}
