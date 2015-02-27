using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Common;
public partial class SubModule_EWF_TaskList_CC : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
        DateTime dtBegin = DateTime.Parse(tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(tbx_end.Text).AddDays(1);

        DataTable dt = EWF_Task_JobCCBLL.GetListByRecipientStaff((int)Session["UserID"], dtBegin, dtEnd);

        string condition = " 1 = 1 ";
        if (ddl_App.SelectedValue != "0")
        {
            condition += " AND App = '" + ddl_App.SelectedValue + "'";
        }
        if (tbx_MessageSubject.Text != "")
        {
            condition += " AND (Title LIKE '%" + tbx_MessageSubject.Text + "%' OR MessageSubject LIKE '%" + tbx_MessageSubject.Text + "%')";
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

        if (ddl_ReadFlag.SelectedValue != "0")
        {
            condition += "AND ReadFlag='" + ddl_ReadFlag.SelectedValue+"'" ;
        }

        dt.DefaultView.RowFilter = condition;
        dt.DefaultView.Sort = " TaskID desc ";
        gv_List.DataSource = dt.DefaultView;
        gv_List.TotalRecordCount = dt.DefaultView.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.DataBind();
    }


    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
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
    protected void btn_pass_Click(object sender, EventArgs e)
    {
        int TaskID, JobCCID;
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                TaskID = int.Parse(gv_List.DataKeys[row.RowIndex]["TaskID"].ToString());
                JobCCID = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
                EWF_TaskBLL taskbll = new EWF_TaskBLL(TaskID);
                EWF_Task_JobCCBLL JobCCBLL = new EWF_Task_JobCCBLL(JobCCID);
                if (taskbll.Model.EndTime == new DateTime(1900, 1, 1) && JobCCBLL.Model.ReadFlag != "Y")
                {
                    string comment = "已阅";
                    comment += "<br/>------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "-------<br/>";                  
                    JobCCBLL.SaveComment(comment);

                    #region 更新任务备注信息
                    string remark = "抄送批注时间:<b><font color=red>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</font></b> ";
                    remark += "抄送批注人:<b><font color=red>" + Session["UserRealName"].ToString() + "</font></b> ";
                    remark += " 批注意见：<b><font color=red>已阅</font></b> ";
                    remark += "<br/>";

                 
                    taskbll.AppendRemark(remark);
 
                    #endregion
 
                }
                if (JobCCBLL.Model.ReadFlag != "Y")
                {
                    JobCCBLL.SetReadFlag("Y");
                }              
               
            }
        }
        MessageBox.Show(this, "批量审阅成功！");
        BindGrid();

    }
}
