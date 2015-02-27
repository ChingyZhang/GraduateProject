using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_OA_Journal_SynergeticPlanAndJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbx_begindate.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_enddate.Text = DateTime.Now.AddMonths(1).AddDays(0 - DateTime.Today.Day).ToString("yyyy-MM-dd");

            BindGrid();
        }
        #region 注册脚本
        string script = "function OpenJournal(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?ID='+id+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenJournal", script, true);
        #endregion
    }

    private void BindGrid()
    {
        string ConditionStr1 = "JN_WorkingPlanDetail.BeginTime between '" + tbx_begindate.Text +
            "' And '" + tbx_enddate.Text + " 23:59:59'  AND JN_WorkingPlanDetail.WorkingClassify=2 AND JN_WorkingPlanDetail.RelateStaff=" + Session["UserID"].ToString();

        gv_PlanList.ConditionString = ConditionStr1;
        gv_PlanList.BindGrid();

        string ConditionStr2 = "JN_Journal.BeginTime between '" + tbx_begindate.Text +
            "' AND '" + tbx_enddate.Text + " 23:59:59'  AND JN_Journal.WorkingClassify=2 AND JN_Journal.RelateStaff=" + Session["UserID"].ToString() +
            " AND MCS_SYS.dbo.UF_Spilt(JN_Journal.ExtPropertys,'|',11)='Y'";
        gv_JournalList.ConditionString = ConditionStr2;
        gv_JournalList.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
