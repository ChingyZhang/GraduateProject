using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model;
using System.Data;
using MCSFramework.BLL.OA;

public partial class SubModule_OA_Journal_WorkingPlan_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbx_begindate.Text = DateTime.Today.ToString("yyyy-MM-01");
            tbx_enddate.Text = DateTime.Today.AddMonths(2).AddDays(0 - DateTime.Today.Day).ToString("yyyy-MM-dd");

            BindDropDown();

            if (MCSTabControl1.SelectedIndex == 0)
            {
                tbl_Condition.Visible = false;
            }
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
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

        #region 绑定职位
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 22, "ViewAllStaffPlan"))
        {
            //有【查看所有员工工作计划】权限
            tr_Position.RootValue = "0";
            tr_Position.SelectValue = "0";
        }
        else
        {
            tr_Position.RootValue = staff.Model.Position.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        #endregion

        #endregion
    }
    #endregion

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tbl_Condition.Visible = false;
            bt_New.Enabled = true;
            gv_planList.Visible = false;
        }
        else if (e.Index == 1)
        {
            tbl_Condition.Visible = true;
            bt_New.Enabled = false;
            gv_planList.Visible = false;
        }
        else
        {
            tbl_Condition.Visible = true;
            gv_List.Visible = false;
            gv_planList.Visible = true;
        }
        BindGrid();
    }

    private void BindGrid()
    {
        string ConditionStr = "JN_WorkingPlan.BeginDate Between '" + tbx_begindate.Text + "' AND '" + tbx_enddate.Text + " 23:59' ";


        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                ConditionStr += " AND JN_WorkingPlan.Staff=" + Session["UserID"].ToString();
                gv_List.OrderFields = "JN_WorkingPlan_ID";
                gv_List.ConditionString = ConditionStr;
                gv_List.BindGrid();
                break;
            case "1":
                if (tbx_StaffName.Text != "") ConditionStr += "AND Org_Staff.RealName LIKE '%" + tbx_StaffName.Text + "%'";

                #region 只显示当前员工的所有下级职位的员工计划
                if (tr_Position.SelectValue != "0")
                {
                    if (cb_IncludeChild.Checked || tr_Position.SelectValue == tr_Position.RootValue)
                    {
                        Org_PositionBLL p = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                        string positions = p.GetAllChildPosition();

                        if (tr_Position.SelectValue != tr_Position.RootValue)
                        {
                            if (positions != "") positions += ",";
                            positions += tr_Position.SelectValue;
                        }

                        ConditionStr += " AND Org_Staff.Position IN (" + positions + ")";
                    }
                    else
                        ConditionStr += " AND Org_Staff.Position = " + tr_Position.SelectValue;
                }
                #endregion

                #region 判断当前可查询的管理片区范围
                if (tr_OrganizeCity.SelectValue != "1")
                {
                    string orgcitys = "";
                    Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                    orgcitys = orgcity.GetAllChildNodeIDs();
                    if (orgcitys != "") orgcitys += ",";
                    orgcitys += tr_OrganizeCity.SelectValue;

                    if (orgcitys != "") ConditionStr += " AND JN_WorkingPlan.OrganizeCity IN (" + orgcitys + ") ";
                }
                #endregion
                gv_List.OrderFields = "JN_WorkingPlan_ID";
                gv_List.ConditionString = ConditionStr;
                gv_List.BindGrid();
                break;
            case "2":
                DateTime dt_begin = DateTime.Parse(tbx_begindate.Text);
                DateTime dt_end = DateTime.Parse(tbx_enddate.Text);
                DataTable dtSummary = JN_WorkingPlanBLL.GetSummary(dt_begin, dt_end, int.Parse(tr_OrganizeCity.SelectValue), int.Parse(tr_Position.SelectValue), tbx_StaffName.Text.Trim(), cb_IncludeChild.Checked ? 1 : 0);
                if (dtSummary.Rows.Count == 0)
                {
                    gv_planList.DataBind();
                    return;
                }

                dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "营业部", "办事处", "Staff", "员工姓名", "工号", "职位", "出差天数", "负责客户数", "拜访客户数→计划", "拜访客户数→实际", "差旅费→计划", "差旅费→实际差旅费", "差旅费→实际用车费" },
                                   new string[] { "WorkingClassify" }, "WorkCounts", true, false);

                dtSummary.Rows[dtSummary.Rows.Count - 1]["出差天数"] = dtSummary.Compute("SUM(出差天数)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["负责客户数"] = dtSummary.Compute("SUM(负责客户数)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["拜访客户数→计划"] = dtSummary.Compute("SUM(拜访客户数→计划)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["拜访客户数→实际"] = dtSummary.Compute("SUM(拜访客户数→实际)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["差旅费→计划"] = dtSummary.Compute("SUM(差旅费→计划)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["差旅费→实际差旅费"] = dtSummary.Compute("SUM(差旅费→实际差旅费)", "true");
                dtSummary.Rows[dtSummary.Rows.Count - 1]["差旅费→实际用车费"] = dtSummary.Compute("SUM(差旅费→实际用车费)", "true");

                gv_planList.DataSource = dtSummary;
                gv_planList.DataBind();
                MatrixTable.GridViewMatric(gv_planList);
                gv_planList.Rows[gv_planList.Rows.Count - 1].Cells[0].Text = "";
                break;

        }       
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }


    protected void bt_New_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkingPlan_CalendarList.aspx");
    }   
    protected void gv_planList_DataBound(object sender, EventArgs e)
    {
        if (gv_planList.HeaderRow != null)
        {
            gv_planList.HeaderRow.Cells[3].Visible = false;
            foreach (GridViewRow r in gv_planList.Rows)
            {
                r.Cells[3].Visible = false;

                #region 金额数据格式化
                if (r.Cells.Count > 2)
                {
                    for (int i = 2; i < r.Cells.Count; i++)
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
}
