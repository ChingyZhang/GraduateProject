using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.FNA;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;

public partial class SubModule_FNA_FeeWriteoff_Pop_EvectionRoteList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["WriteOffID"] = Request.QueryString["WriteOffID"] == null ? 0 : int.Parse(Request.QueryString["WriteOffID"]);

            if ((int)ViewState["WriteOffID"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            FNA_FeeWriteOff writeoff = new FNA_FeeWriteOffBLL((int)ViewState["WriteOffID"]).Model;
            if (writeoff == null)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (writeoff.State > 1)
            {
                //已提交
                bt_Add.Visible = false;
                bt_Remove.Visible = false;
                MCSTabControl1.Visible = false;
                tb_condition.Visible = false;
                bt_Find.Visible = false;
                bt_Save.Visible = false;
                gv_List.Columns[0].Visible = false;
            }

            #region 绑定用户可管辖的管理片区
            Org_StaffBLL staff = new Org_StaffBLL(writeoff.InsertStaff);
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

            select_Staff.SelectText = staff.Model.RealName;
            select_Staff.SelectValue = staff.Model.ID.ToString();


            BindGrid();
        }

        #region 注册脚本
        //注册修改客户状态按钮script
        string script = "function OpenJournal(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n statuschange = window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/OA/Journal/JournalDetail.aspx") +
            "?ID='+id+'&tempid='+tempid+'&ViewFramework=false&CloseMode=close', window, 'dialogWidth:800px;DialogHeight=500px;status:yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenJournal", script, true);

        script = "function OpenWorkingPlan(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n statuschange = window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/OA/Journal/JournalOnWorkingPlan.aspx") +
            "?PlanID='+id+'&tempid='+tempid+'&ViewFramework=false&CloseMode=close', window, 'dialogWidth:960px;DialogHeight=600px;status:yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenWorkingPlan", script, true);
        #endregion
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "" && tr_OrganizeCity.SelectValue != "1")
        {
            select_Staff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx";
        }
    }
    #endregion

    private void BindGrid()
    {
        string condition = "";
        if (MCSTabControl1.SelectedIndex == 0)
        {
            tb_condition.Visible = false;
            bt_Add.Enabled = false;
            bt_Remove.Enabled = true;
            gv_List.Visible = true;
            gv_WorkPlanDetailList.Visible = false;

            condition = "FNA_EvectionRoute.WriteOffID=" + ViewState["WriteOffID"].ToString();
            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else if (MCSTabControl1.SelectedIndex == 1)
        {
            tb_condition.Visible = true;
            bt_Add.Enabled = true;
            bt_Remove.Enabled = false;
            gv_List.Visible = true;
            gv_WorkPlanDetailList.Visible = false;

            condition = "FNA_EvectionRoute.BeginDate BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59' ";
            condition += " AND FNA_EvectionRoute.WriteOffID IS NULL  AND FNA_EvectionRoute.InsertStaff=" + Session["UserID"].ToString();

            #region 组织查询条件
            if (select_Staff.SelectValue != "")
            {
                condition += "AND FNA_EvectionRoute.RelateStaff = " + select_Staff.SelectValue;
            }
            else if (tr_OrganizeCity.SelectValue != "1")   //管理片区及所有下属管理片区
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;
                condition += " AND FNA_EvectionRoute.RelateStaff IN (SELECT MCS_SYS.dbo.Org_Staff WHERE OrganizeCity IN (" + orgcitys + "))";
            }
            #endregion

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else
        {
            tb_condition.Visible = true;
            bt_Add.Enabled = false;
            bt_Remove.Enabled = false;
            gv_List.Visible = false;
            gv_WorkPlanDetailList.Visible = true;
            condition = "JN_WorkingPlan.State=3 AND JN_WorkingPlan.Staff=" + select_Staff.SelectValue +
                " AND JN_WorkingPlanDetail.BeginTime<'" + tbx_end.Text + "' AND JN_WorkingPlanDetail.EndTime>='" + tbx_begin.Text +
                "' AND (MCS_SYS.dbo.UF_Spilt(JN_WorkingPlanDetail.ExtPropertys,'|',1)>'0' OR MCS_SYS.dbo.UF_Spilt(JN_WorkingPlanDetail.ExtPropertys,'|',2)>'0')";
            gv_WorkPlanDetailList.ConditionString = condition;
            gv_WorkPlanDetailList.BindGrid();
        }

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        ReComputeCost(1);
        BindGrid();
    }
    protected void bt_Remove_Click(object sender, EventArgs e)
    {
        ReComputeCost(2);

        BindGrid();
    }

    private void ReComputeCost(int flag)
    {
        decimal Cost1 = 0, Cost2 = 0, Cost3 = 0, Cost4 = 0, Cost5 = 0;
        decimal Cost11 = 0, Cost12 = 0, Cost13 = 0;     //车辆费用
        decimal Cost21 = 0, Cost22 = 0;                 //培训差旅费
        DateTime minbegindate = DateTime.Parse("2999-1-1"), maxenddate = DateTime.Parse("1900-1-1");

        #region 求选中行的差旅费、住宿费、补贴合计
        List<int> selectedevectionids = new List<int>();

        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex][0];
                FNA_EvectionRoute evection = new FNA_EvectionRouteBLL(id).Model;

                #region 根据关联的工作日志的类型,判断是否培训费
                //获取关联的日志类型
                int journalid = 0, workingclassify = 0;
                if (int.TryParse(evection["RelateJournal"], out journalid) && journalid > 0)
                {
                    JN_Journal Joural = new JN_JournalBLL(journalid).Model;
                    if (Joural != null) workingclassify = Joural.WorkingClassify;
                }

                switch (workingclassify)
                {
                    case 4:     //总部组织的培训，计入总部培训-差旅费
                        Cost21 += evection.Cost1 + evection.Cost2 + evection.Cost3 + evection.Cost4 + evection.Cost5;
                        break;
                    case 5:     //省区组织的培训，计入营业部培训-差旅费
                        Cost22 += evection.Cost1 + evection.Cost2 + evection.Cost3 + evection.Cost4 + evection.Cost5;
                        break;
                    default:
                        Cost1 += evection.Cost1;      //交通费
                        Cost2 += evection.Cost2;      //住宿费
                        Cost3 += evection.Cost3;      //补贴
                        Cost4 += evection.Cost4;      //市内交通费
                        Cost5 += evection.Cost5;      //的士费
                        break;
                }
                #endregion

                if (minbegindate > evection.BeginDate) minbegindate = evection.BeginDate;
                if (maxenddate < evection.EndDate) maxenddate = evection.EndDate;

                int cardispatchid = 0;
                if (int.TryParse(gv_List.DataKeys[row.RowIndex]["Car_DispatchRide_ID"].ToString(), out cardispatchid)
                    && cardispatchid > 0)
                {
                    Car_DispatchRide dispatch = new Car_DispatchRideBLL(cardispatchid).Model;
                    if (dispatch != null)
                    {
                        Cost11 += dispatch.RoadToll;    //过路过桥费
                        Cost12 += dispatch.FuelFee;     //油费
                        Cost13 += dispatch.ParkingFee;  //停车费
                        Cost13 += dispatch.OtherFee;    //其他费
                    }
                }
                selectedevectionids.Add(id);
            }
        }
        if (Cost1 + Cost2 + Cost3 + Cost4 + Cost5 + Cost11 + Cost12 + Cost13 + Cost21 + Cost22 == 0)
        {
            MessageBox.Show(this, "对不起，请勾选要报销的差旅行程记录！");
            return;
        }
        #endregion

        #region 重设科目金额
        FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL((int)ViewState["WriteOffID"]);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost1ACTitle"), Cost1, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost2ACTitle"), Cost2, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost3ACTitle"), Cost3, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost4ACTitle"), Cost4, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost5ACTitle"), Cost5, flag, minbegindate, maxenddate);

        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost11ACTitle"), Cost11, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost12ACTitle"), Cost12, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("EvectionCost13ACTitle"), Cost13, flag, minbegindate, maxenddate);

        UpdateDetail(bll, ConfigHelper.GetConfigInt("TrainingCost1ACTitle"), Cost21, flag, minbegindate, maxenddate);
        UpdateDetail(bll, ConfigHelper.GetConfigInt("TrainingCost2ACTitle"), Cost22, flag, minbegindate, maxenddate);
        #endregion

        #region 置差旅行程的关联报销单ID
        foreach (int evectionid in selectedevectionids)
        {
            FNA_EvectionRouteBLL evectionbll = new FNA_EvectionRouteBLL(evectionid);
            if (flag == 1)
                evectionbll.Model.WriteOffID = (int)ViewState["WriteOffID"];
            else
                evectionbll.Model.WriteOffID = 0;
            evectionbll.Update();
        }
        #endregion
    }
    private void UpdateDetail(FNA_FeeWriteOffBLL bll, int AccountTitle, Decimal Cost, int flag, DateTime minbegindate, DateTime maxenddate)
    {
        if (Cost == 0) return;
        FNA_FeeWriteOffDetail d = bll.Items.FirstOrDefault(p => p.AccountTitle == AccountTitle);
        if (d != null)
        {
            if (flag == 1)
            {
                d.WriteOffCost += Cost;
                if (d.BeginDate > minbegindate) d.BeginDate = minbegindate;
                if (d.EndDate < maxenddate) d.EndDate = maxenddate;
            }
            else
            {
                d.WriteOffCost -= Cost;
            }

            if (d.WriteOffCost == 0)
                bll.DeleteDetail(d.ID);
            else
                bll.UpdateDetail(d);
        }
        else
        {
            if (flag == 1)
            {
                d = new FNA_FeeWriteOffDetail();
                d.AccountTitle = AccountTitle;
                d.ApplyCost = 0;
                d.WriteOffCost = Cost;
                d.BeginDate = minbegindate;
                d.EndDate = maxenddate;
                d.BeginMonth = bll.Model.AccountMonth;
                d.EndMonth = bll.Model.AccountMonth;
                bll.Items.Add(d);
                bll.AddDetail(d);
            }
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
    }

}
