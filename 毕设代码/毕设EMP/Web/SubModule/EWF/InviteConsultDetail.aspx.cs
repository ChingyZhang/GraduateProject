﻿using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.EWF;

public partial class SubModule_EWF_InviteConsultDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //当前工作项
            ViewState["InviteConsult"] = Request.QueryString["InviteConsult"] == null ? 0 : int.Parse(Request.QueryString["InviteConsult"]);
            ViewState["TaskID"] = Request.QueryString["TaskID"] == null ? 0 : int.Parse(Request.QueryString["TaskID"]);

            if ((int)ViewState["InviteConsult"] == 0)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../DeskTop.aspx");
                return;
            }

            BindTaskData();
        }
    }

    //绑定基本信息
    private void BindTaskData()
    {
        #region 绑定当前工作项
        EWF_Task_InviteConsultBLL InviteConsultBLL = new EWF_Task_InviteConsultBLL((int)ViewState["InviteConsult"]);
        if (InviteConsultBLL.Model == null || InviteConsultBLL.Model.RecipientStaff != (int)Session["UserID"])
        {
            Response.Redirect("~/SubModule/DeskTop.aspx");
            return;
        }

        if (InviteConsultBLL.Model.ReadFlag != "Y")
        {
            InviteConsultBLL.SetReadFlag("Y");
        }

        EWF_Task_JobBLL JobBLL = new EWF_Task_JobBLL(InviteConsultBLL.Model.Job);
        if (JobBLL.Model.Status != 3)
        {
            MessageBox.Show(this, "该流程已不在等待邀请协助审批状态！");
            bt_SaveDecisionComment.Visible = false;
            tbx_DecisionComment.Visible = false;
        }
        //绑定当前邀审相关信息
        lbl_CurrentJobName.Text = new EWF_Flow_ProcessBLL(JobBLL.Model.CurrentProcess).Model.Name;
        lbl_InvitedStaff.Text = new Org_StaffBLL(InviteConsultBLL.Model.InvitedStaff).Model.RealName;
        lbl_InvitedTime.Text = InviteConsultBLL.Model.InvitedTime.ToString("yyyy-MM-dd HH:mm");
        lb_DecisionComment.Text = InviteConsultBLL.Model.ConsultComment;
        #endregion

        #region 绑定流程信息
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);
        ViewState["Initiator"] = task.Model.Initiator;
        //绑定流程信息
        lbl_Applyer.Text = new Org_StaffBLL(task.Model.Initiator).Model.RealName;
        lbl_AppName.Text = new EWF_Flow_AppBLL(task.Model.App).Model.Name;
        lbl_Title.Text = task.Model.Title;
        hyl_RelateURL.NavigateUrl = task.Model.RelateURL;
        lb_Status.Text = DictionaryBLL.GetDicCollections("EWF_Task_TaskStatus")[task.Model.Status.ToString()].Name;

        lb_StartTime.Text = task.Model.StartTime.ToString();
        if (task.Model.EndTime != new DateTime(1900, 1, 1))
            lb_EndTime.Text = task.Model.EndTime.ToString();
        else
            lb_EndTime.Text = "未结束";

        lt_Remark.Text = task.Model.Remark;

        //显示申请详细信息IFrame
        if (!string.IsNullOrEmpty(task.Model.RelateURL))
        {
            tr_RelateUrl.Visible = true;
            string url = this.ResolveClientUrl(task.Model.RelateURL);
            if (url.IndexOf('?') >= 0)
                url += "&ViewFramework=false&Decision=Y";
            else
                url += "?ViewFramework=false&Decision=Y";
            frame_relateurl.Attributes.Add("src", url);
        }
        else
        {
            tr_RelateUrl.Visible = false;
        }

        //Bind the dataobject info
        NameValueCollection dataobjects = task.GetDataObjectValue();
        pl_dataobjectinfo.BindData(dataobjects);
        pl_dataobjectinfo.SetPanelEnable(false);
        TextBox tbx_ReMark = pl_dataobjectinfo.FindControl("C_Remark") != null ? (TextBox)pl_dataobjectinfo.FindControl("C_Remark") : null;
        if (tbx_ReMark != null)
        {
            tbx_ReMark.Enabled = true;
            tbx_ReMark.ReadOnly = true;
        }
        //绑定审批历史
        this.gv_List_DecisionHistory.DataSource = task.GetDecisionHistory();
        this.gv_List_DecisionHistory.DataBind();

        //绑定附件
        gv_List_Attachment.BindGrid<EWF_Task_Attachment>(task.GetAttachmentsList());
        #endregion
    }

    #region 审批任务
    //暂存意见
    protected void bt_SaveDecisionComment_Click(object sender, EventArgs e)
    {
        if (tbx_DecisionComment.Text == "")
        {
            MessageBox.Show(this, "审批意见必填");
            return;
        }

        string comment = tbx_DecisionComment.Text.Trim();
        comment += "<br/>------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "-------<br/>";

        EWF_Task_InviteConsultBLL InviteConsultBLL = new EWF_Task_InviteConsultBLL((int)ViewState["InviteConsult"]);
        InviteConsultBLL.SaveConsultComment(comment);

        #region 更新任务备注信息
        string remark = "协助时间:<b><font color=red>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</font></b> ";
        remark += "协助审批人:<b><font color=red>" + Session["UserRealName"].ToString() + "</font></b> ";
        remark += " 审批意见：<b><font color=red>" + tbx_DecisionComment.Text + "</font></b> ";
        remark += "<br/>";

        EWF_TaskBLL taskbll = new EWF_TaskBLL((int)ViewState["TaskID"]);
        taskbll.AppendRemark(remark);

        if (cbx_NotifyInitiator.Checked)
        {
            string _content = "<b><font color=red>工作流处理通知！您发起的协助邀审有了回复！</font></b><br/>";
            _content += "工作流:<font color=red>" + lbl_AppName.Text + "</font><br/>";
            _content += "主题:<font color=red>" + lbl_Title.Text + "</font><br/><br/>";
            _content += remark;

            _content += "<br/><a href='" + this.ResolveUrl("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + ViewState["TaskID"].ToString()) + "' target='_blank'><font color=blue>点击查看工作流详细情况</font></a><br/>";

            DataTable _users = new Org_StaffBLL(InviteConsultBLL.Model.InvitedStaff).GetUserList();
            foreach (DataRow dr_user in _users.Rows)
            {
                SendSM(dr_user["UserName"].ToString(), _content);
            }
        }
        #endregion

        MessageBox.ShowAndRedirect(this, "保存成功!", "TaskList_InviteConsult.aspx");
    }

    private void SendSM(string Receiver, string Content)
    {
        SM_MsgBLL bll = new SM_MsgBLL();
        bll.Model.Sender = Session["UserName"].ToString();
        bll.Model.Content = Content;
        bll.Model.SendTime = DateTime.Now;
        bll.Model.Type = 1;
        bll.Model.IsDelete = "N";

        int id = bll.Add();

        SM_ReceiverBLL _receiverbll = new SM_ReceiverBLL();
        _receiverbll.Model.MsgID = id;
        _receiverbll.Model.IsRead = "N";
        _receiverbll.Model.IsDelete = "N";
        _receiverbll.Model.Receiver = Receiver;
        _receiverbll.Add();
    }
    #endregion
}
