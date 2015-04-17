// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using System.Collections.Generic;
using MCSFramework.Model.EWF;
using System.Data;
using MCSFramework.BLL;

public partial class SubModule_EWF_TaskList_NeedDecision : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(" EnableFlag='Y' ");
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));

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
            tr_OrganizeCity.SelectValue = "0";
        }
    }

    private void BindGrid()
    {
        DataTable dt = EWF_Task_JobBLL.GetJobToDecision(int.Parse(Session["UserID"].ToString()));

        string condition = " 1 = 1 ";
        if (ddl_App.SelectedValue != "0")
        {
            condition += " AND App = '" + ddl_App.SelectedValue + "'";
        }
        if (tbx_MessageSubject.Text != "")
        {
            condition += " AND (Title like '%" + tbx_MessageSubject.Text + "%' OR MessageSubject like '%" + tbx_MessageSubject.Text + "%')";
        }

        #region 判断当前可查询的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "1" && tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (tbx_InitiatorStaffName.Text != "")
        {
            condition += " AND ApplyStaffName LIKE '%" + tbx_InitiatorStaffName.Text + "%'";
        }

        dt.DefaultView.RowFilter = condition;
        dt.DefaultView.Sort = " TaskID desc ";
        gv_List.DataSource = dt.DefaultView;
        gv_List.TotalRecordCount = dt.DefaultView.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.DataBind();
        cb_SelectAll.Checked = false;
    }


    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                break;
            case "1":
                Response.Redirect("TaskList_HasDecision.aspx");
                break;
            case "2":
                Response.Redirect("TaskList_InitByMe.aspx");
                break;
            case "3":
                Response.Redirect("TaskHistoryList.aspx");
                break;
        }
    }
    protected void ddl_App_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }

    protected void btn_Search_Click(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }


    protected void btn_pass_Click(object sender, System.EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked && cb_check.Visible)
            {
                int DecisionID = (int)gv_List.DataKeys[row.RowIndex]["DecisionID"];
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["CurrentJobID"];
                Decision(2, CurrentJobID, DecisionID);
            }
        }
        BindGrid();
    }

    protected void btn_nopass_Click(object sender, System.EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked && cb_check.Visible)
            {
                int DecisionID = (int)gv_List.DataKeys[row.RowIndex]["DecisionID"];
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["CurrentJobID"];
                Decision(3, CurrentJobID, DecisionID);
            }
        }
        BindGrid();
    }

    private int Decision(int result, int CurrentJobID, int DecisionID)
    {
        EWF_Task_JobBLL job = new EWF_Task_JobBLL(CurrentJobID);
        return job.Decision(DecisionID, (int)Session["UserID"], result, "批量审核");
    }

    protected void cb_SelectAll_CheckedChanged(object sender, System.EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check != null && cb_check.Visible) cb_check.Checked = cb_SelectAll.Checked;
        }
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EWF_Task_Job Job = new EWF_Task_JobBLL((int)gv_List.DataKeys[e.Row.RowIndex]["CurrentJobID"]).Model;
            if (Job == null) return;

            EWF_Flow_Process p = new EWF_Flow_ProcessBLL(Job.CurrentProcess).Model;
            if (p == null) return;

            if (p.Type == 3)
            {
                EWF_Flow_ProcessDecision Process = new EWF_Flow_ProcessDecisionBLL(Job.CurrentProcess).Model;
                if (Process == null) return;

                if (Process.CanBatchApprove == "N")
                {
                    CheckBox cb_Check = (CheckBox)e.Row.FindControl("cb_Check");

                    if (cb_Check != null)
                    {
                        cb_Check.Checked = false;
                        cb_Check.Visible = false;
                    }
                }
            }

        }
    }
}
