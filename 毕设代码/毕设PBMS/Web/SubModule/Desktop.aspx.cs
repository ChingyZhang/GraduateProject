using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.Model.EWF;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.UD_Control;
using MCSFramework.BLL.RPT;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_Desktop : System.Web.UI.Page
{
    protected static string Username;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page.DataBind();

            if ((int)Session["AccountType"] != 1)
            {
                //非员工
                tbl_MyTask.Visible = false;   //我的任务
                tbl_BBS1.Visible = false;       //各分论坛战最新发帖
                tbl_BBS.Visible = false;        //论坛最新发帖及回帖
                tbl_Decission.Visible = false;  //我的审批                    
            }

            if (tbl_MyTask.Visible) BindTodayTask(true);
            if (tbl_Decission.Visible) BindDession();
            if (tbl_Mail.Visible) BindMail();
            if (tbl_Notice.Visible) BindNotice();

            if (tbl_BBS.Visible) BindReplyLatest();
            if (tbl_BBS1.Visible) BindBSS();
            if (tbl_NewKB.Visible) BindNewKB();

        }
        if (ViewState["JobDecisionCount"] != null && ViewState["JobDecisionCount_Agency"] != null
            && ViewState["JobInviteConsultCount"] != null && ViewState["JobCCCount"] != null && ViewState["MyInitTask"] != null)
        {
            if ((int)ViewState["JobDecisionCount"] > 0) MCSTab_EWF.Items[0].Text += "-<b><font color=red>" + ViewState["JobDecisionCount"].ToString() + "</font></b>";
            if ((int)ViewState["JobDecisionCount_Agency"] > 0) MCSTab_EWF.Items[0].Text += "-<b><font color=red>代理-" + ViewState["JobDecisionCount_Agency"].ToString() + "</font></b>";
            if ((int)ViewState["JobInviteConsultCount"] > 0) MCSTab_EWF.Items[1].Text += "-<b><font color=red>" + ViewState["JobInviteConsultCount"].ToString() + "</font></b>";
            if ((int)ViewState["JobCCCount"] > 0) MCSTab_EWF.Items[2].Text += "-<b><font color=red>" + ViewState["JobCCCount"].ToString() + "</font></b>";
            if ((int)ViewState["MyInitTask"] > 0) MCSTab_EWF.Items[3].Text += "-<b><font color=red>" + ViewState["MyInitTask"].ToString() + "</font></b>";
        }
    }


    #region 绑定我的邮件
    private void BindMail()
    {
        gv_Mail.BindGrid<ML_Mail>(ML_MailBLL.GetRecieveMail((string)Session["UserName"]));
    }

    protected string DisplayHasAttachFile(int id)
    {
        if (new ML_MailBLL(id).GetAttachFiles().Count > 0)
            return "<img src='../DataImages/attach.gif' border='0'>";
        else
            return "";
    }
    protected void gv_Mail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Mail.PageIndex = e.NewPageIndex;
        BindMail();
    }
    #endregion

    #region 绑定最1个月的公告
    private void BindNotice()
    {
        IList<PN_Notice> notices = null;

        //无导和有导店看不同的公告
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "ViewALLNotice"))
        {
            //查看所有公告
            notices = PN_NoticeBLL.GetModelList("PN_Notice.IsDelete ='N'  AND PN_Notice.InsertTime > '" +
                DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "' AND PN_Notice.ApproveFlag=1 " +
                " ORDER BY MCS_SYS.dbo.UF_Spilt(PN_Notice.ExtPropertys,'|',1) DESC, PN_Notice.InsertTime DESC");
        }
        else
        {
            notices = PN_NoticeBLL.GetNoticeByStaff((int)Session["UserID"]);
        }

        gv_Notice.BindGrid<PN_Notice>(notices.Where(p => p["Catalog"] == "" || int.Parse(p["Catalog"]) < 200).ToList());

        //获取特殊公告
        PN_Notice specialnotice = notices.FirstOrDefault(p => p["IsSpecial"] == "1");
        if (specialnotice != null)
        {
            lab_SpecialPN.Text = specialnotice.Content;
        }

    }
    protected void gv_Notice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Notice.PageIndex = e.NewPageIndex;
        BindNotice();
    }
    #endregion

    #region 绑定我的审批
    private void GetEWFCount()
    {
        DataTable dt = EWF_Task_JobBLL.GetMyJobCount((int)Session["UserID"]);
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["JobDecisionCount"] = dt.Rows[0]["JobDecisionCount"];
            ViewState["JobDecisionCount_Agency"] = dt.Rows[0]["JobDecisionCount_Agency"];
            ViewState["JobInviteConsultCount"] = dt.Rows[0]["JobInviteConsultCount"];
            ViewState["JobCCCount"] = dt.Rows[0]["JobCCCount"];
            ViewState["MyInitTask"] = dt.Rows[0]["MyInitTask"];
        }
    }

    protected void MCSTab_EWF_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_Decission.Visible = false;
        gv_EWFInviteConsultList.Visible = false;
        gv_EWFCCList.Visible = false;
        gv_MyTaskList.Visible = false;

        switch (MCSTab_EWF.SelectedIndex)
        {
            case 0:
                hy_EWFMore.NavigateUrl = "EWF/TaskList_NeedDecision.aspx";
                gv_Decission.Visible = true;
                gv_Decission.PageIndex = 0;
                BindDession();
                break;
            case 1:
                hy_EWFMore.NavigateUrl = "EWF/TaskList_InviteConsult.aspx";
                gv_EWFInviteConsultList.Visible = true;
                gv_EWFInviteConsultList.PageIndex = 0;
                BindInviteConsult();
                break;
            case 2:
                hy_EWFMore.NavigateUrl = "EWF/TaskList_CC.aspx";
                gv_EWFCCList.Visible = true;
                gv_EWFCCList.PageIndex = 0;
                BindEWFCC();
                break;
            case 3:
                hy_EWFMore.NavigateUrl = "EWF/TaskList_InitByMe.aspx";
                gv_MyTaskList.Visible = true;
                gv_MyTaskList.PageIndex = 0;
                BindMyTask();
                break;
        }
    }
    private void BindDession()
    {
        GetEWFCount();

        DataTable dt = EWF_Task_JobBLL.GetJobToDecision(int.Parse(Session["UserID"].ToString()));
        string condition = ConfigHelper.GetConfigString("Desktop_EWF_GetDessionCondition");
        if (!string.IsNullOrEmpty(condition)) dt.DefaultView.RowFilter = condition;

        gv_Decission.DataSource = dt.DefaultView;
        gv_Decission.DataBind();

        hy_ApproveFeeApplySummary.Visible = dt.Select("AppCode Like 'FNA_FeeApplyFlow%'").Length > 0;
    }
    private void BindInviteConsult()
    {
        DataTable dt = EWF_Task_InviteConsultBLL.GetNeedConsult((int)Session["UserID"]);

        gv_EWFInviteConsultList.DataSource = dt;
        gv_EWFInviteConsultList.DataBind();
    }
    private void BindEWFCC()
    {
        DataTable dt = EWF_Task_JobCCBLL.GetListByRecipientStaff((int)Session["UserID"], DateTime.Now.AddDays(-7), DateTime.Now);

        dt.DefaultView.RowFilter = "ReadFlag='N' OR (ReadTime>'" + DateTime.Today + "')";
        gv_EWFCCList.DataSource = dt.DefaultView;
        gv_EWFCCList.DataBind();
    }
    private void BindMyTask()
    {
        IList<EWF_Task> list = EWF_TaskBLL.GetByInitiator(Guid.Empty, (int)Session["UserID"],
            DateTime.Now.AddMonths(-6), DateTime.Now, 3, 0);

        gv_MyTaskList.BindGrid<EWF_Task>(list);
    }

    protected void gv_Decission_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Decission.PageIndex = e.NewPageIndex;
        BindDession();
    }
    protected void gv_EWFInviteConsultList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_EWFInviteConsultList.PageIndex = e.NewPageIndex;
        BindInviteConsult();
    }
    protected void gv_EWFCCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_EWFCCList.PageIndex = e.NewPageIndex;
        BindEWFCC();
    }
    protected void gv_MyTaskList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_MyTaskList.PageIndex = e.NewPageIndex;
        BindMyTask();
    }
    #endregion

    #region 绑定我的任务
    protected void bt_RefreshTask_Click(object sender, ImageClickEventArgs e)
    {
        BindTodayTask(true);
    }
    private void BindTodayTask(bool nocache)
    {
        if ((int)Session["AccountType"] == 1)
        {
            DataTable dt = Org_StaffBLL.GetTodayTask(Session["UserName"].ToString(), nocache);
            gv_TodayTask.DataSource = dt;
            gv_TodayTask.BindGrid();
        }
    }
    #endregion

    #region 绑定论坛
    private void BindReplyLatest()
    {
        DataTable dt = BBS_ForumReplyBLL.GetTopReplyLatest(10);
        gv_ReplyLatest.DataSource = dt;
        gv_ReplyLatest.BindGrid();
    }

    private void BindBSS()
    {
        string boards = ConfigHelper.GetConfigString("BBSBoard");             //获取要查询的板块
        if (string.IsNullOrEmpty(boards)) return;

        dl_Board.DataSource = BBS_BoardBLL.GetModelList("ID IN (" + boards + ")");
        dl_Board.DataBind();

        return;


    }

    protected void dl_Board_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BBS_Board Board = (BBS_Board)e.Item.DataItem;
            UC_GridView grd_BBS = (UC_GridView)e.Item.FindControl("grd_BBS");

            grd_BBS.PageIndex = 0;
            BindBBSGridView(grd_BBS, Board.ID);
        }
    }
    protected void grd_BBS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int BoardID = 0;
        int.TryParse(((GridView)sender).Attributes["BoardID"], out BoardID);
        if (BoardID > 0)
        {
            UC_GridView grd_BBS = (UC_GridView)sender;
            grd_BBS.PageIndex = e.NewPageIndex;
            BindBBSGridView(grd_BBS, BoardID);
        }
    }
    private void BindBBSGridView(UC_GridView grd_BBS, int BoardID)
    {
        string condition = "[Board] = " + BoardID + " AND SendTime BETWEEN DATEADD(Day,-15,GetDate()) AND GETDATE() ORDER by [SendTime] desc";
        grd_BBS.BindGrid(BBS_ForumItemBLL.GetModelList(condition));
        grd_BBS.Attributes["BoardID"] = BoardID.ToString();
    }
    #endregion


    #region 绑定最新知识库
    private void BindNewKB()
    {
        string kbcatalog = ConfigHelper.GetConfigString("DesktopDisplayKBCatalog");
        if (string.IsNullOrEmpty(kbcatalog)) return;

        dl_KB.DataSource = KB_CatalogBLL.GetModelList("ID IN (" + kbcatalog + ")");
        dl_KB.DataBind();

    }
    protected void dl_KB_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            KB_Catalog catalog = (KB_Catalog)e.Item.DataItem;
            UC_GridView gv_KB = (UC_GridView)e.Item.FindControl("gv_KB");

            gv_KB.PageIndex = 0;
            BindKBGridView(gv_KB, catalog.ID);
        }
    }
    protected void gv_KB_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int Catalog = 0;
        int.TryParse(((GridView)sender).Attributes["CatalogID"], out Catalog);
        if (Catalog > 0)
        {
            UC_GridView gv_KB = (UC_GridView)sender;
            gv_KB.PageIndex = e.NewPageIndex;
            BindKBGridView(gv_KB, Catalog);
        }
    }
    private void BindKBGridView(UC_GridView gv_KB, int Catalog)
    {
        DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_OA.dbo.KB_Catalog", "ID", "SuperID", Catalog.ToString());
        string catalogs = Catalog.ToString();
        foreach (DataRow dr in dt.Rows)
        {
            catalogs += "," + dr["ID"].ToString();
        }
        string condition = "Catalog IN (" + catalogs + ") AND IsDelete = 'N' AND HasApproved = 'Y' AND ApproveTime>DATEADD(day,-15,GETDATE()) ORDER BY ApproveTime DESC";

        gv_KB.BindGrid(KB_ArticleBLL.GetModelList(condition));
        gv_KB.Attributes["CatalogID"] = Catalog.ToString();
    }
    #endregion




}

