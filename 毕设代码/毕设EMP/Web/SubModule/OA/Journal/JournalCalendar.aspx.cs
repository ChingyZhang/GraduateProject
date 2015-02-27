using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.BLL;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_OA_Journal_JournalCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Staff"] != null)
            {
                Org_Staff staff = new Org_StaffBLL(int.Parse(Request.QueryString["Staff"])).Model;
                if (staff != null)
                {
                    MCSTabControl1.SelectedIndex = 1;
                    ViewState["Staff"] = staff.ID;
                }
            }
            else
            {
                lb_Staff.Visible = false;
                select_Staff.Visible = false;
                MCSTabControl1.SelectedIndex = 0;
                ViewState["Staff"] = (int)Session["UserID"];

                #region 如果非总部职位，其只能选择自己职位及以下职位
                if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 22, "ViewAllStaffJN"))
                {
                    //无【查看所有员工工作日志】权限
                    select_Staff.PageUrl += "?Position=" + new Org_StaffBLL((int)Session["UserID"]).Model.Position;
                }
                #endregion
            }

        }

        #region 注册脚本
        string script = "function OpenJournal(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?ID='+id+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenJournal", script, true);

        script = "function NewJournal(d){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?Day='+d+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NewJournal", script, true);
        #endregion
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            lb_Staff.Visible = false;
            select_Staff.Visible = false;
            ViewState["Staff"] = (int)Session["UserID"];
            bt_Add.Enabled = true;
        }
        else
        {
            lb_Staff.Visible = true;
            select_Staff.Visible = true;
            ViewState["Staff"] = null;
            bt_Add.Enabled = false;
        }
    }


    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (ViewState["Staff"] != null && (int)ViewState["Staff"] > 0)
        {
            e.Cell.HorizontalAlign = HorizontalAlign.Left;

            if (e.Day.Date <= DateTime.Today && MCSTabControl1.SelectedIndex == 0)
            {

                ImageButton ibt = new ImageButton();
                ibt.ImageUrl = "~/Images/gif/gif-0489.gif";
                ibt.BorderWidth = new Unit(0);
                ibt.ToolTip = "新增工作日志";
                ibt.OnClientClick = "javascript:NewJournal('" + e.Day.Date.DayOfYear.ToString() + "')";
                ibt.Click += new ImageClickEventHandler(ibt_Click);
                e.Cell.Controls.Add(ibt);
            }

            #region 获取该员工当天工作日志
            if (cbx_Journal.Checked)
            {
                IList<JN_Journal> journallists = JN_JournalBLL.GetModelList("'" + e.Day.Date.ToString("yyyy-MM-dd") + "' BETWEEN Convert(varchar(10),BeginTime,120) And EndTime AND Staff=" + ViewState["Staff"].ToString());
                if (journallists.Count > 0)
                {
                    BulletedList bt = new BulletedList();
                    foreach (JN_Journal j in journallists)
                    {
                        ListItem item = new ListItem();
                        item.Text = "";
                        if (j.JournalType == 1)
                        {
                            //日报
                            if (j.BeginTime.Hour != 0 && j.EndTime.Hour != 0)
                                item.Text += j.BeginTime.ToString("HH:mm") + "~" + j.EndTime.ToString("HH:mm") + " ";
                            if (j.WorkingClassify != 0)
                            {
                                item.Text += DictionaryBLL.GetDicCollections("OA_WorkingClassify")[j.WorkingClassify.ToString()].Name + " ";
                                switch (j.WorkingClassify)
                                {
                                    case 1://门店拜访
                                        if (j.RelateClient != 0)
                                        {
                                            //门店拜访
                                            CM_Client client = new CM_ClientBLL(j.RelateClient).Model;
                                            if (client != null) item.Text += client.ShortName + " ";

                                            if (j["VisitClientPurpose"] != "" && j["VisitClientPurpose"] != "0")
                                                item.Text += DictionaryBLL.GetDicCollections("OA_JN_VisitClientPurpose")[j["VisitClientPurpose"]].Name + " ";
                                        }
                                        break;
                                    case 2: //协同拜访
                                        if (j.RelateStaff != 0)
                                        {
                                            Org_Staff staff = new Org_StaffBLL(j.RelateStaff).Model;
                                            if (staff != null) item.Text += staff.RealName;
                                        }
                                        break;
                                    default:
                                        item.Text += j.Title;
                                        break;
                                }
                            }
                            else
                            {
                                item.Text += j.Title;
                            }
                        }
                        else
                        {
                            //周报、月报
                            item.Text += DictionaryBLL.GetDicCollections("OA_JournalType")[j.JournalType.ToString()].Name + " ";
                            item.Text += j.Title;
                        }
                        item.Value = "javascript:OpenJournal(" + j.ID.ToString() + ")";
                        bt.Items.Add(item);
                    }
                    bt.DisplayMode = BulletedListDisplayMode.HyperLink;
                    bt.BulletImageUrl = "~/Images/gif/gif-0162.gif";
                    bt.BulletStyle = BulletStyle.CustomImage;
                    bt.CssClass = "calaitem";
                    e.Cell.Controls.Add(bt);
                }
            }
            #endregion

            #region 获取访员工当天的工作计划
            if (cbx_Plan.Checked)
            {
                if (ViewState["PlanDetails"] == null || ((IList<JN_WorkingPlanDetail>)ViewState["PlanDetails"]).Where
                    (p => (p.BeginTime.Date <= e.Day.Date && p.EndTime.Date >= e.Day.Date)).Count() == 0)
                {
                    IList<JN_WorkingPlan> plans = JN_WorkingPlanBLL.GetModelList("'" + e.Day.Date.ToString("yyyy-MM-dd") +
                    "' BETWEEN BeginDate AND EndDate AND Staff=" + ViewState["Staff"].ToString());
                    if (plans.Count > 0)
                    {
                        IList<JN_WorkingPlanDetail> plandetails = new JN_WorkingPlanBLL(plans[0].ID).Items;
                        ViewState["PlanDetails"] = plandetails;
                    }
                    else
                        ViewState["PlanDetails"] = null;
                }

                if (ViewState["PlanDetails"] != null)
                {
                    IList<JN_WorkingPlanDetail> plandetails = (IList<JN_WorkingPlanDetail>)ViewState["PlanDetails"];

                    BulletedList bt = new BulletedList();

                    foreach (JN_WorkingPlanDetail plan in plandetails.Where(p => (p.BeginTime.Date <= e.Day.Date && p.EndTime.Date >= e.Day.Date)))
                    {
                        ListItem item = new ListItem();
                        item.Text = "";

                        if (plan.WorkingClassify == 0) continue;
                        item.Text += DictionaryBLL.GetDicCollections("OA_WorkingClassify")[plan.WorkingClassify.ToString()].Name + " ";
                        switch (plan.WorkingClassify)
                        {
                            case 1://门店拜访
                                if (plan.RelateClient != 0)
                                {
                                    //门店拜访
                                    CM_Client client = new CM_ClientBLL(plan.RelateClient).Model;
                                    if (client != null) item.Text += client.ShortName + " ";
                                }
                                break;
                            case 2: //协同拜访
                                if (plan.RelateStaff != 0)
                                {
                                    Org_Staff staff = new Org_StaffBLL(plan.RelateStaff).Model;
                                    if (staff != null) item.Text += staff.RealName + " ";
                                }
                                break;
                            default:
                                item.Text += plan.Description + " ";
                                break;
                        }
                        if (plan.OfficialCity > 1)
                            item.Text += TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", plan.OfficialCity).Replace("->", "");
                        bt.Items.Add(item);
                    }
                    bt.DisplayMode = BulletedListDisplayMode.Text;
                    bt.BulletImageUrl = "~/Images/gif/gif-0163.gif";
                    bt.BulletStyle = BulletStyle.CustomImage;
                    bt.CssClass = "calaitem";
                    e.Cell.Controls.Add(bt);
                }
            }
            #endregion
        }
    }

    void ibt_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
    }
    protected void cbx_Journal_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void cbx_Plan_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void select_Staff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (!string.IsNullOrEmpty(select_Staff.SelectValue))
        {
            ViewState["Staff"] = int.Parse(select_Staff.SelectValue);
        }
        else
        {
            MessageBox.Show(this, "请选择要查看的员工！");
            return;
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime selectdate = Calendar1.SelectedDate;
        if (MCSTabControl1.SelectedIndex == 0)
        {
            Response.Redirect("JournalList.aspx?Date=" + Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
        }
        else
        {
            if (string.IsNullOrEmpty(select_Staff.SelectValue))
            {
                Response.Redirect("JournalList.aspx?Date=" + Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            }
            else
            {
                Response.Redirect("JournalList.aspx?Date=" + Calendar1.SelectedDate.ToString("yyyy-MM-dd") + "&Staff=" + select_Staff.SelectValue);
            }
        }
    }

    protected void bt_ListView_Click(object sender, EventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 0)
            Response.Redirect("JournalList.aspx");
        else if (select_Staff.SelectValue != "")
            Response.Redirect("JournalList.aspx?Staff=" + select_Staff.SelectValue);
    }

}
