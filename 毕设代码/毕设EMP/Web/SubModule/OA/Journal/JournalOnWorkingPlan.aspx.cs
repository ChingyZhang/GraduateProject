using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using System.Drawing;
using MCSFramework.Common;

public partial class SubModule_OA_Journal_JournalOnWorkingPlan : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL(int.Parse(Request.QueryString["PlanID"]));
        InitGridView(bll.Model.BeginDate, bll.Model.EndDate);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PlanID"] = Request.QueryString["PlanID"] == null ? 0 : int.Parse(Request.QueryString["PlanID"]);

            BindDropDown();

            if ((int)ViewState["PlanID"] == 0)
            {

            }
            else
            {
                //载入已有的工作计划
                BindData();
            }
        }

        #region 注册脚本
        string script = "function OpenJournal(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?ID='+id+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenJournal", script, true);

        script = "function NewJournal(){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NewJournal", script, true);
        #endregion
    }

    private void BindDropDown()
    {

    }

    protected void cb_DisplayCheckedOnly_CheckedChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    /// <summary>
    /// 载入已有的工作项
    /// </summary>
    private void BindData()
    {
        JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL((int)ViewState["PlanID"]);

        ViewState["WorkingPlanData"] = LoadWorkingPlanDetail(bll.Model.ID);
        ViewState["BeginDate"] = bll.Model.BeginDate;
        ViewState["EndDate"] = bll.Model.EndDate;
        ViewState["PlanStaff"] = bll.Model.Staff;

        if ((int)Session["UserID"] != (int)ViewState["PlanStaff"])
        {
            cb_DisplayCheckedOnly.Visible = false;
            cb_DisplayCheckedOnly.Checked = true;
            bt_AddJournal.Visible = false;
            bt_Save.Visible = false;
        }
        else if (bll.Model.EndDate < DateTime.Today)
            cb_DisplayCheckedOnly.Checked = true;


        pl_detail.BindData(bll.Model);

        BindGrid();
        UploadFile1.RelateID = bll.Model.ID;
        UploadFile1.DataBind();
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
                    TableCell cell = gv_List.Rows[i].Cells[(int)ViewState["FirstDateColumnIndex"] + (day - (DateTime)ViewState["BeginDate"]).Days];

                    if ((bool)dt.Rows[i]["P" + day.ToString("MMdd")])
                    {
                        cell.BackColor = Color.Yellow;
                    }

                    if ((bool)dt.Rows[i]["J" + day.ToString("MMdd")])
                    {
                        ((CheckBox)cell.Controls[0]).Checked = true;
                        if ((int)dt.Rows[i]["JID" + day.ToString("MMdd")] > 0)
                        {
                            ((HyperLink)cell.Controls[1]).Visible = true;
                            ((HyperLink)cell.Controls[1]).NavigateUrl = "javascript:OpenJournal(" + dt.Rows[i]["JID" + day.ToString("MMdd")].ToString() + ")";
                        }
                    }
                    else if (cb_DisplayCheckedOnly.Checked)
                    {
                        ((CheckBox)cell.Controls[0]).Visible = false;
                    }

                    if ((int)Session["UserID"] != (int)ViewState["PlanStaff"])
                    {
                        ((CheckBox)cell.Controls[0]).Enabled = false;
                    }
                    day = day.AddDays(1);
                }
            }
        }
    }

    #region 初始化GridView,生成日期列
    private void InitGridView(DateTime begindate, DateTime enddate)
    {
        ViewState["FirstDateColumnIndex"] = gv_List.Columns.Count;

        DateTime day = begindate;
        while (day <= enddate)
        {

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

            TemplateField customField = new TemplateField();
            customField.ShowHeader = true;
            customField.HeaderTemplate = new JournalPlanTemplate("<center>" + day.ToString("M.d") + "<br/>" + week + "</center>");
            customField.HeaderStyle.Width = new Unit(35);

            customField.ItemTemplate = new JournalPlanTemplate(day, new EventHandler(cbx_Journal_CheckedChanged));
            customField.ItemStyle.Width = new Unit(35);
            customField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            gv_List.Columns.Add(customField);

            day = day.AddDays(1);
        }
        if (620 + (enddate - begindate).Days * 40 > 1024)
            gv_List.Width = new Unit(620 + (enddate - begindate).Days * 40);
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

        string expression_p = "", expression_j = "";
        DateTime day = begindate;
        while (day <= enddate)
        {
            DataColumn dc_data1 = new DataColumn("P" + day.ToString("MMdd"), Type.GetType("System.Boolean"));
            dc_data1.DefaultValue = false;
            dt.Columns.Add(dc_data1);

            DataColumn dc_data2 = new DataColumn("J" + day.ToString("MMdd"), Type.GetType("System.Boolean"));
            dc_data2.DefaultValue = false;
            dt.Columns.Add(dc_data2);

            DataColumn dc_data3 = new DataColumn("JID" + day.ToString("MMdd"), Type.GetType("System.Int32"));
            dc_data3.DefaultValue = 0;
            dt.Columns.Add(dc_data3);

            expression_p += "IIF(" + dc_data1.ColumnName + ",1,0) +";
            expression_j += "IIF(" + dc_data2.ColumnName + ",1,0) +";
            day = day.AddDays(1);
        }

        if (expression_p.EndsWith("+")) expression_p = expression_p.Substring(0, expression_p.Length - 1);
        if (expression_j.EndsWith("+")) expression_j = expression_j.Substring(0, expression_j.Length - 1);
        dt.Columns.Add(new DataColumn("P_Counts", Type.GetType("System.Int32"), expression_p));
        dt.Columns.Add(new DataColumn("J_Counts", Type.GetType("System.Int32"), expression_j));

        return dt;
    }
    #endregion

    #region 将现有工作日志明细载入DataTable中
    private DataTable LoadWorkingPlanDetail(int planid)
    {
        JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL(planid);

        DataTable dt = GenareateDataTable(bll.Model.BeginDate, bll.Model.EndDate);

        #region 载入原工作计划
        foreach (JN_WorkingPlanDetail detail in bll.Items.OrderBy(p => p.WorkingClassify))
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
        #endregion

        #region 加载数据库中已填报的日志
        string con = "Staff = " + bll.Model.Staff.ToString() +
            " AND BeginTime BETWEEN '" + bll.Model.BeginDate.ToString("yyyy-MM-dd") + "' AND '" + bll.Model.EndDate.ToString("yyyy-MM-dd 23:59:59") + "' " +
            " AND JournalType = 1";
        IList<JN_Journal> Journals = JN_JournalBLL.GetModelList(con);

        foreach (JN_Journal journal in Journals.OrderBy(p => p.WorkingClassify))
        {
            DataRow[] rows = dt.Select("WorkingClassify = " + journal.WorkingClassify.ToString() +
                " AND Description = '" + journal.Title + "' " +
                " AND RelateStaff = " + (journal.WorkingClassify == 2 ? journal.RelateStaff.ToString() : "0") +
                " AND RelateClient = " + (journal.WorkingClassify == 1 ? journal.RelateClient.ToString() : "0") +
                " AND OfficialCity = " + journal.OfficialCity.ToString());

            if (rows.Length == 0)
            {
                rows = dt.Select("WorkingClassify = " + journal.WorkingClassify.ToString() +
                " AND Description = '' " +
                " AND RelateStaff = " + (journal.WorkingClassify == 2 ? journal.RelateStaff.ToString() : "0") +
                " AND RelateClient = " + (journal.WorkingClassify == 1 ? journal.RelateClient.ToString() : "0") +
                " AND OfficialCity = " + journal.OfficialCity.ToString());
            }

            if (rows.Length > 0)
            {
                rows[0]["J" + journal.BeginTime.ToString("MMdd")] = true;
                rows[0]["JID" + journal.BeginTime.ToString("MMdd")] = journal.ID;

                DateTime date = journal.BeginTime;
                while (date <= journal.EndTime)
                {
                    rows[0]["J" + date.ToString("MMdd")] = true;
                    rows[0]["JID" + date.ToString("MMdd")] = journal.ID;
                    date = date.AddDays(1);
                }
            }
            else
            {
                #region 增加无计划的日志
                DataRow dr = dt.NewRow();

                dr["WorkingClassify"] = journal.WorkingClassify;
                if (journal.WorkingClassify > 0) dr["WorkingClassifyName"] = DictionaryBLL.GetDicCollections("OA_WorkingClassify")[journal.WorkingClassify.ToString()].Name;
                dr["Description"] = journal.Title;

                dr["RelateClient"] = journal.RelateClient;
                if (journal.RelateClient > 0) dr["RelateClientName"] = new CM_ClientBLL(journal.RelateClient).Model.FullName;

                dr["RelateStaff"] = journal.RelateStaff;
                if (journal.RelateStaff > 0) dr["RelateStaffName"] = new Org_StaffBLL(journal.RelateStaff).Model.RealName;

                dr["OfficialCity"] = journal.OfficialCity;
                if (journal.OfficialCity > 0) dr["OfficialCityName"] = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", journal.OfficialCity).Replace("->", " ");

                DateTime date = journal.BeginTime;
                while (date <= journal.EndTime)
                {
                    dr["J" + date.ToString("MMdd")] = true;
                    dr["JID" + date.ToString("MMdd")] = journal.ID;
                    date = date.AddDays(1);
                }

                dt.Rows.Add(dr);
                #endregion
            }
        }
        #endregion
        return dt;
    }
    #endregion

    protected void cbx_Journal_CheckedChanged(object sender, EventArgs e)
    {
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];

            CheckBox cbx = (CheckBox)sender;
            int row = ((GridViewRow)cbx.Parent.Parent).RowIndex;
            DateTime day = DateTime.Parse(cbx.Attributes["Day"]);

            if (!cbx.Checked && (int)dt.Rows[row]["JID" + day.ToString("MMdd")] > 0)
            {
                if (new JN_JournalBLL((int)dt.Rows[row]["JID" + day.ToString("MMdd")]).Model.InsertTime < DateTime.Today)
                {
                    MessageBox.Show(this, "对不起，您不能取消今天以前填报的日志!");
                    cbx.Checked = !cbx.Checked;
                    UpdatePanel2.Update();
                    return;
                }
            }

            //if (day > DateTime.Today)
            //{
            //    MessageBox.Show(this, "对不起，您不能超前填报工作日志!");
            //    cbx.Checked = !cbx.Checked;
            //    UpdatePanel2.Update();
            //    return;
            //}

            dt.Rows[row]["J" + day.ToString("MMdd")] = cbx.Checked;
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (ViewState["WorkingPlanData"] != null)
        {
            DataTable dt = (DataTable)ViewState["WorkingPlanData"];
            DateTime begindate = (DateTime)ViewState["BeginDate"];
            DateTime enddate = (DateTime)ViewState["EndDate"];

            #region 将界面中已选择的日志放入IList中
            IList<JN_Journal> NewJournals = new List<JN_Journal>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JN_Journal jn = null;
                DateTime day = begindate;
                while (day <= enddate)
                {
                    if ((bool)dt.Rows[i]["J" + day.ToString("MMdd")])
                    {
                        #region 新增日志
                        if (jn == null)
                        {
                            jn = new JN_Journal();
                            jn.Title = (string)dt.Rows[i]["Description"];
                            jn.OrganizeCity = new Org_StaffBLL((int)ViewState["PlanStaff"]).Model.OrganizeCity;
                            jn.JournalType = 1;
                            jn.Staff = (int)ViewState["PlanStaff"];
                            jn.BeginTime = day.AddHours(8.5);
                            jn.EndTime = day.AddHours(17.5);
                            jn.WorkingClassify = (int)dt.Rows[i]["WorkingClassify"];
                            if (jn.WorkingClassify == 1)
                            {
                                jn.RelateClient = (int)dt.Rows[i]["RelateClient"];
                                jn.Title = "[客户拜访]" + dt.Rows[i]["RelateClientName"] + jn.Title;
                            }
                            if (jn.WorkingClassify == 2)
                            {
                                jn.RelateStaff = (int)dt.Rows[i]["RelateStaff"];
                                jn["CCSynergeticStaff"] = "N";
                                jn.Title = "[协同拜访]" + dt.Rows[i]["RelateStaffName"] + jn.Title;
                            }
                            jn.OfficialCity = (int)dt.Rows[i]["OfficialCity"];

                            jn["IPAddress"] = Request.UserHostAddress;
                            jn["IPLocation"] = Const_IPLocationBLL.FindByIP(Request.UserHostAddress).Location;
                            jn.ApproveFlag = 2;
                            jn.InsertStaff = (int)Session["UserID"];

                            NewJournals.Add(jn);

                            //如果每天都生成一条独立的日志，则启用这一行
                            jn = null;
                        }
                        else
                        {
                            jn.EndTime = day.AddHours(17);
                        }
                        #endregion
                    }
                    else
                    {
                        jn = null;
                    }

                    day = day.AddDays(1);
                }
            }
            #endregion

            #region 将数据库中已存的计划与IList作比较，相同的从List中移除，不存在于List中的从数据中移除
            JN_WorkingPlanBLL bll = new JN_WorkingPlanBLL((int)ViewState["PlanID"]);
            string con = "Staff = " + bll.Model.Staff.ToString() +
            " AND BeginTime BETWEEN '" + bll.Model.BeginDate.ToString("yyyy-MM-dd") + "' AND '" + bll.Model.EndDate.ToString("yyyy-MM-dd 23:59:59") + "' " +
            " AND JournalType = 1";
            IList<JN_Journal> OrgJournals = JN_JournalBLL.GetModelList(con);
            foreach (JN_Journal orgitem in OrgJournals)
            {
                JN_Journal m = null;
                if (orgitem.WorkingClassify == 1)
                {
                    m = NewJournals.FirstOrDefault(p => p.WorkingClassify == orgitem.WorkingClassify &&
                        p.RelateClient == orgitem.RelateClient &&
                        p.BeginTime.Date == orgitem.BeginTime.Date && p.EndTime.Date == orgitem.EndTime.Date);
                }
                else if (orgitem.WorkingClassify == 2)
                {
                    m = NewJournals.FirstOrDefault(p => p.WorkingClassify == orgitem.WorkingClassify &&
                       p.RelateStaff == orgitem.RelateStaff &&
                       p.BeginTime.Date == orgitem.BeginTime.Date && p.EndTime.Date == orgitem.EndTime.Date);
                }
                else
                {
                    m = NewJournals.FirstOrDefault(p => p.WorkingClassify == orgitem.WorkingClassify &&
                         p.OfficialCity == orgitem.OfficialCity && p.Title == orgitem.Title &&
                         p.BeginTime.Date == orgitem.BeginTime.Date && p.EndTime.Date == orgitem.EndTime.Date);
                }

                if (m == null)
                    new JN_JournalBLL(orgitem.ID).Delete();   //删除日志
                else
                    NewJournals.Remove(m);
            }
            #endregion

            #region 将剩余下来的计划，新增到数据库中
            foreach (JN_Journal item in NewJournals)
            {
                JN_JournalBLL jnbll = new JN_JournalBLL();
                jnbll.Model = item;
                jnbll.Add();
            }
            #endregion

            BindGrid();

            MessageBox.ShowAndRedirect(this, "与该计划相关联的实际工作日志填报保存成功!", "JournalOnWorkingPlan.aspx?PlanID=" + ViewState["PlanID"].ToString());
        }
    }

    protected void bt_AddJournal_Click(object sender, EventArgs e)
    {
        ViewState["WorkingPlanData"] = LoadWorkingPlanDetail((int)ViewState["PlanID"]);
        BindGrid();
    }

    #region 预定义模板列
    public class JournalPlanTemplate : ITemplate
    {
        private DataControlRowType _templateType = DataControlRowType.DataRow;
        private string _columnHeaderText = "";
        private DateTime _day = new DateTime(1900, 1, 1);

        private EventHandler _eventJournalCheckedChanged;

        public JournalPlanTemplate(string ColumnHeaderText)
        {
            _templateType = DataControlRowType.Header;
            _columnHeaderText = ColumnHeaderText;
        }

        public JournalPlanTemplate(DateTime Day, EventHandler CheckedChanged)
        {
            _templateType = DataControlRowType.DataRow;
            _day = Day;
            _eventJournalCheckedChanged = CheckedChanged;
        }

        #region ITemplate 成员
        void ITemplate.InstantiateIn(Control container)
        {
            switch (_templateType)
            {
                case DataControlRowType.Header:
                    Literal lc = new Literal();
                    lc.Text = _columnHeaderText;
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:

                    CheckBox cbx_Journal = new CheckBox();
                    cbx_Journal.ID = "cbx_Journal_" + _day.ToString("MMdd");

                    cbx_Journal.CheckedChanged += _eventJournalCheckedChanged;
                    cbx_Journal.AutoPostBack = true;
                    cbx_Journal.Attributes["Day"] = _day.ToString("yyyy-MM-dd");
                    container.Controls.Add(cbx_Journal);

                    HyperLink hyl_Detail = new HyperLink();
                    hyl_Detail.ID = "hyl_Detail_" + _day.ToString("MMdd");
                    //hyl_Detail.Target = "_blank";
                    hyl_Detail.Text = " 详细";
                    hyl_Detail.CssClass = "listViewTdLinkS1";
                    hyl_Detail.ImageUrl = "~/Images/gif/gif-0162.gif";
                    hyl_Detail.Visible = false;
                    container.Controls.Add(hyl_Detail);
                    break;
                default:
                    break;
            }
        }


        #endregion
    }
    #endregion


}
