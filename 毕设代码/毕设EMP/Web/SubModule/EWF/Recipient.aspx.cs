using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.OA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.EWF;

public partial class SubModule_EWF_Recipient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //当前工作项
            ViewState["CurrentJobID"] = Request.QueryString["CurrentJobID"] == null ? 0 : int.Parse(Request.QueryString["CurrentJobID"]);
            if ((int)ViewState["CurrentJobID"] == 0)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/Index.aspx");
                return;
            }

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();

            BindTaskData();
        }
    }

    private void BindDropDown()
    {
        string condition = " EnableFlag = 'Y'";

        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(condition);
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_Status.DataSource = DictionaryBLL.GetDicCollections("EWF_Task_TaskStatus");
        ddl_Status.DataBind();
        ddl_Status.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    //绑定基本信息
    private void BindTaskData()
    {
        #region 绑定当前工作项
        EWF_Task_JobBLL job = new EWF_Task_JobBLL((int)ViewState["CurrentJobID"]);

        int decisionid = job.StaffCanDecide((int)Session["UserID"]);
        if (decisionid <= 0)
        {
            MessageBox.ShowAndRedirect(this, "对不起，你无权审批当前工作流申请！", "../desktop.aspx");
            return;
        }
        ViewState["DecisionID"] = decisionid;
        ViewState["TaskID"] = job.Model.Task;

        //绑定当前审批人相关信息
        EWF_Flow_Process _CurrentProcess = new EWF_Flow_ProcessBLL(job.Model.CurrentProcess).Model;
        lbl_CurrentJobName.Text = _CurrentProcess.Name;
        if (_CurrentProcess.Type == 10)
        {
            //人员会审环节 无待处理选项
            btn_WaitProcess.Visible = false;
        }

        EWF_Task_JobDecisionBLL decision = new EWF_Task_JobDecisionBLL(decisionid);
        if (decision.Model.ReadFlag != "Y") decision.SetReadFlag("Y");
        lb_DecisionComment.Text = decision.Model.DecisionComment;

        this.lbl_RecipientStaff.Text = new Org_StaffBLL(decision.Model.RecipientStaff).Model.RealName;
        this.lbl_RecipientTime.Text = DateTime.Now.ToString();

        if (decision.Model.RecipientStaff != (int)Session["UserID"])
        {
            ViewState["PrincipalStaff"] = lbl_RecipientStaff.Text;
            MessageBox.Show(this, "请注意，当前申请是由【" + lbl_RecipientStaff.Text + "】授权您来批复！");
        }
        #endregion

        #region 绑定流程信息
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        if (task.Model.Status == 5)
        {
            Response.Redirect("TaskDetail.aspx?TaskID=" + task.Model.ID.ToString());
            return;
        }

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

        ddl_App.SelectedValue = task.Model.App.ToString();

        ViewState["PageIndex"] = 0;
        BindGrid_OtherTask();
        BindGrid_InviteConsult();
    }

    #region 审批任务
    //暂存意见
    protected void bt_SaveDecisionComment_Click(object sender, EventArgs e)
    {
        if (Decision(1) > 0)
            MessageBox.ShowAndRedirect(this, "暂挂操作成功！", "TaskList_NeedDecision.aspx");
        else
            MessageBox.ShowAndRedirect(this, "审批失败！", "TaskList_NeedDecision.aspx");
    }

    //审批通过
    protected void btn_Pass_Click(object sender, EventArgs e)
    {
        if (Decision(2) > 0)
            MessageBox.ShowAndRedirect(this, "设置为审批通过操作成功！", "TaskList_NeedDecision.aspx");
        else
            MessageBox.ShowAndRedirect(this, "审批失败！", "TaskList_NeedDecision.aspx");
    }

    //审批不通过
    protected void btn_NotPass_Click(object sender, EventArgs e)
    {
        if (Decision(3) > 0)
            MessageBox.ShowAndRedirect(this, "设置为审批不通过操作成功！", "TaskList_NeedDecision.aspx");
        else
            MessageBox.ShowAndRedirect(this, "审批失败！", "TaskList_NeedDecision.aspx");
    }

    //待处理
    protected void btn_WaitProcess_Click(object sender, EventArgs e)
    {
        if (Decision(4) > 0)
            MessageBox.ShowAndRedirect(this, "设置为审批待处理操作成功！", "TaskList_NeedDecision.aspx");
        else
            MessageBox.ShowAndRedirect(this, "审批失败！", "TaskList_NeedDecision.aspx");
    }

    private int Decision(int result)
    {
        string decisionComment = tbx_DecisionComment.Text.Trim() == "" ? "已阅" : tbx_DecisionComment.Text.Trim();
        EWF_Task_JobBLL job = new EWF_Task_JobBLL((int)ViewState["CurrentJobID"]);
        int ret = 0;

        if (result != 1)
        {
            ret = job.Decision((int)ViewState["DecisionID"], (int)Session["UserID"], result, decisionComment);
        }
        else
        {
            decisionComment += "<br/>------" + Session["UserRealName"].ToString() + " 于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "批注意见-----<br/>";
            ret = job.SaveDecisionComment((int)ViewState["DecisionID"], (int)Session["UserID"], decisionComment);
        }

        #region 通知任务发起人
        if (cbx_NotifyInitiator.Checked || result == 4)
        {
            string remark = "审批时间:<b><font color=blue>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</font></b> ";
            remark += "审批人:<b><font color=blue>" + Session["UserRealName"].ToString() + "</font></b> ";

            if (ViewState["PrincipalStaff"] != null)
            {
                remark += " <b><font color=red>授权人：" + ViewState["PrincipalStaff"].ToString() + "</font></b> ";
            }

            switch (result)
            {
                case 1:
                    remark += " 审批结果：<b><font color=blue>暂挂审批</font></b>";
                    break;
                case 2:
                    remark += " 审批结果：<b><font color=blue>审批通过</font></b>";
                    break;
                case 3:
                    remark += " 审批结果：<b><font color=blue>审批不通过</font></b>";
                    break;
                case 4:
                    remark += " 审批结果：<b><font color=blue>审批待处理</font></b>";
                    break;
                default:
                    break;
            }
            remark += " 审批意见：<b><font color=blue>" + decisionComment + "</font></b><br/> ";

            EWF_TaskBLL taskbll = new EWF_TaskBLL(job.Model.Task);

            string _content = "<b><font color=blue>工作流处理通知！</font></b><br/>";
            _content += "工作流:<font color=blue>" + lbl_AppName.Text + "</font><br/>";
            _content += "主题:<font color=blue>" + lbl_Title.Text + "</font><br/><br/>";
            _content += decisionComment;

            _content += "<br/><a href='" + this.ResolveUrl("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + job.Model.Task.ToString()) + "' target='_blank'><font color=blue>点击查看工作流详细情况</font></a><br/>";

            DataTable _users = new Org_StaffBLL(taskbll.Model.Initiator).GetUserList();
            foreach (DataRow dr_user in _users.Rows)
            {
                SendSM(dr_user["UserName"].ToString(), _content);
            }
        }
        #endregion

        return ret;
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGrid_OtherTask();
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid_OtherTask();
    }

    private void BindGrid_OtherTask()
    {
        Guid App = Guid.Empty;
        if (ddl_App.SelectedValue != "0") App = new Guid(ddl_App.SelectedValue);

        IList<EWF_Task> list = EWF_TaskBLL.GetByInitiator(App, (int)ViewState["Initiator"], DateTime.Parse(tbx_begin.Text),
            DateTime.Parse(tbx_end.Text).AddDays(1), int.Parse(ddl_Status.SelectedValue), 0);

        try
        {
            list.Remove(list.First<EWF_Task>(item => item.ID == (int)ViewState["TaskID"]));
        }
        catch { }

        gv_List.TotalRecordCount = list.Count;
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        gv_List.BindGrid<EWF_Task>(list);
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int TaskID = (int)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        Response.Redirect("TaskDetail.aspx?TaskID=" + TaskID.ToString());
    }

    protected string StaffCanApproveTask(string TaskID)
    {
        return EWF_TaskBLL.StaffCanApproveTask(int.Parse(TaskID), (int)Session["UserID"]).ToString();
    }


    protected void bt_AddInviteConsult_Click(object sender, EventArgs e)
    {
        if (select_RecipientStaff.SelectValue == "" || select_RecipientStaff.SelectValue == "0")
        {
            MessageBox.Show(this, "请选择要协助的员工！");
            return;
        }

        string[] recipients = select_RecipientStaff.SelectValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in recipients)
        {
            int recipient = 0;
            if (int.TryParse(s, out recipient) && recipient > 0)
            {
                EWF_Task_InviteConsultBLL invite = new EWF_Task_InviteConsultBLL();
                invite.Model.Job = (int)ViewState["CurrentJobID"];
                invite.Model.InvitedTime = DateTime.Now;
                invite.Model.InvitedStaff = (int)Session["UserID"];
                invite.Model.RecipientStaff = recipient;
                invite.Model.ReadFlag = "N";
                invite.Model.MessageSubject = tbx_InvitedMessageSubject.Text;
                invite.Add();
            }
        }


        BindGrid_InviteConsult();
    }

    private void BindGrid_InviteConsult()
    {
        gv_InviteConsult.BindGrid<EWF_Task_InviteConsult>(new EWF_Task_JobBLL((int)ViewState["CurrentJobID"]).GetInviteConsultInfo());
    }
    protected void btn_Up_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string fullfilename = FileUpload1.FileName;

                if (fullfilename.LastIndexOf(".") < 0)
                {
                    MessageBox.Show(this, "无法识别的附件格式!");
                    return;
                }

                int FileSize = (FileUpload1.PostedFile.ContentLength / 1024);
                if (FileSize > ConfigHelper.GetConfigInt("MaxAttachmentSize"))
                {
                    MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                        "KB!当前上传文件大小为:" + FileSize.ToString() + "KB");
                    return;
                }
                string extendname = fullfilename.Substring(fullfilename.LastIndexOf(".") + 1).ToLowerInvariant();
                string filename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));

                byte[] filedata;
                Stream filestream = FileUpload1.PostedFile.InputStream;

                #region 自动压缩上传的图片
                if (ATMT_AttachmentBLL.IsImage(extendname))
                {
                    try
                    {
                        System.Drawing.Image originalImage = System.Drawing.Image.FromStream(filestream);
                        filestream.Position = 0;

                        int width = originalImage.Width;

                        if (width > 1024 || extendname == "bmp")
                        {
                            if (width > 1024) width = 1024;

                            System.Drawing.Image thumbnailimage = ImageProcess.MakeThumbnail(originalImage, width, 0, "W");

                            MemoryStream thumbnailstream = new MemoryStream();
                            thumbnailimage.Save(thumbnailstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            thumbnailstream.Position = 0;
                            FileSize = (int)(thumbnailstream.Length / 1024);
                            extendname = "jpg";

                            filestream = thumbnailstream;
                        }
                    }
                    catch { }
                }
                #endregion

                filedata = new byte[filestream.Length];
                filestream.Read(filedata, 0, (int)filestream.Length);
                filestream.Close();

                EWF_Task_AttachmentBLL ta = new EWF_Task_AttachmentBLL();
                ta.Model.Task = (int)ViewState["TaskID"];
                ta.Model.Name = this.tbx_AttachmentName.Text.Trim();
                if (ta.Model.Name == "") ta.Model.Name = filename;
                ta.Model.Description = this.tbx_AttachmentDescription.Text.Trim();
                //ta.Model.FilePath = saveasfilename;
                ta.Model.UploadStaff = int.Parse(Session["UserID"].ToString());
                ta.Model.FileSize = FileUpload1.PostedFile.ContentLength / 1024;
                ta.Model.FileType = extendname.ToUpper();

                if (ta.Add(filedata) > 0)
                {
                    BindAttachmentList();
                }
                else
                {
                    MessageBox.Show(this, "附件上传失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
        else
        {
            MessageBox.Show(this, "未选择要上传的附件！");
            return;
        }
    }

    private void BindAttachmentList()
    {
        //绑定附件
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);
        gv_List_Attachment.BindGrid<EWF_Task_Attachment>(task.GetAttachmentsList());
    }

    protected void gv_List_Attachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List_Attachment.DataKeys[e.RowIndex]["ID"];
        EWF_Task_AttachmentBLL att = new EWF_Task_AttachmentBLL(id);

        if (att.Model.UploadStaff == (int)Session["UserID"])
        {
            att.Delete();
            BindAttachmentList();
        }
        else
        {
            MessageBox.Show(this, "对不起，您只能删除您自己上传的附件!");
        }
    }
}
