using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Common;
using MCSControls.MCSWebControls;

public partial class SubModule_EWF_ApproveAgencyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                MCSSelectControl select_PrincipalStaff = (MCSSelectControl)pl_detail.FindControl("EWF_ApproveAgency_PrincipalStaff");
                if (select_PrincipalStaff != null)
                {
                    select_PrincipalStaff.SelectText = Session["UserRealName"].ToString();
                    select_PrincipalStaff.SelectValue = Session["UserID"].ToString();
                }
                TextBox tbx_BeginDate = (TextBox)pl_detail.FindControl("EWF_ApproveAgency_BeginDate");
                if (tbx_BeginDate != null) tbx_BeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                Label lb_EnableFlag = (Label)pl_detail.FindControl("EWF_ApproveAgency_EnableFlag");
                if (lb_EnableFlag != null) lb_EnableFlag.Text = "有效";

                BindAppList(false);
                tr_AppList.Visible = true;
                bt_Disable.Visible = false;
            }
            else
            {
                BindData();
            }
        }
    }

    private void BindData()
    {
        EWF_ApproveAgency m = new EWF_ApproveAgencyBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(m);

        if (m.BeginDate < DateTime.Today)
        {
            TextBox tbx_BeginDate = (TextBox)pl_detail.FindControl("EWF_ApproveAgency_BeginDate");
            if (tbx_BeginDate != null) tbx_BeginDate.Enabled = false;
        }

        if (m.EnableFlag == "N" || m.PrincipalStaff != (int)Session["UserID"])
        {
            pl_detail.SetControlsEnable(false);
            bt_Save.Visible = false;
            bt_Disable.Visible = false;
        }
    }

    private void BindAppList(bool BindAllEWFApp)
    {
        IList<EWF_Flow_App> Apps;
        if (!BindAllEWFApp)
            Apps = EWF_Flow_AppBLL.GetModelList(@"EnableFlag='Y' AND ID IN (SELECT DISTINCT EWF_Task.App
            FROM EWF_Task_Job INNER JOIN
                  EWF_Task_JobDecision ON EWF_Task_Job.ID = EWF_Task_JobDecision.Job INNER JOIN
                  EWF_Task ON EWF_Task_Job.Task = EWF_Task.ID
            WHERE EWF_Task_JobDecision.RecipientStaff=" + Session["UserID"].ToString() + " AND EWF_Task_Job.StartTime>DATEADD(MONTH,-3,GETDATE()) )");
        else
            Apps = EWF_Flow_AppBLL.GetModelList("EnableFlag='Y' ");

        cbx_AppList.Items.Clear();
        foreach (EWF_Flow_App app in Apps.OrderBy(p => p.Name))
        {
            cbx_AppList.Items.Add(new ListItem(app.Name, app.ID.ToString()));
        }
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindAppList(true);
        btn_Search.Visible = false;
    }
    protected void cbx_All_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbx_AppList.Items)
        {
            item.Selected = cbx_All.Checked;
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 0)
        {
            EWF_ApproveAgencyBLL bll = new EWF_ApproveAgencyBLL();
            pl_detail.GetData(bll.Model);

            if (bll.Model.AgentStaff == 0)
            {
                MessageBox.Show(this, "请选择代理人!");
                return;
            }

            if (bll.Model.BeginDate < DateTime.Today)
            {
                MessageBox.Show(this, "起始日期不能小于今天!");
                return;
            }

            if (bll.Model.EndDate.Year != 1900 && bll.Model.BeginDate > bll.Model.EndDate)
            {
                MessageBox.Show(this, "截止日期不能小于起始日期!");
                return;
            }

            if (cbx_AppList.SelectedIndex == -1)
            {
                MessageBox.Show(this, "请选择待授权的工作流!");
                return;
            }

            bll.Model.EnableFlag = "Y";
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = (int)Session["UserID"];
            foreach (ListItem item in cbx_AppList.Items)
            {
                if (item.Selected)
                {
                    bll.Model.App = new Guid(item.Value);
                    bll.Add();
                }
            }
        }
        else
        {
            EWF_ApproveAgencyBLL bll = new EWF_ApproveAgencyBLL((int)ViewState["ID"]);

            DateTime orgBeginDate = bll.Model.BeginDate;
            DateTime orgEndDate = bll.Model.EndDate;

            pl_detail.GetData(bll.Model);

            if (bll.Model.BeginDate < DateTime.Today && bll.Model.BeginDate != orgBeginDate)
            {
                MessageBox.Show(this, "起始日期不能小于今天!");
                return;
            }
            if (bll.Model.EndDate < DateTime.Today && bll.Model.EndDate != orgEndDate)
            {
                MessageBox.Show(this, "截止日期不能小于今天!");
                return;
            }

            if (bll.Model.AgentStaff == 0)
            {
                MessageBox.Show(this, "请选择代理人!");
                return;
            }

            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();
        }

        Response.Redirect("ApproveAgencyList.aspx");
    }



    protected void bt_Disable_Click(object sender, EventArgs e)
    {
        EWF_ApproveAgencyBLL bll = new EWF_ApproveAgencyBLL((int)ViewState["ID"]);
        bll.Model.EnableFlag = "N";
        bll.Model.UpdateStaff = (int)Session["UserID"];
        bll.Update();

        MessageBox.ShowAndRedirect(this, "失效成功", "ApproveAgencyList.aspx");
    }
}
