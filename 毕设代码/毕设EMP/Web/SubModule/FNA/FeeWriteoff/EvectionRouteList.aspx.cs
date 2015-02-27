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
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;

public partial class SubModule_FNA_FeeWriteoff_EvectionRouteList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");



            BindGrid();
        }

        #region 注册脚本
        //注册修改客户状态按钮script
        string script = "function OpenDetail(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n statuschange = window.showModalDialog('" + Page.ResolveClientUrl("Pop_EvectionRouteDetail.aspx") +
            "?ID='+id+'&tempid='+tempid+'&ViewFramework=false&CloseMode=close', window, 'dialogWidth:800px;DialogHeight=500px;status:yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenDetail", script, true);

        script = "function NewDetail(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n statuschange = window.showModalDialog('" + Page.ResolveClientUrl("Pop_EvectionRouteDetail.aspx") +
            "?tempid='+tempid+'&ViewFramework=false&CloseMode=close', window, 'dialogWidth:800px;DialogHeight=500px;status:yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NewDetail", script, true);
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
        #endregion

        select_Staff.SelectText = staff.Model.RealName;
        select_Staff.SelectValue = staff.Model.ID.ToString();

        ddl_Transport.DataSource = DictionaryBLL.GetDicCollections("FNA_EvectionTransport");
        ddl_Transport.DataBind();
        ddl_Transport.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_Car.DataSource = Car_CarListBLL.GetCarListByOrganizeCity(staff.Model.OrganizeCity);
        ddl_Car.DataBind();
        ddl_Car.Items.Insert(0, new ListItem("请选择", "0"));
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
        string condition = "FNA_EvectionRoute.BeginDate BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59'";

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
            condition += " AND FNA_EvectionRoute.RelateStaff IN (SELECT ID FROM MCS_SYS.dbo.Org_Staff WHERE OrganizeCity IN (" + orgcitys + "))";
        }

        //申请单号
        if (tbx_SheetCode.Text != "")
        {
            condition += " AND FNA_FeeWriteOff.SheetCode like '%" + tbx_SheetCode.Text + "%'";
        }

        //状态
        if (ddl_State.SelectedValue == "1")
            condition += " AND FNA_EvectionRoute.WriteOffID IS NULL ";
        else if (ddl_State.SelectedValue == "2")
            condition += " AND FNA_EvectionRoute.WriteOffID IS NOT NULL ";

        if (ddl_Transport.SelectedValue != "0")
        {
            condition += " AND FNA_EvectionRoute.Transport=" + ddl_Transport.SelectedValue;
        }

        if (ddl_Car.SelectedValue != "0")
        {
            condition += " AND Car_DispatchRide.CarID=" + ddl_Car.SelectedValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }

    protected int getTotalKilometres(object metresStart, object metresEnd)
    {
        int start = 0, end = 0;
        int.TryParse(metresStart.ToString(), out start);
        int.TryParse(metresEnd.ToString(), out end);
        return end - start;
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        gv_List.SelectedIndex = -1;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BindGrid();
    }

    protected void bt_WriteOff_Click(object sender, EventArgs e)
    {
        #region 求选中行的差旅费、住宿费、补贴合计
        decimal Cost1 = 0, Cost2 = 0, Cost3 = 0, Cost4 = 0, Cost5 = 0;
        decimal Cost11 = 0, Cost12 = 0, Cost13 = 0;     //车辆费用
        decimal Cost21 = 0, Cost22 = 0;

        DateTime minbegindate = DateTime.Parse("2999-1-1"), maxenddate = DateTime.Parse("1900-1-1");
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

        #region 新增费用核销单头
        Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;

        FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL();
        bll.Model.SheetCode = FNA_FeeWriteOffBLL.GenerateSheetCode(staff.OrganizeCity);   //自动产生报销单号
        bll.Model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
        bll.Model.FeeType = ConfigHelper.GetConfigInt("EvectionFeeType");                             //差旅费对应的费用类型
        bll.Model.OrganizeCity = staff.OrganizeCity;
        bll.Model.ApproveFlag = 1;
        bll.Model.State = 1;
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model["HasFeeApply"] = "N";                //无申请单
        bll.Model["IsEvectionWriteOff"] = "Y";        //是差旅报销
        #endregion

        #region 新增费用核销单明细
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost1ACTitle"), Cost1, minbegindate, maxenddate);//交通费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost2ACTitle"), Cost2, minbegindate, maxenddate);//住宿费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost3ACTitle"), Cost3, minbegindate, maxenddate);//补贴
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost4ACTitle"), Cost4, minbegindate, maxenddate);//市内交通费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost5ACTitle"), Cost5, minbegindate, maxenddate);//的士费

        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost11ACTitle"), Cost11, minbegindate, maxenddate);//车辆费用-路桥费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost12ACTitle"), Cost12, minbegindate, maxenddate);//车辆费用-油费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("EvectionCost13ACTitle"), Cost13, minbegindate, maxenddate);//车辆费用-其他杂费

        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("TrainingCost1ACTitle"), Cost21, minbegindate, maxenddate);//总部培训费-差旅费
        AddWriteOffDetail(bll, ConfigHelper.GetConfigInt("TrainingCost2ACTitle"), Cost22, minbegindate, maxenddate);//营业部培训费-差旅费
        #endregion

        int writeoff = bll.Add();

        #region 置差旅行程的关联报销单ID
        foreach (int evectionid in selectedevectionids)
        {
            FNA_EvectionRouteBLL evectionbll = new FNA_EvectionRouteBLL(evectionid);
            evectionbll.Model.WriteOffID = writeoff;
            evectionbll.Update();
        }
        #endregion

        Response.Redirect("FeeWriteoffDetail.aspx?ID=" + writeoff.ToString());
    }

    private void AddWriteOffDetail(FNA_FeeWriteOffBLL bll, int AccountTitle, Decimal Cost, DateTime minbegindate, DateTime maxenddate)
    {
        if (Cost == 0) return;

        FNA_FeeWriteOffDetail d = new FNA_FeeWriteOffDetail();
        d.AccountTitle = AccountTitle;
        d.ApplyCost = 0;
        d.WriteOffCost = Cost;
        d.BeginDate = minbegindate;
        d.EndDate = maxenddate;
        d.BeginMonth = bll.Model.AccountMonth;
        d.EndMonth = bll.Model.AccountMonth;
        bll.Items.Add(d);
    }

    protected void cb_NoPage_CheckedChanged(object sender, EventArgs e)
    {
        gv_List.AllowPaging = !cb_NoPage.Checked;
        BindGrid();
    }
}
